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

namespace Zhuanlan.Droid.Model
{
    public class AvatarModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Urlƴ��ģ��
        /// </summary>
        public string Template { get; set; }
    }
}