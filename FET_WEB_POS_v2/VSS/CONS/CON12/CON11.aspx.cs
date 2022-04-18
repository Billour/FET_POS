using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;

public partial class VSS_CONS_CON11 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 繫結空的資料表，以顯示表頭欄位
            gvMaster.DataSource = GetEmptyDataTable();
            Session["gvMaster"] = (DataTable)gvMaster.DataSource;
            gvMaster.DataBind();
        }
        else
        {
            DataTable dtGvMaster = Session["gvMaster"] as DataTable;
            if (dtGvMaster != null)
            {
                gvMaster.DataSource = dtGvMaster;
                gvMaster.DataBind();
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        gvMaster.PageIndex = 0;
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        Session["gvMaster"] = dtResult;
        gvMaster.DetailRows.CollapseAllRows();
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("CSM_RTNM_UUID", typeof(string));
        dtResult.Columns.Add("RTNNO", typeof(string));
        dtResult.Columns.Add("B_DATE", typeof(string));
        dtResult.Columns.Add("E_DATE", typeof(string));
        dtResult.Columns.Add("RTNDATE", typeof(string));
        dtResult.Columns.Add("MODI_USER", typeof(string));
        dtResult.Columns.Add("MODI_DTM", typeof(string));
        dtResult.Columns.Add("STATUS", typeof(string));
        dtResult.Columns.Add("REMARK", typeof(string));
        return dtResult;
    }

    private DataTable getMasterData()
    {
        string strB_DATE_S = lbB_DATE_S.Text;
        string strB_DATE_E = lbB_DATE_E.Text;
        string strE_DATE_S = lbE_DATE_S.Text;
        string strE_DATE_E = lbE_DATE_E.Text;
        string strRTNDATE_S = lbRTNDATE_S.Text;
        string strRTNDATE_E = lbRTNDATE_E.Text;

        if (strB_DATE_S != "")
            strB_DATE_S = lbB_DATE_S.Date.ToString("yyyyMMdd");

        if (strB_DATE_E != "")
            strB_DATE_E = lbB_DATE_E.Date.ToString("yyyyMMdd");

        if (strE_DATE_S != "")
            strE_DATE_S = lbE_DATE_S.Date.ToString("yyyyMMdd");

        if (strE_DATE_E != "")
            strE_DATE_E = lbE_DATE_E.Date.ToString("yyyyMMdd");

        if (strRTNDATE_S != "")
            strRTNDATE_S = lbRTNDATE_S.Date.ToString("yyyyMMdd");

        if (strRTNDATE_E != "")
            strRTNDATE_E = lbRTNDATE_S.Date.ToString("yyyyMMdd");



        DataTable dtResult = new CON11_Facade().getCSM_RTNM(txtRtnNo.Text, logMsg.STORENO, strB_DATE_S, strB_DATE_E, strE_DATE_S, strE_DATE_E, strRTNDATE_S, strRTNDATE_E, combRTN_STATUS.SelectedItem.Value.ToString());

        return dtResult;
    }

    private DataTable getDetailData()
    {
        DataTable gvDetail = new DataTable();
        gvDetail.Columns.Clear();
        gvDetail.Columns.Add("項次", typeof(string));
        gvDetail.Columns.Add("商品料號", typeof(string));
        gvDetail.Columns.Add("商品名稱", typeof(string));
        gvDetail.Columns.Add("廠商編號", typeof(string));
        gvDetail.Columns.Add("廠商名稱", typeof(string));
        gvDetail.Columns.Add("庫存數量", typeof(string));
        gvDetail.Columns.Add("實際退倉數量", typeof(string));
        for (int i = 1; i < 5; i++)
        {
            DataRow gvDetailRow = gvDetail.NewRow();
            gvDetailRow["項次"] = i;
            gvDetailRow["商品料號"] = "商品料號" + i;
            gvDetailRow["商品名稱"] = "商品名稱" + i;
            gvDetailRow["廠商編號"] = "廠商編號" + i;
            gvDetailRow["廠商名稱"] = "廠商名稱" + i;
            gvDetailRow["庫存數量"] = i % 2;
            gvDetailRow["實際退倉數量"] = i % 2;
            gvDetail.Rows.Add(gvDetailRow);
        }
        return gvDetail;
    }

    #region gvMaster 觸發事件
    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Detail)
        {
            // 繫結明細資料表           
            BindDetailData(e.GetValue(gvMaster.KeyFieldName).ToString(), e.GetValue("RTNNO").ToString());
        }
    }
    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        gvMaster.DataSource = Session["gvMaster"];
        gvMaster.DataBind();

    }
    #endregion

    //Bind退倉明細
    protected void BindDetailData(string M_ID, string strRTNNO)
    {

        ASPxGridView detailGrid = gvMaster.FindChildControl<ASPxGridView>("gvDetail");
        detailGrid.Caption = string.Format("<DIV align='left' style='font-size:10pt'>退倉單號：{0}</DIV>", strRTNNO);
        detailGrid.DataSource = new CON11_Facade().getCSM_RTND(M_ID,logMsg.STORENO);
        detailGrid.DataBind();

    }
}
