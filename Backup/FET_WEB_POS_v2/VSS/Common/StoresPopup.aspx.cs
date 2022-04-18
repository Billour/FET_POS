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
using DevExpress.Web.Data;
using Advtek.Utility;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;


public partial class VSS_Common_StoresPopup : Popup
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // 繫結空的資料表，以顯示表頭欄位
            grid.DataSource = new DataTable();
            grid.DataBind();

            BindZoneType();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
        grid.FocusedRowIndex = -1;
        grid.PageIndex = 0;
    }

    protected void BindData()
    {
        grid.DataSource = new Store_Facade().Query_Store(this.txtStoreNo.Text, this.txtStoreName.Text, StringUtil.CStr(this.ddlDistrict.SelectedItem.Value));
        grid.DataBind();
    }

    /// <summary>
    /// 繫結區域別資料
    /// </summary>
    private void BindZoneType()
    {
        this.ddlDistrict.TextField = "ZONE_NAME";
        this.ddlDistrict.ValueField = "ZONE";
        this.ddlDistrict.DataSource = Common_PageHelper.getZone(true);
        this.ddlDistrict.DataBind();
        this.ddlDistrict.SelectedIndex = 0;
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {            
        if (grid.FocusedRowIndex > -1)
        {
            //object key = grid.GetRowValues(grid.FocusedRowIndex, grid.KeyFieldName);
            //SetReturnValue(StringUtil.CStr(key));
            //object key2 = grid.GetRowValues(grid.FocusedRowIndex, "STORENAME");
            //if (key2 != null) { SetReturnValue2(StringUtil.CStr(key)); };

            //SetReturnOKScript();

            object key;
            switch (KeyFieldValue1.Trim().ToLower())
            {

                case "name": //要傳回門市名稱
                    key = grid.GetRowValues(grid.FocusedRowIndex, KeyFieldValue2.Trim());
                    break;

                default:
                    key = grid.GetRowValues(grid.FocusedRowIndex, grid.KeyFieldName);
                    break;
            }


            SetReturnValue(StringUtil.CStr(key));
            //object key2 = grid.GetRowValues(grid.FocusedRowIndex, "PRODNAME");
            //if (key2 != null) { SetReturnValue2(StringUtil.CStr(key)); };

            SetReturnOKScript();

        }
        else
        {
            SetReturnValue(string.Empty);
            SetReturnValue2(string.Empty);
        }
    }

    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.txtStoreNo.Text = "";
        this.txtStoreName.Text = "";
    }
}
