using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_OPT_OPT05_Import : System.Web.UI.Page
{
    protected void btnImport_Click(object sender, EventArgs e)
    {
        bindMasterData();
        btnCommit.Visible = true;
        btnCalcel.Visible = true;
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
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("用途", typeof(string));
        dtResult.Columns.Add("所屬年月起", typeof(string));
        dtResult.Columns.Add("所屬年月訖", typeof(string));
        dtResult.Columns.Add("字軌", typeof(string));
        dtResult.Columns.Add("起始編號", typeof(string));
        dtResult.Columns.Add("終止編號", typeof(string));
        dtResult.Columns.Add("異常原因", typeof(string));


        DataRow NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "2101";
        NewRow["門市名稱"] = "";
        NewRow["用途"] = "連線";
        NewRow["所屬年月起"] = "201001";
        NewRow["所屬年月訖"] = "201002";
        NewRow["字軌"] = "AA";
        NewRow["起始編號"] = "00000001";
        NewRow["終止編號"] = "00002000";
        NewRow["異常原因"] = "店組不存在";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "2010";
        NewRow["門市名稱"] = "遠企";
        NewRow["用途"] = "連線";
        NewRow["所屬年月起"] = "201003";
        NewRow["所屬年月訖"] = "201004";
        NewRow["字軌"] = "AA";
        NewRow["起始編號"] = "00001000";
        NewRow["終止編號"] = "00003000";
        NewRow["異常原因"] = "發票號區間重覆";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "2010";
        NewRow["門市名稱"] = "遠企";
        NewRow["用途"] = "連線";
        NewRow["所屬年月起"] = "201005";
        NewRow["所屬年月訖"] = "201006";
        NewRow["字軌"] = "AB";
        NewRow["起始編號"] = "00000001";
        NewRow["終止編號"] = "00002000";
        NewRow["異常原因"] = "";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    protected void gvMaster_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = e.Row.DataItem as DataRowView;
            if (!string.IsNullOrEmpty(drv["異常原因"].ToString()))
            {
                e.Row.Style[HtmlTextWriterStyle.Color] = "red";
            }
        }
    }
}
