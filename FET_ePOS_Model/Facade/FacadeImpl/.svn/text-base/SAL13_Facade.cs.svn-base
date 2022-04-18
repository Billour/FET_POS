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
    public class SAL13_Facade
   {
        public DataTable Query_SP_DisCountQuery(
              string sSA_TYPE
            , string sM_TYPE
            , string sSTORE_NO
            , string sMSISDN
            , string sMM
            , string sMM_NAME
            , string sPROD_NO
            , string sPROD_NAME
            , string sARPB
            )
        {
            bool bIsAllEmpty = false; // 是否全部傳入的數為空值
            if (string.IsNullOrEmpty(sSA_TYPE) && string.IsNullOrEmpty(sM_TYPE) && string.IsNullOrEmpty(sSTORE_NO) && string.IsNullOrEmpty(sMSISDN) && string.IsNullOrEmpty(sMM)
                && string.IsNullOrEmpty(sMM_NAME) && string.IsNullOrEmpty(sPROD_NO) && string.IsNullOrEmpty(sPROD_NAME) && string.IsNullOrEmpty(sARPB))
            {
                bIsAllEmpty = true;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(@"
                    SELECT DISTINCT
                      DM.DISCOUNT_MASTER_ID,             
                      DM.DISCOUNT_CODE,                         
                      DM.DISCOUNT_NAME,                           
                      DM.DISCOUNT_MONEY,                    
                      DM.DISCOUNT_TYPE,
                      DM.DISCOUNT_RATE,                   
                      DM.S_DATE,                
                      DM.E_DATE,                
                      SD.DIS_USE_COUNT          
                 FROM DISCOUNT_MASTER DM,
                      STORE_DISCOUNT SD
                WHERE    
                       DM.DISCOUNT_MASTER_ID = SD.DISCOUNT_MASTER_ID(+)
                      
              ");
            if (!bIsAllEmpty)
            {
                sb.Append(@" AND DM.DISCOUNT_MASTER_ID  IN ( ");

                if (!string.IsNullOrEmpty(sSTORE_NO))
                {//1 STORE_NO
                    sb.Append(@" 
                                    SELECT SD.DISCOUNT_MASTER_ID 
                                      FROM STORE_DISCOUNT  SD ,
                                           (SELECT * FROM  DISCOUNT_MASTER 
                                             WHERE S_DATE <= TRUNC(SYSDATE)  
                                               AND NVL(E_DATE , TO_DATE('99991231','YYYYMMDD')) >= TRUNC(SYSDATE) ) M 
                                     WHERE  SD.DISCOUNT_MASTER_ID = M.DISCOUNT_MASTER_ID  
                                          
                            ");
                    sb.Append(@" AND SD.STORE_NO =" + OracleDBUtil.SqlStr(sSTORE_NO));
                }

                if (!string.IsNullOrEmpty(sSA_TYPE) || !string.IsNullOrEmpty(sM_TYPE))
                {//2 sSA_TYPE sM_TYPE
                    sb.Append(@" 
                                    INTERSECT
                                SELECT  DISTINCT RPD.DISCOUNT_MASTER_ID  
                                  FROM RATE_PLAN_DISCOUNT RPD , 
                                      (SELECT * FROM  DISCOUNT_MASTER 
                                        WHERE S_DATE <= TRUNC(SYSDATE)  
                                          AND NVL(E_DATE , TO_DATE('99991231','YYYYMMDD')) >= TRUNC(SYSDATE) ) M 
                                 WHERE  RPD.DISCOUNT_MASTER_ID = M.DISCOUNT_MASTER_ID
                                    AND  UPPER(RPD.VALUE) = 'Y'
                                                                                
                            ");

                    if (!string.IsNullOrEmpty(sSA_TYPE))
                    {
                        string sTmp = "('" + sSA_TYPE.Replace(",", "','") + "')";
                        sb.Append(@" AND  RPD.SA_TYPE IN " + sTmp);
                    }
                    if (!string.IsNullOrEmpty(sM_TYPE))
                    {
                        string sTmpM_TYPE = "('" + sM_TYPE.Replace(",", "','") + "')";
                        sb.Append(@" AND  RPD.M_TYPE IN " + sTmpM_TYPE);
                    }
                }

                if (!string.IsNullOrEmpty(sMSISDN))
                {//3.sMSISDN
                    sb.Append(@" 
                                   INTERSECT  
                                    SELECT DISTINCT CLD2.DISCOUNT_MASTER_ID 
                                      FROM CUST_LEVE_DISCOUNT CLD2  ,
                                           (SELECT * FROM  DISCOUNT_MASTER 
                                             WHERE S_DATE <= TRUNC(SYSDATE)  
                                               AND NVL(E_DATE , TO_DATE('99991231','YYYYMMDD')) >= TRUNC(SYSDATE) ) M 
                                     WHERE CLD2.USE_TYPE='2' 
                                       AND CLD2.DISCOUNT_MASTER_ID = M.DISCOUNT_MASTER_ID   
                            ");
                    sb.Append(@" AND CLD2.MSISDN = " + OracleDBUtil.SqlStr(sMSISDN));
                }

                if (!string.IsNullOrEmpty(sMM) || !string.IsNullOrEmpty(sMM_NAME))
                {//4.sMM
                    sb.Append(@" 
                                   INTERSECT  
                                    SELECT DISTINCT MMD.DISCOUNT_MASTER_ID 
                                      FROM PROMOTION_DISCOUNT MMD , MM PMM,
                                           (SELECT * FROM  DISCOUNT_MASTER 
                                             WHERE S_DATE <= TRUNC(SYSDATE)  
                                               AND NVL(E_DATE , TO_DATE('99991231','YYYYMMDD')) >= TRUNC(SYSDATE) ) M 
                                     WHERE MMD.PROMOTION_CODE = PMM.PROMO_NO
                                       AND MMD.DISCOUNT_MASTER_ID = M.DISCOUNT_MASTER_ID
                            ");
                    if (!string.IsNullOrEmpty(sMM))
                    {
                        sb.Append(@" AND MMD.PROMOTION_CODE =" + OracleDBUtil.SqlStr(sMM));
                    }
                    if (!string.IsNullOrEmpty(sMM_NAME))
                    {
                        sb.Append(" AND  PMM.PROMO_NAME like " + OracleDBUtil.LikeStr(sMM_NAME));
                    }
                }

                if (!string.IsNullOrEmpty(sPROD_NO) || !string.IsNullOrEmpty(sPROD_NAME))
                {//5.sPROD_NO
                    sb.Append(@" 
                                   INTERSECT  
                                    SELECT DISTINCT PD.DISCOUNT_MASTER_ID 
                                      FROM PRODUCT_DISCOUNT PD ,PRODUCT PROD ,
                                           (SELECT * FROM  DISCOUNT_MASTER 
                                             WHERE S_DATE <= TRUNC(SYSDATE)  
                                               AND NVL(E_DATE , TO_DATE('99991231','YYYYMMDD')) >= TRUNC(SYSDATE) ) M 
                                     WHERE PD.PRODNO = PROD.PRODNO
                                       AND PD.DISCOUNT_MASTER_ID = M.DISCOUNT_MASTER_ID 
                                       AND PROD.COMPANYCODE='01' 
                            ");
                    if (!string.IsNullOrEmpty(sPROD_NO))
                    {
                        sb.Append(@" AND PD.PRODNO= " + Advtek.Utility.OracleDBUtil.SqlStr(sPROD_NO));
                    }
                    if (!string.IsNullOrEmpty(sPROD_NAME))
                    {
                        sb.Append(" AND PROD.PRODNAME like ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr("%" + sPROD_NAME + "%"));
                    }
                }
                if (!string.IsNullOrEmpty(sARPB))
                {//6.sPROD_NO
                    sb.Append(@" 
                                  INTERSECT 
                                    SELECT DISTINCT CLD.DISCOUNT_MASTER_ID 
                                      FROM CUST_LEVE_DISCOUNT CLD  ,
                                           (SELECT * FROM  DISCOUNT_MASTER 
                                             WHERE S_DATE <= TRUNC(SYSDATE)  
                                               AND NVL(E_DATE , TO_DATE('99991231','YYYYMMDD')) >= TRUNC(SYSDATE) ) M 
                                     WHERE CLD.USE_TYPE='1' 
                                       AND CLD.DISCOUNT_MASTER_ID = M.DISCOUNT_MASTER_ID 
                            ");
                    if (!string.IsNullOrEmpty(sARPB))
                    {
                        sb.Append(" AND CLD.ARPB_S <= ");
                        sb.Append(sARPB);
                        sb.Append(" AND CLD.ARPB_E >= ");
                        sb.Append(sARPB);
                    }

                }

                sb.Append(@"  )");//DM.DISCOUNT_MASTER_ID  IN (
            }

            sb.Append(@"ORDER BY DM.DISCOUNT_CODE,DM.S_DATE");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            DataView dv = dt.DefaultView;

            DataTable dt1 = dv.ToTable(dt.TableName, false, new string[] { "DISCOUNT_MASTER_ID", "DISCOUNT_CODE", "DISCOUNT_NAME", "DISCOUNT_MONEY", "DISCOUNT_RATE", "S_DATE", "E_DATE", "DIS_USE_COUNT" });

            dt1.Columns.Add(new DataColumn("ROW_NO", System.Type.GetType("System.String")));
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                dt1.Rows[i]["ROW_NO"] = (i + 1).ToString();
            }

            return dt1;
        }

       public DataTable Query_SP_DisCountQuery_XXX(
            string sSA_TYPE
          , string sM_TYPE
          , string sSTORE_NO
          , string sMSISDN
          , string sMM
          , string sMM_NAME
          , string sPROD_NO
          , string sPROD_NAME
          , string sARPB
          )
       {
           DataTable O_DATA = new DataTable();
           using (OracleConnection oConn = OracleDBUtil.GetConnection())
           {
               OracleCommand oraCmd = new OracleCommand("SP_DISCOUNTQUERY");
               oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
               oraCmd.Parameters.Add(new OracleParameter("SA_TYPE", OracleType.VarChar, 2000)).Value = sSA_TYPE;
               oraCmd.Parameters.Add(new OracleParameter("M_TYPE", OracleType.VarChar, 2000)).Value = sM_TYPE;
               oraCmd.Parameters.Add(new OracleParameter("STORE_NO", OracleType.VarChar, 2000)).Value = sSTORE_NO;
               oraCmd.Parameters.Add(new OracleParameter("MSISDN", OracleType.VarChar, 2000)).Value = sMSISDN;
               oraCmd.Parameters.Add(new OracleParameter("MM", OracleType.VarChar, 2000)).Value = sMM;
               oraCmd.Parameters.Add(new OracleParameter("MM_NAME", OracleType.VarChar, 2000)).Value = sMM_NAME;
               oraCmd.Parameters.Add(new OracleParameter("PROD_NO", OracleType.VarChar, 2000)).Value = sPROD_NO;
               oraCmd.Parameters.Add(new OracleParameter("PROD_NAME", OracleType.VarChar, 2000)).Value = sPROD_NAME;
               oraCmd.Parameters.Add(new OracleParameter("ARPB", OracleType.VarChar, 2000)).Value = sARPB;
               oraCmd.Parameters.Add(new OracleParameter("O_DATA", OracleType.Cursor)).Direction = ParameterDirection.Output;
               oraCmd.Connection = oConn;
               oraCmd.ExecuteNonQuery();
               OracleDataAdapter da = new OracleDataAdapter(oraCmd);
               da.Fill(O_DATA);
               return O_DATA;
           }

       }

       // DISCOUNT_MASTER
       public DataTable Query_DISCOUNT_MASTER()
       {
           StringBuilder sb = new StringBuilder();
           sb.Append(@"SELECT * 
                            FROM   VW_DISCOUNT_MASTER 
                            WHERE DEL_FLAG='N' OR DEL_FLAG='0'");

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }

       //PRODUCT_DISCOUNT 指定商品
       public DataTable Query_PRODUCT_DISCOUNT(string sDISCOUNT_MASTER_ID) 
       {
           return Query_VIEW_BY_ID("VW_PRODUCT_DISCOUNT", sDISCOUNT_MASTER_ID);
       }

       //STORE_DISCOUNT  指定門市
       public DataTable Query_STORE_DISCOUNT(string sDISCOUNT_MASTER_ID)
       {
           return Query_VIEW_BY_ID("VW_STORE_DISCOUNT", sDISCOUNT_MASTER_ID);
       }

       //PROMOTION_DISCOUNT  促銷代號
       public DataTable Query_PROMOTION_DISCOUNT(string sDISCOUNT_MASTER_ID)
       {
           return Query_VIEW_BY_ID("VW_PROMOTION_DISCOUNT", sDISCOUNT_MASTER_ID);
       }

       //CUST_LEVE_DISCOUNT  客戶對象
       public DataTable Query_CUST_LEVE_DISCOUNT(string sDISCOUNT_MASTER_ID)
       {
           return Query_VIEW_BY_ID("VW_CUST_LEVE_DISCOUNT", sDISCOUNT_MASTER_ID);
       }

       //COST_CENTER_DISCOUNT 成本中心
       public DataTable Query_COST_CENTER_DISCOUNT(string sDISCOUNT_MASTER_ID)
       {
           return Query_VIEW_BY_ID("VW_COST_CENTER_DISCOUNT", sDISCOUNT_MASTER_ID);
       }

       //GIFT_DISCOUNT  贈品設定
       public DataTable Query_GIFT_DISCOUNT(string sDISCOUNT_MASTER_ID)
       {
           return Query_VIEW_BY_ID("VW_GIFT_DISCOUNT", sDISCOUNT_MASTER_ID);
       }

       //ADD_IN_PROD_DISCOUNT  加價購
       public DataTable Query_ADD_IN_PROD_DISCOUNT(string sDISCOUNT_MASTER_ID)
       {
           return Query_VIEW_BY_ID("VW_ADD_IN_PROD_DISCOUNT", sDISCOUNT_MASTER_ID);
       }

       //RATE_PLAN_DISCOUNT
       public DataTable Query_RATE_PLAN_DISCOUNT(string sDISCOUNT_MASTER_ID)
       {
           return Query_VIEW_BY_ID("VW_RATE_PLAN_DISCOUNT", sDISCOUNT_MASTER_ID);
       }

       public DataTable Query_VIEW_BY_ID(string sViewName, string sDISCOUNT_MASTER_ID)
       {
           StringBuilder sb = new StringBuilder();
           sb.Append(@"SELECT * 
                           FROM   " + sViewName + " WHERE 1=1  ");
           if (!string.IsNullOrEmpty(sDISCOUNT_MASTER_ID))
           {
               sb.Append(" AND DISCOUNT_MASTER_ID = " + OracleDBUtil.SqlStr(sDISCOUNT_MASTER_ID));
           }

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }

       public DataTable Query_CUST_LEVE_DISCOUNT(string sDISCOUNT_MASTER_ID, string sType)
       {
           StringBuilder sb = new StringBuilder();
           sb.Append(@" SELECT CUST_LEVEL_ID,
                              DISCOUNT_MASTER_ID,
                              ARPB_S,
                              ARPB_E,
                              MSISDN,
                              USE_TYPE,
                              ROW_NUMBER () OVER (ORDER BY ARPB_S) AS ROW_NO
                            FROM CUST_LEVE_DISCOUNT  ");

           if (!string.IsNullOrEmpty(sDISCOUNT_MASTER_ID))
           {
               sb.Append(" WHERE DISCOUNT_MASTER_ID = " + OracleDBUtil.SqlStr(sDISCOUNT_MASTER_ID));
           }
           if (sType == "M")
           {//客戶名單
               sb.Append("  AND MSISDN IS NOT NULL ");
               sb.Append("  ORDER BY MSISDN ");
           }
           if (sType == "C")
           {//電話 
               sb.Append("  AND ARPB_S IS NOT NULL ");
               sb.Append("  ORDER BY ARPB_S ");
           }

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }
   }
}
