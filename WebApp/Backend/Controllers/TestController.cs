namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    public TestContext Context { get; set; }
    public TestController(TestContext context)
    {
        Context = context;
    }
    [Route("Uzmi/{id}")]
    [HttpGet]
    public ActionResult Preuzmi(int id)
    {
        try
        {
            var test = Context.Testovi.Where(p => p.ID == id).FirstOrDefault();
            if (test != null)
            {
                return Ok(test);

            }
            else
            {
                return BadRequest("nema taj id");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
    [Route("Dodaj")]
    [HttpPost]
    public async Task<ActionResult> Dodaj([FromBody] Test test)
    {
        if (string.IsNullOrWhiteSpace(test.Nebitno))
        {
            return BadRequest("bitno je");
        }
        try
        {
            Context.Testovi.Add(test);
            await Context.SaveChangesAsync();
            return Ok($"U redu ID je {test.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}