namespace VideoRental.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using Abstract;
    using Data.Extensions;
    using Data.Infrastructure;
    using Data.Repositories;
    using Entities;
    using Utilities;

    public class MembershipService : IMembershipService
    {
        #region Variables

        private readonly IEntityBaseRepository<User> userRepository;
        private readonly IEntityBaseRepository<Role> roleRepository;
        private readonly IEntityBaseRepository<UserRole> userRoleRepository;
        private readonly IEncryptionService encryptionService;
        private readonly IUnitOfWork unitOfWork;

        #endregion

        public MembershipService(IEntityBaseRepository<User> userRepository, IEntityBaseRepository<Role> roleRepository,
            IEntityBaseRepository<UserRole> userRoleRepository, IEncryptionService encryptionService,
            IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.userRoleRepository = userRoleRepository;
            this.encryptionService = encryptionService;
            this.unitOfWork = unitOfWork;
        }

        public MembershipContext ValidateUser(string username, string password)
        {
            var membershipCtx = new MembershipContext();
            var user = userRepository.GetSingleByUsername(username);
            if (user != null && IsUserValid(user, password))
            {
                var userRoles = GetUserRoles(user.UserName);
                membershipCtx.User = user;

                var identity = new GenericIdentity(user.UserName);
                membershipCtx.Principal = new GenericPrincipal(identity, userRoles.Select(u => u.Name).ToArray());
            }

            return membershipCtx;
        }

        public User CreateUser(string username, string email, string password, int[] roles)
        {
            var existingUser = this.userRepository.GetSingleByUsername(username);
            if (existingUser != null)
                throw new Exception("Username is already in use");

            var passwordSalt = encryptionService.CreateSalt();
            var user = new User
            {
                UserName = username,
                Salt = passwordSalt,
                Email = email,
                IsLocked = false,
                HashedPassword = encryptionService.EncryptPassword(password, passwordSalt),
                DateCreated = DateTime.UtcNow
            };

            this.userRepository.Add(user);
            unitOfWork.Commit();

            if (roles != null || roles.Length > 0)
            {
                foreach (var role in roles)
                {
                    AddUserToRole(user, role);
                }
            }

            unitOfWork.Commit();
            return user;
        }

        public User GetUser(int userId)
        {
            return this.userRepository.GetSingle(userId);
        }

        public List<Role> GetUserRoles(string username)
        {
            var result = new List<Role>();
            var existingUser = this.userRepository.GetSingleByUsername(username);
            if (existingUser != null)
            {
                result.AddRange(existingUser.UserRoles.Select(userRole => userRole.Role));
            }

            return result.Distinct().ToList();
        }

        #region Helper methods

        private void AddUserToRole(User user, int roleId)
        {
            var role = roleRepository.GetSingle(roleId);
            if (role == null)
                throw new ApplicationException("Role doesn't exist");

            var userRole = new UserRole
            {
                RoleId = role.Id,
                UserId = user.Id
            };

            userRoleRepository.Add(userRole);
        }

        private bool IsPasswordValid(User user, string password)
        {
            return string.Equals(encryptionService.EncryptPassword(password, user.Salt), user.HashedPassword);
        }

        private bool IsUserValid(User user, string password)
        {
            if (IsPasswordValid(user, password))
                return !user.IsLocked;

            return false;
        }

        #endregion
    }
}