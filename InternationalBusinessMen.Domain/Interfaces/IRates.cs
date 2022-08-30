namespace InternationalBusinessMen.Domain.Interfaces
{
    public interface IRates
    {
        string from { get; set; }
        string to { get; set; }
        decimal rate { get; set; }
    }
}
