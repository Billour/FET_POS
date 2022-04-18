using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;

public partial class VSS_INV_INV08 : BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //供貨商GetSupplierTypes
            bindDdlValTxt(Supplier, INV08_PageHelper.GetSupplierTypes(true, logMsg.STORENO), "SUPPNO", "SUPPNAME");
            bindMasterData(0);
        }

    }

    protected void bindDdlValTxt(ASPxComboBox AspCB, object dataSrc, string valCol, string txtCol)
    {
        AspCB.DataSource = dataSrc;
        AspCB.ValueField = valCol;
        AspCB.TextField = txtCol;
        AspCB.DataBind();
        AspCB.SelectedIndex = 0;
    }

    private void bindMasterData(int _count)
    {
        gvMaster.PageIndex = 0;
        Session["Data"] = getMasterData(_count);
        gvMaster.DataSource = Session["Data"];
        gvMaster.DataBind();
    }

    private DataTable getMasterData(int _count)
    {
        DataTable dtResult = new DataTable();
        INV08_Facade _INV08_Facade = new INV08_Facade();

        dtResult = _INV08_Facade.Query_INVMethodSet(txtPOEONO.Text,
                                                    StringUtil.CStr(Supplier.SelectedItem.Value),
                                                    txtPROD.Text,
                                                    txtORDERNO.Text,
                                                    CHK_SDATE.Text,
                                                    CHK_EDATE.Text,
                                                    StringUtil.CStr(txtSTATUS.SelectedItem.Value),
                                                    ORD_SDATE.Text,
                                                    ORD_EDATE.Text,
                                                    _count,
                                                    logMsg.STORENO);

        return dtResult;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData(1);
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {

        if (e.RowType == GridViewRowType.Data)
        {
            DataRowView row = gvMaster.GetRow(e.VisibleIndex) as DataRowView;
            string s = StringUtil.CStr(row["STATUS"]);
            if (s != "已結案")
            {
                e.Row.Style.Add(HtmlTextWriterStyle.Color, "red");
            }
            ASPxHyperLink link = (ASPxHyperLink)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns[0], "ASPxHyperLink1");
            string encryptUrl = Utils.Param_Encrypt("PO/OE_NO=" + 
                StringUtil.CStr(e.GetValue("PO_OE_NO")) + 
                "&ReceivingNo=" + StringUtil.CStr(e.GetValue("INV_APPROVE_NO")) + 
                "&ORDER_NO=" + StringUtil.CStr(e.GetValue("ORDER_NO")) + 
                "&STATUS=10" + "&STORE_NO=" + StringUtil.CStr(e.GetValue("STORE_NO")));
            string Url = string.Format("~/VSS/INV/INV08/INV09.aspx?Param={0}", encryptUrl);
            

            link.NavigateUrl = Url;
            link.Text = StringUtil.CStr(e.GetValue("PO_OE_NO"));

            ASPxHyperLink lin1 = (ASPxHyperLink)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns[2], "ASPxHyperLink2");
            string encryptUrl2= Utils.Param_Encrypt("PO/OE_NO=" + StringUtil.CStr(e.GetValue("PO_OE_NO")) +
                "&ReceivingNo=" + StringUtil.CStr(e.GetValue("INV_APPROVE_NO")) +
                "&STATUS=20" + 
                "&ORDER_NO=" + StringUtil.CStr(e.GetValue("ORDER_NO")) + 
                "&STORE_NO=" + StringUtil.CStr(e.GetValue("STORE_NO")));
            string Url2=string.Format("~/VSS/INV/INV08/INV09.aspx?Param={0}", encryptUrl2);
            lin1.NavigateUrl = Url2;
            lin1.Text = (StringUtil.CStr(e.GetValue("INV_APPROVE_NO")).Trim() == "" ? "." : StringUtil.CStr(e.GetValue("INV_APPROVE_NO")));


            if (s == "已結案")
            {
                link.Enabled = false;
                link.ForeColor = System.Drawing.Color.Black;
            }
            else if (s == "未驗收")
            {
                lin1.Enabled = false;
                lin1.ForeColor = System.Drawing.Color.Black;
            }
        }

    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView gvMaster = sender as ASPxGridView;
        gvMaster.DataSource = Session["Data"];
        gvMaster.DataBind();
    }

}
