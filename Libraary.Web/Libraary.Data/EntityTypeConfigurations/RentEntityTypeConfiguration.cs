namespace Libraary.Data.EntityTypeConfigurations
{
    using Libraary.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class RentEntityTypeConfiguration : IEntityTypeConfiguration<Rent>
    {
        public void Configure(EntityTypeBuilder<Rent> builder)
        {
            builder
                 .HasKey(key => new { key.BookId, key.UserId });

            builder
                .HasOne(rent => rent.Book)
                .WithMany(book => book.Rents)
                .HasForeignKey(rent => rent.BookId);

            builder
                .HasOne(rent => rent.User)
                .WithMany(user => user.RentedBooks)
                .HasForeignKey(rent => rent.UserId);
        }
    }
}
