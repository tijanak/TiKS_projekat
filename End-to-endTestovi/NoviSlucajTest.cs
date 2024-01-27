using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace End_to_endTestovi
{
    [TestFixture]
    public class NoviSlucajTest : PageTest
    {
        IPage page;
        IBrowser browser;
        [SetUp]
        public async Task Setup()
        {
            browser = await Playwright.Chromium.LaunchAsync(new()
            {
                Headless = false,
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

            await Expect(page.GetByRole(AriaRole.Button, new() { Name = "Dodaj slucaj" })).ToBeVisibleAsync();

            var original = await page.Locator(".post-card").CountAsync();
            
            await page.GetByRole(AriaRole.Button, new() { Name = "Dodaj slucaj" }).ClickAsync();

            await page.GetByRole(AriaRole.Textbox).First.ClickAsync();

            await page.GetByRole(AriaRole.Textbox).First.FillAsync("novi slucaj");

            await page.GetByRole(AriaRole.Button, new() { Name = "Dodaj slucaj" }).ClickAsync();

            await page.GetByRole(AriaRole.Textbox).Nth(1).ClickAsync();

            await page.GetByRole(AriaRole.Textbox).Nth(1).FillAsync("opis");

            await page.GetByRole(AriaRole.Textbox).Nth(3).ClickAsync();

            await page.GetByRole(AriaRole.Textbox).Nth(3).FillAsync("pas");

            await page.GetByRole(AriaRole.Textbox).Nth(2).ClickAsync();

            await page.GetByRole(AriaRole.Textbox).Nth(2).FillAsync("nepoznato");

            await page.GetByRole(AriaRole.Spinbutton).First.ClickAsync();

            await page.GetByRole(AriaRole.Spinbutton).First.FillAsync("-200");

            await page.GetByRole(AriaRole.Spinbutton).Nth(1).ClickAsync();

            await page.GetByRole(AriaRole.Spinbutton).Nth(1).FillAsync("200");

            await page.GetByRole(AriaRole.Button, new() { Name = "Dodaj slucaj" }).ClickAsync();

            await Expect(page.GetByRole(AriaRole.Alert)).ToContainTextAsync("Mora postojati kategorija");

            

            await page.ScreenshotAsync(new() { Path = $"{Globals.scDir}/NoviSlucajTest1.1.png" });

            await page.GetByRole(AriaRole.Combobox).ClickAsync();
            
            await page.GetByRole(AriaRole.Option).First.ClickAsync();

            await page.GetByRole(AriaRole.Button, new() { Name = "Dodaj slucaj" }).ClickAsync();

            await Expect(page.GetByRole(AriaRole.Alert)).ToContainTextAsync("Latituda mora biti u opsegu [-90,90]");

            await page.ScreenshotAsync(new() { Path = $"{Globals.scDir}/NoviSlucajTest1.2.png" });

            await page.GetByRole(AriaRole.Spinbutton).First.ClickAsync();

            await page.GetByRole(AriaRole.Spinbutton).First.FillAsync("-20");

            await page.GetByRole(AriaRole.Spinbutton).Nth(1).ClickAsync();

            await page.GetByRole(AriaRole.Spinbutton).Nth(1).FillAsync("20");
            
            await page.GetByRole(AriaRole.Button, new() { Name = "Dodaj slucaj" }).ClickAsync();
            await Expect(page.GetByText("novi slucaj").First).ToBeVisibleAsync();
            await Expect(page.Locator(".post-card")).ToHaveCountAsync(original + 1);
            await page.ScreenshotAsync(new() { Path = $"{Globals.scDir}/NoviSlucajTest1.3.png" });
            


        }
        [TearDown]
        public async Task Teardown()
        {
            await page.CloseAsync();
            await browser.DisposeAsync();
        }
    }
}
