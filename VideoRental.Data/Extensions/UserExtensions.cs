namespace VideoRental.Data.Extensions
{
    using System.Linq;
    using Entities;
    using Repositories;

    public static class UserExtensions
    {
        public static User GetSingleByUsername(this IEntityBaseRepository<User> userRepository, string username)
        {
            return userRepository.GetAll().FirstOrDefault(x => x.UserName == username);
        }
    }
}