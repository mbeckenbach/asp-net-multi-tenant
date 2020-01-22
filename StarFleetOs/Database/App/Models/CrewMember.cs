using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarFleetOs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarFleetOs.Database.App.Models
{
    public class CrewMemberConfig : IEntityTypeConfiguration<CrewMember>
    {
        public void Configure(EntityTypeBuilder<CrewMember> builder)
        {
            // Primary key
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("newid()");

            // Entity specific
            builder.Property(x => x.Name).HasMaxLength(80).IsRequired();
        }
    }

    public class CrewMember : TenantEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Rank { get; set; }
    }
}
