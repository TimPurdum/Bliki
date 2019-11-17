using Bliki.Data;
using Microsoft.AspNetCore.Components;
using System.Linq;

namespace Bliki.Pages
{
    public class IndexBase : ComponentBase
    {
        [Parameter]
        public string PageLink { get; set; }
        [Inject]
        private PageManager _pageManager { get; set; }

        protected WikiPageModel PageModel { get; set; }

        protected override void OnInitialized()
        {
            
            base.OnInitialized();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            PageModel = _pageManager.LoadPage(PageLink ?? "home");
            
            base.OnAfterRender(firstRender);
        }
    }
}
