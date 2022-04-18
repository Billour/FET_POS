<%@ WebHandler Language="C#" Class="AjaxQuery" %>


using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState;
using Advtek.Utility;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;

public class AjaxQuery : IHttpHandler, IRequiresSessionState
{

    private string Conditions = " AND T1.COMPANYCODE = '01' AND SYSDATE >= NVL(T1.S_DATE, SYSDATE) AND SYSDATE <= NVL(T1.E_DATE, SYSDATE) AND T1.DEL_FLAG='N'";

    public void ProcessRequest(HttpContext context)
    {
        if (string.IsNullOrEmpty(context.Request["mode"]))
        {
            OutputData(context, string.Empty);
        }
        else
        {
            string mode = context.Request["mode"];
            switch (mode.ToUpper())
            {
                case "PROD_INFO":
                    // 以 ProdNo(料號)取得產品資訊
                    if (string.IsNullOrEmpty(context.Request["prodno"]) || string.IsNullOrEmpty(context.Request["storeno"]))
                        OutputData(context, string.Empty);
                    else
                        OutputData(context, GetJsonData(GetProduct(context.Request["prodno"], context.Request["storeno"])));
                    break;
                case "PRODS":
                    // 以多筆 ProdNo(料號)取得產品資訊, 以分號作區隔
                    if (string.IsNullOrEmpty(context.Request["prodno"]) || string.IsNullOrEmpty(context.Request["storeno"]))
                        OutputData(context, string.Empty);
                    else
                        OutputData(context, GetJsonData(GetProducts(context.Request["prodno"], context.Request["storeno"])));
                    break;
                case "QUANTITY":
                    // 以 StoreNo(門市編號), ProdNo(料號)取得庫存數量
                    if (string.IsNullOrEmpty(context.Request["storeno"]) || string.IsNullOrEmpty(context.Request["prodno"]))
                        OutputData(context, string.Empty);
                    else
                        OutputData(context, GetStockProdQty(context.Request["storeno"], context.Request["prodno"]));
                    break;
                case "IMEI":
                    if (string.IsNullOrEmpty(context.Request["prodno"]) || string.IsNullOrEmpty(context.Request["imei"]) || string.IsNullOrEmpty(context.Request["did"]))
                        OutputData(context, string.Empty);
                    else
                        OutputData(context, CheckIMEI(context.Request["did"], context.Request["imei"], context.Request["prodno"]));
                    break;
                case "CACHE":
                    if (string.IsNullOrEmpty(context.Request["masterid"]))
                        OutputData(context, string.Empty);
                    else
                        OutputData(context, GetCache(context.Request["masterid"]));
                    break;
                case "DEL_ITEM":
                    if (string.IsNullOrEmpty(context.Request["masterid"]) || string.IsNullOrEmpty(context.Request["cacheid"]))
                        OutputData(context, string.Empty);
                    else
                        OutputData(context, DeleteCacheItem(context.Request["masterid"], context.Request["cacheid"]));
                    break;
                case "DEL_IMEI":
                    if (string.IsNullOrEmpty(context.Request["did"]) || string.IsNullOrEmpty(context.Request["imeis"]))
                        OutputData(context, string.Empty);
                    else
                        OutputData(context, DeleteIMEICache(context.Request["did"], context.Request["imeis"]));
                    break;
                case "TO_CLOSE":
                    if (string.IsNullOrEmpty(context.Request["cid"]))
                        OutputData(context, string.Empty);
                    else
                        OutputData(context, GetTO_CLOSE(context.Request["cid"]));
                    break;
                case "ADD_PAID":
                    if (string.IsNullOrEmpty(context.Request["data"]) || string.IsNullOrEmpty(context.Request["masterid"]))
                        OutputData(context, string.Empty);
                    else
                        OutputData(context, SavePaidCache(context.Request["data"], context.Request["masterid"]));
                    break;
                case "DEL_PAID":
                    if (string.IsNullOrEmpty(context.Request["pid"]) || string.IsNullOrEmpty(context.Request["masterid"]))
                        OutputData(context, string.Empty);
                    else
                        OutputData(context, DeletePaidCache(context.Request["pid"], context.Request["masterid"]));
                    break;
                case "COMPANY_ID":
                    if (string.IsNullOrEmpty(context.Request["cid"]))
                        OutputData(context, string.Empty);
                    else
                        OutputData(context, CheckCompanyId(context.Request["cid"]));
                    break;
                case "ETC":
                    OutputData(context, GetJsonData(GetETCInfo()));
                    break;
                case "PROMO_PROD":
                    if (string.IsNullOrEmpty(context.Request["pids"]) || string.IsNullOrEmpty(context.Request["pmno"]) || string.IsNullOrEmpty(context.Request["did"]))
                        OutputData(context, string.Empty);
                    else
                        OutputData(context, GetPromotionProducts(context.Request["pmno"], context.Request["did"], context.Request["pids"]));
                    break;
                case "SALE_HISTORY":
                    if (string.IsNullOrEmpty(context.Request["masterid"]))
                        OutputData(context, string.Empty);
                    else
                        OutputData(context, GetHistory(context.Request["masterid"], context.Request["printname"], context.Request["receiptname"]));
                    break;
                //case "DISCOUNT_NAME":

                //讀取加價購商品
                case "GET_ADD_PROD":
                    if (string.IsNullOrEmpty(context.Request["prodno"]))
                        OutputData(context, string.Empty);
                    else
                        OutputData(context, Get_Add_Prods(context.Request["did"], context.Request["prodno"]));
                    break;
                case "GET_ADD_PROD_INFO":
                    if (string.IsNullOrEmpty(context.Request["prodno"]) || string.IsNullOrEmpty(context.Request["discode"]))
                        OutputData(context, string.Empty);
                    else
                        OutputData(context, Get_Add_Prods_Info(context.Request["prodno"], context.Request["discode"]));
                    break;
                case "GET_HP_ADD_PROD":
                    if (string.IsNullOrEmpty(context.Request["prodno"]) || string.IsNullOrEmpty(context.Request["discode"]))
                        OutputData(context, string.Empty);
                    else
                        OutputData(context, Get_Add_Prods_Info(context.Request["prodno"], context.Request["discode"]));
                    break;
                case "UNLOCK_IMEI":
                    if (string.IsNullOrEmpty(context.Request["prodno"]) || string.IsNullOrEmpty(context.Request["imei"]) || string.IsNullOrEmpty(context.Request["did"]))
                        OutputData(context, string.Empty);
                    else
                        OutputData(context, unlock_imei(context.Request["imei"], context.Request["prodno"], (context.Request["did"])));
                    break;
                default:
                    OutputData(context, string.Empty);
                    break;
            }
        }
    }

    public bool IsReusable { get { return false; } }

    #region Private Method : void OutputData(HttpContext context, string data)
    private void OutputData(HttpContext context, string data)
    {
        context.Response.Cache.SetCacheability(HttpCacheability.Public);
        context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        context.Response.Expires = 0;
        context.Response.Cache.SetValidUntilExpires(true);
        context.Response.BufferOutput = false;
        context.Response.ContentType = "text/plain";
        context.Response.Write(data);
    }
    #endregion

    private DataTable GetProduct(string prodNo, string storeNo)
    {
        string stock = Common_PageHelper.GetGoodLOCUUID();
        StringBuilder sb = new StringBuilder();
        sb.Append("Select T1.PRODNO, T1.PRODNAME, T1.UNIT, T1.CEASEDATE, T1.ISKEY, T1.ISSTOCK, T1.ISCONSIGNMENT, T1.PRICE, T1.ULSNO,T1.PRICE as ORI_UNIT_PRICE, ");
        sb.Append("T1.STATUS, T1.IMEI_FLAG, T1.PRODTYPENO, T1.EFFECT_DATE, T1.IS_POS_DEF_PRICE, T1.IS_OPEN_PRICE, T1.IS_DROPSHIPMENT, T1.IS_DISCOUNT, ");
        sb.Append("T1.SUPP_ID, T1.COMPANYCODE, T1.DEL_FLAG, T1.TAXABLE, T1.TAXRATE, T1.DS_FLAG, T1.INV_TYPE, ");
        sb.Append("POS_UUID() as ID, '1' AS QUANTITY,''as POSUUID_DETAIL,'1' as ITEM_TYPE, '' AS PROMOTION_CODE, '' AS BARCODE1, '11' AS SOURCE_TYPE, ");
        sb.Append(" '' as PROMOTION_NAME,");
        sb.Append(" '' as SIM_CARD_NO, ");
        sb.Append(" '' as SH_DISCOUNT_REASON, '' as SH_DISCOUNT_DESC, GET_DISCOUNT_TYPE(T1.PRODNO) AS DISCOUNT_TYPE, ");
        //sb.Append("nvl((select nvl(SUM(ON_HAND_QTY),'0') ");
        //sb.AppendFormat(" FROM INV_ON_HAND_CURRENT where PRODNO = T1.PRODNO and STORE_NO='{0}' and STOCK_ID = '{1}'),'0') as  ON_HAND_QTY  ", storeNo, stock);
        sb.AppendFormat(" INV_OnHandQty(T1.PRODNO,'{0}') as ON_HAND_QTY ", storeNo);
        sb.Append(" FROM   PRODUCT T1  ");
        sb.Append(" WHERE T1.PRODNO not in (select PRODNO from NOT_IN_SALE_PROD where STATUS = 0) ");
        sb.Append(Conditions);
        sb.Append(" AND T1.PRODNO = " + OracleDBUtil.SqlStr(prodNo));

        return OracleDBUtil.Query_Data(StringUtil.CStr(sb));
    }

