



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
            controller = new KategorijaController(_context);
        }

        [Test]
        public void Test1()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProjectContext>();
            optionsBuilder.UseSqlServer("Server = (localdb)\\ProjekatTestiranje; Database = UdomljavanjeZivotinja");
            var _context = new ProjectContext(optionsBuilder.Options);
            var actionresult=controller.Preuzmi(-1);
            Assert.IsInstanceOf<BadRequestObjectResult>(actionresult);
        }
    }
}