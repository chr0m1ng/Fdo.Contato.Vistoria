using Fdo.Contato.Vistoria.Models;
using Plugin.FileUploader.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdo.Contato.Vistoria.Services.Interfaces
{
    public interface IVehicleImageUploadService
    {
        Task<FileUploadResponse> UploadImageAsync(VehicleImage vehicleImage);

        Task<FileUploadResponse> UploadImagesAsync(IEnumerable<VehicleImage> vehicleImages);
    }
}
