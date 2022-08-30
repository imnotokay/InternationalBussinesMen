using System;

namespace InternationalBusinessMen.Domain.Interfaces
{
    public interface ITransactions
    {
        string sku { get; set; }
        decimal amount { get; set; }
        string currency { get; set; }
    }
}
