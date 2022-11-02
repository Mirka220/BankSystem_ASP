namespace BankSystem.Models
{
    public class Currency
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public double CostInKZT { get; set; }
    }
}
