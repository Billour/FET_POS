using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Advtek.Utility;
using System.Collections;
using DevExpress.Web;
using DevExpress.Web.ASPxEditors;

public partial class VSS_CONS_CON02 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["No"]))
            {
                ViewState["ReturnUrl"] = Request.UrlReferrer.ToString();
                lblSupplierNo.Text = Request.QueryString["No"];

                string code = Convert.ToChar(int.Parse(lblSupplierNo.Text.Replace("AC", "")) + 64).ToString();

                txtSupplierName.Text = "廠商" + code;
                //txtFETOwner.Text = code + "先生";
                this.PopupControl1.Text = code + "先生";
                txtContact.Text = code + "小姐";
                txtPhone.Text = "02-27545533";
                txtAddress.Text = "台北市復興南路一段303號";
                txtContractNo.Text = "C10224566";
                //RadioButton1.Checked = true;
                cutoffDateRadioButtonList.SelectedIndex = 0;
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
                CooperationDateRangeFrom.Text = DateTime.Today.AddDays(-rnd.Next(0, 30)).ToString("yyyy/MM/dd");
                CooperationDateRangeTo.Text = DateTime.Parse(CooperationDateRangeFrom.Text).AddDays(rnd.Next(30, 90)).ToString("yyyy/MM/dd");

                vendorTypeComboBox.SelectedIndex = 1;
                DropDownList1_SelectedIndexChanged(vendorTypeComboBox, EventArgs.Empty);

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
                TabContainer1.Visible = false;
                TabContainer2.Visible = false;
                //bindMasterData(1);
            }
        }
        else
        {
            DataTable dtGvMaster = ViewState["gvMaster"] as DataTable;
            if (dtGvMaster != null)
            {
                gvMaster.DataSource = dtGvMaster;
                gvMaster.DataBind();
            }
            DataTable dtMaster1 = ViewState["gvMaster1"] as DataTable;
            if (dtMaster1 != null)
            {
                GridView1.DataSource = dtMaster1;
                GridView1.DataBind();
            }
            DataTable dtMaster2 = ViewState["gvMaster2"] as DataTable;
            if (dtMaster2 != null)
            {
                GridView2.DataSource = dtMaster2;
                GridView2.DataBind();
            }
            DataTable dtMaster3 = ViewState["gvMaster3"] as DataTable;
            if (dtMaster3 != null)
            {
                GridView3.DataSource = dtMaster3;
                GridView3.DataBind();
            }
            DataTable dtMaster4 = ViewState["gvMaster4"] as DataTable;
            if (dtMaster4 != null)
            {
                GridView4.DataSource = dtMaster4;
                GridView4.DataBind();
            }
        }

        if (this.PopupControl1.Text == "")
        {
            lblDepartment.Text = "";
        }
        else
        { lblDepartment.Text = "行銷部"; }


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
        dtResult.Columns.Add("開始日期", typeof(string));
        dtResult.Columns.Add("結束日期", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["商品編號"] = "商品編號1";
        NewRow["商品名稱"] = "商品名稱1";
        NewRow["會計科目"] = "科目名稱";
        NewRow["開始日期"] = "2010/08/01";
        NewRow["結束日期"] = "2010/08/31";
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

        //EnsureChildControls();

        //txtAcct1.Text = txtAcct2.Text = txtAcct3.Text = 
        //    txtAcct4.Text = txtAcct5.Text = txtAcct6.Text = "";
        //txtAcct1.Style.Clear();
        //txtAcct2.Style.Clear();
        //txtAcct3.Style.Clear();
        //txtAcct4.Style.Clear();
        //txtAcct5.Style.Clear();
        //txtAcct6.Style.Clear();
        for (int i = 1; i <= 6; i++)
        {
            ASPxTextBox tb = Page.Master.FindChildControl<ASPxTextBox>("txtAcct" + i);
            tb.Text = "";
            tb.Style.Clear();
        }
        //cbMax.Enabled = cbMin.Enabled = true;
        //cbMax.Checked = cbMin.Checked = false;

        if (vendorTypeComboBox.SelectedIndex > 0)
        {
            /*
             寄售廠商
             
             總額抽成
      
             金額級距
             
             */

            switch (vendorTypeComboBox.SelectedIndex)
            {
                case 1: // 寄售廠商可編輯總金額上限、會計科目
                    for (int i = 1; i <= 6; i++)
                    {
                        ASPxTextBox tb = Page.Master.FindChildControl<ASPxTextBox>("txtAcct" + i);
                        tb.Text = "";
                        tb.Style.Clear();
                        //tb.Style.Add("background-color", "Gray");
                    }
                    txtMinAmt.Style.Clear();


                    //txtAcct1.ControlStyle.BackColor = txtAcct2.ControlStyle.BackColor = System.Drawing.Color.Gray;
                    txtAcct1.Enabled = txtAcct2.Enabled = txtAcct3.Enabled = txtAcct4.Enabled = txtAcct5.Enabled = txtAcct6.Enabled = true;
                    TabContainer1.Visible = true;
                    break;

                case 2:   //外部廠商不可編輯總金額底限、會計科目
                    for (int i = 1; i <= 6; i++)
                    {
                        ASPxTextBox tb = Page.Master.FindChildControl<ASPxTextBox>("txtAcct" + i);
                        //**tb.Style.Add("background-color", "Gray");
                        //tb.ControlStyle.BackColor = System.Drawing.Color.Gray;
                        //tb.Style.Clear();
                    }

                    txtMinAmt.Enabled = false;
                    //cbMax.Enabled = cbMin.Enabled = false;
                    //txtMinAmt.Text = "";
                    //**txtMinAmt.Style.Add("background-color", "Gray");
                    //txtMaxOrderQty.Style.Add("background-color", "Gray");
                    txtAcct1.Enabled = txtAcct2.Enabled = txtAcct3.Enabled = txtAcct4.Enabled = txtAcct5.Enabled = txtAcct6.Enabled = false;


                    TabContainer2.Visible = true;
                    if (vendorTypeComboBox.SelectedIndex != 2)
                    {
                        TabContainer2.Visible = false;
                        //TabPanel3.Visible = false;
                        //TabPanel4.Visible = true;
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
    protected void cutoffDateRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
    {
        cutoffDayTextBox.Enabled = false;
        //TextBox3.Enabled = false;
        if (cutoffDateRadioButtonList.SelectedIndex == 1)
        {
            //TextBox3.Visible = true;
            cutoffDayTextBox.Enabled = true;
        }
    }

    //protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    //{
    //    TextBox3.Enabled = false;
    //    if (RadioButton2.Checked)
    //    {
    //        //TextBox3.Visible = true;
    //        TextBox3.Enabled = true;
    //    }
    //}

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

    private void BindListBox1(ListBox lstBox, DropDownList drp)
    {
        lstBox.Items.Clear();
        string strSubZone = drp.SelectedValue;

        if (strSubZone == "all")
        {
            foreach (ListItem item in drp.Items)
            {
                if (item.Value == Resources.WebResources.DropDownListPrompt || item.Value == "all")
                    continue;

                for (int i = 1; i < 6; i++)
                {
                    ListItem ist = new ListItem(item.Value + NumberUtil.CNumber(i.ToString()).Substring(0, 1) + "店", item.Value + NumberUtil.CNumber(i.ToString()).Substring(0, 1) + "區");
                    lstBox.Items.Add(ist);
                }
            }
        }
        else
        {
            for (int i = 1; i < 6; i++)
            {
                ListItem ist = new ListItem(strSubZone + NumberUtil.CNumber(i.ToString()).Substring(0, 1) + "店", strSubZone + NumberUtil.CNumber(i.ToString()).Substring(0, 1) + "區");
                lstBox.Items.Add(ist);
            }
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
        BindListBox1(ListBox1, ddlSubZone);
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
        if (DropDownList4.SelectedIndex == 0)
        {
            this.ListBox3.Items.Clear();
            ListBox4.Items.Clear();
            return;
        }

        BindListBox1(this.ListBox3, DropDownList4);

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

    protected void btnAdd1_Click(object sender, EventArgs e)
    {
        gvMaster.AddNewRow();
    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        gvMaster.CancelEdit();
        e.Cancel = true;
    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        gvMaster.CancelEdit();
        e.Cancel = true;
    }
    #endregion

    #region GridView1 編輯/更新/取消 相關觸發事件
    protected void btnAdd2_Click(object sender, EventArgs e)
    {
        GridView1.AddNewRow();
    }

    protected void GridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        GridView1.CancelEdit();
        e.Cancel = true;

    }

    protected void GridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        GridView1.CancelEdit();
        e.Cancel = true;
    }
    #endregion

    #region GridView2 編輯/更新/取消 相關觸發事件
    protected void btnAdd3_Click(object sender, EventArgs e)
    {
        GridView2.AddNewRow();
    }
    protected void GridView2_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        GridView2.CancelEdit();
        e.Cancel = true;

    }

    protected void GridView2_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        GridView2.CancelEdit();
        e.Cancel = true;
    }
    #endregion

    #region GridView3 編輯/更新/取消 相關觸發事件
    protected void btnAdd4_Click(object sender, EventArgs e)
    {
        GridView3.AddNewRow();
    }
    protected void GridView3_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        GridView3.CancelEdit();
        e.Cancel = true;

    }

    protected void GridView3_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        GridView3.CancelEdit();
        e.Cancel = true;
    }
    #endregion

    #region GridView4 編輯/更新/取消 相關觸發事件
    protected void btnAdd5_Click(object sender, EventArgs e)
    {
        GridView4.AddNewRow();
    }
    protected void GridView4_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        GridView4.CancelEdit();
        e.Cancel = true;

    }

    protected void GridView4_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        GridView4.CancelEdit();
        e.Cancel = true;
    }
    #endregion


    protected void btnSave_Click(object sender, EventArgs e)
    {
        //lblSupplierNo.Visible = true;
        //ASPxLabel5.Visible = true;

        lblSupplierNo.Text = "001";
        
        
        if (ViewState["ReturnUrl"] != null)
        {
            Response.Redirect(ViewState["ReturnUrl"].ToString());
            
        }
        else
        {
            //Response.Redirect("~/VSS/CONS/CON02.aspx");
        }
    }

}
