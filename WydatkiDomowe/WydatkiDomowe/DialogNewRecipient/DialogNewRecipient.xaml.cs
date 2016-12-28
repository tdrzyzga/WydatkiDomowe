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
        private CityTable city;
        private StreetTable street;
        private PostCodeTable postCode;


        public DialogNewRecipient(BillsBaseDataContext homeBase)
        {
            InitializeComponent();
            LoadDate(homeBase);
        }

        private void LoadDate(BillsBaseDataContext homeBase)
        {
            city = new CityTable(homeBase);
            dialogRecipientCity.ItemsSource = city.CityTab;

            street = new StreetTable(homeBase);
            dialogRecipientStreet.ItemsSource = street.StreetTab;

            postCode = new PostCodeTable(homeBase);
            dialogRecipientPostCode.ItemsSource = postCode.PostCodeTab;
        }
    }
}
