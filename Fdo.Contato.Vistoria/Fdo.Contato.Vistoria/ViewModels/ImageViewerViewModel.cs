namespace Fdo.Contato.Vistoria.ViewModels
{
    public class ImageViewerViewModel : BaseViewModel
    {
        public string ImageSource
        {
            get => _imageSource;
            set
            {
                if (_imageSource != value)
                {
                    _imageSource = value;
                    OnPropertyChanged(nameof(ImageSource));
                }
            }
        }
        private string _imageSource { get; set; }
    }
}
