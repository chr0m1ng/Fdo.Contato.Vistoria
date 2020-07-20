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
        public ICommand PlateEntryChangedCommand { get; }

        private INavigation Navigation => DependencyService.Get<INavigation>();

        public NewVehicleViewModel()
        {
            Title = Constants.MAIN_TITLE;

            OpenVehiclePageCommand = new Command(OpenVehiclePageAsync);
            PlateEntryChangedCommand = new Command(PlateEntryChangedAsync);
        }

        private async void PlateEntryChangedAsync(object obj)
        {
            var entryPlate = obj as Entry;
            if (entryPlate is null)
            {
                return;
            }
            if (entryPlate.Text.Length.Equals(entryPlate.MaxLength))
            {
                await Device.InvokeOnMainThreadAsync(entryPlate.Unfocus);
            }
        }

        private async void OpenVehiclePageAsync()
        {
            if (Vehicle.Plate.IsPlate())
            {
                await Navigation.PushAsync(new VehiclePage());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Placa incorreta", $"{Vehicle.Plate} não parece ser uma placa valida", "Tentar novamente");
            }
        }
    }
}
