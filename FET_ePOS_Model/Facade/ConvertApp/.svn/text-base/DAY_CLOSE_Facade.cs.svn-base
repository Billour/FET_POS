using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using Advtek.Utility;

using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.ConvertApp;
using FET.POS.Model.DTO.ConvertApp;
using FET.POS.Model.Common;


namespace FET.POS.Model.Facade.ConvertApp
{
    public class DAY_CLOSE_Facade : BaseClass
   {



        public string PK_CONVERT_SP_DAY_CLOSE()
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            string sRet = "";
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //OracleDBUtil.ExecuteSql_SP(objTX, "SP_COUNT_RECOMMANDED");
                OracleCommand oraCmd = new OracleCommand("PK_CONVERT.SP_DAY_CLOSE");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("outCODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("outMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Connection = objConn;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();
                sRet = oraCmd.Parameters["outMESSAGE"].Value.ToString();
                if (oraCmd.Parameters["outCODE"].Value.ToString().CompareTo("000") == 0)
                {
                    //更新SYS_PARA三個訂貨相關的欄位值
                    //SO_ORDER_NO、HR_ORDER_NO、PO_ORDER_NO
                    string SO_ORDER_NO = SerialNo.GenNo("SOORDER");
                    string HR_ORDER_NO = SerialNo.GenNo("HRORDER");
                    string PO_ORDER_NO = SerialNo.GenNo("POORDER");

                    string sql = @"update sys_para set para_value='" + SO_ORDER_NO + "' where para_key='SO_ORDER_NO' ";
                    oraCmd = new OracleCommand(sql, objConn, objTX);
                    oraCmd.CommandType = CommandType.Text;
                    oraCmd.ExecuteNonQuery();
                    sql = @" update sys_para set para_value='" + HR_ORDER_NO + "' where para_key='HR_ORDER_NO' ";
                    oraCmd.CommandText = sql;
                    oraCmd.ExecuteNonQuery();
                    sql = @" update sys_para set para_value='" + PO_ORDER_NO + "' where para_key='PO_ORDER_NO' ";
                    oraCmd.CommandText = sql;
                    oraCmd.ExecuteNonQuery();
                    //sql.Replace("\r\n", "\n");

                    
                    
                    //oraCmd.CommandType = CommandType.Text;
                    //oraCmd.CommandText = sql;
                    
                    
                    objTX.Commit();
                }
                else 
                {
                    throw new Exception(sRet);
                }
                

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
            return sRet;
        }

       

       

       #region call SP
       // public void Check_Upload_Temp(string sBATCH_NO, string sUSER_ID)
       // {
       //     OracleConnection objConn = null;
       //     OracleTransaction objTX = null;

       //     try
       //     {
       //         objConn = OracleDBUtil.GetConnection();
       //         objTX = objConn.BeginTransaction();

       //         OracleDBUtil.ExecuteSql_SP(
       //            objTX
       //            , "SP_INV29_CheckImei_Upload_Temp"
       //            , new OracleParameter("inBATCHNO", sBATCH_NO)
       //            , new OracleParameter("inUSERID", sUSER_ID)
       //            );

       //         objTX.Commit();

       //     }
       //     catch (Exception ex)
       //     {
       //         objTX.Rollback();
       //         throw ex;
       //     }
       //     finally
       //     {
       //         objTX = null;
       //         objConn = null;
       //     }
       // }
       #endregion
       

      
   }
}
