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
    public class TopicModel : RealmObject
    {
        public string Url { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
    }
}