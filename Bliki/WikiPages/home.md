<!-- TITLE: Home -->
<!-- SUBTITLE: Bliki - The Blazor-Powered Wiki -->
# Welcome to Bliki
## The Blazor-Powered Wiki

Bliki was designed specifically for developer teams, but could be adapted to any Wiki situation.

### Git Integration
To use automatic git commits on page updates, set the following value in *appsettings.json*.
```json
"CommitToGit": true
```
Set up your Publish (if using compiled) or Source folder as a git repository.
```
C:\git\Bliki> git init
```
Now, whenever a page is saved, **Bliki** automatically calls the following commands.
```
C:\git\Bliki> git add  *
C:\git\Bliki> git commit -m "Saving file *filename* at *time* by *username*
C:\git\Bliki> git push
```
### Editor
The editor uses standard Markdown, and is designed to handle simple keyboard shortcuts.
- `ctrl-b`: **Bold**
- `ctrl-i`: *Italic*