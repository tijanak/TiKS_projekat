namespace Backend.Models;
[Table("Slucaj")]

public class Slucaj
{
    public Slucaj()
    {
        Donacije = new();
        Troskovi = new();
        Kategorija = new();
        Novosti = new();
        Slike = new();
    }
    [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    [MaxLength(50)]
    public string? Naziv { get; set; }
    [MaxLength(500)]
    public string? Opis { get; set; }
    public List<string> Slike { get; set; }
    [JsonIgnore]
    public int? LokacijaId { get; set; }
    [JsonIgnore]
    public Lokacija? Lokacija { get; set; }
    [JsonIgnore]
    public List<Novost> Novosti { get; set; }
    public Korisnik Korisnik { get; set; } = null!;
    [JsonIgnore]
    public List<Kategorija> Kategorija { get; set; }
    [JsonIgnore]
    public int? ZivotinjaId { get; set; }
    [JsonIgnore]
    public Zivotinja? Zivotinja { get; set; }
    [JsonIgnore]
    public List<Trosak> Troskovi { get; set; }
    [JsonIgnore]
    public List<Donacija> Donacije { get; set; }
}