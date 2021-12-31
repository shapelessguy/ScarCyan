using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarCyan
{
    public class Loadings
    {
        public static string[] lines;
        public static void LoadPass()
        {
            Program.password = "none";
            if (!Directory.Exists(Program.path_dir) || !File.Exists(Program.path_file)) return;
            lines = null;
            lines = File.ReadAllLines(Program.path_file);
            Program.password = StringChipher.Decrypt(lines[0], Program.mypassword);

        }

        public static void LoadArgs()
        {
            if (lines == null) return;
            for (int i = 1; i < lines.Length; i++)
            {
                lines[i] = StringChipher.Decrypt(lines[i], Program.password);
                int indice_fineusername = lines[i].IndexOf(Input.segno);
                int indice_finepass = lines[i].Substring(indice_fineusername + Input.segno.Length).IndexOf(Input.segno)+indice_fineusername+Input.segno.Length;
                int indice_fineobject = lines[i].Substring(indice_finepass + Input.segno.Length).IndexOf(Input.segno)+indice_finepass + Input.segno.Length;
                Input.username_list.Add(lines[i].Substring(0, indice_fineusername));
                Input.pass_list.Add(lines[i].Substring(indice_fineusername + Input.segno.Length, indice_finepass - indice_fineusername - Input.segno.Length));
                Input.object_list.Add(lines[i].Substring(indice_finepass + Input.segno.Length, indice_fineobject - indice_finepass - Input.segno.Length));
                Input.email_list.Add(lines[i].Substring(indice_fineobject + Input.segno.Length));
            }
        }
    }
}
