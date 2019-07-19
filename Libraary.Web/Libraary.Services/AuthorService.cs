namespace Libraary.Services
{
    using Data;
    using Domain;
    using DTOs.Author;
    using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<AuthorViewDTO> GetAllByLibraryId(string libraryId)
        {
            var booksWithAuthor = this.db
                .LibraryBooks
                .Where(lb => lb.LibraryId == libraryId)
                .Include(x => x.Book)
                .ThenInclude(x => x.Author)
                .Select(book => new AuthorViewDTO
                {
                    Id = book.Book.Author.Id,
                    Name = book.Book.Author.ToString(),
                    Address = book.Book.Author.Address.ToString(),
                    BooksCount = book.Book.Author.Books.Count
                });

            return booksWithAuthor.GroupBy(author => author.Name).Select(x=>x.First());
        }

        public IEnumerable<string> GetAllAuthorsName()
        {
            return this.db.Authors.Select(author => author.ToString()).ToList();
        }

        public Author GetAuthor(string name)
        {
            return this.db.Authors.SingleOrDefault(author => author.ToString() == name);
        }
    }
}
