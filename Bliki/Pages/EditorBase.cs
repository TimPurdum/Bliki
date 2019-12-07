using Bliki.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;

namespace Bliki.Pages
{
    public class EditorBase: ComponentBase
    {
        [Parameter]
        public string PageLink { get; set; } = "home";
        [Inject]
        private PageManager _pageManager { get; set; } = default!;
        [Inject]
        private NavigationManager _navManager { get; set; } = default!;

        protected WikiPageModel PageModel { get; set; } = new WikiPageModel();


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


        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            try
            {
                base.BuildRenderTree(builder);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected async void Save()
        {
            await _pageManager.SavePage(PageModel);
            
            _navManager.NavigateTo($"/view/{PageModel.PageLink}");
        }

        protected void Cancel()
        {
            if (PageModel.PageLink != null)
            {
                _navManager.NavigateTo($"/view/{PageModel.PageLink}");
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
    }
}
