using Microsoft.Playwright;
using Model.Models;
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


        public void SendOneMessageToManyContacts(List<String> phoneNumbers)
        {
            page.GotoAsync("https://web.whatsapp.com/").GetAwaiter().GetResult();

            //page.WaitForSelectorAsync("#side > div.uwk68 > div > label > div > div._13NKt.copyable-text.selectable-text").GetAwaiter().GetResult();

            //for (int i = 0; i < phoneNumbers.Count(); i++)
            //{

            //    page.ClickAsync("#side > div.uwk68 > div > label > div > div._13NKt.copyable-text.selectable-text").GetAwaiter().GetResult();
            //    page.FillAsync("#side > div.uwk68 > div > label > div > div._13NKt.copyable-text.selectable-text", phoneNumbers[i]).GetAwaiter().GetResult();
            //    page.Keyboard.PressAsync("Enter");
            //    page.WaitForSelectorAsync("#main > footer > div._2BU3P.tm2tP.copyable-area > div._1SEwr > div > div.p3_M1 > div > div._13NKt.copyable-text.selectable-text");

            //    page.Keyboard.TypeAsync("bu mesajı dikkate almayın");

            //    page.ClickAsync("#main > footer > div._2BU3P.tm2tP.copyable-area > div._1SEwr > div > div._3HQNh._1Ae7k > button > span");
            //}
        }

        public void SendManyMessagesToOneContact(List<String> messages)
        {

            page.GotoAsync("https://web.whatsapp.com/").GetAwaiter().GetResult();

            //page.WaitForSelectorAsync("#side > div.uwk68 > div > label > div > div._13NKt.copyable-text.selectable-text").GetAwaiter().GetResult();

        }

    }
}
