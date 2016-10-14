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
using Android.Support.V7.Widget;

namespace Zhuanlan.Droid.UI.Widgets
{
    public class BaseRecyclerViewOnScroll : RecyclerView.OnScrollListener
    {
        private BaseRecyclerView baseRecyclerView;

        public BaseRecyclerViewOnScroll(BaseRecyclerView baseRecyclerView)
        {
            this.baseRecyclerView = baseRecyclerView;
        }
        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);

            int lastItem = 0;
            int firstItem = 0;

            RecyclerView.LayoutManager layoutManager = recyclerView.GetLayoutManager();
            int totalItemCount = layoutManager.ItemCount;
            LinearLayoutManager linearLayoutManager = ((LinearLayoutManager)layoutManager);
            firstItem = linearLayoutManager.FindFirstCompletelyVisibleItemPosition();
            lastItem = linearLayoutManager.FindLastCompletelyVisibleItemPosition();
            if (lastItem == -1) lastItem = linearLayoutManager.FindLastVisibleItemPosition();
            //if (firstItem == 0 || firstItem == RecyclerView.NoPosition)
            //{
            //    if (baseRecyclerView.GetPullRefreshEnable())
            //        baseRecyclerView.SetSwipeRefreshEnable(true);
            //}
            //else
            //{
            //    baseRecyclerView.SetSwipeRefreshEnable(false);
            //}
            if ( !baseRecyclerView.IsRefresh
                    && baseRecyclerView.IsHasMore
                    && (lastItem == totalItemCount - 1)
                    && !baseRecyclerView.IsLoadMore
                    && (dx > 0 || dy > 0))
            {
                baseRecyclerView.IsLoadMore=true;
                baseRecyclerView.LoadMore();
            }
        }
    }
}