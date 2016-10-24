using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhuanlan.Droid.Model;
using Zhuanlan.Droid.UI.Views;
using Zhuanlan.Droid.Utils;

namespace Zhuanlan.Presenter
{
    public class SplashPresenter : ISplashPresenter
    {
        ISplashView splashView;
        public SplashPresenter(ISplashView splashView)
        {
            this.splashView = splashView;
        }
        public async Task GetInitColumns(Stream stream)
        {
            await Task.Run(() =>
            {
                try
                {
                    StringBuilder zhuanlan = new StringBuilder();
                    using (StreamReader sr = new StreamReader(stream, Encoding.Default, true))
                    {
                        while (!sr.EndOfStream)
                        {
                            zhuanlan.Append(sr.ReadLine());
                        }
                    }
                    if (zhuanlan.Length > 0)
                    {
                        var columns = JsonConvert.DeserializeObject<Columns>(zhuanlan.ToString());
                        if (columns.data.Count > 0)
                        {
                            splashView.GetInitColumnsSuccess(columns.data);
                        }
                    }
                    else
                    {
                        splashView.GetInitColumnsFail("初始化数据失败");
                    }
                }
                catch (Exception ex)
                {
                    splashView.GetInitColumnsFail(ex.Message);
                }
            });
        }
        public class Columns
        {
            public List<ColumnModel> data { get; set; }
        }
    }
}
