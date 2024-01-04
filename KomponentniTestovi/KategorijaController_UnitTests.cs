



using Microsoft.AspNetCore.Mvc;

namespace KomponentniTestovi
{
    [TestFixture]
    public class KategorijaController_UnitTests
    {
        KategorijaController controller;
        [OneTimeSetUp]
        public void Setup()
        {
            controller = new KategorijaController(getDbContext());
        }

        [Test]
        public void Test1()
        {
            
            var actionresult=controller.Preuzmi(-1);
            Assert.IsInstanceOf<BadRequestObjectResult>(actionresult);
        }
    }
}