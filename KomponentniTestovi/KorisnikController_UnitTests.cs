
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
        [TestCase("know","catsrule")]
        [TestCase("sqrll","password123")]
        [TestCase("howl","itsmycastle")]
        [TestCase("username", "password")]
        public async Task DodajKorisnika_AssertOk(string username, string password)
        {
            Korisnik k = new Korisnik();
            k.Username = username;
            k.Password = password;
            var actionResult = await controller.DodajKorisnika(k);
            if (actionResult.GetType() == typeof(OkObjectResult))
                Assert.That(0, Is.LessThan(((Korisnik)((OkObjectResult)actionResult).Value).ID));
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [Order(2)]
        [TestCase(null, null)]
        [TestCase("", null)]
        [TestCase("4GCuJ:#(_Cz]GighGv!3)Wav@Pqg&j]$xx@jWW&dM,#ugxQQcC1", null)]
        [TestCase("username", "")]
        [TestCase("username", "4GCuJ:#(_Cz]GighGv!3)Wav@Pqg&j]$xx@jWW&dM,#ugxQQcC1")]
        [TestCase("username", "password")]
        [TestCase("know", "password")]
        public async Task DodajKorisnika_AssertNotOk(string username, string password)
        {
            Korisnik k = new Korisnik();
            k.Username = username;
            k.Password = password;
            var actionResult = await controller.DodajKorisnika(k);
            Assert.IsNotInstanceOf<OkObjectResult>(actionResult);
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
