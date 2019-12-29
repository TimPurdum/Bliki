using Bliki.Components;
using Bliki.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using System;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bliki.Pages
{
    public partial class Editor
    {
        [Parameter]
        public string PageLink { get; set; } = "home";
        [Parameter]
        public bool Bold { get; set; }
        [Parameter]
        public bool Italic { get; set; }
        [Parameter]
        public bool Strikethrough { get; set; }
        [Parameter]
        public bool Header1 { get; set; }
        [Parameter]
        public bool Header2 { get; set; }
        [Parameter]
        public bool Header3 { get; set; }
        [Parameter]
        public bool InlineCode { get; set; }
        [Parameter]
        public bool CodeBlock { get; set; }
        [Parameter]
        public bool NumberedList { get; set; }
        [Parameter]
        public bool BulletList { get; set; }
        [Inject]
        private PageManager _pageManager { get; set; } = default!;
        [Inject]
        private NavigationManager _navManager { get; set; } = default!;
        [Inject]
        private IHttpContextAccessor _httpContextAccessor { get; set; } = default!;
        [Inject]
        private IJSRuntime _jsRuntime { get; set; } = default!;
        [Inject]
        private MarkdownEditorManager _editorManager { get; set; } = default!;
        [Inject]
        private ModalService Modal { get; set; } = default!;

        protected WikiPageModel PageModel { get; set; } = new WikiPageModel();
            
        protected Toolbar? Toolbar { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _userIdentity = _httpContextAccessor.HttpContext.User.Identity;
        }

        protected override void OnAfterRender(bool firstRender)
        {
            try
            {
                base.OnAfterRender(firstRender);
                if (firstRender)
                {
                    CheckPageLink();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void _editorManager_UpdateContent(object? sender, ChangeEventArgs e)
        {
            if (e.Value is string newVal)
            {
                PageModel.Content = newVal;
                StateHasChanged();
            }
        }

        protected void Save()
        {
            _pageManager.SavePage(PageModel, _userIdentity?.Name);

            _navManager.NavigateTo($"/{PageModel.PageLink}");
        }


        protected void Cancel()
        {
            _pageManager.Cancel(PageModel);
            if (PageModel.PageLink != "new-page")
            {
                _navManager.NavigateTo($"/{PageModel.PageLink}");
            }
            else
            {
                _navManager.NavigateTo("/");
            }
        }


        private void CheckPageLink()
        {
            if (PageLink == null || PageLink == "new")
            {
                PageModel = new WikiPageModel { Content = "# New Page", Title = "New Page" };
            }
            else
            {
                PageModel = _pageManager.LoadPage(PageLink);
            }
            StateHasChanged();
        }


        protected void Delete()
        {
            // Modal.Show("Delete", new ConfirmDeleteForm() { PageLink = PageLink });
            if (PageModel.PageLink != "new-page")
            {
                _pageManager.DeletePage(PageLink, _userIdentity?.Name);
            }
        }


        protected async void OnKeyDown(KeyboardEventArgs e)
        {
            switch (e.Key)
            {
                case "b":
                    if (e.CtrlKey)
                    {
                        await ApplyStyling(ToolbarButton.Bold);
                    }
                    break;
                case "i":
                    if (e.CtrlKey)
                    {
                        await ApplyStyling(ToolbarButton.Italic);
                    }
                    break;
                case "s":
                    if (e.CtrlKey)
                    {
                        Save();
                    }
                    break;
                case "ArrowUp":
                case "ArrowLeft":
                case "ArrowRight":
                case "ArrowDown":
                    await CheckFormattingPosition();
                    StateHasChanged();
                    break;
                case "Enter":
                    await CheckFormattingPosition();
                    if (LineIsNumbered(-1))
                    {
                        await ApplyStyling(ToolbarButton.NumberedList);
                    }
                    else if (LineIsBulleted(-1))
                    {
                        await ApplyStyling(ToolbarButton.BulletList);
                    }
                    else
                    {
                        StateHasChanged();
                    }
                    break;
                case "Tab":
                    await InsertTab();
                    break;
            }
        }


        protected async void OnFocus(FocusEventArgs e)
        {
            await CheckFormattingPosition();
            StateHasChanged();
        }


        protected async void OnToolbarButtonClicked(ToolbarButton button)
        {
            await ApplyStyling(button);
            await FocusOnEditor();
        }


        private async Task CheckFormattingPosition()
        {
            try
            {
                var positions = await _jsRuntime.InvokeAsync<int[]>("getCursorPosition", new[] { "editor-text-area" });
                _selectionStart = positions[0];
                _selectionEnd = positions[1];

                _beforePosition = _selectionStart > 0 ? PageModel.Content.Substring(0, _selectionStart) : "";
                _afterPosition = _selectionEnd < PageModel.Content.Length ? PageModel.Content.Substring(_selectionEnd) : "";

                Bold = _boldRegex.Matches(_beforePosition).Count % 2 == 1 &&
                    _boldRegex.Matches(_afterPosition).Count % 2 == 1;
                Italic = _italicRegex.Matches(_beforePosition).Count % 2 == 1 &&
                    _italicRegex.Matches(_afterPosition).Count % 2 == 1;
                Strikethrough = _strikethroughRegex.Matches(_beforePosition).Count % 2 == 1 &&
                    _strikethroughRegex.Matches(_afterPosition).Count % 2 == 1;
                var headerLevel = GetLineHeaderLevel();
                Header1 = headerLevel == 1;
                Header2 = headerLevel == 2;
                Header3 = headerLevel == 3;
                NumberedList = LineIsNumbered();
                BulletList = LineIsBulleted();
            }
            catch
            {
                // let it go...
            }
        }


        private async Task ApplyStyling(ToolbarButton button)
        {
            try
            {
                await CheckFormattingPosition();
                ToggleResult toggleResult = new ToggleResult(PageModel.Content, 0);
                switch (button)
                {
                    case ToolbarButton.Bold:
                        toggleResult = _editorManager
                                .ToggleMarker(TextMarkers.Bold, PageModel.Content, _selectionStart, _selectionEnd);
                        break;
                    case ToolbarButton.Italic:
                        toggleResult = _editorManager
                                .ToggleMarker(TextMarkers.Italic, PageModel.Content, _selectionStart, _selectionEnd);
                        break;
                    case ToolbarButton.Strikethrough:
                        toggleResult = _editorManager
                                .ToggleMarker(TextMarkers.Italic, PageModel.Content, _selectionStart, _selectionEnd);
                        break;
                    case ToolbarButton.Header1:
                        toggleResult = _editorManager
                                .ToggleHeader(1, PageModel.Content, _selectionStart);
                        break;
                    case ToolbarButton.Header2:
                        toggleResult = _editorManager
                                .ToggleHeader(2, PageModel.Content, _selectionStart);
                        break;
                    case ToolbarButton.Header3:
                        toggleResult = _editorManager
                                .ToggleHeader(3, PageModel.Content, _selectionStart);
                        break;
                    case ToolbarButton.InlineCode:
                        toggleResult = _editorManager
                            .ToggleMarker(TextMarkers.InlineCode, PageModel.Content, _selectionStart, _selectionEnd);
                        break;
                    case ToolbarButton.CodeBlock:
                        toggleResult = _editorManager
                            .ToggleMarker(TextMarkers.CodeBlock, PageModel.Content, _selectionStart, _selectionEnd);
                        break;
                    case ToolbarButton.NumberedList:
                        toggleResult = _editorManager
                            .ToggleNumberedList(PageModel.Content, _selectionStart);
                        break;
                    case ToolbarButton.BulletList:
                        toggleResult = _editorManager
                            .ToggleBulletList(PageModel.Content, _selectionStart);
                        break;
                }
                PageModel.Content = toggleResult.Content;
                StateHasChanged();
                await ResetCursor(toggleResult.Offset);
                await CheckFormattingPosition();
            }
            catch
            {
                // ignore;
            }
        }


        private async Task InsertTab()
        {
            await CheckFormattingPosition();
            var toggleResult = _editorManager.InsertTab(PageModel.Content, _selectionStart, _selectionEnd);
            PageModel.Content = toggleResult.Content;
            StateHasChanged();
            await ResetCursor(toggleResult.Offset);
            await CheckFormattingPosition();
        }


        private int GetLineHeaderLevel()
        {
            var line = GetLine();
            var count = 0;
            while (line.StartsWith("#"))
            {
                line = line.TrimStart('#');
                count++;
            }

            return count;
        }


        private bool LineIsNumbered(int relativeToCursor = 0)
        {
            var line = GetLine(relativeToCursor);
            return _numberedLineRegex.IsMatch(line);
        }

        private bool LineIsBulleted(int relativeToCursor = 0)
        {
            var line = GetLine(relativeToCursor);
            return _bulletLineRegex.IsMatch(line);
        }


        private string GetLine(int relativeToCursor = 0)
        {
            _allLines = _lineBreakRegex.Split(PageModel.Content);
            var index = 0;
            for (int i = 0; i < _allLines.Length; i++)
            {
                string line = _allLines[i];
                index += line.Length + 1;
                if (index > _selectionStart)
                {
                    var returnIndex = (i + relativeToCursor) >= 0 && (i + relativeToCursor) < _allLines.Length - 1 ?
                        i + relativeToCursor : i;
                    return _allLines[returnIndex];
                }
            }
            return "";
        }

        private async Task ResetCursor(int offset)
        {
            await _jsRuntime.InvokeVoidAsync("resetCursorPosition", new object[] { "editor-text-area", _selectionStart + offset, _selectionEnd + offset });
        }


        private async Task FocusOnEditor()
        {
            await _jsRuntime.InvokeVoidAsync("focusOnElement", new object[] { "editor-text-area", _selectionStart, _selectionEnd });
        }

        private int _selectionStart;
        private int _selectionEnd;
        private string _beforePosition = "";
        private string _afterPosition = "";
        private string[] _allLines = new string[0];
        private IIdentity? _userIdentity;
        private readonly Regex _boldRegex = new Regex(@"(?:^|[^\*])(\*\*)(?:[^\*]|$)");
        private readonly Regex _italicRegex = new Regex(@"(?:^|[^\*])(\*)(?:[^\*]|$)");
        private readonly Regex _strikethroughRegex = new Regex(@"(?:^|[^~])(~~)(?:[^~]|$)");
        private readonly Regex _lineBreakRegex = new Regex("\r\n|\r|\n");
        private readonly Regex _numberedLineRegex = new Regex(@"^[\s]*[\d]+\. ");
        private readonly Regex _bulletLineRegex = new Regex(@"^[\s]*- ");
    }
}
