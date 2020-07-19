using Fdo.Contato.Vistoria.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdo.Contato.Vistoria.Services.Interfaces
{
    public interface IVehicleCameraService
    {
        Task<VehicleImage> TakePhotoAsync(string plate);

        Task<IEnumerable<VehicleImage>> GetGalleryVehiclePhotosAsync();
    }
}
