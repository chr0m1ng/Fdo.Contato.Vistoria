using NUnit.Framework;
using Xamarin.UITest;

namespace Fdo.Contato.Vistoria.UITest
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class NewVehicleTests
    {
        IApp app;
        Platform platform;

        public NewVehicleTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void PlateEntryPlateValidationWrong()
        {
            app.EnterText("PlateEntry", "aaaa35l");
            app.Tap("NewVehicleButton");
            app.WaitForElement("message");
            app.Screenshot(nameof(PlateEntryPlateValidationWrong));
        }
    }
}
