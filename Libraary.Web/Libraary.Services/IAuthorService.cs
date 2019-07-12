namespace Libraary.Services
{
    using Libraary.Domain;

    public interface IAuthorService
    {
        Author GetAuthor(string name);
    }
}