    private DataTable GetProducts(string prodNo, string storeNo)
    {
        string stock = Common_PageHelper.GetGoodLOCUUID();
        StringBuilder sb = new StringBuilder();
        string nos = "'" + prodNo.Replace(";", "','") + "'";
        sb.Append("Select T1.PRODNO, T1.PRODNAME, T1.UNIT, T1.CEASEDATE, T1.ISKEY, T1.ISSTOCK, T1.ISCONSIGNMENT, T1.PRICE, T1.ULSNO,T1.PRICE as ORI_UNIT_PRICE, ");
        sb.Append("T1.STATUS, T1.IMEI_FLAG, T1.PRODTYPENO, T1.EFFECT_DATE, T1.IS_POS_DEF_PRICE, T1.IS_OPEN_PRICE, T1.IS_DROPSHIPMENT, T1.IS_DISCOUNT, ");
        sb.Append("T1.SUPP_ID, T1.COMPANYCODE, T1.DEL_FLAG, T1.TAXABLE, T1.TAXRATE, T1.DS_FLAG, T1.INV_TYPE, ");
        sb.Append("POS_UUID() as ID, '1' AS QUANTITY,''as POSUUID_DETAIL, '1' as ITEM_TYPE, '' AS PROMOTION_CODE, '' AS BARCODE1, '11' AS SOURCE_TYPE, ");
        sb.Append(" '' as PROMOTION_NAME,");
        sb.Append(" '' as SIM_CARD_NO, ");
        sb.Append(" '' as SH_DISCOUNT_REASON, '' as SH_DISCOUNT_DESC, ");
        //sb.Append("nvl((select nvl(SUM(ON_HAND_QTY),'0') ");
        //sb.AppendFormat(" FROM INV_ON_HAND_CURRENT where PRODNO = T1.PRODNO and STORE_NO='{0}' and STOCK_ID = '{1}'),'0') as  ON_HAND_QTY  ", storeNo, stock);
        sb.AppendFormat(" INV_OnHandQty(T1.PRODNO,'{0}') as ON_HAND_QTY ", storeNo);
        sb.Append(" FROM   PRODUCT T1  ");
        sb.Append(" WHERE  T1.PRODNO IN (" + nos + ") and T1.PRODNO not in (select PRODNO from NOT_IN_SALE_PROD where STATUS = 0)");

        return OracleDBUtil.Query_Data(StringUtil.CStr(sb));
    }

    public string Get_Add_Prods_Info(string prodNo, string discode)
    {
        string posuuid_detail = GuidNo.getUUID();
        string storeNo = this.logMsg.STORENO;
        string stock = Common_PageHelper.GetGoodLOCUUID();
        StringBuilder sb = new StringBuilder();
        sb.Append(" Select ");
        sb.Append(" T1.PRODNO,");
        sb.Append(" T1.PRODNAME,");
        sb.Append(" T1.UNIT,");
        sb.Append(" T1.CEASEDATE,");
        sb.Append(" T1.ISKEY,");
        sb.Append(" T1.ISSTOCK,");
        sb.Append(" T1.ISCONSIGNMENT,");
        sb.Append(" T1.PRICE,");
        sb.Append(" T1.ULSNO,");
        sb.Append(" T1.PRICE as ORI_UNIT_PRICE,");
        sb.Append(" T1.STATUS,");
        sb.Append(" T1.IMEI_FLAG,");
        sb.Append(" T1.PRODTYPENO,");
        sb.Append(" T1.EFFECT_DATE,");
        sb.Append(" T1.IS_POS_DEF_PRICE,");
        sb.Append(" T1.IS_OPEN_PRICE,");
        sb.Append(" T1.IS_DROPSHIPMENT,");
        sb.Append(" T1.IS_DISCOUNT,");
        sb.Append(" T1.SUPP_ID,");
        sb.Append(" T1.COMPANYCODE,");
        sb.Append(" T1.DEL_FLAG,");
        sb.Append(" T1.TAXABLE,");
        sb.Append(" T1.TAXRATE,");
        sb.Append(" T1.DS_FLAG,");
        sb.Append(" T1.INV_TYPE,");
        sb.Append(" POS_UUID() as ID,");
        sb.Append(" '1' AS QUANTITY,");
        sb.AppendFormat(" '{0}'as POSUUID_DETAIL,", posuuid_detail);
        sb.Append(" '7' as ITEM_TYPE,");
        sb.Append(" '' AS PROMOTION_CODE,");
        sb.Append(" '' AS BARCODE1,");
        sb.Append(" '11' AS SOURCE_TYPE,");
        sb.Append(" '' as PROMOTION_NAME,");
        sb.Append(" '' as SIM_CARD_NO, ");
        sb.Append(" '' as SH_DISCOUNT_REASON,");
        sb.Append(" '' as SH_DISCOUNT_DESC, ");
        sb.AppendFormat(" INV_OnHandQty(T1.PRODNO,'{0}') as ON_HAND_QTY ", storeNo);
        sb.Append(" FROM   PRODUCT T1  ");
        sb.Append(" WHERE NVL(T1.IS_DISCOUNT, 'N') = 'N'  ");
        sb.Append(Conditions);
        sb.Append(" AND T1.PRODNO = " + OracleDBUtil.SqlStr(prodNo));
        sb.Append(" UNION ");
        sb.Append(" Select ");
        sb.Append(" T2.DISCOUNT_CODE AS PRODNO,");
        sb.Append(" T2.DISCOUNT_NAME AS PRODNAME,");
        sb.Append(" T1.UNIT,");
        sb.Append(" T1.CEASEDATE,");
        sb.Append(" T1.ISKEY,");
        sb.Append(" T1.ISSTOCK,");
        sb.Append(" T1.ISCONSIGNMENT,");
        sb.Append(" T3.DIS_AMT * -1 AS PRICE,");
        sb.Append(" T1.ULSNO,");
        sb.Append(" T3.DIS_AMT * -1 as ORI_UNIT_PRICE,");
        sb.Append(" T1.STATUS,");
        sb.Append(" '1' AS IMEI_FLAG,");
        sb.Append(" T1.PRODTYPENO,");
        sb.Append(" T1.EFFECT_DATE,");
        sb.Append(" T1.IS_POS_DEF_PRICE,");
        sb.Append(" T1.IS_OPEN_PRICE,");
        sb.Append(" T1.IS_DROPSHIPMENT,");
        sb.Append(" T1.IS_DISCOUNT,");
        sb.Append(" T1.SUPP_ID,");
        sb.Append(" T1.COMPANYCODE,");
        sb.Append(" T1.DEL_FLAG,");
        sb.Append(" T1.TAXABLE,");
        sb.Append(" T1.TAXRATE,");
        sb.Append(" T1.DS_FLAG,");
        sb.Append(" '1' AS INV_TYPE,");
        sb.Append(" POS_UUID() as ID,");
        sb.Append(" '1' AS QUANTITY,");
        sb.AppendFormat(" '{0}'as POSUUID_DETAIL,", posuuid_detail);
        sb.Append(" '15' as ITEM_TYPE,");
        sb.Append(" '' AS PROMOTION_CODE,");
        sb.Append(" '' AS BARCODE1,");
        sb.Append(" '11' AS SOURCE_TYPE,");
        sb.Append(" '' as PROMOTION_NAME,");
        sb.Append(" '' as SIM_CARD_NO, ");
        sb.Append(" '' as SH_DISCOUNT_REASON,");
        sb.Append(" '' as SH_DISCOUNT_DESC, ");
        sb.AppendFormat(" INV_OnHandQty(T1.PRODNO,'{0}') as ON_HAND_QTY ", storeNo);
        sb.Append(" FROM PRODUCT T1 JOIN DISCOUNT_MASTER T2 ON T1.PRODNO=T2.DISCOUNT_CODE ");
        sb.Append(" JOIN ADD_IN_PROD_DISCOUNT T3 ON T2.DISCOUNT_MASTER_ID = T3.DISCOUNT_MASTER_ID ");
        sb.AppendFormat(" WHERE T2.DISCOUNT_CODE = {0} AND T3.PRODNO = {1}", OracleDBUtil.SqlStr(discode), OracleDBUtil.SqlStr(prodNo));
        DataTable dt = OracleDBUtil.Query_Data(StringUtil.CStr(sb));
        return this.GetJsonData(dt);
    }

    #region Private Method : string GetJsonData(DataTable dt)
    private string GetJsonData(DataTable dt)
    {
        if (dt == null)
            return "[]";
        StringBuilder sb = new StringBuilder("[");
        int idx = 0;
        foreach (DataRow dr in dt.Rows)
        {
            idx++;
            sb.Append("{");
            foreach (DataColumn dc in dt.Columns)
            {
                sb.AppendFormat("{0}:'{1}'", dc.ColumnName, dr[dc]);
                if (dc.Ordinal != dt.Columns.Count - 1)
                    sb.Append(",");
            }
            sb.Append("}");
            if (idx != dt.Rows.Count)
                sb.Append(",");
        }
        sb.Append("]");
        return StringUtil.CStr(sb);
    }
    #endregion

    #region Private Method : string GetStockProdQty(string storeNo, string prodNo)
    /// <summary>
    /// 取產品庫存量
    /// </summary>
    /// <param name="storeNo">門市編號</param>
    /// <param name="prodNo">產品料號</param>
    /// <returns>Json String, ex:{ QTY:'10', SAVE_QTY:'5'}; QTY:庫存量, SAVE_QTY:安全庫存量</returns>
    private string GetStockProdQty(string storeNo, string prodNo)
    {
        string stock_id = Common_PageHelper.GetGoodLOCUUID();
        List<string> list = TSAL01_Facade.get_on_hand_qty(stock_id, prodNo, this.logMsg.STORENO);
        string return_str = "{ QTY:'{0}', SAVE_QTY:'{1}'}";
        if (list.Count == 0)
            return_str = string.Format(return_str, "0", "0");
        else
            return_str = string.Format(return_str, list[0], list[1]);

        return return_str;
    }
    #endregion

