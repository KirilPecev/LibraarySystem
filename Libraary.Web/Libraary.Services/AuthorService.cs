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
            var author = this.db
                .Authors
                .SingleOrDefault(a => a.FirstName == authorDto.FirstName
                && a.LastName == authorDto.LastName);

            if (author == null)
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
            }

            int count = this.db.SaveChanges();

            return count != 0;
        }

        public IEnumerable<AuthorViewDTO> GetAll(string libraryId)
        {
            return this.db
                .Libraries
                .SingleOrDefault(lib => lib.Id == libraryId)
                .Authors
                .Select(author => new AuthorViewDTO
                {
                    Id = author.Id,
                    Name = author.ToString(),
                    Address = author.Address.ToString(),
                    BooksCount = author.Books.Count
                }).ToList();
        }

        public Author GetAuthor(string name)
        {
            return this.db.Authors.SingleOrDefault(author => author.ToString() == name);
        }
    }
}
