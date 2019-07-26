namespace Libraary.Data
{
    using Libraary.Data.EntityTypeConfigurations;
    using Libraary.Domain;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class LibraaryDbContext : IdentityDbContext<LibraaryUser, LibraaryRole, string>
    {
        public LibraaryDbContext(DbContextOptions<LibraaryDbContext> options) : base(options) { }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<AuthorBooks> AuthorBooks { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<BookCategory> BookCategories { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Library> Libraries { get; set; }

        public DbSet<LibraryBook> LibraryBooks { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Rent> Rents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BookCategoryEntityTypeConfiguration());
            builder.ApplyConfiguration(new BookLibraryEntityTypeConfiguration());
            builder.ApplyConfiguration(new AuthorBooksEntityTypeConfiguration());
            builder.ApplyConfiguration(new RatingsEntityTypeConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
