using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Faq:BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string CTAText { get; set; }

        public string CTALink { get; set; }

        public bool IsPin { get; set; }
    }

    public class FaqConfig : IEntityTypeConfiguration<Faq>
    {
        public void Configure(EntityTypeBuilder<Faq> builder)
        {
            builder.Property(a => a.CreatorIP).HasMaxLength(45);
            builder.HasOne(m => m.ApplicationUser).WithMany().HasForeignKey(m => m.CreatorUserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
