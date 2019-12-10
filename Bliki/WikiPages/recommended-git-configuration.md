<!-- TITLE: Recommended Git Configuration -->

When working with IDN repositories, the following settings should be used for consistency.  These 
settings may be set at the global level, but if they are not, they should be set at the repository 
level.  The settings are listed by their name in Visual Studio's `Team Explorer > Home > Settings` 
window.  The command-line for updating the setting follows each one, where applicable.  If you use 
another Git-compatible tool, you will need to find the equivalent setting in that software.
- **User Name** - Your full name (i.e. First Last)
  - `git config user.name "Your Name"`
- **Email Address** - Your idnetworks.com email address
  - `git config user.email yourname@idnetworks.com`
- **Prune remote branches during fetch** - `True`
  - `git config fetch.prune true`
- **Enable download of author images from 3rd party source** - `True`
  - *No command-line equivalent*

Though this setting is not currently settable via Visual Studio's Team Explorer window, it
is recommended that you set `core.autocrlf` via the command-line.  However, the recommended value
differs depending on your operating system.  Again, this setting may be changed globally or per 
repository.  To set it globally, add the `--global` argument before the setting name.
- On Windows, set it to `true`:
  - `git config core.autocrlf true`
- On Linux and OS X, set it to `input`:
  - `git config core.autocrlf input`
