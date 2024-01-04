namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class LokacijaController : ControllerBase
{
    public ProjectContext Context { get; set; }
    public LokacijaController(ProjectContext context)
    {
        Context = context;
    }
    [Route("Get/{id}")]
    [HttpGet]
    public ActionResult Preuzmi(int id)
    {
        try
        {
            var lokacija = Context.Lokacije.Where(p => p.ID == id).FirstOrDefault();
            if (lokacija != null)
            {
                return Ok(lokacija);

            }
            else
            {
                return BadRequest($"Ne postoji lokacija sa id-jem {id}");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
    [Route("Post")]
    [HttpPost]
    public async Task<ActionResult> Dodaj([FromBody] Lokacija lokacija)
    {
        try
        {
            Context.Lokacije.Add(lokacija);
            await Context.SaveChangesAsync();
            return Ok(lokacija.ID);
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
            var lokacija = Context.Lokacije.Where(p => p.ID == id).FirstOrDefault();
            if (lokacija != null)
            {
                Context.Lokacije.Remove(lokacija);
                await Context.SaveChangesAsync();
                return Ok("Uspesno izbrisana lokacija");

            }
            else
            {
                return BadRequest($"Ne postoji lokacija sa id-jem {id}");
            }

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [Route("Update/{id}")]
    [HttpPut]
    public async Task<ActionResult> Azuriraj(int id, [FromQuery] int? longitude, [FromQuery] int? latitude, [FromQuery] int? idSlucaja)
    {
        try
        {
            var lokacija = Context.Lokacije.Where(p => p.ID == id).FirstOrDefault();
            if (lokacija != null)
            {
                if (longitude.HasValue)
                {
                    lokacija.Longitude = (int)longitude;
                }
                if (latitude.HasValue)
                {
                    lokacija.Latitude = (int)latitude;
                }
                if (idSlucaja.HasValue)
                {
                    var slucaj = await Context.Slucajevi.FindAsync(idSlucaja);
                    if (slucaj != null)
                    {
                        lokacija.Slucaj = slucaj;
                    }
                    else
                    {
                        return BadRequest($"Slucaj sa id-jem {idSlucaja} ne postoji");
                    }
                }
                await Context.SaveChangesAsync();
                return Ok($"Izmenjena lokacija {lokacija.ID}");
            }
            else
            {
                return BadRequest($"Ne postoji lokacija sa id-jem {id}");
            }

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
}