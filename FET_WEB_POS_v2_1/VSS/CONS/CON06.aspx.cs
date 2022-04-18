using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class VSS_CONS_CON06 : BasePage
{

    public int total_sum
    {
        get
        {
            if (ViewState["total_sum"] == null)
            {
                ViewState["total_sum"] = 0;
            }
            return (int)ViewState["total_sum"];
        }

        set
        {
            ViewState["total_sum"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string dno = Request.QueryString["dno"] == null ? "" : Request.QueryString["dno"].ToString().Trim();
        this.ViewState["dno"] = dno;

        if (!IsPostBack)
        {
            if (this.ViewState["dno"].ToString() == "")
            {
                this.lblOrderNo.Text = "";
                DropDownList1.SelectedIndex = 0;
                // 繫結空的資料表，以顯示表頭欄位
                DataTable dtGvMaster = new DataTable();
                dtGvMaster = GetEmptyDataTable();
                ViewState["gvMaster"] = dtGvMaster;
                gvMaster.DataSource = dtGvMaster;
                gvMaster.DataBind();
                //gvMaster.
                show();

                changeLimitAmount();
            }
            else
            {
                // "101900073"
                lblOrderNo.Text = dno.ToString();
                DropDownList1.SelectedIndex = 1;

                bindMasterData();
                gvMaster.Visible = true;

                show();

                changeLimitAmount();
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

    private void changeLimitAmount()
    {
        gvMaster.TemplateControl.FindChildControl<ASPxLabel>("ASPxLabel1").Text = DropDownList1.SelectedItem.Value.ToString();
        gvMaster.TemplateControl.FindChildControl<ASPxLabel>("ASPxLabel2").Text = total_sum.ToString();
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
        dtMaster.Columns.Add("項次", typeof(int));
        dtMaster.Columns.Add("商品料號", typeof(string));
        dtMaster.Columns.Add("商品名稱", typeof(string));
        dtMaster.Columns.Add("商品類別", typeof(string));
        dtMaster.Columns.Add("建議訂購量", typeof(int));

        dtMaster.Columns.Add("實際訂購量", typeof(int));
        dtMaster.Columns.Add("單價", typeof(int));
        dtMaster.Columns.Add("總價", typeof(int));
        return dtMaster;
    }

    private DataTable getMasterData()
    {
        DataTable dtMaster = GetEmptyDataTable();

        string[] ary1 = { "3G Handset", "SIM Card", "3G Accessory", "On Line Recharge", "SIM Card", "3G Accessory" };

        for (int i = 0; i < 6; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["項次"] = i + 1;
            dtMasterRow["商品料號"] = "A000" + (i + 1);
            dtMasterRow["商品名稱"] = "測試1";
            dtMasterRow["商品類別"] = ary1[i % 3];
            dtMasterRow["建議訂購量"] = 100;

            dtMasterRow["實際訂購量"] = (i + 1) * 10;
            dtMasterRow["單價"] = 200;
            dtMasterRow["總價"] = (i + 1) * 10 * 200;

            total_sum = total_sum + int.Parse(dtMasterRow["總價"].ToString());

            dtMaster.Rows.Add(dtMasterRow);
        }


        return dtMaster;

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblOrderNo.Text = "101900073";
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        changeLimitAmount();
    }

    private void show()
    {
    }

    protected void AddButton_Click(object sender, EventArgs e)
    {
        gvMaster.AddNewRow();
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

    protected void btnSaleToOrder_Click(object sender, EventArgs e)
    {
        Response.Redirect("con06.aspx?dno=101900073");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("CON05.aspx");
    }
}
