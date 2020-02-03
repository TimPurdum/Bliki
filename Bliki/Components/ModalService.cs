using Microsoft.AspNetCore.Components;
using System;


namespace Bliki.Components
{
    public class ModalService
    {
        public event Action<string, RenderFragment>? OnShow;

        public event Action? OnClose;
        public string? PageLink { get; private set; }

        public bool Success { get; set; }


        public void Show(string title, Type contentType, string? pageLink = null)
        {
            PageLink = pageLink;
            if (contentType.BaseType != typeof(ComponentBase))
            {
                throw new ArgumentException($"{contentType.FullName} must be a Blazor Component");
            }

            var content = new RenderFragment(x =>
            {
                x.OpenComponent(1, contentType);
                x.CloseComponent();
            });

            OnShow?.Invoke(title, content);
        }


        public void Show(string title, string content)
        {
            var fragment = new RenderFragment(x => { x.AddContent(0, content); });
            OnShow?.Invoke(title, fragment);
        }


        public void Close()
        {
            OnClose?.Invoke();
        }
    }
}