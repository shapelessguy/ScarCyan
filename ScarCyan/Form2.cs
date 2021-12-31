using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScarCyan
{
    public partial class Password : Form
    {
        public Password()
        {
            InitializeComponent();
            if (Program.password.Length < 15) { textBox3.Hide(); label4.Hide(); }
            textBox1.PasswordChar = '*';
            textBox2.PasswordChar = '*';
            textBox3.PasswordChar = '*';
            textBox1.LostFocus += FocusLost;
            textBox2.LostFocus += FocusLost;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 15 && textBox1.Text.Length != 0 && textBox2.Text.Length != 0)
            {
                textBox1.BackColor = Color.IndianRed;
                textBox2.BackColor = Color.IndianRed;
                textBox1.Text = "";
                textBox2.Text = "";
                return;
            }
            if (Program.password.Length >= 15) if (textBox3.Text != Program.password) { textBox3.BackColor = Color.IndianRed; textBox3.Text = ""; return; }
            if(textBox1.Text != textBox2.Text)
            {
                textBox1.BackColor = Color.IndianRed;
                textBox2.BackColor = Color.IndianRed;
                textBox1.Text = "";
                textBox2.Text = "";
                return;
            }
            if(textBox1.Text=="") Program.password = "none";
            else Program.password = textBox2.Text;
            Close();
        }

        private void FocusLost(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (textbox.Text.Length <15 && textbox.Text.Length != 0) textbox.BackColor = Color.IndianRed;
        }
    }
}
