using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using Advtek.Utility;

public partial class VSS_Common_PromotionsPopup : Popup
{
    protected void Page_Load(object sender, EventArgs e)
    {        
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        okButton.Visible = true;
        cancelButton.Visible = true;
        gvMaster.Settings.ShowVerticalScrollBar = true;
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();       
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("促銷代碼", typeof(string));
        dtResult.Columns.Add("促銷名稱", typeof(string));
        dtResult.Columns.Add("促銷類別", typeof(string));
        dtResult.Columns.Add("生效日期", typeof(string));
        dtResult.Columns.Add("失效日期", typeof(string));

        for (int i = 0; i < 5; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = i;
            NewRow["促銷代碼"] = "A0000" + i;
            NewRow["促銷名稱"] = "促銷名稱" + i;
            NewRow["促銷類別"] = "促銷類別" + i;
            NewRow["生效日期"] = "2010/07/01";
            NewRow["失效日期"] = "2010/09/01";
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }


    protected void radioChoose_CheckedChanged(object sender, EventArgs e)
    {
        //bindDropDownListData(DropDownList1, Label1, Label2, Label3,"摩羅拉手機");
        bindDropDownListData(true, DropDownList1, Label1, Label2, Label3, "摩羅拉手機");
        bindDropDownListData(false, DropDownList2, Label4, Label5, Label6, "商品2_");
        bindDropDownListData(false, DropDownList3, Label7, Label8, Label9, "商品3_");
        bindDropDownListData(false, DropDownList4, Label10, Label11, Label12, "商品4_");
        bindDropDownListData(false, DropDownList5, Label13, Label14, Label15, "商品5_");
        bindDropDownListData(false, DropDownList6, Label16, Label17, Label18, "商品6_");

        productDetail.Style["display"] = "";
    }


    #region 針對DropDownList系列的Bind資料相關函式
    /// <summary>
    /// Bind下拉選單的資料
    /// </summary>
    /// <param name="myDropdownlist"></param>
    /// <param name="lblName"></param>
    /// <param name="lblStock"></param>
    /// <param name="lblPrice"></param>
    /// <param name="DataName"></param>
    private void bindDropDownListData(bool IsEnabled, DropDownList myDropdownlist, Label lblName, Label lblStock, Label lblPrice, string DataName)
    {
        myDropdownlist.Items.Clear();
        myDropdownlist.Items.Add(new ListItem("-請選擇-", ""));

        for (int i = 0; i < 6; i++)
        {
            myDropdownlist.Items.Add(new ListItem("A000" + (i * 2), DataName + i));
        }
        myDropdownlist.SelectedIndex = -1;
        myDropdownlist.Enabled = IsEnabled;
        lblName.Text = "";
        lblStock.Text = "";
        lblPrice.Text = "";
    }

    /// <summary>
    /// 下拉選單選了之後要Bind後面的商品資料
    /// </summary>
    /// <param name="MyDropDownList"></param>
    /// <param name="lblName"></param>
    /// <param name="lblStock"></param>
    /// <param name="lblPrice"></param>
    private void bindProductChooseInfo(DropDownList MyDropDownList, Label lblName, Label lblStock, Label lblPrice)
    {
        int index = MyDropDownList.SelectedIndex;
        string ddlText = MyDropDownList.SelectedItem.Text;
        string ddlValue = MyDropDownList.SelectedValue;

        string ProductName = string.Empty;
        int intStock = 0;
        int intPrice = 0;
        if (!string.IsNullOrEmpty(ddlValue))
        {
            ProductName = ddlText + "名稱";
            intStock = index + 1;
            intPrice = 1000 * (index + 1);
        }
        lblName.Text = ProductName;
        lblStock.Text = StringUtil.CStr(intStock);
        lblPrice.Text = StringUtil.CStr(intPrice);
    }
    #endregion

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindProductChooseInfo(DropDownList1, Label1, Label2, Label3);
        DropDownList2.Enabled = true;
        //bindDropDownListData(DropDownList2, Label4, Label5, Label6,"商品2_");
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindProductChooseInfo(DropDownList2, Label4, Label5, Label6);
        DropDownList3.Enabled = true;
        //bindDropDownListData(DropDownList3, Label7, Label8, Label9, "商品3_");
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindProductChooseInfo(DropDownList3, Label7, Label8, Label9);
        DropDownList4.Enabled = true;
        // bindDropDownListData(DropDownList4, Label10, Label11, Label12, "商品4_");
    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindProductChooseInfo(DropDownList4, Label10, Label11, Label12);
        DropDownList5.Enabled = true;
        //bindDropDownListData(DropDownList5, Label13, Label14, Label15, "商品5_");
    }
    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindProductChooseInfo(DropDownList5, Label13, Label14, Label15);
        DropDownList6.Enabled = true;
        //bindDropDownListData(DropDownList6, Label16, Label17, Label18, "商品6_");
    }
    protected void DropDownList6_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindProductChooseInfo(DropDownList6, Label16, Label17, Label18);

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
        //List<object> keys = gvMaster.GetSelectedFieldValues("商品編號");
        //int i = gvMaster.FocusedRowIndex;

        //bindDropDownListData(DropDownList1, Label1, Label2, Label3,"摩羅拉手機");
        bindDropDownListData(true, DropDownList1, Label1, Label2, Label3, "摩羅拉手機");
        bindDropDownListData(false, DropDownList2, Label4, Label5, Label6, "商品2_");
        bindDropDownListData(false, DropDownList3, Label7, Label8, Label9, "商品3_");
        bindDropDownListData(false, DropDownList4, Label10, Label11, Label12, "商品4_");
        bindDropDownListData(false, DropDownList5, Label13, Label14, Label15, "商品5_");
        bindDropDownListData(false, DropDownList6, Label16, Label17, Label18, "商品6_");

        productDetail.Style["display"] = "";
    }


    //protected void OkButton_Click(object sender, EventArgs e)
    //{
    //    if (grid.FocusedRowIndex > -1)
    //    {
    //        object key = grid.GetRowValues(grid.FocusedRowIndex, grid.KeyFieldName);
    //        SetReturnValue(StringUtil.CStr(key));
    //    }
    //    else
    //    {
    //        SetReturnValue(string.Empty);
    //    }
    //}

    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = getMasterData();
        grid.DataBind();
    }
    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        //aspxChkOnClick('gvMaster_cell0_0_radioChoose')" 
        if (e.VisibleIndex > -1)
        {
            ASPxRadioButton radioChoose = (ASPxRadioButton)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns[0], "radioChoose");
            radioChoose.Attributes.Add("onclick", "aspxChkOnClick('" + radioChoose.ClientID + "')");
        }


    }
}
