using InternationalBusinessMen.Domain.Domains;
using InternationalBusinessMen.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternationalBusinessMen.ApplicationCore
{
    public interface ITransactionsService
    {
        ICollection<Transactions> getAllTransactions(Boolean needStorage = true);
        ICollection<Rates> getAllRates(Boolean needStorage = true);
        TransactionsBySku getTransactionsBySku(string sku);
    }
}