    #region Private Method : string CheckIMEI(string detailid, string imei, string PRODNO)
    /// <summary>
    /// 檢查 IMEI 是否可用, 如果可用, 則儲存至 SALE_IMEI_LOG
    /// </summary>
    /// <param name="detailid">SALE_DETAIL.ID</param>
    /// <param name="imei">IMEI</param>
    /// <param name="PRODNO">商品料號</param>
    /// <returns>Json 回應, { IMEI:'IMEI NO', MESSAGE:'Error Message or empty'</returns>
    private string CheckIMEI(string detailid, string imei, string PRODNO)
    {
        IMEI_Facade imei_facade = new IMEI_Facade();

        //判斷是否有捨棄的IMEI
        bool haveGarbageIMEIRec = TSAL01_Facade.haveGarbageIMEIRec(imei);
        if (haveGarbageIMEIRec)
        {
            //刪除IMEI
            int ret = imei_facade.DeleteINV_IMEI("SALE_IMEI_LOG", imei);
            if (ret < 0)
                return string.Format("{{ RESULT:'0',IMEI:'{0}',MESSAGE:'{1}'}}", imei, "資料庫出問題!");
        }

        string SALE_DATAIL_ID = TSAL01_Facade.CheckINV_IMEI(imei, detailid);
        if (SALE_DATAIL_ID != detailid)
        {
            if (!string.IsNullOrEmpty(SALE_DATAIL_ID))
            {
                //判斷是否已經進入SALE_DETAIL
                string old_detail_id = TSAL01_Facade.CheckIMEI_Detail_ID(SALE_DATAIL_ID);
                if (string.IsNullOrEmpty(old_detail_id))
                {
                    DataTable dtEMIE = null;
                    string locId = FET.POS.Model.Common.Common_PageHelper.GetGoodLOCUUID();
                    //IMEI Flag 4 is just for log

                    dtEMIE = TSAL01_Facade.getApprove_AllowIMEI(logMsg.STORENO, locId, PRODNO, imei);
                    //需判別 IMEI可賣數量
                    if (dtEMIE != null && dtEMIE.Rows.Count > 0)
                    {
                        TSAL01_Facade.UpdateINV_IMEI(detailid, SALE_DATAIL_ID, imei, logMsg.MODI_USER);

                        return string.Format("{{ RESULT:'1',IMEI:'{0}',MESSAGE:''}}", imei);
                    }
                    else
                    {
                        return string.Format("{{ RESULT:'0',IMEI:'{0}',MESSAGE:'{1}'}}", imei, string.Format("此 IMEI({0}) 不允許使用", imei));
                    }
                }
                else
                {
                    return string.Format("{{ IMEI:'{0}',MESSAGE:'{1}'}}", imei, string.Format("此 IMEI({0}) 已銷售!", imei));
                }
            }
            else
            {
                DataTable dtEMIE = null;
                string locId = FET.POS.Model.Common.Common_PageHelper.GetGoodLOCUUID();
                //IMEI Flag 4 is just for log

                dtEMIE = TSAL01_Facade.getApprove_AllowIMEI(logMsg.STORENO, locId, PRODNO, imei);
                //需判別 IMEI可賣數量
                if (dtEMIE != null && dtEMIE.Rows.Count > 0)
                {
                    TSAL01_Facade.InsertINV_IMEI(detailid, PRODNO, imei, logMsg.MODI_USER);
                    return string.Format("{{ RESULT:'1',IMEI:'{0}',MESSAGE:''}}", imei);
                }
                else
                {
                    return string.Format("{{ RESULT:'0',IMEI:'{0}',MESSAGE:'{1}'}}", imei, string.Format("此 IMEI({0}) 不允許使用", imei));
                }
            }
        }
        else
        {
            //判斷是否為同一筆
            return string.Format("{{ RESULT:'1',IMEI:'{0}',MESSAGE:''}}", imei);

        }
    }
    #endregion

    private string GetCache(string masterId)
    {
        StringBuilder sb = null;
        try
        {
            string storeNo = this.logMsg.STORENO;
            string stock = Common_PageHelper.GetGoodLOCUUID();

            sb = new StringBuilder();
            sb.Append("SELECT TO_CHAR(S.TRADE_DATE, 'yyyy/mm/dd') AS TRADE_DATE ");
            sb.Append("FROM SALE_HEAD S ");
            sb.AppendFormat("WHERE S.POSUUID_MASTER={0}", OracleDBUtil.SqlStr(masterId));
            DataTable dtHead = OracleDBUtil.Query_Data(StringUtil.CStr(sb));

            sb = new StringBuilder();
            sb.Append("Select T1.PRODNO, T1.PRODNAME, T1.UNIT, T1.CEASEDATE, T1.ISKEY, T1.ISSTOCK, T1.ISCONSIGNMENT, T2.UNIT_PRICE AS PRICE, T1.ULSNO,T2.ORI_UNIT_PRICE, ");
            sb.Append("T1.STATUS, T1.IMEI_FLAG, T1.PRODTYPENO, T1.EFFECT_DATE, T1.IS_POS_DEF_PRICE, T1.IS_OPEN_PRICE, T1.IS_DROPSHIPMENT, T1.IS_DISCOUNT, ");
            sb.Append("T1.SUPP_ID, T1.COMPANYCODE, T1.DEL_FLAG, T1.TAXABLE, T1.TAXRATE, T1.DS_FLAG, T1.INV_TYPE, ");
            sb.Append("T2.ID, T2.QUANTITY, T2.POSUUID_DETAIL, T2.ITEM_TYPE, T2.PROMOTION_CODE, T2.BARCODE1,T2.BARCODE2,T2.BARCODE3,T2.BUNDLE_ID, T2.SOURCE_TYPE,");
            sb.Append(" T2.SERVICE_SYS_ID,T2.MSISDN,T2.SIM_CARD_NO, ");
            sb.Append("nvl((select PROMO_NAME from MM where PROMO_NO = T2.PROMOTION_CODE and PROMO_STATUS = 1),'') as PROMOTION_NAME, ");
            sb.AppendFormat("nvl((select nvl(ON_HAND_QTY,'0') FROM INV_ON_HAND_CURRENT where PRODNO = T1.PRODNO and STORE_NO='{0}' and STOCK_ID = '{1}'),'0') as ON_HAND_QTY,  ", storeNo, stock);
            sb.Append(" T2.TRANS_TYPE,T2.DATA,T2.VOICE,T2.R_RATE,T2.FUN_ID ");
            sb.Append("FROM   PRODUCT T1, SALE_DETAIL T2  ");
            sb.Append("WHERE  1=1  ");
            sb.Append(Conditions);
            sb.Append(" AND T1.PRODNO=T2.PRODNO AND T2.ITEM_TYPE != '5' AND T2.POSUUID_MASTER=" + OracleDBUtil.SqlStr(masterId));
            DataTable dtDetail = OracleDBUtil.Query_Data(StringUtil.CStr(sb));
            string posuuid_detail = "";
            foreach (DataRow row in dtDetail.Rows)
            {
                if (row["POSUUID_DETAIL"] != null && !string.IsNullOrEmpty(StringUtil.CStr(row["POSUUID_DETAIL"])))
                    posuuid_detail += string.Format("'{0}',", row["POSUUID_DETAIL"]);
            }
            posuuid_detail = posuuid_detail.TrimEnd(',');
            sb = new StringBuilder();
            sb.Append("{");
            sb.AppendFormat("Head:{0},", GetJsonData(dtHead).Trim().TrimStart('[').TrimEnd(']'));
            sb.AppendFormat("Products:{0},", GetJsonData(dtDetail));
            sb.AppendFormat("IMEIs:{0},", GetJsonData(GetMasterIMEI(masterId)));
            sb.AppendFormat("Discount:{0},", GetJsonData(getCatchDiscount(masterId)));
            sb.AppendFormat("PaidCache:{0}", GetJsonData(GetPaidCache(masterId)));
            sb.Append("}");
        }
        catch //(Exception ex)
        {
        }
        finally
        {
            OracleConnection.ClearAllPools();
        }
        return StringUtil.CStr(sb);
    }

