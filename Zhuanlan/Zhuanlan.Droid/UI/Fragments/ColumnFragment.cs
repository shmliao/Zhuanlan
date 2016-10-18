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

namespace Zhuanlan.Droid.UI.Fragments
{
    public class ColumnFragment : Fragment, IOnLoadMoreListener, SwipeRefreshLayout.IOnRefreshListener
    {
        private Handler handler;
        private SwipeRefreshLayout swipeRefreshLayout;
        private RecyclerView recyclerView;
        private ColumnAdapter adapter;
        private View notLoadingView;
        private int mCount = 1;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            handler = new Handler();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            return inflater.Inflate(Resource.Layout.FragmentColumn, container, false);
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            swipeRefreshLayout = view.FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
            swipeRefreshLayout.SetOnRefreshListener(this);
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this.Activity));
            adapter = new ColumnAdapter();
            adapter.OnLoadMoreListener = this;
            
            recyclerView.SetAdapter(adapter);

        }
        private List<InitColumnModel> SetList()
        {
            List<InitColumnModel> dataList = new List<InitColumnModel>();
            int start = 10 * (mCount - 1);
            for (int i = start; i < 10 * mCount; i++)
            {
                dataList.Add(new InitColumnModel() { ID = 0, Column = "Frist" + i });
            }
            return dataList;
        }

        public void OnLoadMoreRequested()
        {
            recyclerView.Post(() =>
            {
                if (mCount >= 2)
                {
                    handler.PostDelayed(() =>
                    {
                        adapter.LoadComplete();
                        if (notLoadingView == null)
                        {
                            notLoadingView = GetLayoutInflater(null).Inflate(Resource.Layout.recyclerview_notloading, (ViewGroup)recyclerView.Parent, false);
                        }
                        adapter.AddFooterView(notLoadingView);
                    }, 1000);
                }
                else
                {
                    handler.PostDelayed(() =>
                    {
                        adapter.ShowLoadMoreFailedView();
                        //mCount++;
                        //adapter.AddData(SetList());
                    }, 1000);
                }
            });
        }

        public void OnRefresh()
        {
            handler.PostDelayed(() =>
            {
                mCount = 1;
                adapter.NewData(SetList());
                adapter.RemoveAllFooterView();
                swipeRefreshLayout.Refreshing = false;
            }, 1000);
        }
    }
}