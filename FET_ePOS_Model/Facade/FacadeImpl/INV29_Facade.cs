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
    public class INV29_Facade
    {
        /// <summary>
        /// IMEI Excel上傳檔(Temp)
        /// </summary>
        /// <param name="BATCH_NO">上傳批號</param>
        public void AddNew_IMEI_UPLOAD_TEMP(INV29_IMEIUpload_DTO ds)
        {
            OracleDBUtil.Insert(ds.Tables["UPLOAD_TEMP"]);
        }

        /// <summary>
        /// IMEI 依批號、UserID,刪除舊檔
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="sUserID">員編</param>
        /// <param name="sBatchNo">批號</param>
        public void Delete_IMEI_UPLOAD_TEMP(INV29_IMEIUpload_DTO ds, string sFINC_ID, string sUSER_ID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                string sqlstr = @"delete UPLOAD_TEMP
                    where  FINC_ID  = " + OracleDBUtil.SqlStr(sFINC_ID) +
                                              "and USER_ID  = " + OracleDBUtil.SqlStr(sUSER_ID);
                OracleDBUtil.ExecuteSql(objTX,sqlstr);

            

                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX.Dispose();

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// IMEI Excel上傳檔(Temp)
        /// </summary>
        /// <param name="BATCH_NO">上傳批號</param>
        public void Update_IMEI_UPLOAD_TEMP(INV29_IMEIUpload_DTO ds, string sBATCH_NO, string sUSER_ID)
        {
            OracleDBUtil.UPDDATE(ds.Tables["UPLOAD_TEMP"], " AND BATCH_NO= " + OracleDBUtil.SqlStr(sBATCH_NO) + " AND USER_ID= " + OracleDBUtil.SqlStr(sUSER_ID));
        }

        /// <summary>
        /// 取得IMEI上傳暫存檔(TEMP)
        /// </summary>
        /// <param name="BATCH_NO">上傳批號</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_IMEI_UPLOAD_TEMP(string sBATCH_NO, string sUSER_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT SID,BATCH_NO,STATUS,F1 as IVRCODE, F2 as CHANNEL, F3 as IMEI, F4 as PRODNO, F5 as ERROR ");
            sb.Append("FROM UPLOAD_TEMP ");
            sb.Append("WHERE 1 =1 ");

            if (!string.IsNullOrEmpty(sBATCH_NO))
            {
                sb.Append(" AND BATCH_NO = " + OracleDBUtil.SqlStr(sBATCH_NO));
            }
            if (!string.IsNullOrEmpty(sUSER_ID))
            {
                sb.Append(" AND USER_ID = " + OracleDBUtil.SqlStr(sUSER_ID));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public void Check_Upload_Temp(string sBATCH_NO, string sUSER_ID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.ExecuteSql_SP(
                   objTX
                   , "SP_INV29_CheckImei_Upload_Temp"
                   , new OracleParameter("inBATCHNO", sBATCH_NO)
                   , new OracleParameter("inUSERID", sUSER_ID)
                   );

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

        public void AddNew_IMEI(string sBATCH_NO, string sUSER_ID, string sHOST_ID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.ExecuteSql_SP(
                   objTX
                   , "SP_INV29_INSERT_IMEI"
                   , new OracleParameter("inBATCHNO", sBATCH_NO)
                   , new OracleParameter("inUSERID", sUSER_ID)
                   , new OracleParameter("inHOSTID", sHOST_ID)
                   );

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
        /// 新增DropShipment主動配貨主檔
        ///     DropShipment主動配貨明細檔
        /// </summary>
        /// <param name="sdt"></param>
        /// <param name="MODI_USER"></param>
        public DataTable SP_CHECK_IMEI_UPLOAD(string I_BATCH_NO, string I_USER_ID, string I_FINC_ID)
        {
            DataTable O_DATA = new DataTable();
            using (OracleConnection oConn = OracleDBUtil.GetConnection())
            {
                OracleCommand oraCmd = new OracleCommand("SP_CHECK_IMEI_UPLOAD");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_BATCH_NO", OracleType.VarChar, 2000)).Value = I_BATCH_NO;
                oraCmd.Parameters.Add(new OracleParameter("I_USER_ID", OracleType.VarChar, 2000)).Value = I_USER_ID;
                oraCmd.Parameters.Add(new OracleParameter("I_FINC_ID", OracleType.VarChar, 2000)).Value = I_FINC_ID;
                oraCmd.Parameters.Add(new OracleParameter("O_DATA", OracleType.Cursor)).Direction = ParameterDirection.Output;
                oraCmd.Connection = oConn;
                //oraCmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(oraCmd);
                da.Fill(O_DATA);
                return O_DATA;
            }

        }
    }
}
