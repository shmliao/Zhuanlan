using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Zhuanlan.Droid.UI.Fragments;
using FragmentManager = Android.Support.V4.App.FragmentManager;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
using Zhuanlan.Droid.UI.Views;
using Zhuanlan.Droid.Presenter;
using BottomNavigationBar;
using BottomNavigationBar.Listeners;
using Com.Umeng.Analytics;

namespace Zhuanlan.Droid.UI.Activitys
{
    [Activity(MainLauncher = true, LaunchMode = Android.Content.PM.LaunchMode.SingleTask)]
    public class MainActivity : AppCompatActivity, IMainView, IOnMenuTabClickListener
    {
        private BottomBar bottomBar;
        private int lastSelecteID;//上一次选中的menuItemId
        private FragmentManager fm;
        private HomeFragment homeFragment;
        private ColumnsFragment columnFragment;
        private IMainPresenter mainPresenter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            mainPresenter = new MainPresenter(this);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            fm = SupportFragmentManager;

            bottomBar = BottomBar.Attach(this, bundle);
            bottomBar.UseFixedMode();

            bottomBar.SetItems(Resource.Menu.bottombar_menu);
            bottomBar.SetOnMenuTabClickListener(this);

            MobclickAgent.SetDebugMode(true);
            MobclickAgent.OpenActivityDurationTrack(false);
            MobclickAgent.SetScenarioType(this, MobclickAgent.EScenarioType.EUmNormal);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            bottomBar.OnSaveInstanceState(outState);
        }
        public void OnMenuTabSelected(int menuItemId)
        {
            if (lastSelecteID > 0)
            {
                mainPresenter.HideNavigationBar(lastSelecteID);
            }
            mainPresenter.SwitchNavigationBar(menuItemId);
            lastSelecteID = menuItemId;
        }

        public void OnMenuTabReSelected(int menuItemId)
        {
        }
        protected override void OnResume()
        {
            base.OnResume();
            MobclickAgent.OnResume(this);
        }
        protected override void OnPause()
        {
            base.OnPause();
            MobclickAgent.OnPause(this);
        }

        public void SwitchHome()
        {
            FragmentTransaction transaction = fm.BeginTransaction();
            if (homeFragment == null)
            {
                homeFragment = new HomeFragment();
                transaction.Add(Resource.Id.frameContent, homeFragment).Commit();
            }
            else
            {
                transaction.Show(homeFragment).Commit();
            }
        }

        public void SwitchColumn()
        {
            FragmentTransaction transaction = fm.BeginTransaction();
            if (columnFragment == null)
            {
                columnFragment = new ColumnsFragment();
                transaction.Add(Resource.Id.frameContent, columnFragment).Commit();
            }
            else
            {
                transaction.Show(columnFragment).Commit();
            }
        }


        public void HideHome()
        {
            FragmentTransaction transaction = fm.BeginTransaction();
            if (homeFragment != null)
            {
                transaction.Hide(homeFragment).Commit();
            }
        }

        public void HideColumn()
        {
            FragmentTransaction transaction = fm.BeginTransaction();
            if (columnFragment != null)
            {
                transaction.Hide(columnFragment).Commit();
            }
        }

    }
}

