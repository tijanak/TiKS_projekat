namespace Backend.Models;
[Table("Zivotinja")]
public class Zivotinja
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    [MaxLength(50)]
    public string? Ime { get; set; }
    [MaxLength(50)]
    public string? Vrsta { get; set; }
    [JsonIgnore]
    public Slucaj Slucaj { get; set; } = null!;
}