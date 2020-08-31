using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Fdo.Contato.Vistoria.Droid.Extensions;
using FFImageLoading.Forms.Platform;
using Java.Util.Concurrent;
using Plugin.FileUploader;
using System.Linq;

namespace Fdo.Contato.Vistoria.Droid
{
    [Activity(Label = "Fdo.Contato.Vistoria", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            DIPS.Xamarin.UI.Android.Library.Initialize();
            CachedImageRenderer.Init(enableFastRenderer: true);
            CachedImageRenderer.InitImageViewHandler();
            FileUploadManager.UploadTimeoutUnit = TimeUnit.Minutes;
            FileUploadManager.SocketUploadTimeout = 2;
            FileUploadManager.ConnectUploadTimeout = 2;
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnDestroy()
        {
            Application.Context.GetExternalFilesDir("Pictures").Path.DeleteDirectoryIfExists();
            Application.Context.GetExternalCacheDirs().FirstOrDefault()?.Path.DeleteDirectoryIfExists();
            base.OnDestroy();
        }
    }
}