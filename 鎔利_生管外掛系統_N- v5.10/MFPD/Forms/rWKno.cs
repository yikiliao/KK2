using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MFPD.Models;

namespace MFPD.Forms
{
    public partial class rWKno : Form
    {
        public string dept;
        public DataTable dtProd = new DataTable(); //產品資料
        public string rType = string.Empty;//決定抓工單的table資料 
        public string rSfb01 = string.Empty;//儲存選取工單資料
        
        public rWKno()
        {
            InitializeComponent();
            initForm();
        }

        private void initForm()
        {
            //sfb01,occ02,ima01,ima02,ima021,sfb06,sfb08,sfb13
            //開啟畫面時置中
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            //產品資料畫面
            this.dataGridView1.ColumnCount = 15;
            this.dataGridView1.Columns[0].Name = "ck";  //
            this.dataGridView1.Columns[1].Name = "sfb22";  //訂單            
            this.dataGridView1.Columns[2].Name = "sfb221";  //訂單 項次
            this.dataGridView1.Columns[3].Name = "sfb01";  //工單
            this.dataGridView1.Columns[4].Name = "occ02";  //客戶
            this.dataGridView1.Columns[5].Name = "ima01";  //料件編號          
            this.dataGridView1.Columns[6].Name = "ima02";  //品名
            this.dataGridView1.Columns[7].Name = "ima021"; //規格
            this.dataGridView1.Columns[8].Name = "sfb06"; //製成編號
            this.dataGridView1.Columns[9].Name = "sfb08"; //生產數
            this.dataGridView1.Columns[10].Name = "sfb13"; //預計生產日
            this.dataGridView1.Columns[11].Name = "sfb15"; //客戶交期

            this.dataGridView1.Columns[12].Name = "sfb04"; //工單狀態
            this.dataGridView1.Columns[13].Name = "sfb223"; //客戶編號
            this.dataGridView1.Columns[14].Name = "sfb224"; //客戶訂單號

            //是否允許使用者編輯
            //this.dataGridView1.ReadOnly = true;
            //是否允許使用者自行新增
            this.dataGridView1.AllowUserToAddRows = false;
            //是否顯示首列資料
            //this.dataGridView1.ColumnHeadersVisible = false;

            //設攔寬
            this.dataGridView1.Columns["sfb22"].HeaderText = "訂單";
            this.dataGridView1.Columns["sfb221"].HeaderText = "項次";
            this.dataGridView1.Columns["sfb01"].HeaderText = "工單";
            this.dataGridView1.Columns["occ02"].HeaderText = "客戶";
            this.dataGridView1.Columns["ima01"].HeaderText = "料件編號";
            this.dataGridView1.Columns["ima02"].HeaderText = "品名";
            this.dataGridView1.Columns["ima021"].HeaderText = "規格";
            this.dataGridView1.Columns["sfb06"].HeaderText = "製程編號";
            this.dataGridView1.Columns["sfb08"].HeaderText = "生產數";
            this.dataGridView1.Columns["sfb13"].HeaderText = "預計生產日";
            this.dataGridView1.Columns["sfb15"].HeaderText = "客戶交期";

            this.dataGridView1.Columns["sfb04"].HeaderText = "工單狀態";
            this.dataGridView1.Columns["sfb223"].HeaderText = "客戶編號";
            this.dataGridView1.Columns["sfb224"].HeaderText = "客戶訂單號(PO)";

            //隱藏
            this.dataGridView1.Columns["sfb04"].Visible = false;
            this.dataGridView1.Columns["sfb223"].Visible = false;
            this.dataGridView1.Columns["sfb224"].Visible = false;

            ////欄位靠右
            //this.dataGridView1.Columns["sfb01"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft;
            //this.dataGridView1.Columns["sfb05"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft;

            //--定義datatable 欄位
            dtProd.Columns.Add("sfb22", typeof(String));
            dtProd.Columns.Add("sfb221", typeof(String));
            dtProd.Columns.Add("sfb01", typeof(String));
            dtProd.Columns.Add("occ02", typeof(String));
            dtProd.Columns.Add("ima01", typeof(String));
            dtProd.Columns.Add("ima02", typeof(String));
            dtProd.Columns.Add("ima021", typeof(String));
            dtProd.Columns.Add("sfb06", typeof(String));
            dtProd.Columns.Add("sfb08", typeof(String));
            dtProd.Columns.Add("sfb13", typeof(String));
            dtProd.Columns.Add("sfb15", typeof(String));

            dtProd.Columns.Add("sfb04", typeof(String));
            dtProd.Columns.Add("sfb223", typeof(String));
            dtProd.Columns.Add("sfb224", typeof(String));

            //標題字體
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("新細明體", 12);

            label1.Text = "依客戶交期+客戶+訂單 排序";
        }

