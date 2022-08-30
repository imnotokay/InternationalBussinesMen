using InternationalBusinessMen.Domain.Interfaces;

namespace InternationalBusinessMen.Domain.Domains
{
    public class Rates : IRates
    {
        public string from { get; set; }
        public string to { get; set; }
        public decimal rate { get; set; }
    }
}
