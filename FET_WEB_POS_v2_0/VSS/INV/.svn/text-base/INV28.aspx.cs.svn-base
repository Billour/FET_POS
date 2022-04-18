using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;

public partial class VSS_INV_INV28 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            detailGrid.Visible = true;
            BindMasterData();
            BindDetailData();

        }
    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        BindMasterData();
    }

    protected void CommandButton_Click(Object sender, CommandEventArgs e)
    {
        
        //grid.Selection.UnselectAll();
        //grid.Selection.SetSelectionByKey(e.CommandArgument, true);
        detailGrid.DataSource = GetDetailData();
        detailGrid.DataBind();
        detailGrid.Visible = true;
    }

    protected void BindMasterData()
    {
        grid.DataSource = GetMasterData();
        grid.DataBind();
    }

    private DataTable GetMasterData()
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

    protected void BindDetailData()
    {        
        detailGrid.DataSource = GetDetailData();
        detailGrid.DataBind();
    }
    private DataTable GetDetailData()
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

    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }

    protected void detailGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.CancelEdit();
        e.Cancel = true;
        BindDetailData();
    }

    protected void detailGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        e.Cancel = true;
        grid.CancelEdit();
        BindDetailData();
    }

    protected void detailGrid_PageIndexChanged(object sender, EventArgs e)
    {
        BindDetailData();
    }
}
