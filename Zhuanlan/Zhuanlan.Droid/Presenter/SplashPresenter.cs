using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public async Task GetInitColumns()
        {
            try
            {
                var columns = JsonConvert.DeserializeObject<Columns>(await OkHttpUtils.Instance.GetAsyn(ApiUtils.Init));
                if (columns.data.Count > 0)
                {
                    splashView.GetInitColumnsSuccess(columns.data);
                }
            }
            catch (Exception ex)
            {
                splashView.GetInitColumnsFail(ex.Message);
            }
        }
        public class Columns
        {
            public List<InitColumnModel> data { get; set; }
        }
    }
}
