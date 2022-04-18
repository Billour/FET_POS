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
    public class LEA05_Facade
    {
        public DataTable Get_LEASE_STOCK(string IMEI)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" SELECT S.DEVICE_TYPE,L.LEASE_ID
                            ,decode(S.DEVICE_TYPE,'DMS','漫遊租賃','HRS','維修租賃') DEVICE_TYPE_NAME
                            ,S.RENT_STATUS SSTATUS
                            ,decode(S.RENT_STATUS,'10','有效','20','預約Booking','30','租賃Booking','40','租賃出借中','50','設備維修中','60','配件遺失,暫停租賃','70','領用到期','99','失效作廢') as SSTATUS_NAME
                            ,S.DEVICE_TYPE
                            ,S.ID,S.IMEI
                            ,S.STORE_NO,DAILY_RENT_PRICE,EARNEST_MONEY
                            ,NVL((SELECT STORENAME FROM STORE T WHERE T.STORE_NO=S.STORE_NO),'') STORE_NAME    FROM LEASE_STOCK S,LEASE_M L
                             WHERE S.LEASE_ID=L.LEASE_ID");
            sb.AppendLine(" AND S.IMEI = " + OracleDBUtil.SqlStr(IMEI));
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }


        public DataTable GetRENT_M(string IMEI, string RSTATUS)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT R.RENT_SHEET_NO,TO_CHAR(R.BOOKING_DATE,'YYYY/MM/DD') BOOKING_DATE
                            ,R.RENT_STATUS RSTATUS
                            ,decode(R.RENT_STATUS,'00','未存檔','10','已預約','20','出租中','30','設備歸還結案','40','預約取消','50','須賠償') as RSTATUS_NAME
                            ,R.MSISDN,R.CUST_NAME
                            ,R.CUST_LEVEL,R.SEX
                            ,TO_CHAR(R.PRE_S_DATE,'YYYY/MM/DD')  PRE_S_DATE				
                            ,TO_CHAR(R.PRE_E_DATE,'YYYY/MM/DD')  PRE_E_DATE        
                            ,R.RECEIVE_TYPE,R.CUST_ADDR                                                       
                            ,TO_CHAR(R.DEPARTURE_DTM,'YYYY/MM/DD')  DEPARTURE_DTM     
                            ,TO_CHAR(R.ARRIVAL_DTM,'YYYY/MM/DD')  ARRIVAL_DTM       
                            ,TO_CHAR(R.REAL_RECEV_DATE,'YYYY/MM/DD')  REAL_RECEV_DATE   
                            ,TO_CHAR(R.REAL_RETURN_DTM,'YYYY/MM/DD')  REAL_RETURN_DTM   
                            ,R.IS_IND_FLAG,R.EARNEST_AMT,R.RENT_AMT,R.IND_AMT
                            FROM LEASE_STOCK S,RENT_M R
                            WHERE S.IMEI=R.IMEI ");

            sb.AppendLine(" AND R.RENT_STATUS = " + OracleDBUtil.SqlStr(RSTATUS));
            sb.AppendLine(" AND S.IMEI = " + OracleDBUtil.SqlStr(IMEI));
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }


        public DataTable GetIND_ITEM_NAME(string IMEI)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT IND_ITEM_NAME,IND_UNIT_PRICE
                            FROM LEASE_STOCK L,RENT_INDEMNIFY_ITEMS R
                            WHERE L.LEASE_ID=R.LEASE_ID");
            sb.AppendLine(" AND IMEI = " + OracleDBUtil.SqlStr(IMEI));
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

        public string GetRENT_STATUS(string IMEI)
        {
            string str = "";
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT RENT_STATUS ");
                sb.Append("FROM   LEASE_STOCK ");
                sb.Append(" WHERE IMEI =" + OracleDBUtil.SqlStr(IMEI) + " ");
                DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
                foreach (DataRow dr in dt.Rows)
                {
                    str = dr["RENT_STATUS"].ToString();
                }
                return str;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (objConn.State == ConnectionState.Open) objConn.Close();
                //objConn.Dispose();
                //OracleConnection.ClearAllPools();
            }
        }

        public string GetREAL_IDEMNIFY_ITEMS(string SHEET_NO, string ITEMS)
        {
            string str = "N";
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * ");
                sb.Append("FROM   REAL_IDEMNIFY_ITEMS ");
                sb.Append(" WHERE RENT_SHEET_NO =" + OracleDBUtil.SqlStr(SHEET_NO) + " ");
                sb.Append(" AND RENT_INDEMNIFY_ITEMS =" + OracleDBUtil.SqlStr(ITEMS) + " ");
                DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
                if (dt.Rows.Count > 0)
                {
                    str = "Y";
                }
                return str;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (objConn.State == ConnectionState.Open) objConn.Close();
                //objConn.Dispose();
                //OracleConnection.ClearAllPools();
            }
        }

        public int SaveData(DataTable RENT_M, string DoNUC, DataTable REAL_IDEMNIFY_ITEMS)
        {
            int intResult = 0;
            OracleConnection objConn = null;
            OracleTransaction objTx = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTx = objConn.BeginTransaction();
                string strSql = "";
                switch ( DoNUC.Substring (0,1))
                {
                        //預約(新增)
                    case "N":
                        intResult += OracleDBUtil.Insert(objTx, RENT_M);
                        strSql = "  UPDATE LEASE_STOCK";
                        strSql += "           SET RENT_STATUS = '20'    ";
                        strSql += "           WHERE LEASE_ID = " + OracleDBUtil.SqlStr(RENT_M.Rows[0]["LEASE_ID"].ToString());
                        OracleDBUtil.ExecuteSql(objTx, strSql);
                        break;
                    //預約(修改)
                    case "U":
                        intResult += OracleDBUtil.UPDDATEByUUID_IncludeDBNull(objTx, RENT_M, "RENT_SHEET_NO");
                        strSql = "  UPDATE LEASE_STOCK";
                        strSql += "           SET RENT_STATUS = '20'    ";
                        strSql += "           WHERE LEASE_ID = " + OracleDBUtil.SqlStr(RENT_M.Rows[0]["LEASE_ID"].ToString());
                        OracleDBUtil.ExecuteSql(objTx, strSql);
                        break;
                    //作廢
                    case "C":
                        strSql = "  UPDATE RENT_M";
                        strSql += "           SET RENT_STATUS = '40'    ";
                        strSql += "           WHERE RENT_SHEET_NO = " + OracleDBUtil.SqlStr(RENT_M.Rows[0]["RENT_SHEET_NO"].ToString());
                        OracleDBUtil.ExecuteSql(objTx, strSql);
                        strSql = "  UPDATE LEASE_STOCK";
                        strSql += "           SET RENT_STATUS = '10'    ";
                        strSql += "           WHERE LEASE_ID = " + OracleDBUtil.SqlStr(RENT_M.Rows[0]["LEASE_ID"].ToString());
                        OracleDBUtil.ExecuteSql(objTx, strSql);
                        break;
                    //領取
                    case "G":
                        if (DoNUC.Substring(1, 1) == "N")
                        {
                            intResult += OracleDBUtil.Insert(objTx, RENT_M);
                        }
                        strSql = "  UPDATE RENT_M";
                        strSql += "           SET RENT_STATUS = '20'    ";
                        strSql += "           WHERE RENT_SHEET_NO = " + OracleDBUtil.SqlStr(RENT_M.Rows[0]["RENT_SHEET_NO"].ToString());
                        OracleDBUtil.ExecuteSql(objTx, strSql);
                        strSql = "  UPDATE LEASE_STOCK";
                        strSql += "           SET RENT_STATUS = '40'    ";
                        strSql += "           WHERE LEASE_ID = " + OracleDBUtil.SqlStr(RENT_M.Rows[0]["LEASE_ID"].ToString());
                        OracleDBUtil.ExecuteSql(objTx, strSql);
                        break;
                    //歸還
                    case "R":
                        strSql = "  UPDATE RENT_M";
                        strSql += "           SET RENT_STATUS = '30'    ";
                        strSql += "           WHERE RENT_SHEET_NO = " + OracleDBUtil.SqlStr(RENT_M.Rows[0]["RENT_SHEET_NO"].ToString());
                        OracleDBUtil.ExecuteSql(objTx, strSql);
                        strSql = "  UPDATE LEASE_STOCK";
                        strSql += "           SET RENT_STATUS = '10'    ";
                        strSql += "           WHERE LEASE_ID = " + OracleDBUtil.SqlStr(RENT_M.Rows[0]["LEASE_ID"].ToString());
                        OracleDBUtil.ExecuteSql(objTx, strSql);
                        break;
                }
                strSql = " DELETE REAL_IDEMNIFY_ITEMS WHERE ";
                strSql += " RENT_SHEET_NO = " + OracleDBUtil.SqlStr(RENT_M.Rows[0]["RENT_SHEET_NO"].ToString());
                OracleDBUtil.ExecuteSql(objTx, strSql);
                if (REAL_IDEMNIFY_ITEMS != null)
                {
                    intResult += OracleDBUtil.Insert(objTx, REAL_IDEMNIFY_ITEMS);
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

    }
}
