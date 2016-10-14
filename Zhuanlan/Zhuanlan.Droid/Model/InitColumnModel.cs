using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhuanlan.Droid.Model
{
    public class InitColumnModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Column { get; set; }
    }
}
