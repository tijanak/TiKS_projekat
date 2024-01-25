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
    public async Task<ActionResult> DodajTrosak([FromBody] Trosak t, [FromQuery]int idSlucaja){
        try{
            if(t.Namena==null || t.Namena.Length==0) return BadRequest("Trosak mora imati namenu");
            if(t.Namena.Length>=50) return BadRequest("namena moze imati max 50 karaktera");
            if(t.Kolicina<100) return BadRequest("Trosak mora biti makar 100din");
            
            var s = await Context.Slucajevi.Where(s=>s.ID==idSlucaja).FirstOrDefaultAsync();
            if(s==null) return NotFound("Slucaj ne postoji u bazi");

            t.Slucaj=s;
            s.Troskovi.Add(t);
            await Context.Troskovi.AddAsync(t);
            await Context.SaveChangesAsync();
            return Ok(t);
        }
        catch(Exception e){
            return BadRequest(e);
        }
    }
    [HttpPut("izmenitrosak")]
    public async Task<ActionResult> IzmeniTrosak([FromQuery] int id_troska, [FromQuery]string? namena, [FromQuery]int? kolicina, [FromQuery]int? id_slucaja){
        try{
            var stari_trosak = Context.Troskovi.Where(t => t.ID==id_troska).FirstOrDefault();
            if(stari_trosak==null)  return NotFound("Pogresan ID troska");
            
            bool nesto_menjano=false;
            if(namena!=null ){
                if(namena.Length>0 && namena.Length<=50){
                stari_trosak.Namena=namena;
                nesto_menjano=true;
                }
                else return BadRequest("namena ima min 1 max 50 karaktera");
            }
            if(kolicina!=null){
                if(kolicina>100){
                    stari_trosak.Kolicina=(int)kolicina;
                    nesto_menjano=true;
                }
                else return BadRequest("kolicina je min 100din");
            }
            if(id_slucaja!=null)
            {
                var slucaj = await Context.Slucajevi.FindAsync(id_slucaja);
                if(slucaj!=null){
                stari_trosak.Slucaj=slucaj;
                nesto_menjano=true;
                }
                else return NotFound("Los ID slucaja");
            }
            if(nesto_menjano){
                Context.Troskovi.Update(stari_trosak);
                await Context.SaveChangesAsync();
            }
            else return BadRequest("nijedna vrednost nije promenjena");
            
            return Ok(stari_trosak);
        }
        catch(Exception e){
            return BadRequest(e);
        }
    }
    [HttpDelete]
    public async Task<ActionResult> UkloniTrosak([FromQuery] int id){
        try{
            var t = await Context.Troskovi.FindAsync(id);
            if(t==null){
                return NotFound("Nema svakako");
            }
            Context.Troskovi.Remove(t);
            await Context.SaveChangesAsync();
            return Ok(t);
        }
        catch(Exception e){
            return BadRequest(e);
        }
    }
}