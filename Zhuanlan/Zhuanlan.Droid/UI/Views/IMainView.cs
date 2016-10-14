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

namespace Zhuanlan.Droid.UI.Views
{
    public interface IMainView
    {
        void SwitchHome();
        void SwitchArticle();
        void SwitchColumn();
        void SwitchLike();
        void HideHome();
        void HideArticle();
        void HideColumn();
        void HideLike();
    }
}