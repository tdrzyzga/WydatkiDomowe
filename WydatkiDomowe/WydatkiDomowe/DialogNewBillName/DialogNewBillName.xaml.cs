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
        public bool Result { get; private set; }
        private BillsBaseDataContext homeBase;
        private CollectionListView<BillName> collectionListView;

        public DialogNewBillName(BillsBaseDataContext db)
        {
            InitializeComponent();
            homeBase = db;
            collectionListView = new CollectionListView<BillName>(db);
            loadListView();
            Result = false;
        }

        private void loadListView()
        {
            collectionListView.LoadCollection();
            listViewBillName.ItemsSource = collectionListView.Collection;
        }

        private void refreshListView()
        {
            collectionListView.RefreshCollection();
        }

        private void dialogRecipientCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void dialogBillNameSave_Click(object sender, RoutedEventArgs e)
        {
            BillName newBillName = new BillName();
            newBillName.Name = dialogBillName.Text;
            newBillName.RequiredDate = (DateTime) dialogBillNameDate.SelectedDate;
            homeBase.BillNames.InsertOnSubmit(newBillName);
            homeBase.SubmitChanges();
            refreshListView();
            Result = true;
        }
    }
}
