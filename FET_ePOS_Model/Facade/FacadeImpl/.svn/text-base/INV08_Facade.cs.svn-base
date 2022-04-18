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
    public class INV08_Facade
    {
        public DataTable Query_INVMethodSet(string POEONO, string Supplier, string ProdNo, string OrderNo, string CHK_SDATE, string CHK_EDATE, string Status, string ORD_SDATE, string ORD_EDATE, int _count, string STORE)
        {
            StringBuilder sb = new StringBuilder();
            //,NVL(( select DISTINCT NVL(TO_DATE(ORDDATE,'YYYYMMDD'),'') from ORDER_M WHERE substr(ORDER_NO,0,2)||substr(ORDER_NO,5,6)||substr(ORDER_NO,12,3) = Q.ORDER_NO AND STORE_NO= Q.STORE_NO ),'') ORDDATE 

            sb.AppendLine(@"SELECT  distinct Q.ORDER_SHIPCON_HEADER_ID 
                                    ,Q.STORE_NO    
                                    ,Q.STORENAME 
                                    ,Q.ORDER_NO   
                                    ,Q.OEPO_NO      AS PO_OE_NO  
                                    ,Q.STATUS_CNAME AS STATUS  
                                    ,NVL(trim(Q.INV_APPROVE_NO),' ') AS INV_APPROVE_NO   
                                    ,Q.CHECK_IN_DTM               
                                    ,NVL(( select EMPNAME  from EMPLOYEE WHERE EMPNO = Q.MODI_USER ),'') MODI_USER
                                    ,TO_CHAR(Q.MODI_DTM, 'yyyy/mm/dd hh24:mi:ss') MODI_DTM   
                                    ,NVL(( select DISTINCT NVL(TO_DATE(ORDDATE,'YYYYMMDD'),'') from ORDER_M WHERE ORDER_NO = Q.ORDER_NO AND STORE_NO= Q.STORE_NO and rownum=1 ),'') ORDDATE 
                            FROM  QueryOEPOInfo Q  
                            ");
            //if (_count != 0 && _count.ToString() != string.Empty)
            //{
            //    sb.AppendLine(",INVENTORY_APPROVAL_M INVM, INVENTORY_APPROVAL_D INVD   ");
            //}
            sb.AppendLine("WHERE 1 =1 ");
//            if (_count != 0 && _count.ToString() != string.Empty)
//            {
//                sb.AppendLine(@" AND Q.INV_APPROVE_NO = INVM.INV_APPROVAL_NO
//                                 AND INVM.INV_APPROVAL_M_ID = INVD.INV_APPROVAL_M_ID  
//                             ");
//            }
            if (STORE.Trim() != System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"].ToString())
            {
                sb.AppendLine(" AND Q.STORE_NO = " + OracleDBUtil.SqlStr(STORE.Trim()));
            }
            if (!string.IsNullOrEmpty(POEONO))
            {
                sb.AppendLine(" AND Q.OEPO_NO LIKE " + OracleDBUtil.LikeStr(POEONO.Trim()));
            }
            if (!string.IsNullOrEmpty(OrderNo))
            {
                sb.AppendLine(" AND Q.ORDER_NO LIKE " + OracleDBUtil.LikeStr(OrderNo.Trim()));
            }
            if (!string.IsNullOrEmpty(Supplier))
            {
                //sb.Append(" AND Q.VENDORNAME = ");
                //sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(Supplier.Trim()));
                sb.AppendLine(" AND Q.VENDORNAME LIKE " + OracleDBUtil.LikeStr(Supplier.Trim()));
            }
            if (!string.IsNullOrEmpty(ProdNo))
            {
                sb.AppendLine(" AND Q.PROD_NO LIKE " + OracleDBUtil.LikeStr(ProdNo.Trim()));
            }

            if (!string.IsNullOrEmpty(CHK_SDATE))
            {
                sb.AppendLine(" AND to_date( to_char( Q.CHECK_IN_DTM,'YYYY/MM/DD'), 'YYYY/MM/DD') >= " + OracleDBUtil.DateStr(CHK_SDATE));

            }
            if (!string.IsNullOrEmpty(CHK_EDATE))
            {
                sb.AppendLine(" AND to_date( to_char( Q.CHECK_IN_DTM,'YYYY/MM/DD'), 'YYYY/MM/DD') <= " + OracleDBUtil.DateStr(CHK_EDATE));

            }
            if (!string.IsNullOrEmpty(ORD_SDATE))
            {
                sb.AppendLine(" AND NVL(( select DISTINCT NVL(TO_DATE(ORDDATE,'YYYYMMDD'),'')  from ORDER_M WHERE ORDER_NO = Q.ORDER_NO AND STORE_NO= Q.STORE_NO and rownum=1 ),'') >= " + OracleDBUtil.DateStr(ORD_SDATE));

            }
            if (!string.IsNullOrEmpty(ORD_EDATE))
            {
                sb.AppendLine(" AND NVL(( select DISTINCT NVL(TO_DATE(ORDDATE,'YYYYMMDD'),'')  from ORDER_M WHERE ORDER_NO= Q.ORDER_NO AND STORE_NO= Q.STORE_NO and rownum=1 ),'') <= " + OracleDBUtil.DateStr(ORD_EDATE));

            }
            if (Status != "ALL" && Status != string.Empty)
            {
                sb.AppendLine(" AND Q.STATUS = " + OracleDBUtil.SqlStr(Status));
            }

            if (_count == 0 && _count.ToString() != string.Empty)
            {
                sb.AppendLine(" AND (Q.STATUS = '00' or Q.STATUS = '50') ");
            }

            sb.AppendLine("     ORDER BY Q.OEPO_NO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable GetINVM(string INV_APPROVAL_NO, string PooeNo, string ORDER_NO, string STORE)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" select  m.ORDER_SHIPCON_HEADER_ID  
                                    ,m.STORE_NO      AS STORE_NO 
                                    ,STOR.STORENAME  AS STORENAME  
                                    ,m.ORDER_NO      AS ORDER_NO    
                                    ,m.OEPO_NO       AS PO_OE_NO  
                                    ,m.STATUS        AS STATUS    
                                    ,decode(m.STATUS ,'00','未驗收','50','部份驗收','70','已結案') as STATUS_CNAME
                                    ,ChkList.INV_APPROVE_NO  AS INV_APPROVE_NO  
                                    ,TO_CHAR(ChkList.MODI_DTM, 'YYYY/MM/DD') AS MODI_DTM  
                                    ,ChkList.MODI_USER AS MODI_USER  
                            from  Order_ShipConfirm_Header m ,OEPO_CheckNo_List ChkList ,STORE STOR              
                            where m.ORDER_SHIPCON_HEADER_ID = ChkList.ORDER_SHIPCON_HEADER_ID (+)
                            and m.STORE_NO = STOR.STORE_NO   
                            AND m.STORE_NO = " + OracleDBUtil.SqlStr(STORE.Trim())
                         );
            if (PooeNo.Trim() != string.Empty)
            {
                sb.AppendLine(" AND m.OEPO_NO = " + OracleDBUtil.SqlStr(PooeNo.Trim()));
            }
            if (ORDER_NO.Trim() != string.Empty)
            {
                sb.AppendLine(" AND m.ORDER_NO = " + OracleDBUtil.SqlStr(ORDER_NO.Trim()));
            }
            if (INV_APPROVAL_NO.Trim() != string.Empty)
            {
                sb.AppendLine(" AND  ChkList.INV_APPROVE_NO = " + OracleDBUtil.SqlStr(INV_APPROVAL_NO.Trim()));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable GetINVD_INVANO(string INV_APPROVAL_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" SELECT INV_APPROVAL_D_ID DUUID,M.PO_OE_NO    AS PO_OE_NO    
                                    ,D.PRODNO      AS PRODNO    
                                    ,PROD.PRODNAME AS PRODNAME  
                                    ,NVL(D.IN_QTY,0)      AS IN_QTY   
                                    ,''                AS SUPPNO  
                                    ,SUPPNAME           AS SUPPNAME 
                                    ,NVL(D.ACCEPT_QTY,0)  AS ACCEPT_QTY 
                                    ,NVL(D.ON_HAND_QTY,0) AS ON_HAND_QTY 
                                    ,'' AS imgIMEI               
                                    ,'' AS IMEI                   
                                    ,PROD.IMEI_FLAG AS IMEI_FLAG        
                                    ,0 AS CHECK_IN_QTY
                                    ,NVL((SELECT COUNT(*) FROM INV_APPRO_IMEI WHERE INV_APPRO_IMEI.INV_APPROVAL_D_ID=D.INV_APPROVAL_D_ID AND INV_APPRO_IMEI.PRODNO=D.PRODNO),0 ) AS IMEI_QTY 
                            from INVENTORY_APPROVAL_M M ,INVENTORY_APPROVAL_D D ,PRODUCT PROD             
                            WHERE 1 = 1                
                            AND M.INV_APPROVAL_M_ID = D.INV_APPROVAL_M_ID    
                            AND D.PRODNO = PROD.PRODNO                                  
                        ");

            if (INV_APPROVAL_NO.Trim() != string.Empty)
            {
                sb.AppendLine(" AND  M.INV_APPROVAL_NO = " + OracleDBUtil.SqlStr(INV_APPROVAL_NO.Trim()));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable GetINVD_POOENO(string POOENO, string ORDER_NO, string STORE)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT  POS_UUID DUUID
                                    , M.ORDER_SHIPCON_HEADER_ID   AS ORDER_SHIPCON_HEADER_ID
                                    , M.STORE_NO                  AS STORE_NO 
                                    , M.OEPO_NO                   AS PO_OE_NO 
                                    , PROD.PRODNO                 AS PRODNO
                                    , PROD.PRODNAME               AS PRODNAME
                                    --,PROD.SUPP_ID                AS SUPPNO  
                                    --,SUPPLIER.SUPPNAME           AS SUPPNAME 
                                    , ''                          AS SUPPNO  
                                    , VENDORNAME                  AS SUPPNAME  
                                    , NVL(D.SHIPCONF_QTY,0)       AS IN_QTY                            --SHIPCONF等於到貨量
                                    , NVL(D.SHIPCONF_QTY,0) - NVL(D.INCOUNT_QTY,0) AS ACCEPT_QTY       --SHIPCONF-累計進貨驗收數 等於本次驗收數
                                    , NVL(D.INCOUNT_QTY,0) AS CHECK_IN_QTY --累計進貨驗收數 等於已驗收數
                                    , 0 AS ON_HAND_QTY           
                                    , '' AS imgIMEI  
                                    , '' AS IMEI             
                                    , PROD.IMEI_FLAG AS IMEI_FLAG  
                                    , 0 AS IMEI_QTY           
                              FROM  Order_ShipConfirm_Header M ,Order_ShipConfirm_DETAIL d1,PRODUCT PROD ,OENO_INQTY D 
                                    --,SUPPLIER          
                              where M.ORDER_SHIPCON_HEADER_ID = D.ORDER_SHIPCON_HEADER_ID
                                and D.ITEMCODE = PROD.PRODNO     
                                AND d.ORDER_SHIPCON_HEADER_ID = d1.ORDER_SHIPCON_HEADER_ID(+)
                                AND d.itemcode = d1.PRODNO(+)
                              --and PROD.SUPP_ID = SUPPLIER.SUPPNO    
                                and NVL(D.INWAY_QTY,0) > 0                                 
                                ");
            if (!string.IsNullOrEmpty(STORE.Trim()))
            {
                sb.AppendLine(" AND m.STORE_NO = " + OracleDBUtil.SqlStr(STORE.Trim()));
            }
            if (!string.IsNullOrEmpty(POOENO.Trim()))
            {
                sb.AppendLine(" AND M.OEPO_NO = " + OracleDBUtil.SqlStr(POOENO.Trim()));
            }
            if (!string.IsNullOrEmpty(ORDER_NO.Trim()))
            {
                sb.AppendLine(" AND M.ORDER_NO = " + OracleDBUtil.SqlStr(ORDER_NO.Trim()));
            }
            sb.AppendLine("    ORDER BY PROD.PRODNO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable GetSUPPLIER(string SUPPNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT  SUPPNAME    AS SUPPNAME  
                                    , CONTNAME    AS CONTNAME    
                                    , CONTTEL     AS CONTTEL     
                                    , FAX         AS FAX         
                                    , EMAIL       AS EMAIL      
                            FROM SUPPLIER            
                            WHERE 1 = 1                       
                        ");

            if (SUPPNO.Trim() != string.Empty)
            {
                sb.AppendLine(" AND SUPPNO = " + OracleDBUtil.SqlStr(SUPPNO.Trim()));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable GetIMEI(string POOENO, string PRODNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT IMEI AS IMEI  
                            FROM INV_APPRO_IMEI 
                            WHERE 1 = 1          
                        ");

            if (POOENO.Trim() != string.Empty)
            {
                sb.AppendLine(" AND PO_OE_NO = " + OracleDBUtil.SqlStr(POOENO.Trim()));
            }
            if (PRODNO.Trim() != string.Empty)
            {
                sb.AppendLine(" AND PRODNO = " + OracleDBUtil.SqlStr(PRODNO.Trim()));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public int SaveINV(INV08_InventoryApproval_DTO.INVENTORY_APPROVAL_MDataTable dtINVM,
                           INV08_InventoryApproval_DTO.INVENTORY_APPROVAL_DDataTable dtINVD,
                           INV08_InventoryApproval_DTO.OEPO_CHECKNO_LISTDataTable dtOEPO,
                           string OSH_ID, string IN_STATUS)
        {

            int intResult = 0;
            OracleConnection objConn = null;
            OracleTransaction objTx = null;
            try
            {
                INVENTORY_Facade Inventory = new INVENTORY_Facade();
                objConn = OracleDBUtil.GetConnection();
                objTx = objConn.BeginTransaction();

                intResult += OracleDBUtil.Insert(objTx, dtINVM);
                intResult += OracleDBUtil.Insert(objTx, dtINVD);
                //if (IN_STATUS.ToString().Trim() == "00-未驗收")
                //{
                intResult += OracleDBUtil.Insert(objTx, dtOEPO);
                //}

                //int _CHECK_IN_QTY = 0;  //驗收量
                //int _INWAY_QTY = 0;     //在途量
                //int _SHIPCONF_QTY = 0;  //出貨確認數
                //int _INCOUNT_QTY = 0;   //累計進貨驗收數
                string STORE_NO = dtINVM.Rows[0]["STORE_NO"].ToString();
                string NO = dtINVM.Rows[0]["ORDER_NO"].ToString();

                if (dtINVD.Rows.Count > 0)
                {
                    string Code = "";
                    string Message = "";
                    string Stock = FET.POS.Model.Common.Common_PageHelper.GetGoodLOCUUID();
                    string PO_OE_NO = dtINVM.Rows[0]["PO_OE_NO"].ToString();
                    string MODI_USER = dtINVM.Rows[0]["MODI_USER"].ToString();
                    string Year = DateTime.Now.Year.ToString().Substring(0, 2);
                    //string SNO = NO.Substring(8, 3);
                    //string ONO = NO.Substring(2, 6);

                    string TNO = NO;// "SO" + Year + ONO + "-" + SNO;
                    foreach (DataRow dr in dtINVD.Rows)
                    {

                        //UPDATE OENO_INQTY-門市進貨主檔
                        //累計進貨驗收數 = 累計進貨驗收數 + 本次驗收量
                        //在途量=0 =>STATUS='70'; 在途量>0 =>STATUS='50';
                        string strSql = " UPDATE OENO_INQTY SET ";
                        strSql += "              CHECK_IN_QTY = " + dr["ACCEPT_QTY"].ToString();
                        strSql += "             ,INWAY_QTY    = " + dr["ON_HAND_QTY"].ToString();
                        strSql += "             ,INCOUNT_QTY  = INCOUNT_QTY + " + dr["ACCEPT_QTY"].ToString();
                        strSql += "             ,CHECK_IN_DTM =SYSDATE";
                        if (int.Parse(dr["ON_HAND_QTY"].ToString()) == 0)
                        { strSql += "           ,STATUS = '70' "; }
                        else
                        { strSql += "           ,STATUS = '50' "; }
                        strSql += "        WHERE 1 =1   ";
                        strSql += "          AND OE_PO_NO = " + OracleDBUtil.SqlStr(PO_OE_NO);
                        strSql += "          AND STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO);
                        strSql += "          AND ORDER_NO = " + OracleDBUtil.SqlStr(NO);
                        strSql += "          AND ITEMCODE = " + OracleDBUtil.SqlStr(dr["PRODNO"].ToString());
                        strSql += "          AND ORDER_SHIPCON_HEADER_ID = " + OracleDBUtil.SqlStr(OSH_ID.ToString());

                        OracleDBUtil.ExecuteSql(objTx, strSql);

                        //UPDATE ORDER_SHIPCONFIRM_HEADER-訂單出貨確認主檔 -- UPDATE OENO 狀態
                        strSql = @" UPDATE Order_ShipConfirm_Header M 
                                    SET M.Status =             
                                        DECODE (                        
                                            (select count(*) as cnt  
                                             from OENO_INQTY D         
                                             where D.ORDER_SHIPCON_HEADER_ID = M.ORDER_SHIPCON_HEADER_ID 
                                             and D.INWAY_QTY > 0 ),0,'70','50')            
                                    WHERE M.ORDER_SHIPCON_HEADER_ID = " + OracleDBUtil.SqlStr(OSH_ID.ToString()) + " ";

                        OracleDBUtil.ExecuteSql(objTx, strSql);


                        //UPDATE ORDER_SHIPCONFIRM_DETAIL-訂單出貨確認主檔 -- UPDATE OENO 狀態
                        strSql = @" UPDATE ORDER_SHIPCONFIRM_DETAIL M  
                                    SET M.Status =              
                                    DECODE (                     
                                         (select count(*) as cnt   
                                          from OENO_INQTY D                
                                          where D.ORDER_SHIPCON_HEADER_ID = M.ORDER_SHIPCON_HEADER_ID 
                                          and D.INWAY_QTY > 0 ),0,'70','50')   
                                    WHERE M.ORDER_SHIPCON_HEADER_ID = " + OracleDBUtil.SqlStr(OSH_ID.ToString()) + " ";

                        OracleDBUtil.ExecuteSql(objTx, strSql);


                        //UPDATE SCQC_D-卡片已補貨量 -- IN_QTY 己補貨量的數量
                        strSql = @" UPDATE SCQC_D SET IN_QTY = IN_QTY + " + dr["ACCEPT_QTY"].ToString() + @"
                                    WHERE  SCQC_M_ID IN (select scqc_m_id FROM scqc_m WHERE STORE_NO= " + OracleDBUtil.SqlStr(STORE_NO) + @") 
                                    AND SIM_GROUP_ID IN (SELECT SIM_GROUP_ID from SIM_GROUP_PROD WHERE PRODNO= " + OracleDBUtil.SqlStr(dr["PRODNO"].ToString()) + " )";
                        OracleDBUtil.ExecuteSql(objTx, strSql);


                        //異動庫存 PK_INVENTORY_SHIPACCEPT(); 
                        Code = "";
                        Message = "";

                        Inventory.PK_INVENTORY_SHIPACCEPT(objTx, "1", dr["PRODNO"].ToString(),
                           STORE_NO, Stock, PO_OE_NO, Convert.ToInt32(dr["ACCEPT_QTY"].ToString()),
                           MODI_USER, dr["INV_APPROVAL_D_ID"].ToString(), ref Code, ref Message);

                        if (Code != "000") throw new Exception("PO/OE_NO：" + PO_OE_NO + ", 商品料號：" + dr["PRODNO"].ToString() + "異動庫存檔失敗. ERROR_MSG:" + Message);

                        DataTable dtIMEI = new IMEI_Facade().getINV_IMEI("INV_APPRO_IMEI", dr["INV_APPROVAL_D_ID"].ToString(), dr["PRODNO"].ToString());
                        foreach (DataRow drIMEI in dtIMEI.Rows)
                        {
                            Call_SP_ImeiCheckIn(objTx, drIMEI["IMEI"].ToString(), STORE_NO, "RETAIL"
                                , Stock, dr["PRODNO"].ToString(), " ", MODI_USER
                                , NO, GetORDDATE(NO, STORE_NO)
                                , (dr["DS_FLAG"].ToString() != "Y" ? dr["ORDER_NO"].ToString() : " "), (dr["DS_FLAG"].ToString() == "Y" ? dr["ORDER_NO"].ToString() : " "), ref Code, ref Message);
                            if (Code != "000") throw new Exception("PO/OE_NO：" + PO_OE_NO + ", 商品料號：" + dr["PRODNO"].ToString() + "變更IMEI狀態失敗. ERROR_MSG:" + Message);
                        }

                        string strSqlPROD = @"  select ORDER_D.ORDER_ID,CHECK_IN_QTY 
                                                from order_d ,order_m 
                                                where 1=1
                                                and order_d.ORDER_ID(+) = order_m.ORDER_ID 
                                                and store_no = " + OracleDBUtil.SqlStr(STORE_NO) + @"
                                                and order_no = " + OracleDBUtil.SqlStr(TNO) + @"
                                                and PRODNO = " + OracleDBUtil.SqlStr(dr["PRODNO"].ToString());
                        string PRODQTY = "0";
                        DataTable dtx = OracleDBUtil.Query_Data(strSqlPROD);
                        string ORDER_ID = "";
                        foreach (DataRow drP in dtx.Rows)
                        {
                            PRODQTY = drP["CHECK_IN_QTY"].ToString();
                            ORDER_ID = drP["ORDER_ID"].ToString();
                            if (PRODQTY == "")
                            {
                                PRODQTY = "0";
                            }
                        }

//                        int totalqty = Convert.ToInt32(PRODQTY) + Convert.ToInt32(dr["ACCEPT_QTY"].ToString());
//                        string strSqlP = @" update ORDER_D set CHECK_IN_QTY = " + OracleDBUtil.SqlStr(totalqty.ToString()) + @" 
//                                            where 1=1
//                                            and ORDER_ID = " + OracleDBUtil.SqlStr(ORDER_ID) + @" 
//                                            and PRODNO = " + OracleDBUtil.SqlStr(dr["PRODNO"].ToString());

                        //**2011/04/30 Tina：CHECK_IN_QTY此欄位是存放這次的驗收量，INCOUNTQTY才是存放累積的驗收量。
                        string strSqlP = @" update ORDER_D set CHECK_IN_QTY = " + dr["ACCEPT_QTY"].ToString() + @" 
                                                             , INCOUNTQTY = NVL(INCOUNTQTY, 0) + " + dr["ACCEPT_QTY"].ToString() + @" 
                                            where 1=1
                                            and ORDER_ID = " + OracleDBUtil.SqlStr(ORDER_ID) + @" 
                                            and PRODNO = " + OracleDBUtil.SqlStr(dr["PRODNO"].ToString());
                        //strSqlP += " and store_no = '" + dtINVM.Rows[0]["STORE_NO"].ToString() + "'";

                        OracleDBUtil.ExecuteSql(objTx, strSqlP);

                    }
                }

                //UPDATE-ORDER_M-STATUS 
                string strSql1 = @" UPDATE order_m  SET status = '70'   
                                    WHERE 1 = 1      
                                    AND store_no = " + OracleDBUtil.SqlStr(STORE_NO) + @"
                                    AND order_no = " + OracleDBUtil.SqlStr(NO) + @"
                                    AND (SELECT COUNT(ORDER_ITEMS_ID)  
                                         FROM order_d WHERE order_id IN (  
                                                    SELECT DISTINCT order_id  
                                                    FROM order_m    
                                                    WHERE store_no = " + OracleDBUtil.SqlStr(STORE_NO) + @"     
                                                    AND order_no = " + OracleDBUtil.SqlStr(NO) + @")
                                          ) = 
                                        (SELECT COUNT(OENO_INQTY_ID) AS cnt     
                                         FROM oeno_inqty                   
                                         WHERE order_shipcon_header_id IN ( 
                                                    SELECT DISTINCT order_shipcon_header_id  
                                                    FROM order_shipconfirm_header   
                                                    WHERE store_no = " + OracleDBUtil.SqlStr(STORE_NO) + @"    
                                                    AND order_no = " + OracleDBUtil.SqlStr(NO) + @")   
                                         AND inway_qty = 0
                                        )  ";

                OracleDBUtil.ExecuteSql(objTx, strSql1);


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
        /// 取得定購日期
        /// </summary>
        public string GetORDDATE(string ORDER_NO, string STORE_NO)
        {
            string str = "";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"select  to_char( to_date(ORDDATE,'YYYY/MM/DD'),'YYYY/MM/DD')  ORDDATE 
                            from ORDER_M 
                            WHERE 1=1 
                            AND ORDER_NO = " + OracleDBUtil.SqlStr(ORDER_NO) + @"
                            AND STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO)
                         );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            if (dt.Rows.Count > 0)
            {
                str = dt.Rows[0]["ORDDATE"].ToString();
            }

            return str;
        }

        /// <summary>
        /// 變更IMEI狀態 (進貨驗收)
        /// </summary>
        ///<paramname="objTX"></param>
        ///<paramname="inIMEI">IMEI</param>
        ///<paramname="inIVRCODE">店點代碼</param>
        ///<paramname="inCHANNEL_ID">通路</param>
        ///<paramname="inLOC_ID">倉別</param>
        ///<paramname="inPRODNO">產品代碼</param>
        ///<paramname="inHOST_ID">連線主機IP</param>
        ///<paramname="inUSER_ID">登入者員編</param>
        ///<paramname="inOrder_NO">訂貨單號</param>
        ///<paramname="inORDER_DATE">訂購日期</param>
        ///<paramname="inOE_NO">出貨單號nonedropshmenet</param>
        ///<paramname="inPO_NO">出貨單號dropshmenet</param>
        ///<paramname="outMSGCODE"></param>
        ///<paramname="outMESSAGE"></param>
        private void Call_SP_ImeiCheckIn(OracleTransaction objTX, string inIMEI, string inIVRCODE, string inCHANNEL_ID, string inLOC_ID, string inPRODNO,
            string inHOST_ID, string inUSER_ID, string inOrder_NO, string inORDER_DATE, string inOE_NO, string inPO_NO, ref string outMSGCODE, ref string outMESSAGE)
        {
            OracleCommand oraCmd = null;

            try
            {

                //oraCmd.Parameters.Add(new OracleParameter("inIMEI", inIMEI));     //IMEI
                //oraCmd.Parameters.Add(new OracleParameter("inIVRCODE", inIVRCODE));          //店點代碼
                //oraCmd.Parameters.Add(new OracleParameter("inCHANNEL_ID", inCHANNEL_ID));       //通路
                //oraCmd.Parameters.Add(new OracleParameter("inLOC_ID", inLOC_ID));           //倉別
                //oraCmd.Parameters.Add(new OracleParameter("inPRODNO", inPRODNO));           //產品代碼
                //oraCmd.Parameters.Add(new OracleParameter("inHOST_ID", inHOST_ID));          //連線主機IP
                //oraCmd.Parameters.Add(new OracleParameter("inUSER_ID", inUSER_ID));          //登入者員編
                //oraCmd.Parameters.Add(new OracleParameter("inOrder_NO", inOrder_NO));         //訂貨單號
                //oraCmd.Parameters.Add(new OracleParameter("inORDER_DATE", inORDER_DATE));       //訂購日期
                //oraCmd.Parameters.Add(new OracleParameter("inOE_NO", inOE_NO));            //出貨單號 none dropshmenet
                //oraCmd.Parameters.Add(new OracleParameter("inPO_NO", inPO_NO));            //出貨單號 dropshmenet
                //oraCmd.Parameters.Add(new OracleParameter("outMSGCODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                //oraCmd.Parameters.Add(new OracleParameter("outMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;



                oraCmd = new OracleCommand("PK_IMEI.SP_IMEICHECKIN");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;

                oraCmd.Parameters.Add(new OracleParameter("inIMEI", OracleType.VarChar, 2000)).Value = inIMEI;
                oraCmd.Parameters.Add(new OracleParameter("inIVRCODE", OracleType.VarChar, 2000)).Value = inIVRCODE;
                oraCmd.Parameters.Add(new OracleParameter("inCHANNEL_ID", OracleType.VarChar, 2000)).Value = inCHANNEL_ID;
                oraCmd.Parameters.Add(new OracleParameter("inLOC_ID", OracleType.VarChar, 2000)).Value = inLOC_ID;
                oraCmd.Parameters.Add(new OracleParameter("inPRODNO", OracleType.VarChar, 2000)).Value = inPRODNO;
                oraCmd.Parameters.Add(new OracleParameter("inHOST_ID", OracleType.VarChar, 2000)).Value = inHOST_ID;
                oraCmd.Parameters.Add(new OracleParameter("inUSER_ID", OracleType.VarChar, 2000)).Value = inUSER_ID;
                oraCmd.Parameters.Add(new OracleParameter("inOrder_NO", OracleType.VarChar, 2000)).Value = inOrder_NO;
                oraCmd.Parameters.Add(new OracleParameter("inORDER_DATE", OracleType.VarChar, 2000)).Value = inORDER_DATE;
                oraCmd.Parameters.Add(new OracleParameter("inOE_NO", OracleType.VarChar, 2000)).Value = inOE_NO;
                oraCmd.Parameters.Add(new OracleParameter("inPO_NO", OracleType.VarChar, 2000)).Value = inPO_NO;

                oraCmd.Parameters.Add(new OracleParameter("outMSGCODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("outMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;


                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                outMSGCODE = oraCmd.Parameters["outMSGCODE"].Value.ToString();
                outMESSAGE = oraCmd.Parameters["outMESSAGE"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
            }
        }

        public DataTable Query_PRODNO(string PRODNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT PRODUCT.PRODNO     AS PRODNO 
                                 , PRODUCT.PRODNAME   AS PRODNAME  
                                 , SUPPLIER.SUPPNAME  AS SUPPNAME 
                                 , PRODUCT.SUPP_ID    AS SUPPNO 
                            FROM  PRODUCT           
                            INNER JOIN SUPPLIER ON PRODUCT.SUPP_ID = SUPPLIER.SUPPNO 
                            WHERE 1 = 1            
                            AND PRODUCT.PRODNO = " + OracleDBUtil.SqlStr(PRODNO)
                       );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

    }
}