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
using Realms;

namespace Zhuanlan.Droid.Model
{
    /// <summary>
    /// 文章话题信息
    /// </summary>
    public class PostTopicModel : RealmObject
    {
        /// <summary>
        /// 该话题下文章数
        /// </summary>
        public int PostsCount { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
    }
}