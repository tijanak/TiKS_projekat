namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class DonacijaController : ControllerBase
{
    public ProjectContext Context { get; set; }
    public DonacijaController(ProjectContext context)
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
            var donacija = await Context.Donacije.Where(p => p.ID == id).FirstOrDefaultAsync();
            if (donacija != null)
            {
                return Ok(donacija);
            }
            else
            {
                return NotFound($"Ne postoji donacija sa id-jem {id}");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
    [Route("Post/{idKorisnika}/{idSlucaja}")]
    [HttpPost]
    public async Task<ActionResult> Dodaj([FromBody] Donacija donacija, int idKorisnika, int idSlucaja)
    {
        if (donacija == null) return BadRequest("Donacija ne sme da bude null");
        if (donacija.Kolicina <= 0)
            return BadRequest("Količina mora biti pozitivna");
        var korisnik = await Context.Korisnici.FindAsync(idKorisnika);
        if (korisnik == null)
            return BadRequest($"Korisnik sa id-jem {idKorisnika} ne postoji u bazi");
        donacija.Korisnik = korisnik;
        var slucaj = await Context.Slucajevi.FindAsync(idSlucaja);
        if (slucaj == null)
            return BadRequest($"Slucaj sa id-jem {idSlucaja} ne postoji u bazi");
        donacija.Slucaj = slucaj;
        try
        {

            Donacija toAdd = new Donacija();
            toAdd.Kolicina = donacija.Kolicina;
            toAdd.Korisnik = donacija.Korisnik;
            toAdd.Slucaj = donacija.Slucaj;
            Context.Donacije.Add(toAdd);
            await Context.SaveChangesAsync();
            return Ok(toAdd.ID);
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
        if (id < 0) return BadRequest("ID mora biti pozitivan");
        try
        {
            var donacija = Context.Donacije.Where(p => p.ID == id).FirstOrDefault();
            if (donacija != null)
            {
                Context.Donacije.Remove(donacija);
                await Context.SaveChangesAsync();
                return Ok("Uspesno izbrisana donacija");

            }
            else
            {
                return NotFound($"Ne postoji donacija sa id-jem {id}");
            }

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [Route("Update/{id}")]
    [HttpPut]
    public async Task<ActionResult> Azuriraj(int id, [FromQuery] int? kolicina, [FromQuery] int? idKorisnika, [FromQuery] int? idSlucaja)
    {
        if (id < 0) return BadRequest("ID ne može biti negativan");

        try
        {
            var donacija = Context.Donacije.Where(p => p.ID == id).FirstOrDefault();
            if (donacija != null)
            {
                if (kolicina.HasValue)
                {
                    if (kolicina <= 0) return BadRequest("Količina mora da bude pozitivna");
                    donacija.Kolicina = (int)kolicina;
                }
                if (idKorisnika.HasValue)
                {
                    var korisnik = await Context.Korisnici.FindAsync(idKorisnika);
                    if (korisnik != null)
                    {
                        donacija.Korisnik = korisnik;
                    }
                    else
                    {
                        return BadRequest($"Korisnik sa id-jem {idKorisnika} ne postoji");
                    }
                }
                if (idSlucaja.HasValue)
                {
                    var slucaj = await Context.Slucajevi.FindAsync(idSlucaja);
                    if (slucaj != null)
                    {
                        donacija.Slucaj = slucaj;
                    }
                    else
                    {
                        return BadRequest($"Slucaj sa id-jem {idSlucaja} ne postoji");
                    }
                }
                await Context.SaveChangesAsync();
                return Ok($"Izmenjena donacija {donacija.ID}");
            }
            else
            {
                return NotFound($"Ne postoji donacija sa id-jem {id}");
            }

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}