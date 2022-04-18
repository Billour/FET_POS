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

public partial class VSS_Common_EmployeesDepartmentPopup : Popup
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !IsCallback)
        {
            // 繫結空的資料表，以顯示表頭欄位
            grid.DataSource = new DataTable();
            grid.DataBind();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
        grid.FocusedRowIndex = -1;
        grid.PageIndex = 0;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        EmpNoTextBox.Text = "";
        EmpNameTextBox.Text = "";
        grid.DataSource = new DataTable();
        grid.DataBind();
    }

    protected void BindData()
    {
        grid.DataSource = new Employee_Facade().Query_EmployeesDepartment(EmpNoTextBox.Text, EmpNameTextBox.Text); 
        grid.DataBind();
    }
    private DataTable GetMasterData()
    {
        DataTable dtResult = new DataTable();
        return dtResult;
    }

    protected string GetFieldValue(object item)
    {
        WebDescriptorRowBase row = item as WebDescriptorRowBase;
        return StringUtil.CStr(grid.GetRowValues(row.VisibleIndex, grid.KeyFieldName));
    }

    protected string GetFieldChecked(object item)
    {
        if (Session["FocusedRow"] == null)
            return string.Empty;

        WebDescriptorRowBase row = item as WebDescriptorRowBase;
        object obj = grid.GetRowValues(row.VisibleIndex, grid.KeyFieldName);
        return Session["FocusedRow"] == obj ? "checked" : string.Empty;
    }


    protected void OkButton_Click(object sender, EventArgs e)
    {
        if (grid.FocusedRowIndex > -1)
        {

            object key = grid.GetRowValues(grid.FocusedRowIndex, grid.KeyFieldName);
            SetReturnValue(StringUtil.CStr(key));
            object key2 = grid.GetRowValues(grid.FocusedRowIndex, "EMPNAME");
            if (key2 != null) { SetReturnValue2(StringUtil.CStr(key)); };

            SetReturnOKScript();

        }
        else
        {
            SetReturnValue(string.Empty);
            SetReturnValue2(string.Empty);
        }
        //string No = "";
        //string Name = "";
        //List<object> keyValues = this.grid.GetSelectedFieldValues(grid.KeyFieldName);
        //foreach (string skey in keyValues)
        //{
        //    No = No + skey + ",";
        //    Name = Name + grid.GetRowValues(grid.FocusedRowIndex, "EMPNAME") + ",";
        //}
        //if (No == null || No == "")
        //{
        //    SetReturnValue(string.Empty);
        //    SetReturnValue2(string.Empty);
        //}else
        //{
        //    SetReturnValue(No.Substring(0, No.Length - 1));
        //    SetReturnValue2(Name.Substring(0, Name.Length - 1));
        //    SetReturnOKScript();
        //}
    }


    /// <summary>
    /// 主GridView的分頁變更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.EmpNoTextBox.Text = "";
        this.EmpNameTextBox.Text = "";
    }
}
