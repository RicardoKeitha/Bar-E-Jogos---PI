using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PI___Butecus_Sta_Tereza
{
    static class ControlMenuSystem
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoginSystem loginSystem = new LoginSystem();
            Application.Run(loginSystem);
        }
    }
}
