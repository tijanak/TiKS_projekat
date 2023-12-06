namespace Backend.Models;
[Table("Testing")]
public class Test
{
    [Key]
    [Required]
    public int ID { get; set; }
    [MaxLength(50)]
    public string? Nebitno { get; set; }
}