using Bliki.Interfaces;
using System;
using System.Management.Automation;

namespace Bliki.Data
{
    public class GitManager: IGitManager
    {
        public void Commit(string fileName)
        {
            var shell = PowerShell.Create();
            shell.AddScript(@"cd ..\..\..\..\");
            shell.AddScript(@"git add *");
            shell.AddScript($@"git commit -m 'Saving file {fileName} at {DateTime.Now.ToString()}'");
            shell.AddScript(@"git push");
            var results = shell.Invoke();
        }
    }
}
