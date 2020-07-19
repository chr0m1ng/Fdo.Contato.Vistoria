using Fdo.Contato.Vistoria.Extensions;
using Fdo.Contato.Vistoria.Models;
using Fdo.Contato.Vistoria.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace Fdo.Contato.Vistoria.ViewModels
{
    public class NewVehicleViewModel : BaseViewModel
    {
        public ICommand OpenVehiclePageCommand { get; }

        private readonly INavigation _navigation;

        public NewVehicleViewModel(INavigation navigation)
        {
            _navigation = navigation;

            Title = Constants.MAIN_TITLE;

            OpenVehiclePageCommand = new Command(OpenVehiclePageAsync);
        }

        private async void OpenVehiclePageAsync()
        {
            if (Vehicle.Plate.IsPlate())
            {
                await _navigation.PushAsync(new VehiclePage());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Placa incorreta", $"{Vehicle.Plate} não parece ser uma placa valida", "Tentar novamente");
            }
        }
    }
}
