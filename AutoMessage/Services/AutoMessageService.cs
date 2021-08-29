using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace AutoMessage.Services
{
    public class AutoMessageService
    {

        readonly IPage page;

        public AutoMessageService()
        {

            var playwright = Playwright.CreateAsync().GetAwaiter().GetResult();

            var browser = playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,

            }).GetAwaiter().GetResult();
            var context = browser.NewContextAsync(//new BrowserNewContextOptions
            /*{
                RecordVideoDir = "videos/"
            }*/).GetAwaiter().GetResult();

            // Open new page
            page = context.NewPageAsync().GetAwaiter().GetResult();


        }


        public void SendMessage()
        {
            page.GotoAsync("https://web.whatsapp.com/").GetAwaiter().GetResult();
        }

    }
}
