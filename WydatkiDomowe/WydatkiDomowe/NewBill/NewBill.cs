using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WydatkiDomowe
{
    public class NewBill
    {
        public int ID { get; private set; }

        private BillsBaseDataContext dateBase;

        public NewBill(BillsBaseDataContext db)
        {
            dateBase = db;
        }

        public int AddItem(int recipientID, int billNameID, decimal amount, DateTime paymentDate, DateTime requiredDate)
        {
            Bills newBill = new Bills();
            newBill.RecipientID = recipientID;
            newBill.BillNameID = billNameID;
            newBill.Amount = amount;
            newBill.PaymentDate = paymentDate;
            newBill.RequiredDate = requiredDate;

            dateBase.Bills.InsertOnSubmit(newBill);
            dateBase.SubmitChanges();

            ID = dateBase.Bills.Single(i => i == newBill).BillsID;

            return ID;
        }        
    }
}
