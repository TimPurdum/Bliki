using Bliki.Data;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Bliki.Shared
{
    public class NavMenuBase : ComponentBase
    {
        [Inject]
        private PageManager _pageManager { get; set; }
        private bool collapseNavMenu = true;
        protected IList<NavPageMeta> PageMetas { get; set; }

        protected string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        protected void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        protected override void OnInitialized()
        {
            PageMetas = _pageManager.GetPageMetas();
            base.OnInitialized();
        }
    }
}
