
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
        [TestCase(2,302)]
        public async Task DodeliSlucaj_AssertOk(int id_korisnika, int id_slucaja)
        {
            var actionResult = await controller.DodeliSlucaj(id_korisnika, id_slucaja);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [Order(4)]
        [TestCase(500, 300)]
        [TestCase(1,301)]
        public async Task DodeliDonaciju_AssertOk(int id_korisnika, int id_donacije)
        {
            var actionResult = await controller.DodeliDonaciju(id_korisnika, id_donacije);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [Order(5)]
        [TestCase(2,302)]
        public async Task OduzmiSlucaj_AssertOk(int id_korisnika, int id_slucaja)
        {
            var actionResult = await controller.OduzmiSlucaj(id_korisnika, id_slucaja);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [Order(6)]
        [TestCase(1,301)]
        public async Task OduzmiDonaciju_AssertOk(int id_korisnika, int id_donacije)
        {
            var actionResult = await controller.OduzmiSlucaj(id_korisnika, id_donacije);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        //[TestCase(1)]
        //public void PreuzmiKorisnika_AssertOk(int id)
        //{
        //    var actionresult = controller.PreuzmiKorisnika(id);
        //    Assert.IsInstanceOf<OkObjectResult>(actionresult);
        //}

        //[Test]
        //public void IzmeniKorisnika_Test()
        //{

        //}

        //[Test]
        //public void ObrisiKorisnika_Test()
        //{

        //}
    }
}
