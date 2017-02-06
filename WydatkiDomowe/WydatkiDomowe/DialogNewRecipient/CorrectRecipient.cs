using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;

namespace WydatkiDomowe
{
    public class CorrectRecipient: INotifyPropertyChanged
    {
        public bool Result { get; private set; }
        public bool IncorrectBuildingNr { get; private set; }
        public bool IncorrectName { get; private set; }
        public bool IncorrectAccount { get; private set; }
        public bool IncorrectPostCode { get; private set; }
        public bool IncorrectStreet { get; private set; }
        public bool IncorrectCity { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private BillsBaseDataContext dateBase;

        public CorrectRecipient(BillsBaseDataContext db)
        {
            dateBase = db;
            resetField();
        }

        public void CheckData(string name, string account, Tuple<string, object> street, string buildingNr, Tuple<string, object> postCode, Tuple<string, object> city)
        {
            resetField();

            string warnings = "";

            warnings += checkName(name);
            warnings += checkAccount(account);
            warnings += checkStreet(street.Item1);
            warnings += checkBuildingNr(buildingNr);
            warnings += checkPostCode(postCode.Item1);
            warnings += checkCity(city.Item1);

            if (isIncorrect())
            {
                Result = false;
                MessageBox.Show(warnings, "Niepoprawne dane!");
            }
         }

        private bool isIncorrect()
        {
            return (IncorrectName || IncorrectAccount || IncorrectStreet || IncorrectBuildingNr || IncorrectPostCode || IncorrectCity);
        }

        private string checkCity(string city)
        {
            string warnings = "";

            if (CorrectData.isEpmty(city))
            {
                warnings += "Wprowadź nazwę miasta!\n";
                IncorrectCity = true;
                OnPropertyChanged("IncorrectCity");
            }
            else
            {
                if (CorrectData.containsNumbers(city))
                {
                    warnings += "Nazwa miasta zawiera liczby!\n";
                    IncorrectCity = true;
                    OnPropertyChanged("IncorrectCity");
                }
                else
                {
                    IncorrectCity = false;
                    OnPropertyChanged("IncorrectCity");
                }
            }

            return warnings;
        }

        private string checkPostCode(string postCode)
        {
            string warnings = "";

            if (CorrectData.isEpmty(postCode))
            {
                warnings += "Wprowadź kod pocztowy!\n";
                IncorrectPostCode = true;
                OnPropertyChanged("IncorrectPostCode");
            }
            else
            {
                if (CorrectData.containsLetters(postCode))
                {
                    warnings += "Kod pocztowy zawiera litery!\n";
                    IncorrectPostCode = true;
                    OnPropertyChanged("IncorrectPostCode");
                }
                else
                {
                    IncorrectPostCode = false;
                    OnPropertyChanged("IncorrectPostCode");
                }
            }

            return warnings;
        }

        private string checkBuildingNr(string buildingNr)
        {
            string warnings = "";

            if (CorrectData.isEpmty(buildingNr))
            {
                warnings += "Wprowadź numer budynku!\n";
                IncorrectBuildingNr = true;
                OnPropertyChanged("IncorrectBuildingNr");
            }
            else
            {
                IncorrectBuildingNr = false;
                OnPropertyChanged("IncorrectBuildingNr");
            }

            return warnings;
        }

        private string checkStreet(string street)
        {
            string warnings = "";

            if (CorrectData.isEpmty(street))
            {
                warnings += "Wprowadź nazwę ulicy!\n";
                IncorrectStreet = true;
                OnPropertyChanged("IncorrectStreet");
            }
            else
            {
                if (CorrectData.containsNumbers(street))
                {
                    warnings += "Nazwa ulicy zawiera liczby!\n";
                    IncorrectStreet = true;
                    OnPropertyChanged("IncorrectStreet");
                }
                else
                {
                    IncorrectStreet = false;
                    OnPropertyChanged("IncorrectStreet");
                }
            }

            return warnings;
        }

        private string checkAccount(string account)
        {
            string warnings = "";
            bool isShort = true;
            bool containsLetters = true;

            if (CorrectData.isEpmty(account))
            {
                warnings += "Wprowadź numer konta!\n";
                IncorrectAccount = true;
                OnPropertyChanged("IncorrectAccount");
            }
            else
            {
                if (CorrectData.isTooShort(account))
                {
                    warnings += "Za krótki numer konta!\n";
                    isShort = true;
                }
                else
                    isShort = false;

                if (CorrectData.containsLetters(account))
                {
                    warnings += "Numer konta zawiera litery!\n";
                    containsLetters = true;
                }
                else
                    containsLetters= false;

                if (isShort || containsLetters)
                {
                    IncorrectAccount = true;
                    OnPropertyChanged("IncorrectAccount");
                }
                else
                {
                    IncorrectAccount = false;
                    OnPropertyChanged("IncorrectAccount");
                }
            }
 
            return warnings;
        }

        private string checkName(string name)
        {
            string warnings = "";

            if (CorrectData.isEpmty(name))
            {
                warnings += "Wprowadź nazwę odbiorcy!\n";
                IncorrectName = true;
                OnPropertyChanged("IncorrectName");
            }
            else
            {

                if (existInDatebase(name))
                {
                    warnings += "Podana nazwa odbiorcy isnieje już w bazie danych!\n";
                    IncorrectName = true;
                    OnPropertyChanged("IncorrectName");
                }
                else
                {
                    IncorrectName = false;
                    OnPropertyChanged("IncorrectName");
                }
            }

            return warnings;
        }

        private bool existInDatebase(string name)
        {
            return dateBase.Recipients.Any(i=> i.Name == name);  
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
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
