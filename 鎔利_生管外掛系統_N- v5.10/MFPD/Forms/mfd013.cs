using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MFPD.Models;

namespace MFPD.Forms
{
    public partial class mfd013 : Form
    {
        string rSfb01;
        string rEcm03;//製程序
        string Menu_Sel;
        string FlowStation;//流程站
        
        public mfd013()
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
            //生產工單
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.RowHeadersWidth = 5;            
            this.dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.RowHeadersWidth = 5;
            this.dataGridView2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView3.AllowUserToResizeRows = false;
            this.dataGridView3.RowHeadersWidth = 5;
            this.dataGridView3.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView3.Columns["doc"].Visible = false;

            //生產統計
            this.dataGridView4.AllowUserToResizeRows = false;
            this.dataGridView4.RowHeadersWidth = 5;
            this.dataGridView4.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView4.Columns[3].Visible = false;
            this.dataGridView4.Columns[4].Visible = false;
            this.dataGridView4.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView4.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView4.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView4.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView4.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView4.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView4.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView4.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //不良統計
            this.dataGridView5.AllowUserToResizeRows = false;
            this.dataGridView5.RowHeadersWidth = 5;
            this.dataGridView5.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            
            this.dataGridView5.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;                      

            //圖型屬性設定
            setChartAtri();

            label22.Text = "灰色：已進入製程生產中";
            label22.BackColor = Color.LightGray;
            label23.Text = "綠色：已進入製程最後一站";
            label23.BackColor = Color.LightGreen;

            label24.Text = "達成率(F) = 良品數(C) / 標準產出(A)";
            label25.Text = "良品率(G) = 良品數(C) / 良品轉入(B)";
            label26.Text = "產出率(H) = 良品數(C)+報廢數(E) / 良品轉入(B)";
        }

        private void mfd004_Load(object sender, EventArgs e)
        {
            find_d();//找已轉派工資料出來顯示
            gColorTT();//已生產改變顏色
            //gColor();//已生產改變顏色
        }
        

        private void find_d()
        {
            DataTable dt = sql_sfb_file.FindList(Login.DeptOne, string.Empty, "3");//轉派工入日排
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


        private void gColor()
        {
            //如果MES有回饋過 淺灰
            //如果MES最後一站(已有回饋) 綠色
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string wkno = dataGridView1.Rows[i].Cells["sfb01"].Value.ToString();

                if (ReFinsh(wkno)) //生產最後一站有回饋
                {
                    //dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;//改變整列顏色
                    dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.LightGreen;
                    continue;
                }
                else
                {
                    if (DoWork(wkno)) //製程已回饋
                    {
                        //dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;//改變整列顏色
                        dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.LightGray;
                    }
                    else
                    {
                        //do nothing;
                    }
                }
            }
        }

