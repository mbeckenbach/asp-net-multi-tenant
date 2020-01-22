using Microsoft.EntityFrameworkCore;
using StarFleetOs.Database.Tenants.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarFleetOs.Database.Tenants
{
    public class TenantsDbContext : DbContext
    {
        #region Constructor

        public TenantsDbContext(
            DbContextOptions<TenantsDbContext> options
        )
            : base(options)
        {
        }

        #endregion

        #region Entity DbSets

        public DbSet<AppTenant> AppTenants { get; set; }

        #endregion

        #region Model configuration

        /// <summary>
        /// Configures entities
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set table schema
            modelBuilder.HasDefaultSchema("tnt");

            // Apply entity configurations
            // Customizations must go after base.OnModelCreating(builder)
            modelBuilder.ApplyConfiguration(new AppTenantConfig());
        }

        #endregion
    }
}
