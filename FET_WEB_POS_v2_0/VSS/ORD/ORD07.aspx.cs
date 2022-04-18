using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using DevExpress.Web.ASPxGridView;

public partial class VSS_ORD07_ORD07 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bindMasterData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("主配單號", typeof(string));
        dtResult.Columns.Add("主配日期", typeof(string));
        dtResult.Columns.Add("出貨倉別", typeof(string));
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));

        string[] array0 = { "未儲存", "已存檔", "配送確認", "已作廢" };

        for (int i = 0; i < 10; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["主配單號"] = "HO10081700" + i;
            NewRow["主配日期"] = "2010/07/12";
            NewRow["出貨倉別"] = "Retail-北";
            NewRow["狀態"] = "已存檔";
            NewRow["更新人員"] = "王小明";
            NewRow["更新日期"] = "2010/07/13";
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    protected void gvMasterDV_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView gv = (ASPxGridView)sender;
        gv.DataSource = ViewState["gvMaster"] as DataTable;
        gv.DataBind();
    }
    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        //取得控制項裏的值出來
        DevExpress.Web.ASPxGridView.GridViewDataTextColumn col = new GridViewDataTextColumn();
        col = (GridViewDataTextColumn)((ASPxGridView)sender).Columns["主配單號"];       
        HyperLink hl = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, col, "hlkdno1") as HyperLink;
        if (hl != null)
        {
            hl.NavigateUrl = "~/VSS/ORD/ORD08.aspx?dno=" + hl.Text;
        }
         
    }
    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
    }
}
