using Bliki.Interfaces;
using Markdig.Syntax;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Bliki.Data
{
    public class PageManager
    {
        private readonly IGitManager _gitManager;

        public PageManager(IGitManager gitManager)
        {
            _gitManager = gitManager;
            FindWikiPageDirectory(Directory.GetCurrentDirectory());
        }

        public PageManager(IGitManager gitManager, string wikiDirectory)
        {
            _gitManager = gitManager;
            _wikiPageDirectory = wikiDirectory;
        }

        public bool SavePage(WikiPageModel model, string? userName)
        {
            try
            {
                if (model.Title == null)
                {
                    model.Title = "Unnamed";
                }

                if (model.PageLink != CreatePageLink(model.Title))
                {
                    DeletePage(model.PageLink, userName);
                    model.PageLink = CreatePageLink(model.Title);
                }

                var file = BuildMarkdownFileContent(model);
                var savePath = GetFilePath(model.PageLink);
                File.WriteAllText(savePath, file);
                Task.Run(async () =>
                {
                    await _gitManager.Commit(model.PageLink, userName);
                });
                return true;
            }
            catch(Exception ex)
            {
                LogException(ex);
                return false;
            }
        }

        public IList<NavPageMeta> GetNavMenuMetas()
        {
            var filePaths = Directory.GetFiles(_wikiPageDirectory);
            var result = new List<NavPageMeta>();

            foreach (var path in filePaths)
            {
                var fileName = Path.GetFileName(path).Replace(".md", "");
                if (fileName == "bliki.exceptions") continue;
                result.Add(new NavPageMeta(CreatePageTitle(fileName), fileName));
            }

            return result;
        }


        public IList<NavPageMeta> GetCurrentPageHeaders(string pageLink)
        {
            var pageModel = LoadPage(string.IsNullOrEmpty(pageLink) ? "home" : pageLink);
            var result = new List<NavPageMeta>();
            var mdDoc = Markdig.Markdown.Parse(pageModel.Content);
            var tw = new StringWriter();
            foreach (var block in mdDoc)
            {
                if (block is HeadingBlock heading)
                {
                    var content = heading.Inline.FirstChild.ToString();
                    
                    if (content != null)
                    {
                        result.Add(new NavPageMeta(content, CreatePageLink(content)));
                    }
                }
            }

            return result;
        }


        public WikiPageModel LoadPage(string pageLink)
        {
            if (string.IsNullOrEmpty(pageLink))
            {
                pageLink = "home";
            }
            var filePath = GetFilePath(pageLink);
            
            if (File.Exists(filePath))
            {
                return BuildPageModel(filePath);
            }

            return new WikiPageModel { Content = "# New Page", PageLink = pageLink, Title = "New Page" };
        }


        private string GetFilePath(string pageLink)
        {
            return Path.Combine(_wikiPageDirectory, $"{pageLink}.md");
        }


        private WikiPageModel BuildPageModel(string filePath)
        {
            var content = File.ReadAllText(filePath);
            var titleRgx = new Regex("<!-- TITLE: (.*) -->");
            var titleMatch = titleRgx.Match(content);
            var title = CreatePageTitle(Path.GetFileNameWithoutExtension(filePath));
            if (titleMatch.Success) 
            {
                title = titleMatch.Result("$1");
                content = titleRgx.Replace(content, "");
            }
            
            var subTitleRgx = new Regex("<!-- SUBTITLE: (.*) -->");
            var subTitle = "A Special Page";
            var subMatch = subTitleRgx.Match(content);
            if (subMatch.Success)
            {
                subTitle = subMatch.Result("$1");
                content = subTitleRgx.Replace(content, "");
            }

            content = content.Trim();
            
            return new WikiPageModel
            {
                Content = content,
                Title = title,
                SubTitle = subTitle,
                PageLink = CreatePageLink(title)
            };
        }


        private string BuildMarkdownFileContent(WikiPageModel model)
        {
            return $@"<!-- TITLE: {model.Title} -->
<!-- SUBTITLE: {model.SubTitle} -->
{model.Content}";
        }


        private string CreatePageLink(string title)
        {
            var link = title.Replace(' ', '-').ToLowerInvariant();
            return HttpUtility.UrlEncode(link);
        }

        private string CreatePageTitle(string pageLink)
        {
            var decode = HttpUtility.UrlDecode(pageLink);
            var title = decode.Replace('-', ' ');
            var textInfo = CultureInfo.CurrentCulture.TextInfo;

            return textInfo.ToTitleCase(title);
        }


        private void LogException(Exception ex)
        {
            var exceptionPath = GetFilePath("bliki.exceptions");
            File.AppendAllText(exceptionPath, ex.Message);
        }


        private void FindWikiPageDirectory(string path)
        {
            try
            {
                var wikiPath = Path.Combine(path, "WikiPages");
                _wikiPageDirectory = wikiPath;
                if (!Directory.Exists(wikiPath))
                {
                    Directory.CreateDirectory(wikiPath);
                    SavePage(new WikiPageModel
                    {
                        Title = "Home",
                        PageLink = "home",
                        Content = "# Welcome to Bliki!\n## The Blazor-Powered Wiki"
                    }, "Bliki");
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }


        public void DeletePage(string link, string? userName)
        {
            var path = GetFilePath(link);
            if (File.Exists(path))
            {
                File.Delete(path);
                Task.Run(async () =>
                {
                    await _gitManager.Commit(link, userName, true);
                });
            }
        }

        private string _wikiPageDirectory = @"WikiPages";
    }
}
