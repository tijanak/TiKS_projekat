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
        modelBuilder.Entity<Slucaj>().HasOne(t => t.Lokacija).WithOne(t => t.Slucaj);
        modelBuilder.Entity<Lokacija>().HasOne(t => t.Slucaj).WithOne(t => t.Lokacija);
        base.OnModelCreating(modelBuilder);
    }

}