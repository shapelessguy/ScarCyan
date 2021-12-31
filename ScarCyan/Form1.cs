using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScarCyan
{
    public partial class ScarCyan : Form
    {
        Password PassForm;
        Panel Pass;
        TextBox Pass_textbox;
        Label Pass_label;
        Label anim;
        Color neutral_color = Color.Gray;
        Color back_color = Color.Black;
        Color fore_color = Color.White;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem copia_user;
        private ToolStripMenuItem copia_pass;
        private ToolStripMenuItem copia_email;
        private ToolStripMenuItem copia_object;
        string stacco = " \u2192 ";
        string stacco_ = ":    ";
        public ScarCyan()
        {
            InitializeComponent();
           //listBox1.Click += SelectionListbox;

            contextMenuStrip = new ContextMenuStrip
            {
                ImageScalingSize = new System.Drawing.Size(24, 24),
                Size = new System.Drawing.Size(141, 34)
            };
            copia_user = new ToolStripMenuItem
            {
                Size = new System.Drawing.Size(140, 30),
                Text = "Copy Username"
            };
            copia_pass = new ToolStripMenuItem
            {
                Size = new System.Drawing.Size(140, 30),
                Text = "Copy Password"
            };
            copia_object = new ToolStripMenuItem
            {
                Size = new System.Drawing.Size(140, 30),
                Text = "Copy Object"
            };
            copia_email = new ToolStripMenuItem
            {
                Size = new System.Drawing.Size(140, 30),
                Text = "Copy E-mail"
            };
            copia_user.Click += Copia_user_Click;
            copia_pass.Click += Copia_pass_Click;
            copia_email.Click += Copia_email_Click;
            copia_object.Click += Copia_object_Click;
            //listBox1.ContextMenuStrip = contextMenuStrip;
            listBox1.MouseDown += SelectionListbox;
            Pass = new Panel()
            {
                Size = Size,
            };
            Controls.Add(Pass);
            if (Program.password.Length >= 15)
            {
                Pass.Show();
                Pass.BringToFront();
            }
            else
            {
                Pass.Hide();
                Loadings.LoadArgs();
                Init();
            }

            anim = new Label() { BackgroundImage = Properties.Resources.Verde, BackgroundImageLayout = ImageLayout.Stretch, Visible = false, Text = "", Size = new Size(20,20), };
            anim.SendToBack();
            Pass.Controls.Add(anim);
            Pass_label = new Label() {
                Text = "Password", TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false,
                Size = new Size(600, 40),
                Font = new System.Drawing.Font("Mongolian Baiti", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                BackColor = back_color,
                ForeColor = fore_color,
            };
            Pass_label.Location = new Point((int)(Pass.Width - Pass_label.Width)/2, (int)(Pass.Height - Pass_label.Height) / 3);
            Pass_textbox = new TextBox()
            {
                Location = new Point(Pass_label.Location.X, Pass_label.Location.Y + Pass_label.Size.Height), Size = Pass_label.Size,
                Font = new System.Drawing.Font("Mongolian Baiti", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                BackColor = back_color,
                ForeColor = fore_color,
            };
            Pass.Controls.Add(Pass_label);
            Pass.Controls.Add(Pass_textbox);
            Pass_textbox.KeyDown += EnterKey0;
            Pass_textbox.PasswordChar = '*';
            textBox1.Text = "UserName";
            textBox2.Text = "Password";
            textBox3.Text = "Object";
            textBox4.Text = "E-mail";
            textBox1.ForeColor = neutral_color;
            textBox2.ForeColor = neutral_color;
            textBox3.ForeColor = neutral_color;
            textBox4.ForeColor = neutral_color;
            textBox1.GotFocus += FocusGot;
            textBox2.GotFocus += FocusGot;
            textBox3.GotFocus += FocusGot;
            textBox4.GotFocus += FocusGot;
            textBox1.LostFocus += FocusLost;
            textBox2.LostFocus += FocusLost;
            textBox3.LostFocus += FocusLost;
            textBox4.LostFocus += FocusLost;
            panel1.Click += ClickNull;
            Click += ClickNull;
            if (Program.visualization)
            {
                Text = "ScarCyan - Visualization";
                foreach (Control controllo in panel1.Controls) controllo.Hide();
                listBox1.Show();
                listBox1.Size = panel1.Size;
                listBox1.Location = new Point(0, 0);
                listBox1.BringToFront();
            }
            Timer TypePass = new Timer() { Enabled = true, Interval = 200 };
            TypePass.Tick += (o, e) => { Pass_textbox.Focus(); };
            
        }

        void FocusGot(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (textbox.ForeColor == neutral_color) { textbox.Text = ""; textbox.ForeColor = fore_color; }
        }
        void FocusLost(object sender, EventArgs e)
        {

            TextBox textbox = (TextBox)sender;
            if (textbox.ForeColor != neutral_color && textbox.Text=="")
            {
                if (textbox.Name == "textBox1") textbox.Text = "UserName";
                if (textbox.Name == "textBox2") textbox.Text = "Password";
                if (textbox.Name == "textBox3") textbox.Text = "Object";
                if (textbox.Name == "textBox4") textbox.Text = "E-mail";
                textbox.ForeColor = neutral_color;
            }
        }

        void Copia_user_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Input.username_list[listBox1.SelectedIndex]);
        }
        void Copia_pass_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Input.pass_list[listBox1.SelectedIndex]);
        }
        void Copia_object_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Input.object_list[listBox1.SelectedIndex]);
        }
        void Copia_email_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Input.email_list[listBox1.SelectedIndex]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.ForeColor != neutral_color || textBox2.ForeColor != neutral_color || textBox3.ForeColor != neutral_color || textBox4.ForeColor != neutral_color)
            {
                if (textBox1.ForeColor != neutral_color) Input.username_list.Add(textBox1.Text); else Input.username_list.Add("");
                if (textBox2.ForeColor != neutral_color) Input.pass_list.Add(textBox2.Text); else Input.pass_list.Add("");
                if (textBox3.ForeColor != neutral_color) Input.object_list.Add(textBox3.Text); else Input.object_list.Add("");
                if (textBox4.ForeColor != neutral_color) Input.email_list.Add(textBox4.Text); else Input.email_list.Add("");
                string stacco0 = "", stacco1 = "", stacco2 = "";
                if (Input.object_list[Input.object_list.Count - 1] != "") stacco0 = stacco_;
                if (Input.username_list[Input.username_list.Count - 1] != "") stacco1 = stacco;
                if (Input.email_list[Input.email_list.Count - 1] != "") stacco2 = stacco;
                string stringa = Input.object_list[Input.object_list.Count - 1] + stacco0 + Input.username_list[Input.username_list.Count - 1] + stacco1 + Input.email_list[Input.email_list.Count - 1] + stacco2 + Input.pass_list[Input.pass_list.Count - 1];
                listBox1.Items.Add(stringa);
                Order(listBox1);
            }
            textBox1.Text = "Username";
            textBox2.Text = "Password";
            textBox3.Text = "Object";
            textBox4.Text = "E-mail";
            textBox1.ForeColor = neutral_color;
            textBox2.ForeColor = neutral_color;
            textBox3.ForeColor = neutral_color;
            textBox4.ForeColor = neutral_color;
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();
            button1.Hide();
            button2.Show();
            button3.Show();
            button7.Show();

        }
        private void Order(ListBox listBox)
        {

            Dictionary<int, string> dict = new Dictionary<int, string>();
            int j = 0;
            foreach (string stringa in listBox.Items) { dict.Add(j, stringa); j++; }

            var myList = dict.ToList();
            myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

            List<string> newEmailList = new List<string>();
            List<string> newPassList = new List<string>();
            List<string> newUsernameList = new List<string>();
            List<string> newObjectList = new List<string>();
            foreach (var pair in myList)
            {
                newEmailList.Add(Input.email_list[pair.Key]);
                newPassList.Add(Input.pass_list[pair.Key]);
                newUsernameList.Add(Input.username_list[pair.Key]);
                newObjectList.Add(Input.object_list[pair.Key]);
            }
            Input.email_list = newEmailList;
            Input.pass_list = newPassList;
            Input.username_list = newUsernameList;
            Input.object_list = newObjectList;


            List<string> lista = new List<string>();
            foreach (string stringa in listBox.Items) { lista.Add(stringa); }
            lista.Sort();
            listBox.Items.Clear();
            foreach (string stringa in lista) listBox.Items.Add(stringa);
        }

        public void Init()
        {
            FormClosing += FormClosing_;
            textBox1.KeyDown += EnterKey;
            textBox2.KeyDown += EnterKey;
            textBox3.KeyDown += EnterKey;
            textBox4.KeyDown += EnterKey;
            for (int i = 0; i < Input.username_list.Count; i++)
            {
                string stacco0 = "", stacco1 = "", stacco2 = "";
                if (Input.object_list[i] != "") stacco0 = stacco_;
                if (Input.username_list[i] != "") stacco1 = stacco;
                if (Input.email_list[i] != "") stacco2 = stacco;
                string stringa = Input.object_list[i] + stacco0 + Input.username_list[i] + stacco1 + Input.email_list[i] + stacco2 + Input.pass_list[i];
                listBox1.Items.Add(stringa);
            }
            Order(listBox1);
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Hide();
            button3.Hide();
            button7.Hide();
            textBox1.Show();
            textBox2.Show();
            textBox3.Show();
            textBox4.Show();
            button1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Input.username_list.RemoveAt(listBox1.SelectedIndex);
            Input.pass_list.RemoveAt(listBox1.SelectedIndex);
            Input.object_list.RemoveAt(listBox1.SelectedIndex);
            Input.email_list.RemoveAt(listBox1.SelectedIndex);
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void EnterKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                button1_Click(null, null);
            }
        }
        private void EnterKey0(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                anim.Show();
                anim.BringToFront();
                Pass_textbox.Visible = false;
                Pass_label.Visible = false;
                Update();
                for (int i=0; i<20; i++)
                {
                    double t = (double)i * 0.8;
                    double x = Pass.Width / 2 + 10 * Math.Cos(t) - 10;
                    double y = Pass.Height / 3 + Pass_label.Height/2 + 10 * Math.Sin(t);

                    anim.Location = new Point((int)x,(int)y);
                    System.Threading.Thread.Sleep(50);
                }
                anim.SendToBack();
                anim.Hide();
                Pass_textbox.Visible = true;
                Pass_label.Visible = true;
                if (Pass_textbox.Text == Program.password)
                {
                    Pass.Hide();
                    Loadings.LoadArgs();
                    Init();
                }
                Pass_textbox.Text = "";
            }
        }

        private void FormClosing_(object sender, EventArgs e)
        {
            Savings.Save();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (PassForm == null || PassForm.IsDisposed) { PassForm = new Password(); PassForm.Show(); }
            else { PassForm.Show(); PassForm.Focus(); }
        }

        private void ClickNull(object sender, EventArgs e)
        {
            listBox1.SelectedItem = null;
            button3.Enabled = false;
            button7.Enabled = false;
            listBox1.Focus();
        }

        //Importa
        private void button4_Click(object sender, EventArgs e)
        {
            using (var fbd = new OpenFileDialog())
            {
                fbd.Filter = "File text|*.txt";
                DialogResult result = fbd.ShowDialog();
                string data = DateTime.Now.ToFileTime().ToString();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SafeFileName))
                {
                    try
                    {
                        if (File.Exists(Program.path_file))
                        {
                            File.Copy(Program.path_dir + @"\Scar.txt", Program.path_dir + @"\Scar_.txt");
                            File.Delete(Program.path_file);
                        }
                        File.Copy(fbd.FileName, Program.path_file);
                        try { Loadings.LoadPass(); Loadings.LoadArgs(); } catch (Exception)
                        {
                            File.Delete(Program.path_file);
                            if (File.Exists(Program.path_dir + @"\Scar_.txt"))
                            {
                                File.Copy(Program.path_dir + @"\Scar_.txt", Program.path_dir + @"\Scar.txt");
                                File.Delete(Program.path_dir + @"\Scar_.txt");
                            }
                            MessageBox.Show("Corrupted file");
                            return;
                        }
                        if (File.Exists(Program.path_dir + @"\Scar_.txt")) File.Delete(Program.path_dir + @"\Scar_.txt");

                        MessageBox.Show("Credentials imported successfully!");
                        listBox1.Items.Clear();
                        Loadings.lines = null;
                        Input.username_list.Clear();
                        Input.pass_list.Clear();
                        Input.object_list.Clear();
                        Input.email_list.Clear();
                        Loadings.LoadPass();
                        if (Program.password.Length >= 15)
                        {
                            Pass.Show();
                            Pass.BringToFront();
                        }
                    }
                    catch (Exception) { MessageBox.Show("Error"); }
                }
            }
        }

        //Esporta
        private void button5_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                string data = DateTime.Now.ToFileTime().ToString();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    try
                    {
                        Savings.Save();
                        Directory.CreateDirectory(fbd.SelectedPath + @"\Scar" + data);
                        File.Copy(Program.path_file, fbd.SelectedPath + @"\Scar" + data + @"\Scar.txt");
                        System.IO.File.WriteAllBytes(fbd.SelectedPath + @"\Scar" + data + @"\ScarCyan.exe", Properties.Resources.ScarCyan);

                        File.SetCreationTime(fbd.SelectedPath + @"\Scar" + data + @"\Scar.txt", DateTime.Now);
                        File.SetLastWriteTime(fbd.SelectedPath + @"\Scar" + data + @"\Scar.txt", DateTime.Now);
                        File.SetCreationTime(fbd.SelectedPath + @"\Scar" + data + @"\ScarCyan.exe", DateTime.Now);
                        File.SetLastWriteTime(fbd.SelectedPath + @"\Scar" + data + @"\ScarCyan.exe", DateTime.Now);
                        MessageBox.Show("Credentials exported into the folder "+ fbd.SelectedPath + @"\Scar" + data);
                    }
                    catch (Exception) { MessageBox.Show("Error"); }
                }
            }
        }

        private void SelectionListbox(object sender, MouseEventArgs e)
        {
            int nIdx = listBox1.IndexFromPoint(listBox1.PointToClient(Cursor.Position));
            if(e.Button == MouseButtons.Right)
            {
                if (nIdx >= 0) {
                    contextMenuStrip.Items.Clear();
                    if (Input.object_list[nIdx] != "") contextMenuStrip.Items.Add(copia_object);
                    if (Input.username_list[nIdx] != "") contextMenuStrip.Items.Add(copia_user);
                    if (Input.email_list[nIdx] != "") contextMenuStrip.Items.Add(copia_email);
                    if (Input.pass_list[nIdx] != "") contextMenuStrip.Items.Add(copia_pass);
                    listBox1.SelectedItem = listBox1.Items[nIdx];
                    button3.Enabled = true;
                    button7.Enabled = true;
                    contextMenuStrip.Show();
                    contextMenuStrip.Location = Cursor.Position;
                    contextMenuStrip.Update(); }
                return;
            }
            if (nIdx < 0) { listBox1.SelectedItem = null; button3.Enabled = false; button7.Enabled = false; }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string user, pass, email, objec;
            if (Input.object_list[listBox1.SelectedIndex] != "") objec = Input.object_list[listBox1.SelectedIndex]; else objec = "";
            if (Input.username_list[listBox1.SelectedIndex] != "") user = Input.username_list[listBox1.SelectedIndex]; else user = "";
            if (Input.email_list[listBox1.SelectedIndex] != "") email = Input.email_list[listBox1.SelectedIndex]; else email = "";
            if (Input.pass_list[listBox1.SelectedIndex] != "") pass = Input.pass_list[listBox1.SelectedIndex]; else pass = "";

            button3_Click(sender, e);
            button2_Click(sender, e);

            if (user != "") { textBox1.Text = user; textBox1.ForeColor = fore_color; }
            if (objec != "") { textBox3.Text = objec; textBox3.ForeColor = fore_color; }
            if (email != "") { textBox4.Text = email; textBox4.ForeColor = fore_color; }
            if (pass != "") { textBox2.Text = pass; textBox2.ForeColor = fore_color; }

        }
    }
}
