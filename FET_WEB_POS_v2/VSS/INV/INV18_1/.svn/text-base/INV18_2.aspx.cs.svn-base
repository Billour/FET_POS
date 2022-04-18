using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using System.Collections;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;

using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;

public partial class VSS_INV_INV18_2 : Popup
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["gvMaster"] = getMasterData();
        bindMasterData();
    }

    protected void bindMasterData()
    {
        gvMaster.DataSource = ViewState["gvMaster"];
        gvMaster.DataBind();
    }

    private DataTable getMasterData()
    {
        return new INV18_1_Facade().Query_SADJ_REASONMethodSet();
    }

    protected void OkButton_Click(object sender, EventArgs e)
    {
        if (gvMaster.FocusedRowIndex > -1)
        {
            object key = gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName);
            SetReturnValue(StringUtil.CStr(key));
        }
        else
        {
            SetReturnValue(string.Empty);
        }
    }

    protected void grid_SelectionChanged(object sender, EventArgs e)
    {
        List<object> keys = gvMaster.GetSelectedFieldValues("STOCKADJ_DESCRIPTION");
        int i = gvMaster.FocusedRowIndex;
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = getMasterData();
        grid.DataBind();
    }
}
