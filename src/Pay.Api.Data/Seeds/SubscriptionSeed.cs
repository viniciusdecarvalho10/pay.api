using System;
using System.Collections.Generic;
using Pay.Api.Core.Extensions;
using Pay.Api.Domain.Constants;
using Pay.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Pay.Api.Data.Migrations
{
    public partial class SubscriptionSeed : ISeed
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subscription>().HasData(
                new Subscription()
                {
                    Id = Constants.SubscriptionSystemId,
                    Name = "Subscription para system administration",
                    CreateDate = DateTime.Parse("2022-05-15T21:21:46")
                }
            );
        }
    }
}