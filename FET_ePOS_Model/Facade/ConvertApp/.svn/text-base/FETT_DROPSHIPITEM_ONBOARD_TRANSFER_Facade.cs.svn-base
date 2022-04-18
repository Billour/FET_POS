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
    public class FETT_DROPSHIPITEM_ONBOARD_TRANSFER_Facade : BaseClass
   {
        public void Insert_DROPSHIPITEM_ONBOARD(DataTable dtAdd)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                
                OracleDBUtil.ExecuteSql(objTX,"DELETE FROM FETT_DROPSHIPITEM_ONBOARD_TEMP");

                //SOM
                //if (dtUpd.Rows.Count > 0)
                //    OracleDBUtil.UPDDATEByUUID(dtUpd, "SEGMENT1");
                if(dtAdd.Rows.Count>0)
                    OracleDBUtil.Insert(dtAdd);

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
        /// 多個KEY值用
        /// </summary>
        /// <param name="objTX">OracleTransaction</param>
        /// <param name="_DT">DataTable</param>
        /// <param name="arrUUIDField">string[]</param>
        /// <returns></returns>
        public int UPDDATEByUUID(OracleTransaction objTX, DataTable _DT, string[] arrUUIDField)
        {
            int intResult = 0;
            StringBuilder _sbScript;
            StringBuilder _sbWhereCondition;

            try
            {
                foreach (DataRow dr in _DT.Rows)
                {
                    _sbScript = new StringBuilder();
                    _sbWhereCondition =

                    _sbScript.Append(" UPDATE " + _DT.TableName.ToString());
                    _sbScript.Append(" SET ");

                    for (int i = 0; i <= _DT.Columns.Count - 1; i++)
                    {
                        if (Convert.IsDBNull(dr[i]) == false)
                        {
                            //取欄位名稱
                            _sbScript.Append(_DT.Columns[i].ColumnName.ToString() + "=");

                            //取欄位值
                            switch (_DT.Columns[i].DataType.Name.ToUpper())
                            {
                                case "DATETIME":
                                case "DATE":
                                    _sbScript.Append(OracleDBUtil.DateFormate(dr[i]));
                                    break;

                                default:
                                    _sbScript.Append(OracleDBUtil.SqlStr(StringUtil.CStr(dr[i])));
                                    break;
                            }
                            if (i != _DT.Columns.Count - 1) _sbScript.Append(",");
                        }
                    }

                    string tempSQL = _sbScript.ToString();
                    int intLength = tempSQL.Length;
                    string strCol = tempSQL.Substring(intLength - 1, 1);
                    if (strCol == ",") tempSQL = tempSQL.Substring(0, intLength - 1);

                    string strSQL = tempSQL.ToString();
                    strSQL += " Where  " + arrUUIDField[0] + "=" + OracleDBUtil.SqlStr(StringUtil.CStr(dr[arrUUIDField[0]]));
                    for (int i = 1; i < arrUUIDField.Length; i++) 
                    {
                        string typename=_DT.Columns[arrUUIDField[i]].DataType.Name.ToUpper();
                        if (typename == "INTEGER" || typename=="NUMBER" )
                        {
                            strSQL += " And " + arrUUIDField[i] + "=" + StringUtil.CStr(dr[arrUUIDField[i]]);
                        }
                        else 
                        {
                            strSQL += " And " + arrUUIDField[i] + "=" + OracleDBUtil.SqlStr(StringUtil.CStr(dr[arrUUIDField[i]]));
                        }
                        
                    }

                    intResult = OracleDBUtil.ExecuteSql(objTX, strSQL);

                    if (intResult == 0) throw new Exception("UPDATE SQL Execute 失敗. ");
                }
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {

            }
            return intResult;
        }

        public DataTable Query_OLD_FETT_DROPSHIPITEM_ONBOARD()
        {
            OracleConnection oCon = null;
            try
            {
                //OLD POS
                //string sCon = OracleDBUtil.GetOldPOSConnectionStringByTNSName();
                //OracleConnection oCon = OracleDBUtil.GetConnectionByConnString(sCon);
                oCon = OracleDBUtil.GetERPPOSConnection();

                ////回寫狀態STATUS='T'
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Length = 0;
                sb.Append("SELECT SEGMENT1, D_ONBOARD  ");
                sb.Append("FROM FETT_DROPSHIPITEM_ONBOARD  ");

                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }


       #region call SP
        public string PK_CONVERT_SP_DROPSHIPITEM_ONBOARD()
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            string sRet = "";
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //OracleDBUtil.ExecuteSql_SP(objTX,"PK_CONVERT.SP_DROPSHIPITEM_ONBOARD");

                OracleCommand oraCmd = new OracleCommand("PK_CONVERT.SP_DROPSHIPITEM_ONBOARD");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("outMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Connection = objConn;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();
                sRet = oraCmd.Parameters["outMESSAGE"].Value.ToString();

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
            return sRet;
        }
       #endregion
       

      
   }
}
