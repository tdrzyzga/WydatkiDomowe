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
        private CollectionToView<MainView> collectionBills;
        private CollectionToView<Recipient> collectionRecipient;
        private CollectionToView<BillName> collectionBillName;
        private int recipientID;
        private DateTime paymentDate;
        private DateTime requiredDate;
        private decimal amount;
        private int billNameID;
        private CorrectBill correctBill;
        private bool update;
        private int updatedBillID;
        private StackPanel innerStack;

        public MainWindow()
        {
            dateBase = new BillsBaseDataContext();
            correctBill = new CorrectBill();
            InitializeComponent();

            loadCollection(dateBase);
            loadDateToWindow();
            update = false;
        }
        
        private void newRecipient_Click(object sender, RoutedEventArgs e)
        {
            DialogNewRecipient newRecipient = new DialogNewRecipient(dateBase);
            newRecipient.ShowDialog();

            if (newRecipient.Result)
            {
                collectionRecipient.RefreshCollection();
                refreshListView();
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
                refreshListView();
            }

            newBillName.Close();
        }

        private void mainSave_Click(object sender, RoutedEventArgs e)
        {
            if (checkCorrectData())
            {
                NewOrUpdateBill newOrUpdateBill = new NewOrUpdateBill(dateBase);
                downloadDateFromWindow();
                if (update)
                {
                    newOrUpdateBill.UpdateItem(updatedBillID, recipientID, billNameID, amount, paymentDate, requiredDate);
                    update = false;
                }
                else
                    newOrUpdateBill.AddItem(recipientID, billNameID, amount, paymentDate, requiredDate);
                
                refreshView();
            }
        }

        private bool checkCorrectData()
        {
            correctBill.CheckData(mainBillName, mainRecipient, mainAmount.Text);
            return correctBill.Result;
        }

        private void loadDateToWindow()
        {
            loadComboboxes();
            loadCheckBoxes();
            loadListView();
        }

        private void loadListView()
        {
            collectionBills.LoadCollection();
            listViewBills.ItemsSource = collectionBills.Collection;
        }

        private void loadComboboxes()
        {
            mainBillsGrid.DataContext = correctBill;
            collectionRecipient.LoadCollection();
            mainRecipient.ItemsSource = collectionRecipient.Collection;
            collectionBillName.LoadCollection();
            mainBillName.ItemsSource = collectionBillName.Collection;
        }

        private void loadCollection(BillsBaseDataContext dateBase)
        {
            collectionBills = new CollectionToView<MainView>(dateBase);
            collectionRecipient = new CollectionToView<Recipient>(dateBase);
            collectionBillName = new CollectionToView<BillName>(dateBase);
        }

        private void loadCheckBoxes()
        {
            innerStack = new StackPanel { Orientation = Orientation.Horizontal };
            innerStack.HorizontalAlignment = HorizontalAlignment.Stretch;
            innerStack.VerticalAlignment = VerticalAlignment.Stretch;

            foreach (var c in dateBase.BillNames)
            {
                CheckBox cb = new CheckBox();
                cb.HorizontalAlignment = HorizontalAlignment.Left;
                cb.VerticalAlignment = VerticalAlignment.Center;
                cb.Margin = new Thickness(5, 0, 0, 0);

                cb.Content = c.Name;
                innerStack.Children.Add(cb);
            }

            mainCheckBoxGrid.Children.Add(innerStack);
        }

        private void downloadDateFromWindow()
        {
            recipientID = (int)mainRecipient.SelectedValue;
            billNameID = (int)mainBillName.SelectedValue;
            amount = decimal.Parse(mainAmount.Text);
            paymentDate = (DateTime) mainPaymentDate.SelectedDate;
            requiredDate = (DateTime)mainRequiredDate.SelectedDate;
        }
        
        private void refreshView()
        {
            clearView();
            refreshListView();
        }

        private void refreshListView()
        {
            collectionBills.RefreshCollection();
        }

        private void clearView()
        {
            mainAmount.Text = string.Empty;
            mainBillName.Text = string.Empty;
            mainRecipient.Text = string.Empty;
            mainPaymentDate.SelectedDate = DateTime.Now;
            mainRequiredDate.SelectedDate = null;
        }

        private void windows_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private void mainBillName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mainBillName.IsDropDownOpen)
                setRequiredDate();
        }

        private void setRequiredDate()
        {
            BillName item = new BillName();
            item = dateBase.BillNames.Single(i => i.BillNameID == (int)mainBillName.SelectedValue);

            if (existInBillTable(item))
            {
                var items = dateBase.Bills.Where(n => n.BillNameID == item.BillNameID).OrderBy(n => n.RequiredDate);
                mainRequiredDate.SelectedDate = setDate(item, items.AsEnumerable().Last().RequiredDate);
            }
            else
                mainRequiredDate.SelectedDate = setDate(item);
        }

        private bool existInBillTable(BillName item)
        {
            return dateBase.Bills.Any(i => i.BillNameID == item.BillNameID);
        }

        private DateTime setDate(BillName item)
        {
            return item.FirstPaymentDate;
        }

        private DateTime setDate(BillName item, DateTime lastDate)
        {
            return lastDate.AddDays(item.PaymentsFrequency);
        }

        private void listViewBills_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainView mainView = new MainView();
            ListView listView = sender as ListView;

            mainView = listView.SelectedItems[0] as MainView;

            mainBillName.Text = mainView.Bill;
            mainRecipient.Text = mainView.Recipient;
            mainAmount.Text = mainView.Amount.ToString();
            mainPaymentDate.SelectedDate = mainView.PaymentDate.Date;
            mainRequiredDate.SelectedDate = mainView.RequiredDate.Date;

            update = true;

            int id = dateBase.BillNames.Single(i => i.Name == mainView.Bill).BillNameID;
            updatedBillID = dateBase.Bills.Single(i => (i.BillNameID == id && i.PaymentDate == mainView.PaymentDate)).BillsID;
        }
    }
}
