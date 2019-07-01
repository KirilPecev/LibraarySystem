namespace Libraary.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Libraary.Common;
    using Libraary.Domain;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(LibraaryDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<LibraaryRole>>();

            await SeedRoleAsync(roleManager, GlobalConstants.AdministratorRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.OwnerRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.UserRoleName);
        }

        private static async Task SeedRoleAsync(RoleManager<LibraaryRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new LibraaryRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
