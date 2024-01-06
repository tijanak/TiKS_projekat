




namespace KomponentniTestovi
{
    [TestFixture]
    public class KategorijaController_UnitTests
    {
        KategorijaController controller;
        [OneTimeSetUp]
        public void Setup()
        {
            Slucaj[] slucajevi = { new Slucaj { ID = 1 }, new Slucaj { ID = 2 } };
            Kategorija[] kategorije = { new Kategorija { ID = 100, Prioritet = 100 }, new Kategorija { ID=101,Prioritet=5} };
            controller = new KategorijaController(getDbContext(slucajevi:slucajevi,kategorije:kategorije));
        }

        [Test]
        [TestCase("", 1)]
        [TestCase(null, 1)]
        [TestCase("___________________________________________________", 1)]
        [TestCase("tip", 100)]
        [TestCase("   ", 1)]
        public async Task PostTest1(string? tip,double prioritet )
        {
            Kategorija kategorija= new Kategorija();
            kategorija.Tip = tip;
            kategorija.Prioritet=prioritet;
            var result = await controller.Dodaj(kategorija);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        [Test, Combinatorial]
        public async Task PostTest2([Values("a", "__________________________________________________","tip")] string? tip, [Values(double.MinValue,double.MaxValue,1,-1,0)] double prioritet)
        {
            Kategorija kategorija = new Kategorija();
            kategorija.Tip = tip;
            kategorija.Prioritet = prioritet;
            var result = await controller.Dodaj(kategorija);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var id = (result as OkObjectResult).Value;
            Assert.IsNotNull(id);
            result = await controller.Obrisi((int)id);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        [Test]
        public async Task PostTest3()
        {
            var result = await controller.Dodaj(null);
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
        [TestCase(100)]
        [TestCase(101)]
        public async Task GetTest3(int id)
        {

            var actionresult = await controller.Preuzmi(id);
            Assert.IsInstanceOf<OkObjectResult>(actionresult);
        }
    }
}