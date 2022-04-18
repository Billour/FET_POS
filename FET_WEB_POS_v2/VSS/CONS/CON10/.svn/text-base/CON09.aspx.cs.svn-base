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


public partial class VSS_CONS_CON09 : BasePage
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
        //gvMaster.PageIndex = 0;
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        Session["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DetailRows.CollapseAllRows();
        gvMaster.DataBind();
    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("CSM_RTNM_UUID", typeof(string));
        dtResult.Columns.Add("RTNNO", typeof(string));
        dtResult.Columns.Add("B_DATE", typeof(string));
        dtResult.Columns.Add("E_DATE", typeof(string));
        dtResult.Columns.Add("STATUS", typeof(string));
        dtResult.Columns.Add("MODI_USER", typeof(string));
        dtResult.Columns.Add("MODI_DTM", typeof(string));
        return dtResult;
    }

    private DataTable getMasterData()
    {

        string strB_DATE_S = B_DATE_S.Text;
        string strB_DATE_E = B_DATE_E.Text;
        string strE_DATE_S = E_DATE_S.Text;
        string strE_DATE_E = E_DATE_E.Text;

        if (strB_DATE_S != "")
            strB_DATE_S = B_DATE_S.Date.ToString("yyyyMMdd");

        if (strB_DATE_E != "")
            strB_DATE_E = B_DATE_E.Date.ToString("yyyyMMdd");

        if (strE_DATE_S != "")
            strE_DATE_S = E_DATE_S.Date.ToString("yyyyMMdd");

        if (strE_DATE_E != "")
            strE_DATE_E = E_DATE_E.Date.ToString("yyyyMMdd");


        DataTable dtResult = new CON09_Facade().getCSM_RTNM(txtRtnNo.Text, txtStoreNo.Text, txtSuppNo.Text, txtPRODNO.Text, strB_DATE_S, strB_DATE_E, strE_DATE_S, strE_DATE_E, RTN_STATUS.SelectedItem.Value.ToString());

        return dtResult;
    }

    //Bind訂單明細
    protected void BindDetailData(string M_ID, string strORDNO)
    {

        ASPxGridView detailGrid = gvMaster.FindChildControl<ASPxGridView>("gvDetail");
        detailGrid.Caption = string.Format("<DIV align='left' style='font-size:10pt'>訂單編號：{0}</DIV>", strORDNO);
        detailGrid.DataSource = new CON09_Facade().getCSM_RTND(M_ID);
        detailGrid.DataBind();

    }



    #region gvDetail 觸發事件

    protected void gvDetail_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView detailGrid = gvMaster.FindChildControl<ASPxGridView>("gvDetail");
        string M_ID = detailGrid.GetMasterRowKeyValue().ToString();
        //BindDetailData(M_ID, ((ASPxLabel)detailGrid.FindTitleTemplateControl("lblDetORDNO")).Text);
    }

    #endregion
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
        gvMaster.DetailRows.CollapseAllRows();

    }
    #endregion

    protected void btnExport_Click(object sender, EventArgs e)
    {

        ExportExcelData eel = new ExportExcelData();
        this.Page.Controls.Add(eel);
        List<object> keyValues = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);


        if (keyValues.Count > 0)
        {
            string strCSM_RTNM_UUID = "";
            foreach (string key in keyValues)
            {
                strCSM_RTNM_UUID = strCSM_RTNM_UUID + "'" + key.ToString() + "',";

            }
            strCSM_RTNM_UUID = strCSM_RTNM_UUID.Substring(0, strCSM_RTNM_UUID.Length - 1);
            eel.ExportExcel(new CON09_Facade().ExportWeightDistribute(strCSM_RTNM_UUID));
        }

      

    }
}
