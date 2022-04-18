using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using DevExpress.Web;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxGridView.Rendering;
using DevExpress.Web.ASPxPager;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.DTO;
using FET.POS.Model.Common;
using System.Runtime.Serialization;

public partial class VSS_LEA_LEA03 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            GetStoreInfo();
            bindMasterData();
            cbbStoreNo.Value   = logMsg.STORENO ;
        }
    }

    private void GetStoreInfo()
    {
        DataTable dt = new DataTable();
        dt = new Store_Facade().Get_Store("");
        cbbStoreNo.DataSource = dt;
        cbbStoreNo.TextField = "STORENAME";
        cbbStoreNo.ValueField = "STORE_NO";
        cbbStoreNo.DataBind();

        cbbStoreNo.SelectedIndex = 0;
    }

    private void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        
        dtResult = new LEA03_Facade().Query_Rent_M( StringUtil.CStr(cbbStoreNo.SelectedItem.Value) ,
                                                        txtCustName.Text ,
                                                        txtMsisdn.Text ,
                                                        cbBookingToday.Checked);
        Session["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            ASPxButton btnSelect = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["BUTTON"], "btnSelect") as ASPxButton;

            string RentStatus = StringUtil.CStr(e.GetValue("RENT_STATUS"));
            //租賃狀態
            //00:未存檔
            //10:已預約
            //20:出租中
            //30:設備歸還結案
            //40:預約取消
            //50:須賠償
            switch (RentStatus)
            {
                case "10":
                    btnSelect.Enabled = true;
                    break;
                case "20":
                    btnSelect.Enabled = true;
                    break;
                default:
                    btnSelect.Enabled = false;
                    break;
            }
        }
    }

}
