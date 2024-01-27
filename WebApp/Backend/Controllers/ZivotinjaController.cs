namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]

public class ZivotinjaController : ControllerBase
{
    public ProjectContext Context { get; set; }

    public ZivotinjaController(ProjectContext Context)
    {
        this.Context = Context;
    }

    [HttpGet("preuzmi/{id_slucaja}")]
    public ActionResult Preuzmi(int id_slucaja)
    {
        try
        {
            var zivotinja = Context.Zivotinje.Where(z => z.Slucaj.ID == id_slucaja).FirstOrDefault();
            if (zivotinja == null) return NotFound("Zivotinja ne postoji.");
            return Ok(zivotinja);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet("preuzmizivotinju/{id}")]
    public ActionResult PreuzmiZivotinju(int id)
    {
        try
        {
            var zivotinja = Context.Zivotinje.Where(z => z.ID == id).FirstOrDefault();
            if (zivotinja == null) return NotFound("Zivotinja ne postoji.");
            return Ok(zivotinja);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    [HttpPost("dodajzivotinju")]
    public async Task<ActionResult> DodajZivotinju([FromBody] Zivotinja z, [FromQuery] int idSlucaja)
    {
        try
        {

            if (z == null) return BadRequest("fali zivotinja");
            if (z.Ime == null || z.Ime.Length > 50) return BadRequest("predugacko ime zivotinje");
            if (z.Vrsta == null || z.Vrsta.Length > 50) return BadRequest("predugacko ime vrste");

            var s = Context.Slucajevi.Where(s => s.ID == idSlucaja).FirstOrDefault();
            if (s == null) return NotFound("Slucaj ne postoji u bazi");
            z.Slucaj = s;
            s.Zivotinja = z;

            await Context.Zivotinje.AddAsync(z);
            await Context.SaveChangesAsync();
            return Ok(z);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    [HttpPut("izmenizivotinju")]
    public async Task<ActionResult> IzmeniZivotinju([FromQuery] int id_zivotinje, [FromQuery] string? ime, [FromQuery] string? vrsta, [FromQuery] int? idSlucaja)
    {
        try
        {
            var staro = Context.Zivotinje.Where(z => z.ID == id_zivotinje).FirstOrDefault();
            bool nesto_menjano = false;
            if (staro == null)
            {
                return NotFound("Pogresan ID");
            }
            if (ime != null)
            {
                if (ime.Length > 0 && ime.Length <= 50)
                {
                    staro.Ime = ime;
                    nesto_menjano = true;
                }
                else return BadRequest("duzina imena mora biti izmedju 0 i 50 karaktera");
            }

            if (vrsta != null)
            {
                if (vrsta.Length > 0 && vrsta.Length <= 50)
                {
                    staro.Vrsta = vrsta;
                    nesto_menjano = true;
                }
                else return BadRequest("duzina vrste mora biti izmedju 0 i 50 karaktera");
            }

            if (idSlucaja != null)
            {
                var s = Context.Slucajevi.Where(s => s.ID == idSlucaja).FirstOrDefault();
                if (s == null) return NotFound(idSlucaja);
                staro.Slucaj = s;
                nesto_menjano = true;
            }
            if (nesto_menjano)
            {
                Context.Zivotinje.Update(staro);
                await Context.SaveChangesAsync();
            }
            else return BadRequest("nijedna vrednost nije promenjena");

            return Ok(staro);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    [HttpDelete("obrisizivotinju/{id}")]
    public async Task<ActionResult> ObrisiZivotinju(int id)
    {
        try
        {
            var z = await Context.Zivotinje.FindAsync(id);
            if (z == null)
            {
                return NotFound("Nema svakako");
            }
            Context.Zivotinje.Remove(z);
            await Context.SaveChangesAsync();
            return Ok(z);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}