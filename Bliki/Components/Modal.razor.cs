using Microsoft.AspNetCore.Components;

namespace Bliki.Components
{
	public partial class Modal
    {
		[Inject]
		ModalService ModalService { get; set; } = default!;

		protected bool IsVisible { get; set; }
		protected string? Title { get; set; }
		protected object? Content { get; set; }

		protected override void OnInitialized()
		{
			base.OnInitialized();
			ModalService.OnShow += ShowModal;
			ModalService.OnClose += CloseModal;
		}

		public void ShowModal(string title, object content)
		{
			Title = title;
			Content = content;
			IsVisible = true;

			StateHasChanged();
		}


		public void CloseModal()
		{
			IsVisible = false;
			Title = "";
			Content = null;

			StateHasChanged();
		}

		public void Dispose()
		{
			ModalService.OnShow -= ShowModal;
			ModalService.OnClose -= CloseModal;
		}
	}
}
