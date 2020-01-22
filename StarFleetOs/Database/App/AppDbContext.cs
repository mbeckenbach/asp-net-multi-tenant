using Microsoft.EntityFrameworkCore;
using StarFleetOs.Database.App.Models;
using StarFleetOs.Database.Tenants.Models;
using StarFleetOs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace StarFleetOs.Database.App
{
    public class AppDbContext : DbContext
    {
        private readonly IEntityTypeProvider entityTypeProvider;
        private readonly AppTenant tenant;

        #region Constructor

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            IEntityTypeProvider entityTypeProvider,
            AppTenant tenant
        )
            : base(options)
        {
            this.entityTypeProvider = entityTypeProvider;
            this.tenant = tenant;
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

            // Adds global query filter methods to entities
            foreach (var type in entityTypeProvider.GetEntityTypes())
            {
                var method = SetGlobalQueryMethod.MakeGenericMethod(type);
                method.Invoke(this, new object[] { modelBuilder });
            }
        }

        #endregion

        #region MultiTenant Query Filter

        /// <summary>
        /// Creates a global query filter method
        /// </summary>
        private static readonly MethodInfo SetGlobalQueryMethod = typeof(AppDbContext)
            .GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Single(t => t.IsGenericMethod && t.Name == nameof(SetGlobalQuery));

        /// <summary>
        /// Applies global query filters to tenant entities
        /// </summary>
        /// <param name="builder"></param>
        /// <typeparam name="T"></typeparam>
        // ReSharper disable once MemberCanBePrivate.Global
        public void SetGlobalQuery<T>(ModelBuilder builder) where T : TenantEntity
        {
            //Debug.WriteLine("Adding global query for: " + typeof(T));
            builder.Entity<T>().HasQueryFilter(e => e.TenantId == tenant.Id);
        }

        #endregion
    }
}
