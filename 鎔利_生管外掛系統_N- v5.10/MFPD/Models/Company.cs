using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MFPD.Forms;

namespace MFPD.Models
{
    class Company
    {
        #region 資料屬性
        public string Code { get; set; }
        public string Name { get; set; }
        public string LongName { get; set; }
        #endregion

        public static DataTable ToDoList(string Code)
        {
            string sql = string.Empty;
            sql += "SELECT code,name,longname from company";
            sql += " where 1=1";
            if (Code.Length > 0)
            {
                sql += " and code ='" + Code + "'";
            }
            sql += " order by code ";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
            return dt;
        }

    }
}
