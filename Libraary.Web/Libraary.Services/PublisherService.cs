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
                    PhoneNumber = dto.PhoneNumber,
                    URLAddress = dto.URLAddress,
                    Address = new Address()
                    {
                        Country = dto.Address.Country,
                        Town = dto.Address.Town,
                        Street = dto.Address.Street,
                        Zip = dto.Address.Zip
                    }
                });
            }

            int count = this.db.SaveChanges();
            return count != 0;
        }

        public IEnumerable<PublisherViewModelDTO> GetAll()
        {
            return this.db
                 .Publishers
                 .Select(p => new PublisherViewModelDTO
                 {
                     Name = p.Name,
                     Phone = p.PhoneNumber,
                     URL = p.URLAddress
                 })
                 .ToList();
        }

        public Publisher GetPublisher(string name)
        {
            return this.db.Publishers.SingleOrDefault(p => p.Name == name);
        }
    }
}
