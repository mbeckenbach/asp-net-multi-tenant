using Microsoft.EntityFrameworkCore;
using StarFleetOs.Database.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarFleetOs.Database.App
{
    public class AppDbContext : DbContext
    {
        #region Constructor

        public AppDbContext(
            DbContextOptions<AppDbContext> options
        )
            : base(options)
        {
        }

        #endregion

        #region Entity DbSets
        public DbSet<CrewMember> CrewMembers { get; set; }

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
            modelBuilder.HasDefaultSchema("app");

            // Apply entity configurations
            // Customizations must go after base.OnModelCreating(builder)
            modelBuilder.ApplyConfiguration(new CrewMemberConfig());
        }

        #endregion
    }
}
