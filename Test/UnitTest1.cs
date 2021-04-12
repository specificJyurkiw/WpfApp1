using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using NUnit.Framework;

namespace Test
{
    public class Tests
    {
        private FlaUI.Core.Application app;

        [SetUp]
        public void Setup()
        {
            app = FlaUI.Core.Application.Launch("..\\..\\..\\..\\WpfApp1\\bin\\Debug\\WpfApp1.exe");
        }

        [TearDown]
        public void TearDown()
        {
            app.Close();
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