using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace REST_JP.Data
{
    public class Transaction
    {
        [Key]
        public int TransId { get; set; }
        public DateTime TransDate { get; set; }
        public float Amount { get; set; }
        public int SenderAccountID { get; set; }
        public int ReceiverAccountID { get; set; }

        public Transaction(int TransId, DateTime TransDate, float Amount, int SenderAccountID, int ReceiverAccountID)
        {
            this.TransId = TransId;
            this.TransDate = TransDate;
            this.Amount = Amount;
            this.SenderAccountID = SenderAccountID;
            this.ReceiverAccountID = ReceiverAccountID;
        }

    }
}
