namespace Libraary.Data.Seeding
{
    using Common;
    using Domain;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    internal class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(LibraaryDbContext dbContext, IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService(typeof(LibraaryDbContext)) as LibraaryDbContext;

            await SeedCategoryAsync(context, GlobalConstants.BookCategories);
        }

        private static async Task SeedCategoryAsync(LibraaryDbContext context, string[] categories)
        {
            var result = false;
            foreach (var category in categories)
            {
                if (!context.Categories.Any(c => c.CategoryName == category))
                {
                    await context.Categories.AddAsync(new Category() { CategoryName = category });
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
