using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace End_to_endTestovi
{

    [TestFixture]
    public class RegisterTest : PageTest
    {

        private IAPIRequestContext Request;
        IPage page;
        IBrowser browser;
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
            await page.GotoAsync("http://127.0.0.1:4000/");
            await page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();

            
        }

        [Test]
        [Order(1)]
        public async Task Test1()
        {
            await page.Locator("input[type=\"text\"]").ClickAsync();

            await page.Locator("input[type=\"text\"]").FillAsync("novi_korisnik");

            await page.Locator("input[type=\"password\"]").ClickAsync();

            await page.Locator("input[type=\"password\"]").FillAsync("sifra");

            await page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();

            await Expect(page.Locator("#root div").Filter(new() { HasTextRegex = new Regex("^Main$") })).ToBeVisibleAsync();
            await page.ScreenshotAsync(new() { Path = $"{Globals.scDir}/RegisterTest1.png" });
        }
        [Test]
        [Order(2)]
        public async Task Test2()
        {
            await page.Locator("input[type=\"text\"]").ClickAsync();

            await page.Locator("input[type=\"text\"]").FillAsync("admin");

            await page.Locator("input[type=\"password\"]").ClickAsync();

            await page.Locator("input[type=\"password\"]").FillAsync("sifra");

            await page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();

            await Expect(page.GetByRole(AriaRole.Alert)).ToContainTextAsync("Zauzeto korisnicko ime");
            await page.ScreenshotAsync(new() { Path = $"{Globals.scDir}/RegisterTest2.png" });

        }
        [TearDown]
        public async Task Teardown()
        {
            await using var response = await Request.DeleteAsync("Korisnik/uklonikorisnika/username/novi_korisnik");

            await page.CloseAsync();
            await browser.DisposeAsync();
        }
    }
}
