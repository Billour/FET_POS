using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_CON12_CON12 : Advtek.Utility.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string dno = Request.QueryString["dno"] == null ? "" : Request.QueryString["dno"].ToString().Trim();
        this.ViewState["dno"] = dno;

        if (!IsPostBack)
        {
            if (this.ViewState["dno"].ToString() == "")
            {
                this.DropDownList1.SelectedIndex = 0;
                // 繫結空的資料表，以顯示表頭欄位
                gvMaster.DataSource = GetEmptyDataTable();
                gvMaster.DataBind();
            }
            else
            {
                // "COR2010073001"
                this.DropDownList1.SelectedIndex = 1;

                bindMasterData();
                //退倉日期-系統日
                lblReturnDate.Text = DateTime.Now.ToString("yyyy/MM/dd");

            }
        }
        else
        {
            DataTable dtGvMaster = ViewState["gvMaster"] as DataTable;
            if (dtGvMaster != null)
            {
                gvMaster.DataSource = dtGvMaster;
                gvMaster.DataBind();
            }
        }
    }

    protected void bindMasterData()
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = getMasterData();
        ViewState["gvMaster"] = dtGvMaster;
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("項次", typeof(string));
        dtMaster.Columns.Add("商品料號", typeof(string));
        dtMaster.Columns.Add("商品名稱", typeof(string));
        dtMaster.Columns.Add("廠商編號", typeof(string));
        dtMaster.Columns.Add("廠商名稱", typeof(string));
        dtMaster.Columns.Add("庫存數量", typeof(string));
        dtMaster.Columns.Add("實際退倉數量", typeof(string));
        return dtMaster;
    }

    private DataTable getMasterData()
    {
        DataTable dtMaster = GetEmptyDataTable();

        for (int i = 1; i < 6; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["項次"] = i;
            dtMasterRow["商品料號"] = "AC1110700" + i;
            dtMasterRow["商品名稱"] = "商品名稱" + i;
            dtMasterRow["廠商編號"] = "GG00001";
            dtMasterRow["廠商名稱"] = "廠商名稱" + i;
            dtMasterRow["庫存數量"] = i;
            dtMasterRow["實際退倉數量"] = "0";
            dtMaster.Rows.Add(dtMasterRow);
        }
        return dtMaster;
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        //lblOrderNo.Text = "COR2010073001";
        Label2.Text = "01 已存檔";

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (DropDownList1.SelectedIndex > 0)
        {
            bindMasterData();
            //退倉日期-系統日
            lblReturnDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }
        else
        {
            // 繫結空的資料表，以顯示表頭欄位
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("CON11.aspx");
    }
}
