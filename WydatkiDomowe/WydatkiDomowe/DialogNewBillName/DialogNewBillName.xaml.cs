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
using System.Data.SqlClient;

namespace WydatkiDomowe
{
    /// <summary>
    /// Interaction logic for DialogNewBillName.xaml
    /// </summary>
    public partial class DialogNewBillName : Window
    {
        public bool Result { get; private set; }
        private BillsBaseDataContext homeBase;
        private CollectionToView<BillName> collectionListView;
        private string name;
        private DateTime firstPaymentDate;
        private string paymentsFrequency;
        private CorrectBillName correctBillName;

        public DialogNewBillName(BillsBaseDataContext db)
        {
            correctBillName = new CorrectBillName(db);

            InitializeComponent();

            homeBase = db;
            collectionListView = new CollectionToView<BillName>(db);

            loadDateToWindow();
            Result = false;
        }

        private void loadDateToWindow()
        {
            dialogBillNameGrid.DataContext = correctBillName;
            loadListView();
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
            if (checkCorrectData())
            {
                BillName newBillName = new BillName();
                newBillName.Name = name;
                newBillName.FirstPaymentDate = firstPaymentDate;
                newBillName.PaymentsFrequency = Int32.Parse(paymentsFrequency);

                homeBase.BillNames.InsertOnSubmit(newBillName);
                homeBase.SubmitChanges();

                refreshListView();
                Result = true;
            }
            
        }

        private bool checkCorrectData()
        {
            downloadDateFromWindow();
            correctBillName.CheckData(name, paymentsFrequency);
            return correctBillName.Result;
        }

        private void downloadDateFromWindow()
        {
            name = dialogBillName.Text.Trim();
            firstPaymentDate = (DateTime)dialogBillFirstPaymentDate.SelectedDate;
            paymentsFrequency = dialogPaymentsFrequency.Text.Trim();
        }
    }
}
