using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace StarFleetOs.Database.Tenants.Models
{
    public class AppTenantConfig : IEntityTypeConfiguration<AppTenant>
    {
        public void Configure(EntityTypeBuilder<AppTenant> builder)
        {
            // Primary key
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("newid()");

            // Entity specific
            builder.Property(x => x.Identifier).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(80).IsRequired();
            builder.Property(x => x.Domain).HasMaxLength(255).IsRequired();
        }
    }

    public class AppTenant
    {
        public Guid Id { get; set; }

        public string Identifier { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
    }
}
