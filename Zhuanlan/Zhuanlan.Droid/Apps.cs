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
using Zhuanlan.Droid.Utils;

namespace Zhuanlan.Droid
{
    [Application]
    public class Apps : Application
    {
        public Apps() { }
        public Apps(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }
        public override void OnCreate()
        {
            base.OnCreate();
            // 配置全局异常捕获
            //if (!BuildConfig.Debug)
            {
                AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironment_UnhandledExceptionRaiser;
            }
        }
        protected override void Dispose(bool disposing)
        {
            AndroidEnvironment.UnhandledExceptionRaiser -= AndroidEnvironment_UnhandledExceptionRaiser;
            base.Dispose(disposing);
        }

        async void AndroidEnvironment_UnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs ex)
        {
            await Task.Run(() =>
            {
                new Handler().Post(() =>
                {
                    Toast.MakeText(this, "很抱歉，程序出现异常，正在从异常中恢复", ToastLength.Long).Show();
                });
            });

            //保存错误日志
            try
            {
                await LogUtils.SaveAsyn(this, ex.Exception);
                //让程序继续运行2秒，保证错误日志的保存
                System.Threading.Thread.Sleep(2000);
            }
            catch (Java.Lang.Exception e)
            {
                e.PrintStackTrace();
            }
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            Java.Lang.JavaSystem.Exit(1);
        }
    }
}