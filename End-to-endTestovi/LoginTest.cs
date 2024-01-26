
namespace End_to_endTestovi
{

    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class LoginTest:PageTest
    {
        IPage page;
        IBrowser browser;
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
            await page.GotoAsync("http://127.0.0.1:4000/");
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
            await page.CloseAsync();
            await browser.DisposeAsync();
        }
    }
}