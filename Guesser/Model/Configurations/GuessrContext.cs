using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Model.Configurations;

public class GuessrContext:DbContext
{
    public DbSet<User> Users { get; set; }
    public List<User> ListUsers { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Words>()
            .HasDiscriminator<string>("SUBJECT")
            .HasValue<SEW>("SEW")
            .HasValue<MEDT>("MEDT")
            .HasValue<INSY>("INSY")
            .HasValue<SYTB>("SYTB")
            .HasValue<SYTE>("SYTE")
            .HasValue<NWTK>("NWTK")
            .HasValue<ITP>("ITP");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        
        // Verbindungszeichenfolge für MySQL (passen Sie diese an Ihre DB-Details an)
        optionsBuilder.UseMySql(
            "Server=localhost;Database=CDP;User=root;Password=123mysql;",
            new MySqlServerVersion(new Version(8, 0, 29))
        );
    }

    public List<User> GetUsers()
    {
        return Users.ToList();
    }
}