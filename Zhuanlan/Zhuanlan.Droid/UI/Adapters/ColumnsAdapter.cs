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
using Zhuanlan.Droid.Model;
using Zhuanlan.Droid.UI.Listeners;
using Java.Lang;
using Square.Picasso;
using Zhuanlan.Droid.UI.Widgets;
using Zhuanlan.Droid.UI.Activitys;

namespace Zhuanlan.Droid.UI.Adapters
{
    public class ColumnsAdapter : RecyclerView.Adapter, View.IOnClickListener
    {
        public const int LoadingView = 0x00000111;
        public const int FooterView = 0x00000222;
        private Context context;
        protected LayoutInflater layoutInflater;
        private LinearLayout footerLayout;
        private LinearLayout copyFooterLayout;
        private View loadMoreFailedView;

        private bool loadingMoreEnable;

        public List<ColumnModel> List;
        public IOnLoadMoreListener OnLoadMoreListener;

        public ColumnsAdapter()
        {
            List = new List<ColumnModel>();
        }
        public override int ItemCount
        {
            get
            {
                var count = 0;
                if (List.Count > 0)
                {
                    count = List.Count + 1;
                }
                else
                {
                    if (footerLayout != null)
                    {
                        count = 1;
                    }
                }
                return count;
            }
        }
        public override int GetItemViewType(int position)
        {
            if (List.Count == 0 || position == List.Count)
            {
                if (footerLayout == null)
                {
                    return LoadingView;
                }
                else
                {
                    return FooterView;
                }
            }
            return base.GetItemViewType(position);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            int viewType = viewHolder.ItemViewType;

            switch (viewType)
            {
                case LoadingView:
                    AddLoadMore(viewHolder);
                    break;
                case FooterView:
                    break;
                default:
                    var item = (ItemViewHolder)viewHolder;
                    var model = List[position];
                    item.ItemView.Tag = model.Slug;
                    item.ItemView.SetOnClickListener(this);
                    item.title.Text = model.Name;
                    if (model.Description == null || model.Description == "")
                    {
                        item.description.Visibility = ViewStates.Gone;
                    }
                    else
                    {
                        item.description.Text = model.Description;
                        item.description.Visibility = ViewStates.Visible;
                    }
                    item.followersCount.Text = model.FollowersCount + " ÈË¹Ø×¢";
                    item.postsCount.Text = " ¡¤ " + model.PostsCount + " ÎÄÕÂ";

                    var avatar = model.Avatar.Template.Replace("{id}", model.Avatar.ID);
                    avatar = avatar.Replace("{size}", "l");
                    Picasso.With(context)
                                .Load(avatar)
                               .Transform(new CircleTransform())
                               .Placeholder(Resource.Drawable.ic_image_placeholder)
                               .Error(Resource.Drawable.ic_image_placeholder)
                               .Into(item.avatar);
                    break;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            this.context = parent.Context;
            this.layoutInflater = LayoutInflater.From(context);
            switch (viewType)
            {
                case LoadingView:
                    return new LoadingViewHolder(layoutInflater.Inflate(Resource.Layout.recyclerview_loadmore, parent, false));
                case FooterView:
                    return new FooterViewHolder(footerLayout);
                default:
                    return new ItemViewHolder(layoutInflater.Inflate(Resource.Layout.columns_item, parent, false));
            }
        }
        public class ItemViewHolder : RecyclerView.ViewHolder
        {
            public ImageView avatar { get; set; }
            public TextView title { get; set; }
            public TextView description { get; set; }
            public TextView followersCount { get; set; }
            public TextView postsCount { get; set; }
            public ItemViewHolder(View view)
                : base(view)
            {
                avatar = view.FindViewById<ImageView>(Resource.Id.llAvatar);
                title = view.FindViewById<TextView>(Resource.Id.txtTitle);
                description = view.FindViewById<TextView>(Resource.Id.txtDescription);
                followersCount = view.FindViewById<TextView>(Resource.Id.txtFollowersCount);
                postsCount = view.FindViewById<TextView>(Resource.Id.txtPostsCount);
            }
        }
        public class LoadingViewHolder : RecyclerView.ViewHolder
        {
            public LoadingViewHolder(View view)
                : base(view)
            {
            }
        }
        public class FooterViewHolder : RecyclerView.ViewHolder
        {
            public FooterViewHolder(View view)
                : base(view)
            {
            }
        }
        public void NewData(List<ColumnModel> list)
        {
            this.List = list;
            if (loadMoreFailedView != null)
            {
                RemoveFooterView(loadMoreFailedView);
            }
            NotifyDataSetChanged();
        }
        public void Remove(int position)
        {
            List.RemoveAt(position);
            NotifyItemRemoved(position);
        }
        public void Add(int position, ColumnModel item)
        {
            List.Insert(position, item);
            NotifyItemInserted(position);
        }
        public void AddData(List<ColumnModel> newList)
        {
            loadingMoreEnable = false;
            this.List.AddRange(newList);
            NotifyItemRangeChanged(List.Count - newList.Count, newList.Count);
        }
        public void AddFooterView(View footer)
        {
            AddFooterView(footer, -1);
        }
        public void AddFooterView(View footer, int index)
        {
            if (footerLayout == null)
            {
                if (copyFooterLayout == null)
                {
                    footerLayout = new LinearLayout(footer.Context);
                    footerLayout.Orientation = Orientation.Vertical;
                    footerLayout.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                    copyFooterLayout = footerLayout;
                }
                else
                {
                    footerLayout = copyFooterLayout;
                }
            }
            index = index >= footerLayout.ChildCount ? -1 : index;
            footerLayout.AddView(footer, index);
            this.NotifyItemChanged(ItemCount);
        }
        public void RemoveFooterView(View footer)
        {
            if (footerLayout == null) return;

            footerLayout.RemoveView(footer);
            if (footerLayout.ChildCount == 0)
            {
                footerLayout = null;
            }
            this.NotifyDataSetChanged();
        }
        public void RemoveAllFooterView()
        {
            if (footerLayout == null) return;

            footerLayout.RemoveAllViews();
            footerLayout = null;
        }
        public void ShowLoadMoreFailedView()
        {
            LoadComplete();
            if (loadMoreFailedView == null)
            {
                loadMoreFailedView = layoutInflater.Inflate(Resource.Layout.recyclerview_loadmore_failed, null);
                loadMoreFailedView.Click += delegate
                {
                    RemoveFooterView(loadMoreFailedView);
                };
            }
            AddFooterView(loadMoreFailedView);
        }
        public void LoadComplete()
        {
            loadingMoreEnable = false;
            this.NotifyItemChanged(ItemCount);
        }

        private void AddLoadMore(RecyclerView.ViewHolder holder)
        {
            if (!loadingMoreEnable)
            {
                loadingMoreEnable = true;
                OnLoadMoreListener.OnLoadMoreRequested();
            }
        }
        public void OnClick(View v)
        {
            if (v.Tag != null)
            {
                ColumnActivity.Start(context, v.Tag.ToString());
            }
        }
    }
}