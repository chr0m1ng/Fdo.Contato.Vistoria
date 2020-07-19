using DIPS.Xamarin.UI;
using Fdo.Contato.Vistoria.Models;
using Fdo.Contato.Vistoria.Models.Interfaces;
using Fdo.Contato.Vistoria.Services;
using Fdo.Contato.Vistoria.Services.Interfaces;
using Plugin.Media;
using Xamarin.Forms;

[assembly: ExportFont("fontello.ttf", Alias = "FloatingButtonIconsFont")]
namespace Fdo.Contato.Vistoria
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            InjectDependencies();

            Library.Initialize();

            MainPage = new AppShell();
        }

        private void InjectDependencies()
        {
            DependencyService.RegisterSingleton<IVehicleCameraService>(new VehicleCameraService());
            DependencyService.RegisterSingleton<IVehicle>(new Vehicle());
        }

        protected override async void OnStart()
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                Quit();
            }

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
