using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MFPD.Forms;



namespace MFPD
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new mfd998());
            Application.Run(new Login());//系統登入畫面
        }
    }
}
