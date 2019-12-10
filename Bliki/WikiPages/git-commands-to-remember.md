<!-- TITLE: Git Commands to Remember -->

# Maintenance
## Clean
`git clean -dxf`

This command will remove all files from the repository folder which are not committed to the repository.  This includes all ignored folders and files (such as `bin`, `obj`, and `packages` folders).  In other words, it essentially resets the contents of the repository folder back to what it would be if you had just done a fresh clone of the repository from the server.  This is a useful way to force Visual Studio to do a truly clean rebuild of the solution.  Since the clean and rebuild options in VS don't clear out everything, you sometimes get strange persistent errors or warnings which won't go away.  For instance, the clean option in VS won't delete the `obj` folders, the NuGet `packages` folder, nor the `.vs` folder (which contains the intellisense cache, among other temporary things). 
### Notes
* If you have the solution open in Visual Studio, or you are running anything from any of the `bin` folders (including services), it will fail to delete the files and ask if you want to rety, so you'll want to shut everything down first before running it
* **Warning**: this command will delete all uncommitted new files, so be sure to commit all your changes first before running it.

## Housekeeping (Garbage Collection)
`git gc --aggressive`

The official documentation recommends that you run this command "on a regular basis".  It compacts the repository to save space and make it run faster.  It's also been known to fix some inexplicable git errors too.

### Notes
* The `--aggressive` isn't really necessary, but since, like everyone else, you probably aren't running this very regularly, what the hey.
* **Warning**: Unless you add a `--no-prune` parameter, this command will automatically prune all unreferenced commits.  For instance, if you delete a branch, all the commits that were made on that branch are still in your repository, so they can still be found if you try hard enough.  However, after running this command, all those deleted commits, as it were, will be gone for good.

# Review and Merging
## Get List of Commits Only on Feature Branch

`git cherry -v master [<feature-branch>]`

Displays a list of all the commits that have been done on the feature branch which have not yet been merged into the master branch.  In other words, it shows the list of changes that have only been made to the feature-branch.  It displays the commits, in a list, with the oldest one at the top and the most recent one at the bottom.  

Normally, all of the commits will be prefixed with a `+` symbol.  If, however, there are some which start with a `-`, that means that the changes from those commits have already been copied to master via a cherry-pick or rebase operation (when doing so, the commits in master get a different ID, so they are considered different commits, even though they contain the same changes).

### Notes
* The `<feature-branch>` parameter is optional.  If ommitted, it compares the current branch to master.  Therefore, if you currently have the feature branch checked out, you don't need to specify it.
* The `-v` parameter causes it to display the comments for the commits; without it, it only displays the commit IDs.