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
    public ActionResult Preuzmi(int id)
    {
        try
        {
            var slucaj = Context.Slucajevi.Where(p => p.ID == id).FirstOrDefault();
            if (slucaj != null)
            {
                return Ok(slucaj);

            }
            else
            {
                return BadRequest($"Ne postoji slucaj sa id-jem {id}");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
    [Route("Post")]
    [HttpPost]
    public async Task<ActionResult> Dodaj([FromBody] Slucaj slucaj)
    {
        try
        {
            Context.Slucajevi.Add(slucaj);
            await Context.SaveChangesAsync();
            return Ok($"ID:{slucaj.ID}");
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
            var slucaj = Context.Slucajevi.Where(p => p.ID == id).FirstOrDefault();
            if (slucaj != null)
            {
                Context.Slucajevi.Remove(slucaj);
                await Context.SaveChangesAsync();
                return Ok("Uspesno izbrisan slucaj");

            }
            else
            {
                return BadRequest($"Ne postoji slucaj sa id-jem {id}");
            }

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [Route("Update/{id}")]
    [HttpPut]
    public async Task<ActionResult> Azuriraj(int id, [FromQuery] string naziv, [FromQuery] string? opis, [FromQuery] string? slika)
    {
        try
        {
            var slucaj = Context.Slucajevi.Where(p => p.ID == id).FirstOrDefault();
            if (slucaj != null)
            {
                if (naziv != null)
                {
                    slucaj.Naziv = naziv;
                }
                if (opis != null)
                {
                    slucaj.Opis = opis;
                }
                if (slika != null)
                {
                    slucaj.Slika = slika;
                }
                await Context.SaveChangesAsync();
                return Ok($"Izmenjen slucaj {slucaj.ID}");
            }
            else
            {
                return BadRequest($"Ne postoji slucaj sa id-jem {id}");
            }

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}