using Bliki.Components;
using Bliki.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
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
        [Inject]
        private IHttpContextAccessor _httpContextAccessor { get; set; } = default!;
        [Inject]
        private ModalService Modal { get; set; } = default!;
        private WikiPageModel PageModel { get; set; } = new WikiPageModel();


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

        private void OpenEditor()
        {
            if (_pageManager.CanEdit(PageModel))
            {
                if (_httpContextAccessor.HttpContext.User.Identity.Name is string username)
                {
                    _pageManager.LockForEditing(PageModel, username);
                    _navManager.NavigateTo($"editor/{PageLink ?? "home"}");
                }
                else
                {
                    Modal.Show("Can't Edit", "User Session is Invalid! Please log in again.");
                }
            }
            else
            {
                Modal.Show("Can't Edit", 
                    @"Sorry, that page is currently being edited by another user.
Please try again later.");
            }
        }

        private async Task ScrollToElementId(string elementId)
        {
            await _jsRuntime
                .InvokeVoidAsync("scrollToElementId", new[] { elementId });
        }


        private string _previousSectionLink = "";
    }
}
