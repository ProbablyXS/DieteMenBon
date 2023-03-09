using DieteMenBon.assets;
using DieteMenBon.Properties;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SeriesCollection = LiveCharts.SeriesCollection;

namespace DieteMenBon
{
    public partial class FormConsultationPatient : Form
    {

        public static int OpenPageForm = 1; //1 is etude patient - //2 consultations - //3 mensurations
        public static int OpenOptionForm = 1; //1 is Ajouter un client - //2 is Consulter un client

        public static int MenuOnePage = 1;
        //public static int MenuTwoPage = 1;
        //public static int MenuTreePage = 1;

        public static int Id = 0; //1 is Ajouter un client - //2 is Consulter un client
        public static int OldConsultationIdValue = 0;

        public static int tbLocationX, tbLocationY = 0;

        public FormMesPatients FormMesPatients;

        public FormConsultationPatient(object Form)
        {
            InitializeComponent();

            FormMesPatients = (FormMesPatients)Form;

            this.SetStyle(ControlStyles.ResizeRedraw, true); //FIX ICON SHOWED FOR RESIZE

            this.dataGridView1.MouseWheel += new MouseEventHandler(mousewheel);
            this.dataGridView2.MouseWheel += new MouseEventHandler(mousewheel);

            try
            {
                DATABASE.dbConnect.Open(); //Connect to DB
            }
            catch { }

            if (OpenOptionForm == 1) //1 is Ajouter un client
            {
                var timeResult = DateTime.Now.Date - dateTimePicker1.Value.Date;
                label22.Text = (timeResult.Days / 365).ToString();

                comboBox1.SelectedIndex = 0;

                button10.Visible = false;
                button7.Visible = false;
                button2.Visible = false;

                label20.Visible = false;
                comboBox2.Visible = false;

                label21.Text = Arithmetic.GeneratorPatient_ID();
            }
            else if (OpenOptionForm == 2) //2 is Consulter un client
            {
                try
                {
                    //ETUDE PATIENT
                    button5.Visible = false;
                    label21.Text = Id.ToString();
                    //comboBox2.SelectedIndex = 0;
                    DATABASE.RefreshHistoryPatient(comboBox2, Id);

                    //CONSULTATIONS
                    dataGridView1.RowTemplate.Height = 35;
                    DATABASE.RefreshAllConsultations(dataGridView1, Id, label5, label16);

                    //MENSURATION
                    dataGridView2.RowTemplate.Height = 35;
                    DATABASE.RefreshAllMensurations(dataGridView2, Id, label83, label31);
                    GRAPH();
                }
                catch { }
            }

        }

