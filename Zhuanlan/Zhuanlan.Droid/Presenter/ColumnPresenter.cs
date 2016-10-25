using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Zhuanlan.Droid.Model;
using Zhuanlan.Droid.UI.Views;
using Newtonsoft.Json;
using Zhuanlan.Droid.Utils;

namespace Zhuanlan.Droid.Presenter
{
    public class ColumnPresenter : IColumnPresenter
    {
        private int limit = 10;
        IColumnView columnView;
        public ColumnPresenter(IColumnView columnView)
        {
            this.columnView = columnView;
        }
        public async Task GetColumn(string slug)
        {
            try
            {
                var column = JsonConvert.DeserializeObject<ColumnModel>(await OkHttpUtils.Instance.GetAsyn(ApiUtils.GetColumn(slug)));
                columnView.GetColumnSuccess(column);
                await GetPosts(slug, 0);
            }
            catch (Exception ex)
            {
                columnView.GetColumnFail(ex.Message);
            }
        }
        public async Task GetPosts(string slug, int offset)
        {
            try
            {
                var posts = JsonConvert.DeserializeObject<List<PostModel>>(await OkHttpUtils.Instance.GetAsyn(ApiUtils.GetPosts(slug, limit, offset)));
                columnView.GetPostsSuccess(posts);
            }
            catch (Exception ex)
            {
                columnView.GetColumnFail(ex.Message);
            }
        }
    }
}