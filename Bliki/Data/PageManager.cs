using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text.Json;
using System.Web;

namespace Bliki.Data
{
    public class PageManager
    {
        public bool SavePage(WikiPageModel model)
        {
            try
            {
                if (model.PageLink == null)
                {
                    model.PageLink = CreatePageLink(model.Title);
                }
                var json = JsonSerializer.Serialize(model);
                var savePath = GetFilePath(model.PageLink);
                File.WriteAllText(savePath, json);
                GitCommit(model.PageLink);
                return true;
            }
            catch(Exception ex)
            {
                LogException(ex);
                return false;
            }
        }

        public IList<NavPageMeta> GetPageMetas()
        {
            var fileNames = Directory.GetFiles(wikiPageDirectory);
            var result = new List<NavPageMeta>();

            foreach (var name in fileNames)
            {
                result.Add(new NavPageMeta { PageLink = name, Title = CreatePageTitle(name) });
            }

            return result;
        }

        public WikiPageModel LoadPage(string pageLink)
        {
            try
            {
                var filePath = GetFilePath(pageLink);
                if (!File.Exists(filePath))
                {
                    return new WikiPageModel { Content = "# New Page", PageLink = pageLink, Title = "New Page" };
                }
                var json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<WikiPageModel>(json);
            }
            catch(Exception ex)
            {
                LogException(ex);
                return null;
            }
        }


        private string GetFilePath(string fileName)
        {
            return Path.Combine(wikiPageDirectory, fileName);
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


        private void GitCommit(string fileName)
        {
            var shell = PowerShell.Create();
            shell.AddScript(@"cd ..\..\..\");
            shell.AddScript(@"git init");
            shell.AddScript(@"git add *");
            shell.AddScript($@"git commit -m 'Saving file {fileName} at {DateTime.Now.ToString()}'");
            shell.AddScript(@"git push");
            var results = shell.Invoke();
        }


        private void LogException(Exception ex)
        {
            var exceptionPath = GetFilePath("bliki.exceptions");
            File.AppendAllText(exceptionPath, ex.Message);
        }

        private const string wikiPageDirectory = @"..\..\..\WikiPages";
    }
}
