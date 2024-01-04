namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class KategorijaController : ControllerBase
{
    public ProjectContext Context { get; set; }
    public KategorijaController(ProjectContext context)
    {
        Context = context;
    }
    [Route("Get/{id}")]
    [HttpGet]
    public ActionResult Preuzmi(int id)
    {
        try
        {
            var kategorija = Context.Kategorije.Where(p => p.ID == id).FirstOrDefault();
            if (kategorija != null)
            {
                return Ok(kategorija);

            }
            else
            {
                return BadRequest($"Ne postoji kategorija sa id-jem {id}");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
    [Route("Post")]
    [HttpPost]
    public async Task<ActionResult> Dodaj([FromBody] Kategorija kategorija)
    {
        try
        {
            Context.Kategorije.Add(kategorija);
            await Context.SaveChangesAsync();
            return Ok($"ID:{kategorija.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [Route("Delete/{id}")]
    [HttpPost]
    public async Task<ActionResult> Obrisi(int id)
    {
        try
        {
            var kategorija = Context.Kategorije.Where(p => p.ID == id).FirstOrDefault();
            if (kategorija != null)
            {
                Context.Kategorije.Remove(kategorija);
                await Context.SaveChangesAsync();
                return Ok("Uspesno izbrisana kategorija");

            }
            else
            {
                return BadRequest($"Ne postoji kategorija sa id-jem {id}");
            }

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [Route("Update/{id}")]
    [HttpPost]
    public async Task<ActionResult> Azuriraj(int id, [FromQuery] string? tip)
    {
        try
        {
            var kategorija = Context.Kategorije.Where(p => p.ID == id).FirstOrDefault();
            if (kategorija != null)
            {
                kategorija.Tip = tip;
                await Context.SaveChangesAsync();
                return Ok($"Izmenjena kategorija {kategorija.ID}");
            }
            else
            {
                return BadRequest($"Ne postoji kategorija sa id-jem {id}");
            }

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}