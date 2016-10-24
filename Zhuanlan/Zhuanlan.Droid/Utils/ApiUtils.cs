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
        public const string Init = "http://zhouqiaoqiao.com/zhuanlan.json";

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
    }
}
