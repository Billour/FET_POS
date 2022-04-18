using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using FET.POS.Model.DTO;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class SAL15_Facade
    {
        /// <summary>
        /// 取得查詢結果HappyGo點數兌換-來店禮(Master Data)
        /// </summary>
        /// <returns>取得有效的來店禮</returns>
        public DataTable Query_HgConvertibleGiftM(string ActivityId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT M.ACTIVITY_ID
                            , M.ACTIVITY_NO
                            , M.ACTIVITY_NAME
                            , M.S_DATE
                            , M.E_DATE
                            , M.TYPE
                            , M.PRODNO
                            , TO_CHAR(M.DIVIDABLE_POINT) DIVIDABLE_POINT
                            , M.MEMBER_CHECK_FLAG
                            , M.USE_COUNT
                            ,(SELECT PRODNAME FROM PRODUCT WHERE PRODNO= M.PRODNO)AS PRODNAME
                            ,'' AS QTY
                            ,P.ISSTOCK
                        FROM HG_CONVERTIBLE_GIFT_M M , PRODUCT P
                        WHERE M.PRODNO = P.PRODNO  (+)
                        AND M.DEL_FLAG = 'N' 
                        AND (  
                             maxdate(TO_CHAR(M.S_DATE, 'yyyy/mm/dd'),TO_CHAR(SYSDATE,'YYYY/MM/DD'))
                          <= mindate(TO_CHAR(nvl(M.E_DATE,to_date('9999/12/31','yyyy/MM/dd')), 'yyyy/MM/dd'),  TO_CHAR(SYSDATE,'YYYY/MM/DD'))
                               )");
            if (!string.IsNullOrEmpty(ActivityId))
            {
                string[] arrayId = ActivityId.Split(",".ToCharArray());
                string Id = string.Empty;
                int i = 1;
                foreach (string activity in arrayId)
                {
                    if (i != arrayId.Length)
                    {
                        Id += OracleDBUtil.SqlStr(activity) + ",";
                    }
                    else
                    {
                        Id += OracleDBUtil.SqlStr(activity);
                    }
                    i++;
                }
                sb.AppendLine(" AND M.ACTIVITY_ID IN(" + Id + ")");
            }


            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }
        /// <summary>
        /// insert資料到db
        /// </summary>
        /// <param name="SAL015_HG_GIFT_EXCH_TRANS_DTO">SAL015_HG_GIFT_EXCH_TRANS_DTO</param>
        /// <returns></returns>
        public int InsertData(SAL015_HG_GIFT_EXCH_TRANS_DTO SAL015_HG_GIFT_EXCH_TRANS_DTO)
        {

            int intResult = 0;
            OracleConnection objConn = null;
            OracleTransaction objTx = null;
            SAL015_HG_GIFT_EXCH_TRANS_DTO.HG_GIFT_EXCH_TRANS_LOGDataTable dtM = SAL015_HG_GIFT_EXCH_TRANS_DTO.HG_GIFT_EXCH_TRANS_LOG;
            SAL015_HG_GIFT_EXCH_TRANS_DTO.HG_GIFT_EXCH_ITEMSDataTable dtD = SAL015_HG_GIFT_EXCH_TRANS_DTO.HG_GIFT_EXCH_ITEMS;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTx = objConn.BeginTransaction();
                if (dtM.Rows.Count > 0)
                {
                    intResult += OracleDBUtil.Insert(objTx, dtM);
                }
                if (dtD.Rows.Count > 0)
                {
                    intResult += OracleDBUtil.Insert(objTx, dtD);
                }

                objTx.Commit();
            }

            catch (Exception ex)
            {
                objTx.Rollback();
                throw ex;
            }
            finally
            {
                objTx.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
            return intResult;

        }
        /// <summary>
        /// 取得兌點名單SID
        /// </summary>
        /// <param name="ACTIVITY_ID">ACTIVITY_ID</param>
        /// <param name="MSISDN">門號</param>
        /// <returns>bool</returns>
        public string Query_HgConvertMemberList(string ACTIVITY_ID, string MSISDN)
        {
            string Sid = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT ACTIVITY_NO,SID ");
            sb.Append(" FROM HG_CONVERT_MEMBER_LIST ");
            sb.Append(" WHERE ACTIVITY_ID = " + OracleDBUtil.SqlStr(ACTIVITY_ID));
            sb.Append(" AND MSISDN = " + OracleDBUtil.SqlStr(MSISDN));
            sb.Append(" AND FUNC_ID = 'OPT15'");
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            Sid = dt.Rows.Count > 0 ? dt.Rows[0]["SID"].ToString() : null;
            return Sid;
        }

        /// <summary>
        /// 取得HG_GIFT_EXCH_ITEMS的HG_GIFT_ITEMS_ID
        /// </summary>
        /// <param name="HG_GIFT_LOG_ID">HG_GIFT_LOG_ID</param>
        /// <param name="GIFT_PRODNO">GIFT_PRODNO</param>
        /// <returns>bool</returns>
        public string GetHgGiftItemsId(string HG_GIFT_LOG_ID, string GIFT_PRODNO)
        {
            string Sid = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append(@" SELECT D.*
                        FROM HG_GIFT_EXCH_TRANS_LOG  M , HG_GIFT_EXCH_ITEMS D
                        WHERE M.HG_GIFT_LOG_ID = D.HG_GIFT_LOG_ID ");
            sb.Append(" AND M.HG_GIFT_LOG_ID= " + OracleDBUtil.SqlStr(HG_GIFT_LOG_ID));
            sb.Append(" AND D.GIFT_PRODNO = " + OracleDBUtil.SqlStr(GIFT_PRODNO));
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            Sid = dt.Rows.Count > 0 ? dt.Rows[0]["HG_GIF_ITEMS_ID"].ToString() : null;
            return Sid;
        }



        /// <summary>
        /// 檢查此活動是否有指定門市
        /// </summary>
        /// <param name="ActivityID">活動代號</param>
        /// <param name="Store_NO">門市代碼</param>
        /// <returns></returns>
        public bool Check_HgConvertibleGiftD(string ActivityID, string Store_NO)
        {
            bool chk = false;
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"SELECT SID,STORE_NO,DEL_FLAG
                        FROM HG_CONVERT_GIFT_D
                        WHERE 1=1");

                if (!string.IsNullOrEmpty(ActivityID))
                {
                    sb.Append(" AND ACTIVITY_ID = " + OracleDBUtil.SqlStr(ActivityID));
                }

                DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
                DataRow[] dr = dt.Select("STORE_NO='" + Store_NO + "' AND DEL_FLAG ='N' ");
                if (dt.Rows.Count == 0) //如果查不到資料表示無指定門市
                    chk = true;
                else               
                    if (dr.Length > 0) { chk = false; }
             
            }
            catch (Exception)
            {

            }

            return chk;
        }
        /// <summary>
        ///判斷此活動兌點名單是否有重複申請
        /// </summary>
        /// <param name="ACTIVITY_ID">ACTIVITY_ID</param>
        /// <param name="MSISDN">門號</param>
        /// <returns>bool</returns>
        public bool CheckMemberGiftExchRepeat(string ACTIVITY_ID, string MSISDN)
        {
            bool blResult = false;
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT USE_COUNT
                        FROM HG_CONVERT_MEMBER_LIST
                        WHERE USE_COUNT = 1
                        AND ACTIVITY_ID=  " + OracleDBUtil.SqlStr(ACTIVITY_ID));
            sb.Append(" AND MSISDN = " + OracleDBUtil.SqlStr(MSISDN));
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            blResult = dt.Rows.Count > 0 ? true : false;
            return blResult;
        }
        /// <summary>
        /// 確認是否可折抵
        /// </summary>
        /// <param name="ACTIVITY_ID"></param>
        /// <returns>true可折抵false不可折抵</returns>
        public bool CheckExchOk(string ACTIVITY_ID)
        {
            bool blResult = false;
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT USE_COUNT , CONSUME_COUNT
                        FROM HG_CONVERTIBLE_GIFT_M 
                        WHERE   (USE_COUNT - NVL(CONSUME_COUNT,0))  >= 1
                        AND ACTIVITY_ID=  " + OracleDBUtil.SqlStr(ACTIVITY_ID));
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            blResult = dt.Rows.Count > 0 ? true : false;
            return blResult;
        }

        /// <summary>
        ///回押兌換次數＝１, 是否兌換=1
        /// </summary>
        /// <param name="ACTIVITY_ID">ACTIVITY_ID</param>
        /// <param name="MSISDN">門號</param>
        /// <returns>bool</returns>
        public int UpdateMemberList(string ACTIVITY_ID, string MSISDN)
        {
            int intResult = 0;
            StringBuilder sb = new StringBuilder();
            OracleConnection objConn = null;
            OracleTransaction objTx = null;
            sb.Append(@"UPDATE HG_CONVERT_MEMBER_LIST
                        SET  USE_COUNT = 1 ,
                        EXCHANGE_FLAG = '1'
                        WHERE ACTIVITY_ID=  " + OracleDBUtil.SqlStr(ACTIVITY_ID));
            sb.Append(" AND MSISDN = " + OracleDBUtil.SqlStr(MSISDN));
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTx = objConn.BeginTransaction();

                intResult += OracleDBUtil.ExecuteSql(objTx, sb.ToString());
                objTx.Commit();
            }

            catch (Exception ex)
            {
                objTx.Rollback();
                throw ex;
            }
            finally
            {
                objTx.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
            return intResult;
        }
        /// <summary>
        /// 押已折抵次數加1
        /// </summary>
        /// <param name="ACTIVITY_ID">ACTIVITY_ID</param>
        /// <returns></returns>
        public int UpdateConvertibleGiftM(string ACTIVITY_ID)
        {
            int intResult = 0;
            StringBuilder sb = new StringBuilder();
            OracleConnection objConn = null;
            OracleTransaction objTx = null;
            sb.Append(@"UPDATE HG_CONVERTIBLE_GIFT_M
                        SET CONSUME_COUNT  =  NVL(CONSUME_COUNT,0) + 1
                        WHERE ACTIVITY_ID=  " + OracleDBUtil.SqlStr(ACTIVITY_ID));
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTx = objConn.BeginTransaction();

                intResult += OracleDBUtil.ExecuteSql(objTx, sb.ToString());
                objTx.Commit();
            }

            catch (Exception ex)
            {
                objTx.Rollback();
                throw ex;
            }
            finally
            {
                objTx.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
            return intResult;
        }
        /// <summary>
        /// 取多少金額可換1點數
        /// </summary>
        /// <returns></returns>
        public decimal GetPointCount()
        {
            decimal Money = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT ACCU_CURRENCY / DIVIDABLE_POINT AS MONEY,ACCU_CURRENCY,DIVIDABLE_POINT
            FROM HG_ACCUMULATE 
            WHERE TRUNC(SYSDATE) BETWEEN S_DATE AND E_DATE");
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            Money = dt.Rows.Count > 0 ? Convert.ToDecimal(dt.Rows[0]["MONEY"].ToString()) : 0;
            return Money;
        }
        /// <summary>
        /// 扣庫存
        /// </summary>
        /// <param name="objTX"></param>
        /// <param name="I_SERVICE_TYPE"></param>
        /// <param name="I_PRODNO"></param>
        /// <param name="I_STORE_NO"></param>
        /// <param name="I_STOCK_ID"></param>
        /// <param name="I_SHEET_NO"></param>
        /// <param name="I_INV_QTY"></param>
        /// <param name="I_USER"></param>
        /// <param name="I_SOURCE_REFERENCE"></param>
        /// <param name="O_RT_CODE"></param>
        /// <param name="O_RT_MESSAGE"></param>
        public void PK_INVENTORY_SALE(OracleTransaction objTX, string I_SERVICE_TYPE, string I_PRODNO, string I_STORE_NO,
                                      string I_STOCK_ID, string I_SHEET_NO, int I_INV_QTY, string I_USER, string I_SOURCE_REFERENCE,
                                      ref string O_RT_CODE, ref string O_RT_MESSAGE)
        {
            OracleCommand oraCmd = null;

            try
            {
                oraCmd = new OracleCommand("PK_INVENTORY.SALE");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_SERVICE_TYPE", OracleType.VarChar, 2000)).Value = I_SERVICE_TYPE;
                oraCmd.Parameters.Add(new OracleParameter("I_PRODNO", OracleType.VarChar, 2000)).Value = I_PRODNO;
                oraCmd.Parameters.Add(new OracleParameter("I_STORE_NO", OracleType.VarChar, 2000)).Value = I_STORE_NO;
                oraCmd.Parameters.Add(new OracleParameter("I_STOCK_ID", OracleType.VarChar, 2000)).Value = I_STOCK_ID;
                oraCmd.Parameters.Add(new OracleParameter("I_SHEET_NO", OracleType.VarChar, 2000)).Value = I_SHEET_NO;
                oraCmd.Parameters.Add(new OracleParameter("I_INV_QTY", OracleType.Number)).Value = I_INV_QTY;
                oraCmd.Parameters.Add(new OracleParameter("I_USER", OracleType.VarChar, 2000)).Value = I_USER;
                oraCmd.Parameters.Add(new OracleParameter("I_SOURCE_REFERENCE", OracleType.VarChar, 2000)).Value = I_SOURCE_REFERENCE;
                oraCmd.Parameters.Add(new OracleParameter("O_RT_CODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("O_RT_MESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();
                O_RT_CODE = oraCmd.Parameters["O_RT_CODE"].Value.ToString();
                O_RT_MESSAGE = oraCmd.Parameters["O_RT_MESSAGE"].Value.ToString();
            }
            catch (Exception ex)
            {
                O_RT_CODE = "999";
                O_RT_MESSAGE = "扣庫存失敗!";
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
            }
        }
    }
}
