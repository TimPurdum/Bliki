<!-- TITLE: Home -->
<!-- SUBTITLE: Bliki - The Blazor-Powered Wiki -->
# Welcome to Bliki
Bliki was designed specifically for developer teams, but could be adapted to any Wiki situation.
 
## Git Integration
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
## Editor
The editor uses standard Markdown, and is designed to handle simple keyboard shortcuts.
- `ctrl-b`: **Bold**
- `ctrl-i`: *Italic*
    in addition to the standard toolbar.

Of course, you can also add your own markdown marks, such as `*`italic`*`, `**`bold`**`, or `##` headers.
The markdown renderer is courtesy of [Markdig](https://github.com/lunet-io/markdig), an open-source .NET markdown renderer.
1. Both numbered lists *and*
- bulleted, and 
    - nested lists, are all supported.