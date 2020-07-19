using Fdo.Contato.Vistoria.Models.Interfaces;
using Fdo.Contato.Vistoria.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fdo.Contato.Vistoria.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public IAppSettings AppSettings => DependencyService.Get<IAppSettings>();
        public SettingsPage()
        {
            InitializeComponent();

            BindingContext = new SettingsViewModel();
        }

        protected override void OnAppearing()
        {
            HostNameEntry.Text = AppSettings.HostName;
            AuthKeyEntry.Text = AppSettings.AuthKey;
            base.OnAppearing();
        }
    }
}