using Bliki.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;

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

        protected override void OnInitialized()
        {
            try
            {
                base.OnInitialized();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void OnParametersSet()
        {
            try
            {
                base.OnParametersSet();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void OnAfterRender(bool firstRender)
        {
            try
            {
                base.OnAfterRender(firstRender);
                PageMetas = _pageManager.GetPageMetas();
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
