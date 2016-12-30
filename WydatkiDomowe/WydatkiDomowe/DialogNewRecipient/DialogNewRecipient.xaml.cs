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
        private BillsBaseDataContext homeBase;
        private ObservableCollection<RecipientView> collectionRecipient;
        
        public DialogNewRecipient(BillsBaseDataContext db)
        {
            InitializeComponent();
            homeBase = db;
            LoadDate();
            LoadListView();
        }

        private void LoadListView()
        {
            collectionRecipient = new ObservableCollection<RecipientView>();
            foreach (var i in homeBase.GetTable<RecipientView>())
                collectionRecipient.Add(i);
            listViewRecipient.ItemsSource = collectionRecipient;
        }

        private void RefreshListView()
        {
            collectionRecipient.Clear();
            foreach (var i in homeBase.GetTable<RecipientView>())
                collectionRecipient.Add(i);
        }

        private void LoadDate()
        {
            dialogRecipientCity.ItemsSource = homeBase.GetTable<City>();
            dialogRecipientStreet.ItemsSource = homeBase.GetTable<Street>();
            dialogRecipientPostCode.ItemsSource = homeBase.GetTable<PostCode>();

        }

        private void DialogRecipientSave_Click(object sender, RoutedEventArgs e)
        {  
            AddRecipient();
            RefreshListView();
        }

        private void AddRecipient()
        {    
            Recipient newRecipient = new Recipient();
            newRecipient.Name = dialogRecipientName.Text;
            newRecipient.Account = dialogRecipientAccount.Text;            
            newRecipient.PostCodeID = AddPostCode();     
            newRecipient.CityID = AddCity();
            newRecipient.StreetID = AddStreet();
            newRecipient.BuildingNR = dialogRecipientBuildingNr.Text;

            homeBase.Recipients.InsertOnSubmit(newRecipient);
            homeBase.SubmitChanges();
        }

        private int AddStreet()
        {
            int streetID;

            if (dialogRecipientStreet.SelectedValue == null)
            {
                Street newStreet = new Street();
                newStreet.Name = dialogRecipientStreet.Text;
                homeBase.Streets.InsertOnSubmit(newStreet);
                homeBase.SubmitChanges();
                streetID = homeBase.Streets.Single(i => i.Name == dialogRecipientStreet.Text).StreetID;
            }
            else
                streetID = (int)dialogRecipientStreet.SelectedValue;
            return streetID;
        }

        private int AddPostCode()
        {
            int postCodeID;

            if (dialogRecipientPostCode.SelectedValue == null)
            {
                PostCode newPostCode = new PostCode();
                newPostCode.Name = dialogRecipientPostCode.Text;
                homeBase.PostCodes.InsertOnSubmit(newPostCode);
                homeBase.SubmitChanges();
                postCodeID = homeBase.PostCodes.Single(i => i.Name == dialogRecipientPostCode.Text).PostCodeID;
            }
            else
                postCodeID = (int)dialogRecipientPostCode.SelectedValue;

            return postCodeID;
        }

        private int AddCity()
        {
            int cityID;

            if (dialogRecipientCity.SelectedValue == null)
            {
                City newCity = new City();
                newCity.Name = dialogRecipientCity.Text;
                homeBase.Cities.InsertOnSubmit(newCity);
                homeBase.SubmitChanges();
                cityID = homeBase.Cities.Single(i => i.Name == dialogRecipientCity.Text).CityID;
            }

            else
                cityID = (int)dialogRecipientCity.SelectedValue;

            return cityID;
        }

        private void DialogRecipientCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
