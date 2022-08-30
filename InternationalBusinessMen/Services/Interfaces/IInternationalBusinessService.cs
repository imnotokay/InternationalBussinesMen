using InternationalBusinessMen.Domain.Domains;
using System.Collections.Generic;

namespace InternationalBusinessMen.Services.Interfaces
{
    public interface IInternationalBusinessService
    {
        ICollection<Transactions> getAllTransactions();
        ICollection<Rates> getAllRates();
        TransactionsBySku getTransactionsBySku(string sku);
    }
}
