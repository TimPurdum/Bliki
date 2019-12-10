<!-- TITLE: Administration -->
<!-- SUBTITLE: Directions for administering the Puffin Papers -->

# Introduction
Puffin Papers is currently hosted on the same laptop, in Steve's office, that we use as our [Puffin VM host](puffin-vm-host).  The system on which it is built uses a Git repository as its data store.  Each article/page is stored simply as a markdown file in that repository.  Though the system currently provides no way to view the change log for any given page, via the web interface, the full history is easily viewable via Git.  The host is configured to automatically push all changes to [a shared repository the company's official Git server](http://10.1.1.220/list-of-git-repositories#puffinpapers-git), on a regular basis.  Therefore, if you want to view the history, you can clone that repository from the server and view the commit history using any of the standard Git tools.  The primary purpose of pushing to the server is for backup, but it does also serve as a convenient way to view the repository without needing to connect to the Puffin Papers host machine.
# Adding New Users
New users must be manually created by an administrator, following these steps:

1. Log into your account.
2. Click `Settings` in the navigation pane.
3. Click `Users` in the navigation pane.
4. Click `Create/Authorize User` in the top right corner.
5. Give the user an email, password, and public name.
6. Send the user their credentials to log in.