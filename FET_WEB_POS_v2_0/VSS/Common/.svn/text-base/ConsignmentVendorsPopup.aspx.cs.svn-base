using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;

public partial class VSS_Common_ConsignmentVendorsPopup : Popup
{
    protected void Page_Load(object sender, EventArgs e)
    {             
    }

    protected void bindMasterData()
    {
        DataTable dtResult = getMasterData();
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Clear();
        dtResult.Columns.Add("廠商代號", typeof(string));
        dtResult.Columns.Add("廠商名稱", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["廠商代號"] = "2101";
        NewRow["廠商名稱"] = "廠商1";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["廠商代號"] = "2103";
        NewRow["廠商名稱"] = "廠商2";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["廠商代號"] = "2104";
        NewRow["廠商名稱"] = "廠商3";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["廠商代號"] = "2106";
        NewRow["廠商名稱"] = "廠商4";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["廠商代號"] = "2107";
        NewRow["廠商名稱"] = "廠商5";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["廠商代號"] = "2108";
        NewRow["廠商名稱"] = "廠商6";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }

    protected void OkButton_Click(object sender, EventArgs e)
    {
        if (gvMaster.FocusedRowIndex > -1)
        {
            object key = gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName);
            SetReturnValue(key.ToString());
        }
        else
        {
            SetReturnValue(string.Empty);
        }
    }

    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = getMasterData();
        grid.DataBind();
    }
}
