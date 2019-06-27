using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Pinterest
{
    [Activity(Label = "@string/app_name", Theme = "@style/SplashTheme", MainLauncher = true)]
    class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));
            Thread.Sleep(2000);
            Finish();
            OverridePendingTransition(0, 0);
        }
    }
}