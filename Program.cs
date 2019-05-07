using System;
using System.Windows.Forms;

namespace vAdmin
{
    static class Program
    {

        public const String BASE = "http://10.0.0.100";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
