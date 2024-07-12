using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SanDiegoUnified.Models;

namespace SanDiegoUnified
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form = new Form1();
            Mainform.MyForm = form;
            Application.Run(form);
        }
    }
}
