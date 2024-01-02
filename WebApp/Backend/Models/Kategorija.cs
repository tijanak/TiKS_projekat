namespace Backend.Models;
[Table("Kategorija")]
public class Kategorija
{
    [Key]
    [Required]
    public int ID { get; set; }
    [MaxLength(50)]
    public string? Tip { get; set; }
    public List<Slucaj>? Slucajevi { get; set; }
}