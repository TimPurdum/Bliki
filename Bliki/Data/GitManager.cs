using Bliki.Interfaces;
using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace Bliki.Data
{
    public class GitManager: IGitManager
    {
        public void Commit(string fileName)
        {
            using (var shell = PowerShell.Create())
            {
                if (shell.Runspace.RunspaceStateInfo.State != RunspaceState.Opened)
                {
                    shell.Runspace.ResetRunspaceState();
                    shell.Runspace.Open();
                }
                shell.AddScript(@"cd ..\..\..\..\");
                shell.AddScript(@"git add *");
                shell.AddScript($@"git commit -m 'Saving file {fileName} at {DateTime.Now.ToString()}'");
                shell.AddScript(@"git push");
                var results = shell.Invoke();
            }
        }
    }
}
