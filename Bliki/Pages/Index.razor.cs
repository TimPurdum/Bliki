using Bliki.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Bliki.Pages
{
    public partial class Index
    {
        [Parameter]
        public string? PageLink { get; set; }
        [Parameter]
        public string? SectionLink { get; set; }
            
        [Inject]
        private PageManager _pageManager { get; set; } = default!;
        [Inject]
        protected NavigationManager _navManager { get; set; } = default!;
        [Inject]
        protected IJSRuntime _jsRuntime { get; set; } = default!;

        protected WikiPageModel PageModel { get; set; } = new WikiPageModel();


        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            if (PageLink == null) _navManager.NavigateTo("home", true);
            PageModel = _pageManager.LoadPage(string.IsNullOrEmpty(PageLink) ? "home" : PageLink);
        }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            try
            {
                if (string.IsNullOrEmpty(PageLink))
                {
                    _navManager.NavigateTo("/home", true);
                }
                var model = _pageManager.LoadPage(PageLink ?? "home");

                if (!PageModel.Equals(model))
                {
                    PageModel = model;
                    StateHasChanged();
                }

                if (SectionLink != _previousSectionLink &&
                        !string.IsNullOrEmpty(SectionLink))
                {
                    await ScrollToElementId(SectionLink);
                    _previousSectionLink = SectionLink;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void SectionNavigationHandler(string elementId)
        {
            StateHasChanged();
        }

        private async Task ScrollToElementId(string elementId)
        {
            await _jsRuntime
                .InvokeVoidAsync("scrollToElementId", new[] { elementId });
        }


        private string _previousSectionLink = "";
    }
}
