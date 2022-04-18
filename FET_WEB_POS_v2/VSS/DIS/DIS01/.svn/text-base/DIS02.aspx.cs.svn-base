using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
using Advtek.Utility;

public partial class VSS_DIS_DIS02 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Category.DataSource = getDiscountCategory();
            Category.TextField = "Field";
            Category.ValueField = "Value";
            Category.DataBind();
            Category.SelectedIndex = 0;
        }
    }

    private void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }

    private DataTable getMasterData()
    {
        string sCategory = StringUtil.CStr(Category.SelectedItem.Value); //類別
        string sPartNumberOfDiscount = PartNumberOfDiscount.Text.Trim();//折扣料號
        string sDiscountName = DiscountName.Text.Trim();//折扣名稱
        string sDiscountAmount = DiscountAmount.Text.Trim();//折扣金額
        string sDiscountRate = DiscountRate.Text.Trim();//折扣比率
        string sS_DATE = ASPxDateEdit1.Text.Trim();//有效期間(起)
        string sE_DATE = ASPxDateEdit2.Text.Trim();//有效期間(迄)
        //檢查起迄
        if (!(string.IsNullOrEmpty(sE_DATE)) && !(string.IsNullOrEmpty(sS_DATE)) && Convert.ToDateTime(sE_DATE) < Convert.ToDateTime(sS_DATE))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Common", "alert('日期迄日須大於起日，請重新輸入!!');", true);
            return new DataTable();
        }
        return new DIS02_Facade().Query_DiscountMaster(sCategory, sPartNumberOfDiscount, sDiscountName, sDiscountAmount, sDiscountRate, sS_DATE, sE_DATE);
    }

    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        bindMasterData();
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            ASPxHyperLink link = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["DISCOUNT_CODE"], "ASPxHyperLink1") as ASPxHyperLink;
            string status = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "STATUS"));
            if (status != "已過期")
            {
                //link.NavigateUrl = "~/VSS/DIS/DIS01/DIS01.aspx?" + StringUtil.CStr(Request.QueryString) + "&DiscountCodeUUID=" + e.GetValue("DISCOUNT_MASTER_ID");

                //**2011/04/20 Tina：傳遞參數時，要先以加密處理。
                string encryptUrl = Utils.Param_Encrypt("DiscountCodeUUID=" + e.GetValue("DISCOUNT_MASTER_ID"));
                link.NavigateUrl = string.Format("~/VSS/DIS/DIS01/DIS01.aspx?Param={0}", encryptUrl);
            }
        }

    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }

    /// <summary>
    /// 取得折扣類別資料(固定式)
    /// </summary>
    /// <returns></returns>
    private DataTable getDiscountCategory()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("Field", typeof(string)));
        dt.Columns.Add(new DataColumn("Value", typeof(string)));
        string[] arrStr = new string[] { 
                "ALL", "0",
                "一般", "1",
                "贈品設定", "6", 
                "加價購", "7", 
                "舊機回收","2" ,
                "租賃","3",
                "特殊折扣","4" ,
                "HappyGo折扣","5",
                "網購折扣","99"};
        DataRow dr;
        for (int i = 0; i < arrStr.Length; i += 2)
        {
            dr = dt.NewRow();
            dr["Field"] = arrStr[i];
            dr["Value"] = arrStr[i + 1];
            dt.Rows.Add(dr);
        }
        return dt;
    }

}
