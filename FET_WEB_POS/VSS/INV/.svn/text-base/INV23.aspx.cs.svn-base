using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_INV23_INV23 : Advtek.Utility.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //gvMaster_RowEditing();
            bindMasterData();
            //lblReturnDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }
    }
    private DataTable GetEmptyDataTable()
    {
       DataTable dtResult = new DataTable();
       dtResult.Columns.Add("項次", typeof(string));
       dtResult.Columns.Add("倉別名稱", typeof(string));
       dtResult.Columns.Add("可銷售", typeof(string));
       dtResult.Columns.Add("檢核IMEI", typeof(string));
       dtResult.Columns.Add("更新人員", typeof(string));
       dtResult.Columns.Add("更新日期", typeof(string));

       return dtResult;
    }
    protected void bindMasterData()
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = getMasterData();
        ViewState["gvMaster"] = dtGvMaster;
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
    }

    private DataTable getMasterData()
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("項次", typeof(string));
        dtMaster.Columns.Add("倉別名稱", typeof(string));
        dtMaster.Columns.Add("可銷售", typeof(string));
        dtMaster.Columns.Add("檢核IMEI", typeof(string));
        dtMaster.Columns.Add("更新人員", typeof(string));
        dtMaster.Columns.Add("更新日期", typeof(string));


        //string[] array1 = { "有效", "失效" };
        //string[] array2 = { "不控管", "銷售時記錄", "銷售時確認", "庫存異動控管" };
        string[] array3 = { "銷售倉",  "展示倉", "租賃倉", "維修倉" };

        for (int i = 1; i < 5; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["項次"] = i;
           // dtMasterRow["狀態"] = array1[i % 2];
            dtMasterRow["倉別名稱"] = array3[i % 4];
            //dtMasterRow["檢核IMEI"] = array2[i % 4];
            dtMasterRow["更新人員"] = "王小明";
            dtMasterRow["更新日期"] = Convert.ToDateTime("2010/07/20" + DateTime.Now.ToLongTimeString()).ToString("yyyy/MM/dd HH:mm:ss");
            dtMaster.Rows.Add(dtMasterRow);
        }
        return dtMaster;
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        //lblOrderNo.Text = "COR2010073001";
        //Label2.Text = "01 已存檔";

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
    protected void gvMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
      protected void gvMaster_RowCreated(object sender, GridViewRowEventArgs e)
    {
        // 檢查是不是標題列 
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells.Clear();
            // 建立自訂的標題 
            GridView gv = (GridView)sender;

            #region 第一列標頭

            GridViewRow gvRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            // 增加  欄位 
    
            TableCell tc3 = new TableCell();
            tc3.Text = "項次";
            tc3.RowSpan = 2;   // 跨兩列
            gvRow.Cells.Add(tc3);

            //TableCell tc4 = new TableCell();
            //tc4.Text = "狀態";
            //tc4.RowSpan = 2;   // 跨兩列
            //gvRow.Cells.Add(tc4);

            TableCell tc5 = new TableCell();
            tc5.Text = "倉別名稱";
            tc5.RowSpan = 2;   // 跨兩列
            gvRow.Cells.Add(tc5);

            TableCell tc16 = new TableCell();
            tc16.Text = "倉別屬性";
            tc16.ColumnSpan = 2;
            
            gvRow.Cells.Add(tc16);

           

            TableCell tc14 = new TableCell();
            tc14.Text = "更新人員";
            tc14.RowSpan = 2;   // 跨兩列
            gvRow.Cells.Add(tc14);

            TableCell tc15 = new TableCell();
            tc15.Text = "更新日期";
            tc15.RowSpan = 2;   // 跨兩列
            gvRow.Cells.Add(tc15);

            gvRow.BackColor = System.Drawing.Color.FromName("#780C0C");
            gvRow.ForeColor = System.Drawing.Color.White;
            gvRow.HorizontalAlign = HorizontalAlign.Center;

            
            gv.Controls[0].Controls.AddAt(0, gvRow);

            #endregion

            #region 第二列標頭
            GridViewRow gvRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell tcs16 = new TableCell();
            tcs16.Text = "可銷售";
            gvRow2.Cells.Add(tcs16);


            TableCell tcs6 = new TableCell();
            tcs6.Text = "檢核IMEI";
            gvRow2.Cells.Add(tcs6);


            //gv.Controls[0].Controls.Add(gvRow2);


            gvRow2.BackColor = System.Drawing.Color.FromName("#780C0C");
            gvRow2.ForeColor = System.Drawing.Color.White;
            gvRow2.HorizontalAlign = HorizontalAlign.Center;

            gv.Controls[0].Controls.AddAt(1, gvRow2);
            gv.Style.Add("width", "98%");
            //gv.ControlStyle.CssClass = "mGrid";
            //gv.CssClass = "mGrid";
            #endregion

        }

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
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
         }
         else
         {
            bindMasterData();
         }
      }
}
