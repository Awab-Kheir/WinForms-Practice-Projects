using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class StopwatchForm : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public StopwatchForm()
        {
            InitializeComponent();
        }

        private TimeSpan elapsedTime = TimeSpan.Zero;

        private void StopwatchForm_Load(object sender, EventArgs e)
        {
            timer1.Start();

            //For Maximize and Minimize
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = true;

            MinimizeBox = true;
            MaximizeBox = false;
            ControlBox = true;
        }

        private void lblScreen_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
                timer1.Enabled = false;
            else 
                timer1.Enabled = true;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(1));

            lblScreen.Text = elapsedTime.ToString(@"hh\:mm\:ss");
        }

        private void lblScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = this.Location;
            }
        }

        private void lblScreen_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));

                this.Location = new Point(
                    dragFormPoint.X + diff.X,
                    dragFormPoint.Y + diff.Y);
            }
        }

        private void lblScreen_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        //For Maximize and Minimize
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_SYSMENU = 0x00080000;
                const int WS_MINIMIZEBOX = 0x00020000;

                CreateParams cp = base.CreateParams;
                cp.Style |= WS_SYSMENU;
                cp.Style |= WS_MINIMIZEBOX;
                return cp;
            }
        }
    }
}

