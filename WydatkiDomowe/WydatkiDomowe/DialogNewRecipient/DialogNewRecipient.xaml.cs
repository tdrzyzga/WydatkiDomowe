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

namespace WydatkiDomowe
{
    /// <summary>
    /// Interaction logic for DialogNewRecipient.xaml
    /// </summary>
    public partial class DialogNewRecipient : Window
    {
        private BillsBaseDataContext homeBase;
        
        public DialogNewRecipient(BillsBaseDataContext db)
        {
            InitializeComponent();
            homeBase = db;
            LoadDate();
        }

        private void LoadDate()
        {
            dialogRecipientCity.ItemsSource = homeBase.GetTable<City>();
            dialogRecipientStreet.ItemsSource = homeBase.GetTable<Street>();
            dialogRecipientPostCode.ItemsSource = homeBase.GetTable<PostCode>();
        }

        private void DialogRecipientSave_Click(object sender, RoutedEventArgs e)
        {
            AddAccount();    
            AddRecipient();
        }

        private void AddRecipient()
        {    
            Recipient newRecipient = new Recipient();
            newRecipient.Name = dialogRecipientName.Text;
            newRecipient.AccountID = homeBase.Accounts.Single(i => i.Name == dialogRecipientAccount.Text).AccountID;
            newRecipient.PostCodeID = (int)dialogRecipientPostCode.SelectedValue;
            newRecipient.CityID = (int)dialogRecipientCity.SelectedValue;
            newRecipient.StreetID = (int)dialogRecipientStreet.SelectedValue;
            newRecipient.BuildingNR = dialogRecipientBuildingNr.Text;

            homeBase.Recipients.InsertOnSubmit(newRecipient);
            homeBase.SubmitChanges();
        }

        private void AddAccount()
        {
            Account newAccount = new Account();
            newAccount.Name = dialogRecipientAccount.Text;

            homeBase.Accounts.InsertOnSubmit(newAccount);
            homeBase.SubmitChanges();
        }
    }
}
