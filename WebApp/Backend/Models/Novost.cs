using System.Text.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models;
[Table("Novost")]
public class Novost
{
    [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    [MaxLength(5000)]
    public string? Tekst { get; set; }
    public DateTime Datum { get; set; }
    public string? Slika { get; set; }
    public Slucaj Slucaj { get; set; } = null!;
}