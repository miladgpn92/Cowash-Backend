using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ContactUs
{
    public class ContactUs:BaseEntity
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string MessageText { get; set; }
        public ContactUsState ContactUsState { get; set; }
    }

    public class ContactUsConfiguration : IEntityTypeConfiguration<ContactUs>
    {
        public void Configure(EntityTypeBuilder<ContactUs> builder)
        {
            builder.Property(a => a.FullName).HasMaxLength(70);
            builder.Property(a => a.PhoneNumber).HasMaxLength(11);
            builder.Property(a => a.MessageText).HasMaxLength(500);
            builder.HasOne(a => a.ApplicationUser).WithMany(a=> a.ContactUses).OnDelete(DeleteBehavior.SetNull);

        }
    }

    public enum ContactUsState
    {
        seen,
        unseen
    }

}
