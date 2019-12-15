using Bliki.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bliki.Shared
{
    public partial class NavMenu
    {
        [Inject]
        private PageManager _pageManager { get; set; } = default!;
        [Inject]
        protected NavigationManager _navManager { get; set; } = default!;


        private bool collapseNavMenu = true;
        protected string _previousUri = "";

        protected IList<NavPageMeta> PageMetas { get; set; } = new List<NavPageMeta>();
        protected IList<NavPageMeta> CurrentPageHeaders { get; set; } = new List<NavPageMeta>();

        protected string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        protected void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }


        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            _navManager.LocationChanged += _navManager_LocationChanged;
        }

        private void _navManager_LocationChanged(object? sender, 
            LocationChangedEventArgs e)
        {
            CheckRoute();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            try
            {
                base.OnAfterRender(firstRender);
                CheckRoute();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void CheckRoute()
        {
            var newUri = _navManager
                        .ToBaseRelativePath(_navManager.Uri);

            var metaList = _pageManager.GetNavMenuMetas();
            if (!PageMetas.SequenceEqual(metaList))
            {
                PageMetas = metaList;
                StateHasChanged();
            }

            if (!newUri.Contains("/") && newUri != _previousUri)
            {
                _previousUri = newUri;
                var currentHeaders =
                _pageManager.GetCurrentPageHeaders(newUri);
                if (!CurrentPageHeaders.SequenceEqual(currentHeaders))
                {
                    CurrentPageHeaders = currentHeaders;
                    StateHasChanged();
                }
            }
        }
    }
}
