namespace VideoRental.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private VideoRentalContext dbContext;
        
        public VideoRentalContext Init()
        {
            return dbContext ?? (dbContext = new VideoRentalContext());
        }

        protected override void DisposeCore()
        {
            dbContext?.Dispose();
        }
    }
}