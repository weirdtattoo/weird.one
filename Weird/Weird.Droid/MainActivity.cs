using System;

using Android.App;
using Android.Content.PM;
 
using Android.OS;

namespace Weird.Droid
{
    [Activity(Label = "Weird", Icon = "@drawable/icon", MainLauncher = true,
         ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            string dbPath = FileAccessHelper.GetLocalFilePath(this.Assets, "bank.db");

            LoadApplication(new App(dbPath));
        }
    }

}
  


