namespace Backend.Models;
[Table("Trosak")]
public class Trosak
{
    [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    [MaxLength(50)]
    public string? Namena { get; set; }
    public int Kolicina { get; set; }
    [JsonIgnore]
    public Slucaj Slucaj { get; set; } = null!;
}