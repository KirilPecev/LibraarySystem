namespace Libraary.Data.EntityTypeConfigurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BookLibraryEntityTypeConfiguration : IEntityTypeConfiguration<LibraryBook>
    {
        public void Configure(EntityTypeBuilder<LibraryBook> builder)
        {
            builder
                .HasKey(key => new { key.LibraryId, key.BookId });

            builder
                .HasOne(bc => bc.Book)
                .WithMany(b => b.LibraryBooks)
                .HasForeignKey(bc => bc.BookId);

            builder
                .HasOne(bc => bc.Library)
                .WithMany(c => c.LibraryBooks)
                .HasForeignKey(bc => bc.LibraryId);
        }
    }
}
