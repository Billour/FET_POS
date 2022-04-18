using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using Advtek.Utility;
public partial class VSS_INV05_INV05 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string dno = Request.QueryString["dno"] == null ? "" : Request.QueryString["dno"].ToString().Trim();
        this.ViewState["dno"] = dno;
      

        if (!IsPostBack)
        {
            BindData();

            tbDate.Text = DateTime.Now.ToString("yyyy/MM/dd");

            if (this.ViewState["dno"] == "")
            {
                this.lbOrderNo.Text = "";
                bindMasterEmptyData();
            }
            else 
            {
                // "HR100914003";
                lbOrderNo.Text = dno.ToString();
                bindMasterData();
            }

            BindData();

        }
    }
    protected void bindMasterEmptyData()
    {
        DataTable dtResult = new DataTable();
        ViewState["gvEmptyMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));

        Random rnd = new Random();

        for (int i = 0; i < 6; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["商品料號"] = rnd.Next(1000, 9999).ToString() + rnd.Next(1000, 9999).ToString();
            NewRow["商品名稱"] = "手機" + Convert.ToChar(65 + i);
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        lbOrderNo.Text = "HR100819001";
    }

    protected void HiddenField1_ValueChanged(Object sender, EventArgs e)
    {
        bindMasterData();
    }

    /// <summary>
    /// 新增-顯示Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNew_Click(object sender, EventArgs e)
    {
        //if (this.ViewState["dno"] != "")
        //{
        //    bindMasterData();
        //}
        //else
        //{
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
        //}
    }
    /// <summary>
    /// 取消新增-隱藏Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        gvMaster.ShowFooterWhenEmpty = false;
        gvMaster.ShowFooter = false;
        // 重新繫結資料
        if (gvMaster.Rows.Count == 0)
        {
            bindMasterEmptyData();
            //gvMaster.DataSource = GetEmptyDataTable();
            //gvMaster.DataBind();
        }
        else
        {
            bindMasterData();
        }
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
