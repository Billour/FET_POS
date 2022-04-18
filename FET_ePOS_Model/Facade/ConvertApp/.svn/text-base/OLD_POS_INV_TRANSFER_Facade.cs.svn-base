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
    public class OLD_POS_INV_TRANSFER_Facade : BaseClass
   {
       

        public void UpdateINV_ON_HAND_CURRENT(DataTable dtAdd, DataTable dtUpd)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                
                if(dtAdd.Rows.Count>0)
                    OracleDBUtil.Insert(dtAdd);

                StringBuilder sb=new StringBuilder();
                string LOCID=Query_INV_GOODLOCUUID();
                foreach(DataRow dr in dtUpd.Rows)
                {
                    sb.Length=0;
                    sb.Append("Update INV_ON_HAND_CURRENT ");
                    sb.Append(" SET  ON_HAND_QTY=" + dr["ENDQTY"].ToString());
                    sb.Append("     ,MODI_USER='CONVERT' ");
                    sb.Append("     ,MODI_DTM=sysdate " ); 
                    sb.Append(" WHERE PRODNO=" + OracleDBUtil.SqlStr(dr["PRODNO"].ToString()));
                    sb.Append("  AND STORE_NO=" + OracleDBUtil.SqlStr(dr["STORENO"].ToString()) );
                    sb.Append("  AND STOCK_ID='" + LOCID + "'" );

                    OracleDBUtil.ExecuteSql(objTX, sb.ToString());
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
                objTX.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        

        public DataTable Query_ERP_THISMONTH_STOCK()
        {
            OracleConnection oCon = null;
            try
            {
                //ERP
                oCon = OracleDBUtil.GetERPPOSConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Length = 0;
                sb.Append("SELECT PRODNO,STORENO,ENDQTY,  ");
                sb.Append("'N' AS ISNEW  ");
                sb.Append("FROM STOCK  ");
                sb.Append("WHERE STKYYMM=to_char(sysdate,'YYMM') ");
                //sb.Append("WHERE STKYYMM='1012' ");
                //sb.Append("where rownum <2  ");

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

        public bool Query_INV_ON_HAND_CURRENT(string PRODNO,string STORENO)
        {
            OracleConnection oCon = null;
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("SELECT COUNT(*) FROM INV_ON_HAND_CURRENT  ");
                sb.Append("WHERE PRODNO=" + OracleDBUtil.SqlStr(PRODNO)  + " AND STORE_NO=" +OracleDBUtil.SqlStr(STORENO));
                
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

        public string Query_INV_GOODLOCUUID()
        {
            OracleConnection oCon = null;
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("SELECT INV_GOODLOCUUID() from dual  ");
                

                string sRet = "";
                oCon=OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];
                sRet = dt.Rows[0][0].ToString();
                
                return sRet;
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
