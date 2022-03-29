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
    public partial class mfr010 : Form
    {        
        List<bool> LCdept = new List<bool>();//存 部門勾選
        List<bool> LPrno = new List<bool>();//存 工號勾選
        string Dept;
        

        public mfr010()
        {
            InitializeComponent();            
            Config.SetFormSize(this, "R");
            Config.SetPer(this);
            init_Form();
        }

        private void init_Form()
        {
            Dept = Login.DeptOne;
            Config.Set_DateTo(f_begdate, " ");//清空預設日期
            Config.Set_DateTo(f_enddate, " ");//清空預設日期                        
            f_begdate.Value = DateTime.Now.AddDays(0);//內定今天
            f_enddate.Value = DateTime.Now.AddDays(7);//內定今天+7
            Set_Print_Style();
        }

        private void Set_Print_Style()
        {
            f_print_style.Items.Add("1.生管");
            f_print_style.Items.Add("2.業務");
            f_print_style.Items.Add("3.工作站");
            f_print_style.Items.Add("4.工作站負荷");
            f_print_style.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int st = f_print_style.SelectedIndex;
            switch (st)
            {
                case 0:
                    sty_List();//清單
                    break;

                case 1:
                    sty_Sale();//交叉報表
                    break;

                case 2:
                    sty_Work();//交叉報表
                    break;

                case 3:
                    sty_Work2();//交叉報表
                    break;

                default:
                    break;
            }
        }


        private void sty_Sale()
        {
            string rPlant = Login.DeptOne;            
            string rBegdate = f_begdate.Text;//生產日(起)
            string rEnddate = f_enddate.Text;//生產日(迄)
            string rPrintType = "業務";

            var Org_Color = lb_msg.ForeColor;
            Cursor_wait(); //改變滑鼠游標漏斗指標

            DataTable dt = Schmast.ToList(rPlant, rBegdate, rEnddate);            
            var count = dt.Rows.Count;
            if (count == 0)
            {
                Config.Show("...無符合資料...");
                crystalReportViewer1.ReportSource = null;
                crystalReportViewer1.Refresh();
            }
            else
            {
                string ReportCond = f_Cond(rPlant, rBegdate, rEnddate, rPrintType);//列印條件
                Print_cs_Sale(dt, ReportCond);
            }
            UnCursor_wait(Org_Color); //改變滑鼠游標還原預設
        }

        private void sty_List()
        {
            string rPlant = Login.DeptOne;
            string rBegdate = f_begdate.Text;//生產日(起)
            string rEnddate = f_enddate.Text;//生產日(迄)
            string rPrintType = "生管";

            var Org_Color = lb_msg.ForeColor;
            Cursor_wait(); //改變滑鼠游標漏斗指標

            DataTable dt = Procsca.ToDoList(rPlant, rBegdate, rEnddate);

            var count = dt.Rows.Count;

            if (count == 0)
            {
                Config.Show("...無符合資料...");
                crystalReportViewer1.ReportSource = null;
                crystalReportViewer1.Refresh();
            }
            else
            {
                string ReportCond = f_Cond(rPlant, rBegdate, rEnddate, rPrintType);//列印條件
                Print_cs(dt, ReportCond);
            }
            UnCursor_wait(Org_Color); //改變滑鼠游標還原預設
        }

        private void sty_Work()
        {
            string rPlant = Login.DeptOne;            
            string rBegdate = f_begdate.Text;//生產日(起)
            string rEnddate = f_enddate.Text;//生產日(迄) 
            string rPrintType = "工作站";

            var Org_Color = lb_msg.ForeColor;
            Cursor_wait(); //改變滑鼠游標漏斗指標

            DataTable dt = WorkSch.ToList(rPlant, string.Empty, rBegdate, rEnddate);
            var count = dt.Rows.Count;
            if (count == 0)
            {
                Config.Show("...無符合資料...");
                crystalReportViewer1.ReportSource = null;
                crystalReportViewer1.Refresh();
            }
            else
            {
                string ReportCond = f_Cond(rPlant, rBegdate, rEnddate, rPrintType);//列印條件
                Print_cs_Work(dt, ReportCond);
            }
            UnCursor_wait(Org_Color); //改變滑鼠游標還原預設
        }

        private void sty_Work2()
        {
            string rPlant = Login.DeptOne;
            string rBegdate = f_begdate.Text;//生產日(起)
            string rEnddate = f_enddate.Text;//生產日(迄) 
            string rPrintType = "工作站負荷";

            var Org_Color = lb_msg.ForeColor;
            Cursor_wait(); //改變滑鼠游標漏斗指標

            DataTable dt = WorkSch.ToList(rPlant, string.Empty, rBegdate, rEnddate);
            var count = dt.Rows.Count;
            if (count == 0)
            {
                Config.Show("...無符合資料...");
                crystalReportViewer1.ReportSource = null;
                crystalReportViewer1.Refresh();
            }
            else
            {
                string ReportCond = f_Cond(rPlant, rBegdate, rEnddate, rPrintType);//列印條件
                Print_cs_Work2(dt, ReportCond);
            }
            UnCursor_wait(Org_Color); //改變滑鼠游標還原預設
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    string rPlant = Login.DeptOne;           
        //    string rBegdate = f_begdate.Text;//生產日(起)
        //    string rEnddate = f_enddate.Text;//生產日(迄)                       

        //    var Org_Color = lb_msg.ForeColor;
        //    Cursor_wait(); //改變滑鼠游標漏斗指標

        //    DataTable dt = Procsca.ToDoList(rPlant, rBegdate, rEnddate);

        //    var count = dt.Rows.Count;

        //    if (count == 0)
        //    {
        //        Config.Show("...無符合資料...");
        //        crystalReportViewer1.ReportSource = null;
        //        crystalReportViewer1.Refresh();
        //    }
        //    else
        //    {                
        //        string ReportCond = f_Cond(rPlant, rBegdate, rEnddate);//列印條件
        //        Print_cs(dt, ReportCond);
        //    }
        //    UnCursor_wait(Org_Color); //改變滑鼠游標還原預設
        //}


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

        private void Print_cs_Sale(DataTable dt, string ReportCond)
        {
            var rpname = "加工主排程表(業務)";
            CrystalReport_mfr016 rp = new CrystalReport_mfr016();
            rp.SetDataSource(dt);
            rp.SetParameterValue("CompName", Login.COMPNAME);//公司名稱            
            rp.SetParameterValue("ReportName", rpname);//報表名稱
            rp.SetParameterValue("ReportCond", ReportCond);//列印條件
            rp.SetParameterValue("ReportId", "mfr010");//程式編號            
            rp.SetParameterValue("ReportAuthor", Login.IDNAME);//製表人
            crystalReportViewer1.ReportSource = rp;
            crystalReportViewer1.Refresh();
        }

        private void Print_cs_Work(DataTable dt, string ReportCond)
        {
            var rpname = "加工主排程表(業務-工作站)";
            CrystalReport_mfr003_3 rp = new CrystalReport_mfr003_3();
            rp.SetDataSource(dt);
            rp.SetParameterValue("CompName", Login.COMPNAME);//公司名稱            
            rp.SetParameterValue("ReportName", rpname);//報表名稱
            rp.SetParameterValue("ReportCond", ReportCond);//列印條件
            rp.SetParameterValue("ReportId", "mfr010");//程式編號            
            rp.SetParameterValue("ReportAuthor", Login.IDNAME);//製表人
            crystalReportViewer1.ReportSource = rp;
            crystalReportViewer1.Refresh();
        }
        //
        private void Print_cs(DataTable dt, string ReportCond)
        {
            var rpname = "加工主排程表(生管)";
            CrystalReport_mfr003_1 rp = new CrystalReport_mfr003_1();
            rp.SetDataSource(dt);            
            rp.SetParameterValue("CompName", Login.COMPNAME);//公司名稱            
            rp.SetParameterValue("ReportName",rpname);//報表名稱
            rp.SetParameterValue("ReportCond", ReportCond);//列印條件
            rp.SetParameterValue("ReportId", "mfr010");//程式編號            
            rp.SetParameterValue("ReportAuthor",Login.IDNAME);//製表人
            crystalReportViewer1.ReportSource = rp;
            crystalReportViewer1.Refresh();
        }

        private void Print_cs_Work2(DataTable dt, string ReportCond)
        {
            var rpname = "加工主排程表(工作站負荷)";
            CrystalReport_mfr003_4 rp = new CrystalReport_mfr003_4();
            rp.SetDataSource(dt);
            rp.SetParameterValue("CompName", Login.COMPNAME);//公司名稱            
            rp.SetParameterValue("ReportName", rpname);//報表名稱
            rp.SetParameterValue("ReportCond", ReportCond);//列印條件
            rp.SetParameterValue("ReportId", "mfr010");//程式編號            
            rp.SetParameterValue("ReportAuthor", Login.IDNAME);//製表人
            crystalReportViewer1.ReportSource = rp;
            crystalReportViewer1.Refresh();
        }

        private string f_Cond(string rPlant, string rBegdate, string rEnddate, string rPrintType)
        {
            string cond = "";
            cond += string.Format(" | 公司={0}", rPlant);

            if (!string.IsNullOrWhiteSpace(rPrintType))
            {
                cond += string.Format(" | 列印類型={0}", rPrintType);
            }

            if (!string.IsNullOrWhiteSpace(rBegdate))
            {
                cond += string.Format(" | 預計開工日(起)>={0}", rBegdate);
            }
            if (!string.IsNullOrWhiteSpace(rEnddate))
            {
                cond += string.Format(" | 預計開工日(迄)<={0}", rEnddate);
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
            f_enddate.Text = f_begdate.Value.AddDays(7).ToString("yyyy/MM/dd");
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
