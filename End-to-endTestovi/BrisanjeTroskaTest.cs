using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace End_to_endTestovi
{
    [TestFixture]
    internal class BrisanjeTroskaTest : PageTest
    {
        IPage page;
        IBrowser browser;
        int testSlucajId = 0;
        int testTrosakId = 0;
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

            await using var response3 = await Request.PostAsync("Trosak/dodajtrosak?idSlucaja=" + testSlucajId, new APIRequestContextOptions()
            {
                Headers = headers2,
                DataObject = new
                {
                    namena = "Test Dodat Trosak",
                    kolicina = 5000,
                    slucaj = new { Korisnik = new { } }
                }
            });
            k = await response3.JsonAsync();
            if (k.HasValue)
            {
                var j = k.GetValueOrDefault();

                var id = j.GetProperty("id").GetInt32();
                testTrosakId = id;
            }



            await page.GotoAsync("http://127.0.0.1:4000/");
            await page.Locator("input[type=\"text\"]").ClickAsync();

            await page.Locator("input[type=\"text\"]").FillAsync("admin");

            await page.Locator("input[type=\"password\"]").ClickAsync();

            await page.Locator("input[type=\"password\"]").FillAsync("admin");

            await page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();

        }

        [Test]
        public async Task ObrisiTrosak_test()
        {
            
            await page.Locator($"[id=\"_{testSlucajId}\"]").GetByRole(AriaRole.Button, new() { Name = "Doniraj" }).ClickAsync();
            await Expect(page.Locator(".troskovi-broj")).ToBeVisibleAsync();
            var br = await page.Locator(".troskovi-broj").CountAsync();

            await page.Locator($"#troskovi{testTrosakId}").GetByRole(AriaRole.Button).ClickAsync();
            
            await Expect(page.Locator(".troskovi-broj")).ToHaveCountAsync(br-1);
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

