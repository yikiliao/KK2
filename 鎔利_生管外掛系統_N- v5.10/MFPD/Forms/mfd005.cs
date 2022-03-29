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
    public partial class mfd005 : Form
    {
        string rSfb01;
        string Menu_Sel;

        string SFB04;
        string SFB223;
        string SFB224;
        public mfd005()
        {
            InitializeComponent();
            Config.SetForm(this);
            //Config.SetFormSize(this, "F");
            //Config.SetPer(this);            
            init_GridView();
        }

        private void init_GridView()
        {
            //是否允許使用者編輯
            //this.dataGridView1.ReadOnly = true;
            //是否允許使用者自行新增
            //this.dataGridView1.AllowUserToAddRows = false;

            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.RowHeadersWidth = 5;
            this.dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.RowHeadersWidth = 5;
            this.dataGridView2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView2.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView2.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView2.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.dataGridView3.AllowUserToResizeRows = false;
            this.dataGridView3.RowHeadersWidth = 5;
            this.dataGridView3.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView3.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView3.Columns[1].Visible = false;//隱藏
        }

        private void mfd004_Load(object sender, EventArgs e)
        {
            clsForm();            
            find_d();
        }


        private void clsForm()
        {
            btn_Ins.Enabled = false;
            btn_Del.Enabled = false;
            btn_ecd01.Enabled = false;//作業編號按鈕
            btn_ecd07.Enabled = false;//工作站按鈕
            HeadCls();
            RecodeCls();
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();          

            foreach (DataGridViewRow item in dataGridView3.Rows)
            {
                dataGridView3.Rows.RemoveAt(item.Index);
            }
        }

        private void find_d()
        {
            dataGridView1.Rows.Clear();            
            DataTable dt = sql_sfb_file.FindList(Login.DeptOne, string.Empty, "1");//進主排的
            int idx = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView1.Rows.Add(new object[] {
                    false,
                    dt.Rows[i]["sfb22"],
                    dt.Rows[i]["sfb221"],
                    dt.Rows[i]["sfb01"],
                    dt.Rows[i]["occ02"],
                    dt.Rows[i]["sfb224"],
                    Convert.ToInt16(dt.Rows[i]["sfb08"])});
                idx += 1;
            }            
            if (idx > 0)
            {
                dataGridView1.Rows[0].Selected = true;                
                rSfb01 = dataGridView1.Rows[0].Cells[3].Value.ToString();//工單號
                f_Data();//找資料
            }
        }

        //private void find_d()
        //{
        //    dataGridView1.Rows.Clear();
        //    DataTable dt = sql_sfb_file.FindList(Login.DeptOne, string.Empty, "1");//進主排的
        //    int idx = 0;
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        dataGridView1.Rows.Add(new object[] {
        //            dt.Rows[i]["sfb22"],
        //            dt.Rows[i]["sfb221"],
        //            dt.Rows[i]["sfb01"],
        //            dt.Rows[i]["occ02"],
        //            dt.Rows[i]["sfb08"]});
        //        idx += 1;
        //    }
        //    if (idx > 0)
        //    {
        //        dataGridView1.Rows[0].Selected = true;
        //        rSfb01 = dataGridView1.Rows[0].Cells[2].Value.ToString();//工單號
        //        f_Data();//找資料
        //    }
        //}


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex < 0) || (e.ColumnIndex < 0)) return;
            dataGridView1.Rows[e.RowIndex].Selected = true;
            isCheck(e.RowIndex);//點選勾選            
            rSfb01 = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();//工單號
            f_Data();//找資料 
            RecodeCls();
        }

        private void isCheck(int e)
        {
            if ((bool)dataGridView1.Rows[e].Cells["ck"].Value == false)
                dataGridView1.Rows[e].Cells["ck"].Value = true;
            else
                dataGridView1.Rows[e].Cells["ck"].Value = false;            
        }
        
        private void f_Data()
        {            
            DataTable dt = sql_sfb_file.FindList(string.Empty, rSfb01, "1");//找進主排的資料
            HeadShow(dt);
            PschShow();//
            GvSort();//排序
        }

        //private void f_Data(string rSfb01)
        //{
        //    string sql = string.Empty;
        //    sql += "select sfb_file.sfb01,occ_file.occ02,ima_file.ima01,ima_file.ima02,ima_file.ima021,sfb_file.sfb06,sfb_file.sfb08,sfb_file.sfb13,sfb_file.sfb15,sfb_file.status from sfb_file";
        //    sql += " LEFT OUTER JOIN occ_file on occ_file.occ01 = sfb_file.sfb223";
        //    sql += " LEFT OUTER JOIN ima_file on ima_file.ima01 = sfb_file.sfb05";
        //    sql += " where 1=1";
        //    sql += " and sfb_file.sfb01='" + rSfb01 + "'";
        //    sql += " and sfb_file.status ='1'"; //進主排的
        //    DataTable dt = DOSQL.GetDataTable(Login.WU, sql);            
        //    HeadShow(dt);
        //    PschShow();//
        //    GvSort();//排序
        //}
        private void HeadShow(DataTable dt)
        {
            f_sfb01.Text = dt.Rows[0]["sfb01"].ToString();
            f_sfb22.Text = dt.Rows[0]["sfb22"].ToString();
            f_sfb221.Text = dt.Rows[0]["sfb221"].ToString();
            f_occ02.Text = dt.Rows[0]["occ02"].ToString();
            f_ima01.Text = dt.Rows[0]["ima01"].ToString();
            f_ima02.Text = dt.Rows[0]["ima02"].ToString();
            f_ima021.Text = dt.Rows[0]["ima021"].ToString();
            f_sfb06.Text = dt.Rows[0]["sfb06"].ToString();
            f_sfb08.Text = dt.Rows[0]["sfb08"].ToString();
            f_sfb13.Text = dt.Rows[0]["sfb13m"].ToString();
            f_sfb15.Text = dt.Rows[0]["sfb15m"].ToString();
            
            f_sfb223.Text = dt.Rows[0]["sfb223"].ToString();
            f_sfb224.Text = dt.Rows[0]["sfb224"].ToString();
        }

        private void PschShow()
        {
            string sql = string.Empty;
            sql += "SELECT ecm03,ecm04,ecm45,ecm06,eca02,begdate,rg,enddate from procsch";
            sql += " where 1=1";
            sql += " and ecm01='" + f_sfb01.Text + "'";
            sql += " ORDER BY ecm01,ecm03";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql);

            dataGridView2.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView2.Rows.Add(new object[] {
                    dt.Rows[i]["ecm03"],
                    dt.Rows[i]["ecm04"],
                    dt.Rows[i]["ecm45"],
                    dt.Rows[i]["ecm06"],
                    dt.Rows[i]["eca02"],
                    dt.Rows[i]["begdate"],
                    dt.Rows[i]["rg"],
                    dt.Rows[i]["enddate"]});
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex < 0) || (e.ColumnIndex < 0)) return;
            dataGridView2.Rows[e.RowIndex].Selected = true;
            btn_imp_Click(null, null);
        }

        private void btn_imp_Click(object sender, EventArgs e)
        {
            if (f_ckSel() == false)
            {
                MessageBox.Show("請選擇單一列資料");
                return;
            }
            //匯入相關資料
            SingRecord();
            //顯示已入排程資料
            ShowGv3();
        }

        private void SingRecord()
        {
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Selected == true)
                {
                    f_r_ecm03.Text = dataGridView2.Rows[i].Cells["ecm03"].Value.ToString();
                    f_r_ecd01.Text = dataGridView2.Rows[i].Cells["ecm04"].Value.ToString();
                    f_r_ecd02.Text = dataGridView2.Rows[i].Cells["ecm45"].Value.ToString();
                    f_r_ecd07.Text = dataGridView2.Rows[i].Cells["ecm06"].Value.ToString();
                    f_r_eca02.Text = dataGridView2.Rows[i].Cells["eca02"].Value.ToString();


                    f_r_begdate_s.Value = System.Convert.ToDateTime(dataGridView2.Rows[i].Cells["begdate"].Value.ToString());
                    f_r_begdate.Text = dataGridView2.Rows[i].Cells["begdate"].Value.ToString();
                    f_r_rg.Text = dataGridView2.Rows[i].Cells["rg"].Value.ToString();
                    f_r_enddate_s.Value = System.Convert.ToDateTime(dataGridView2.Rows[i].Cells["enddate"].Value.ToString());
                    f_r_enddate.Text = dataGridView2.Rows[i].Cells["enddate"].Value.ToString();
                }
            }
        }

        //private void ShowGv3()
        //{
        //    DataTable dt = Procscm.SumScm(f_r_begdate.Text, f_r_ecd01.Text);
        //    dataGridView3.DataSource = dt;
        //}

        private void ShowGv3()
        {
            DataTable dt = Procscm.SumScm(f_r_begdate.Text, f_r_ecd01.Text);

            dataGridView3.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView3.Rows.Add(new object[] {
                    dt.Rows[i]["sfb01"],
                    dt.Rows[i]["ecm04"],
                    Convert.ToInt16(dt.Rows[i]["sfb08"])});
            }
        }


        private bool f_ckSel()
        {
            bool rf = false;
            int rcnt = 0;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Selected == true)
                {
                    rcnt += 1;
                }
            }
            if (rcnt == 1)
                rf = true;
            else
                rf = false;
            return rf;
        }

        private void f_r_begdate_s_ValueChanged(object sender, EventArgs e)
        {
            f_r_begdate.Text = f_r_begdate_s.Value.ToString("yyyy/MM/dd");
            f_r_enddate.Text = f_r_begdate_s.Value.ToString("yyyy/MM/dd");
        }

        private void f_r_enddate_s_ValueChanged(object sender, EventArgs e)
        {
            f_r_enddate.Text = f_r_enddate_s.Value.ToString("yyyy/MM/dd");
        }

        private void btn_ecd01_Click(object sender, EventArgs e)
        {
            rEcd fm = new rEcd();
            fm.Dept = string.Empty;
            if (fm.ShowDialog() == DialogResult.OK)
            {
                //接收回傳資料
                DataTable dt = fm.dtProd;
                RecordShow(dt);//顯示資料
            }
        }

        private void RecordShow(DataTable dt)
        {
            f_r_ecd01.Text = dt.Rows[0]["ecd01"].ToString();
            f_r_ecd02.Text = dt.Rows[0]["ecd02"].ToString();
            f_r_ecd07.Text = dt.Rows[0]["ecd07"].ToString();
            f_r_eca02.Text = getEca02(f_r_ecd07.Text);            
        }

        private string getEca02(string Eca01)
        {
            string rs = string.Empty;
            DataTable dt = eca_file.Get(Eca01);
            rs = dt.Rows[0]["eca02"].ToString();
            return rs;
        }

        private int TimeSpan(string Begdate, string Enddate)
        {
            int a = new TimeSpan(System.Convert.ToDateTime(Enddate).Ticks - System.Convert.ToDateTime(Begdate).Ticks).Days;
            return a;
        }

        private void btn_ecd07_Click(object sender, EventArgs e)
        {
            rEca fm = new rEca();
            fm.Dept = string.Empty;
            if (fm.ShowDialog() == DialogResult.OK)
            {
                //接收回傳資料
                DataTable dt = fm.dtProd;
                f_r_ecd07.Text = dt.Rows[0]["eca01"].ToString();
                f_r_eca02.Text = getEca02(f_r_ecd07.Text);
            }
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {           
            if (Config.Message("確定刪除?") == "Y")
            {
                foreach (DataGridViewRow item in this.dataGridView2.SelectedRows)
                {
                    dataGridView2.Rows.RemoveAt(item.Index);
                }
            }
        }

        private void btn_Ins_Click(object sender, EventArgs e)
        {            
            if (f_RecordCK() == false)
            {
                MessageBox.Show("欄位不可空白");
                return;
            }
            //檢查製程序編號
            if (Ecm03Doub() == true)
            {
                MessageBox.Show("序號不可重複");
                f_r_ecm03.Select();
                return;
            }
            f_r_rg.Text = TimeSpan(f_r_begdate.Text, f_r_enddate.Text).ToString();
            dataGridView2.Rows.Add(new object[] {System.Convert.ToInt32(f_r_ecm03.Text),
                f_r_ecd01.Text,
                f_r_ecd02.Text,
                f_r_ecd07.Text,
                f_r_eca02.Text,
                f_r_begdate.Text,
                f_r_rg.Text,
                f_r_enddate.Text});
            GvSort();//排序
        }

        private bool Ecm03Doub()
        {
            bool rf = false;
            Int32 a = System.Convert.ToInt32(f_r_ecm03.Text);
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                Int32 b = System.Convert.ToInt32(dataGridView2.Rows[i].Cells["ecm03"].Value.ToString());
                if (a == b)
                {
                    rf = true;
                    break;
                }
            }
            return rf;
        }

        private bool f_RecordCK()
        {
            bool rf = false;
            if (f_r_ecm03.Text.ToString().Trim().Length == 0 ||
                f_r_ecd01.Text.ToString().Trim().Length == 0 ||
                f_r_ecd02.Text.ToString().Trim().Length == 0 ||
                f_r_ecd07.Text.ToString().Trim().Length == 0 ||
                f_r_eca02.Text.ToString().Trim().Length == 0 ||
                f_r_begdate.Text.ToString().Trim().Length == 0 ||
                f_r_enddate.Text.ToString().Trim().Length == 0)
            {
                rf = false;
            }
            else
            {
                rf = true;
            }
            return rf;
        }

        private void GvSort()
        {
            // 根據 資料行1 (Name) 做 小到大 排序 
            dataGridView2.Sort(dataGridView2.Columns["ecm03"], System.ComponentModel.ListSortDirection.Ascending);
        }

        private void btn_upd_Click(object sender, EventArgs e)
        {
            if (f_r_ecm03.Text.Length == 0) return;
            Int16 a_ecm03 = System.Convert.ToInt16(f_r_ecm03.Text); //製程序
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                Int16 r_ecm03 = System.Convert.ToInt16(dataGridView2.Rows[i].Cells["ecm03"].Value.ToString());//明細製程序
                Int16 r_rg = System.Convert.ToInt16(dataGridView2.Rows[i].Cells["rg"].Value.ToString());//明細間隔
                if (a_ecm03 == r_ecm03)
                {
                    dataGridView2.Rows[i].Cells["begdate"].Value = f_r_begdate.Text;
                    dataGridView2.Rows[i].Cells["enddate"].Value = f_r_enddate.Text;
                    dataGridView2.Rows[i].Cells["rg"].Value = TimeSpan(f_r_begdate.Text, f_r_enddate.Text).ToString();
                }
                if (a_ecm03 < r_ecm03)
                {
                    string td = dataGridView2.Rows[i - 1].Cells["enddate"].Value.ToString(); // 上一筆的完工日
                    dataGridView2.Rows[i].Cells["begdate"].Value = td;
                    dataGridView2.Rows[i].Cells["enddate"].Value = rEndDate(td, r_rg);
                    dataGridView2.Rows[i].Cells["rg"].Value = TimeSpan(dataGridView2.Rows[i].Cells["begdate"].Value.ToString(), dataGridView2.Rows[i].Cells["enddate"].Value.ToString()).ToString();
                }
            }
        }

        //private void btn_upd_Click(object sender, EventArgs e)
        //{
        //    if (f_r_ecm03.Text.Length == 0) return;
        //    Int16 a_ecm03 = System.Convert.ToInt16(f_r_ecm03.Text); //製程序
        //    for (int i = 0; i < dataGridView2.Rows.Count; i++)
        //    {
        //        Int16 r_ecm03 = System.Convert.ToInt16(dataGridView2.Rows[i].Cells["ecm03"].Value.ToString());//明細製程序
        //        Int16 r_rg = System.Convert.ToInt16(dataGridView2.Rows[i].Cells["rg"].Value.ToString());//明細間隔
        //        if (a_ecm03 == r_ecm03)
        //        {
        //            dataGridView2.Rows[i].Cells["begdate"].Value = f_r_begdate.Text;
        //            dataGridView2.Rows[i].Cells["enddate"].Value = f_r_enddate.Text;
        //            dataGridView2.Rows[i].Cells["rg"].Value = TimeSpan(f_r_begdate.Text, f_r_enddate.Text).ToString();                    
        //        }
        //        if (a_ecm03 < r_ecm03)
        //        {
        //            dataGridView2.Rows[i].Cells["begdate"].Value = f_r_enddate.Text;
        //            dataGridView2.Rows[i].Cells["enddate"].Value = rEndDate(f_r_enddate.Text, r_rg);
        //            dataGridView2.Rows[i].Cells["rg"].Value = TimeSpan(dataGridView2.Rows[i].Cells["begdate"].Value.ToString(), dataGridView2.Rows[i].Cells["enddate"].Value.ToString()).ToString();
        //        }
        //    }
        //}

        private string rEndDate(string d, Int16 r)
        {
            DateTime a = new DateTime();
            DateTime b = new DateTime();
            a = System.Convert.ToDateTime(d);
            b = a.AddDays(r);
            return b.ToString("yyyy/MM/dd");
        }

        private void f_OK_Click(object sender, EventArgs e)
        {
            Upd();
        }

        //private void f_OK_Click(object sender, EventArgs e)
        //{
        //    //如果 主檔沒資料就新增  否則就是修改
        //    if (f_Check() == false) { MessageBox.Show("請輸入資料"); return; }
        //    string rSel = f_sfb01.Text;
        //    if (Procsca.GetTable(rSel).Rows.Count == 0)
        //    {
        //        Add();
        //        clsForm();//清畫面
        //        find_d();//找已排入主排資料
        //    }
        //    else
        //    {
        //        Upd();
        //    }
        //}

        

        private void Add()
        {
            if (sql_sfb_file.HaveRow(f_sfb01.Text, Login.WU).Rows.Count != 0)
            {
                MessageBox.Show("已有此筆資料");
                return;
            }
            if (Config.Message("確定新增?") == "Y")
            {
                try
                {
                    f_Insert();//--主排程                    
                    clsForm();//清畫面
                    find_d();//找已排入主排資料
                }
                catch (Exception ex)
                {
                    Config.Show(ex.Message);
                }
            }
            else
            {
                return;
            }
        }

        //private void Add()
        //{
        //    if (Config.Message("確定新增?") == "Y")
        //    {
        //        try
        //        {
        //            f_Insert();//--主排程
        //            if (ProcscaDay.GetTable(f_sfb01.Text).Rows.Count == 0)//--日排程如沒資料寫入
        //            {
        //                f_InsertDay();
        //            }
        //            clsForm();//清畫面
        //            find_d();//找已排入主排資料
        //        }
        //        catch (Exception ex)
        //        {
        //            Config.Show(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}

        private void Upd()
        {
            if (Config.Message("確定修改?") == "Y")
            {
                try
                {

                    f_Update();
                }
                catch (Exception ex)
                {
                    Config.Show(ex.Message);
                }
            }
            else
            {
                return;
            }
        }

        private void f_Update()
        {
            //刪除明細
            Del_Procsch();
            Ins_Procsch(); //加入明細
            //------
            Del_Procscm();//刪除排程
            Ins_Procscm();//排程

            //--
            Upd_Procsca();
        }


        private void Upd_Procsca()
        {
            //抓第一站開工日
            string sql = string.Empty;
            sql += "SELECT top 1 begdate from procsch WHERE 1=1 ";
            sql += " and ecm01 = '" + rSfb01 + "'";
            sql += " ORDER BY ecm03";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
            string bdate = dt.Rows[0][0].ToString();
            //抓最後一站開工日
            sql = string.Empty;
            sql += "SELECT top 1 enddate from procsch WHERE 1=1 ";
            sql += " and ecm01 = '" + rSfb01 + "'";
            sql += " ORDER BY ecm03 desc";
            DataTable dt1 = DOSQL.GetDataTable(Login.WU, sql);
            string edate = dt1.Rows[0][0].ToString();

            sql = string.Empty;
            sql = string.Format("update procsca set sfb13='{0}', sfb15='{1}' where sfb01='{2}'", bdate, edate, rSfb01);
            int i = DOSQL.Excute(Login.WU, sql);
            if (i <= 0) MessageBox.Show("修改失敗-Procsca");

            //update sfb_file 主排程開始日期及完工日期
            sql = string.Empty;
            sql = string.Format("update sfb_file set sfb13m='{0}', sfb15m='{1}', sfb13d='{2}', sfb15d='{3}', sfb13w='{4}', sfb15w='{5}' where sfb01='{6}'", bdate, edate, bdate, edate, bdate, edate, rSfb01);
            int j = DOSQL.Excute(Login.WU, sql);
            if (j <= 0) MessageBox.Show("修改失敗-sfb_file");

            //主排報表記錄檔
            TrnMast(rSfb01, bdate, edate, System.Convert.ToDecimal(f_sfb08.Text));

        }



        private void Del_Procsch()
        {
            string rs = f_sfb01.Text;
            string sql = string.Empty;
            sql += "delete from procsch where ecm01 ='" + rs + "'";
            int i = DOSQL.Excute(Login.WU, sql);
        }

        private void Del_Procscm()
        {
            string rs = f_sfb01.Text;
            string sql = string.Empty;
            sql += "delete from procscm where sfb01 ='" + rs + "'";
            int i = DOSQL.Excute(Login.WU, sql);
        }

        private void Del_ProcschDay()
        {
            string rs = f_sfb01.Text;
            string sql = string.Empty;
            sql += "delete from procsch where ecm01 ='" + rs + "'";
            int i = DOSQL.Excute(Login.WU, sql);
        }

        private void Ins_ProcschDay()
        {
            string sql = string.Empty;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                ProcschDay p_ = new ProcschDay();
                p_.Ecm01 = f_sfb01.Text;
                p_.Ecm03 = System.Convert.ToInt16(dataGridView2.Rows[i].Cells["ecm03"].Value.ToString());
                p_.Ecm04 = dataGridView2.Rows[i].Cells["ecm04"].Value.ToString();
                p_.Ecm45 = dataGridView2.Rows[i].Cells["ecm45"].Value.ToString();
                p_.Ecm06 = dataGridView2.Rows[i].Cells["ecm06"].Value.ToString();
                p_.Eca02 = dataGridView2.Rows[i].Cells["eca02"].Value.ToString();
                p_.Begdate = dataGridView2.Rows[i].Cells["begdate"].Value.ToString();
                p_.Enddate = dataGridView2.Rows[i].Cells["enddate"].Value.ToString();
                p_.Rg = System.Convert.ToInt16(dataGridView2.Rows[i].Cells["rg"].Value.ToString());
                sql += ProcschDay.Set_Insert(p_);
            }
            if (sql.Length > 0)
            {
                Double i = DOSQL.Excute(Login.WU, sql);
                if (i <= 0) MessageBox.Show("新增失敗-ProcschDay");
            }
        }

        private void Ins_ProcschDay(string sfb01)
        {
            string sql_1 = "select * from procsch where ecm01='" + sfb01 + "'";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql_1);
            string sql = string.Empty;            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ProcschDay p_ = new ProcschDay();
                p_.Ecm01 = dt.Rows[i]["ecm01"].ToString();
                p_.Ecm03 = System.Convert.ToInt16(dt.Rows[i]["ecm03"].ToString());
                p_.Ecm04 = dt.Rows[i]["ecm04"].ToString();
                p_.Ecm45 = dt.Rows[i]["ecm45"].ToString();
                p_.Ecm06 = dt.Rows[i]["ecm06"].ToString();
                p_.Eca02 = dt.Rows[i]["eca02"].ToString();
                p_.Begdate = dt.Rows[i]["begdate"].ToString();
                p_.Enddate = dt.Rows[i]["enddate"].ToString();
                p_.Rg = System.Convert.ToInt16(dt.Rows[i]["rg"].ToString());
                sql += ProcschDay.Set_Insert(p_);
            }
            if (sql.Length > 0)
            {
                Double i = DOSQL.Excute(Login.WU, sql);
                if (i <= 0) MessageBox.Show("新增失敗-ProcschDay");
            }
        }


        private void Del_ProcscmDay()
        {
            string rs = f_sfb01.Text;
            string sql = string.Empty;
            sql += "delete from procscm where sfb01 ='" + rs + "'";
            int i = DOSQL.Excute(Login.WU, sql);
        }

        private void Ins_ProcscmDay()
        {
            string sql = string.Empty;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                int DRG = System.Convert.ToInt16(dataGridView2.Rows[i].Cells["rg"].Value.ToString());//間距
                for (int j = 0; j <= DRG; j++)
                {
                    ProcscmDay p_ = new ProcscmDay();
                    p_.Sfb01 = f_sfb01.Text;
                    p_.Ecb03 = System.Convert.ToInt16(dataGridView2.Rows[i].Cells["ecm03"].Value.ToString());
                    p_.Schdate = getSchdate(j, dataGridView2.Rows[i].Cells["begdate"].Value.ToString());
                    p_.Ecm04 = dataGridView2.Rows[i].Cells["ecm04"].Value.ToString();
                    p_.Ecm45 = dataGridView2.Rows[i].Cells["ecm45"].Value.ToString();
                    p_.Ecm06 = dataGridView2.Rows[i].Cells["ecm06"].Value.ToString();
                    p_.Eca02 = dataGridView2.Rows[i].Cells["eca02"].Value.ToString();
                    sql += ProcscmDay.Set_Insert(p_);
                }
            }
            if (sql.Length > 0)
            {
                Double i = DOSQL.Excute(Login.WU, sql);
                if (i <= 0) MessageBox.Show("新增失敗-ProcscmDay");
            }
        }

        private void Ins_ProcscmDay(string sfb01)
        {
            string sql_1 = "select * from procscm where sfb01='" + sfb01 + "'";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql_1);
            string sql = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ProcscmDay p_ = new ProcscmDay();
                p_.Sfb01 = dt.Rows[i]["sfb01"].ToString();
                p_.Ecb03 = System.Convert.ToInt16(dt.Rows[i]["ecb03"].ToString());
                p_.Schdate = dt.Rows[i]["schdate"].ToString();
                p_.Ecm04 = dt.Rows[i]["ecm04"].ToString();
                p_.Ecm45 = dt.Rows[i]["ecm45"].ToString();
                p_.Ecm06 = dt.Rows[i]["ecm06"].ToString();
                p_.Eca02 = dt.Rows[i]["eca02"].ToString();
                sql += ProcscmDay.Set_Insert(p_);
            }
            if (sql.Length > 0)
            {
                Double i = DOSQL.Excute(Login.WU, sql);
                if (i <= 0) MessageBox.Show("新增失敗-ProcscmDay");
            }
        }

        private string getSchdate(int j, string rd)
        {
            return System.Convert.ToDateTime(rd).AddDays(j).ToString("yyyy/MM/dd");
        }

        private void btn_unOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void f_prno_bt_Click(object sender, EventArgs e)
        //{
        //    rWKno fm = new rWKno();
        //    fm.dept = Login.DeptOne;
        //    fm.rType = "A";
        //    if (fm.ShowDialog() == DialogResult.OK)
        //    {
        //        DataTable dt = fm.dtProd;
        //        HeadShow(dt);//單頭                
        //        HeadtlShow();//單身
        //        RecodeCls();//清單列資料
        //        GvSort();//排序
        //    }
        //}

        private void f_prno_bt_Click(object sender, EventArgs e)
        {
            rWKno fm = new rWKno();
            fm.dept = Login.DeptOne;            
            if (fm.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = fm.dtProd;
                btcTrn(dt);//轉入資料
                clsForm();//清畫面
                find_d();//找已排入主排資料                
            }
        }

        private void btcTrn(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                f_sfb01.Text = dt.Rows[i]["sfb01"].ToString();
                f_sfb22.Text = dt.Rows[i]["sfb22"].ToString();
                f_sfb221.Text = dt.Rows[i]["sfb221"].ToString();
                f_occ02.Text = dt.Rows[i]["occ02"].ToString();
                f_ima01.Text = dt.Rows[i]["ima01"].ToString();
                f_ima02.Text = dt.Rows[i]["ima02"].ToString();
                f_ima021.Text = dt.Rows[i]["ima021"].ToString();
                f_sfb06.Text = dt.Rows[i]["sfb06"].ToString();
                f_sfb08.Text = dt.Rows[i]["sfb08"].ToString();
                f_sfb13.Text = dt.Rows[i]["sfb13"].ToString();
                f_sfb15.Text = dt.Rows[i]["sfb15"].ToString();

                f_sfb223.Text = dt.Rows[i]["sfb223"].ToString();
                f_sfb224.Text = dt.Rows[i]["sfb224"].ToString();
                Ins_Procsca();
                Ins_Sfb("1");
            }

            //轉主排程各站製程及主排日誌檔
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sfb01 = dt.Rows[i]["sfb01"].ToString();
                string begdate = dt.Rows[i]["sfb13"].ToString();
                string enddate = dt.Rows[i]["sfb15"].ToString();
                string sql = string.Empty;
                sql += "SELECT ecm01,ecm03,ecm04,ecm45,ecm06,eca02 from ew.ecm_file";
                sql += " LEFT OUTER JOIN ew.eca_file on eca_file.eca01 = ecm06";
                sql += " where 1=1";
                sql += " and ecm01='" + sfb01 + "'";
                sql += " ORDER BY ecm01,ecm03";
                DataTable dt2 = DOORC.GetDataTable(Login.TT, sql);
                DataTable dt3 = btcTrn_1(dt2, begdate, enddate);//轉各工作站開工完工日及工作日期間距

                iPsch(dt3);
                iPscm(dt3);
            }

            //轉主排程報表資料檔
            for (int i = 0; i < dt.Rows.Count; i++)
            {                
                string sfb01 = dt.Rows[i]["sfb01"].ToString();
                string begdate = dt.Rows[i]["sfb13"].ToString();
                string enddate = dt.Rows[i]["sfb15"].ToString();
                decimal sfb08 = System.Convert.ToDecimal(dt.Rows[i]["sfb08"].ToString());
                TrnMast(sfb01, begdate, enddate, sfb08);
            }
        }
        
        private void TrnMast(string sfb01,string begdate,string enddate,decimal sfb08)
        {
            string sql = string.Empty;
            sql += "delete from schmast where sch01='" + sfb01 + "'";
            Double a = DOSQL.Excute(Login.WU, sql);
            

            int rg = TimeSpan(begdate, enddate);
            for (int j = 0; j <= rg; j++)
            {
                Schmast p = new Schmast();
                p.Sch01 = sfb01;
                p.Sch02 = getSchdate(j, begdate);
                p.Sch03 = sfb08;
                sql += Schmast.Set_Insert(p);
            }
            if (sql.Length > 0)
            {
                Double j = DOSQL.Excute(Login.WU, sql);
                if (j <= 0) MessageBox.Show("新增失敗-Schmast");
            }
        }


       

        private void iPsch(DataTable dt)
        {
            string sql = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Procsch p_procsch = new Procsch();
                p_procsch.Ecm01 = dt.Rows[i]["ecm01"].ToString();
                p_procsch.Ecm03 = System.Convert.ToInt16(dt.Rows[i]["ecm03"].ToString());
                p_procsch.Ecm04 = dt.Rows[i]["ecm04"].ToString();
                p_procsch.Ecm45 = dt.Rows[i]["ecm45"].ToString();
                p_procsch.Ecm06 = dt.Rows[i]["ecm06"].ToString();
                p_procsch.Eca02 = dt.Rows[i]["eca02"].ToString();
                p_procsch.Begdate =(string)dt.Rows[i]["begdate"];
                p_procsch.Enddate =(string)dt.Rows[i]["enddate"];
                p_procsch.Rg = System.Convert.ToInt16(dt.Rows[i]["rg"].ToString());
                sql += Procsch.Set_Insert(p_procsch);
            }
            if (sql.Length > 0)
            {
                Double i = DOSQL.Excute(Login.WU, sql);
                if (i <= 0) MessageBox.Show("新增失敗-Procsch2");
            }
        }

        private void iPscm(DataTable dt)
        {
            string sql = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int DRG = System.Convert.ToInt16(dt.Rows[i]["rg"].ToString());//間距
                for (int j = 0; j <= DRG; j++)
                {
                    Procscm p_ = new Procscm();
                    p_.Sfb01 = dt.Rows[i]["ecm01"].ToString();
                    p_.Ecb03 = System.Convert.ToInt16(dt.Rows[i]["ecm03"].ToString());
                    p_.Schdate = getSchdate(j, dt.Rows[i]["begdate"].ToString());
                    p_.Ecm04 = dt.Rows[i]["ecm04"].ToString();
                    p_.Ecm45 = dt.Rows[i]["ecm45"].ToString();
                    p_.Ecm06 = dt.Rows[i]["ecm06"].ToString();
                    p_.Eca02 = dt.Rows[i]["eca02"].ToString();
                    sql += Procscm.Set_Insert(p_);
                }
            }
            if (sql.Length > 0)
            {
                Double i = DOSQL.Excute(Login.WU, sql);
                if (i <= 0) MessageBox.Show("新增失敗-Procscm2");
            }
        }


        private DataTable btcTrn_1(DataTable dt,string begdate, string enddate)
        {
            //--定義datatable 欄位
            DataTable dtProcsch = new DataTable();
            dtProcsch.Columns.Add("ecm01", typeof(String));
            dtProcsch.Columns.Add("ecm03", typeof(String));
            dtProcsch.Columns.Add("ecm04", typeof(String));
            dtProcsch.Columns.Add("ecm45", typeof(String));
            dtProcsch.Columns.Add("ecm06", typeof(String));
            dtProcsch.Columns.Add("eca02", typeof(String));
            dtProcsch.Columns.Add("begdate", typeof(String));
            dtProcsch.Columns.Add("enddate", typeof(String));
            dtProcsch.Columns.Add("rg", typeof(String));
            //--定義datatable 欄位
            int lastrow = dt.Rows.Count - 1;//最後一筆
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Bdate = string.Empty;
                string Edate = string.Empty;
                string rg = "0";
                if (i == lastrow)
                {
                    Bdate = begdate;//預計生產日
                    Edate = enddate;//客戶交期
                }
                else
                {
                    Bdate = begdate;//預計生產日
                    Edate = begdate;//客戶交期
                }
                rg = TimeSpan(Bdate, Edate).ToString();

                DataRow dr = dtProcsch.NewRow();
                dr["ecm01"] = dt.Rows[i]["ecm01"].ToString();
                dr["ecm03"] = dt.Rows[i]["ecm03"].ToString();
                dr["ecm04"] = dt.Rows[i]["ecm04"].ToString();
                dr["ecm45"] = dt.Rows[i]["ecm45"].ToString();
                dr["ecm06"] = dt.Rows[i]["ecm06"].ToString();
                dr["eca02"] = dt.Rows[i]["eca02"].ToString();
                dr["begdate"] = Bdate;
                dr["enddate"] = Edate;
                dr["rg"] = rg;
                dtProcsch.Rows.Add(dr);
            }
            return dtProcsch;
        }



        private void HeadtlShow()
        {
            dataGridView2.Rows.Clear();
            string sql = string.Empty;
            sql += "SELECT ecm03,ecm04,ecm45,ecm06,eca02 from ew.ecm_file";
            sql += " LEFT OUTER JOIN ew.eca_file on eca_file.eca01 = ecm06";
            sql += " where 1=1";
            sql += " and ecm01='" + f_sfb01.Text + "'";
            sql += " ORDER BY ecm01,ecm03";
            DataTable dt = DOORC.GetDataTable(Login.TT, sql);

            string begdate = string.Empty;
            string enddate = string.Empty;
            string rg = "0";

            int lastrow = dt.Rows.Count - 1;//最後一筆
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == lastrow)
                {
                    begdate = f_sfb13.Text;//預計生產日
                    enddate = f_sfb15.Text;//客戶交期
                }
                else
                {
                    begdate = f_sfb13.Text;//預計生產日
                    enddate = f_sfb13.Text;//客戶交期
                }
                rg = TimeSpan(begdate, enddate).ToString();
                dataGridView2.Rows.Add(new object[] {
                    dt.Rows[i]["ecm03"],
                    dt.Rows[i]["ecm04"],
                    dt.Rows[i]["ecm45"],
                    dt.Rows[i]["ecm06"],
                    dt.Rows[i]["eca02"],
                    begdate,
                rg,enddate});
            }
        }


        //private void HeadtlShow()
        //{
        //    dataGridView2.Rows.Clear();
        //    string sql = string.Empty;
        //    sql += "SELECT ecm03,ecm04,ecm45,ecm06,eca02 from ecm_file";
        //    sql += " LEFT OUTER JOIN eca_file on eca_file.eca01 = ecm06";
        //    sql += " where 1=1";
        //    sql += " and ecm01='" + f_sfb01.Text + "'";
        //    sql += " ORDER BY ecm01,ecm03";
        //    DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
            
        //    string begdate = string.Empty;
        //    string enddate = string.Empty;
        //    string rg = "0";

        //    int lastrow = dt.Rows.Count - 1;//最後一筆
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        if (i == lastrow)
        //        {
        //            begdate = f_sfb13.Text;//預計生產日
        //            enddate = f_sfb15.Text;//客戶交期
        //        }
        //        else
        //        {
        //            begdate = f_sfb13.Text;//預計生產日
        //            enddate = f_sfb13.Text;//客戶交期
        //        }
        //        rg = TimeSpan(begdate, enddate).ToString();
        //        dataGridView2.Rows.Add(new object[] {
        //            dt.Rows[i]["ecm03"],
        //            dt.Rows[i]["ecm04"],
        //            dt.Rows[i]["ecm45"],
        //            dt.Rows[i]["ecm06"],
        //            dt.Rows[i]["eca02"],
        //            begdate,
        //        rg,enddate});
        //    }
        //}

        

        private void HeadCls()
        {
            f_sfb01.Text = string.Empty;
            f_sfb22.Text = string.Empty;
            f_sfb221.Text = string.Empty;
            f_occ02.Text = string.Empty;
            f_ima01.Text = string.Empty;
            f_ima02.Text = string.Empty;
            f_ima021.Text = string.Empty;
            f_sfb06.Text = string.Empty;
            f_sfb08.Text = string.Empty;
            f_sfb13.Text = string.Empty;
            f_sfb15.Text = string.Empty;
            f_sfb223.Text = string.Empty;
            f_sfb224.Text = string.Empty;
            
        }

        private void RecodeCls()
        {
            f_r_ecm03.Text = string.Empty;
            f_r_ecd01.Text = string.Empty;
            f_r_ecd02.Text = string.Empty;
            f_r_ecd07.Text = string.Empty;
            f_r_eca02.Text = string.Empty;
            f_r_begdate.Text = string.Empty;
            f_r_rg.Text = string.Empty;
            f_r_enddate.Text = string.Empty;
        }

        

        private bool f_Check()
        {
            if (f_sfb01.Text.Length == 0)
                return false;
            else
                return true;
        }
        private void f_Insert()
        {
            Ins_Procsca();//主檔
            Ins_Procsch();//明細
            Ins_Procscm();//排程
            Ins_Sfb("1");
        }

        private void f_InsertDay()
        {
            Ins_ProcscaDay();//主檔
            Ins_ProcschDay();//明細
            Ins_ProcscmDay();//排程
            Upd_Sfb("2");
        }

        private void Upd_Sfb(string status)
        {
            int i = sql_sfb_file.UpdateStatus(f_sfb01.Text, status, Login.WU);
            if (i <= 0) MessageBox.Show("修改失敗-sfb_file.status");
        }
        private void Upd_Sfb(string sfb01, string status)
        {
            int i = sql_sfb_file.UpdateStatus(sfb01, status, Login.WU);
            if (i <= 0) MessageBox.Show("修改失敗-sfb_file.status");
        }

        private void Ins_Sfb(string Status)
        {
            string sql = string.Empty;
            sql_sfb_file p_sfb = new sql_sfb_file();

            p_sfb.Sfb01 = f_sfb01.Text;
            p_sfb.Sfb22 = f_sfb22.Text;
            p_sfb.Sfb221 = f_sfb221.Text;
            p_sfb.Sfb02 = "";           
            p_sfb.Sfb05 = f_ima01.Text;
            p_sfb.Sfb06 = f_sfb06.Text;
            p_sfb.Sfb08 = System.Convert.ToDecimal(f_sfb08.Text); 
            p_sfb.Sfb13 = f_sfb13.Text;
            p_sfb.Sfb81 = "";
            p_sfb.Sfb223 = f_sfb223.Text;
            p_sfb.Sfb224 = f_sfb224.Text;
            p_sfb.Sfb15 = f_sfb15.Text;

            p_sfb.Occ02 = f_occ02.Text;
            p_sfb.Ima01 = f_ima01.Text;
            p_sfb.Ima02 = f_ima02.Text;
            p_sfb.Ima021 = f_ima021.Text;

            p_sfb.Status = Status;

            p_sfb.Sfb13m = f_sfb13.Text;
            p_sfb.Sfb15m = f_sfb15.Text;
            p_sfb.Sfb13d = f_sfb13.Text;
            p_sfb.Sfb15d = f_sfb15.Text;
            p_sfb.Sfb13w = f_sfb13.Text;
            p_sfb.Sfb15w = f_sfb15.Text;

            sql_sfb_file.Ins_(p_sfb, Login.WU);
        }


        private void Ins_Procsca()
        {
            string sql = string.Empty;
            Procsca p_procsca = new Procsca();
            p_procsca.Sfb01 = f_sfb01.Text;
            p_procsca.Occ02 = f_occ02.Text;
            p_procsca.Ima01 = f_ima01.Text;
            p_procsca.Ima02 = f_ima02.Text;
            p_procsca.Ima021 = f_ima021.Text;
            p_procsca.Sfb06 = f_sfb06.Text;
            p_procsca.Sfb08 = System.Convert.ToDecimal(f_sfb08.Text);
            p_procsca.Sfb13 = f_sfb13.Text;
            p_procsca.Sfb15 = f_sfb15.Text;

            int i = Procsca.Ins_(p_procsca, Login.WU);
            if (i <= 0) MessageBox.Show("新增失敗-Procsca");
        }

        private void Ins_ProcscaDay()
        {
            string sql = string.Empty;
            ProcscaDay p_procscaday = new ProcscaDay();
            p_procscaday.Sfb01 = f_sfb01.Text;
            p_procscaday.Occ02 = f_occ02.Text;
            p_procscaday.Ima01 = f_ima01.Text;
            p_procscaday.Ima02 = f_ima02.Text;
            p_procscaday.Ima021 = f_ima021.Text;
            p_procscaday.Sfb06 = f_sfb06.Text;
            p_procscaday.Sfb08 = System.Convert.ToDecimal(f_sfb08.Text);
            p_procscaday.Sfb13 = f_sfb13.Text;
            p_procscaday.Sfb15 = f_sfb15.Text;

            int i = ProcscaDay.Ins_(p_procscaday, Login.WU);
            if (i <= 0) MessageBox.Show("新增失敗-ProcscaDay");
        }

        private void Ins_ProcscaDay(string sfb01)
        {
            string sql_1 = "select * from procsca where sfb01='" + sfb01 + "'";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql_1);
            if (dt.Rows.Count > 0)
            {
                string sql = string.Empty;
                ProcscaDay p_procscaday = new ProcscaDay();
                p_procscaday.Sfb01 = dt.Rows[0]["sfb01"].ToString();
                p_procscaday.Occ02 = dt.Rows[0]["occ02"].ToString();
                p_procscaday.Ima01 = dt.Rows[0]["ima01"].ToString();
                p_procscaday.Ima02 = dt.Rows[0]["ima02"].ToString();
                p_procscaday.Ima021 = dt.Rows[0]["ima021"].ToString();
                p_procscaday.Sfb06 = dt.Rows[0]["sfb06"].ToString();
                p_procscaday.Sfb08 = System.Convert.ToDecimal(dt.Rows[0]["sfb08"].ToString());
                p_procscaday.Sfb13 = dt.Rows[0]["sfb13"].ToString();
                p_procscaday.Sfb15 = dt.Rows[0]["sfb15"].ToString();

                int i = ProcscaDay.Ins_(p_procscaday, Login.WU);
                if (i <= 0) MessageBox.Show("新增失敗-ProcscaDay");
            }            
        }

        private void Ins_Procsch()
        {
            string sql = string.Empty;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                Procsch p_procsch = new Procsch();
                p_procsch.Ecm01 = f_sfb01.Text;
                p_procsch.Ecm03 = System.Convert.ToInt16(dataGridView2.Rows[i].Cells["ecm03"].Value.ToString());
                p_procsch.Ecm04 = dataGridView2.Rows[i].Cells["ecm04"].Value.ToString();
                p_procsch.Ecm45 = dataGridView2.Rows[i].Cells["ecm45"].Value.ToString();
                p_procsch.Ecm06 = dataGridView2.Rows[i].Cells["ecm06"].Value.ToString();
                p_procsch.Eca02 = dataGridView2.Rows[i].Cells["eca02"].Value.ToString();
                p_procsch.Begdate = dataGridView2.Rows[i].Cells["begdate"].Value.ToString();
                p_procsch.Enddate = dataGridView2.Rows[i].Cells["enddate"].Value.ToString();
                p_procsch.Rg = System.Convert.ToInt16(dataGridView2.Rows[i].Cells["rg"].Value.ToString());
                sql += Procsch.Set_Insert(p_procsch);
            }
            if (sql.Length > 0)
            {
                Double i = DOSQL.Excute(Login.WU, sql);
                if (i <= 0) MessageBox.Show("新增失敗-Procsch");
            }
        }               


        private void Ins_Procscm()
        {
            string sql = string.Empty;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                int DRG = System.Convert.ToInt16(dataGridView2.Rows[i].Cells["rg"].Value.ToString());//間距
                for (int j = 0; j <= DRG; j++)
                {
                    Procscm p_ = new Procscm();
                    p_.Sfb01 = f_sfb01.Text;
                    p_.Ecb03 = System.Convert.ToInt16(dataGridView2.Rows[i].Cells["ecm03"].Value.ToString());
                    p_.Schdate = getSchdate(j, dataGridView2.Rows[i].Cells["begdate"].Value.ToString());
                    p_.Ecm04 = dataGridView2.Rows[i].Cells["ecm04"].Value.ToString();
                    p_.Ecm45 = dataGridView2.Rows[i].Cells["ecm45"].Value.ToString();
                    p_.Ecm06 = dataGridView2.Rows[i].Cells["ecm06"].Value.ToString();
                    p_.Eca02 = dataGridView2.Rows[i].Cells["eca02"].Value.ToString();
                    sql += Procscm.Set_Insert(p_);
                }
            }
            if (sql.Length > 0)
            {
                Double i = DOSQL.Excute(Login.WU, sql);
                if (i <= 0) MessageBox.Show("新增失敗-Procscm");
            }
        }

        private void isDecimal(object sender, KeyPressEventArgs e)
        {
            if ((byte)e.KeyChar != 46 &&
                (byte)e.KeyChar != 49 &&
                (byte)e.KeyChar != 50 &&
                (byte)e.KeyChar != 51 &&
                (byte)e.KeyChar != 52 &&
                (byte)e.KeyChar != 53 &&
                (byte)e.KeyChar != 54 &&
                (byte)e.KeyChar != 55 &&
                (byte)e.KeyChar != 56 &&
                (byte)e.KeyChar != 57 &&
                (byte)e.KeyChar != 48 &&
                (byte)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        //private void btn_TrnDay_Click(object sender, EventArgs e)
        //{
        //    if (f_sfb01.Text.Trim().Length == 0)
        //    {
        //        MessageBox.Show("請先選擇工單");
        //        return;
        //    }
        //    if (Procsca.GetTable(f_sfb01.Text).Rows.Count == 0)
        //    {
        //        MessageBox.Show("主排程無資料請先轉主排");
        //        return;
        //    }
        //    if (Config.Message("是否轉日排程") == "Y")
        //    {
        //        if (ProcscaDay.GetTable(f_sfb01.Text).Rows.Count == 0)//--日排程如沒資料寫入
        //        {
        //            f_InsertDay();
        //            clsForm();//清畫面                
        //        }
        //    }
        //    find_d();//找已排入主排資料
        //}

        private void btn_TrnDay_Click(object sender, EventArgs e)
        {
            btnRtn("D");                
        }

        //private void btn_Back_Click(object sender, EventArgs e)
        //{
        //    if (f_sfb01.Text.Trim().Length == 0)
        //    {
        //        MessageBox.Show("請先選擇工單");
        //        return;
        //    }

        //    DataTable dt = sql_sfb_file.HaveRow(f_sfb01.Text, Login.WU);
        //    if (dt.Rows.Count > 0)
        //    {
        //        if (Config.Message("是否回轉TT") == "Y")
        //        {
        //            sql_sfb_file.Del_(f_sfb01.Text, Login.WU);
        //            Procsca.Del_(f_sfb01.Text, Login.WU);
        //            Procsch.Del_(f_sfb01.Text, Login.WU);
        //            Procscm.Del_(f_sfb01.Text, Login.WU);
        //        }
        //    }
        //    clsForm();//清畫面 
        //    find_d();//找已排入主排資料
        //}

        private void btn_Back_Click(object sender, EventArgs e)
        {
            btnRtn("T");            
        }
        

        private void btnRtn(string rSel)
        {   
            string sfb01 = string.Empty;
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("請先選擇工單");
                return;
            }
            else
            {
                //轉TT
                if (rSel == "T")
                {
                    if (Config.Message("是否回轉TT") == "Y")
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            bool isSelected = Convert.ToBoolean(dataGridView1.Rows[i].Cells["ck"].Value);
                            if (isSelected)
                            {
                                sfb01 = dataGridView1.Rows[i].Cells["sfb01"].Value.ToString();
                                rtnTT(sfb01);
                            }
                        }
                    }
                }

                //轉日排
                if (rSel == "D")
                {
                    if (Config.Message("是否轉日排程") == "Y")
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            bool isSelected = Convert.ToBoolean(dataGridView1.Rows[i].Cells["ck"].Value);
                            if (isSelected)
                            {
                                sfb01 = dataGridView1.Rows[i].Cells["sfb01"].Value.ToString();
                                trnDay(sfb01);
                            }
                        }
                    }
                }
                clsForm();//清畫面 
                find_d();//找已排入主排資料
            }
        }

        private void rtnTT(string sfb01)
        {
            sql_sfb_file.Del_(sfb01, Login.WU);
            Procsca.Del_(sfb01, Login.WU);
            Procsch.Del_(sfb01, Login.WU);
            Procscm.Del_(sfb01, Login.WU);
            Schmast.Del_(sfb01, Login.WU);
        }

        //private void rtnTT(string sfb01)
        //{
        //    DataTable dt = sql_sfb_file.HaveRow(sfb01, Login.WU);
        //    if (dt.Rows.Count > 0)
        //    {
        //        sql_sfb_file.Del_(sfb01, Login.WU);
        //        Procsca.Del_(sfb01, Login.WU);
        //        Procsch.Del_(sfb01, Login.WU);
        //        Procscm.Del_(sfb01, Login.WU);
        //    }
        //}

        private void trnDay(string sfb01)
        {
            Ins_ProcscaDay(sfb01);//主檔
            Ins_ProcschDay(sfb01);//明細
            Ins_ProcscmDay(sfb01);//排程
            Upd_Sfb(sfb01, "2");
        }


        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn newColumn = dataGridView1.Columns[e.ColumnIndex];            
            string btText = newColumn.HeaderText;
            switch (btText)
            {
                case "全選":
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Cells["ck"].Value = true;
                    }
                    newColumn.HeaderText = "選取";
                    break;

                case "選取":
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Cells["ck"].Value = false;
                    }
                    newColumn.HeaderText = "全選";
                    break;
                default:
                    break;
            }
        }
    }
}
