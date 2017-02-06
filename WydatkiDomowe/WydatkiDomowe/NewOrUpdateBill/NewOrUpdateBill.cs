using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WydatkiDomowe
{
    public class NewOrUpdateBill
    {
        public int ID { get; private set; }

        private BillsBaseDataContext dateBase;

        public NewOrUpdateBill(BillsBaseDataContext db)
        {
            dateBase = db;
        }

        public void UpdateItem(int id, int recipientID, int billNameID, decimal amount, DateTime paymentDate, DateTime requiredDate)
        {
            Bills updateBill = dateBase.Bills.Single(i => i.BillsID == id);
            updateBill.RecipientID = recipientID;
            updateBill.BillNameID = billNameID;
            updateBill.Amount = amount;
            updateBill.PaymentDate = paymentDate;
            updateBill.RequiredDate = requiredDate;

            dateBase.SubmitChanges();

            ID = id;
        }  

        public void AddItem(int recipientID, int billNameID, decimal amount, DateTime paymentDate, DateTime requiredDate)
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
        }        
    }
}
