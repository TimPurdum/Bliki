using Bliki.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Threading.Tasks;

namespace Bliki.Data
{
    public class GitManager: IGitManager
    {
        private bool _enabled;

        public GitManager(IConfiguration config)
        {
            _enabled = config.GetValue<bool>("CommitToGit");
        }

        public async Task Commit(string fileName, string? userName, bool delete = false)
        {
            if (!_enabled) return;

            await Task.Run(() =>
            {
                using (var shell = PowerShell.Create())
                {
                    if (shell.Runspace.RunspaceStateInfo.State != RunspaceState.Opened)
                    {
                        shell.Runspace.ResetRunspaceState();
                        shell.Runspace.Open();
                    }

                    shell.AddScript(@"git add *");
                    shell.AddScript($@"git commit -m '{(delete ? "Deleting" : "Saving")} file {fileName} at {DateTime.Now.ToString()} by {userName}'");
                    shell.AddScript(@"git push");
                    var results = shell.Invoke();
                }
            });
        }
    }

    public class GitSetting
    {
        public bool UseGit { get; set; } = false;
    }
}
