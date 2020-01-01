using Bliki.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bliki.Data
{
    public class GitManager: IGitManager
    {
        private bool _enabled;
        private Regex _fileNameCleaner = new Regex(@"[^a-zA-Z\-\._]*");
        private Regex _userNameCleaner = new Regex(@"[^a-zA-Z\-\._@]*");

        public GitManager(IConfiguration config)
        {
            _enabled = config.GetValue<bool>("CommitToGit");
        }
        
        public async Task Commit(string fileName, string? userName, bool delete = false)
        {
            if (!_enabled) return;

            await Task.Run(() =>
            {
                Process.Start("git", $"config user.email \"{userName}\"");
                Process.Start("git", $"config user.name \"{userName}\"");

                var startInfo = new ProcessStartInfo("git");
                startInfo.RedirectStandardError = true;
                startInfo.RedirectStandardOutput = true;
                startInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                startInfo.Arguments = "add *";
                var proc1 = Process.Start(startInfo);
                Debug.WriteLine(proc1.StandardOutput.ReadToEnd());
                Debug.WriteLine(proc1.StandardError.ReadToEnd());

                startInfo.Arguments = $@"commit -m ""{(delete ? "Deleting" : "Saving")} file {_fileNameCleaner.Replace(fileName, "")} at {DateTime.Now.ToString("MM-dd-yyyy")} by {_userNameCleaner.Replace(userName, "")}""";
                var proc2 = Process.Start(startInfo);
                Debug.WriteLine(proc2.StandardOutput.ReadToEnd());
                Debug.WriteLine(proc2.StandardError.ReadToEnd());

                startInfo.Arguments = "push";
                var proc3 = Process.Start(startInfo);
                Debug.WriteLine(proc3.StandardOutput.ReadToEnd());
                Debug.WriteLine(proc3.StandardError.ReadToEnd());
            });
        }

        public string? FetchCommitLog(string? fileName)
        {
            if (!_enabled) return null;

            var procInfo = new ProcessStartInfo("git");
            procInfo.RedirectStandardOutput = true;
            procInfo.RedirectStandardError = true;
            
            if (string.IsNullOrEmpty(fileName))
            {
                procInfo.Arguments = "log";
            }
            else
            {
                procInfo.Arguments = $"log --follow -p -- **/{fileName}";
            }

            var proc = Process.Start(procInfo);
            
            var raw = proc.StandardOutput.ReadToEnd();
            return FormatGitLog(raw);
        }


        private string FormatGitLog(string raw)
        {
            var lines = raw.Split('\n', '\r');
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (line.StartsWith("@@"))
                {
                    line = line.Insert(0, "<span style=\"color:blue\">");
                    line = line.Insert(line.LastIndexOf("@@") + 2, "</span>");
                }
                else if (line.StartsWith("-"))
                {
                    line = line.Insert(0, "<span style=\"color:red\">");
                    line = line + "</span>";
                }
                else if (line.StartsWith("+"))
                {
                    line = line.Insert(0, "<span style=\"color:green\">");
                    line = line + "</span>";
                }
                else if (line.StartsWith("commit "))
                {
                    line = line.Insert(0, "<span style=\"color:yellow\">");
                    line = line + "</span>";
                }
                lines[i] = line;
            }
            return string.Join("</br>", lines);
        }
    }
}
