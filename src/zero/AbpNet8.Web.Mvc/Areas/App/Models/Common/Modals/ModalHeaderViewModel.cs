namespace AbpNet8.Web.Areas.App.Models.Common.Modals
{
    public class ModalHeaderViewModel
    {
        public string Title { get; set; }

        public ModalHeaderViewModel(string title)
        {
            Title = title;
        }
    }
    public class ModalFooterViewModel
    {
        public string IdSaveButton { get; set; }

        public ModalFooterViewModel(string IdSaveButton)
        {
            this.IdSaveButton = IdSaveButton;
        }
    }
}