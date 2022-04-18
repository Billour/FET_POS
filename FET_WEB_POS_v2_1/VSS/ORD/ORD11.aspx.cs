using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;

public partial class VSS_ORD_ORD11 : BasePage
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

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("銷售基準", typeof(string));
        dtResult.Columns.Add("訂貨天數上限", typeof(string));
        dtResult.Columns.Add("訂貨天數下限", typeof(string));
        dtResult.Columns.Add("開始日期", typeof(string));
        dtResult.Columns.Add("結束日期", typeof(string));
        dtResult.Columns.Add("安全係數", typeof(string));

        return dtResult;
    }

    private DataTable getMasterData()
    {
        string[] ss = { "半個月", "一個月", "指定期間" }; 


        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("銷售基準", typeof(string));
        dtResult.Columns.Add("訂貨天數上限", typeof(string));
        dtResult.Columns.Add("訂貨天數下限", typeof(string));
        dtResult.Columns.Add("開始日期", typeof(string));
        dtResult.Columns.Add("結束日期", typeof(string));
        dtResult.Columns.Add("安全係數", typeof(int));

        for (int i = 0; i < 10; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["商品料號"] = "1001001" + i.ToString("00");
            NewRow["商品名稱"] = "商品名稱" + i;
            NewRow["銷售基準"] = ss[i % 3];
            NewRow["訂貨天數上限"] = "99";
            NewRow["訂貨天數下限"] = "1";
            NewRow["開始日期"] = (i % 3) == 2 ? "2010/07/01" : "";
            NewRow["結束日期"] = (i % 3) == 2 ? "2010/07/01" : "";
            NewRow["安全係數"] = "7";
            dtResult.Rows.Add(NewRow);

        }
        return dtResult;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }

    protected void gvMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        //設定編輯欄位
        gridview.EditIndex = e.NewEditIndex;
        this.ViewState["editIndex"] = gridview.EditIndex;
        //Bind原查詢資料
        DataTable dt = ViewState["gvMaster"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }
    /// <summary>
    /// 新增-顯示Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
       
        if (ViewState["gvMaster"] == null)
        {
            DataTable dt = GetEmptyDataTable();
            gvMaster.DataSource = dt;
            gvMaster.DataBind();
            ViewState["gvMaster"] = dt;
        }
        gvMaster.AddNewRow();

    }

    /// <summary>
    /// 取消新增-隱藏Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        
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
            if (e.Row.RowState != DataControlRowState.Edit && e.Row.RowState != (DataControlRowState.Alternate | DataControlRowState.Edit))
            {
                HiddenField hf1 = (HiddenField)e.Row.Cells[4].FindControl("hf1");
                Label lb1 = (Label)e.Row.Cells[4].FindControl("Label11");
                lb1.Text = (hf1.Value == "0" ? "半個月" : (hf1.Value == "1" ? "一個月" : "指定期間"));
            }
            else
            {
                //DropDownList ddl1 = (DropDownList)e.Row.Cells[4].FindControl("DropDownList1");
                //HiddenField hf2 = (HiddenField)e.Row.Cells[4].FindControl("hf2");
                //AdvTekUserCtrl.postbackDate_TextBox ptb1 = (AdvTekUserCtrl.postbackDate_TextBox)e.Row.Cells[5].FindControl("PostbackDate_TextBox3");
                //AdvTekUserCtrl.postbackDate_TextBox ptb2 = (AdvTekUserCtrl.postbackDate_TextBox)e.Row.Cells[6].FindControl("PostbackDate_TextBox4");

                //if (hf2.Value != "2")
                //{
                //    ptb1.Enabled = (false);
                //    ptb2.Enabled = (false);
                //}

                //ddl1.SelectedIndex = int.Parse(hf2.Value);

            }
        }
    }

    //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DropDownList ddl = (DropDownList)gvMaster.Rows[int.Parse(this.ViewState["editIndex"].ToString())].Cells[4].FindControl("DropDownList1");

    //    AdvTekUserCtrl.postbackDate_TextBox ptb1 = (AdvTekUserCtrl.postbackDate_TextBox)gvMaster.Rows[int.Parse(this.ViewState["editIndex"].ToString())].Cells[5].FindControl("PostbackDate_TextBox3");
    //    AdvTekUserCtrl.postbackDate_TextBox ptb2 = (AdvTekUserCtrl.postbackDate_TextBox)gvMaster.Rows[int.Parse(this.ViewState["editIndex"].ToString())].Cells[6].FindControl("PostbackDate_TextBox4");

    //    if (ddl.SelectedIndex == 2)
    //    {
    //        ptb1.Enabled = (true);
    //        ptb2.Enabled = (true);
    //    }
    //    else
    //    {
    //        ptb1.Enabled = (false);
    //        ptb2.Enabled = (false);
    //    }
    //}
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DropDownList ddl = (DropDownList)gvMaster.FooterRow.Cells[4].FindControl("DropDownList2");
        DropDownList ddl = (DropDownList)sender;

        GridViewRow gvr = ddl.Parent.Parent as GridViewRow;

        //AdvTekUserCtrl.postbackDate_TextBox ptb1 = (AdvTekUserCtrl.postbackDate_TextBox)gvr.FindControl("PostbackDate_TextBox33");
        //AdvTekUserCtrl.postbackDate_TextBox ptb2 = (AdvTekUserCtrl.postbackDate_TextBox)gvr.FindControl("PostbackDate_TextBox44");

        //if (ddl.SelectedIndex == 2)
        //{
        //    ptb1.Enabled = (true);
        //    ptb2.Enabled = (true);
        //}
        //else
        //{
        //    ptb1.Enabled = (false);
        //    ptb2.Enabled = (false);
        //}
    }
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
        DataRow[] DRSelf = dt.Select("商品料號='" + e.Keys[0].ToString().Trim() + "'");
        if (DRSelf.Length > 0)
        {

            //DRSelf[0]["開始日期"] = e.NewValues["開始日期"].ToString();
            //DRSelf[0]["結束日期"] = e.NewValues["結束日期"].ToString();
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

        //DataRow DRNew = dt.NewRow();
        //DRNew["項次"] = e.NewValues["項次"];
        //DRNew["網購需求量"] = Convert.ToInt32(e.NewValues["網購需求量"]);
        //DRNew["商品編號"] = e.NewValues["商品編號"];
        //DRNew["商品名稱"] = e.NewValues["商品名稱"];
        //DRNew["門市庫存量"] = e.NewValues["門市庫存量"];
        //DRNew["在途量"] = e.NewValues["在途量"];
        //DRNew["預訂量"] = e.NewValues["預訂量"];
        //dt.Rows.Add(DRNew);
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
        ViewState["gvMaster"] = dt;
    }
    protected void ASPxComboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  Int32 idx = (Int32)ViewState["EditASPxComboBox"];
    }
    
    protected void gvMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        ViewState["EditASPxComboBox"] = "2";
       
      
    }
    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Detail)
        {
            // 繫結明細資料表           
            //ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("gvDetailDV");
            //detailGrid.DataSource = getGvProdDetailData();
            //detailGrid.DataBind();
        }
    }
    private DataTable GetEmptyDataTable1()
    {
        DataTable dtResult = new DataTable();
        //DataTable dtProdDetail = new DataTable();
        dtResult.Columns.Clear();
        dtResult.Columns.Add("商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("搭配量", typeof(int));
        dtResult.Columns.Add("訂購量", typeof(int));
        return dtResult;
    }
    private DataTable getGvProdDetailData()
    {
        DataTable dtProdDetail = GetEmptyDataTable1();

        for (int i = 1; i < 2; i++)
        {
            DataRow dtProdDetailRow = dtProdDetail.NewRow();
            dtProdDetailRow["商品編號"] = "A000" + i;
            dtProdDetailRow["商品名稱"] = "商品名稱" + i;
            dtProdDetailRow["搭配量"] = 1;//100 * (i + 1);
            dtProdDetailRow["訂購量"] = 2;
            dtProdDetail.Rows.Add(dtProdDetailRow);
        }
        return dtProdDetail;
    }
    protected void gvMaster_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    { 

        ViewState["EditASPxComboBox"] = "2";
    }
}
