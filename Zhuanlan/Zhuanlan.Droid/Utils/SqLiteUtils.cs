using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhuanlan.Droid.Model;

namespace Zhuanlan.Droid.Utils
{
    public class SQLiteUtils
    {
        private static SQLiteConnection conn;
        public static SQLiteConnection Instance
        {
            get
            {
                if (conn == null)
                {
                    conn = new SQLiteConnection(new SQLitePlatformAndroid(), Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "zhuanlan.db"));
                    conn.CreateTable<InitColumnModel>();
                }
                return conn;
            }
        }
    }
}
