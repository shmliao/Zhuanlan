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
using Android.Webkit;
using Android.Util;

namespace Zhuanlan.Droid.Utils
{
    public class PostWebView : WebView
    {
        private const string PostCSS = "file:///android_asset/post.css";

        private const string HtmlBegin = "" +
            "<!DOCTYPE html>\n" +
            "<html>\n";
        private const string HeadBegin = "" +
            "<head>\n" +
            "<meta charset=\"UTF-8\">\n" +
            "<meta name=\"viewport\" content=\"width=device-width,initial-scale=1,maximum-scale=1\">\n";
        private const string HeadlEnd = "" +
                "</head>\n" +
                "<body>\n";
        private const string HtmlEnd = "" +
                "</body>\n" +
                "</html>";

        public PostWebView(Context context)
            : base(context)
        {
        }
        public PostWebView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }
        public void LoadRenderedContent(string body)
        {
            var data = HtmlBegin + HeadBegin + "<link type=\"text/css\" rel=\"stylesheet\" href=\"" + PostCSS + "\">\n" + HeadlEnd + "<div class=\"post-content\">" + body + "</div>\n" + HtmlEnd;
            LoadDataWithBaseURL(null, data, "text/html", "utf-8", null);
        }
    }
}