using System;
using System.Threading;
using System.Threading.Tasks;
using DOCOsoft.UserManagement.Domain.Common;
using DOCOsoft.UserManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DOCOsoft.UserManagement.Persistence
{
    public class DocoSoftDbContext : DbContext
    {
        public DocoSoftDbContext(DbContextOptions<DocoSoftDbContext> options)
           : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DocoSoftDbContext).Assembly);
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = Guid.Parse("{7A3786D1-5DBF-479E-96C7-1252A55BECE3}"),
                FirstName = "Andrii",
                LastName = "Tkach",
                Email = "andtkach@gmail.com"
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
