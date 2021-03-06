using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Zhuanlan.Droid.Model;
using Zhuanlan.Droid.UI.Views;
using Newtonsoft.Json;
using Zhuanlan.Droid.Utils;

namespace Zhuanlan.Droid.Presenter
{
    public class ColumnsPresenter : IColumnsPresenter
    {
        private int limit = 10;
        private IColumnsView columnsView;
        public ColumnsPresenter(IColumnsView columnsView)
        {
            this.columnsView = columnsView;
        }
        public async Task GetColumns(int offset)
        {
            try
            {
                var columns = JsonConvert.DeserializeObject<List<ColumnModel>>(await OkHttpUtils.Instance.GetAsyn(ApiUtils.GetRecommendationColumns(limit, offset)));
                columnsView.GetColumnsSuccess(columns);
            }
            catch (Exception ex)
            {
                columnsView.GetColumnsFail(ex.Message);
            }
        }
    }
}