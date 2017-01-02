using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace WydatkiDomowe
{
    /// <summary>
    /// Interaction logic for DialogNewBillName.xaml
    /// </summary>
    public partial class DialogNewBillName : Window
    {
        private BillsBaseDataContext homeBase;
        private ObservableCollection<BillName> collectionBillName;

        public DialogNewBillName(BillsBaseDataContext db)
        {
            InitializeComponent();
            homeBase = db;
            LoadListView();
        }

        private void LoadListView()
        {
            collectionBillName = new ObservableCollection<BillName>();
            foreach (var i in homeBase.GetTable<BillName>())
                collectionBillName.Add(i);
            listViewBillName.ItemsSource = collectionBillName;
        }

        private void RefreshListView()
        {
            collectionBillName.Clear();
            foreach (var i in homeBase.GetTable<BillName>())
                collectionBillName.Add(i);
        }

        private void DialogRecipientCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void DialogBillNameSave_Click(object sender, RoutedEventArgs e)
        {
            BillName newBillName = new BillName();
            newBillName.Name = dialogBillName.Text;
            newBillName.RequiredDate = (DateTime) dialogBillNameDate.SelectedDate;
            homeBase.BillNames.InsertOnSubmit(newBillName);
            homeBase.SubmitChanges();
            RefreshListView();
        }
    }
}
