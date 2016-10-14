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
using Android.Util;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Zhuanlan.Droid.UI.Adapters;
using Zhuanlan.Droid.UI.Listeners;
using Android.Views.Animations;
using Android.Animation;

namespace Zhuanlan.Droid.UI.Widgets
{
    public class BaseRecyclerView : LinearLayout
    {
        private SwipeRefreshLayout swipeRefreshLayout;
        private RecyclerView recyclerView;
        private LinearLayout loadMoreLayout;
        private View footerView;
        private FrameLayout emptyView;
        public IOnLoadMoreListener OnLoadMoreListener { get; set; }
        public bool IsHasMore { get; set; }
        public bool IsRefresh { get; set; }
        public bool IsLoadMore { get; set; }

        private EmptyAdapterDataObserver emptyDataObserver;

        public BaseRecyclerView(Context context) : base(context)
        {
            InitView(context);
            IsHasMore = true;
        }

        public BaseRecyclerView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            InitView(context);
            IsHasMore = true;
        }
        public void InitView(Context context)
        {
            View view = LayoutInflater.From(context).Inflate(Resource.Layout.base_recylerview_layout, null);

            swipeRefreshLayout = view.FindViewById<SwipeRefreshLayout>(Resource.Id.refreshLayout);
            swipeRefreshLayout.Refresh += delegate
            {
                if (!IsRefresh)
                {
                    IsRefresh = true;
                    Refresh();
                }
            };

            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.VerticalScrollBarEnabled = true;
            recyclerView.HasFixedSize = true;
            recyclerView.SetItemAnimator(new DefaultItemAnimator());

            recyclerView.AddOnScrollListener(new BaseRecyclerViewOnScroll(this));
            recyclerView.SetOnTouchListener(new OnTouchRecyclerView(this));

            footerView = view.FindViewById(Resource.Id.footerView);
            emptyView = view.FindViewById<FrameLayout>(Resource.Id.emptyView);

            loadMoreLayout = view.FindViewById<LinearLayout>(Resource.Id.loadMoreLayout);

            footerView.Visibility = ViewStates.Gone;
            emptyView.Visibility = ViewStates.Gone;

            var linearLayoutManager = new LinearLayoutManager(view.Context);
            recyclerView.SetLayoutManager(linearLayoutManager);

            this.AddView(view);
        }
        public void ScrollToTop()
        {
            recyclerView.ScrollToPosition(0);
        }
        public void SetEmptyView(View empty)
        {
            emptyView.RemoveAllViews();
            emptyView.AddView(empty);
        }
        public void SetAdapter(RecyclerView.Adapter adapter)
        {
            if (emptyDataObserver == null)
            {
                emptyDataObserver = new EmptyAdapterDataObserver(this);
            }
            RecyclerView.Adapter oldAdapter = recyclerView.GetAdapter();
            if (oldAdapter != null)
            {
                oldAdapter.UnregisterAdapterDataObserver(emptyDataObserver);
            }
            if (adapter != null)
            {
                recyclerView.SetAdapter(adapter);
                ShowEmptyView();
                adapter.RegisterAdapterDataObserver(emptyDataObserver);
            }
        }
        public void ShowEmptyView()
        {
            RecyclerView.Adapter adapter = recyclerView.GetAdapter();
            if (adapter != null && emptyView.ChildCount != 0)
            {
                if (adapter.ItemCount == 0)
                {
                    footerView.Visibility = ViewStates.Gone;
                    emptyView.Visibility = ViewStates.Visible;
                }
                else
                {
                    emptyView.Visibility = ViewStates.Gone;
                }
            }
        }
        protected override void OnDetachedFromWindow()
        {
            base.OnDetachedFromWindow();
            RecyclerView.Adapter adapter = recyclerView.GetAdapter();
            if (adapter != null)
            {
                adapter.UnregisterAdapterDataObserver(emptyDataObserver);
            }
        }
        public void SetRefreshing(bool isRefreshing)
        {
            swipeRefreshLayout.Post(() =>
            {
                swipeRefreshLayout.Refreshing = isRefreshing;
            });
        }

        public void Refresh()
        {
            if (OnLoadMoreListener != null)
            {
                OnLoadMoreListener.OnRefresh();
            }
        }

        public void LoadMore()
        {
            if (OnLoadMoreListener != null && IsHasMore)
            {
                footerView.Animate()
                        .TranslationY(0)
                        .SetDuration(300)
                        .SetInterpolator(new AccelerateDecelerateInterpolator())
                        .SetListener(new BaseAnimatorListenerAdapter(this))
                        .Start();
                Invalidate();
                OnLoadMoreListener.OnLoadMore();
            }
        }
        public void LoadMoreListenerCompleted()
        {
            IsRefresh = false;
            SetRefreshing(false);

            IsLoadMore = false;
            footerView.Animate()
                    .TranslationY(footerView.Height)
                    .SetDuration(300)
                    .SetInterpolator(new AccelerateDecelerateInterpolator())
                    .Start();

        }
        public class BaseAnimatorListenerAdapter : AnimatorListenerAdapter
        {
            BaseRecyclerView baseRecyclerView;
            public BaseAnimatorListenerAdapter(BaseRecyclerView baseRecyclerView)
            {
                this.baseRecyclerView = baseRecyclerView;
            }
            public override void OnAnimationStart(Animator animation)
            {
                base.OnAnimationStart(animation);
                baseRecyclerView.footerView.Visibility = ViewStates.Visible;
            }
        }

        public class EmptyAdapterDataObserver : RecyclerView.AdapterDataObserver
        {
            BaseRecyclerView baseRecyclerView;
            public EmptyAdapterDataObserver(BaseRecyclerView baseRecyclerView)
            {
                this.baseRecyclerView = baseRecyclerView;
            }
            public override void OnChanged()
            {
                baseRecyclerView.ShowEmptyView();
            }
        }
        public class OnTouchRecyclerView : Java.Lang.Object, IOnTouchListener
        {
            BaseRecyclerView baseRecyclerView;
            public OnTouchRecyclerView(BaseRecyclerView baseRecyclerView)
            {
                this.baseRecyclerView = baseRecyclerView;
            }
            public bool OnTouch(View v, MotionEvent e)
            {
                return baseRecyclerView.IsRefresh || baseRecyclerView.IsLoadMore;
            }
        }
    }
}