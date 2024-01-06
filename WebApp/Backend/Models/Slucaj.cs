namespace Backend.Models;
[Table("Slucaj")]

public class Slucaj
{
<<<<<<< Updated upstream
    public Slucaj()
    {
        Donacije = new();
        Troskovi = new();
        Kategorija = new();
        Novosti = new();
        Slike = new();
    }
    [Key]
    [Required]
=======
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
>>>>>>> Stashed changes
    public int ID { get; set; }
    [MaxLength(50)]
    public string? Naziv { get; set; }
    [MaxLength(500)]
    public string? Opis { get; set; }
    public List<string> Slike { get; set; }
    public int LokacijaId { get; set; }
    [JsonIgnore]
    public Lokacija? Lokacija { get; set; }
    [JsonIgnore]
    public List<Novost> Novosti { get; set; }
    [JsonIgnore]
    public Korisnik? Korisnik { get; set; }
    [JsonIgnore]
    public List<Kategorija> Kategorija { get; set; }

    public int ZivotinjaId { get; set; }
    public Zivotinja? Zivotinja { get; set; }
    [JsonIgnore]
    public List<Trosak> Troskovi { get; set; }
    [JsonIgnore]
    public List<Donacija> Donacije { get; set; }
}