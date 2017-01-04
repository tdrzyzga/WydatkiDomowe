using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace WydatkiDomowe
{
    class CollectionListView<T> where T: class
    {
        public ObservableCollection<T> Collection{get; private set;}
        private BillsBaseDataContext dateBase;

        public CollectionListView(BillsBaseDataContext db)
        {
            Collection = new ObservableCollection<T>();
            dateBase = db;
        }

        public void LoadCollection()
        {
            foreach (var i in dateBase.GetTable<T>())
                Collection.Add(i);
        }

        public void RefreshCollection()
        {
            Collection.Clear();
            foreach (var i in dateBase.GetTable<T>())
                Collection.Add(i);
        }
    }
}
