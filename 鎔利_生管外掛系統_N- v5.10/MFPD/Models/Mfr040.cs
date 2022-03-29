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
    class Mfr040
    {
        #region 資料屬性
        public string Doc { get; set; }//單據號
        public string Shb02 { get; set; }//機台開始日期
        public string Shb021 { get; set; }//機台開始時間
        public string Shb03 { get; set; }//機台結束日期
        public string Shb031 { get; set; }//機台結束時間
        public decimal Shb032 { get; set; }
        public decimal Shb033 { get; set; }
        public string Shb04 { get; set; }//工作站代碼
        public string Shb05 { get; set; }//工號
        public Int32 Shb06 { get; set; }//製程序
        

        public string Shb07 { get; set; }
        public string Shb08 { get; set; }
        public string Shb081 { get; set; }
        public string Shb082 { get; set; }
        public string Shb09 { get; set; }
        public string Shb10 { get; set; }
        public decimal Shb111 { get; set; }//成品數
        public decimal Shb112 { get; set; }//不良數
        public decimal Shb115 { get; set; }//BONUS數
        public string Ima01 { get; set; }//料號

        public string Ima02 { get; set; }
        public string Ima021 { get; set; }
        public string Occ02 { get; set; }//客戶
        public string Sfb224 { get; set; }//客戶訂單
        public string Eci06 { get; set; }//機台名稱
        public string Eca02 { get; set; }//工作站名稱
        public int Cnt { get; set; }//操作人數
        #endregion

        //public static DataTable ToDoList(string Dept, string rCdept, string begDate, string endDate)
        //{
        

        //    string sql = string.Empty;
        //    sql += "SELECT InProc.*,sfb_file.ima01,sfb_file.ima02,sfb_file.ima021,sfb_file.occ02,sfb_file.sfb224,eci_file.eci06,eca_file.eca02 from InProc";            
        //    sql += " LEFT OUTER JOIN sfb_file on sfb_file.sfb01 = InProc.shb05";
        //    sql += " LEFT OUTER JOIN eca_file on eca_file.eca01 = InProc.shb04";
        //    sql += " LEFT OUTER JOIN eci_file on eci_file.eci01 = InProc.shb09";
        //    sql += " WHERE 1=1";
        //    sql += " and SUBSTRING(InProc.doc,1,1)='" + Dept + "'";
        //    if (!string.IsNullOrEmpty(rCdept))
        //    {
        //        sql += " and InProc.shb04 = '" + rCdept + "'";
        //    }
        //    sql += " and  InProc.shb02 BETWEEN '" + begDate + "' and '" + endDate + "'";
        //    sql += " order by InProc.shb02,InProc.shb021";
        //    DataTable dt = DOSQL.GetDataTable(Login.WU, sql);

        

        //    return dt;
        //}

        public static DataTable ToDoList(string Dept, string rCdept, string begDate, string endDate,DataTable dtProc)
        {             
            string sql = string.Empty;
            sql += "SELECT InProc.*,sfb_file.ima01,sfb_file.ima02,sfb_file.ima021,sfb_file.occ02,sfb_file.sfb224,eci_file.eci06,eca_file.eca02 from InProc";
            sql += " LEFT OUTER JOIN sfb_file on sfb_file.sfb01 = InProc.shb05";
            sql += " LEFT OUTER JOIN eca_file on eca_file.eca01 = InProc.shb04";
            sql += " LEFT OUTER JOIN eci_file on eci_file.eci01 = InProc.shb09";
            sql += " WHERE 1=1";
            sql += " and SUBSTRING(InProc.doc,1,1)='" + Dept + "'";
            if (!string.IsNullOrEmpty(rCdept))
            {
                sql += " and InProc.shb04 = '" + rCdept + "'";
            }
            sql += " and  InProc.shb02 BETWEEN '" + begDate + "' and '" + endDate + "'";
            sql += " order by InProc.shb02,InProc.shb021";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
            DataTable dt2 =  In_dtProc(dt, dtProc);
            return dt2;
        }

        private static DataTable In_dtProc(DataTable dt, DataTable dtProc)
        {
            dtProc.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {                
                DataRow dr = dtProc.NewRow();
                dr["Doc"] = dt.Rows[i]["doc"].ToString();
                dr["Shb02"] = dt.Rows[i]["shb02"].ToString();
                dr["Shb021"] = dt.Rows[i]["shb021"].ToString();
                dr["Shb03"] = dt.Rows[i]["shb03"].ToString();
                dr["Shb031"] = dt.Rows[i]["shb031"].ToString();
                dr["Shb032"] = System.Convert.ToDecimal(dt.Rows[i]["shb032"].ToString());
                dr["Shb033"] = System.Convert.ToDecimal(dt.Rows[i]["shb033"].ToString());
                dr["Shb04"] = dt.Rows[i]["shb04"].ToString();
                dr["Shb05"] = dt.Rows[i]["shb05"].ToString();
                dr["Shb06"] = System.Convert.ToInt32(dt.Rows[i]["shb06"].ToString());

                dr["Shb07"] = dt.Rows[i]["shb07"].ToString();
                dr["Shb08"] = dt.Rows[i]["shb08"].ToString();                
                dr["Shb081"] = dt.Rows[i]["shb08"].ToString();
                dr["Shb082"] = dt.Rows[i]["shb08"].ToString();
                dr["Shb09"] = dt.Rows[i]["shb09"].ToString();
                dr["Shb10"] = dt.Rows[i]["shb10"].ToString();
                dr["Shb111"] = System.Convert.ToDecimal(dt.Rows[i]["shb111"].ToString());
                dr["Shb112"] = System.Convert.ToDecimal(dt.Rows[i]["shb112"].ToString());
                dr["Shb115"] = System.Convert.ToDecimal(dt.Rows[i]["shb115"].ToString());
                dr["Ima01"] = dt.Rows[i]["ima01"].ToString();

                dr["Ima02"] = dt.Rows[i]["ima02"].ToString();
                dr["Ima021"] = dt.Rows[i]["ima021"].ToString();
                dr["Occ02"] = dt.Rows[i]["occ02"].ToString();
                dr["Sfb224"] = dt.Rows[i]["sfb224"].ToString();
                dr["Eci06"] = dt.Rows[i]["eci06"].ToString();
                dr["Eca02"] = dt.Rows[i]["eca02"].ToString();
                dr["Cnt"] = getCnt(dt.Rows[i]["doc"].ToString(),
                    dt.Rows[i]["shb05"].ToString(),
                    System.Convert.ToInt32(dt.Rows[i]["shb06"].ToString()));

                dtProc.Rows.Add(dr);
            }
            return dtProc;
        }

        private static Int32 getCnt(string Doc,string Shb05,Int32 Shb06)
        {
            Int32 rf = 0;
            string sql = string.Empty;
            sql += " SELECT count(*) cnt from InEdf where 1=1";
            sql += " and InEdf.doc ='" + Doc + "'";
            sql += " and InEdf.edf03='" + Shb05 + "'";
            sql += " and InEdf.edf07=" + Shb06;
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
            rf = System.Convert.ToInt32(dt.Rows[0]["cnt"].ToString());
            return rf;
        }
    }
}