        private void gColorTT()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string wkno = dataGridView1.Rows[i].Cells["sfb01"].Value.ToString();
                if (ReFinshTT(wkno)==true) //生產最後一站有回饋
                {
                    //dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;//改變整列顏色
                    dataGridView1.Rows[i].Cells[7].Style.BackColor = Color.LightGreen;
                    continue;
                }
                else
                {
                    if (ReDoshTT(wkno)) //還沒回饋
                    {   
                        continue;
                    }
                    else//生產中
                    {
                        //dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;//改變整列顏色
                        dataGridView1.Rows[i].Cells[7].Style.BackColor = Color.LightSkyBlue;
                        
                    }
                }
            }
            
        }


        private bool DoWork(string Sfb01)
        {
            bool rf = false;
            string sql = string.Empty;
            sql += "select * from InProc where 1=1";
            sql += " and shb05='" + Sfb01 + "'";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
            if (dt.Rows.Count > 0) rf = true;
            return rf;
        }

        private bool ReFinsh(string Sfb01)
        {
            bool rf = false;
            string sql = string.Empty;
            sql += "SELECT top 1 * from procschwork WHERE 1=1";
            sql += " and ecm01='" + Sfb01 + "'";
            sql += " ORDER BY ecm03 DESC";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
            Int16 ecm03 = System.Convert.ToInt16(dt.Rows[0]["ecm03"].ToString());//這張工單最後一站製程序
            
            string sql2 = string.Empty;
            sql2 += "select * from InProc where 1=1";
            sql2 += " and shb05='" + Sfb01 + "'";
            sql2 += " and shb06=" + ecm03;
            DataTable dt2 = DOSQL.GetDataTable(Login.WU, sql2);
            if (dt2.Rows.Count > 0) rf = true;
            return rf;
        }

        private bool ReFinshTT(string Sfb01)
        {
            bool rf = false;
            string sql2 = string.Empty;
            sql2 += "select ecm03,ecm04,ecm45,ecm06,ecm65,ecm301,ecm311,ecm315,ecm313   from ew.ecm_file";
            sql2 += " WHERE 1=1";
            sql2 += " and ecm01='" + Sfb01 + "'";
            sql2 += " ORDER BY ecm03 desc";
            DataTable dt2 = DOORC.GetDataTable(Login.TT, sql2);

            Int16 a = System.Convert.ToInt16(dt2.Rows[0]["ecm311"].ToString());
            Int16 b = System.Convert.ToInt16(dt2.Rows[0]["ecm315"].ToString());
            Int16 c = System.Convert.ToInt16(dt2.Rows[0]["ecm313"].ToString());


            if (a + b + c > 0) rf = true;
            return rf;
        }

        private bool ReDoshTT(string Sfb01)
        {
            bool rf = false;
            string sql2 = string.Empty;
            sql2 += "select  sum(ecm311+ecm315+ecm313) a from ew.ecm_file";
            sql2 += " WHERE 1=1";
            sql2 += " and ecm01='" + Sfb01 + "'";
            sql2 += " ORDER BY ecm03 ";
            DataTable dt2 = DOORC.GetDataTable(Login.TT, sql2);
            Int16 a = System.Convert.ToInt16(dt2.Rows[0]["a"].ToString());
            if (a == 0) rf = true;
            return rf;
        }



        //private void find_d()
        //{            
        //    DataTable dt = sfb_file.FindList(Login.DeptOne, string.Empty, "2");//入日排
        //    dataGridView1.Rows.Clear();

        //    int idx = 0;
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        dataGridView1.Rows.Add(new object[] {
        //            dt.Rows[i]["sfb01"],
        //            dt.Rows[i]["occ02"],
        //            dt.Rows[i]["sfb08"]});
        //        idx += 1;
        //    }
        //    if (idx > 0)
        //    {
        //        dataGridView1.Rows[0].Selected = true;
        //        rSfb01 = dataGridView1.Rows[0].Cells[0].Value.ToString();//工單號
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
            dataGridView4.Rows[0].Selected = true;
            //dataGridView4.Sort(dataGridView4.Columns["ecm03"], System.ComponentModel.ListSortDirection.Ascending);


            //GvSort();//排製程序
            //dataGridView3.Rows.Clear();//清除回饋畫面資料
            //RecodeCls();           
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
            DataTable dt = sql_sfb_file.FindList(string.Empty, rSfb01, "3"); //入生產
            HeadShow(dt);            
            gv4Show();//生產統計資料       
            gv5Show();//不良統計資料
            picShow();//圖型顯示
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
            f_sfb13.Text = dt.Rows[0]["sfb13w"].ToString();
            f_sfb15.Text = dt.Rows[0]["sfb15w"].ToString();

            f_sfb223.Text = dt.Rows[0]["sfb223"].ToString();
            f_sfb224.Text = dt.Rows[0]["sfb224"].ToString();
            f_sfb22.Text = dt.Rows[0]["sfb22"].ToString();
            f_sfb221.Text = dt.Rows[0]["sfb221"].ToString();
        }

        private void PschShow()
        {
            string sql = string.Empty;
            sql += "SELECT ecm03,ecm04,ecm45,ecm06,eca02,begdate,rg,enddate from procschwork";
            sql += " where 1=1";
            sql += " and ecm01='" + rSfb01 + "'";
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
                    dt.Rows[i]["eca02"]
                });
            }
        }


        private void gv4Show()
        {   
            string sql = string.Empty;
            sql += "select ecm03,ecm04,ecm45,ecm06,ecm65,ecm301,ecm311,ecm315,ecm313   from ew.ecm_file";
            sql += " WHERE 1=1";
            sql += " and ecm01='" + rSfb01 + "'";
            sql += " ORDER BY ecm03";
            DataTable dt = DOORC.GetDataTable(Login.TT, sql);

            dataGridView4.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double A = System.Convert.ToDouble(dt.Rows[i]["ecm65"].ToString());// 標準產出
                double B = System.Convert.ToDouble(dt.Rows[i]["ecm301"].ToString());//良品轉入數
                double C = System.Convert.ToDouble(dt.Rows[i]["ecm311"].ToString());//良品數
                double D = System.Convert.ToDouble(dt.Rows[i]["ecm315"].ToString());//Bonus
                double E = System.Convert.ToDouble(dt.Rows[i]["ecm313"].ToString());//報廢數
                double F = System.Convert.ToDouble(dt.Rows[i]["ecm65"].ToString());
                
                dataGridView4.Rows.Add(new object[] {
                    dt.Rows[i]["ecm03"],
                    dt.Rows[i]["ecm04"],                    
                    dt.Rows[i]["ecm45"],
                    dt.Rows[i]["ecm06"],
                    "",
                    A,//標準產出
                    B,//良品轉入數
                    C,//良品數
                    D,//Bonus
                    E, //報廢數
                    Math.Round((C /(A+0.00001)) * 100, 2),//達成率
                    Math.Round((C /(B+0.00001)) * 100, 2),//良品率
                    Math.Round(((C+E) /(B+0.00001)) * 100, 2),//產出率
                });
            }
        }

        //抓MES的資料
        //private void gv5Show()
        //{
        //    string sql = string.Empty;
        //    sql += "SELECT shc06,shc04,qce03,ecm04,ecm45, sum(shc05) shc05 from InShc ";
        //    sql += " LEFT OUTER JOIN qce_file on qce01 = InShc.shc04";
        //    sql += " LEFT OUTER JOIN procschwork on ecm01 = InShc.wkno and ecm03 = InShc.shc06";
        //    sql += " WHERE 1=1";
        //    sql += " and InShc.wkno = '" + rSfb01 + "'";
        //    sql += " and InShc.status ='1'";
        //    sql += " GROUP BY shc06,shc04,qce03,ecm04,ecm45";
        //    sql += " ORDER BY shc06,shc05 desc,shc04";

        //    DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
        //    dataGridView5.Rows.Clear();

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        dataGridView5.Rows.Add(new object[] {
        //            dt.Rows[i]["shc06"],
        //            dt.Rows[i]["ecm04"],
        //            dt.Rows[i]["ecm45"],
        //            dt.Rows[i]["shc04"],
        //            dt.Rows[i]["qce03"],
        //            dt.Rows[i]["shc05"]
        //        });
        //    }
        //}

            /// <summary>
            /// 抓TT的資料
            /// </summary>
        private void gv5Show()
        {
            string sql = string.Empty;
            sql += "SELECT shb06,shb081,shb082,shc04,qce03, sum(shc05) shc05  from ew.shb_file ";
            sql += " left OUTER JOIN ew.shc_file on shc01 = ew.shb_file.shb01";
            sql += " left OUTER JOIN ew.qce_file on qce01 = ew.shc_file.shc04";
            sql += " WHERE 1=1";
            sql += " and shb05 = '" + rSfb01 + "'";
            sql += " and shbconf='Y'";
            sql += " and shc05 > 0";
            sql += " GROUP BY shb06,shb081,shb082,shc04,qce03";
            sql += " ORDER BY shb06,shc05 DESC,SHC04";

            DataTable dt = DOORC.GetDataTable(Login.TT, sql);
            dataGridView5.Rows.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView5.Rows.Add(new object[] {
                    dt.Rows[i]["shb06"],
                    dt.Rows[i]["shb081"],
                    dt.Rows[i]["shb082"],
                    dt.Rows[i]["shc04"],
                    dt.Rows[i]["qce03"],
                    dt.Rows[i]["shc05"]
                });
            }
        }


        private void picShow()
        {                      
            drawPic();
        }

        private void setChartAtri()
        {
            #region 設置圖表的屬性
            //圖表的背景色
            chart1.BackColor = Color.FromArgb(211, 223, 240);
            
            //圖表背景色的漸變方式
            chart1.BackGradientStyle = GradientStyle.VerticalCenter;

            //圖表的邊框顏色
            chart1.BorderlineColor = Color.FromArgb(26, 59, 105);

            //圖表的邊框線條樣式
            chart1.BorderlineDashStyle = ChartDashStyle.Solid;

            //圖表邊框線條的寬度
            chart1.BorderlineWidth = 3;

            //圖表邊框的皮膚
            chart1.BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            #endregion
        }

        //private void drawPic()
        //{
        //    string sql = string.Empty;
        //    sql += "SELECT top 10 qce03, sum(shc05) shc05 from InShc ";
        //    sql += " LEFT OUTER JOIN qce_file on qce01 = InShc.shc04";
        //    sql += " WHERE 1=1";
        //    sql += " and InShc.wkno = '" + rSfb01 + "'";
        //    sql += " and InShc.status ='1'";
        //    sql += " GROUP BY qce03";
        //    sql += " ORDER BY shc05";
        //    DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
        //    this.chart1.DataSource = dt;
        //    this.chart1.Series[0].LegendText = "不良項目";
        //    this.chart1.Series[0].IsVisibleInLegend = false;    //不顯示條狀圖上面的說明
        //    this.chart1.Series[0].ChartType = SeriesChartType.Bar;//值條圖
        //    this.chart1.Series[0].IsValueShownAsLabel = true;   //條狀圖上面的數字
        //    this.chart1.Series[0].XValueMember = "qce03";       //X軸的值
        //    this.chart1.Series[0].YValueMembers = "shc05";      //Y軸的值
        //    this.chart1.DataBind();
        //}

            //抓TT資料
        private void drawPic()
        {
            string sql = string.Empty;
            sql += "SELECT qce03, sum(shc05) shc05 from ew.shc_file ";
            sql += " left OUTER JOIN ew.shb_file on SHB01 = SHC01";
            sql += " left OUTER JOIN ew.qce_file on qce01 = shc04";
            sql += " WHERE 1=1";
            sql += " and shb05 = '" + rSfb01 + "'";
            sql += " and shbconf='Y'";
            sql += " GROUP BY qce03";
            sql += " ORDER BY shc05";
            DataTable dt = DOORC.GetDataTable(Login.TT, sql);
            this.chart1.DataSource = dt;
            this.chart1.Series[0].LegendText = "不良項目";      //條狀圖上面的說明
            this.chart1.Series[0].IsVisibleInLegend = false;    //不顯示條狀圖上面的說明
            this.chart1.Series[0].ChartType = SeriesChartType.Bar;//值條圖
            this.chart1.Series[0].IsValueShownAsLabel = true;   //條狀圖上面的數字
            this.chart1.Series[0].XValueMember = "qce03";       //X軸的值
            this.chart1.Series[0].YValueMembers = "shc05";      //Y軸的值
            this.chart1.DataBind();
        }

        private decimal getAchRate(decimal a,decimal b)
        {
            decimal rf = 0;
            rf = Math.Round((b / a) * 100, 2);
            return rf;
        }



        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex < 0) || (e.ColumnIndex < 0)) return;
            dataGridView2.Rows[e.RowIndex].Selected = true;
            rEcm03 = dataGridView2.Rows[e.RowIndex].Cells["ecm03"].Value.ToString(); //製程序

            //匯入相關資料
            SingRecord();

            //顯示回饋資料
            ShowProd(rSfb01, rEcm03);
        }

        private void ShowProd(string Sfb01, string Ecm03)
        {   
            DataTable dt = Inproc.ToDoList(Sfb01, Ecm03);
            dataGridView3.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView3.Rows.Add(new object[] {
                    dt.Rows[i]["doc"],
                    System.Convert.ToDateTime(dt.Rows[i]["shb02"]).ToString("yyyy/MM/dd"),
                    dt.Rows[i]["shb021"],
                    dt.Rows[i]["shb031"],
                    dt.Rows[i]["shb032"],
                    dt.Rows[i]["shb111"],
                    dt.Rows[i]["shb112"],
                    dt.Rows[i]["shb115"],
                    dt.Rows[i]["eci06"],
                    procStatus(dt.Rows[i]["status"].ToString())});
            }
        }

        private string procStatus(string status)
        {
            string rf = string.Empty;
            if (status == "0") rf = "未確認";
            if (status == "1") rf = "已確認";
            if (status == "X") rf = "作廢";
            return rf;
        }

        //private void ShowProd(string Sfb01, string Ecm03)
        //{
        //    dataGridView3.Rows.Clear();
        //    DataTable dt = Inproc.ToDoList(Sfb01, Ecm03);

        //    //if (dt.Rows.Count == 0)
        //    //    dataGridView3.Rows.Clear();
        //    //else
        //    //{
        //    //    DataTable dt1 = (DataTable)dataGridView3.DataSource;
        //    //    dt1.Rows.Clear();
        //    //    dataGridView3.DataSource = null;
        //    //}

        //    if (dt.Rows.Count > 0)
        //    {
        //        dataGridView3.DataSource = dt;
        //        dataGridView3.Rows[0].Selected = true;
        //    }
        //    else
        //    {
        //        //DataTable dt1 = (DataTable)dataGridView3.DataSource;
        //        //dt1.Rows.Clear();
        //        //dataGridView3.DataSource = dt1;
        //    }
        //}


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

        private void ShowGv3()
        {
            DataTable dt = ProcscmDay.SumScm(f_r_begdate.Text, f_r_ecd01.Text);
            dataGridView3.DataSource = dt;
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
                    string td = dataGridView2.Rows[i - 1].Cells["enddate"].Value.ToString(); // 上一筆的完工日
                    dataGridView2.Rows[i].Cells["begdate"].Value = td;
                    dataGridView2.Rows[i].Cells["enddate"].Value = rEndDate(td, r_rg);
                    dataGridView2.Rows[i].Cells["rg"].Value = TimeSpan(dataGridView2.Rows[i].Cells["begdate"].Value.ToString(), dataGridView2.Rows[i].Cells["enddate"].Value.ToString()).ToString();
                }
            }
        }

        private string rEndDate(string d, Int16 r)
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


        //private void btn_upd_Click(object sender, EventArgs e)
        //{
        //    if (f_r_ecm03.Text.Length == 0) return;
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
        //    }
        //}

        private void f_OK_Click(object sender, EventArgs e)
        {

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
            Del_ProcschWork(Sfb01);
            Ins_ProcschWork(Sfb01); //加入明細
            //------
            Del_ProcscmWork(Sfb01);//刪除排程
            Ins_ProcscmWork(Sfb01);//排程 

            //--
            Upd_ProcscaDay(Sfb01);
        }

        private void f_Update()
        {
            //刪除明細
            string Sfb01 = f_sfb01.Text;
            Del_ProcschWork(Sfb01);
            Ins_ProcschWork(Sfb01); //加入明細
            //------
            Del_ProcscmWork(Sfb01);//刪除排程
            Ins_ProcscmWork(Sfb01);//排程 

            //--
            Upd_ProcscaDay(Sfb01);
        }
        private void Upd_ProcscaDay(string rSfb01)
        {
            //抓第一站開工日
            string sql = string.Empty;
            sql += "SELECT top 1 begdate from procschwork WHERE 1=1 ";
            sql += " and ecm01 = '" + rSfb01 + "'";
            sql += " ORDER BY ecm03";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
            string bdate = dt.Rows[0][0].ToString();
            //抓最後一站開工日
            sql = string.Empty;
            sql += "SELECT top 1 enddate from procschwork WHERE 1=1 ";
            sql += " and ecm01 = '" + rSfb01 + "'";
            sql += " ORDER BY ecm03 desc";
            DataTable dt1 = DOSQL.GetDataTable(Login.WU, sql);
            string edate = dt1.Rows[0][0].ToString();

            sql = string.Empty;
            sql = string.Format("update procscawork set sfb13='{0}', sfb15='{1}' where sfb01='{2}'", bdate, edate, rSfb01);
            int i = DOSQL.Excute(Login.WU, sql);
            if (i <= 0) MessageBox.Show("修改失敗-Procscawork");

            //update sfb_file 主排程開始日期及完工日期
            sql = string.Empty;
            sql = string.Format("update sfb_file set sfb13w='{0}', sfb15w='{1}' where sfb01='{2}'", bdate, edate, rSfb01);
            int j = DOSQL.Excute(Login.WU, sql);
            if (j <= 0) MessageBox.Show("修改失敗-sfb_file");
        }

        private void Del_ProcschWork(string Sfb01)
        {            
            string sql = string.Empty;
            sql += "delete from procschwork where ecm01 ='" + Sfb01 + "'";
            int i = DOSQL.Excute(Login.WU, sql);
        }

        private void Ins_ProcschWork(string Sfb01)
        {
            string sql = string.Empty;
            FlowStation = string.Empty;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                ProcschWork p_ = new ProcschWork();
                p_.Ecm01 = Sfb01;
                p_.Ecm03 = System.Convert.ToInt16(dataGridView2.Rows[i].Cells["ecm03"].Value.ToString());
                p_.Ecm04 = dataGridView2.Rows[i].Cells["ecm04"].Value.ToString();
                p_.Ecm45 = dataGridView2.Rows[i].Cells["ecm45"].Value.ToString();
                p_.Ecm06 = dataGridView2.Rows[i].Cells["ecm06"].Value.ToString();
                p_.Eca02 = dataGridView2.Rows[i].Cells["eca02"].Value.ToString();
                p_.Begdate = dataGridView2.Rows[i].Cells["begdate"].Value.ToString();
                p_.Enddate = dataGridView2.Rows[i].Cells["enddate"].Value.ToString();
                p_.Rg = System.Convert.ToInt16(dataGridView2.Rows[i].Cells["rg"].Value.ToString());
                sql += ProcschWork.Set_Insert(p_);                
                FlowStation += FlowS(p_.Ecm03, p_.Ecm04, p_.Ecm06);//紀錄修改的流程站，比對批次修改時流程站是否相同                
            }
            if (sql.Length > 0)
            {
                Double i = DOSQL.Excute(Login.WU, sql);
                if (i <= 0) MessageBox.Show("新增失敗-ProcschWork");
            }
        }

        private void Del_ProcscmWork(string Sfb01)
        {            
            string sql = string.Empty;
            sql += "delete from procscmwork where sfb01 ='" + Sfb01 + "'";
            int i = DOSQL.Excute(Login.WU, sql);            
        }

        private void Ins_ProcscmWork(string Sfb01)
        {
            string sql = string.Empty;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                int DRG = System.Convert.ToInt16(dataGridView2.Rows[i].Cells["rg"].Value.ToString());//間距
                for (int j = 0; j <= DRG; j++)
                {
                    ProcscmWork p_ = new ProcscmWork();
                    p_.Sfb01 = Sfb01;
                    p_.Ecb03 = System.Convert.ToInt16(dataGridView2.Rows[i].Cells["ecm03"].Value.ToString());
                    p_.Schdate = getSchdate(j, dataGridView2.Rows[i].Cells["begdate"].Value.ToString());
                    p_.Ecm04 = dataGridView2.Rows[i].Cells["ecm04"].Value.ToString();
                    p_.Ecm45 = dataGridView2.Rows[i].Cells["ecm45"].Value.ToString();
                    p_.Ecm06 = dataGridView2.Rows[i].Cells["ecm06"].Value.ToString();
                    p_.Eca02 = dataGridView2.Rows[i].Cells["eca02"].Value.ToString();
                    sql += ProcscmWork.Set_Insert(p_);
                }
            }
            if (sql.Length > 0)
            {
                Double i = DOSQL.Excute(Login.WU, sql);
                if (i <= 0) MessageBox.Show("新增失敗-ProcscmWork");
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

        }

        private void Upd_Sfb(string sfb01, string status)
        {            
            int i = sfb_file.UpdateStatus(sfb01, status);
            if (i <= 0) MessageBox.Show("修改失敗-sfb_file.status");
            string sql = "";
            sql += "update sfb_file set closedate = '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + "'";
            sql += " where sfb01 = '" + sfb01 + "'";
            int a = DOSQL.Excute(Login.WU, sql);
            if (i <= 0) MessageBox.Show("修改失敗-sfb_file.closedate");
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
            if (f_sfb01.Text.Trim().Length == 0)
            {
                MessageBox.Show("請先選擇工單");
                return;
            }
            if (Procsca.GetTable(f_sfb01.Text).Rows.Count > 0)
            {
                if (Config.Message("是否轉主排程") == "Y")
                {
                    f_Insert();
                    clsForm();
                    find_d();
                }
            }
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

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex < 0) || (e.ColumnIndex < 0)) return;
            dataGridView3.Rows[e.RowIndex].Selected = true;
            string doc = dataGridView3.Rows[e.RowIndex].Cells["doc"].Value.ToString();//單據號

            //--按鈕事件員工資料
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                DataTable dt = Inedf.EmpList(doc, rSfb01, rEcm03);//單據號,工號,工序
                if (dt.Rows.Count > 0)
                {
                    string Emp = Getemp(dt);//
                    ShowEmp(Emp);//顯示員工資料
                }
            }
            //----------
        }

        private void ShowEmp(string Emp)
        {
            rEmployee fm = new rEmployee();
            fm.Dept = Login.DeptOne;
            fm.EmpList = Emp;
            if (fm.ShowDialog() == DialogResult.OK)
            {                
            }
        }


        private string Getemp(DataTable dt)
        {
            string a = string.Empty;
            string b = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                a += string.Format("'{0}',", dt.Rows[i]["edf09"].ToString()); 
            }            
            b = a.Substring(0, a.Length - 1);
            return b;
        }

        private void btn_TrnWk_Click_1(object sender, EventArgs e)
        {
            btnRtn("X");//工單結案            
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
                if (rSel == "X")
                {
                    if (Config.Message("是否工單結案？") == "Y")
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            bool isSelected = Convert.ToBoolean(dataGridView1.Rows[i].Cells["ck"].Value);
                            if (isSelected)
                            {
                                sfb01 = dataGridView1.Rows[i].Cells["sfb01"].Value.ToString();
                                trnClose(sfb01);
                            }
                        }
                    }
                }

                if (rSel == "D")
                {
                    if (Config.Message("已有生產回饋過工單無法轉回日排\n\r是否轉回日排程？") == "Y")
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            bool isSelected = Convert.ToBoolean(dataGridView1.Rows[i].Cells["ck"].Value);
                            if (isSelected)
                            {                                
                                sfb01 = dataGridView1.Rows[i].Cells["sfb01"].Value.ToString();
                                int j = Inproc.ToDoList(sfb01, string.Empty).Rows.Count;//已生產無法轉回日排
                                if (j == 0)
                                {
                                    trnDay(sfb01);
                                }
                            }
                        }
                    }
                }
                clsForm();//清畫面
                find_d();//找已排入日排資料
            }
        }

        private void trnClose(string sfb01)
        {            
            Upd_Sfb(sfb01, "X");
        }

        //private void btn_TrnWk_Click_1(object sender, EventArgs e)
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
        //    if (Config.Message("工單是否結案?") == "Y")
        //    {
        //        Upd_Sfb("X");
        //        clsForm();//清畫面
        //    }
        //    find_d();//找已排入日排資料
        //}

        private void f_OK_Click_1(object sender, EventArgs e)
        {
            if (f_Check() == false) { MessageBox.Show("請輸入資料"); return; }
            if (Config.Message("確定修改?") == "Y")
            {
                try
                {
                    string Sfb01 = f_sfb01.Text;
                    f_Update(Sfb01);
                    Config.Show("修改成功");                    
                    if (Config.Message("是否批次修改所勾選工單 排程起訖日期?") == "Y")
                    {
                        PichUpdate(FlowStation);
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


        private void PichUpdate(string FlowStation)
        {
            
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                bool isSelected = Convert.ToBoolean(dataGridView1.Rows[i].Cells["ck"].Value);
                if (isSelected)
                {   
                    string sfb01 = dataGridView1.Rows[i].Cells["sfb01"].Value.ToString();
                    if (FlowStation == CheckStation(sfb01)) f_Update(sfb01);//流程站相同修改
                }
            }
            Config.Show("批次修改成功");
        }

        private string CheckStation(string sfb01)
        {
            string fl = string.Empty;
            string sql = string.Empty;
            sql += "select * from procschwork where 1=1";
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

        private string FlowS(Int16 a,string b,string c)
        {
            return a.ToString() + "'" + b + "'" + c + "|";
        }
        


        private void btnTrnDay_Click(object sender, EventArgs e)
        {
            btnRtn("D");            
        }

        

        private void trnDay(string Sfb01)
        {            
            int i = sfb_file.UpdateStatus(Sfb01, "2");
            if (i <= 0) MessageBox.Show("修改失敗-sfb_file.status");
            ProcscaWork.Del_(Sfb01, Login.WU);
            ProcschWork.Del_(Sfb01, Login.WU);
            ProcscmWork.Del_(Sfb01, Login.WU);
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

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.RowIndex < 0)) return;
            dataGridView2.Rows[e.RowIndex].Selected = true;
            rEcm03 = dataGridView2.Rows[e.RowIndex].Cells["ecm03"].Value.ToString(); //製程序

            //匯入相關資料
            SingRecord();

            //顯示回饋資料
            ShowProd(rSfb01, rEcm03);
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.RowIndex < 0)) return;
            dataGridView1.Rows[e.RowIndex].Selected = true;
            isCheck(e.RowIndex);//點選勾選
            rSfb01 = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();//工單號
            f_Data();//找資料
            GvSort();//排製程序
            dataGridView3.Rows.Clear();//清除回饋畫面資料
            RecodeCls();
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex < 0) || (e.ColumnIndex < 0)) return;
            dataGridView4.Rows[e.RowIndex].Selected = true;
            rEcm03 = dataGridView4.Rows[e.RowIndex].Cells[0].Value.ToString(); //製程序
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            mfd013_1 fm = new mfd013_1();
            fm.Sfb01 = rSfb01;
            fm.Ecm03 = rEcm03;            
            if (fm.ShowDialog() == DialogResult.OK)
            {
                //do nothing
            }
        }
    }
}
