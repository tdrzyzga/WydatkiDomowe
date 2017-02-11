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
    public partial class DialogNewOrUpdateBillName : Window
    {
        public bool Result { get; private set; }
        private BillsBaseDataContext dateBase;
        private CollectionToView<BillName> collectionListView;        
        private string name;
        private DateTime firstPaymentDate;
        private string paymentsFrequency;
        private CorrectBillName correctBillName;
        private bool update;
        private int updatedBillNameID;

        public DialogNewOrUpdateBillName(BillsBaseDataContext db)
        {
            correctBillName = new CorrectBillName(db);

            InitializeComponent();

            dateBase = db;
            collectionListView = new CollectionToView<BillName>(db);

            loadDateToWindow();
            update = false;
            Result = false;
        }

        private void dialogBillNameSave_Click(object sender, RoutedEventArgs e)
        {
            if (checkCorrectData())
            {
                if (update)
                {
                    updateBillNameItem();
                    update = false;
                }
                else
                    addBillNameItem();

                refreshView();
                Result = true;
            }
        }

        private void updateBillNameItem()
        {
            BillName updateBillName = dateBase.BillNames.Single(i => i.BillNameID == updatedBillNameID);
            updateBillName.Name = name;
            updateBillName.FirstPaymentDate = firstPaymentDate;
            updateBillName.PaymentsFrequency = Int32.Parse(paymentsFrequency);

            dateBase.SubmitChanges();
        }

        private void addBillNameItem()
        {
            BillName newBillName = new BillName();
            newBillName.Name = name;
            newBillName.FirstPaymentDate = firstPaymentDate;
            newBillName.PaymentsFrequency = Int32.Parse(paymentsFrequency);

            dateBase.BillNames.InsertOnSubmit(newBillName);
            dateBase.SubmitChanges();
        }

        private void dialogRecipientCancel_Click(object sender, RoutedEventArgs e)
        {
            clearView();
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
        
        private void refreshView()
        {
            clearView();
            refreshListView();
        }
        
        private void refreshListView()
        {
            collectionListView.RefreshCollection();
        }

        private void clearView()
        {
            dialogBillName.Text = string.Empty;
            dialogBillNamePaymentsFrequency.Text = string.Empty;
            dialogBillNameFirstPaymentDate.SelectedDate = DateTime.Now;
        }

        private bool checkCorrectData()
        {
            trimText();
            changeHeightLetters();
            downloadDateFromWindow();
            correctBillName.CheckData(update, name, paymentsFrequency);
            return correctBillName.Result;
        }

        private void downloadDateFromWindow()
        {
            name = dialogBillName.Text;
            firstPaymentDate = (DateTime)dialogBillNameFirstPaymentDate.SelectedDate;
            paymentsFrequency = dialogBillNamePaymentsFrequency.Text;
        }

        private void changeHeightLetters()
        {
            dialogBillName.Text = dialogBillName.Text.UppercaseFirstInWords();
            dialogBillNamePaymentsFrequency.Text = dialogBillNamePaymentsFrequency.Text.UppercaseFirstInWords();
        }

        private void trimText()
        {
            dialogBillName.Text = dialogBillName.Text.Trim();
            dialogBillNamePaymentsFrequency.Text = dialogBillNamePaymentsFrequency.Text.Trim();
        }

        private void listViewBillName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BillName billName = new BillName();
            ListView listView = sender as ListView;

            if (listView.SelectedItems.Count == 1)
            {
                billName = listView.SelectedItems[0] as BillName;

                dialogBillName.Text = billName.Name;
                dialogBillNamePaymentsFrequency.Text = billName.PaymentsFrequency.ToString();
                dialogBillNameFirstPaymentDate.SelectedDate = billName.FirstPaymentDate.Date;

                update = true;
                updatedBillNameID = dateBase.BillNames.Single(i => i.Name == billName.Name).BillNameID;
            }
        }

    }
}
