namespace Backend.Models;
[Table("Slucaj")]

public class Slucaj
{
    [Key]
    [Required]
    public int ID { get; set; }
    [MaxLength(50)]
    public string? Naziv { get; set; }
    [MaxLength(5000)]
    public string? Opis { get; set; }
    [MaxLength(200)]
    public string? Slika { get; set; }
    public int LokacijaId { get; set; }
    public Lokacija? Lokacija { get; set; }
    public List<Novost>? Novosti { get; set; }
    public Korisnik? Korisnik { get; set; }
    public List<Kategorija>? Kategorija { get; set; }

    public int ZivotinjaId { get; set; }
    public Zivotinja? Zivotinja { get; set; }
    public List<Trosak>? Troskovi { get; set; }
    public List<Donacija>? Donacije { get; set; }
}