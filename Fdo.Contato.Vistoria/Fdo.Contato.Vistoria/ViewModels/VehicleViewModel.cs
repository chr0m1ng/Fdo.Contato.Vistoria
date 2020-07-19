using DIPS.Xamarin.UI.Controls.Modality;
using Fdo.Contato.Vistoria.Services.Interfaces;
using System.Windows.Input;
using Xamarin.Forms;

namespace Fdo.Contato.Vistoria.ViewModels
{
    public class VehicleViewModel : BaseViewModel
    {
        public ICommand AddCameraPhotoCommand { get; }
        public ICommand AddGalleryPhotoCommand { get; }

        private IVehicleCameraService CameraService => DependencyService.Get<IVehicleCameraService>();

        public VehicleViewModel()
        {
            Vehicle.Images.Clear();

            Title = Vehicle.Plate;

            AddCameraPhotoCommand = new Command(AddCameraPhotoAsync);
            AddGalleryPhotoCommand = new Command(AddGalleryPhotoAsync);
        }

        private async void AddCameraPhotoAsync()
        {
            var photo = await CameraService.TakePhotoAsync(Vehicle.Plate);
            if (photo == null)
            {
                return;
            }
            await Device.InvokeOnMainThreadAsync(() => Vehicle.Images.Add(photo));
        }

        private async void AddGalleryPhotoAsync()
        {
            var photos = await CameraService.GetGalleryVehiclePhotosAsync();
            if (photos == null)
            {
                return;
            }
            await Device.InvokeOnMainThreadAsync(() => Vehicle.Images.AddRange(photos));
        }
    }
}
