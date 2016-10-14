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

namespace Zhuanlan.Droid.UI.Adapters
{
    public class ColumnAdapter : RecyclerView.Adapter, View.IOnClickListener
    {
        private Context context;
        public List<InitColumnModel> TopicList;
        public ColumnAdapter(Context context)
        {
            this.context = context;
            TopicList = new List<InitColumnModel>();
        }
        public override int ItemCount
        {
            get
            {
                return TopicList.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            ItemViewHolder holder = (ItemViewHolder)viewHolder;
            var model = TopicList[position];
            holder.title.Text = model.Column;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.column_item, parent, false);
            return new ItemViewHolder(view);
        }
        public class ItemViewHolder : RecyclerView.ViewHolder
        {
            public TextView title { get; set; }
            public ItemViewHolder(View view)
                : base(view)
            {
                title = view.FindViewById<TextView>(Resource.Id.title);
            }
        }
        public void OnClick(View v)
        {
            throw new NotImplementedException();
        }
    }
}