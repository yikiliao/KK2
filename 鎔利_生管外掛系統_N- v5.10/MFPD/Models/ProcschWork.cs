using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using MFPD.Forms;

namespace MFPD.Models
{
    class ProcschWork
    {
        #region 資料屬性
        public string Ecm01 { get; set; }
        public Int16 Ecm03 { get; set; }
        public string Ecm04 { get; set; }
        public string Ecm45 { get; set; }
        public string Ecm06 { get; set; }
        public string Eca02 { get; set; }
        public string Begdate { get; set; }
        public string Enddate { get; set; }
        public Int16 Rg { get; set; }//間距
        public string Schdate { get; set; }

        public DateTime Add_date { get; set; }
        public DateTime Mod_date { get; set; }
        #endregion
        public static string Set_Insert(ProcschWork rProcschWork)
        {
            string sql = string.Empty;
            sql += "insert into procschwork (ecm01,ecm03,ecm04,ecm45,ecm06,eca02,begdate,enddate,rg,add_date,mod_date) ";
            sql += string.Format("values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}',{8},'{9}','{10}');\r\n",
                rProcschWork.Ecm01,
                rProcschWork.Ecm03,
                rProcschWork.Ecm04,
                rProcschWork.Ecm45,
                rProcschWork.Ecm06,
                rProcschWork.Eca02,
                rProcschWork.Begdate,
                rProcschWork.Enddate,
                rProcschWork.Rg,
                DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"),
                DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
            return sql;
        }

        public static string Set_Delete(string Sfb01)
        {
            string sql = string.Empty;
            sql += "delete procschwork where sfb01='" + Sfb01 + "'";
            return sql;
        }

        public static int Del_(string sfb01, string cnstr)
        {
            string sql = "";
            sql += "delete procschwork where ecm01 ='" + sfb01 + "'";
            int a = DOSQL.Excute(cnstr, sql);
            return a;
        }

    }
}
