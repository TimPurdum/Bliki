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
                if (firstRender)
                {
                    CheckRoute();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void CheckRoute()
        {
            var routeParts = _navManager
                .ToBaseRelativePath(_navManager.Uri)
                .Split('/');

            var metaList = _pageManager.GetNavMenuMetas();
            if (!PageMetas.SequenceEqual(metaList))
            {
                PageMetas = metaList;
                StateHasChanged();
            }

            string? newFolder = null;
            string? newUri;
            if (routeParts.Length > 1 && !string.IsNullOrEmpty(routeParts[1]))
            {
                newFolder = routeParts[0];
                newUri = routeParts[1];
            }
            else
            {
                newUri = routeParts[0];
            }

            newUri = newUri.Split("#")[0];

            if (newUri != _previousUri || newFolder != null && newFolder != _previousFolder)
            {
                _previousFolder = newFolder;
                _previousUri = newUri;

                var currentHeaders =
                    _pageManager.GetCurrentPageHeaders(newUri, newFolder);
                if (!CurrentPageHeaders.SequenceEqual(currentHeaders))
                {
                    CurrentPageHeaders = currentHeaders;
                    StateHasChanged();
                }
            }
        }


        private void NavigateToSection(string sectionLink)
        {
            _navManager.NavigateTo($"{_previousUri}#{sectionLink}", true);
        }


        private string? _previousFolder;
        protected string _previousUri = "";


        private bool collapseNavMenu = true;
    }
}