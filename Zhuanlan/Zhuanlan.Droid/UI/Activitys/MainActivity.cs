using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using System;
using Zhuanlan.Droid.UI.Fragments;
using BottomNavigationBar;
using BottomNavigationBar.Listeners;
using FragmentManager = Android.Support.V4.App.FragmentManager;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
using Zhuanlan.Droid.UI.Views;
using Zhuanlan.Droid.Presenter;

namespace Zhuanlan.Droid.UI.Activitys
{
    [Activity]
    public class MainActivity : AppCompatActivity, IOnMenuTabClickListener, IMainView
    {
        private BottomBar bottomBar;
        private int lastSelecteID;//上一次选中的menuItemId
        private FragmentManager fm;
        private HomeFragment homeFragment;
        private ArticleFragment articleFragment;
        private ColumnFragment columnFragment;
        private LikeFragment likeFragment;
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


        public void SwitchArticle()
        {
            FragmentTransaction transaction = fm.BeginTransaction();
            if (articleFragment == null)
            {
                articleFragment = new ArticleFragment();
                transaction.Add(Resource.Id.frameContent, articleFragment).Commit();
            }
            else
            {
                transaction.Show(articleFragment).Commit();
            }
        }

        public void SwitchColumn()
        {
            FragmentTransaction transaction = fm.BeginTransaction();
            if (columnFragment == null)
            {
                columnFragment = new ColumnFragment();
                transaction.Add(Resource.Id.frameContent, columnFragment).Commit();
            }
            else
            {
                transaction.Show(columnFragment).Commit();
            }
        }

        public void SwitchLike()
        {
            FragmentTransaction transaction = fm.BeginTransaction();
            if (likeFragment == null)
            {
                likeFragment = new LikeFragment();
                transaction.Add(Resource.Id.frameContent, likeFragment).Commit();
            }
            else
            {
                transaction.Show(likeFragment).Commit();
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
        public void HideArticle()
        {
            FragmentTransaction transaction = fm.BeginTransaction();
            if (articleFragment != null)
            {
                transaction.Hide(articleFragment).Commit();
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

        public void HideLike()
        {
            FragmentTransaction transaction = fm.BeginTransaction();
            if (likeFragment != null)
            {
                transaction.Hide(likeFragment).Commit();
            }
        }
    }
}

