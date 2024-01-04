
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
            Korisnik[] korisnici = { new Korisnik { ID=1} };
            Slucaj[] slucajevi = { new Slucaj { ID=1} };
            controller = new DonacijaController(getDbContext(korisnici:korisnici,slucajevi:slucajevi));
            
        }
        [Test]
        public async Task PostTest1()
        {
            Donacija donacija = new Donacija();
            donacija.Kolicina = -1;
            var result=await controller.Dodaj(donacija,1,1);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        [Test]
        public async Task PostTest2()
        {
            Donacija donacija = new Donacija();
            donacija.Kolicina = 500;
            var result = await controller.Dodaj(donacija, -1, 1);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        [Test]
        public async Task PostTest3()
        {
            Donacija donacija = new Donacija();
            donacija.Kolicina = 500;
            var result = await controller.Dodaj(donacija, 1, -1);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        [Test]
        public async Task PostTest4()
        {
            Donacija donacija = new Donacija();
            donacija.Kolicina = 500;
            var result = await controller.Dodaj(donacija, 1, 1);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        [Test]
        public void Test1()
        {
            
            var actionresult = controller.Preuzmi(-1);
            Assert.IsInstanceOf<BadRequestObjectResult>(actionresult);
        }
    }
}
