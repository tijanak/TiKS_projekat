
namespace KomponentniTestovi
{
    [TestFixture]
    internal class KorisnikController_UnitTests
    {
        KorisnikController controller;
        [OneTimeSetUp]
        public void Setup()
        {
            //string connectionString;
            //connectionString = "Server = (localdb)\\ProjekatTestiranje; Database = UdomljavanjeZivotinja";
            //var optionsBuilder = new DbContextOptionsBuilder<ProjectContext>();
            //optionsBuilder.UseSqlServer(connectionString);
            //var _context = new ProjectContext(optionsBuilder.Options);
            controller = new KorisnikController(getDbContext());
        }

        [Test]
        public async Task DodajKorisnika_Test()
        {
            //
            Korisnik k = new Korisnik();
            k.ID = 0;
            k.Username = "username";
            k.Password = "password";
            //Slucaj s = new Slucaj();
            //s.Opis = "opis ovog slucaja";
            //s.Naziv = "necete verovati";
            //k.Slucajevi=new List<Slucaj>();
            //k.Slucajevi.Add(s);
            var actionresult = await controller.DodajKorisnika(k);
            Assert.IsInstanceOf<OkObjectResult>(actionresult);
        }

        [Test]
        public void PreuzmiKorisnika_Test()
        {
            int id = 0;
            var actionresult = controller.PreuzmiKorisnika(0);
            Assert.IsInstanceOf<OkObjectResult>(actionresult);
        }

        [Test]
        public void IzmeniKorisnika_Test()
        {
            
        }

        [Test]
        public void ObrisiKorisnika_Test()
        {

        }
    }
}
