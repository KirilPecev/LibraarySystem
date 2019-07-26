namespace Libraary.Data.EntityTypeConfigurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class RatingsEntityTypeConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasOne(b => b.Book)
                .WithOne(r => r.Rating)
                .HasForeignKey<Rating>(r => r.BookId);
        }
    }
}
