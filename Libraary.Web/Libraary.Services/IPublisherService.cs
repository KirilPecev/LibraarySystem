namespace Libraary.Services
{
    using Domain;
    using DTOs.Publisher;
    using System.Collections.Generic;

    public interface IPublisherService
    {
        bool Add(AddPublisherDTO dto);

        Publisher GetPublisher(string name);

        IEnumerable<PublisherViewModelDTO> GetAllByLibraryId(string libraryId);

        IEnumerable<string> GetAllPublishersName();
    }
}
