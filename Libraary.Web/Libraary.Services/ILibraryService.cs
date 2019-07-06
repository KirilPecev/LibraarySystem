namespace Libraary.Services
{
    using DTOs.Library;

    public interface ILibraryService
    {
        string Add(AddLibraryDTO library);

        bool AddOwner(OwnerDTO dto);
    }
}
