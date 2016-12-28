using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WydatkiDomowe
{
    class PostCodeTable
    {
        public Table<PostCode> PostCodeTab { get; set; }

        public PostCodeTable(BillsBaseDataContext homeBase)
        {
            PostCodeTab = homeBase.GetTable<PostCode>();
        }
    }
}
