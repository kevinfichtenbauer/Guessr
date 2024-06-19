using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;

namespace Model.Entities;
public enum ESUBJECTS {SEW,MEDT,INSY,SYTB,SYTE,NWTK,ITP}
[Table("USERS")]
public class User
{
    [Key]
    [Column("UID")]
    public int UID { get; set; }
    [Column("USERNAME", TypeName = "VARCHAR(255)")][Required]
    public string Name { get; set; }
    [Column("HIGHSCORE")]
    public int HighScore { get; set; }
    [Column("TIMESCORE_SEC")]
    public int HighScoreSec { get; set; }
}
public enum EDIFFICULTY{EASY,MEDIUM,HARD}
[Table("WORDS")]
public class Words
{
    [Key]
    [Column("WID"),DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int WID { get; set; }
    [Column("WORD")][Required]
    public string Word { get; set; }
    [Column("DIFFICULTY")][Required]
    public EDIFFICULTY Difficulty { get; set; }
}
public class SEW:Words{ }
public class MEDT:Words{ }
public class INSY:Words{ }
public class SYTB:Words{ }
public class SYTE:Words{ }
public class NWTK:Words{ }
public class ITP:Words{ }

public class GroupedTheme
{
    public string Subject { get; set; }
    public List<Words> Words { get; set; }
}
public enum EGameMode{Normal,Time,none}
public class SharedClass
{
    public User SharedUser { get; set; } = new User(){Name = ""};
    public EGameMode GameMode { get; set; } = EGameMode.none;
}
