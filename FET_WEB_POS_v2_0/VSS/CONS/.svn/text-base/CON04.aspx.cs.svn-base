using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_CON04_CON04 : Advtek.Utility.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {            
            if (!string.IsNullOrEmpty(Request.QueryString["No"]))
            {
                ViewState["ReturnUrl"] = Request.UrlReferrer.ToString();
                ddlSupplierNo.SelectedIndex = 1;
                lblStatus.Text = "已存檔";

                txtProductName.Text = "iPhone 4";
                txtProductCode.Text = "69314900";
                ddlProductCategory.SelectedIndex = 1;
                Random rnd = new Random();
                SupportDateRangeFrom.Text = DateTime.Today.AddDays(-rnd.Next(0, 30)).ToString("yyyy/MM/dd");
                SupportDateRangeTo.Text = DateTime.Parse(SupportDateRangeFrom.Text).AddDays(rnd.Next(30, 90)).ToString("yyyy/MM/dd");
                ASPxDateEdit1.Text = SupportDateRangeTo.Text;
                bindMasterData();
                bindGridView1Data();

                txtAcct1.Text = "10";
                txtAcct2.Text = "15";
                txtAcct3.Text = "200011";
                txtAcct4.Text = "200012";
                txtAcct5.Text = "5000";
                txtAcct6.Text = "5001";
                txtUnit.Text = "組";
            }
            else
            {
                gvMaster.DataSource = new DataTable();
                gvMaster.DataBind();
                GridView1.DataSource = new DataTable();
                GridView1.DataBind();
            }

            //bindGridView1Data();
        }
        else
        {
            DataTable dtGvMaster = ViewState["gvMaster"] as DataTable;
            if (dtGvMaster != null)
            {
                gvMaster.DataSource = dtGvMaster;
                gvMaster.DataBind();
            }
            DataTable dtGridView1 = ViewState["GridView1"] as DataTable;
            if (dtGridView1 != null)
            {
                GridView1.DataSource = dtGridView1;
                GridView1.DataBind();
            }
        }
    }
    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    protected void bindGridView1Data()
    {
        DataTable dtResult = new DataTable();
        dtResult = getGridView1Data();
        ViewState["GridView1"] = dtResult;
        GridView1.DataSource = dtResult;
        GridView1.DataBind();
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("佣金比率", typeof(string));
        dtResult.Columns.Add("起始月份", typeof(string));
        dtResult.Columns.Add("結束月份", typeof(string));


        DataRow NewRow = dtResult.NewRow();
        NewRow["佣金比率"] = "10";
        NewRow["起始月份"] = "2010/07";
        NewRow["結束月份"] = "2010/10";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["佣金比率"] = "15";
        NewRow["起始月份"] = "2010/11";
        NewRow["結束月份"] = "";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }
    private DataTable getGridView1Data()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("更新日期", typeof(string));
        dtResult.Columns.Add("生效日期", typeof(string));
        dtResult.Columns.Add("失效日期", typeof(string));
        dtResult.Columns.Add("商品金額", typeof(string));

        for (int i = 0; i < 5; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["更新日期"] = "2010/07/01";
            NewRow["生效日期"] = "2010/07/" + (2 * i + 1).ToString("00");
            NewRow["失效日期"] = "2010/07/" + (2 * i + 2).ToString("00");
            NewRow["商品金額"] = 100 * (i + 1);
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        gvMaster.AddNewRow();        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ViewState["ReturnUrl"] != null)
        {
            Response.Redirect(ViewState["ReturnUrl"].ToString());
        }
        else
        {
            Response.Redirect("~/VSS/CONS/CON04.aspx");
        }
    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        gvMaster.CancelEdit();
        e.Cancel = true;
    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        gvMaster.CancelEdit();
        e.Cancel = true;
    }
}
