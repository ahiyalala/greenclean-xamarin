using System;

using Android.App;
using Android.Content.PM;
using GreenClean;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

namespace GreenClean.Droid
{
    [Activity(Label = "GreenClean", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            // set the layout resources first
            FormsAppCompatActivity.ToolbarResource = Resource.Layout.Toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.Tabbar;

            // then call base.OnCreate and the Xamarin.Forms methods
            base.OnCreate(bundle);
            Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

