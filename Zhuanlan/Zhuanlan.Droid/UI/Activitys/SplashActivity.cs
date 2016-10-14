using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Zhuanlan.Droid.Utils;
using SQLite.Net;
using Zhuanlan.Droid.Model;
using SQLite.Net.Platform.XamarinAndroid;
using System.IO;
using Zhuanlan.Droid.UI.Views;
using Zhuanlan.Presenter;

namespace Zhuanlan.Droid.UI.Activitys
{
    [Activity(MainLauncher = true)]
    public class SplashActivity : Activity, ISplashView
    {
        private ISplashPresenter splashPresenter;
        private Handler handler;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            {
                Window.AddFlags(WindowManagerFlags.LayoutNoLimits);
            }
            SetContentView(Resource.Layout.Splash);

            handler = new Handler();
            splashPresenter = new SplashPresenter(this);
        }
        protected override async void OnResume()
        {
            base.OnResume();
            if (SQLiteUtils.Instance.Table<InitColumnModel>().Count() == 0)
            {
                await splashPresenter.GetInitColumns();
            }
            else
            {
                handler.PostDelayed(() =>
                {
                    StartActivity(new Intent(this, typeof(MainActivity)));
                }, 3000);
            }
        }
        public void GetInitColumnsFail(string msg)
        {
            handler.Post(() =>
            {
                Toast.MakeText(this, msg, ToastLength.Short).Show();
            });
        }

        public void GetInitColumnsSuccess(List<InitColumnModel> list)
        {
            if (SQLiteUtils.Instance.InsertAll(list) > 0)
            {
                handler.PostDelayed(() =>
                {
                    StartActivity(new Intent(this, typeof(MainActivity)));
                }, 2000);
            }
        }
    }
}