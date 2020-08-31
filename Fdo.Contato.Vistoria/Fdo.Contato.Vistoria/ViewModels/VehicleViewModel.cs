using Fdo.Contato.Vistoria.Models;
using Fdo.Contato.Vistoria.Services.Interfaces;
using Fdo.Contato.Vistoria.Views;
using Plugin.FileUploader;
using Plugin.FileUploader.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Fdo.Contato.Vistoria.ViewModels
{
    public class VehicleViewModel : BaseViewModel
    {
        public ICommand AddCameraPhotoCommand { get; }
        public ICommand AddGalleryPhotoCommand { get; }
        public ICommand RetryUploadCommand { get; }
        public ICommand OpenImageCommand { get; }

        private IVehicleCameraService CameraService => DependencyService.Get<IVehicleCameraService>();
        private IVehicleImageUploadService UploadService => DependencyService.Get<IVehicleImageUploadService>();
        private INavigation Navigation => DependencyService.Get<INavigation>();

        public VehicleViewModel()
        {
            Vehicle.Images.Clear();

            Title = Vehicle.Plate;

            AddCameraPhotoCommand = new Command(AddCameraPhotoAsync);
            AddGalleryPhotoCommand = new Command(AddGalleryPhotoAsync);
            RetryUploadCommand = new Command(RetryUploadPhotoAsync);
            OpenImageCommand = new Command(OpenImagePreviewAsync);

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
            UpdateImagesIconStatus(e.Tag, Constants.ERROR_ICON, true);
        }

        private void UpdateImagesIconStatus(string tag, string icon, bool isError = false)
        {
            foreach (var image in Vehicle.Images.Where(vi => vi.Tag == tag))
            {
                image.UploadError = isError;
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
            var response = await Application.Current.MainPage.DisplayActionSheet("Quantas imagens deseja enviar novamente?", "cancelar", null, Constants.RETRY_ALL_BUTTON, Constants.RETRY_ONE_BUTTON);
            if (response is null)
            {
                return;
            }
            if (response.Equals(Constants.RETRY_ONE_BUTTON))
            {
                await RetrySingleImageAsync(vehicleImage);
            }
            else if (response.Equals(Constants.RETRY_ALL_BUTTON))
            {
                await RetryAllImagesAsync();
            }
        }

        private async Task RetrySingleImageAsync(VehicleImage vehicleImage)
        {
            vehicleImage.PrepareToRetryUpload();
            vehicleImage.Tag = Guid.NewGuid().ToString();
            await UploadService.UploadImageAsync(vehicleImage);
        }

        private async Task RetryAllImagesAsync()
        {
            var allImages = Vehicle.Images.Where(vi => vi.UploadError).ToList();
            var tag = Guid.NewGuid().ToString();
            foreach (var image in allImages)
            {
                image.PrepareToRetryUpload();
                image.Tag = tag;
            }
            await UploadService.UploadImagesAsync(allImages);
        }

        private async void OpenImagePreviewAsync(object obj)
        {
            var vehicleImage = obj as VehicleImage;
            if (vehicleImage is null)
            {
                return;
            }
            await Navigation.PushAsync(new ImageViewerPage(vehicleImage));
        }
    }
}