    private string GetTO_CLOSE(string posuuid_detail)
    {
        string[] Keys = posuuid_detail.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

        string result = "";
        try
        {
            StringBuilder sb = new StringBuilder();
            string storeNo = this.logMsg.STORENO;
            string stock = Common_PageHelper.GetGoodLOCUUID();
            string key_string = "";
            foreach (string key in Keys)
                key_string += OracleDBUtil.SqlStr(key) + ",";
            key_string = key_string.TrimEnd(',');


            sb.Append("Select T2.SEQNO,T2.PRODNO, 'SIM' AS PRODNAME, 'SET' AS UNIT, T1.CEASEDATE, T1.ISKEY, '0' AS ISSTOCK, '0' AS ISCONSIGNMENT, NVL(T2.UNIT_PRICE,0) as PRICE, T1.ULSNO, NVL(T2.UNIT_PRICE,0) as ORI_UNIT_PRICE,'1' AS STATUS, '1' AS IMEI_FLAG, ");
            sb.Append("'20' AS PRODTYPENO, T1.EFFECT_DATE, T1.IS_POS_DEF_PRICE, 'N' AS IS_OPEN_PRICE, T1.IS_DROPSHIPMENT, T1.IS_DISCOUNT, T1.SUPP_ID, '01' AS COMPANYCODE, 'N' AS DEL_FLAG, T1.TAXABLE, T1.TAXRATE, ");
            sb.Append("T1.DS_FLAG, T1.INV_TYPE, POS_UUID() AS ID, T2.POSUUID_DETAIL, T2.QUANTITY, 2 As ITEM_TYPE, T2.PROMOTION_CODE, T2.BARCODE1,T2.BARCODE2,T2.BARCODE3,T3.BUNDLE_ID, T3.SERVICE_TYPE AS SOURCE_TYPE,T3.SERVICE_SYS_ID, ");
            sb.Append("T2.ID as SOURCE_REFERENCE_KEY, T2.MSISDN,T2.SIM_CARD_NO, nvl((select PROMO_NAME from MM where PROMO_NO = T2.PROMOTION_CODE and PROMO_STATUS = 1),'') as PROMOTION_NAME, ");
            sb.Append(" '' as SIM_CARD_NO, ");
            sb.Append(" '' as SH_DISCOUNT_REASON, '' as SH_DISCOUNT_DESC, ");
            sb.Append("1 as ON_HAND_QTY,T3.FUN_ID,T3.BILLING_ACCOUNT_ID,T3.SUBSCRIBE_NO,T3.HRS_NO, ");
            sb.Append(" T3.TRANS_TYPE,T3.DATA,T3.VOICE,T3.R_RATE ");
            sb.Append("FROM   PRODUCT T1 RIGHT JOIN  TO_CLOSE_ITEM T2 ON T1.PRODNO = T2.PRODNO JOIN TO_CLOSE_HEAD T3 ON T2.POSUUID_DETAIL = T3.POSUUID_DETAIL ");

            sb.Append("WHERE  T2.PRODNO='SIM' ");
            sb.AppendFormat("AND T2.POSUUID_DETAIL IN ({0})", key_string);
            sb.Append(" UNION ");

            sb.Append("Select T2.SEQNO,T1.PRODNO, T1.PRODNAME, T1.UNIT, T1.CEASEDATE, T1.ISKEY, T1.ISSTOCK, T1.ISCONSIGNMENT, NVL(T2.UNIT_PRICE,0) as PRICE, T1.ULSNO, NVL(T2.UNIT_PRICE,0) as ORI_UNIT_PRICE, ");
            sb.Append("T1.STATUS, T1.IMEI_FLAG, T1.PRODTYPENO, T1.EFFECT_DATE, T1.IS_POS_DEF_PRICE, T1.IS_OPEN_PRICE, T1.IS_DROPSHIPMENT, T1.IS_DISCOUNT, ");
            sb.Append("T1.SUPP_ID, T1.COMPANYCODE, T1.DEL_FLAG, T1.TAXABLE, T1.TAXRATE, T1.DS_FLAG, T1.INV_TYPE, ");
            sb.Append("POS_UUID() AS ID, T2.POSUUID_DETAIL, T2.QUANTITY, 2 As ITEM_TYPE, T2.PROMOTION_CODE, T2.BARCODE1,T2.BARCODE2,T2.BARCODE3,T3.BUNDLE_ID, T3.SERVICE_TYPE AS SOURCE_TYPE,T3.SERVICE_SYS_ID, ");
            sb.Append("T2.ID as SOURCE_REFERENCE_KEY, T2.MSISDN,T2.SIM_CARD_NO,");
            sb.Append("nvl((select PROMO_NAME from MM where PROMO_NO = T2.PROMOTION_CODE and PROMO_STATUS = 1),'') as PROMOTION_NAME, ");
            sb.Append(" '' as SIM_CARD_NO, ");
            sb.Append(" '' as SH_DISCOUNT_REASON, '' as SH_DISCOUNT_DESC, ");
            sb.Append("nvl((select nvl(SUM(ON_HAND_QTY),'0') ");
            sb.AppendFormat(" FROM INV_ON_HAND_CURRENT where PRODNO = T1.PRODNO and STORE_NO='{0}' and STOCK_ID = '{1}'),'0') as ON_HAND_QTY,T3.FUN_ID,T3.BILLING_ACCOUNT_ID,T3.SUBSCRIBE_NO,T3.HRS_NO,  ", storeNo, stock);
            sb.Append(" T3.TRANS_TYPE,T3.DATA,T3.VOICE,T3.R_RATE ");
            sb.Append("FROM   PRODUCT T1  JOIN  TO_CLOSE_ITEM T2 ON T1.PRODNO = T2.PRODNO JOIN TO_CLOSE_HEAD T3 ON T2.POSUUID_DETAIL = T3.POSUUID_DETAIL ");
            sb.Append("WHERE  1=1 ");
            sb.Append(Conditions);
            sb.AppendFormat(" AND T2.POSUUID_DETAIL IN ({0}) ", key_string);

            DataTable dt = OracleDBUtil.Query_Data(StringUtil.CStr(sb));
            DataView view = dt.DefaultView;
            view.Sort = "SEQNO";
            dt = view.ToTable();
            string LOCK = "0";
            string IS_CANCEL = "1";
            foreach (DataRow row in dt.Rows)
            {
                if (LOCK == "0")
                {
                    if (StringUtil.CStr(row["FUN_ID"]) == "180")
                    {
                        LOCK = "1";
                    }
                }

                if (IS_CANCEL == "1")
                {
                    if (StringUtil.CStr(row["SOURCE_TYPE"]) == "5")
                    {
                        IS_CANCEL = "0";
                    }
                }

            }



            //讀取折扣
            sb = new StringBuilder();
            sb.Append("{");
            sb.AppendFormat("Products:{0},", GetJsonData(dt));
            sb.AppendFormat("LOCK:'{0}',", LOCK);
            sb.AppendFormat("IS_CANCEL:'{0}',", IS_CANCEL);
            sb.AppendFormat("IMEIs:[],");
            sb.AppendFormat("Discount:{0}", GetJsonData(this.getDiscount(key_string)));
            sb.Append("}");
            result = StringUtil.CStr(sb);
        }
        catch (Exception ex)
        {
            string e = ex.Message;
        }
        finally
        {
            OracleConnection.ClearAllPools();
        }
        return result;
    }

