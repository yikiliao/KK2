using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using MFPD.Forms;

namespace MFPD.Models
{
    class Mfr040_4
    {
        #region 資料屬性
        public string Doc { get; set; } //單據號
        public string Wkno { get; set; }//工號
        public Int32 Shc06 { get; set; }//工序
        public string Shc04 { get; set; }//不良原因代碼
        public string Qce03 { get; set; }//不良原因說明 
        public decimal Shc05 { get; set; }//不良數量
        #endregion

        public static DataTable ToDoList()
        {
            string sql = string.Empty;
            sql += "SELECT InShc.doc,InShc.wkno,InShc.shc04,InShc.shc05,InShc.shc06,qce_file.qce03 from InShc";
            sql += " LEFT OUTER JOIN qce_file on qce01 = InShc.shc04";
            sql += " WHERE 1=1";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
            return dt;
        }

    }
}
