using Bliki.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bliki.Shared
{
    public class NavMenuBase : ComponentBase
    {
        [Inject]
        private PageManager _pageManager { get; set; } = default!;
        private bool collapseNavMenu = true;
        protected IList<NavPageMeta> PageMetas { get; set; } = new List<NavPageMeta>();

        protected string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        protected void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }


        protected override void OnAfterRender(bool firstRender)
        {
            try
            {
                base.OnAfterRender(firstRender);
                var metaList = _pageManager.GetNavMenuMetas();
                if (!PageMetas.SequenceEqual(metaList))
                {
                    PageMetas = metaList;
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
