namespace Libraary.Services
{
    using Libraary.Domain;
    using System.Collections.Generic;

    public interface IUserService
    {
        string GetFirstName(string email);

        void ChangeRoles(string email, string oldRole, string newRole);

        LibraaryUser GetUser(string email);

        IList<LibraaryUser> GetUsersInRole(string role);

        string GetFirstAndLastNamesOfUser(Library library, IList<LibraaryUser> owners);

        string GetUserLibraryId(string name);

        int GetUsersCount();

        void AddRole(string email, string v);

        string GetPhoneOfUserByName(string userName);
    }
}