        //RESIZE
        private const int cGrip = 13;
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Dock == DockStyle.None)
            {
                Rectangle rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
                ControlPaint.DrawSizeGrip(e.Graphics, Color.IndianRed, rc);
            }
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84 && Dock == DockStyle.None)
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


        private void mousewheel(object sender, MouseEventArgs e)
        {
            DataGridView datagridview = sender as DataGridView;

            if (e.Delta > 0 && datagridview.FirstDisplayedScrollingRowIndex > 0)
            {
                datagridview.FirstDisplayedScrollingRowIndex--;
            }
            else if (e.Delta < 0)
            {
                datagridview.FirstDisplayedScrollingRowIndex++;
            }
        }

        private void button_MouseEnter(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (button.BackColor == Settings.Default.defaultGreenBackColor) return;
            button.BackColor = Settings.Default.defaultOrangeBackColor;
        }

        private void button_MouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (button.BackColor == Settings.Default.defaultGreenBackColor) return;
            button.BackColor = Settings.Default.defaultOrangeBackColor;
        }

        private void buttonPage_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (button.Name == button3.Name)
            {
                MenuOnePage = 1;

                panel3.Visible = false;
                panel3.SendToBack();

                button3.BackColor = Settings.Default.defaultGreenBackColor;
                button3.FlatAppearance.BorderColor = Settings.Default.defaultGreenBackColor;

                button4.BackColor = Settings.Default.defaultOrangeBackColor;
                button4.FlatAppearance.BorderColor = Settings.Default.defaultOrangeBackColor;
            }
            else if (button.Name == button4.Name)
            {
                MenuOnePage = 2;

                panel3.BringToFront();
                label1.BringToFront();
                label21.BringToFront();
                label20.BringToFront();
                comboBox2.BringToFront();
                label25.BringToFront();
                label30.BringToFront();

                panel3.Visible = true;

                button3.BackColor = Settings.Default.defaultOrangeBackColor;
                button3.FlatAppearance.BorderColor = Settings.Default.defaultOrangeBackColor;

                button4.BackColor = Settings.Default.defaultGreenBackColor;
                button4.FlatAppearance.BorderColor = Settings.Default.defaultGreenBackColor;
            }
        }

        private void textBox_Click(object sender, EventArgs e)
        {
            TextBox textbox = sender as TextBox;

            tbLocationX = textbox.Location.X;
            tbLocationY = textbox.Location.Y;

            textbox.Dock = DockStyle.Fill;
            textbox.BringToFront();

            if (OpenPageForm == 1 || OpenPageForm == 2)
            {
                label25.Text = textbox.Tag.ToString();
                label25.Visible = true;
                label25.BringToFront();

                label30.Text = "Longueur: " + textbox.Text.Length + '\r' + "Longueur max: " + textbox.MaxLength;
                label30.Visible = true;
                label30.BringToFront();

                button6.Visible = true;
                button6.BringToFront();

                label1.Visible = false;
                label21.Visible = false;
                label20.Visible = false;
                comboBox2.Visible = false;
                button7.Visible = false;
                button5.Visible = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            foreach (Control pn in Controls.OfType<Panel>())
            {
                foreach (Control tb in pn.Controls)
                {
                    if (tb is TextBox && tb.Dock == DockStyle.Fill)
                    {
                        tb.Location = (new Point(tbLocationX, tbLocationY));
                        tb.Dock = DockStyle.None;
                        tb.Anchor = AnchorStyles.None;
                    }
                }
            }

            label25.Visible = false;
            label30.Visible = false;
            button6.Visible = false;


            if (OpenOptionForm == 1)  //1 is Ajouter un client
            {
                button7.Visible = false;
                button5.Visible = true;
            }
            else if (OpenOptionForm == 2) //2 is Consulter un client
            {
                button5.Visible = false;
                button7.Visible = true;

                label20.Visible = true;
                comboBox2.Visible = true;
            }

            label1.Visible = true;
            label21.Visible = true;
        }

        private void textBox_MouseHover(object sender, EventArgs e)
        {
            TextBox textbox = sender as TextBox;
            toolTip1.Show(textbox.Text, textbox);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            var timeResult = DateTime.Now.Date - dateTimePicker1.Value.Date;
            label22.Text = (timeResult.Days / 365).ToString();
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox_KeyPressDoubleOnly(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 46 && e.KeyChar != 8)
                e.Handled = true;
        }

        private void fixCharacterUTF8InTextbox()
        {
            foreach (Control pn in Controls.OfType<Panel>())
            {
                foreach (Control tb in pn.Controls)
                {
                    if (tb is TextBox)
                    {
                        if (tb.Text == null) continue;

                        if (tb.Text.ToString().Contains("'"))
                        {
                            tb.Text = tb.Text.ToString().Replace("'", " ");
                        }
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            fixCharacterUTF8InTextbox();

            DATABASE.AddNewPatient(label21, comboBox2, textBox2, textBox4, comboBox1, dateTimePicker1,
                label22, textBox55, textBox52, textBox8, textBox9, textBox10, textBox60, textBox61,
                textBox11, textBox12, textBox13, textBox5, textBox6, textBox18, textBox59, textBox56,
                textBox58, textBox57, textBox21, textBox22, textBox23, textBox25, textBox26, textBox27,
                textBox24, textBox7, textBox15, textBox14, textBox16, textBox51, textBox39, textBox35,
                textBox38, textBox37, textBox36, textBox3, textBox45, textBox44, textBox32, textBox31,
                textBox29, textBox33, textBox34, checkBox1, textBox28, checkBox2, textBox30, checkBox3, textBox46, checkBox4, textBox47, checkBox5, textBox48, checkBox6,
   textBox50, textBox1, textBox49, textBox42, textBox41, textBox40, textBox53, textBox43, textBox54);

            DATABASE.RefreshAllPatients(FormMesPatients.dataGridView1, FormMesPatients.label21);
            DATABASE.GetTotalMoneyCountForMonthAndYear(FormMesPatients.label3, FormMesPatients.label4);

            Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DATABASE.GetHistoryPatient(label21, comboBox2, textBox2, textBox4, comboBox1, dateTimePicker1,
               label22, textBox55, textBox52, textBox8, textBox9, textBox10, textBox60, textBox61,
               textBox11, textBox12, textBox13, textBox5, textBox6, textBox18, textBox59, textBox56,
               textBox58, textBox57, textBox21, textBox22, textBox23, textBox25, textBox26, textBox27,
               textBox24, textBox7, textBox15, textBox14, textBox16, textBox51, textBox39, textBox35,
               textBox38, textBox37, textBox36, textBox3, textBox45, textBox44, textBox32, textBox31,
               textBox29, textBox33, textBox34, checkBox1, textBox28, checkBox2, textBox30, checkBox3, textBox46, checkBox4, textBox47, checkBox5, textBox48, checkBox6,
               textBox50, textBox1, textBox49, textBox42, textBox41, textBox40, textBox53, textBox43, textBox54);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Voulez-vous vraiment enregistrer ses modifications à la date d'aujourd'hui ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {

                fixCharacterUTF8InTextbox();

                DATABASE.ReplaceNewInformationsPatient(label21, comboBox2, textBox2, textBox4, comboBox1, dateTimePicker1,
   label22, textBox55, textBox52, textBox8, textBox9, textBox10, textBox60, textBox61,
   textBox11, textBox12, textBox13, textBox5, textBox6, textBox18, textBox59, textBox56,
   textBox58, textBox57, textBox21, textBox22, textBox23, textBox25, textBox26, textBox27,
   textBox24, textBox7, textBox15, textBox14, textBox16, textBox51, textBox39, textBox35,
   textBox38, textBox37, textBox36, textBox3, textBox45, textBox44, textBox32, textBox31,
   textBox29, textBox33, textBox34, checkBox1, textBox28, checkBox2, textBox30, checkBox3, textBox46, checkBox4, textBox47, checkBox5, textBox48, checkBox6,
   textBox50, textBox1, textBox49, textBox42, textBox41, textBox40, textBox53, textBox43, textBox54);

                DATABASE.RefreshHistoryPatient(comboBox2, Id);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (button.Name == button1.Name && OpenOptionForm == 2)
            {
                OpenPageForm = 1;

                button1.BackColor = Settings.Default.defaultGreenBackColor;
                button10.BackColor = Settings.Default.defaultOrangeBackColor;
                button2.BackColor = Settings.Default.defaultOrangeBackColor;

                panel2.Visible = false;
                panelMensuration.Visible = false;
                button8.Visible = false;

                panel3.Visible = true;
                panel1.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button7.Visible = true;

            }
            else if (button.Name == button10.Name)
            {
                OpenPageForm = 2;

                button10.BackColor = Settings.Default.defaultGreenBackColor;
                button1.BackColor = Settings.Default.defaultOrangeBackColor;
                button2.BackColor = Settings.Default.defaultOrangeBackColor;

                panel1.Visible = false;
                panel3.Visible = false;
                panelMensuration.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button7.Visible = false;
                button8.Visible = false;

                button8.Visible = true;
                panel2.Visible = true;
                panel2.BringToFront();
            }
            else if (button.Name == button2.Name)
            {
                OpenPageForm = 3;

                panel1.Visible = false;
                panel2.Visible = false;
                panel3.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button7.Visible = false;
                button8.Visible = false;

                button2.BackColor = Settings.Default.defaultGreenBackColor;
                button1.BackColor = Settings.Default.defaultOrangeBackColor;
                button10.BackColor = Settings.Default.defaultOrangeBackColor;

                panelMensuration.Visible = true;
                panelMensuration.BringToFront();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DATABASE.AddNewConsultation(Id);
            DATABASE.RefreshAllConsultations(dataGridView1, Id, label5, label16);

            DATABASE.RefreshAllMensurations(dataGridView2, Id, label83, label31);
            GRAPH();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 9) //DELETE 
            {
                try
                {

                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    DialogResult dialogResult = MessageBox.Show("Voulez-vous vraiment supprimer la consultation " + row.Cells[0].Value + " du " + row.Cells[1].Value, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        DATABASE.DeleteConsultation(row.Cells[0].Value);
                        DATABASE.RefreshAllConsultations(dataGridView1, Id, label5, label16);
                        DATABASE.RefreshAllMensurations(dataGridView2, Id, label83, label31);
                        GRAPH();
                        DATABASE.GetTotalMoneyCountForMonthAndYear(FormMesPatients.label3, FormMesPatients.label4);
                    }
                }
                catch { DATABASE.GetTotalMoneyCountForMonthAndYear(FormMesPatients.label3, FormMesPatients.label4); }
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                OldConsultationIdValue = (int)dataGridView1[0, e.RowIndex].Value;
            }
            catch { }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                DATABASE.fixUTF8Character(dataGridView1, e.RowIndex); //replace "'" character with space

                DATABASE.UpdateConsultations(OldConsultationIdValue,
                    row.Cells[0].Value, row.Cells[1].Value, (row.Cells[2].Value), row.Cells[3].Value,
                    row.Cells[4].Value, row.Cells[5].Value, row.Cells[6].Value, row.Cells[7].Value,
                    row.Cells[8].Value, Id);

                DATABASE.RefreshAllConsultations(dataGridView1, Id, label5, label16);

                DATABASE.RefreshAllMensurations(dataGridView2, Id, label83, label31);

                DATABASE.GetTotalMoneyCountForMonthAndYear(FormMesPatients.label3, FormMesPatients.label4);
                GRAPH();
            }
            catch
            {
                DATABASE.RefreshAllConsultations(dataGridView1, Id, label5, label16);
                DATABASE.GetTotalMoneyCountForMonthAndYear(FormMesPatients.label3, FormMesPatients.label4);
            }
        }

        private void textBox_TextChanged(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb.Lines.Length >= 12 && e.KeyChar == '\r')
            {
                e.Handled = true;
            }
        }

        private void textBox13_KeyDown(object sender, KeyEventArgs e)
        {
            bool ctrlV = e.Modifiers == Keys.Control && e.KeyCode == Keys.V;

            if (ctrlV && Clipboard.ContainsText())
            {
                try
                {
                    Clipboard.SetText(Clipboard.GetText().Replace('\r', ' '));
                    e.Handled = true;

                }
                catch { }
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            label30.Text = "Longueur: " + tb.Text.Length + '\r' + "Longueur max: " + tb.MaxLength;
        }

        private void dataGridView2_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                OldConsultationIdValue = (int)dataGridView2[0, e.RowIndex].Value;
            }
            catch { }
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                DATABASE.fixUTF8Character(dataGridView2, e.RowIndex); //replace "'" character with space

                DATABASE.UpdateMensurations(OldConsultationIdValue,
                    row.Cells[0].Value, row.Cells[1].Value, (row.Cells[2].Value), (row.Cells[3].Value),
                    (row.Cells[4].Value), (row.Cells[5].Value), (row.Cells[6].Value), (row.Cells[7].Value),
                    (row.Cells[8].Value), (row.Cells[9].Value), (row.Cells[10].Value),
                    (row.Cells[11].Value), (row.Cells[12].Value), Id);

                DATABASE.RefreshAllConsultations(dataGridView2, Id, label5, label16);
                DATABASE.RefreshAllMensurations(dataGridView2, Id, label83, label31);
                DATABASE.GetTotalMoneyCountForMonthAndYear(FormMesPatients.label3, FormMesPatients.label4);
                GRAPH();
        }
            catch
            {
                DATABASE.RefreshAllConsultations(dataGridView1, Id, label5, label16);
                DATABASE.GetTotalMoneyCountForMonthAndYear(FormMesPatients.label3, FormMesPatients.label4);
            }
}

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Bitmap bmp = new Bitmap(Width, Height);
            //DrawToBitmap(bmp, new Rectangle(0, 0, Width, Height));
            //bmp.Save("Test.bmp");

            //Bitmap image = new Bitmap(bmp);
            //e.Graphics.DrawString("welcome to test", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(10, 10));

            var ptsX = 0;
            var ptsY = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {

                e.Graphics.DrawString(row.Cells[2].Value.ToString(), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(ptsX, ptsY));

                ptsX += 100;
                ptsY += 100;
            }
            //e.Graphics.DrawImage(image, 0, 0);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void cartesianChart1_DataClick(object sender, ChartPoint chartPoint)
        {
            MessageBox.Show(cartesianChart1.DefaultLegend.ToString());
        }

        private void cartesianChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (Dock == DockStyle.Fill)
            {
                Dock = DockStyle.None;
            }
            else
            {
                //FormBorderStyle = FormBorderStyle.None;
                Dock = DockStyle.Fill;
            }
        }

        private void header_panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (Dock == DockStyle.None)
            {
                AnimationsForms.Frm.Frm_MouseDown(this, e);
            }
        }

        private void header_panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dock == DockStyle.None)
            {
                AnimationsForms.Frm.Frm_MouseMove(this, e);
            }
        }

        private void header_panel_MouseUp(object sender, MouseEventArgs e)
        {
            if (Dock == DockStyle.None)
            {
                AnimationsForms.Frm.Frm_MouseUp(this, e);
            }
        }

        private void FormConsultationPatient_Click(object sender, EventArgs e)
        {
            BringToFront();
        }

        private void GRAPH()
        {
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisY.Clear();
            cartesianChart1.Series.Clear();

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Numero",
                MinValue = 1,
                //Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Mensurations",
                MinValue = 1,
                //LabelFormatter = value => value.ToString("C")
            });

            cartesianChart1.LegendLocation = LegendLocation.Top;

            SeriesCollection series = new SeriesCollection();

            var YEAR = Convert.ToDateTime(dataGridView2.Rows[0].Cells[1].Value).ToString("yyyy");
            var DAYS = Convert.ToDateTime(dataGridView2.Rows[0].Cells[1].Value).ToString("dd");

            List<double> POIDS = new List<double>();
            List<double> TAILLE = new List<double>();
            List<double> VENTRE = new List<double>();
            List<double> HANCHE = new List<double>();
            List<double> CUISSE = new List<double>();
            List<double> BRAS = new List<double>();
            POIDS.Add(0);
            TAILLE.Add(0);
            VENTRE.Add(0);
            HANCHE.Add(0);
            CUISSE.Add(0);
            BRAS.Add(0);

            //POIDS
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                //var Month = Convert.ToDateTime(row.Cells[1].Value).ToString("MM").TrimStart().Trim();
                POIDS.Add((double)row.Cells[2].Value);
                TAILLE.Add((double)row.Cells[3].Value);
                VENTRE.Add((double)row.Cells[4].Value);
                HANCHE.Add((double)row.Cells[5].Value);
                CUISSE.Add((double)row.Cells[6].Value);
                BRAS.Add((double)row.Cells[7].Value);
            }

            series.Add(new LineSeries() { Title = "POIDS [kg]", Values = new ChartValues<double>(POIDS) });
            series.Add(new LineSeries() { Title = "TAILLE [cm]", Values = new ChartValues<double>(TAILLE) });
            series.Add(new LineSeries() { Title = "VENTRE [cm]", Values = new ChartValues<double>(VENTRE) });
            series.Add(new LineSeries() { Title = "HANCHE [cm]", Values = new ChartValues<double>(HANCHE) });
            series.Add(new LineSeries() { Title = "CUISSE [cm]", Values = new ChartValues<double>(CUISSE) });
            series.Add(new LineSeries() { Title = "BRAS [cm]", Values = new ChartValues<double>(BRAS) });
            cartesianChart1.Series = series;
        }
    }
}
