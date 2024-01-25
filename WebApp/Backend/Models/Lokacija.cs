namespace Backend.Models;
[Table("Lokacija")]
public class Lokacija
{
    [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public Slucaj Slucaj { get; set; } = null!;

}
