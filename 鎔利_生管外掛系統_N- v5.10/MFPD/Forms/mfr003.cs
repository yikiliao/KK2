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
    public partial class mfr003 : Form
    {        
        List<bool> LCdept = new List<bool>();//存 部門勾選
        List<bool> LPrno = new List<bool>();//存 工號勾選
        string Dept;

        //public string rDept; //傳過來的廠部
        //public Int16 rYy;//傳過來的年度
        //public Int16 rMm;//傳過來的月份
        //public string rPrno;//工號
        //public string rIsCall;//傳過來的

        //public void SetValue()
        //{            
        //    this.f_comDept.SelectedValue = rDept;
        //    this.f_cdept.Text = null;
        //    this.f_yy.Text = rYy.ToString();
        //    this.f_month.Text = rMm.ToString();
        //    this.f_prno.Text = string.Format("'{0}'", rPrno);
        //    this.f_type.SelectedIndex = 0;

        //    this.f_comDept.Enabled = false;
        //    this.f_cdept.Enabled = false;
        //    this.f_yy.Enabled = false;
        //    this.f_month.Enabled = false;
        //    this.f_type.Enabled = false;
        //    this.f_prno.Enabled = false;
        //    this.button2.Enabled = false;
        //    this.button3.Enabled = false;
        //    this.button1_Click(null, null);
        //    this.button1.Enabled = false;
        //}

        public mfr003()
        {
            InitializeComponent();            
            Config.SetFormSize(this, "R");
            Config.SetPer(this);
            init_Form();
        }

        private void init_Form()
        {
            Dept = Login.DeptOne;
            D_mfwork();//作業名稱
            Config.Set_DateTo(f_begdate, " ");//清空預設日期
            Config.Set_DateTo(f_enddate, " ");//清空預設日期
            f_ecm04.SelectedIndex = f_ecm04.Items.Count - 1;            
            f_begdate.Value = DateTime.Now.AddDays(1);//內定明天
            f_enddate.Value = DateTime.Now.AddDays(1);//內定明天
            f_model.Checked = true;
        }

        

        private void D_mfwork()
        {   
            DataTable dt = ecb_file.GToDoList(Dept);            
            //加入一列空白可以選全部
            DataRow dr = dt.NewRow();
            dr["ecb06"] = "";
            dr["ecb17"] = "--全選--";
            dt.Rows.Add(dr);

            f_ecm04.DataSource = dt;
            f_ecm04.ValueMember = "ecb06";
            f_ecm04.DisplayMember = "ecb17";
            f_ecm04.SelectedIndex = 0;
        }
         

        private void button1_Click(object sender, EventArgs e)
        {
            string rPlant = Login.DeptOne;
            string rMfWork = f_ecm04.SelectedValue == null ? "" : f_ecm04.SelectedValue.ToString();//作業名稱
            string rBegdate = f_begdate.Text;//生產日(起)
            string rEnddate = f_enddate.Text;//生產日(迄)                       

            var Org_Color = lb_msg.ForeColor;
            Cursor_wait(); //改變滑鼠游標漏斗指標

            DataTable dt = WorkSch.ToList(rPlant, rMfWork, rBegdate, rEnddate);
            var count = dt.Rows.Count;
            
            if (count == 0)
            {
                Config.Show("...無符合資料...");
                crystalReportViewer1.ReportSource = null;
                crystalReportViewer1.Refresh();
            }
            else
            {                
                string ReportCond = f_Cond(rPlant, rMfWork, rBegdate, rEnddate);//列印條件
                if (f_model.Checked == true)
                    Print_cs(dt, ReportCond);
                else
                    //Print_cs2(dt, ReportCond);
                    Print_cs3(dt, ReportCond);
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

        //
        private void Print_cs(DataTable dt, string ReportCond)
        {
            var rpname = "加工生產排程表--現場";
            CrystalReport_mfr003 rp = new CrystalReport_mfr003();
            rp.SetDataSource(dt);            
            rp.SetParameterValue("CompName", Login.COMPNAME);//公司名稱            
            rp.SetParameterValue("ReportName",rpname);//報表名稱
            rp.SetParameterValue("ReportCond", ReportCond);//列印條件
            rp.SetParameterValue("ReportId", "mfr003");//程式編號            
            rp.SetParameterValue("ReportAuthor",Login.IDNAME);//製表人
            crystalReportViewer1.ReportSource = rp;
            crystalReportViewer1.Refresh();
        }

        private void Print_cs2(DataTable dt, string ReportCond)
        {
            var rpname = "加工生產排程表--業務";
            CrystalReport_mfr003_2 rp = new CrystalReport_mfr003_2();
            rp.SetDataSource(dt);
            rp.SetParameterValue("CompName", Login.COMPNAME);//公司名稱            
            rp.SetParameterValue("ReportName", rpname);//報表名稱
            rp.SetParameterValue("ReportCond", ReportCond);//列印條件
            rp.SetParameterValue("ReportId", "mfr003");//程式編號            
            rp.SetParameterValue("ReportAuthor", Login.IDNAME);//製表人
            crystalReportViewer1.ReportSource = rp;
            crystalReportViewer1.Refresh();
        }

        private void Print_cs3(DataTable dt, string ReportCond)
        {
            var rpname = "加工生產排程表--業務3";
            CrystalReport_mfr003_3 rp = new CrystalReport_mfr003_3();
            rp.SetDataSource(dt);
            rp.SetParameterValue("CompName", Login.COMPNAME);//公司名稱            
            rp.SetParameterValue("ReportName", rpname);//報表名稱
            rp.SetParameterValue("ReportCond", ReportCond);//列印條件
            rp.SetParameterValue("ReportId", "mfr003");//程式編號            
            rp.SetParameterValue("ReportAuthor", Login.IDNAME);//製表人
            crystalReportViewer1.ReportSource = rp;
            crystalReportViewer1.Refresh();
        }


        private string f_Cond(string rPlant, string rMfWork, string rBegdate, string rEnddate)
        {
            string cond = "";
            cond += string.Format(" | 公司={0}", rPlant);

            if (!string.IsNullOrWhiteSpace(rMfWork))
            {
                cond += string.Format(" | 作業名稱={0}", rMfWork);
            }            

            if (!string.IsNullOrWhiteSpace(rBegdate))
            {
                cond += string.Format(" | 生產日期(起)>={0}", rBegdate);
            }
            if (!string.IsNullOrWhiteSpace(rEnddate))
            {
                cond += string.Format(" | 生產日期(迄)<={0}", rEnddate);
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
