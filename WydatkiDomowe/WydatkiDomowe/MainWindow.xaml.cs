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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace WydatkiDomowe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BillsBaseDataContext dateBase { get; set; }
        private CollectionToView<MainView> collectionListView;
        private CollectionToView<Recipient> collectionRecipient;
        private CollectionToView<BillName> collectionBillName;
        private int recipientID;
        private DateTime paymentDate;
        private decimal amount;
        private int billNameID;

        public MainWindow()
        {
            dateBase = new BillsBaseDataContext();
            InitializeComponent();

            collectionListView = new CollectionToView<MainView>(dateBase);
            collectionRecipient = new CollectionToView<Recipient>(dateBase);
            collectionBillName = new CollectionToView<BillName>(dateBase);
            loadDateToWindow();
        }

        private void loadDateToWindow()
        {
            loadComboboxes();
            loadListView();
        }

        private void loadListView()
        {
            collectionListView.LoadCollection();
            listViewBills.ItemsSource = collectionListView.Collection;
        }

        private void loadComboboxes()
        {
            collectionRecipient.LoadCollection();
            mainRecipient.ItemsSource = collectionRecipient.Collection;
            collectionBillName.LoadCollection();
            mainBillName.ItemsSource = collectionBillName.Collection;
        }

        private void newRecipient_Click(object sender, RoutedEventArgs e)
        {
            DialogNewRecipient newRecipient = new DialogNewRecipient(dateBase);
            newRecipient.ShowDialog();
            if (newRecipient.Result)
            {
                collectionRecipient.RefreshCollection();
            }
            newRecipient.Close();
        }

        private void newBillName_Click(object sender, RoutedEventArgs e)
        {
            DialogNewBillName newBillName = new DialogNewBillName(dateBase);
            newBillName.ShowDialog();
            if (newBillName.Result)
            {
                collectionBillName.RefreshCollection();
            }
            newBillName.Close();
        }

        private void mainSave_Click(object sender, RoutedEventArgs e)
        {
            NewBill newBill = new NewBill(dateBase);
            downloadDateFromWindow();
            newBill.AddItem(recipientID, billNameID, amount, paymentDate);
            refreshListView();
        }

        private void downloadDateFromWindow()
        {
            recipientID = (int)mainRecipient.SelectedValue;
            billNameID = (int)mainBillName.SelectedValue;
            amount = decimal.Parse(mainAmount.Text);
            paymentDate = (DateTime) mainPaymentDate.SelectedDate;
        }

        private void refreshListView()
        {
            collectionListView.RefreshCollection();
        }

        private void windows_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
