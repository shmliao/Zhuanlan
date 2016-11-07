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
    public class HomePresenter : IHomePresenter
    {
        private int limit = 10;
        private IHomeView homeView;
        public HomePresenter(IHomeView homeView)
        {
            this.homeView = homeView;
        }
        public async Task GetPosts(int offset)
        {
            try
            {
                var body = await OkHttpUtils.Instance.GetAsyn(ApiUtils.GetRecommendationPosts(limit, offset));
                var posts = JsonConvert.DeserializeObject<List<PostModel>>(body);
                homeView.GetPostsSuccess(posts);
            }
            catch (Exception ex)
            {
                homeView.GetPostsFail(ex.Message);
            }
        }
    }
}