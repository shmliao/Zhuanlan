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
        /// <param name="column">专栏名称</param>
        /// <returns></returns>
        public static string GetColumn(string column)
        {
            return Host + "/columns/" + column;
        }
    }
}
