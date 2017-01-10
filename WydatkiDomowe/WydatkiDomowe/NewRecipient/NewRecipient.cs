using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;

namespace WydatkiDomowe
{ 
    class NewRecipient
    {
        public int ID {get; private set;}

        private BillsBaseDataContext dateBase;
        
        public NewRecipient(BillsBaseDataContext db)
        {
            dateBase = db;
        }

        public void AddItem(string name, string account, Tuple<string, object> street, string buildingNr,Tuple<string, object> postCode, Tuple<string, object> city)
        {
            try
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
            catch (SqlException ex)
            {
                if (ex.Message.Contains("U_Account"))
                    MessageBox.Show("Podana nazwa konta isnieje już w bazie danych!");
                else if (ex.Message.Contains("U_Recipient"))
                    MessageBox.Show("Podana nazwa odbiorcy isnieje już w bazie danych!");
            }
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
