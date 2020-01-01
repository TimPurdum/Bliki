using Bliki.Components;
using Bliki.Data;
using Bliki.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Bliki.Pages
{
    public partial class Index
    {
        [Parameter]
        public string? PageLink { get; set; }
        [Parameter]
        public string? SectionLink { get; set; }
        [Parameter]
        public string SearchTerm { get; set; } = "";
        protected List<NavPageMeta> SearchResults => _pageManager.SearchForPages(SearchTerm.ToLowerInvariant());
            
        [Inject]
        private PageManager _pageManager { get; set; } = default!;
        [Inject]
        protected NavigationManager _navManager { get; set; } = default!;
        [Inject]
        protected IJSRuntime _jsRuntime { get; set; } = default!;
        [Inject]
        private ModalService Modal { get; set; } = default!;
        private WikiPageModel PageModel { get; set; } = new WikiPageModel();
        [Inject]
        private AuthenticationStateProvider _authenticationStateProvider { get; set; } = default!;


        protected override async Task OnInitializedAsync()
        {
            _userIdentity = (await _authenticationStateProvider.GetAuthenticationStateAsync())?.User?.Identity;
        }


        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            PageModel = _pageManager.LoadPage(string.IsNullOrEmpty(PageLink) ? "home" : PageLink);
        }


        protected override void OnAfterRender(bool firstRender)
        {
            try
            {
                var fragment = new Uri(_navManager.Uri).Fragment;
                if (fragment.StartsWith("#"))
                {
                    _navManager.NavigateTo($"{_previousPageLink}/{fragment.Replace("#", "")}");
                    return;
                }
                if (string.IsNullOrEmpty(PageLink))
                {
                    _navManager.NavigateTo(string.IsNullOrEmpty(_previousPageLink) ? "/home" : _previousPageLink, true);
                    return;
                }

                _previousPageLink = PageLink;

                if (SectionLink != _previousSectionLink &&
                        !string.IsNullOrEmpty(SectionLink))
                {
                    ScrollToElementId(SectionLink);
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
            if (_userIdentity?.Name is string username)
            {
                if (_pageManager.CanEdit(PageModel, username))
                {
                    _pageManager.LockForEditing(PageModel, username);
                    _navManager.NavigateTo($"editor/{PageLink ?? "home"}");
                }
                else
                {
                    Modal.Show("Can't Edit",
                        @"Sorry, that page is currently being edited by another user.
Please try again later.");
                }
            }
            else
            {
                Modal.Show("Can't Edit", "User Session is Invalid! Please log in again.");
            }
        }

        private void ScrollToElementId(string elementId)
        {
            Task.Run(async () =>
            {
                await _jsRuntime
                .InvokeVoidAsync("scrollToElementId", new[] { elementId });
            });
        }


        private string _previousSectionLink = "";
        private string _previousPageLink = "";
        private IIdentity? _userIdentity;

        private void ClearSearch()
        {
            SearchTerm = "";
        }
    }
}
