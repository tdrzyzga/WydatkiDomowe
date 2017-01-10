using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;

namespace WydatkiDomowe
{
    class CorrectRecipient: INotifyPropertyChanged
    {
        public bool Result { get; private set; }
        public bool IncorrectBuildingNr { get; private set; }
        public bool IncorrectName { get; private set; }
        public bool IncorrectAccount { get; private set; }
        public bool IncorrectPostCode { get; private set; }
        public bool IncorrectStreet { get; private set; }
        public bool IncorrectCity { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public CorrectRecipient()
        {
            resetField();
        }

        public void CheckData(string name, string account, Tuple<string, object> street, string buildingNr, Tuple<string, object> postCode, Tuple<string, object> city)
        {
            string warnings = "";

            if (CorrectData.isEpmty(name))
            {
                warnings += "Wprowadź nazwę odbiorcy!";
                IncorrectName = true;
                OnPropertyChanged("IncorrectName");
                Result = false;
            }
            else IncorrectName = false;
            if (CorrectData.isEpmty(account))
            {
                warnings += "\nWprowadź numer konta!";
                IncorrectAccount = true;
                OnPropertyChanged("IncorrectAccount");
                Result = false;
            }
            else IncorrectAccount = false;
            if (CorrectData.isTooShort(account))
            {
                warnings += "\nZa krótki numer konta!";
                IncorrectAccount = true;
                OnPropertyChanged("IncorrectAccount");
                Result = false;
            }
            else IncorrectAccount = false;
            if (CorrectData.containsLetters(account))
            {
                warnings += "\nNumer konta zawiera litery!";
                IncorrectAccount = true;
                OnPropertyChanged("IncorrectAccount");
                Result = false;
            }
            else IncorrectAccount = false;
            if (CorrectData.containsNumbers(street.Item1))
            {
                warnings += "\nNazwa ulicy zawiera liczby!";
                IncorrectStreet = true;
                OnPropertyChanged("IncorrectStreet");
                Result = false;
            }
            else IncorrectStreet = false;
            if (CorrectData.isEpmty(buildingNr))
            {
                warnings += "\nWprowadź numer budynku!";
                IncorrectBuildingNr = true;
                OnPropertyChanged("IncorrectBuildingNr");
                Result = false;
            }
            else IncorrectBuildingNr = false;
            if (CorrectData.containsLetters(postCode.Item1))
            {
                warnings += "\nKod pocztowy zawiera litery!";
                IncorrectPostCode = true;
                OnPropertyChanged("IncorrectPostCode");
                Result = false;
            }
            else IncorrectPostCode = false;
            if (CorrectData.containsNumbers(city.Item1))
            {
                warnings += "\nNazwa miasta zawiera liczby!";                
                IncorrectCity = true;
                OnPropertyChanged("IncorrectCity");
                Result = false;
            }
            else IncorrectCity = false;
            if (!Result)
                MessageBox.Show(warnings);
            resetField();
        }

        private void resetField()
        {
            Result = true;
            IncorrectBuildingNr = false;
            IncorrectName = false;
            IncorrectAccount = false;
            IncorrectPostCode = false;
            IncorrectStreet = false;
            IncorrectCity = false;
        }
        
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
