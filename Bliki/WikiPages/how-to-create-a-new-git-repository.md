<!-- TITLE: How To Create A New Git Repository -->
<!-- SUBTITLE: Instructions for administering the central Git server -->

1. If you do not already have a login for the `IDN-GIT` server, contact either Mike Lawton or Steven Doggart to get one.  To create a new user, they (or someone else with an administrator account) will need to run the following:
    * `sudo adduser [USER_NAME]`
    * `sudo usermod -a -G sudo [USER_NAME]`

2. Run a bash terminal to connect to the server (one comes bundled with Git)
    * `ssh idn-git -l username`
*Note: if your username matches your Windows username, you can drop the -l parameter and value*

3. Create a new Git Repository
    * `cd /opt/git`
    * `sudo git init --bare ––shared=group [REPO_NAME]`
    * `sudo chown -R git.git [REPO_NAME]`

4. Configure the repository for HTTP access
    * `cd [REPO_NAME]`
    * `sudo git config --file config http.receivepack true`

5. Sign out of your SSH session
    * `exit`

6. Add the new repository to the [List of Git Repositories](list-of-git-repositories) document.  The URL for the new repository will be:
    * `http://idn-git.idnetworks.com/git/[REPO_NAME]`