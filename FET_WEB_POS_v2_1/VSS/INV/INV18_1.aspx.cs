using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using DevExpress.Web.ASPxGridView;

public partial class VSS_INV_INV18_1 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string dno = Request.QueryString["dno"] == null ? "" : Request.QueryString["dno"].ToString().Trim();
        this.ViewState["dno"] = dno;

        if (!IsPostBack)
        {

            if (this.ViewState["dno"].ToString() == "")
            {
                this.lblOrderNo.Text = "";
                bindEmptyData();
            }
            else 
            {
                // "SA2011-1009003";
                lblOrderNo.Text = dno.ToString();
                bindgvMaster();
                btnSave.Visible = true;
            }
        }
    }

    protected void bindEmptyData()
    {

        DataTable dtResult = new DataTable();

        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
        
    }
    protected void bindgvMaster()
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
        dtMaster.Columns.Add("項次", typeof(int));
        dtMaster.Columns.Add("商品料號", typeof(string));
        dtMaster.Columns.Add("商品名稱", typeof(string));
        dtMaster.Columns.Add("庫存量", typeof(string));
        dtMaster.Columns.Add("調整量", typeof(int));
        dtMaster.Columns.Add("調整原因", typeof(string));


        for (int i = 1; i < 12; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["項次"] = i;
            dtMasterRow["商品料號"] = "A000" + i;
            dtMasterRow["商品名稱"] = "商品名稱" + i;
            dtMasterRow["庫存量"] = 1500 * (i + 1);
            dtMasterRow["調整量"] = 1000 * (i + 1);
            dtMasterRow["調整原因"] = "調整原因" + i;

            dtMaster.Rows.Add(dtMasterRow);
        }
        return dtMaster;

    }
    

    private DataTable getGvProdDetailData()
    {
        DataTable dtProdDetail = new DataTable();
        //dtProdDetail.Columns.Clear();
        //dtProdDetail.Columns.Add("商品編號", typeof(string));
        //dtProdDetail.Columns.Add("商品名稱", typeof(string));
        //dtProdDetail.Columns.Add("數量", typeof(int));

        //for (int i = 1; i < 2; i++)
        //{
        //    DataRow dtProdDetailRow = dtProdDetail.NewRow();
        //    dtProdDetailRow["商品編號"] = "A000" + i;
        //    dtProdDetailRow["商品名稱"] = "商品名稱" + i;
        //    dtProdDetailRow["數量"] = 100 * (i + 1);
        //    dtProdDetail.Rows.Add(dtProdDetailRow);
        //}
        return dtProdDetail;

    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        gvMaster.AddNewRow();
    }

    



    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblOrderNo.Text = "SA2011-1008001";
        //"GG000001";

    }

    protected void gvMaster_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //GridPageSize = int.Parse(e.Parameters);
        gvMaster.SettingsPager.PageSize = int.Parse(e.Parameters);
        gvMaster.DataBind();
    }

    /// <summary>
    /// The HtmlRowPrepared event is raised for each grid row (data row, group row, etc.) 
    /// within the ASPxGridView. 
    /// You can handle this event to change the style settings of individual rows.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
        ASPxGridView gvMaster = sender as ASPxGridView;
        gvMaster.DataSource = getMasterData();
        gvMaster.DataBind();
    }


    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        gvMaster.CancelEdit();
        e.Cancel = true;
        gvMaster.DataSource = getMasterData();
        gvMaster.DataBind();
        //e.NewValues["TemplateID"] = Guid.NewGuid(); // I set this PK myself.
    }

  

}
