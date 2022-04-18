using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_OPT_OPT10 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 繫結空的資料表，以顯示表頭欄位
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
        }
    }
    protected void bindMasterData( )
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = getMasterData( );
        ViewState["gvMaster"] = dtGvMaster;
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("狀態", typeof(string));
        dtMaster.Columns.Add("商品編號", typeof(string));
        dtMaster.Columns.Add("商品名稱", typeof(string));
        dtMaster.Columns.Add("商品類別", typeof(string));
        dtMaster.Columns.Add("單位", typeof(string));
        dtMaster.Columns.Add("單機價格", typeof(string));
        dtMaster.Columns.Add("有效日期1", typeof(string));
        dtMaster.Columns.Add("有效日期2", typeof(string));
        dtMaster.Columns.Add("扣庫存", typeof(string));
        dtMaster.Columns.Add("檢核IMEI", typeof(string));
        dtMaster.Columns.Add("自訂價格", typeof(string));
        dtMaster.Columns.Add("科目1", typeof(string));
        dtMaster.Columns.Add("科目2", typeof(string));
        dtMaster.Columns.Add("科目3", typeof(string));
        dtMaster.Columns.Add("科目4", typeof(string));
        dtMaster.Columns.Add("科目5", typeof(string));
        dtMaster.Columns.Add("科目6", typeof(string));
        dtMaster.Columns.Add("更新人員", typeof(string));
        dtMaster.Columns.Add("更新日期", typeof(string));
        return dtMaster;
    }

    private DataTable getMasterData( )
    {
        DataTable dtMaster = GetEmptyDataTable();
       

        string[] array1 = { "3G Handset", "SIM Card", "3G Accessory" };
        string[] array2 = { "失效", "已過期", "未生效" };
        string[] array3 = { "不控管", "銷售時記錄", "銷售時確認", "庫存異動控管" };
        for (int i = 0; i < 6; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["狀態"] = array2[i % 3];
            dtMasterRow["商品編號"] = "A00100" + i;
            dtMasterRow["商品名稱"] = "商品名稱" + i;
            dtMasterRow["商品類別"] = array1[i % 3];
            dtMasterRow["單位"] = i + "個";
            dtMasterRow["單機價格"] = i+"1234"+i;
            dtMasterRow["有效日期1"] = Convert.ToDateTime("2010/07/20" + DateTime.Now.ToLongTimeString()).ToString("yyyy/MM/dd");
            dtMasterRow["有效日期2"] = Convert.ToDateTime("2010/07/20" + DateTime.Now.ToLongTimeString()).ToString("yyyy/MM/dd");
            dtMasterRow["扣庫存"] = "扣庫存" + i;
            dtMasterRow["檢核IMEI"] = array3[i % 4];
            dtMasterRow["自訂價格"] = "自訂價格" + i;
            dtMasterRow["科目1"] = i+"1";
            dtMasterRow["科目2"] = i + "12";
            dtMasterRow["科目3"] = i + "123";
            dtMasterRow["科目4"] = i + "12345";
            dtMasterRow["科目5"] = i + "123";
            dtMasterRow["科目6"] = i + "123";


            dtMasterRow["更新人員"] = "王小明";

            dtMasterRow["更新日期"] = Convert.ToDateTime("2010/07/20" + DateTime.Now.ToLongTimeString()).ToString("yyyy/MM/dd HH:mm:ss");

            dtMaster.Rows.Add(dtMasterRow);
        }
        return dtMaster;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData( );
        btnNew.Visible = true;
        btnDelete.Visible = true;
        //Div1.Visible = true;
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
            bindMasterData( );
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
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
        }
        else
        {
            bindMasterData( );
        }
    }


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


        //Bind新資料(重取資料)
        if (gvMaster.Rows[e.RowIndex].FindControl("chkSalRecordEdit") != null)
        {
            CheckBox chkSalRecordEdit = (CheckBox)(gvMaster.Rows[e.RowIndex].FindControl("chkSalRecordEdit"));
            CheckBox chkStokeEdit = (CheckBox)(gvMaster.Rows[e.RowIndex].FindControl("chkStokeEdit"));
            if (chkStokeEdit.Checked == true && chkSalRecordEdit.Checked == false)
            {

                //ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "page", "<script type='text/javascript'>alert('銷售記錄必需勾選');;</script>", false);
                ////Page.ClientScript.RegisterStartupScript(this.GetType(), "page", "<script type='text/javascript'>alert('');;</script>");
            }
            else
            {
                gridview.EditIndex = -1;
                bindMasterData( );
            }
        }



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
    protected void gvMaster_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //// 檢查是不是標題列 
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    e.Row.Cells.Clear();
        //    // 建立自訂的標題 
        //    GridView gv = (GridView)sender;

        //    #region 第一列標頭

        //    GridViewRow gvRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

        //    // 增加  欄位 
        //    TableCell tc1 = new TableCell();
        //    tc1.Text = "";
        //    CheckBox chkBox = new CheckBox();
        //    tc1.Controls.Add(chkBox);
        //    tc1.RowSpan = 2;   // 跨兩列
        //    gvRow.Cells.Add(tc1);

        //    TableCell tc2 = new TableCell();
        //    tc2.Text = "";
        //    tc2.RowSpan = 2;   // 跨兩列
        //    gvRow.Cells.Add(tc2);

        //    TableCell tc3 = new TableCell();
        //    tc3.Text = "商品編號";
        //    tc3.RowSpan = 2;   // 跨兩列
        //    gvRow.Cells.Add(tc3);

        //    TableCell tc4 = new TableCell();
        //    tc4.Text = "商品名稱";
        //    tc4.RowSpan = 2;   // 跨兩列
        //    gvRow.Cells.Add(tc4);

        //    TableCell tc5 = new TableCell();
        //    tc5.Text = "商品類別";
        //    tc5.RowSpan = 2;   // 跨兩列
        //    gvRow.Cells.Add(tc5);

        //    TableCell tc16 = new TableCell();
        //    tc16.Text = "單位";
        //    tc16.RowSpan = 2;   // 跨兩列
        //    gvRow.Cells.Add(tc16);


        //    TableCell tc6 = new TableCell();
        //    tc6.Text = "檢核IMEI";
        //    tc6.ColumnSpan = 2;   // 跨兩欄 
        //    gvRow.Cells.Add(tc6);

        //    TableCell tc7 = new TableCell();
        //    tc7.Text = "自訂價格";
        //    tc7.RowSpan = 2;   // 跨兩列
        //    gvRow.Cells.Add(tc7);

        //    TableCell tc8 = new TableCell();
        //    tc8.Text = "科目1";
        //    tc8.RowSpan = 2;   // 跨兩列
        //    gvRow.Cells.Add(tc8);

        //    TableCell tc9 = new TableCell();
        //    tc9.Text = "科目2";
        //    tc9.RowSpan = 2;   // 跨兩列
        //    gvRow.Cells.Add(tc9);

        //    TableCell tc10 = new TableCell();
        //    tc10.Text = "科目3";
        //    tc10.RowSpan = 2;   // 跨兩列
        //    gvRow.Cells.Add(tc10);

        //    TableCell tc11 = new TableCell();
        //    tc11.Text = "科目4";
        //    tc11.RowSpan = 2;   // 跨兩列
        //    gvRow.Cells.Add(tc11);

        //    TableCell tc12 = new TableCell();
        //    tc12.Text = "科目5";
        //    tc12.RowSpan = 2;   // 跨兩列
        //    gvRow.Cells.Add(tc12);

        //    TableCell tc13 = new TableCell();
        //    tc13.Text = "科目6";
        //    tc13.RowSpan = 2;   // 跨兩列
        //    gvRow.Cells.Add(tc13);

        //    TableCell tc14 = new TableCell();
        //    tc14.Text = "更新人員";
        //    tc14.RowSpan = 2;   // 跨兩列
        //    gvRow.Cells.Add(tc14);

        //    TableCell tc15 = new TableCell();
        //    tc15.Text = "更新日期";
        //    tc15.RowSpan = 2;   // 跨兩列
        //    gvRow.Cells.Add(tc15);

        //    gvRow.BackColor = System.Drawing.Color.FromName("#780C0C");
        //    gvRow.ForeColor = System.Drawing.Color.White;

        //    //gvRow.Attributes.Add("bgcolor", "#780C0C");
        //    //gvRow.Attributes.Add("font- color", "#FF0000");
        //    gv.Controls[0].Controls.AddAt(0, gvRow);

        //    #endregion

        //    #region 第二列標頭
        //    GridViewRow gvRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);



        //    TableCell tcs16 = new TableCell();
        //    tcs16.Text = "銷售記錄";
        //    gvRow2.Cells.Add(tcs16);


        //    TableCell tcs6 = new TableCell();
        //    tcs6.Text = "IMEI控管";
        //    gvRow2.Cells.Add(tcs6);


        //    gvRow2.BackColor = System.Drawing.Color.FromName("#780C0C");
        //    gvRow2.ForeColor = System.Drawing.Color.FromName("#FFFFFF");
        //    gv.Controls[0].Controls.AddAt(1, gvRow2);
        //    //gv.CssClass = "mGrid";
        //    #endregion

        //}

    }

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
