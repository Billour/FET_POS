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
    public class POS_SOMSOD_WEB_SOMSOD_TRANSFER_Facade : BaseClass
   {
        public void UpdateAndInsert_SOM(DataTable dtAddSOM, DataTable dtUpdSOM, DataTable dtAddSOD, DataTable dtUpdSOD)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                //SOM
                if (dtUpdSOM.Rows.Count > 0)
                    OracleDBUtil.UPDDATEByUUID(dtUpdSOM, "SOID");
                if(dtAddSOM.Rows.Count>0)
                    OracleDBUtil.Insert(dtAddSOM);
                //SOD
                if (dtUpdSOD.Rows.Count > 0)
                    UPDDATEByUUID(objTX, dtUpdSOD, new string[] { "SOID", "STORENO", "LINENUMBER" });
                if (dtAddSOD.Rows.Count > 0)
                    OracleDBUtil.Insert(dtAddSOD);

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

        public DataTable Query_OLD_SOM()
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
                sb.Append("SELECT *  ");
                sb.Append("FROM SOM  ");

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

        public DataTable Query_OLD_SOD()
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
                sb.Append("SELECT *  ");
                sb.Append("FROM SOD  ");

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

        public bool Query_SOM_COUNT1(string SOID)
        {
            OracleConnection oCon = null;
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("SELECT count(*) FROM SOM  ");
                sb.Append("WHERE SOID='" + SOID + "' ");
                bool bRet = false;
                oCon=OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];
                if (dt.Rows[0][0].ToString() != "0")
                {
                    bRet = true;
                }
                return bRet;
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

        public bool Query_SOM_COUNT2(string SOID,string BSS_FLAG,string BSS_DATE, string BSN_FLAG, string BSN_DATE, string BR_FLAG, string BR_DATE)
        {
            OracleConnection oCon = null;
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("SELECT count(*) FROM SOM  ");
                sb.Append("WHERE SOID='" + SOID + "' and nvl(BSS_FLAG,' ')='" + BSS_FLAG + "' and nvl(to_char(BSS_DATE,'YYYY/MM/DD'),' ')='" + BSS_DATE  + "'" +
                             " and nvl(BSN_FLAG,' ')='" + BSN_FLAG + "' and nvl(to_char(BSN_DATE,'YYYY/MM/DD'),' ')='" + BSN_DATE  + "'" +
                             " and nvl(BR_FLAG,' ')='" + BR_FLAG + "' and nvl(to_char(BR_DATE,'YYYY/MM/DD'),' ')='" + BR_DATE  + "'");
                bool bRet = false;
                oCon=OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];
                if (dt.Rows[0][0].ToString() != "0")
                {
                    bRet = true;
                }
                return bRet;
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

        public bool Query_SOD_COUNT(string SOID,string STORENO,string LINENUMBER)
        {
            OracleConnection oCon = null;
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("SELECT count(*) FROM SOD  ");
                sb.Append("WHERE SOID='" + SOID + "' AND LINENUMBER=" + LINENUMBER + " AND STORENO='" + STORENO  + "'");
                bool bRet = false;
                oCon=OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];
                if (dt.Rows[0][0].ToString() != "0")
                {
                    bRet = true;
                }
                return bRet;
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
