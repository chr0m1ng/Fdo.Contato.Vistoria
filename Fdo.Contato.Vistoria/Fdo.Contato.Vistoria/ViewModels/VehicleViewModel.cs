using Fdo.Contato.Vistoria.Models;
using Fdo.Contato.Vistoria.Providers;
using Fdo.Contato.Vistoria.Services.Interfaces;
using Fdo.Contato.Vistoria.Views;
using Plugin.FileUploader;
using Plugin.FileUploader.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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

        private readonly IVehicleCameraService _cameraService;
        private readonly IVehicleImageUploadService _uploadService;
        private readonly INavigation _navigation;

        public VehicleViewModel()
        {
            Device.BeginInvokeOnMainThread(() => _vehicle.Images.Clear());

            Title = _vehicle.Plate;

            AddCameraPhotoCommand = new Command(AddCameraPhotoAsync);
            AddGalleryPhotoCommand = new Command(AddGalleryPhotoAsync);
            RetryUploadCommand = new Command(RetryUploadPhotoAsync);
            OpenImageCommand = new Command(OpenImagePreviewAsync);

            _cameraService = DependencyService.Get<IVehicleCameraService>();
            _uploadService = DependencyService.Get<IVehicleImageUploadService>();
            _navigation = DependencyService.Get<INavigation>();

            CrossFileUploader.Current.FileUploadProgress += UpdateFileUploadProgressAsync;
            CrossFileUploader.Current.FileUploadCompleted += UpdateIconUploadCompleted;
            CrossFileUploader.Current.FileUploadError += UpdateIconUploadError;

            Task.Run(async () => await LoadVehicleImagesAsync(CancellationToken.None));
        }

        ~VehicleViewModel()
        {
            CrossFileUploader.Current.FileUploadProgress -= UpdateFileUploadProgressAsync;
            CrossFileUploader.Current.FileUploadCompleted -= UpdateIconUploadCompleted;
            CrossFileUploader.Current.FileUploadError -= UpdateIconUploadError;
        }

        private async void UpdateFileUploadProgressAsync(object sender, FileUploadProgress e)
        {
            foreach (var image in _vehicle.Images.Where(vi => vi.Tag == e.Tag))
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
            foreach (var image in _vehicle.Images.Where(vi => vi.Tag == tag))
            {
                image.UploadError = isError;
                image.StatusImage = icon;
            }
        }

        private async void AddCameraPhotoAsync()
        {
            var photo = await _cameraService.TakePhotoAsync(_vehicle.Plate);
            if (photo is null)
            {
                return;
            }
            Device.BeginInvokeOnMainThread(() => _vehicle.Images.Add(photo));
            await _uploadService.UploadImageAsync(photo);
        }

        private async void AddGalleryPhotoAsync()
        {
            var photos = await _cameraService.GetGalleryVehiclePhotosAsync();
            if (photos is null)
            {
                return;
            }
            Device.BeginInvokeOnMainThread(() => _vehicle.Images.AddRange(photos));
            await _uploadService.UploadImagesAsync(photos);
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
            await _uploadService.UploadImageAsync(vehicleImage);
        }

        private async Task RetryAllImagesAsync()
        {
            var allImages = _vehicle.Images.Where(vi => vi.UploadError).ToList();
            var tag = Guid.NewGuid().ToString();
            foreach (var image in allImages)
            {
                image.PrepareToRetryUpload();
                image.Tag = tag;
            }
            await _uploadService.UploadImagesAsync(allImages);
        }

        private async void OpenImagePreviewAsync(object obj)
        {
            var vehicleImage = obj as VehicleImage;
            if (vehicleImage is null)
            {
                return;
            }
            await _navigation.PushAsync(new ImageViewerPage(vehicleImage));
        }

        private async Task LoadVehicleImagesAsync(CancellationToken cancellationToken)
        {
            var imageUrls = await GetVehicleImagesUrlsAsync(cancellationToken);
            var vehicleImages = imageUrls?.Select(i =>
                new VehicleImage()
                {
                    Name = Path.GetFileName(i),
                    ImageAlbumPath = i
                }
            );
            if (vehicleImages != null)
            {
                Device.BeginInvokeOnMainThread(() => _vehicle.Images.AddRange(vehicleImages));
            }
        }

        private async Task<IEnumerable<string>> GetVehicleImagesUrlsAsync(CancellationToken cancellationToken)
        {
            var vehicleImages = ContatoApiClientProvider.GetClient();
            var images = await vehicleImages.ListVehicleImagesAsync(_vehicle.Plate, cancellationToken);
            return images?.Select(i => $"{_appSettings.HostName}/{_vehicle.Plate}/{i}");
        }
    }
}
