namespace VideoRental.Services.Abstract
{
    using System.Collections.Generic;
    using Entities;
    using Utilities;

    public interface IMembershipService
    {
        MembershipContext ValidateUser(string username, string password);
        User CreateUser(string username, string email, string password, int[] roles);
        User GetUser(int userId);
        List<Role> GetUserRoles(string username);
    }
}