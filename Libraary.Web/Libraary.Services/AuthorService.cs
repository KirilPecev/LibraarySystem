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
            var author = GetAuthorByFirstNameAndLastName(authorDto.FirstName, authorDto.LastName);

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

        private Author GetAuthorByFirstNameAndLastName(string firstName, string lastName)
        {
            return this.db
                .Authors
                .SingleOrDefault(a => a.FirstName == firstName
                                      && a.LastName == lastName);
        }

        public IEnumerable<AuthorViewDTO> GetAllByLibraryId(string libraryId)
        {
            var authors = this.db.Libraries.SingleOrDefault(l => l.Id == libraryId)
                ?.LibraryBooks
                .SelectMany(lb => lb.Book.AuthorBooks)
                .Select(ab => ab.Author)
                .Distinct()
                .ToList();

            return authors.Select(ab => new AuthorViewDTO()
            {
                Id = ab.Id,
                Name = ab.ToString(),
                Address = ab.Address.ToString(),
                BooksCount = ab.AuthorBooks.Count()
            })
                .ToList();
        }

        public IEnumerable<string> GetAllAuthorsByName()
        {
            return this.db.Authors.Select(author => author.ToString()).ToList();
        }

        public Author GetAuthorByFullName(string name)
        {
            return this.db.Authors.SingleOrDefault(author => author.ToString() == name);
        }
    }
}