    private DataTable getDiscount(string POSUUID_DETAIL)
    {
        if (POSUUID_DETAIL.Length == 0)
            return null;
        DataTable dt = new DataTable();
        if (!string.IsNullOrEmpty(POSUUID_DETAIL))
        {
            OracleConnection conn = null;


            string sqlstr = "";
            try
            {
                conn = OracleDBUtil.GetConnection();
                //判斷是否為代收
                string sale_type = TSAL01_Facade.get_sale_type(POSUUID_DETAIL.TrimStart('\'').TrimEnd('\''));
                if (sale_type == "1")
                {
                    //判斷

                    sqlstr = string.Format("select T1.*, T2.PRODNAME AS DISCOUNT_NAME,'1' as QUANTITY from TO_CLOSE_DISCOUNT T1, product T2 where T1.posuuid_detail in ({0}) And T1.discount_id=T2.prodno order by  T1.SEQNO", POSUUID_DETAIL);

                    OracleDataAdapter da = new OracleDataAdapter(sqlstr, conn);
                    da.Fill(dt);
                    //如果沒資料，在抓一次Discount
                    if (dt.Rows.Count == 0)
                    {
                        string[] detailId_list = POSUUID_DETAIL.Replace("'", "").Split(new char[] { ',', ';' });
                        foreach (string detailId in detailId_list)
                        {
                            Discount_Facade.CreateDiscount(detailId);
                        }

                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                OracleConnection.ClearAllPools();
            }
        }
        return dt;
    }

    private DataTable getCatchDiscount(string POSUUID_MASTER)
    {
        if (POSUUID_MASTER.Length == 0)
            return null;
        DataTable dt = new DataTable();
        if (!string.IsNullOrEmpty(POSUUID_MASTER))
        {
            OracleConnection conn = null;
            string sqlstr = string.Format("select T1.SOURCE_REFERENCE_KEY as ID,rownum as SEQNO,T1.PRODNO AS DISCOUNT_ID, T2.PRODNAME AS DISCOUNT_NAME,'1' as QUANTITY,T1.UNIT_PRICE as DISCOUNT_PRICE ,T1.UNIT_PRICE as DISCOUNT_AMOUNT from SALE_DETAIL T1, product T2 where T1.posuuid_master = {0} And T1.prodno = T2.prodno and T1.item_type='5'  order by T1.POSUUID_DETAIL, T1.SEQNO", OracleDBUtil.SqlStr(POSUUID_MASTER));
            try
            {
                conn = OracleDBUtil.GetConnection();
                OracleDataAdapter da = new OracleDataAdapter(sqlstr, conn);
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                OracleConnection.ClearAllPools();
            }
        }
        return dt;
    }

    private DataTable GetMasterIMEI(string masterId)
    {
        DataTable dt = new DataTable();
        OracleConnection conn = null;
        //OracleTransaction trans = null;
        try
        {
            conn = OracleDBUtil.GetConnection();

            string sqlstr = "select d.ID,l.IMEI from SALE_IMEI_LOG l join SALE_DETAIL d on l.SALE_DETAIL_ID = d.ID where d.POSUUID_MASTER = " + OracleDBUtil.SqlStr(masterId);
            OracleDataAdapter da = new OracleDataAdapter(sqlstr, conn);
            da.Fill(dt);
        }
        catch //(Exception ex)
        {
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            OracleConnection.ClearAllPools();
        }
        return dt;
    }

    private string DeleteCacheItem(string masterId, string cacheId)
    {
        string ids = string.Empty;
        foreach (string id in cacheId.Split(';'))
            ids += OracleDBUtil.SqlStr(id) + ",";
        string result = string.Empty;
        if (ids.Length != 0)
        {
            ids = ids.Substring(0, ids.Length - 1);
            OracleConnection ConnPOS = null;
            OracleTransaction TransPOS = null;
            try
            {
                ConnPOS = OracleDBUtil.GetConnection();
                TransPOS = ConnPOS.BeginTransaction();
                // 刪除 SALE_DETAIL
                string sql = string.Format("Delete From SALE_DETAIL S Where S.POSUUID_MASTER={0} AND S.ID IN ({1})", OracleDBUtil.SqlStr(masterId), ids);
                OracleDBUtil.ExecuteSql(TransPOS, sql);
                // 刪除 SALE_IMEI_LOG
                sql = string.Format("Delete From SALE_IMEI_LOG S Where S.SALE_DETAIL_ID IN ({0})", ids);
                OracleDBUtil.ExecuteSql(TransPOS, sql);
                // 檢查 SALE_DETAIL 中是否還有資料, 如果沒有, 則刪除 SALE_HEAD
                sql = string.Format("Select S.ID From SALE_DETAIL S Where S.POSUUID_MASTER={0}", OracleDBUtil.SqlStr(masterId));
                DataTable dt = OracleDBUtil.GetDataSet(TransPOS, sql).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    sql = string.Format("Delete From SALE_HEAD S WHERE POSUUID_MASTER={0}", OracleDBUtil.SqlStr(masterId));
                    OracleDBUtil.ExecuteSql(TransPOS, sql);
                }
                TransPOS.Commit();
                result = "{ RESULT:'1' }";
            }
            catch (Exception ex)
            {
                TransPOS.Rollback();
                result = string.Format("{{ RESULT:'0', ERROR:'{0}' }}", ex.Message);
            }
            finally
            {
                if (ConnPOS.State == ConnectionState.Open) ConnPOS.Close();
                TransPOS.Dispose();
                ConnPOS.Dispose();

                OracleConnection.ClearAllPools();
            }
        }
        return result;
    }

    private string DeleteIMEICache(string detailId, string imeis)
    {
        string result = "{ RESULT:'0', ERROR:'ERROR' }";
        string[] imeis_array = imeis.Split(';');
        OracleConnection ConnPOS = null;
        OracleTransaction TransPOS = null;
        try
        {
            ConnPOS = OracleDBUtil.GetConnection();
            TransPOS = ConnPOS.BeginTransaction();
            foreach (string imei in imeis_array)
            {
                TSAL01_Facade.Delete_SALE_IMEI_LOG(detailId, imei, TransPOS);
            }
            TransPOS.Commit();
            result = "{ RESULT:'1' }";
        }
        catch (Exception ex)
        {
            TransPOS.Rollback();
            result = string.Format("{{ RESULT:'0', ERROR:'{0}' }}", ex.Message);
        }
        finally
        {
            if (ConnPOS.State == ConnectionState.Open) ConnPOS.Close();
            TransPOS.Dispose();
            ConnPOS.Dispose();

            OracleConnection.ClearAllPools();
        }
        // { RESULT:'0', ERROR:'' }
        return result;
    }

    private DataTable GetPaidCache(string masterId)
    {
        DataTable dt = TSAL01_Facade.getPaid_Detail(masterId);
        return dt;
    }

    private string SavePaidCache(string data, string POSUUID_MASTER)
    {
        string[] dat = data.Split(':');
        for (int i = 0; i < dat.Length; i++)
            dat[i] = dat[i].Trim();
        string Desc = string.Empty;
        switch (Convert.ToInt32(dat[0]))
        {
            case 1:
                Desc = "現金"; break;
            case 2:
                Desc = string.Format("信用卡號:{0},序號:{1},調閱編號:{2}", dat[2], dat[3], dat[4]);
                break;
            case 3:
                Desc = string.Format("信用卡號:{0},授權碼:{1}", dat[2], dat[3]);
                break;
            case 4:
                Desc = string.Format("信用卡號:{0},序號:{1},調閱編號:{2},銀行別:{3},分期期數{4}", dat[2], dat[3], dat[4], dat[5], dat[7]);
                break;
            case 6:
                Desc = string.Format("金融卡號:{0},序號:{1}", dat[2], dat[3]);
                break;
            case 7:
                {
                    Desc = string.Format("HG卡號:{0},兌換點數:{1},剩餘點數:{2}", dat[2], dat[3], dat[4]);
                    try
                    {
                        string paidId = TSAL01_Facade.InsertHappyGoPaid_Detail(dat, POSUUID_MASTER);
                        return string.Format("{{ RESULT:'1',ID:'{0}' }}", paidId);
                    }
                    catch (Exception e)
                    {
                        return string.Format("{{ RESULT:'0',ID:'{0}' }}", e.Message);
                    }
                }
            case 9:
                Desc = string.Format("聯名卡卡號:{0},SAM交易序號:{1},卡片交易序號:{2},餘額:{3}", dat[1], dat[2], dat[3], dat[4]);
                break;
            default:
                break;
        }
        try
        {
            string posuuid = TSAL01_Facade.InsertPaid_Detail(string.Join(":", dat), Desc, POSUUID_MASTER);
            return string.Format("{{ RESULT:'1',ID:'{0}' }}", posuuid);
        }
        catch (Exception ex)
        {
            return string.Format("{{ RESULT:'0',ID:'{0}' }}", ex.Message);
        }

    }

    private string DeletePaidCache(string ids, string POSUUID_MASTER)
    {
        string result = "{ RESULT:'0', ERROR:'ERROR' }";
        string[] id_array = ids.Split(';');
        OracleConnection ConnPOS = null;
        OracleTransaction TransPOS = null;
        try
        {
            ConnPOS = OracleDBUtil.GetConnection();
            TransPOS = ConnPOS.BeginTransaction();
            foreach (string id in id_array)
            {
                TSAL01_Facade.DetelePaid_detail(id, POSUUID_MASTER, TransPOS);
            }
            TransPOS.Commit();
            result = "{ RESULT:'1' }";
        }
        catch (Exception ex)
        {
            TransPOS.Rollback();
            result = string.Format("{{ RESULT:'0', ERROR:'{0}' }}", ex.Message);
        }
        finally
        {
            if (ConnPOS.State == ConnectionState.Open) ConnPOS.Close();
            TransPOS.Dispose();
            ConnPOS.Dispose();

            OracleConnection.ClearAllPools();
        }
        // { RESULT:'0', ERROR:'' }
        return result;
    }

    private string CheckCompanyId(string id)
    {
        return string.Format("{{ RESULT:'{0}' }}", Convert.ToInt32(TSAL01_Facade.ValidateCompanyId(id)));
    }

    private DataTable GetETCInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Select T1.PRODNO, T1.PRODNAME, T1.UNIT, T1.CEASEDATE, T1.ISKEY, T1.ISSTOCK, T1.ISCONSIGNMENT, T1.PRICE, T1.ULSNO, ");
        sb.Append("T1.STATUS, T1.IMEI_FLAG, T1.PRODTYPENO, T1.EFFECT_DATE, T1.IS_POS_DEF_PRICE, T1.IS_OPEN_PRICE, T1.IS_DROPSHIPMENT, T1.IS_DISCOUNT, ");
        sb.Append("T1.SUPP_ID, T1.COMPANYCODE, T1.DEL_FLAG, T1.TAXABLE, T1.TAXRATE, T1.DS_FLAG, T1.INV_TYPE, ");
        sb.Append("POS_UUID() AS ID, '1' AS QUANTITY, '' AS POSUUID_DETAIL, '1' AS ITEM_TYPE, '1' AS ON_HAND_QTY, '' AS PROMOTION_CODE, '' AS BARCODE1, '11' AS SOURCE_TYPE ");
        sb.Append("FROM   PRODUCT T1 ");
        sb.Append("WHERE  T1.PRODNO=" + OracleDBUtil.SqlStr(TSAL01_Facade.getFETCProuductNo()));

        return OracleDBUtil.Query_Data(StringUtil.CStr(sb));
    }

