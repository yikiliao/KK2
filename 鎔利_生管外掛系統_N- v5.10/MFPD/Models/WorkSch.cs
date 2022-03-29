using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using MFPD.Forms;

namespace MFPD.Models
{
    class WorkSch
    {
        #region 資料屬性
        public string Sfb01 { get; set; }//工單
        public string Ecb03 { get; set; }//序
        public string Schdate { get; set; }//排程日期
        public string Ecm04 { get; set; }//作業編號
        public string Ecm45 { get; set; }
        public string Ecm06 { get; set; }//製程站
        public string Eca02 { get; set; }

        public decimal Sfb08 { get; set; } //生產數
        public string Sfb81 { get; set; }//輸入日期
        public string Sfb223 { get; set; }//客戶代碼
        public string Occ02 { get; set; } //客戶名稱
        public string Ima01 { get; set; } //料號
        public string Ima02 { get; set; } //品名
        public string Ima021 { get; set; } //規格
        public string Sfb224 { get; set; } //客戶訂單號
        public string Sfb15 { get; set; }//客戶交期

        #endregion

        public static DataTable ToList(string Dept, string Ecm04, string Begdate, string EndDate)
        {
            string sql = string.Empty;
            sql += "SELECT procscm.sfb01,procscm.ecb03,procscm.schdate,procscm.ecm04,procscm.ecm45,procscm.ecm06,procscm.eca02,";
            sql += "sfb_file.sfb08,sfb_file.sfb81,sfb_file.sfb223,sfb_file.occ02,sfb_file.ima01,sfb_file.ima02,sfb_file.ima021,sfb_file.sfb224,sfb_file.sfb15 from procscm";
            sql += " LEFT OUTER JOIN sfb_file on sfb_file.sfb01 = procscm.sfb01";            
            sql += " WHERE 1=1";
            sql += " and SUBSTRING(procscm.sfb01,1,1)='" + Dept + "'";
            if (Ecm04.Length > 0)
            {
                sql += " and procscm.ecm04 = '" + Ecm04 + "'";
            }
            sql += " and procscm.schdate BETWEEN '" + Begdate + "' and '" + EndDate + "'";
            sql += " ORDER BY procscm.sfb01";
            DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
            return dt;
        }


        //public static DataTable ToList(string Dept, string Ecm04, string Begdate,string EndDate)
        //{
        //    string sql = string.Empty;            
        //    sql += "SELECT procscm.sfb01,procscm.ecb03,procscm.schdate,procscm.ecm04,procscm.ecm45,procscm.ecm06,procscm.eca02,";
        //    sql += "sfb_file.sfb08,sfb_file.sfb81,sfb_file.sfb223,occ_file.occ02,ima_file.ima01,ima_file.ima02,ima_file.ima021,sfb_file.sfb224,sfb_file.sfb15 from procscm";
        //    sql += " LEFT OUTER JOIN sfb_file on sfb_file.sfb01 = procscm.sfb01";
        //    sql += " LEFT OUTER JOIN occ_file on occ_file.occ01 = sfb_file.sfb223";
        //    sql += " LEFT OUTER JOIN ima_file on ima_file.ima01 = sfb_file.sfb05";
        //    sql += " WHERE 1=1";
        //    sql += " and SUBSTRING(procscm.sfb01,1,1)='" + Dept + "'";
        //    if (Ecm04.Length > 0)
        //    {
        //        sql += " and procscm.ecm04 = '" + Ecm04 + "'";
        //    }
        //    sql += " and procscm.schdate BETWEEN '" + Begdate + "' and '" + EndDate + "'";            
        //    sql += " ORDER BY procscm.sfb01";
        //    DataTable dt = DOSQL.GetDataTable(Login.WU, sql);
        //    return dt;
        //}

    }
}
