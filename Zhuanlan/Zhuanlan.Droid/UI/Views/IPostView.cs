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
using Zhuanlan.Droid.Model;

namespace Zhuanlan.Droid.UI.Views
{
    public interface IPostView
    {
        void GetPostFail(string msg);
        void GetPostSuccess(PostModel post);
        void GetContributedFail(string msg);
        void GetContributedSuccess(List<ContributedModel> lists);
    }
}