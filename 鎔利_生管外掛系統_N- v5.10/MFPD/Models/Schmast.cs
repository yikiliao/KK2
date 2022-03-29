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
    class Schmast
    {
        public string Sch01 { get; set; } //工單
        public string Sch02 { get; set; } //日期
        public decimal Sch03 { get; set; } //數量
        public string Ima01 { get; set; }
        public string Occ02 { get; set; }
        public string Ima02 { get; set; }

        public static DataTable ToList(string Dept, string Begdate, string EndDate)
        {
            string sql = string.Empty;
            sql += "SELECT schmast.sch01,schmast.sch02,schmast.sch03,sfb_file.ima01,sfb_file.occ02,sfb_file.ima02 from schmast ";
            sql += " LEFT OUTER JOIN sfb_file on sfb_file.sfb01 = schmast.sch01";
            sql += " WHERE 1=1";
            sql += " and SUBSTRING(schmast.sch01,1,1)='" + Dept + "'";            
            sql += " and schmast.sch02 BETWEEN '" + Begdate + "' and '" + EndDate + "'";
            sql += " ORDER BY schmast.sch01";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
            return dt;
        }

        public static string Set_Insert(Schmast rc)
        {
            string sql = string.Empty;
            sql += "insert into schmast (sch01,sch02,sch03) ";
            sql += string.Format("values('{0}','{1}',{2});\r\n",
                rc.Sch01,
                rc.Sch02,
                rc.Sch03);
            return sql;
        }

        public static int Del_(string sfb01, string cnstr)
        {
            string sql = "";
            sql += "delete schmast where sch01 ='" + sfb01 + "'";
            int a = DOSQL.Excute(cnstr, sql);
            return a;
        }
    }
}