    private string GetPromotionProducts(string promotionNo, string detailId, string products)
    {
        DataTable discountDt = new DataTable();
        discountDt.Columns.Add("SEQNO");
        discountDt.Columns.Add("posuuid_detail");
        discountDt.Columns.Add("discount_master_id");
        discountDt.Columns.Add("DISCOUNT_ID");
        discountDt.Columns.Add("DISCOUNT_NAME");
        discountDt.Columns.Add("DISCOUNT_PRICE");
        discountDt.Columns.Add("DISCOUNT_AMOUNT");
        discountDt.Columns.Add("QUANTITY");
        discountDt.Columns["QUANTITY"].DataType = System.Type.GetType(" System.Int32");
        discountDt.Columns["QUANTITY"].DefaultValue = 1;
        // products ex : 11111111;22222222;...
        // Return { RESULT:'1', Products:[], Discount:[] } or { RESULT:'1', ERROR:'Error Message' }
        products = products.Replace(";", "','");
        string stock = Common_PageHelper.GetGoodLOCUUID();
        StringBuilder sb = new StringBuilder();
        string storeNo = this.logMsg.STORENO;
        sb.Append("Select T1.PRODNO, T1.PRODNAME, T1.UNIT, T1.CEASEDATE, T1.ISKEY, T1.ISSTOCK, T1.ISCONSIGNMENT, T1.PRICE, T1.ULSNO,T1.PRICE as ORI_UNIT_PRICE, ");
        sb.Append("T1.STATUS, T1.IMEI_FLAG, T1.PRODTYPENO, T1.EFFECT_DATE, T1.IS_POS_DEF_PRICE, T1.IS_OPEN_PRICE, T1.IS_DROPSHIPMENT, T1.IS_DISCOUNT, ");
        sb.Append("T1.SUPP_ID, T1.COMPANYCODE, T1.DEL_FLAG, T1.TAXABLE, T1.TAXRATE, T1.DS_FLAG, T1.INV_TYPE, ");
        sb.AppendFormat("POS_UUID() as ID, '1' AS QUANTITY,'{0}'as POSUUID_DETAIL,'2' as ITEM_TYPE, '{1}' AS PROMOTION_CODE, '' AS BARCODE1, '4' AS SOURCE_TYPE, ", detailId, promotionNo);
        sb.AppendFormat("nvl((select PROMO_NAME from MM where PROMO_NO = '{0}'),'') as PROMOTION_NAME, ", promotionNo);
        sb.Append(" '' as SIM_CARD_NO, ");
        sb.Append(" '' as SH_DISCOUNT_REASON, '' as SH_DISCOUNT_DESC, ");
        sb.Append("nvl((select nvl(SUM(ON_HAND_QTY),'0') ");
        sb.AppendFormat(" FROM INV_ON_HAND_CURRENT where PRODNO = T1.PRODNO and STORE_NO='{0}' and STOCK_ID = '{1}'),'0') as  ON_HAND_QTY  ", storeNo, stock);
        sb.Append(" FROM   PRODUCT T1  ");
        sb.Append(" WHERE  NVL(T1.IS_DISCOUNT, 'N') = 'N'  ");
        sb.Append(Conditions);
        sb.Append(" AND T1.PRODNO in ('" + products + "')");

        DataTable pDt = OracleDBUtil.Query_Data(StringUtil.CStr(sb));

        string sqlstr = "";
        string MSISDN = "";
        string PRODNO = "";
        string R_Rate = "";
        string DATA = "";
        string Voice = "";
        string TRANS_TYPE = "";
        string MNP = "";
        string BUNDLE_TYPE = "";
        string MODI_USER = "";
        string SERVICE_TYPE = "";
        string STORE_NO = "";
        string total_amount = "0";
        OracleConnection conn = null;
        OracleDataAdapter da = null;

        try
        {
            conn = OracleDBUtil.GetConnection();
            if (!string.IsNullOrEmpty(detailId))
            {
                sqlstr = "select * from TO_CLOSE_HEAD where posuuid_detail = " + OracleDBUtil.SqlStr(detailId);
                da = new OracleDataAdapter(sqlstr, conn);
                DataTable chDt = new DataTable();
                da.Fill(chDt);
                if (chDt.Rows.Count > 0)
                {
                    R_Rate = StringUtil.CStr(chDt.Rows[0]["R_Rate"]);
                    DATA = StringUtil.CStr(chDt.Rows[0]["DATA"]);
                    Voice = StringUtil.CStr(chDt.Rows[0]["Voice"]);
                    TRANS_TYPE = StringUtil.CStr(chDt.Rows[0]["TRANS_TYPE"]);
                    MNP = StringUtil.CStr(chDt.Rows[0]["MNP"]);
                    BUNDLE_TYPE = StringUtil.CStr(chDt.Rows[0]["BUNDLE_TYPE"]);
                    MODI_USER = StringUtil.CStr(chDt.Rows[0]["MODI_USER"]);
                    SERVICE_TYPE = StringUtil.CStr(chDt.Rows[0]["SERVICE_TYPE"]);
                    STORE_NO = StringUtil.CStr(chDt.Rows[0]["STORE_NO"]);
                    MSISDN = StringUtil.CStr(chDt.Rows[0]["MSISDN"]);
                }

                //抓出商品料號
                sqlstr = string.Format("select PRODNO from TO_CLOSE_ITEM where  posuuid_detail = {0}", OracleDBUtil.SqlStr(detailId));
                OracleCommand cmd = new OracleCommand(sqlstr, conn);
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    PRODNO += dr.GetString(0) + ",";
                }
                PRODNO = PRODNO.TrimEnd(',');
                dr.Close();
            }
            else
            {
                PRODNO = products;
                STORE_NO = this.logMsg.STORENO;
                MODI_USER = this.logMsg.MODI_USER;
            }

            int i = 1;
            foreach (DataRow tRow in pDt.Rows)
            {
                string PRONO = StringUtil.CStr(tRow["PRODNO"]);
                //從算discount

                sqlstr = string.Format("select PRODNO from TO_CLOSE_ITEM where PRODNO = {0} and posuuid_detail = {1}", OracleDBUtil.SqlStr(PRONO), OracleDBUtil.SqlStr(detailId));
                DataTable ciDt = new DataTable();
                da = new OracleDataAdapter(sqlstr, conn);
                da.Fill(ciDt);
            }


            OracleCommand discountCmd = new OracleCommand("sp_query_discount_ws");
            discountCmd.Connection = conn;
            discountCmd.CommandType = CommandType.StoredProcedure;
            discountCmd.Parameters.Add("v_msisdn", OracleType.NVarChar).Value = MSISDN;
            discountCmd.Parameters.Add("v_r_rate", OracleType.NVarChar).Value = R_Rate;
            discountCmd.Parameters.Add("v_promotion_code", OracleType.NVarChar).Value = promotionNo;
            discountCmd.Parameters.Add("v_item_code", OracleType.NVarChar).Value = PRODNO;
            discountCmd.Parameters.Add("v_data", OracleType.NVarChar).Value = DATA;
            discountCmd.Parameters.Add("v_voice", OracleType.NVarChar).Value = Voice;
            discountCmd.Parameters.Add("v_trans_type", OracleType.NVarChar).Value = TRANS_TYPE;
            discountCmd.Parameters.Add("v_mnp", OracleType.NVarChar).Value = MNP;
            discountCmd.Parameters.Add("v_bundle_type", OracleType.NVarChar).Value = BUNDLE_TYPE;
            discountCmd.Parameters.Add("v_employee_id", OracleType.NVarChar).Value = MODI_USER;
            discountCmd.Parameters.Add("v_sys_id", OracleType.NVarChar).Value = SERVICE_TYPE;
            discountCmd.Parameters.Add("v_store_id", OracleType.NVarChar).Value = STORE_NO;
            discountCmd.Parameters.Add("o_data", OracleType.Cursor).Direction = ParameterDirection.Output;
            discountCmd.Parameters.Add("v_msgcode", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
            discountCmd.Parameters.Add("v_message", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
            discountCmd.ExecuteNonQuery();
            string v_msgcode = StringUtil.CStr(discountCmd.Parameters["v_msgcode"].Value);
            OracleDataReader dr2 = (OracleDataReader)discountCmd.Parameters["o_data"].Value;

            List<string> gifList = new List<string>();
            while (dr2.Read())
            {
                DataRow r = discountDt.NewRow();
                r["posuuid_detail"] = detailId;
                r["SEQNO"] = i++;
                if (dr2["discount_master_id"] != null)
                {
                    gifList.Add(StringUtil.CStr(dr2["discount_master_id"]));
                }
                r["discount_master_id"] = StringUtil.CStr(dr2["discount_master_id"]);
                r["DISCOUNT_ID"] = dr2["discount_code"] == null ? "" : StringUtil.CStr(dr2["discount_code"]);
                r["DISCOUNT_NAME"] = dr2["DISCOUNT_NAME"] == null ? "" : StringUtil.CStr(dr2["DISCOUNT_NAME"]);
                if (string.IsNullOrEmpty(StringUtil.CStr(dr2["DISCOUNT_MONEY"])))
                {
                    double price = Convert.ToDouble(dr2["DISCOUNT_MONEY"]);
                    r["DISCOUNT_PRICE"] = price * -1;
                }
                else
                {
                    if (dr2["DISCOUNT_RATE"] != DBNull.Value)
                    {
                        double rate = Convert.ToDouble(dr2["DISCOUNT_RATE"] == null ? 0 : dr2["DISCOUNT_RATE"]) * 0.01;
                        double t_amount = Convert.ToDouble(total_amount);
                        r["DISCOUNT_PRICE"] = t_amount * rate * -1;
                    }
                    else
                    {
                        r["DISCOUNT_PRICE"] = 0;
                    }
                }
                r["DISCOUNT_AMOUNT"] = r["DISCOUNT_PRICE"];
                discountDt.Rows.Add(r);
            }
            dr2.Close();


            sb = new StringBuilder();
            sb.Append(" SELECT ");
            sb.Append(" gd.prodno AS prodno, ");
            sb.Append("  prod.prodname AS prodname ");
            sb.Append(" FROM gift_discount gd, product prod ");
            sb.Append(" WHERE ");
            sb.Append(" discount_master_id = :discount_master_id ");
            sb.Append(" AND gd.prodno = prod.prodno AND companycode = '01' ");

            OracleCommand cmd2 = new OracleCommand(StringUtil.CStr(sb), conn);
            cmd2.Parameters.Add(":discount_master_id", OracleType.VarChar);

            //抓贈品
            foreach (string discount_master_id in gifList)
            {
                cmd2.Parameters[":discount_master_id"].Value = discount_master_id;
                OracleDataReader dr3 = cmd2.ExecuteReader();
                while (dr3.Read())
                {
                    DataRow r = discountDt.NewRow();
                    r["DISCOUNT_ID"] = dr3.GetString(0);
                    r["DISCOUNT_NAME"] = dr3.GetString(1);
                    r["DISCOUNT_PRICE"] = 0;
                    r["DISCOUNT_AMOUNT"] = 0;
                    r["SEQNO"] = i++;
                    discountDt.Rows.Add(r);
                }
                dr3.Close();
            }


        }
        catch (Exception ex)
        {
            return string.Format("{{ RESULT:'0', ERROR:'{0}'}}", ex.Message);
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            OracleConnection.ClearPool(conn);
        }

        return string.Format("{{ RESULT:'1', Products:{0}, Discount:{1} }}", GetJsonData(pDt), GetJsonData(discountDt));
    }

    private string GetHistory(string masterId, string printname, string receiptname)
    {
        /*	取得歷史銷售資料	
         * {
         *		Header:{},
         *		Products:[],
         *		Discount:[],
         *		PaidCache:[],
         *		IMEIs:[]
         *	}
         *	Or
         *	{
         *		ERROR:'Error Message'
         *	}
         */
        try
        {
            StringBuilder sb = new StringBuilder();
            string stock = Common_PageHelper.GetGoodLOCUUID();
            string storeNo = this.logMsg.STORENO;
            string INVOICE_URL = "";
            string RECEIPT_URL = "";
            if (string.IsNullOrEmpty(printname)) printname = "";
            try
            {
                //產生發票
                INVOICE_URL = PrintInventory(masterId, printname);
                //產生收據
                RECEIPT_URL = Collection_Receipt(masterId, receiptname);
            }
            catch
            {

            }
            // 檔頭
            sb = new StringBuilder();
            sb.Append("Select S.SALE_NO, TO_CHAR(S.TRADE_DATE, 'yyyy/mm/dd') AS TRADE_DATE, TO_CHAR(S.MODI_DTM, 'yyyy/mm/dd') AS MODI_DTM, S.MODI_USER, S.UNI_NO, S.UNI_TITLE, S.REMARK, I.INVOICE_NO, ");
            sb.AppendFormat("'{0}' AS INVOICE_URL,'{1}' AS RECEIPT_URL,S.SALE_STATUS, ", INVOICE_URL, RECEIPT_URL);
            sb.Append("MI.INVOICE_NO AS M_INVOICE_NO ");
            sb.Append("From SALE_HEAD S Left Join INVOICE_HEAD I ON  S.POSUUID_MASTER=I.POSUUID_MASTER ");
            sb.Append("Left Join MANUAL_INVOICE_HEAD MI ON S.POSUUID_MASTER=MI.POSUUID_MASTER ");
            sb.AppendFormat("Where S.POSUUID_MASTER={0}", OracleDBUtil.SqlStr(masterId));
            DataTable dtHeader = OracleDBUtil.Query_Data(StringUtil.CStr(sb));
            // 明細
            sb = new StringBuilder();
            sb.Append("Select T1.PRODNO, T1.PRODNAME, T1.UNIT, T1.CEASEDATE, T1.ISKEY, T1.ISSTOCK, T1.ISCONSIGNMENT, T2.UNIT_PRICE AS PRICE, T1.ULSNO,T2.ORI_UNIT_PRICE, ");
            sb.Append("T1.STATUS, T1.IMEI_FLAG, T1.PRODTYPENO, T1.EFFECT_DATE, T1.IS_POS_DEF_PRICE, T1.IS_OPEN_PRICE, T1.IS_DROPSHIPMENT, T1.IS_DISCOUNT, ");
            sb.Append("T1.SUPP_ID, T1.COMPANYCODE, T1.DEL_FLAG, T1.TAXABLE, T1.TAXRATE, T1.DS_FLAG, T1.INV_TYPE, ");
            sb.Append("T2.ID, T2.QUANTITY, T2.POSUUID_DETAIL, T2.ITEM_TYPE, T2.PROMOTION_CODE, T2.BARCODE1, T2.SOURCE_TYPE, POS_UUID() AS NEW_ID, ");
            //sb.Append("nvl((select nvl(SUM(ON_HAND_QTY),'0') ");
            //sb.AppendFormat(" FROM INV_ON_HAND_CURRENT where PRODNO = T1.PRODNO and STORE_NO='{0}' and STOCK_ID = '{1}'),'0') as ON_HAND_QTY  ", storeNo, stock);
            sb.Append("nvl((select PROMO_NAME from MM where PROMO_NO = T2.PROMOTION_CODE and PROMO_STATUS = 1),'') as PROMOTION_NAME, ");
            sb.Append(" '999' as ON_HAND_QTY  ");
            sb.Append("FROM   PRODUCT T1, SALE_DETAIL T2  ");
            sb.AppendFormat("WHERE  T1.PRODNO=T2.PRODNO AND T2.POSUUID_MASTER={0} AND T2.ITEM_TYPE != '5'", OracleDBUtil.SqlStr(masterId));
            DataTable dtProducts = OracleDBUtil.Query_Data(StringUtil.CStr(sb));
            // 折扣			
            sb = new StringBuilder();
            sb.Append("Select T1.PRODNO AS DISCOUNT_ID, T1.PRODNAME AS DISCOUNT_NAME, T2.UNIT_PRICE AS DISCOUNT_PRICE,  ");
            sb.Append("T2.UNIT_PRICE AS DISCOUNT_AMOUNT,");
            sb.Append("T2.SOURCE_REFERENCE_KEY as ID, T2.QUANTITY, T2.POSUUID_DETAIL  ");
            sb.Append("FROM   PRODUCT T1, SALE_DETAIL T2  ");
            sb.AppendFormat("WHERE  T1.PRODNO=T2.PRODNO AND T2.POSUUID_MASTER={0} AND T2.ITEM_TYPE = '5'", OracleDBUtil.SqlStr(masterId));
            DataTable dtDiscount = OracleDBUtil.Query_Data(StringUtil.CStr(sb));




            return string.Format("{{ Header:{0}, Products:{1}, Discount:{2}, PaidCache:{3}, IMEIs:{4} }}",
                (dtHeader == null || dtHeader.Rows.Count == 0) ? "{}" : GetJsonData(dtHeader).TrimStart('[').TrimEnd(']'),
                GetJsonData(dtProducts),
                GetJsonData(dtDiscount),
                GetJsonData(TSAL01_Facade.getPaid_Detail(masterId, true)),
                GetJsonData(GetMasterIMEI(masterId)));
        }
        catch (Exception ex)
        {
            return string.Format("{{ ERROR:'{0}' }}", ex.Message.Replace("\n", "\\\n"));
        }

    }


    //public string Get_Add_Prods_Info(string discount_code, string prodno)
    //{
    //    string stock = Common_PageHelper.GetGoodLOCUUID();
    //    StringBuilder sb = new StringBuilder();
    //    sb.Append("Select T1.PRODNO, T1.PRODNAME, T1.UNIT, T1.CEASEDATE, T1.ISKEY, T1.ISSTOCK, T1.ISCONSIGNMENT, T1.PRICE, T1.ULSNO,T1.PRICE as ORI_UNIT_PRICE, ");
    //    sb.Append("T1.STATUS, T1.IMEI_FLAG, T1.PRODTYPENO, T1.EFFECT_DATE, T1.IS_POS_DEF_PRICE, T1.IS_OPEN_PRICE, T1.IS_DROPSHIPMENT, T1.IS_DISCOUNT, ");
    //    sb.Append("T1.SUPP_ID, T1.COMPANYCODE, T1.DEL_FLAG, T1.TAXABLE, T1.TAXRATE, T1.DS_FLAG, T1.INV_TYPE, ");
    //    sb.Append("POS_UUID() as ID, '1' AS QUANTITY,''as POSUUID_DETAIL,'1' as ITEM_TYPE, '' AS PROMOTION_CODE, '' AS BARCODE1, '11' AS SOURCE_TYPE, ");
    //    sb.Append(" '' as PROMOTION_NAME,");
    //    sb.Append(" '' as SIM_CARD_NO, ");
    //    sb.Append(" '' as SH_DISCOUNT_REASON, '' as SH_DISCOUNT_DESC, ");
    //    //sb.Append("nvl((select nvl(SUM(ON_HAND_QTY),'0') ");
    //    //sb.AppendFormat(" FROM INV_ON_HAND_CURRENT where PRODNO = T1.PRODNO and STORE_NO='{0}' and STOCK_ID = '{1}'),'0') as  ON_HAND_QTY  ", storeNo, stock);
    //    //sb.AppendFormat(" INV_OnHandQty(T1.PRODNO,'{0}') as ON_HAND_QTY ", storeNo);
    //    sb.Append(" FROM   PRODUCT T1  ");
    //    sb.Append(" WHERE T1.PRODNO not in (select PRODNO from NOT_IN_SALE_PROD where STATUS = 0) AND  NVL(T1.IS_DISCOUNT, 'N') = 'N'  ");
    //    sb.Append(Conditions);
    //    //sb.Append(" AND T1.PRODNO = " + OracleDBUtil.SqlStr(prodNo));

    //    return GetJsonData(OracleDBUtil.Query_Data(StringUtil.CStr(sb)));
    //}

    public string Get_Add_Prods(string posuuid_detail, string prodno)
    {
        string employee_id = this.logMsg.MODI_USER;
        string store_no = this.logMsg.STORENO;
        Hashtable table = Discount_Facade.get_add_in_prod_discount(prodno, store_no, employee_id, posuuid_detail);
        StringBuilder sb = new StringBuilder();
        foreach (DictionaryEntry t in table)
        {

            sb.AppendFormat("{{ DISCOUNT_CODE:'{0}',PRODS:{1}}}", StringUtil.CStr(t.Key), GetJsonData((DataTable)t.Value));
            sb.Append(",");
        }

        string result = @"[" + StringUtil.CStr(sb).TrimEnd(',') + "]";
        // Return Json Format
        //        @"[
        //			{
        //             ID: '',
        //				DISCOUNT_ID:'DDXSSDSDW',
        //             DISCOUNT_NAME:'',
        //             
        //				PRODS:
        //					[
        //						{PRODNO:'451100398', PRODNAME:'XBOX 360 S型櫃陳列物', UNI_PRICE:'500', SALE_PRICE:'200'}, 
        //						{PRODNO:'152800063', PRODNAME:'Sharp WX-T931 黑色簡配3.5G', UNI_PRICE:'200', SALE_PRICE:'100'}
        //					]
        //			},
        //			{
        //				DISCOUNT_CODE:'CCSDEWSD',
        //				PRODS:
        //					[
        //						{PRODNO:'157200002', PRODNAME:'iPhone 3G 8GB-Black(MB489TA/B)', UNI_PRICE:'19000', SALE_PRICE:'10000'},
        //                        {PRODNO:'309600205', PRODNAME:'Asus VX6 筆電(藍寶堅尼/白)', UNI_PRICE:'26990', SALE_PRICE:'20000'},
        //                        {PRODNO:'309600016', PRODNAME:'BenQ E1020銀 相機', UNI_PRICE:'3500', SALE_PRICE:'3000'}
        //					]
        //			},
        //			{
        //				DISCOUNT_CODE:'sdsdsdeeeee',
        //				PRODS:
        //					[
        //						{PRODNO:'300700283', PRODNAME:'Moto C168/V361/V191/A732 配件包', UNI_PRICE:'450', SALE_PRICE:'300'},
        //                        {PRODNO:'300700321', PRODNAME:'Nokia 2680S/7100台製配件包', UNI_PRICE:'450', SALE_PRICE:'300'}
        //					]
        //			},
        //			{
        //				DISCOUNT_CODE:'cdssdsd',
        //				PRODS:
        //					[
        //						{PRODNO:'309600012', PRODNAME:'微軟Microsoft Arc 滑鼠(白)', UNI_PRICE:'1890', SALE_PRICE:'1500'}, 
        //						{PRODNO:'309900313', PRODNAME:'KINGMAX 4GB 記憶卡', UNI_PRICE:'399', SALE_PRICE:'200'}
        //					]
        //			}						
        //		]";

        return result;
    }


    public string unlock_imei(string imei, string PRODNO, string detailid)
    {
        string sale_detail_id = TSAL01_Facade.CheckINV_IMEI(imei, detailid);
        string old_detail_id = TSAL01_Facade.CheckIMEI_Detail_ID(sale_detail_id);
        if (!string.IsNullOrEmpty(old_detail_id))
        {
            return string.Format("{{ RESULT:'0',IMEI:'{0}',MESSAGE:'查無資料'}}", imei);
        }

        DataTable dtEMIE = null;
        string locId = FET.POS.Model.Common.Common_PageHelper.GetGoodLOCUUID();
        //IMEI Flag 4 is just for log

        dtEMIE = TSAL01_Facade.getApprove_AllowIMEI(logMsg.STORENO, locId, PRODNO, imei);
        //需判別 IMEI可賣數量
        if (dtEMIE != null && dtEMIE.Rows.Count > 0)
        {
            TSAL01_Facade.UpdateINV_IMEI(sale_detail_id, old_detail_id, imei, logMsg.MODI_USER);
            return string.Format("{{ RESULT:'1',IMEI:'{0}',MESSAGE:''}}", imei);
        }
        else
        {
            return string.Format("{{ RESULT:'0',IMEI:'{0}',MESSAGE:'{1}'}}", imei, string.Format("此 IMEI({0}) 不允許使用", imei));
        }
    }

    public LogMessage logMsg { get { return HttpContext.Current.Session["logMsg"] as LogMessage; } }


    #region 收據
    public static string Collection_Receipt(string posuuid_master, string ReceiptName)
    {
        string return_url = "";
        List<List<string>> dir = new List<List<string>>();
        OracleConnection conn = null;
        OracleCommand cmd = null;
        OracleDataAdapter da = null;
        OracleDataReader dr = null;
        string trade_date = "";
        string store_no = "";
        string machice_id = "";
        string RECEIPT_NO = "";
        string total_amount = "";
        string barcode1 = "";
        string barcode2 = "";
        string barcode3 = "";
        string CARD_NO = "";
        string msisdn = "";
        string SALE_PERSON = "";
        string hg_card_no = "";
        string pay_mode1 = ""; //現金
        string pay_mode2 = ""; //信用卡
        string pay_mode7 = "";//HappyGo折抵
        string pay_mode8 = "";//找零金
        string sqlstr = "select sh.trade_date,SH.STORE_NO,SH.MACHINE_ID,RH.RECEIPT_NO,SH.SALE_PERSON,sh.hg_card_no from sale_head sh  join RECEIPT_HEAD rh on RH.POSUUID_MASTER = SH.POSUUID_MASTER where  sh.posuuid_master = " + OracleDBUtil.SqlStr(posuuid_master);
        try
        {
            conn = OracleDBUtil.GetConnection();
            cmd = new OracleCommand(sqlstr, conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                trade_date = dr.IsDBNull(0) ? "" : dr.GetDateTime(0).ToString("yyyy-MM-dd hh:mm");//StringUtil.CStr(dr[0]);
                store_no = StringUtil.CStr(dr[1]);
                machice_id = StringUtil.CStr(dr[2]);
                RECEIPT_NO = StringUtil.CStr(dr[3]);
                SALE_PERSON = StringUtil.CStr(dr[4]);
                hg_card_no = StringUtil.CStr(dr[5]);
            }
            dr.Close();

            //抓取detal
            sqlstr = "select barcode1,barcode2,barcode3,msisdn,total_amount,id,fun_id,CARD_NO from sale_detail where source_type = 3 and  posuuid_master = " + OracleDBUtil.SqlStr(posuuid_master);
            DataTable detail = new DataTable();
            cmd = new OracleCommand(sqlstr, conn);
            da = new OracleDataAdapter(cmd);
            da.Fill(detail);
            foreach (DataRow row in detail.Rows)
            {
                string sale_detail_id = StringUtil.CStr(row["id"]);
                barcode1 = StringUtil.CStr(row["barcode1"]);
                barcode2 = StringUtil.CStr(row["barcode2"]);
                barcode3 = StringUtil.CStr(row["barcode3"]);
                CARD_NO = StringUtil.CStr(row["CARD_NO"]);
                msisdn = StringUtil.CStr(row["msisdn"]);
                total_amount = StringUtil.CStr(row["total_amount"]);
                string fun_id = StringUtil.CStr(row["fun_id"]);

                //抓出拆分金額
                //抓現金
                sqlstr = "select sum(amount) from bill_dispatch where pay_mode_id = 1 and sale_detail_id = " + OracleDBUtil.SqlStr(sale_detail_id);
                cmd = new OracleCommand(sqlstr, conn);
                pay_mode1 = cmd.ExecuteScalar() == null ? "0" : StringUtil.CStr(cmd.ExecuteScalar());

                //抓信用卡
                sqlstr = "select sum(amount) from bill_dispatch where pay_mode_id in(2,3,4) and sale_detail_id = " + OracleDBUtil.SqlStr(sale_detail_id);
                cmd = new OracleCommand(sqlstr, conn);
                pay_mode2 = cmd.ExecuteScalar() == null ? "0" : StringUtil.CStr(cmd.ExecuteScalar());

                //找零金
                sqlstr = "select sum(amount) from bill_dispatch where pay_mode_id = 8 and sale_detail_id = " + OracleDBUtil.SqlStr(sale_detail_id);
                cmd = new OracleCommand(sqlstr, conn);
                pay_mode8 = cmd.ExecuteScalar() == null ? "0" : StringUtil.CStr(cmd.ExecuteScalar());

                //happyGo
                sqlstr = "select sum(amount) from bill_dispatch where pay_mode_id = 7 and sale_detail_id = " + OracleDBUtil.SqlStr(sale_detail_id);
                cmd = new OracleCommand(sqlstr, conn);
                pay_mode7 = cmd.ExecuteScalar() == null ? "0" : StringUtil.CStr(cmd.ExecuteScalar());
                //1: 遠傳帳單
                //2: 和信帳單
                //3: Seednet帳單
                //4: 遠通帳單(有單)
                //5: 遠通帳單(無單)
                //6: 速博帳單
                //判斷類型
                List<string> list = new List<string>();
                switch (fun_id)
                {
                    case "1":
                        //FET

                        list.Add("4");
                        list.Add(trade_date);
                        list.Add(store_no);
                        list.Add(machice_id);
                        list.Add(RECEIPT_NO);
                        list.Add("遠傳電信帳單");
                        list.Add(total_amount);
                        list.Add(barcode1);
                        list.Add(msisdn);
                        list.Add(pay_mode1);
                        list.Add(pay_mode2);
                        list.Add(pay_mode7);
                        list.Add("0");
                        list.Add(hg_card_no);
                        list.Add(SALE_PERSON);
                        dir.Add(list);
                        break;
                    case "2"://KGT

                        list.Add("5");
                        list.Add(trade_date);
                        list.Add(store_no);
                        list.Add(machice_id);
                        list.Add(RECEIPT_NO);
                        list.Add("和信電信帳單");
                        list.Add(total_amount);
                        list.Add(barcode1);
                        list.Add(pay_mode1);
                        list.Add(pay_mode2);
                        list.Add(pay_mode7);
                        list.Add(pay_mode8);
                        list.Add(hg_card_no);
                        list.Add(SALE_PERSON);
                        dir.Add(list);
                        break;
                    case "3"://Seednet
                        list.Add("8");
                        list.Add(trade_date);
                        list.Add(store_no);
                        list.Add(machice_id);
                        list.Add(RECEIPT_NO);
                        list.Add("Seednet帳單");
                        list.Add(total_amount);
                        list.Add(barcode1);
                        list.Add(barcode2);
                        list.Add(barcode3);
                        list.Add(pay_mode1);
                        list.Add(SALE_PERSON);
                        dir.Add(list);
                        break;
                    case "4"://ETC(有單
                        list.Add("3");
                        list.Add(trade_date);
                        list.Add(store_no);
                        list.Add(machice_id);
                        list.Add(RECEIPT_NO);
                        list.Add("ETC 代收");
                        list.Add(total_amount);
                        list.Add(barcode1);
                        list.Add(barcode2);
                        list.Add(barcode3);
                        list.Add(pay_mode1);
                        list.Add(barcode2);
                        list.Add(SALE_PERSON);
                        dir.Add(list);
                        break;
                    case "5"://ETC(無單
                        list.Add("3");
                        list.Add(trade_date);
                        list.Add(store_no);
                        list.Add(machice_id);
                        list.Add(RECEIPT_NO);
                        list.Add("ETC 代收");
                        list.Add(total_amount);
                        list.Add(barcode1);
                        list.Add(barcode2);
                        list.Add(barcode3);
                        list.Add(pay_mode1);
                        list.Add(pay_mode2);
                        list.Add(SALE_PERSON);
                        dir.Add(list);
                        break;
                    case "6"://NCIC
                        list.Add("6");
                        list.Add(trade_date);
                        list.Add(store_no);
                        list.Add(machice_id);
                        list.Add(RECEIPT_NO);
                        list.Add("速博電信帳單");
                        list.Add(total_amount);
                        list.Add(barcode1);
                        list.Add(barcode2);
                        list.Add(barcode3);
                        list.Add(pay_mode1);

                        list.Add(SALE_PERSON);
                        dir.Add(list);
                        break;

                }

            }

            string fileName = "";
            if (dir.Count > 0)
            {
                SAL01_Facade facade = new SAL01_Facade();
                string filePath = facade.getUploadPath(posuuid_master);
                IRClass pri = new PriReceipt();
                fileName = pri.Print("M", null, dir, ReceiptName);

                if (fileName == null || string.IsNullOrEmpty(fileName))
                {
                    throw new Exception("列印收據失敗，請重印收據!!");
                }
                else
                {
                    return_url = HttpContext.Current.Request.ApplicationPath + "/Downloads/Receipt/" + fileName;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            OracleConnection.ClearPool(conn);
        }
        return return_url;
    }

    #endregion

    private string PrintInventory(string POSUUID_MASTER, string printname)
    {
        if (!TSAL01_Facade.IsInventory(POSUUID_MASTER)) return "";
        string url = "";
        Receipt myReceipt = new Receipt();
        SAL01_Facade facade = new SAL01_Facade();
        //判別是否有發票
        string filePath = facade.getUploadPath(POSUUID_MASTER);
        if (!string.IsNullOrEmpty(filePath))
        {
            string fileName = myReceipt.generateReceiptretest(POSUUID_MASTER, "與正本相符", printname);
            if (fileName == null || string.IsNullOrEmpty(fileName))
            {
                throw new Exception("列印發票失敗，請重印發票!!");
            }
            else
            {
                url = HttpContext.Current.Request.ApplicationPath + filePath + "/" + fileName;
            }
        }
        else
        {
            throw new Exception("列印發票失敗，請重印發票!!");
        }

        return url;
    }
}