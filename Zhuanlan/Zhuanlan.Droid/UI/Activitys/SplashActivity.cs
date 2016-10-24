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
using Zhuanlan.Droid.Model;
using System.IO;
using Zhuanlan.Droid.UI.Views;
using Zhuanlan.Presenter;
using Realms;

namespace Zhuanlan.Droid.UI.Activitys
{
    [Activity(MainLauncher = true)]
    public class SplashActivity : Activity, ISplashView
    {
        private ISplashPresenter splashPresenter;
        private Realm realm;
        private Handler handler;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            {
                Window.AddFlags(WindowManagerFlags.LayoutNoLimits);
            }
            SetContentView(Resource.Layout.Splash);

            realm = Realm.GetInstance();
            handler = new Handler();
            splashPresenter = new SplashPresenter(this);
        }
        protected override async void OnResume()
        {
            base.OnResume();
            if (realm.All<ColumnModel>().Count() == 0)
            {
                var stream = this.Assets.Open("zhuanlan.json");
                await splashPresenter.GetInitColumns(stream);
            }
            else
            {
                handler.PostDelayed(() =>
                {
                    //StartActivity(new Intent(this, typeof(MainActivity)));
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

        public async void GetInitColumnsSuccess(List<ColumnModel> list)
        {
            foreach (var item in list)
            {
                await realm.WriteAsync((realm) =>
                 {
                     var column = realm.CreateObject<ColumnModel>();
                     column.Slug = item.Slug;
                 });
            }
            if (realm.All<ColumnModel>().Count() > 0)
            {
                handler.Post(() =>
                {
                    //StartActivity(new Intent(this, typeof(MainActivity)));
                });
            }
        }
    }
}