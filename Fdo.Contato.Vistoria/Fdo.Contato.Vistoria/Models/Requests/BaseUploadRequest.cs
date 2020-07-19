using Fdo.Contato.Vistoria.Models.Interfaces;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Fdo.Contato.Vistoria.Models.Requests
{
    public abstract class BaseUploadRequest
    {
        public const string REQUEST_FILE_FIELD_NAME = "file";

        private const string REQUEST_PLATE_FIELD_NAME = "placa";
        private const string REQUEST_AUTH_HEADER_NAME = "x-api-key";

        public string Url { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public Dictionary<string, string> Parameters { get; set; }

        private IVehicle Vehicle => DependencyService.Get<IVehicle>();
        private IAppSettings AppSettings => DependencyService.Get<IAppSettings>();

        public BaseUploadRequest()
        {
            Headers = new Dictionary<string, string>()
            {
                { REQUEST_AUTH_HEADER_NAME, AppSettings.AuthKey}
            };
            Parameters = new Dictionary<string, string>()
            {
                { REQUEST_PLATE_FIELD_NAME, Vehicle.Plate }
            };
            Url = AppSettings.HostName;
        }
    }
}
