using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UBC.Core.Domain.Entities;

namespace UBC.Core.Data.Context
{
    public class IdentityContext : IdentityDbContext<UserEntity>
    {
        #region Constructor

        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        #endregion

        #region DBSets

        public DbSet<UserEntity> TbUsers { get; set; }

        #endregion

        #region ModelBuilder

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().ToTable("Users").HasKey(t => t.Id);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.EnableSensitiveDataLogging(false);

        #endregion
    }
}
