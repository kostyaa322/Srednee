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
    public partial class FormSpecial : Form
    {
        GraphicsPath border;
        Region region;
        SqlConnection sql = new SqlConnection(@"Data Source=DESKTOP-KQ56AQ7\BBB; Initial Catalog=bazapraktika; Integrated Security=True");
        public FormSpecial()
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormSpecial_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bazapraktikaDataSet.Specialty1". При необходимости она может быть перемещена или удалена.
            this.specialty1TableAdapter.Fill(this.bazapraktikaDataSet.Specialty1);

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            sql.Open();
            cmd = new SqlCommand();
            cmd.Connection = sql;
            if (MessageBox.Show("Вы уверены, что хотите удалить запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlCommand com = new SqlCommand("DELETE FROM Specialty1 WHERE ID_Specialty=@id", sql);
                int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                com.Parameters.AddWithValue("@id", id);
                try
                {
                    com.ExecuteNonQuery();
                    MessageBox.Show("Вы успешно удалили запись.");
                    string commandText = "select * from Specialty1";
                    SqlDataAdapter itm = new SqlDataAdapter(commandText, sql);
                    DataTable dt2 = new DataTable();
                    itm.Fill(dt2);
                    dataGridView1.DataSource = dt2;
                    sql.Close();
                    tBox1.Text = "";
                    tBox2.Text = "";
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
            if ((tBox1.Text == "") ||  (tBox1.Text == " "))
            {
                MessageBox.Show("Не введены или не полностью введены данные для добавления!");
                tBox1.Text = "";
                tBox2.Text = "";
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
                    SqlCommand com = new SqlCommand("INSERT INTO Specialty1 VALUES (@NameSpecialty)", sql);
                    com.Parameters.AddWithValue("@NameSpecialty", tBox1.Text);
                    try
                    {
                        com.ExecuteNonQuery();
                        MessageBox.Show("Вы успешно Добаили запись.");
                        tBox1.Text = "";
                        tBox2.Text = "";
                        string commandText = "select * from Specialty1";
                        SqlDataAdapter itm = new SqlDataAdapter(commandText, sql);
                        DataTable dt2 = new DataTable();
                        itm.Fill(dt2);
                        dataGridView1.DataSource = dt2;
                        sql.Close();
                        tBox1.Text = "";
                        tBox2.Text = "";
                    }
                    catch
                    {
                        MessageBox.Show("Добавить не удалось!");
                    }
                }
                sql.Close();
            }
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            if ((tBox1.Text == "") || (tBox2.Text == "") || (tBox1.Text == " ") || (tBox2.Text == " "))
            {
                MessageBox.Show("Не введены или не полностью введены данные для изменения!");
                tBox1.Text = "";
                tBox2.Text = "";

                return;
            }
            else
            {
                string NameSpecialty = Convert.ToString(tBox1.Text);
                string ID_Specialty = Convert.ToString(tBox2.Text);
                SqlCommand cmd;
                sql.Open();
                cmd = new SqlCommand();
                cmd.Connection = sql;
                if (MessageBox.Show("Вы уверены, что хотите изменить запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string con = "UPDATE Specialty1 SET NameSpecialty= '" + NameSpecialty + "' WHERE ID_Specialty= " + ID_Specialty + "";

                    SqlCommand com = new SqlCommand(con, sql);
                    try
                    {
                        com.ExecuteNonQuery();
                        MessageBox.Show("Вы успешно изменили запись.");
                        tBox1.Text = "";
                        tBox2.Text = "";
                        string commandText = "select * from Specialty1";
                        SqlDataAdapter itm = new SqlDataAdapter(commandText, sql);
                        DataTable dt2 = new DataTable();
                        itm.Fill(dt2);
                        dataGridView1.DataSource = dt2;
                        sql.Close();
                        tBox1.Text = "";
                        tBox2.Text = "";
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
