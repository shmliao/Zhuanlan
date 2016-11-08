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
    /// <summary>
    /// 专栏话题信息
    /// </summary>
    public class TopicModel
    {
        /// <summary>
        /// 话题在知乎官网中的地址
        /// </summary>
        public string Url { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
    }
}