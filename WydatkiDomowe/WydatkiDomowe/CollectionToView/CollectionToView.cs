using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Collections;
using System.Windows.Data;
using System.ComponentModel;

namespace WydatkiDomowe
{
    public class CollectionToView<T> where T :  class
    {
        public CollectionView Collection { get; private set; }

        private ObservableCollection<T> collection;
        private BillsBaseDataContext dateBase;

        public CollectionToView(BillsBaseDataContext db)
        {            
            dateBase = db;

            collection = new ObservableCollection<T>();
            Collection = new CollectionView(collection);
            Collection = (CollectionView)CollectionViewSource.GetDefaultView(collection);
            //Collection.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        }

        public void LoadCollection()
        {
            foreach (var i in dateBase.GetTable<T>())
                collection.Add(i);
            Collection.Refresh();
        }

        public void RefreshCollection()
        {
            collection.Clear();
            foreach (var i in dateBase.GetTable<T>())
                collection.Add(i);
            Collection.Refresh();
        }
    }
}
