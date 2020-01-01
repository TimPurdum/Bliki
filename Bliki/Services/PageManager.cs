﻿using Bliki.Interfaces;
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
        private readonly IGitManager _gitManager;
        private static List<EditingSession> _editingSessions 
            = new List<EditingSession>();

        public PageManager(IGitManager gitManager)
        {
            _gitManager = gitManager;
            FindWikiPageDirectory(Directory.GetCurrentDirectory());
        }

        internal List<NavPageMeta> SearchForPages(string searchTerm)
        {
            var allPages = Directory.GetFiles(_wikiPageDirectory);
            var result = new List<NavPageMeta>();
            foreach (var pagePath in allPages)
            {
                var pageMeta = BuildPageModel(pagePath);
                if (pageMeta.Content.Contains(searchTerm))
                {
                    
                    result.Add(GetNavMenuItem(pagePath));
                }
            }

            return result;
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
                if (_editingSessions.FirstOrDefault(s => s.PageModel.Equals(model)) is EditingSession session)
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
                result.Add(GetNavMenuItem(path));
            }

            return result;
        }


        private NavPageMeta GetNavMenuItem(string path)
        {
            var fileName = Path.GetFileNameWithoutExtension(path);
            return new NavPageMeta(CreatePageTitle(fileName), fileName);
        }

        internal void LockForEditing(WikiPageModel pageModel, string username)
        {
            if (!_editingSessions.Any(s => s.PageModel.Equals(pageModel) && s.UserName.Equals(username)))
            {
                _editingSessions.Add(new EditingSession(pageModel, username));
            }
        }

        internal void Cancel(WikiPageModel model)
        {
            if (_editingSessions.FirstOrDefault(s => s.PageModel.Equals(model)) is EditingSession session)
            {
                _editingSessions.Remove(session);
            }
        }

        internal bool CanEdit(WikiPageModel pageModel, string username)
        {
            return !_editingSessions.Any(s => s.PageModel.Equals(pageModel) && !s.UserName.Equals(username));
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

        public static void ClearAbandonedEditingSessions()
        {
            _editingSessions.RemoveAll(s => s.CheckedOutTime < DateTime.Now.AddMinutes(-10));
        }


        private string GetFilePath(string pageLink)
        {
            return Path.Combine(_wikiPageDirectory, $"{pageLink}.md");
        }


        private WikiPageModel BuildPageModel(string filePath)
        {
            var title = CreatePageTitle(Path.GetFileNameWithoutExtension(filePath));
            var pageLink = CreatePageLink(title);
            var saved = _loadedPages.FirstOrDefault(p => p.PageLink == pageLink);
            if (saved != null) return saved;

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
                PageLink = pageLink
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
        private static List<WikiPageModel> _loadedPages = new List<WikiPageModel>();
    }
}
