using Bliki.Components;
using Bliki.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bliki.Pages
{
    public class EditorBase : ComponentBase
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

        protected WikiPageModel PageModel { get; set; } = new WikiPageModel();
            
        protected Toolbar? Toolbar { get; set; }

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
            _pageManager.SavePage(PageModel, _httpContextAccessor.HttpContext.User.Identity.Name);

            _navManager.NavigateTo($"/{PageModel.PageLink}");
        }


        protected void Cancel()
        {
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
            if (PageModel.PageLink != "new-page")
            {
                _pageManager.DeletePage(PageLink, _httpContextAccessor.HttpContext.User.Identity.Name);
            }
        }


        protected async void OnKeyDown(KeyboardEventArgs e)
        {
            if (e.CtrlKey)
            {
                switch (e.Key)
                {
                    case "b":
                        await ApplyStyling(ToolbarButton.Bold);
                        break;
                    case "i":
                        await ApplyStyling(ToolbarButton.Italic);
                        break;
                }
            }
            else if (e.Key.Contains("Arrow") || e.Key == "Enter")
            {
                await CheckFormattingPosition();
                StateHasChanged();
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
            }
            catch
            {
                // let it go...
            }
        }


        private async Task ApplyStyling(ToolbarButton button)
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
            }
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


        private string GetLine()
        {
            _allLines = _lineBreakRegex.Split(PageModel.Content);
            var index = 0;
            foreach (var line in _allLines)
            {
                index += line.Length;
                if (index >= _selectionStart)
                {
                    return line;
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

        protected int _selectionStart;
        protected int _selectionEnd;
        protected string _beforePosition = "";
        protected string _afterPosition = "";
        private string[] _allLines = new string[0];
        private readonly Regex _boldRegex = new Regex(@"(?:^|[^\*])(\*\*)(?:[^\*]|$)");
        private readonly Regex _italicRegex = new Regex(@"(?:^|[^\*])(\*)(?:[^\*]|$)");
        private readonly Regex _strikethroughRegex = new Regex(@"(?:^|[^~])(~~)(?:[^~]|$)");
        private readonly Regex _lineBreakRegex = new Regex("\r\n|\r|\n");
    }
}
