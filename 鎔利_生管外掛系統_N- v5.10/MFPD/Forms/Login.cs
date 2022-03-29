using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


using MFPD.Models;

using System.Collections;
using System.Configuration;

namespace MFPD.Forms
{
    public partial class Login : Form
    {        
        public static string DEPT;      //ej 廠部代碼
        public static string DEPT_NAME; //廠部簡稱
        public static string COMPNAME;  //公司全名
        public static string MFPD;      //Sql 連線字串
        public static string TT;        //Oracle 連線字串        
        public static Int16 DB;         //正式區 1 測試區 2
        public static string ID;        //登入帳號
        public static string WU;        //DB 鎔利MES
        public static string IDNAME;    //中文名稱
        public static string DBNAME;    //正式區 測試區
        public static string DeptOne;   //判斷單據第一碼

        int TryTime=0;        
        public Login()
        {
            InitializeComponent();                        
            init_Form();
            //f_id.Text = "yiki";
            //f_password.Text = "yiki";
        }

        private void init_Form()
        {
            //*******************調整視窗位置大小
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;//保留邊框;把最大控制,小等的控制項取消
            //this.Font = new Font("新細明體", 10);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;

            string DBname = ConfigurationManager.AppSettings["DB"];

            //連線資訊**********************
            //TINI oTINI = new TINI(Path.Combine(Application.StartupPath, @"ini\Setup.ini"));
            //DEPT = oTINI.getKeyValue("LOCAL", "CODE"); //廠部代碼
            //DEPT_NAME = oTINI.getKeyValue("LOCAL", "NAME");
            //COMPNAME = oTINI.getKeyValue("LOCAL", "COMPNAME");            
            //MFPD = oTINI.getKeyValue("LOCAL", "MFPD"); //sql連線字串            
            //TT = oTINI.getKeyValue("LOCAL", "TT"); //TT連線字串
            //DB = System.Convert.ToInt16(oTINI.getKeyValue("LOCAL", "NO")); //登入資料庫
            //*************************************************

            TINI oTINI = new TINI(Path.Combine(Application.StartupPath, @"ini\Setup.ini"));
            MFPD = oTINI.getKeyValue("LOCAL", "MFPD"); //sql連線字串
            //MFPD = ConfigurationManager.ConnectionStrings["MDORC"].ToString();   //來源
            WU = ConfigurationManager.ConnectionStrings["WU"].ToString();   //來源
            TT = ConfigurationManager.ConnectionStrings["TT"].ToString();

            DEPT = ConfigurationManager.AppSettings["CODE"]; //ew
            DEPT_NAME = ConfigurationManager.AppSettings["NAME"];//
            COMPNAME = ConfigurationManager.AppSettings["COMPNAME"];//鎔利興業股份有限公司
            DBNAME = ConfigurationManager.AppSettings["DBNAME"];

            DeptOne =  DEPT.Substring(1, 1).ToUpper();  //J或W

            //******************************
            D_Plant();//營運中心            
            f_plant.SelectedIndex = 0;
        }

        private void D_Plant()
        {
            string sql = string.Empty;
            sql += "SELECT code,name from company where 1=1";
            DataTable dt = DOSQL.GetDataTable(WU, sql);
            f_plant.DisplayMember = "name";
            f_plant.ValueMember = "code";
            f_plant.DataSource = dt;
        }



        //private void D_Plant()
        //{
        //    ArrayList data = new ArrayList();
        //    data.Add(new DictionaryEntry(DEPT_NAME, DEPT));
        //    f_plant.DisplayMember = "Key";
        //    f_plant.ValueMember = "Value";
        //    f_plant.DataSource = data;            
        //}


        private void button1_Click(object sender, EventArgs e)
        {  
            string Usrid = f_id.Text.Trim();
            string Passwd = f_password.Text.Trim();
            ID = Usrid; //登入帳號 
            TryTime = TryTime + 1;
            if (TryTime > 5)
            {
                Config.Show("登入錯誤次數太多");
                System.Threading.Thread.Sleep(1000);//停1秒
                Application.Exit();
            }
               
            if (f_Check(Usrid, Passwd) == false)
                return;
            try
            {
                DEPT = f_plant.SelectedValue.ToString(); //ew
                DeptOne = DEPT.Substring(1, 1).ToUpper();  //J或W
                DEPT_NAME = Company.ToDoList(DEPT).Rows[0][1].ToString();
                COMPNAME = Company.ToDoList(DEPT).Rows[0][2].ToString();
                this.Hide();
                Home fm = new Home(Usrid);
                fm.Show();
            }
            catch (Exception ex)
            {
                Config.Show(ex.Message);
            }
            
        }

        private bool f_Check(string Usrid,string Passwd)
        {            
            var p_sst901 = new sst901();
            p_sst901 = sst901.Get(Usrid);
            if (p_sst901 == null)
            {
                Config.Show("無此使用者");
                return false;
            }
            if (p_sst901.Erp_id == Usrid && p_sst901.Password == Passwd)
            {
                // do nothing
                IDNAME = p_sst901.Pr_name;
            }
            else
            {
                Config.Show("密碼錯誤");
                return false;
            }
            return true;
        }

        private void menu_exit_Click(object sender, EventArgs e)
        {
            if (Config.Message("是否離開?") == "Y")
            Application.Exit();
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            //離開(Control+Del)
            if (e.Control && e.KeyCode == Keys.Delete)
            {
                menu_exit_Click(sender, e);
            }

            //確認(Escape)
            if (e.KeyCode == Keys.Escape)
            {
                button1_Click(sender, e);
            }
        }

        //產生提示訊息
        private void button1_MouseHover(object sender, EventArgs e)
        {
            ToolTip ttTip = new ToolTip();
            ttTip.SetToolTip(button1, "Escape");
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

        private void Login_Load(object sender, EventArgs e)
        {            
            FindControl(this);
        }

        private void FindControl(Control ctl_false)
        {   
            foreach (Control c in ctl_false.Controls)
            {
                FindControl(c);
                if (c is TextBox)
                {
                    (c as TextBox).GotFocus += new EventHandler(Control_GotFocus);
                    (c as TextBox).LostFocus += new EventHandler(Control_LostFocus);
                }
            }
        }



        private void Control_GotFocus(object sender, EventArgs e)
        {
            Config.Control_Click(sender, e);
        }

        private void Control_LostFocus(object sender, EventArgs e)
        {
            Config.Control_Leave(sender, e);
        }

        private void f_id_Validating(object sender, CancelEventArgs e)
        {
            //string errMsg;
            //if (!isNotEmpty(f_id.Text, out errMsg))
            //{
            //    e.Cancel = true;
            //    Config.Show("帳號"+errMsg);
            //}
        }

        

        private void f_password_Validating(object sender, CancelEventArgs e)
        {
            //string errMsg;
            //if (!isNotEmpty(f_password.Text, out errMsg))
            //{
            //    e.Cancel = true;
            //    Config.Show("密碼"+errMsg);
            //}
        }


        public bool isNotEmpty(string st, out string errMsg)
        {
            if (st.Trim().Length==0)
            {
                errMsg = "不可空白";
                return false;
            }

            errMsg = "";
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            f_id.Text = "yiki";
            f_password.Text = "yiki";
            button1_Click(null, null);
        }
    }
}
