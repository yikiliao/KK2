using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MFPD.Forms;
using System.Windows.Forms;

namespace MFPD.Models
{
    class Mfr009
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
        #endregion

        public static DataTable unToDoList(string dept, string Begdate, string Enddate)
        {
            //--加入table 放資料
            DataTable dtProd = new DataTable(); //產品資料            
            dtProd.Columns.Add("sfb01", typeof(String));
            dtProd.Columns.Add("occ02", typeof(String));
            dtProd.Columns.Add("ima01", typeof(String));
            dtProd.Columns.Add("ima02", typeof(String));
            dtProd.Columns.Add("ima021", typeof(String));
            dtProd.Columns.Add("sfb06", typeof(String));
            dtProd.Columns.Add("sfb08", typeof(String));
            dtProd.Columns.Add("sfb13", typeof(String));
            dtProd.Columns.Add("sfb15", typeof(String));
            dtProd.Columns.Add("sfb04", typeof(String));
            dtProd.Columns.Add("sfb223", typeof(String));
            dtProd.Columns.Add("sfb224", typeof(String));
            //------------------

            DataTable dt = sfb_file.ToDoList(dept, Begdate, Enddate); //前一面傳過來的作業名稱代碼

            Int16 j = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sfb01 = "'" + dt.Rows[i]["sfb01"].ToString() + "'";
                if (Procsca.ToDoList(sfb01).Rows.Count > 0)
                    continue;
                else
                {
                    j += 1;
                    DataRow dr = dtProd.NewRow();
                    dr["sfb01"] = dt.Rows[i]["sfb01"].ToString();
                    dr["occ02"] = dt.Rows[i]["occ02"].ToString();
                    dr["ima01"] = dt.Rows[i]["ima01"].ToString();
                    dr["ima02"] = dt.Rows[i]["ima02"].ToString();
                    dr["ima021"] = dt.Rows[i]["ima021"].ToString();
                    dr["sfb06"] = dt.Rows[i]["sfb06"].ToString();
                    dr["sfb08"] = dt.Rows[i]["sfb08"].ToString();
                    dr["sfb13"] = dt.Rows[i]["sfb13"].ToString();
                    dr["sfb15"] = dt.Rows[i]["sfb15"].ToString();
                    dtProd.Rows.Add(dr);
                }
            }
            return dtProd;
        }


        //public static DataTable unToDoList(string dept, string Begdate, string Enddate)
        //{
        //    string rf = string.Empty;
        //    if (dept == "J") rf = "JS012%";
        //    if (dept == "W") rf = "WS012%";
        //    string sql = "";
        //    sql += "SELECT sfb01,occ02,ima01,ima02,ima021,sfb02,sfb06,sfb08,sfb13,sfb15 from sfb_file";
        //    sql += " LEFT OUTER JOIN ima_file on ima01 = sfb_file.sfb05";
        //    sql += " LEFT OUTER JOIN occ_file on occ01 = sfb_file.sfb223";
        //    sql += " WHERE 1=1";
        //    sql += " and sfb_file.sfb01 like ('" + rf + "')";
        //    sql += " and sfb_file.status = '0'";
        //    sql += " and sfb_file.sfb15 BETWEEN '" + Begdate + "' and '" + Enddate + "'";
        //    sql += " ORDER BY sfb_file.sfb01";
        //    DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
        //    return dt;
        //}
    }
}
