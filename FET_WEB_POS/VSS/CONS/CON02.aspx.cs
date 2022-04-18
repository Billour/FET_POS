using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Advtek.Utility;
using System.Collections;
public partial class VSS_CON02_Default : Advtek.Utility.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {                        
            if (!string.IsNullOrEmpty(Request.QueryString["No"]))
            {
                ViewState["ReturnUrl"] = Request.UrlReferrer.ToString();
                lblSupplierNo.Text = Request.QueryString["No"];

                string code = Convert.ToChar(int.Parse(lblSupplierNo.Text.Replace("AC", "")) + 64).ToString();

                txtSupplierName.Text = "廠商" + code;
                txtFETOwner.Text = code + "先生";
                txtContact.Text = code + "小姐";
                txtPhone.Text = "02-27545533";
                txtAddress.Text = "台北市復興南路一段303號";
                txtContractNo.Text = "C10224566";
                RadioButton1.Checked = true;
                txtUnifiedBusinessNo.Text = "89014356";
                txtSupplierCode.Text = lblSupplierNo.Text.Substring(0, 2);
                txtOwner.Text = code + "先生";
                txtOwnerPhone.Text = "";
                txtEmail.Text = "service@sample.com";
                txtFax.Text = "02-22345567";
                txtOwnerPhone.Text = "02-22345555";
                txtMinAmt.Text = "200000";
                lblStatus.Text = "已存檔";
                
                Random rnd = new Random();
                postbackDate_TextBox1.Text = DateTime.Today.AddDays(-rnd.Next(0, 30)).ToString("yyyy/MM/dd");
                postbackDate_TextBox2.Text = DateTime.Parse(postbackDate_TextBox1.Text).AddDays(rnd.Next(30, 90)).ToString("yyyy/MM/dd");

                DropDownList1.SelectedIndex = 1;
                DropDownList1_SelectedIndexChanged(DropDownList1, EventArgs.Empty);

                txtAcct1.Text = "10";
                txtAcct2.Text = "15";
                txtAcct3.Text = "200011";
                txtAcct4.Text = "200012";
                txtAcct5.Text = "5000";
                txtAcct6.Text = "5001";

                bindMasterData(1);
            }
            else
            {
                bindMasterData(1);
            }            
        }
    }
    protected void bindMasterData(int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData(TempCount);
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();

        ViewState["gvMaster1"] = getMasterData2(TempCount);
        GridView1.DataSource = (DataTable)ViewState["gvMaster1"];
        GridView1.DataBind();

        ViewState["gvMaster2"] = getGridView2Data();
        GridView2.DataSource = (DataTable)ViewState["gvMaster2"];
        GridView2.DataBind();

        ViewState["gvMaster3"] = getGridView3Data();
        GridView3.DataSource = (DataTable)ViewState["gvMaster3"];
        GridView3.DataBind();

        ViewState["gvMaster4"] = getGridView4Data();
        GridView4.DataSource = (DataTable)ViewState["gvMaster4"];
        GridView4.DataBind();
    }
    private DataTable getMasterData(int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("佣金比率", typeof(string));
        dtResult.Columns.Add("起始月份", typeof(string));
        dtResult.Columns.Add("結束月份", typeof(string));

        for (int i = 0; i < TempCount; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["佣金比率"] = "10";
            NewRow["起始月份"] = "07/2010";
            NewRow["結束月份"] = "10/2010";
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    private DataTable getMasterData2(int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("佣金比率", typeof(string));
        dtResult.Columns.Add("起始日期", typeof(string));
        dtResult.Columns.Add("結束日期", typeof(string));

        for (int i = 0; i < TempCount; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["佣金比率"] = "10";
            NewRow["起始日期"] = "2010/07/01";
            NewRow["結束日期"] = "2010/07/31";
            dtResult.Rows.Add(NewRow);
        }

        return dtResult;
    }

    private DataTable getGridView2Data()
    {        
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("級距項次", typeof(int));
        dtResult.Columns.Add("起金額級距", typeof(int));
        dtResult.Columns.Add("訖金額級距", typeof(int));
        dtResult.Columns.Add("佣金比率", typeof(string));
        dtResult.Columns.Add("開始日期", typeof(string));
        dtResult.Columns.Add("結束日期", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["級距項次"] = 1;
        NewRow["起金額級距"] = 0;
        NewRow["訖金額級距"] = 100000;
        NewRow["佣金比率"] = "10";
        NewRow["開始日期"] = "2010/08/01";
        NewRow["結束日期"] = "2010/08/31";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["級距項次"] = 2;
        NewRow["起金額級距"] = 100001;
        NewRow["訖金額級距"] = 200000;
        NewRow["佣金比率"] = "10";
        NewRow["開始日期"] = "2010/08/01";
        NewRow["結束日期"] = "2010/08/31";
        dtResult.Rows.Add(NewRow);
        
        return dtResult;
    }

    private DataTable getGridView3Data()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("會計科目", typeof(string)); 

        DataRow NewRow = dtResult.NewRow();
        NewRow["商品編號"] = "商品編號1";
        NewRow["商品名稱"] = "商品名稱1";
        NewRow["會計科目"] = "科目名稱";
        dtResult.Rows.Add(NewRow);
        return dtResult;
    }

    private DataTable getGridView4Data()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("信用卡別", typeof(string));
        dtResult.Columns.Add("手續費", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["項次"] = "1";
        NewRow["信用卡別"] = "VISA";
        NewRow["手續費"] = "2";
        dtResult.Rows.Add(NewRow);
        return dtResult;
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        TabContainer1.Visible = false;
        TabContainer2.Visible = false;

        
        txtAcct1.Enabled = txtAcct2.Enabled = txtAcct3.Enabled = txtAcct4.Enabled = txtAcct5.Enabled = txtAcct6.Enabled = true;
        txtMinAmt.Enabled = true;
        txtMinAmt.Style.Clear();
        for (int i = 1; i <= 6; i++)
        {
            TextBox tb = (TextBox)Page.FindControl("txtAcct" + i);
            tb.Text = "";
            tb.Style.Clear();
        }
        //cbMax.Enabled = cbMin.Enabled = true;
        //cbMax.Checked = cbMin.Checked = false;

        if (DropDownList1.SelectedIndex > 0) {
            /*
             寄售廠商
             
             總額抽成
      
             金額級距
             
             */

            switch (DropDownList1.SelectedIndex)
            {
                case 1: // 寄售廠商可編輯總金額上限、會計科目
                    for (int i = 1; i <= 6; i++)
                    {
                        TextBox tb = (TextBox)Page.FindControl("txtAcct" + i);
                        tb.Text = "";
                        tb.Style.Clear();
                        //tb.Style.Add("background-color", "Gray");
                    }
                    txtMinAmt.Style.Clear();
                    

                    //txtAcct1.ControlStyle.BackColor = txtAcct2.ControlStyle.BackColor = System.Drawing.Color.Gray;
                    txtAcct1.Enabled = txtAcct2.Enabled = txtAcct3.Enabled = txtAcct4.Enabled = txtAcct5.Enabled = txtAcct6.ReadOnly = true;
                    TabContainer1.Visible = true;
                    break;
              
                case 2:   //外部廠商不可編輯總金額底限、會計科目
                    for (int i = 1; i <= 6; i++)
                    {
                        TextBox tb = (TextBox)Page.FindControl("txtAcct" + i);
                        tb.Style.Add("background-color", "Gray");
                        //tb.ControlStyle.BackColor = System.Drawing.Color.Gray;
                        //tb.Style.Clear();
                    }
                  
                    txtMinAmt.Enabled = false;
                    //cbMax.Enabled = cbMin.Enabled = false;
                    //txtMinAmt.Text = "";
                    txtMinAmt.Style.Add("background-color", "Gray");
                    //txtMaxOrderQty.Style.Add("background-color", "Gray");
                    txtAcct1.Enabled = txtAcct2.Enabled = txtAcct3.Enabled = txtAcct4.Enabled = txtAcct5.Enabled = txtAcct6.ReadOnly = false;
                  

                    TabContainer2.Visible = true;
                    if(DropDownList1.SelectedIndex != 2)
                    {
                        TabPanel3.Visible = false;
                        TabPanel4.Visible = true;
                    }
                    break;
            }
        }
        else
        {

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        bindMasterData(1);
    }


    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        TextBox3.Visible= false;
        if (RadioButton2.Checked)
        {
            TextBox3.Visible = true;
            TextBox3.Enabled = true;
        }
    }

    protected void txtFETOwner_TextChanged(Object sender, EventArgs e)
    {
        lblDepartment.Text = "行銷部";
    }

    #region 選擇店組相關函式
    private void BindData(string strZone)
    {
        ddlSubZone.Items.Clear();
        ddlSubZone.Items.Insert(0, "請選擇");
        for (int i = 1; i < 6; i++)
        {
            ListItem ist = new ListItem(strZone + NumberUtil.CNumber(i.ToString()).Substring(0, 1) + "區", strZone + NumberUtil.CNumber(i.ToString()).Substring(0, 1) + "區");
            this.ddlSubZone.Items.Add(ist);
        }
    }

    private void BindListBox1(string strSubZone)
    {
        this.ListBox1.Items.Clear();
        for (int i = 1; i < 6; i++)
        {
            ListItem ist = new ListItem(strSubZone + NumberUtil.CNumber(i.ToString()).Substring(0, 1) + "店", strSubZone + NumberUtil.CNumber(i.ToString()).Substring(0, 1) + "區");
            this.ListBox1.Items.Add(ist);
        }
    }

    protected void ddlSubZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubZone.SelectedIndex == 0)
        {
            this.ListBox1.Items.Clear();
            ListBox2.Items.Clear();
            return;
        }
        BindListBox1(ddlSubZone.SelectedValue);
    }
    
    protected void btnAdd_Click(object sender, ImageClickEventArgs e)
    {
        ArrayList itemList = new ArrayList();
        foreach (ListItem x in ListBox1.Items)
        {
            if (x.Selected)
            {
                ListBox2.Items.Add(x);
                itemList.Add(x);
            }
        }
        foreach (object i in itemList)
        {
            ListBox1.Items.Remove((ListItem)i);
        }
    }

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        ArrayList itemList = new ArrayList();
        foreach (ListItem x in ListBox2.Items)
        {
            if (x.Selected)
            {
                ListBox1.Items.Insert(0, x);
                itemList.Add(x);
            }
        }
        foreach (object i in itemList)
        {
            ListBox2.Items.Remove((ListItem)i);
        }
    }
    #endregion

    #region 選擇店組相關函式
    protected void ddlSubZone_SelectedIndexChanged2(object sender, EventArgs e)
    {
        string strSubZone = DropDownList4.SelectedValue;

        if (DropDownList4.SelectedIndex == 0)
        {
            this.ListBox3.Items.Clear();
            ListBox4.Items.Clear();
            return;
        }
        for (int i = 1; i < 6; i++)
        {
            ListItem ist = new ListItem(strSubZone + NumberUtil.CNumber(i.ToString()).Substring(0, 1) + "店", strSubZone + NumberUtil.CNumber(i.ToString()).Substring(0, 1) + "區");
            this.ListBox3.Items.Add(ist);
        }
    }

    protected void btnAdd_Click2(object sender, ImageClickEventArgs e)
    {
        ArrayList itemList = new ArrayList();
        foreach (ListItem x in ListBox3.Items)
        {
            if (x.Selected)
            {
                ListBox4.Items.Add(x);
                itemList.Add(x);
            }
        }
        foreach (object i in itemList)
        {
            ListBox3.Items.Remove((ListItem)i);
        }
    }

    protected void btnBack_Click2(object sender, ImageClickEventArgs e)
    {
        ArrayList itemList = new ArrayList();
        foreach (ListItem x in ListBox4.Items)
        {
            if (x.Selected)
            {
                ListBox3.Items.Insert(0, x);
                itemList.Add(x);
            }
        }
        foreach (object i in itemList)
        {
            ListBox4.Items.Remove((ListItem)i);
        }
    }
    #endregion

    #region gvMaster 編輯/更新/取消 相關觸發事件
    protected void gvMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        //設定編輯欄位
        gridview.EditIndex = e.NewEditIndex;
        //Bind原查詢資料
        DataTable dt = ViewState["gvMaster"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }

    protected void gvMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView gridview = sender as GridView;

        //取得資料

        //更新資料庫

        //取消編輯狀態
        gridview.EditIndex = -1;

        //Bind新資料(重取資料)
        bindMasterData(1);
    }

    protected void gvMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        gridview.EditIndex = -1;
        //Bind原查詢資料
        DataTable dt = ViewState["gvMaster"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }

    protected void btnAdd1_Click(object sender, EventArgs e)
    {
        gvMaster.ShowFooter = true;
        gvMaster.ShowFooterWhenEmpty = true;
        System.Web.UI.HtmlControls.HtmlTableRow tr =
            gvMaster.FindChildControl<System.Web.UI.HtmlControls.HtmlTableRow>("trEmptyData");
        if (tr != null)
        {
            // 隱藏顯示文字訊息的表格列
            tr.Visible = false;
        }
        else
        {
            // 重新繫結資料
            ViewState["gvMaster"] = getMasterData(1);
            gvMaster.DataSource = (DataTable)ViewState["gvMaster"];
            gvMaster.DataBind();
        }
    }

    /// <summary>
    /// 取消新增-隱藏Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        gvMaster.ShowFooterWhenEmpty = false;
        gvMaster.ShowFooter = false;
        // 重新繫結資料
        if (gvMaster.Rows.Count == 0)
        {
            gvMaster.DataSource = new DataTable();
            gvMaster.DataBind();
        }
        else
        {
            ViewState["gvMaster"] = getMasterData(1);
            gvMaster.DataSource = (DataTable)ViewState["gvDetail1"];
            gvMaster.DataBind();
        }
    }

    #endregion

    #region GridView1 編輯/更新/取消 相關觸發事件
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        //設定編輯欄位
        gridview.EditIndex = e.NewEditIndex;
        //Bind原查詢資料
        DataTable dt = ViewState["gvMaster1"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView gridview = sender as GridView;        
        //取消編輯狀態
        gridview.EditIndex = -1;

        //Bind新資料(重取資料)
        bindMasterData(1);
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        gridview.EditIndex = -1;
        //Bind原查詢資料
        DataTable dt = ViewState["gvMaster1"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }

    protected void btnAdd2_Click(object sender, EventArgs e)
    {
        GridView1.ShowFooter = true;
        GridView1.ShowFooterWhenEmpty = true;
        System.Web.UI.HtmlControls.HtmlTableRow tr =
            GridView1.FindChildControl<System.Web.UI.HtmlControls.HtmlTableRow>("trEmptyData");
        if (tr != null)
        {
            // 隱藏顯示文字訊息的表格列
            tr.Visible = false;
        }
        else
        {
            // 重新繫結資料
            ViewState["gvMaster1"] = getMasterData2(1);
            GridView1.DataSource = (DataTable)ViewState["gvMaster1"];
            GridView1.DataBind();
        }
    }

    /// <summary>
    /// 取消新增-隱藏Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel2_Click(object sender, EventArgs e)
    {
        GridView1.ShowFooterWhenEmpty = false;
        GridView1.ShowFooter = false;
        // 重新繫結資料
        if (GridView1.Rows.Count == 0)
        {
            GridView1.DataSource = new DataTable();
            GridView1.DataBind();
        }
        else
        {
            ViewState["gvMaster1"] = getMasterData2(1);
            GridView1.DataSource = (DataTable)ViewState["gvMaster1"];
            GridView1.DataBind();
        }
    }

    #endregion

    #region GridView2 編輯/更新/取消 相關觸發事件
    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        //設定編輯欄位
        gridview.EditIndex = e.NewEditIndex;
        //Bind原查詢資料
        DataTable dt = ViewState["gvMaster2"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }

    protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView gridview = sender as GridView;

        //取得資料

        //更新資料庫

        //取消編輯狀態
        gridview.EditIndex = -1;

        //Bind新資料(重取資料)
        bindMasterData(1);
    }

    protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        gridview.EditIndex = -1;
        //Bind原查詢資料
        DataTable dt = ViewState["gvMaster2"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }

    protected void btnAdd3_Click(object sender, EventArgs e)
    {
        GridView2.ShowFooter = true;
        GridView2.ShowFooterWhenEmpty = true;
        System.Web.UI.HtmlControls.HtmlTableRow tr =
            GridView2.FindChildControl<System.Web.UI.HtmlControls.HtmlTableRow>("trEmptyData");
        if (tr != null)
        {
            // 隱藏顯示文字訊息的表格列
            tr.Visible = false;
        }
        else
        {
            // 重新繫結資料
            ViewState["gvMaster2"] = getGridView2Data();
            GridView2.DataSource = (DataTable)ViewState["gvMaster2"];
            GridView2.DataBind();
        }
    }

    /// <summary>
    /// 取消新增-隱藏Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel3_Click(object sender, EventArgs e)
    {
        GridView2.ShowFooterWhenEmpty = false;
        GridView2.ShowFooter = false;
        // 重新繫結資料
        if (GridView2.Rows.Count == 0)
        {
            GridView2.DataSource = new DataTable();
            GridView2.DataBind();
        }
        else
        {
            ViewState["gvMaster2"] = getGridView2Data();
            GridView2.DataSource = (DataTable)ViewState["gvMaster2"];
            GridView2.DataBind();
        }
    }

    #endregion

    #region GridView3 編輯/更新/取消 相關觸發事件
    protected void GridView3_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        //設定編輯欄位
        gridview.EditIndex = e.NewEditIndex;
        //Bind原查詢資料
        DataTable dt = ViewState["gvMaster3"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }

    protected void GridView3_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView gridview = sender as GridView;

        //取得資料

        //更新資料庫

        //取消編輯狀態
        gridview.EditIndex = -1;

        //Bind新資料(重取資料)
        bindMasterData(1);
    }

    protected void GridView3_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        gridview.EditIndex = -1;
        //Bind原查詢資料
        DataTable dt = ViewState["gvMaster3"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }


    protected void btnAdd4_Click(object sender, EventArgs e)
    {
        GridView3.ShowFooter = true;
        GridView3.ShowFooterWhenEmpty = true;
        System.Web.UI.HtmlControls.HtmlTableRow tr =
            GridView3.FindChildControl<System.Web.UI.HtmlControls.HtmlTableRow>("trEmptyData");
        if (tr != null)
        {
            // 隱藏顯示文字訊息的表格列
            tr.Visible = false;
        }
        else
        {
            // 重新繫結資料
            ViewState["gvMaster3"] = getGridView3Data();
            GridView3.DataSource = (DataTable)ViewState["gvMaster3"];
            GridView3.DataBind();
        }
    }

    /// <summary>
    /// 取消新增-隱藏Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel4_Click(object sender, EventArgs e)
    {
        GridView3.ShowFooterWhenEmpty = false;
        GridView3.ShowFooter = false;
        // 重新繫結資料
        if (GridView3.Rows.Count == 0)
        {
            GridView3.DataSource = new DataTable();
            GridView3.DataBind();
        }
        else
        {
            ViewState["gvMaster3"] = getGridView3Data();
            GridView3.DataSource = (DataTable)ViewState["gvMaster3"];
            GridView3.DataBind();
        }
    }


    #endregion

    #region GridView4 編輯/更新/取消 相關觸發事件
    protected void GridView4_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        //設定編輯欄位
        gridview.EditIndex = e.NewEditIndex;
        //Bind原查詢資料
        DataTable dt = ViewState["gvMaster4"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }

    protected void GridView4_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView gridview = sender as GridView;

        //取得資料

        //更新資料庫

        //取消編輯狀態
        gridview.EditIndex = -1;

        //Bind新資料(重取資料)
        bindMasterData(1);
    }

    protected void GridView4_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        gridview.EditIndex = -1;
        //Bind原查詢資料
        DataTable dt = ViewState["gvMaster4"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }

    protected void btnAdd5_Click(object sender, EventArgs e)
    {
        GridView4.ShowFooter = true;
        GridView4.ShowFooterWhenEmpty = true;
        System.Web.UI.HtmlControls.HtmlTableRow tr =
            GridView4.FindChildControl<System.Web.UI.HtmlControls.HtmlTableRow>("trEmptyData");
        if (tr != null)
        {
            // 隱藏顯示文字訊息的表格列
            tr.Visible = false;
        }
        else
        {
            // 重新繫結資料
            ViewState["gvMaster4"] = getGridView4Data();
            GridView4.DataSource = (DataTable)ViewState["gvMaster4"];
            GridView4.DataBind();
        }
    }

    /// <summary>
    /// 取消新增-隱藏Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel5_Click(object sender, EventArgs e)
    {
        GridView4.ShowFooterWhenEmpty = false;
        GridView4.ShowFooter = false;
        // 重新繫結資料
        if (GridView4.Rows.Count == 0)
        {
            GridView4.DataSource = new DataTable();
            GridView4.DataBind();
        }
        else
        {
            ViewState["gvMaster4"] = getGridView4Data();
            GridView4.DataSource = (DataTable)ViewState["gvMaster4"];
            GridView4.DataBind();
        }
    }
    #endregion


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ViewState["ReturnUrl"] != null)
        {
            Response.Redirect(ViewState["ReturnUrl"].ToString());
        }
        else
        {
            Response.Redirect("~/VSS/CONS/CON02.aspx");
        }
    }

}
