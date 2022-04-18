using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using System.Threading;

namespace CANCLE_TO_CLOSE
{
    class Program
    {
        private static string sMSG = "";
        static void Main(string[] args)
        {
            OracleConnection conn = OracleDBUtil.GetConnection();
            OracleTransaction trans = conn.BeginTransaction();
            ConvertLog log = new ConvertLog("CANCLE_TO_CLOSE");
            try
            {
                int j = RTry__Num(trans);
                for (int i = 1; i <= j; i++)
                {
                    CANCLE_TO_CLOSE SCAR = new CANCLE_TO_CLOSE(conn, trans, log);
                }
                log.Success(sMSG);

            }
            catch (Exception ex)
            {
                OutputMsg("<<例外產生>>");
                OutputMsg(ex.Message);
                log.Fail(sMSG);

            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                OracleConnection.ClearAllPools();
                conn = null;
                Thread.Sleep(3000);
            }
        }
        static void OutputMsg(string s)
        {
            Console.WriteLine(s);
            sMSG += s + "\r\n";
        }
        static int RTry__Num(OracleTransaction trans)
        {
            int j = 1;
            string sqlStr = @"SELECT * FROM SYS_PARA WHERE PARA_KEY = 'CANCLE_TO_CLOSE_RTRY_NUM'";
            DataTable dt = OracleDBUtil.GetDataSet(trans, sqlStr).Tables[0];
            if (dt.Rows.Count > 0)
            {
                j += int.Parse(dt.Rows[0]["PARA_VALUE"].ToString());
            }
            return j;
        }
    }
    public class CANCLE_TO_CLOSE
    {
        OracleConnection conn;
        OracleTransaction trans;
        ConvertLog log;
        private static string sMSG = "";
        public CANCLE_TO_CLOSE(OracleConnection conn, OracleTransaction trans, ConvertLog log)
        {
            this.conn = conn;
            this.trans = trans;
            this.log = log;
            string posuuitDetail = "";
            string prePosuuidDetail = "";
            int ret = new int();

            DataTable dt = getCancleTO_CLOSE_DATA();
            OutputMsg("共有" + dt.Rows.Count + "筆資料要取消");
            log.Write_Detail("共有" + dt.Rows.Count + "筆資料要取消" +
                "，詳細資料:SELECT * FROM SYS_PROCESS_LOG WHERE FUNC_GROUP='" + log.Task_Name + "' AND MACHINE_ID='" + log.SID + "' ORDER BY CREATE_DTM DESC"
                );
            foreach (DataRow dr in dt.Rows)
            {



                OracleConnection conn1 = OracleDBUtil.GetConnection();
                OracleTransaction trans1 = conn1.BeginTransaction();
                try
                {
                    if (dr["POSUUID_DETAIL"] != null && (!string.IsNullOrEmpty(StringUtil.CStr(dr["POSUUID_DETAIL"]))))
                        posuuitDetail = StringUtil.CStr(dr["POSUUID_DETAIL"]);

                    if (posuuitDetail != "" && posuuitDetail != prePosuuidDetail)
                    {
                        prePosuuidDetail = posuuitDetail;
                        OutputMsg("Cancel To Close Data:" + StringUtil.CStr(dr["SYSID"]) + ":" + StringUtil.CStr(dr["POSUUID_DETAIL"]));
                        log.Write_Detail("Cancel To Close Data:" + StringUtil.CStr(dr["SYSID"]) + ":" + StringUtil.CStr(dr["POSUUID_DETAIL"]));
                        ret = CancelOuterSystem(StringUtil.CStr(dr["POSUUID_DETAIL"]), StringUtil.CStr(dr["SERVICE_TYPE"]),
                                                        StringUtil.CStr(dr["SERVICE_SYS_ID"]), StringUtil.CStr(dr["BUNDLE_ID"]),
                                                        StringUtil.CStr(dr["STORE_NO"]), StringUtil.CStr(dr["SALE_PERSON"]),
                                                        StringUtil.CStr(dr["BARCODE1"]), StringUtil.CStr(dr["BARCODE2"]),
                                                        StringUtil.CStr(dr["BARCODE3"]), StringUtil.CStr(dr["AMOUNT"]));
                        if (ret == 0)
                        {
                            //取消交易,commit外部系統成功才刪除未結清單中資料
                            System.Text.StringBuilder posuuid_detailList = new System.Text.StringBuilder("");
                            posuuid_detailList.Append(OracleDBUtil.SqlStr(StringUtil.CStr(dr["POSUUID_DETAIL"]))).Append(",");
                            OutputMsg("Delete To Close Data" + StringUtil.CStr(dr["POSUUID_DETAIL"]));
                            log.Write_Detail("Delete To Close Data" + StringUtil.CStr(dr["POSUUID_DETAIL"]));
                            delTO_CLOSE(trans1, posuuid_detailList);
                        }
                        else
                        {
                            OutputMsg("Insert Data Upload Log" + StringUtil.CStr(dr["POSUUID_DETAIL"]));
                            log.Write_Detail("Insert Data Upload Log" + StringUtil.CStr(dr["POSUUID_DETAIL"]));
                            InsertDataUploadLog(trans1, StringUtil.CStr(dr["POSUUID_DETAIL"]));
                        }
                    }
                    trans1.Commit();

                }
                catch (Exception ex)
                {
                    OutputMsg("<<例外產生>>");
                    OutputMsg(ex.Message);
                    log.Fail(sMSG);
                }
                finally
                {
                    if (conn1.State == ConnectionState.Open) conn1.Close();
                    conn1.Dispose();
                    OracleConnection.ClearAllPools();
                    conn1 = null;
                }
            }
        }


