using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;

using Advtek.Utility;
using System.Threading;

namespace SO_UPLOAD_LOG
{
    class Program
    {
        static string sMSG = "";
        static string sCacheSql = "";

        static void Main(string[] args)
        {
            //初始化LOG
            OutputMsg("1.SO_UPLOAD_LOG開始");
            OutputMsg("2.初始化LOG");
            ConvertLog con_log = new ConvertLog("SO_UPLOAD_LOG");

            try
            {
                //eStore獨賣件貨到門市驗收
                OutputMsg("3.eStore獨賣件貨到門市驗收");
                BS();
                //網購訂單門市銷售結帳
                OutputMsg("4.網購訂單門市銷售結帳");
                BSS();
                //網購訂單門市銷售結帳作廢
                OutputMsg("5.網購訂單門市銷售結帳作廢");
                BSN();
                //網購件未取件
                OutputMsg("6.網購件未取件");
                BR();

                OutputMsg("7.執行結束，寫入LOG");
                con_log.Success(sMSG);
                Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
                //失敗資訊
                OutputMsg(ex.Message);
                
                con_log.Fail(sMSG);
                Thread.Sleep(5000);
            }
        }

        static void BS() 
        {
            OracleConnection wcon = null;
            OracleConnection pcon = null;
            OracleTransaction wotx = null;
            OracleTransaction potx = null;
            StringBuilder sb = new StringBuilder();
            int iTEN = 0;
            int iCount = 0;
            try {
                //BS建立新舊連線
                OutputMsg("3.1 BS建立新舊連線");
                wcon = OracleDBUtil.GetConnection();
                pcon = OracleDBUtil.GetERPPOSConnection();
                //查詢SOM，bs_flag=1,bs_date is null
                OutputMsg("3.2 查詢SOM，bs_flag=1,bs_date is null");
                sb.Append("SELECT SOID,PACKAGE_NO,SONO,CREATE_DTM FROM SOM WHERE BS_FLAG='1' AND BS_DATE IS NULL AND PACKAGE_NO IS NOT NULL AND SONO IS NOT NULL ");
                DataTable dt = SelectTable(sb.ToString(), wcon);
                //回寫POS，By package_no,sono
                OutputMsg("3.3 回寫POS，By package_no,sono");
                
                if(dt.Rows.Count>0)
                {
                    potx=pcon.BeginTransaction();
                    sb.Length = 0;
                    foreach(DataRow dr in dt.Rows){
                        if (iTEN > 400)
                        {
                            
                            sb.Insert(0, "BEGIN ");
                            sb.Append(" END;");
                            
                            ExeSql(sb.Replace("\r", " ").Replace("\n", " ").ToString(), potx);
                            sb.Length = 0;
                            iTEN = 0;
                        }
                        sb.Append(" UPDATE SOM SET BS_FLAG='1',BS_DATE=SYSDATE,MODIUSER='CONVERT',MODIDATE=SYSDATE WHERE PACKAGE_NO=" + OracleDBUtil.SqlStr(dr["PACKAGE_NO"].ToString())
                                 + " AND SONO=" + OracleDBUtil.SqlStr(dr["SONO"].ToString()) + "; ");
                        iCount++;
                        iTEN++;
                    }
                    if (sb.Length > 0) {
                        sb.Insert(0, "BEGIN ");
                        sb.Append(" END;");
                        ExeSql(sb.Replace("\r", " ").Replace("\n", " ").ToString(), potx);
                    }
                    potx.Commit();
                    OutputMsg("  SOM(POS) 更新筆數:" + iCount.ToString());
                    //回寫SOM(WEB)，BS_DATE by soid
                    OutputMsg("3.4 回寫SOM(WEB)，BS_DATE by soid");
                    sb.Length = 0;
                    iTEN = 0;
                    iCount = 0;
                    wotx = wcon.BeginTransaction();
                    //StringBuilder sb2 = new StringBuilder();
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (iTEN > 400) 
                        {
                            sb.Insert(0, "BEGIN ");
                            sb.Append(" END;");
                            ExeSql(sb.ToString().Replace("\r", " ").Replace("\n", " "), wotx);
                            iTEN = 0;
                            sb.Length = 0;
                        }
                        //UPDATE SOM
                        sb.Append(" UPDATE SOM SET BS_DATE=SYSDATE,MODIUSER='CONVERT',MODIDATE=SYSDATE WHERE soid=" + OracleDBUtil.SqlStr(dr["soid"].ToString()) + "; ");
                        //INSERT SO_UPDATE_LOG
                        string sTmp = string.Format(@" INSERT INTO SO_UPDATE_LOG (
                                     SONO, STATUS, TRANS_DATE, DWNFLAG,SOID, CREATE_USER,CREATE_DTM, MODI_USER, MODI_DTM)
                                     VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8}); "
                                     , OracleDBUtil.SqlStr(dr["SONO"].ToString()), "'1'", (dr["CREATE_DTM"] != DBNull.Value) ? OracleDBUtil.DateFormate(Convert.ToDateTime(dr["CREATE_DTM"].ToString())) : "NULL"
                                     , "'0'", OracleDBUtil.SqlStr(dr["SOID"].ToString()) , "'CONVERT'" , "SYSDATE","'CONVERT'","SYSDATE");
                        sb.Append(sTmp);
                        iCount++;
                        iTEN++;
                    }
                    if (sb.Length > 0) 
                    {
                        sb.Insert(0, "BEGIN ");
                        sb.Append(" END;");
                        ExeSql(sb.ToString().Replace("\r", " ").Replace("\n", " "), wotx);
                    }
                    wotx.Commit();
                    OutputMsg("  SOM(WEB) ，SO_UPDATE_LOG，更新筆數:" + iCount.ToString());

                }else
                {
                    //BS查無資料
                    OutputMsg("3.4 BS查無資料，SOM(WEB) ，SO_UPDATE_LOG，更新筆數:0");
                }
            }
            catch (Exception ex) {
                OutputMsg("3.eStore獨賣件貨到門市驗收，產生例外");    
                throw ex; 
            }
            finally {
                if (potx != null) potx.Dispose();
                if (wotx != null) wotx.Dispose();
                if (pcon.State == ConnectionState.Open) pcon.Close();
                pcon.Dispose();
                if (wcon.State == ConnectionState.Open) wcon.Close();
                wcon.Dispose();
                OracleConnection.ClearAllPools();
            }
            
        }

        static void BSS()
        {
            OracleConnection wcon = null;
            OracleConnection pcon = null;
            OracleTransaction wotx = null;
            OracleTransaction potx = null;
            StringBuilder sb = new StringBuilder();
            try
            {
                //BSS建立新舊連線
                OutputMsg("4.1 BSS建立新舊連線");
                wcon = OracleDBUtil.GetConnection();
                pcon = OracleDBUtil.GetERPPOSConnection();
                
                //查詢SOM，bs_flag=1,bs_date is null
                OutputMsg("4.2 查詢SOM，bss_flag=1,bss_date is null");
                sb.Append("SELECT SOID,PACKAGE_NO,SONO,CREATE_DTM FROM SOM WHERE BSS_FLAG='1' AND BSS_DATE IS NULL AND PACKAGE_NO IS NOT NULL AND SONO IS NOT NULL ");
                DataTable dt = SelectTable(sb.ToString(), wcon);
                //回寫POS，By package_no,sono
                OutputMsg("4.3 回寫POS，By package_no,sono");
                int iCount=0,iTEN=0 ;
                if (dt.Rows.Count > 0)
                {
                    potx = pcon.BeginTransaction();
                    sb.Length = 0;

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (iTEN > 400) 
                        {
                            sb.Insert(0, "BEGIN ");
                            sb.Append(" END;");
                            ExeSql(sb.Replace("\r", " ").Replace("\n", " ").ToString(), potx);
                            sb.Length = 0;
                            iTEN = 0;
                        }
                        sb.Append(" UPDATE SOM SET BSS_FLAG='1',BSS_DATE=SYSDATE,MODIUSER='CONVERT',MODIDATE=SYSDATE WHERE PACKAGE_NO=" + OracleDBUtil.SqlStr(dr["PACKAGE_NO"].ToString())
                                 + " AND SONO=" + OracleDBUtil.SqlStr(dr["SONO"].ToString()) + "; ");
                        iTEN++;
                        iCount++;
                    }

                    if (sb.Length > 0) {
                        sb.Insert(0, "BEGIN ");
                        sb.Append(" END;");
                        ExeSql(sb.Replace("\r", " ").Replace("\n", " ").ToString(), potx);
                        sb.Length = 0;
                        iTEN = 0;
                    }
                    potx.Commit();
                    OutputMsg("  SOM(POS)，更新筆數:" + iCount.ToString());
                    //回寫SOM(WEB)，BSS_DATE by soid
                    OutputMsg("4.4  回寫SOM(WEB)，BSS_DATE by soid");
                    sb.Length = 0;
                    iTEN = 0;
                    iCount = 0;
                    wotx = wcon.BeginTransaction();
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (iTEN > 400) 
                        {
                            sb.Insert(0, "BEGIN ");
                            sb.Append(" END;");
                            ExeSql(sb.Replace("\r", " ").Replace("\n", " ").ToString(), wotx);
                            sb.Length = 0;
                            iTEN = 0;
                        }
                        //UPDATE SOM
                        sb.Append(" UPDATE SOM SET BSS_DATE=SYSDATE,MODIUSER='CONVERT',MODIDATE=SYSDATE WHERE soid=" + OracleDBUtil.SqlStr(dr["SOID"].ToString()) + "; ");
                        //INSERT SO_UPDATE_LOG
                        string sTmp = string.Format(@" INSERT INTO SO_UPDATE_LOG (
                                     SONO, STATUS, TRANS_DATE, DWNFLAG,SOID, CREATE_USER,CREATE_DTM, MODI_USER, MODI_DTM)
                                     VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8}); "
                                     , OracleDBUtil.SqlStr(dr["SONO"].ToString()), "'1'", (dr["CREATE_DTM"] != DBNull.Value) ? OracleDBUtil.DateFormate(Convert.ToDateTime(dr["CREATE_DTM"].ToString())) : "NULL"
                                     , "'0'", OracleDBUtil.SqlStr(dr["SOID"].ToString()), "'CONVERT'", "SYSDATE", "'CONVERT'", "SYSDATE");
                        sb.Append(sTmp);

                        iCount++;
                        iTEN++;
                    }
                    if (sb.Length > 0) {
                        sb.Insert(0,"BEGIN ");
                        sb.Append(" END;");
                        ExeSql(sb.ToString().Replace("\r", " ").Replace("\n", " "), wotx);
                        sb.Length = 0;
                        iTEN = 0;
                    }
                    wotx.Commit();
                    OutputMsg("  SOM(WEB),SO_UPDATE_LOG，更新筆數:" + iCount.ToString());
                }
                else
                {
                    //BSS查無資料
                    OutputMsg("4.3 BSS查無資料，SOM(WEB),SO_UPDATE_LOG 更新筆數:0");
                }
            }
            catch (Exception ex) {
                OutputMsg("4.網購訂單門市銷售結帳，產生例外");
                throw ex; 
            }
            finally
            {
                if (potx != null) potx.Dispose();
                if (wotx != null) wotx.Dispose();
                if (pcon.State == ConnectionState.Open) pcon.Close();
                pcon.Dispose();
                if (wcon.State == ConnectionState.Open) wcon.Close();
                wcon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        static void BSN()
        {
            OracleConnection wcon = null;
            OracleConnection pcon = null;
            OracleTransaction wotx = null;
            OracleTransaction potx = null;
            StringBuilder sb = new StringBuilder();
            try
            {
                //BSN建立新舊連線
                OutputMsg("5.1 BSN建立新舊連線");
                wcon = OracleDBUtil.GetConnection();
                pcon = OracleDBUtil.GetERPPOSConnection();

                //BSN_OVER_DAY
                OutputMsg("5.2 查詢逾期設定，PARA_KET='BSN_OVER_DAY'");
                sb.Length = 0;
                sb.Append("SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY='BSN_OVER_DAY' AND ROWNUM=1 ");
                DataTable dtOVER = SelectTable(sb.ToString(), wcon);
                int iOverDay = Convert.ToInt32(dtOVER.Rows[0][0].ToString());

                //查詢SOM，bss_flag=1,bss_date is null
                OutputMsg("5.3 查詢SOM，bss_flag=0,bss_date is null");
                sb.Length = 0;
                sb.Append("SELECT SOID,PACKAGE_NO,SONO,nvl(to_char(CREATE_DTM,'YYYY/MM/DD'),'9999/12/31') as CREATE_DTM FROM SOM WHERE BSS_FLAG='0' AND BSS_DATE IS NULL AND PACKAGE_NO IS NOT NULL AND SONO IS NOT NULL ");
                DataTable dt = SelectTable(sb.ToString(), wcon);

                DateTime dTODAY = DateTime.Now;
                //回寫POS，By package_no,sono
                OutputMsg("5.4 回寫POS，By package_no,sono");
                int iTEN = 0,iCount=0;
                potx = pcon.BeginTransaction();
                if (dt.Rows.Count > 0)
                {
                    sb.Length = 0;
                    foreach (DataRow dr in dt.Rows) 
                    {
                        if (dTODAY.Subtract(Convert.ToDateTime(dr["CREATE_DTM"].ToString())).TotalDays > iOverDay ) 
                        {
                            if (iTEN > 400)
                            {
                                sb.Insert(0, "BEGIN ");
                                sb.Append(" END; ");
                                ExeSql(sb.ToString().Replace("\r", " ").Replace("\n", " "), potx);
                                sb.Length = 0;
                                iTEN = 0;
                            }
                            sb.Append(" UPDATE SOM SET BSN_FLAG='1',BSN_DATE=SYSDATE,MODIUSER='CONVERT',MODIDATE=SYSDATE WHERE PACKAGE_NO=" + OracleDBUtil.SqlStr(dr["PACKAGE_NO"].ToString())
                                 + " AND SONO=" + OracleDBUtil.SqlStr(dr["SONO"].ToString()) + "; ");
                            iCount++;
                            iTEN++;
                        }
                    }

                    if (sb.Length > 0)
                    {
                        sb.Insert(0, "BEGIN ");
                        sb.Append(" END; ");
                        ExeSql(sb.ToString().Replace("\r", " ").Replace("\n", " "), potx);
                        sb.Length = 0;
                        iTEN = 0;
                    }
                    potx.Commit();
                    OutputMsg("  SOM(POS)，更新筆數:" + iCount.ToString());
                    //回寫SOM(WEB)，BSN_DATE by soid
                    wotx = wcon.BeginTransaction();
                    sb.Length = 0;
                    iCount = 0;
                    iTEN = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (iTEN > 400) {
                            sb.Insert(0, "BEGIN ");
                            sb.Append(" END; ");
                            ExeSql(sb.ToString().Replace("\r", " ").Replace("\n", " "), wotx);
                            sb.Length = 0;
                            iTEN = 0;
                        }
                        sb.Append(" UPDATE SOM SET BSS_DATE=SYSDATE,MODIUSER='CONVERT',MODIDATE=SYSDATE WHERE SOID=" + OracleDBUtil.SqlStr(dr["SOID"].ToString()) + "; ");
                        //INSERT SO_UPDATE_LOG
                        string sTmp = string.Format(@" INSERT INTO SO_UPDATE_LOG (
                                     SONO, STATUS, TRANS_DATE, DWNFLAG,SOID, CREATE_USER,CREATE_DTM, MODI_USER, MODI_DTM)
                                     VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8}); "
                                     , OracleDBUtil.SqlStr(dr["SONO"].ToString()), "'1'", (dr["CREATE_DTM"] != DBNull.Value) ? OracleDBUtil.DateFormate(Convert.ToDateTime(dr["CREATE_DTM"].ToString())) : "NULL"
                                     , "'0'", OracleDBUtil.SqlStr(dr["SOID"].ToString()), "'CONVERT'", "SYSDATE", "'CONVERT'", "SYSDATE");
                        sb.Append(sTmp);

                        iTEN++;
                        iCount++;
                    }

                    if (sb.Length > 0) {
                        sb.Insert(0, "BEGIN ");
                        sb.Append(" END; ");
                        ExeSql(sb.ToString().Replace("\r", " ").Replace("\n", " "), wotx);
                        sb.Length = 0;
                        iTEN = 0;
                    }
                    wotx.Commit();
                    OutputMsg("  SOM(WEB)，SO_UPDATE_LOG，更新筆數:" + iCount.ToString());
                    //wotx.Rollback();
                }
                else
                {
                    //BSN查無資料
                    OutputMsg("BSN查無資料，SOM(POS)，SO_UPDATE_LOG，更新筆數:0");
                }
            }
            catch (Exception ex) {
                OutputMsg("5.網購訂單門市銷售結帳作廢，產生例外");
                throw ex;
            }
            finally
            {
                if (wotx != null) wotx.Dispose();
                if (potx != null) potx.Dispose();
                if (pcon.State == ConnectionState.Open) pcon.Close();
                pcon.Dispose();
                if (wcon.State == ConnectionState.Open) wcon.Close();
                wcon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        static void BR()
        {
            OracleConnection wcon = null;
            OracleConnection pcon = null;
            OracleTransaction wotx = null;
            OracleTransaction potx = null;
            StringBuilder sb = new StringBuilder();
            try
            {
                //BR建立新舊連線
                OutputMsg("6.1 BR建立新舊連線");
                wcon = OracleDBUtil.GetConnection();
                pcon = OracleDBUtil.GetERPPOSConnection();
                //查詢SOM，bs_flag=1,bs_date is null
                OutputMsg("6.2 查詢SOM，br_flag=1,bs_date is null");
                sb.Append("SELECT SOID,PACKAGE_NO,SONO,CREATE_DTM FROM SOM WHERE BR_FLAG='1' AND BR_DATE IS NULL AND PACKAGE_NO IS NOT NULL AND SONO IS NOT NULL ");
                DataTable dt = SelectTable(sb.ToString(), wcon);
                //回寫POS，By package_no,sono
                OutputMsg("6.3 回寫POS，By package_no,sono");
                int iCount = 0;
                int iTEN = 0;
                if (dt.Rows.Count > 0)
                {
                    potx = pcon.BeginTransaction();
                    sb.Length = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (iTEN > 400) 
                        {
                            sb.Insert(0, "BEGIN ");
                            sb.Append(" END;");
                            ExeSql(sb.Replace("\r", " ").Replace("\n", " ").ToString(), potx);
                            sb.Length = 0;
                            iTEN = 0;
                        }
                        sb.Append(" UPDATE SOM SET BR_FLAG='1',BR_DATE=SYSDATE,MODIUSER='CONVERT',MODIDATE=SYSDATE WHERE PACKAGE_NO=" + OracleDBUtil.SqlStr(dr["PACKAGE_NO"].ToString())
                                 + " AND SONO=" + OracleDBUtil.SqlStr(dr["SONO"].ToString()) + "; ");
                        iTEN++;
                        iCount++;
                    }
                    if (sb.Length>0)
                    {
                        sb.Insert(0, "BEGIN ");
                        sb.Append(" END;");
                        ExeSql(sb.Replace("\r", " ").Replace("\n", " ").ToString(), potx);
                        sb.Length = 0;
                        iTEN = 0;
                    }
                    potx.Commit();
                    OutputMsg("   SOM(POS)，更新筆數:" + iCount.ToString());
                    //回寫SOM(WEB)，BS_DATE by soid
                    sb.Length = 0;
                    iCount = 0;
                    iTEN = 0;
                    
                    wotx = wcon.BeginTransaction();
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (iTEN > 400)
                        {
                            sb.Insert(0, "BEGIN ");
                            sb.Append(" END;");
                            ExeSql(sb.Replace("\r", " ").Replace("\n", " ").ToString(), wotx);
                            sb.Length = 0;
                            iTEN = 0;
                        }
                        //UPDATE SOM
                        sb.Append(" UPDATE SOM SET BR_DATE=SYSDATE,MODIUSER='CONVERT',MODIDATE=SYSDATE WHERE SOID=" + OracleDBUtil.SqlStr(dr["SOID"].ToString()) + "; ");
                        //INSERT SO_UPDATE_LOG
                        string sTmp = string.Format(@" INSERT INTO SO_UPDATE_LOG (
                                     SONO, STATUS, TRANS_DATE, DWNFLAG,SOID, CREATE_USER,CREATE_DTM, MODI_USER, MODI_DTM)
                                     VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8}); "
                                     , OracleDBUtil.SqlStr(dr["SONO"].ToString()), "'1'", (dr["CREATE_DTM"] != DBNull.Value) ? OracleDBUtil.DateFormate(Convert.ToDateTime(dr["CREATE_DTM"].ToString())) : "NULL"
                                     , "'0'", OracleDBUtil.SqlStr(dr["SOID"].ToString()), "'CONVERT'", "SYSDATE", "'CONVERT'", "SYSDATE");
                        sb.Append(sTmp);

                        iTEN++;
                        iCount++;

                    }
                    if (sb.Length>0)
                    {
                        sb.Insert(0, "BEGIN ");
                        sb.Append(" END;");
                        ExeSql(sb.Replace("\r", " ").Replace("\n", " ").ToString(), wotx);
                        sb.Length = 0;
                        iTEN = 0;
                    }
                    wotx.Commit();
                    OutputMsg("   SOM(WEB)，SO_UPDATE_LOG，更新筆數:" + iCount.ToString());
                }
                else
                {
                    //BR查無資料
                    OutputMsg("6.3 BR查無資料，SO_UPDATE_LOG，SOM(POS)更新筆數:0");
                }
            }
            catch (Exception ex) {
                OutputMsg("6.網購件未取件，產生例外");
                throw ex; 
            }
            finally
            {
                if (potx != null) potx.Dispose();
                if (wotx != null) wotx.Dispose();
                if (pcon.State == ConnectionState.Open) pcon.Close();
                pcon.Dispose();
                if (wcon.State == ConnectionState.Open) wcon.Close();
                wcon.Dispose();
                OracleConnection.ClearAllPools();
            }

        }

        static DataTable SelectTable(string sql,OracleConnection con) 
        {
            DataTable dt = new DataTable();
            try
            {
                sCacheSql=sql;
                dt = OracleDBUtil.GetDataSet(con, sql).Tables[0];
            }
            catch (Exception ex) {
                OutputMsg("最後執行SQL:");
                OutputMsg(sCacheSql);
                throw ex; 
            }
            return dt;
        }

        static int ExeSql(string sql,OracleConnection con)
        {
            OracleTransaction otx = null;
            int i = 0;
            try
            {
                sCacheSql = sql;
                otx = con.BeginTransaction();
                i=OracleDBUtil.ExecuteSql(otx, sql);
                otx.Commit();
            }
            catch (Exception ex)
            {
                otx.Rollback();
                OutputMsg("最後執行SQL:");
                OutputMsg(sCacheSql);
                throw ex;
            }
            finally 
            {
                if (otx != null) otx.Dispose();
            }
            return i;
        }

        static int ExeSql(string sql, OracleTransaction otx)
        {
            int i = 0;
            try
            {
                sCacheSql = sql;
                i = OracleDBUtil.ExecuteSql(otx, sql);
            }
            catch (Exception ex)
            {
                otx.Rollback();
                OutputMsg("最後執行SQL:");
                OutputMsg(sCacheSql);
                throw ex;
            }
            return i;
        }


        static void OutputMsg(string s)
        {
            Console.WriteLine(s);
            sMSG += s + "\r\n";
        }

        #region 舊程式20110210
        //static void Main(string[] args)
        //{
        //    //初始化LOG
        //    Console.WriteLine("1.SO_UPLOAD_LOG");
        //    Console.WriteLine("2.初始化LOG");
        //    ConvertLog con_log = new ConvertLog("SO_UPLOAD_LOG");

        //    try
        //    {
        //        //new SCH08_Facade().so_update_log();
        //        Console.WriteLine("3.SO_UPDATE_LOG(POS)=>SO_UPDATE_LOG(WEB)");
        //        int i1=so_update_log_WEB();
        //        Console.WriteLine("4.SO_UPDATE_LOG(WEB)=>SO_UPDATE_LOG(POS)");
        //        int i2=so_update_log_POS();

        //        //成功資訊
        //        Console.WriteLine("5.執行結束，寫入LOG");
        //        con_log.Success("SO_UPDATE_LOG(WEB):新增筆數" + i1.ToString() + ",SO_UPDATE_LOG(POS):新增筆數" + i2.ToString());
        //        Thread.Sleep(5000);
        //    }
        //    catch (Exception ex)
        //    {
        //        //失敗資訊
        //        con_log.Fail(ex.Message);
        //        Console.WriteLine("例外產生");
        //        Console.WriteLine(ex.Message);
        //        Thread.Sleep(5000);
        //    }
        //}

