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
    public ActionResult Preuzmi(int id)
    {
        try
        {
            var donacija = Context.Donacije.Where(p => p.ID == id).FirstOrDefault();
            if (donacija != null)
            {
                return Ok(donacija);
            }
            else
            {
                return BadRequest($"Ne postoji donacija sa id-jem {id}");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
    [Route("Post")]
    [HttpPost]
    public async Task<ActionResult> Dodaj([FromBody] Donacija donacija)
    {
        try
        {
            Context.Donacije.Add(donacija);
            await Context.SaveChangesAsync();
            return Ok($"ID: {donacija.ID}");
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
                return BadRequest($"Ne postoji donacija sa id-jem {id}");
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

        try
        {
            var donacija = Context.Donacije.Where(p => p.ID == id).FirstOrDefault();
            if (donacija != null)
            {
                if (kolicina.HasValue)
                {
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
                return BadRequest($"Ne postoji donacija sa id-jem {id}");
            }

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}