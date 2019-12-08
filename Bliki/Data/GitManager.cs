﻿using Bliki.Interfaces;
using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Threading.Tasks;

namespace Bliki.Data
{
    public class GitManager: IGitManager
    {
        public async Task Commit(string fileName, string? userName, bool delete = false)
        {
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
}
