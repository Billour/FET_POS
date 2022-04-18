using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using Advtek.Utility;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
public partial class VSS_INV11_INV11 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string dno = Request.QueryString["InventoryNo"] == null ? "" : Request.QueryString["InventoryNo"].ToString().Trim();
        this.ViewState["InventoryNo"] = dno;


        if (!IsPostBack)
        {


            if (this.ViewState["InventoryNo"] == "")
            {
                this.Label1.Text = "SC2101-1007002";
                gvMaster.Visible = false;
                

            }
            else
            {

                Label1.Text = dno.ToString();
                RadioButtonList1.Enabled = false;
                bindMasterData();
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
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("倉別", typeof(string));
        dtResult.Columns.Add("商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("單位", typeof(string));
        dtResult.Columns.Add("帳上庫存", typeof(string));
        dtResult.Columns.Add("門市盤點量", typeof(string));
        dtResult.Columns.Add("盤差量", typeof(string));

        Random rnd = new Random();


        for (int i = 0; i < 3; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = i + 1;
            NewRow["倉別"] = "銷售倉";
            NewRow["商品編號"] = "00000" + i;
            NewRow["商品名稱"] = "商品名稱" + i;
            NewRow["單位"] = "SET";

            int inv = 7 * (i + 1) + i;

            NewRow["帳上庫存"] = inv;
            NewRow["門市盤點量"] = rnd.Next(inv - 3, inv);
            NewRow["盤差量"] = Convert.ToInt32(NewRow["帳上庫存"]) - Convert.ToInt32(NewRow["門市盤點量"]);
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }


    protected void Button5_Click(object sender, EventArgs e)
    {
        gvMaster.Visible = true;
        btnSave.Visible = true;
        btnCancel.Visible = true;
        bindMasterData();
        gvMaster.Style["display"] = "";
    }
}
