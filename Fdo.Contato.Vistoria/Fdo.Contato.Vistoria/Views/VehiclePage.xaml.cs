using Fdo.Contato.Vistoria.Models;
using Fdo.Contato.Vistoria.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fdo.Contato.Vistoria.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VehiclePage : ContentPage
    {
        public VehiclePage()
        {
            InitializeComponent();

            BindingContext = new VehicleViewModel();
        }

        private async void TappedImageAsync(object sender, EventArgs e)
        {
            var layout = sender as BindableObject;
            var image = layout.BindingContext as VehicleImage;
            await DisplayAlert("Teste", image?.Name, "ok");
        }
    }
}