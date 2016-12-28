using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WydatkiDomowe
{
    class StreetTable
    {
        public Table<Street> StreetTab { get; set; }

        public StreetTable(BillsBaseDataContext homeBase)
        {
            StreetTab = homeBase.GetTable<Street>();
        }
    }
}