//        static int so_update_log_WEB()
//        {

//            OracleConnection objConn_erp = null;
//            OracleConnection objConn = null;
//            OracleTransaction OldPosTrans = null;
//            OracleTransaction NewPosTrans = null;
//            int iRet = 0;
//            try
//            {

//                objConn_erp = OracleDBUtil.GetERPPOSConnection();
//                objConn = OracleDBUtil.GetConnection();
//                NewPosTrans = objConn.BeginTransaction();
//                //從舊的POS取出
//                //SCH08_Product_Type_DTO.SO_UPDATE_LOG_OLDDataTable dt =
//                //        new SCH08_Product_Type_DTO.SO_UPDATE_LOG_OLDDataTable();
//                DataTable dt = new DataTable();

//                StringBuilder sb = new StringBuilder();

//                sb.AppendLine("SELECT * ");
//                sb.AppendLine("FROM SO_UPDATE_LOG WHERE DWNFLAG=0 ");

//                dt.Load(OracleDBUtil.GetDataReader(objConn_erp, sb.ToString()));

//                if (dt.Rows.Count > 0)
//                {
//                    iRet = dt.Rows.Count;
//                    foreach (DataRow dr in dt.Rows) 
//                    {
//                        sb.Length = 0;
//                        sb.AppendLine(
//                        @"Insert into SO_UPDATE_LOG
//                           (SONO, STATUS, TRANS_DATE, 
//                            DWNFLAG, DWNDATE, SENDDATE, 
//                            SONOX, SOID, CREATE_USER, 
//                            CREATE_DTM, MODI_USER, MODI_DTM)
//                            Values
//                           ([SONO], [STATUS], [TRANS_DATE], 
//                            [DWNFLAG], [DWNDATE], [SENDDATE], 
//                            [SONOX], [SOID], [CREATE_USER], 
//                            [CREATE_DTM], [MODI_USER], [MODI_DTM])"
//                        );

