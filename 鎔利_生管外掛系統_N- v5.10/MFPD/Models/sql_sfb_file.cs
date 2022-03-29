using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using MFPD.Forms;

namespace MFPD.Models
{
    class sql_sfb_file
    {
        #region 資料屬性
        public string Sfb01 { get; set; }
        public string Sfb22 { get; set; }
        public string Sfb221 { get; set; }
        public string Sfb02 { get; set; }        
        public string Sfb05 { get; set; }
        public string Sfb06 { get; set; }
        public decimal Sfb08 { get; set; }
        public string Sfb04 { get; set; }
        public string Sfb13 { get; set; }
        public string Sfb81 { get; set; }
        public string Sfb223 { get; set; }
        public string Sfb224 { get; set; }
        public string Sfb15 { get; set; }//客戶交期 從訂單轉過來的客戶交期

        public string Occ02 { get; set; }
        public string Ima01 { get; set; }
        public string Ima02 { get; set; }
        public string Ima021 { get; set; }
        public string Status { get; set; }

        public string Sfb13m { get; set; }

        public string Sfb15m { get; set; }

        public string Sfb13d { get; set; }

        public string Sfb15d { get; set; }
        public string Sfb13w { get; set; }

        public string Sfb15w { get; set; }

        #endregion

        //public static void Ins_(sql_sfb_file p_sfb, string local)
        //{
        //    string sql = "";
        //    sql += "insert into sfb_file (sfb01,sfb02,sfb05,sfb06,sfb08,sfb04,sfb13,sfb81,sfb223,sfb224,sfb15,status) ";
        //    sql += "values(@sfb01,@sfb02,@sfb05,@sfb06,@sfb08,@sfb04,@sfb13,@sfb81,@sfb223,@sfb224,@sfb15,@status)";
        //    int i = 0;
        //    using (SqlConnection conn = new SqlConnection(local))
        //    {
        //        conn.Open();
        //        SqlTransaction tran = conn.BeginTransaction();
        //        SqlCommand comm = new SqlCommand(sql, conn);
        //        comm.Parameters.AddWithValue("@sfb01", p_sfb.Sfb01);
        //        comm.Parameters.AddWithValue("@sfb02", p_sfb.Sfb02);
        //        comm.Parameters.AddWithValue("@sfb05", p_sfb.Sfb05);
        //        comm.Parameters.AddWithValue("@sfb06", p_sfb.Sfb06);
        //        comm.Parameters.AddWithValue("@sfb08", p_sfb.Sfb08);
        //        comm.Parameters.AddWithValue("@sfb04", p_sfb.Sfb04);
        //        comm.Parameters.AddWithValue("@sfb13", p_sfb.Sfb13);
        //        comm.Parameters.AddWithValue("@sfb81", p_sfb.Sfb81);
        //        comm.Parameters.AddWithValue("@sfb223", p_sfb.Sfb223);
        //        comm.Parameters.AddWithValue("@sfb224", p_sfb.Sfb224);
        //        comm.Parameters.AddWithValue("@sfb15", p_sfb.Sfb15);

        //        comm.Parameters.AddWithValue("@status", p_sfb.Status);

        //        comm.Transaction = tran;
        //        i = comm.ExecuteNonQuery();
        //        if (i == 0)
        //        {
        //            tran.Rollback();
        //        }
        //        else
        //        {
        //            tran.Commit();
        //        }
        //        conn.Close();
        //    }
        //}

        public static DataTable ToDoList(string rSel)
        {
            string sql = string.Empty;
            sql += "select * from sfb_file where 1=1";
            if (rSel != string.Empty)
            {
                sql += " and sfb01 in (" + rSel + ")";
            }
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
            return dt;
        }

