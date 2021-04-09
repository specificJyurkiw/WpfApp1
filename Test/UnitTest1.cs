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
            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);
                Assert.That(window.Title.CompareTo("MainWindow") == 0);
            }
            app.Close();
        }
    }
}