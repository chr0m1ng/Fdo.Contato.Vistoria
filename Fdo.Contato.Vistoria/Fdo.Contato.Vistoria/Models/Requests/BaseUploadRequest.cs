using Fdo.Contato.Vistoria.Models.Interfaces;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Fdo.Contato.Vistoria.Models.Requests
{
    public abstract class BaseUploadRequest
    {
        public const string REQUEST_FILE_FIELD_NAME = "files";
        private const string REQUEST_AUTH_HEADER_NAME = "x-api-key";

        public string Url { get; set; }
        public Dictionary<string, string> Headers { get; set; }

        private readonly IVehicle _vehicle;
        private readonly IAppSettings _appSettings;

        public BaseUploadRequest()
        {
            _vehicle = DependencyService.Get<IVehicle>();
            _appSettings = DependencyService.Get<IAppSettings>();

            Headers = new Dictionary<string, string>()
            {
                { REQUEST_AUTH_HEADER_NAME, _appSettings.AuthKey}
            };
            Url = $"{_appSettings.HostName}/{_vehicle.Plate}";
        }
    }
}
