namespace MFPD.Forms
{
    partial class mfd013_1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btn_OK = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.doc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shb02 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shb021 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shb031 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shb032 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shb111 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shb112 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shb115 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eci06 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_OK.Location = new System.Drawing.Point(12, 398);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(92, 40);
            this.btn_OK.TabIndex = 4;
            this.btn_OK.Text = "確認";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("新細明體-ExtB", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.doc,
            this.shb02,
            this.shb021,
            this.shb031,
            this.shb032,
            this.shb111,
            this.shb112,
            this.shb115,
            this.eci06,
            this.status,
            this.Column1});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("新細明體-ExtB", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(8, 8);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(957, 384);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick_1);
            // 
            // doc
            // 
            this.doc.DataPropertyName = "doc";
            this.doc.HeaderText = "單據號";
            this.doc.Name = "doc";
            this.doc.ReadOnly = true;
            this.doc.Width = 71;
            // 
            // shb02
            // 
            this.shb02.DataPropertyName = "shb02";
            this.shb02.HeaderText = "日期";
            this.shb02.Name = "shb02";
            this.shb02.ReadOnly = true;
            this.shb02.Width = 58;
            // 
            // shb021
            // 
            this.shb021.DataPropertyName = "shb021";
            this.shb021.HeaderText = "開始";
            this.shb021.Name = "shb021";
            this.shb021.ReadOnly = true;
            this.shb021.Width = 58;
            // 
            // shb031
            // 
            this.shb031.DataPropertyName = "shb031";
            this.shb031.HeaderText = "結束";
            this.shb031.Name = "shb031";
            this.shb031.ReadOnly = true;
            this.shb031.Width = 58;
            // 
            // shb032
            // 
            this.shb032.DataPropertyName = "shb032";
            this.shb032.HeaderText = "工時(分)";
            this.shb032.Name = "shb032";
            this.shb032.ReadOnly = true;
            this.shb032.Width = 79;
            // 
            // shb111
            // 
            this.shb111.DataPropertyName = "shb111";
            this.shb111.HeaderText = "良品數";
            this.shb111.Name = "shb111";
            this.shb111.ReadOnly = true;
            this.shb111.Width = 71;
            // 
            // shb112
            // 
            this.shb112.DataPropertyName = "shb112";
            this.shb112.HeaderText = "報廢數";
            this.shb112.Name = "shb112";
            this.shb112.ReadOnly = true;
            this.shb112.Width = 71;
            // 
            // shb115
            // 
            this.shb115.DataPropertyName = "shb115";
            this.shb115.HeaderText = "Bonus數";
            this.shb115.Name = "shb115";
            this.shb115.ReadOnly = true;
            this.shb115.Width = 76;
            // 
            // eci06
            // 
            this.eci06.DataPropertyName = "eci06";
            this.eci06.HeaderText = "機台";
            this.eci06.Name = "eci06";
            this.eci06.ReadOnly = true;
            this.eci06.Width = 58;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "狀態";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Width = 58;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Text = "員工";
            this.Column1.UseColumnTextForButtonValue = true;
            this.Column1.Width = 5;
            // 
            // mfd013_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 461);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_OK);
            this.Name = "mfd013_1";
            this.Text = "mfd013_1";
            this.Load += new System.EventHandler(this.rEcd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn doc;
        private System.Windows.Forms.DataGridViewTextBoxColumn shb02;
        private System.Windows.Forms.DataGridViewTextBoxColumn shb021;
        private System.Windows.Forms.DataGridViewTextBoxColumn shb031;
        private System.Windows.Forms.DataGridViewTextBoxColumn shb032;
        private System.Windows.Forms.DataGridViewTextBoxColumn shb111;
        private System.Windows.Forms.DataGridViewTextBoxColumn shb112;
        private System.Windows.Forms.DataGridViewTextBoxColumn shb115;
        private System.Windows.Forms.DataGridViewTextBoxColumn eci06;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
    }
}