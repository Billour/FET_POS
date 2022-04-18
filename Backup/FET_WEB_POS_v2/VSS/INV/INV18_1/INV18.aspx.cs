using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using System.Web.Configuration;
public partial class VSS_INV_INV18 : BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (logMsg.ROLE_TYPE != WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);
            btnClear.Enabled = false;
            btnSearch.Enabled = false;
            
            return;
        }


        if (!IsPostBack && !IsCallback)
        {
            // 繫結空的資料表，以顯示表頭欄位
            gvMaster.DataSource = new INV18_StockADJ_DTO().STOCKADJM;
            gvMaster.DataBind();
            if (StringUtil.CStr(Session["DOIMPOT"]) == "true")
            {
                Session["DOIMPOT"] = "false";
               

            }
            if (!IsPostBack && !Page.IsCallback)
            {

                ViewState["STATUS"] = "10";
                gvMaster.DataSource = getMasterData();
                gvMaster.DataBind();
            }
            
        }
    }

    protected void bindMasterData()
    {
        gvMaster.DataSource = getMasterData();
        gvMaster.DataBind();
    }

    private DataTable getMasterData()
    {
        DataTable dtMaster = new DataTable();

        INV18_1_Facade _INV18_1_Facade = new INV18_1_Facade();

        dtMaster = _INV18_1_Facade.Query_SADJMethodSet(this.txtADJNO.Text,
                                                        this.txtSTOREName.Text,
                                                        this.txtS_DATE.Text,
                                                        this.txtE_DATE.Text,
                                                        this.txtS_PRODNO.Text,
                                                        this.txtE_PRODNO.Text,
                                                        StringUtil.CStr(ViewState["STATUS"]));

        return dtMaster;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["STATUS"] = "";
        bindMasterData();
        gvMaster.PageIndex = 0;
    }

    //匯出
    protected void btnExport_Click(object sender, EventArgs e)
    {
        INV18_1_Facade INV18_1_Facade = new INV18_1_Facade();

        DataTable dt = INV18_1_Facade.ExPort_SADJMethodSet(this.txtADJNO.Text,
                                                            this.txtSTOREName.Text,
                                                            this.txtS_DATE.Text,
                                                            this.txtE_DATE.Text,
                                                            this.txtS_PRODNO.Text,
                                                            this.txtE_PRODNO.Text,
                                                            StringUtil.CStr(ViewState["STATUS"]));
        string filename = new Output().Print("XLS", "庫存調整作業<td></td><td>匯出時間：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "</td>", null, dt, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("Inventory_Adjustment_"+ DateTime.Now.ToString("yyyy/MM/dd") +".xls")); 
    

    }

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            e.Row.Cells[5].Text = new Employee_Facade().GetEmpName(StringUtil.CStr(e.GetValue("MODI_USER")));
        }
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        bindMasterData();
    }
    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            DevExpress.Web.ASPxGridView.GridViewDataTextColumn col = new GridViewDataTextColumn();
            col = (GridViewDataTextColumn)((ASPxGridView)sender).Columns["STKCHKNO"];

            HyperLink hl = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, col, "HyperLink1") as HyperLink;
            if (hl != null)
            {
                string encryptUrl = Utils.Param_Encrypt("dno=" + hl.Text);
                string Url = string.Format("~/VSS/INV/INV18_1/INV18_1.aspx?Param={0}", encryptUrl);

                hl.NavigateUrl = Url;
            }
        }

    }
}
