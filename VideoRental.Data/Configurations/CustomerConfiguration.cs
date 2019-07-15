using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoRental.Entities;

namespace VideoRental.Data.Configurations
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(c => c.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(c => c.LastName).IsRequired().HasMaxLength(100);
            builder.Property(c => c.IdentityCard).IsRequired().HasMaxLength(50);
            builder.Property(c => c.UniqueKey).IsRequired();
            builder.Property(c => c.Mobile).HasMaxLength(10);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(200);
            builder.Property(c => c.DateOfBirth).IsRequired();
        }
    }
}