using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pay.Api.Domain.Entities;

namespace Pay.Api.Data.Mapping
{
  public class SubscriptionMap : IEntityTypeConfiguration<Subscription>
  {
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
      builder.ToTable("subscription");

      builder
          .HasKey(a => a.Id);

      builder
          .Property(a => a.Id)
          .IsRequired()
          .HasColumnName("Id");

      builder
          .Property(a => a.Name)
          .IsRequired(false)
          .HasColumnType("varchar(100)")
          .HasMaxLength(100)
          .HasColumnName("Name");

      builder
          .Property(a => a.Document)
          .HasColumnType("varchar(45)")
          .HasMaxLength(45)
          .HasColumnName("Document");

      builder
          .Property(a => a.Address)
          .HasColumnType("varchar(150)")
          .HasMaxLength(150)
          .HasColumnName("Address");

      builder
          .Property(a => a.ZipCode)
          .HasColumnType("varchar(10)")
          .HasMaxLength(10)
          .HasColumnName("ZipCode");

      builder
          .Property(a => a.Email)
          .IsRequired(false)
          .HasColumnType("varchar(100)")
          .HasMaxLength(100)
          .HasColumnName("Email");

      builder
          .Property(a => a.Phone)
          .HasColumnType("varchar(25)")
          .HasMaxLength(25)
          .HasColumnName("Phone");
    }
  }
}