        /// <summary>
        /// 取得未結清單資料 for 取消交易用
        /// </summary>
        /// <returns></returns>
        public DataTable getCancleTO_CLOSE_DATA()
        {

            string sqlStr = @"SELECT   DECODE(h.SERVICE_TYPE,'1','IA','2','Loyalty','3','Payment'
                            ,'4','SSI','5','OLR','6','HRS','7','DMS','8','ETC','9','NCIC','10','eStore') SYSID,h.POSUUID_DETAIL, h.SERVICE_TYPE, h.SERVICE_SYS_ID, h.BUNDLE_ID, h.TOTAL_AMOUNT, h.STORE_NO, h.SALE_PERSON 
                                    , i.BARCODE1, i.BARCODE2, i.BARCODE3, i.AMOUNT  FROM TO_CLOSE_HEAD h ,TO_CLOSE_ITEM i
                            WHERE SYSDATE> h.CREATE_DTM + (SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY = 
                            'CANCLE_TO_CLOSE_TIME_' || DECODE(h.SERVICE_TYPE,'1','IA','2','Loyalty','3','Payment'
                            ,'4','SSI','5','OLR','6','HRS','7','DMS','8','ETC','9','NCIC','10','eStore') )/24
                            and h.POSUUID_DETAIL = i.POSUUID_DETAIL(+)  ";
            DataTable dt = dt = OracleDBUtil.GetDataSet(trans, sqlStr).Tables[0];

            return dt;
        }

