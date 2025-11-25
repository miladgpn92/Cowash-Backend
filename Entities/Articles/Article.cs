using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Article : BaseWithSeoEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string DescriptionForEditor { get; set; }
        public string PicUrl { get; set; }
        public string Slug { get; set; }
        public int? ArticleCategoryId { get; set; }
        public ArticleCategory ArticleCategory { get; set; }

        public bool IsPin { get; set; }

        public class ArticleConfig : IEntityTypeConfiguration<Article>
        {
            public void Configure(EntityTypeBuilder<Article> builder)
            {
                builder.Property(a => a.CreatorIP).HasMaxLength(15);
                builder.Property(a => a.Title).HasMaxLength(200);
         
            
                builder.Property(a => a.SeoTitle).HasMaxLength(200);
                builder.HasIndex(a => a.Slug).IsUnique();
                builder.Property(a => a.Slug).HasMaxLength(150);

                builder.HasOne(m => m.ApplicationUser).WithMany(m => m.Articles).HasForeignKey(m => m.CreatorUserId).OnDelete(DeleteBehavior.SetNull);

                builder.HasOne(m => m.ArticleCategory).WithMany(m => m.Articles).HasForeignKey(m => m.ArticleCategoryId).OnDelete(DeleteBehavior.SetNull);
            }
        }



    }
}
