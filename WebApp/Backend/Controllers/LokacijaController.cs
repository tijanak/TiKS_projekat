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
    public async Task<ActionResult> Preuzmi(int id)
    {
        if (id < 0) return BadRequest("ID ne može biti negativan");
        try
        {
            var lokacija = await Context.Lokacije.Where(p => p.ID == id).FirstOrDefaultAsync();
            if (lokacija != null)
            {
                return Ok(lokacija);

            }
            else
            {
                return NotFound($"Ne postoji lokacija sa id-jem {id}");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
    [Route("Post/{idSlucaja}")]
    [HttpPost]
    public async Task<ActionResult> Dodaj([FromBody] Lokacija lokacija, int idSlucaja)
    {
        if (lokacija == null) return BadRequest("Lokacija ne sme da bude null");
        if (lokacija.Latitude < -90 || lokacija.Latitude > 90) return BadRequest("Latituda mora biti u opsegu [-90,90]");
        if (lokacija.Longitude < -180 || lokacija.Longitude >= 180) return BadRequest("Longituda mora biti u opsegu [-180,180)");
        var slucaj = await Context.Slucajevi.FindAsync(idSlucaja);
        if (slucaj == null) return BadRequest($"Ne postoji slučaj sa idjem {idSlucaja}");
        lokacija.Slucaj = slucaj;
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
        if (id < 0) return BadRequest("ID ne može biti negativan");
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
                return NotFound($"Ne postoji lokacija sa id-jem {id}");
            }

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [Route("Update/{id}")]
    [HttpPut]
    public async Task<ActionResult> Azuriraj(int id, [FromQuery] double? longitude, [FromQuery] double? latitude, [FromQuery] int? idSlucaja)
    {
        try
        {
            var lokacija = Context.Lokacije.Where(p => p.ID == id).FirstOrDefault();
            if (lokacija != null)
            {
                if (longitude.HasValue)
                {

                    if (longitude < -180 || longitude >= 180) return BadRequest("Longituda mora biti u opsegu [-180,180)");
                    lokacija.Longitude = (double)longitude;
                }
                if (latitude.HasValue)
                {
                    if (latitude < -90 || latitude > 90) return BadRequest("Latituda mora biti u opsegu [-90,90]");
                    lokacija.Latitude = (double)latitude;
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
                return NotFound($"Ne postoji lokacija sa id-jem {id}");
            }

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
}