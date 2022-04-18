using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using System.Collections;
public partial class VSS_CON10_Default : Advtek.Utility.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string dno = Request.QueryString["dno"] == null ? "" : Request.QueryString["dno"].ToString().Trim();
        this.ViewState["dno"] = dno;

        if (!IsPostBack)
        {
            if (this.ViewState["dno"].ToString() == "")
            {
                this.lblOrderNo.Text = "";
                // 繫結空的資料表，以顯示表頭欄位
                gvMaster.DataSource = GetEmptyDataTable();
                gvMaster.DataBind();
            }
            else
            {
                // "101900073"
                lblOrderNo.Text = dno.ToString();

                bindMasterData();

                TextBox4.Text = DateTime.Now.ToShortDateString();
            }

            //Detail資料
            BindData();
        }
    }
    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("廠商代號", typeof(string));
        dtResult.Columns.Add("廠商名稱", typeof(string));
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        return dtResult;
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        for (int i = 0; i < 6; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["廠商代號"] = "廠商代號" + i + 1;
            NewRow["廠商名稱"] = "廠商名稱" + i + 1;
            NewRow["商品料號"] = "商品料號" + i + 1;
            NewRow["商品名稱"] = "商品名稱" + i + 1;
            dtResult.Rows.Add(NewRow);
        }

        return dtResult;
    }
    private DataTable getDetailData(int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("備註", typeof(string));


        for (int i = 0; i < TempCount; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["門市編號"] = "門市編號" + (i + 1).ToString("000");
            NewRow["門市名稱"] = "門市名稱" + (i + 1).ToString("000");
            NewRow["備註"] = "大手機";

            dtResult.Rows.Add(NewRow);
        }

        return dtResult;
    }



    protected void btnNew_Click(object sender, EventArgs e)
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
            bindMasterData();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        gvMaster.ShowFooterWhenEmpty = false;
        gvMaster.ShowFooter = false;
        // 重新繫結資料
        if (gvMaster.Rows.Count == 0)
        {
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
        }
        else
        {
            bindMasterData();
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.lblOrderNo.Text = "COR2010072101";//"A43234-aaabb3";
        this.Label1.Text = "巳存檔";
    }


    protected void HiddenField1_ValueChanged(Object sender, EventArgs e)
    {
       
    }

    #region 選擇店組相關函式
    //private void BindData(string strZone)
    private void BindData()
    {
        ddlSubZone.Items.Clear();
        ddlSubZone.Items.Add("請選擇");
        ddlSubZone.Items.Add("ALL");
        ddlSubZone.Items.Add("北一區");
        ddlSubZone.Items.Add("北二區");
        ddlSubZone.Items.Add("中一區");
        ddlSubZone.Items.Add("南一區");
        //ListItem ist = new ListItem("北一區");

    }

    private void BindListBox1(string strSubZone)
    {
        this.ListBox1.Items.Clear();
        if (strSubZone == "1")
        {
            //ListItem ist = new ListItem("北一區一店");
            this.ListBox1.Items.Add("北一區一店");
            this.ListBox1.Items.Add("北一區二店");
            this.ListBox1.Items.Add("北二區一店");
            this.ListBox1.Items.Add("北一區二店");
            this.ListBox1.Items.Add("北一區三店");
            this.ListBox1.Items.Add("中一區一店");
            this.ListBox1.Items.Add("中一區二店");
            this.ListBox1.Items.Add("南一區一店");
        }
        else
        {
            for (int i = 1; i < 6; i++)
            {
                ListItem ist = new ListItem(strSubZone + NumberUtil.CNumber(i.ToString()).Substring(0, 1) + "店", strSubZone + NumberUtil.CNumber(i.ToString()).Substring(0, 1) + "區");
                this.ListBox1.Items.Add(ist);
            }
        }
    }

    //protected void ddlZone_SelectedIndexChanged1(object sender, EventArgs e)
    //{
    //    BindData(ddlZone.SelectedValue);
    //}

    protected void ddlSubZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubZone.SelectedIndex == 1)
        {
            BindListBox1("1");
        }
        else if (ddlSubZone.SelectedIndex > 0)
        {
            BindListBox1(ddlSubZone.SelectedValue);
        }

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
        bindMasterData();
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
    #endregion


    #region 分頁相關 (共用)
    protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //此函式可共用
        GridView gridview = sender as GridView;
        gridview.PageIndex = e.NewPageIndex;

        DataTable dt = ViewState[gridview.ID] as DataTable ?? null;//GridView的資料要存於ViewState以方便共用此Function
        gridview.DataSource = dt;
        gridview.DataBind();
    }
    protected void btnGoToIndex_Click(object sender, EventArgs e)
    {
        //此函式可共用
        GridView gridview = (sender as Button).Parent.Parent.Parent.Parent as GridView;
        TextBox textbox = (sender as Button).Parent.FindControl("tbGoToIndex") as TextBox;
        string strIndex = textbox.Text.Trim();
        int index = 0;
        if (int.TryParse(strIndex, out index))
        {
            index = index - 1;
            if (index >= 0 && index <= gridview.PageCount - 1)
            {
                gridview.PageIndex = index;
                DataTable dt = ViewState[gridview.ID] as DataTable ?? null;//GridView的資料要存於ViewState以方便共用此Function
                gridview.DataSource = dt;
                gridview.DataBind();
            }
            else
            {
                textbox.Text = string.Empty;
            }
        }
        else
        {
            textbox.Text = string.Empty;
        }
    }
    #endregion






}
