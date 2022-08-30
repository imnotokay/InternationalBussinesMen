using System.Collections.Generic;
using InternationalBusinessMen.Domain.Domains;

namespace InternationalBusinessMen.Domain.Interfaces
{
    public interface ITransactionsBySku
    {
        ICollection<Transactions> Transactions { get; set; }
        decimal total { get; set; }
    }
}
