using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Fdo.Contato.Vistoria.Services;
using Fdo.Contato.Vistoria.Views;

namespace Fdo.Contato.Vistoria
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
