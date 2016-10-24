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
    public interface IColumnView
    {
        void GetColumnFail(string msg);
        void GetColumnSuccess(ColumnModel column);
    }
}