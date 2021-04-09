using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Automation;



namespace ConsoleTesting
{
    class Program
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        static void Main(string[] args)
        {
            Console.WriteLine("Begin Automation Testing!");

            Console.WriteLine("Launching Main Window!");
            Process process = null;
            process = Process.Start("..\\..\\..\\..\\WpfApp1\\bin\\Debug\\WpfApp1.exe");

            int loopCount = 0;
            do
            {
                Console.WriteLine("Looking for WpfApp1");
                ++loopCount;
                Thread.Sleep(100);

            } while (process == null && loopCount < 50);

            if (process == null) throw new Exception("Failed to find main window process.");
            else Console.WriteLine("Found main window process.");

            // Next, fetch a reference to the host machine's Desktop as an
            // AutomationElement object
            Console.WriteLine("Getting desktop.");

            //AutomationElement desktop = null;
            //desktop = AutomationElement.RootElement;

            //if (desktop == null) throw new Exception("Unable to get desktop");
            //else Console.WriteLine("Found desktop!");

            AutomationElement mainWindow = null;
            loopCount = 0;
            IntPtr hwnd = IntPtr.Zero;

            do
            {
                Console.WriteLine("Looking for main window...");
                //mainWindow = desktop.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "WpfApp1"));

                hwnd = FindWindow(null, "MainWindow");
                Console.WriteLine(hwnd.ToString());

                if (hwnd != IntPtr.Zero) mainWindow = AutomationElement.FromHandle(hwnd);
                
                ++loopCount;
                Thread.Sleep(200);
            } while (mainWindow == null && loopCount < 50);

            if (mainWindow == null) Console.WriteLine("Never found a main window handle.");
            else Console.WriteLine("Found main window handle.");

                Console.WriteLine("End Automation Testing!");
        }
    }
}