//                        foreach (DataColumn dc in dt.Columns)
//                        {
//                            string dcname = dc.ColumnName;
//                            if (!dcname.Equals("DWNFLAG"))
//                            {
//                                switch (dc.DataType.ToString())
//                                {
//                                    case "System.String":
//                                        sb.Replace("[" + dcname + "]", OracleDBUtil.SqlStr(dr[dcname].ToString()));
//                                        break;
//                                    case "System.Decimal":
//                                        sb.Replace("[" + dcname + "]", (dr[dcname] != DBNull.Value) ? dr[dcname].ToString() : "0");
//                                        break;
//                                    case "System.DateTime":
//                                        sb.Replace("[" + dcname + "]", (dr[dcname] != DBNull.Value) ? OracleDBUtil.DateFormate(dr[dcname]) : "null");
//                                        break;
//                                    default:
//                                        sb.Replace("[" + dcname + "]", OracleDBUtil.SqlStr(dr[dcname].ToString()));
//                                        break;
//                                }
//                            }
//                            else {
//                                sb.Replace("[" + dcname + "]", "'1'"); //DownFlag
//                            }
                            
//                        }

//                        OracleDBUtil.ExecuteSql(NewPosTrans, sb.ToString());
//                    }
//                    NewPosTrans.Commit();

