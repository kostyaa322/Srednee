using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Srednee
{
    public partial class AvtorizF : Form
    {
        GraphicsPath border;
        Region region;
        public static string Rolee = "";
        public AvtorizF()
        {
            InitializeComponent();
            border = GetRoundedRectanglePath(this.Bounds, new SizeF(50, 50));
            region = new Region(border);
            PasText.UseSystemPasswordChar = false;
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            this.Region = region;
        }
        private GraphicsPath GetRoundedRectanglePath(RectangleF rect, SizeF roundSize)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLine(rect.Left + roundSize.Width / 2, rect.Top, rect.Right - roundSize.Width / 2, rect.Top);
            path.AddArc(rect.Right - roundSize.Width, rect.Top, roundSize.Width, roundSize.Height, 270, 90);
            path.AddLine(rect.Right, rect.Top + roundSize.Height / 2, rect.Right, rect.Bottom - roundSize.Height / 2);
            path.AddArc(rect.Right - roundSize.Width, rect.Bottom - roundSize.Height, roundSize.Width, roundSize.Height, 0, 90);
            path.AddLine(rect.Right - roundSize.Width / 2, rect.Bottom, rect.Left + roundSize.Width / 2, rect.Bottom);
            path.AddArc(rect.Left, rect.Bottom - roundSize.Height, roundSize.Width, roundSize.Height, 90, 90);
            path.AddLine(rect.Left, rect.Bottom - roundSize.Height / 2, rect.Left, rect.Top + roundSize.Height / 2);
            path.AddArc(rect.Left, rect.Top, roundSize.Width, roundSize.Height, 180, 90);
            path.CloseFigure();
            return path;
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }
            base.WndProc(ref m);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            SqlDataReader dr;
            string Loginn = NameText.Text;
            string Passwordd = PasText.Text;
            SqlConnection sql = new SqlConnection(@"Data Source=DESKTOP-KQ56AQ7\BBB; Initial Catalog=bazapraktika; Integrated Security=True");
            cmd = new SqlCommand();
            sql.Open();
            cmd.Connection = sql;
            string pp = "SELECT * FROM Users WHERE Loginn = '" + NameText.Text + "'AND Passwordd='" + PasText.Text + "'";
            cmd.CommandText = pp;
            dr = cmd.ExecuteReader();
            if ((NameText.Text == "") || (PasText.Text == "") || (NameText.Text == " ") || (PasText.Text == " "))
            {
                MessageBox.Show("Не введены или не полностью введены данные для входа!");
                NameText.Text = "";
                PasText.Text = "";
            }
            else
            {
                if (dr.Read())
                {
                    Rolee = dr.GetValue(3).ToString();
                    string gg = Rolee.Substring(0, 1);
                    MessageBox.Show("Добро пожаловать " + NameText.Text + "!");
                    if (gg == "1")
                    {
                        Form kd = new AdminF();
                        kd.Show();
                        this.Hide();
                    }
                    if (gg == "2")
                    {
                        Form kd = new OperatorF();
                        kd.Show();
                        this.Hide();
                    }
                    if (gg == "3")
                    {
                        Form kd = new AnalitikF();
                        kd.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Не верно введеный логин или пароль!");
                }
            }
            sql.Close();
        }

        private void NameText_Enter(object sender, EventArgs e)
        {
            if (NameText.Text == "Логин")
            {
                NameText.Text = "";
                NameText.ForeColor = Color.Black;
            }
        }

        private void PasText_Enter(object sender, EventArgs e)
        {
            if (PasText.Text == "Пароль")
            {
                PasText.Text = "";
                PasText.ForeColor = Color.Black;
                PasText.UseSystemPasswordChar = true;
            }
        }

        private void NameText_Leave(object sender, EventArgs e)
        {
            if (NameText.Text == "")
            {
                NameText.Text = "Логин";
                NameText.ForeColor = Color.Silver;
            }
        }

        private void PasText_Leave(object sender, EventArgs e)
        {
            if (PasText.Text == "")
            {
                PasText.Text = "Пароль";
                PasText.ForeColor = Color.Silver;
                PasText.UseSystemPasswordChar = false;
            }
        }

        private void AvtorizF_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Pen pen = new Pen(Brushes.White, 4);
            pen.LineJoin = LineJoin.Bevel;
            pen.MiterLimit = 4;
            g.DrawRectangle(pen, new Rectangle(NameText.Location.X - 1, NameText.Location.Y - 1, NameText.Width + 1, NameText.Height + 1));
            g.DrawRectangle(pen, new Rectangle(PasText.Location.X - 1, PasText.Location.Y - 1, PasText.Width + 1, PasText.Height + 1));
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                PasText.UseSystemPasswordChar = false;
            }
            else
            {
                PasText.UseSystemPasswordChar = true;

            }
        }

        private void NameText_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'A' || l > 'z') && l != '\b')
            {
                e.Handled = true;
            }
        }

        private void PasText_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if (!char.IsDigit(l) && l != 8)
            {
                e.Handled = true;
            }
        }
    }
}
