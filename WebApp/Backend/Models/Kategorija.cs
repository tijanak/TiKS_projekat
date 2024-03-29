namespace Backend.Models;
[Table("Kategorija")]

[Index(nameof(Prioritet), IsUnique = true)]
public class Kategorija
{
    [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    [MaxLength(50)]
    public string? Tip { get; set; }

    public double Prioritet { get; set; }
    [JsonIgnore]
    public List<Slucaj> Slucajevi { get; set; } = new();
}