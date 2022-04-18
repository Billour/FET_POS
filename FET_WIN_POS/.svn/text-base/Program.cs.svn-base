using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;

using System.Windows.Forms;

namespace FetPos
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
            LoginForm loginForm = new LoginForm();
            Application.Run(loginForm);
            if (loginForm.DialogResult == DialogResult.OK)
            {
                Application.Run(new MainForm());
            }           
        }
    }
}
