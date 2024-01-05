


namespace KomponentniTestovi
{
    [TestFixture]
    public class LokacijaController_UnitTests
    {
        LokacijaController controller;
        [OneTimeSetUp]
        public void Setup()
        {
            Slucaj[] slucajevi = { new Slucaj { ID = 1 }, new Slucaj { ID = 2 } };
            Lokacija[] lokacije = { new Lokacija { ID=50},new Lokacija { ID=51}, new Lokacija { ID = 52 }, new Lokacija { ID = 53 } };
            controller = new LokacijaController(getDbContext(slucajevi:slucajevi,lokacije:lokacije));
        }

        [Test]
        [TestCase(double.MaxValue, double.MinValue, 1)]
        [TestCase(-180, -90, 1)]
        [TestCase(0, 180, 1)]
        [TestCase(-90.1, -180, 1)]
        [TestCase(-90, -180, 3)]
        public async Task PostTest1(double latituda, double longituda, int idSlucaj)
        {
            Lokacija lokacija = new Lokacija();
            lokacija.Latitude = latituda;
            lokacija.Longitude = longituda;
            var result = await controller.Dodaj(lokacija, idSlucaj);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        [Test, Combinatorial]
        public async Task PostTest2([Values(-90, 90, 0,89.9,-89.9)] double latituda, [Values(-180, -179.9,0,179.9)] double longituda, [Values(1, 2)] int idSlucaj)
        {
            Lokacija lokacija = new Lokacija();
            lokacija.Latitude = latituda;
            lokacija.Longitude = longituda;
            var result = await controller.Dodaj(lokacija, idSlucaj);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var id = (result as OkObjectResult).Value;
            Assert.IsNotNull(id);
            result = await controller.Obrisi((int)id);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        [Test]
        public async Task PostTest3()
        {
            var result = await controller.Dodaj(null, 1);
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
        [TestCase(50, -89.9, null, 2)]
        [TestCase(50, null, 179.9, 2)]
        [TestCase(51, 90, null, 2)]
        [TestCase(51, 90, -180, 1)]
        [TestCase(51, null, null, null)]
        public async Task UpdateTest1(int id, double? latituda, double? longituda, int? idSlucaj)
        {
            var result = await controller.Azuriraj(id, longituda,latituda,idSlucaj);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var uBazi = await controller.Preuzmi(id);
            Assert.IsInstanceOf<OkObjectResult>(uBazi);
            var lokacija = ((uBazi as OkObjectResult).Value) as Lokacija;
            Assert.Multiple(() =>
            {
                Assert.IsTrue(latituda == null || lokacija.Latitude == latituda);
                Assert.IsTrue(longituda == null || lokacija.Longitude == longituda);
                Assert.IsTrue(idSlucaj == null || lokacija.Slucaj.ID == idSlucaj);
            });
        }
        [Test]

        [TestCase(50, null, -int.MaxValue, null)]
        [TestCase(51, null, 180, null)]
        [TestCase(51, null, null, -1)]
        [TestCase(51, -90.1, null, null)]
        [TestCase(51, null, -180.1, null)]
        [TestCase(51, 90.1, null, null)]
        public async Task UpdateTest2(int id, double? latituda, double? longituda, int? idSlucaj)
        {
            var result = await controller.Azuriraj(id, longituda, latituda, idSlucaj);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        [Test]

        [TestCase(int.MaxValue, null, null, 1)]
        [TestCase(int.MinValue, 0, 0, 2)]
        [TestCase(200, null, 5, 2)]
        public async Task UpdateTest3(int id, double? latituda, double? longituda, int? idSlucaj)
        {
            var result = await controller.Azuriraj(id, longituda, latituda, idSlucaj);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
    
}
