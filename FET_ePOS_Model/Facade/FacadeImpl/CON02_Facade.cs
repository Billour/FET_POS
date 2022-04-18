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
    public class CON02_Facade 
    {
        /// <summary>
        /// </summary>
        /// <param name="sdt"></param>
        /// <param name="MODI_USER"></param>
        public static DataTable SP_CHECK_CONSIGNMENT_GOODS_PS(string SP_Name,string I_BATCH_NO, string C_BATCH_NO, string I_USER_ID, string I_FINC_ID)
        {
            DataTable O_DATA = new DataTable();
            using (OracleConnection oConn = OracleDBUtil.GetConnection())
            {
                OracleCommand oraCmd = new OracleCommand(SP_Name);//"SP_CHECK_CSM_SUPPLIER");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_BATCH_NO", OracleType.VarChar, 2000)).Value = I_BATCH_NO;
                if(!string.IsNullOrEmpty(C_BATCH_NO))
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
        /// 取得從查詢來的db資料CSM_SUPPLIER 寄外部廠商
        /// </summary>
        /// <param name="popSupplierNo"></param>
        /// <returns></returns>
        public DataTable GetDataFromDB(string popSupplierNo)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" SELECT DISTINCT SUPP_ID , SUPP_NO, SUPP_NAME , BOSS_NAME, BOSS_TEL_NO,
                         CSM_TYPE ,CONTACE , TELNO, SUPP_ADDRESS, CONTRACTNO, CLOSEDAY,COMPANY_ID ,
                        COMPANY_ID,S_DATE,E_DATE,FET_CONTACE_USER , EMAIL,FAX, AMOUNT_MAX , ACCOUNTCODE ,S_DATE ,
                        BOSS_TEL_NO,to_char(MODI_DTM,'yyyy/mm/dd hh24:mi:ss') MODI_DTM ,  E_DATE, MEMO,
                               MODI_USER   FROM CSM_SUPPLIER WHERE STATUS='1' ");
            if (!string.IsNullOrEmpty(popSupplierNo))
            {
                sb.AppendLine(" AND SUPP_NO = " + OracleDBUtil.SqlStr(popSupplierNo.ToString()));
            }
            sb.AppendLine(" ORDER BY SUPP_NO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

        /// <summary>
        /// 查詢使用的SQL
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="popSupplierNo"></param>
        /// <param name="popSupplierName"></param>
        /// <param name="txtCompanyId"></param>
        /// <returns></returns>
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
        
        public void ImportTemp(DataTable dt)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.ExecuteSql(objTX,
                    @"delete UPLOAD_TEMP
                    where nvl(STATUS,'T')<>'C' " +
                                              "and FINC_ID  = " + OracleDBUtil.SqlStr(dt.Rows[0]["FINC_ID"].ToString()) +
                                              "and USER_ID  = " + OracleDBUtil.SqlStr(dt.Rows[0]["USER_ID"].ToString()));

                OracleDBUtil.Insert(objTX, dt);

                //ora_com
                OracleDBUtil.ExecuteSql_SP(
                    objTX
                    , "PK_Upload_Check.ORD13_CHECK"
                    , new OracleParameter("p_BATCH_NO", dt.Rows[0]["BATCH_NO"].ToString())
                    , new OracleParameter("p_USER_ID", dt.Rows[0]["USER_ID"].ToString())
                    , new OracleParameter("p_FINC_ID", dt.Rows[0]["FINC_ID"].ToString())
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
                objTX.Dispose();

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }
        
        public INV05_RTNM_DTO.UPLOAD_TEMPDataTable GetTemp(string BATCH_NO)
        {
            INV05_RTNM_DTO.UPLOAD_TEMPDataTable dt = new INV05_RTNM_DTO.UPLOAD_TEMPDataTable();
            OracleConnection objConn = null;
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" SELECT * FROM UPLOAD_TEMP WHERE BATCH_NO=" + OracleDBUtil.SqlStr(BATCH_NO.ToString()));

                objConn = OracleDBUtil.GetConnection();
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }

            return dt;
        }

        //取得table CSM_SUPP_COMMISSION 佣金比率設定主檔
        public DataTable GetCOMMISSIONs(string ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" 
                 SELECT CSC_ID ,
                            COMMISSION COMMISSIONRATE,
                            S_DATE,
                            E_DATE FROM CSM_SUPP_COMMISSION
            ");
            if (!string.IsNullOrEmpty(ID))
            {
                sb.AppendLine("  WHERE  SUPP_ID = " + OracleDBUtil.SqlStr(ID.ToString()));
            }
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //取得table CMS_SUP_AMT_LEVEL 外部廠商金額級距
        public DataTable GetAmtLevel(string ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" 
                SELECT * FROM CSM_SUP_AMT_LEVEL
            ");
            if (!string.IsNullOrEmpty(ID))
            {
                sb.AppendLine("  WHERE  SUPP_ID = " + OracleDBUtil.SqlStr(ID.ToString()));
            }
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //取得table PRODUCT 商品主檔
        public DataTable GetSupProd(string ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" 
                SELECT  C.PRODNO , T.PRODNAME ,
                     C.ACCOUNT_CODE , C.S_YYMM , C.E_YYMM
                        FROM CSM_SUP_PROD C, PRODUCT T 
            ");

            if (!string.IsNullOrEmpty(ID))
            {
                sb.AppendLine(@"  WHERE  C.SUPP_ID = " + OracleDBUtil.SqlStr(ID.ToString()) +
                    " AND  C.PRODNO = T.PRODNO "                     
                    );
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //取得table CMS_CREDIT_CARD_PROCE_RATE 信用卡手續費率設定檔
        public DataTable GetCard(string ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" 
              SELECT  CREDIT_CARD_TYPE_ID TYPE ,CHARGE_RATE RATE  FROM CSM_CREDIT_CARD_PROCE_RATE
            ");
            if (!string.IsNullOrEmpty(ID))
            {
                sb.AppendLine("  WHERE  SUPP_ID = " + OracleDBUtil.SqlStr(ID.ToString()));
            }
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //取得table CSM_SUPPSTORE 外部廠商指定店組
        public DataTable GetStore(string ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" 
                SELECT S.STORE_NO, S.STORENAME 
               FROM CSM_SUPPSTORE C ,  STORE S
               WHERE  S.STORE_NO = C.STORE_NO
            ");
            if (!string.IsNullOrEmpty(ID))
            {
                sb.AppendLine("   AND  C.SUPP_ID = " + OracleDBUtil.SqlStr(ID.ToString()));
            }
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //取得 寄銷商品 Product 的會計科目
        public DataTable GetProdDataByKey(string PRODNO)
        {
            DataTable dt = new DataTable();
            dt = new Product_Facade().Query_ProductConsignmentSale(PRODNO, "", "", "");
            return dt;
        }

        //存檔 寄銷廠商
        public int SaveOut(CON02_CSM_SUPPLIER.CSM_SUPPLIERDataTable dtRTNM, 
            CON02_CSM_SUPPLIER.CSM_SUPP_COMMISSIONDataTable dtCommission ,
            CON02_CSM_SUPPLIER.CSM_SUPPSTOREDataTable dtSuppstore)
        {
            int intResult = 0;
            OracleConnection objConn = null;
            OracleTransaction objTx = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTx = objConn.BeginTransaction();

                if(dtRTNM != null)
                    intResult += OracleDBUtil.Insert(objTx, dtRTNM);
                if (dtCommission != null)
                    intResult += OracleDBUtil.Insert(objTx, dtCommission);
                if (dtSuppstore != null)
                    intResult += OracleDBUtil.Insert(objTx, dtSuppstore);
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

        //存檔 外部廠商
        public int SaveOutFactory(CON02_CSM_SUPPLIER.CSM_SUPPLIERDataTable dtRTNM,
            CON02_CSM_SUPPLIER.CSM_SUPP_COMMISSIONDataTable dtCommission,
            CON02_CSM_SUPPLIER.CSM_SUPPSTOREDataTable dtSuppstore,
            CON02_CSM_SUPPLIER.CSM_SUP_AMT_LEVELDataTable dtSupAmtLevel,
            CON02_CSM_SUPPLIER.CSM_SUP_PRODDataTable dtProd,
            CON02_CSM_SUPPLIER.CSM_CREDIT_CARD_PROCE_RATEDataTable dtCard
            )
        {
            int intResult = 0;
            OracleConnection objConn = null;
            OracleTransaction objTx = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTx = objConn.BeginTransaction();
                if (dtRTNM != null)
                    intResult += OracleDBUtil.Insert(objTx, dtRTNM);
                if (dtCommission != null)
                    intResult += OracleDBUtil.Insert(objTx, dtCommission);
                if (dtSuppstore != null)
                    intResult += OracleDBUtil.Insert(objTx, dtSuppstore);
                if (dtSupAmtLevel != null)
                    intResult += OracleDBUtil.Insert(objTx, dtSupAmtLevel);
                if (dtProd != null)
                    intResult += OracleDBUtil.Insert(objTx, dtProd);
                if (dtCard != null)
                    intResult += OracleDBUtil.Insert(objTx, dtCard);
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
        /// 刪除寄外部廠商資料
        /// </summary>
        /// <param name="suppNo"></param>
        public void DeleteSupplier(string suppNo)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                List<string> listSql = new List<string>();

                listSql.Add(@"DELETE FROM CSM_SUPP_COMMISSION WHERE SUPP_ID = 
                                    (SELECT SUPP_ID FROM CSM_SUPPLIER  WHERE SUPP_NO = " + OracleDBUtil.SqlStr(suppNo) + " )  ");

                listSql.Add(@"DELETE FROM CSM_SUPPSTORE WHERE SUPP_ID = 
                                    (SELECT SUPP_ID FROM CSM_SUPPLIER  WHERE SUPP_NO = " + OracleDBUtil.SqlStr(suppNo) + " )  ");

                listSql.Add(@"DELETE FROM CSM_SUP_AMT_LEVEL WHERE SUPP_ID = 
                                    (SELECT SUPP_ID FROM CSM_SUPPLIER  WHERE SUPP_NO = " + OracleDBUtil.SqlStr(suppNo) + " )  ");

                listSql.Add(@"DELETE FROM CSM_SUP_PROD WHERE SUPP_ID = 
                                    (SELECT SUPP_ID FROM CSM_SUPPLIER  WHERE SUPP_NO = " + OracleDBUtil.SqlStr(suppNo) + " )  ");

                listSql.Add(@"DELETE FROM CSM_CREDIT_CARD_PROCE_RATE WHERE SUPP_ID = 
                                    (SELECT SUPP_ID FROM CSM_SUPPLIER  WHERE SUPP_NO = " + OracleDBUtil.SqlStr(suppNo) + " )  ");

                listSql.Add(@"DELETE FROM CSM_SUPPLIER WHERE SUPP_ID = 
                                    (SELECT SUPP_ID FROM CSM_SUPPLIER  WHERE SUPP_NO = " + OracleDBUtil.SqlStr(suppNo) + " )  ");


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
