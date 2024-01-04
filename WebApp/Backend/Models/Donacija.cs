namespace Backend.Models;
[Table("Donacija")]
public class Donacija
{
    [Key]
    [Required]
    public int ID { get; set; }
    public int Kolicina { get; set; }
    public Slucaj? Slucaj { get; set; }
    public Korisnik? Korisnik { get; set; }
}