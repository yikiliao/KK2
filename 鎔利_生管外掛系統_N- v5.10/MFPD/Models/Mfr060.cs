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
    class Mfr060
    {
        #region 資料屬性
        public string Sfb01 { get; set; }//單據號
        public string Sfb22 { get; set; }//訂單
        public string Sfb221 { get; set; }//訂單項次
        public string Ima01 { get; set; }//料號
        public string Ima02 { get; set; }//
        public string Ima021 { get; set; }
        public string Occ02 { get; set; }//客戶
        public decimal Sfb08 { get; set; }//派工數
        public Int32 Weekname { get; set; }//星期幾
        public string Schdate { get; set; }//開工日
        public string Ecm04 { get; set; }//製成代碼
        public string Ecm45 { get; set; }//製成說明
        public string Ecm06 { get; set; }//工作站代碼
        public string Eca02 { get; set; }//工作站說明
        public string Sfb224 { get; set; }//客戶訂單號
        public Int32 Ecb03 { get; set; }//工序
        #endregion

        public static DataTable ToDoList(string Dept, string rCdept, string begDate, string endDate, DataTable dtProc)
        {
            string sql = string.Empty;
            sql += "select sfb_file.sfb01,sfb_file.sfb22,sfb_file.sfb221,sfb_file.ima01,sfb_file.ima02,sfb_file.ima021,sfb_file.occ02,sfb_file.sfb08";
            sql += " ,DATEPART(WEEKDAY, procscmwork.schdate) weekname,";
            sql += " procscmwork.schdate,procscmwork.ecm04,procscmwork.ecm45,procscmwork.ecm06,procscmwork.eca02,sfb_file.sfb224,procscmwork.ecb03";
            sql += " from sfb_file";
            sql += " LEFT OUTER JOIN procscmwork on procscmwork.sfb01 = sfb_file.sfb01";
            sql += " WHERE 1=1";
            sql += " and SUBSTRING(sfb_file.sfb01,1,1)='" + Dept + "'";
            sql += " and sfb_file.status='3'";
            if (!string.IsNullOrEmpty(rCdept))
            {
                sql += " and procscmwork.ecm06 = '" + rCdept + "'";
            }
            sql += " and  procscmwork.schdate BETWEEN '" + begDate + "' and '" + endDate + "'";
            sql += " ORDER BY procscmwork.ecb03,procscmwork.schdate,sfb_file.sfb01";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
            DataTable dt2 = In_dtProc(dt, dtProc);            
            return dt2;
        }

        private static DataTable In_dtProc(DataTable dt, DataTable dtProc)
        {
            dtProc.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dtProc.NewRow();
                dr["Sfb01"] = dt.Rows[i]["sfb01"].ToString();
                dr["Sfb22"] = dt.Rows[i]["sfb22"].ToString();
                dr["Sfb221"] = dt.Rows[i]["sfb221"].ToString();
                dr["Ima01"] = dt.Rows[i]["ima01"].ToString();
                dr["Ima02"] = dt.Rows[i]["ima02"].ToString();
                dr["Ima021"] = dt.Rows[i]["ima021"].ToString();
                dr["Occ02"] = dt.Rows[i]["occ02"].ToString();
                dr["Sfb08"] = System.Convert.ToDecimal(dt.Rows[i]["sfb08"].ToString());
                dr["Weekname"] = System.Convert.ToInt32(dt.Rows[i]["weekname"].ToString());
                dr["Schdate"] = dt.Rows[i]["schdate"].ToString();
                dr["Ecm04"] = dt.Rows[i]["ecm04"].ToString();
                dr["Ecm45"] = dt.Rows[i]["ecm45"].ToString();
                dr["Ecm06"] = dt.Rows[i]["ecm06"].ToString();
                dr["Eca02"] = dt.Rows[i]["eca02"].ToString();
                dr["Sfb224"] = dt.Rows[i]["sfb224"].ToString();
                dr["Ecb03"] = System.Convert.ToInt32(dt.Rows[i]["ecb03"].ToString());
                dtProc.Rows.Add(dr);
            }
            return dtProc;
        }

    }
}
