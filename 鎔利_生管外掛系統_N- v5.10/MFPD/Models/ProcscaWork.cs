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
    class ProcscaWork
    {
        #region 資料屬性
        public string Sfb01 { get; set; }
        public string Occ02 { get; set; }
        public string Ima01 { get; set; }
        public string Ima02 { get; set; }
        public string Ima021 { get; set; }
        public string Sfb06 { get; set; }
        public decimal Sfb08 { get; set; }//生產數
        public string Sfb13 { get; set; }//預計生產日
        public string Sfb15 { get; set; }//客戶交期

        public DateTime Add_date { get; set; }
        public DateTime Mod_date { get; set; }
        #endregion

        public static DataTable ToDoList(string rSel)
        {
            string sql = string.Empty;
            sql += "select * from procscawork where 1=1";
            if (rSel != string.Empty)
            {
                sql += " and sfb01 in (" + rSel + ")";
            }
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
            return dt;
        }

        public static DataTable ToDoList(string Dept, string begDate, string endDate)
        {
            string sql = string.Empty;
            sql += "select * from procscawork where 1=1";
            sql += " and SUBSTRING(sfb01,1,1)='" + Dept + "'";
            sql += " and sfb13 BETWEEN '" + begDate + "' and '" + endDate + "'";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
            return dt;
        }

        public static DataTable GetTable(string rSel)
        {
            string sql = string.Empty;
            sql += "select * from procscawork where 1=1";
            sql += " and sfb01 = '" + rSel + "'";            
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
            return dt;
        }

        public static string Set_Insert(ProcscaDay rProcscaDay)
        {
            string sql = string.Empty;
            sql += "insert into procscawork (sfb01,occ02,ima01,ima02,ima021,sfb06,sfb08,sfb13,sfb15) ";
            sql += string.Format("values('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}');\r\n",
                rProcscaDay.Sfb01,
                rProcscaDay.Occ02,
                rProcscaDay.Ima01,
                rProcscaDay.Ima02,
                rProcscaDay.Ima021,
                rProcscaDay.Sfb06,
                rProcscaDay.Sfb08,
                rProcscaDay.Sfb13, rProcscaDay.Sfb15);
            return sql;
        }
        public static int Del_(string sfb01, string cnstr)
        {
            string sql = "";
            sql += "delete procscawork where sfb01 ='" + sfb01 + "'";
            int a = DOSQL.Excute(cnstr, sql);
            return a;
        }
        public static int Ins_(ProcscaWork p_procscawork, string local)
        {
            string sql = "";
            sql += "insert into procscawork (sfb01,occ02,ima01,ima02,ima021,sfb06,sfb08,sfb13,sfb15,add_date,mod_date) ";
            sql += "values(@sfb01,@occ02,@ima01,@ima02,@ima021,@sfb06,@sfb08,@sfb13,@sfb15,@add_date,@mod_date)";
            int i = 0;
            using (SqlConnection conn = new SqlConnection(local))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@sfb01", p_procscawork.Sfb01);
                comm.Parameters.AddWithValue("@occ02", p_procscawork.Occ02);
                comm.Parameters.AddWithValue("@ima01", p_procscawork.Ima01);
                comm.Parameters.AddWithValue("@ima02", p_procscawork.Ima02);
                comm.Parameters.AddWithValue("@ima021", p_procscawork.Ima021);
                comm.Parameters.AddWithValue("@sfb06", p_procscawork.Sfb06);
                comm.Parameters.AddWithValue("@sfb08", p_procscawork.Sfb08);
                comm.Parameters.AddWithValue("@sfb13", p_procscawork.Sfb13);
                comm.Parameters.AddWithValue("@sfb15", p_procscawork.Sfb15);

                comm.Parameters.AddWithValue("@add_date", DateTime.Now);
                comm.Parameters.AddWithValue("@mod_date", DateTime.Now);

                comm.Transaction = tran;
                i = comm.ExecuteNonQuery();
                if (i == 0)
                {
                    tran.Rollback();
                }
                else
                {
                    tran.Commit();
                }
                conn.Close();
            }
            return i;
        }

    }
}
