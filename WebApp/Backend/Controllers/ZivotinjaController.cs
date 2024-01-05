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
                return NotFound("Zivotinja ne postoji.");
            }
            return Ok(z);
        }
        catch(Exception e){
            return BadRequest(e);
        }
    }

    [HttpPost("dodajzivotinju")]
    public async Task<ActionResult> DodajZivotinju([FromQuery] Zivotinja z, [FromQuery]int idSlucaja){
        try{

            if(z.ID==null) return BadRequest("Fali ID"); //smara, ali treba da ostane provera

            var s = Context.Slucajevi.Where(s=>s.ID==idSlucaja).FirstOrDefault();
            if(s==null)
                return NotFound("Slucaj ne postoji u bazi");
            
            z.Slucaj=s;
            await Context.Zivotinje.AddAsync(z);
            await Context.SaveChangesAsync();
            return Ok(z);
        }
        catch(Exception e){
            return BadRequest(e);
        }
    }
    [HttpPut("izmenizivotinju")]
    public async Task<ActionResult> IzmeniZivotinju([FromQuery] int id_zivotinje, [FromQuery]string? ime, [FromQuery]string? vrsta, [FromQuery]int? idSlucaja){
        try{
            var staro  = Context.Zivotinje.Where(z => z.ID== id_zivotinje).FirstOrDefault();
            bool nesto_menjano=false;
            if(staro==null){
                return NotFound("Pogresan ID");
            }
            if(ime!=null)
                {
                    staro.Ime=ime;
                    nesto_menjano=true;
                }

            if(vrsta!=null)    
                {
                    staro.Vrsta=vrsta;
                    nesto_menjano=true;
                }

            if(idSlucaja!=null)
            {
                var s = Context.Slucajevi.Where(s=>s.ID==idSlucaja).FirstOrDefault();
                if(s==null)
                return NotFound(idSlucaja);
                staro.Slucaj=s;
                nesto_menjano=true;
            }
            if(nesto_menjano)
            {
                Context.Zivotinje.Update(staro);
                await Context.SaveChangesAsync();
            }
            return Ok(staro);
        }
        catch(Exception e){
            return BadRequest(e);
        }
    }
    [HttpDelete("obrisizivotinju/{id}")]
    public async Task<ActionResult> ObrisiZivotinju(int id){
        try{
            var z = await Context.Zivotinje.FindAsync(id);
            if(z==null){
                return NotFound("Nema svakako");
            }
            Context.Zivotinje.Remove(z);
            await Context.SaveChangesAsync();
            return Ok(z);
        }
        catch(Exception e){
            return BadRequest(e);
        }
    }
}