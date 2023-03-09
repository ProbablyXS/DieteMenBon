using DieteMenBon.assets;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DieteMenBon
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();

            try
            {
                DATABASE.dbConnect.Open(); //Connect to DB
            }
            catch { }


            //this.Scale(0.8f);

            AnimationsForms.OpenForm(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AnimationsForms.CloseApplicationForm(this);
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (txt.Name == textBox3.Name)
            {
                if (textBox3.Text == "Identifiant")
                {
                    textBox3.Clear();
                }
            }
            else if (txt.Name == textBox1.Name)
            {
                if (textBox1.Text == "Mot de passe")
                {
                    textBox1.PasswordChar = '*';
                    textBox1.Clear();
                }
            }
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (txt.Name == textBox3.Name)
            {
                if (textBox3.Text == "")
                {
                    textBox3.Text = "Identifiant";
                }
            }
            else if (txt.Name == textBox1.Name)
            {
                if (textBox1.Text == "")
                {
                    textBox1.PasswordChar = (char)0;
                    textBox1.Text = "Mot de passe";
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Process.Start("tel:+3385219502");
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            AnimationsForms.Frm.Frm_MouseDown(this, e);
        }

        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            AnimationsForms.Frm.Frm_MouseMove(this, e);
        }

        private void Login_MouseUp(object sender, MouseEventArgs e)
        {
            AnimationsForms.Frm.Frm_MouseUp(this, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DATABASE.LoginConnection(this, textBox3, textBox1, label3);
        }

        private void FormLogin_Activated(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                AnimationsForms.OpenForm(this);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                DATABASE.LoginConnection(this, textBox3, textBox1, label3);
            }
        }
    }
}
