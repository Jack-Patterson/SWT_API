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
        public int Id { get; set; }
        public string Name { get; set; }
        public string Iban { get; set; }
        public string Pac { get; set; }
        public float Balance { get; set; }

        public Account(int accountId, string name, string iban, string pac, float balance)
        {
            Id = accountId;
            Name = name;
            Iban = iban;
            Pac = pac;
            Balance = balance;
        }

    }
}
