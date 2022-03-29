using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MFPD.Forms;

namespace MFPD.Models
{
    class Mfr040_2
    {
        #region 資料屬性
        public string Empid { get; set; }//工號
        public string Empname { get; set; }//姓名
        public string Bhh { get; set; }//時
        public string Bmm { get; set; }//分 
        public string Ehh { get; set; }//
        public string Emm { get; set; }//
        public decimal Worktime { get; set; }//工時
        #endregion

        public static DataTable ToDoList()
        {
            string sql = string.Empty;
            sql += "SELECT doc,empid,empname,bhh,bmm,ehh,emm,worktime from emptime WHERE 1=1";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
            return dt;
        }
    }
}
