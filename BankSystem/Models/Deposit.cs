using BankSystem.Models;

namespace BankSystem.Models
{
    public class Deposit
    {
        public Guid Id { get; set; }
        public decimal Depos { get; set; }

        public Currency Сurrency { get; set; }
    }
}
