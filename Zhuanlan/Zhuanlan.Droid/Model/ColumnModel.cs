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
    public class ColumnModel 
    {
        /// <summary>
        /// 关注数
        /// </summary>
        public int FollowersCount { get; set; }
        /// <summary>
        /// 创建者信息
        /// </summary>
        public AuthorModel Creator { get; set; }
        /// <summary>
        /// 专栏话题信息
        /// </summary>
        public IList<TopicModel> Topics { get; }
        /// <summary>
        /// 激活状态
        /// </summary>
        public string ActivateState { get; set; }
        /// <summary>
        /// 专栏链接
        /// </summary>
        public string Href { get; set; }
        /// <summary>
        /// 是否接受提交文章
        /// </summary>
        public bool AcceptSubmission { get; set; }
        public bool FirstTime { get; set; }
        /// <summary>
        /// 文章话题信息
        /// </summary>
        public IList<PostTopicModel> PostTopics { get; }
        public string PendingName { get; set; }
        public IList<PostTopicModel> PendingTopics { get; }
        /// <summary>
        /// 头像
        /// </summary>
        public AvatarModel Avatar { get; set; }
        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool CanManage { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 距下一次修改名称的时间
        /// </summary>
        public int NameCanEditUntil { get; set; }
        /// <summary>
        /// 被封禁的原因
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// 被封禁的时间
        /// </summary>
        public int BanUntil { get; set; }
        /// <summary>
        /// 个性网址
        /// </summary>
        public string Slug { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 专栏介绍信息
        /// </summary>
        public string Intro { get; set; }
        /// <summary>
        /// 距下一次修改话题的时间
        /// </summary>
        public int TopicsCanEditUntil { get; set; }
        public string ActivateAuthorRequested { get; set; }
        /// <summary>
        /// 评论权限
        /// </summary>
        public string CommentPermission { get; set; }
        /// <summary>
        /// 是否关注改专栏
        /// </summary>
        public bool Following { get; set; }
        /// <summary>
        /// 专栏文章数
        /// </summary>
        public int PostsCount { get; set; }
        /// <summary>
        /// 当前帐号是否有发表文章的权限
        /// </summary>
        public bool CanPost { get; set; }
    }
}