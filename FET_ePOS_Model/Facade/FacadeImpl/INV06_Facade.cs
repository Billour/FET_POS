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
    public class INV06_Facade
    {
        //public DataTable QueryRTNMData(string S_DATE, string E_DATE, string eKey, bool Empty, string STORENO, string RTNNO, string PRODNO, string STATUS)
        //{
        //    OracleConnection objConn = null;

        //    try
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        sb.Append("SELECT RTNM.RTNNO,RTNM.RTNN_ID, ");
        //        sb.Append("       RTNM.B_DATE, ");
        //        sb.Append("       RTNM.E_DATE, ");
        //        sb.Append("       RTNM.MODI_USER, ");
        //        sb.Append("       RTNM.MODI_DTM, ");
        //        sb.Append("       RTNM.STATUS,RTND_UP.RTNDATE,RTNM.RTNN_ID ");
        //        sb.Append(" FROM RTNM, RTND_PROD, RTND_STORE, RTND_UP ");
        //        sb.Append(" WHERE RTNM.RTNN_ID = RTND_PROD.RTNN_ID(+) ");
        //        sb.Append(" AND  RTNM.RTNN_ID = RTND_STORE.RTNN_ID(+) ");
        //        sb.Append(" AND RTND_PROD.RTND_PROD_ID = RTND_UP.RTND_PROD_ID(+) ");
        //        sb.Append(" AND RTND_STORE.STATUS in ('10','60') AND RTNM.STATUS in ('10','60') ");



        //        if (!string.IsNullOrEmpty(STORENO))
        //        {
        //            sb.Append(" AND RTND_STORE.STORE_NO = " + OracleDBUtil.SqlStr(STORENO));
        //        }

        //        if (!string.IsNullOrEmpty(S_DATE) && !string.IsNullOrEmpty(E_DATE))
        //        {
        //            sb.Append(" AND RTNM.B_DATE <=" + OracleDBUtil.TimeStr(S_DATE));
        //            sb.Append(" AND RTNM.E_DATE >= " + OracleDBUtil.TimeStr(E_DATE));
        //        }
        //        if (!string.IsNullOrEmpty(S_DATE) && string.IsNullOrEmpty(E_DATE))
        //        {
        //            sb.Append(" AND RTNM.B_DATE <= " + OracleDBUtil.TimeStr(S_DATE));
        //        }
        //        if (string.IsNullOrEmpty(S_DATE) && !string.IsNullOrEmpty(E_DATE))
        //        {
        //            sb.Append(" AND RTNM.E_DATE >= " + OracleDBUtil.TimeStr(E_DATE));
        //        }
        //        if (!string.IsNullOrEmpty(RTNNO))
        //        {
        //            sb.Append(" AND RTNNO = " + OracleDBUtil.SqlStr(RTNNO));
        //        }
        //        if (!string.IsNullOrEmpty(PRODNO))
        //        {
        //            sb.Append(" AND RTND_PROD.PRODNO = " + OracleDBUtil.SqlStr(PRODNO));
        //        }

        //        objConn = OracleDBUtil.GetConnection();
        //        DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (objConn.State == ConnectionState.Open) objConn.Close();
        //        objConn.Dispose();
        //        OracleConnection.ClearAllPools();
        //    }

        //}

        public DataTable QueryRTNDData(string S_DATE, string E_DATE, string eKey, bool Empty, string STORENO, string RTNNO, string PRODNO, string STATUS, string WorkDay)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("SELECT RTNM.RTNNO,Product.PRODNO,Product.PRODNAME, ");
            //sb.Append("       RTNM.B_DATE, ");
            //sb.Append("       ON_HAND_QTY, ");
            //sb.Append("       RTNM.E_DATE, ");
            //sb.Append("       RTNM.MODI_USER, ");
            //sb.Append("       RTNM.MODI_DTM, ");
            //sb.Append("       RTNM.STATUS,RTND_UP.RTNDATE,RTNM.RTNN_ID ");
            //sb.Append(" FROM RTNM, RTND_PROD, RTND_STORE, Product,inv_on_Hand_Current,RTND_UP  ");
            //sb.Append(" WHERE RTNM.RTNN_ID = RTND_PROD.RTNN_ID(+) AND RTNM.RTNN_ID = RTND_STORE.RTNN_ID(+) ");
            //sb.Append(" AND RTND_PROD.PRODNO = PRODuct.PRODNO(+) AND RTND_PROD.PRODNO = inv_on_Hand_Current.PRODNO(+) AND RTND_STORE.STORE_NO = inv_on_Hand_Current.STORE_NO(+) ");
            //sb.Append(" AND RTND_PROD.RTND_PROD_ID = RTND_UP.RTND_PROD_ID(+) AND RTND_STORE.RTND_STORE_ID = RTND_UP.RTND_STORE_ID(+) ");
            //sb.Append(" AND RTND_STORE.STATUS  in ('10','60') AND RTNM.STATUS  in ('10','60') ");

            sb.Append("SELECT distinct   RTNM.RTNNO,");
            sb.Append("       RTNM.B_DATE, ");
            sb.Append("       RTNM.E_DATE, ");
            sb.Append("       to_char((TO_DATE((SELECT RTNDATE FROM RTND_UP WHERE  RTND_STORE.RTND_STORE_ID = RTND_UP.RTND_STORE_ID AND ROWNUM = 1),'YYYY/MM/DD')),'YYYY/MM/DD') RTNDATE, ");
            sb.Append("        E.EMPNAME AS MODI_USER, ");
            sb.Append("       TO_CHAR(RTNM.MODI_DTM, 'yyyy/mm/dd hh24:mi:ss') MODI_DTM, ");
            sb.Append("       RTNM.STATUS,RTNM.RTNN_ID ");
            sb.Append("from RTNM  join RTND_STORE on RTNM.RTNN_ID = RTND_STORE.RTNN_ID  ");
            sb.Append("           join RTND_PROD on RTNM.RTNN_ID = RTND_PROD.RTNN_ID ");
            sb.Append("           join inv_on_Hand_Current on RTND_PROD.PRODNO = inv_on_Hand_Current.PRODNO and RTND_STORE.STORE_NO = inv_on_Hand_Current.STORE_NO ");
            sb.Append("           left join Employee E on RTNM.MODI_USER = E.EMPNO ");
            //
            //sb.Append("left join inv_on_Hand_Current on RTND_PROD.PRODNO = inv_on_Hand_Current.PRODNO and RTND_STORE.STORE_NO = inv_on_Hand_Current.STORE_NO ");
            //sb.Append("left join RTND_UP on RTND_PROD.RTND_PROD_ID = RTND_UP.RTND_PROD_ID and RTND_STORE.RTND_STORE_ID = RTND_UP.RTND_STORE_ID ");
            sb.Append("WHERE 1 = 1   ");
            // MantisBT #796 退倉的開始日未開始,不可開放給門市user輸入~by vivian 2011/3/16
            sb.Append(" AND RTNM.B_DATE <= " + OracleDBUtil.DateStr(OracleDBUtil.WorkDay(STORENO)));

            if (!string.IsNullOrEmpty(STORENO))
            {
                sb.Append(" AND RTND_STORE.STORE_NO = " + OracleDBUtil.SqlStr(STORENO));
            }

            #region // 根據SA說法，若查詢條件輸入退倉日期，將無法查詢到未完成的資料
            //MantisBT #482 庫存有資料，但無法以退倉日當天的日期來查出退倉資料
            if (!string.IsNullOrEmpty(S_DATE))
            {
                //sb.Append(" AND RTNM.B_DATE >= " + OracleDBUtil.DateStr(S_DATE));
                sb.Append(" AND (TO_DATE((SELECT RTNDATE FROM RTND_UP WHERE  RTND_STORE.RTND_STORE_ID = RTND_UP.RTND_STORE_ID AND ROWNUM = 1),'YYYY/MM/DD'))  >= " + OracleDBUtil.DateStr(S_DATE));
            }
            if (!string.IsNullOrEmpty(E_DATE))
            {
                //sb.Append(" AND RTNM.E_DATE <= " + OracleDBUtil.DateStr(E_DATE));
                sb.Append(" AND (TO_DATE((SELECT RTNDATE FROM RTND_UP WHERE  RTND_STORE.RTND_STORE_ID = RTND_UP.RTND_STORE_ID AND ROWNUM = 1),'YYYY/MM/DD'))  <= " + OracleDBUtil.DateStr(E_DATE));
            }
            //if (!string.IsNullOrEmpty(WorkDay))
            //{
            //    //sb.Append(" AND RTNM.B_DATE <=" + OracleDBUtil.DateStr(WorkDay));
            //    //sb.Append(" AND RTNM.E_DATE >= " + OracleDBUtil.DateStr(WorkDay));
            //    sb.Append(" AND (TO_DATE((SELECT RTNDATE FROM RTND_UP WHERE  RTND_STORE.RTND_STORE_ID = RTND_UP.RTND_STORE_ID AND ROWNUM = 1),'YYYY/MM/DD'))  >= " + OracleDBUtil.DateStr(S_DATE));
            //    sb.Append(" AND (TO_DATE((SELECT RTNDATE FROM RTND_UP WHERE  RTND_STORE.RTND_STORE_ID = RTND_UP.RTND_STORE_ID AND ROWNUM = 1),'YYYY/MM/DD'))  <= " + OracleDBUtil.DateStr(E_DATE));
            //}
            #endregion

            if (!string.IsNullOrEmpty(RTNNO))
            {
                sb.Append(" AND RTNNO Like " + OracleDBUtil.LikeStr(RTNNO));
            }//
            if (!string.IsNullOrEmpty(PRODNO))
            {
                sb.Append(" AND RTNM.RTNN_ID in (Select RTND_PROD.RTNN_ID From RTND_PROD ");
                sb.Append("                  join RTND_STORE on RTND_PROD.RTNN_ID = RTND_STORE.RTNN_ID");
                sb.Append("                   join inv_on_Hand_Current on RTND_PROD.PRODNO = inv_on_Hand_Current.PRODNO and RTND_STORE.STORE_NO = inv_on_Hand_Current.STORE_NO");
                sb.Append("                  WHERE RTND_PROD.PRODNO LIKE " + OracleDBUtil.LikeStr(PRODNO) + " group by RTND_PROD.RTNN_ID )");
            }
            if (!string.IsNullOrEmpty(STATUS))
            {
                //sb.Append(" AND RTND_STORE.STATUS   = " + OracleDBUtil.SqlStr(STATUS));
                sb.Append(" AND RTNM.STATUS  = " + OracleDBUtil.SqlStr(STATUS));
            }
            else
            {
                //sb.Append(" AND RTND_STORE.STATUS  in ('10','60') AND RTNM.STATUS  in ('10','60') ") ;
                sb.Append("  AND RTNM.STATUS  in ('10','60') ");

            }
            sb.Append(" ORDER BY 1 ");

            //if (!string.IsNullOrEmpty(S_DATE) && !string.IsNullOrEmpty(E_DATE))
            //{
            //    sb.Append(" AND RTND_UP.RTNDATE <=" + OracleDBUtil.SqlStr(S_DATE.Replace("/", "")));
            //    sb.Append(" AND RTND_UP.RTNDATE >= " + OracleDBUtil.SqlStr(E_DATE.Replace("/", "")));
            //}
            //if (!string.IsNullOrEmpty(S_DATE) && string.IsNullOrEmpty(E_DATE))
            //{
            //    sb.Append(" AND RTND_UP.RTNDATE <= " + OracleDBUtil.SqlStr(S_DATE.Replace("/", "")));
            //}
            //if (string.IsNullOrEmpty(S_DATE) && !string.IsNullOrEmpty(E_DATE))
            //{
            //    sb.Append(" AND RTND_UP.RTNDATE >= " + OracleDBUtil.SqlStr(E_DATE.Replace("/", "")));
            //}

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable QueryStatus(string RTNNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("  select STATUS FROM RTNM where 1=1 ");
            if (!string.IsNullOrEmpty(RTNNO))
            {
                sb.Append(" AND RTNN_ID = " + OracleDBUtil.SqlStr(RTNNO));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable QueryRTNMBottomData(string STORENO, string RTNNO)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("  select RTNM.RTNN_ID,RTND_PROD.PRODNO,PRODNAME,IMEI_FLAG,INV_ON_HAND_CURRENT.ON_HAND_QTY,RTND_PROD.RTND_PROD_ID,RTND_STORE.RTND_STORE_ID,OPENQTY,UNOPENQTY,RTNQTY,ENDQTY,RTNM.STATUS ");
            //sb.Append("         ,(select AFTER_PROCESS.DESCRIPTION from AFTER_PROCESS where AFTER_PROCESS.after_process_code=RTNM.AFTER_PROCESS_CODE and rownum=1) as DESCRIPTION ");
            //sb.Append("         ,(select  RETURN_DESCRIPTION from RETURN_REASON where RETURN_REASON.RETURN_REASON_CODE = RTNM.RETURN_REASON_CODE and rownum=1) as RETURN_DESCRIPTION ");
            //sb.Append("         ,to_char(RTNM.MODI_DTM,'YYYY/MM/DD') as MODI_DTM ");
            //sb.Append("         ,RTNM.MODI_USER as MODI_USER ");
            //sb.Append("         ,RTND_UP.RTNDATE as RTNDATE ");
            //sb.Append("         ,RTNM.RTNNO as RTNNO");
            //sb.Append("         ,to_char(RTNM.B_DATE,'YYYY/MM/DD') as B_DATE");
            //sb.Append("         ,to_char(RTNM.E_DATE,'YYYY/MM/DD') as E_DATE");
            //sb.Append("         ,RTNM.REMARK as REMARK ");
            //sb.Append("         ,RTND_STORE.STORE_NO as STORE_NO ");
            //sb.Append(" FROM RTNM, RTND_PROD, PRODUCT, RTND_STORE, INV_ON_HAND_CURRENT,RTND_UP  ");
            //sb.Append(" WHERE RTNM.RTNN_ID = RTND_PROD.RTNN_ID AND RTND_PROD.PRODNO = PRODUCT.PRODNO AND RTNM.RTNN_ID = RTND_STORE.RTNN_ID  ");
            //sb.Append(" AND RTND_STORE.STORE_NO = INV_ON_HAND_CURRENT.STORE_NO(+) AND PRODUCT.PRODNO = INV_ON_HAND_CURRENT.PRODNO(+)  ");
            //sb.Append(" AND RTND_PROD.RTND_PROD_ID = RTND_UP.RTND_PROD_ID(+) AND RTND_STORE.RTND_STORE_ID = RTND_UP.RTND_STORE_ID(+) ");

            sb.Append("  select RTNM.RTNN_ID,RTND_PROD.PRODNO,PRODNAME,RTND_PROD.RTND_PROD_ID,RTND_STORE.RTND_STORE_ID,OPENQTY,UNOPENQTY,nvl(RTNQTY,0) RTNQTY,ENDQTY,RTNM.STATUS ");
            sb.Append(", nvl(INV_ON_HAND_CURRENT.ON_HAND_QTY,0) ON_HAND_QTY ");
            sb.Append(", (SELECT RTND_IMEI.IMEI FROM RTND_IMEI WHERE RTND_UP.RTND_UP_ID = RTND_IMEI.RTND_UP_ID AND ROWNUM = 1) IMEI ");
            sb.Append(", (SELECT COUNT(RTND_IMEI.IMEI) FROM RTND_IMEI WHERE RTND_UP.RTND_UP_ID = RTND_IMEI.RTND_UP_ID) AS IMEI_QTY ");
            sb.Append(",  IMEI_FLAG ");
            sb.Append("         ,(select AFTER_PROCESS.DESCRIPTION from AFTER_PROCESS where AFTER_PROCESS.after_process_code=RTNM.AFTER_PROCESS_CODE and rownum=1) as DESCRIPTION ");
            sb.Append("         ,(select  RETURN_DESCRIPTION from RETURN_REASON where RETURN_REASON.RETURN_REASON_CODE = RTNM.RETURN_REASON_CODE and rownum=1) as RETURN_DESCRIPTION ");
            sb.Append("         ,to_char(RTNM.MODI_DTM,'YYYY/MM/DD hh24:mi:ss') as MODI_DTM ");
            sb.Append("         ,RTNM.MODI_USER as MODI_USER ");
            sb.Append("         , to_char(TO_DATE(RTND_UP.RTNDATE,'YYYY/MM/DD'),'YYYY/MM/DD') as RTNDATE ");
            sb.Append("         ,RTNM.RTNNO as RTNNO");
            sb.Append("         ,to_char(RTNM.B_DATE,'YYYY/MM/DD') as B_DATE");
            sb.Append("         ,to_char(RTNM.E_DATE,'YYYY/MM/DD') as E_DATE");
            sb.Append("         ,RTNM.REMARK as REMARK ");
            sb.Append("         ,RTND_STORE.STORE_NO as STORE_NO ");
            sb.Append("         , nvl(RTND_IMEI.RTND_UP_ID,POS_UUID) as RTND_UP_ID ");
            sb.Append("         , (nvl(INV_ON_HAND_CURRENT.ON_HAND_QTY,0)-nvl(RTNQTY,0)) as DIFF "); //差異量=帳上庫存量-退倉量
            sb.Append("  from RTNM INNER join RTND_PROD on RTNM.RTNN_ID = RTND_PROD.RTNN_ID INNER join PRODUCT on RTND_PROD.PRODNO = PRODUCT.PRODNO ");
            sb.Append("  INNER join RTND_STORE on RTNM.RTNN_ID = RTND_STORE.RTNN_ID   left join INV_ON_HAND_CURRENT on RTND_STORE.STORE_NO = INV_ON_HAND_CURRENT.STORE_NO ");
            sb.Append(" and INV_ON_HAND_CURRENT.PRODNO = PRODUCT.PRODNO  left join RTND_UP on RTND_PROD.RTND_PROD_ID = RTND_UP.RTND_PROD_ID and RTND_STORE.RTND_STORE_ID = RTND_UP.RTND_STORE_ID ");
            sb.Append(" Left Join RTND_IMEI on RTND_UP.RTND_UP_ID = RTND_IMEI.RTND_UP_ID  ");
            sb.Append("WHERE 1 = 1 and INV_ON_HAND_CURRENT.STOCK_ID=INV_GOODLOCUUID  ");

            if (!string.IsNullOrEmpty(STORENO))
            {
                sb.Append(" AND RTND_STORE.STORE_NO = " + OracleDBUtil.SqlStr(STORENO));
            }
            if (!string.IsNullOrEmpty(RTNNO))
            {
                sb.Append(" AND RTNM.RTNN_ID = " + OracleDBUtil.SqlStr(RTNNO));
            }
            sb.Append(" ORDER BY PRODNO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable QueryRTNMTopData(string STORENO, string RTNNO)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append(" select RTNNO,B_DATE,E_DATE,RTNDATE,RTNM.STATUS,RTNM.MODI_USER,RTNM.MODI_DTM , DESCRIPTION,RETURN_DESCRIPTION ");
            //sb.Append("  from RTNM, RTND_PROD, RTND_STORE, RTND_UP,AFTER_PROCESS, Return_REASON ");
            //sb.Append(" WHERE RTNM.RTNN_ID = RTND_PROD.RTNN_ID(+) AND RTNM.RTNN_ID = RTND_STORE.RTNN_ID(+) ");
            //sb.Append(" AND RTND_PROD.RTND_PROD_ID = RTND_UP.RTND_PROD_ID(+) AND RTNM.AFTER_PROCESS_CODE = AFTER_PROCESS.AFTER_PROCESS_CODE(+) ");
            //sb.Append(" AND RTNM.RETURN_REASON_CODE = RETURN_REASON.RETURN_REASON_CODE(+) ");
            sb.Append(" select RTNNO,B_DATE,E_DATE,RTNDATE,RTNM.STATUS,RTNM.MODI_USER,RTNM.MODI_DTM , DESCRIPTION,RETURN_DESCRIPTION ");
            sb.Append("  from RTNM left join RTND_PROD on RTNM.RTNN_ID = RTND_PROD.RTNN_ID left join RTND_STORE on RTNM.RTNN_ID = RTND_STORE.RTNN_ID ");
            sb.Append("  left join RTND_UP on  RTND_PROD.RTND_PROD_ID = RTND_UP.RTND_PROD_ID left join AFTER_PROCESS on RTNM.AFTER_PROCESS_CODE = AFTER_PROCESS.AFTER_PROCESS_CODE left join Return_REASON on RTNM.RETURN_REASON_CODE = RETURN_REASON.RETURN_REASON_CODE ");
            sb.Append("WHERE 1 = 1");

            if (!string.IsNullOrEmpty(STORENO))
            {
                sb.Append(" AND RTND_STORE.STORE_NO = " + OracleDBUtil.SqlStr(STORENO));
            }
            if (!string.IsNullOrEmpty(RTNNO))
            {
                sb.Append(" AND RTNM.RTNN_ID = " + OracleDBUtil.SqlStr(RTNNO));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public void AddNewOne_RTND(INV06_RTNM ds)
        {
            OracleDBUtil.Insert(ds.Tables["RTND_UP"]);
        }

        public void UpdateOne_RTND(INV06_RTNM ds)
        {
            OracleDBUtil.UPDDATEByUUID(ds.Tables["RTND_UP"], "RTND_UP_ID");
        }

        public void Add_RTND_Update_RTNM(INV06_RTNM dsRTND, DataTable dtInfo, string sUser)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.Insert(objTX, dsRTND.Tables["RTND_UP"]);

                OracleDBUtil.UPDDATEByUUID(objTX, dsRTND.Tables["RTNM"], "RTNN_ID");

                string sLOC = FET.POS.Model.Common.Common_PageHelper.GetGoodLOCUUID();

                INVENTORY_Facade Inventory = new INVENTORY_Facade();

                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DataRow dr = dtInfo.Rows[i];

                    //異動庫存 PK_INVENTORY_REJECTINVENTORY(); 
                    string sCode = "";
                    string sMessage = "";
                    string Stock = FET.POS.Model.Common.Common_PageHelper.GetGoodLOCUUID();

                    Inventory.PK_INVENTORY_REJECTINVENTORY(objTX, "1", dr["PRODNO"].ToString(),
                       dr["STORE_NO"].ToString(), sLOC, dr["RTNNO"].ToString(),
                       -Convert.ToInt32(dsRTND.Tables["RTND_UP"].Rows[i]["RTNQTY"].ToString()), sUser, dr["RTNN_ID"].ToString(), ref sCode, ref sMessage);

                    if (sCode.CompareTo("000") != 0)
                    {
                        new Exception(sCode + ":" + sMessage);
                    }

                    /*
                    OracleCommand oraCmd = new OracleCommand("PK_Inventory.RejectInventory");
                    oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    oraCmd.Parameters.Add(new OracleParameter("I_SERVICE_TYPE", OracleType.VarChar, 1)).Value = "1";
                    oraCmd.Parameters.Add(new OracleParameter("I_INV_TRAN_TYPE", OracleType.VarChar, 2)).Value = "RO";
                    oraCmd.Parameters.Add(new OracleParameter("I_PRODNO", OracleType.VarChar, 50)).Value = dr["PRODNO"].ToString();
                    oraCmd.Parameters.Add(new OracleParameter("I_STORE_NO", OracleType.VarChar, 50)).Value = dr["STORE_NO"].ToString();
                    oraCmd.Parameters.Add(new OracleParameter("I_STOCK_ID", OracleType.VarChar, 50)).Value = sLOC;
                    oraCmd.Parameters.Add(new OracleParameter("I_SHEET_NO", OracleType.VarChar, 50)).Value = dr["RTNNO"].ToString();
                    //oraCmd.Parameters.Add(new OracleParameter("I_INV_QTY", OracleType.Number, 20)).Value = Convert.ToInt32(dr["RTNQTY"].ToString());
                    oraCmd.Parameters.Add(new OracleParameter("I_INV_QTY", OracleType.Number, 20)).Value = -Convert.ToInt32(dsRTND.Tables["RTND_UP"].Rows[i]["RTNQTY"].ToString());
                    oraCmd.Parameters.Add(new OracleParameter("I_USER", OracleType.VarChar, 20)).Value = sUser;
                    oraCmd.Parameters.Add(new OracleParameter("O_RT_CODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                    oraCmd.Parameters.Add(new OracleParameter("O_RT_MESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                    oraCmd.Connection = objConn;
                    oraCmd.Transaction = objTX;
                    oraCmd.ExecuteNonQuery();

                    string sCode = oraCmd.Parameters["O_RT_CODE"].Value.ToString();
                    string sMsg = oraCmd.Parameters["O_RT_MESSAGE"].Value.ToString();
                    if (sCode.CompareTo("000") != 0)
                    {
                        new Exception(sCode + ":" + sMsg);
                    }

                    */

                    string strImeiSql = "";

                    strImeiSql = "SELECT IMEI  ";
                    strImeiSql += " FROM RTND_IMEI WHERE RTND_UP_ID = " + OracleDBUtil.SqlStr(dsRTND.Tables["RTND_UP"].Rows[i]["RTND_UP_ID"].ToString());

                    DataSet ImeiDs = OracleDBUtil.GetDataSet(objTX, strImeiSql);


                    if (ImeiDs.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow ImeiDr in ImeiDs.Tables[0].Rows)
                        {
                            string Code = "";
                            string Message = "";
                            Call_SP_IMEISTORERTN(objTX, ImeiDr["IMEI"].ToString(), dr["STORE_NO"].ToString(), "RETAIL"
                                                         , " ", " ", sUser, ref Code, ref Message);

                            if (Code == "999") throw new Exception(Code + ":" + Message);

                        }

                    }
                }
                //sRet = oraCmd.Parameters["outMESSAGE"].Value.ToString();
                //objTX.Rollback();
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
        /// 變更IMEI狀態 (退倉)
        /// </summary>
        /// <param name="objTX"></param>
        /// <param name="inIMEI">IMEI</param>
        /// <param name="inIVRCODE"></param>
        /// <param name="inChannel_ID"></param>
        /// <param name="inSTATUS"></param>
        /// <param name="inPOS_MASTER_UUID"></param>
        /// <param name="inFUNC_ID"></param>
        /// <param name="inHOST_ID"></param>
        /// <param name="inUSER_ID">人員</param>
        /// <param name="outMSGCODE"></param>
        /// <param name="outMESSAGE"></param>
        private void Call_SP_IMEISTORERTN(OracleTransaction objTX, string inIMEI, string inIVRCODE, string inChannel_ID,
             string inFUNC_ID, string inHOST_ID, string inUSER_ID, ref string outMSGCODE, ref string outMESSAGE)
        {
            OracleCommand oraCmd = null;

            try
            {
                oraCmd = new OracleCommand("PK_IMEI.SP_IMEISTORERTN");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("inIMEI", inIMEI));
                oraCmd.Parameters.Add(new OracleParameter("inIVRCODE", inIVRCODE));
                oraCmd.Parameters.Add(new OracleParameter("inChannel_ID", inChannel_ID));
                //oraCmd.Parameters.Add(new OracleParameter("inSTATUS", inSTATUS));
                //oraCmd.Parameters.Add(new OracleParameter("inPOS_MASTER_UUID", inPOS_MASTER_UUID));
                oraCmd.Parameters.Add(new OracleParameter("inFUNC_ID", inFUNC_ID));
                oraCmd.Parameters.Add(new OracleParameter("inHOST_ID", inHOST_ID));
                oraCmd.Parameters.Add(new OracleParameter("inUSER_ID", inUSER_ID));
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

        public void UpdateOne_RTNM(INV06_RTNM ds)
        {
            OracleDBUtil.UPDDATEByUUID(ds.Tables["RTNM"], "RTNN_ID");
        }

        /// <summary>
        /// 查詢退倉單明細資料(INV06用)
        /// </summary>
        /// <param name="STORENO">門市編號</param>
        /// <param name="RTNNO">退倉單號</param>
        /// <returns>DataTable</returns>
        public DataTable QueryRTNDDetailData(string STORENO, string RTNNID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("  select RTNM.RTNN_ID,RTND_PROD.PRODNO,PRODNAME,RTND_PROD.RTND_PROD_ID,RTND_STORE.RTND_STORE_ID,OPENQTY,UNOPENQTY,nvl(RTNQTY,0) RTNQTY,ENDQTY,RTNM.STATUS ");
            sb.Append(", nvl(INV_ON_HAND_CURRENT.ON_HAND_QTY,0) ON_HAND_QTY ");
            sb.Append(", (SELECT RTND_IMEI.IMEI FROM RTND_IMEI WHERE RTND_UP.RTND_UP_ID = RTND_IMEI.RTND_UP_ID AND ROWNUM = 1) IMEI ");
            sb.Append(", (SELECT COUNT(RTND_IMEI.IMEI) FROM RTND_IMEI WHERE RTND_UP.RTND_UP_ID = RTND_IMEI.RTND_UP_ID) AS IMEI_QTY ");
            sb.Append(",  IMEI_FLAG ");
            sb.Append("         ,(select AFTER_PROCESS.DESCRIPTION from AFTER_PROCESS where AFTER_PROCESS.after_process_code=RTNM.AFTER_PROCESS_CODE and rownum=1) as DESCRIPTION ");
            sb.Append("         ,(select  RETURN_DESCRIPTION from RETURN_REASON where RETURN_REASON.RETURN_REASON_CODE = RTNM.RETURN_REASON_CODE and rownum=1) as RETURN_DESCRIPTION ");
            sb.Append("         ,to_char(RTNM.MODI_DTM,'YYYY/MM/DD hh24:mi:ss') as MODI_DTM ");
            sb.Append("         ,RTNM.MODI_USER as MODI_USER ");
            sb.Append("         ,to_char(TO_DATE(RTND_UP.RTNDATE,'YYYY/MM/DD'),'YYYY/MM/DD') as RTNDATE ");
            sb.Append("         ,RTNM.RTNNO as RTNNO");
            sb.Append("         ,to_char(RTNM.B_DATE,'YYYY/MM/DD') as B_DATE");
            sb.Append("         ,to_char(RTNM.E_DATE,'YYYY/MM/DD') as E_DATE");
            sb.Append("         ,RTNM.REMARK as REMARK ");
            sb.Append("         ,RTND_STORE.STORE_NO as STORE_NO ");
            sb.Append("         , nvl(RTND_IMEI.RTND_UP_ID,POS_UUID) as RTND_UP_ID ");
            sb.Append("         , (nvl(INV_ON_HAND_CURRENT.ON_HAND_QTY,0)-nvl(RTNQTY,0)) as DIFF "); //差異量=帳上庫存量-退倉量
            sb.Append("         , RTND_UP.ERPRTN_DATE ");
            sb.Append("         , RTND_UP.ERPRTN_NO ");
            sb.Append("         , RTND_UP.ERPRTN_QTY ");
            sb.Append("  from RTNM   ");
            sb.Append("  INNER join RTND_PROD on RTNM.RTNN_ID = RTND_PROD.RTNN_ID  ");
            sb.Append("  INNER join PRODUCT on RTND_PROD.PRODNO = PRODUCT.PRODNO ");
            sb.Append("  INNER join RTND_STORE on RTNM.RTNN_ID = RTND_STORE.RTNN_ID    ");
            sb.Append("  left join INV_ON_HAND_CURRENT on RTND_STORE.STORE_NO = INV_ON_HAND_CURRENT.STORE_NO and INV_ON_HAND_CURRENT.PRODNO = PRODUCT.PRODNO ");
            sb.Append("  INNER join RTND_UP on RTND_PROD.RTND_PROD_ID = RTND_UP.RTND_PROD_ID and RTND_STORE.RTND_STORE_ID = RTND_UP.RTND_STORE_ID ");
            sb.Append(" Left Join RTND_IMEI on RTND_UP.RTND_UP_ID = RTND_IMEI.RTND_UP_ID  ");
            sb.Append("WHERE 1 = 1 and INV_ON_HAND_CURRENT.STOCK_ID=INV_GOODLOCUUID  ");

            if (!string.IsNullOrEmpty(STORENO))
            {
                sb.Append(" AND RTND_STORE.STORE_NO = " + OracleDBUtil.SqlStr(STORENO));
            }
            if (!string.IsNullOrEmpty(RTNNID))
            {
                sb.Append(" AND RTNM.RTNN_ID = " + OracleDBUtil.SqlStr(RTNNID));
            }
            //sb.Append("SELECT RTNM.RTNNO,Product.PRODNO,Product.PRODNAME, ");
            //sb.Append("       RTNM.B_DATE, ");
            //sb.Append("       ON_HAND_QTY, ");
            //sb.Append("       RTNM.E_DATE, ");
            //sb.Append("       RTNM.MODI_USER, ");
            //sb.Append("       RTNM.MODI_DTM, ");
            //sb.Append("       RTNM.STATUS,(case when RTND_UP.RTNDATE is null then RTND_UP.RTNDATE else (substr(RTND_UP.RTNDATE,1,4) || '/' || substr(RTND_UP.RTNDATE,5,2) || '/' || substr(RTND_UP.RTNDATE,7,2) ) end) as RTNDATE,RTNM.STATUS,  ");
            //sb.Append("       RTNM.RTNN_ID  as RTNN_ID ");
            //sb.Append(" FROM RTNM,RTND_PROD, RTND_STORE,Product,inv_on_Hand_Current,RTND_UP  ");
            //sb.Append(" WHERE RTNM.RTNN_ID = RTND_PROD.RTNN_ID(+) AND RTNM.RTNN_ID = RTND_STORE.RTNN_ID(+) ");
            //sb.Append(" AND RTND_PROD.PRODNO = PRODuct.PRODNO(+) AND RTND_PROD.PRODNO = inv_on_Hand_Current.PRODNO(+) AND RTND_STORE.STORE_NO = inv_on_Hand_Current.STORE_NO(+) ");
            //sb.Append(" AND RTND_PROD.RTND_PROD_ID = RTND_UP.RTND_PROD_ID(+) AND RTND_STORE.RTND_STORE_ID = RTND_UP.RTND_STORE_ID(+) ");

            //sb.Append("SELECT RTNM.RTNNO,Product.PRODNO,Product.PRODNAME, ");
            //sb.Append("       RTNM.B_DATE, ");
            //sb.Append("       ON_HAND_QTY, ");
            //sb.Append("       RTNM.E_DATE, ");
            //sb.Append("       RTNM.MODI_USER, ");
            //sb.Append("       RTNM.MODI_DTM, ");
            //sb.Append("       RTNM.STATUS,( case when RTND_UP.RTNDATE is null then RTND_UP.RTNDATE else (substr(RTND_UP.RTNDATE,1,4) || '/' || substr(RTND_UP.RTNDATE,5,2) || '/' || substr(RTND_UP.RTNDATE,7,2) ) end) as RTNDATE,RTNM.STATUS,  ");
            //sb.Append("       RTNM.RTNN_ID  as RTNN_ID ");
            //sb.Append("from RTNM left JOin RTND_PROD on RTNM.RTNN_ID = RTND_PROD.RTNN_ID left join RTND_STORE on RTNM.RTNN_ID = RTND_STORE.RTNN_ID left join Product  on RTND_PROD.PRODNO = PRODuct.PRODNO ");
            //sb.Append("left join inv_on_Hand_Current on RTND_PROD.PRODNO = inv_on_Hand_Current.PRODNO and RTND_STORE.STORE_NO = inv_on_Hand_Current.STORE_NO ");
            //sb.Append("left join RTND_UP on RTND_PROD.RTND_PROD_ID = RTND_UP.RTND_PROD_ID and RTND_STORE.RTND_STORE_ID = RTND_UP.RTND_STORE_ID   ");
            //sb.Append("WHERE 1 = 1 and INV_ON_HAND_CURRENT.STOCK_ID=INV_GOODLOCUUID ");

            //if (!string.IsNullOrEmpty(STORENO))
            //{
            //    sb.Append(" AND RTND_STORE.STORE_NO = " + OracleDBUtil.SqlStr(STORENO));
            //}
            //if (!string.IsNullOrEmpty(S_DATE) && !string.IsNullOrEmpty(E_DATE))
            //{
            //    sb.Append(" AND RTNM.B_DATE >=" + OracleDBUtil.TimeStr(S_DATE));
            //    sb.Append(" AND RTNM.E_DATE <= " + OracleDBUtil.TimeStr(E_DATE));
            //}
            //if (!string.IsNullOrEmpty(S_DATE) && string.IsNullOrEmpty(E_DATE))
            //{
            //    sb.Append(" AND RTNM.B_DATE >= " + OracleDBUtil.TimeStr(S_DATE));
            //}
            //if (string.IsNullOrEmpty(S_DATE) && !string.IsNullOrEmpty(E_DATE))
            //{
            //    sb.Append(" AND RTNM.E_DATE <= " + OracleDBUtil.TimeStr(E_DATE));
            //}
            //if (!string.IsNullOrEmpty(RTNNO))
            //{
            //    sb.Append(" AND RTNNO = " + OracleDBUtil.SqlStr(RTNNO));
            //}
            //if (!string.IsNullOrEmpty(PRODNO))
            //{
            //    sb.Append(" AND RTND_PROD.PRODNO = " + OracleDBUtil.SqlStr(PRODNO));
            //}
            //if (!string.IsNullOrEmpty(STATUS))
            //{
            //    sb.Append(" AND RTNM.STATUS = " + OracleDBUtil.SqlStr(STATUS));
            //}

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得列印退倉單明細的資料
        /// </summary>
        /// <param name="STORENO">門市編號</param>
        /// <param name="RTNNO">退倉單號</param>
        /// <returns>DataTable</returns>
        public DataTable Query_PrintRTNDDetailData(string STORENO, string RTNNID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT  rownum AS 項次,Data1.* From ");

            sb.Append("(SELECT DISTINCT RTND_PROD.PRODNO AS 商品料號 ");
            sb.Append("      ,PRODNAME AS 商品名稱 ");
            sb.Append("      ,nvl(INV_ON_HAND_CURRENT.ON_HAND_QTY,0) AS 帳上庫存量 ");
            sb.Append("      ,UNOPENQTY AS 未拆封數量 ");
            sb.Append("      ,OPENQTY AS 已拆封數量 ");
            sb.Append("      ,nvl(RTNQTY,0) AS 退倉數量 ");
            sb.Append("      ,(FN_RTND_IMEI(RTND_UP.RTND_UP_ID)) AS IMEI ");
            sb.Append("      , (nvl(INV_ON_HAND_CURRENT.ON_HAND_QTY,0)-nvl(RTNQTY,0)) as 差異量 "); //差異量=帳上庫存量-退倉量                
            sb.Append("  from RTNM   ");
            sb.Append("  INNER join RTND_PROD on RTNM.RTNN_ID = RTND_PROD.RTNN_ID  ");
            sb.Append("  INNER join PRODUCT on RTND_PROD.PRODNO = PRODUCT.PRODNO ");
            sb.Append("  INNER join RTND_STORE on RTNM.RTNN_ID = RTND_STORE.RTNN_ID    ");
            sb.Append("  left join INV_ON_HAND_CURRENT on RTND_STORE.STORE_NO = INV_ON_HAND_CURRENT.STORE_NO and INV_ON_HAND_CURRENT.PRODNO = PRODUCT.PRODNO ");
            sb.Append("  INNER join RTND_UP on RTND_PROD.RTND_PROD_ID = RTND_UP.RTND_PROD_ID and RTND_STORE.RTND_STORE_ID = RTND_UP.RTND_STORE_ID ");
            sb.Append(" Left Join RTND_IMEI on RTND_UP.RTND_UP_ID = RTND_IMEI.RTND_UP_ID  ");
            sb.Append("WHERE 1 = 1 and INV_ON_HAND_CURRENT.STOCK_ID=INV_GOODLOCUUID  ");

            if (!string.IsNullOrEmpty(STORENO))
            {
                sb.Append(" AND RTND_STORE.STORE_NO = " + OracleDBUtil.SqlStr(STORENO));
            }
            if (!string.IsNullOrEmpty(RTNNID))
            {
                sb.Append(" AND RTNM.RTNN_ID = " + OracleDBUtil.SqlStr(RTNNID));
            }
            sb.Append(") Data1  ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

    }


}
