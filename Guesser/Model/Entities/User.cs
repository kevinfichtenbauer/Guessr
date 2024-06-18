using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Model.Entities;
public enum ESUBJECTS {SEW,MEDT,INSY,SYTB,SYTE,NWTK,ITP}
[Table("USERS")]
public class User
{
    [Key]
    [Column("UID")]
    public int UID { get; set; }
    [Column("USERNAME", TypeName = "VARCHAR(255)")]
    public string Name { get; set; }
    [Column("HIGHSCORE")]
    public int HighScore { get; set; }
}

[Table("WORDS")]
public class Words
{
    [Key]
    [Column("WID"),DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int WID { get; set; }
    [Column("WORD")]
    public string Word { get; set; }
}
public class SEW:Words{ }
public class MEDT:Words{ }
public class INSY:Words{ }
public class SYTB:Words{ }
public class SYTE:Words{ }
public class NWTK:Words{ }
public class ITP:Words{ }
