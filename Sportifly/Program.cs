using Sportifly.User;
using System;
using System.Windows.Forms;

namespace Sportifly
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DependencyConfig.ConfigureServices();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormService());
        }
    }
}
