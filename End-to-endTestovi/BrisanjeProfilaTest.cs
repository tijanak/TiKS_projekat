using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace End_to_endTestovi
{
    [TestFixture]
    public class BrisanjeProfilaTest : PageTest
    {
        IPage page;
        IBrowser browser;
        string korisnickoIme = "brisanjetest";
        string password = "sifra";
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
          
        }

        [Test]
        [Order(1)]
        public async Task Test1()
        {

            await page.GotoAsync("http://127.0.0.1:4000/");
            await page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();
            await page.Locator("input[type=\"text\"]").ClickAsync();

            await page.Locator("input[type=\"text\"]").FillAsync(korisnickoIme);

            await page.Locator("input[type=\"password\"]").ClickAsync();

            await page.Locator("input[type=\"password\"]").FillAsync(password);

            await page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();

            await Expect(page.Locator("#root div").Filter(new() { HasTextRegex = new Regex("^Main$") })).ToBeVisibleAsync();

            await Expect(page.Locator(".settings")).ToBeVisibleAsync();


            await page.Locator(".settings").ClickAsync();

            await page.GetByText("Profil").ClickAsync();

            await Expect(page.GetByRole(AriaRole.Button, new() { Name = "Obrisi profil" })).ToBeVisibleAsync();

            await page.ScreenshotAsync(new() { Path = $"{Globals.scDir}/BrisanjeProfilaTest1.1.png" });


            await page.GetByRole(AriaRole.Button, new() { Name = "Obrisi profil" }).ClickAsync();

            await Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Login page" })).ToBeVisibleAsync();


            await page.ScreenshotAsync(new() { Path = $"{Globals.scDir}/BrisanjeProfilaTest1.2.png" });

        }
        [TearDown]
        public async Task Teardown()
        {
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
            await using var response = await Request.DeleteAsync("Korisnik/uklonikorisnika/username/"+korisnickoIme);

            await page.CloseAsync();
            await browser.DisposeAsync();
        }
    }
}
