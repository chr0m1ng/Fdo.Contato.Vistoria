using Fdo.Contato.Vistoria.Models.Interfaces;

namespace Fdo.Contato.Vistoria.Models
{
    public class Vehicle : IVehicle
    {
        public Vehicle()
        {
            Images = new ExtendedObservableCollection<VehicleImage>();
        }

        private string _plate { get; set; }
        public string Plate
        {
            get => _plate;
            set => _plate = value.ToUpper();
        }

        public ExtendedObservableCollection<VehicleImage> Images { get; set; }
    }
}
