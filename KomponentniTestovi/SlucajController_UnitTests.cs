


using Backend.Models;

namespace KomponentniTestovi
{
    [TestFixture]
    public class SlucajController_UnitTests
    {
        SlucajController controller;
        [OneTimeSetUp]
        public void Setup()
        {
            Zivotinja[] zivotinje = { new Zivotinja { ID=3} };
            Lokacija[] lokacije = { new Lokacija { ID=2} };
            Korisnik[] korisnici = { new Korisnik { ID=1} };
            Kategorija[] kategorije = { new Kategorija { ID=1}, new Kategorija { ID = 2 }, new Kategorija { ID = 3 } };
            Slucaj[] slucajevi = { new Slucaj { ID = 50,Slike=new List<string> { "nekaSlika.jpg"} }, new Slucaj { ID = 51 }, new Slucaj { ID = 52 }, new Slucaj { ID = 53 } };
            controller = new SlucajController(getDbContext(lokacije:lokacije,zivotinje:zivotinje,korisnici:korisnici,slucajevi:slucajevi,kategorije:kategorije));
        }

        [Test]
        [TestCase("naziv", "opis",1, new int[] { },new string[] { "slika.jpg" })]
        [TestCase("___________________________________________________", "opis", 1, new int[] { 1,3},new string[] { "slika.jpg" })]
        [TestCase("naziv", "opis", 1, new int[] { 1,3}, new string[] { "slika.jpg"," " })]
        [TestCase("", "opis", 1, new int[] { }, new string[] { "slika.jpg" })]
        [TestCase("naziv", "", 1, new int[] { }, new string[] { "slika.jpg" })]
        [TestCase("naziv", "______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________", 1, new int[] { 1, 3 }, new string[] { "slika.jpg" })]
        [TestCase("naziv", "opis", 1, new int[] { }, new string[] { "slika.jpg" })]
        [TestCase("naziv", "opis", 1, new int[] { 1, 3,5 }, new string[] { "slika.jpg" })]
        [TestCase("naziv", "opis", 100, new int[] { 1, 3 }, new string[] { "slika.jpg" })]
        [TestCase("naziv", null, 1, new int[] { 1, 3 }, new string[] { "slika.jpg" })]
        [TestCase(null, "opis", 1, new int[] { 1, 3 }, new string[] { "slika.jpg" })]
        [TestCase("naziv", "opis", 1, new int[] {1,3 }, new string[] { "" })]
        [TestCase("naziv", "opis", 1, new int[] { 1, 3 }, new string[] { null })]
        public async Task PostTest1(string? naziv, string? opis, int? idKorisnika,int[]kategorijeIDs, string[]slike)
        {
            Slucaj slucaj=new Slucaj();
            slucaj.Naziv= naziv;
            slucaj.Opis = opis;
            
            var result = await controller.Dodaj(slucaj,idKorisnika, new List<int>(kategorijeIDs),new List<string>(slike));
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        public static string[][] slike_cases()
        {
            return new[]{ new[] { "slika.jpg" }, new[] { "slika.png","slika2.png" }, new string[] { } };
        }
        [Test, Combinatorial]
       
        public async Task PostTest2([ValueSource(nameof(slike_cases))] string[] slike,[Values("a", "__________________________________________________", "tip")] string? naziv, [Values("opis", "____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________")]string?opis, 
                                    [Values(1)]int? idKorisnika, [Values(new int[]{ 1}, new int[] { 1,2 })] int[] idKategorija)
        {
            Slucaj slucaj = new Slucaj();
            slucaj.Naziv = naziv;
            slucaj.Opis = opis;
            var result = await controller.Dodaj(slucaj,idKorisnika,new List<int>(idKategorija),new List<string>(slike));
            Assert.IsInstanceOf<OkObjectResult>(result);
            var id = (result as OkObjectResult).Value;
            Assert.IsNotNull(id);
            result = await controller.Obrisi((int)id);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        [Test]
        public async Task PostTest3()
        {
            var result = await controller.Dodaj(null,1, new List<int>() { 1},new List<string>());
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
        [TestCase(50)]
        [TestCase(51)]
        public async Task GetTest3(int id)
        {

            var actionresult = await controller.Preuzmi(id);
            Assert.IsInstanceOf<OkObjectResult>(actionresult);
        }
        [Test]
        [TestCase(52)]
        [TestCase(53)]
        public async Task DeleteTest1(int id)
        {
            var actionresult = await controller.Obrisi(id);
            Assert.IsInstanceOf<OkObjectResult>(actionresult);
            var result = await controller.Preuzmi(id);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
        [Test]
        [TestCase(int.MinValue)]

        public async Task DeleteTest2(int id)
        {
            var actionresult = await controller.Obrisi(id);
            Assert.IsInstanceOf<BadRequestObjectResult>(actionresult);
        }
        [Test]
        [TestCase(int.MaxValue)]
        public async Task DeleteTest3(int id)
        {
            var actionresult = await controller.Obrisi(id);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionresult);
        }
        [Test]
        [TestCase(50,null,  null, null, null, null, null,null, null, new int[] {1,2,3 })]
        [TestCase(50, null, null, new string[] {"slika.jpg" }, new string[] { }, null, null, null, new int[] { }, null)]
        [TestCase(50, null, null, new string[] {  }, new string[] { "slika.jpg","nekaSlika.jpg"}, null, null, null, new int[] { }, null)]
        [TestCase(50, "__________________________________________________", null, null, null, null, null, null, null, null)]
        [TestCase(50, "a", null, null, null, null, null, null, null, null)]
        [TestCase(51, null, "opis", null, null, null, null, null, null, null)]
        [TestCase(51, null, null, null, null, 2, null, null, null, null)]
        [TestCase(50, null, null, null, null, null, 1, null, null, null)]
        [TestCase(50, null, null, null, null, null, null, 3, null, null)]
        [TestCase(51, null, null, null, null, null, null, null,null, new int[] {3,2,1 })]
        [TestCase(51, null, null, null, null, null, null, null, new int[] { 2,}, null)]
        

        public async Task UpdateTest1(int id, string? naziv, string? opis,  string[]?addSlike,string[]?removeSlike,int? idLokacija, int? idKorisnika, int? idZivotinja, int[]? idRemoveKategorija, int[]? idAddKategorija)
        {
            var result = await controller.Azuriraj(id, naziv, opis, addSlike,removeSlike, idLokacija, idKorisnika, idZivotinja,idRemoveKategorija, idAddKategorija);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var uBazi = await controller.Preuzmi(id);
            Assert.IsInstanceOf<OkObjectResult>(uBazi);
            var slucaj = ((uBazi as OkObjectResult).Value) as Slucaj;
            Assert.Multiple(() =>
            {
                Assert.IsTrue(naziv == null || slucaj.Naziv==naziv);
                Assert.IsTrue(opis == null || slucaj.Opis==opis);
                Assert.IsTrue(idLokacija == null || slucaj.Lokacija.ID==idLokacija);
                Assert.IsTrue(idKorisnika == null || slucaj.Korisnik.ID==idKorisnika);
                Assert.IsTrue(idZivotinja==null||slucaj.Zivotinja.ID==idZivotinja);
                Assert.IsTrue(addSlike == null || !addSlike.Except(slucaj.Slike).Any());
                Assert.IsTrue(removeSlike == null || !slucaj.Slike.Intersect(removeSlike).Any());
                Assert.IsTrue(idAddKategorija == null || !idAddKategorija.Except(slucaj.Kategorija.Select(k=>k.ID)).Any());
                Assert.IsTrue(idRemoveKategorija == null || !idRemoveKategorija.Intersect(slucaj.Kategorija.Select(k => k.ID)).Any());
            });
        }
        [Test]
        [TestCase(-50, null, null, null, null, null, null, null, null, null)]
        [TestCase(50, "", null,new string[] { }, new string[] { }, null, null, null, new int[] { }, new int[] { })]
        [TestCase(50, "___________________________________________________", null, new string[] { }, new string[] { }, null, null, null, new int[] { }, new int[] { })]
        [TestCase(51, null, "", new string[] { }, new string[] { }, null, null, null, new int[] { }, new int[] { })]
        [TestCase(51, null, "_____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________"
                    , null, null, null, null, null, null, null)]
        [TestCase(50, null, null, new string[] { "slika.jpg", null }, new string[] { }, null, null, null, new int[] { }, new int[] { })]
        [TestCase(50 , null,null, new string[] { ""}, new string[] { }, null, null, null, new int[] { }, new int[] { })]
        [TestCase(50, null, null, new string[] { null }, new string[] { }, null, null, null, new int[] { }, new int[] { })]
        [TestCase(50, null, null, new string[] { }, new string[] { null }, null, null, null, new int[] { }, new int[] { })]
        [TestCase(50, null, null, new string[] { }, new string[] { null,"slika.jpg" }, null, null, null, new int[] { }, new int[] { })]
        [TestCase(50, null, null, null, null, int.MinValue, null, null, new int[] { }, new int[] { })]
        [TestCase(50, null, null, null, null, 1, null, null, new int[] { }, new int[] { })]
        [TestCase(50, null, null, null, null, null,int.MinValue, null, new int[] { }, new int[] { })]
        [TestCase(50, null, null, null, null, null,4,  null, new int[] { }, new int[] { })]
        [TestCase(50, null, null, null, null, null, null, int.MinValue, new int[] { }, new int[] { })]
        [TestCase(50, null, null, null, null, null, null, 4, new int[] { }, new int[] { })]
        [TestCase(50, null, null, null, null, null, null, null, null, new int[] { 2,int.MaxValue })]
        public async Task UpdateTest2(int id, string? naziv, string? opis, string[]? addSlike, string[]? removeSlike, int? idLokacija, int? idKorisnika, int? idZivotinja, int[]? idRemoveKategorija, int[]? idAddKategorija)
        {
            var result = await controller.Azuriraj(id, naziv, opis, addSlike, removeSlike, idLokacija, idKorisnika, idZivotinja, idRemoveKategorija, idAddKategorija);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        [Test]

        [TestCase(int.MaxValue)]
        [TestCase(70)]
        public async Task UpdateTest3(int id)
        {
            var result = await controller.Azuriraj(id, "tip", "", new string[] { }, new string[] { }, null, null, null, new int[] { }, new int[] { });
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
}
