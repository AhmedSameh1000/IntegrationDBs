using AutoRepairPro.Data.Models;
using Integration.data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace Integration.data.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public DbSet<FromDb> fromDbs { get; set; }
        public DbSet<ToDb> toDbs { get; set; }
        public DbSet<TableFrom>  tableFroms { get; set; }
        public DbSet<TableTo> tableTos { get; set; }

        public DbSet<Models.Module> modules { get; set; }
        public DbSet<ColumnFrom> columnFroms { get; set; }
        public DbSet<ColumnTo>  columnTos{ get; set; }
        public DbSet<ConditionFrom>  conditionFroms { get; set; }
        public DbSet<ConditionTo>  ConditionTos{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure one-to-one relationship between TableFrom and TableTo
            modelBuilder.Entity<TableFrom>()
                .HasOne(t => t.TableTo)
                .WithOne(t => t.TableFrom)
                .HasForeignKey<TableFrom>(t => t.TableToId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TableTo>()
                .HasOne(t => t.TableFrom)
                .WithOne(t => t.TableTo)
                .HasForeignKey<TableTo>(t => t.TableFromId)
                .OnDelete(DeleteBehavior.Restrict);

            // Apply configurations from assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

    }
}

