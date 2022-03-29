namespace MFPD.Forms
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.label1 = new System.Windows.Forms.Label();
            this.f_id = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.f_password = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.menu_exit = new System.Windows.Forms.ToolStripButton();
            this.label3 = new System.Windows.Forms.Label();
            this.f_db = new System.Windows.Forms.ComboBox();
            this.f_plant = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(22, 51);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "帳號";
            // 
            // f_id
            // 
            this.f_id.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.f_id.Location = new System.Drawing.Point(95, 48);
            this.f_id.Margin = new System.Windows.Forms.Padding(2);
            this.f_id.Name = "f_id";
            this.f_id.Size = new System.Drawing.Size(153, 30);
            this.f_id.TabIndex = 0;
            this.f_id.Validating += new System.ComponentModel.CancelEventHandler(this.f_id_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(22, 96);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "密碼";
            // 
            // f_password
            // 
            this.f_password.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.f_password.Location = new System.Drawing.Point(95, 90);
            this.f_password.Margin = new System.Windows.Forms.Padding(2);
            this.f_password.Name = "f_password";
            this.f_password.Size = new System.Drawing.Size(153, 30);
            this.f_password.TabIndex = 1;
            this.f_password.UseSystemPasswordChar = true;
            this.f_password.Validating += new System.ComponentModel.CancelEventHandler(this.f_password_Validating);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Location = new System.Drawing.Point(95, 210);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 41);
            this.button1.TabIndex = 4;
            this.button1.Text = "確定(Esc)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.MouseHover += new System.EventHandler(this.button1_MouseHover);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_exit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(325, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // menu_exit
            // 
            this.menu_exit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menu_exit.Image = ((System.Drawing.Image)(resources.GetObject("menu_exit.Image")));
            this.menu_exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menu_exit.Name = "menu_exit";
            this.menu_exit.Size = new System.Drawing.Size(23, 22);
            this.menu_exit.Text = "toolStripButton2";
            this.menu_exit.ToolTipText = "離開(Ctrl+Del)";
            this.menu_exit.Click += new System.EventHandler(this.menu_exit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(22, 140);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "廠部";
            // 
            // f_db
            // 
            this.f_db.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.f_db.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.f_db.FormattingEnabled = true;
            this.f_db.Location = new System.Drawing.Point(95, 180);
            this.f_db.Name = "f_db";
            this.f_db.Size = new System.Drawing.Size(153, 27);
            this.f_db.TabIndex = 3;
            this.f_db.Visible = false;
            // 
            // f_plant
            // 
            this.f_plant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.f_plant.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.f_plant.FormattingEnabled = true;
            this.f_plant.Location = new System.Drawing.Point(95, 136);
            this.f_plant.Name = "f_plant";
            this.f_plant.Size = new System.Drawing.Size(153, 27);
            this.f_plant.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(22, 184);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 19);
            this.label4.TabIndex = 24;
            this.label4.Text = "資料庫";
            this.label4.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(312, 283);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(13, 17);
            this.button2.TabIndex = 25;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 300);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.f_plant);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.f_db);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.f_password);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.f_id);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Login";
            this.Text = "Login登入畫面";
            this.Load += new System.EventHandler(this.Login_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Login_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox f_id;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox f_password;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton menu_exit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox f_db;
        private System.Windows.Forms.ComboBox f_plant;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
    }
}