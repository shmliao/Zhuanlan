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
    public class PostModel : RealmObject
    {
        /// <summary>
        /// 文章标题大图是否全屏
        /// </summary>
        public bool IsTitleImageFullScreen { get; set; }
        /// <summary>
        /// 评级
        /// </summary>
        public string Rating { get; set; }
        /// <summary>
        /// 源路径
        /// </summary>
        public string SourceUrl { get; set; }
        /// <summary>
        /// 发表时间
        /// </summary>
        public string PublishedTime { get; set; }
        /// <summary>
        /// 链接信息
        /// </summary>
        public LinkModel Links { get; set; }
        /// <summary>
        /// 该专栏的创建者信息
        /// </summary>
        public AuthorModel Author { get; set; }
        /// <summary>
        /// 文章网页内容获取
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 文章标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 文章标题大图url(需要注意的是,titleImage的值有可能为空)，和头像一样，也可以组合不同的尺寸参数获取不同尺寸的图片
        /// </summary>
        public string TitleImage { get; set; }
        /// <summary>
        /// 文章简要信息
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// HTML格式的文章内容详情，可以通过WebView或者UIWebView展示内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 文章状态(是否发表)
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// api请求地址
        /// </summary>
        public string Href { get; set; }
        public MetaModel Meta { get; set; }
        /// <summary>
        /// 评论权限
        /// </summary>
        public string CommentPermission { get; set; }
        /// <summary>
        /// 短网址?
        /// </summary>
        public string SnapshotUrl { get; set; }
        /// <summary>
        /// 是否可以评论
        /// </summary>
        public bool CanComment { get; set; }
        public int Slug { get; set; }
        /// <summary>
        /// 评论数量
        /// </summary>
        public int CommentsCount { get; set; }
        /// <summary>
        /// 赞的数量
        /// </summary>
        public int LikesCount { get; set; }
    }
}