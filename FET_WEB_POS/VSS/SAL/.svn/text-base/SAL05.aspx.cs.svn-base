using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class VSS_SAL_SAL05 : System.Web.UI.Page
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        divContent.Visible = true;
        gvMaster.Visible = true;
        gvDetail.Visible = false;
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.SelectedIndex = -1;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("申請日期", typeof(string));
        dtResult.Columns.Add("服務屬性", typeof(string));
        dtResult.Columns.Add("服務類別", typeof(string));
        dtResult.Columns.Add("應收總金額", typeof(int));
        dtResult.Columns.Add("客戶門號", typeof(string));
        dtResult.Columns.Add("銷售人員", typeof(string));
        return dtResult;
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        string[] array0 = { "未結帳" };
        string[] array1 = { "IA", "IA", "IA", "HRS", "Payment", "IA" };
        //string[] array2 = { "全球卡", "換補卡", "2轉3", "新啟用", "續約", "代收", "維修", "網購", "預購" };
        string[] array2 = { "新啟用", "續約", "續約", "維修", "代收", "續約" };
        for (int i = 0; i < 6; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = i + 1;
            NewRow["狀態"] = array0[0];
            NewRow["申請日期"] = "2010/07/01";
            NewRow["服務屬性"] = array1[i];
            NewRow["服務類別"] = array2[i];
            NewRow["應收總金額"] = 3000;
            NewRow["客戶門號"] = "091536214" + i;
            NewRow["銷售人員"] = "王小明";
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    protected void bindDetailData(string KeyNo)
    {
        DataTable dtResult = new DataTable();
        dtResult = getDetailData(KeyNo);
        ViewState["gvDetail"] = dtResult;
        gvDetail.DataSource = dtResult;
        gvDetail.DataBind();
    }

    private DataTable getDetailData(string KeyNo)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("促銷代號", typeof(string));
        dtResult.Columns.Add("促銷名稱", typeof(string));
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("卡片序號(SIM)", typeof(string));
        dtResult.Columns.Add("金額", typeof(string));

        for (int i = 0; i < 3; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = i + 1;
            NewRow["促銷代號"] = KeyNo + "_A0" + i;
            NewRow["促銷名稱"] = "大雙網促銷";
            NewRow["商品料號"] = "20102852" + i;
            NewRow["商品名稱"] = string.Format("BenQ S{0}20i(銀,簡)", i);
            NewRow["卡片序號(SIM)"] = "234313353453" + i;
            NewRow["金額"] = 1000 * (i + 1);
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    protected void gvMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView mygridview = sender as GridView;
        if (mygridview.SelectedIndex >= 0)
        {
            string KeyNo = mygridview.Rows[mygridview.SelectedIndex].Cells[4].Text;
            bindDetailData(KeyNo);
            gvDetail.Visible = true;
        }
    }

    #region 分頁(共用)
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
