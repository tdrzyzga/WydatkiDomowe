using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WydatkiDomowe
{
    class CorrectBill : INotifyPropertyChanged
    {
        public bool Result { get; private set; }
        public bool IncorrectBillName { get; private set; }
        public bool IncorrectRecipient { get; private set; }
        public bool IncorrectAmount { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public CorrectBill()
        {
            resetField();
        }

        public void CheckData(object billName, object recipient, string amount)
        {
            resetField();

            string warnings = String.Empty;

            warnings += checkBillName(billName);
            warnings += checkRecipient(recipient);
            warnings += checkAmount(amount);

            if (isIncorrect())
            {
                Result = false;
                MessageBox.Show(warnings, "Niepoprawne dane!");
            }
        }

        private string checkBillName(object billName)
        {
            string warnings = String.Empty;

            var temp = billName as ComboBox;

            if (temp.SelectedValue == null)
            {
                warnings += "Wybierz rachunek do zapłaty!\n";
                IncorrectBillName = true;
                OnPropertyChanged("IncorrectBillName");
            }
            else
            {
                IncorrectBillName = false;
                OnPropertyChanged("IncorrectBillName");
            }

            return warnings;
        }

        private string checkRecipient(object recipient)
        {
            string warnings = String.Empty;

            var temp = recipient as ComboBox;

            if (temp.SelectedValue == null)
            {
                warnings += "Wybierz odbiorcę!\n";
                IncorrectRecipient = true;
                OnPropertyChanged("IncorrectRecipient");
            }
            else
            {
                IncorrectRecipient = false;
                OnPropertyChanged("IncorrectRecipient");
            }

            return warnings;
        }

        private string checkAmount(string amount)
        {
            string warnings = String.Empty;

            if (CorrectData.isEpmty(amount))
            {
                warnings += "Wprowadź kwotę!\n";
                IncorrectAmount = true;
                OnPropertyChanged("IncorrectAmount");
            }
            else
            {
                if (CorrectData.isNotDouble(amount))
                {
                    warnings += "Kwota nie jest liczbą!\n";
                    IncorrectAmount = true;
                    OnPropertyChanged("IncorrectAmount");
                }
                else
                {
                    IncorrectAmount = false;
                    OnPropertyChanged("IncorrectAmount");
                }
            }

            return warnings;
        }

        private bool isIncorrect()
        {
            return (IncorrectBillName || IncorrectRecipient || IncorrectAmount);
        }

        private void resetField()
        {
            Result = true;
            IncorrectBillName = false;
            IncorrectRecipient = false;
            IncorrectAmount = false;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
