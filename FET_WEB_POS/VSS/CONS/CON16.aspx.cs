using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_CON16_CON16 : Advtek.Utility.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string dno = Request.QueryString["dno"] == null ? "" : Request.QueryString["dno"].ToString().Trim();
        this.ViewState["dno"] = dno;

        if (!Page.IsPostBack)
        {
            //btnSave.Visible = Button2.Visible = Button4.Visible = false;
            if (this.ViewState["dno"].ToString() == "")
            {
                this.Label1.Text = "";
                bindEmptyData();

            }
            else
            {
                bindMasterData();
                btnOk.Visible = false;
            }
        }
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
        //ViewState["gvMaster"] = dtGvMaster;
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
    }

    private DataTable getMasterData()
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("廠商編號", typeof(string));
        dtMaster.Columns.Add("廠商名稱", typeof(string));
        dtMaster.Columns.Add("商品編號", typeof(string));
        dtMaster.Columns.Add("商品名稱", typeof(string));
        dtMaster.Columns.Add("庫存量", typeof(string));
        dtMaster.Columns.Add("門市盤點量", typeof(string));
        dtMaster.Columns.Add("盤差量", typeof(string));

        for (int i = 0; i < 5; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["廠商編號"] = "AC00" + i;
            dtMasterRow["廠商名稱"] = "全虹";
            dtMasterRow["商品編號"] = "A000" + i;
            dtMasterRow["商品名稱"] = "商品名稱" + i;
            dtMasterRow["庫存量"] = 5 + 2 * i;
            dtMasterRow["門市盤點量"] = 2+i;
            dtMasterRow["盤差量"] = 3 + i;
            dtMaster.Rows.Add(dtMasterRow);
        }
        return dtMaster;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblOrderNo.Text = "STC2010072801";
        Label1.Text = "已存檔";
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        bindMasterData();
       // btnSave.Visible = Button2.Visible = Button4.Visible = true;
    }

    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        //bindMasterData();
        //Label5.Text = System.DateTime.Now.ToString("yyyy/MM/dd");
        //btnSave.Visible = Button2.Visible = Button4.Visible = true;
    }
}
