
namespace KomponentniTestovi
{
    [TestFixture]
    internal class ZivotinjaController_UnitTests
    {
        ZivotinjaController controller;

        [OneTimeSetUp]
        public void Setup()
        {
            Zivotinja[] z = { new Zivotinja{ID=1 } };
            Slucaj[] s = { new Slucaj { ID = 1 }, new Slucaj { ID=2} };
            controller = new ZivotinjaController(getDbContext(zivotinje: z, slucajevi:s));
        }

        [Test, Order(1)]
        public void PreuzmiZivotinju_Test()
        {
            var actionResult = controller.PreuzmiZivotinju(2);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult);
        }

        [Test, Order(2)]
        public async Task DodajZivotinju_Test()
        {
            Zivotinja z = new Zivotinja { ID = 2, Ime = "Tomica", Vrsta = "macka" };
            var actionResult = await controller.DodajZivotinju(z,1);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [Test, Order(3)]
        public void PreuzmiZivotinju_Test2()
        {
            var actionResult = controller.PreuzmiZivotinju(2);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [Test,Order(4)]
        public async Task IzmeniZivotinju_Test()
        {
            var actionResult = await controller.IzmeniZivotinju(2,null,"lav",2);
            if (((OkObjectResult)actionResult).Value != null)
            {
                TestContext.Out.WriteLine(((Zivotinja)((OkObjectResult)actionResult).Value).Vrsta == "lav");
                TestContext.Out.WriteLine(((Zivotinja)((OkObjectResult)actionResult).Value).Slucaj.ID);
            }

            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [Test, Order(5)]
        public async Task IzmeniZivotinju2_Test()
        {
            var actionResult = await controller.IzmeniZivotinju(2, null, null, 3); //Slucaj sa ID=3 ne postoji
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult);
        }
        [Test,Order(6)]
        public async Task ObrisiZivotinju_Test()
        {
            var actionResult = await controller.ObrisiZivotinju(2);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }
        [Test,Order(7)]
        public void PreuzmiZivotinju_Test3()
        {
            var actionResult = controller.PreuzmiZivotinju(2);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult);
        }
    }
}
