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
            var slucaj = await Context.Slucajevi.Where(p => p.ID == id).FirstOrDefaultAsync();
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
    public async Task<ActionResult> Dodaj([FromBody] Slucaj slucaj, [FromBody] Lokacija? lokacija, [FromBody] Zivotinja? zivotinja, [FromQuery] int? idKorisnika, [FromQuery] Kategorija kategorija)
    {
        if (slucaj == null) return BadRequest("Slučaj ne može biti null");
        if (slucaj.Naziv ==null || slucaj.Naziv.Length>50) return BadRequest("Slučaj mora da ima naziv");
        if (slucaj.Opis ==null || slucaj.Opis.Length>500) return BadRequest("Opis slučaja mora da bude do 500 karaktera");
        if (lokacija == null) return BadRequest("Lokacija ne može biti null");
        if (idKorisnika == null) return BadRequest("ID korisnika ne može biti null");
        if (kategorija == null) return BadRequest("Kategorija ne može biti null");
        if (zivotinja == null) return BadRequest("Životinja ne može biti null");
        try
        {
            var korisnik = Context.Korisnici.Where(k => k.ID == idKorisnika).FirstOrDefault();
            if (korisnik == null) return BadRequest("Korisnik ne postoji");
            
            slucaj.Lokacija = lokacija;
            slucaj.Korisnik = korisnik;
            korisnik.Slucajevi.Add(slucaj);
            slucaj.Kategorija.Add(kategorija);
            slucaj.Zivotinja = zivotinja;
        
            Context.Slucajevi.Add(slucaj);
            await Context.SaveChangesAsync();
            return Ok(slucaj.ID);
        }
        catch (Exception e)
        {
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
    public async Task<ActionResult> Azuriraj(int id, [FromQuery] string? naziv, [FromQuery] string? opis, [FromQuery] int? idLokacija, [FromQuery] int? idKorisnika, [FromQuery] int? idZivotinja, [FromQuery] int? idKategorija)
    {
        try
        {
            var slucaj = Context.Slucajevi.Where(p => p.ID == id).FirstOrDefault();
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

                }
                if (idKorisnika.HasValue)
                {
                    var korisnik = Context.Korisnici.Where(k => k.ID == idKorisnika).FirstOrDefault();
                    if (korisnik == null) return BadRequest($"Ne postoji korisnik sa id-jem{idKorisnika}");
                    if (slucaj.Korisnik != null) slucaj.Korisnik.Slucajevi.Remove(slucaj);
                    slucaj.Korisnik = korisnik;

                }
                if (idZivotinja.HasValue)
                {
                    var zivotinja = Context.Zivotinje.Where(k => k.ID == idZivotinja).FirstOrDefault();
                    if (zivotinja == null) return BadRequest($"Ne postoji zivotinja sa id-jem{idZivotinja}");
                    slucaj.Zivotinja = zivotinja;

                }
                if(idKategorija.HasValue)
                {
                    var kategorija = Context.Kategorije.Where(k=>k.ID==idKategorija).FirstOrDefault();
                    if(kategorija==null) return BadRequest($"Ne postoji kategorija sa id-jem{idKategorija}");
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
}