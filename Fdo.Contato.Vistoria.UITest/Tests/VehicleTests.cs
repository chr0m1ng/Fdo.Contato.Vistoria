using Fdo.Contato.Vistoria.UITest.Pages;
using NUnit.Framework;
using Xamarin.UITest;

namespace Fdo.Contato.Vistoria.UITest.Tests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class VehicleTests : BaseTestFixture
    {
        public VehicleTests(Platform platform) : base(platform) { }

        [Test]
        [TestCase("abc1e34")]
        public void OpenCameraAndTakePhoto(string plate)
        {
            new NewVehiclePage().CreateVehicleFolder(plate, true);

            new VehiclePage()
                .OpenCamera();
        }
    }
}
