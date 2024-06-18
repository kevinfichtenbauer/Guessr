using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Model.Configurations;

public class GuessrContext:DbContext
{
    public GuessrContext(DbContextOptions<GuessrContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
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
}