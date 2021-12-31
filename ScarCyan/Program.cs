using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScarCyan
{
    static class Program
    {
        static public string path_dir = @"C:\ProgramData\Cyan";
        static public string path_file = @"C:\ProgramData\Cyan\Scar.txt";
        static public string mypassword = "amgsdfJHGKjhbogu38299nqwlkòK£lggkFkslIOnPlagsjssdYRUJJ93";
        static public string password = "none";
        static public bool visualization = false; //Compili la bin con valore true, trovi il file exe e lo copi in properties. Infine aggiorni il file in Properties.
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            int j = 0;
            foreach (Process clsProcess in Process.GetProcesses()) if (clsProcess.ProcessName == "ScarCyan") j++;
            if (j > 1 && !visualization) return;

            if (visualization)
            {
                string path = Process.GetCurrentProcess().MainModule.FileName;
                path = Path.GetDirectoryName(path);
                path_dir = path;
                path_file = path + @"\Scar.txt";
            }
            try { Loadings.LoadPass(); Loadings.LoadArgs();} catch (Exception) { if (visualization) { MessageBox.Show("Corrupted file"); return; } else File.Delete(path_file);}
            Loadings.lines = null;
            Input.username_list.Clear();
            Input.pass_list.Clear();
            Input.object_list.Clear();
            Input.email_list.Clear();
            Loadings.LoadPass();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ScarCyan());
        }
    }
}
