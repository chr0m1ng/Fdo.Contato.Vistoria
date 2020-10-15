using RestEase;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fdo.Contato.Vistoria.Services.Interfaces
{
    public interface IContatoApiClient
    {
        [Header("x-api-key")]
        string Authorization { get; set; }

        [Get("{plate}")]
        Task<IEnumerable<string>> ListVehicleImagesAsync([Path] string plate, CancellationToken cancellationToken);
    }
}
