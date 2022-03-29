using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MFPD.Forms;

namespace MFPD.Models
{
    class Mfr040_3
    {
        #region 資料屬性
        public string Doc { get; set; } //單據號
        public string Edf03 { get; set; }//工單
        public Int32 Edf07 { get; set; }//工序
        public string Edf09 { get; set; }//工號
        public string BCnName { get; set; }//姓名
        public string Btime { get; set; }//時
        public string Etime { get; set; }//分 
        public decimal Edf15 { get; set; }//工時
        #endregion

        //public static DataTable ToDoList()
        //{
        //    string sql = string.Empty;
        //    sql += "SELECT InEdf.doc,InEdf.edf03,InEdf.edf07,InEdf.edf09,employee.BCnName,InEdf.bTime,InEdf.eTime,InEdf.edf15 from InEdf";
        //    sql += " LEFT OUTER JOIN employee on employee.BCode = InEdf.edf09";
        //    sql += " WHERE 1=1";
        //    DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
        //    return dt;
        //}
        public static DataTable ToDoList()
        {
            string sql = string.Empty;
            sql += "SELECT InEdf.doc,InEdf.edf03,InEdf.edf07,InEdf.edf09,InEdf.empname BCnName,InEdf.bTime,InEdf.eTime,InEdf.edf15 from InEdf";            
            sql += " WHERE 1=1";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
            return dt;
        }
    }
}
