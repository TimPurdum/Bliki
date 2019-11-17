using Bliki.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Web;

namespace Bliki.Pages
{
    public class EditorBase: ComponentBase
    {
        [Parameter]
        public string PageLink { get; set; }
        [Inject]
        private PageManager _pageManager { get; set; }
        [Inject]
        private NavigationManager _navManager { get; set; }

        protected WikiPageModel PageModel { get; set; }

        protected override void OnInitialized()
        {
            CheckPageLink();

            base.OnInitialized();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            //if (!firstRender)
            //{
            //    CheckPageLink();
            //    StateHasChanged();
            //}
            base.OnAfterRender(firstRender);
        }

        protected void Save()
        {
            _pageManager.SavePage(PageModel);
            StateHasChanged();
            _navManager.NavigateTo($"/{PageModel.PageLink}");
        }

        protected void Cancel()
        {
            if (PageModel.PageLink != null)
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
            if (PageLink == "new")
            {
                PageModel = new WikiPageModel { Content = "# New Page" };
            }
            else
            {
                PageModel = _pageManager.LoadPage(PageLink);
            }
        }
    }
}
