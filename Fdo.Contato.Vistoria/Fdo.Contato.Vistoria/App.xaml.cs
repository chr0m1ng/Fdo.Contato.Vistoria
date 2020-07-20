using DIPS.Xamarin.UI;
using Fdo.Contato.Vistoria.Models;
using Fdo.Contato.Vistoria.Models.Interfaces;
using Fdo.Contato.Vistoria.Services;
using Fdo.Contato.Vistoria.Services.Interfaces;
using Fdo.Contato.Vistoria.ViewModels;
using Plugin.Media;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: ExportFont("fontello.ttf", Alias = "FloatingButtonIconsFont")]
namespace Fdo.Contato.Vistoria
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            Library.Initialize();

            MainPage = new AppShell();
            
            InjectDependencies();
        }

        private void InjectDependencies()
        {
            DependencyService.RegisterSingleton(new ImageViewerViewModel());
            DependencyService.RegisterSingleton<IVehicleCameraService>(new VehicleCameraService());
            DependencyService.RegisterSingleton<IVehicle>(new Vehicle());
            DependencyService.RegisterSingleton<IAppSettings>(new AppSettings());
            DependencyService.RegisterSingleton<IVehicleImageUploadService>(new VehicleImageUploadService());
            DependencyService.RegisterSingleton(MainPage.Navigation);
        }

        protected override async void OnStart()
        {
            await CrossMedia.Current.Initialize();

            if (!await VehicleCameraService.IsCameraAvailableAsync() || !await CheckWiFiConnectivityAsync())
            {
                Quit();
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private async Task<bool> CheckWiFiConnectivityAsync()
        {
            if (!Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi))
            {
                await MainPage.DisplayAlert("Sem Wi-Fi", "Não foi detectada uma conexão Wi-Fi no dispositivo", "ok");
                return false;
            }
            return true;
        }
    }
}
