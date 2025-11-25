using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace Entities
{
    public class ArticleCategory : BaseWithSeoEntity
    {
        public string Title { get; set; }



        public string Slug { get; set; }

        public bool IsPin { get; set; }

        public virtual ICollection<Article> Articles { get; set; }


    }

    public class ArticleCategoryConfiguration : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder.Property(a => a.Title).HasMaxLength(150);
            builder.Property(a => a.CreatorIP).HasMaxLength(45);
            builder.Property(a => a.SeoTitle).HasMaxLength(150);
            builder.HasIndex(a => a.Slug).IsUnique();
            builder.Property(a => a.Slug).HasMaxLength(150);
            builder.HasOne(m => m.ApplicationUser).WithMany(m => m.ArticleCategory).HasForeignKey(m => m.CreatorUserId).OnDelete(DeleteBehavior.SetNull);

        }
    }
}
