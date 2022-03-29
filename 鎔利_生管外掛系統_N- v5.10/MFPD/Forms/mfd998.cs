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
using System.Configuration;

namespace MFPD.Forms
{
    public partial class mfd998 : Form
    {
        public mfd998()
        {
            InitializeComponent();
        }
        
        private Series[] _series = null;
        private readonly double[] _y = new double[] { 100, 57, 93, 26, 77, 88 };
        private readonly Color[] _colors = new Color[] { Color.Peru, Color.PowderBlue, Color.RosyBrown,
            Color.Salmon, Color.Sienna, Color.SlateBlue, Color.IndianRed, Color.Black, Color.Red, Color.Yellow };
        private readonly String[] _users = new String[] { "小王", "小風", "小明", "小姿", "小玉", "小蟹" };

        //private void mfd998_Load(object sender, EventArgs e)
        //{
        //    chart1.Titles.Add("長條圖");
        //    int _length = _y.Length;
        //    _series = new Series[_length];
        //    for (int index = 0; index < _length; index++)
        //    {
        //        _series[index] = new Series();
        //        _series[index].Color = _colors[index];
        //        _series[index].ChartType = SeriesChartType.Column;
        //        _series[index].Name = _users[index];
        //        _series[index].IsValueShownAsLabel = true;
        //        _series[index].Points.Add(_y[index]);
        //        chart1.Series.Add(_series[index]);
        //    }
        //}

        //private void mfd998_Load(object sender, EventArgs e)
        //{
        //    //chart1.Series.Clear();            
        //    //setChartAtri();
        //    //inp();
        //}

        private void mfd998_Load(object sender, EventArgs e)
        {
            //chart2.Series.Clear();
            setChartAtri2();
            getData();
        }

        private void getData()
        {
            string WU = ConfigurationManager.ConnectionStrings["WU"].ToString();   //來源
            string sql = string.Empty;
            sql += "SELECT top 10 qce03, sum(shc05) shc05 from InShc ";
            sql += " LEFT OUTER JOIN qce_file on qce01 = InShc.shc04";
            sql += " WHERE 1=1";
            sql += " and InShc.wkno = 'JS012-22010017'";
            sql += " and InShc.status ='1'";
            sql += " GROUP BY qce03";
            sql += " ORDER BY shc05";
            DataTable dt = DOSQL.GetDataTable(WU, sql);

            ////找出不良名稱
            //string[] x = (from p in dt.AsEnumerable()                          
            //              select p.Field<string>("qce03")).ToArray();

            ////找出不良數量
            //decimal[] y = (from p in dt.AsEnumerable()                       
            //               select p.Field<decimal>("shc05")).ToArray();

            //MessageBox.Show(x[0]);
            //MessageBox.Show(x[1]);
            //MessageBox.Show(x[2]);

            this.chart2.DataSource = dt;
            this.chart2.Series[0].LegendText = "不良項目統計";//顯示條狀圖上面的說明
            this.chart2.Series[0].IsVisibleInLegend = false;//不顯示條狀圖上面的說明

            this.chart2.Series[0].ChartType = SeriesChartType.Bar;  //條狀圖
            this.chart2.Series[0].IsValueShownAsLabel = true;   //條狀圖上面的數字
            //this.chart2.Series[0].IsValueShownAsLabel = false;
            this.chart2.Series[0].XValueMember = "qce03";//X軸的值
            this.chart2.Series[0].YValueMembers = "shc05";//Y軸的值
            this.chart2.DataBind();


            //chart2.Series[0].LegendText = "Brazil Order Statistics";
            //chart2.Series[0].ChartType = SeriesChartType.Column;
            //chart2.Series[0].IsValueShownAsLabel = true;
            //chart2.Series[0].Color = _colors[0];
            //chart2.Series[0].Points.DataBindXY(x, y);
        }

        private void setChartAtri2()
        {
            #region 設置圖表的屬性
            //圖表的背景色
            chart2.BackColor = Color.FromArgb(211, 223, 240);

            //圖表背景色的漸變方式
            chart2.BackGradientStyle = GradientStyle.VerticalCenter;

            //圖表的邊框顏色
            chart2.BorderlineColor = Color.FromArgb(26, 59, 105);

            //圖表的邊框線條樣式
            chart2.BorderlineDashStyle = ChartDashStyle.Solid;

            //圖表邊框線條的寬度
            chart2.BorderlineWidth = 3;

            //圖表邊框的皮膚
            chart2.BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;

            #endregion
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

        private void inp()
        {
            string WU = ConfigurationManager.ConnectionStrings["WU"].ToString();   //來源
            string sql = string.Empty;
            sql += "SELECT top 10 shc04,qce03, sum(shc05) shc05 from InShc ";
            sql += " LEFT OUTER JOIN qce_file on qce01 = InShc.shc04";            
            sql += " WHERE 1=1";
            sql += " and InShc.wkno = 'JS012-22010017'";
            sql += " and InShc.status ='1'";
            sql += " GROUP BY shc04,qce03";
            sql += " ORDER BY shc05 desc,shc04";

            DataTable dt = DOSQL.GetDataTable(WU, sql);

            chart1.Titles.Add("不良項目統計");
            int _length = dt.Rows.Count; //筆數
            _series = new Series[_length];
            
            for (int i = 0; i < _length; i++)
            {
                _series[i] = new Series();
                _series[i].Color = _colors[i];  //直條圖顏色
                _series[i].ChartType = SeriesChartType.Column;  //直條圖
                _series[i].Name = dt.Rows[i]["qce03"].ToString();
                _series[i].IsValueShownAsLabel = true;  //顯示直條圖數字                
                _series[i].Points.Add(System.Convert.ToInt16(dt.Rows[i]["shc05"].ToString()));
                
                chart1.Series.Add(_series[i]);
            }
            
        }


    }
}
