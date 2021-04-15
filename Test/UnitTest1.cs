using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Test
{
    public class Tests
    {
        private FlaUI.Core.Application app;
        private readonly string apiUrl = System.Environment.GetEnvironmentVariable("APPVEYOR_API_URL");

        [SetUp]
        public void Setup()
        {
            app = FlaUI.Core.Application.Launch("..\\..\\..\\..\\WpfApp1\\bin\\Debug\\WpfApp1.exe");
        }

        [TearDown]
        public void TearDown()
        {
            app.Close();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync("api/tests", new Dictionary<string, string>()
                {
                    { "testName", TestContext.CurrentContext.Test.Name },
                    { "testFramework", "NUnit3" },
                    { "fileName", TestContext.CurrentContext.Test.ClassName },
                    { "outcome", TestStatus.Failed.ToString() },
                    { "durationMilliseconds", "12" },
                    { "ErrorMessage", "Something went wrong" },
                    { "ErrorStackTrace", "stacktrace here!" },
                    { "StdOut", "Whoop! There it is!" },
                    { "StdErr", "Oh noes!" }
                }).Result;
            }
        }

        [Test]
        public void Test1()
        {
            using (UIA3Automation automation = new UIA3Automation())
            {
                Window window = app.GetMainWindow(automation);
                Assert.That(window.Title.CompareTo("MainWindow") == 0);
            }
        }

        [Test]
        public void Test2()
        {
            using (UIA3Automation automation = new UIA3Automation())
            {
                Window window = app.GetMainWindow(automation);
                AutomationElement displayButton = window.FindFirstDescendant(fe => fe.ByAutomationId("displayButton"));
                Button db = displayButton.AsButton();

                //Assert.IsTrue(dl.Text.Length == 0);
                db.Click();

                AutomationElement displayLabel = window.FindFirstDescendant(fe => fe.ByAutomationId("displayLabel"));
                int ms = 0;
                while (displayLabel == null && ms < 2000)
                {
                    System.Threading.Thread.Sleep(100);
                    ms += 100;
                    displayLabel = window.FindFirstDescendant(fe => fe.ByAutomationId("displayLabel"));
                }
                Assert.IsNotNull(displayLabel);
                Label dl = displayLabel.AsLabel();
                Assert.IsTrue(dl.Text.Length > 0);
            }
        }
    }
}