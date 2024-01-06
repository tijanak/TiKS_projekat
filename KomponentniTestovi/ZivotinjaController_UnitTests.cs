
namespace KomponentniTestovi
{
    [TestFixture]
    internal class ZivotinjaController_UnitTests
    {
        ZivotinjaController controller;

        [OneTimeSetUp]
        public void Setup()
        {
            Zivotinja[] z = { new Zivotinja{ID=58 } };
            Slucaj[] s = { new Slucaj { ID = 1 }, new Slucaj { ID=2} };
            controller = new ZivotinjaController(getDbContext(zivotinje: z, slucajevi:s));
        }

        [Test, Order(1)]
        public void PreuzmiZivotinju_AssertNotFound()
        {
            var actionResult = controller.PreuzmiZivotinju(1);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult);
        }

        [Test, Order(2)]
        public async Task DodajZivotinju_AssertOk()
        {
            Zivotinja z = new Zivotinja { Ime = "Tomica", Vrsta = "macka" };
            var actionResult = await controller.DodajZivotinju(z,1);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [Test, Order(3)]
        public void PreuzmiZivotinju_AssertOk()
        {
            var actionResult = controller.PreuzmiZivotinju(1);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [Test,Order(4)]
        public async Task IzmeniZivotinju_AssertOk()
        {
            var actionResult = await controller.IzmeniZivotinju(1,null,"lav",2);
            if (((OkObjectResult)actionResult).Value != null)
            {
                TestContext.Out.WriteLine(((Zivotinja)((OkObjectResult)actionResult).Value).Vrsta == "lav");
                TestContext.Out.WriteLine(((Zivotinja)((OkObjectResult)actionResult).Value).Slucaj.ID);
            }

            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [Test, Order(5)]
        public async Task IzmeniZivotinju2_AssertNotFound()
        {
            var actionResult = await controller.IzmeniZivotinju(1, null, null, 3); //Slucaj sa ID=3 ne postoji
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult);
        }
        [Test,Order(6)]
        public async Task ObrisiZivotinju_AssertOk()
        {
            var actionResult = await controller.ObrisiZivotinju(1);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }
        [Test,Order(7)]
        public void PreuzmiZivotinju_AssertNotFound2()
        {
            var actionResult = controller.PreuzmiZivotinju(1);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult);
        }
    }
}
