namespace Libraary.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(LibraaryDbContext dbContext, IServiceProvider serviceProvider);
    }
}
