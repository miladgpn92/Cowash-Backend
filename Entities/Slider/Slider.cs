using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Slider:BaseEntity
    {
        public string Title { get; set; }

        public string TopTitle { get; set; }

        public string Description { get; set; }

        public string PicUrl { get; set; }
        public string Link { get; set; }

        public bool IsActive { get; set; }

    }

    public class SliderConfiguration : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.Property(a => a.Title).HasMaxLength(200);
            builder.HasOne(a => a.ApplicationUser).WithMany(a => a.Slideres).OnDelete(DeleteBehavior.SetNull);
        }
    }

}
