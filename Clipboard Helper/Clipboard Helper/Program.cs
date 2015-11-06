using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clipboard_Helper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            masterForm = new MasterForm();

            Application.ApplicationExit += Application_ApplicationExit;
            Application.Run(masterForm);

        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            Console.WriteLine("Exiting");
            masterForm.trayIcon.Dispose();
        }

        static MasterForm masterForm;
    }
}
