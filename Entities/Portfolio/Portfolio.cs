using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Portfolio : BaseWithSeoEntity
    {
        public string ThumbPicUrl { get; set; }

        public string Title { get; set; }

        public string DateOfWork { get; set; }

        public string Description { get; set; }

        public string ChallengePicUrl { get; set; }

        public string ChallengeDescription { get; set;}

        public string SolutionPicUrl { get; set; }

        public string SolutionDescription { get; set; }

        public string GalleryFilesUrl { get; set; }

        public string Slug { get; set; }

        public bool IsPin { get; set; }

        public int? PortfolioCategoryId { get; set; }
        public virtual PortfolioCategory PortfolioCategory { get; set; }

    }


    public class PortfolioConfig : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.Property(a => a.CreatorIP).HasMaxLength(45);
            builder.HasIndex(a => a.Slug).IsUnique();
            builder.Property(a => a.Slug).HasMaxLength(200).IsRequired();
            builder.HasOne(m => m.ApplicationUser).WithMany().HasForeignKey(m => m.CreatorUserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(m => m.PortfolioCategory).WithMany(m => m.Portfolios).HasForeignKey(m => m.PortfolioCategoryId).OnDelete(DeleteBehavior.SetNull);

        }
    }
}
