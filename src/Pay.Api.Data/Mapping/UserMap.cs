using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pay.Api.Domain.Entities;

namespace Pay.Api.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

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
                .Property(a => a.Email)
                .IsRequired(false)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .HasColumnName("Email");

            builder
                .Property(a => a.Password)
                .IsRequired(false)
                .HasColumnType("varchar(200)")
                .HasMaxLength(200)
                .HasColumnName("Password");

            builder
                .Property(a => a.Phone)
                .HasColumnType("varchar(25)")
                .HasMaxLength(25)
                .HasColumnName("Phone");
        }
    }
}