//                    OracleDBUtil.ExecuteSql(objConn_erp,
//                        @"UPDATE SO_UPDATE_LOG
//                      SET DWNFLAG=1
//                      WHERE DWNFLAG=0");
//                }

//            }
//            catch (Exception ex)
//            {
//                if (OldPosTrans != null) OldPosTrans.Rollback();
//                if (NewPosTrans != null) NewPosTrans.Rollback();
//                throw ex;
//            }
//            finally
//            {
//                if (NewPosTrans != null) NewPosTrans.Dispose();
//                if (OldPosTrans!=null) OldPosTrans.Dispose();

//                if (objConn.State == ConnectionState.Open) objConn.Close();
//                objConn.Dispose();

//                if (objConn_erp.State == ConnectionState.Open) objConn_erp.Close();
//                objConn_erp.Dispose();
//                OracleConnection.ClearAllPools();
//            }
//            return iRet;

//        }

//        static int so_update_log_POS()
//        {

//            OracleConnection objConn = null;
//            OracleConnection objConn_erp = null;
//            OracleTransaction OldPosTrans = null;
//            OracleTransaction NewPosTrans = null;
//            int iRet = 0;
//            try
//            {

//                objConn_erp = OracleDBUtil.GetERPPOSConnection();
//                OldPosTrans = objConn_erp.BeginTransaction();
//                objConn = OracleDBUtil.GetConnection();
//                StringBuilder sb = new StringBuilder();
//                //從新的POS取出
//                DataTable dtNewPosLog = new DataTable();
//                dtNewPosLog.Load(OracleDBUtil.GetDataReader(
//                    objConn,
//                    "SELECT * FROM SO_UPDATE_LOG WHERE DWNFLAG=0"
//                    ));