        public static void Ins_(sql_sfb_file p_sfb, string local)
        {
            string sql = "";
            sql += "insert into sfb_file (sfb01,sfb22,sfb221,sfb02,sfb05,sfb06,sfb08,sfb13,sfb81,sfb223,sfb224,sfb15,occ02,ima01,ima02,ima021,status,add_date,mod_date,sfb13m,sfb15m,sfb13d,sfb15d,sfb13w,sfb15w) ";
            sql += "values(@sfb01,@sfb22,@sfb221,@sfb02,@sfb05,@sfb06,@sfb08,@sfb13,@sfb81,@sfb223,@sfb224,@sfb15,@occ02,@ima01,@ima02,@ima021,@status,@add_date,@mod_date,@sfb13m,@sfb15m,@sfb13d,@sfb15d,@sfb13w,@sfb15w)";
            int i = 0;
            using (SqlConnection conn = new SqlConnection(local))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@sfb01", p_sfb.Sfb01);
                comm.Parameters.AddWithValue("@sfb22", p_sfb.Sfb22);                
                comm.Parameters.AddWithValue("@sfb221", p_sfb.Sfb221);
                comm.Parameters.AddWithValue("@sfb02", p_sfb.Sfb02);
                comm.Parameters.AddWithValue("@sfb05", p_sfb.Sfb05);
                comm.Parameters.AddWithValue("@sfb06", p_sfb.Sfb06);
                comm.Parameters.AddWithValue("@sfb08", p_sfb.Sfb08);
                
                comm.Parameters.AddWithValue("@sfb13", p_sfb.Sfb13);
                comm.Parameters.AddWithValue("@sfb81", p_sfb.Sfb81);
                comm.Parameters.AddWithValue("@sfb223", p_sfb.Sfb223);
                comm.Parameters.AddWithValue("@sfb224", p_sfb.Sfb224);
                comm.Parameters.AddWithValue("@sfb15", p_sfb.Sfb15);
                comm.Parameters.AddWithValue("@occ02", p_sfb.Occ02);
                comm.Parameters.AddWithValue("@ima01", p_sfb.Ima01);
                comm.Parameters.AddWithValue("@ima02", p_sfb.Ima02);
                comm.Parameters.AddWithValue("@ima021", p_sfb.Ima021);
                comm.Parameters.AddWithValue("@status", p_sfb.Status);

                comm.Parameters.AddWithValue("@add_date", DateTime.Now);
                comm.Parameters.AddWithValue("@mod_date", DateTime.Now);

                comm.Parameters.AddWithValue("@sfb13m", p_sfb.Sfb13m);
                comm.Parameters.AddWithValue("@sfb15m", p_sfb.Sfb15m);
                comm.Parameters.AddWithValue("@sfb13d", p_sfb.Sfb13d);
                comm.Parameters.AddWithValue("@sfb15d", p_sfb.Sfb15d);
                comm.Parameters.AddWithValue("@sfb13w", p_sfb.Sfb13w);
                comm.Parameters.AddWithValue("@sfb15w", p_sfb.Sfb15w);

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
        }

        public static DataTable FindList(string Dept, string Sfb01, string Status)
        {
            string sql = "";
            sql += "select sfb_file.sfb01,sfb_file.sfb22,sfb_file.sfb221,sfb_file.occ02,sfb_file.ima01,sfb_file.ima02,sfb_file.ima021,sfb_file.sfb06,sfb_file.sfb08,sfb_file.sfb13,sfb_file.sfb15,sfb_file.status,sfb_file.sfb04,sfb_file.sfb223,sfb_file.sfb224," +
                "sfb_file.sfb13m,sfb_file.sfb15m," +
                "sfb_file.sfb13d,sfb_file.sfb15d," +
                "sfb_file.sfb13w,sfb_file.sfb15w" +
                " from sfb_file";
            //sql += " LEFT OUTER JOIN occ_file on occ_file.occ01 = sfb_file.sfb223";
            //sql += " LEFT OUTER JOIN ima_file on ima_file.ima01 = sfb_file.sfb05";
            sql += " where 1=1";
            if (!string.IsNullOrEmpty(Dept))
            {
                sql += " and SUBSTRING(sfb_file.sfb01,1,1)='" + Dept + "'";
            }
            if (!string.IsNullOrEmpty(Sfb01))
            {
                sql += " and sfb_file.sfb01='" + Sfb01 + "'";
            }
            if (!string.IsNullOrEmpty(Status))
            {
                sql += " and sfb_file.status='" + Status + "'";
            }
            sql += " ORDER BY sfb_file.sfb22,sfb_file.sfb221";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
            return dt;
        }


