using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Pay.Api.Data.Extensions;
using Pay.Api.Data.Mapping;
using Pay.Api.Domain.Constants;
using Pay.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Pay.Api.Data.Context
{
    public partial class RepositoryDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<UserSubscription> UserSubscription { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(new UserMap().Configure);
            modelBuilder.Entity<Subscription>(new SubscriptionMap().Configure);
            modelBuilder.Entity<UserSubscription>(new UserSubscriptionMap().Configure);

            // Filtra as entidades que herdam (BaseTenantControlAccess) pela idSubscription
            modelBuilder.SetQueryFilterOnAllEntities<BaseTenantControlAccess>(e => e.SubscriptionId == GetSubscriptionId() || e.SubscriptionId == Constants.SubscriptionSystemId);

            modelBuilder.Registrations();
            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges(bool accept)
        {
            InserirSubscription();

            return base.SaveChanges(accept);
        }
        public override async Task<int> SaveChangesAsync(bool accept, CancellationToken cancellationToken = default)
        {
            InserirSubscription();

            return await base.SaveChangesAsync(accept, cancellationToken);
        }

        protected void InserirSubscription()
        {
            var entities = ChangeTracker.Entries()
                .Where(t => t.State == EntityState.Added || t.State == EntityState.Modified)
                .Select(t => t.Entity)
                .ToArray();

            foreach (var entity in entities)
            {
                if (entity is BaseTenantControlAccess entityBaseSubscription)
                {
                    if (entityBaseSubscription.SubscriptionId == Guid.Empty || (entityBaseSubscription.SubscriptionId != GetSubscriptionId() && GetSubscriptionId() != Guid.Empty))
                    {
                        entityBaseSubscription.SubscriptionId = (Guid)GetSubscriptionId();
                    }
                }
            }
        }

        private Guid? GetSubscriptionId()
        {
            return _tenantService == null ? Guid.Empty : _tenantService.GetSubscriptionId();
        }
    }
}