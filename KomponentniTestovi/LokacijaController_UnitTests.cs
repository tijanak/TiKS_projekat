


namespace KomponentniTestovi
{
    [TestFixture]
    public class LokacijaController_UnitTests
    {
        LokacijaController controller;
        [OneTimeSetUp]
        public void Setup()
        {
            controller= new LokacijaController(getDbContext());
        }

        [Test]
        public void Test1()
        {
            
            var actionresult = controller.Preuzmi(-1);
            Assert.IsInstanceOf<BadRequestObjectResult>(actionresult);
        }
    }
    
}
