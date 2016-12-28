using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WydatkiDomowe
{
    class CityTable
    {
        public Table<City> CityTab { get; set; }

        public CityTable(BillsBaseDataContext homeBase)
        {
            CityTab = homeBase.GetTable<City>();
        }
    }
}
