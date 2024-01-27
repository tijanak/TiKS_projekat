
using Microsoft.Playwright;

namespace End_to_endTestovi
{

    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class LoginTest:PageTest
    {
        IPage page;
        IBrowser browser;

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
            await using var response2 = await Request.PostAsync("Korisnik/dodajkorisnika", new APIRequestContextOptions()
            {
                
                Headers = headers2,
                DataObject = new
                {
                    Username = "admin2",
                    Password = "admin2",
                }
            });

            await page.GotoAsync("http://127.0.0.1:4000/");
        }

        [Test]
        [Order(1)]
        public async Task Test1()
        {
            await page.Locator("input[type=\"text\"]").ClickAsync();

            await page.Locator("input[type=\"text\"]").FillAsync("admin");

            await page.Locator("input[type=\"password\"]").ClickAsync();

            await page.Locator("input[type=\"password\"]").FillAsync("admin");

            await page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();

            await Expect(page.Locator("div").Filter(new() { HasText = "LOGOLOGOMain" }).Nth(2)).ToBeVisibleAsync();
            await page.ScreenshotAsync(new() { Path = $"{Globals.scDir}/LoginTest1.png" });
        }
        [Test]
        [Order(2)]
        public async Task Test2()
        {
            await page.Locator("input[type=\"text\"]").ClickAsync();

            await page.Locator("input[type=\"text\"]").FillAsync("admin");

            await page.Locator("input[type=\"password\"]").ClickAsync();

            await page.Locator("input[type=\"password\"]").FillAsync("admin");

            await page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();


            await page.GetByLabel("Open settings").ClickAsync();

            await Expect(page.GetByText("Logout")).ToBeVisibleAsync();

            await page.GetByRole(AriaRole.Menuitem, new() { Name = "Logout" }).ClickAsync();

            await Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Login page" })).ToBeVisibleAsync();
            await page.ScreenshotAsync(new() { Path = $"{Globals.scDir}/LoginTest2.png" });

        }
        [TearDown]
        public async Task Teardown()
        {
            await using var response = await Request.DeleteAsync("Korisnik/uklonikorisnika/username/admin2");

            await page.CloseAsync();
            await browser.DisposeAsync();
        }
    }
}