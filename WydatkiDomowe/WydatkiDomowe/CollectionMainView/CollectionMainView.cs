﻿using System;
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
        private DateTime dateStart;
        private DateTime dateEnd;

        public CollectionMainView(BillsBaseDataContext db)
        {
            dateBase = db;

            collection = new ObservableCollection<MainView>();
            Collection = new CollectionView(collection);
            Collection = (CollectionView)CollectionViewSource.GetDefaultView(collection);
            Collection.SortDescriptions.Add(new SortDescription("PaymentDate", ListSortDirection.Descending ));
            Collection.SortDescriptions.Add(new SortDescription("Bill", ListSortDirection.Ascending));
            Collection.Filter = filterByDateRange;
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

        public void Search(DateTime start, DateTime end)
        {
            dateStart = start;
            dateEnd = end;
            Collection.Refresh();
        }

        public void SetDefaultSortDescription()
        {
            Collection.SortDescriptions.Clear();
            Collection.SortDescriptions.Add(new SortDescription("PaymentDate", ListSortDirection.Descending));
            Collection.SortDescriptions.Add(new SortDescription("Bill", ListSortDirection.Ascending));
            Collection.Refresh();
        }
        
        public void SetNewSortDescritpion(SortDescription sortDescription)
        {
            Collection.SortDescriptions.Clear();
            Collection.SortDescriptions.Add(sortDescription);
            Collection.Refresh();
        }
        
        public bool IsAscending()
        {
            return Collection.SortDescriptions.Any(i => i.Direction == ListSortDirection.Ascending);
        }

        private bool filterByDateRange(object item)
        {
            MainView bill = item as MainView;

            if (dateStart == DateTime.MinValue || dateEnd == DateTime.MinValue)
                return true;
            else
                return isInDateRange(bill.PaymentDate);
        }

        private bool isInDateRange(DateTime date)
        {
            if (date >= dateStart && date <= dateEnd)
                return true;
            else
                return false;
        }
    }    
}
