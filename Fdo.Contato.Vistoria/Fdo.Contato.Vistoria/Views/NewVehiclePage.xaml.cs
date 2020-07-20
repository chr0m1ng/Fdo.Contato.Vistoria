using Fdo.Contato.Vistoria.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fdo.Contato.Vistoria.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewVehiclePage : ContentPage
    {
        private const int PLATE_DASH_POS = 3;
        private const string PLATE_DASH = "-";

        public NewVehiclePage()
        {
            InitializeComponent();

            PlateEntry.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeCharacter);

            BindingContext = new NewVehicleViewModel();
        }

        private async void PlateEntryTextChangedAsync(object sender, TextChangedEventArgs e)
        {
            await Device.InvokeOnMainThreadAsync(() =>
            {
                var entry = sender as Entry;
                if (e.NewTextValue.Length == entry.MaxLength)
                {
                    entry.Unfocus();
                    return;
                }
                if (e.NewTextValue.Length == PLATE_DASH_POS && e.NewTextValue.Length > e.OldTextValue.Length)
                {
                    entry.Text += PLATE_DASH;
                }
            });
        }
    }
}