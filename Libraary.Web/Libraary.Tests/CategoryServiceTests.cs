namespace Libraary.Tests
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Common;
    using Services;
    using Services.DTOs.Publisher;
    using Xunit;

    public class CategoryServiceTests
    {
        private ICategoryService categoryService;

        [Fact]
        public void GetCategories_ShouldReturnAllCategories()
        {
            string errorMessagePrefix = "CategoryService GetCategories() method does not work properly.";

            var context = LibraaryDbContextInMemoryFactory.InitializeContext();
            this.categoryService = new CategoryService(context);
            LibraaryDbContextInMemoryFactory.SeedDb(context);

            var categories = this.categoryService.GetCategories();
            Assert.True(categories.Count() != 0, errorMessagePrefix);
        }

        [Fact]
        public void GetCategory_WithCorrectData_ShouldReturnCategory()
        {
            string errorMessagePrefix = "CategoryService GetCategory() method does not work properly.";

            var context = LibraaryDbContextInMemoryFactory.InitializeContext();
            this.categoryService = new CategoryService(context);
            LibraaryDbContextInMemoryFactory.SeedDb(context);

            var category = this.categoryService.GetCategory("Fantasy");
            Assert.True(category != null, errorMessagePrefix);
        }

        [Fact]
        public void GetCategory_WithIncorrectData_ShouldReturnCategory()
        {
            string errorMessagePrefix = "CategoryService GetCategory() method does not work properly.";

            var context = LibraaryDbContextInMemoryFactory.InitializeContext();
            this.categoryService = new CategoryService(context);

            var category = this.categoryService.GetCategory("Fantasyss");
            Assert.True(category == null, errorMessagePrefix);
        }
    }
}