        /// <summary>
        /// Cancel 外部系統交易
        /// </summary>
        /// <param name="posuuid_detail">未結交易表頭檔主鍵</param>
        /// <param name="service_type">交易類型</param>
        /// <param name="service_sys_id">外部系統主鍵</param>
        /// <param name="bundle_id">bundle_id</param>
        /// <param name="store_no">交易店點</param>
        /// <param name="sale_person">交易人員</param>
        /// <param name="barcode1">barcode1</param>
        /// <param name="barcode2">barcode2</param>
        /// <param name="barcode3">barcode3</param>
        /// <param name="amount">單筆交易金額</param>
        /// <returns>結果:0 成功, -1 失敗</returns>
        public int CancelOuterSystem(string posuuid_detail, string service_type, string service_sys_id, string bundle_id,
                                        string store_no, string sale_person, string barcode1, string barcode2, string barcode3, string amount)
        {
            string outerConnStr = "";
            string BOUouterConnStr = "";
            string outerCmd = "";
            string strMCode = "";
            string SYSID = "";
            string BouCmd = "";
            string BouID = "";
            string CheckFlag = "";
            //string SYSOK = "";
            string strLogMsg = "";
            string strBundleLogMsg = "";
            OracleConnection objConn = null;
            OracleCommand oraCmd = null;
            OracleConnection BouConn = null;
            OracleCommand BouoraCmd = null;

            string deletedBoundleId = "";

            try
            {
                bool cancelOk = true;
                if (posuuid_detail != null && (!string.IsNullOrEmpty(posuuid_detail)))
                {
                    if (service_type != null && (!string.IsNullOrEmpty(service_type)))
                    {
                        switch (service_type)
                        {
                            case "1":   //IA
                                outerConnStr = OracleDBUtil.GetIAConnectionStringByTNSName();
                                SYSID = "IA";
                                break;
                            case "2":   //LOY
                                outerConnStr = OracleDBUtil.GetLOYConnectionStringByTNSName();
                                SYSID = "LOY";
                                break;
                            case "4":   //SSI
                                outerConnStr = OracleDBUtil.GetSSIConnectionStringByTNSName();
                                SYSID = "SSI";
                                break;
                            case "3":   //PAYMENT
                                outerConnStr = OracleDBUtil.GetPAYMENTConnectionStringByTNSName();
                                SYSID = "PY";
                                break;
                            case "10":   //E-Store
                                outerConnStr = OracleDBUtil.GetEStoreConnectionStringByTNSName();
                                SYSID = "ES";
                                break;
                            default:
                                break;
                        }

                        objConn = Advtek.Utility.OracleDBUtil.GetConnection();

                        if (bundle_id != "" && deletedBoundleId.IndexOf(bundle_id + "^" + barcode1 + "^" + barcode2 + "^" + barcode3) < 0)
                        {

                            BOUouterConnStr = OracleDBUtil.GetBOUConnectionStringByTNSName();
                            BouConn = OracleDBUtil.GetConnectionByConnString(BOUouterConnStr);

                            //BouCmd = "SP_POS_Feedback_Cancel";
                            BouID = "BOU";

                            string strSQL = "Select para_value from sys_para where para_key = " + OracleDBUtil.SqlStr(BouID + "_CANCEL");
                            DataTable dt1 = OracleDBUtil.GetDataSet(objConn, strSQL).Tables[0];
                            if (dt1 != null && dt1.Rows.Count > 0)
                                if (dt1.Rows[0][0] != null)
                                    BouCmd = dt1.Rows[0][0].ToString();
                            BouoraCmd = new OracleCommand(BouCmd);
                            BouoraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        }

                        if (SYSID != "")
                        {
                            string strSQL = "Select para_value from sys_para where para_key = " + OracleDBUtil.SqlStr(SYSID + "_CANCEL");
                            DataTable dtSys = OracleDBUtil.GetDataSet(objConn, strSQL).Tables[0];
                            if (dtSys != null && dtSys.Rows.Count > 0)
                                if (dtSys.Rows[0][0] != null)
                                    outerCmd = dtSys.Rows[0][0].ToString();
                            objConn = OracleDBUtil.GetConnectionByConnString(outerConnStr);
                        }



                        //}

                        try
                        {
                            oraCmd = new OracleCommand(outerCmd);
                            oraCmd.CommandType = System.Data.CommandType.StoredProcedure;

                            switch (SYSID)
                            {
                                case "IA":
                                    oraCmd.Parameters.Add(new OracleParameter("ACTI_NO", OracleType.VarChar, 2000)).Value = service_sys_id;
                                    oraCmd.Parameters.Add(new OracleParameter("UUID_DETAILS", OracleType.VarChar, 2000)).Value = posuuid_detail;
                                    oraCmd.Parameters.Add(new OracleParameter("STATUS", OracleType.VarChar, 4000)).Direction = ParameterDirection.Output;
                                    oraCmd.Parameters.Add(new OracleParameter("ERR_CODE", OracleType.Number)).Direction = ParameterDirection.Output;
                                    oraCmd.Parameters.Add(new OracleParameter("ERR_MESG", OracleType.VarChar, 4000)).Direction = ParameterDirection.Output;
                                    strLogMsg = ",ACTI_NO=" + service_sys_id + ", UUID_DETAILS=" + posuuid_detail;
                                    break;
                                case "LOY":
                                    oraCmd.Parameters.Add(new OracleParameter("POSuuid_Details", OracleType.VarChar, 2000)).Value = posuuid_detail;
                                    oraCmd.Parameters.Add(new OracleParameter("image_number", OracleType.VarChar, 2000)).Value = service_sys_id;
                                    oraCmd.Parameters.Add(new OracleParameter("msg", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                                    strLogMsg = ", POSuuid_Details=" + posuuid_detail;
                                    break;
                                case "SSI":
                                    oraCmd.Parameters.Add(new OracleParameter("image_number", OracleType.VarChar, 2000)).Value = service_sys_id;
                                    oraCmd.Parameters.Add(new OracleParameter("POSuuid_Details", OracleType.VarChar, 2000)).Value = posuuid_detail;
                                    oraCmd.Parameters.Add(new OracleParameter("result", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                                    strLogMsg = ",image_number=" + service_sys_id + ", UUID_DETAILS=" + posuuid_detail;
                                    break;
                                case "PY":
                                    oraCmd.Parameters.Add(new OracleParameter("SYS_KEY", OracleType.VarChar, 2000)).Value = service_sys_id;
                                    oraCmd.Parameters.Add(new OracleParameter("POSUUID_DETAILS", OracleType.VarChar, 2000)).Value = posuuid_detail;
                                    oraCmd.Parameters.Add(new OracleParameter("BARCODE1", OracleType.VarChar, 2000)).Value = barcode1;
                                    oraCmd.Parameters.Add(new OracleParameter("BARCODE2", OracleType.VarChar, 2000)).Value = barcode2;
                                    oraCmd.Parameters.Add(new OracleParameter("BARCODE3", OracleType.VarChar, 2000)).Value = barcode3;
                                    oraCmd.Parameters.Add(new OracleParameter("PAY_AMOUNT", OracleType.VarChar, 2000)).Value = amount;
                                    oraCmd.Parameters.Add(new OracleParameter("STOREID", OracleType.VarChar, 2000)).Value = store_no;
                                    oraCmd.Parameters.Add(new OracleParameter("EMPLOYEE_ID", OracleType.VarChar, 2000)).Value = sale_person;
                                    oraCmd.Parameters.Add(new OracleParameter("RTN_SYS_KEY", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                                    oraCmd.Parameters.Add(new OracleParameter("RTN_POSUUID_DETAILS", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                                    strLogMsg = ",SYS_KEY=" + service_sys_id + ", POSUUID_DETAILS=" + posuuid_detail + ", BARCODE1=" + barcode1
                                                + ", BARCODE2=" + barcode2 + ", BARCODE3=" + barcode3 + ", PAY_AMOUNT=" + amount + ", STOREID=" + store_no
                                                + ", EMPLOYEE_ID=" + sale_person;
                                    break;
                                case "ES":
                                    oraCmd.Parameters.Add(new OracleParameter("POSuuid_details", OracleType.VarChar, 2000)).Value = posuuid_detail;
                                    oraCmd.Parameters.Add(new OracleParameter("package_id", OracleType.VarChar, 2000)).Value = service_sys_id;
                                    oraCmd.Parameters.Add(new OracleParameter("employee_Id", OracleType.VarChar, 2000)).Value = sale_person;
                                    oraCmd.Parameters.Add(new OracleParameter("Store_Id", OracleType.VarChar, 2000)).Value = store_no;
                                    oraCmd.Parameters.Add(new OracleParameter("STATUS", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                                    oraCmd.Parameters.Add(new OracleParameter("ERROR_CODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                                    oraCmd.Parameters.Add(new OracleParameter("ERROR_DESCRIPTION", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                                    strLogMsg = ", POSuuid_details=" + posuuid_detail + ", package_id=" + service_sys_id
                                                + ", employee_Id=" + sale_person + ", Store_Id=" + store_no;
                                    break;
                            }




                            // oraCmd.Parameters.Add(new OracleParameter(SOURCE_REFERENCE_Name, OracleType.VarChar, 2000)).Value = SERVICE_SYS_ID;
                            //  oraCmd.Parameters.Add(new OracleParameter("POSuuid_Master", OracleType.VarChar, 2000)).Value = POSUUID_MASTER;

                            if (oraCmd != null && oraCmd.ToString() != "")
                            {
                                oraCmd.Connection = objConn;
                                oraCmd.ExecuteNonQuery();
                            }
                            switch (SYSID)
                            {
                                case "IA":
                                    strMCode = "SP_IA_CANCEL_POS,ERR_CODE=" + oraCmd.Parameters["ERR_CODE"].Value.ToString() + ",ERR_MESG="
                                                + oraCmd.Parameters["ERR_MESG"].Value.ToString();
                                    if (oraCmd.Parameters["ERR_CODE"].Value.ToString() != "0")
                                        cancelOk = false;
                                    break;
                                case "LOY":
                                    strMCode = "SP_LOY_CANCEL_POS,msg=" + oraCmd.Parameters["msg"].Value.ToString();
                                    if (oraCmd.Parameters["msg"].Value.ToString().IndexOf("FAIL", 0) != -1)
                                        cancelOk = false;
                                    break;
                                case "SSI":
                                    strMCode = "SP_SSI_CANCEL_POS,result=" + oraCmd.Parameters["result"].Value.ToString();
                                    if (oraCmd.Parameters["result"].Value.ToString() == "N")
                                        cancelOk = false;
                                    break;
                                case "PY":
                                    strMCode = "SP_POS2PAYMENT_CANCEL_DATA,RTN_SYS_KEY=" + oraCmd.Parameters["RTN_SYS_KEY"].Value.ToString() + ",RTN_POSUUID_DETAILS=" + oraCmd.Parameters["RTN_POSUUID_DETAILS"].Value.ToString();
                                    if (oraCmd.Parameters["RTN_SYS_KEY"].Value.ToString() == "")
                                        cancelOk = false;
                                    break;
                                case "ES":
                                    strMCode = "SP_POS4eStore_CancelOrder,ERROR_CODE=" + oraCmd.Parameters["ERROR_CODE"].Value.ToString() + ",ERROR_DESCRIPTION="
                                                + oraCmd.Parameters["ERROR_DESCRIPTION"].Value.ToString() + ",STATUS=" + oraCmd.Parameters["STATUS"].Value.ToString();
                                    if (oraCmd.Parameters["ERROR_CODE"].Value.ToString() != "")
                                        cancelOk = false;
                                    break;

                            }

                            if (SYSID != "")
                            {
                                OutputMsg("POS通知服務系統取消交易完成: SP=" + outerCmd + strLogMsg
                                                + ", connection string[" + outerConnStr + "]," + OracleDBUtil.SqlStr(strMCode));
                                log.Write_Detail("POS通知服務系統取消交易完成: SP=" + outerCmd + strLogMsg
                                                + ", connection string[" + outerConnStr + "]," + OracleDBUtil.SqlStr(strMCode));
                                CheckFlag = "1";
                            }


                            if (bundle_id != "" && deletedBoundleId.IndexOf(bundle_id + "^" + barcode1 + "^" + barcode2 + "^" + barcode3) < 0)
                            {

                                BouoraCmd.Parameters.Add(new OracleParameter("BundleID", OracleType.VarChar, 2000)).Value = bundle_id;
                                BouoraCmd.Parameters.Add(new OracleParameter("ReturnCode", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                                BouoraCmd.Parameters.Add(new OracleParameter("ReturnMsg", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                                deletedBoundleId += "," + bundle_id + "^" + barcode1 + "^" + barcode2 + "^" + barcode3;

                                BouoraCmd.Connection = BouConn;
                                BouoraCmd.ExecuteNonQuery();

                                strMCode = "SP_POS_Feedback_Cancel,ReturnMsg=" + BouoraCmd.Parameters["ReturnMsg"].Value.ToString();
                                strBundleLogMsg = ",BundleID=" + bundle_id;
                                if (BouoraCmd.Parameters["ReturnCode"].Value.ToString() == "9999")
                                    cancelOk = false;

                                OutputMsg("POS通知服務系統取消交易完成: SP=" + BouCmd + strBundleLogMsg
                                         + ", connection string[" + BOUouterConnStr + "]," + OracleDBUtil.SqlStr(strMCode));
                                log.Write_Detail("POS通知服務系統取消交易完成: SP=" + BouCmd + strBundleLogMsg
                                         + ", connection string[" + BOUouterConnStr + "]," + OracleDBUtil.SqlStr(strMCode));

                            }
                        }
                        catch (Exception ex)
                        {
                            string msg = ex.Message;
                            cancelOk = false;
                            if (SYSID != "" && CheckFlag != "1")
                            {
                                OutputMsg("POS通知服務系統取消交易失敗: SP=" + outerCmd + strLogMsg
                                                + ", connection string[" + outerConnStr + "]," + OracleDBUtil.SqlStr(msg));
                                log.Write_Detail("POS通知服務系統取消交易失敗: SP=" + outerCmd + strLogMsg
                                                + ", connection string[" + outerConnStr + "]," + OracleDBUtil.SqlStr(msg));
                            }
                            if (bundle_id != "")
                            {
                                OutputMsg("POS通知服務系統取消交易失敗: SP=" + BouCmd + strBundleLogMsg
                                             + ", connection string[" + BOUouterConnStr + "]," + OracleDBUtil.SqlStr(msg));
                                log.Write_Detail("POS通知服務系統取消交易失敗: SP=" + BouCmd + strBundleLogMsg
                                             + ", connection string[" + BOUouterConnStr + "]," + OracleDBUtil.SqlStr(msg));
                            }
                        }
                        finally
                        {
                            //  oraCmd.Dispose();
                        }
                    }
                }
                if (cancelOk)
                    return 0;
                else
                    return -1;
            }
            catch (Exception ex)
            {
                if (SYSID != "")
                {
                    OutputMsg("POS通知服務系統取消交易失敗: SP=" + outerCmd + strLogMsg
                                                + OracleDBUtil.SqlStr(ex.Message.Replace("'", "-").Replace("\"", " ")));
                    log.Write_Detail("POS通知服務系統取消交易失敗: SP=" + outerCmd + strLogMsg
                                                + OracleDBUtil.SqlStr(ex.Message.Replace("'", "-").Replace("\"", " ")));
                }
                if (bundle_id != "")
                {
                    OutputMsg("POS通知服務系統取消交易失敗: SP=" + BouCmd + strBundleLogMsg
                                              + OracleDBUtil.SqlStr(ex.Message.Replace("'", "-").Replace("\"", " ")));
                    log.Write_Detail("POS通知服務系統取消交易失敗: SP=" + BouCmd + strBundleLogMsg
                                              + OracleDBUtil.SqlStr(ex.Message.Replace("'", "-").Replace("\"", " ")));
                }
                return -1;
            }
            finally
            {
                if (oraCmd != null)
                    oraCmd.Dispose();

                if (objConn != null && objConn.State == ConnectionState.Open)
                    objConn.Close();

                if (objConn != null)
                    objConn.Dispose();

                if (BouoraCmd != null)
                    BouoraCmd.Dispose();

                if (BouConn != null && BouConn.State == ConnectionState.Open)
                    BouConn.Close();

                if (BouConn != null)
                    BouConn.Dispose();
                OracleConnection.ClearAllPools();
                objConn = null;
                BouConn = null;
            }
        }

        /// <summary>
        /// 刪除未結清單資料
        /// </summary>
        /// <param name="posuuid_detailList">未結清單表頭檔主鍵值群</param>
        /// <returns></returns>
        public int delTO_CLOSE(OracleTransaction trans1, StringBuilder posuuid_detailList)
        {

            string where = "";
            if (posuuid_detailList.ToString().Substring(posuuid_detailList.ToString().Length - 1, 1) == ",")
                where = posuuid_detailList.ToString().Substring(0, posuuid_detailList.ToString().Length - 1);
            else
                where = posuuid_detailList.ToString();
            string sqlStr = @"Delete FROM TO_CLOSE_ITEM WHERE POSUUID_DETAIL IN (" + where + ") ";
            try
            {

                if (Advtek.Utility.OracleDBUtil.ExecuteSql(trans1, sqlStr) > -1)
                {
                    sqlStr = @"Delete FROM TO_CLOSE_DISCOUNT WHERE POSUUID_DETAIL IN (" + where + ") ";
                    if (Advtek.Utility.OracleDBUtil.ExecuteSql(trans1, sqlStr) > -1)
                    {
                        sqlStr = @"Delete FROM TO_CLOSE_HEAD where POSUUID_DETAIL IN (" + where + ") ";
                        int ret = Advtek.Utility.OracleDBUtil.ExecuteSql(trans1, sqlStr);
                        if (ret > -1)
                        {
                            return ret;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }

        }

        /// <summary>
        /// 新增取消交易失敗外部交易到log檔中
        /// </summary>
        public void InsertDataUploadLog(OracleTransaction trans1, string possuuid_detail)
        {

            string sqlStr = "";
            string uid = Advtek.Utility.GuidNo.getUUID().ToString();
            sqlStr = @"Insert into data_upload_log(id, posuuid_detail, data_type, scan_count, status, cancel_date) 
                                            Values(" + OracleDBUtil.SqlStr(uid) + ", " + OracleDBUtil.SqlStr(possuuid_detail) + ", '1', 0, '1', sysdate)";
            int ret = Advtek.Utility.OracleDBUtil.ExecuteSql(trans1, sqlStr);

        }

        static void OutputMsg(string s)
        {
            Console.WriteLine(s);
            sMSG += s + "\r\n";
        }
    }
}
