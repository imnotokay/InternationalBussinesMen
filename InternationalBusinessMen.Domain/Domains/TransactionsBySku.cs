using System.Collections.Generic;
using InternationalBusinessMen.Domain.Interfaces;


namespace InternationalBusinessMen.Domain.Domains
{
    public class TransactionsBySku : ITransactionsBySku
    {
        public ICollection<Transactions> Transactions { get; set; }
        public decimal total { get; set; }
    }
}
