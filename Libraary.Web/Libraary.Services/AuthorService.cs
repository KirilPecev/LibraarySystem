namespace Libraary.Services
{
    using Data;
    using Domain;
    using DTOs.Author;
    using System.Collections.Generic;
    using System.Linq;

    public class AuthorService : IAuthorService
    {
        private readonly LibraaryDbContext db;

        public AuthorService(LibraaryDbContext db)
        {
            this.db = db;
        }

        public bool Add(AddAuthorDTO authorDto)
        {
            this.db.Authors.Add(new Author()
            {
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName,
                Address = new Address()
                {
                    Country = authorDto.Address.Country,
                    Town = authorDto.Address.Town,
                    Street = authorDto.Address.Street,
                    Zip = authorDto.Address.Zip
                }
            });

            int count = this.db.SaveChanges();
            if (count == 0)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<AuthorViewDTO> GetAll()
        {
            return this.db.Authors.Select(author => new AuthorViewDTO
            {
                Id = author.Id,
                Name = author.ToString(),
                Address = author.Address.ToString(),
                BooksCount = author.Books.Count
            })
                 .ToList();
        }

        public Author GetAuthor(string name)
        {
            return this.db.Authors.SingleOrDefault(author => author.ToString() == name);
        }
    }
}
