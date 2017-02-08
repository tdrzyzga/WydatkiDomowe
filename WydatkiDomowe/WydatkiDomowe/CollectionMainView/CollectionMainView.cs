using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Controls;
using System.ComponentModel;

namespace WydatkiDomowe
{
    class CollectionMainView
    {
        public CollectionView Collection { get; private set; }

        private ObservableCollection<MainView> collection;
        private BillsBaseDataContext dateBase;

        public CollectionMainView(BillsBaseDataContext db)
        {
            dateBase = db;

            collection = new ObservableCollection<MainView>();
            Collection = new CollectionView(collection);
            Collection = (CollectionView)CollectionViewSource.GetDefaultView(collection);
            Collection.SortDescriptions.Add(new SortDescription("PaymentDate", ListSortDirection.Descending ));
            Collection.SortDescriptions.Add(new SortDescription("Bill", ListSortDirection.Ascending));
        }

        public void LoadCollection()
        {
            foreach (var i in dateBase.MainViews)
                collection.Add(i);
            Collection.Refresh();
        }

        public void RefreshCollection()
        {
            collection.Clear();
            foreach (var i in dateBase.MainViews)
                collection.Add(i);
            Collection.Refresh();
        }

        public void Show(IEnumerable<string> bills)
        {
            collection.Clear();
            List<MainView> tempList = new List<MainView>();
            foreach (string bill in bills)
                tempList.AddRange(dateBase.MainViews.Where(i => i.Bill == bill).Select(i => i));
            foreach (var i in tempList)
                collection.Add(i);
            Collection.Refresh();
        }
    }    
}
