using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Srednee
{
    public partial class OperatorF : Form
    {
        GraphicsPath border;
        Region region;
        public OperatorF()
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
            Form a = new AvtorizF();
            a.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Form a = new OperatorInfF();
            a.Show();
            this.Hide();
        }
    }
}
