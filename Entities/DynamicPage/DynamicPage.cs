using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DynamicPage : BaseWithSeoEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string DescriptionForEditor { get; set; }
        public string Slug { get; set; }

        public bool IsPin { get; set; }
    }

    public class DynamicPageConfig : IEntityTypeConfiguration<DynamicPage>
    {
        public void Configure(EntityTypeBuilder<DynamicPage> builder)
        {
            builder.Property(a => a.CreatorIP).HasMaxLength(15);
            builder.Property(a => a.Title).HasMaxLength(200);
            builder.Property(a => a.SeoTitle).HasMaxLength(200);
            builder.HasIndex(a => a.Slug).IsUnique();
            builder.Property(a => a.Slug).HasMaxLength(150);

            builder.HasOne(a => a.ApplicationUser).WithMany(a => a.DynamicPages).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
