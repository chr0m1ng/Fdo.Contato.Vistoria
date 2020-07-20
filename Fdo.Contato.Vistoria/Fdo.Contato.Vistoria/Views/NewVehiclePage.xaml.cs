using Fdo.Contato.Vistoria.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fdo.Contato.Vistoria.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewVehiclePage : ContentPage
    {
        public NewVehiclePage()
        {
            InitializeComponent();

            PlateEntry.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeCharacter);

            BindingContext = new NewVehicleViewModel();
        }
    }
}