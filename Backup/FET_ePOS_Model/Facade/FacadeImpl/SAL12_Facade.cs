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
    public class SAL12_Facade
    {
        public DataTable Query_PRODUCT(
              string sISCONSIGNMENT
            , string sISEXPIRED
            , string sPRODTYPENO
            , string sPRODCATEGORY
            , string sPRODNO
            , string sPRODNAME
            , string sSUPPNAME
            , string sS_DATE1
            , string sS_DATE2
            , string sE_DATE1
            , string sE_DATE2
            , string sPRICE1
            , string sPRICE2
            )
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
                    SELECT * FROM (
                    SELECT
                        T1.PRODNO,
                        T1.PRODNAME,
                        T1.UNIT,
                        T1.PRODTYPENO,
                        T3.PRODTYPENAME,
                        T1.IMEI_FLAG,
                        (
                            CASE T1.IS_POS_DEF_PRICE
                                WHEN 'Y'
                                THEN 'True'
                                ELSE 'False'
                            END) AS IS_POS_DEF_PRICE,
                        (
                            CASE T1.ISSTOCK
                                WHEN 'Y'
                                THEN 'True'
                                ELSE 'False'
                            END)                          AS ISSTOCK,
                        TO_CHAR (T1.S_DATE, 'yyyy/mm/dd') AS S_DATE,
                        TO_CHAR (T1.E_DATE, 'yyyy/mm/dd') AS E_DATE,
                        T1.PRICE,
                        T1.SUPP_ID,
                        T2.SUPPNAME,
                        T1.ISCONSIGNMENT,
                        T1.IS_DISCOUNT,
                        T1.DEL_FLAG,
                        T4.CATEGORY
                    FROM
                        PRODUCT T1,
                        SUPPLIER T2 ,
                        PRODUCT_TYPE T3,
                        PROD_MAPPING T4
                    WHERE
                        T1.COMPANYCODE = '01'
                    AND NVL(IS_DISCOUNT,'N') ='N'
                    AND T1.SUPP_ID = T2.SUPPNO(+)
                    AND T1.PRODTYPENO = T3.PRODTYPENO
                    AND T1.ERP_ATTRIBUTE_1=T4.ERP_ATTRIBUTE1
                    AND T1.DEL_FLAG='N') T WHERE 1=1
                ");

            if (!string.IsNullOrEmpty(sISCONSIGNMENT))
            {
                if (sISCONSIGNMENT == "True")
                {
                    sb.Append(" AND ISCONSIGNMENT = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr("1"));
                }
                else
                {
                    sb.Append(" AND (ISCONSIGNMENT = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr("0"));
                    sb.Append(" OR ");
                    sb.Append(" ISCONSIGNMENT IS NULL)");
                }

            }
            if (!string.IsNullOrEmpty(sISEXPIRED) && sISEXPIRED == "False")
            {
                sb.Append(" AND E_DATE >= TO_CHAR (SYSDATE, 'yyyy/mm/dd')");
            }
            if (!string.IsNullOrEmpty(sPRODTYPENO))
            {
                sb.Append(" AND  PRODTYPENO= " + OracleDBUtil.SqlStr(sPRODTYPENO));
            }
            if (!string.IsNullOrEmpty(sPRODCATEGORY))
            {
                //sPRODCATEGORY
                sb.Append(" AND  CATEGORY= " + OracleDBUtil.SqlStr(sPRODCATEGORY));
            }
            if (!string.IsNullOrEmpty(sPRODNO))
            {
                sb.Append(" AND PRODNO = " + OracleDBUtil.SqlStr(sPRODNO));
            }
            if (!string.IsNullOrEmpty(sPRODNAME))
            {
                sb.Append(" AND LOWER(PRODNAME) LIKE " + OracleDBUtil.ToLowerLikeStr(sPRODNAME.Trim()));
            }
            if (!(string.IsNullOrEmpty(sSUPPNAME)))
            {
                sb.Append(" AND LOWER(SUPPNAME) LIKE " + OracleDBUtil.ToLowerLikeStr(sSUPPNAME.Trim()));
            }
            if (!string.IsNullOrEmpty(sS_DATE1))
            {
                sb.Append(" AND S_DATE >= " + OracleDBUtil.SqlStr(sS_DATE1));
            }
            if (!string.IsNullOrEmpty(sS_DATE2))
            {
                sb.Append(" AND S_DATE <= " + OracleDBUtil.SqlStr(sS_DATE2));
            }
            if (!string.IsNullOrEmpty(sE_DATE1))
            {
                sb.Append(" AND E_DATE >= " + OracleDBUtil.SqlStr(sE_DATE1));
            }
            if (!string.IsNullOrEmpty(sE_DATE2))
            {
                sb.Append(" AND E_DATE <= " + OracleDBUtil.SqlStr(sE_DATE2));
            }
            if (!string.IsNullOrEmpty(sPRICE1))
            {
                sb.Append(" AND PRICE >= " + OracleDBUtil.SqlStr(sPRICE1));
            }
            if (!string.IsNullOrEmpty(sPRICE2))
            {
                sb.Append(" AND PRICE <= " + OracleDBUtil.SqlStr(sPRICE2));
            }

            sb.Append(" ORDER BY PRODNO ASC,S_DATE ASC");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable Query_STORETRANSFER_D(string strSTNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT  ");
            sb.Append("       STORETRANSFER_D.SEQNO SEQNO ");
            sb.Append("      ,STORETRANSFER_D.STNO STNO ");
            sb.Append("      ,STORETRANSFER_D.prodno prodno ");
            sb.Append("      ,(select PRODUCT.PRODNAME from product where product.prodno=STORETRANSFER_D.prodno) prodName ");
            sb.Append("      ,decode((select IMEI_FLAG from PRODUCT where PRODUCT.prodno= STORETRANSFER_D.prodno ),'0','False','True') chkIMEI ");
            sb.Append("      ,TRANOUTQTY outQty ");
            sb.Append("      ,(select IMEI from STORETRANSFER_IMEI where STORETRANSFER_IMEI.STNO= STORETRANSFER_D.stno and STORETRANSFER_D.SEQNO=STORETRANSFER_IMEI.SEQNO) inIMEI ");
            sb.Append("      ,TRANINQTY inQty ");
            sb.Append("      ,(select IMEI from STORETRANSFER_IMEI where STORETRANSFER_IMEI.STNO= STORETRANSFER_D.stno and STORETRANSFER_D.SEQNO=STORETRANSFER_IMEI.SEQNO) outIMEI ");
            sb.Append("FROM   STORETRANSFER_D ");
            sb.Append("WHERE 1 = 1 ");
            if (!string.IsNullOrEmpty(strSTNO))
            {
                sb.Append(" AND STORETRANSFER_D.STNO = " + OracleDBUtil.SqlStr(strSTNO));
            }
            else
            {
                sb.Append(" AND 1<>1 ");
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable ExportSTORETRANSFER_M_D(string strSTNOList)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT  ");
            sb.Append("       T1.STNO as 移撥單號 ");
            sb.Append("      ,(case T1.TSTATUS when '20' then '在途' else '已撥入' end) as 移撥狀態 ");
            sb.Append("      ,T1.FROM_STORE_NO as 移出門市 ");
            sb.Append("      ,T1.STDATE as 移出日期 ");
            sb.Append("      ,T1.TO_STORE_NO as 移入門市 ");
            sb.Append("      ,T1.TSTDATE as 移入日期 ");
            sb.Append("      ,T1.MODI_USER as 更新人員 ");
            sb.Append("      ,T1.MODI_DTM as 更新日期 ");
            sb.Append("      ,T2.PRODNO as  商品料號 ");
            sb.Append("      ,T3.PRODNAME 商品名稱 ");
            sb.Append("      ,T2.TRANOUTQTY 移出數量 ");
            sb.Append("      ,T4.IMEI 移出IMEI ");
            sb.Append("      ,T2.TRANINQTY 移入數量 ");
            sb.Append("      ,T4.IMEI 移入IMEI ");
            sb.Append("FROM STORETRANSFER_M T1 ");
            sb.Append("     LEFT OUTER JOIN ");
            sb.Append("     STORETRANSFER_D T2 on T1.STNO=T2.STNO ");
            sb.Append("     LEFT OUTER JOIN ");
            sb.Append("     PRODUCT T3 on T3.PRODNO=T2.PRODNO ");
            sb.Append("     LEFT OUTER JOIN ");
            sb.Append("     STORETRANSFER_IMEI T4 on T4.STNO=T2.STNO and T4.SEQNO=T2.SEQNO ");
            sb.Append("WHERE 1=1 ");

            if (!string.IsNullOrEmpty(strSTNOList))
            {
                sb.Append(" AND T1.STNO = " + OracleDBUtil.SqlStr(strSTNOList));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }


        public DataTable getDiscount(
            string v_Data,
            string v_Voice,
            string v_RatePlan,
            string v_ProdNo,
            string v_PromoteCode,
            string v_StoreNo,
            int v_RRPB,
            string v_Msisdn
        )
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            OracleCommand oraCmd = null;
            //string sRet = "";
            DataTable O_DATA = new DataTable();
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //oraCmd = new OracleCommand("PKG_DISCOUNT.DiscountQuery");
                oraCmd = new OracleCommand("SP_SAL12_QUERY_DISCOUNT");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("v_Data", v_Data));
                oraCmd.Parameters.Add(new OracleParameter("v_Voice", v_Voice));
                oraCmd.Parameters.Add(new OracleParameter("v_RatePlan", v_RatePlan));
                oraCmd.Parameters.Add(new OracleParameter("v_ProdNo", v_ProdNo));
                oraCmd.Parameters.Add(new OracleParameter("v_PromoteCode", v_PromoteCode));
                oraCmd.Parameters.Add(new OracleParameter("v_StoreNo", v_StoreNo));
                oraCmd.Parameters.Add(new OracleParameter("v_RRPB", v_RRPB));
                oraCmd.Parameters.Add(new OracleParameter("v_Msisdn", v_Msisdn));
                oraCmd.Parameters.Add(new OracleParameter("O_CUR", OracleType.Cursor)).Direction = ParameterDirection.Output;
                //oraCmd.Parameters.Add(new OracleParameter("v_MsgCode", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                //oraCmd.Parameters.Add(new OracleParameter("v_Message", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();
                //columns: DISCOUNT_CODE，DISCOUNT_NAME，DISCOUNT_MONEY，DISCOUNT_GIFT_ADD
                OracleDataAdapter da = new OracleDataAdapter(oraCmd);
                da.Fill(O_DATA);
                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
                oraCmd.Dispose();
                objTX.Dispose();
                //objConn.Dispose();
            }
            return O_DATA;
        }

        public DataTable Query_ProdTypeNo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT  ");
            sb.Append("       PRODTYPENO, PRODTYPENAME ");
            sb.Append("FROM   VW_SAL12_PRODUCT_TYPE ");
            sb.Append("ORDER BY PRODTYPENO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }
    }
}
