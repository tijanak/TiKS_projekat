

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
            string connectionString;
            if (ConfigurationManager.ConnectionStrings["db_connection_string"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["db_connection_string"].ConnectionString;
            }
            else
            {
                throw new Exception("Connection string not set");
            }
            var optionsBuilder = new DbContextOptionsBuilder<ProjectContext>();
            optionsBuilder.UseSqlServer(connectionString);
            var _context = new ProjectContext(optionsBuilder.Options);
            controller = new SlucajController(_context);
        }

        [Test]
        public void Test1()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProjectContext>();
            optionsBuilder.UseSqlServer("Server = (localdb)\\ProjekatTestiranje; Database = UdomljavanjeZivotinja");
            var _context = new ProjectContext(optionsBuilder.Options);
            var actionresult = controller.Preuzmi(-1);
            Assert.IsInstanceOf<BadRequestObjectResult>(actionresult);
        }
    }
}
