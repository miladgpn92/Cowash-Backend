using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PortfolioCategory : BaseWithSeoEntity
    {
        public string Title { get; set; }
        public string Slug { get; set; }

        public bool IsPin { get; set; }

        public virtual ICollection<Portfolio> Portfolios { get; set; }
    }

    public class PortfolioCategoryConfiguration : IEntityTypeConfiguration<PortfolioCategory>
    {
        public void Configure(EntityTypeBuilder<PortfolioCategory> builder)
        {
            builder.Property(a => a.Title).HasMaxLength(150).IsRequired();
            builder.Property(a => a.CreatorIP).HasMaxLength(45);
            builder.Property(a => a.SeoTitle).HasMaxLength(150);
            builder.HasIndex(a => a.Slug).IsUnique();
            builder.Property(a => a.Slug).HasMaxLength(150).IsRequired();
            builder.HasOne(m => m.ApplicationUser).WithMany().HasForeignKey(m => m.CreatorUserId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
