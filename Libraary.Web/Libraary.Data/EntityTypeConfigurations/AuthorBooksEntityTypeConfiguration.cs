namespace Libraary.Data.EntityTypeConfigurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class AuthorBooksEntityTypeConfiguration : IEntityTypeConfiguration<AuthorBooks>
    {
        public void Configure(EntityTypeBuilder<AuthorBooks> builder)
        {
            builder
                .HasKey(key => new { key.AuthorId, key.BookId });

            builder
                .HasOne(ab => ab.Author)
                .WithMany(a => a.AuthorBooks)
                .HasForeignKey(ab => ab.AuthorId);

            builder
                .HasOne(ab => ab.Book)
                .WithMany(a => a.AuthorBooks)
                .HasForeignKey(ab => ab.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
