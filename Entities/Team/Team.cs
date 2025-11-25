using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Team:BaseEntity
    {
        public string FullName { get; set; }
        public string JobPosition { get; set; }
        public string PicUrl { get; set; }
        public bool IsPin { get; set; }
    }

    public class TeamConfig : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.Property(a => a.CreatorIP).HasMaxLength(45);
            builder.HasOne(m => m.ApplicationUser).WithMany().HasForeignKey(m => m.CreatorUserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
