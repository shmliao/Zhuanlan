using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Zhuanlan.Droid.UI.Adapters;
using Zhuanlan.Droid.UI.Listeners;
using Zhuanlan.Droid.Utils;
using Zhuanlan.Droid.Model;
using Zhuanlan.Droid.UI.Views;
using Zhuanlan.Droid.Presenter;
using Realms;
using System.Collections;

namespace Zhuanlan.Droid.UI.Fragments
{
    public class HomeFragment : Fragment, IHomeView, IOnLoadMoreListener, SwipeRefreshLayout.IOnRefreshListener
    {
        private Handler handler;
        private SwipeRefreshLayout swipeRefreshLayout;
        private RecyclerView recyclerView;
        private HomeAdapter adapter;
        private View notLoadingView;
        private int limit = 5;
        private int offset = 0;
        private IHomePresenter homePresenter;
        private Realm realm;
        private List<int> offsetList = new List<int>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            handler = new Handler();
            homePresenter = new HomePresenter(this);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            return inflater.Inflate(Resource.Layout.fragment_home, container, false); ;
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            swipeRefreshLayout = view.FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
            swipeRefreshLayout.SetOnRefreshListener(this);
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this.Activity));
            adapter = new HomeAdapter();
            adapter.OnLoadMoreListener = this;

            recyclerView.SetAdapter(adapter);
            recyclerView.Post(() =>
            {
                swipeRefreshLayout.Refreshing = true;
                OnRefresh();
            });
        }
        public async void OnLoadMoreRequested()
        {
            await homePresenter.GetPosts(offset);
        }

        public async void OnRefresh()
        {
            if (offset > 0)
                offset = 0;
            await homePresenter.GetPosts(offset);
        }

        public void GetPostsFail(string msg)
        {
            if (offset > 0)
            {
                adapter.ShowLoadMoreFailedView();
            }
            else
            {
                if (swipeRefreshLayout.Refreshing)
                {
                    swipeRefreshLayout.Refreshing = false;
                }
                Toast.MakeText(this.Activity, msg, ToastLength.Short).Show();
            }
        }

        public void GetPostsSuccess(List<PostModel> lists)
        {
            if (offset == 0)
            {
                handler.Post(() =>
                {
                    if (swipeRefreshLayout.Refreshing)
                    {
                        swipeRefreshLayout.Refreshing = false;
                    }
                    adapter.NewData(lists);
                    adapter.RemoveAllFooterView();
                    offset += lists.Count;
                });
            }
            else
            {
                handler.Post(() =>
                {
                    adapter.AddData(lists);
                    offset += lists.Count;
                });
            }
        }
    }
}