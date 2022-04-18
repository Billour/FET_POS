using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_CON04_CON04 : Advtek.Utility.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {            
            if (!string.IsNullOrEmpty(Request.QueryString["No"]))
            {
                ViewState["ReturnUrl"] = Request.UrlReferrer.ToString();
                ddlSupplierNo.SelectedIndex = 1;
                lblStatus.Text = "已存檔";

                txtProductName.Text = "iPhone 4";
                txtProductCode.Text = "69314900";
                ddlProductCategory.SelectedIndex = 1;
                Random rnd = new Random();
                PostbackDate_TextBox1.Text = DateTime.Today.AddDays(-rnd.Next(0, 30)).ToString("yyyy/MM/dd");
                PostbackDate_TextBox2.Text = DateTime.Parse(PostbackDate_TextBox1.Text).AddDays(rnd.Next(30, 90)).ToString("yyyy/MM/dd");
                PostbackDate_TextBox3.Text = PostbackDate_TextBox2.Text;
                bindMasterData();
                bindGridView1Data();

                txtAcct1.Text = "10";
                txtAcct2.Text = "15";
                txtAcct3.Text = "200011";
                txtAcct4.Text = "200012";
                txtAcct5.Text = "5000";
                txtAcct6.Text = "5001";
                txtUnit.Text = "組";
            }
            else
            {
                gvMaster.DataSource = new DataTable();
                gvMaster.DataBind();
                GridView1.DataSource = new DataTable();
                GridView1.DataBind();
            }

            //bindGridView1Data();
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
    protected void bindGridView1Data()
    {
        DataTable dtResult = new DataTable();
        dtResult = getGridView1Data();
        ViewState["GridView1"] = dtResult;
        GridView1.DataSource = dtResult;
        GridView1.DataBind();
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("佣金比率", typeof(string));
        dtResult.Columns.Add("起始月份", typeof(string));
        dtResult.Columns.Add("結束月份", typeof(string));


        DataRow NewRow = dtResult.NewRow();
        NewRow["佣金比率"] = "10";
        NewRow["起始月份"] = "2010/07";
        NewRow["結束月份"] = "2010/10";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["佣金比率"] = "15";
        NewRow["起始月份"] = "2010/11";
        NewRow["結束月份"] = "";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }
    private DataTable getGridView1Data()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("更新日期", typeof(string));
        dtResult.Columns.Add("生效日期", typeof(string));
        dtResult.Columns.Add("失效日期", typeof(string));
        dtResult.Columns.Add("商品金額", typeof(string));

        for (int i = 0; i < 5; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["更新日期"] = "2010/07/01";
            NewRow["生效日期"] = "2010/07/" + (2 * i + 1).ToString("00");
            NewRow["失效日期"] = "2010/07/" + (2 * i + 2).ToString("00");
            NewRow["商品金額"] = 100 * (i + 1);
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        //bindMasterData();        
    }

    /// <summary>
    /// 新增-顯示Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
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
            gvMaster.DataSource = new DataTable();
            gvMaster.DataBind();
        }
        else
        {
            bindMasterData();
        }
    }

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

        //if (Convert.ToDateTime(e.Row.Cells[3].Text) >= Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM")))
        //{
        //    Label lbStartDate = (Label)e.Row.Cells[3].FindControl("lbStartDate");
        //    lbStartDate.Visible = true;
        //}

        Label lbStartDate = (Label)gridview.Rows[e.NewEditIndex].Cells[3].FindControl("lbStartDate");
        if (Convert.ToDateTime(lbStartDate.Text) <= Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM")))
        {
            
            lbStartDate.Visible = true;
            TextBox tbStartDate = (TextBox)gridview.Rows[e.NewEditIndex].Cells[3].FindControl("tbStartDate");
            tbStartDate.Visible = false;
        }
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

    protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex != -1 && e.Row.Cells[4].Text.Replace("&nbsp;","").Length>0)
            {
                if (Convert.ToDateTime(e.Row.Cells[4].Text) < Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM")) )
                {
                     Button bt = (Button) e.Row.Cells[1].Controls[1];
                    bt.Enabled = false;
                }
                
            }
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
            Response.Redirect("~/VSS/CONS/CON04.aspx");
        }
    }
}
