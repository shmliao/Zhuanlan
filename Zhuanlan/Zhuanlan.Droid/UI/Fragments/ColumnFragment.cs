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
using Zhuanlan.Droid.UI.Widgets;

namespace Zhuanlan.Droid.UI.Fragments
{
    public class ColumnFragment : Fragment, IOnLoadMoreListener
    {
        private BaseRecyclerView baseRecyclerView;
        private ColumnAdapter adapter;
        private int mCount = 1;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            return inflater.Inflate(Resource.Layout.FragmentColumn, container, false);
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            baseRecyclerView = view.FindViewById<BaseRecyclerView>(Resource.Id.baseRecyclerView);
            baseRecyclerView.OnLoadMoreListener = this;
            adapter= new ColumnAdapter(this.Activity);
            baseRecyclerView.SetAdapter(adapter);
            
            GetData();
        }
        private void GetData()
        {
            adapter.TopicList = SetList();
            adapter.NotifyDataSetChanged();
            baseRecyclerView.LoadMoreListenerCompleted();
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
        public void OnRefresh()
        {
            setRefresh();
            GetData();
        }

        public void OnLoadMore()
        {
            //mCount = mCount + 1;
            //GetData();
        }
        private void setRefresh()
        {
            adapter.TopicList.Clear();
            mCount = 1;
        }
    }
}