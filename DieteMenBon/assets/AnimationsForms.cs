using DieteMenBon.Properties;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DieteMenBon.assets
{
    class AnimationsForms
    {
        // 'MOUVE FORM WITH MOUSE'
        public static AnimationsForms Frm = new AnimationsForms();

        private bool drag;
        private int mousex;
        private int mousey;

        public void Frm_MouseDown(object sender, MouseEventArgs e)
        {
            Form frm = sender as Form;

            drag = true;

            mousex = Cursor.Position.X - frm.Left;
            mousey = Cursor.Position.Y - frm.Top;
        }

        public void Frm_MouseMove(object sender, MouseEventArgs e)
        {
            Form pn = sender as Form;

            if (drag)
            {
                pn.Top = Cursor.Position.Y - mousey;
                pn.Left = Cursor.Position.X - mousex;
            }
        }

        public void Frm_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }
        // MOUVE FORM WITH MOUSE'






        public static async Task OpenForm(Form frm)
        {
            try
            {

                while (frm.Opacity <= 1)
                {
                    await Task.Delay(8);

                    if (frm.Opacity <= 1)
                    {
                        frm.Opacity += 0.1;

                        if (frm.Opacity == 1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }

            }
            catch { }
        }

        public static async Task MinimizeForm(Form frm)
        {
            try
            {
                await Task.Delay(100);

                while (frm.Opacity >= 0)
                {
                    await Task.Delay(8);

                    if (frm.Opacity <= 1)
                    {
                        frm.Opacity -= 0.1;

                        if (frm.Opacity == 0)
                        {
                            await Task.Delay(10);
                            frm.WindowState = FormWindowState.Minimized;
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }

            }
            catch { }
        }

        public static async Task HideForm(Form frm)
        {
            try
            {
                await Task.Delay(100);

                while (frm.Opacity >= 0)
                {
                    await Task.Delay(8);

                    if (frm.Opacity <= 1)
                    {
                        frm.Opacity -= 0.1;

                        if (frm.Opacity == 0)
                        {
                            await Task.Delay(10);
                            frm.Hide();
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }

            }
            catch { }
        }

        public static async Task CloseApplicationForm(Form frm)
        {
            try
            {
                await Task.Delay(100);

                while (frm.Opacity >= 0)
                {
                    await Task.Delay(8);

                    if (frm.Opacity <= 1)
                    {
                        frm.Opacity -= 0.1;

                        if (frm.Opacity == 0)
                        {
                            await Task.Delay(10);
                            frm.WindowState = FormWindowState.Minimized;
                            await Task.Delay(1000);
                            Application.Exit();
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }

            }
            catch { }
        }

        public static async Task CloseForm(Form frm, FormLogin loginForm)
        {
            try
            {
                await Task.Delay(100);

                while (frm.Opacity >= 0)
                {
                    await Task.Delay(8);

                    if (frm.Opacity <= 1)
                    {
                        frm.Opacity -= 0.1;

                        if (frm.Opacity == 0)
                        {
                            await Task.Delay(10);
                            frm.Close();
                            await Task.Delay(500);
                            loginForm.Show();
                            OpenForm(loginForm);
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }

            }
            catch { }
        }

        public static async Task DisconnectedForm(Form frm, FormLogin loginForm)
        {
            try
            {
                await Task.Delay(100);

                while (frm.Opacity >= 0)
                {
                    await Task.Delay(8);

                    if (frm.Opacity <= 1)
                    {
                        frm.Opacity -= 0.1;

                        if (frm.Opacity == 0)
                        {
                            await Task.Delay(10);
                            frm.Close();
                            await Task.Delay(500);
                            loginForm.Show();
                            loginForm.label3.Visible = true;
                            loginForm.label3.ForeColor = Settings.Default.defaultOrangeBackColor;
                            loginForm.label3.Text = "Vous avez été déconnecté";
                            OpenForm(loginForm);
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }

            }
            catch { }
        }

        public static async Task ChangeNumber(Label LabelValue, double NewValue)
        {
            try
            {
                double val = Convert.ToDouble(LabelValue.Text.Replace("€", ""));
                double speed = Convert.ToDouble(Math.Abs(NewValue / 60).ToString("0.000"));

                if (val > 0)
                {
                    val = 0;
                }

                while (val != NewValue)
                {
                    await Task.Delay(1);

                    if (Math.Round(val) == Math.Round(NewValue) || (NewValue.CompareTo(val) > 100) && (NewValue.CompareTo(val) < 100))
                    {
                        await Task.Delay(10);
                        LabelValue.Text = NewValue + "€";
                        return;
                    }
                    else if (Math.Round(val) <= Math.Round(NewValue))
                    {
                        val += speed;
                        LabelValue.Text = Math.Round(val).ToString() + "€";
                    }
                    else if (Math.Round(val) >= Math.Round(NewValue))
                    {
                        val -= speed;
                        LabelValue.Text = Math.Round(val).ToString() + "€";
                    }
                }

                await Task.Delay(10);
                LabelValue.Text = Math.Round(val).ToString() + "€";
            }
            catch { }
        }

    }
}
