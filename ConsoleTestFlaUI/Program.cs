using System;
using FlaUI.UIA3;

namespace ConsoleTestFlaUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = FlaUI.Core.Application.Launch("..\\..\\..\\..\\WpfApp1\\bin\\Debug\\WpfApp1.exe");
            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);
                Console.WriteLine(window.Title);
            }
        }
    }
}
