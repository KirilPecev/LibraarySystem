namespace Libraary.Services
{
    using Libraary.Data;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly LibraaryDbContext context;

        public UserService(LibraaryDbContext context)
        {
            this.context = context;
        }

        public string GetFirstName(string email)
        {
            return this.context.Users.SingleOrDefault(user => user.Email == email).FirstName;
        }
    }
}
