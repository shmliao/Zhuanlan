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
using System.Threading.Tasks;
using System.IO;

namespace Zhuanlan.Droid.Utils
{
    public class LogUtils
    {

        public static async Task SaveAsyn(Context context, Exception ex)
        {
            await Task.Run(() =>
            {
                var PackageInfo = context.PackageManager.GetPackageInfo(context.PackageName, 0);
                var path = Android.OS.Environment.ExternalStorageDirectory.Path + "/" + PackageInfo.PackageName + "/log";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string dbPath = System.IO.Path.Combine(path, "log.txt");
                if (!System.IO.File.Exists(dbPath))
                {
                    System.IO.File.Create(dbPath);
                }
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("-----" + DateTime.Now.ToString() + "-----\n");
                sb.Append("生产厂商：\n");
                sb.Append(Build.Manufacturer).Append("\n\n");
                sb.Append("手机型号：\n");
                sb.Append(Build.Model).Append("\n\n");
                sb.Append("系统版本：\n");
                sb.Append(Build.VERSION.Release).Append("\n\n");
                sb.Append("异常时间：\n");
                sb.Append(DateTime.Now.ToString()).Append("\n\n");
                sb.Append("异常信息：\n");
                sb.Append(ex).Append("\n");
                sb.Append(ex.Message).Append("\n\n");
                System.IO.File.AppendAllText(dbPath, sb.ToString());
            });
        }
    }
}