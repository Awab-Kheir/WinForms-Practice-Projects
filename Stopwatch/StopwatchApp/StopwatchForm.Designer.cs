namespace WindowsFormsApp1
{
    partial class StopwatchForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StopwatchForm));
            this.lblScreen = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblScreen
            // 
            this.lblScreen.AutoSize = true;
            this.lblScreen.BackColor = System.Drawing.Color.MidnightBlue;
            this.lblScreen.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScreen.ForeColor = System.Drawing.SystemColors.Control;
            this.lblScreen.Location = new System.Drawing.Point(38, 9);
            this.lblScreen.Name = "lblScreen";
            this.lblScreen.Size = new System.Drawing.Size(94, 28);
            this.lblScreen.TabIndex = 2;
            this.lblScreen.Text = "00:00:00";
            this.lblScreen.Click += new System.EventHandler(this.lblScreen_Click);
            this.lblScreen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblScreen_MouseDown);
            this.lblScreen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblScreen_MouseMove);
            this.lblScreen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblScreen_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // StopwatchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(148, 32);
            this.Controls.Add(this.lblScreen);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "StopwatchForm";
            this.Text = "MyTimer";
            this.Load += new System.EventHandler(this.StopwatchForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblScreen;
        private System.Windows.Forms.Timer timer1;
    }
}


