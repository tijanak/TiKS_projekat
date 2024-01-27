using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace End_to_endTestovi
{
    public class IzmeniSlucajTest : PageTest
    {
        private IAPIRequestContext Request;
        IPage page;
        IBrowser browser;

        int testSlucajId = 0;
        [SetUp]
        public async Task Setup()
        {
            browser = await Playwright.Chromium.LaunchAsync(new()
            {
                //Headless = false,
                //SlowMo = 1000
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
            await page.GotoAsync("http://127.0.0.1:4000/");

            await page.Locator("input[type=\"text\"]").ClickAsync();

            await page.Locator("input[type=\"text\"]").FillAsync("admin");

            await page.Locator("input[type=\"password\"]").ClickAsync();

            await page.Locator("input[type=\"password\"]").FillAsync("admin");

            await page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();

        }

        [Test]
        [Order(1)]
        public async Task Test1()
        {

        }
        [TearDown]
        public async Task Teardown()
        {
            await using var response2 = await Request.DeleteAsync("Slucaj/Delete/" + testSlucajId);

            await page.CloseAsync();
            await browser.DisposeAsync();
        }
    }
}