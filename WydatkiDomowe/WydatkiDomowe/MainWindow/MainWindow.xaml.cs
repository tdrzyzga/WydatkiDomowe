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
        private CollectionMainView collectionBills;
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
        private CheckBox checkBoxAll;

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
            DialogNewOrUpdateRecipient newRecipient = new DialogNewOrUpdateRecipient(dateBase);
            newRecipient.ShowDialog();

            if (newRecipient.Result)
            {
                collectionRecipient.RefreshCollection();
                refreshCheckBoxes();
                refreshListView();
            }

            newRecipient.Close();
        }

        private void newBillName_Click(object sender, RoutedEventArgs e)
        {
            DialogNewOrUpdateBillName newBillName = new DialogNewOrUpdateBillName(dateBase);
            newBillName.ShowDialog();

            if (newBillName.Result)
            {
                collectionBillName.RefreshCollection();
                refreshCheckBoxes();
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
            createCheckBoxes();
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
            collectionBills = new CollectionMainView(dateBase);
            collectionRecipient = new CollectionToView<Recipient>(dateBase);
            collectionBillName = new CollectionToView<BillName>(dateBase);
        }

        private void refreshCheckBoxes()
        {
            createCheckBoxes();
        }

        private void createCheckBoxes()
        {
            if (innerStack != null)
                mainCheckBoxGrid.Children.Clear();
            innerStack = new StackPanel { Orientation = Orientation.Horizontal };
            innerStack.HorizontalAlignment = HorizontalAlignment.Stretch;
            innerStack.VerticalAlignment = VerticalAlignment.Stretch;
            innerStack.Margin = new Thickness(2, 0, 0, 0);

            createCheckBoxAll();  

            foreach (var c in dateBase.BillNames)
            {
                CheckBox cb = new CheckBox();
                cb.HorizontalAlignment = HorizontalAlignment.Left;
                cb.VerticalAlignment = VerticalAlignment.Center;
                cb.Margin = new Thickness(5, 0, 0, 0);
                cb.IsChecked = true;
                cb.Checked += checkBox_Checked;
                cb.Unchecked += checkBox_Checked;
                cb.Content = c.Name;
                innerStack.Children.Add(cb);
            }

            mainCheckBoxGrid.Children.Add(innerStack);
        }

        private void createCheckBoxAll()
        {
            checkBoxAll = new CheckBox();
            checkBoxAll.HorizontalAlignment = HorizontalAlignment.Left;
            checkBoxAll.VerticalAlignment = VerticalAlignment.Center;
            checkBoxAll.Margin = new Thickness(5, 0, 0, 0);
            checkBoxAll.IsChecked = true;
            checkBoxAll.Checked += checkBoxAll_Checked;            
            checkBoxAll.Content = "Wszystkie";
            innerStack.Children.Add(checkBoxAll);
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
                setPaymentAndRequiredDate();
        }

        private void setPaymentAndRequiredDate()
        {
            BillName item = new BillName();
            item = dateBase.BillNames.Single(i => i.BillNameID == (int)mainBillName.SelectedValue);
            DateTime tempDate;

            if (existInBillTable(item))
            {
                var items = dateBase.Bills.Where(n => n.BillNameID == item.BillNameID).OrderBy(n => n.RequiredDate);
                tempDate = setDate(item, items.AsEnumerable().Last().RequiredDate);
                mainRequiredDate.SelectedDate = tempDate;
                mainPaymentDate.SelectedDate = tempDate;
            }
            else
            {
                tempDate = setDate(item);
                mainRequiredDate.SelectedDate = tempDate;
                mainPaymentDate.SelectedDate = tempDate;
            }
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

            if (listView.SelectedItems.Count == 1)
            {
                mainView = listView.SelectedItems[0] as MainView;

                mainBillName.Text = mainView.Bill;
                mainRecipient.Text = mainView.Recipient;
                mainAmount.Text = mainView.Amount.ToString("F");
                mainPaymentDate.SelectedDate = mainView.PaymentDate.Date;
                mainRequiredDate.SelectedDate = mainView.RequiredDate.Date;

                update = true;

                int id = dateBase.BillNames.Single(i => i.Name == mainView.Bill).BillNameID;
                updatedBillID = dateBase.Bills.Single(i => (i.BillNameID == id && i.PaymentDate == mainView.PaymentDate)).BillsID;
            }
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {            
            IEnumerable<CheckBox> selectedBoxes = this.innerStack.Children.OfType<CheckBox>()
                                                    .Where(cb => cb.IsChecked == true);

            setCheckBoxAll();
            
            IEnumerable<string> bills = selectedBoxes.Select(i => i.Content.ToString());
            collectionBills.Show(bills);
        }

        private void setCheckBoxAll()
        {
            IEnumerable<CheckBox> selectedBoxes = this.innerStack.Children.OfType<CheckBox>()
                                        .Select(cb => cb);
            if (selectedBoxes.Any(cb => cb.IsChecked == false))
                checkBoxAll.IsChecked = false;
            else
                checkBoxAll.IsChecked = true;
        }

        private void checkBoxAll_Checked(object sender, RoutedEventArgs e)
        {
            IEnumerable<CheckBox> checkBoxes = this.innerStack.Children.OfType<CheckBox>()
                                                    .Select(cb => cb);

            foreach (var i in checkBoxes)
                i.IsChecked = true;

            checkBoxAll.IsChecked = true;
            collectionBills.SetDefaultSortDescription();
        }

        private void mainDateRange_Click(object sender, RoutedEventArgs e)
        {
            collectionBills.Search((DateTime)mainDateStart.SelectedDate, (DateTime)mainDateEnd.SelectedDate);            
        }

        private void mainCancel_Click(object sender, RoutedEventArgs e)
        {
            clearView();
        }

        private void yearlyRaport_Click(object sender, RoutedEventArgs e)
        {
            YearlyRaportWindow newYearlyRaport = new YearlyRaportWindow(dateBase);
            newYearlyRaport.Show();
        }
        
        private void nameColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = e.Source as GridViewColumnHeader;

            string columnName = column.Tag.ToString();
            ListSortDirection sortDirection = new ListSortDirection();

            if (collectionBills.IsAscending())
                sortDirection = ListSortDirection.Descending;
            else
                sortDirection = ListSortDirection.Ascending;

            collectionBills.SetNewSortDescritpion(new SortDescription(columnName, sortDirection));
        }
    }
}
