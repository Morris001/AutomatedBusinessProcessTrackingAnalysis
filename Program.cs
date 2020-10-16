using GoogleDriveRestAPI_v3.Models;
using System;
using System.Windows.Forms;

namespace TimeTracker.View
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine("Running");
            GoogleDriveFilesRepository.FileUpload("Output2020_09_03.csv");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            
            //Application.Run(new Form1());
        }
    }
}
