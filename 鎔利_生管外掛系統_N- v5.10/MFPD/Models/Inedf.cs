using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using MFPD.Forms;

namespace MFPD.Models
{
    class Inedf
    {
        #region 資料屬性
        public string Doc { get; set; } //單據號
        public string Edf03 { get; set; } //工單號
        public string Edf09 { get; set; } //工號
        #endregion
              

        public static DataTable EmpList(string Doc,string Sfb01,string Ecm03)
        {            
            string sql = string.Empty;
            sql += "SELECT doc,edf03,edf09 from InEdf ";
            sql += " where 1=1";
            sql += " and doc='" + Doc + "'"; //單據號
            if (Sfb01.Length > 0)
            {
                sql += " and edf03='" + Sfb01 + "'";
            }
            if (Ecm03.Length > 0)
            {
                Int16 edf07 = System.Convert.ToInt16(Ecm03);
                sql += " and edf07=" + edf07;
            }
            sql += " order by edf09 ";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
            return dt;
        }

    }
}
