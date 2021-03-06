﻿using System;
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
    public partial class DialogNewOrUpdateRecipient : Window
    {
        public bool Result { get; private set; }

        private CollectionToView<RecipientView> collectionRecipients;
        private CollectionToView<Street> collectionStreet;
        private CollectionToView<PostCode> collectionPostCode;
        private CollectionToView<City> collectionCity;
        private BillsBaseDataContext dateBase;
        private Tuple<string, object> street;
        private Tuple<string, object> city;
        private Tuple<string, object> postCode;
        private string name;
        private string account;
        private string buildingNr;
        private CorrectRecipient correctRecipient;
        private bool update;
        private int updatedRecipientID;

        public DialogNewOrUpdateRecipient(BillsBaseDataContext db)
        {          
            InitializeComponent();

            dateBase = db;
            correctRecipient = new CorrectRecipient(db);
            initializeCollection(db);
            loadDateToWindow();
            update = false;
            Result = false;        
        }

        private void initializeCollection(BillsBaseDataContext db)
        {
            collectionRecipients = new CollectionToView<RecipientView>(db);
            collectionStreet = new CollectionToView<Street>(db);
            collectionPostCode = new CollectionToView<PostCode>(db);
            collectionCity = new CollectionToView<City>(db);
        }

        private void loadDateToWindow()
        {
            loadComboboxes();
            dialogRecipientGrid.DataContext = correctRecipient;
            loadListView();
        }

        private void dialogRecipientSave_Click(object sender, RoutedEventArgs e)
        {
            if (checkCorrectData())
            {
                NewOrUpdateRecipient newOrUpdateRecipient = new NewOrUpdateRecipient(dateBase);

                if (update)
                {
                    newOrUpdateRecipient.UpdateItem(updatedRecipientID, name, account, street, buildingNr, postCode, city);
                    update = false;
                }
                else
                {                    
                    newOrUpdateRecipient.AddItem(name, account, street, buildingNr, postCode, city);
                }
                refreshView();
                Result = true;
            }
        }
        

        private void dialogRecipientCancel_Click(object sender, RoutedEventArgs e)
        {
            clearView();
        }


        private void loadComboboxes()
        {
            collectionStreet.LoadCollection();
            dialogRecipientStreet.ItemsSource = collectionStreet.Collection;
            collectionPostCode.LoadCollection();
            dialogRecipientPostCode.ItemsSource = collectionPostCode.Collection;
            collectionCity.LoadCollection();
            dialogRecipientCity.ItemsSource = collectionCity.Collection;
        }

        private void loadListView()
        {
            collectionRecipients.LoadCollection();
            collectionRecipients.Collection.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            listViewRecipient.ItemsSource = collectionRecipients.Collection;
        }

        private void refreshView()
        {
            clearView();
            refreshListView();
            refreshComboboxes();
        }

        private void clearView()
        {
            dialogRecipientName.Text = string.Empty;
            dialogRecipientAccount.Text = string.Empty;
            dialogRecipientStreet.Text = string.Empty;
            dialogRecipientBuildingNr.Text = string.Empty;
            dialogRecipientPostCode.Text = string.Empty;
            dialogRecipientCity.Text = string.Empty;
        }

        private void refreshListView()
        {
            collectionRecipients.RefreshCollection();
        }

        private void refreshComboboxes()
        {
            collectionStreet.RefreshCollection();
            collectionPostCode.RefreshCollection();
            collectionCity.RefreshCollection();
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
            trimText();
            changeHeightLetters();
            downloadDateFromWindow();

            correctRecipient.CheckData(update, name, account, street, buildingNr, postCode, city);
            return correctRecipient.Result;
        }

        private void changeHeightLetters()
        {
            dialogRecipientName.Text = dialogRecipientName.Text.UppercaseFirstInWords();
            dialogRecipientStreet.Text = dialogRecipientStreet.Text.UppercaseFirstInWords();
            dialogRecipientPostCode.Text = dialogRecipientPostCode.Text.UppercaseFirstInWords();
            dialogRecipientCity.Text = dialogRecipientCity.Text.UppercaseFirstInWords();
        }

        private void trimText()
        {
            dialogRecipientName.Text = dialogRecipientName.Text.Trim();
            dialogRecipientAccount.Text = dialogRecipientAccount.Text.Trim();
            dialogRecipientStreet.Text = dialogRecipientStreet.Text.Trim();
            dialogRecipientBuildingNr.Text = dialogRecipientBuildingNr.Text.Trim();
            dialogRecipientPostCode.Text = dialogRecipientPostCode.Text.Trim();
            dialogRecipientCity.Text = dialogRecipientCity.Text.Trim();
        }

        private void listViewRecipient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RecipientView recipientView = new RecipientView();
            ListView listView = sender as ListView;

            if (listView.SelectedItems.Count == 1)
            {
                recipientView = listView.SelectedItems[0] as RecipientView;

                dialogRecipientName.Text = recipientView.Name;
                dialogRecipientAccount.Text = recipientView.Account;
                dialogRecipientStreet.Text = recipientView.Street;
                dialogRecipientBuildingNr.Text = recipientView.BuildingNR;
                dialogRecipientPostCode.Text = recipientView.PostCode;
                dialogRecipientCity.Text = recipientView.City;

                update = true;
                updatedRecipientID = dateBase.Recipients.Single(i => i.Name == recipientView.Name).RecipientID;
            }
        }

        private void nameColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = e.Source as GridViewColumnHeader;

            string columnName = column.Tag.ToString();
            ListSortDirection sortDirection = new ListSortDirection();

            if (collectionRecipients.IsAscending())
                sortDirection = ListSortDirection.Descending;
            else
                sortDirection = ListSortDirection.Ascending;

            collectionRecipients.SetNewSortDescritpion(new SortDescription(columnName, sortDirection));
        }
    }
}
