using System;
using Xamarin.UITest;

namespace Fdo.Contato.Vistoria.UITest
{
    public static class AppManager
    {
        private const string BUNDLE_ID = "com.x4devsonly.contato.vistoria";

        private static IApp app;
        public static IApp App
        {
            get
            {
                if (app == null)
                {
                    throw new NullReferenceException($"{nameof(App)} not set. Call {nameof(StartApp)} before trying to access it.");
                }
                return app;
            }
        }

        private static Platform? platform;
        public static Platform Platform
        {
            get
            {
                if (platform == null)
                {
                    throw new NullReferenceException($"{nameof(Platform)} not set.");
                }
                return platform.Value;
            }

            set
            {
                platform = value;
            }
        }

        public static void StartApp()
        {
            if (Platform == Platform.Android)
            {
                app = ConfigureApp
                    .Android
                    .InstalledApp(BUNDLE_ID)
                    .EnableLocalScreenshots()
                    .StartApp();
            }

            if (Platform == Platform.iOS)
            {
                app = ConfigureApp
                    .iOS
                    .InstalledApp(BUNDLE_ID)
                    .EnableLocalScreenshots()
                    .StartApp();
            }
        }
    }
}
