using LoginForm.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
// using System.Windows.Forms.VisualStyles; ← يمكن حذفه
//Windows حسب ثيم Controls موجود لتخصيص مظهر VisualStyleElement  
//ليس له علاقة مباشرة بإدخال النص أو كلمات السر.
// إذا لا تحتاجه using ويمكنك تجاهله أو إزالة الـ Visual Studio وThemes ظهر في مشروعك تلقائيًا بسبب استخدام 



namespace LoginForm
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        public short failedAttempts = 3;
        public int lockSeconds = 0;

        public void LockSystem()
        {
            lockSeconds = 10;
            txtUserName.Enabled = false;
            txtPassword.Enabled = false;
            lblAccepted.Text = $"Locked: {lockSeconds} Secound";
            timer2.Interval = 1000;
            /*if (!timer1.Enabled)*/
            timer2.Start();
                                              //    يبدأ العد من الصفر  Start() إذا كان المؤقت متوقّفًا
                                              //  لايعيد العد بل لايفعل اي شيء Start() إذا كان المؤقت يعمل أصلا
        }

        public void ResetForm()
        {
            if (timer2.Enabled)
                return;

            txtUserName.Visible = true;
            txtPassword.Visible = true;
            txtUserName.Clear();
            txtPassword.Clear();
            lblUserName.Visible = true;
            lblPassword.Visible = true;
            btnShowPassword.Visible = true;
            btnSubmit.Visible = true;
            btnOk.Visible = false;
            lblAccepted.Visible = false;
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;

            txtUserName.Focus();
        }

        public bool Verification()
        {
            if (txtUserName.Text == txtUserName.Tag.ToString() && txtPassword.Text == txtPassword.Tag?.ToString())
                return true; 
            else
                return false;
        }

        public void Login() 
        {
            if (txtUserName.Text == "" || txtPassword.Text == "")
                return;

            if (Verification())
            {
                lblAccepted.Visible = true;
                lblAccepted.ForeColor = Color.Green;
                lblAccepted.Text = "Accepted";
                failedAttempts = 3;
            }
            else
            {
                failedAttempts--;
                txtUserName.Visible = txtPassword.Visible = lblUserName.Visible = 
                lblPassword.Visible = btnSubmit.Visible = btnShowPassword.Visible =
                pictureBox1.Visible = pictureBox2.Visible = false;

                btnOk.Visible = lblAccepted.Visible = true;
                lblAccepted.ForeColor = Color.Maroon;
                lblAccepted.Text = "The Password is incorect. Try Again \n Remaind " + failedAttempts + " Time";

            }

            if(failedAttempts == 0)
            {
                LockSystem();
            }

        }

        /*************************************************************************************/
        //string correctUser = "admin";          instend of using tag
        //string correctPass = "1234";

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUserName.Tag = "admin";
            txtPassword.Tag = "12340000";

            //foreach (Control c in this.Controls)            //AI
            //{
            //    if (c is TextBox)
            //        c.KeyDown += TextBox_KeyDown;
            //}

            foreach (var txt in GetAllTextBoxes(this))         //AI
                txt.KeyDown += TextBox_KeyDown;

            lblDateTime.Text = DateTime.Now.ToString("yyyy/MM/dd   hh:mm:ss tt");   //("yyyy/MM/dd HH:mm:ss");  
            timer1.Start(); // يعمل Timer للتأكد أن  


            txtPassword.UseSystemPasswordChar = true;
            //txtPassword.PasswordChar = '*';               هي بدل يلي فوقها
        }
      
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                e.Cancel = true;
                txtUserName.Focus();
                errorProvider1.SetError(txtUserName, "UserName should have a value!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUserName, "");
            }

        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))              
            {
                e.Cancel = true;
                txtPassword.Focus();
                errorProvider1.SetError(txtPassword, "Password should have a value!");
            }
            else
            {
                e.Cancel = false;                          //   بس اكيد في شي تاني بتعمله false كأنه لتحت بتكفي وعم تصير هي لحلها  II
                errorProvider1.SetError(txtPassword, ""); //    يلي كان طالع error مشان اذا اول شي دخل خطأ بعدبن دخل صح يروح ال  II
            }

        }

        //private void TextBox_KeyDown(object sender, KeyEventArgs e)                   //I+AI
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;

        //        Control current = sender as Control;
        //        Control next = this.GetNextControl(current, true);

        //        if (/*next != null*/next.TabIndex != 2)
        //        {
        //            // ينتقل للحقل التالي
        //            next.Focus();
        //        }
        //        else
        //        {
        //            // هذا آخر TextBox → نفذ عملية Submit
        //            Login();
        //        }
        //    }
        //}
                                                                           //AI

        private List<TextBox> GetAllTextBoxes(Control parent)            //  TextBoxes in form هي دالة تساعدك على جمع جميع          
        {                                                                //  Container  أو اي  Panel أو GroupBox  حتى لو كانوا داخل 
            List<TextBox> list = new List<TextBox>();

            foreach (Control c in parent.Controls)
            {
                if (c is TextBox)
                    list.Add((TextBox)c);

                if (c.HasChildren)
                    list.AddRange(GetAllTextBoxes(c));
            }

            return list.OrderBy(t => t.TabIndex).ToList();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)             //AI
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                TextBox current = sender as TextBox;

                List<TextBox> boxes = GetAllTextBoxes(this);
                int index = boxes.IndexOf(current);

                if (index < boxes.Count - 1)
                {
                    // ليس آخر TextBox → الانتقال للي بعده
                    boxes[index + 1].Focus();
                }
                else
                {
                    // هذا آخر TextBox
                    Login();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("yyyy/MM/dd   hh:mm:ss tt");

            dateTimePicker1.Value = DateTime.Now;
        }

        private void btnShowPassword_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
            btnShowPassword.BackgroundImage = Resources.eye;
            //txtPassword.PasswordChar = '\0';           او هي الطريقة

                                                         //UseSystemPasswordChar  بعض المطورين يفضلون 
                                                         //(يظهر نقاط بدلاً من نجوم Windows 10/11) لأنه يتكيف مع شكل النظام  
        }

        private void btnShowPassword_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            btnShowPassword.BackgroundImage = Resources.hidden;
            //txtPassword.PasswordChar = '*';           او هي الطريقة

        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = colorDialog1.Color;

            }
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = true;
            fontDialog1.ShowApply = true;
            fontDialog1.ShowEffects = true;

            fontDialog1.Font = this.Font;

            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                lblUserName.Font = fontDialog1.Font;
                lblPassword.Font = fontDialog1.Font;
                lblLogin.Font = fontDialog1.Font;
                lblAccepted.Font = fontDialog1.Font;
                btnOk.Font = fontDialog1.Font;
                lblDateTime.Font = fontDialog1.Font;
                dateTimePicker1.Font = fontDialog1.Font;//.CalendarFont
                btnSubmit.Font = fontDialog1.Font;
                lblUserName.ForeColor = fontDialog1.Color;
                lblPassword.ForeColor = fontDialog1.Color;
                lblLogin.ForeColor = fontDialog1.Color;
                lblAccepted.ForeColor = fontDialog1.Color;
                btnOk.ForeColor = fontDialog1.Color;
                lblDateTime.ForeColor = fontDialog1.Color;
                btnSubmit.ForeColor = fontDialog1.Color;

            }
        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {
            lblUserName.Font = fontDialog1.Font;
            lblPassword.Font = fontDialog1.Font;
            lblLogin.Font = fontDialog1.Font;
            lblAccepted.Font = fontDialog1.Font;
            btnOk.Font = fontDialog1.Font;
            lblDateTime.Font = fontDialog1.Font;
            dateTimePicker1.Font = fontDialog1.Font;//.CalendarFont
            btnSubmit.Font = fontDialog1.Font;
            lblUserName.ForeColor = fontDialog1.Color;
            lblPassword.ForeColor = fontDialog1.Color;
            lblLogin.ForeColor = fontDialog1.Color;
            lblAccepted.ForeColor = fontDialog1.Color;
            btnOk.ForeColor = fontDialog1.Color;
            lblDateTime.ForeColor = fontDialog1.Color;
            btnSubmit.ForeColor = fontDialog1.Color;

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lockSeconds--;

            if (lockSeconds > 0)
            {
                lblAccepted.Text = $"Locked: {lockSeconds} Secound";
            }
            else
            {
                timer2.Stop();
                txtUserName.Enabled = true;
                txtPassword.Enabled = true;
                failedAttempts = 3;
                lblAccepted.Text = "Can you Try again now.";
            }

        }
    }
}

