﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using System.ComponentModel;

namespace WydatkiDomowe
{ 
    public class NewOrUpdateRecipient
    {
        public int ID {get; private set;}

        private BillsBaseDataContext dateBase;
        
        public NewOrUpdateRecipient(BillsBaseDataContext db)
        {
            dateBase = db;
        }

        public void AddItem(string name, string account, Tuple<string, object> street, string buildingNr, Tuple<string, object> postCode, Tuple<string, object> city)
        {

            Recipient newRecipient = new Recipient();
            newRecipient.Name = name;
            newRecipient.Account = account;
            newRecipient.StreetID = addItem<Street>(street);
            newRecipient.BuildingNR = buildingNr;
            newRecipient.PostCodeID = addItem<PostCode>(postCode);
            newRecipient.CityID = addItem<City>(city);

            dateBase.Recipients.InsertOnSubmit(newRecipient);
            dateBase.SubmitChanges();
            ID = dateBase.Recipients.Single(i => i.Name == name).RecipientID;
        }


        public void UpdateItem(int id, string name, string account, Tuple<string, object> street, string buildingNr, Tuple<string, object> postCode, Tuple<string, object> city)
        {           
            Recipient updatedRecipient = dateBase.Recipients.Single(i => i.RecipientID == id);
            updatedRecipient.Name = name;
            updatedRecipient.Account = account;
            updatedRecipient.StreetID = addItem<Street>(street);
            updatedRecipient.BuildingNR = buildingNr;
            updatedRecipient.PostCodeID = addItem<PostCode>(postCode);
            updatedRecipient.CityID = addItem<City>(city);

            dateBase.SubmitChanges();
            ID = id;
        }

        private int addItem<T>(Tuple<string, object> item) where T: INameInterface, new() 
        {
            int ID;
            
            if (item.Item2 == null)
            {
                var newItem = new T();
                newItem.Name = item.Item1;
                if( newItem is City)
                {
                    dateBase.Cities.InsertOnSubmit(newItem as City);
                    dateBase.SubmitChanges();
                    ID = dateBase.Cities.Single(i => i.Name == item.Item1).CityID;
                }
                else if (newItem is Street)
                {
                    dateBase.Streets.InsertOnSubmit(newItem as Street);
                    dateBase.SubmitChanges();
                    ID = dateBase.Streets.Single(i => i.Name == item.Item1).StreetID;
                }
                else
                {
                    dateBase.PostCodes.InsertOnSubmit(newItem as PostCode);
                    dateBase.SubmitChanges();
                    ID = dateBase.PostCodes.Single(i => i.Name == item.Item1).PostCodeID;
                }
            }
            else
                ID = (int)item.Item2;

            return ID;
        }     
    }
}
