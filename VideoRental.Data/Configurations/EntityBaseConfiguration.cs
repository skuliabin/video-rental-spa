using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoRental.Entities;

namespace VideoRental.Data.Configurations
{
    internal class EntityBaseConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IEntityBase
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}