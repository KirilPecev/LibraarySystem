namespace Libraary.Services
{
    using Libraary.Domain;
    using System.Collections.Generic;

    public interface ICategoryService
    {
        IEnumerable<string> GetCategories();

        Category GetCategory(string category);
    }
}
