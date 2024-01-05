
using EntityFrameworkCoreMock;
using Microsoft.AspNetCore.Mvc;

namespace KomponentniTestovi
{
    [TestFixture]
    public class DonacijaController_UnitTests
    {
        
        DonacijaController controller;
        [OneTimeSetUp]
        public void Setup()
        {
            Korisnik[] korisnici = { new Korisnik { ID=1}, new Korisnik { ID = 2 } };
            Slucaj[] slucajevi = { new Slucaj { ID=1}, new Slucaj { ID = 2 } };
            Donacija[] donacije = { new Donacija { ID=21},  new Donacija { ID =20 },new Donacija { ID=30},new Donacija { ID=80},new Donacija { ID=90} };
            controller = new DonacijaController(getDbContext(donacije:donacije,korisnici:korisnici,slucajevi:slucajevi));
            
        }
        
        [Test]
        [TestCase(500,1,-1)]
        [TestCase(-int.MaxValue, 1, 2)]
        [TestCase(500, 0, 1)]
        public async Task PostTest1(int kolicina,int idSlucaj,int idKorisnik)
        {
            Donacija donacija = new Donacija();
            donacija.Kolicina = kolicina;
            var result = await controller.Dodaj(donacija, idKorisnik, idSlucaj);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        [Test,Pairwise]
        public async Task PostTest2([Values(500,int.MaxValue,1)]int kolicina, [Values(1,2)]int idSlucaj, [Values(1,2)]int idKorisnik)
        {
            Donacija donacija = new Donacija();
            donacija.Kolicina = kolicina;
            var result = await controller.Dodaj(donacija, idKorisnik, idSlucaj);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var id = (result as OkObjectResult).Value;
            Assert.IsNotNull(id);
            result = await controller.Obrisi((int)id);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        [Test]
        public async Task PostTest3()
        {
            var result = await controller.Dodaj(null, 1, 1);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        [Test]
        public async Task PostTest4()
        {
            Donacija donacija = new Donacija();
            var result = await controller.Dodaj(donacija, 1, 1);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        [Test]
        [TestCase(-1)]
        [TestCase(-int.MaxValue)]
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
        [TestCase(20)]
        [TestCase(21)]
        public async Task GetTest3(int id)
        {

            var actionresult = await controller.Preuzmi(id);
            Assert.IsInstanceOf<OkObjectResult>(actionresult);
        }

        [Test]
        [TestCase(30)]
        public async Task DeleteTest1(int id)
        {
            var actionresult = await controller.Obrisi(id);
            Assert.IsInstanceOf<OkObjectResult>(actionresult);
            var result = await controller.Preuzmi(id);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
        [Test]
        [TestCase(-int.MaxValue)]

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
        [TestCase(80,null,null,2)]
        [TestCase(80, null, 1, 2)]
        [TestCase(90, 500, null, 2)]
        [TestCase(90, null, null, null)]
        public async Task UpdateTest1(int id,int? kolicina,int? idKorisnika,int? idSlucaja)
        {
            var result = await controller.Azuriraj(id, kolicina,idKorisnika,idSlucaja);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var uBazi=await controller.Preuzmi(id);
            Assert.IsInstanceOf<OkObjectResult>(uBazi);
            var donacija = ((uBazi as OkObjectResult).Value) as Donacija;
            Assert.Multiple(() =>
            {
                Assert.IsTrue(kolicina == null || donacija.Kolicina == kolicina);
                Assert.IsTrue(idKorisnika == null || donacija.Korisnik.ID == idKorisnika);
                Assert.IsTrue(idSlucaja == null || donacija.Slucaj.ID == idSlucaja);
            });
        }
        [Test]

        [TestCase(80, null, null, 3)]
        [TestCase(80, null, 5, 2)]
        [TestCase(90, -50, null, 2)]
        [TestCase(90, null, -int.MaxValue, null)]
        [TestCase(-1, null, null, null)]
        public async Task UpdateTest2(int id, int? kolicina, int? idKorisnika, int? idSlucaja)
        {
            var result = await controller.Azuriraj(id, kolicina, idKorisnika, idSlucaja);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        [Test]
        [TestCase(40,50,null,null)]
        public async Task UpdateTest3(int id, int? kolicina, int? idKorisnika, int? idSlucaja)
        {
            var result = await controller.Azuriraj(id, kolicina, idKorisnika, idSlucaja);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
}
