namespace Backend.Models;

public class ProjectContext : DbContext
{
    public virtual DbSet<Slucaj> Slucajevi { get; set; }
    public virtual DbSet<Lokacija> Lokacije { get; set; }
    public virtual DbSet<Kategorija> Kategorije { get; set; }
    public virtual DbSet<Donacija> Donacije { get; set; }

    public virtual DbSet<Korisnik> Korisnici { get; set; }
    public virtual DbSet<Novost> Novosti { get; set; }
    public virtual DbSet<Trosak> Troskovi { get; set; }
    public virtual DbSet<Zivotinja> Zivotinje { get; set; }

    public ProjectContext(DbContextOptions options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Korisnik>()
                .HasMany(c => c.Slucajevi)
                .WithOne(x => x.Korisnik).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Korisnik>().HasMany(k => k.Donacije).WithOne(d => d.Korisnik).OnDelete(DeleteBehavior.NoAction);
    }

}