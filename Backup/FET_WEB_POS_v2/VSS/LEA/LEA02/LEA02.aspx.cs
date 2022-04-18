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

public partial class VSS_LEA_LEA02 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack && !IsCallback)
        {
            GetZoneInf0();
            GetStoreInfo(StringUtil.CStr(cbbZone.SelectedItem.Value));
            bindMasterData();
            this.btnSearch.Attributes.Add("style", "text-decoration:color:blue;");

            //if (logMsg.ROLE_TYPE != StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"]))
            //{
            //    DataTable dt = new Store_Facade().Query_StoreZone_ByKey(logMsg.STORENO);

            //    if (dt.Rows.Count > 0)
            //    {
            //        cbbStoreNo.SelectedItem.Text  = StringUtil.CStr(dt.Rows[0]["STORENAME"]);
            //        cbbZone.SelectedIndex = cbbZone.Items.IndexOfValue(StringUtil.CStr(dt.Rows[0]["ZONE"]));
            //    }
            //    cbbZone.Enabled = false;
            //    cbbStoreNo.Enabled = false;
            //}
        }
    }

    private void GetZoneInf0()
    {
        DataTable dt = new DataTable();
        dt = Common_PageHelper.getZone(true);

        cbbZone.DataSource = dt;
        cbbZone.TextField = "ZONE_NAME";
        cbbZone.ValueField = "ZONE";
        cbbZone.DataBind();

        cbbZone.SelectedIndex = 0;
    }

    private void GetStoreInfo(string Zone)
    {
        DataTable dt = new DataTable();
        dt = new Store_Facade().Get_Store(Zone);
        cbbStoreNo.DataSource = dt;
        cbbStoreNo.TextField = "STORENAME";
        cbbStoreNo.ValueField = "STORE_NO";
        cbbStoreNo.DataBind();

        cbbStoreNo.SelectedIndex = 0;
    }

    private void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        string DeviceType = string.Empty;
        if (!string.IsNullOrEmpty(StringUtil.CStr(rbDeviceType.SelectedItem.Value)))
        {
            DeviceType = StringUtil.CStr(rbDeviceType.SelectedItem.Value);
        }
        dtResult = new LEA02_Facade().Query_LeaseStock_M(DeviceType,
                                                        StringUtil.CStr(cbbZone.SelectedItem.Value),
                                                        StringUtil.CStr(cbbStoreNo.SelectedItem.Value));
        Session["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }

    private DataTable getDetail(string DeviceType, string StoreNo)
    {
        DataTable dtResult = new DataTable();
        
        dtResult = new LEA02_Facade().Query_LeaseStock_D(DeviceType,
                                                StoreNo);

        return dtResult;
    }

    protected void cbbZone_OnValueChanged(object sender, EventArgs e)
    {
        GetStoreInfo(StringUtil.CStr(cbbZone.SelectedItem.Value));
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvMaster.Visible = true;
        bindMasterData();
    }

    protected void gvMaster_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Detail)
        {
            // 繫結明細資料表  
            Session["gvDetail"] = null;
            ASPxGridView gvDetail = e.Row.FindChildControl<ASPxGridView>("gvDetail");
            string StoreNo = StringUtil.CStr(e.GetValue("STORE_NO"));
            string DeviceType = StringUtil.CStr(e.GetValue("DEVICE_TYPE"));
            DataTable dt = getDetail(DeviceType, StoreNo);
            Session["gvDetail"] = dt;
            gvDetail.DataSource = dt;
            gvDetail.DataBind();
        }

    }

    protected void gvDetail_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        DataTable dtDetail = new DataTable();
        if (Session["gvDetail"] != null)
        {
            dtDetail = Session["gvDetail"] as DataTable;
        }


        if (e.RowType == GridViewRowType.Data)
        {
            if (dtDetail.Rows.Count > 0)
            {
                
                DevExpress.Web.ASPxGridView.ASPxGridView gvDetail = gvMaster.FindChildControl<DevExpress.Web.ASPxGridView.ASPxGridView>("gvDetail");
                ASPxButton btnReserve = gvDetail.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvDetail.Columns["BUTTON"], "btnReserve") as ASPxButton;
                ASPxButton btnAdd = gvDetail.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvDetail.Columns["BUTTON"], "btnAdd") as ASPxButton;
                string RentStatus = StringUtil.CStr(e.GetValue("RENT_STATUS"));
                //租賃狀態
                //10:有效
                //20:預約中
                //20:租賃出借中
                //30:設備維修中
                //40:配件遺失,暫停租賃
                //50:領用到期
                //99:失效作廢
                switch (RentStatus)
                {
                    case "10":
                        btnReserve.Enabled = true;
                        btnAdd.Enabled = true;
                        break;
                    case "20":
                        btnReserve.Enabled = false;
                        btnAdd.Enabled = true;
                        break;
                    default:
                        btnReserve.Enabled = false;
                        btnAdd.Enabled = false;
                        break;
                }
                

            }


        }
    }
    
    protected void gvDetail_PageIndexChanged(object sender, EventArgs e)
    {

    }
}
