/* namespace Backend.Controllers;
[ApiController]
[Route("[controller]")]
public class KorisnikController : ControllerBase
{
    public ProjectContext Context { get; set; }
    public KorisnikController(ProjectContext context)
===={
        this.Context = Context;
    }

    [Route("preuzmikorisnika/{id}")]
    [HttpGet]
    public async Task<ActionResult<Korisnik>> PreuzmiKorisnika(int id)
    {
        try
        {
            Korisnik korisnik = await Context.Korisnici.Where(k => k.ID == id).FirstOrDefault();
            if (korisnik != null)
            {
                return Ok(korisnik);
            }
            else
            {
                return NotFound("Trazeni korisnik ne postoji");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }


    [HttpPost]
    public async Task<ActionResult<int>> DodajKorisnika([FromBody] Korisnik korisnik)
    {
        try
        {
            var id = await Context.Korisnici.AddAsync(korisnik);
            await Context.SaveChangesAsync();
            return Created(korisnik.ID);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult> IzmeniKorisnika([FromBody] Korisnik korisnik)
        {
            try
            {
                Korisnik stariKorisnik = await Context.Korisnici.FindAsync(korisnik.ID);

                if(stariKorisnik!=null){
                    stariKorisnik.Username = korisnik.Username;
                    stariKorisnik.Password = korisnik.Password;
                    stariKorisnik.Slucajevi = korisnik.Slucajevi;   //surely ovako nece raditi
                    Context.Korisnici.Update(stariKorisnik);
                    await Context.SaveChangesAsync();
                    return Ok();
                }
                else{
                    NotFound("Trazeni korisnik ne postoji");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


[HttpDelete]
public async Task<ActionResult<Korisnik>> UkloniKorisnika([FromBody] int ID){

    try
            {
                var korisnik = await Context.Korisnici.FindAsync(ID);
                
                if(korisnik!=null)
                {
                    Context.Korisnici.Remove(korisnik);
                    await Context.Korisnici.SaveChangesAsync();
                    return Ok(korisnik);
                }
                else{
                    NotFound("Trazeni korisnik svakako ne postoji");
                }
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
}

} */