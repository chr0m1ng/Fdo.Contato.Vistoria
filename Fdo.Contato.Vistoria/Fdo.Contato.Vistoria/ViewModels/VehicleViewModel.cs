using Fdo.Contato.Vistoria.Models;
using Fdo.Contato.Vistoria.Services.Interfaces;
using Plugin.FileUploader;
using Plugin.FileUploader.Abstractions;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Fdo.Contato.Vistoria.ViewModels
{
    public class VehicleViewModel : BaseViewModel
    {
        public ICommand AddCameraPhotoCommand { get; }
        public ICommand AddGalleryPhotoCommand { get; }
        public ICommand RetryUploadCommand { get; }

        private IVehicleCameraService CameraService => DependencyService.Get<IVehicleCameraService>();
        private IVehicleImageUploadService UploadService => DependencyService.Get<IVehicleImageUploadService>();

        public VehicleViewModel()
        {
            Vehicle.Images.Clear();

            Title = Vehicle.Plate;

            AddCameraPhotoCommand = new Command(AddCameraPhotoAsync);
            AddGalleryPhotoCommand = new Command(AddGalleryPhotoAsync);
            RetryUploadCommand = new Command(RetryUploadPhotoAsync);

            CrossFileUploader.Current.FileUploadProgress += UpdateFileUploadProgressAsync;
            CrossFileUploader.Current.FileUploadCompleted += UpdateIconUploadCompleted;
            CrossFileUploader.Current.FileUploadError += UpdateIconUploadError;
        }

        ~VehicleViewModel()
        {
            CrossFileUploader.Current.FileUploadProgress -= UpdateFileUploadProgressAsync;
            CrossFileUploader.Current.FileUploadCompleted -= UpdateIconUploadCompleted;
            CrossFileUploader.Current.FileUploadError -= UpdateIconUploadError;
        }

        private async void UpdateFileUploadProgressAsync(object sender, FileUploadProgress e)
        {
            foreach (var image in Vehicle.Images.Where(vi => vi.Tag == e.Tag))
            {
                await Device.InvokeOnMainThreadAsync(() => image.UploadProgress = e.Percentage / 100.0f);
            }
        }

        private void UpdateIconUploadCompleted(object sender, FileUploadResponse e)
        {
            UpdateImagesIconStatus(e.Tag, Constants.SUCCESS_ICON);
        }

        private void UpdateIconUploadError(object sender, FileUploadResponse e)
        {
            UpdateImagesIconStatus(e.Tag, Constants.ERROR_ICON);
        }

        private void UpdateImagesIconStatus(string tag, string icon)
        {
            foreach (var image in Vehicle.Images.Where(vi => vi.Tag == tag))
            {
                image.StatusImage = icon;
            }
        }

        private async void AddCameraPhotoAsync()
        {
            var photo = await CameraService.TakePhotoAsync(Vehicle.Plate);
            if (photo is null)
            {
                return;
            }
            Vehicle.Images.Add(photo);
            await UploadService.UploadImageAsync(photo);
        }

        private async void AddGalleryPhotoAsync()
        {
            var photos = await CameraService.GetGalleryVehiclePhotosAsync();
            if (photos is null)
            {
                return;
            }
            Vehicle.Images.AddRange(photos);
            await UploadService.UploadImagesAsync(photos);
        }

        private async void RetryUploadPhotoAsync(object obj)
        {
            var vehicleImage = obj as VehicleImage;
            if (vehicleImage is null)
            {
                return;
            }
            var response = await Application.Current.MainPage.DisplayAlert("Enviar novamente", $"Deseja reenviar a imagem {vehicleImage.Name}?", "sim", "não");
            if (response)
            {
                vehicleImage.StatusImage = string.Empty;
                vehicleImage.UploadProgress = default;
                await UploadService.UploadImageAsync(vehicleImage);
            }
        }
    }
}
