namespace Libraary.Services
{
    using System.Collections.Generic;

    public interface ICategoryService
    {
        IEnumerable<string> GetCategories();
    }
}
