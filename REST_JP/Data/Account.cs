using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace REST_JP.Data
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        public string Name { get; set; }
        public string Iban { get; set; }
        public int Pac { get; set; }
        public float Balance { get; set; }

        public Account(int AccountId, string Name, string Iban, int Pac, float Balance)
        {
            this.AccountId = AccountId;
            this.Name = Name;
            this.Iban = Iban;
            this.Pac = Pac;
            this.Balance = Balance;
        }

    }
}
