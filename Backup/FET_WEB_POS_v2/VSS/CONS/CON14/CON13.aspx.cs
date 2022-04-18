using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;

public partial class VSS_CONS_CON13 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //SuppNo.TextField = "SUPP_NO";
            //SuppNo.ValueField = "SUPP_NO";
            //DataTable DT = Supplier_Facade.GetSupplierNo(true);
            //DT.Rows[0][0] = "-請選擇-";
            //DT.AcceptChanges();
            //SuppNo.SelectedIndex = 0;
            //SuppNo.DataSource = DT;// Supplier_Facade.GetSupplierNo(true);
            //SuppNo.DataBind();

            // 繫結空的資料表，以顯示表頭欄位
            //gvMaster.DataSource = GetEmptyDataTable();
            //gvMaster.DataBind();
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



     protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvMaster.DetailRows.CollapseAllRows();
        bindMasterData();

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
        DataTable dtResult = new CON14_Facade().get_Master_VW_CON13_SELECT(ORDER_ID.Text, OENO.Text, STATUS.Text, PRODNO.Text, ORDERDTS.Text, ORDERDTE.Text, SuppNo.Text);
        return dtResult;
    }


    #region gvMaster 觸發事件
    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        gvMaster.DataSource = ViewState["gvMaster"];
        gvMaster.DataBind();
        gvMaster.DetailRows.CollapseAllRows();

    }
    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Detail)
        {
            // 繫結明細資料表           
            BindDetailData( e.GetValue("ORDNO").ToString());
        }
    }

    //Bind訂單明細
    protected void BindDetailData(  string strORDNO)
    {
        ASPxGridView detailGrid = gvMaster.FindChildControl<ASPxGridView>("gvDetail");
        detailGrid.Caption = string.Format("<DIV align='left' style='font-size:10pt'>訂單編號：{0}</DIV>", strORDNO);
        detailGrid.DataSource = new CON14_Facade().getDetail_VW_CON13_SELECT(strORDNO);
        detailGrid.DataBind();
    }

    protected void gvDetail_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView detailGrid = gvMaster.FindChildControl<ASPxGridView>("gvDetail");
        string M_ID = detailGrid.GetMasterRowKeyValue().ToString();
    }

    #endregion

  
    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getSuppInfo(string SuppNo)
    {
        string strInfo = "";
        try
        {
            if (!string.IsNullOrEmpty(SuppNo))
            {

                DataTable dtProd = CON06_PageHelper.GetSuppDataByKey(SuppNo);
                if (dtProd.Rows.Count > 0)
                {
                    strInfo = dtProd.Rows[0]["SUPP_ID"].ToString() + "∩" + dtProd.Rows[0]["SUPP_NAME"].ToString() + "∩" + dtProd.Rows[0]["AMOUNT_MAX"].ToString();
                }
            }
        }
        catch (Exception)
        {
            strInfo = "";
        }
        return strInfo;
    }
}
