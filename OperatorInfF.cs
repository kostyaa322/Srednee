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
    public partial class OperatorInfF : Form
    {
        GraphicsPath border;
        Region region;
        SqlConnection sql = new SqlConnection(@"Data Source=DESKTOP-KQ56AQ7\BBB; Initial Catalog=bazapraktika; Integrated Security=True");
        public OperatorInfF()
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

        private void button1_Click(object sender, EventArgs e)
        {
            Form a = new OperatorF();
            a.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void OperatorInfF_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bazapraktikaDataSet.Students". При необходимости она может быть перемещена или удалена.
            this.studentsTableAdapter.Fill(this.bazapraktikaDataSet.Students);

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            sql.Open();
            cmd = new SqlCommand();
            cmd.Connection = sql;
            if (MessageBox.Show("Вы уверены, что хотите удалить запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlCommand com = new SqlCommand("DELETE FROM Students WHERE Id_Students=@id", sql);
                int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                com.Parameters.AddWithValue("@id", id);
                try
                {
                    com.ExecuteNonQuery();
                    MessageBox.Show("Вы успешно удалили запись.");
                    string commandText = "select * from Students";
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
                    textBox7.Text = "";
                    textBox8.Text = "";
                }
                catch
                {
                    MessageBox.Show("Удалить не удалось!");
                }
            }
            sql.Close();
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "") || (textBox2.Text == "") || (textBox3.Text == "") || (textBox4.Text == "") || (textBox5.Text == "") || (textBox6.Text == "") || (textBox7.Text == "") || (textBox8.Text == "") || (textBox1.Text == " ") || (textBox2.Text == " ") || (textBox3.Text == " ") || (textBox4.Text == " ") || (textBox5.Text == " ") || (textBox6.Text == " ") || (textBox7.Text == " ") || (textBox8.Text == " "))
            {
                MessageBox.Show("Не введены или не полностью введены данные для добавления!");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
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
                    SqlCommand com = new SqlCommand("INSERT INTO Students VALUES (@Surname,@Namee,@Patronymic,@DateBirth,@Gender,@Age,@Id_Groups,@ID_Course)", sql);
                    com.Parameters.AddWithValue("@Surname", textBox1.Text);
                    com.Parameters.AddWithValue("@Namee", textBox2.Text);
                    com.Parameters.AddWithValue("@Patronymic", textBox3.Text);
                    com.Parameters.AddWithValue("@DateBirth", textBox4.Text);
                    com.Parameters.AddWithValue("@Gender", textBox5.Text);
                    com.Parameters.AddWithValue("@Age", textBox6.Text);
                    com.Parameters.AddWithValue("@Id_Groups", textBox7.Text);
                    com.Parameters.AddWithValue("@ID_Course", textBox8.Text);
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
                        textBox7.Text = "";
                        textBox8.Text = "";
                        string commandText = "select * from Students";
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
                        textBox7.Text = "";
                        textBox8.Text = "";
                    }
                    catch
                    {
                        MessageBox.Show("Добавить не удалось!");
                    }
                }
                sql.Close();
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

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
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                return;
            }
            else
            {
                string Surname = Convert.ToString(textBox1.Text);
                string Namee = Convert.ToString(textBox2.Text);
                string Patronymic = Convert.ToString(textBox3.Text);
                DateTime DateBirth = Convert.ToDateTime(textBox4.Text);
                string Gender = Convert.ToString(textBox5.Text);
                int Age = Convert.ToInt32(textBox6.Text);
                int Id_Groups = Convert.ToInt32(textBox7.Text);
                int ID_Course = Convert.ToInt32(textBox8.Text);
                int ID_Students = Convert.ToInt32(textBox9.Text);
                SqlCommand cmd;
                sql.Open();
                cmd = new SqlCommand();
                cmd.Connection = sql;
                if (MessageBox.Show("Вы уверены, что хотите изменить запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string con = "UPDATE Students SET Surname= '" + Surname + "', Namee= '" + Namee + "', Patronymic= '" + Patronymic + "', DateBirth= '" + DateBirth + "', Gender= '" + Gender + "', Age= '" + Age + "', Id_Groups= '" + Id_Groups + "', ID_Course= '" + ID_Course + "' WHERE ID_Students= " + ID_Students + "";

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
                        textBox7.Text = "";
                        textBox8.Text = "";
                        textBox9.Text = "";
                        string commandText = "select * from Students";
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
                        textBox7.Text = "";
                        textBox8.Text = "";
                    }
                    catch
                    {
                        MessageBox.Show("Изменить не удалось!");
                    }
                }
                sql.Close();
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (textPoisk.Text == "")
            {
                MessageBox.Show("Не введен ID для поиска");
                return;
            }
            else
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Students Where Id_Students=@ID", sql);
                cmd.Parameters.AddWithValue("ID", (textPoisk.Text));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                sql.Close();
            }
        }

        private void pictureBox5_DoubleClick(object sender, EventArgs e)
        {
            sql.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Students", sql);
            cmd.Parameters.AddWithValue("ID", (textPoisk.Text));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            textPoisk.Text = "";
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            sql.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form a = new FormGroups();
            a.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form a = new FormSpecial();
            a.Show();
        }
    }
}
