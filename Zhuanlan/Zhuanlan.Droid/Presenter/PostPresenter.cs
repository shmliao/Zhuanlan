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
    public class PostPresenter : IPostPresenter
    {
        IPostView postView;
        public PostPresenter(IPostView postView)
        {
            this.postView = postView;
        }
        public async Task GetPost(string slug)
        {
            try
            {
                var post = JsonConvert.DeserializeObject<PostModel>(await OkHttpUtils.Instance.GetAsyn(ApiUtils.GetPost(slug)));
                postView.GetPostSuccess(post);
            }
            catch (Exception ex)
            {
                postView.GetPostFail(ex.Message);
            }
        }
    }
}