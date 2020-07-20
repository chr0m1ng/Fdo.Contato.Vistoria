using Fdo.Contato.Vistoria.Extensions;
using Fdo.Contato.Vistoria.Models;
using Fdo.Contato.Vistoria.Services.Interfaces;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

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

            if (medias is null)
            {
                return null;
            }
            var tag = Guid.NewGuid();
            foreach(var media in medias)
            {
                media.AlbumPath = media.Path;
            }
            return medias.Select(m => new VehicleImage(tag) { Image = m, Name = m.Path.GetFileName() });
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

            if (media is null)
            {
                return null;
            }
            return new VehicleImage() { Name = media.Path.GetFileName(), Image = media };
        }

        public static async Task<bool> IsCameraAvailableAsync()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("Sem Camera", "Não foi detectada nenhuma camera no dispositivo", "ok");
                return false;
            }
            return true;
        }
    }
}
