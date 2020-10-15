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
            return await UploadImagesAsync(new List<VehicleImage>() { vehicleImage });
        }

        public async Task<FileUploadResponse> UploadImagesAsync(IEnumerable<VehicleImage> vehicleImages)
        {
            var uploadImagesRequest = new UploadImagesRequest(vehicleImages.Select(vi => vi.ImageAlbumPath));
            return await CrossFileUploader.Current.UploadFileAsync(uploadImagesRequest.Url, uploadImagesRequest.FilePathItems.ToArray(), vehicleImages.FirstOrDefault()?.Tag, uploadImagesRequest.Headers);
        }
    }
}
