using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EmployeeManagementSystem
{
    public partial class EmployeeManagementForm : Form
    {
        public EmployeeManagementForm()
        {
            InitializeComponent();
            CreateCustomTitleBar();
        }
        // =======================
        // ===== Title Bar Code ===
        // =======================

        // WinAPI للسحب
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        // Controls
        Panel titleBar;
        Label titleText;
        PictureBox iconBox;

        // إنشاء الشريط
        private void CreateCustomTitleBar()
        {
            // إزالة شريط الويندوز
            this.FormBorderStyle = FormBorderStyle.None;

            // شريط العنوان
            titleBar = new Panel();
            titleBar.Height = 35;
            titleBar.Dock = DockStyle.Top;
            titleBar.BackColor = Color.FromArgb(0, 120, 215);  // يمكنك تغيير اللون
            titleBar.MouseDown += TitleBar_MouseDown;
            this.Controls.Add(titleBar);

            // PictureBox للأيقونة
            iconBox = new PictureBox();
            iconBox.Width = 30;
            iconBox.Dock = DockStyle.Left;
            iconBox.SizeMode = PictureBoxSizeMode.CenterImage;

            // ضع الأيقونة الخاصة بالفورم
            iconBox.Image = this.Icon.ToBitmap();

            titleBar.Controls.Add(iconBox);


            // نص العنوان
            titleText = new Label();
            titleText.Text = this.Text;
            titleText.ForeColor = Color.White;
            titleText.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            titleText.AutoSize = false;
            titleText.TextAlign = ContentAlignment.MiddleLeft;
            titleText.Dock = DockStyle.Fill;
            titleText.Padding = new Padding(30, 0, 0, 0);
            titleText.MouseDown += TitleBar_MouseDown;
            titleBar.Controls.Add(titleText);

            // زر التصغير
            btnMin = new Button();
            btnMin.Text = "—";
            btnMin.Dock = DockStyle.Right;
            btnMin.Width = 45;
            btnMin.FlatStyle = FlatStyle.Flat;
            btnMin.FlatAppearance.BorderSize = 0;
            btnMin.ForeColor = Color.White;
            btnMin.BackColor = Color.FromArgb(0, 120, 215);
            btnMin.Click += (s, e) => this.WindowState = FormWindowState.Minimized;
            titleBar.Controls.Add(btnMin);

            // زر التكبير
            btnMax = new Button();
            btnMax.Text = "◻";
            btnMax.Dock = DockStyle.Right;
            btnMax.Width = 45;
            btnMax.FlatStyle = FlatStyle.Flat;
            btnMax.FlatAppearance.BorderSize = 0;
            btnMax.ForeColor = Color.White;
            btnMax.BackColor = Color.FromArgb(0, 120, 215);
            btnMax.Click += BtnMax_Click;
            titleBar.Controls.Add(btnMax);

            // زر الإغلاق
            btnClose = new Button();
            btnClose.Text = "✕";
            btnClose.Dock = DockStyle.Right;
            btnClose.Width = 45;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.ForeColor = Color.White;
            btnClose.BackColor = Color.Red;
            btnClose.Click += (s, e) => this.Close();
            titleBar.Controls.Add(btnClose);

        }

        // سحب الفوم
        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        // تكبير – استعادة
        private void BtnMax_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                btnMax.Text = "❐";
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                btnMax.Text = "◻";
            }
        }

        /******************************************************************************************************/

        public void ResetColomn()
        {
            txtID.Clear();          //  احسن .Clear();  بس  txtID.Text = "";  نفسها  
            txtFullName.Clear();
            //txtBirth.Clear();
            txtCountry.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtJop.Clear();
            txtDepartment.Clear();
            //dtpHireDate.Clear();
            txtSalary.Clear();
            //cbStatus.Clear();
            //cbLevel.Clear();
            txtID.Focus();
        }

        /***********************************************************************/


        private void EmployeeManagementForm_Paint(object sender, PaintEventArgs e)
        {
            Color MediumBlue = Color.FromArgb(0, 0, 192);

            Pen Pen = new Pen(MediumBlue);
            Pen.Width = 10;

            //Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            Pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;     //LineCap.ArrowAnchor
            Pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            //draw Horizental lines
            e.Graphics.DrawLine(Pen, 0, 120, 1300, 120);
      

        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtFullName.Text))
                return;

            ListViewItem item = new ListViewItem(txtID.Text.Trim());


            item.SubItems.Add(txtFullName.Text.Trim());
            item.SubItems.Add(dtpBirth.Text.Trim());
            item.SubItems.Add(txtCountry.Text.Trim());


            if (rbMale.Checked)
            {
                item.ImageIndex = 0;
                item.SubItems.Add(rbMale.Text.Trim());
            }
            else
            {
                item.ImageIndex = 1;
                item.SubItems.Add(rbFemale.Text.Trim());
            }


        
            item.SubItems.Add(txtAddress.Text.Trim());
            item.SubItems.Add(txtPhone.Text.Trim());
            item.SubItems.Add(txtEmail.Text.Trim());
            item.SubItems.Add(txtJop.Text.Trim());
            item.SubItems.Add(txtDepartment.Text.Trim());
            item.SubItems.Add(dtpHireDate.Text.Trim());
            item.SubItems.Add(txtSalary.Text.Trim());
            item.SubItems.Add(cbStatus.Text.Trim());
            item.SubItems.Add(cbLevel.Text.Trim());
            listView1.Items.Add(item);

            ResetColomn();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                listView1.Items.Remove(listView1.SelectedItems[0]);
            }
        }

        private void EmployeeManagementForm_Load(object sender, EventArgs e)
        {
            if (cbView.Items.Count > 0)
                cbView.SelectedIndex = 0;
            //cbView.SelectedItem = "Details";  or

            if (cbStatus.Items.Count > 0)
                cbStatus.SelectedIndex = 0;

            if (cbLevel.Items.Count > 0)
                cbLevel.SelectedIndex = 0;
        }

        private void cbView_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch(cbView.SelectedIndex)
            {
                case 0:
                    listView1.View = View.Details;
                    break;

                case 1:
                    listView1.View = View.LargeIcon;
                    break;

                case 2:
                    listView1.View = View.SmallIcon;
                    break;

                case 3:
                    listView1.View = View.List;
                    break;

                case 4:
                    listView1.View = View.Tile;
                    break;
            }

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show((listView1.SelectedItems[0].Text.ToString()));// +""+ listView1.SelectedItems[1]));

            //MessageBox.Show(listView1.SelectedItems[0].Text);   he
        }




        private void SetSubItem(ListViewItem item, int index, string text)
        {
            if (item == null) return;

            while (item.SubItems.Count <= index)
                item.SubItems.Add(string.Empty);

            item.SubItems[index].Text = text ?? string.Empty;
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            // تحقق من وجود عنصر محدد
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("اختر سجلًا أولاً ثم اضغط تعديل.", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // تحقق من الحقول الإلزامية (نفس التحقق في زر الإضافة)
            if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtFullName.Text))
                return;

            var item = listView1.SelectedItems[0];

            // الرقم (النص الرئيسي)
            item.Text = txtID.Text.Trim();
            // بقية الأعمدة (تطابق ترتيب الإضافة)
            SetSubItem(item, 1, txtFullName.Text.Trim());
            SetSubItem(item, 2, dtpBirth.Text.Trim());
            SetSubItem(item, 3, txtCountry.Text.Trim());

            // الجنس وصورة العنصر
            if (rbMale.Checked)
            {
                item.ImageIndex = 0;
                SetSubItem(item, 4, rbMale.Text.Trim());
            }
            else
            {
                item.ImageIndex = 1;
                SetSubItem(item, 4, rbFemale.Text.Trim());
            }

            SetSubItem(item, 5, txtAddress.Text.Trim());
            SetSubItem(item, 6, txtPhone.Text.Trim());
            SetSubItem(item, 7, txtEmail.Text.Trim());
            SetSubItem(item, 8, txtJop.Text.Trim());
            SetSubItem(item, 9, txtDepartment.Text.Trim());
            SetSubItem(item, 10, dtpHireDate.Text.Trim());
            SetSubItem(item, 11, txtSalary.Text.Trim());
            SetSubItem(item, 12, cbStatus.Text.Trim());
            SetSubItem(item, 13, cbLevel.Text.Trim());

            // خيار: إظهار رسالة نجاح
            MessageBox.Show("تم تعديل السجل بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // إن أردت مسح الحقول بعد التعديل علّق السطور التالية
            // txtID.Clear(); txtFullName.Clear(); /* ... باقي الحقول ... */ txtID.Focus();
        }



        // حدث لملء الحقول عند اختيار صف من ListView (مفيد قبل الضغط على تعديل)
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;

            var item = listView1.SelectedItems[0];

            // ضع القيم في الحقول. افحص وجود كل SubItem قبل القراءة.
            txtID.Text = item.SubItems.Count > 0 ? item.SubItems[0].Text : string.Empty;
            txtFullName.Text = item.SubItems.Count > 1 ? item.SubItems[1].Text : string.Empty;

            if (item.SubItems.Count > 2)
            {
                DateTime dt;
                if (DateTime.TryParse(item.SubItems[2].Text, out dt))
                    dtpBirth.Value = dt;
                else
                    dtpBirth.Value = DateTime.Today;
            }

            txtCountry.Text = item.SubItems.Count > 3 ? item.SubItems[3].Text : string.Empty;

            if (item.SubItems.Count > 4)
            {
                var gender = item.SubItems[4].Text;
                rbMale.Checked = (gender == rbMale.Text);
                rbFemale.Checked = (gender == rbFemale.Text);
            }

            txtAddress.Text = item.SubItems.Count > 5 ? item.SubItems[5].Text : string.Empty;
            txtPhone.Text = item.SubItems.Count > 6 ? item.SubItems[6].Text : string.Empty;
            txtEmail.Text = item.SubItems.Count > 7 ? item.SubItems[7].Text : string.Empty;
            txtJop.Text = item.SubItems.Count > 8 ? item.SubItems[8].Text : string.Empty;
            txtDepartment.Text = item.SubItems.Count > 9 ? item.SubItems[9].Text : string.Empty;

            if (item.SubItems.Count > 10)
            {
                DateTime hd;
                if (DateTime.TryParse(item.SubItems[10].Text, out hd))
                    dtpHireDate.Value = hd;
                else
                    dtpHireDate.Value = DateTime.Today;
            }

            txtSalary.Text = item.SubItems.Count > 11 ? item.SubItems[11].Text : string.Empty;
            cbStatus.Text = item.SubItems.Count > 12 ? item.SubItems[12].Text : string.Empty;
            cbLevel.Text = item.SubItems.Count > 13 ? item.SubItems[13].Text : string.Empty;
        }
    }
}




