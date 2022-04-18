using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Data;

using Advtek.Utility;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using System.Web.UI.HtmlControls;
using FET.POS.Model.Facade.FacadeImpl;
using NPOI.HSSF.UserModel;
using FET.POS.Model.Common;

public partial class VSS_ORD_ORD04_ORD04 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (logMsg.STORENO != StringUtil.CStr(System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultStore"]))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);
                gvMasterDV.Enabled = false;
                gvDetailDV.Enabled = false;
                btnSearch.Enabled = false;
                btnReset.Enabled = false;
                Literal8.Text = "";     //更新人員
                ddlArea.ClientEnabled = false;     //區域別
                txtOrdNoStart.ClientEnabled = false;     //訂單編號起
                txtOrdNoEnd.ClientEnabled = false;     //訂單編號訖
                txtbox1.ClientEnabled = false;     //門市編號
                txtOrdDateStart.ClientEnabled = false;     //訂單日期起
                txtOrdDateStart.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                txtOrdDateEnd.ClientEnabled = false;     //訂單日期訖
                txtOrdDateEnd.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                Literal6.Text = "";     //更新日期
                ASPxTextBox1.ClientEnabled = false;     //商品料號
                ASPxComboBox1.ClientEnabled = false;     //訂單狀態
                return;
            }
            else
            {
                bindZoneDistrict();
                bindEmptyData();
                Literal8.Text = logMsg.OPERATOR + "-" + new Employee_Facade().GetEmpName(logMsg.OPERATOR);
                Literal6.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                txtOrdDateStart.Text = DateTime.Now.ToString("yyyy/MM/dd");
                txtOrdDateEnd.Text = DateTime.Now.ToString("yyyy/MM/dd");
            }
        }
    }

    protected void bindZoneDistrict()
    {
        ASPxComboBox ddlDistrict = this.ddlArea as ASPxComboBox;
        ddlDistrict.DataSource = Common_PageHelper.getZone(true);
        ddlDistrict.TextField = "ZONE_NAME";
        ddlDistrict.ValueField = "ZONE";
        ddlDistrict.DataBind();
        ddlDistrict.SelectedIndex = 0;

    }

    protected void GetData1()
    {
        string txtOrdNoStart1 = txtOrdNoStart.Text;
        string txtOrdNoEnd1 = txtOrdNoEnd.Text;
        string storeno = txtbox1.Text;
        string ORDDate = txtOrdDateStart.Text;
        string ORDDate1 = txtOrdDateEnd.Text;
        string PRODNO = ASPxTextBox1.Text;
        string Status = StringUtil.CStr(ASPxComboBox1.SelectedItem.Value);
        string zone = StringUtil.CStr(ddlArea.SelectedItem.Value);

        ORD04_Facade facade04 = new ORD04_Facade();

        DataTable dt = new DataTable();
        if (gvDetailDV.FocusedRowIndex != -1)
        {
            string PRODNO1 = StringUtil.CStr(gvDetailDV.GetRowValues(gvDetailDV.FocusedRowIndex, "PRODNO"));
            dt = facade04.TopDatatable1(txtOrdNoStart1, txtOrdNoEnd1, storeno, ORDDate, ORDDate1, PRODNO1, Status, zone);
        }
        else
        {
            dt = facade04.TopDatatable1(txtOrdNoStart1, txtOrdNoEnd1, storeno, ORDDate, ORDDate1, PRODNO, Status, zone);
        }

        gvDetailDV.DataSource = null;
        gvDetailDV.DataBind();

        DataTable dt2 = new DataTable();
        dt2.Clear();
        dt2 = facade04.TopDatatable2(txtOrdNoStart1, txtOrdNoEnd1, storeno, ORDDate, ORDDate1, PRODNO, Status, zone);

        gvDetailDV.DataSource = dt2;
        gvDetailDV.Visible = true;
        gvDetailDV.DataBind();
        gvMasterDV.DataSource = dt;
        gvMasterDV.DataBind();
    }

    protected void GetData()
    {
        string txtOrdNoStart1 = txtOrdNoStart.Text;
        string txtOrdNoEnd1 = txtOrdNoEnd.Text;
        string storeno = txtbox1.Text;
        string ORDDate = txtOrdDateStart.Text;
        string ORDDate1 = txtOrdDateEnd.Text;
        string PRODNO = ASPxTextBox1.Text;
        string Status = StringUtil.CStr(ASPxComboBox1.SelectedItem.Value);
        string zone = StringUtil.CStr(ddlArea.SelectedItem.Value);

        ORD04_Facade facade04 = new ORD04_Facade();

        DataTable dt = new DataTable();

        gvDetailDV.FocusedRowIndex = -1;


        dt = facade04.TopDatatable1(txtOrdNoStart1, txtOrdNoEnd1, storeno, ORDDate, ORDDate1, PRODNO, Status,zone);


        gvDetailDV.DataSource = null;
        gvDetailDV.DataBind();

        DataTable dt2 = new DataTable();
        dt2.Clear();
        dt2 = facade04.TopDatatable2(txtOrdNoStart1, txtOrdNoEnd1, storeno, ORDDate, ORDDate1, PRODNO, Status, zone);

        gvDetailDV.DataSource = dt2;
        gvDetailDV.Visible = true;
        gvDetailDV.DataBind();
        gvMasterDV.DataSource = dt;
        gvMasterDV.DataBind();
        gvDetailDV.FocusedRowIndex = -1;

    }

    protected void bindEmptyData()
    {
        DataTable dtResult = new DataTable();

        gvMasterDV.DataSource = dtResult;
        gvMasterDV.DataBind();
        gvDetailDV.DataSource = dtResult;
        gvDetailDV.DataBind();
    }

    #region Button 觸發事件

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetData();
        Literal6.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
        this.gvDetailDV.PageIndex = 0;
        this.gvMasterDV.PageIndex = 0;
        this.gvDetailDV.FocusedRowIndex = -1;
        this.gvMasterDV.FocusedRowIndex = -1;

    }

    protected void btnReBindData_Click(object sender, EventArgs e)
    {
        string txtOrdNoStart1 = txtOrdNoStart.Text;
        string txtOrdNoEnd1 = txtOrdNoEnd.Text;
        string storeno = txtbox1.Text;
        string ORDDate = txtOrdDateStart.Text;
        string ORDDate1 = txtOrdDateEnd.Text;
        string PRODNO = ASPxTextBox1.Text;
        string Status = StringUtil.CStr(ASPxComboBox1.SelectedItem.Value);
        string zone = StringUtil.CStr(ddlArea.SelectedItem.Value);

        ORD04_Facade facade04 = new ORD04_Facade();

        DataTable dt = new DataTable();
        if (gvDetailDV.FocusedRowIndex != -1)
        {
            string PRODNO1 = StringUtil.CStr(gvDetailDV.GetRowValues(gvDetailDV.FocusedRowIndex, "PRODNO"));
            dt = facade04.TopDatatable1(txtOrdNoStart1, txtOrdNoEnd1, storeno, ORDDate, ORDDate1, PRODNO1, Status, zone);
        }
        else
        {
            dt = facade04.TopDatatable1(txtOrdNoStart1, txtOrdNoEnd1, storeno, ORDDate, ORDDate1, PRODNO, Status, zone);
        }

        gvDetailDV.DataSource = null;
        gvDetailDV.DataBind();

        DataTable dt2 = new DataTable();
        dt2.Clear();
        dt2 = facade04.TopDatatable2(txtOrdNoStart1, txtOrdNoEnd1, storeno, ORDDate, ORDDate1, PRODNO, Status, zone);

        gvDetailDV.DataSource = dt2;
        gvDetailDV.Visible = true;
        gvDetailDV.DataBind();
        gvMasterDV.DataSource = dt;
        gvMasterDV.DataBind();
        gvDetailDV.FocusedRowIndex = -1;
    }

    #endregion

    #region gvMaster 觸發事件

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
              
                string STATUS = StringUtil.CStr(gvMasterDV.GetRowValues(e.VisibleIndex, "STATUS"));
               
                if (STATUS == "已傳輸")
                {
                    e.Enabled = true;
                }
                else
                {
                    e.Enabled = false;
                }

            }
        }
    }

    protected void gvMasterDV_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ORD04_Facade Facade = new ORD04_Facade();
        ORD04_ORDERM ORDERM_DTO = new ORD04_ORDERM();
        ORD04_ORDERM.ORDER_DDataTable dtRD;
        ORD04_ORDERM.ORDER_DRow drRD;
        dtRD = ORDERM_DTO.Tables["ORDER_D"] as ORD04_ORDERM.ORDER_DDataTable;
        drRD = dtRD.NewORDER_DRow();

        DataTable ORDERD = ORD04_PageHelper.GetORDER(StringUtil.CStr(e.Keys["ORDER_ITEMS_ID"]));

        drRD.ORDER_ITEMS_ID = StringUtil.CStr(e.Keys["ORDER_ITEMS_ID"]);
        if (e.NewValues["REMARK"] == null)
        {
            drRD.REMARK = "";
        }
        else
        {
            drRD.REMARK = StringUtil.CStr(e.NewValues["REMARK"]);
        }
        drRD.HQ_ADJ_ORDER_QTY = Convert.ToInt32(StringUtil.CStr(e.NewValues["HQ_ADJ_ORDER_QTY"]));

        dtRD.Rows.Add(drRD);


        //一搭一商品之調整數量亦同步調整到搭贈商品之調整數量
        string Order_ID = StringUtil.CStr(gvMasterDV.GetRowValues(gvMasterDV.FocusedRowIndex, "ORDER_ID"));
        string ProdNo = StringUtil.CStr(gvMasterDV.GetRowValues(gvMasterDV.FocusedRowIndex, "PRODNO"));
        DataTable dtONE = ORD04_PageHelper.GetOneORDER_ITEMS_ID(Order_ID, ProdNo);
        foreach (DataRow dr in dtONE.Rows)
        {
            drRD = dtRD.NewORDER_DRow();
            if (!string.IsNullOrEmpty(StringUtil.CStr(dr["ORDER_ITEMS_ID"])))
            {
                drRD.ORDER_ITEMS_ID = StringUtil.CStr(dr["ORDER_ITEMS_ID"]);

                if (e.NewValues["REMARK"] == null)
                {
                    drRD.REMARK = "";
                }
                else
                {
                    drRD.REMARK = StringUtil.CStr(e.NewValues["REMARK"]);
                }
                drRD.HQ_ADJ_ORDER_QTY = Convert.ToInt32(StringUtil.CStr(e.NewValues["HQ_ADJ_ORDER_QTY"]));

                dtRD.Rows.Add(drRD);
            }
        }

        //// 一搭一商品之調整數量亦同步調整到主商品之調整數量
        //DataTable dtONE = ORD04_PageHelper.GetOneORDER_ITEMS_ID(StringUtil.CStr(e.Keys["ORDER_ITEMS_ID"]));
        //foreach (DataRow dr in dtONE.Rows)
        //{
        //    drRD = dtRD.NewORDER_DRow();
        //    if (!string.IsNullOrEmpty(StringUtil.CStr(dr["ORDER_ITEMS_ID"])))
        //    {
        //        drRD.ORDER_ITEMS_ID = StringUtil.CStr(dr["ORDER_ITEMS_ID"]);

        //        if (e.NewValues["REMARK"] == null)
        //        {
        //            drRD.REMARK = "";
        //        }
        //        else
        //        {
        //            drRD.REMARK = StringUtil.CStr(e.NewValues["REMARK"]);
        //        }
        //        drRD.HQ_ADJ_ORDER_QTY = Convert.ToInt32(StringUtil.CStr(e.NewValues["HQ_ADJ_ORDER_QTY"]));

        //        dtRD.Rows.Add(drRD);
        //    }
        //}

        ORDERM_DTO.AcceptChanges();

        Facade.UpdateOne_ORDER(ORDERM_DTO);

        gvMasterDV.CancelEdit();
        e.Cancel = true;

        string txtOrdNoStart1 = txtOrdNoStart.Text;
        string txtOrdNoEnd1 = txtOrdNoEnd.Text;
        string storeno = txtbox1.Text;
        string ORDDate = txtOrdDateStart.Text;
        string ORDDate1 = txtOrdDateEnd.Text;
        string PRODNO = ASPxTextBox1.Text;
        string Status = StringUtil.CStr(ASPxComboBox1.SelectedItem.Value);
        GetData1();
        this.gvDetailDV.FocusedRowIndex = -1;
        this.gvMasterDV.FocusedRowIndex = -1;

    }

    protected void gvMasterDV_PageIndexChanged(object sender, EventArgs e)
    {
        GetData();
    }

    protected void gvMasterDV_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            string s = StringUtil.CStr(gvMasterDV.GetRowValues(e.VisibleIndex, "STATUS"));
            switch (s)
            {

                case "10":
                    e.Row.Cells[3].Text = "正式";

                    break;
             
                case "50":
                    e.Row.Cells[3].Text = "己傳輸";

                    break;
                case "51":
                    e.Row.Cells[3].Text = "已成單";

                    break;
            

            }
            //   10-正式/11-預訂/20-己傳輸/51-已成單/70-已刪除/80-未驗收/85-部份驗收/90-己結案。
            if (s == "未完成")
            {
                e.Row.Style.Add(HtmlTextWriterStyle.Color, "red");
            }
        }

        if (e.RowType == GridViewRowType.Data || e.RowType == GridViewRowType.InlineEdit)
        {
            string sORDDATE = StringUtil.CStr(gvMasterDV.GetRowValues(e.VisibleIndex, "ORDDATE"));
            e.Row.Cells[1].Text = sORDDATE.Substring(0, 4) + "/" + sORDDATE.Substring(4, 2) + "/" + sORDDATE.Substring(6, 2);
        }
    }

    protected void gvMasterDV_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            string s = StringUtil.CStr(gvMasterDV.GetRowValues(e.VisibleIndex, "STATUS"));
            switch (s)
            {
                case "10":
                    s = "正式";
                    break;
            
                case "50":
                    s = "己傳輸";
                    break;
               
            }
            //   10-正式/11-預訂/50-己傳輸/51-已成單/70-已刪除/80-未驗收/85-部份驗收/90-己結案。
            if (s == "未完成")
            {
                e.Row.Style.Add(HtmlTextWriterStyle.Color, "red");
            }
        }

    }

    protected void gvMasterDV_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        ORD04_Facade facade = new ORD04_Facade();

        string Key = "";
        if (e.Keys["ORDER_ITEMS_ID"] != null)
        {
            Key = StringUtil.CStr(e.Keys["ORDER_ITEMS_ID"]);
        }
        string strMoneyAmt = ((ASPxLabel)gvMasterDV.FindEditRowCellTemplateControl((GridViewDataColumn)gvMasterDV.Columns["ORDQTY"], "ORDQTY")).Text.Trim();
        string storeno = ((ASPxLabel)gvMasterDV.FindEditRowCellTemplateControl((GridViewDataColumn)gvMasterDV.Columns["STORE_NO"], "STORE_NO")).Text.Trim();
        string Quota = ((ASPxTextBox)gvMasterDV.FindEditRowCellTemplateControl((GridViewDataColumn)gvMasterDV.Columns["HQ_ADJ_ORDER_QTY"], "txtDiscountQuota")).Text.Trim();
        int total = Convert.ToInt32(Quota);

        string[] PRODNOs = facade.GetProdNo(Key).Split(';');
        string strProdNo = PRODNOs[0];
        string strProdNo_M = "";
        string ProdNo_M_ATR = "0";
        if (PRODNOs.Length == 2)
        {
            strProdNo_M = PRODNOs[1];
            ProdNo_M_ATR = facade.getPOSATR(strProdNo_M);
            if (string.IsNullOrEmpty(ProdNo_M_ATR))
            {
                ProdNo_M_ATR = "0";
            }
        }

        string POSATRQTY = facade.getPOSATR(strProdNo);
        if (string.IsNullOrEmpty(POSATRQTY))
        {
            POSATRQTY = "0";
        }

        int ATR = 0;
        string ProdNo = "";
        //判斷是否有搭贈商品，
        //若沒有，就直接以商品的ATR量作比較
        //若有，則先判斷 主商品的ATR量 和 搭贈商品的ATR量 何者較少，以較少者作比較
        if (string.IsNullOrEmpty(strProdNo_M))  
        {
            ATR = Convert.ToInt32(POSATRQTY);
            ProdNo = strProdNo; 
        }
        else
        {
            if (Convert.ToInt32(strProdNo_M) <= Convert.ToInt32(POSATRQTY))
            {
                ATR = Convert.ToInt32(strProdNo_M);
                ProdNo = strProdNo_M;
            }
            else
            {
                ATR = Convert.ToInt32(POSATRQTY);
                ProdNo = strProdNo;
            }
        }

        if (total > ATR)
        {
            e.RowError = "商品料號:" + ProdNo + " ATR量不足";

        }

    }

    protected void gvMasterDV_FocusedRowChanged(object sender, EventArgs e)
    {
        if (gvMasterDV.FocusedRowIndex >= 0)
        {
            string txtOrdNoStart1 = txtOrdNoStart.Text;
            string txtOrdNoEnd1 = txtOrdNoEnd.Text;
            string storeno = ""; //StringUtil.CStr(gvMasterDV.GetRowValues(gvMasterDV.FocusedRowIndex, "STORE_NO"));  
            string ORDDate = txtOrdDateStart.Text;
            string ORDDate1 = txtOrdDateEnd.Text;
            string PRODNO = ASPxTextBox1.Text;
            string Status = StringUtil.CStr(ASPxComboBox1.SelectedItem.Value);
            string zone = StringUtil.CStr(ddlArea.SelectedItem.Value);
            //string Key = StringUtil.CStr(gvMasterDV.GetRowValues(gvMasterDV.FocusedRowIndex, "ORDER_ITEMS_ID"));
            string KeyPRODNO = StringUtil.CStr(gvMasterDV.GetRowValues(gvMasterDV.FocusedRowIndex, "PRODNO"));

            ORD04_Facade facade04 = new ORD04_Facade();

            DataTable dt2 = new DataTable();
            dt2.Clear();
            dt2 = facade04.TopDatatable3(txtOrdNoStart1, txtOrdNoEnd1, storeno, ORDDate, ORDDate1, PRODNO, Status, zone, KeyPRODNO);
            gvDetailDV.DataSource = dt2;
            gvDetailDV.Visible = true;
            gvDetailDV.DataBind();
            gvDetailDV.FocusedRowIndex = -1;
        }
    }

    protected void gvMasterDV_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        

    }

    protected void gvMasterDV_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        this.gvDetailDV.FocusedRowIndex = -1;
        this.gvMasterDV.FocusedRowIndex = -1;
        GetData1();

    }

    #endregion

    protected void gvDetailDV_FocusedRowChanged(object sender, EventArgs e)
    {
        ASPxGridView gv = sender as ASPxGridView;
        if (gv.FocusedRowIndex >= 0)
        {
            string txtOrdNoStart1 = txtOrdNoStart.Text;
            string txtOrdNoEnd1 = txtOrdNoEnd.Text;
            string storeno = txtbox1.Text;
            string ORDDate = txtOrdDateStart.Text;
            string ORDDate1 = txtOrdDateEnd.Text;
            string PRODNO = ASPxTextBox1.Text;
            string Status = StringUtil.CStr(ASPxComboBox1.SelectedItem.Value);
            string zone = StringUtil.CStr(ddlArea.SelectedItem.Value);
            ORD04_Facade facade04 = new ORD04_Facade();

            string key = StringUtil.CStr(gv.GetRowValues(gvDetailDV.FocusedRowIndex, "PRODNO_M"));
            DataTable dtx = new DataTable();
            dtx = facade04.TopDatatable1(txtOrdNoStart1, txtOrdNoEnd1, storeno, ORDDate, ORDDate1, key, Status,zone);
            gvMasterDV.DataSource = dtx;
            gvMasterDV.DataBind();
            gvMasterDV.FocusedRowIndex = -1;
        }

    }

    protected void gvDetailDV_PageIndexChanged(object sender, EventArgs e)
    {
        string txtOrdNoStart1 = txtOrdNoStart.Text;
        string txtOrdNoEnd1 = txtOrdNoEnd.Text;
        string storeno = txtbox1.Text;
        string ORDDate = txtOrdDateStart.Text;
        string ORDDate1 = txtOrdDateEnd.Text;
        string PRODNO = ASPxTextBox1.Text;
        string Status = StringUtil.CStr(ASPxComboBox1.SelectedItem.Value);
        string zone = StringUtil.CStr(ddlArea.SelectedItem.Value);

        ORD04_Facade facade04 = new ORD04_Facade();

        DataTable dt = new DataTable();

        gvDetailDV.FocusedRowIndex = -1;

        gvDetailDV.DataSource = null;
        gvDetailDV.DataBind();

        DataTable dt2 = new DataTable();
        dt2.Clear();
        dt2 = facade04.TopDatatable2(txtOrdNoStart1, txtOrdNoEnd1, storeno, ORDDate, ORDDate1, PRODNO, Status, zone);

        gvDetailDV.DataSource = dt2;
        gvDetailDV.Visible = true;
        gvDetailDV.DataBind();
    }
}
