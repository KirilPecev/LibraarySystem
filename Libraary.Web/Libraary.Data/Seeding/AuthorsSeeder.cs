namespace Libraary.Data.Seeding
{
    using Common;
    using Domain;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    internal class AuthorsSeeder : ISeeder
    {
        public async Task SeedAsync(LibraaryDbContext dbContext, IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService(typeof(LibraaryDbContext)) as LibraaryDbContext;

            await SeedCategoryAsync(context, GlobalConstants.Authors);
        }

        private static async Task SeedCategoryAsync(LibraaryDbContext context, string[] authors)
        {
            foreach (var author in authors)
            {
                var firstName = author.Split()[0];
                var lastName = author.Split()[1];
                var nationality = author.Split()[2];

                if (!context.Authors.Any(a => a.FirstName == firstName && a.LastName == lastName))
                {
                    await context.Authors.AddAsync(new Author()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Nationality = nationality
                    });
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
