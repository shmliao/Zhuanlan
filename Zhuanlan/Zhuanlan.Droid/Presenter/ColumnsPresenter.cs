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
        IColumnsView columnsView;
        public ColumnsPresenter(IColumnsView columnsView)
        {
            this.columnsView = columnsView;
        }
        public async Task GetColumns(List<ColumnModel> lists)
        {
            try
            {
                List<ColumnModel> columns = new List<ColumnModel>();
                foreach (var item in lists)
                {
                    columns.Add(JsonConvert.DeserializeObject<ColumnModel>(await OkHttpUtils.Instance.GetAsyn(ApiUtils.GetColumn(item.Slug))));
                }
                columnsView.GetColumnsSuccess(columns);
            }
            catch (Exception ex)
            {
                columnsView.GetColumnsFail(ex.Message);
            }
        }
    }
}