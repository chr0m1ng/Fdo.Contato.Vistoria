using Fdo.Contato.Vistoria.Models.Interfaces;
using Xamarin.Essentials;

namespace Fdo.Contato.Vistoria.Models
{
    public class AppSettings : IAppSettings
    {
        public string HostName
        {
            get => Preferences.Get(Constants.SETTINGS_HOST_KEY, Constants.SETTINGS_HOST_DEFAULT_VALUE);
            set => Preferences.Set(Constants.SETTINGS_HOST_KEY, value);
        }
        public string AuthKey
        {
            get => Preferences.Get(Constants.SETTINGS_AUTH_KEY, Constants.SETTINGS_AUTH_DEFAULT_VALUE);
            set => Preferences.Set(Constants.SETTINGS_AUTH_KEY, value);
        }
    }
}
