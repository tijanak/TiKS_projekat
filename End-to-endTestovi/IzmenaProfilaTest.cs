using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace End_to_endTestovi
{
    [TestFixture]
    public class IzmenaProfilaTest : PageTest
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
            await Expect(page.Locator(".settings")).ToBeVisibleAsync();


            await page.Locator(".settings").ClickAsync();

            await page.GetByText("Profil").ClickAsync();

            await Expect(page.Locator(".edit_profile")).ToBeVisibleAsync();
            await page.Locator(".edit_profile").ClickAsync();

            await Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Promena podataka" })).ToBeVisibleAsync();

            await page.GetByLabel("Username *").ClickAsync();

            await page.GetByLabel("Username *").FillAsync("admin");

            await page.GetByRole(AriaRole.Button, new() { Name = "Sacuvaj" }).ClickAsync();
            await Expect(page.GetByRole(AriaRole.Alert)).ToContainTextAsync("korisnicko ime zauzeto");
            await page.ScreenshotAsync(new() { Path = $"{Globals.scDir}/IzmenaProfilaTest1.png" });

        }
        [Test]
        [Order(2)]
        public async Task Test2()
        {
            await Expect(page.Locator(".settings")).ToBeVisibleAsync();


            await page.Locator(".settings").ClickAsync();

            await page.GetByText("Profil").ClickAsync();

            await Expect(page.Locator(".edit_profile")).ToBeVisibleAsync();
            await page.Locator(".edit_profile").ClickAsync();

            await Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Promena podataka" })).ToBeVisibleAsync();

            await page.GetByLabel("Username *").ClickAsync();

            await page.GetByLabel("Username *").FillAsync("noviUsername");

            await page.GetByRole(AriaRole.Button, new() { Name = "Sacuvaj" }).ClickAsync();
            await Expect(page.Locator(".username_label")).ToContainTextAsync("noviUsername");
            await page.ScreenshotAsync(new() { Path = $"{Globals.scDir}/IzmenaProfilaTest2.png" });

        }
        [Test]
        [Order(3)]
        public async Task Test3()
        {
            await Expect(page.Locator(".settings")).ToBeVisibleAsync();


            await page.Locator(".settings").ClickAsync();

            await page.GetByText("Profil").ClickAsync();

            await Expect(page.Locator(".edit_profile")).ToBeVisibleAsync();
            await page.Locator(".edit_profile").ClickAsync();

            await Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Promena podataka" })).ToBeVisibleAsync();

            await page.GetByLabel("Username *").ClickAsync();

            await page.GetByLabel("Username *").FillAsync("testUser");


            await page.GetByRole(AriaRole.Button, new() { Name = "Odustani" }).ClickAsync();
            await Expect(page.Locator(".username_label")).ToContainTextAsync("admin");
            await page.ScreenshotAsync(new() { Path = $"{Globals.scDir}/IzmenaProfilaTest3.png" });

        }
        [TearDown]
        public async Task Teardown()
        {
            await using var response2 = await Request.PutAsync("Korisnik/izmeniusernamepassword",new APIRequestContextOptions()
            {
                Params = new Dictionary<string, object>
            {
                { "id_korisnika",Globals.adminId },{"username","admin" },{"password","admin"} 
            },
            });
            await page.CloseAsync();
            await browser.DisposeAsync();
        }
    }
}
