﻿using System;
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
    public partial class FormGroups : Form
    {
        GraphicsPath border;
        Region region;
        SqlConnection sql = new SqlConnection(@"Data Source=DESKTOP-KQ56AQ7\BBB; Initial Catalog=bazapraktika; Integrated Security=True");
        public FormGroups()
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

        private void FormGroups_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bazapraktikaDataSet.Groups". При необходимости она может быть перемещена или удалена.
            this.groupsTableAdapter.Fill(this.bazapraktikaDataSet.Groups);

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Close();
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
                SqlCommand com = new SqlCommand("DELETE FROM Groups WHERE Id_Groups=@id", sql);
                int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                com.Parameters.AddWithValue("@id", id);
                try
                {
                    com.ExecuteNonQuery();
                    MessageBox.Show("Вы успешно удалили запись.");
                    string commandText = "select * from Groups";
                    SqlDataAdapter itm = new SqlDataAdapter(commandText, sql);
                    DataTable dt2 = new DataTable();
                    itm.Fill(dt2);
                    dataGridView1.DataSource = dt2;
                    sql.Close();
                    tBox1.Text = "";
                    tBox2.Text = "";
                    tBox3.Text = "";
                    tBox4.Text = "";
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
            if ((tBox1.Text == "") || (tBox2.Text == "") || (tBox3.Text == "")|| (tBox1.Text == " ") || (tBox2.Text == " ") || (tBox3.Text == " "))
            {
                MessageBox.Show("Не введены или не полностью введены данные для добавления!");
                tBox1.Text = "";
                tBox2.Text = "";
                tBox3.Text = "";
                tBox4.Text = "";
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
                    SqlCommand com = new SqlCommand("INSERT INTO Groups VALUES (@GroupName,@NumberPeople,@ID_Specialty)", sql);
                    com.Parameters.AddWithValue("@GroupName", tBox1.Text);
                    com.Parameters.AddWithValue("@NumberPeople", tBox2.Text);
                    com.Parameters.AddWithValue("@ID_Specialty", tBox3.Text);
                    try
                    {
                        com.ExecuteNonQuery();
                        MessageBox.Show("Вы успешно Добаили запись.");
                        tBox1.Text = "";
                        tBox2.Text = "";
                        tBox3.Text = "";
                        tBox4.Text = "";
                        string commandText = "select * from Groups";
                        SqlDataAdapter itm = new SqlDataAdapter(commandText, sql);
                        DataTable dt2 = new DataTable();
                        itm.Fill(dt2);
                        dataGridView1.DataSource = dt2;
                        sql.Close();
                        tBox1.Text = "";
                        tBox2.Text = "";
                        tBox3.Text = "";
                        tBox4.Text = "";
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
            if ((tBox1.Text == "") || (tBox2.Text == "") || (tBox3.Text == "") || (tBox4.Text == "") || (tBox1.Text == " ") || (tBox2.Text == " ") || (tBox3.Text == " ") || (tBox4.Text == " "))
            {
                MessageBox.Show("Не введены или не полностью введены данные для изменения!");
                tBox1.Text = "";
                tBox2.Text = "";
                tBox3.Text = "";
                tBox4.Text = "";
                return;
            }
            else
            {  
                string GroupName = Convert.ToString(tBox1.Text);
                int NumberPeople = Convert.ToInt32(tBox2.Text);
                int ID_Specialty = Convert.ToInt32(tBox3.Text);
                int Id_Groups = Convert.ToInt32(tBox4.Text);
                SqlCommand cmd;
                sql.Open();
                cmd = new SqlCommand();
                cmd.Connection = sql;
                if (MessageBox.Show("Вы уверены, что хотите изменить запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string con = "UPDATE Groups SET GroupName= '" + GroupName + "', NumberPeople= '" + NumberPeople + "', ID_Specialty= '" + ID_Specialty + "' WHERE Id_Groups= " + Id_Groups + "";

                    SqlCommand com = new SqlCommand(con, sql);
                    try
                    {
                        com.ExecuteNonQuery();
                        MessageBox.Show("Вы успешно изменили запись.");
                        tBox1.Text = "";
                        tBox2.Text = "";
                        tBox3.Text = "";
                        tBox4.Text = "";
                        string commandText = "select * from Groups";
                        SqlDataAdapter itm = new SqlDataAdapter(commandText, sql);
                        DataTable dt2 = new DataTable();
                        itm.Fill(dt2);
                        dataGridView1.DataSource = dt2;
                        sql.Close();
                        tBox1.Text = "";
                        tBox2.Text = "";
                        tBox3.Text = "";
                        tBox4.Text = "";
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
