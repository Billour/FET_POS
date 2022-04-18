using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_SAL_SAL01_inputIMEIData : Popup//System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindMasterData();
            //btnCommit.Visible = true;
        }
    }

    protected void OkButton_Click(object sender, EventArgs e)
    {
        //if (grid.FocusedRowIndex > -1)
        //{
        //    object key = grid.GetRowValues(grid.FocusedRowIndex, grid.KeyFieldName);
        //    SetReturnValue(key.ToString());
        //}
        //else
        //{
            
        //}

        SetReturnValue("7780944056407860");
    }
 
    protected void bindMasterData()
    {
        grid.DataSource = getMasterData();
        grid.DataBind();
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("IMEI", typeof(string));

        for (int i = 0; i < 5; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["IMEI"] = string.Format("778{0}944{0}564{0}786{0}", i);
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        bindMasterData();
    }
}
