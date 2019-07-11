namespace Libraary.Services
{
    using Libraary.Data;
    using Libraary.Domain;
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

        public void ChangeRoles(string email, string oldRole, string newRole)
        {
            var user = this.userManager.Users.SingleOrDefault(u => u.Email == email);
            this.userManager.RemoveFromRoleAsync(user, oldRole).Wait();
            this.userManager.AddToRoleAsync(user, newRole).Wait();
        }

        public string GetFirstAndLastNamesOfUser(Library library, IList<LibraaryUser> owners)
        {
            return owners.Where(l => l.LibraryId == library.Id)
                .FirstOrDefault()
                .ToString();
        }

        public string GetFirstName(string email)
        {
            return this.db.Users.SingleOrDefault(user => user.Email == email).FirstName;
        }

        public LibraaryUser GetUser(string email)
        {
            return this.db.Users.SingleOrDefault(u => u.Email == email);
        }

        public string GetUserLibraryId(string name)
        {
            return this.db.Users.SingleOrDefault(user => user.UserName == name).LibraryId;
        }

        public int GetUsersCount()
        {
            return this.db.Users.Count();
        }

        public IList<LibraaryUser> GetUsersInRole(string role)
        {
            return this.userManager.GetUsersInRoleAsync(role).Result;
        }
    }
}
