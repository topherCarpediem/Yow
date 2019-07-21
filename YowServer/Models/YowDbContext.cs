using Microsoft.EntityFrameworkCore;

namespace Yow.YowServer.Models
{
    public class YowDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<KeyVault> KeyVaults { get; set; }

        public YowDbContext(DbContextOptions<YowDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne<KeyVault>(u => u.KeyVault)
                      .WithOne(kv => kv.User)
                      .HasForeignKey<KeyVault>(kv => kv.UserId);
            });
        }

    }
}