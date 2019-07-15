using Microsoft.EntityFrameworkCore;
using VideoRental.Data.Configurations;
using VideoRental.Entities;

namespace VideoRental.Data
{
    public class VideoRentalContext : DbContext
    {
        public VideoRentalContext()
        {
            Database.EnsureCreated();
        }

        #region Entity Sets

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Error> Errors { get; set; }

        #endregion

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=(LocalDb)\v11.0;Initial Catalog=HomeCinema;Integrated Security=SSPI; MultipleActiveResultSets=true providerName=System.Data.SqlClient");
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new MovieConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new StockConfiguration());
            modelBuilder.ApplyConfiguration(new RentalConfiguration());
        }
    }
}