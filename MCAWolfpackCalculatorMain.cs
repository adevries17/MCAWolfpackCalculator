using System;
using System.Windows.Forms;

namespace MCAWolfpackCalculator {
    static class MCAWolfpackCalculatorMain {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainDiag());
        }
    }
}
