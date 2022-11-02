using BankSystem.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BankSystem.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                if (context.Currencies.Any() && context.Deposits.Any())
                {
                    return;
                }


                context.Currencies.AddRange(
                    new Currency
                    {
                        Name = "Dollar",
                        ShortName = "USD",
                        CostInKZT = 467.5
                    },
                     new Currency
                     {
                         Name = "Euro",
                         ShortName = "EUR",
                         CostInKZT = 463.2
                     },
                      new Currency
                      {
                          Name = "Yuan",
                          ShortName = "CNY",
                          CostInKZT = 64.4
                      }
                    );

                context.Deposits.AddRange(
                    new Deposit
                    {
                        Depos = 10000,
                        Сurrency = new Currency
                        {
                            Name = "Tenge",
                            ShortName = "KZT",
                            CostInKZT = 1
                        }
                    }
                    );

                context.SaveChanges();
            }

        }

    }
}
