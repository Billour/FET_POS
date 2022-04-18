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
    public class LEA01_Facade 
    {
        
        public DataTable GetSelectData(string strType, string popSupplierNo, string popSupplierName, string txtCompanyId)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" SELECT SUPP_ID , SUPP_NO, SUPP_NAME , 
                        CASE CSM_TYPE  WHEN '1' then '寄銷廠商' WHEN '2' then '外部廠商' else CSM_TYPE end  CSM_TYPE  ,
                        COMPANY_ID,S_DATE,E_DATE,FET_CONTACE_USER
                        ,BOSS_TEL_NO,to_char(MODI_DTM,'yyyy/mm/dd hh24:mi:ss') MODI_DTM , 
                               MODI_USER   FROM CSM_SUPPLIER WHERE STATUS='1' ");
            if (!string.IsNullOrEmpty(strType))
            {
                sb.AppendLine(" AND CSM_TYPE = " + OracleDBUtil.SqlStr(strType.ToString()));
            }
            if (!string.IsNullOrEmpty(popSupplierNo))
            {
                sb.AppendLine(" AND SUPP_NO = " + OracleDBUtil.SqlStr(popSupplierNo.ToString()));
            }

            if (!string.IsNullOrEmpty(popSupplierName))
            {
                sb.AppendLine(" AND SUPP_NAME like " + OracleDBUtil.LikeStr(popSupplierName.ToString()));
            }
            if (!string.IsNullOrEmpty(txtCompanyId))
            {
                sb.AppendLine(" AND COMPANY_ID = " + OracleDBUtil.SqlStr(txtCompanyId.ToString()));
            }
            sb.AppendLine(" ORDER BY SUPP_NO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }
        
        //取得table lease_m
        public DataTable GetLeaseData( string ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" 
                SELECT * FROM LEASE_M
            ");
            if (!string.IsNullOrEmpty(ID))
            {
                sb.AppendLine("  WHERE  LEASE_ID = " + OracleDBUtil.SqlStr(ID.ToString()));
            }
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

        //取得table RENT_INDEMNIFY_ITEMS 設備租賃賠償項目
        public DataTable GetRentIndemnifyItems(string ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" 
                SELECT * FROM RENT_INDEMNIFY_ITEMS
            ");
            if (!string.IsNullOrEmpty(ID))
            {
                sb.AppendLine("  WHERE  LEASE_ID = " + OracleDBUtil.SqlStr(ID.ToString()));
            }
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

        //取得table RENT_DISCOUNT_ITEMS 設備租賃折扣項目
        public DataTable GetRentDiscountItems(string ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" 
                SELECT R.PRODNO,
                            (SELECT P.PRODNAME FROM PRODUCT P WHERE P.PRODNO = R.PRODNO)  PRODNAME,
                            R.DISCOUNT_AMT,
                            R.DISCOUNT_RATE ,
                            R.COST_CENTER_NO ,
                            R.ACCOUNT_CODE ,
                            R.RENT_DISCOUNT_ID
                        FROM RENT_DISCOUNT_ITEMS R
            ");
            if (!string.IsNullOrEmpty(ID))
            {
                sb.AppendLine("   WHERE  R.LEASE_ID = " + OracleDBUtil.SqlStr(ID.ToString()));
            }
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

        public DataTable getDtStock()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT L.STORE_NO  STORE_NO, S.STORENAME STORE_NAME ,  L.SERIAL_NO SERIAL_NO FROM LEASE_STOCK L ,
            STORE S
            WHERE RENT_STATUS IN ('10' )
                AND L.STORE_NO IN( SELECT DISTINCT STORE_NO FROM STORE)
                AND L.STORE_NO = S.STORE_NO
                ");
            sb.Append(" ORDER BY STORE_NO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得　外部廠商UUID
        /// </summary>
        /// <returns></returns>
        public string getSuppID(string suppNO)
        {
            StringBuilder sb = new StringBuilder();
            string str = string.Empty;

            sb.AppendLine(@" SELECT DISTINCT SUPP_ID FROM CSM_SUPPLIER ");
            if (!string.IsNullOrEmpty(suppNO))
            {
                sb.AppendLine("  WHERE SUPP_NO = " + OracleDBUtil.SqlStr(suppNO.ToString()));
            }
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            if (dt.Rows.Count != 0)
            {
                str = dt.Rows[0]["SUPP_ID"].ToString();
            }
            return str;
        }

        /// <summary>
        /// 取得　外部廠商 NO
        /// </summary>
        /// <returns></returns>
        public string getSuppNO(string suppID)
        {
            StringBuilder sb = new StringBuilder();
            string str = string.Empty;

            sb.AppendLine(@" SELECT DISTINCT SUPP_NO FROM CSM_SUPPLIER ");
            if (!string.IsNullOrEmpty(suppID))
            {
                sb.AppendLine("  WHERE SUPP_ID  = " + OracleDBUtil.SqlStr(StringUtil.CStr(suppID)));
            }
            DataTable dt = OracleDBUtil.Query_Data(StringUtil.CStr(sb));
            if (dt.Rows.Count != 0)
            {
                str = StringUtil.CStr(dt.Rows[0]["SUPP_NO"]);
            }
            else
            {
                str = "";
            }
            return str;
        }

        /// <summary>
        /// 取得　設備租賃手機庫存
        /// </summary>
        /// <returns></returns>
        public DataTable getLEASE_STOCK()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" SELECT * FROM LEASE_STOCK  ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //存檔 設備租賃設定　        
        public int SaveOut(LEA01_LEASE_M.LEASE_MDataTable dtM,
            LEA01_LEASE_M.RENT_INDEMNIFY_ITEMSDataTable dtIND,
            LEA01_LEASE_M.RENT_DISCOUNT_ITEMSDataTable dtDISCOUNT)
        {
            int intResult = 0;
            OracleConnection objConn = null;
            OracleTransaction objTx = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTx = objConn.BeginTransaction();

                if (dtM != null)
                    intResult += OracleDBUtil.Insert(objTx, dtM);
                if (dtIND != null)
                    intResult += OracleDBUtil.Insert(objTx, dtIND);
                if (dtDISCOUNT != null)
                    intResult += OracleDBUtil.Insert(objTx, dtDISCOUNT);
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

        //刪除 
        public int DeleteAll(DataTable dtM, DataTable dtIND, DataTable dtDISCOUNT)
        {
            int intResult = 0;
            OracleConnection objConn = null;
            OracleTransaction objTx = null;
            try
            {//DELETE(OracleTransaction objTX, DataTable _DT, string _strWhereScript)
                objConn = OracleDBUtil.GetConnection();
                objTx = objConn.BeginTransaction();

                if (dtM != null)
                    intResult += OracleDBUtil.DELETE(objTx, dtM, " AND " + dtM.Columns[0].ColumnName + " = " + dtM.Rows[0] );
                if (dtIND != null)
                    intResult += OracleDBUtil.DELETE(objTx, dtIND, " AND 1=1 ");
                if (dtDISCOUNT != null)
                    intResult += OracleDBUtil.DELETE(objTx, dtDISCOUNT, " AND 1=1 ");
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
            return 1;
        }

        //設備租賃設定查詢用SQL
        public DataTable GetSelectData()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" 
                SELECT * FROM VW_LEA07
            ");
            //if (!string.IsNullOrEmpty(strType))
            //{
            //    sb.AppendLine(" AND CSM_TYPE = " + OracleDBUtil.SqlStr(strType.ToString()));
            //}

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

        /// <summary>
        /// 刪除設備租賃賠償項目
        /// </summary>
        /// <param name="suppNo"></param>
        public void DeleteLease(string leaseID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                List<string> listSql = new List<string>();

                listSql.Add(@"DELETE FROM LEASE_M WHERE LEASE_ID = " + OracleDBUtil.SqlStr(leaseID) + " )  ");

                listSql.Add(@"DELETE FROM RENT_DISCOUNT_ITEMS  WHERE LEASE_ID =  " + OracleDBUtil.SqlStr(leaseID) + " )  ");

                listSql.Add(@"DELETE FROM RENT_INDEMNIFY_ITEMS  WHERE LEASE_ID =  " + OracleDBUtil.SqlStr(leaseID) + " )  ");

                foreach (string sSQL in listSql)
                {
                    OracleDBUtil.ExecuteSql(objTX, sSQL);
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
        /// </summary>
        /// <param name="sdt"></param>
        /// <param name="MODI_USER"></param>
        public static DataTable SP_CHECK_CONSIGNMENT_GOODS_PS(string SP_Name, string I_BATCH_NO, string C_BATCH_NO, string I_USER_ID, string I_FINC_ID)
        {
            DataTable O_DATA = new DataTable();
            using (OracleConnection oConn = OracleDBUtil.GetConnection())
            {
                OracleCommand oraCmd = new OracleCommand(SP_Name);//"SP_CHECK_CSM_SUPPLIER");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_BATCH_NO", OracleType.VarChar, 2000)).Value = I_BATCH_NO;
                if (!string.IsNullOrEmpty(C_BATCH_NO))
                    oraCmd.Parameters.Add(new OracleParameter("C_BATCH_NO", OracleType.VarChar, 2000)).Value = C_BATCH_NO;
                oraCmd.Parameters.Add(new OracleParameter("I_USER_ID", OracleType.VarChar, 2000)).Value = I_USER_ID;
                oraCmd.Parameters.Add(new OracleParameter("I_FINC_ID", OracleType.VarChar, 2000)).Value = I_FINC_ID;
                oraCmd.Parameters.Add(new OracleParameter("O_DATA", OracleType.Cursor)).Direction = ParameterDirection.Output;
                oraCmd.Connection = oConn;
                oraCmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(oraCmd);
                da.Fill(O_DATA);
                return O_DATA;
            }

        }

        /// <summary>
        /// 刪除設備租賃設定作業資料
        /// </summary>
        /// <param name="groupNo"></param>
        public void DeleteGroup(string groupNo)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                List<string> listSql = new List<string>();

                listSql.Add(@"DELETE FROM LEASE_M WHERE LEASE_ID = " + OracleDBUtil.SqlStr(groupNo) + "   ");

                listSql.Add(@"DELETE FROM RENT_DISCOUNT_ITEMS WHERE LEASE_ID = " + OracleDBUtil.SqlStr(groupNo) + "   ");

                listSql.Add(@"DELETE FROM RENT_INDEMNIFY_ITEMS WHERE LEASE_ID = " + OracleDBUtil.SqlStr(groupNo) + "   ");

                foreach (string sSQL in listSql)
                {
                    OracleDBUtil.ExecuteSql(objTX, sSQL);
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
