namespace VideoRental.Services.Utilities
{
    using System.Security.Principal;
    using Entities;

    public class MembershipContext
    {
        public IPrincipal Principal { get; set; }
        public User User { get; set; }

        public bool IsValid()
        {
            return Principal != null;
        }
    }
}