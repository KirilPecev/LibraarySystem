namespace Libraary.Services
{
    using Data;
    using Domain;
    using DTOs.Publisher;
    using System.Collections.Generic;
    using System.Linq;

    public class PublisherService : IPublisherService
    {
        private readonly LibraaryDbContext db;

        public PublisherService(LibraaryDbContext db)
        {
            this.db = db;
        }

        public bool Add(AddPublisherDTO dto)
        {
            var publisher = GetPublisher(dto.Name);

            if (publisher == null)
            {
                this.db.Publishers.Add(new Publisher
                {
                    Name = dto.Name,
                    URLAddress = dto.URLAddress,
                });
            }

            int count = this.db.SaveChanges();
            return count != 0;
        }

        public IEnumerable<PublisherViewModelDTO> GetAllByLibraryId(string libraryId)
        {
            return this.db
               .LibraryBooks
               .Where(lb => lb.LibraryId == libraryId)
               .Select(book => new PublisherViewModelDTO
               {
                   Name = book.Book.Publisher.Name,
                   URL = book.Book.Publisher.URLAddress
               })
                 .Distinct()
                 .ToList();
        }

        public IEnumerable<string> GetAllPublishersName()
        {
            return this.db.Publishers.Select(p => p.Name).ToList();
        }

        public Publisher GetPublisher(string name)
        {
            return this.db.Publishers.SingleOrDefault(p => p.Name == name);
        }
    }
}
