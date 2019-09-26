using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataModel>().HasKey(x => x.Id);
            modelBuilder.Entity<LoggedUser>().HasKey(x => x.Id);
            modelBuilder.Entity<Manufacturer>().HasKey(x => x.Id);
            //One to One
            modelBuilder.Entity<DataModel>()
                .HasOne(x => x.Manufacturer)
                .WithOne(x => x.DataModel)
                .HasForeignKey<Manufacturer>(x => x.DataModelPKey)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            //One to Many
            modelBuilder.Entity<LoggedUser>()
                .HasOne(x => x.DataModel)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.DataModelPKey)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<DataModel> PcData { get; set; }
    }
}