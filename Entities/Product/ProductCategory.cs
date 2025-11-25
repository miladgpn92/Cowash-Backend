using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Entities
{
    public class ProductCategory : BaseWithSeoEntity
    {
        public string Title { get; set; }

        public string Slug { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }

    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.Property(a => a.Title).HasMaxLength(150).IsRequired();
            builder.Property(a => a.Slug).HasMaxLength(150).IsRequired();
            builder.HasIndex(a => a.Slug).IsUnique();
            builder.Property(a => a.CreatorIP).HasMaxLength(45);
            builder.Property(a => a.SeoTitle).HasMaxLength(150);
            builder.HasOne(a => a.ApplicationUser)
                .WithMany()
                .HasForeignKey(a => a.CreatorUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
