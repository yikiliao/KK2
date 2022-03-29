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
    public partial class mfd007 : Form
    {
        string rSfb01;
        string Menu_Sel;
        string FlowStation;//流程站
        public mfd007()
        {
            InitializeComponent();
            Config.SetForm(this);
            //Config.SetFormSize(this, "F");
            //Config.SetPer(this);
            //this.StartPosition = FormStartPosition.CenterScreen;
            init_GridView();
        }

        private void init_GridView()
        {
            //是否允許使用者編輯
            //this.dataGridView1.ReadOnly = true;
            //是否允許使用者自行新增
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
            find_d();
        }        

        private void find_d()
        {            
            DataTable dt = sql_sfb_file.FindList(Login.DeptOne, string.Empty, "2");//入日排
            dataGridView1.Rows.Clear();

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
        //    string sql = string.Empty;
        //    sql += "select procscaday.sfb01,procscaday.occ02,procscaday.ima01,procscaday.ima02,procscaday.ima021,procscaday.sfb06,procscaday.sfb08,procscaday.sfb13 from procscaday";
        //    sql += " LEFT OUTER JOIN sfb_file on sfb_file.sfb01 = procscaday.sfb01";
        //    sql += " where 1=1";
        //    sql += " and SUBSTRING(procscaday.sfb01,1,1)='" + Login.DeptOne + "'";
        //    sql += " and sfb_file.status <> 'X'"; //沒結案的
        //    DataTable dt = DOSQL.GetDataTable(Login.WU, sql);

        //    dataGridView1.Rows.Clear();
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        dataGridView1.Rows.Add(new object[] {
        //            dt.Rows[i]["sfb01"],
        //            dt.Rows[i]["occ02"],
        //            dt.Rows[i]["sfb08"]});
        //    }
        //}

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex < 0) || (e.ColumnIndex < 0)) return;
            dataGridView1.Rows[e.RowIndex].Selected = true;
            isCheck(e.RowIndex);//點選勾選
            rSfb01 = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();//工單號
            f_Data();//找資料
            GvSort();//排製程序
            RecodeCls();//清除單列資料
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
            DataTable dt = sql_sfb_file.FindList(string.Empty, rSfb01, "2");//入日排
            HeadShow(dt);
            PschShow();
        }

        //private void f_Data()
        //{
        //    DataTable dt = ProcscaDay.GetTable(rSfb01);           
        //    HeadShow(dt);
        //    PschShow();//
        //}
        private void HeadShow(DataTable dt)
        {
            f_sfb01.Text = dt.Rows[0]["sfb01"].ToString();
            f_occ02.Text = dt.Rows[0]["occ02"].ToString();
            f_ima01.Text = dt.Rows[0]["ima01"].ToString();
            f_ima02.Text = dt.Rows[0]["ima02"].ToString();
            f_ima021.Text = dt.Rows[0]["ima021"].ToString();
            f_sfb06.Text = dt.Rows[0]["sfb06"].ToString();
            f_sfb08.Text = dt.Rows[0]["sfb08"].ToString();
            f_sfb13.Text = dt.Rows[0]["sfb13d"].ToString();
            f_sfb15.Text = dt.Rows[0]["sfb15d"].ToString();

            f_sfb223.Text = dt.Rows[0]["sfb223"].ToString();
            f_sfb224.Text = dt.Rows[0]["sfb224"].ToString();
            f_sfb22.Text = dt.Rows[0]["sfb22"].ToString();
            f_sfb221.Text = dt.Rows[0]["sfb221"].ToString();
        }

        private void PschShow()
        {
            string sql = string.Empty;
            sql += "SELECT ecm03,ecm04,ecm45,ecm06,eca02,begdate,rg,enddate from procschday";
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
        //    DataTable dt = ProcscmDay.SumScm(f_r_begdate.Text, f_r_ecd01.Text);
        //    dataGridView3.DataSource = dt;
        //}

        private void ShowGv3()
        {
            DataTable dt = ProcscmDay.SumScm(f_r_begdate.Text, f_r_ecd01.Text);
            dataGridView3.Rows.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView3.Rows.Add(new object[] {
                    dt.Rows[i]["sfb01"],
                    dt.Rows[i]["ecm04"],
                    Convert.ToInt16(dt.Rows[i]["sfb08"])});
            }

            //dataGridView3.DataSource = dt;
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
            //f_r_begdate_s.Value = DateTime.Now;
            //f_r_begdate.Text = f_r_begdate_s.Value.ToString("yyyy/MM/dd");
            //f_r_enddate_s.Value = DateTime.Now;
            //f_r_enddate.Text = f_r_begdate_s.Value.ToString("yyyy/MM/dd");
            //f_r_rg.Text = TimeSpan(f_r_begdate.Text, f_r_enddate.Text).ToString();
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
            if (f_r_ecm03.Text.Length == 0) return;
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
            if (f_RecordCK() == false)
            {
                MessageBox.Show("欄位不可空白");
                return;
            }
            //檢查製程序編號
            if (Ecm03Doub() == true)
            {
                rowUpd();
            }
            else
            {   
                rowAdd();
            }            
        }

        private void rowUpd()
        {            
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
                    dataGridView2.Rows[i].Cells["ecm04"].Value = f_r_ecd01.Text; //作業編號
                    dataGridView2.Rows[i].Cells["ecm45"].Value = f_r_ecd02.Text; //作業名稱
                    dataGridView2.Rows[i].Cells["ecm06"].Value = f_r_ecd07.Text; //工作站
                    dataGridView2.Rows[i].Cells["eca02"].Value = f_r_eca02.Text; //工作站名稱
                }
                if (a_ecm03 < r_ecm03)
                {
                    string td = dataGridView2.Rows[i-1].Cells["enddate"].Value.ToString(); // 上一筆的完工日
                    dataGridView2.Rows[i].Cells["begdate"].Value = td;                    
                    dataGridView2.Rows[i].Cells["enddate"].Value = rEndDate(td, r_rg);
                    dataGridView2.Rows[i].Cells["rg"].Value = TimeSpan(dataGridView2.Rows[i].Cells["begdate"].Value.ToString(), dataGridView2.Rows[i].Cells["enddate"].Value.ToString()).ToString();                    
                }
            }
        }

        private string rEndDate(string d,Int16 r)
        {
            DateTime a = new DateTime();
            DateTime b = new DateTime();
            a = System.Convert.ToDateTime(d);
            b = a.AddDays(r);           
            return b.ToString("yyyy/MM/dd");
        }


        //private void rowUpd()
        //{
        //    Int16 a_ecm03 = System.Convert.ToInt16(f_r_ecm03.Text); //製程序
        //    for (int i = 0; i < dataGridView2.Rows.Count; i++)
        //    {
        //        Int16 r_ecm03 = System.Convert.ToInt16(dataGridView2.Rows[i].Cells["ecm03"].Value.ToString());//明細製程序
        //        if (a_ecm03 == r_ecm03)
        //        {
        //            dataGridView2.Rows[i].Cells["begdate"].Value = f_r_begdate.Text;
        //            dataGridView2.Rows[i].Cells["enddate"].Value = f_r_enddate.Text;
        //            dataGridView2.Rows[i].Cells["rg"].Value = TimeSpan(f_r_begdate.Text, f_r_enddate.Text).ToString();
        //        }
        //        if (a_ecm03 < r_ecm03)
        //        {
        //            dataGridView2.Rows[i].Cells["begdate"].Value = f_r_enddate.Text;
        //            dataGridView2.Rows[i].Cells["enddate"].Value = f_r_enddate.Text;
        //            dataGridView2.Rows[i].Cells["rg"].Value = TimeSpan(dataGridView2.Rows[i].Cells["begdate"].Value.ToString(), dataGridView2.Rows[i].Cells["enddate"].Value.ToString()).ToString();
        //        }
        //    }
        //}

        private void rowAdd()
        {
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

        //private void f_OK_Click(object sender, EventArgs e)
        //{
        //    if (f_Check() == false) { MessageBox.Show("請輸入資料"); return; }
        //    if (Config.Message("確定修改?") == "Y")
        //    {
        //        try
        //        {
        //            f_Update();
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



        private void f_OK_Click(object sender, EventArgs e)
        {
            if (f_Check() == false) { MessageBox.Show("請輸入資料"); return; }
            if (Config.Message("確定修改?") == "Y")
            {
                try
                {
                    string Sfb01 = f_sfb01.Text;
                    f_Update(Sfb01);
                    Config.Show("修改成功");
                    if (Config.Message("是否批次修改相同製程的" +
                        "\r\n排程起訖日期?") == "Y")
                    {
                        //開窗查找
                        openWind(Sfb01);                        
                    }
                    else
                    {
                        //do nothing
                    }

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

        private void openWind(string Sfb01)
        {
            rDayWKno fm = new rDayWKno();
            fm.dept = Login.DeptOne;
            fm.rSfb01 = Sfb01;//工單
            fm.FlowStation = FlowStation;//流程站
            if (fm.ShowDialog() == DialogResult.OK)
            {
                if (fm.dtProd.Rows.Count > 0)
                {
                    //接收回傳資料
                    for (int i = 0; i < fm.dtProd.Rows.Count; i++)
                    {
                        string sfb01 = fm.dtProd.Rows[i]["sfb01"].ToString();
                        f_Update(sfb01);//修改
                    }
                    Config.Show("修改成功");
                }
                else
                {
                    Config.Show("資料未修改");
                }
            }
        }


        private void PichUpdate(string FlowStation)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                bool isSelected = Convert.ToBoolean(dataGridView1.Rows[i].Cells["ck"].Value);
                if (isSelected)
                {
                    string sfb01 = dataGridView1.Rows[i].Cells["sfb01"].Value.ToString();
                    if (FlowStation == CheckStation(sfb01))
                    {
                        f_Update(sfb01);//流程站相同修改
                    }
                }
            }
            Config.Show("批次修改成功");
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



        private bool f_Check()
        {
            if (f_sfb01.Text.Length == 0)
                return false;
            else
                return true;
        }

        private void f_Update(string Sfb01)
        {
            //刪除明細
            Del_ProcschDay(Sfb01);
            Ins_ProcschDay(Sfb01); //加入明細
            //------
            Del_ProcscmDay(Sfb01);//刪除排程
            Ins_ProcscmDay(Sfb01);//排程      

            //--
            Upd_ProcscaDay(Sfb01);
        }



        private void Upd_ProcscaDay(string Sfb01)
        {
            //抓第一站開工日
            string sql = string.Empty;
            sql += "SELECT top 1 begdate from procschday WHERE 1=1 ";
            sql += " and ecm01 = '" + Sfb01 + "'";
            sql += " ORDER BY ecm03";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
            string bdate = dt.Rows[0][0].ToString();
            //抓最後一站開工日
            sql = string.Empty;
            sql += "SELECT top 1 enddate from procschday WHERE 1=1 ";
            sql += " and ecm01 = '" + Sfb01 + "'";
            sql += " ORDER BY ecm03 desc";
            DataTable dt1 = DOSQL.GetDataTable(Login.WU, sql);
            string edate = dt1.Rows[0][0].ToString();

            sql = string.Empty;
            sql = string.Format("update procscaday set sfb13='{0}', sfb15='{1}' where sfb01='{2}'", bdate, edate, Sfb01);
            int i = DOSQL.Excute(Login.WU, sql);
            if (i <= 0) MessageBox.Show("修改失敗-Procscaday");

            //update sfb_file 主排程開始日期及完工日期
            sql = string.Empty;
            sql = string.Format("update sfb_file set sfb13d='{0}', sfb15d='{1}', sfb13w='{2}', sfb15w='{3}' where sfb01='{4}'", bdate, edate, bdate, edate, Sfb01);
            int j = DOSQL.Excute(Login.WU, sql);
            if (j <= 0) MessageBox.Show("修改失敗-sfb_file");
        }

        private void Del_ProcschDay(string Sfb01)
        {            
            string sql = string.Empty;
            sql += "delete from procschday where ecm01 ='" + Sfb01 + "'";
            int i = DOSQL.Excute(Login.WU, sql);
        }

        private void Ins_ProcschDay(string Sfb01)
        {
            string sql = string.Empty;
            FlowStation = string.Empty;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                ProcschDay p_ = new ProcschDay();
                p_.Ecm01 = Sfb01;
                p_.Ecm03 = System.Convert.ToInt16(dataGridView2.Rows[i].Cells["ecm03"].Value.ToString());
                p_.Ecm04 = dataGridView2.Rows[i].Cells["ecm04"].Value.ToString();
                p_.Ecm45 = dataGridView2.Rows[i].Cells["ecm45"].Value.ToString();
                p_.Ecm06 = dataGridView2.Rows[i].Cells["ecm06"].Value.ToString();
                p_.Eca02 = dataGridView2.Rows[i].Cells["eca02"].Value.ToString();
                p_.Begdate = dataGridView2.Rows[i].Cells["begdate"].Value.ToString();
                p_.Enddate = dataGridView2.Rows[i].Cells["enddate"].Value.ToString();
                p_.Rg = System.Convert.ToInt16(dataGridView2.Rows[i].Cells["rg"].Value.ToString());
                sql += ProcschDay.Set_Insert(p_);
                FlowStation += FlowS(p_.Ecm03, p_.Ecm04, p_.Ecm06);//紀錄修改的流程站，比對批次修改時流程站是否相同
            }
            if (sql.Length > 0)
            {
                Double i = DOSQL.Excute(Login.WU, sql);
                if (i <= 0) MessageBox.Show("新增失敗-ProcschDay");
            }
        }

        private void Del_ProcscmDay(string Sfb01)
        {            
            string sql = string.Empty;
            sql += "delete from procscmday where sfb01 ='" + Sfb01 + "'";
            int i = DOSQL.Excute(Login.WU, sql);
        }

        private void Ins_ProcscmDay(string Sfb01)
        {
            string sql = string.Empty;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                int DRG = System.Convert.ToInt16(dataGridView2.Rows[i].Cells["rg"].Value.ToString());//間距
                for (int j = 0; j <= DRG; j++)
                {
                    ProcscmDay p_ = new ProcscmDay();
                    p_.Sfb01 = Sfb01;
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

        private string getSchdate(int j, string rd)
        {
            return System.Convert.ToDateTime(rd).AddDays(j).ToString("yyyy/MM/dd");
        }

        private void btn_unOK_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void btn_TrnWk_Click(object sender, EventArgs e)
        {
            btnRtn("W");
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
                if (rSel == "W")
                {
                    if (Config.Message("是否轉派工生產") == "Y")
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            bool isSelected = Convert.ToBoolean(dataGridView1.Rows[i].Cells["ck"].Value);
                            if (isSelected)
                            {
                                sfb01 = dataGridView1.Rows[i].Cells["sfb01"].Value.ToString();
                                trnWork(sfb01);
                            }
                        }
                    }
                }

                if (rSel == "M")
                {
                    if (Config.Message("是否轉回主排") == "Y")
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            bool isSelected = Convert.ToBoolean(dataGridView1.Rows[i].Cells["ck"].Value);
                            if (isSelected)
                            {
                                sfb01 = dataGridView1.Rows[i].Cells["sfb01"].Value.ToString();
                                trnMPS(sfb01);
                            }
                        }
                    }
                }
                clsForm();//清畫面
                find_d();//找已排入日排資料
            }
        }

        private void trnWork(string sfb01)
        {
            Ins_ProcscaWork(sfb01);//主檔
            Ins_ProcschWork(sfb01);//明細
            Ins_ProcscmWork(sfb01);//排程
            Upd_Sfb(sfb01, "3");
        }

        private void trnMPS(string sfb01)
        {            
            Upd_Sfb(sfb01, "1");
            ProcscaDay.Del_(sfb01, Login.WU);
            ProcschDay.Del_(sfb01, Login.WU);
            ProcscmDay.Del_(sfb01, Login.WU);
        }



        //private void btn_TrnWk_Click(object sender, EventArgs e)
        //{
        //    if (f_sfb01.Text.Trim().Length == 0)
        //    {
        //        MessageBox.Show("請先選擇工單");
        //        return;
        //    }
        //    if (ProcscaDay.GetTable(f_sfb01.Text).Rows.Count == 0)
        //    {
        //        MessageBox.Show("日排程無資料請先轉日排");
        //        return;
        //    }
        //    if (Config.Message("是否轉派工?") == "Y")
        //    {
        //        Ins_ProcscaWork(f_sfb01.Text);//主檔
        //        Ins_ProcschWork(f_sfb01.Text);//明細
        //        Ins_ProcscmWork(f_sfb01.Text);//排程
        //       Upd_Sfb("3");
        //        clsForm();//清畫面
        //    }
        //    find_d();//找已排入日排資料
        //}

        private void Ins_ProcscaWork(string sfb01)
        {
            string sql_1 = "select * from procscaday where sfb01='" + sfb01 + "'";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql_1);
            if (dt.Rows.Count > 0)
            {
                string sql = string.Empty;
                ProcscaWork p_procscawork = new ProcscaWork();
                p_procscawork.Sfb01 = dt.Rows[0]["sfb01"].ToString();
                p_procscawork.Occ02 = dt.Rows[0]["occ02"].ToString();
                p_procscawork.Ima01 = dt.Rows[0]["ima01"].ToString();
                p_procscawork.Ima02 = dt.Rows[0]["ima02"].ToString();
                p_procscawork.Ima021 = dt.Rows[0]["ima021"].ToString();
                p_procscawork.Sfb06 = dt.Rows[0]["sfb06"].ToString();
                p_procscawork.Sfb08 = System.Convert.ToDecimal(dt.Rows[0]["sfb08"].ToString());
                p_procscawork.Sfb13 = dt.Rows[0]["sfb13"].ToString();
                p_procscawork.Sfb15 = dt.Rows[0]["sfb15"].ToString();

                int i = ProcscaWork.Ins_(p_procscawork, Login.WU);
                if (i <= 0) MessageBox.Show("新增失敗-ProcscaWork");
            }
        }



        private void Ins_ProcschWork(string sfb01)
        {
            string sql_1 = "select * from procschday where ecm01='" + sfb01 + "'";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql_1);
            string sql = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ProcschWork p_ = new ProcschWork();
                p_.Ecm01 = dt.Rows[i]["ecm01"].ToString();
                p_.Ecm03 = System.Convert.ToInt16(dt.Rows[i]["ecm03"].ToString());
                p_.Ecm04 = dt.Rows[i]["ecm04"].ToString();
                p_.Ecm45 = dt.Rows[i]["ecm45"].ToString();
                p_.Ecm06 = dt.Rows[i]["ecm06"].ToString();
                p_.Eca02 = dt.Rows[i]["eca02"].ToString();
                p_.Begdate = dt.Rows[i]["begdate"].ToString();
                p_.Enddate = dt.Rows[i]["enddate"].ToString();
                p_.Rg = System.Convert.ToInt16(dt.Rows[i]["rg"].ToString());
                sql += ProcschWork.Set_Insert(p_);
            }
            if (sql.Length > 0)
            {
                Double i = DOSQL.Excute(Login.WU, sql);
                if (i <= 0) MessageBox.Show("新增失敗-ProcschWork");
            }
        }


        private void Ins_ProcscmWork(string sfb01)
        {
            string sql_1 = "select * from procscmday where sfb01='" + sfb01 + "'";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql_1);
            string sql = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ProcscmWork p_ = new ProcscmWork();
                p_.Sfb01 = dt.Rows[i]["sfb01"].ToString();
                p_.Ecb03 = System.Convert.ToInt16(dt.Rows[i]["ecb03"].ToString());
                p_.Schdate = dt.Rows[i]["schdate"].ToString();
                p_.Ecm04 = dt.Rows[i]["ecm04"].ToString();
                p_.Ecm45 = dt.Rows[i]["ecm45"].ToString();
                p_.Ecm06 = dt.Rows[i]["ecm06"].ToString();
                p_.Eca02 = dt.Rows[i]["eca02"].ToString();
                sql += ProcscmWork.Set_Insert(p_);
            }
            if (sql.Length > 0)
            {
                Double i = DOSQL.Excute(Login.WU, sql);
                if (i <= 0) MessageBox.Show("新增失敗-ProcscmWork");
            }
        }


        private void Upd_Sfb(string sfb01, string status)
        {            
            int i = sfb_file.UpdateStatus(sfb01, status);
            if (i <= 0) MessageBox.Show("修改失敗-sfb_file.status");

            string sql = string.Empty;
            sql += "update sfb_file set sfb13w='" + f_sfb13.Text + "'";
            sql += " ,sfb15w='" + f_sfb15.Text + "'";
            sql += " where sfb01='" + sfb01 + "'";
            int a = DOSQL.Excute(Login.WU, sql);
            if (a <= 0) MessageBox.Show("修改失敗-sfb_file.sfb13w");
        }

        private void clsForm()
        {   
            HeadCls();
            RecodeCls();
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();            
            foreach (DataGridViewRow item in dataGridView3.Rows)
            {
                dataGridView3.Rows.RemoveAt(item.Index);
            }
        }

        private void HeadCls()
        {
            f_sfb01.Text = string.Empty;
            f_occ02.Text = string.Empty;
            f_ima01.Text = string.Empty;
            f_ima02.Text = string.Empty;
            f_ima021.Text = string.Empty;
            f_sfb06.Text = string.Empty;
            f_sfb08.Text = string.Empty;
            f_sfb13.Text = string.Empty;
            f_sfb15.Text = string.Empty;
            f_sfb22.Text = string.Empty;
            f_sfb221.Text = string.Empty;
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

        private void btn_Back_Click(object sender, EventArgs e)
        {
            btnRtn("M");
        }

        private void f_Insert()
        {
            string Sfb01 = f_sfb01.Text;
            //Ins_Procsca();//主檔
            //Ins_Procsch();//明細
            //Ins_Procscm();//排程
            Upd_Sfb(Sfb01,"1");
            ProcscaDay.Del_(Sfb01, Login.WU);
            ProcschDay.Del_(Sfb01, Login.WU);
            ProcscmDay.Del_(Sfb01, Login.WU);
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

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.RowIndex < 0)) return;
            dataGridView1.Rows[e.RowIndex].Selected = true;
            isCheck(e.RowIndex);//點選勾選
            rSfb01 = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();//工單號
            f_Data();//找資料
            GvSort();//排製程序
            RecodeCls();//清除單列資料
        }
    }
}
