//-------------------------------------------------
//加工工單未者排程清冊
//程式編號 PrgId:mfr009
//說明 Descript:列印出日排程的資料 區分  業務跟現場用
//Author:Yiki Liao
//Date:2021/11
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
    public partial class mfr040 : Form
    {        
        List<bool> LCdept = new List<bool>();//存 部門勾選
        List<bool> LPrno = new List<bool>();//存 工號勾選
        string Dept;
        DataTable dtProc = new DataTable();

        
        public mfr040()
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
            cb_employee.Checked = true;
            cb_ngitem.Checked = true;
        }

        private void Sett_DataTable()
        {
            dtProc.Columns.Add("Doc", typeof(String));//單據號
            dtProc.Columns.Add("Shb02", typeof(String));//機台開始日期
            dtProc.Columns.Add("Shb021", typeof(String));//機台開始時間
            dtProc.Columns.Add("Shb03", typeof(String));//機台結束日期
            dtProc.Columns.Add("Shb031", typeof(String));// 機台結束時間
            dtProc.Columns.Add("Shb032", typeof(decimal));
            dtProc.Columns.Add("Shb033", typeof(decimal));
            dtProc.Columns.Add("Shb04", typeof(String));// 工作站代碼
            dtProc.Columns.Add("Shb05", typeof(String));//工號
            dtProc.Columns.Add("Shb06", typeof(Int32));//製程序

            dtProc.Columns.Add("Shb07", typeof(String));
            dtProc.Columns.Add("Shb08", typeof(String));
            dtProc.Columns.Add("Shb081", typeof(String));
            dtProc.Columns.Add("Shb082", typeof(String));
            dtProc.Columns.Add("Shb09", typeof(String));
            dtProc.Columns.Add("Shb10", typeof(String));
            dtProc.Columns.Add("Shb111", typeof(decimal));//成品數
            dtProc.Columns.Add("Shb112", typeof(decimal));//不良數
            dtProc.Columns.Add("Shb115", typeof(decimal));//BONUS數


            dtProc.Columns.Add("Ima01", typeof(String));//料號
            dtProc.Columns.Add("Ima02", typeof(String));
            dtProc.Columns.Add("Ima021", typeof(String));
            dtProc.Columns.Add("Occ02", typeof(String));//客戶
            dtProc.Columns.Add("Sfb224", typeof(String));//客戶訂單
            dtProc.Columns.Add("Eci06", typeof(String));//機台名稱
            dtProc.Columns.Add("Eca02", typeof(String));//工作站名稱
            dtProc.Columns.Add("Cnt", typeof(Int32));//操作人數
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

            string Sub1 = "N";
            string Sub2 = "N";
            if (cb_employee.Checked == true) Sub1 = "Y";
            if (cb_ngitem.Checked == true) Sub2 = "Y";


            var Org_Color = lb_msg.ForeColor;
            Cursor_wait(); //改變滑鼠游標漏斗指標            
            //DataTable dt = Mfr040.ToDoList(rPlant, rCdept, rBegdate, rEnddate);
            DataTable dt = Mfr040.ToDoList(rPlant, rCdept, rBegdate, rEnddate, dtProc);

            var count = dt.Rows.Count;            
            if (count == 0)
            {
                Config.Show("...無符合資料...");
                crystalReportViewer1.ReportSource = null;
                crystalReportViewer1.Refresh();
            }
            else
            {
                string ReportCond = f_Cond(rPlant, rBegdate, rEnddate, rCdept, Sub1, Sub2);//列印條件
                Print_cs(dt, ReportCond, Sub1, Sub2);
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

        private void Print_cs(DataTable dt, string ReportCond,string Sub1,string Sub2)
        {
            var rpname = "生產日報表";
            CrystalReport_mfr040 rp = new CrystalReport_mfr040();
            rp.SetDataSource(dt);            
            DataTable dt3 = Mfr040_3.ToDoList();//生產人員
            DataTable dt4 = Mfr040_4.ToDoList();//不良項目
            
            rp.Subreports["CrystalReport_mfr040_3.rpt"].SetDataSource(dt3); //子報表-生產人員
            rp.Subreports["CrystalReport_mfr040_4.rpt"].SetDataSource(dt4); //子報表-不良項目

            rp.SetParameterValue("CompName", Login.COMPNAME);//公司名稱            
            rp.SetParameterValue("ReportName", rpname);//報表名稱
            rp.SetParameterValue("ReportCond", ReportCond);//列印條件
            rp.SetParameterValue("ReportId", "mfr040");//程式編號            
            rp.SetParameterValue("ReportAuthor", Login.IDNAME);//製表人
            rp.SetParameterValue("Sub1", Sub1);//子報表-人員資料 --是否列印
            rp.SetParameterValue("Sub2", Sub2);//子報表-不良項目 --是否列印

            crystalReportViewer1.ReportSource = rp;
            crystalReportViewer1.Refresh();
        }


        //private void Print_cs(DataTable dt, string ReportCond)
        //{
        //    var rpname = "生產日報表";
        //    CrystalReport_mfr040 rp = new CrystalReport_mfr040();
        //    rp.SetDataSource(dt);
        //    DataTable dt2 = Mfr040_1.ToDoList();//不良項目
        //    DataTable dt3 = Mfr040_2.ToDoList();//生產人員

        //    rp.Subreports["CrystalReport_mfr040_1.rpt"].SetDataSource(dt2); //子報表
        //    rp.Subreports["CrystalReport_mfr040_2.rpt"].SetDataSource(dt3); //子報表

        //    rp.SetParameterValue("CompName", Login.COMPNAME);//公司名稱            
        //    rp.SetParameterValue("ReportName",rpname);//報表名稱
        //    rp.SetParameterValue("ReportCond", ReportCond);//列印條件
        //    rp.SetParameterValue("ReportId", "mfr040");//程式編號            
        //    rp.SetParameterValue("ReportAuthor",Login.IDNAME);//製表人

        //    crystalReportViewer1.ReportSource = rp;
        //    crystalReportViewer1.Refresh();
        //}

        private string f_Cond(string rPlant, string rBegdate, string rEnddate, string rCdept, string Sub1,string Sub2)
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
            if (Sub1 == "Y")
            {
                cond += string.Format(" | 列印生產人員");
            }
            if (Sub2 == "Y")
            {
                cond += string.Format(" | 列印不良項目");
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
