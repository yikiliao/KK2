using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MFPD.Forms;

namespace MFPD.Models
{
    class Mfr050
    {
        #region 資料屬性
        public string Doc { get; set; }
        public string Stid { get; set; }//部門代碼
        public string Stname { get; set; }//部門說明
        public string Empid { get; set; }//工號
        public string Empname { get; set; }
        public string Rdt { get; set; }//系統日期
        public string Bdate { get; set; }
        public string Bhh { get; set; }
        public string Bmm { get; set; }
        public string Edate { get; set; }
        public string Ehh { get; set; }
        public string Emm { get; set; }
        public decimal Worktime { get; set; }//工時
        public string Class { get; set; }//
        public string Machno { get; set; }//機台編號
        public string Eci06 { get; set; }//機台名稱
        public string Ecg02 { get; set; }//班別
        public string Edf03 { get; set; }//工單       
        public string Ima01 { get; set; }//料號
        public string Ima02 { get; set; }//品名
        public string Ima021 { get; set; }//規格
        public string Occ02 { get; set; }//客戶
        public string Sfb224 { get; set; }//客戶訂單
        public int Shb06 { get; set; } //製程序
        public decimal Shb111 { get; set; }//良品
        public decimal Shb112 { get; set; }//不良
        public decimal Shb115 { get; set; }//Bonus
        #endregion

        public static DataTable ToDoList(string Dept, string rCdept, string begDate, string endDate)
        {
            string sql = string.Empty;
            sql += "SELECT emptime.*,emphead.class,emphead.machno,eci_file.eci06,ecg_file.ecg02,InEdf.edf03,sfb_file.ima01,sfb_file.ima02,sfb_file.ima021,sfb_file.occ02,sfb_file.sfb224 ";
            sql += ",InProc.shb06,InProc.shb111,InProc.shb112,InProc.shb115";
            sql += " from emptime ";
            sql += " LEFT OUTER JOIN emphead on emphead.doc = emptime.doc";
            sql += " LEFT OUTER JOIN eci_file on eci_file.eci01 = emphead.machno";
            sql += " LEFT OUTER JOIN ecg_file on ecg_file.ecg01 = emphead.class";
            sql += " LEFT OUTER JOIN InEdf on InEdf.doc = emptime.doc and InEdf.edf09 = emptime.empid";
            sql += " LEFT OUTER JOIN sfb_file on sfb_file.sfb01 = InEdf.edf03";
            sql += " LEFT OUTER JOIN InProc on InProc.doc = emptime.doc and InProc.shb05 = InEdf.edf03";
            sql += " WHERE 1=1";
            sql += " and SUBSTRING(emptime.doc,1,1)='" + Dept + "'";
            if (!string.IsNullOrEmpty(rCdept))
            {
                sql += " and emptime.stid = '" + rCdept + "'";
            }
            sql += " and  emptime.bdate BETWEEN '" + begDate + "' and '" + endDate + "'";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
            return dt;
        }


    }
}
