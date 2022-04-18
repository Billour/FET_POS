using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OracleClient;
using System.Globalization;
using FET.POS.Model.Helper;
using FET.POS.Model.DTO;
using Advtek.Utility;
using System.Collections.Specialized;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class SAL05_Facade
    {
        private DateTime SysDate = DateTime.Now;

        /// <summary>
        /// 取得表頭
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public DataTable getTO_CLOSE_HEAD(OrderedDictionary args)
        {
            OracleConnection advtekUtilityOracleDBUtilGetConnection = null;
            DataTable dt = new DataTable();

            string where = "";
            string sqlStr = @"SELECT ROWNUM ITEMS
                                    , DECODE(h.STATUS,1,'未結', 2,'已結', 3,'取消', h.STATUS) STATUS_NAME
                                    , DECODE(h.SERVICE_TYPE,1,'新啟用',2,'續約',3,'代收',4,'變更促代',5,'線上儲值',6,'維修',10,'網購',h.SERVICE_TYPE) SERVICE_TYPENAME 
                                    , E.EMPNAME SALE_PERSON 
                                    , h.*
                              FROM TO_CLOSE_HEAD h, EMPLOYEE E
                              WHERE h.SALE_PERSON = E.EMPNO(+) ";

            //sqlStr += "DECODE(h.SERVICE_TYPE,1,'新啟用',2,'續約',3,'2轉3',4,'換補卡',5,'代收',6,'維修',7,'網購',8,'預購',9,'MNP(IA)',10,'特殊授權(IA)',11,'變更促代-換貨(SSI)',12,'變更促代-不換貨(SSI)',h.SERVICE_TYPE) SERVICE_TYPENAME ,h.* FROM TO_CLOSE_HEAD h WHERE 1 = 1  ";
            if (args["STORE_NO"].ToString() != "") //門市代碼
                sqlStr += " AND h.STORE_NO = " + OracleDBUtil.SqlStr(args["STORE_NO"].ToString());
            //if (args["STATUS"].ToString() != "") //狀態
            //    sqlStr += " AND h.STATUS = " + OracleDBUtil.SqlStr(args["STATUS"].ToString());
            if (args["S_DATE"].ToString() != "")  //申請日期 起
                sqlStr += " AND trunc(h.APPLY_DATE) >= " + OracleDBUtil.DateStr(args["S_DATE"].ToString());
            if (args["E_DATE"].ToString() != "")  //申請日期 止
                sqlStr += " AND trunc(h.APPLY_DATE) <= " + OracleDBUtil.DateStr(args["E_DATE"].ToString());
            if (args["SERVICE_TYPE"].ToString() != "")  //服務類別
                sqlStr += " AND h.SERVICE_TYPE = " + OracleDBUtil.SqlStr(args["SERVICE_TYPE"].ToString());
            if (args["MSISDN"].ToString() != "")  //客戶門號
                where += getTO_CLOSE_ITEMByPOSUUID_DETAIL(args["MSISDN"].ToString());
            if (args["SALE_PERSON"] != null && args["SALE_PERSON"].ToString() != "ALL")  //銷售人員
                sqlStr += " AND h.SALE_PERSON = " + OracleDBUtil.SqlStr(args["SALE_PERSON"].ToString());
            if (where != "")
            {
                sqlStr += " AND h.POSUUID_DETAIL in(" + where + ")";
            }

            sqlStr += " Order by h.APPLY_DATE Desc ";

            try
            {
                if(StringUtil.CStr(args["STATUS"]) == "2")
                    sqlStr = sqlStr.Replace("TO_CLOSE_HEAD", "TO_CLOSE_HEAD_LOG");

                advtekUtilityOracleDBUtilGetConnection = OracleDBUtil.GetConnection();
                dt =  OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
                //if (dt == null || dt.Rows.Count == 0)
                //{
                //    sqlStr = sqlStr.Replace("TO_CLOSE_HEAD", "TO_CLOSE_HEAD_LOG");
                //    dt = OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
                //}
            }
            catch //(Exception ex)
            {  
            }
            finally
            {
                if (advtekUtilityOracleDBUtilGetConnection.State == ConnectionState.Open) advtekUtilityOracleDBUtilGetConnection.Close();
                advtekUtilityOracleDBUtilGetConnection.Dispose();
                OracleConnection.ClearAllPools();
            }

            return dt;
        }

        /// <summary>
        /// 取得表頭by Detail_UUID
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public DataTable getTO_CLOSE_HEADByUUID(string Detail_UUID, string Store_No)
        {
            OracleConnection advtekUtilityOracleDBUtilGetConnection = null;
            DataTable dt = new DataTable();

            string sqlStr = @"SELECT ROWNUM ITEMS, DECODE(h.STATUS,1,'未結',
                                                    2,'已結',
                                                    3,'取消',
                                                    h.STATUS) STATUS_NAME ,DECODE(h.SERVICE_TYPE,1,'新啟用',2,'續約',3,'2轉3',4,'換補卡',5,'代收',6,'維修',7,'網購',8,'預購',9,'MNP(IA)',10,'特殊授權(IA)',11,'變更促代-換貨(SSI)',12,'變更促代-不換貨(SSI)',h.SERVICE_TYPE) SERVICE_TYPENAME,h.* FROM TO_CLOSE_HEAD h WHERE 1 = 1  ";
            if (!string.IsNullOrEmpty(Detail_UUID))
            {
                sqlStr += " AND h.POSUUID_DETAIL = " + OracleDBUtil.SqlStr(Detail_UUID);
            }

            if (!string.IsNullOrEmpty(Store_No))
            {
                sqlStr += " AND h.STORE_NO = " + OracleDBUtil.SqlStr(Store_No);
            }

            try
            {
                advtekUtilityOracleDBUtilGetConnection = OracleDBUtil.GetConnection();
                dt = OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
            }
            catch //(Exception ex)
            { 
            }
            finally
            {
                if (advtekUtilityOracleDBUtilGetConnection.State == ConnectionState.Open) advtekUtilityOracleDBUtilGetConnection.Close();
                advtekUtilityOracleDBUtilGetConnection.Dispose();
                OracleConnection.ClearAllPools();
            }
            return dt;
        }

        public DataTable getEmployee(string STORENO, string loginEmpNo)
        {
            OracleConnection advtekUtilityOracleDBUtilGetConnection = null;
            DataTable dt = new DataTable();

            string sqlStr = @"Select Distinct Sale_Person as EMPNO From Sale_Head 
                                Where STORE_NO = " + OracleDBUtil.SqlStr(STORENO)
                                + " And Sale_Person = " + OracleDBUtil.SqlStr(loginEmpNo);

            try
            {
                advtekUtilityOracleDBUtilGetConnection = OracleDBUtil.GetConnection();
                dt = OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    sqlStr = @"SELECT * FROM EMPLOYEE WHERE EMPNO in 
                                (Select Distinct Sale_Person as EMPNO From Sale_Head 
                                Where STORE_NO = " + OracleDBUtil.SqlStr(STORENO) + ")";
                    dt = OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
                }
                else
                {
                    sqlStr = @"SELECT * FROM EMPLOYEE WHERE EMPNO = " + OracleDBUtil.SqlStr(loginEmpNo);
                    dt = OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        sqlStr = @"SELECT * FROM EMPLOYEE WHERE EMPNO in 
                                (Select Distinct Sale_Person as EMPNO From Sale_Head 
                                Where STORE_NO = " + OracleDBUtil.SqlStr(STORENO) + ") Union "
                                + " SELECT * FROM EMPLOYEE WHERE EMPNO = " + OracleDBUtil.SqlStr(loginEmpNo);
                        dt = OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
                    }
                    else
                    {
                        sqlStr = @"SELECT EMPNO, EMPNAME FROM EMPLOYEE WHERE EMPNO in 
                                (Select Distinct Sale_Person as EMPNO From Sale_Head 
                                Where STORE_NO = " + OracleDBUtil.SqlStr(STORENO) + ") Union "
                                + " SELECT Distinct " + OracleDBUtil.SqlStr(loginEmpNo) + " as EMPNO, " + OracleDBUtil.SqlStr(loginEmpNo)
                                + " as EMPNAME FROM EMPLOYEE WHERE STORENO = " + OracleDBUtil.SqlStr(STORENO);
                        dt = OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
                    }
                }
            }
            catch //(Exception ex)
            {

            }
            finally
            {
                if (advtekUtilityOracleDBUtilGetConnection.State == ConnectionState.Open) advtekUtilityOracleDBUtilGetConnection.Close();
                advtekUtilityOracleDBUtilGetConnection.Dispose();
                OracleConnection.ClearAllPools();
            }
            return dt;
        }

        /// <summary>
        /// 取得表身明細
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public DataTable getTO_CLOSE_ITEM(string POSUUID_DETAIL)
        {
            OracleConnection advtekUtilityOracleDBUtilGetConnection = null;
            DataTable dt = new DataTable();

            string sqlStr = @"SELECT ROWNUM ITEMS,I.*,MM.PROMO_NAME,P.PRODNAME, NVL(P.ISSTOCK,0) ISSTOCK
                            FROM TO_CLOSE_ITEM I,
                                    (Select * from MM Where PROMO_STATUS = '1') MM,PRODUCT P
                           WHERE I.PROMOTION_CODE = MM.PROMO_NO(+)
                             AND I.PRODNO = P.PRODNO(+) 
                             AND I.POSUUID_DETAIL = '" + POSUUID_DETAIL + "'";
            try
            {
                advtekUtilityOracleDBUtilGetConnection = OracleDBUtil.GetConnection();
                dt = Advtek.Utility.OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
                if (dt == null || dt.Rows.Count == 0)
                {
                    sqlStr = sqlStr.Replace("TO_CLOSE_ITEM", "TO_CLOSE_ITEM_LOG");
                    dt = OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
                }
            }
            catch //(Exception ex)
            {
            }
            finally
            {
                if (advtekUtilityOracleDBUtilGetConnection.State == ConnectionState.Open) advtekUtilityOracleDBUtilGetConnection.Close();
                advtekUtilityOracleDBUtilGetConnection.Dispose();
                OracleConnection.ClearAllPools();
            }
            return dt;
        }

        /// <summary>
        /// 取得表身UUID
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public string getTO_CLOSE_ITEMByPOSUUID_DETAIL(string MSISDN)
        {//ID,SEQNO,PROMOTION_CODE,PROMOTION_NAME,PRODNO,PRODNAME,SERIAL_NO,AMOUNT,SALE_QTY,
            //                 PRICE,CREATE_USER,BARCODE1,CREATE_DTM,BARCODE2,MODI_USER,BARCODE3,MODI_DTM,POSUUID_DETAIL
            //marked by Tina
//            string sqlStr = @"SELECT MSISDN
//                            FROM TO_CLOSE_ITEM i WHERE i.MSISDN = '" + MSISDN + "'";
//            OracleConnection advtekUtilityOracleDBUtilGetConnection = Advtek.Utility.OracleDBUtil.GetConnection();
//            DataTable dtd = Advtek.Utility.OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
//            foreach (DataRow dtr in dtd.Rows)
//            {
//                MSISDN += dtr["MSISDN"].ToString() + ",";
//            }
//            if (MSISDN.Length > 0)
//                MSISDN = MSISDN.Substring(0, MSISDN.Length - 1);
//            return MSISDN == "" ? "''" : MSISDN;
            //marked by Tina

            OracleConnection advtekUtilityOracleDBUtilGetConnection = null;
            DataTable dtd = new DataTable();

            string sqlStr = @"SELECT POSUUID_DETAIL
                            FROM TO_CLOSE_ITEM WHERE MSISDN = '" + MSISDN + "'";
            try
            {
                advtekUtilityOracleDBUtilGetConnection = OracleDBUtil.GetConnection();
                dtd = OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
            }
            catch //(Exception ex)
            {
            }
            finally
            {
                if (advtekUtilityOracleDBUtilGetConnection.State == ConnectionState.Open) advtekUtilityOracleDBUtilGetConnection.Close();
                advtekUtilityOracleDBUtilGetConnection.Dispose();
                OracleConnection.ClearAllPools();
            }

            string strUUIDs = "";
            foreach (DataRow dtr in dtd.Rows)
            {
                strUUIDs += "'" + dtr["POSUUID_DETAIL"].ToString() + "',";
            }
            if (strUUIDs.Length > 0)
                strUUIDs = strUUIDs.Substring(0, strUUIDs.Length - 1);
            return strUUIDs == "" ? "''" : strUUIDs;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public DataTable getTO_CLOSE_DISCOUNT(string POSUUID_DETAIL)
        {
            OracleConnection advtekUtilityOracleDBUtilGetConnection = null;
            DataTable dt = new DataTable();

            string sqlStr = @"SELECT d.*,M.DISCOUNT_NAME,
                              (SELECT PROMO_NAME FROM VW_DISCOUNTQUERY v WHERE ROWNUM = 1 AND v.DISCOUNT_MASTER_ID = d.DISCOUNT_ID) PROMO_NAME
                            FROM TO_CLOSE_DISCOUNT d,discount_master m
                             WHERE D.DISCOUNT_ID=M.DISCOUNT_CODE(+) 
                               AND d.POSUUID_DETAIL = '" + POSUUID_DETAIL + "'";
            try
            {
                advtekUtilityOracleDBUtilGetConnection = OracleDBUtil.GetConnection();
                dt = OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
                if (dt == null || dt.Rows.Count == 0)
                {
                    sqlStr = sqlStr.Replace("TO_CLOSE_DISCOUNT", "TO_CLOSE_DISCOUNT_LOG");
                    dt = OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
                }
            }
            catch //(Exception ex)
            {
            }
            finally
            {
                if (advtekUtilityOracleDBUtilGetConnection.State == ConnectionState.Open) advtekUtilityOracleDBUtilGetConnection.Close();
                advtekUtilityOracleDBUtilGetConnection.Dispose();
                OracleConnection.ClearAllPools();
            }

            return dt;

        }

        public DataTable getDiscountDetail(string DISCOUNT_CODE)
        {
            OracleConnection advtekUtilityOracleDBUtilGetConnection = null;
            DataTable dt = new DataTable();

            string sqlStr = @"SELECT s.STORENAME, v.* FROM VW_DISCOUNTQUERY v, STORE s WHERE s.STORE_NO = v.STORE_NO(+) AND ROWNUM = 1 AND v.DISCOUNT_CODE = " + OracleDBUtil.SqlStr(DISCOUNT_CODE);

            try
            {
                advtekUtilityOracleDBUtilGetConnection = OracleDBUtil.GetConnection();
                dt = OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
            }
            catch //(Exception ex)
            {
            }
            finally
            {
                if (advtekUtilityOracleDBUtilGetConnection.State == ConnectionState.Open) advtekUtilityOracleDBUtilGetConnection.Close();
                advtekUtilityOracleDBUtilGetConnection.Dispose();
                OracleConnection.ClearAllPools();
            }

            return dt;

        }
        /// <summary>
        /// 取消交易
        /// </summary>
        /// <param name="NewValues"></param>
        /// <param name="MODI_USER"></param>
        public void updateCancelTrancation(List<object> POSUUID_DETAILs, object MODI_USER)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                //表頭資料
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                SAL05_TO_CLOSE_DTO.TO_CLOSE_HEADDataTable dtm = new SAL05_TO_CLOSE_DTO.TO_CLOSE_HEADDataTable();
                foreach (DataColumn dc in dtm.Columns) dc.AllowDBNull = true;
                foreach (object POSUUID_DETAIL in POSUUID_DETAILs)
                {
                    SAL05_TO_CLOSE_DTO.TO_CLOSE_HEADRow dtrm = dtm.NewTO_CLOSE_HEADRow();
                    dtrm.POSUUID_DETAIL = POSUUID_DETAIL.ToString();
                    dtrm.STATUS = "3";//取消
                    dtrm.MODI_USER = MODI_USER.ToString();
                    dtrm.MODI_DTM = SysDate;
                    dtm.AddTO_CLOSE_HEADRow(dtrm);
                    dtm.AcceptChanges();
                }

                OracleDBUtil.UPDDATEByUUID(dtm, "POSUUID_DETAIL");
                objTX.Commit();
            }
            catch (Exception ex)
            {

                objTX.Rollback();
                throw ex;
            }
            finally {

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public static string get_to_close_type(string service_type)
        {
            return "";
        }

        /// <summary>
        /// 取得未結清單資料 for 取消交易用
        /// </summary>
        /// <param name="POSUUID_DETAILs">未結清單表頭檔鍵值</param>
        /// <returns></returns>
        public DataTable getCancleTO_CLOSE_DATA(List<object> POSUUID_DETAILs)
        {
            OracleConnection objConn = null;
            StringBuilder where = new StringBuilder("");
            foreach (object POSUUID_DETAIL in POSUUID_DETAILs)
            {
                if (where.ToString().IndexOf(POSUUID_DETAIL.ToString()) < 0)
                {
                    where.Append("'").Append(POSUUID_DETAIL.ToString()).Append("',");
                }
            }
            string sqlStr = @"Select h.POSUUID_DETAIL, h.SERVICE_TYPE, h.SERVICE_SYS_ID, h.BUNDLE_ID, h.TOTAL_AMOUNT, h.STORE_NO, h.SALE_PERSON 
                                    , i.BARCODE1, i.BARCODE2, i.BARCODE3, i.AMOUNT,h.MSISDN 
                                FROM TO_CLOSE_HEAD h, 
                                (Select POSUUID_DETAIL, BARCODE1, BARCODE2, BARCODE3, AMOUNT From TO_CLOSE_ITEM 
                                 Where BARCODE1 is not null or BARCODE2 is not null or BARCODE3 is not null 
                                 And POSUUID_DETAIL IN (" + where.ToString().Substring(0, where.ToString().Length - 1) + ")) i "
                                 + " Where h.POSUUID_DETAIL = i.POSUUID_DETAIL(+) And h.POSUUID_DETAIL in ("
                                 + where.ToString().Substring(0, where.ToString().Length - 1) + ") ";
            DataTable dt = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                dt = OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
            }
            catch
            {

            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
            return dt;
        }
    }
}
