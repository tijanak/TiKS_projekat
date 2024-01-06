using System.Formats.Tar;

namespace Backend.Controllers;
[ApiController]
[Route("[controller]")]
public class KorisnikController : ControllerBase
{
    public ProjectContext Context { get; set; }
    private SlucajController slucaj_controller;
    private DonacijaController donacija_controller;
    public KorisnikController(ProjectContext Context)
    {
        this.Context = Context;
        slucaj_controller=new SlucajController(Context);
        donacija_controller=new DonacijaController(Context);
    }

    [HttpGet("preuzmikorisnika/{id}")]
    public ActionResult PreuzmiKorisnika(int id)
    {
        try
        {
            var korisnik = Context.Korisnici.Where(k => k.ID == id).FirstOrDefault();
            if (korisnik != null) return Ok(korisnik);
            return BadRequest("Trazeni korisnik ne postoji");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }


    [HttpPost("dodajkorisnika")]
    public async Task<ActionResult> DodajKorisnika([FromQuery] Korisnik korisnik, [FromQuery]int? id_donacije, [FromQuery]int? id_slucaja)
    {
        try
        {
            if(korisnik.Username==null||korisnik.Username=="") return BadRequest("korisnik mora da ima username");
            if(korisnik.Username.Length>50) return BadRequest("predugacko korisnicko ime");
            var vec_postoji_korisnik_sa_username=Context.Korisnici.Where(k=>k.Username==korisnik.Username).FirstOrDefault();
            if(vec_postoji_korisnik_sa_username!=null) return BadRequest("korisnicko ime zauzeto");
            
            if(korisnik.Password==null||korisnik.Password=="") return BadRequest("Korisnik mora da ima sifru");
            if(korisnik.Password.Length>50) return BadRequest("predugacka lozinka");
            
            if(null==korisnik.Donacije) korisnik.Donacije=new List<Donacija>();
            if(null==korisnik.Slucajevi) korisnik.Slucajevi=new List<Slucaj>();
            Context.Korisnici.Add(korisnik);
            await Context.SaveChangesAsync();
            if(null!=id_donacije) {
                var don_actionResult=await DodeliDonaciju(korisnik.ID,(int)id_donacije); 
                if(don_actionResult.GetType()!=typeof(OkObjectResult)) return don_actionResult;
            }
            if(null!=id_slucaja) return await DodeliSlucaj(korisnik.ID,(int)id_slucaja);
            return Ok(korisnik.ID);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPut("dodelislucaj")]
    public async Task<ActionResult> DodeliSlucaj([FromQuery]int id_korisnika, [FromQuery]int id_slucaja){
        try{
                
            var actionResult = PreuzmiKorisnika(id_korisnika);
            if(actionResult.GetType()!=typeof(OkObjectResult)) return NotFound("Nema takvog korisnika");
            var actionResult_slucaj = slucaj_controller.Preuzmi(id_slucaja);
            if(actionResult_slucaj.GetType()!=typeof(OkObjectResult)) return NotFound("Nema takvog slucaja");

            var korisnik = ((OkObjectResult)actionResult).Value;
            var slucaj = ((OkObjectResult)actionResult_slucaj).Value;
            if(korisnik==null) return NotFound("Nema takvog korisnika");
            if(slucaj==null) return NotFound("Nema takvog slucaja");
            
            Korisnik k = (Korisnik)korisnik;
            Slucaj s = (Slucaj)slucaj;
            if(k.Slucajevi==null) k.Slucajevi=new List<Slucaj>();
            for(int i=0;i<k.Slucajevi.Count;i++)
                if(k.Slucajevi[i].ID==id_slucaja) return BadRequest("Korisniku je vec dodeljen zeljeni slucaj");
            k.Slucajevi.Add(s);
            Context.Korisnici.Update(k);

            s.Korisnik=k;
            Context.Slucajevi.Update(s);

            await Context.SaveChangesAsync();
            return Ok(k);
            }
        catch(Exception e){
            return BadRequest(e);
        }
    }

    [HttpPut("dodelidonaciju")]
    public async Task<ActionResult> DodeliDonaciju([FromQuery]int id_korisnika,[FromQuery]int id_donacije){
        try{
            var actionResult = PreuzmiKorisnika(id_korisnika);
            if(actionResult.GetType()!=typeof(OkObjectResult)) return NotFound("Nema takvog korisnika");
            var actionResult_donacija = await donacija_controller.Preuzmi(id_donacije);
            if(actionResult_donacija.GetType()!=typeof(OkObjectResult)) return NotFound("Nema takve donacije");
            

            var korisnik = ((OkObjectResult)actionResult).Value;
            var donacija = ((OkObjectResult)actionResult_donacija).Value;
            if(korisnik==null) return NotFound("Nema takvog korisnika");
            if(donacija==null) return NotFound("Nema takve donacije");
            
            Korisnik k = (Korisnik)korisnik;
            Donacija d = (Donacija)donacija;
            if(k.Donacije==null) k.Donacije=new List<Donacija>();
            for(int i=0;i<k.Donacije.Count;i++)
                if(k.Donacije[i].ID==id_donacije) return BadRequest("Korisniku je vec dodeljena zeljena donacija");
            k.Donacije.Add(d);
            Context.Korisnici.Update(k);

            d.Korisnik=k;
            Context.Donacije.Update(d);

            await Context.SaveChangesAsync();
            return Ok(k);
        }
        catch(Exception e){
            return BadRequest(e);
        }
    }
    
    [HttpPut("oduzmidonaciju")]
    public async Task<ActionResult> OduzmiDonaciju([FromQuery]int id_korisnika,[FromQuery]int id_donacije){
        try{
            var actionResult = PreuzmiKorisnika(id_korisnika);
            if(actionResult.GetType()!=typeof(OkObjectResult)) return NotFound("Nema takvog korisnika");
            
            var korisnik = ((OkObjectResult)actionResult).Value;
            if(korisnik==null) return NotFound("Nema takvog korisnika");
            Korisnik k = (Korisnik)korisnik;
            if(k.Donacije==null) return BadRequest("Korisnik nije napravio tu donaciju");
            int i;
            for(i=0;i<k.Donacije.Count;i++)
                if(k.Donacije[i].ID==id_donacije) break;
            if(i==k.Donacije.Count) return BadRequest("Korisnik nije napravio tu donaciju");
            
            k.Donacije.RemoveAt(i);
            await donacija_controller.Obrisi(id_donacije);

            Context.Korisnici.Update(k);
            await Context.SaveChangesAsync();
            return Ok(korisnik);
        }
        catch(Exception e){
            return BadRequest(e);
        }        
    }

    [HttpPut("oduzmislucaj")]
    public async Task<ActionResult> OduzmiSlucaj([FromQuery]int id_korisnika,[FromQuery]int id_slucaja){
        try{
            var actionResult = PreuzmiKorisnika(id_korisnika);
            if(actionResult.GetType()!=typeof(OkObjectResult)) return NotFound("Nema takvog korisnika");
            
            var korisnik = ((OkObjectResult)actionResult).Value;
            if(korisnik==null) return NotFound("Nema takvog korisnika");
            Korisnik k = (Korisnik)korisnik;
            if(k.Slucajevi==null) return BadRequest("Korisnik nije napravio tu donaciju");
            int i;
            for(i=0;i<k.Slucajevi.Count;i++)
                if(k.Slucajevi[i].ID==id_slucaja) break;
            if(i==k.Slucajevi.Count) return BadRequest("Korisnik nije napravio tu donaciju");
            
            k.Slucajevi.RemoveAt(i);
            await slucaj_controller.Obrisi(id_slucaja);

            Context.Korisnici.Update(k);
            await Context.SaveChangesAsync();
            return Ok(korisnik);
        }
        catch(Exception e){
            return BadRequest(e);
        }        
    }

    [HttpPut("izmeniusernamepassword")]
    public async Task<ActionResult> IzmeniUsernamePassword([FromQuery] int id_korisnika,string? username,string? password)
    {
        try
        {
            var stari_korisnik = await Context.Korisnici.FindAsync(id_korisnika);
        
            if(stari_korisnik==null) return NotFound("Trazeni korisnik ne postoji");

            if(null==username || username.Length<1||username.Length>50) return BadRequest("username mora biti izmedju 1 i 50 karaktera");
            var vec_postoji_korisnik_sa_username=Context.Korisnici.Where(k=>k.Username==username).FirstOrDefault();
            if(vec_postoji_korisnik_sa_username!=null) return BadRequest("korisnicko ime zauzeto");
            stari_korisnik.Username = username;
            
            if(null==password || password.Length<1||password.Length>50) return BadRequest("password mora biti izmedju 1 i 50 karaktera");
            stari_korisnik.Password = password;

            Context.Korisnici.Update(stari_korisnik);
            await Context.SaveChangesAsync();
            return Ok(stari_korisnik);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpDelete("uklonikorisnika/{ID}")]
    public async Task<ActionResult<Korisnik>> UkloniKorisnika(int ID){
    try
        {
            var korisnik = await Context.Korisnici.FindAsync(ID);
            if(korisnik==null) return NotFound("Trazeni korisnik svakako ne postoji");
            
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