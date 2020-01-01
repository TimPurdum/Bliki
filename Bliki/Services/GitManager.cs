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
    }
}
