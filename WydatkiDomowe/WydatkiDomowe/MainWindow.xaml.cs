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
        public MainWindow()
        {
            InitializeComponent();
            dateBase = new BillsBaseDataContext();
            mainBillName.ItemsSource = dateBase.GetTable<BillName>();
            mainRecipient.ItemsSource = dateBase.GetTable<Recipient>();
        }

        private void NewRecipient_Click(object sender, RoutedEventArgs e)
        {
            DialogNewRecipient newRecipient = new DialogNewRecipient(dateBase);
            newRecipient.ShowDialog();
            newRecipient.Close();
        }

        private void NewBillName_Click(object sender, RoutedEventArgs e)
        {
            DialogNewBillName newBillName = new DialogNewBillName(dateBase);
            newBillName.ShowDialog();
            newBillName.Close();
        }

    }
}