//                if (dtNewPosLog.Rows.Count > 0)
//                {
//                    iRet = dtNewPosLog.Rows.Count;
//                    OldPosTrans = objConn.BeginTransaction();
//                    foreach (DataRow dr in dtNewPosLog.Rows)
//                    {
//                        sb.Length = 0;
//                        sb.AppendLine(
//                        @"Insert into SO_UPDATE_LOG
//                           (SONO, STATUS, TRANS_DATE, 
//                            DWNFLAG, DWNDATE, SENDDATE, 
//                            SONOX, SOID, CREATE_USER, 
//                            CREATE_DTM, MODI_USER, MODI_DTM)
//                            Values
//                           ([SONO], [STATUS], [TRANS_DATE], 
//                            [DWNFLAG], [DWNDATE], [SENDDATE], 
//                            [SONOX], [SOID], [CREATE_USER], 
//                            [CREATE_DTM], [MODI_USER], [MODI_DTM])"
//                        );

//                        foreach (DataColumn dc in dtNewPosLog.Columns)
//                        {
//                            string dcname = dc.ColumnName;
//                            if (!dcname.Equals("DWNFLAG"))
//                            {
//                                switch (dc.DataType.ToString())
//                                {
//                                    case "System.String":
//                                        sb.Replace("[" + dcname + "]", OracleDBUtil.SqlStr(dr[dcname].ToString()));
//                                        break;
//                                    case "System.Decimal":
//                                        sb.Replace("[" + dcname + "]", (dr[dcname] != DBNull.Value) ? dr[dcname].ToString() : "0");
//                                        break;
//                                    case "System.DateTime":
//                                        sb.Replace("[" + dcname + "]", (dr[dcname] != DBNull.Value) ? OracleDBUtil.DateFormate(dr[dcname]) : "null");
//                                        break;
//                                    default:
//                                        sb.Replace("[" + dcname + "]", OracleDBUtil.SqlStr(dr[dcname].ToString()));
//                                        break;
//                                }
//                            }
//                            else
//                            {
//                                sb.Replace("[" + dcname + "]", "'1'"); //DownFlag
//                            }
//                        }

//                        OracleDBUtil.ExecuteSql(OldPosTrans, sb.ToString());
//                    }
//                    OldPosTrans.Commit();

//                    OracleDBUtil.ExecuteSql(objConn,
//                    @"UPDATE SO_UPDATE_LOG
//                      SET DWNFLAG=1
//                      WHERE DWNFLAG=0");
//                }
//            }
//            catch (Exception ex)
//            {
//                if (OldPosTrans != null) OldPosTrans.Rollback();
//                if (NewPosTrans != null) NewPosTrans.Rollback();
//                throw ex;
//            }
//            finally
//            {
//                if (NewPosTrans != null) NewPosTrans.Dispose();
//                if (OldPosTrans != null) OldPosTrans.Dispose();

//                if (objConn.State == ConnectionState.Open) objConn.Close();
//                objConn.Dispose();

//                if (objConn_erp.State == ConnectionState.Open) objConn_erp.Close();
//                objConn_erp.Dispose();
//                OracleConnection.ClearAllPools();
//            }
//            return iRet;

        //        }
        #endregion
    }
}
