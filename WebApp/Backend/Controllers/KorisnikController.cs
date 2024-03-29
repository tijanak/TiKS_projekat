using System.Formats.Tar;

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

    [HttpGet("preuzmikorisnika/{id}")]
    public async Task<ActionResult> PreuzmiKorisnika(int id)
    {
        try
        {
            var korisnik = await Context.Korisnici.Where(k => k.ID == id).Include(k => k.Slucajevi).Select(k => new { k.Username, k.Password, k.Slucajevi }).FirstOrDefaultAsync();


            if (korisnik != null)
            {

                return Ok(korisnik);
            }
            return NotFound("Trazeni korisnik ne postoji");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("preuzmisvekorisnike")]
    public async Task<ActionResult> PreuzmiSveKorisnike()
    {
        try
        {
            return Ok(await Context.Korisnici.ToListAsync());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("Login/{username}/{password}")]
    public async Task<ActionResult> Login(string username, string password)
    {
        try
        {
            var korisnik = await Context.Korisnici.Where(k => k.Username == username).FirstOrDefaultAsync();
            if (korisnik == null) return BadRequest("ne postoji korisnik sa datim usernameom");
            if (korisnik.Password.CompareTo(password) != 0) return BadRequest("pogresna lozinka");
            return Ok(korisnik);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("dodajkorisnika")]
    public async Task<ActionResult> DodajKorisnika([FromBody] Korisnik korisnik)
    {
        try
        {
            if (korisnik.Username == null || korisnik.Username == "") return BadRequest("korisnik mora da ima username");
            if (korisnik.Username.Length > 50) return BadRequest("predugacko korisnicko ime");
            var vec_postoji_korisnik_sa_username = Context.Korisnici.Where(k => k.Username == korisnik.Username).FirstOrDefault();
            if (vec_postoji_korisnik_sa_username != null) return BadRequest("korisnicko ime zauzeto");

            if (korisnik.Password == null || korisnik.Password == "") return BadRequest("Korisnik mora da ima sifru");
            if (korisnik.Password.Length > 50) return BadRequest("predugacka lozinka");

            korisnik.Donacije = new List<Donacija>();
            korisnik.Slucajevi = new List<Slucaj>();
            Context.Korisnici.Add(korisnik);
            await Context.SaveChangesAsync();
            return Ok(korisnik);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpPut("izmeniusernamepassword")]
    public async Task<ActionResult> IzmeniUsernamePassword([FromQuery] int id_korisnika, [FromQuery] string? username, [FromQuery] string? password)
    {
        try
        {
            var stari_korisnik = await Context.Korisnici.FindAsync(id_korisnika);

            if (stari_korisnik == null) return NotFound("Trazeni korisnik ne postoji");
            if (null != username)
            {
                if (username.Length < 1 || username.Length > 50) return BadRequest("username mora biti izmedju 1 i 50 karaktera");
                var vec_postoji_korisnik_sa_username = Context.Korisnici.Where(k => username.CompareTo(k.Username) == 0).FirstOrDefault();
                if (vec_postoji_korisnik_sa_username != null) return BadRequest("korisnicko ime zauzeto");
                stari_korisnik.Username = username;
            }
            if (null != password)
            {
                if (password.Length < 1 || password.Length > 50) return BadRequest("password mora biti izmedju 1 i 50 karaktera");
                stari_korisnik.Password = password;
            }

            Context.Korisnici.Update(stari_korisnik);
            await Context.SaveChangesAsync();
            return Ok(stari_korisnik);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("uklonikorisnika/username/{username}")]
    public async Task<ActionResult> UkloniKorisnikaUsername(string username)
    {
        try
        {
            var korisnik = await Context.Korisnici.Where(k => k.Username == username).FirstOrDefaultAsync();
            if (korisnik == null) return NotFound("Trazeni korisnik svakako ne postoji");

            Context.Korisnici.Remove(korisnik);
            await Context.SaveChangesAsync();
            return Ok(korisnik);

        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    [HttpDelete("uklonikorisnika/{ID}")]
    public async Task<ActionResult> UkloniKorisnika(int ID)
    {
        try
        {
            var korisnik = await Context.Korisnici.FindAsync(ID);
            if (korisnik == null) return NotFound("Trazeni korisnik svakako ne postoji");

            Context.Korisnici.Remove(korisnik);
            await Context.SaveChangesAsync();
            return Ok(korisnik);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}