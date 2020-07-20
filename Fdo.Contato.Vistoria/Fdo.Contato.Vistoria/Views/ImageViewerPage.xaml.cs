using Fdo.Contato.Vistoria.Models;
using Fdo.Contato.Vistoria.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fdo.Contato.Vistoria.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageViewerPage : ContentPage
    {
        public ImageViewerViewModel ImageViewerViewModel => DependencyService.Get<ImageViewerViewModel>();

        public ImageViewerPage(VehicleImage vehicleImage)
        {
            InitializeComponent();

            ImageViewerViewModel.Title = vehicleImage.Name;
            ImageViewerViewModel.ImageSource = vehicleImage.Image.Path;
            BindingContext = ImageViewerViewModel;
        }
    }
}