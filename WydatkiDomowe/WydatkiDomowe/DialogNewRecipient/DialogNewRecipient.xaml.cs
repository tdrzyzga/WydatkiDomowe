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
using System.ComponentModel;

namespace WydatkiDomowe
{
    /// <summary>
    /// Interaction logic for DialogNewRecipient.xaml
    /// </summary>
    public partial class DialogNewRecipient : Window
    {
        public bool Result { get; private set; }

        private CollectionToView<RecipientView> collectionListView;
        private BillsBaseDataContext dateBase;
        private Tuple<string, object> street;
        private Tuple<string, object> city;
        private Tuple<string, object> postCode;
        private string name;
        private string account;
        private string buildingNr;
        private CorrectRecipient correctRecipient;

        public DialogNewRecipient(BillsBaseDataContext db)
        {
            correctRecipient = new CorrectRecipient(db);

            InitializeComponent();

            dateBase = db;
            collectionListView = new CollectionToView<RecipientView>(db);
            loadDateToWindow();

            Result = false;        
        }

        private void loadDateToWindow()
        {
            dialogRecipientCity.ItemsSource = dateBase.Cities;
            dialogRecipientStreet.ItemsSource = dateBase.Streets;
            dialogRecipientPostCode.ItemsSource = dateBase.PostCodes;
            dialogRecipientGrid.DataContext = correctRecipient;
            loadListView();
        }
        
        private void loadListView()
        {
            collectionListView.LoadCollection();
            listViewRecipient.ItemsSource = collectionListView.Collection;
        }

        private void dialogRecipientSave_Click(object sender, RoutedEventArgs e)
        {
            if (checkCorrectData())
            {
                NewRecipient newRecipient = new NewRecipient(dateBase);
                newRecipient.AddItem(name, account, street, buildingNr, postCode, city);
                refreshListView();
                Result = true;
            }
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

        private bool checkCorrectData()
        {
            trimDate();
            changeLetters();
            downloadDateFromWindow();

            correctRecipient.CheckData(name, account, street, buildingNr, postCode, city);
            return correctRecipient.Result;
        }

        private void changeLetters()
        {
            dialogRecipientName.Text = dialogRecipientName.Text.UppercaseFirst();
            dialogRecipientStreet.Text = dialogRecipientStreet.Text.UppercaseFirst();
            dialogRecipientPostCode.Text = dialogRecipientPostCode.Text.UppercaseFirst();
            dialogRecipientCity.Text = dialogRecipientCity.Text.UppercaseFirst();
        }
        
        private void trimDate()
        {
            dialogRecipientName.Text = dialogRecipientName.Text.Trim();
            dialogRecipientAccount.Text = dialogRecipientAccount.Text.Trim();
            dialogRecipientStreet.Text = dialogRecipientStreet.Text.Trim();
            dialogRecipientBuildingNr.Text = dialogRecipientBuildingNr.Text.Trim();
            dialogRecipientPostCode.Text = dialogRecipientPostCode.Text.Trim();
            dialogRecipientCity.Text = dialogRecipientCity.Text.Trim();
        }            

        private void dialogRecipientCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void refreshListView()
        {
            collectionListView.RefreshCollection();
        }
    }
}
