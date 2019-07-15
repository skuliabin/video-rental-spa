using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoRental.Entities;

namespace VideoRental.Data.Configurations
{
    internal class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.Property(r => r.CustomerId).IsRequired();
            builder.Property(r => r.StockId).IsRequired();
            builder.Property(r => r.Status).IsRequired().HasMaxLength(10);
            builder.Property(r => r.RentalDate).IsRequired();
        }
    }
}