using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_CONS_CON14 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblOrderNo.Text = Request.QueryString["SlipNo"];
            bindEmptyData();
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
        ReceivedDate1.Text = DateTime.Now.ToString("yyyy/MM/dd");
    }

    protected void bindEmptyData()
    {

        DataTable dtResult = new DataTable();

        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
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
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("進貨編號", typeof(string));
        dtResult.Columns.Add("商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("廠商編號", typeof(string));
        dtResult.Columns.Add("廠商名稱", typeof(string));
        dtResult.Columns.Add("實際到貨數量", typeof(int));
        dtResult.Columns.Add("驗收量", typeof(int));
        dtResult.Columns.Add("備註", typeof(string));


        return dtResult;
    }

    private DataTable getMasterData()
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("進貨編號", typeof(string));
        dtMaster.Columns.Add("商品編號", typeof(string));
        dtMaster.Columns.Add("商品名稱", typeof(string));
        dtMaster.Columns.Add("廠商編號", typeof(string));
        dtMaster.Columns.Add("廠商名稱", typeof(string));
        dtMaster.Columns.Add("實際到貨數量", typeof(int));
        dtMaster.Columns.Add("驗收量", typeof(int));
        dtMaster.Columns.Add("備註", typeof(string));

        Random rnd = new Random();

        string[] rnn = { "501001", "09876543", "123456789", "09854543", "3456789", "428771", "121270", "761234", "46551", "5555123" };

        for (int i = 1; i <= 10; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["進貨編號"] = rnn[i - 1]; //lblOrderNo.Text;//"CAAC120100728" + i.ToString("0#");;            
            dtMasterRow["商品編號"] = "1001002" + i.ToString("0#");
            dtMasterRow["商品名稱"] = "手機" + Convert.ToChar(64 + i).ToString();
            dtMasterRow["廠商編號"] = "AC" + i;
            dtMasterRow["廠商名稱"] = "廠商名稱" + i;
            dtMasterRow["實際到貨數量"] = rnd.Next(1, 10);
            dtMasterRow["驗收量"] = 0;
            dtMasterRow["備註"] = "";//"備註"+i;
            dtMaster.Rows.Add(dtMasterRow);
        }
        return dtMaster;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "已驗收";
        lblModifiedDate.Text = DateTime.Now.ToString("yy/MM/dd HH:mm");
        lblOrderNo.Text = "CAAC12010072800";
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindMasterData();
        lblStatus.Text = "待進貨";
        divButtons.Visible = true;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("CON13.aspx");
    }
}
