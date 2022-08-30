using InternationalBusinessMen.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternationalBusinessMen.Domain.Domains
{
    public class Transactions : ITransactions
    {
        public string sku { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; }
    }
}
