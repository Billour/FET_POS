using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;

using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using System.Xml;
using System.Threading;
using Advtek.Utility;

public partial class VSS_CONS_CON16_1 : BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !IsCallback)
        {
            //取得空的資料表
            gvMaster.DataSource = new CONS16_CSM_STOCKCHK_M().CSM_STOCKCHK_M;
            gvMaster.DataBind();
        } 
    }

    private void BindMasterData()
    {
        gvMaster.DataSource = new CON16_Facade().Query_StockChkM(this.txtStkchkNo.Text, this.txtStkchkDateS.Text, this.txtStkchkDateE.Text, this.logMsg.STORENO, this.dropStyle.SelectedItem.Value.ToString());
        gvMaster.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        BindgvDetailData("    ");
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            if (e.GetValue("STATUS").ToString() == "已盤點")
            {
                e.Row.Style.Add(HtmlTextWriterStyle.Color, "red");
            }
        }
      
    }
    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }

    protected void BindgvDetailData(string strSTKCHKNO)
    {
        gvDetail.Visible = true;
        DataTable dt = new CON16_Facade().Query_StockChkD(strSTKCHKNO);
        gvDetail.Caption = string.Format("<DIV align='left' style='font-size:10pt'>盤點單號：{0}</DIV>", strSTKCHKNO);
        gvDetail.DataSource = dt;
        gvDetail.DataBind();
    }
    protected void gvMaster_FocusedRowChanged(object sender, EventArgs e)
    {
        changeGridView();
    }

    private void changeGridView()
    {
        if (gvMaster.FocusedRowIndex >= 0)
        {
            int focusedRowIndex = gvMaster.FocusedRowIndex;
            Session["strSTKCHKNO"] = gvMaster.GetRowValues(focusedRowIndex, gvMaster.KeyFieldName);
            BindgvDetailData(Session["strSTKCHKNO"].ToString());
        }
    }
    protected void gvDetail_PageIndexChanged(object sender, EventArgs e)
    {
        BindgvDetailData(Session["strSTKCHKNO"].ToString());
    }

}
