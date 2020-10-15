using Fdo.Contato.Vistoria.Models.Interfaces;
using Fdo.Contato.Vistoria.Services.Interfaces;
using RestEase;
using Xamarin.Forms;

namespace Fdo.Contato.Vistoria.Providers
{

    public static class ContatoApiClientProvider
    {
        private static IAppSettings AppSettings => DependencyService.Get<IAppSettings>();

        public static IContatoApiClient GetClient()
        {
            var client = RestClient.For<IContatoApiClient>(AppSettings.HostName);
            client.Authorization = AppSettings.AuthKey;
            return client;
        }
    }
}
