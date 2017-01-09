using System;
using System.Collections.Generic;
using System.Data.Linq;
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
    /// Interaction logic for DialogNewRecipient.xaml
    /// </summary>
    public partial class DialogNewRecipient : Window
    {
        public bool Result { get; private set; }
        private CollectionListView<RecipientView> collectionListView;
        private BillsBaseDataContext dateBase;
        private Tuple<string, object> street;
        private Tuple<string, object> city;
        private Tuple<string, object> postCode;
        private string name;
        private string account;
        private string buildingNr;

        public DialogNewRecipient(BillsBaseDataContext db)
        {
            InitializeComponent();
            dateBase = db;
            collectionListView = new CollectionListView<RecipientView>(db);
            loadDateToWindow();
            Result = false;
        }

        private void loadDateToWindow()
        {
            dialogRecipientCity.ItemsSource = dateBase.GetTable<City>();
            dialogRecipientStreet.ItemsSource = dateBase.GetTable<Street>();
            dialogRecipientPostCode.ItemsSource = dateBase.GetTable<PostCode>();
            loadListView();
        }

        private void dialogRecipientSave_Click(object sender, RoutedEventArgs e)
        {
            NewRecipient newRecipient = new NewRecipient(dateBase);
            downloadDateFromWindow();
            newRecipient.AddItem(name, account, street, buildingNr, postCode, city);
            refreshListView();
            Result = true;
        }

        private void downloadDateFromWindow()
        {
            name = dialogRecipientName.Text;
            account = dialogRecipientAccount.Text;
            street = new Tuple<string, object>(dialogRecipientStreet.Text, dialogRecipientStreet.SelectedValue);
            buildingNr = dialogRecipientBuildingNr.Text;
            city = new Tuple<string, object>(dialogRecipientCity.Text, dialogRecipientCity.SelectedValue);
            postCode = new Tuple<string, object>(dialogRecipientPostCode.Text, dialogRecipientPostCode.SelectedValue);
        }
        private void dialogRecipientCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void loadListView()
        {
            collectionListView.LoadCollection();
            listViewRecipient.ItemsSource = collectionListView.Collection;
        }

        private void refreshListView()
        {
            collectionListView.RefreshCollection();
        }
    }
}
