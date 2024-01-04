namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]

public class NovostController : ControllerBase
{
    public ProjectContext Context { get; set; }

    public NovostController(ProjectContext Context){
        this.Context=Context;
    }

    [HttpGet("preuzminovost/{id}")]
    public ActionResult<Novost> PreuzmiNovost(int id){
        try{
            var n = Context.Novosti.Where(n=>n.ID==id).FirstOrDefault();
            if(n == null){
                return NotFound("Trazena novost ne postoji");
            }
            return Ok(n);
        }
        catch(Exception e){
            return BadRequest(e);
        }
    }

    [HttpPost("dodajnovost")]
    public async Task<ActionResult> DodajNovost([FromBody] Novost n){
        try{
            await Context.Novosti.AddAsync(n);
            await Context.SaveChangesAsync();
            return Created();
        }
        catch(Exception e){
            return BadRequest(e);
        }
    }

    [HttpPut]
    public async Task<ActionResult> IzmeniNovost([FromBody] Novost n){
        try{
            var stara_novost = await Context.Novosti.FindAsync(n.ID);
            if(stara_novost==null){
                return NotFound("Pogresan ID");
            }
            stara_novost.Tekst=n.Tekst;
            stara_novost.Slika=n.Slika;
            Context.Novosti.Update(stara_novost);
            await Context.SaveChangesAsync();
            return Ok();
        }
        catch(Exception e){
            return BadRequest(e);
        }
    }

    public async Task<ActionResult> UkloniNovost([FromBody] int id){
        try{
            var n = await Context.Novosti.FindAsync(id);
            if(n==null){
                return NotFound("Nema svakako");
            }
            Context.Novosti.Remove(n);
            await Context.SaveChangesAsync();
            return Ok();
        }       
        catch(Exception e){
            return BadRequest(e);
        }
    }
}