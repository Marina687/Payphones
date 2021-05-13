using Microsoft.EntityFrameworkCore;
using MoscowPayphones.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoscowPayphones.InfrastructureServices.Gateways.Database
{
    public class PayphonesContext : DbContext
    {
        public DbSet<Payphones> Payphones { get; set; }

        public PayphonesContext(DbContextOptions<PayphonesContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            FillTestData(modelBuilder);
        }
        private void FillTestData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payphones>().HasData(
                       new
                       {
                            Id = 1L,
                            DescriptionLocation = "Вавилова улица, дом 5А",
                            IntercityConnectionPayment = "бесплатно",
                            Name = "Таксофон № 1449",
                            PayWay = "карта",
                            ValidUniversalServicesCard = "не действует"
                        },
                        new
                        {
                            Id = 2L,
                            DescriptionLocation = "Вавилова улица, дом 51, Школа №199",
                            IntercityConnectionPayment = "бесплатно",
                            Name = "Таксофон № 1499",
                            PayWay = "карта",
                            ValidUniversalServicesCard = "не действует"
                        },
                        new
                        {
                            Id = 3L,
                            DescriptionLocation = "Вавилова улица, дом 6",
                            IntercityConnectionPayment = "бесплатно",
                            Name = "Таксофон № 76",
                            PayWay = "карта",
                            ValidUniversalServicesCard = "не действует"
                        },
                        new
                        {
                            Id = 4L,
                            DescriptionLocation = "Валдайский проезд, дом 14, Школа №158",
                            IntercityConnectionPayment = "бесплатно",
                            Name = "Таксофон № 1857",
                            PayWay = "карта",
                            ValidUniversalServicesCard = "не действует"

                        });
        }
    }
}
