namespace Backend.Models;

public class TestContext : DbContext
{
    public DbSet<Test> Testovi { get; set; }
    public DbSet<Korisnik> Korisnici { get; set; }

    public TestContext(DbContextOptions options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}