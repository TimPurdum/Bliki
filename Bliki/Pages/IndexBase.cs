using Bliki.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;

namespace Bliki.Pages
{
    public class IndexBase : ComponentBase
    {
        [Parameter]
        public string PageLink { get; set; } = "home";
        [Inject]
        private PageManager _pageManager { get; set; } = default!;

        protected WikiPageModel PageModel { get; set; } = new WikiPageModel();

        protected override void OnInitialized()
        {
            
            base.OnInitialized();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            try
            {
                base.OnAfterRender(firstRender);
                var model = _pageManager.LoadPage(PageLink ?? "home");

                if (PageModel != model)
                {
                    PageModel = model;
                    StateHasChanged();
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
    }
}
