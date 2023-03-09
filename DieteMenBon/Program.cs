using System;
using System.Windows.Forms;

namespace DieteMenBon
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

            //Application.Run(new FormConsultationPatient());
            Application.Run(new FormLogin());
            //Application.Run(FormConsultationPatient);
        }
    }
}
