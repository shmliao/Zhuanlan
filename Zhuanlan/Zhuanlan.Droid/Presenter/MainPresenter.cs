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
using Newtonsoft.Json;
using Zhuanlan.Droid.Model;
using System.IO;
using Zhuanlan.Droid.UI.Views;

namespace Zhuanlan.Droid.Presenter
{
    public class MainPresenter : IMainPresenter
    {
        private IMainView mainView;

        public MainPresenter(IMainView mainView)
        {
            this.mainView = mainView;
        }
        public void SwitchNavigationBar(int id)
        {
            switch (id)
            {
                case Resource.Id.bb_menu_home:
                    mainView.SwitchHome();
                    break;
                case Resource.Id.bb_menu_article:
                    mainView.SwitchArticle();
                    break;
                case Resource.Id.bb_menu_column:
                    mainView.SwitchColumn();
                    break;
                case Resource.Id.bb_menu_like:
                    mainView.SwitchLike();
                    break;
                default:
                    break;
            }
        }
        public void HideNavigationBar(int id)
        {
            switch (id)
            {
                case Resource.Id.bb_menu_home:
                    mainView.HideHome();
                    break;
                case Resource.Id.bb_menu_article:
                    mainView.HideArticle();
                    break;
                case Resource.Id.bb_menu_column:
                    mainView.HideColumn();
                    break;
                case Resource.Id.bb_menu_like:
                    mainView.HideLike();
                    break;
                default:
                    break;
            }
        }

        public async Task StartVersion()
        {

        }
    }
}