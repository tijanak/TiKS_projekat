



using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KomponentniTestovi
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProjectContext>();
            optionsBuilder.UseSqlServer("Server = (localdb)\\ProjekatTestiranje; Database = UdomljavanjeZivotinja");
            var _context = new ProjectContext(optionsBuilder.Options);
            KategorijaController k=new KategorijaController(_context);
            var actionresult=k.Preuzmi(-1) as OkObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(actionresult);
        }
    }
}