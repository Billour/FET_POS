using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class VSS_CON06_CON06 : Advtek.Utility.BasePage
{

    public int total_sum = 0;

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

                show();
                Literal18.Visible = false;
                Label6.Visible = false;
                Order_TotalPrice.Visible = false;
                Label8.Visible = false;
            }
            else
            {
                // "101900073"
                lblOrderNo.Text = dno.ToString();
                DropDownList1.SelectedIndex = 1;

                bindMasterData();
                gvMaster.Visible = true;

                show();

            }

            ////
            //Literal20.Visible = false;
            //Label5.Visible = false;

            //Literal18.Visible = false;
            //Label6.Visible = false;
            //Order_TotalPrice.Visible = false;
            //Label8.Visible = false;

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

        if (DropDownList1.SelectedIndex > 0)
        {
            switch (DropDownList1.SelectedIndex)
            {
                case 1: // AC1
                    Literal20.Visible = true;
                    Label5.Visible = true;

                    Literal18.Visible = true;
                    Label6.Visible = true;
                    Order_TotalPrice.Visible = true;
                    Label8.Visible = true;

                    break;
                case 2: // AC2
                    Literal20.Visible = false;
                    Label5.Visible = false;

                    Literal18.Visible = true;
                    Label6.Visible = true;
                    Order_TotalPrice.Visible = true;
                    Label8.Visible = true;

                    break;
                case 3: // AP1
                    Literal20.Visible = true;
                    Label5.Visible = true;

                    Literal18.Visible = true;
                    Label6.Visible = true;
                    Order_TotalPrice.Visible = true;
                    Label8.Visible = true;

                    break;
            }
        }
        else
        {
            // 請選擇
            Literal20.Visible = false;
            Label5.Visible = false;

            Literal18.Visible = true;
            Label6.Visible = true;
            Order_TotalPrice.Visible = true;
            Label8.Visible = true;

        }

    }

    private void show()
    {
        if (DropDownList1.SelectedIndex == 1 || DropDownList1.SelectedIndex == 3)
        {
            Literal20.Visible = true;
            Label5.Visible = true;
        }
        else
        {
            Literal20.Visible = false;
            Label5.Visible = false;
        }

        Literal18.Visible = true;
        Label6.Visible = true;
        Order_TotalPrice.Visible = true;
        Label8.Visible = true;
        Order_TotalPrice.Text = total_sum.ToString();
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
}
