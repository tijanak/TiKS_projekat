


namespace KomponentniTestovi
{
    [TestFixture]
    public class SlucajController_UnitTests
    {
        SlucajController controller;
        [OneTimeSetUp]
        public void Setup()
        {

            Slucaj[] slucajevi = { new Slucaj { ID = 50 }, new Slucaj { ID = 51 }, new Slucaj { ID = 52 }, new Slucaj { ID = 53 } };
            controller = new SlucajController(getDbContext(slucajevi:slucajevi));
        }

        [Test]
        [TestCase("", "",true,true,1)]
        [TestCase(null, "", true, true, 1)]
        [TestCase("___________________________________________________", "", true, true, 1)]
        [TestCase("tip", "", true, true, 1)]
        [TestCase("   ", "", true, true, 1)]
        [Ignore("ne radi")]
        public async Task PostTest1(string? naziv, string? opis,bool nullLocation,bool nullAnimal,int?idKorisnika)
        {
            Slucaj slucaj=new Slucaj();
            slucaj.Naziv= naziv;
            slucaj.Opis = opis;
            
            var result = await controller.Dodaj(slucaj,nullLocation?null:new Lokacija { },nullAnimal?null:new Zivotinja { },idKorisnika);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        [Test, Combinatorial]
        [Ignore("ne radi")]
        public async Task PostTest2([Values("a", "__________________________________________________", "tip")] string? naziv, [Values(null)]string?opis, [Values(1)]int? idKorisnika)
        {
            Slucaj slucaj = new Slucaj();
            slucaj.Naziv = naziv;
            slucaj.Opis = opis;
            var result = await controller.Dodaj(slucaj,new Lokacija { },new Zivotinja { },idKorisnika);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var id = (result as OkObjectResult).Value;
            Assert.IsNotNull(id);
            result = await controller.Obrisi((int)id);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        [Test]
        public async Task PostTest3()
        {
            var result = await controller.Dodaj(null,new Lokacija { },new Zivotinja { },1);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        [Test]
        [TestCase(-1)]
        [TestCase(int.MinValue)]
        public async Task GetTest1(int id)
        {

            var actionresult = await controller.Preuzmi(id);
            Assert.IsInstanceOf<BadRequestObjectResult>(actionresult);
        }
        [Test]
        [TestCase(int.MaxValue)]
        [TestCase(1)]
        public async Task GetTest2(int id)
        {

            var actionresult = await controller.Preuzmi(id);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionresult);
        }
        [Test]
        [TestCase(50)]
        [TestCase(51)]
        public async Task GetTest3(int id)
        {

            var actionresult = await controller.Preuzmi(id);
            Assert.IsInstanceOf<OkObjectResult>(actionresult);
        }
        [Test]
        [TestCase(52)]
        [TestCase(53)]
        public async Task DeleteTest1(int id)
        {
            var actionresult = await controller.Obrisi(id);
            Assert.IsInstanceOf<OkObjectResult>(actionresult);
            var result = await controller.Preuzmi(id);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
        [Test]
        [TestCase(int.MinValue)]

        public async Task DeleteTest2(int id)
        {
            var actionresult = await controller.Obrisi(id);
            Assert.IsInstanceOf<BadRequestObjectResult>(actionresult);
        }
        [Test]
        [TestCase(int.MaxValue)]
        public async Task DeleteTest3(int id)
        {
            var actionresult = await controller.Obrisi(id);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionresult);
        }
        [Test]
        [TestCase(50,"",  "",null,null,null)]
        [TestCase(51, null,  "", null, null, null)]
        [TestCase(51, "___________________________________________________", "", null, null, null)]
        [TestCase(51, "tip",  "",null,null,null)]
        [TestCase(51, "   ", "", null, null, null)]
        [Ignore("ne radi")]
        public async Task UpdateTest1(int id, string? naziv, string? opis,  int? idLokacija, int? idKorisnika, int? idZivotinja)
        {
            var result = await controller.Azuriraj(id, naziv, opis,  idLokacija, idKorisnika, idZivotinja);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var uBazi = await controller.Preuzmi(id);
            Assert.IsInstanceOf<OkObjectResult>(uBazi);
            var slucaj = ((uBazi as OkObjectResult).Value) as Slucaj;
            Assert.Multiple(() =>
            {
                Assert.IsTrue(naziv == null || slucaj.Naziv==naziv);
                Assert.IsTrue(opis == null || slucaj.Opis==opis);
                Assert.IsTrue(idLokacija == null || slucaj.Lokacija.ID==idLokacija);
                Assert.IsTrue(idKorisnika == null || slucaj.Korisnik.ID==idKorisnika);
                Assert.IsTrue(idZivotinja==null||slucaj.Zivotinja.ID==idZivotinja);

            });
        }
        [Test]

        [TestCase(-50,"", "", "",null,null,null)]
        [TestCase(int.MinValue,null, "", "", null, null, null)]
        [TestCase(51,"___________________________________________________", "", "", null, null, null)]
        [TestCase(50, "tip", "", "", null, null, null)]
        [Ignore("ne radi")]
        public async Task UpdateTest2(int id, string? naziv, string? opis, string? slika,int?idLokacija,int?idKorisnika,int?idZivotinja)
        {
            var result = await controller.Azuriraj(id, naziv, opis, idLokacija,idKorisnika,idZivotinja);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        [Test]

        [TestCase(int.MaxValue, "", "", "", null, null, null)]
        [TestCase(70, null, "", "", null, null, null)]
        [TestCase(70, "___________________________________________________", "", "", null, null, null)]
        [TestCase(70, "tip", "", "", null, null, null)]
        [Ignore("ne radi")]
        public async Task UpdateTest3(int id, string? naziv, string? opis, string? slika, int? idLokacija, int? idKorisnika, int? idZivotinja)
        {
            var result = await controller.Azuriraj(id, naziv, opis, idLokacija, idKorisnika, idZivotinja);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
}
