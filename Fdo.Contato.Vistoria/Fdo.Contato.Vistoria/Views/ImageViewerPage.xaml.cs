using Fdo.Contato.Vistoria.Models;
using Fdo.Contato.Vistoria.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fdo.Contato.Vistoria.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageViewerPage : ContentPage
    {
        private readonly ImageViewerViewModel _imageViewerViewModel;

        public ImageViewerPage()
        {
            _imageViewerViewModel = DependencyService.Get<ImageViewerViewModel>();
        }

        public ImageViewerPage(VehicleImage vehicleImage) : this()
        {
            InitializeComponent();

            _imageViewerViewModel.Title = vehicleImage.Name;
            _imageViewerViewModel.ImageSource = vehicleImage.ImageAlbumPath;
            BindingContext = _imageViewerViewModel;
        }
    }
}