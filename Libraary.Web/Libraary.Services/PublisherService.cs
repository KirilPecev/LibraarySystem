﻿namespace Libraary.Services
{
    using Data;
    using Domain;
    using DTOs.Publisher;
    using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<PublisherViewModelDTO> GetAllByLibraryId(string libraryId)
        {
            return this.db
               .LibraryBooks
               .Where(lb => lb.LibraryId == libraryId)
               .Include(x => x.Book)
               .ThenInclude(x => x.Publisher)
                .Select(book => new PublisherViewModelDTO
                {
                    Name = book.Book.Publisher.Name,
                    Phone = book.Book.Publisher.PhoneNumber,
                    URL = book.Book.Publisher.URLAddress
                })
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