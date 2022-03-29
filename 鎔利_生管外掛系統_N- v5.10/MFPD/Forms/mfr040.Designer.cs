namespace MFPD.Forms
{
    partial class mfr040
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.f_eca = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.f_enddate = new System.Windows.Forms.DateTimePicker();
            this.button5 = new System.Windows.Forms.Button();
            this.f_begdate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lb_msg = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.cb_employee = new System.Windows.Forms.CheckBox();
            this.cb_ngitem = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1166, 132);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(469, 103);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "確認";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_ngitem);
            this.groupBox1.Controls.Add(this.cb_employee);
            this.groupBox1.Controls.Add(this.f_eca);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.f_enddate);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.f_begdate);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(10, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(453, 121);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查詢條件";
            // 
            // f_eca
            // 
            this.f_eca.FormattingEnabled = true;
            this.f_eca.Location = new System.Drawing.Point(67, 26);
            this.f_eca.Name = "f_eca";
            this.f_eca.Size = new System.Drawing.Size(93, 20);
            this.f_eca.TabIndex = 136;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 134;
            this.label1.Text = "部門";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(381, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 12);
            this.label7.TabIndex = 133;
            this.label7.Text = "(迄)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(205, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 12);
            this.label5.TabIndex = 132;
            this.label5.Text = "(起) ～";
            // 
            // button4
            // 
            this.button4.Image = global::MFPD.Properties.Resources.date_reflash;
            this.button4.Location = new System.Drawing.Point(357, 76);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(18, 18);
            this.button4.TabIndex = 131;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // f_enddate
            // 
            this.f_enddate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.f_enddate.Location = new System.Drawing.Point(263, 74);
            this.f_enddate.Margin = new System.Windows.Forms.Padding(2);
            this.f_enddate.Name = "f_enddate";
            this.f_enddate.Size = new System.Drawing.Size(93, 22);
            this.f_enddate.TabIndex = 3;
            this.f_enddate.ValueChanged += new System.EventHandler(this.f_enddate_ValueChanged);
            // 
            // button5
            // 
            this.button5.Image = global::MFPD.Properties.Resources.date_reflash;
            this.button5.Location = new System.Drawing.Point(159, 76);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(18, 18);
            this.button5.TabIndex = 129;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // f_begdate
            // 
            this.f_begdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.f_begdate.Location = new System.Drawing.Point(67, 74);
            this.f_begdate.Margin = new System.Windows.Forms.Padding(2);
            this.f_begdate.Name = "f_begdate";
            this.f_begdate.Size = new System.Drawing.Size(93, 22);
            this.f_begdate.TabIndex = 2;
            this.f_begdate.ValueChanged += new System.EventHandler(this.f_begdate_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 128;
            this.label4.Text = "日期";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lb_msg);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 646);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1166, 20);
            this.panel2.TabIndex = 1;
            // 
            // lb_msg
            // 
            this.lb_msg.AutoSize = true;
            this.lb_msg.Location = new System.Drawing.Point(4, 5);
            this.lb_msg.Name = "lb_msg";
            this.lb_msg.Size = new System.Drawing.Size(29, 12);
            this.lb_msg.TabIndex = 0;
            this.lb_msg.Text = "訊息";
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(1086, 132);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(80, 514);
            this.panel3.TabIndex = 2;
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 132);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(1086, 514);
            this.crystalReportViewer1.TabIndex = 3;
            // 
            // cb_employee
            // 
            this.cb_employee.AutoSize = true;
            this.cb_employee.Checked = true;
            this.cb_employee.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_employee.Location = new System.Drawing.Point(263, 26);
            this.cb_employee.Name = "cb_employee";
            this.cb_employee.Size = new System.Drawing.Size(96, 16);
            this.cb_employee.TabIndex = 137;
            this.cb_employee.Text = "列印生產人員";
            this.cb_employee.UseVisualStyleBackColor = true;
            // 
            // cb_ngitem
            // 
            this.cb_ngitem.AutoSize = true;
            this.cb_ngitem.Checked = true;
            this.cb_ngitem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_ngitem.Location = new System.Drawing.Point(263, 53);
            this.cb_ngitem.Name = "cb_ngitem";
            this.cb_ngitem.Size = new System.Drawing.Size(96, 16);
            this.cb_ngitem.TabIndex = 138;
            this.cb_ngitem.Text = "列印不良項目";
            this.cb_ngitem.UseVisualStyleBackColor = true;
            // 
            // mfr040
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1166, 666);
            this.Controls.Add(this.crystalReportViewer1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "mfr040";
            this.Text = "mfr040-生產日報表";
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lb_msg;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DateTimePicker f_enddate;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DateTimePicker f_begdate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox f_eca;
        private System.Windows.Forms.CheckBox cb_employee;
        private System.Windows.Forms.CheckBox cb_ngitem;
    }
}