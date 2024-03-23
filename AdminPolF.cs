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
    public partial class AdminPolF : Form
    {
        GraphicsPath border;
        Region region;
        SqlConnection sql = new SqlConnection(@"Data Source=DESKTOP-KQ56AQ7\BBB; Initial Catalog=bazapraktika; Integrated Security=True");
        public static int indexrow;
        public static string id_user;
        public static string Loginn;
        public static string Passwordd;
        public static string Roles;
        public static string Surnamess;
        public static string Namess;
        public static string Patrtonymicss;
        public AdminPolF()
        {
            InitializeComponent();
            border = GetRoundedRectanglePath(this.Bounds, new SizeF(50, 50));
            region = new Region(border);
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
            Form a = new AdminF();
            a.Show();
            this.Hide();
        }

        private void AdminPolF_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bazapraktikaDataSet.Users". При необходимости она может быть перемещена или удалена.
            this.usersTableAdapter.Fill(this.bazapraktikaDataSet.Users);

        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "") || (textBox2.Text == "") || (textBox3.Text == "") || (textBox4.Text == "") || (textBox5.Text == "") || (textBox6.Text == "") || (textBox1.Text == " ") || (textBox2.Text == " ") || (textBox3.Text == " ") || (textBox4.Text == " ") || (textBox5.Text == " ") || (textBox6.Text == " "))
            {
                MessageBox.Show("Не введены или не полностью введены данные для добавления!");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                return;
            }
            else
            {
                SqlConnection sql = new SqlConnection(@"Data Source=DESKTOP-KQ56AQ7\BBB; Initial Catalog=bazapraktika; Integrated Security=True");
                SqlCommand cmd;
                sql.Open();
                cmd = new SqlCommand();
                cmd.Connection = sql;
                if (MessageBox.Show("Вы уверены, что хотите добавить запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlCommand com = new SqlCommand("INSERT INTO Users VALUES (@Loginn,@Passwordd,@Rolee,@Surnamess,@Namess,@Patronymicss)", sql);
                    com.Parameters.AddWithValue("@Loginn", textBox1.Text);
                    com.Parameters.AddWithValue("@Passwordd", textBox2.Text);
                    com.Parameters.AddWithValue("@Rolee", textBox3.Text);
                    com.Parameters.AddWithValue("@Surnamess", textBox4.Text);
                    com.Parameters.AddWithValue("@Namess", textBox5.Text);
                    com.Parameters.AddWithValue("@Patronymicss", textBox6.Text);
                    try
                    {
                        com.ExecuteNonQuery();
                        MessageBox.Show("Вы успешно Добаили запись.");
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        string commandText = "select * from Users";
                        SqlDataAdapter itm = new SqlDataAdapter(commandText, sql);
                        DataTable dt2 = new DataTable();
                        itm.Fill(dt2);
                        dataGridView1.DataSource = dt2;
                        sql.Close();
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                    }
                    catch
                    {
                        MessageBox.Show("Добавить не удалось!");
                    }
                }
                sql.Close();
            }
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            sql.Open();
            cmd = new SqlCommand();
            cmd.Connection = sql;
            if (MessageBox.Show("Вы уверены, что хотите удалить запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlCommand com = new SqlCommand("DELETE FROM Users WHERE Id_users=@id", sql);
                int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                com.Parameters.AddWithValue("@id", id);
                try
                {
                    com.ExecuteNonQuery();
                    MessageBox.Show("Вы успешно удалили запись.");
                    string commandText = "select * from Users";
                    SqlDataAdapter itm = new SqlDataAdapter(commandText, sql);
                    DataTable dt2 = new DataTable();
                    itm.Fill(dt2);
                    dataGridView1.DataSource = dt2;
                    sql.Close();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                }
                catch
                {
                    MessageBox.Show("Удалить не удалось!");
                }
            }
            sql.Close();
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "") || (textBox2.Text == "") || (textBox3.Text == "") || (textBox4.Text == "") || (textBox5.Text == "") || (textBox6.Text == "") || (textBox1.Text == " ") || (textBox2.Text == " ") || (textBox3.Text == " ") || (textBox4.Text == " ") || (textBox5.Text == " ") || (textBox6.Text == " "))
            {
                MessageBox.Show("Не введены или не полностью введены данные для изменения!");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                return;
            }
            else
            {
                string Loginn = Convert.ToString(textBox1.Text);
                string Passwordd = Convert.ToString(textBox2.Text);
                string Rolee = Convert.ToString(textBox3.Text);
                string Surname = Convert.ToString(textBox4.Text);
                string Namee = Convert.ToString(textBox5.Text);
                string Patronymic = Convert.ToString(textBox6.Text);
                SqlCommand cmd;
                sql.Open();
                cmd = new SqlCommand();
                cmd.Connection = sql;
                if (MessageBox.Show("Вы уверены, что хотите изменить запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string con = "UPDATE Users SET Loginn= '" + Loginn + "', Passwordd= '" + Passwordd + "', Rolee= '" + Rolee + "', Surname= '" + Surname + "', Namee= '" + Namee + "', Patronymic= '" + Patronymic + "' WHERE Rolee= " + Rolee + "";

                    SqlCommand com = new SqlCommand(con, sql);
                    try
                    {
                        com.ExecuteNonQuery();
                        MessageBox.Show("Вы успешно изменили запись.");
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        string commandText = "select * from Users";
                        SqlDataAdapter itm = new SqlDataAdapter(commandText, sql);
                        DataTable dt2 = new DataTable();
                        itm.Fill(dt2);
                        dataGridView1.DataSource = dt2;
                        sql.Close();
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                    }
                    catch
                    {
                        MessageBox.Show("Изменить не удалось!");
                    }
                }
                sql.Close();
            }
        }
    }
}
