namespace Fdo.Contato.Vistoria.Models.Interfaces
{
    public interface IVehicle
    {
        string Plate { get; set; }
        ExtendedObservableCollection<VehicleImage> Images { get; set; }
    }
}
