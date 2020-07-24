using Fdo.Contato.Vistoria.UITest.Extensions;
using System;
using Xamarin.UITest.Queries;

namespace Fdo.Contato.Vistoria.UITest.Pages
{
    public class NewVehiclePage : BasePage
    {
        private const string PLATE_ENTRY_ID = "PlateEntry";
        private const string CREATE_FOLDER_ENTRY_ID = "CreateFolderButton";

        private const string FAILED_PLATE_DISPLAY_ALERT_TITLE = "Placa incorreta";
        private const string FAILED_PLATE_DISPLAY_ALERT_CANCEL = "Tentar novamente";
        private readonly Func<string, string> CreateFailedPlateDisplayAlertMessage = plate => $"{plate.EnsureMaxLength(7).ToUpper()} não parece ser uma placa valida";

        private readonly Func<AppQuery, AppQuery> PlateEntry;
        private readonly Func<AppQuery, AppQuery> CreateFolderButton;
        private readonly Func<AppQuery, AppQuery> FailedPlateDisplayAlertTitle;
        private readonly Func<AppQuery, AppQuery> FailedPlateDisplayAlertCancel;
        private readonly Func<AppQuery, string, AppQuery> FailedPlateDisplayAlertMessage;

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = x => x.Marked(nameof(NewVehiclePage)),
            IOS = x => x.Marked(nameof(NewVehiclePage))
        };

        public NewVehiclePage()
        {
            PlateEntry = x => x.Marked(PLATE_ENTRY_ID);
            CreateFolderButton = x => x.Marked(CREATE_FOLDER_ENTRY_ID);
            FailedPlateDisplayAlertTitle = x => x.Marked(FAILED_PLATE_DISPLAY_ALERT_TITLE);
            FailedPlateDisplayAlertCancel = x => x.Marked(FAILED_PLATE_DISPLAY_ALERT_CANCEL);
            FailedPlateDisplayAlertMessage = (x, plate) => x.Marked(CreateFailedPlateDisplayAlertMessage(plate));
        }

        public NewVehiclePage EnterPlate(string plate, bool clearBefore = false)
        {
            if (clearBefore)
            {
                ClearPlateEntry();
            }

            App.EnterText(PlateEntry, plate);
            App.Screenshot(nameof(EnterPlate));
            return this;
        }

        public NewVehiclePage ClearPlateEntry()
        {
            App.ClearText(PlateEntry);
            App.Screenshot(nameof(ClearPlateEntry));
            return this;
        }

        public NewVehiclePage WaitForWrongPlateError(string plate)
        {
            App.WaitForElement(FailedPlateDisplayAlertTitle);
            App.WaitForElement(x => FailedPlateDisplayAlertMessage(x, plate));
            App.WaitForElement(FailedPlateDisplayAlertCancel);
            App.Screenshot(nameof(WaitForWrongPlateError));
            return this;
        }

        public NewVehiclePage ClickRetryPlate()
        {
            App.Tap(FailedPlateDisplayAlertCancel);
            App.Screenshot(nameof(ClickRetryPlate));
            return this;
        }

        public NewVehiclePage ConfirmPlate()
        {
            App.Tap(CreateFolderButton);
            App.Screenshot(nameof(ConfirmPlate));
            return this;
        }

        public NewVehiclePage CreateVehicleFolder(string plate, bool clearBefore = false, TimeSpan? timeSpan = null)
        {
            EnterPlate(plate, clearBefore);
            ConfirmPlate();
            WaitForPageToLeave(timeSpan);
            App.Screenshot(nameof(CreateVehicleFolder));
            return this;
        }
    }
}
