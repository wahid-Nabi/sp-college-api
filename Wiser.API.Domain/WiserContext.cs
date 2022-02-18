using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wiser.API.Entities.Models;

namespace Wiser.API.Entities
{
    public class WiserContext : IdentityDbContext<SystemUser, SystemRole, string,
     IdentityUserClaim<string>, SystemUserRole, IdentityUserLogin<string>,
     IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public WiserContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IBaseModel).IsAssignableFrom(type.ClrType))
                    modelBuilder.SetSoftDeleteFilter(type.ClrType);
            }

            modelBuilder.Entity<Department>()
                .Property(x => x.Enabled)
                .HasDefaultValue(false);

            modelBuilder.Entity<Department>()
               .HasOne(c => c.DepartmentCategory)
               .WithMany(m => m.Departments)
               .HasForeignKey(f => f.DepartmentCategoryId);

            modelBuilder.Entity<DepartmentUserMapping>()
               .HasOne(c => c.Department)
               .WithMany(m => m.DepartmentUserMappings)
               .HasForeignKey(f => f.DepartmentId);

            modelBuilder.Entity<DepartmentUserMapping>()
              .HasOne(c => c.SystemUser)
              .WithMany(m => m.DepartmentUserMappings)
              .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<NewsNotification>()
             .HasOne(c => c.Department)
             .WithMany(m => m.NewsNotifications)
             .HasForeignKey(f => f.DepartmentId);

            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;
                    case EntityState.Deleted:  // remove
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }
        }

        public DbSet<Institute> Institutes { get; set; }
        public DbSet<NewsNotification> NewsNotifications { get; set; }
        public DbSet<DepartmentCategory> DepartmentCategory { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<DepartmentUserMapping> DepartmentUserMapping { get; set; }
        public DbSet<DepartmentSection> DeparmentSection { get; set; }
    }
}
