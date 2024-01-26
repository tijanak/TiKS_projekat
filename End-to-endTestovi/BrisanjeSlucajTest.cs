using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace End_to_endTestovi
{
    [TestFixture]
    public class BrisanjeSlucajTest : PageTest
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

            await Expect(page.Locator(".post-card").First).ToBeVisibleAsync();
            
            var original = await page.Locator(".post-card").CountAsync();
            var idPrvog = await page.EvaluateAsync("() => {return document.getElementsByClassName('post-card')[0]['id']}");
            Assert.IsNotNull(idPrvog);
            await page.Locator($"#{idPrvog} .delete-btn").ClickAsync();
            await Expect(page.Locator($"#{idPrvog}")).ToHaveCountAsync(0);
            await page.ScreenshotAsync(new() { Path = $"{Globals.scDir}/BrisanjeSlucajTest1.png" });
            await Expect(page.Locator(".post-card")).ToHaveCountAsync(original - 1);

        }
        [Test]
        [Order(2)]
        public async Task Test2()
        {
            
        }
        [TearDown]
        public async Task Teardown()
        {
            await page.CloseAsync();
            await browser.DisposeAsync();
        }
    }
}
