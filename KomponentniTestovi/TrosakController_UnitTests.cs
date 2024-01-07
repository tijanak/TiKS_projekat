using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomponentniTestovi
{
    [TestFixture]
    internal class TrosakController_UnitTests
    {
        TrosakController controller;
        [OneTimeSetUp]
        public void Setup()
        {
            Trosak[] t = { new Trosak { ID=2024, Namena="operacija",Kolicina=10000} };
            Slucaj[] s = { new Slucaj { ID = 999 } };
            controller = new TrosakController(getDbContext(troskovi:t, slucajevi:s));
        }

        [Order(2)]
        [TestCase(2024)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void PreuzmiTrosak_AssertOk(int id_troska)
        {
            var actionResult = controller.PreuzmiTrosak(id_troska);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [Order(1)]
        [Repeat(3)]
        [TestCase("lekovi", 5000, 999)]
        [TestCase("hrana", 3000, 999)]
        [TestCase("igracke", 2000, 999)]
        [TestCase("hrana", 1000, 999)]
        public async Task DodajTrosak_AssertOk(string namena,int cena, int idSlucaja)
        {
            Trosak t = new Trosak { Namena=namena, Kolicina=cena};
            var actionResult = await controller.DodajTrosak(t,idSlucaja);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [Test]
        [Sequential]
        public async Task DodajTrosak_AssertNotOk([Values("","valid","valid")] string namena,[Values(200,99,200)]int cena, [Values(999, 999, 3)]int idSlucaja)
        {
            Trosak t = new Trosak { Namena = namena, Kolicina = cena };
            var actionResult = await controller.DodajTrosak(t, idSlucaja);
            Assert.IsNotInstanceOf<OkObjectResult>(actionResult);
        }

        [Order(3)]
        [Test]
        [Sequential]
        [Retry(2)]
        [MaxTime(2000)]
        public async Task IzmeniTrosak_AssertOk([Random(200, 10000, 4)] int cena, [Range(1,4,1)] int id_troska)
        {
            var actionResult = await controller.IzmeniTrosak(id_troska, null, cena, null);
            TestContext.Out.WriteLine($"{cena} + {id_troska}");
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [TestCase(99,null, 2024,null)]
        [TestCase(null, "", 2024, null)]
        [TestCase(null, null, 2023, null)]
        [TestCase(200, null, 2024, -1)]
        public async Task IzmeniTrosak_AssertNotOk_ID2024(int? cena, string? namena, int id_troska, int? id_slucaja)
        {
            var actionResult = await controller.IzmeniTrosak(id_troska, namena, cena, id_slucaja);
            TestContext.Out.WriteLine($"{cena} + {namena} +{id_troska}+{id_slucaja}");
            Assert.IsNotInstanceOf<OkObjectResult>(actionResult);
        }

        [Test]
        public async Task ObrisiDodateTroskove_AssertOk([Range(1,4,1)]int id)
        {
            var actionResult = await controller.UkloniTrosak(id);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }
    }
}
