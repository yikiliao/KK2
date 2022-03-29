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
    public partial class rDayWKno : Form
    {
        public string dept;
        public DataTable dtProd = new DataTable(); //產品資料
        public string rType = string.Empty;//決定抓工單的table資料 
        public string rSfb01 = string.Empty;//儲存選取工單資料

        
        public string FlowStation = string.Empty;


        public rDayWKno()
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
            this.dataGridView1.ColumnCount = 7;
            this.dataGridView1.Columns[0].Name = "ck";  //
            this.dataGridView1.Columns[1].Name = "sfb22";  //訂單            
            this.dataGridView1.Columns[2].Name = "sfb221";  //訂單 項次
            this.dataGridView1.Columns[3].Name = "sfb01";  //工單
            this.dataGridView1.Columns[4].Name = "occ02";  //客戶            
            this.dataGridView1.Columns[5].Name = "sfb224"; //客戶訂單號
            this.dataGridView1.Columns[6].Name = "sfb08"; //派工數

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
            this.dataGridView1.Columns["sfb224"].HeaderText = "客戶訂單";
            this.dataGridView1.Columns["sfb08"].HeaderText = "派工數";

            ////欄位靠右
            this.dataGridView1.Columns["sfb221"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns["sfb08"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //--定義datatable 欄位
            dtProd.Columns.Add("sfb01", typeof(String));
            
            //標題字體
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("新細明體", 12);
        }

        
        private void rWKno_Load(object sender, EventArgs e)
        {
            getData();//抓資料 (同步過來的TT工單)
            label1.Text = FlowStation;
        }

        private void getData()
        {
            //判斷dt 的 流程跟 flowstation相同才顯示,表示可以修改
            dataGridView1.Rows.Clear();            
            DataTable dt = sql_sfb_file.FindList(Login.DeptOne, string.Empty, "2");//入日排

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sfb01 = dt.Rows[i]["sfb01"].ToString();
                if (sfb01 == rSfb01) continue;//工單相同不顯示
                if (FlowStation == CheckStation(sfb01)) //流程相同才修改
                {
                    dataGridView1.Rows.Add(new object[] {
                    true,
                    dt.Rows[i]["sfb22"],
                    dt.Rows[i]["sfb221"],
                    dt.Rows[i]["sfb01"],
                    dt.Rows[i]["occ02"],
                    dt.Rows[i]["sfb224"],
                    Convert.ToInt16(dt.Rows[i]["sfb08"])});
                }
            }
        }


        private string CheckStation(string sfb01)
        {
            string fl = string.Empty;
            string sql = string.Empty;
            sql += "select * from procschday where 1=1";
            sql += " and ecm01='" + sfb01 + "'";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                fl += FlowS(System.Convert.ToInt16(dt.Rows[i]["ecm03"].ToString()),
                    dt.Rows[i]["ecm04"].ToString(),
                    dt.Rows[i]["ecm06"].ToString());//紀錄修改的流程站，比對批次修改時流程站是否相同
            }
            return fl;
        }

        private string FlowS(Int16 a, string b, string c)
        {
            return a.ToString() + "'" + b + "'" + c + "|";
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex < 0) || (e.ColumnIndex < 0)) return;
            dataGridView1.Rows[e.RowIndex].Selected = true;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            OK_A();            
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
                    dr["sfb01"] = dataGridView1.Rows[i].Cells[3].Value.ToString();                    
                    dtProd.Rows.Add(dr);
                }
            }
            //if (dtProd.Rows.Count == 0)
            //{
            //    MessageBox.Show("尚未選取工單，請點選");
            //    return;
            //}            
            DialogResult = System.Windows.Forms.DialogResult.OK;//這一定要設
            Close();
        }

    }
}
