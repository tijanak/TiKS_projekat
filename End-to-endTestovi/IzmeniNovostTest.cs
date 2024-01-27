using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace End_to_endTestovi
{
    [TestFixture]
    internal class IzmeniNovostTest : PageTest
    {
        IPage page;
        IBrowser browser;
        int testSlucajId = 0;
        int testNovostId = 0;
        private IAPIRequestContext Request;
        [SetUp]
        public async Task Setup()
        {
            browser = await Playwright.Chromium.LaunchAsync(new()
            {
                //Headless = false,
                //SlowMo = 500
            });

            page = await browser.NewPageAsync(new()
            {
                ViewportSize = new()
                {
                    Width = 1280,
                    Height = 720
                },
                ScreenSize = new()
                {
                    Width = 1280,
                    Height = 720
                },
                RecordVideoSize = new()
                {
                    Width = 1280,
                    Height = 720
                },
                RecordVideoDir = Globals.vidDir,
            });

            var headers = new Dictionary<string, string>
        {
            { "Accept", "application/json" }
        };
            Request = await Playwright.APIRequest.NewContextAsync(new()
            {
                BaseURL = "http://localhost:5100",
                ExtraHTTPHeaders = headers,
                IgnoreHTTPSErrors = true
            });
            Dictionary<string, string> headers2 = new()
        {
            { "Content-Type", "application/json" }
        };
            await using var response2 = await Request.PostAsync("Slucaj/Post?idKorisnika=" + Globals.adminId + "&kategorijeIDs=" + Globals.kategorijaId[0], new APIRequestContextOptions()
            {
                Headers = headers2,
                DataObject = new
                {
                    Naziv = "Test",
                    Opis = "Test",
                    Slike = new string[] { },
                    Korisnik = new { }
                }
            });
            var k = await response2.JsonAsync();
            if (k.HasValue)
            {
                var j = k.GetValueOrDefault();

                var id = j.GetInt32();
                testSlucajId = id;
            }

            await using var response3 = await Request.PostAsync("Novost/dodajnovost?id_slucaja=" + testSlucajId, new APIRequestContextOptions()
            {
                Headers = headers2,
                DataObject = new
                {
                    Tekst = "Test Edit Novost",
                    Slika = "imgs/stockphoto.jpg",
                    Slucaj = new { Korisnik=new { } },
                    Datum = new DateTime()
                }
            }) ;
            k = await response3.JsonAsync();
            if (k.HasValue)
            {
                var j = k.GetValueOrDefault();

                var id = j.GetInt32();
                testNovostId = id;
            }



            await page.GotoAsync("http://127.0.0.1:4000/");
            await page.Locator("input[type=\"text\"]").ClickAsync();

            await page.Locator("input[type=\"text\"]").FillAsync("admin");

            await page.Locator("input[type=\"password\"]").ClickAsync();

            await page.Locator("input[type=\"password\"]").FillAsync("admin");

            await page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();

        }


        [Test]
        [Order(2)]
        public async Task IzmeniNovost_test()
        {

            await page.Locator($"#_{testSlucajId} .novosti-btn").ClickAsync();

            await page.Locator($"#novosti{testNovostId}").GetByRole(AriaRole.Button).First.ClickAsync();

            await page.Locator("#tekst").ClickAsync();

            await page.Locator("#tekst").PressAsync("Control+a");

            await page.Locator("#tekst").FillAsync("Edited");

            await page.GetByLabel("Izmeni novost").Locator("input[type=\"date\"]").FillAsync("2024-01-24");

            await page.GetByRole(AriaRole.Button, new() { Name = "Izmeni" }).ClickAsync();

            await Expect(page.GetByText("Edited")).ToBeVisibleAsync();

        }

        [Test]
        [Order(1)]
        public async Task OdustaniOdPromeneNovosti_test()
        {

            await page.Locator($"#_{testSlucajId} .novosti-btn").ClickAsync();

            await page.Locator($"#novosti{testNovostId}").GetByRole(AriaRole.Button).First.ClickAsync();

            await page.Locator("#tekst").ClickAsync();

            await page.Locator("#tekst").PressAsync("Control+a");

            await page.Locator("#tekst").FillAsync("Odustacu od ovog");

            await page.GetByRole(AriaRole.Button, new() { Name = "Izmeni" }).ClickAsync();

            await Expect(page.GetByText("Test Edit Novost")).ToBeVisibleAsync();

        }

        [TearDown]
        public async Task Teardown()
        {
            var headers = new Dictionary<string, string>
            {
                { "Accept", "application/json" }
            };
            await using var response2 = await Request.PostAsync("Slucaj/Delete/" + testSlucajId, new APIRequestContextOptions()
            {
                Headers = headers
            });

            await Page.CloseAsync();
            await browser.DisposeAsync();
        }
    }
}

