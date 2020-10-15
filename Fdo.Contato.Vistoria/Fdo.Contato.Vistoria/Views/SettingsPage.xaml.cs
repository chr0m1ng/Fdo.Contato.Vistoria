using Fdo.Contato.Vistoria.Models.Interfaces;
using Fdo.Contato.Vistoria.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fdo.Contato.Vistoria.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public readonly IAppSettings _appSettings;
        public SettingsPage()
        {
            InitializeComponent();

            _appSettings = DependencyService.Get<IAppSettings>();

            BindingContext = new SettingsViewModel();
        }

        protected override void OnAppearing()
        {
            HostNameEntry.Text = _appSettings.HostName;
            AuthKeyEntry.Text = _appSettings.AuthKey;
            base.OnAppearing();
        }
    }
}