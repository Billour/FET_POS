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

public partial class VSS_INV_INV04 : BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            B_DATE.Text = DateTime.Today.AddDays(-30).ToString("yyyy/MM/dd");
            E_DATE.Text = DateTime.Today.ToString("yyyy/MM/dd");

            //取得空的資料表
            gvMaster.DataSource = new INV05_RTNM_DTO().RTNM;
            gvMaster.DataBind();
        }

    }

    private void bindMasterData()
    {
        gvMaster.DataSource = getMasterData();
        gvMaster.DataBind();
    }

    private DataTable getMasterData()
    {
        DataTable dtMaster = new DataTable();
        dtMaster = new INV05_Facade().Query_RTNMMethodSet(RTN_NO.Text, this.txtStoreNo.Text, this.txtProdNo.Text,
                                   this.B_DATE.Text, this.E_DATE.Text, StringUtil.CStr(this.RTN_STATUS.SelectedItem.Value));
        return dtMaster;
    }

    #region Button 觸發事件

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        gvMaster.PageIndex = 0;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {

        ExportExcelData eel = new ExportExcelData();
        this.Page.Controls.Add(eel);
        eel.ExportExcel(new INV05_Facade().ExportWeightDistribute(RTN_NO.Text, this.txtStoreNo.Text, this.txtProdNo.Text,
                                        B_DATE.Text, E_DATE.Text, StringUtil.CStr(RTN_STATUS.SelectedItem.Value)));

    }

    #endregion

    #region gvMaster 觸發事件

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        bindMasterData();
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {

        if (e.RowType == GridViewRowType.Data)
        {
            string STATUS = StringUtil.CStr(e.GetValue("STATUS"));
            if (STATUS == "已傳輸")
            {
                e.Row.Style.Add(HtmlTextWriterStyle.Color, "red");
            }

            DevExpress.Web.ASPxGridView.GridViewDataTextColumn col = new GridViewDataTextColumn();
            col = (GridViewDataTextColumn)((ASPxGridView)sender).Columns["RTNNO"];       

            HyperLink hl = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, col, "hlkdno1") as HyperLink;
            if (hl != null)
            {
                string encryptUrl = Utils.Param_Encrypt("dno=" + hl.Text);
                string Url = string.Format("~/VSS/INV/INV05/INV05.aspx?Param={0}", encryptUrl);

                hl.NavigateUrl = Url;
            }
        }

    }

    #endregion
}
