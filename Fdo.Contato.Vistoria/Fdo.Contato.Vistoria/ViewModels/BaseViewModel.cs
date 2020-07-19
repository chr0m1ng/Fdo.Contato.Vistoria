using Fdo.Contato.Vistoria.Models.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Fdo.Contato.Vistoria.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IVehicle Vehicle => DependencyService.Get<IVehicle>();
        public IAppSettings AppSettings => DependencyService.Get<IAppSettings>();

        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }
        private string _title { get; set; }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
