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
using System.Threading.Tasks;
using Zhuanlan.Droid.Model;

namespace Zhuanlan.Droid.Presenter
{
    public interface IColumnPresenter
    {
        Task GetColumn(string slug);
        Task GetPosts(string slug, int offset);
    }
}