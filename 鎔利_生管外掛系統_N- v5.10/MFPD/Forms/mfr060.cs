//-------------------------------------------------
//周生產排程報
//程式編號 PrgId:mfr060
//說明 Descript:列印出已排程(狀態3)各工作站的生產數，資料抓到datatable 再印出來
//Author:Yiki Liao
//Date:2022/02
//-------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Collections;
using MFPD.Models;
using MFPD.Crpts;

namespace MFPD.Forms
{
    public partial class mfr060 : Form
    {        
        List<bool> LCdept = new List<bool>();//存 部門勾選
        List<bool> LPrno = new List<bool>();//存 工號勾選
        string Dept;
        DataTable dtProc = new DataTable();

        
        public mfr060()
        {
            InitializeComponent();            
            Config.SetFormSize(this, "R");
            Config.SetPer(this);
            init_Form();
            Sett_DataTable();
        }

        private void init_Form()
        {
            Dept = Login.DeptOne;                   
            
            Config.Set_DateTo(f_begdate, " ");//清空預設日期
            Config.Set_DateTo(f_enddate, " ");//清空預設日期
                       
            f_begdate.Value = DateTime.Now;
            f_enddate.Value = DateTime.Now;
            D_mfwork(Dept);//部門            
        }

        private void Sett_DataTable()
        {
            dtProc.Columns.Add("Sfb01", typeof(String));//工單號
            dtProc.Columns.Add("Sfb22", typeof(String));//訂單
            dtProc.Columns.Add("Sfb221", typeof(String));//訂單項次
            dtProc.Columns.Add("Ima01", typeof(String));//料號
            dtProc.Columns.Add("Ima02", typeof(String));
            dtProc.Columns.Add("Ima021", typeof(String));
            dtProc.Columns.Add("Occ02", typeof(String));//客戶
            dtProc.Columns.Add("Sfb08", typeof(decimal));//派工數
            dtProc.Columns.Add("Weekname", typeof(Int32));//星期幾
            dtProc.Columns.Add("Schdate", typeof(String));//開工日
            dtProc.Columns.Add("Ecm04", typeof(String));//製成代碼
            dtProc.Columns.Add("Ecm45", typeof(String));//說明
            dtProc.Columns.Add("Ecm06", typeof(String));//工作站代碼
            dtProc.Columns.Add("Eca02", typeof(String));//工作站說明
            dtProc.Columns.Add("Sfb224", typeof(String));//客戶訂單號
            dtProc.Columns.Add("Ecb03", typeof(Int32));//工序
        }

        private void D_mfwork(string Dept)
        {
            DataTable dt = eca_file.ToDoList(Dept);
            //加入一列空白可以選全部
            DataRow dr = dt.NewRow();
            dr["eca01"] = "";
            dr["eca02"] = "--全選--";
            dt.Rows.Add(dr);

            f_eca.DataSource = dt;
            f_eca.ValueMember = "eca01";
            f_eca.DisplayMember = "eca02";
            f_eca.SelectedIndex = 0;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string rPlant = Login.DeptOne;
            string rCdept = f_eca.SelectedValue.ToString(); //生產部門
            string rBegdate = f_begdate.Text;//生產日(起)
            string rEnddate = f_enddate.Text;//生產日(迄)

            var Org_Color = lb_msg.ForeColor;
            Cursor_wait(); //改變滑鼠游標漏斗指標            
            
            DataTable dt = Mfr060.ToDoList(rPlant, rCdept, rBegdate, rEnddate, dtProc);

            var count = dt.Rows.Count;
            if (count == 0)
            {
                Config.Show("...無符合資料...");
                crystalReportViewer1.ReportSource = null;
                crystalReportViewer1.Refresh();
            }
            else
            {
                string ReportCond = f_Cond(rPlant, rBegdate, rEnddate, rCdept);//列印條件
                Print_cs(dt, ReportCond);
            }
            UnCursor_wait(Org_Color); //改變滑鼠游標還原預設
        }


        private void Cursor_wait()
        {
            lb_msg.ForeColor = Color.Blue;
            lb_msg.Text = "資料處理中...請稍候";
            System.Threading.Thread.Sleep(1000);//停1秒
            this.Cursor = Cursors.WaitCursor;//漏斗指標
        }

        private void UnCursor_wait(System.Drawing.Color Org_Color)
        {
            lb_msg.ForeColor = Org_Color;
            this.Cursor = Cursors.Default;//還原預設
            lb_msg.Text = "";
        }

        private void Print_cs(DataTable dt, string ReportCond)
        {
            var rpname = "週生產排程表";
            CrystalReport_mfr060 rp = new CrystalReport_mfr060();
            rp.SetDataSource(dt);

            rp.SetParameterValue("CompName", Login.COMPNAME);//公司名稱            
            rp.SetParameterValue("ReportName", rpname);//報表名稱
            rp.SetParameterValue("ReportCond", ReportCond);//列印條件
            rp.SetParameterValue("ReportId", "mfr060");//程式編號            
            rp.SetParameterValue("ReportAuthor", Login.IDNAME);//製表人            

            crystalReportViewer1.ReportSource = rp;
            crystalReportViewer1.Refresh();
        }


        

        private string f_Cond(string rPlant, string rBegdate, string rEnddate, string rCdept)
        {
            string cond = "";
            cond += string.Format(" | 公司={0}", rPlant);
            if (!string.IsNullOrWhiteSpace(rCdept))
                cond += string.Format(" | 部門={0}", rCdept);
            else
                cond += string.Format(" | 部門={0}", "全選");

            if (!string.IsNullOrWhiteSpace(rBegdate))
            {
                cond += string.Format(" | 日期(起)>={0}", rBegdate);
            }
            if (!string.IsNullOrWhiteSpace(rEnddate))
            {
                cond += string.Format(" | 日期(迄)<={0}", rEnddate);
            }            
            if (cond.Length == 0)
            {
                cond = "印全部資料";
            }
            return cond;
        }

        
        private void init_prno()
        {
            
            LPrno.Clear();
        }

        private void init_cdept()
        {
            
            LCdept.Clear();
        }

        private void f_comDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            init_cdept();
            init_prno();
        }

        private void f_begdate_ValueChanged(object sender, EventArgs e)
        {
            Config.Set_DateTo(f_begdate, f_begdate.Value.ToString("yyyy/MM/dd"));
            f_enddate.Text = f_begdate.Text;
        }

        private void f_enddate_ValueChanged(object sender, EventArgs e)
        {
            Config.Set_DateTo(f_enddate, f_enddate.Value.ToString("yyyy/MM/dd"));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Config.Set_DateTo(f_begdate, " ");//清空預設日期
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Config.Set_DateTo(f_enddate, " ");//清空預設日期
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                System.Windows.Forms.SendKeys.Send("{tab}");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
