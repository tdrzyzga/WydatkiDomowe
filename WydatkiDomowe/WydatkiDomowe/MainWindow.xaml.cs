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

namespace WydatkiDomowe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BillsBaseDataContext dateBase { get; set; }
        private CollectionListView<MainView> collectionListView;
        private int recipientID;
        private DateTime paymentDate;
        private decimal amount;
        private int billNameID;

        public MainWindow()
        {
            InitializeComponent();
            dateBase = new BillsBaseDataContext();
            collectionListView = new CollectionListView<MainView>(dateBase);
            loadDateToWindow();
        }

        private void loadDateToWindow()
        {
            mainBillName.ItemsSource = dateBase.GetTable<BillName>();
            mainRecipient.ItemsSource = dateBase.GetTable<Recipient>();
            loadListView();
        }

        private void loadListView()
        {
            collectionListView.LoadCollection();
            listViewBills.ItemsSource = collectionListView.Collection;
        }

        private void newRecipient_Click(object sender, RoutedEventArgs e)
        {
            DialogNewRecipient newRecipient = new DialogNewRecipient(dateBase);
            newRecipient.ShowDialog();
            if (newRecipient.Result)
                mainRecipient.ItemsSource = dateBase.GetTable<Recipient>();
            newRecipient.Close();
        }

        private void newBillName_Click(object sender, RoutedEventArgs e)
        {
            DialogNewBillName newBillName = new DialogNewBillName(dateBase);
            newBillName.ShowDialog();
            if (newBillName.Result)
            {
                mainBillName.ItemsSource = dateBase.GetTable<BillName>();
                mainBillName.Items.Refresh();
                
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
    }
}
