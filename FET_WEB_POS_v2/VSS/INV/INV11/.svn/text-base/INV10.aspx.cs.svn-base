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

public partial class VSS_INV_INV10 : BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !IsCallback)
        {
            //取得空的資料表
            gvMaster.DataSource = new INV11_StockCHK_DTO().STOCKCHK_M;
            gvMaster.DataBind();
        } 
    }

    private void BindMasterData()
    {
        gvMaster.DataSource = new INV11_Facade().Query_StockChkM(this.txtStkchkNo.Text, this.txtStkchkDateS.Text, this.txtStkchkDateE.Text, this.logMsg.STORENO);
        gvMaster.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            //** 2011/03/16 Tina：SA 4.5.2.1=>查詢結果，若有符合查詢結果，該次盤點若己盤點完成且有盤差，則以紅色顯示。
            if (StringUtil.CStr(e.GetValue("STATUS")) == "已盤點")
            {
                if (Convert.ToInt64(e.GetValue("SUM_DIFF_STKQTY")) != 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.Color, "red");
                }
            }
            DevExpress.Web.ASPxGridView.GridViewDataTextColumn col = new GridViewDataTextColumn();
            col = (GridViewDataTextColumn)((ASPxGridView)sender).Columns["STKCHKNO"];

            HyperLink hl = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, col, "HyperLink1") as HyperLink;
            if (hl != null)
            {
                string encryptUrl = Utils.Param_Encrypt("InventoryNo=" + hl.Text);
                string Url = string.Format("~/VSS/INV/INV11/INV11.aspx?Param={0}", encryptUrl);

                hl.NavigateUrl = Url;
            }
        }
      
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }

}
