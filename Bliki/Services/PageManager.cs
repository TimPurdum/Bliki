using Bliki.Interfaces;
using Markdig;
using Markdig.Syntax;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;


namespace Bliki.Data
{
    public class PageManager
    {
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
                    DeletePage(model.PageLink, userName, model.Folder);
                    model.PageLink = CreatePageLink(model.Title);
                }

                var file = BuildMarkdownFileContent(model);
                var savePath = GetFilePath(model.PageLink, model.Folder);

                File.WriteAllText(savePath, file);
                Task.Run(async () => { await _gitManager.Commit(model.PageLink, userName); });
                if (_editingSessions.FirstOrDefault(s => s.PageModel.Equals(model)) is
                    EditingSession session)
                {
                    _editingSessions.Remove(session);
                }

                if (_loadedPages.Contains(model))
                {
                    _loadedPages.Remove(model);
                }

                _loadedPages.Add(model);

                return true;
            }
            catch (Exception ex)
            {
                LogException(ex);
                return false;
            }
        }


        public IList<NavPageMeta> GetNavMenuMetas()
        {
            var folders = Directory.GetDirectories(_wikiPageDirectory);
            var filePaths = Directory.GetFiles(_wikiPageDirectory).ToList();
            foreach (var folder in folders)
            {
                if (filePaths.Any(fp => fp.Contains(folder)))
                {
                    var parentFile = filePaths.First(fp => fp.Contains(folder));
                    var subFiles = Directory.GetFiles(folder);
                    var index = filePaths.IndexOf(parentFile);
                    filePaths.InsertRange(index + 1, subFiles);
                }
            }

            var result = new List<NavPageMeta>();

            foreach (var path in filePaths)
            {
                var folder = Path.GetFileName(Path.GetDirectoryName(path));
                if (folder != null && folder.EndsWith("WikiPages"))
                {
                    folder = null;
                }

                result.Add(GetNavMenuItem(path, folder));
            }

            return result;
        }


        public IList<NavPageMeta> GetCurrentPageHeaders(string pageLink, string? folder)
        {
            var pageModel = LoadPage(string.IsNullOrEmpty(pageLink) ? "home" : pageLink, folder);
            var result = new List<NavPageMeta>();
            var mdDoc = Markdown.Parse(pageModel.Content);
            var tw = new StringWriter();
            foreach (var block in mdDoc)
            {
                if (block is HeadingBlock heading)
                {
                    var content = heading.Inline.FirstChild.ToString();

                    if (content != null)
                    {
                        result.Add(new NavPageMeta(content, CreatePageLink(content), null));
                    }
                }
            }

            return result;
        }


        public bool PageExists(string pageLink, string? folder)
        {
            var filePath = GetFilePath(pageLink, folder);
            return File.Exists(filePath);
        }


        public WikiPageModel LoadPage(string pageLink, string? folder)
        {
            var filePath = GetFilePath(pageLink, folder);

            if (File.Exists(filePath))
            {
                return BuildPageModel(filePath, folder);
            }

            return new WikiPageModel
            {
                Content = "# New Page", PageLink = pageLink, Title = CreatePageTitle(pageLink),
                Folder = folder
            };
        }


        public static void ClearAbandonedEditingSessions()
        {
            _editingSessions.RemoveAll(s => s.CheckedOutTime < DateTime.Now.AddMinutes(-10));
        }


        public void DeletePage(string link, string? userName, string? folder = null)
        {
            var path = GetFilePath(link, folder);
            if (File.Exists(path))
            {
                File.Delete(path);
                Task.Run(async () => { await _gitManager.Commit(link, userName, true); });
            }
        }


        internal List<NavPageMeta> SearchForPages(string searchTerm)
        {
            var allPages = Directory.GetFiles(_wikiPageDirectory);
            var result = new List<NavPageMeta>();
            foreach (var pagePath in allPages)
            {
                var pageMeta = BuildPageModel(pagePath, null);
                if (pageMeta.Content.Contains(searchTerm))
                {
                    string? folder = Path.GetFullPath(Path.GetDirectoryName(pagePath)!);
                    if (folder.EndsWith("WikiPages"))
                    {
                        folder = null;
                    }

                    result.Add(GetNavMenuItem(pagePath, folder));
                }
            }

            return result;
        }


        private NavPageMeta GetNavMenuItem(string path, string? folder)
        {
            var fileName = Path.GetFileNameWithoutExtension(path);
            return new NavPageMeta(CreatePageTitle(fileName), fileName, folder);
        }


        internal void LockForEditing(WikiPageModel pageModel, string username)
        {
            if (!_editingSessions.Any(s =>
                s.PageModel.Equals(pageModel) && s.UserName.Equals(username)))
            {
                _editingSessions.Add(new EditingSession(pageModel, username));
            }
        }


        internal void Cancel(WikiPageModel model)
        {
            if (_editingSessions.FirstOrDefault(s => s.PageModel.Equals(model)) is EditingSession
                session)
            {
                _editingSessions.Remove(session);
            }
        }


        internal bool CanEdit(WikiPageModel pageModel, string username)
        {
            return !_editingSessions.Any(s =>
                s.PageModel.Equals(pageModel) && !s.UserName.Equals(username));
        }


        private string GetFilePath(string pageLink, string? folder)
        {
            var basePath = _wikiPageDirectory;
            if (!string.IsNullOrEmpty(folder))
            {
                basePath = Path.Combine(_wikiPageDirectory, folder);
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }
            }

            return Path.Combine(basePath, $"{pageLink}.md");
        }


        private WikiPageModel BuildPageModel(string filePath, string? folder)
        {
            var title = CreatePageTitle(Path.GetFileNameWithoutExtension(filePath));
            var pageLink = CreatePageLink(title);
            var saved = _loadedPages.FirstOrDefault(p => p.PageLink == pageLink);
            if (saved != null)
            {
                return saved;
            }

            var content = File.ReadAllText(filePath);
            var titleRgx = new Regex("<!-- TITLE: (.*) -->");
            var titleMatch = titleRgx.Match(content);

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

            var model = new WikiPageModel
            {
                Content = content,
                Title = title,
                SubTitle = subTitle,
                PageLink = pageLink,
                Folder = folder
            };
            _loadedPages.Add(model);

            return model;
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
            var exceptionPath = GetFilePath("bliki.exceptions", null);
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


        private readonly IGitManager _gitManager;

        private string _wikiPageDirectory = @"WikiPages";
        private static readonly List<EditingSession> _editingSessions
            = new List<EditingSession>();
        private static readonly List<WikiPageModel> _loadedPages = new List<WikiPageModel>();
    }
}