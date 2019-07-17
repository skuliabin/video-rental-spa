namespace VideoRental.Data.Infrastructure
{
    using System;

    public interface IDbFactory : IDisposable
    {
        VideoRentalContext Init();
    }
}