        //public static DataTable FindList(string Dept, string Sfb01, string Status)
        //{
        //    string sql = "";
        //    sql += "select sfb_file.sfb01,sfb_file.sfb22,sfb_file.sfb221,occ_file.occ02,ima_file.ima01,ima_file.ima02,ima_file.ima021,sfb_file.sfb06,sfb_file.sfb08,sfb_file.sfb13,sfb_file.sfb15,sfb_file.status,sfb_file.sfb04,sfb_file.sfb223,sfb_file.sfb224," +
        //        "sfb_file.sfb13m,sfb_file.sfb15m," +
        //        "sfb_file.sfb13d,sfb_file.sfb15d," +
        //        "sfb_file.sfb13w,sfb_file.sfb15w" +
        //        " from sfb_file";
        //    sql += " LEFT OUTER JOIN occ_file on occ_file.occ01 = sfb_file.sfb223";
        //    sql += " LEFT OUTER JOIN ima_file on ima_file.ima01 = sfb_file.sfb05";
        //    sql += " where 1=1";
        //    if (!string.IsNullOrEmpty(Dept))
        //    {
        //        sql += " and SUBSTRING(sfb_file.sfb01,1,1)='" + Dept + "'";
        //    }
        //    if (!string.IsNullOrEmpty(Sfb01))
        //    {
        //        sql += " and sfb_file.sfb01='" + Sfb01 + "'";
        //    }
        //    if (!string.IsNullOrEmpty(Status))
        //    {
        //        sql += " and sfb_file.status='" + Status + "'";
        //    }
        //    sql += " ORDER BY sfb_file.sfb22,sfb_file.sfb221";
        //    DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
        //    return dt;
        //}


        public static DataTable HaveRow(string Sfb01, string cnstr)
        {
            string sql = "";
            sql += "select * from sfb_file where 1= 1 ";
            sql += " and sfb01= '" + Sfb01 + "'";

            DataTable dt;
            dt = DOSQL.GetDataTable(cnstr, sql);
            return dt;
        }

        public static int UpdateStatus(string sfb01, string status, string cnstr)
        {
            string sql = "";
            sql += "update sfb_file set status = '" + status + "'";
            sql += " where sfb01 = '" + sfb01 + "'";
            int a = DOSQL.Excute(cnstr, sql);
            return a;
        }

        public static int Del_(string sfb01, string cnstr)
        {
            string sql = "";
            sql += "delete sfb_file where sfb01 ='" + sfb01 + "'";
            int a = DOSQL.Excute(cnstr, sql);
            return a;
        }

        public static void Upd_(sql_sfb_file p_sfb, string local)
        {
            string sql = "";
            sql += "update sfb_file set sfb02=@sfb02,sfb05=@sfb05,sfb13=@sfb13,sfb08=@sfb08,sfb04=@sfb04,sfb06=@sfb06,sfb223=@sfb223,sfb224=@sfb224,sfb15=@sfb15 ";
            sql += " where sfb01 = @sfb01";
            int i = 0;
            using (SqlConnection conn = new SqlConnection(local))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@sfb02", p_sfb.Sfb02);
                comm.Parameters.AddWithValue("@sfb05", p_sfb.Sfb05);
                comm.Parameters.AddWithValue("@sfb13", p_sfb.Sfb13);
                comm.Parameters.AddWithValue("@sfb08", p_sfb.Sfb08);
                comm.Parameters.AddWithValue("@sfb04", p_sfb.Sfb04);
                comm.Parameters.AddWithValue("@sfb06", p_sfb.Sfb06);
                comm.Parameters.AddWithValue("@sfb223", p_sfb.Sfb223);
                comm.Parameters.AddWithValue("@sfb224", p_sfb.Sfb224);
                comm.Parameters.AddWithValue("@sfb15", p_sfb.Sfb15);
                comm.Parameters.AddWithValue("@sfb01", p_sfb.Sfb01);

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
        }

    }
}
