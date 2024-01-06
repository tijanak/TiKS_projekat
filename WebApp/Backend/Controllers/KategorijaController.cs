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
    public async Task<ActionResult> Preuzmi(int id)
    {
        if (id < 0) return BadRequest("ID ne može biti negativan");
        try
        {
            var kategorija = await Context.Kategorije.Where(p => p.ID == id).FirstOrDefaultAsync();
            if (kategorija != null)
            {
                return Ok(kategorija);

            }
            else
            {
                return NotFound($"Ne postoji kategorija sa id-jem {id}");
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
        if (kategorija == null) return BadRequest("Kategorija ne sme da bude null");
        var postoji = Context.Kategorije.Where(k => k.Prioritet == kategorija.Prioritet).FirstOrDefault();
        if (postoji != null) return BadRequest($"Već postoji kategorija sa prioritetom {kategorija.Prioritet}");
        if (string.IsNullOrEmpty(kategorija.Tip) || string.IsNullOrWhiteSpace(kategorija.Tip)) return BadRequest("Tip ne može biti prazan");
        if (kategorija.Tip.Length > 50) return BadRequest("Maksimalna dužina za tip je 50");
        try
        {

            Context.Kategorije.Add(kategorija);
            await Context.SaveChangesAsync();
            return Ok(kategorija.ID);
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
    [HttpPut]
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