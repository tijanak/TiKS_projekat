namespace Backend.Models;
[Table("Trosak")]
public class Trosak
{
    [Key]
    [Required]
    public int ID { get; set; }
    [MaxLength(50)]
    public string? Namena { get; set; }
    public int Kolicina { get; set; }
    public Slucaj? Slucaj { get; set; }
}