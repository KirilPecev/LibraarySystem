namespace Libraary.Data.Seeding
{
    using Common;
    using Domain;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    internal class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(LibraaryDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<LibraaryUser>>();

            await SeedUserAsync(userManager,
                GlobalConstants.AdministratorEmail,
                GlobalConstants.AdministratorPassword,
                GlobalConstants.AdministratorFirstName,
                GlobalConstants.AdministratorLastName,
                GlobalConstants.AdministratorRoleName);
        }

        private static async Task SeedUserAsync(UserManager<LibraaryUser> userManager, string email, string password, string firsName, string lastName, string roleName)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                var newUser = new LibraaryUser { UserName = email, Email = email, FirstName = firsName, LastName = lastName, EmailConfirmed = true};
                await userManager.UpdateSecurityStampAsync(newUser);
                var result = await userManager.CreateAsync(newUser, password);

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(newUser,
                        roleName).Wait();
                }

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
