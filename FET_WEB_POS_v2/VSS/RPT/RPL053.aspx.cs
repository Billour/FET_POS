using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FET.POS.Model.Facade.FacadeImpl;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using System.Data;
using Advtek.Utility;


public partial class VSS_RPT_RPL053 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (StringUtil.CStr(logMsg.STORENO) != StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"]))
        {
            this.txtSTOREStart.Enabled = false;
            this.txtSTOREEnd.Enabled = false;
            this.txtSTOREStart.Text = StringUtil.CStr(logMsg.STORENO);
            this.txtSTOREEnd.Text = StringUtil.CStr(logMsg.STORENO);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
    }



    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        gvMaster.DataSource = Session["Data"];
        gvMaster.DataBind();
    }


    private void BindMasterData()
    {


        Session["Data"] = GetQueryData();
        this.gvMaster.DataSource = Session["Data"];
        this.gvMaster.DataBind();

    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (StringUtil.CStr(logMsg.STORENO) == StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"]))
        {
            this.txtSTOREStart.Text = "";
            this.txtSTOREEnd.Text = "";
        }

        this.txtOrdDateStart.Text = "";
        this.txtOrdDateEnd.Text = "";
        this.txtProductTypeNoS.Text = "";
        this.txtProductTypeNoE.Text = "";
        this.txtProdNo_S.Text = "";
        this.txtProdNo_E.Text = "";

        this.RadioAll.Checked = false;
        this.RadioButton1.Checked = true;
        this.RadioButton2.Checked = false;
        gvMaster.DataSource = null;
        gvMaster.DataBind();

    }


    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = GetQueryData();
        DataTable dtheader = new DataTable();
        dtheader.Columns.Add("header1", typeof(string));
        dtheader.Columns.Add("header2", typeof(string));
        DataRow NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "門市編號： " + this.txtSTOREStart.Text + "~" + this.txtSTOREEnd.Text;
        NewRowHeader["header2"] = "列印日期： " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        dtheader.Rows.Add(NewRowHeader);

        NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "交易日期： " + this.txtOrdDateStart.Text + "~" + this.txtOrdDateEnd.Text;
        NewRowHeader["header2"] = "列印人員： " + logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER);
        dtheader.Rows.Add(NewRowHeader);

        NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "商品類別： " + this.txtProductTypeNoS.Text + "~" + this.txtProductTypeNoE.Text;
        NewRowHeader["header2"] = "頁　　次：1 ";
        dtheader.Rows.Add(NewRowHeader);

        NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "商品料號： " + this.txtProdNo_S.Text + "~" + this.txtProdNo_E.Text;
        NewRowHeader["header2"] = "總 筆 數： " + dt.Rows.Count;
        dtheader.Rows.Add(NewRowHeader);




        string filename = new Output().Print("RPL053", "庫存日報表", dtheader, dt, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("StockSearch.xls"));
        //ProcessRequest(filename);
        Response.Close();
    }


    public DataTable GetQueryData()
    {
        string Search_list = "";
        if (this.RadioAll.Checked)
        {
            Search_list = "ALL";
        }
        else if (this.RadioButton1.Checked)
        {
            Search_list = "0";
        }
        else if (this.RadioButton2.Checked)
        {
            Search_list = "1";
        }

        return new RPL_Facade().RPL053(
            Search_list, this.txtSTOREStart.Text, this.txtSTOREEnd.Text, this.txtOrdDateStart.Text, this.txtOrdDateEnd.Text
            , this.txtProductTypeNoS.Text, this.txtProductTypeNoE.Text, this.txtProdNo_S.Text, this.txtProdNo_E.Text);
    }


}
