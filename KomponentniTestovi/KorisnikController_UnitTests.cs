
namespace KomponentniTestovi
{
    [TestFixture]
    [TestOf(typeof(KorisnikController))]
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
            Korisnik[] k = { new Korisnik { ID = 500 } };
            Slucaj[] slucajevi = { new Slucaj { ID = 250 }, new Slucaj { ID = 251 }, new Slucaj { ID = 220 } };
            Donacija[] donacije = { new Donacija { ID = 300 }, new Donacija { ID = 301 }, new Donacija { ID = 302 } };

            controller = new KorisnikController(getDbContext(korisnici:k,slucajevi:slucajevi, donacije:donacije));
        }

        [Order(1)]
        [TestCase("know","catsrule",null,null)]
        [TestCase("sqrll","password123",null,250)]
        [TestCase("howl","itsmycastle",300,null)]
        public async Task DodajKorisnika_AssertOk(string username, string password,int? id_donacije,int? id_slucaja)
        {
            Korisnik k = new Korisnik();
            k.Username = username;
            k.Password = password;
            var actionResult = await controller.DodajKorisnika(k,id_donacije,id_slucaja);
            if (actionResult.GetType() == typeof(OkObjectResult))
                Assert.That(0, Is.LessThan((int)((OkObjectResult)actionResult).Value));
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [Order(2)]
        [TestCase(null, null, null, null)]
        [TestCase("", null, null, null)]
        [TestCase("4GCuJ:#(_Cz]GighGv!3)Wav@Pqg&j]$xx@jWW&dM,#ugxQQcC1", null, null, null)]
        [TestCase("username","",null,null)]
        [TestCase("username", "4GCuJ:#(_Cz]GighGv!3)Wav@Pqg&j]$xx@jWW&dM,#ugxQQcC1", null,null)]
        [TestCase("username","password",600,null)]
        [TestCase("username", "password", null, 600)]
        [TestCase("know", "password", null, null)]
        public async Task DodajKorisnika_AssertNotOk(string username, string password, int? id_donacije, int? id_slucaja)
        {
            Korisnik k = new Korisnik();
            k.Username = username;
            k.Password = password;
            var actionResult = await controller.DodajKorisnika(k, id_donacije, id_slucaja);
            Assert.IsNotInstanceOf<OkObjectResult>(actionResult);
        }

        [Order(3)]
        [TestCase(500,250)]
        [TestCase(2,251)]
        public async Task DodeliSlucaj_AssertOk(int id_korisnika, int id_slucaja)
        {
            var actionResult = await controller.DodeliSlucaj(id_korisnika, id_slucaja);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [Order(4)]
        [TestCase(500, 300)]
        [TestCase(1,301)]
        [TestCase(1,302)]
        public async Task DodeliDonaciju_AssertOk(int id_korisnika, int id_donacije)
        {
            var actionResult = await controller.DodeliDonaciju(id_korisnika, id_donacije);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [Order(5)]
        [TestCase(2,251)]
        public async Task OduzmiSlucaj_AssertOk(int id_korisnika, int id_slucaja)
        {
            var actionResult = await controller.OduzmiSlucaj(id_korisnika, id_slucaja);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [Order(6)]
        [TestCase(1,301)]
        public async Task OduzmiDonaciju_AssertOk(int id_korisnika, int id_donacije)
        {
            var actionResult = await controller.OduzmiDonaciju(id_korisnika, id_donacije);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [Order(7)]
        [TestCase(2,"qwokka",null)]
        [TestCase(3, null, "novasifra")]
        public async Task IzmeniUsernamePassword_AssertOk(int id_korisnika,string?username, string? password)
        {
            var actionResult = await controller.IzmeniUsernamePassword(id_korisnika, username, password);
            
            TestContext.Out.WriteLine(((Korisnik)((OkObjectResult)actionResult).Value).Username);
            if (actionResult.GetType() != typeof(OkObjectResult)) Assert.Fail("Nije Ok");
                
            if(null!=username)
                Assert.That(username, Is.EqualTo(((Korisnik)((OkObjectResult)actionResult).Value).Username));

            Assert.NotNull(((Korisnik)((OkObjectResult)actionResult).Value).Password);
        }

        [Test]
        public async Task UkloniKorisnika_NotFound([Values(-1,-1000,15000)]int id_korisnika)
        {
            var actionResult = await controller.UkloniKorisnika(id_korisnika);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult);
        }

        [TestCase(500), Order(9)]
        public async Task UkloniKorisnika_Ok(int id_korisnika)
        {
            var actionResult = await controller.UkloniKorisnika(id_korisnika);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [TestCase(1), Description("korisnik sa id-jem 1 se dodaje dinamicki, kad se pokrene samo ovaj test nema ga u bazi")]
        [TestCase(-1)]
        [Explicit("trenutno")]
        public void PreuzmiKorisnika_NotFoundExplicit(int id)
        {
            var actionresult = controller.PreuzmiKorisnika(id);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionresult);
        }

        [TestCase(500)]
        [Order(8)]
        public void PreuzmiKorisnika_Ok(int id)
        {
            var actionresult = controller.PreuzmiKorisnika(id);
            Assert.IsInstanceOf<OkObjectResult>(actionresult);
        }

    }
}
