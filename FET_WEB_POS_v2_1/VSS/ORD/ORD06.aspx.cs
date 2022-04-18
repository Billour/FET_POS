using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class VSS_ORD_ORD06 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindEmptyData();
        }
    }
    protected void bindEmptyData()
    {

        DataTable dtResult = new DataTable();
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMasterDV.DataSource = dtResult;
        gvMasterDV.DataBind();
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("主商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("搭配商品編號", typeof(string));
        dtResult.Columns.Add("搭配商品名稱", typeof(string));
        dtResult.Columns.Add("開始日期", typeof(DateTime));
        dtResult.Columns.Add("結束日期", typeof(DateTime));
        dtResult.Columns.Add("更新日期", typeof(DateTime));
        dtResult.Columns.Add("更新人員", typeof(string));

        string[] array1 = { "HTC Hero", "iPhone4", "NOKIA X6", "Sony Ericesson" };

        for (int i = 0; i < 10; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = i+1;
            NewRow["狀態"] = "過期";
            NewRow["主商品編號"] = "15020000" + i;
            NewRow["商品名稱"] = array1 [i % 4];
            NewRow["搭配商品編號"] = "25020444" + i;
            NewRow["搭配商品名稱"] = "大雙網促銷";
            NewRow["開始日期"] = "2010/07/01";
            NewRow["結束日期"] = "2012/07/01";;
            NewRow["更新日期"] = "2010/07/01";
            NewRow["更新人員"] = "王小明";
            dtResult.Rows.Add(NewRow);

        }
        return dtResult;
    }

    protected void bindDetailData(string KeyNo)
    {       
       DataTable dtResult = new DataTable();
       dtResult = getDetailData(KeyNo);
       ViewState["gvDetail"] = dtResult;
    }

    private DataTable getDetailData(string KeyNo)
    {
       DataTable dtResult = new DataTable();

       dtResult.Columns.Add("項次", typeof(string));
       dtResult.Columns.Add("搭配商品編號", typeof(string));
       dtResult.Columns.Add("商品名稱", typeof(string));

       for (int i = 0; i < 2; i++)
       {
          DataRow NewRow = dtResult.NewRow();


          NewRow["項次"] = (i + 1).ToString();
          NewRow["搭配商品編號"] =KeyNo + "_A0" + i;
          NewRow["商品名稱"] = "大雙網促銷";
          dtResult.Rows.Add(NewRow);
       }
       return dtResult;
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        gvMasterDV.DataSource = getMasterData();
        gvMasterDV.DataBind();
        gvMasterDV.AddNewRow();
    }
    protected void btnNew3_Click(object sender, EventArgs e)
    {
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

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

    protected void gvMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
       GridView mygridview = sender as GridView;
       if (mygridview.SelectedIndex >= 0)
       {
          string KeyNo = mygridview.Rows[mygridview.SelectedIndex].Cells[5].Text;
          bindDetailData(KeyNo);

       }
    }

    protected void gvDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
       GridView gridview = sender as GridView;
       gridview.EditIndex = -1;
       //Bind原查詢資料
       DataTable dt = ViewState["gvDetail"] as DataTable;
       gridview.DataSource = dt;
       gridview.DataBind();
    }
    protected void gvDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
       GridView gridview = sender as GridView;
       //設定編輯欄位
       gridview.EditIndex = e.NewEditIndex;
       //Bind原查詢資料
       DataTable dt = ViewState["gvDetail"] as DataTable;
       gridview.DataSource = dt;
       gridview.DataBind();
    }
    protected void gvDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView gridview = sender as GridView;

        //取得資料

        //更新資料庫

        //取消編輯狀態
        gridview.EditIndex = -1;
      
        //Bind新資料(重取資料)
        bindDetailData("3");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
       
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
       
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
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
    protected void gvMasterDV_PageIndexChanged(object sender, EventArgs e)
    {

        DataTable dt = ViewState["gvMaster"] as DataTable;
        ((ASPxGridView)sender).DataSource = dt;
        ((ASPxGridView)sender).DataBind(); 
    }
   
    protected void gvMasterDV_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvMaster"] as DataTable ?? null;
        DataRow[] DRSelf = dt.Select("項次='" + e.Keys[0].ToString().Trim() + "'");
        if (DRSelf.Length > 0)
        {

            DRSelf[0]["開始日期"] = e.NewValues["開始日期"].ToString();
            DRSelf[0]["結束日期"] = e.NewValues["結束日期"].ToString();
        }
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();

    }
    protected void gvDetailDV_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvDetail"] as DataTable ?? null;
        DataRow[] DRSelf = dt.Select("搭配商品編號='" + e.Keys[0].ToString().Trim() + "'");
        if (DRSelf.Length > 0)
        {

            DRSelf[0]["搭配商品編號"] = e.NewValues["搭配商品編號"].ToString();
            DRSelf[0]["商品名稱"] = e.NewValues["商品名稱"].ToString();
        }
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }
    protected void gvMasterDV_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvMaster"] as DataTable ?? getMasterData();

       
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
        ViewState["gvMaster"] = dt;
    }
    protected void gvDetailDV_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvDetail"] as DataTable ?? getDetailData("");

       
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
        ViewState["gvDetail"] = dt;
    }
    protected void gvMasterDV_FocusedRowChanged(object sender, EventArgs e)
    {
        if (gvMasterDV.FocusedRowIndex >= 0)
        {
            this.hdNo.Value = gvMasterDV.GetRowValues(gvMasterDV.FocusedRowIndex, "主商品編號").ToString();

            DataTable dt = getDetailData(this.hdNo.Value);
            ViewState["gvDetail"] = dt;

        }

    }    

    protected void gvMasterDV_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        
    }

    protected void gvMasterDV_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        

        if (e.RowType == GridViewRowType.Data)
        {
            List<object> keyValues = this.gvMasterDV.GetSelectedFieldValues("主商品編號");
            foreach (string key in keyValues)
            {
                if (key == e.GetValue("主商品編號").ToString())
                {
                    if (key == this.hdNo.Value)
                    {
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    }
                    else
                    {
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                        e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
                    }

                }
            }
        }
    }

    protected void gvMasterDV_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxGridView gv = (ASPxGridView)sender;
        if (gv.IsNewRowEditing)
        {
            PopupControl pc = ((ASPxGridView)sender).FindChildControl<PopupControl>("PopupControl1");
            pc.Text = this.PopupControl1.Text;
        }
        //e.NewValues["項次"] = ASPxTextBox1.Text;
    }
    protected void gvMasterDV_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {

    }

    protected void ASPxTextBox1_DataBound(object sender, EventArgs e)
    {
        //if (gvMasterDV.IsNewRowEditing)
        //{
        //    (sender as ASPxTextBox).Text = ASPxTextBox1.Text;
        //}
    }

    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvMaster"] as DataTable ?? null;
       
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }
}
