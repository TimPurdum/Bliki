using Microsoft.AspNetCore.Components;
using System;

namespace Bliki.Components
{
	public class ModalService
    {
		public event Action<string, object>? OnShow;

		public event Action? OnClose;

		public void Show(string title, Type contentType)
		{
			if (contentType.BaseType != typeof(ComponentBase))
			{
				throw new ArgumentException($"{contentType.FullName} must be a Blazor Component");
			}

			var content = new RenderFragment(x => { x.OpenComponent(1, contentType); x.CloseComponent(); });
			OnShow?.Invoke(title, content);
		}


		public void Show(string title, string content)
		{
			OnShow?.Invoke(title, content);
		}



		public void Close()
		{
			OnClose?.Invoke();
		}
	}
}

