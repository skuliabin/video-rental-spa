namespace VideoRental.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private VideoRentalContext dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public VideoRentalContext DbContext => dbContext ?? (dbContext = dbFactory.Init());

        public void Commit()
        {
            DbContext.Commit();
        }
    }
}