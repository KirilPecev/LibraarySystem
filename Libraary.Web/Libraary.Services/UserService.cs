namespace Libraary.Services
{
    using Data;
    using Domain;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly LibraaryDbContext db;
        private readonly UserManager<LibraaryUser> userManager;

        public UserService(LibraaryDbContext db, UserManager<LibraaryUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public void AddRole(string email, string role)
        {
            var user = this.userManager.Users.SingleOrDefault(u => u.Email == email);
            this.userManager.AddToRoleAsync(user, role).Wait();
        }

        public void ChangeRoles(string email, string oldRole, string newRole)
        {
            var user = this.userManager.Users.SingleOrDefault(u => u.Email == email);
            this.userManager.RemoveFromRoleAsync(user, oldRole).Wait();
            this.userManager.AddToRoleAsync(user, newRole).Wait();
        }

        public string GetFirstAndLastNamesOfUser(Library library, IList<LibraaryUser> owners)
        {
            return owners
                .FirstOrDefault(l => l.LibraryId == library.Id)
                ?.ToString();
        }

        public string GetFirstName(string email)
        {
            return this.GetUser(email).FirstName;
        }

        public LibraaryUser GetUser(string email)
        {
            return this.userManager.Users.SingleOrDefault(u => u.Email == email);
        }

        public string GetUserLibraryId(string email)
        {
            return this.GetUser(email).LibraryId;
        }

        public int GetUsersCount()
        {
            return this.userManager.Users.Count();
        }

        public IList<LibraaryUser> GetUsersInRole(string role)
        {
            return this.userManager.GetUsersInRoleAsync(role).Result;
        }
    }
}
