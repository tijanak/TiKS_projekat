namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]

public class TrosakController : ControllerBase
{
    public ProjectContext Context{get; set;}

    public TrosakController(ProjectContext Context){
        this.Context=Context;
    }

    [HttpGet("preuzmitrosak/{id}")]
    public ActionResult PreuzmiTrosak(int id){
        try{
            var t = Context.Troskovi.Where(t => t.ID==id).FirstOrDefault();
            if(t == null){
                return NotFound("Trazeni trosak ne postoji.");
            }
            return Ok(t);
        }
        catch(Exception e){
            return BadRequest(e);
        }
    }

    [HttpPost("dodajtrosak")]
    public async Task<ActionResult> DodajTrosak([FromBody] Trosak t){
        try{
            await Context.Troskovi.AddAsync(t);
            await Context.SaveChangesAsync();
            return Ok();
        }
        catch(Exception e){
            return BadRequest(e);
        }
    }
    [HttpPut]
    public async Task<ActionResult> IzmeniTrosak([FromBody] Trosak n){
        try{
            var stari_trosak = await Context.Troskovi.FindAsync(n.ID);
            if(stari_trosak==null){
                return NotFound("Pogresan ID");
            }
            stari_trosak.Namena=n.Namena;
            stari_trosak.Kolicina=n.Kolicina;
            Context.Troskovi.Update(stari_trosak);
            await Context.SaveChangesAsync();
            return Ok();
        }
        catch(Exception e){
            return BadRequest(e);
        }
    }
    [HttpDelete]
    public async Task<ActionResult> UkloniTrosak([FromBody] int id){
        try{
            var t = await Context.Troskovi.FindAsync(id);
            if(t==null){
                return NotFound("Nema svakako");
            }
            Context.Troskovi.Remove(t);
            await Context.SaveChangesAsync();
            return Ok();
        }
        catch(Exception e){
            return BadRequest(e);
        }
    }
}