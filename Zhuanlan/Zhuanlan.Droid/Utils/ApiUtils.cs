using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhuanlan.Droid.Utils
{
    public class ApiUtils
    {
        public const string Host = "https://zhuanlan.zhihu.com/api";
        /// <summary>
        /// 获取专栏信息
        /// </summary>
        /// <param name="slug">专栏名称</param>
        /// <returns></returns>
        public static string GetColumn(string slug)
        {
            return Host + "/columns/" + slug;
        }
        /// <summary>
        /// 获取专栏文章信息
        /// </summary>
        /// <param name="slug">专栏名称</param>
        /// <param name="limit">数量限制</param>
        /// <param name="offset">偏移量</param>
        /// <returns></returns>
        public static string GetPosts(string slug, int limit, int offset)
        {
            return Host + "/columns/" + slug + "/posts?limit=" + limit + "&offset=" + offset;
        }
        /// <summary>
        /// 获取文章信息
        /// </summary>
        /// <param name="slug">文章名称</param>
        /// <returns></returns>
        public static string GetPost(string slug)
        {
            return Host + "/posts/" + slug;
        }
        /// <summary>
        /// 获取文章所属专栏
        /// </summary>
        /// <param name="slug">文章名称</param>
        /// <returns></returns>
        public static string GetContributed(string slug)
        {
            return Host + "/posts/" + slug + "/contributed";
        }
        /// <summary>
        /// 获取推荐专栏信息
        /// </summary>
        /// <param name="limit">数量限制</param>
        /// <param name="offset">偏移量</param>
        /// <returns></returns>
        public static string GetRecommendationColumns(int limit, int offset)
        {
            return Host + "/recommendations/columns?limit=" + limit + "&offset=" + offset;
        }
        /// <summary>
        /// 获取推荐文章信息
        /// </summary>
        /// <param name="limit">数量限制</param>
        /// <param name="offset">偏移量</param>
        /// <returns></returns>
        public static string GetRecommendationPosts(int limit, int offset)
        {
            return Host + "/recommendations/posts?limit=" + limit + "&offset=" + offset;
        }
    }
}
