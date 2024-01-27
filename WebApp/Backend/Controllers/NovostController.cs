namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]

public class NovostController : ControllerBase
{
    public ProjectContext Context { get; set; }

    public NovostController(ProjectContext Context)
    {
        this.Context = Context;
    }

    [HttpGet("preuzminovost/{id}")]
    public ActionResult PreuzmiNovost(int id)
    {
        try
        {
            var n = Context.Novosti.Where(n => n.ID == id).FirstOrDefault();
            if (n == null)
            {
                return NotFound("Trazena novost ne postoji");
            }
            return Ok(n);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet("preuzminovosti/{id_slucaja}")]
    public async Task<ActionResult> PreuzmiNovosti(int id_slucaja)
    {
        try
        {
            var n = await Context.Novosti.Where(n => n.Slucaj.ID == id_slucaja).OrderByDescending(n=>n.Datum).ToListAsync();
            if (n == null)
            {
                return NotFound("Bez novosti");
            }
            return Ok(n);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet("preuzmisvenovosti")]
    public async Task<ActionResult> PreuzmiSveNovosti(){
        try
        {
            return Ok(await Context.Novosti.Include(n=>n.Slucaj).ToListAsync());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("dodajnovost")]
    public async Task<ActionResult> DodajNovost([FromBody] Novost n, [FromQuery] int? id_slucaja)
    {
        try
        {
            if (null == n) return BadRequest("fali novost");

            if (n.Tekst == null || n.Tekst.Length == 0 || n.Tekst.Length > 5000) return BadRequest("Tekst je predugacak. Max 5000 karaktera");

            if (n.Slika == null || n.Slika.Length == 0) return BadRequest("Fali slika");

            if (null == id_slucaja) return BadRequest("fali id slucaja");

            // var poslednja_novost = await Context.Novosti.Where(n => n.Slucaj.ID == id_slucaja).OrderByDescending(n => n.Datum).FirstOrDefaultAsync();

            // if (poslednja_novost != null && poslednja_novost.Datum.CompareTo(n.Datum) >= 0)
            //     return BadRequest("Ne mozete dodati retrospektivno novosti");


            var slucaj = await Context.Slucajevi.FindAsync(id_slucaja);

            if (slucaj == null) return NotFound("id_slucaja los");

            n.Slucaj = slucaj;
            slucaj.Novosti.Add(n);

            
            await Context.Novosti.AddAsync(n);
            await Context.SaveChangesAsync();
            return Ok(n.ID);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPut("izmeninovost")]
    public async Task<ActionResult> IzmeniNovost([FromBody] Novost n)
    {
        try
        {
            var stara_novost = await Context.Novosti.FindAsync(n.ID);
            bool nesto_menjano = false;
            if (stara_novost == null)
            {
                return NotFound("Pogresan ID");
            }
            if(n.Slika!=null&&n.Slika.Length>0&&n.Slika!=stara_novost.Slika)
                stara_novost.Slika=n.Slika;
            if (n.Tekst != null)
            {
                if (n.Tekst.Length > 0 && n.Tekst.Length < 5000)
                {
                    stara_novost.Tekst = n.Tekst;
                    nesto_menjano = true;
                }
                else return BadRequest("tekst neodgovarajuce duzine. mora biti 0<tekst<5000 karaktera");
            }

            if (n.Datum != null)
            {
                if (DateTime.Compare(n.Datum, DateTime.Today) <= 0)
                {
                    stara_novost.Datum = (DateTime)n.Datum;
                    nesto_menjano = true;
                }
                else return BadRequest("nevalidan datum");
            }

            if (nesto_menjano)
            {
                Context.Novosti.Update(stara_novost);
                await Context.SaveChangesAsync();
            }
            else return BadRequest("morate promeniti nesto");

            return Ok(stara_novost);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    [HttpDelete("ukloninovost/{id}")]
    public async Task<ActionResult> UkloniNovost(int id)
    {
        try
        {
            var n = await Context.Novosti.FindAsync(id);
            if (n == null)
            {
                return NotFound("Nema svakako");
            }
            Context.Novosti.Remove(n);
            await Context.SaveChangesAsync();
            return Ok(n);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}