        //private void rWKno_Load(object sender, EventArgs e)
        //{
        //    if (rType=="A") getData();//抓資料 (同步過來的TT工單))
        //    if (rType == "B") getData2();//抓資料 (排程的工單資料)
        //}
        private void rWKno_Load(object sender, EventArgs e)
        {
            getData();//抓資料 (同步過來的TT工單)
        }

        //已經有排程不顯示

        private void getData()
        {
            int rcnt = 0;
            dataGridView1.Rows.Clear();
            DataTable dt = sfb_file.ToDoList(dept); //前一面傳過來的作業名稱代碼
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sfb01 = "'" + dt.Rows[i]["sfb01"].ToString() + "'";
                if (sql_sfb_file.ToDoList(sfb01).Rows.Count > 0)
                    continue;
                else
                {
                    dataGridView1.Rows.Add(new object[] {false,
                        dt.Rows[i]["sfb22"],
                        dt.Rows[i]["sfb221"],
                        dt.Rows[i]["sfb01"],
                    dt.Rows[i]["occ02"],
                    dt.Rows[i]["ima01"],
                    dt.Rows[i]["ima02"],
                    dt.Rows[i]["ima021"],
                    dt.Rows[i]["sfb06"],
                    dt.Rows[i]["sfb08"],
                    dt.Rows[i]["sfb13"],
                    dt.Rows[i]["sfb15"],
                    dt.Rows[i]["sfb04"],
                    dt.Rows[i]["sfb223"],
                    dt.Rows[i]["sfb224"]});
                    rcnt += 1;
                }
            }
            if (rcnt > 0)
            {
                dataGridView1.Rows[0].Selected = true;
                label2.Text = "筆數：" + rcnt.ToString("##0");
            }
        }

