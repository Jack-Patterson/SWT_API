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
        public int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public float Amount { get; set; }
        public int SenderAccID { get; set; }
        public int ReceiverAccID { get; set; }

        public Transaction(int transactionId, DateTime date, float amount, int senderAccID, int receiverAccID)
        {
            TransactionId = transactionId;
            Date = date;
            Amount = amount;
            SenderAccID = senderAccID;
            ReceiverAccID = receiverAccID;
        }

    }
}
