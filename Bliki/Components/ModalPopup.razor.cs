using Microsoft.AspNetCore.Components;


namespace Bliki.Components
{
    public partial class ModalPopup
    {
        [Inject]
		private ModalService ModalService { get; set; } = default!;
        [Inject]
        private FileModalService FileService { get; set; } = default!;

        protected bool IsVisible { get; set; }
        protected string? Title { get; set; }
        protected RenderFragment? Content { get; set; }


        public void ShowModal(string title, RenderFragment content)
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
            FileService.OnShow -= ShowModal;
            FileService.OnClose -= CloseModal;
        }


        protected override void OnInitialized()
        {
            base.OnInitialized();
            ModalService.OnShow += ShowModal;
            ModalService.OnClose += CloseModal;
            FileService.OnShow += ShowModal;
            FileService.OnClose += CloseModal;
        }
    }
}