namespace Backend.Models;

public class ProjectContext : DbContext
{
    public DbSet<Korisnik> Korisnici { get; set; }
    public DbSet<Slucaj> Slucajevi { get; set; }
    public DbSet<Lokacija> Lokacije { get; set; }
    public DbSet<Kategorija> Kategorije { get; set; }

    public DbSet<Donacija> Donacije { get; set; }
    public ProjectContext(DbContextOptions options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}