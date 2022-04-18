using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using Advtek.Utility;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
public partial class VSS_INV05_INV05 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string dno = Request.QueryString["dno"] == null ? "" : Request.QueryString["dno"].ToString().Trim();
        this.ViewState["dno"] = dno;
      

        if (!IsPostBack)
        {
            BindData();

            ASPxDateEdit1.Text = DateTime.Now.ToString("yyyy/MM/dd");

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

    protected void btnAddNew_Click(object sender, EventArgs e)
    { gvMaster.AddNewRow(); }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        /*
        if (e.RowType == GridViewRowType.Detail)
        {
            // 繫結明細資料表           
            ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("detailGrid");
            detailGrid.DataSource = GetDetailData();
            detailGrid.DataBind();            
        }
        */
    }

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        //if (e.RowType == GridViewRowType.Detail)
        //{
        //    // 繫結明細資料表           
        //    ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("detailGrid");
        //    detailGrid.DataSource = GetDetailData();
        //    detailGrid.DataBind();
        //}
    }

    /// <summary>
    /// 主GridView的分頁變更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = getMasterData();
        grid.DataBind();
    }
    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvMaster"] as DataTable ?? null;
        gvMaster.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
        ////e.NewValues["TemplateID"] = Guid.NewGuid(); // I set this PK myself.
    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvMaster"] as DataTable ?? null;
        //// DataRow[] DRSelf = dt.Select("項次='" + e.Keys[0].ToString().Trim() + "'");
        // if (DRSelf.Length > 0)
        // {

        //     DRSelf[0]["類別"] = e.NewValues["類別"];
        //     DRSelf[0]["兑點代號"] = e.NewValues["兑點代號"];
        //     DRSelf[0]["兑點名稱"] = e.NewValues["兑點名稱"];
        //     DRSelf[0]["開始日期"] = e.NewValues["開始日期"];
        //     DRSelf[0]["結束日期"] = e.NewValues["結束日期"];
        //     DRSelf[0]["點數"] = e.NewValues["點數"];
        //     DRSelf[0]["兑換金額"] = e.NewValues["兑換金額"];
        // }
        gvMaster.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
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

   

    

}
