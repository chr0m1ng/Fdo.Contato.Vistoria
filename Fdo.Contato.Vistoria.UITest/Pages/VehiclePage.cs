using System;
using Xamarin.UITest.Queries;

namespace Fdo.Contato.Vistoria.UITest.Pages
{
    public class VehiclePage : BasePage
    {
        private const string ADD_MEDIA_TEXT_BUTTON = "";
        private const string OPEN_CAMERA_TEXT_BUTTON = "";
        private const string OPEN_GALLERY_TEXT_BUTTON = "";

        public readonly Func<AppQuery, AppQuery> MediaButton;
        public readonly Func<AppQuery, AppQuery> CameraButton;
        public readonly Func<AppQuery, AppQuery> GalleryButton;

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = x => x.Marked(nameof(VehiclePage)),
            IOS = x => x.Marked(nameof(VehiclePage))
        };

        public VehiclePage()
        {
            MediaButton = x => x.Text(ADD_MEDIA_TEXT_BUTTON);
            CameraButton = x => x.Marked(OPEN_CAMERA_TEXT_BUTTON);
            GalleryButton = x => x.Marked(OPEN_GALLERY_TEXT_BUTTON);
        }

        public VehiclePage OpenGallery()
        {
            TapAddMedia();
            App.Tap(GalleryButton);
            App.Screenshot(nameof(OpenGallery));
            return this;
        }

        public VehiclePage OpenCamera()
        {
            TapAddMedia();
            App.Tap(CameraButton);
            App.Screenshot(nameof(OpenCamera));
            return this;
        }

        public VehiclePage TapAddMedia()
        {
            App.Tap(MediaButton);
            App.Screenshot(nameof(TapAddMedia));
            return this;
        }
    }
}
