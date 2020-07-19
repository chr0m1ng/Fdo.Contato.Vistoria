using Fdo.Contato.Vistoria.Models;
using Fdo.Contato.Vistoria.Models.Requests;
using Fdo.Contato.Vistoria.Services.Interfaces;
using Plugin.FileUploader;
using Plugin.FileUploader.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fdo.Contato.Vistoria.Services
{
    public class VehicleImageUploadService : IVehicleImageUploadService
    {
        public async Task<FileUploadResponse> UploadImageAsync(VehicleImage vehicleImage)
        {
            var uploadImageRequest = new UploadImagesRequest(new List<string>() { vehicleImage.Image.Path });
            return await CrossFileUploader.Current.UploadFileAsync(uploadImageRequest.Url, uploadImageRequest.FilePathItems.ToArray(), vehicleImage.Tag, uploadImageRequest.Headers, uploadImageRequest.Parameters);
        }

        public async Task<FileUploadResponse> UploadImagesAsync(IEnumerable<VehicleImage> vehicleImages)
        {
            var uploadImagesRequest = new UploadImagesRequest(vehicleImages.Select(vi => vi.Image.Path));
            return await CrossFileUploader.Current.UploadFileAsync(uploadImagesRequest.Url, uploadImagesRequest.FilePathItems.ToArray(), vehicleImages.FirstOrDefault()?.Tag, uploadImagesRequest.Headers, uploadImagesRequest.Parameters);
        }
    }
}
