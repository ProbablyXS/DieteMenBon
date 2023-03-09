using DieteMenBon.assets;
using DieteMenBon.Properties;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DieteMenBon
{
    public partial class FormMesPatients : Form
    {
        public FormLogin FormLogin;
        private int refreshAll = 0;
        public static int deconnectLapsTime = 3600; // 1 heure

        public FormMesPatients(Form loginForm)
        {
            InitializeComponent();

            FormLogin = (FormLogin)loginForm;

            this.SetStyle(ControlStyles.ResizeRedraw, true); //FIX ICON SHOWED FOR RESIZE

            try
            {
                DATABASE.dbConnect.Open(); //Connect to DB

            }
            catch { }

            dataGridView1.RowTemplate.Height = 35;
            DATABASE.RefreshAllPatients(dataGridView1, label21);

            label7.Text = "Bonjour " + FormLogin.textBox3.Text;

            AutoDisconnectAsync();

            //this.Scale(0.8f);

        }

        public async Task AutoDisconnectAsync()
        {
            while (true)
            {
                await Task.Delay(1000);

                if (deconnectLapsTime == 0)
                {
                    AnimationsForms.DisconnectedForm(this, FormLogin);
                }
                else
                {
                    deconnectLapsTime -= 1;
                    //label8.Text = deconnectLapsTime.ToString();
                }
            }
        }

        //RESIZE
        private const int cGrip = 13;
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.IndianRed, rc);
        }
        protected override void WndProc(ref Message m)
        {
            deconnectLapsTime = 3600; //1 heure

            if (m.Msg == 0x84)
            {  // Trap WM_NCHITTEST
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);

                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                    return;
                }
            }
            base.WndProc(ref m);
        }
        //END RESIZE



        private void button2_Click(object sender, EventArgs e)
        {
            DATABASE.RefreshAllPatients(dataGridView1, label21);
            DATABASE.GetTotalMoneyCountForMonthAndYear(label3, label4);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormConsultationPatient.OpenOptionForm = 1;

            FormConsultationPatient f = new FormConsultationPatient(this) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };

            f.FormBorderStyle = FormBorderStyle.None;
            panel1.Controls.Add(f);

            f.BringToFront();

            f.Show();
        }

        private void NewProjectForm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            DATABASE.RefreshAllPatients(dataGridView1, label21);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 8) //CONSULTER 
            {
                try
                {
                    FormConsultationPatient.OpenOptionForm = 2;
                    FormConsultationPatient.Id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;

                    FormConsultationPatient f = new FormConsultationPatient(this) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };

                    f.FormBorderStyle = FormBorderStyle.None;
                    panel1.Controls.Add(f);

                    f.BringToFront();

                    f.Show();
                }
                catch { }
            }

            if (e.ColumnIndex == 9) //DELETE 
            {
                try
                {

                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    DialogResult dialogResult = MessageBox.Show("Voulez-vous vraiment supprimer " + row.Cells[1].Value + " " + row.Cells[2].Value + " ID: " + row.Cells[0].Value, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        DATABASE.DeletePatient(row.Cells[0].Value);
                        DATABASE.RefreshAllPatients(dataGridView1, label21);
                        DATABASE.GetTotalMoneyCountForMonthAndYear(label3, label4);
                    }
                }
                catch { }
            }
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            DATABASE.SearchPatient(dataGridView1, textBox3, label21);
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Recherche")
            {
                textBox3.Clear();
                textBox3.ForeColor = Color.Black;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Recherche";
                textBox3.ForeColor = Color.Silver;
                DATABASE.RefreshAllPatients(dataGridView1, label21);
                DATABASE.GetTotalMoneyCountForMonthAndYear(label3, label4);
            }
        }

        public static explicit operator FormMesPatients(DialogResult v)
        {
            throw new NotImplementedException();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DATABASE.SearchPatient(dataGridView1, textBox3, label21);
        }

        private void header_panel_MouseDown(object sender, MouseEventArgs e)
        {
            AnimationsForms.Frm.Frm_MouseDown(this, e);
        }

        private void header_panel_MouseMove(object sender, MouseEventArgs e)
        {
            AnimationsForms.Frm.Frm_MouseMove(this, e);
        }

        private void header_panel_MouseUp(object sender, MouseEventArgs e)
        {
            AnimationsForms.Frm.Frm_MouseUp(this, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AnimationsForms.CloseApplicationForm(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AnimationsForms.MinimizeForm(this);
        }

        private async void FormMesPatients_Activated(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                await AnimationsForms.OpenForm(this);
            }


            if (Form.ActiveForm == this && refreshAll == 0)
            {
                DATABASE.GetTotalMoneyCountForMonthAndYear(label3, label4);
                refreshAll = 1;
            }
            else
            {
                refreshAll = 0;
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;

            button01.BackColor = Settings.Default.defaultIndiaRedBackColor;
            button02.BackColor = Settings.Default.defaultIndiaRedBackColor;
            button03.BackColor = Settings.Default.defaultIndiaRedBackColor;
            button04.BackColor = Settings.Default.defaultIndiaRedBackColor;

            if (bt.Name == button01.Name)
            {
                panel1.Visible = false;
                panel7.Visible = false;
            }
            else if (bt.Name == button02.Name)
            {
                panel1.Visible = true;
                panel7.Visible = false;
            }
            else if (bt.Name == button03.Name)
            {

            }
            else if (bt.Name == button04.Name)
            {
                panel1.Visible = false;
                panel7.Visible = true;
            }

            bt.BackColor = Settings.Default.selectedIndiaBackColor;
        }

        private void label11_MouseEnter(object sender, EventArgs e)
        {
            label11.ForeColor = Settings.Default.defaultOrangeBackColor;
        }

        private void label11_MouseLeave(object sender, EventArgs e)
        {
            label11.ForeColor = Color.White;
        }

        private void label11_Click(object sender, EventArgs e)
        {
            AnimationsForms.CloseForm(this, FormLogin);
        }
    }
}
