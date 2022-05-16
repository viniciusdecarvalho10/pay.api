using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pay.Api.Domain.Entities;

namespace Pay.Api.Data.Mapping
{
    public class UserSubscriptionMap : IEntityTypeConfiguration<UserSubscription>
    {
        public void Configure(EntityTypeBuilder<UserSubscription> builder)
        {
            builder.ToTable("userSubscription");

            builder
                .HasKey(a => a.Id);

            builder
                .Property(a => a.Id)
                .IsRequired()
                .HasColumnName("Id");

            builder
                .HasOne<Subscription>(o => o.SubscriptionReferencia)
                .WithMany()
                .HasForeignKey(o => o.SubscriptionId)
                .HasPrincipalKey(d => d.Id)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne<User>(o => o.UserReferencia)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .HasPrincipalKey(d => d.Id)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
