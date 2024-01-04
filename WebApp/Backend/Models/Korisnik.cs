namespace Backend.Models;
[Table("Korisnik")]
public class Korisnik
{
    [Key]
    [Required]
    public int ID { get; set; }
    [MaxLength(50)]
    public string? Username { get; set; }
    [MaxLength(50)]
    public string? Password { get; set; }
    public List<Slucaj>? Slucajevi { get; set; }
    public List<Donacija>? Donacije { get; set; }
}