using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Advtek.Utility;
using System.Data;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
using System.Web.Configuration;
using System.Web.UI.HtmlControls;

public partial class VSS_INV_INV13_INV13 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !Page.IsCallback)
        {
            //取得空的資料表
            gvMaster.DataSource = new DataTable();
            gvMaster.DataBind();

            if (logMsg.ROLE_TYPE == WebConfigurationManager.AppSettings["DefaultRoleHQ"])  //總部人員可查詢各門市資料, 門市人員只能查詢自己的門市
            {
                divSTORE.Visible = true;
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
        gvMaster.FocusedRowIndex = -1;
        gvMaster.DetailRows.CollapseAllRows();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //ASPxGridView detailGrid = gvMaster.FindChildControl<ASPxGridView>("gvDetail");
        //HtmlControl test = detailGrid.FindChildControl<HtmlControl>("fDownload");
        //string M_ID = StringUtil.CStr(detailGrid.GetRowValues(0, "NP_ORDER_M_ID"));
        //if (M_ID != "")
        //{
        //    DataTable dt = new INV13_Facade().QueryOrderDetailData(M_ID);
        //    Receipt rt = new Receipt();
        //    string Filename = rt.testbarcode(dt);
        //    string Path1 = @"/Downloads";
        //    string path = HttpContext.Current.Server.MapPath("~") + Path1 + Filename;
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PrintInvoice", "alert('列印條碼完成!');document.getElementById('" +
        //                                                          fDownload.ClientID + "').src='" + Request.ApplicationPath + Path1 + "/" + Filename + "';", true);
        //}

        //**2011/04/29 Tina：「條碼列印」改成跟「進貨驗收作業」一樣的方式 => 產生檔案至「C:\Program Files\遠傳標籤」路徑下
        ASPxGridView detailGrid = gvMaster.FindChildControl<ASPxGridView>("gvDetail");
        string M_ID = StringUtil.CStr(detailGrid.GetRowValues(0, "NP_ORDER_M_ID"));
        if (M_ID != "")
        {
            DataTable dt = new INV13_Facade().QueryOrderDetailData(M_ID);

            string NP_ORDER_ON = "NP_ORDER_ON";
            string barcodestr = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (barcodestr == "")
                {
                    barcodestr = StringUtil.CStr(dt.Rows[i]["PRODNO"]) + "^" + StringUtil.CStr(dt.Rows[i]["PRODNAME"]) + "^" + StringUtil.CStr(dt.Rows[i]["ORDER_QTY"]);
                }
                else
                {
                    barcodestr = barcodestr + "|" + StringUtil.CStr(dt.Rows[i]["PRODNO"]) + "^" + StringUtil.CStr(dt.Rows[i]["PRODNAME"]) + "^" + StringUtil.CStr(dt.Rows[i]["ORDER_QTY"]);
                }
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PrintInvoice", " Call_BarcodePrintFile(\"" + NP_ORDER_ON + "\",\"" + barcodestr + "\");", true);
            //**2011/04/29 Tina：代入的NP_ORDER_ON，原本作法是列印時要帶出進貨單號，但後來改成產生txt檔至特定路徑下，不需要NP_ORDER_ON，所以此NP_ORDER_ON隨意帶入一個值。
        }

    }

    public void ProcessRequest(string filename)
    {
        string filePath = "/FET_POS/Downloads/" + filename;
        ScriptManager.RegisterClientScriptBlock(this,
                                                this.GetType(),
                                                "test",
                                                "document.getElementById('" + fDownload1.ClientID + "').src='" + filePath + "';",
                                                true);
    }

    protected void BindMasterData()
    {
        if (!divSTORE.Visible)//總部人員可查詢各門市資料, 門市人員只能查詢自己的門市
        {
            txtStoreNO.Text = logMsg.STORENO;
        }
        gvMaster.DataSource = new INV13_Facade().QueryOrderData(txtSDate.Text, txtEDate.Text, txtStoreNO.Text);
        gvMaster.DataBind();
        gvMaster.CollapseAll();
    }

    protected void BindDetailData(string M_ID)
    {

        ASPxGridView detailGrid = gvMaster.FindChildControl<ASPxGridView>("gvDetail");
        detailGrid.DataSource = new INV13_Facade().QueryOrderDetailData(M_ID);
        detailGrid.DataBind();
        //   tempMID.Value = M_ID;
       
        if (divSTORE.Visible)//總部人員不需顯示條碼列印的Button
        {
            HtmlGenericControl divButton = gvMaster.FindChildControl<HtmlGenericControl>("divButton");
            divButton.Visible = false;
        }

    }

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Detail)
        {
            // 繫結明細資料表           
            BindDetailData(StringUtil.CStr(e.GetValue(gvMaster.KeyFieldName)));
        }
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.DetailRows.CollapseAllRows();
    }

    protected void gvDetail_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView detailGrid = gvMaster.FindChildControl<ASPxGridView>("gvDetail");
        string M_ID = StringUtil.CStr(detailGrid.GetRowValues(0, "NP_ORDER_M_ID"));
        tempMID.Value = M_ID;
       
        BindDetailData(M_ID);
    }

    
}
