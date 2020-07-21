using Fdo.Contato.Vistoria.UITest.Extensions;
using Fdo.Contato.Vistoria.UITest.Pages;
using NUnit.Framework;
using Xamarin.UITest;

namespace Fdo.Contato.Vistoria.UITest.Tests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class NewVehicleTests : BaseTestFixture
    {
        public NewVehicleTests(Platform platform) : base(platform) { }

        [Test]
        [TestCase("aaaaaaa")]
        [TestCase("aaaaa23")]
        [TestCase("aãa0000")]
        [TestCase("aaa")]
        [TestCase("fas00c2")]
        [TestCase("aac-0101")]
        public void PlateEntryPlateValidationShouldFail(string plate)
        {
            new NewVehiclePage()
                .EnterPlate(plate, true)
                .ConfirmPlate()
                .WaitForWrongPlateError(plate)
                .ClickRetryPlate();
        }

        [Test]
        [TestCase("aaa0000")]
        [TestCase("aaa0A00")]
        [TestCase("aSC0A00")]
        public void PlateEntryValidationShouldSucceed(string plate)
        {
            new NewVehiclePage()
                .EnterPlate(plate, true)
                .ConfirmPlate()
                .WaitForPageToLeave();
        }
    }
}
