using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using MFPD.Forms;

using MFPD.Models;

namespace MFPD
{   
    class PanelControl
    {        
        private Form sFm;
        public string formshow(string formname)
        {
            //in ('S','L')
            char[] sarray = new char[] { ',' };
            string DataRang = sst901.Get(Home.Id).Rang;
            string[] sarray2 = DataRang.Replace("in ('", "").Replace("')", "").Replace("'", "").Split(sarray);




            switch (formname)
            {
               //更改密碼
                case "ssi999":
                    ssi999 fm = new ssi999();
                    fm.Show();
                    break;

                //製造課桶數維護
                case "mfd000":
                    mfd000 mfd000 = new mfd000();
                    mfd000.Show();
                    break;

                //製造課生產排程(預計)
                case "mfd001":
                    mfd001 mfd001 = new mfd001();
                    mfd001.Show();
                    break;

                //製造課生產排程(實際)
                case "mfd002":
                    //mfd002 mfd002 = new mfd002();
                    //mfd002.Show();
                    sFm = new mfd002();
                    sFm.Show();
                    break;

                //加工課生產排程
                case "mfd003":                    
                    sFm = new mfd003();
                    sFm.Show();
                    break;


                //加工課生產排程(主)
                case "mfd005":
                    sFm = new mfd005();
                    sFm.Show();
                    break;

                //加工課生產排程(日)
                case "mfd007":
                    sFm = new mfd007();
                    sFm.Show();
                    break;

                //加工生產回饋(日)
                case "mfd011":
                    sFm = new mfd011();
                    sFm.Show();
                    break;

                //
                case "mfd013":
                    sFm = new mfd013();
                    sFm.Show();
                    break;



                //製造課生產排程表
                case "mfr001":
                    mfr001 mfr001 = new mfr001();
                    mfr001.Show();
                    break;

                //製造課生產排程表
                case "mfr002":
                    mfr002 mfr002 = new mfr002();
                    mfr002.Show();
                    break;

                //加工生產排程表(主)
                case "mfr003":
                    sFm = new mfr003();
                    sFm.Show();
                    break;

                //加工生產主排程表(生管)
                case "mfr010":
                    sFm = new mfr010();
                    sFm.Show();
                    break;
                

                //加工生產排程表(日)
                case "mfr020":
                    sFm = new mfr020();
                    sFm.Show();
                    break;
                

                //未主排程加工工單清冊
                case "mfr030":
                    sFm = new mfr030();
                    sFm.Show();
                    break;

                //生產日報表
                case "mfr040":
                    sFm = new mfr040();
                    sFm.Show();
                    break;

                //員工出勤狀況統計表
                case "mfr050":
                    sFm = new mfr050();
                    sFm.Show();
                    break;

                //排程週報表
                case "mfr060":
                    sFm = new mfr060();
                    sFm.Show();
                    break;
            }
            return null;
        }

    }
}
