using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;

namespace WydatkiDomowe
{
    public class CorrectBillName : INotifyPropertyChanged
    {
        public bool Result { get; private set; }
        public bool IncorrectName { get; private set; }
        public bool IncorrectPaymentsFrequency { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private BillsBaseDataContext dateBase;

        public CorrectBillName(BillsBaseDataContext db)
        {
            dateBase = db;
            resetField();
        }

        public void CheckData(string name, string paymentsFrequency)
        {
            resetField();

            string warnings = "";

            warnings += checkName(name);
            warnings += checkPaymentsFrequency(paymentsFrequency);

            if (isIncorrect())
            {
                Result = false;
                MessageBox.Show(warnings, "Niepoprawne dane!");
            }
        }

        private bool isIncorrect()
        {
            return (IncorrectName || IncorrectPaymentsFrequency );
        }

        private string checkPaymentsFrequency(string paymentsFrequency)
        {
            string warnings = "";

            if (CorrectData.isEpmty(paymentsFrequency))
            {
                warnings += "Wprowadź częstotliwość kolejnych wpłat!\n";
                IncorrectPaymentsFrequency = true;
                OnPropertyChanged("IncorrectPaymentsFrequency");
            }
            else
            {
                if (CorrectData.isString(paymentsFrequency))
                {
                    warnings += "Częstotliwość kolejnych wpłat nie jest liczbą całkowitą dodatnią!\n";
                    IncorrectPaymentsFrequency = true;
                    OnPropertyChanged("IncorrectPaymentsFrequency");
                }
                else
                {
                    IncorrectPaymentsFrequency = false;
                    OnPropertyChanged("IncorrectPaymentsFrequency");
                }
            }

            return warnings;
        }
        
        private bool existInDatebase(string name)
        {
            return dateBase.BillNames.Any(i => i.Name == name);
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
                name = name.Trim();

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

        private void resetField()
        {
            Result = true;
            IncorrectName = false;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
