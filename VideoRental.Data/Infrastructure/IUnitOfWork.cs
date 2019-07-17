namespace VideoRental.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}