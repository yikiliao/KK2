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

namespace MFPD.Forms
{
    public partial class mfd999 : Form
    {
        private int m_nTimeCount = 0;
        Random clsRanom = new Random();
        public mfd999()
        {
            InitializeComponent();

            //// 設定ChartArea
            //ChartArea chtArea = new ChartArea("ViewArea");
            //chtArea.AxisX.Minimum = 0; //X軸數值從0開始
            //chtArea.AxisX.ScaleView.Size = 10; //設定視窗範圍內一開始顯示多少點
            //chtArea.AxisX.Interval = 5;
            //chtArea.AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            //chtArea.AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All; //設定scrollbar
            //chart1.ChartAreas[0] = chtArea; // chart new 出來時就有內建第一個chartarea

            //// 設定 Timer
            //Timer clsTimer = new Timer();
            //clsTimer.Tick += new EventHandler(RefreshChart);
            //clsTimer.Interval = 300;
            //clsTimer.Start();

        }


        

        private void RefreshChart(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            // 新增一個點至Series中
            chart1.Series[0].Points.AddXY(m_nTimeCount, clsRanom.Next(0, 100));
            if (m_nTimeCount > 10)
            {
                chart1.ChartAreas[0].AxisX.ScaleView.Position = m_nTimeCount - 10; //將視窗焦點維持在最新的點那邊
            }
            m_nTimeCount++;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[] data1 = new double[360];
            //double[] data2 = new double[360];
            //double[] data3 = new double[360];

            for (int i = 0; i < 360; i++)
            {
                data1[i] = Math.Sin(i * 2 * Math.PI / 360);
                //data2[i] = Math.Cos(i * 2 * Math.PI / 360);
                //data3[i] = data1[i] + data2[i];
            }

            // 建立好資料
            chart1.Series[0].Points.Clear();
            //chart1.Series[1].Points.Clear();
            //chart1.Series[2].Points.Clear();
            // 匯入Chart1
            for (int i = 0; i < 360; i++)
            {                
                chart1.Series[0].Points.AddXY(i, data1[i]);
                //chart1.Series[1].Points.AddXY(i, data2[i]);
                //chart1.Series[2].Points.AddXY(i, data3[i]);
            }
        }

        private void mfd999_Load(object sender, EventArgs e)
        {
            setChartAtri();
            setChartTitle();
            //setChartArea();
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
            chart1.BorderlineWidth = 1;

            //圖表邊框的皮膚
            chart1.BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            #endregion
        }

        private void setChartTitle()
        {
            #region 設置圖表的Title
            Title title = new Title();
            //標題內容
            title.Text = "BER";

            //標題的字體
            title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12, FontStyle.Bold);

            //標題字體顏色
            title.ForeColor = Color.FromArgb(26, 59, 105);

            //標題陰影顏色
            title.ShadowColor = Color.FromArgb(32, 0, 0, 0);

            //標題陰影偏移量
            title.ShadowOffset = 3;

            chart1.Titles.Add(title);
            #endregion
        }


        private void setChartArea()
        {
            //圖表區的名字

            ChartArea chartArea = new ChartArea("Default");

            //背景色

            chartArea.BackColor = Color.White;//Color.FromArgb(64, 165, 191, 228);

            //背景漸變方式

            chartArea.BackGradientStyle = GradientStyle.None;

            //漸變和陰影的輔助背景色

            chartArea.BackSecondaryColor = Color.White;

            //邊框顏色

            chartArea.BorderColor = Color.Green;

            //邊框線條寬度

            chartArea.BorderWidth = 2;

            //邊框線條樣式

            chartArea.BorderDashStyle = ChartDashStyle.Solid;

            //陰影顏色

            //chartArea.ShadowColor = Color.Transparent;

            //設置X軸和Y軸線條的顏色和寬度

            chartArea.AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);

            chartArea.AxisX.LineWidth = 1;

            chartArea.AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);

            chartArea.AxisY.LineWidth = 1;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            
            for (int j = 0; j < 100; j++)

                chart1.Series[0].Points.AddXY(j, j);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //chart2.Series[0]["PointWidth"] = "1";
            int x = 0;
            int y = 200;
            
            chart2.Series[0].Points.AddY(y);
                       
            chart2.Series[1].Points.AddY(60);
            chart2.Series[2].Points.AddY(58);
            chart2.Series[3].Points.AddY(47);
            chart2.Series[4].Points.AddY(5);
            chart2.Series[5].Points.AddY(2);
            chart2.Series[6].Points.AddY(2);
            chart2.Series[7].Points.AddY(1);
            //chart2.Series[1].AxisLabel = "實際";
        }
    }
}
