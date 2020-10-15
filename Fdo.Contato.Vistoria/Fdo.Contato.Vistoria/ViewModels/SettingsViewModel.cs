using Fdo.Contato.Vistoria.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace Fdo.Contato.Vistoria.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public string HostNameEntry { get; set; }
        public string AuthKeyEntry { get; set; }

        public ICommand SaveSettingsCommand { get; }

        public SettingsViewModel()
        {
            Title = Constants.SETTINTGS_TITLE;
            SaveSettingsCommand = new Command(SaveSettingsAsync);
        }

        private async void SaveSettingsAsync()
        {
            _appSettings.HostName = HostNameEntry;
            _appSettings.AuthKey = AuthKeyEntry;
            await Application.Current.MainPage.DisplayAlert("Salvo", "Configurações salvas com sucesso!", "ok");
        }
    }
}
