/* namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]

public class ZivotinjaController : ControllerBase
{
    public ProjectContext Context{get; set;}

    public TrosakController(ProjectContext Context){
        this.Context=Context;
    }

    [HttpGet("preuzmizivotinju/{id}")]
    public async Task<ActionResult> PreuzmiZivotinju(int id){
        try{
            var z = await Context.Zivotinje.Where(z => z.id==id).FirstOrDefault();
            if(z == null){
                return NotFound("Zivotinja ne postoji. ;)")
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
    public async Task<ActionResult> IzmeniZivotinju([FromBody] Zivotinja z){
        try{
            var staro = await Context.Zivotinje.FindAsync(z.ID);
            if(staro==null){
                return NotFound("Pogresan ID");
            }
            staro.Namena=n.Namena;
            staro.Kolicina=n.Kolicina;
            Context.Zivotinje.Update(staro);
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
            var z = await Context.Zivotinje.FindAsync(id);
            if(z==null){
                return NotFound("Nema svakako");
            }
            await Context.Zivotinje.Remove(id);
            await Context.SaveChangesAsync();
            return Ok();
        }
        catch(Exception e){
            return BadRequest(e);
        }
    }
} */