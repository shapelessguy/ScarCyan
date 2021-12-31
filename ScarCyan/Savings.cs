using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarCyan
{
    public class Savings
    {

        public static void Save()
        {
            if (Program.visualization) return;
            Console.WriteLine("Saving..");
            if (!Directory.Exists(Program.path_dir)) Directory.CreateDirectory(Program.path_dir);
            if (File.Exists(Program.path_file)) File.Delete(Program.path_file);
            using(StreamWriter stream = new StreamWriter(Program.path_file))
            {
                stream.WriteLine(StringChipher.Encrypt(Program.password, Program.mypassword));
                for(int i=0; i<Input.username_list.Count; i++)
                {
                    string stringa = StringChipher.Encrypt(Input.username_list[i] + Input.segno + Input.pass_list[i] + Input.segno + Input.object_list[i] + Input.segno + Input.email_list[i], Program.password);
                    stream.WriteLine(stringa);
                }
            }
        }
    }
}
