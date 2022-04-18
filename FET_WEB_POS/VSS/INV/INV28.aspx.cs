using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;

public partial class VSS_INV_INV28 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        getGvMaster();
        //div1.Visible = true;
    }

    protected void getGvMaster()
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
        dtMaster.Columns.Add("門市編號", typeof(string));
        dtMaster.Columns.Add("門市名稱", typeof(string));
        dtMaster.Columns.Add("商品料號", typeof(string));
        dtMaster.Columns.Add("商品名稱", typeof(string));
        dtMaster.Columns.Add("展示起日", typeof(string));
        dtMaster.Columns.Add("展示訖日", typeof(string));
        dtMaster.Columns.Add("拆封數量", typeof(string));
        dtMaster.Columns.Add("折扣方式", typeof(string));
        dtMaster.Columns.Add("金額/佔比", typeof(string));

        string[] str = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        string[] array1 = { "百分比", "金額" };
        string[] array2 = { "50%", "100" };

        for (int i = 0; i < 30; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            // "SA" + 店代號 + "-" + "YYMM" + 流水號(3碼) 

            string strLen0 = "";
            switch ((i + 1).ToString().Length)
            {
                case 1:
                    strLen0 = "00";
                    break;
                case 2:
                    strLen0 = "0";
                    break;
                case 3:
                    strLen0 = "";
                    break;
            }
            //dtMasterRow["調整單號"] = "SA2011-" + DateTime.Now.Year.ToString().Substring(2, 2) + DateTime.Now.ToString("MM") + strLen0 + Convert.ToString(i + 1).ToString(); //"BBA0000" + i;

            dtMasterRow["門市編號"] = "GA0000" + i;
            dtMasterRow["門市名稱"] = "門市" + str[i > 25 ? i - 26 : i];
            dtMasterRow["商品料號"] = "ST2101-10081500" + i;
            dtMasterRow["商品名稱"] = "商品名稱" + str[i > 25 ? i - 26 : i];
            dtMasterRow["展示起日"] = "2010/07/12";
            dtMasterRow["展示訖日"] = "2010/07/12";
            dtMasterRow["拆封數量"] = i;
            dtMasterRow["折扣方式"] = array1[i % 2];
            dtMasterRow["金額/佔比"] = array2[i % 2];

            dtMaster.Rows.Add(dtMasterRow);
        }
        return dtMaster;

    }

    protected void bindDetailData()
    {
        DataTable dtgvDetail = new DataTable();
        dtgvDetail = getDetailData();
        ViewState["gvDetail"] = dtgvDetail;
        gvDetail.DataSource = dtgvDetail;
        gvDetail.DataBind();
    }
    private DataTable getDetailData()
    {
        DataTable gvDetail = new DataTable();
        gvDetail.Columns.Clear();
        gvDetail.Columns.Add("IMEI", typeof(string));
        gvDetail.Columns.Add("更新人員", typeof(string));
        gvDetail.Columns.Add("更新日期", typeof(string));

        for (int i = 1; i < 2; i++)
        {
            DataRow gvDetailRow = gvDetail.NewRow();
            gvDetailRow["IMEI"] = "7780-9440-5640-7860";
            gvDetailRow["更新人員"] = "王小明";
            gvDetailRow["更新日期"] = "2010/07/13";
            gvDetail.Rows.Add(gvDetailRow);
        }
        return gvDetail;
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

    protected void gvMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "select")
        {
            bindDetailData();
            DIVdetail.Visible = true;
            this.gvDetail.Visible = true;
        }

    }

    protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowIndex != -1)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        if (e.Row.Cells[5].Text == "已存檔")
        //        {
        //            Button fix = (Button)e.Row.Cells[0].FindControl("btnfix");
        //            fix.Visible = true;
        //            fix.Enabled = true;
        //            Button btnView = (Button)e.Row.Cells[0].FindControl("btnView");
        //            btnView.Enabled = true;
        //            btnView.Visible = false;

        //        }
        //    }
        //}
    }

    protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowIndex != -1)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        HiddenField hfIMEI = (HiddenField)e.Row.Cells[3].FindControl("hidIMEI");
        //        CheckBox cbIMEI = (CheckBox)e.Row.Cells[3].FindControl("CheckBox3");
        //        cbIMEI.Checked = (hfIMEI.Value == "1") ? (true) : (false);
        //    }
        //}
    }

    protected void gvDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView gridview = sender as GridView;

        //取得資料

        //更新資料庫

        //取消編輯狀態
        gridview.EditIndex = -1;

        //Bind新資料(重取資料)
        bindDetailData();
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

    protected void gvDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        gridview.EditIndex = -1;
        //Bind原查詢資料
        DataTable dt = ViewState["gvDetail"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
}
