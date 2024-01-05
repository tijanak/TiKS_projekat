namespace Backend.Models;
[Table("Donacija")]
public class Donacija
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    public int Kolicina { get; set; }
    [JsonIgnore]
    public Slucaj? Slucaj { get; set; }
    [JsonIgnore]
    public Korisnik? Korisnik { get; set; }
}