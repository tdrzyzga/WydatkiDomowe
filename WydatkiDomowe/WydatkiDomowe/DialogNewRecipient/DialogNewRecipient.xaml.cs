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
        private CityTable cityTab;
        private StreetTable streetTab;
        private PostCodeTable postCodeTab;
        private BillsBaseDataContext homeBase;


        public DialogNewRecipient(BillsBaseDataContext db)
        {
            InitializeComponent();
            homeBase = db;
            LoadDate();
        }

        private void LoadDate()
        {
            cityTab = new CityTable(homeBase);
            dialogRecipientCity.ItemsSource = cityTab.CityTab;

            streetTab = new StreetTable(homeBase);
            dialogRecipientStreet.ItemsSource = streetTab.StreetTab;

            postCodeTab = new PostCodeTable(homeBase);
            dialogRecipientPostCode.ItemsSource = postCodeTab.PostCodeTab;
        }

        private void dialogRecipientSave_Click(object sender, RoutedEventArgs e)
        {           

            Account newAccount = new Account();
            newAccount.Name = dialogRecipientAccount.Text;

            Recipient newRecipient = new Recipient();
            newRecipient.Name = dialogRecipientName.Text;
            newRecipient.PostCodeID = (int)dialogRecipientPostCode.SelectedValue;
            newRecipient.CityID = (int)dialogRecipientCity.SelectedValue;
            newRecipient.StreetID = (int)dialogRecipientStreet.SelectedValue;
            newRecipient.BuildingNR = dialogRecipientBuildingNr.Text;

            homeBase.Accounts.InsertOnSubmit(newAccount);
            homeBase.Recipients.InsertOnSubmit(newRecipient);

            homeBase.SubmitChanges();
        }
    }
}
