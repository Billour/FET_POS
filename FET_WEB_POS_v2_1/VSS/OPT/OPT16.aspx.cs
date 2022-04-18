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

public partial class VSS_OPT_OPT16 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            bindMasterData(0);
            //this.Literal2.Text = "*" + Resources.WebResources.ListCorrespondCode;
        }
    }
    protected void bindMasterData(int tempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult = GetMasterData(tempCount);
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }

    private DataTable GetMasterData(int tempCount)
    {

        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("活動代號", typeof(string));
        dtResult.Columns.Add("活動名稱", typeof(string));
        dtResult.Columns.Add("HappyGo卡號", typeof(string));
        dtResult.Columns.Add("異常原因", typeof(string));
        
        if (tempCount > 0)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["活動代號"] = "800000003";
            NewRow["活動名稱"] = "活動名稱";
            NewRow["HappyGo卡號"] = "HappyGo卡號";
            NewRow["異常原因"] = "";
            dtResult.Rows.Add(NewRow);
        }

        return dtResult;
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        Session["gvMaster"] = GetMasterData(2);
        bindMasterData(2);
    }

    protected void gvMaster_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //GridPageSize = int.Parse(e.Parameters);
        gvMaster.SettingsPager.PageSize = int.Parse(e.Parameters);
        gvMaster.DataBind();
    }

}
