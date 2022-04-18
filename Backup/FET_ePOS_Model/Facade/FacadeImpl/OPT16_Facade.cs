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
    public class OPT16_Facade
    {

        /// <summary>
        /// 取得查詢結果HG_兌點名單檔(Temp)
        /// </summary>
        /// <param name="UPLOAD_BATCH_NO">上傳批號_UUID</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_HgConvertMemberListTmp(string UPLOAD_BATCH_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT SID, ACTIVITY_NO, ACTIVITY_NAME, MSISDN, EXCEPTIOB_CAUSE ");
            sb.Append(" FROM HG_CONVERT_MEMBER_LIST_TEMP ");
            sb.Append(" WHERE UPLOAD_BATCH_NO = " + OracleDBUtil.SqlStr(UPLOAD_BATCH_NO));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public int Query_HgConvertMemberListTmpIsEXCEPTIOB_CAUSE(string UPLOAD_BATCH_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT SID, ACTIVITY_NO, ACTIVITY_NAME, MSISDN, EXCEPTIOB_CAUSE ");
            sb.Append(" FROM HG_CONVERT_MEMBER_LIST_TEMP ");
            sb.Append(" WHERE UPLOAD_BATCH_NO = " + OracleDBUtil.SqlStr(UPLOAD_BATCH_NO));
            sb.Append(" AND EXCEPTIOB_CAUSE is not null ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt.Rows.Count;
        }

        /// <summary>
        /// 新增HG_兌點名單檔(Temp)
        /// </summary>
        /// <param name="DS">OPT16 DataSet</param>
        public void AddNew_HgConvertMemberListTemp(OPT16_HgConvertMemberList_DTO ds)
        {
           OracleDBUtil.Insert(ds.Tables["HG_CONVERT_MEMBER_LIST_TEMP"]);
        }

        /// <summary>
        /// 刪除HG_兌點名單檔
        /// </summary>
        /// <param name="DS">OPT16 DataSet</param>
        public void Delete_HgConvertMemberList(string UUID, string FuncId)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            StringBuilder _sbScript;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                _sbScript = new StringBuilder();
                _sbScript.Append(" Delete HG_CONVERT_MEMBER_LIST ");
                _sbScript.Append(" Where  ACTIVITY_ID = '" + UUID + "' and FUNC_ID = '" + FuncId + "' ");

                OracleDBUtil.ExecuteSql(objTX, _sbScript.ToString());
                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX = null;

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public void Delete_UPLOAD_TEMP(string BATCH_NO)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            StringBuilder _sbScript;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                _sbScript = new StringBuilder();
                _sbScript.Append("Delete UPLOAD_TEMP Where  BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO));
                OracleDBUtil.ExecuteSql(objTX, _sbScript.ToString());

                _sbScript.Length = 0;
                _sbScript.Append("Delete HG_CONVERT_MEMBER_LIST_TEMP Where UPLOAD_BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO));
                OracleDBUtil.ExecuteSql(objTX, _sbScript.ToString());

                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX = null;

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 新增HG_兌點名單Excel上傳檔(Temp)
        /// </summary>
        /// <param name="DS">OPT16 DataSet</param>
        /// <param name="BATCH_NO">上傳批號</param>
        public void AddNew_UploadTemp(OPT16_HgConvertMemberList_DTO ds, string BATCH_NO, string FUNC_ID, string DISCOUNT_ID)
        {
            OracleDBUtil.Insert(ds.Tables["UPLOAD_TEMP"]);
            Check_ImportData(BATCH_NO,FUNC_ID, DISCOUNT_ID); //欄位檢查
        }

        /// <summary>
        /// 檢查HG_兌點名單Excel上傳檔資料(Temp)
        /// </summary>
        /// <param name="BATCH_NO">上傳批號</param>
        public void Check_ImportData(string BATCH_NO,string FUNC_ID, string DISCOUNT_ID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                if (FUNC_ID == "OPT15")
                {
                    OracleDBUtil.ExecuteSql_SP(
                       objTX
                       , "SP_CHECK_HGCONVERTMEMBERLIST"
                       , new OracleParameter("v_BATCHNO", BATCH_NO)
                       , new OracleParameter("v_ACTIVITY_NO", DISCOUNT_ID)
                       );
                }
                else if (FUNC_ID == "OPT13")
                {
                    OracleDBUtil.ExecuteSql_SP(
                          objTX
                          , "SP_CHECK_HGCONVERT_REST_PROD"
                          , new OracleParameter("v_BATCHNO", BATCH_NO)
                          );
                
                }
                else if (FUNC_ID == "OPT13_1")
                {
                    OracleDBUtil.ExecuteSql_SP(
                          objTX
                          , "SP_CHECK_HGCONVERTMEMBERLIST_R"
                          , new OracleParameter("v_BATCHNO", BATCH_NO)
                          , new OracleParameter("v_ACTIVITY_NO", DISCOUNT_ID)
                          );

                }


                objTX.Commit();

            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX = null;

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 新增HG_兌點名單檔
        /// </summary>
        /// <param name="UPLOAD_BATCH_NO">上傳批號_UUID</param>
        /// <param name="FUNC_ID">Function ID(ex. OPT13 OPT15)</param>
        public void AddNew_HgConvertMemberList(string UPLOAD_BATCH_NO, string FUNC_ID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                if (FUNC_ID == "OPT15" )
                {
                    OracleDBUtil.ExecuteSql_SP(
                       objTX
                       , "SP_INSERT_HGCONVERTMEMBERLIST"
                       , new OracleParameter("v_BATCHNO", UPLOAD_BATCH_NO)
                       );
                }
                else if (FUNC_ID == "OPT13")
                {
                    OracleDBUtil.ExecuteSql_SP(
                          objTX
                          , "SP_INSERT_HGCONVERT_REST_PROD"
                          , new OracleParameter("v_BATCHNO", UPLOAD_BATCH_NO)
                          );
                }
                else if (FUNC_ID == "OPT13_1")
                {
                    OracleDBUtil.ExecuteSql_SP(
                          objTX
                          , "SP_INSERT_HG_MEMBERLIST_R"
                          , new OracleParameter("v_BATCHNO", UPLOAD_BATCH_NO)
                          );
                }
                objTX.Commit();

            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX = null;

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }


    }
}
