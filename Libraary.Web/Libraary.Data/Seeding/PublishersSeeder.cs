namespace Libraary.Data.Seeding
{
    using Common;
    using Domain;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    internal class PublishersSeeder : ISeeder
    {
        public async Task SeedAsync(LibraaryDbContext dbContext, IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService(typeof(LibraaryDbContext)) as LibraaryDbContext;

            await SeedCategoryAsync(context, GlobalConstants.Publishers);
        }

        private static async Task SeedCategoryAsync(LibraaryDbContext context, string[] publishers)
        {
            foreach (var publisher in publishers)
            {
                var urlAddress = publisher.Split('|')[1];
                var name = publisher.Split('|')[0];
                if (!context.Publishers.Any(p => p.Name == name))
                {
                    await context.Publishers.AddAsync(new Publisher()
                    {
                        Name = name,
                        URLAddress = urlAddress
                    });
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
