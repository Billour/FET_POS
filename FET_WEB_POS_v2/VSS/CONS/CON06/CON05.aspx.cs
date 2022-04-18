using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//using AdvTek.CustomControls;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;
using FET.POS.Model.Facade.FacadeImpl;

public partial class VSS_CONS_CON05 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //判斷是否為總部人員
            if (logMsg.STORENO == "HQ")
            {
                pcSTORENO.Visible = true;
                lblSTORE.Visible = false;
            }
            else
            {
                pcSTORENO.Visible = false;
                lblSTORE.Visible = true;
                lblSTORE.Text = this.logMsg.STORENO + " " + new Store_Facade().GetStoreName(this.logMsg.STORENO);
                pcSTORENO.Text = this.logMsg.STORENO;
            }

            // 繫結空的資料表，以顯示表頭欄位
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
        }
        else
        {
            DataTable dtGvMaster = ViewState["gvMaster"] as DataTable;
            if (dtGvMaster != null)
            {
                gvMaster.DataSource = dtGvMaster;
                gvMaster.DataBind();
            }
        }
    }

    #region Button 觸發事件
        //查詢鈕
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindMasterData();
        }

    #endregion

    #region gvMaster 觸發事件
        protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
        {
            gvMaster.DataSource = ViewState["gvMaster"];
            gvMaster.DataBind();
            gvMaster.DetailRows.CollapseAllRows();

        }
        protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Detail)
            {
                // 繫結明細資料表           
                BindDetailData(e.GetValue(gvMaster.KeyFieldName).ToString(), e.GetValue("ORDNO").ToString());
            }
        }
 
    #endregion

    #region gvMaster 觸發事件

        protected void gvDetail_PageIndexChanged(object sender, EventArgs e)
        {
            ASPxGridView detailGrid = gvMaster.FindChildControl<ASPxGridView>("gvDetail");
            string M_ID = detailGrid.GetMasterRowKeyValue().ToString();
            //BindDetailData(M_ID, ((ASPxLabel)detailGrid.FindTitleTemplateControl("lblDetORDNO")).Text);
        }

    #endregion

    //Bind訂單
    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("訂單日期", typeof(string));
        dtResult.Columns.Add("廠商編號", typeof(string));
        dtResult.Columns.Add("廠商名稱", typeof(string));
        dtResult.Columns.Add("區域", typeof(string));
        dtResult.Columns.Add("訂單編號", typeof(string));
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        return dtResult;
    }

    private DataTable getMasterData()
    {
        string strORDDATE = txtORDDATE.Text;
        if (strORDDATE != "")
        {
            strORDDATE = txtORDDATE.Date.ToString("yyyyMMdd");
        }
        DataTable dtResult = new CON05_Facade().getCSM_ORDERM(strORDDATE, txtORDNO.Text, ddlSTATUS.SelectedItem.Value.ToString(), txtPRODNO.Text, pcSTORENO.Text);
        return dtResult;
    }

    //Bind訂單明細
    protected void BindDetailData(string M_ID, string strORDNO)
    {
        ASPxGridView detailGrid = gvMaster.FindChildControl<ASPxGridView>("gvDetail");
        detailGrid.Caption = string.Format("<DIV align='left' style='font-size:10pt'>訂單編號：{0}</DIV>", strORDNO);
        detailGrid.DataSource = new CON05_Facade().getCSM_ORDERD(M_ID);
        detailGrid.DataBind();       
    }


}
