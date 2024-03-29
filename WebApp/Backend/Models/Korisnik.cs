namespace Backend.Models;
[Table("Korisnik")]
public class Korisnik
{
    [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    [MaxLength(50)]
    public string? Username { get; set; }
    [MaxLength(50)]
    public string? Password { get; set; }
    [JsonIgnore]
    public List<Slucaj> Slucajevi { get; set; } = new();
    [JsonIgnore]
    public List<Donacija> Donacije { get; set; } = new();
}