        //private void getData()
        //{
        //    int rcnt = 0;
        //    dataGridView1.Rows.Clear();
        //    DataTable dt = sfb_file.ToDoList(dept); //前一面傳過來的作業名稱代碼
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        string sfb01 = "'" + dt.Rows[i]["sfb01"].ToString() + "'";
        //        if (Procsca.ToDoList(sfb01).Rows.Count > 0)
        //            continue;
        //        else
        //        {
        //            dataGridView1.Rows.Add(new object[] {false,
        //                dt.Rows[i]["sfb22"],
        //                dt.Rows[i]["sfb221"],
        //                dt.Rows[i]["sfb01"],
        //            dt.Rows[i]["occ02"],
        //            dt.Rows[i]["ima01"],
        //            dt.Rows[i]["ima02"],
        //            dt.Rows[i]["ima021"],
        //            dt.Rows[i]["sfb06"],
        //            dt.Rows[i]["sfb08"],
        //            dt.Rows[i]["sfb13"],
        //            dt.Rows[i]["sfb15"],
        //            dt.Rows[i]["sfb04"],
        //            dt.Rows[i]["sfb223"],
        //            dt.Rows[i]["sfb224"]});
        //            rcnt += 1;
        //        }
        //    }
        //    if (rcnt > 0)
        //    {
        //        dataGridView1.Rows[0].Selected = true;
        //    }            
        //}



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex < 0) || (e.ColumnIndex < 0)) return;
            dataGridView1.Rows[e.RowIndex].Selected = true;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            OK_A();
            //if (rType == "A")
            //{
            //    OK_A();
            //}
            //if (rType == "B")
            //{
            //    OK_B();
            //}            
        }

        private void OK_A()
        {
            DataRow dr;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                bool isSelected = Convert.ToBoolean(dataGridView1.Rows[i].Cells["ck"].Value);
                if (isSelected)
                {
                    dr = dtProd.NewRow();
                    dr["sfb22"] = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    dr["sfb221"] = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    dr["sfb01"] = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    dr["occ02"] = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    dr["ima01"] = dataGridView1.Rows[i].Cells[5].Value.ToString();
                    dr["ima02"] = dataGridView1.Rows[i].Cells[6].Value.ToString();
                    dr["ima021"] = dataGridView1.Rows[i].Cells[7].Value.ToString();
                    dr["sfb06"] = dataGridView1.Rows[i].Cells[8].Value.ToString();
                    dr["sfb08"] = dataGridView1.Rows[i].Cells[9].Value.ToString();
                    dr["sfb13"] = dataGridView1.Rows[i].Cells[10].Value.ToString();
                    dr["sfb15"] = dataGridView1.Rows[i].Cells[11].Value.ToString();
                    dr["sfb04"] = dataGridView1.Rows[i].Cells[12].Value.ToString();
                    dr["sfb223"] = dataGridView1.Rows[i].Cells[13].Value.ToString();
                    dr["sfb224"] = dataGridView1.Rows[i].Cells[14].Value.ToString();
                    dtProd.Rows.Add(dr);
                }
            }
            if (dtProd.Rows.Count == 0)
            {
                MessageBox.Show("尚未選取工單，請點選");
                return;
            }            
            DialogResult = System.Windows.Forms.DialogResult.OK;//這一定要設
            Close();
        }


        //private void OK_A()
        //{
        //    DataRow dr;
        //    for (int i = 0; i < dataGridView1.Rows.Count; i++)
        //    {
        //        if (dataGridView1.Rows[i].Selected == true)
        //        {
        //            dr = dtProd.NewRow();
        //            dr["sfb01"] = dataGridView1.Rows[i].Cells[0].Value.ToString();
        //            dr["occ02"] = dataGridView1.Rows[i].Cells[1].Value.ToString();
        //            dr["ima01"] = dataGridView1.Rows[i].Cells[2].Value.ToString();
        //            dr["ima02"] = dataGridView1.Rows[i].Cells[3].Value.ToString();
        //            dr["ima021"] = dataGridView1.Rows[i].Cells[4].Value.ToString();
        //            dr["sfb06"] = dataGridView1.Rows[i].Cells[5].Value.ToString();
        //            dr["sfb08"] = dataGridView1.Rows[i].Cells[6].Value.ToString();
        //            dr["sfb13"] = dataGridView1.Rows[i].Cells[7].Value.ToString();
        //            dr["sfb15"] = dataGridView1.Rows[i].Cells[8].Value.ToString();
        //            dr["sfb04"] = dataGridView1.Rows[i].Cells[9].Value.ToString();
        //            dr["sfb223"] = dataGridView1.Rows[i].Cells[10].Value.ToString();
        //            dr["sfb224"] = dataGridView1.Rows[i].Cells[11].Value.ToString();                    
        //            dtProd.Rows.Add(dr);
        //        }
        //    }
        //    if (dtProd.Rows.Count == 0)
        //    {
        //        MessageBox.Show("尚未選取工單，請點選");
        //        return;
        //    }
        //    //if (dtProd.Rows.Count > 1)
        //    //{
        //    //    MessageBox.Show("不可複選單，請重新點選");
        //    //    return;
        //    //}
        //    DialogResult = System.Windows.Forms.DialogResult.OK;//這一定要設
        //    Close();
        //}

        private void OK_B()
        {  
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected == true)
                {
                    rSfb01 += "'" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "',";                    
                }
            }
            if (rSfb01.Length > 0)
            {
                rSfb01 = rSfb01.Substring(0, rSfb01.Length - 1);//取字串把最後一個逗號去掉
            }            
            DialogResult = System.Windows.Forms.DialogResult.OK;//這一定要設
            Close();
        }

    }
}
