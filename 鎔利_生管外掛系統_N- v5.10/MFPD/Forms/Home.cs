using MFPD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.OleDb;

namespace MFPD.Forms
{
    public partial class Home : Form
    {
        public static string Id;//使用者帳號       
        static List<umenuE> LS1 = new List<umenuE>();
        PanelControl panelcontrol = new PanelControl();

        public Home(string Userid)
        {
            InitializeComponent();            
            Config.SetFormSize(this, "M");            
            Config.SetPer(this);
            Id = Userid;
            label1.Text = null;
            //if (Login.DeptOne == "J") { Login.COMPNAME = Login.COMPNAME + "－大埔美"; };

            this.Text += String.Format("         {0}  【登入使用者：{1}－{2}】", Login.COMPNAME, Login.IDNAME, Login.DBNAME);            
        }
                

        private void Home_Load(object sender, EventArgs e)
        {
            Query();
            PopulateTreeView("", null);           
        }      
        private void Query()
        {
            LS1.Clear();
            LS1 = umenuE.Treeview(Id).ToList();//依使用者抓menu
            if (LS1.Count() == 0)
            {
                MessageBox.Show("無符合資料");
                return;
            }            
        }        
        private void PopulateTreeView(string parentId, TreeNode parentNode)
        {
            var filteredItems = LS1.Where(item =>item.ParentId == parentId); 
            TreeNode childNode;
            foreach (var i in filteredItems.ToList())
            {   
                if (parentNode == null)
                    childNode = treeView1.Nodes.Add(i.Id,i.IdName);
                else
                    childNode = parentNode.Nodes.Add(i.Id, i.IdName);

                PopulateTreeView(i.Id, childNode);
            }
        }
        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            panelcontrol.formshow(e.Node.Name);            
        }


        private void menu_exit_Click(object sender, EventArgs e)
        {
            //if (Config.Message("是否離開?") == "Y")
            //Application.Exit();
            exit();
        }

        private void Home_KeyDown(object sender, KeyEventArgs e)
        {
            //離開(Control+Del)
            if (e.Control && e.KeyCode == Keys.Delete)
            {
                menu_exit_Click(sender, e);
            }
        }

        

        private void exit()
        {
            if (Config.Message("是否離開?") == "Y")
            {
                Application.Exit();
            }
            else
            {
                return;
            }
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {            
            Application.Exit();
        }

    }
}
