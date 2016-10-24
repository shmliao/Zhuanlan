using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhuanlan.Presenter
{
    public interface ISplashPresenter
    {
        Task GetInitColumns(Stream stream);
    }
}
