

using EntityFrameworkCoreMock;
using Microsoft.AspNetCore.Mvc;

namespace KomponentniTestovi
{
    [TestFixture]
    public class SlucajController_UnitTests
    {
        SlucajController controller;
        [OneTimeSetUp]
        public void Setup()
        {
            controller = new SlucajController(getDbContext());
        }

        [Test]
        public void Test1()
        {
            
            var actionresult = controller.Preuzmi(-1);
            Assert.IsInstanceOf<BadRequestObjectResult>(actionresult);
        }
    }
}
