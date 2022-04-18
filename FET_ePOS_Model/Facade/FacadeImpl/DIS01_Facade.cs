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
    public class DIS01_Facade
    {
        #region Query Data

        public DataTable Query_ProductType(string CCN)
        {
            //**2011/04/22 Tina：商品分類來源從PRODUCT_TYPE改成CCN_ACCOUNT FOR UAT ISSUE.
            StringBuilder sb = new StringBuilder();
//            sb.AppendLine(@"SELECT PRODTYPENO
//                                , PRODTYPENAME 
//                            FROM PRODUCT_TYPE
//                            WHERE 1 =1 
//                        ");
            sb.AppendLine(@"SELECT DESCRIPTION PRODTYPENO
                                 , DESCRIPTION PRODTYPENAME 
                            FROM CCN_ACCOUNT
                            WHERE 1 =1 
                        ");
            if (!string.IsNullOrEmpty(CCN))
            {
                sb.AppendLine(" AND CCN = " + OracleDBUtil.SqlStr(CCN));
            }
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得促銷資訊(只包含已生效)
        /// </summary>
        /// <param name="PROMO">促銷代號</param>
        /// <returns></returns>
        public DataTable Query_PromoData_ByKey(string PROMO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT PROMO_NAME 
                            FROM MM 
                            WHERE TRUNC(SYSDATE) >= TRUNC(B_DATE) 
                              AND TRUNC(SYSDATE) <= TRUNC(NVL(E_DATE, TO_DATE('9999/12/31', 'YYYY/MM/DD')))
                              AND PROMO_NO = " + OracleDBUtil.SqlStr(PROMO)
                    );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable Query_ProdType_ByKey(string ProdTypeNo)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT PRODTYPENAME 
                            FROM PRODUCT_TYPE 
                            WHERE PRODTYPENO = " + OracleDBUtil.SqlStr(ProdTypeNo)
                      );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable Query_ProdType_Account_ByKey(string ProdTypeNo)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT ACCOUNT 
                            FROM PRODUCT_TYPE 
                            WHERE PRODTYPENO = " + OracleDBUtil.SqlStr(ProdTypeNo)
                      );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable Query_ProdType_ByName(string ProdTypeName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT PRODTYPENO 
                            FROM PRODUCT_TYPE 
                            WHERE PRODTYPENAME = " + OracleDBUtil.SqlStr(ProdTypeName)
                        );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable Query_Account_ByProdType( string ProdTypeNo)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT CCN, SEGMENT4 ACCOUNT
                            FROM CCN_ACCOUNT 
                            WHERE DESCRIPTION = " + OracleDBUtil.SqlStr(ProdTypeNo)
                      );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public string Query_FullAccount_ByProdType(string CCNO, string ProdTypeNo, string ACCOUNT)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT  SEGMENT1 || SEGMENT2 || SEGMENT3 || SEGMENT4 || SEGMENT5 || SEGMENT6 ACCOUNT
                            FROM CCN_ACCOUNT 
                            WHERE CCN = " + OracleDBUtil.SqlStr(CCNO) + @"
                              AND DESCRIPTION = " + OracleDBUtil.SqlStr(ProdTypeNo) + @"
                              AND SEGMENT4 = " + OracleDBUtil.SqlStr(ACCOUNT)
                      );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            string strFullAccount = "";
            foreach (DataRow dr in dt.Rows)
            {
                strFullAccount = dr["ACCOUNT"].ToString();
            }
            return strFullAccount;
        }

        public DataTable Query_CostCenterByKey(string sCostNo)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT  
                            COST_CENTER_NAME 
                            FROM   COST_CENTER 
                            WHERE COST_CENTER_NO = " + OracleDBUtil.SqlStr(sCostNo)
                          );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable Query_DiscountMasterByKey(string sDNO, string sDate)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT  
                            DISCOUNT_MASTER_ID 
                            FROM  DISCOUNT_MASTER 
                            WHERE DEL_FLAG = 'N' 
                              AND TO_CHAR(NVL(E_DATE,TO_DATE('9999/12/31','yyyy/MM/dd')),'yyyy/MM/dd') > " + OracleDBUtil.SqlStr(sDate) + @"  
                              AND DISCOUNT_CODE = " + OracleDBUtil.SqlStr(sDNO)
                          );
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable Query_MAX_DISCOUNT_CODE()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT TO_CHAR(NVL(TO_NUMBER(MAX(DISCOUNT_CODE)),0)+1,'00000000')
                            FROM DISCOUNT_MASTER
                            WHERE REGEXP_LIKE(DISCOUNT_CODE,'[[:digit:]]{8}') "
                         );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //折扣設定主檔
        public DataTable Query_DiscountMasterByKey(string MASTER_UUID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT   
                            DISCOUNT_MASTER_ID      --折扣設定主檔ID_UUID
                            ,DISCOUNT_CODE          --折扣料號
                            ,DISCOUNT_NAME          --折扣名稱
                            ,CREATE_USER            --建立人員
                            ,CREATE_DTM             --建立日期
                            ,MODI_USER              --異動人員
                            ,TO_CHAR(MODI_DTM, 'YYYY/MM/DD') MODI_DTM   --異動日期
                            ,DISCOUNT_MONEY         --折扣金額
                            ,DISCOUNT_RATE          --折扣比率
                            ,TO_CHAR(S_DATE, 'YYYY/MM/DD') S_DATE   --有效期間_起日
                            ,TO_CHAR(E_DATE, 'YYYY/MM/DD') E_DATE   --有效期間_訖日
                            ,ACCOUNT_CODE           --會計科目
                            ,DIS_USE_MONEY_UBOND    --折扣上限設定金額
                            ,DISCOUNT_TYPE          --折扣類別
                            ,DIS_USE_TYPE           --折扣上限次數使用方式
                            ,DECODE(STATUS, '00', '未存檔', '10', '已存檔', STATUS) STATUS  --狀態
                            , (CASE WHEN trunc(S_DATE) <= trunc(SYSDATE) AND (trunc(E_DATE) >= trunc(SYSDATE) OR E_DATE IS NULL) THEN '有效'
                                            WHEN trunc(E_DATE) < trunc(SYSDATE) THEN '已過期'
                                            ELSE '尚未生效' 
                                END) STATUS_DATE
                            FROM   DISCOUNT_MASTER 
                            WHERE  DISCOUNT_MASTER_ID = " + OracleDBUtil.SqlStr(MASTER_UUID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //取得所有明細資料
        public DataSet Query_AllDetailData_ByMasterUUID(string MASTER_UUID)
        {
            OracleConnection objConn = null;
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();
            try
            {
                objConn = OracleDBUtil.GetConnection();

                //費率及申辦類型1
                StringBuilder sb = new StringBuilder();
                dt = new DataTable("RatePlan");
                sb.AppendLine(@"SELECT  *
                               FROM   RATE_PLAN_DISCOUNT
                               WHERE  DISCOUNT_MASTER_ID = " + OracleDBUtil.SqlStr(MASTER_UUID));
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                ds.Tables.Add(dt);

                //指定商品2
                sb = new StringBuilder();
                dt = new DataTable("Product");
                sb.AppendLine(@" SELECT  M.PRODNO,P.PRODNAME
                                FROM   PRODUCT_DISCOUNT M, PRODUCT P 
                                WHERE M.PRODNO = P.PRODNO 
                                AND P.COMPANYCODE = '01'
                                AND  M.DISCOUNT_MASTER_ID = " + OracleDBUtil.SqlStr(MASTER_UUID));
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                ds.Tables.Add(dt);

                //指定門市3
                sb = new StringBuilder();
                dt = new DataTable("Store");
                sb.AppendLine(@" SELECT M.STORE_NO, S.STORENAME, Z.ZONE_NAME, M.DIS_USE_COUNT
                                FROM   STORE_DISCOUNT M, STORE S, ZONE Z 
                                WHERE  M.STORE_NO = S.STORE_NO AND S.ZONE = Z.ZONE
                                AND    DISCOUNT_MASTER_ID = " + OracleDBUtil.SqlStr(MASTER_UUID));
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                ds.Tables.Add(dt);

                //指定促銷4
                sb = new StringBuilder();
                dt = new DataTable("Promotion");
                sb.AppendLine(@" SELECT MM.PROMO_NO, MM.PROMO_NAME
                                FROM   PROMOTION_DISCOUNT M, MM 
                                WHERE  M.PROMOTION_CODE = MM.PROMO_NO
                                AND    DISCOUNT_MASTER_ID = " + OracleDBUtil.SqlStr(MASTER_UUID));
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                ds.Tables.Add(dt);

                //客戶對象5_客戶等級
                sb = new StringBuilder();
                dt = new DataTable("Customer1");
                sb.AppendLine(@" SELECT  CUST_LEVEL_ID, ARPB_S, ARPB_E, USE_TYPE
                                FROM   CUST_LEVE_DISCOUNT 
                                WHERE  USE_TYPE = '1' --客戶等級
                                AND  DISCOUNT_MASTER_ID = " + OracleDBUtil.SqlStr(MASTER_UUID));
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                ds.Tables.Add(dt);

                //客戶對象5_客戶名單
                sb = new StringBuilder();
                dt = new DataTable("Customer2");
                sb.AppendLine(@" SELECT  CUST_LEVEL_ID, ARPB_S, ARPB_E, USE_TYPE, MSISDN
                                FROM   CUST_LEVE_DISCOUNT 
                                WHERE  USE_TYPE = '2' --客戶名單
                                AND  DISCOUNT_MASTER_ID = " + OracleDBUtil.SqlStr(MASTER_UUID));
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                ds.Tables.Add(dt);

                //成本中心6
                sb = new StringBuilder();
                dt = new DataTable("CostCenter");
//                sb.AppendLine(@" SELECT M.COSTCENTER_DIS_ID, M.COST_CENTER_NO, P.PRODTYPENAME,  M.ACCOUNTCODE, M.AMT, M.REMARK
//                                       , P.PRODTYPENO PROD_CATEG, P.PRODTYPENAME PROD_CATEGNAME
//                                FROM   COST_CENTER_DISCOUNT M, PRODUCT_TYPE P
//                                WHERE  M.PROD_CATEG = P.PRODTYPENO 
//                                AND  DISCOUNT_MASTER_ID = " + OracleDBUtil.SqlStr(MASTER_UUID));

                //**2011/04/22 Tina：商品分類的來源從PRODUCT_TYPE改成CCN_ACCOUNT
                sb.AppendLine(@" SELECT COSTCENTER_DIS_ID
                                        , COST_CENTER_NO
                                        , ACCOUNTCODE
                                        , AMT
                                        , REMARK
                                        , PROD_CATEG 
                                        , PROD_CATEG PROD_CATEGNAME
                                FROM   COST_CENTER_DISCOUNT
                                WHERE  DISCOUNT_MASTER_ID = " + OracleDBUtil.SqlStr(MASTER_UUID));
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                ds.Tables.Add(dt);

                //贈品設定7
                sb = new StringBuilder();
                dt = new DataTable("SetProduct");
                sb.AppendLine(@" SELECT M.PRODNO, P.PRODNAME, M.AMT
                                FROM   GIFT_DISCOUNT M, PRODUCT P
                                WHERE  M.PRODNO = P.PRODNO AND P.COMPANYCODE='01' 
                                AND  DISCOUNT_MASTER_ID = " + OracleDBUtil.SqlStr(MASTER_UUID));
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                ds.Tables.Add(dt);

                //加價購8
                sb = new StringBuilder();
                dt = new DataTable("AddProduct");
                sb.AppendLine(@" SELECT M.PRODNO, P.PRODNAME, M.UNIT_PRICE, M.DIS_AMT
                                FROM   ADD_IN_PROD_DISCOUNT M, PRODUCT P 
                                WHERE  M.PRODNO = P.PRODNO AND P.COMPANYCODE='01' 
                                AND  DISCOUNT_MASTER_ID = " + OracleDBUtil.SqlStr(MASTER_UUID));
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                ds.Tables.Add(dt);

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

            return ds;
        }

        //取得所有匯入資料
        public DataSet Query_AllImportData_ByBatchNo(string BATCH_NO)
        {
            OracleConnection objConn = null;
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();
            try
            {
                objConn = OracleDBUtil.GetConnection();

                #region Heaer_一般0
                StringBuilder sb = new StringBuilder();
                dt = new DataTable("Header0");
                sb.AppendLine(@"SELECT  SID 
                                        , F1 NO                 --序號
                                        , F2 DISCOUNT_NAME      --折扣名稱
                                        , F3 DISCOUNT_MONEY     --折扣金額
                                        , F4 DISCOUNT_RATE      --商品折扣比率
                                        , F5 ACCOUNTCODE        --會計科目
                                        , F6 DIS_USE_COUNTTYPE  --折扣上限次數類型
                                        , F7 DIS_USE_COUNT      --折扣上限次數
                                        , F8 S_DATE             --有效日期(起)
                                        , F9 E_DATE             --有效日期(迄) 
                                        , F30 RESULT            --異常原因
                               FROM   UPLOAD_TEMP
                               WHERE  BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO) + @"
                                 AND FINC_ID = 'Header' 
                               ORDER BY F1");
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                ds.Tables.Add(dt);
                #endregion
                #region Heaer_舊機回收1
                sb = new StringBuilder();
                dt = new DataTable("Header1");
                sb.AppendLine(@"SELECT  SID 
                                        , F1 DISCOUNT_NO         --折扣料號
                                        , F2 DISCOUNT_NAME       --折扣名稱
                                        , F3 DISCOUNT_MONEY      --折扣金額
                                        , F4 DISCOUNT_RATE       --商品折扣比率
                                        , F5 DIS_USE_COUNTTYPE   --折扣上限次數類型
                                        , F6 DIS_USE_COUNT       --折扣上限次數
                                        , F7 S_DATE              --有效日期(起)
                                        , F8 E_DATE              --有效日期(迄)
                                        , F30 RESULT              --異常原因
                                FROM UPLOAD_TEMP 
                                WHERE BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO) + @"
                                  AND FINC_ID = 'Header'");
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                ds.Tables.Add(dt);
                #endregion
                #region 費率及申辦類型2
                sb = new StringBuilder();
                dt = new DataTable("RatePlan");
                sb.AppendLine(@"SELECT SID 
                                       , F1 NO          --序號
                                       , F2 RATES       --費率
                                       , F3 GA          --GA
                                       , F4 LOYALTY     --Loyalty
                                       , F5 TWOTOTHREE  --2轉3
                                       , F6 MNP         --MNP
                                       , F30 RESULT      --異常原因
                                FROM  UPLOAD_TEMP 
                                WHERE BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO) + @"
                                  AND FINC_ID = 'Tab_RatePlan'
                                ORDER BY F1");
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                ds.Tables.Add(dt);
                #endregion
                #region 指定商品3
                sb = new StringBuilder();
                dt = new DataTable("Product");
                sb.AppendLine(@"SELECT  U.SID 
                                        , U.F1 NO     --序號
                                        , U.F2 PRODNO --商品料號
                                        , P.PRODNAME  --商品名稱
                                        , F30 RESULT   --異常原因
                                FROM UPLOAD_TEMP U, PRODUCT P 
                                WHERE U.F2 = P.PRODNO(+)
                                  AND U.BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO) + @"
                                  AND U.FINC_ID = 'Tab_Product'
                                ORDER BY U.F1");
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                ds.Tables.Add(dt);
                #endregion
                #region 指定門市4
                sb = new StringBuilder();
                dt = new DataTable("Store");
                sb.AppendLine(@"SELECT  U.SID 
                                        , U.F1 NO               --序號
                                        , U.F2 STORE_NO         --門市編號
                                        , S.STORENAME           --門市名稱
                                        , ZONE.ZONE_NAME        --區域別
                                        , U.F3 DIS_USE_COUNT    --折扣上限次數
                                        , F30 RESULT             --異常原因
                                FROM  UPLOAD_TEMP U, STORE S, ZONE
                                WHERE U.F2 = S.STORE_NO(+)
                                  AND S.ZONE = ZONE.ZONE(+)
                                  AND U.BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO) + @"
                                  AND U.FINC_ID = 'Tab_Store'
                                ORDER BY F1");
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                ds.Tables.Add(dt);
                #endregion
                #region 指定促銷5
                sb = new StringBuilder();
                dt = new DataTable("Promotion");
                sb.AppendLine(@"SELECT  SID 
                                        , F1 NO        --序號
                                        , F2 PROMO_NO  --促銷代號
                                        , M.PROMO_NAME --促銷名稱
                                        , F30 RESULT    --異常原因
                                FROM   UPLOAD_TEMP U, MM M
                                WHERE U.F2 = M.PROMO_NO(+)
                                  AND U.BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO) + @"
                                  AND U.FINC_ID = 'Tab_Promotion'
                                ORDER BY U.F1");
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                ds.Tables.Add(dt);
                #endregion
                #region 客戶對象_客戶等級6
                sb = new StringBuilder();
                dt = new DataTable("Customer");
                sb.AppendLine(@"SELECT  SID 
                                        , F1 NO      --序號
                                        , F2 ARPB_S  --ARPB金額(起)
                                        , F3 ARPB_E  --ARPB金額(訖)
                                        , F30 RESULT  --異常原因
                                FROM  UPLOAD_TEMP 
                                WHERE BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO) + @"
                                  AND FINC_ID = 'Tab_Customer'
                                ORDER BY F1");
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                ds.Tables.Add(dt);
                #endregion
                #region 客戶對象_客戶名單7
                sb = new StringBuilder();
                dt = new DataTable("List");
                sb.AppendLine(@"SELECT  SID 
                                        , F1 NO      --序號
                                        , F2 MSISDN  --客戶門號
                                        , F30 RESULT  --異常原因
                                FROM  UPLOAD_TEMP 
                                WHERE BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO) + @"
                                  AND FINC_ID = 'Tab_List'
                                ORDER BY F1");
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                ds.Tables.Add(dt);
                #endregion
                #region 成本中心8
                sb = new StringBuilder();
                dt = new DataTable("CCD");//CostCenter
                sb.AppendLine(@"SELECT  SID 
                                        , F1 NO            --序號
                                        , F2 COSTCENTERNO  --成本中心編號
                                        , F3 PROD_CATEG    --商品分類
                                        , F4 ACCOUNTCODE   --會計科目
                                        , F5 AMT           --金額
                                        , F6 REMARK        --備註
                                        , F30 RESULT        --異常原因
                                FROM   UPLOAD_TEMP 
                                WHERE BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO) + @"
                                  AND FINC_ID = 'Tab_CCD'
                                ORDER BY F1");
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                ds.Tables.Add(dt);
                #endregion
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

            return ds;
        }

        #endregion

        public void DeletOne_DISCOUNT_MASTER_MethodSet(string MASTER_ID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.ExecuteSql(objTX,
                    @"update DISCOUNT_MASTER SET DEL_FLAG = 'Y'
                                         where DISCOUNT_MASTER_ID = " + OracleDBUtil.SqlStr(MASTER_ID) + "");
                OracleDBUtil.ExecuteSql(objTX,
                    @"update PRODUCT SET DEL_FLAG = 'Y'
                                         where PRODNO = (SELECT DISCOUNT_CODE FROM DISCOUNT_MASTER WHERE DISCOUNT_MASTER_ID = " + OracleDBUtil.SqlStr(MASTER_ID) + ")");

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

        public void InsertDISCOUNTALLDataSet(DataSet ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    OracleDBUtil.Insert(objTX, ds.Tables[i]);
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

        public void UpdateDISCOUNTALLDataSet(DataSet ds, string DISCOUNT_MASTER_ID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                foreach (DataTable dt in ds.Tables)
                {
                    //只有折扣主檔(DISCOUNT_MASTER)和商品主檔(PRODUCT)是採用Update，其餘則採用Delete + Insert
                    if (dt.TableName.ToUpper() == "DISCOUNT_MASTER" || dt.TableName.ToUpper() == "PRODUCT")
                    {
                        if (dt.TableName.ToUpper() == "PRODUCT")
                        {
                            OracleDBUtil.UPDDATEByUUID_IncludeDBNull(objTX, dt, "PRODNO");
                        }
                        else
                        {
                            OracleDBUtil.UPDDATEByUUID_IncludeDBNull(objTX, dt, "DISCOUNT_MASTER_ID");
                        }
                    }
                    else
                    {
                        string strsqlMaster = "DELETE FROM " + dt.TableName.ToUpper() + " WHERE DISCOUNT_MASTER_ID = " + OracleDBUtil.SqlStr(DISCOUNT_MASTER_ID);
                        OracleDBUtil.ExecuteSql(objTX, strsqlMaster);

                        OracleDBUtil.Insert(objTX, dt);
                    }
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
        /// 新增或修改折扣料號設定資料，須發送Mail通知建單人員、異動人員，以及Accounting人員。
        /// </summary>
        /// <param name="DISCOUNT_MASTER_ID">折扣設定主檔_UUID</param>
        public void SEND_MAIL(string DISCOUNT_MASTER_ID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleCommand oraCmd = new OracleCommand("SP_DIS01_ALERT_MAIL");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("inDISCOUNT_MASTER_ID", OracleType.VarChar, 32)).Value = DISCOUNT_MASTER_ID;
                oraCmd.Parameters.Add(new OracleParameter("outSTATUS", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("outMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Connection = objConn;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                string sCode = oraCmd.Parameters["outSTATUS"].Value.ToString();
                string sMsg = oraCmd.Parameters["outMESSAGE"].Value.ToString();
                if (sCode.CompareTo("000") != 0)
                {
                    new Exception(sCode + ":" + sMsg);
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

        public void ImportHeadQuarter(DIS01_DiscountMasterDataSet_DTO ds, string strType, string DisType)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                string strSql = null;
                strSql = "delete UPLOAD_TEMP where nvl(STATUS,'T')<>'C' " +
                                              " and FINC_ID  = '" + ds.Tables["UPLOAD_TEMP"].Rows[0]["FINC_ID"] + "'" +
                                              " and USER_ID  = '" + ds.Tables["UPLOAD_TEMP"].Rows[0]["USER_ID"] + "'";
                OracleDBUtil.ExecuteSql(objTX, strSql);

                OracleDBUtil.Insert(ds.Tables["UPLOAD_TEMP"]);

                string doPackageName = "PK_Upload_Check.";
                //ora_com
                switch (strType)
                {
                    case "PRODUCT":
                        doPackageName += "DIS01_PROD_CHECK";
                        break;
                    case "STORE":
                        doPackageName += "DIS01_STORE_CHECK";
                        break;
                    case "PROMO":
                        doPackageName += "DIS01_PROMO_CHECK";
                        break;
                    case "CUSTMOBIL":
                        doPackageName += "DIS01_CUSTMOBIL_CHECK";
                        break;
                    case "CCenter":
                        doPackageName += "DIS01_COSTCR_CHECK";
                        break;
                    case "GIFT":
                        doPackageName += "DIS01_GIFT_CHECK";
                        break;
                }

                if (strType == "CCenter")
                {
                    OracleDBUtil.ExecuteSql_SP(
                        objTX
                        , doPackageName
                        , new OracleParameter("p_BATCH_NO", ds.Tables["UPLOAD_TEMP"].Rows[0]["BATCH_NO"].ToString())
                        , new OracleParameter("p_USER_ID", ds.Tables["UPLOAD_TEMP"].Rows[0]["USER_ID"].ToString())
                        , new OracleParameter("p_FINC_ID", ds.Tables["UPLOAD_TEMP"].Rows[0]["FINC_ID"].ToString())
                        , new OracleParameter("p_DiscountType", DisType)
                        );
                }
                else
                {
                    OracleDBUtil.ExecuteSql_SP(
                        objTX
                        , doPackageName
                        , new OracleParameter("p_BATCH_NO", ds.Tables["UPLOAD_TEMP"].Rows[0]["BATCH_NO"].ToString())
                        , new OracleParameter("p_USER_ID", ds.Tables["UPLOAD_TEMP"].Rows[0]["USER_ID"].ToString())
                        , new OracleParameter("p_FINC_ID", ds.Tables["UPLOAD_TEMP"].Rows[0]["FINC_ID"].ToString())
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
        /// 大量匯入_Excel匯入
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="DisType">類別 1:一般  2:舊機回收</param>
        /// <param name="BATCH_NO">批號</param>
        /// <param name="USER_ID">異動人員代號</param>
        public void Import(DIS01_DiscountMasterDataSet_DTO.UPLOAD_TEMPDataTable dt, string DisType, string BATCH_NO, string USER_ID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                string strSql = "DELETE UPLOAD_TEMP WHERE USER_ID = " + OracleDBUtil.SqlStr(USER_ID);
                OracleDBUtil.ExecuteSql(objTX, strSql);

                OracleDBUtil.Insert(dt);

                string doPackageName = "PK_Upload_Check.";
                //類別
                switch (DisType)
                {
                    case "1":  //一般
                        doPackageName += "DIS01_GENERAL_CHECK";
                        break;
                    case "2":  //舊機回嬖
                        doPackageName += "DIS01_OLDPHONE_CHECK";
                        break;
                    default:
                        break;
                }

                OracleDBUtil.ExecuteSql_SP(
                     objTX
                     , doPackageName
                     , new OracleParameter("p_BATCH_NO", BATCH_NO)
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
        /// 大量匯入_確定匯入
        /// </summary>
        /// <param name="DisType">類別 1:一般  2:舊機回收</param>
        /// <param name="BATCH_NO">批號</param>
        /// <param name="OUT_CODE">回傳碼</param>
        /// <param name="OUT_MESSAGE">回傳訊息</param>
        public void Commit(string DisType, string BATCH_NO, ref string OUT_CODE, ref string OUT_MESSAGE)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            OracleCommand oraCmd = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                string doPackageName = "PK_Upload_Check.";
                //類別
                switch (DisType)
                {
                    case "1":  //一般
                        doPackageName += "DIS01_GENERAL_IMPORT";
                        break;
                    case "2":  //舊機回嬖
                        doPackageName += "DIS01_OLDPHONE_IMPORT";
                        break;
                    default:
                        break;
                }

                //OracleDBUtil.ExecuteSql_SP(
                //     objTX
                //     , doPackageName
                //     , new OracleParameter("p_BATCH_NO", BATCH_NO)
                //     , new OracleParameter("outMSGCODE", OUT_CODE)
                //     , new OracleParameter("outMESSAGE", OUT_MESSAGE)
                //     );

                oraCmd = new OracleCommand(doPackageName);
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("p_BATCH_NO", OracleType.VarChar, 2000)).Value = BATCH_NO;                      //批號
                oraCmd.Parameters.Add(new OracleParameter("outMSGCODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output; //回傳碼
                oraCmd.Parameters.Add(new OracleParameter("outMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output; //回傳訊息

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                OUT_CODE = oraCmd.Parameters["outMSGCODE"].Value.ToString();
                OUT_MESSAGE = oraCmd.Parameters["outMESSAGE"].Value.ToString(); 

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

        public DataTable GetImportTempData(string BATCH_NO, string FINC_ID, string USER_ID, string TYPE)
        {
            StringBuilder sb = null;

            switch (TYPE)
            {
                case "PRODUCT":
                    sb = new StringBuilder();
                    sb.AppendLine(@"SELECT F2 AS PRODNO 
                                        ,F3 AS PRODNAME
                                        ,F4 AS RESULT
                                  FROM UPLOAD_TEMP 
                                  WHERE 1=1
                                  AND BATCH_NO ='" + BATCH_NO + @"' 
                                  AND USER_ID  ='" + USER_ID + @"' 
                                  AND FINC_ID  ='" + FINC_ID + @"' 
                                  AND F1       ='" + TYPE + @"'     
                                ");
                    break;
                case "STORE":
                    sb = new StringBuilder();
                    sb.AppendLine(@"SELECT F2 AS STORENO 
                                        ,F3 AS STORENAME 
                                        ,F4 AS ZONENAME
                                        ,F5 AS RESULT
                                   FROM UPLOAD_TEMP
                                   WHERE 1=1 
                                   AND BATCH_NO ='" + BATCH_NO + @"'
                                   AND USER_ID  ='" + USER_ID + @"' 
                                   AND FINC_ID  ='" + FINC_ID + @"' 
                                   AND F1       ='" + TYPE + @"'     
                                ");
                    break;
                case "PROMO":
                    sb = new StringBuilder();
                    sb.AppendLine(@"SELECT F2 AS PROMO
                                        ,F3 AS PROMONAME
                                        ,F4 AS RESULT
                                   FROM UPLOAD_TEMP 
                                   WHERE 1=1 
                                   AND BATCH_NO ='" + BATCH_NO + @"' 
                                   AND USER_ID  ='" + USER_ID + @"'  
                                   AND FINC_ID  ='" + FINC_ID + @"' 
                                   AND F1       ='" + TYPE + @"'     
                                ");
                    break;
                case "CUSTMOBIL":
                    sb = new StringBuilder();
                    sb.AppendLine(@"SELECT F2 AS CUSTMO 
                                        ,F3 AS RESULT 
                                   FROM UPLOAD_TEMP 
                                   WHERE 1=1 
                                   AND BATCH_NO ='" + BATCH_NO + @"' 
                                   AND USER_ID  ='" + USER_ID + @"' 
                                   AND FINC_ID  ='" + FINC_ID + @"'
                                   AND F1       ='" + TYPE + @"'     
                                ");
                    break;
                case "CCenter":
                    sb = new StringBuilder();
                    sb.AppendLine(@"SELECT F2 AS COSTNO 
                                        ,F3 AS PRODNAME 
                                        ,F4 AS ACCode
                                        ,F5 AS COSTAMT
                                        ,F6 AS REMARK 
                                        ,F7 AS RESULT
                                   FROM UPLOAD_TEMP 
                                   WHERE 1=1 
                                   AND BATCH_NO ='" + BATCH_NO + @"' 
                                   AND USER_ID  ='" + USER_ID + @"' 
                                   AND FINC_ID  ='" + FINC_ID + @"'  
                                   AND F1       ='" + TYPE + @"'     
                                ");
                    break;
                case "GIFT":
                    sb = new StringBuilder();
                    sb.AppendLine(@"SELECT F2 AS PRODNO 
                                        ,F3 AS PRODNAME 
                                        ,F4 AS AMT
                                        ,F5 AS RESULT
                                  FROM UPLOAD_TEMP 
                                  WHERE 1=1
                                  AND BATCH_NO ='" + BATCH_NO + @"'
                                  AND USER_ID  ='" + USER_ID + @"' 
                                  AND FINC_ID  ='" + FINC_ID + @"' 
                                  AND F1       ='" + TYPE + @"'   
                                ");
                    break;

            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public void UpdateOne_UpLoadTempMethodSet(string BATCH_NO, string FINC_ID, string USER_ID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.ExecuteSql(objTX,
                    @"update UPLOAD_TEMP SET STATUS = 'C'
                                         where BATCH_NO = '" + BATCH_NO + @"'
                                          and FINC_ID  = '" + FINC_ID + @"'
                                          and USER_ID  = '" + USER_ID + @"'");


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

        public DataTable Get_UploadTemp(string BATCH_NO, string strType)
        {
            StringBuilder sb = null;

            switch (strType)
            {
                case "PRODUCT":
                    sb = new StringBuilder();
                    sb.AppendLine(@" SELECT U.BATCH_NO 
                                        , U.FINC_ID 
                                        , U.USER_ID    
                                        , U.F2 AS PRODNO 
                                        , U.F3 AS PRODNAME 
                                        , NVL(P.PRICE, 0) PRICE
                                    FROM UPLOAD_TEMP U, PRODUCT P
                                    WHERE U.F2 = P.PRODNO(+)
                                    AND U.STATUS = 'C'            
                                ");

                    if (BATCH_NO.Trim() != string.Empty)
                    {
                        sb.AppendLine(" AND BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO.Trim()));
                    }

                    break;
                case "STORE":
                    sb = new StringBuilder();
                    sb.AppendLine(@" SELECT BATCH_NO 
                                        , FINC_ID 
                                        , USER_ID
                                        , F2 AS STORENO 
                                        , F3 AS STORENAME 
                                        , F4 AS ZONENAME 
                                    FROM UPLOAD_TEMP 
                                    WHERE 1 = 1             
                                    AND STATUS = 'C'             
                                ");

                    if (BATCH_NO.Trim() != string.Empty)
                    {
                        sb.AppendLine(" AND BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO.Trim()));
                    }

                    break;
                case "PROMO":
                    sb = new StringBuilder();
                    sb.AppendLine(@" SELECT BATCH_NO 
                                        , FINC_ID 
                                        , USER_ID
                                        , F2 AS PROMO
                                        , F3 AS PROMONAME 
                                    FROM UPLOAD_TEMP
                                    WHERE 1=1 
                                    AND STATUS = 'C'             
                                ");

                    if (BATCH_NO.Trim() != string.Empty)
                    {
                        sb.AppendLine(" AND BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO.Trim()));
                    }

                    break;
                case "CUSTMOBIL":
                    sb = new StringBuilder();
                    sb.AppendLine(@" SELECT BATCH_NO 
                                        , FINC_ID 
                                        , USER_ID
                                        , F2 AS CUSTMO 
                                    FROM UPLOAD_TEMP 
                                    WHERE 1=1 
                                    AND STATUS = 'C'        
                                ");

                    if (BATCH_NO.Trim() != string.Empty)
                    {
                        sb.AppendLine(" AND BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO.Trim()));
                    }
                    break;
                case "CCenter":
                    sb = new StringBuilder();
                    sb.AppendLine(@" SELECT BATCH_NO 
                                        , FINC_ID 
                                        , USER_ID
                                        , F2 AS COSTNO 
                                        , F3 AS PRODNAME
                                        , F4 AS ACCode
                                        , F5 AS COSTAMT
                                        , F6 AS REMARK 
                                    FROM UPLOAD_TEMP 
                                    WHERE 1=1 
                                    AND STATUS = 'C'             
                                ");

                    if (BATCH_NO.Trim() != string.Empty)
                    {
                        sb.AppendLine(" AND BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO.Trim()));
                    }
                    break;
                case "GIFT":
                    sb = new StringBuilder();
                    sb.AppendLine(@" SELECT U.BATCH_NO 
                                        , U.FINC_ID 
                                        , U.USER_ID    
                                        , U.F2 AS PRODNO 
                                        , U.F3 AS PRODNAME 
                                        , NVL(U.F4, 0) AMT
                                    FROM UPLOAD_TEMP U
                                    WHERE U.STATUS = 'C'            
                                ");

                    if (BATCH_NO.Trim() != string.Empty)
                    {
                        sb.AppendLine(" AND BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO.Trim()));
                    }
                    break;
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得【Happy Go】或【舊機回收】固定的會計科目
        /// </summary>
        /// <param name="PARA_KEY"></param>
        /// <returns></returns>
        public DataTable GetSYS_PARA(string PARA_KEY)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT *  
                            FROM SYS_PARA 
                            WHERE 1 = 1    
                              AND PARA_KEY = " + OracleDBUtil.SqlStr(PARA_KEY.Trim())
                         );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 回傳匯入資料中有異常的資料筆數
        /// </summary>
        /// <param name="BATCH_NO">批號</param>
        /// <returns></returns>
        public int GetErrorCount(string BATCH_NO)
        {
            StringBuilder sb = null;
            sb = new StringBuilder();
            sb.AppendLine(@" SELECT SID
                             FROM UPLOAD_TEMP U
                             WHERE F30 IS NOT NULL 
                               AND BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt.Rows.Count;
        }
    }
}
