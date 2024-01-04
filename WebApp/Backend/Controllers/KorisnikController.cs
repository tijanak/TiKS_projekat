namespace Backend.Controllers;
[ApiController]
[Route("[controller]")]
public class KorisnikController : ControllerBase
{
    public ProjectContext Context { get; set; }
    public KorisnikController(ProjectContext Context)
    {
        this.Context = Context;
    }

    [Route("preuzmikorisnika/{id}")]
    [HttpGet]
    public ActionResult PreuzmiKorisnika(int id)
    {
        try
        {
            var korisnik = Context.Korisnici.Where(k => k.ID == id).FirstOrDefault();
            if (korisnik != null)
            {
                return Ok(korisnik);
            }
            else
            {
                return BadRequest("Trazeni korisnik ne postoji");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }


    [HttpPost]
    public async Task<ActionResult> DodajKorisnika([FromBody] Korisnik korisnik)
    {
        try
        {
            Context.Korisnici.Add(korisnik);
            await Context.SaveChangesAsync();
            return Ok(korisnik.ID);
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
                var stari_korisnik = await Context.Korisnici.FindAsync(korisnik.ID);

                if(stari_korisnik!=null){
                    stari_korisnik.Username = korisnik.Username;
                    stari_korisnik.Password = korisnik.Password;
                    stari_korisnik.Slucajevi = korisnik.Slucajevi;
                    Context.Korisnici.Update(stari_korisnik);
                    await Context.SaveChangesAsync();
                    return Ok();
                }
                else{
                    return NotFound("Trazeni korisnik ne postoji");
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
                await Context.SaveChangesAsync();
                return Ok(korisnik);
            }
            else{
                return NotFound("Trazeni korisnik svakako ne postoji");
            }
            
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
}

}