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
            LoadDateToWindow();
        }

        private void LoadDateToWindow()
        {
            dialogRecipientCity.ItemsSource = dateBase.GetTable<City>();
            dialogRecipientStreet.ItemsSource = dateBase.GetTable<Street>();
            dialogRecipientPostCode.ItemsSource = dateBase.GetTable<PostCode>();
            LoadListView();
        }

        private void DialogRecipientSave_Click(object sender, RoutedEventArgs e)
        {
            NewRecipient newRecipient = new NewRecipient(dateBase);
            DownloadDateFromWindow();
            newRecipient.AddItem(name, account, street, buildingNr, postCode, city);
            RefreshListView();
        }

        private void DownloadDateFromWindow()
        {
            name = dialogRecipientName.Text;
            account = dialogRecipientAccount.Text;
            street = new Tuple<string, object>(dialogRecipientStreet.Text, dialogRecipientStreet.SelectedValue);
            buildingNr = dialogRecipientBuildingNr.Text;
            city = new Tuple<string, object>(dialogRecipientCity.Text, dialogRecipientCity.SelectedValue);
            postCode = new Tuple<string, object>(dialogRecipientPostCode.Text, dialogRecipientPostCode.SelectedValue);
        }
        private void DialogRecipientCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void LoadListView()
        {
            collectionListView.LoadCollection();
            listViewRecipient.ItemsSource = collectionListView.Collection;
        }

        private void RefreshListView()
        {
            collectionListView.RefreshCollection();
        }
    }
}
