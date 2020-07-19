using Fdo.Contato.Vistoria.Extensions;
using Fdo.Contato.Vistoria.Models;
using Fdo.Contato.Vistoria.Services.Interfaces;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fdo.Contato.Vistoria.Services
{
    public class VehicleCameraService : IVehicleCameraService
    {
        public async Task<IEnumerable<VehicleImage>> GetGalleryVehiclePhotosAsync()
        {
            var pickMediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Full
            };

            var multiPickerOptions = new MultiPickerOptions()
            {
                AlbumSelectTitle = "Albuns",
                BackButtonTitle = "Cancelar",
                PhotoSelectTitle = "Selecionar fotos",
                DoneButtonTitle = "Adicionar fotos"
            };

            var medias = await CrossMedia.Current.PickPhotosAsync(pickMediaOptions, multiPickerOptions);

            if (medias == null)
            {
                return null;
            }
            return medias.Select(m => new VehicleImage() { Image = m, Name = m.Path.GetFileName() });
        }

        public async Task<VehicleImage> TakePhotoAsync(string plate)
        {
            var storeCameraMediaOptions = new StoreCameraMediaOptions()
            {
                AllowCropping = true,
                SaveToAlbum = true,
                Directory = plate
            };

            var media = await CrossMedia.Current.TakePhotoAsync(storeCameraMediaOptions);

            if (media == null)
            {
                return null;
            }
            return new VehicleImage() { Name = media.Path.GetFileName(), Image = media };
        }
    }
}
