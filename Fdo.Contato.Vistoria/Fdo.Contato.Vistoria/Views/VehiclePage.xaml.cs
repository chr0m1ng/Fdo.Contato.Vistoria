using Fdo.Contato.Vistoria.ViewModels;
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
    }
}