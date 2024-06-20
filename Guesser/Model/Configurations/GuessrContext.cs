using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Model.Configurations;

public class GuessrContext:DbContext
{
    public GuessrContext():base(){}
    public GuessrContext(DbContextOptions<GuessrContext> context) : base(context){}
    public DbSet<User> Users { get; set; }
    public DbSet<Words> DBWords { get; set; }
    /*public List<string> Subjects { get; set; }
    public List<Words> WordList { get; set; }*/
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
        builder.Entity<Words>()
            .Property(s => s.Difficulty)
            .HasConversion<string>();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        
        // Verbindungszeichenfolge für MySQL (passen Sie diese an Ihre DB-Details an)
        optionsBuilder.UseMySql(
            "Server=localhost;Database=CDP;User=root;Password=123mysql;",
            new MySqlServerVersion(new Version(8, 0, 29))
        );
    }
    public IQueryable<GroupedTheme> GetThemesSeperated()
    {
        IQueryable<GroupedTheme> groupedThemes =
            this.DBWords
                .GroupBy(a => EF.Property<string>(a, "SUBJECT"))
                .Select(s => new GroupedTheme
                {
                    Subject = s.Key,
                    Words = s.ToList()
                });
        return groupedThemes;
    }
    public List<Words> GetWords(string subject)
    {
        return this.DBWords.Where(s => EF.Property<string>(s, "SUBJECT") == subject).ToList();
    }

    public List<User> GetUsers()
    {
        List<User> _users = new List<User>();
        foreach (var x in Users)
        {
            _users.Add(x);
        }
        return _users;
    }
    public void SaveNormalScore(User user)
    {
        var existingUser = Users.FirstOrDefault(u => u.Name == user.Name);

        if (existingUser != null)
        {
            if (existingUser.HighScore < user.HighScore)
            {
                existingUser.HighScore = user.HighScore;
            }
        }
        else
        {
            Users.Add(new User() { Name = user.Name, HighScore = user.HighScore });
        }

        this.SaveChanges();
    }
    public void SaveTimeScore(User user)
    {
        var existingUser = Users.FirstOrDefault(u => u.Name == user.Name);

        if (existingUser != null)
        {
            if (existingUser.TimeHighScore < user.TimeHighScore || existingUser.HighScoreSec < user.HighScoreSec)
            {
                existingUser.TimeHighScore = user.TimeHighScore;
                existingUser.HighScoreSec = user.HighScoreSec;
            }
        }
        else
        {
            Users.Add(new User() { Name = user.Name, HighScore = user.HighScore });
        }

        this.SaveChanges();
    }
}