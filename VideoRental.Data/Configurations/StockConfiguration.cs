using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoRental.Entities;

namespace VideoRental.Data.Configurations
{
    internal class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.Property(s => s.MovieId).IsRequired();
            builder.Property(s => s.UniqueKey).IsRequired();
            builder.Property(s => s.IsAvailable).IsRequired();
            builder.HasMany(s => s.Rentals).WithOne(r => r.Stock).HasForeignKey(r => r.StockId);
        }
    }
}