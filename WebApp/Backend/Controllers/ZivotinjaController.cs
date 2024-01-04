namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]

public class ZivotinjaController : ControllerBase
{
    public ProjectContext Context{get; set;}

    public ZivotinjaController(ProjectContext Context){
        this.Context=Context;
    }

    [HttpGet("preuzmizivotinju/{id}")]
    public ActionResult PreuzmiZivotinju(int id){
        try{
            var z = Context.Zivotinje.Where(z => z.ID==id).FirstOrDefault();
            if(z == null){
                return NotFound("Zivotinja ne postoji. ;)");
            }
            
            return Ok(z);
        }
        catch(Exception e){
            return BadRequest(e);
        }
    }

    [HttpPost("dodajzivotinju")]
    public async Task<ActionResult> DodajZivotinju([FromBody] Zivotinja z){
        try{
            await Context.Zivotinje.AddAsync(z);
            await Context.SaveChangesAsync();
            return Ok();
        }
        catch(Exception e){
            return BadRequest(e);
        }
    }
    [HttpPut]
    public async Task<ActionResult> IzmeniZivotinju([FromBody] Zivotinja zivotinja){
        try{
            var staro  = Context.Zivotinje.Where(z => z.ID== zivotinja.ID).FirstOrDefault();
            if(staro==null){
                return NotFound("Pogresan ID");
            }
            staro.Ime=zivotinja.Ime;
            staro.Vrsta=zivotinja.Vrsta;
            Context.Zivotinje.Update(staro);
            await Context.SaveChangesAsync();
            return Ok();
        }
        catch(Exception e){
            return BadRequest(e);
        }
    }
    [HttpDelete]
    public async Task<ActionResult> UkloniZivotinju([FromBody] int id){
        try{
            var z = await Context.Zivotinje.FindAsync(id);
            if(z==null){
                return NotFound("Nema svakako");
            }
            Context.Zivotinje.Remove(z);
            await Context.SaveChangesAsync();
            return Ok();
        }
        catch(Exception e){
            return BadRequest(e);
        }
    }
}