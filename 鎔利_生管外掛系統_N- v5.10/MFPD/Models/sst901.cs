using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using MFPD.Forms;

namespace MFPD.Models
{
    class sst901
    {
        #region 資料屬性
        public string Erp_id { get; set; }
        public string Pr_name { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public string Vaild { get; set; }
        public string Add_date { get; set; }
        public string Add_user { get; set; }
        public string Mod_date { get; set; }
        public string Mod_user { get; set; }
        public string Rang { get; set; }
        #endregion

        public static IEnumerable<sst901> ToDoList(string Erp_id)
        {
            string sql = null;
            ArrayList parm = new ArrayList();
            sql = null;
            sql += "select * from sst901 where 1= 1 ";
            if (!string.IsNullOrEmpty(Erp_id.Trim()))
            {
                parm.Add(String.Format("%{0}%", Erp_id.Trim()));
                sql += " and erp_id like ?";
            }            
            sql += " order by erp_id ";
            DataSet DeptDS = DBConnector.executeQuery(Login.MFPD, sql, parm);
            return from p in DeptDS.Tables[0].AsEnumerable()
                   select new sst901
                   {
                       Erp_id = p.IsNull("erp_id") ? "" : p.Field<string>("erp_id").Trim(),
                       Pr_name = p.IsNull("pr_name") ? "" : p.Field<string>("pr_name").Trim(),
                       Password = p.IsNull("password") ? "" : p.Field<string>("password").Trim(),
                       Mail = p.IsNull("mail") ? "" : p.Field<string>("mail").Trim(),
                       Vaild = p.IsNull("vaild") ? "" : p.Field<string>("vaild").Trim(),
                       Add_date = p.IsNull("add_date") ? "" : p.Field<DateTime>("add_date").ToString("yyyy/MM/dd HH:mm:ss"),
                       Add_user = p.IsNull("add_user") ? "" : p.Field<string>("add_user").Trim(),
                       Mod_date = p.IsNull("mod_date") ? "" : p.Field<DateTime>("mod_date").ToString("yyyy/MM/dd HH:mm:ss"),
                       Mod_user = p.IsNull("mod_user") ? "" : p.Field<string>("mod_user").Trim(),
                       Rang = p.IsNull("rang") ? "" : p.Field<string>("rang").Trim(),
                   };
        }
        public string Insert()
        {
            try
            {
                ArrayList parm = new ArrayList();
                parm.Add(string.IsNullOrEmpty(Erp_id) ? null : Erp_id.Trim());
                parm.Add(string.IsNullOrEmpty(Pr_name) ? null : Pr_name.Trim());
                parm.Add(string.IsNullOrEmpty(Password) ? null : Password.Trim());
                parm.Add(string.IsNullOrEmpty(Mail) ? null : Mail.Trim());
                parm.Add(string.IsNullOrEmpty(Vaild) ? null : Vaild.Trim());
                parm.Add(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                parm.Add(string.IsNullOrEmpty(Add_user) ? null : Add_user.Trim());
                parm.Add(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                parm.Add(string.IsNullOrEmpty(Mod_user) ? null : Mod_user.Trim());
                parm.Add(string.IsNullOrEmpty(Rang) ? null : Rang.Trim());

                if (sst901.Get(Erp_id) != null)
                {
                    return "已有此帳號";
                }
               
                else
                {
                    string sql = null;
                    sql += "insert into sst901";
                    sql += "(erp_id,pr_name,password,mail,vaild,add_date,add_user,mod_date,mod_user,rang)";
                    sql += " values(?,?,?,?,?,?,?,?,?,?)";
                    if (DBConnector.executeSQL(Login.MFPD, sql, parm) == 0)
                        return "新增失敗";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "新增成功";
        }
        public string Delete(string Erp_id)
        {
            try
            {
                ArrayList parm = new ArrayList();
                parm.Add(Erp_id);
                string sql = null;
                sql += "delete from sst901 where erp_id=?";
                if (DBConnector.executeSQL(Login.MFPD, sql, parm) == 0)
                    return "刪除失敗";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "刪除成功";
        }
        public string Update(string Erp_id)
        {
            try
            {
                ArrayList parm = new ArrayList();
                parm.Add(string.IsNullOrEmpty(Pr_name) ? null : Pr_name.Trim());
                parm.Add(string.IsNullOrEmpty(Password) ? null : Password.Trim());
                parm.Add(string.IsNullOrEmpty(Mail) ? null : Mail.Trim());
                parm.Add(string.IsNullOrEmpty(Vaild) ? null : Vaild.Trim());
                parm.Add(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                parm.Add(string.IsNullOrEmpty(Mod_user) ? null : Mod_user.Trim());
                parm.Add(string.IsNullOrEmpty(Rang) ? null : Rang.Trim());

                parm.Add(string.IsNullOrEmpty(Erp_id) ? null : Erp_id.Trim());
                string sql = null;
                sql += "update sst901 set pr_name=?,password=?,mail=?,vaild=?,mod_date=?,mod_user=?,rang=?";
                sql += " where erp_id =?";
                if (DBConnector.executeSQL(Login.MFPD, sql, parm) == 0)
                    return "修改失敗";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
            return "修改成功";
        }

        //public static sst901 Get(string Erp_id)
        //{
        //    // 查詢資料庫資料
        //    string sql = null;
        //    sql += "select * from sst901 ";
        //    sql += " where erp_id = '" + Erp_id + "'";
        //    sql += " and vaild='Y'";
        //    DataTable dt = DOSQL.GetDataTable(Login.MFPD, sql);            
        //    // 將查詢到的資料回傳
        //    if (dt.Rows.Count == 0)
        //        return null;
        //    return new sst901
        //    {
        //        Erp_id = dt.Rows[0].IsNull("erp_id") ? "" : dt.Rows[0].Field<string>("erp_id").Trim(),
        //        Pr_name = dt.Rows[0].IsNull("pr_name") ? "" : dt.Rows[0].Field<string>("pr_name").Trim(),
        //        Password = dt.Rows[0].IsNull("password") ? "" : dt.Rows[0].Field<string>("password").Trim(),
        //        Mail = dt.Rows[0].IsNull("mail") ? "" : dt.Rows[0].Field<string>("mail").Trim(),
        //        Vaild = dt.Rows[0].IsNull("vaild") ? "" : dt.Rows[0].Field<string>("vaild").Trim(),
        //        Add_date = dt.Rows[0].IsNull("add_date") ? "" : dt.Rows[0].Field<DateTime>("add_date").ToString("yyyy/MM/dd HH:mm:ss"),
        //        Add_user = dt.Rows[0].IsNull("add_user") ? "" : dt.Rows[0].Field<string>("add_user").Trim(),
        //        Mod_date = dt.Rows[0].IsNull("mod_date") ? "" : dt.Rows[0].Field<DateTime>("mod_date").ToString("yyyy/MM/dd HH:mm:ss"),
        //        Mod_user = dt.Rows[0].IsNull("mod_user") ? "" : dt.Rows[0].Field<string>("mod_user").Trim(),
        //        Rang = dt.Rows[0].IsNull("rang") ? "" : dt.Rows[0].Field<string>("rang").Trim(),
        //    };
        //}


        public static sst901 Get(string Erp_id)
        {
            // 查詢資料庫資料
            ArrayList parm = new ArrayList();
            parm.Add(Erp_id);

            string sql = null;
            sql += "select * from sst901 ";
            sql += " where erp_id = ? ";
            sql += " and vaild='Y'";
            DataSet DeptDS = DBConnector.executeQuery(Login.MFPD, sql, parm);
            // 將查詢到的資料回傳
            if (DeptDS.Tables[0].Rows.Count == 0)
                return null;
            return new sst901
            {
                Erp_id = DeptDS.Tables[0].Rows[0].IsNull("erp_id") ? "" : DeptDS.Tables[0].Rows[0].Field<string>("erp_id").Trim(),
                Pr_name = DeptDS.Tables[0].Rows[0].IsNull("pr_name") ? "" : DeptDS.Tables[0].Rows[0].Field<string>("pr_name").Trim(),
                Password = DeptDS.Tables[0].Rows[0].IsNull("password") ? "" : DeptDS.Tables[0].Rows[0].Field<string>("password").Trim(),
                Mail = DeptDS.Tables[0].Rows[0].IsNull("mail") ? "" : DeptDS.Tables[0].Rows[0].Field<string>("mail").Trim(),
                Vaild = DeptDS.Tables[0].Rows[0].IsNull("vaild") ? "" : DeptDS.Tables[0].Rows[0].Field<string>("vaild").Trim(),
                Add_date = DeptDS.Tables[0].Rows[0].IsNull("add_date") ? "" : DeptDS.Tables[0].Rows[0].Field<DateTime>("add_date").ToString("yyyy/MM/dd HH:mm:ss"),
                Add_user = DeptDS.Tables[0].Rows[0].IsNull("add_user") ? "" : DeptDS.Tables[0].Rows[0].Field<string>("add_user").Trim(),
                Mod_date = DeptDS.Tables[0].Rows[0].IsNull("mod_date") ? "" : DeptDS.Tables[0].Rows[0].Field<DateTime>("mod_date").ToString("yyyy/MM/dd HH:mm:ss"),
                Mod_user = DeptDS.Tables[0].Rows[0].IsNull("mod_user") ? "" : DeptDS.Tables[0].Rows[0].Field<string>("mod_user").Trim(),
                Rang = DeptDS.Tables[0].Rows[0].IsNull("rang") ? "" : DeptDS.Tables[0].Rows[0].Field<string>("rang").Trim(),
            };
        }


        public static sst901 GetMail(string Erp_id)
        {
            // 查詢資料庫資料
            ArrayList parm = new ArrayList();
            parm.Add(Erp_id);

            string sql = null;
            sql += "select mail from sst901 ";
            sql += " where erp_id = ? ";
            DataSet DeptDS = DBConnector.executeQuery(Login.MFPD, sql, parm);
            // 將查詢到的資料回傳
            if (DeptDS.Tables[0].Rows.Count == 0)
                return null;
            return new sst901
            {     
                Mail = DeptDS.Tables[0].Rows[0].IsNull("mail") ? "" : DeptDS.Tables[0].Rows[0].Field<string>("mail").Trim(),    

            };
        }
        public static sst901 Singin(string Erp_id,string Password)
        {
            // 查詢資料庫資料
            ArrayList parm = new ArrayList();
            parm.Add(Erp_id);
            parm.Add(Password);
            string sql = null;
            sql += "select pr_name  from sst901 ";
            sql += " where erp_id = ? and password = ?";
            DataSet DeptDS = DBConnector.executeQuery(Login.MFPD, sql, parm);
            // 將查詢到的資料回傳
            if (DeptDS.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            return new sst901
            {
                Pr_name = DeptDS.Tables[0].Rows[0].IsNull("pr_name") ? "" : DeptDS.Tables[0].Rows[0].Field<string>("pr_name").Trim(),
            };
        }
        public static IEnumerable<sst901> gridviewErpid(string Vaild)
        {
            string sql = null;
            ArrayList parm = new ArrayList();
            sql = null;
            sql += "select erp_id,pr_name,mail from sst901 where 1= 1 ";
            if (Vaild.Length > 0)
            {
                parm.Add(Vaild.Trim());
                sql += " and vaild= ?";
            }
            sql += " order by erp_id ";

            DataSet DeptDS = DBConnector.executeQuery(Login.MFPD, sql, parm);
            return from p in DeptDS.Tables[0].AsEnumerable()
                   select new sst901
                   {
                       Erp_id = p.IsNull("erp_id") ? "" : p.Field<string>("erp_id").Trim(),
                       Pr_name = p.IsNull("pr_name") ? "" : p.Field<string>("pr_name").Trim(),
                       Mail = p.IsNull("mail") ? "" : p.Field<string>("mail").Trim(),
                   };
        }

        public static IEnumerable<sst901> gridviewErpid(string Vaild, string UserId)
        {
            string sql = null;
            ArrayList parm = new ArrayList();
            sql = null;
            sql += "select erp_id,pr_name,mail from sst901 where 1= 1 ";
            if (Vaild.Length > 0)
            {
                parm.Add(Vaild.Trim());
                sql += " and vaild= ?";
            }
            if (UserId.Length > 0)
            {
                parm.Add(UserId.Trim());
                sql += " and erp_id != ?";
            }
            sql += " order by erp_id ";

            DataSet DeptDS = DBConnector.executeQuery(Login.MFPD, sql, parm);
            return from p in DeptDS.Tables[0].AsEnumerable()
                   select new sst901
                   {
                       Erp_id = p.IsNull("erp_id") ? "" : p.Field<string>("erp_id").Trim(),
                       Pr_name = p.IsNull("pr_name") ? "" : p.Field<string>("pr_name").Trim(),
                       Mail = p.IsNull("mail") ? "" : p.Field<string>("mail").Trim(),
                   };
        }


    }
}
