using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using FET.POS.Model.Facade.FacadeImpl;
using Advtek.Utility;

public partial class VSS_Common_CostCenterPopup : Popup
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            grid.DataSource = new DataTable();
            grid.DataBind();
        }
    }


    protected void BindData()
    {
        grid.DataSource = GetMasterData();
        grid.DataBind();
    }
 
    private DataTable GetMasterData()
    {
       DataTable dtResult = new DataTable();
       dtResult = new CostCenter_Facade().Query_CostCenter(this.txtCUSTNO.Text.Trim(), this.txtCUSTNAME.Text.Trim());
       return dtResult;
    }
    protected void okButton_Click(object sender, EventArgs e)
    {
        if (grid.FocusedRowIndex > -1)
        {
            object key = grid.GetRowValues(grid.FocusedRowIndex, grid.KeyFieldName);
            SetReturnValue(StringUtil.CStr(key));
        }
        else
        {
            SetReturnValue(string.Empty);
        }
    }
    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = GetMasterData();
        grid.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
}
