using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using MFPD.Forms;

namespace MFPD.Models
{
    class Procscm
    {
        #region 資料屬性
        public string Sfb01 { get; set; }//工單        
        public Int16 Ecb03 { get; set; }//製程序
        public string Schdate { get; set; }//排程日期        
        public string Ecm04 { get; set; }//作業編號
        public string Ecm45 { get; set; }//作業說明
        public string Ecm06 { get; set; }//工作站
        public string Eca02 { get; set; } //工作站說明
        public string Ima01 { get; set; } //料號
        public string Ima02 { get; set; } //品名
        public string Ima021 { get; set; } //規格
        public string Sfb08 { get; set; } //生產數
        public string Occ02 { get; set; } //客戶名稱

        public DateTime Add_date { get; set; }
        public DateTime Mod_date { get; set; }
        #endregion

        public static string Set_Insert(Procscm rc)
        {
            string sql = string.Empty;
            sql += "insert into procscm (Sfb01,Ecb03,Schdate,Ecm04,Ecm45,Ecm06,Eca02,add_date,mod_date) ";
            sql += string.Format("values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}');\r\n",
                rc.Sfb01,
                rc.Ecb03,
                rc.Schdate,
                rc.Ecm04,
                rc.Ecm45,
                rc.Ecm06,
                rc.Eca02,
                DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"),
                DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
            return sql;
        }

        public static DataTable SumScm(string Schdate,string Ecm04)
        {
            string sql = string.Empty;            
            sql += "SELECT procscm.sfb01,procscm.ecm04,sfb_file.sfb08 from procscm";
            sql += " LEFT OUTER JOIN sfb_file on sfb_file.sfb01 = procscm.sfb01";
            sql += " WHERE 1=1";
            sql += " and procscm.schdate = '" + Schdate + "'";
            sql += " and procscm.ecm04 = '" + Ecm04 + "'";
            sql += " ORDER BY procscm.sfb01";            
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
            return dt;
        }

        public static int Del_(string sfb01, string cnstr)
        {
            string sql = "";
            sql += "delete procscm where sfb01 ='" + sfb01 + "'";
            int a = DOSQL.Excute(cnstr, sql);
            return a;
        }

        



    }
}
