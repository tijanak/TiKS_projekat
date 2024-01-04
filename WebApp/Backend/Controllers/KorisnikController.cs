namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class KorisnikController : ControllerBase
{
    public ProjectContext Context { get; set; }
    public KorisnikController(ProjectContext context)
    {
        Context = context;
    }
    [Route("Get/{id}")]
    [HttpGet]
    public ActionResult Preuzmi(int id)
    {
        try
        {
            var korisnik = Context.Korisnici.Where(p => p.ID == id).FirstOrDefault();
            if (korisnik != null)
            {
                return Ok(korisnik);

            }
            else
            {
                return BadRequest("Ne postoji korisnik sa trazenim id-jem");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
    [Route("Post")]
    [HttpPost]
    public async Task<ActionResult> Dodaj([FromBody] Korisnik korisnik)
    {
        /*if (string.IsNullOrWhiteSpace(test.Nebitno))
        {
            return BadRequest("bitno je");
        }*/
        try
        {
            Context.Korisnici.Add(korisnik);
            await Context.SaveChangesAsync();
            return Ok($"U redu ID je {korisnik.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}