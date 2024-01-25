namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class SlucajController : ControllerBase
{
    public ProjectContext Context { get; set; }
    public SlucajController(ProjectContext context)
    {
        Context = context;
    }
    [Route("Get/{id}")]
    [HttpGet]
    public async Task<ActionResult> Preuzmi(int id)
    {
        if (id < 0) return BadRequest("ID ne može biti negativan");
        try
        {
            var slucaj = await Context.Slucajevi.Where(p => p.ID == id).Include(s=>s.Novosti).Include(s=>s.Donacije).Include(s=>s.Troskovi).Include(s=>s.Kategorija).FirstOrDefaultAsync();
            if (slucaj != null)
            {
                return Ok(slucaj);

            }
            else
            {
                return NotFound($"Ne postoji slucaj sa id-jem {id}");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
    [Route("Post")]
    [HttpPost]
    public async Task<ActionResult> Dodaj([FromBody] Slucaj slucaj, [FromQuery] int? idKorisnika, [FromQuery] List<int> kategorijeIDs, [FromQuery] List<string> slike)
    {
        if (slucaj == null) return BadRequest("Slučaj ne može biti null");
        if (slucaj.Naziv == null || slucaj.Naziv.Length > 50) return BadRequest("Slučaj mora da ima naziv(do 50 karaktera)");
        if (string.IsNullOrEmpty(slucaj.Opis) || slucaj.Opis.Length > 500) return BadRequest("Opis slucaja je obavezan(do 500 karaktera)");
        if (idKorisnika == null) return BadRequest("ID korisnika ne može biti null");
        if (kategorijeIDs.Count == 0) return BadRequest("Mora postojati kategorija");
        foreach (string slika in slike)
        {
            if (string.IsNullOrEmpty(slika) || string.IsNullOrWhiteSpace(slika)) return BadRequest("Putanja do slike ne moze biti prazna");
            slucaj.Slike.Add(slika);
        }
        try
        {
            var korisnik = Context.Korisnici.Where(k => k.ID == idKorisnika).FirstOrDefault();
            if (korisnik == null) return BadRequest("Korisnik ne postoji");

            slucaj.Korisnik = korisnik;
            korisnik.Slucajevi.Add(slucaj);
            foreach (int id in kategorijeIDs)
            {
                var kategorija = Context.Kategorije.Where(k => k.ID == id).FirstOrDefault();
                if (kategorija == null)
                {
                    return BadRequest($"Kategorija sa IDjem {id} ne postoji");
                }
            }

            Context.Slucajevi.Add(slucaj);
            await Context.SaveChangesAsync();
            return Ok(slucaj.ID);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.InnerException.Message);
            return BadRequest(e.Message);
        }
    }
    [Route("Delete/{id}")]
    [HttpDelete]
    public async Task<ActionResult> Obrisi(int id)
    {

        if (id < 0) return BadRequest("ID ne može biti negativan");
        try
        {
            var slucaj = Context.Slucajevi.Where(p => p.ID == id).FirstOrDefault();
            if (slucaj != null)
            {
                Context.Slucajevi.Remove(slucaj);
                await Context.SaveChangesAsync();
                return Ok("Uspesno izbrisan slucaj");

            }
            else
            {
                return NotFound($"Ne postoji slucaj sa id-jem {id}");
            }

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [Route("Update/{id}")]
    [HttpPut]
    public async Task<ActionResult> Azuriraj(int id, [FromQuery] string? naziv, [FromQuery] string? opis, [FromQuery] string[]? addSlike, [FromQuery] string[]? removeSlike, [FromQuery] int? idLokacija, [FromQuery] int? idKorisnika, [FromQuery] int? idZivotinja, [FromQuery] int[]? idRemoveKategorija, int[]? idAddKategorija)
    {
        try
        {
            var slucaj = Context.Slucajevi.Where(p => p.ID == id).FirstOrDefault();
            if (id < 0) return BadRequest("ID ne može biti negativan");
            if (slucaj != null)
            {
                if (naziv != null)
                {
                    if (string.IsNullOrEmpty(naziv) || string.IsNullOrWhiteSpace(naziv)) return BadRequest("Naziv ne može biti prazan");
                    if (naziv.Length > 50) return BadRequest("Naziv ne može biti duži od 50 karaktera");
                    slucaj.Naziv = naziv;
                }
                if (opis != null)
                {

                    if (string.IsNullOrEmpty(opis) || string.IsNullOrWhiteSpace(opis)) return BadRequest("Opis ne može biti prazan");
                    if (opis.Length > 500) return BadRequest("Opis ne može biti duži od 500 karaktera");
                    slucaj.Opis = opis;
                }
                if (idLokacija.HasValue)
                {
                    var lokacija = Context.Lokacije.Where(l => l.ID == idLokacija).FirstOrDefault();
                    if (lokacija == null) return BadRequest($"Ne postoji lokacija sa idjem{idLokacija}");
                    slucaj.Lokacija = lokacija;
                    lokacija.Slucaj = slucaj;
                }
                if (idKorisnika.HasValue)
                {
                    var korisnik = Context.Korisnici.Where(k => k.ID == idKorisnika).FirstOrDefault();
                    if (korisnik == null) return BadRequest($"Ne postoji korisnik sa id-jem{idKorisnika}");
                    if (slucaj.Korisnik != null) slucaj.Korisnik.Slucajevi.Remove(slucaj);
                    slucaj.Korisnik = korisnik;
                    korisnik.Slucajevi.Add(slucaj);
                }
                if (idZivotinja.HasValue)
                {
                    var zivotinja = Context.Zivotinje.Where(k => k.ID == idZivotinja).FirstOrDefault();
                    if (zivotinja == null) return BadRequest($"Ne postoji zivotinja sa id-jem{idZivotinja}");
                    slucaj.Zivotinja = zivotinja;
                    zivotinja.Slucaj = slucaj;
                }
                if (addSlike != null)
                {
                    foreach (string slika in addSlike)
                    {
                        if (string.IsNullOrEmpty(slika) || string.IsNullOrWhiteSpace(slika)) return BadRequest("Putanja do slike ne moze biti prazna");
                        if (slucaj.Slike.Contains(slika)) return BadRequest("Ne može se ponavljati slika");
                        slucaj.Slike.Add(slika);
                    }
                }
                if (removeSlike != null)
                {
                    foreach (string slika in removeSlike)
                    {
                        if (slika == null) return BadRequest("Null argument");
                        slucaj.Slike.Remove(slika);
                    }
                }
                if (idAddKategorija != null)
                {
                    foreach (int kId in idAddKategorija)
                    {
                        var kategorija = Context.Kategorije.Where(k => k.ID == kId).FirstOrDefault();
                        if (kategorija == null) return BadRequest($"Ne postoji kategorija sa IDjem {kId}");
                        slucaj.Kategorija.Add(kategorija);
                        kategorija.Slucajevi.Add(slucaj);
                    }
                }
                if (idRemoveKategorija != null)
                {
                    foreach (int kId in idRemoveKategorija)
                    {
                        var kategorija = Context.Kategorije.Where(k => k.ID == kId).FirstOrDefault();
                        if (kategorija == null) return BadRequest($"Ne postoji kategorija sa IDjem {kId}");
                        slucaj.Kategorija.Remove(kategorija);
                        kategorija.Slucajevi.Remove(slucaj);
                    }
                }

                await Context.SaveChangesAsync();
                return Ok($"Izmenjen slucaj {slucaj.ID}");
            }
            else
            {
                return NotFound($"Ne postoji slucaj sa id-jem {id}");
            }

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [Route("Get/All")]
    [HttpGet]
    public async Task<ActionResult> PreuzmiSveSlucajeve()
    {
        try
        {
            return Ok(await Context.Slucajevi.Include(s=>s.Novosti).Include(s=>s.Donacije).Include(s=>s.Troskovi).Include(s=>s.Kategorija).ToListAsync());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [Route("Get/WithCategory")]
    [HttpGet]
    public async Task<ActionResult> PreuzmiSveSlucajeveSaKategorijama([FromQuery] int[] kategorijeIds)
    {
        if (kategorijeIds == null) return BadRequest("Moraju se proslediti idjevi kategorija");
        try
        {
            return Ok(await Context.Slucajevi.Where(s => kategorijeIds.Intersect(s.Kategorija.Select(k => k.ID)).ToArray().Length == kategorijeIds.Length).ToListAsync());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}