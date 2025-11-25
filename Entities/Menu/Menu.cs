using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Menu : BaseEntity
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }

    }

    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.Property(a => a.Title).HasMaxLength(200);
            builder.HasIndex(a => a.Slug).IsUnique();
            builder.Property(a => a.Slug).HasMaxLength(150);

            builder.HasOne(a => a.ApplicationUser).WithMany(a => a.Menus).OnDelete(DeleteBehavior.SetNull);
        }
    }

}
