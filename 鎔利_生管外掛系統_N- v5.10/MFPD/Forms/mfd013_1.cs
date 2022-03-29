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
    public partial class mfd013_1 : Form
    {
        public DataTable dtProd = new DataTable();
        public string Sfb01;//工單
        public string Ecm03;//項次
        public mfd013_1()
        {
            InitializeComponent();
            initForm();
        }

        private void initForm()
        {            
            //開啟畫面時置中
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //標題字體
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("新細明體", 12);
            dataGridView1.DefaultCellStyle.Font = new Font("新細明體", 12);
        }

        private void rEcd_Load(object sender, EventArgs e)
        {            
            getData(); //抓資料
        }

        private void getData()
        {
            string Name = string.Empty;
            dataGridView1.Rows.Clear();
            DataTable dt = Inproc.ToDoList(Sfb01, Ecm03);
            dataGridView1.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView1.Rows.Add(new object[] {
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex < 0) || (e.ColumnIndex < 0)) return;
            dataGridView1.Rows[e.RowIndex].Selected = true;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;//這一定要設
            Close();
            //Add_();
        }

        private void Add_()
        {
            DataRow dr;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected == true)
                {
                    dr = dtProd.NewRow();
                    dr["ecd01"] = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    dr["ecd02"] = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    dr["ecd05"] = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    dr["ecd07"] = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    dtProd.Rows.Add(dr);
                }
            }
            if (dtProd.Rows.Count == 0)
            {
                MessageBox.Show("尚未選取工單，請點選");
                return;
            }
            if (dtProd.Rows.Count > 1)
            {
                MessageBox.Show("不可複選單，請重新點選");
                return;
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;//這一定要設
            Close();
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex < 0) || (e.ColumnIndex < 0)) return;
            dataGridView1.Rows[e.RowIndex].Selected = true;
            string doc = dataGridView1.Rows[e.RowIndex].Cells["doc"].Value.ToString();//單據號

            //--按鈕事件員工資料
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                DataTable dt = Inedf.EmpList(doc, Sfb01, Ecm03);//單據號,工號,工序
                if (dt.Rows.Count > 0)
                {
                    string Emp = Getemp(dt);//
                    ShowEmp(Emp);//顯示員工資料
                }
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

        private void ShowEmp(string Emp)
        {
            rEmployee fm = new rEmployee();
            fm.Dept = Login.DeptOne;
            fm.EmpList = Emp;
            if (fm.ShowDialog() == DialogResult.OK)
            {
            }
        }

    }
}
