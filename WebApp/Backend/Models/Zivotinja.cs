namespace Backend.Models;
[Table("Zivotinja")]
public class Zivotinja
{
    [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    [MaxLength(50)]
    public string? Ime { get; set; }
    [MaxLength(50)]
    public string? Vrsta { get; set; }
    public Slucaj Slucaj { get; set; } = null!;
}