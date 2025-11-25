using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities
{
    public class Product : BaseWithSeoEntity
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string ProductCode { get; set; }
        public string Dimensions { get; set; }
        public string GalleryFilesUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Description { get; set; }
        public int? ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(a => a.Title).HasMaxLength(200).IsRequired();
            builder.Property(a => a.Slug).HasMaxLength(200).IsRequired();
            builder.HasIndex(a => a.Slug).IsUnique();
            builder.Property(a => a.ProductCode).HasMaxLength(150);
            builder.Property(a => a.Dimensions).HasMaxLength(200);
            builder.Property(a => a.GalleryFilesUrl).HasMaxLength(2000);
            builder.Property(a => a.ThumbnailUrl).HasMaxLength(500);
            builder.Property(a => a.CreatorIP).HasMaxLength(45);
            builder.Property(a => a.SeoTitle).HasMaxLength(150);

            builder.HasOne(a => a.ApplicationUser)
                .WithMany()
                .HasForeignKey(a => a.CreatorUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.ProductCategory)
                .WithMany(a => a.Products)
                .HasForeignKey(a => a.ProductCategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
