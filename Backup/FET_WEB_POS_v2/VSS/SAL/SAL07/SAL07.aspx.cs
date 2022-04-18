using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Resources;

using Advtek.Utility;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;

using FET.POS.Model.DTO;
using FET.POS.Model.Common;
using FET.POS.Model.Facade.FacadeImpl;

public partial class VSS_SAL_SAL07_SAL07 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStoreNo2.Text = logMsg.STORENO;
            
        }

    }

    private DataTable GetMasterData(bool IsExport)
    {
        //處理專案類型
        string SDate = "";
        string EDate = "";
        string ToDay = DateTime.Today.ToString("yyyy/MM/dd");

        if (StringUtil.CStr(this.cbProjectType.Value) == "1")
        {
            SDate = ToDay;
            EDate = ToDay;
        }
        else if (StringUtil.CStr(this.cbProjectType.Value) == "2")
        {
            SDate = ToDay;
        }

        
        //處理促銷名稱
        string PromoName = "";

        if (txtPromoName1.Text != "")
        {
            //前置
            PromoName += txtPromoName1.Text;
        }
        if (txtPromoName2.Text != "")
        {
            PromoName += "%" + txtPromoName2.Text;
        }
        if (txtPromoName3.Text != "")
        {
            PromoName += "%" + txtPromoName3.Text;
        }
        if (txtPromoName4.Text != "")
        {
            PromoName += "%" + txtPromoName4.Text;
        }
        if (txtPromoName5.Text != "")
        {
            PromoName += "%" + txtPromoName5.Text;
        }
        if (txtPromoName6.Text != "")
        {
            PromoName += "%" + txtPromoName6.Text;
        }

        if (PromoName != "") PromoName += "%";
        
        //**2011/03/25 Tina：促銷代碼 查詢字串 自動轉成大寫
        return new SAL07_Facade().Query_MM(
            txtPromoNO.Text,
            PromoName, SDate, EDate, PromotPrice_start.Text, PromotPrice_end.Text,
            txtProdNO.Text, txtProductName1.Text ,txtProductName2.Text ,txtProductName3.Text, IsExport,
            CheckBox3.Checked, this.logMsg.ROLE_TYPE, this.logMsg.STORENO,ASPxCheckBox1.Checked);
    }

    private void BindMasterData()
    {
        DataTable dt = GetMasterData(false);
        if (dt.Rows.Count > 0)
        {
            ViewState["getData"] = true;
        }
        else
        {
            ViewState["getData"] = false;
        }
       
        gvMaster.DataSource = dt;
        gvMaster.DataBind();

        //ASPxButton1.Enabled = (dt.Rows.Count > 0);

        gvDetail.Visible = false;

        gvMaster.FocusedRowIndex = -1;
       
    }

    private void BindDetailData()
    {
        gvDetail.DataSource = new SAL07_Facade().Query_Detail(
           StringUtil.CStr( gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "UUID")),
           this.CheckBox3.Checked, 
           this.logMsg.ROLE_TYPE, 
           this.logMsg.STORENO,
           this.txtProdNO.Text,
           this.txtProductName1.Text,
           this.txtProductName2.Text
           );
        //最後三個傳入的參數
        //商品料號
        //商品廠牌
        //商品型號

        gvDetail.DataBind();
        gvDetail.Visible = true;
        //ASPxPageControl1.TabPages[1].Enabled = false;

        lblPromoCode.Text = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "PROMO_NO"));
        lblPromoName.Text = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "PROMO_NAME"));
    }

    private void BindDetailData2()
    {
        if (lblPromoCode.Text == "") return;

        #region 組合勾選條件
        string m_type = "";
        CheckBoxList cbRate = this.DISItemChargesAndApply1.FindControl("cbRate") as CheckBoxList;
        CheckBoxList cbGAG1 = this.DISItemChargesAndApply1.FindControl("cbGAG1") as CheckBoxList;
        CheckBoxList cbGAG2 = this.DISItemChargesAndApply1.FindControl("cbGAG2") as CheckBoxList;
        CheckBoxList cbLoyalty = this.DISItemChargesAndApply1.FindControl("cbLoyalty") as CheckBoxList;
        CheckBoxList cb223G1 = this.DISItemChargesAndApply1.FindControl("cb223G1") as CheckBoxList;
        CheckBoxList cb223G2 = this.DISItemChargesAndApply1.FindControl("cb223G2") as CheckBoxList;
        CheckBoxList cbMNP = this.DISItemChargesAndApply1.FindControl("cbMNP") as CheckBoxList;

        CheckBoxList[] chkList = new CheckBoxList[] { cbGAG1, cbGAG2, cbLoyalty, cbLoyalty, cb223G1, cb223G2 };

        string MNP = "";
        foreach (ListItem it in cbMNP.Items)
        {
            if (it.Selected)
                MNP = "MNP";
        }

        string DATA = "N";
        string VOICE = "N";
        if (cbRate.Items[1].Selected == true)//"DATA")
            DATA = "Y";
        if (cbRate.Items[0].Selected == true)// "VOICE")
            VOICE = "Y";

        foreach (CheckBoxList list in chkList)
        {
            foreach (ListItem item in list.Items)
            {
                if (item.Selected)
                {
                    if (m_type != "")
                        m_type += "," + item.Value;
                    else
                        m_type = item.Value;
                }
            }
        }

        #endregion

        DataTable dt = getProdData(); //輸出的資料

        DataTable dtDiscount = new SAL01_Facade().getMixPromotion_ItemDiscount(
            txtStoreNo2.Text, "", lblPromoCode.Text, txtPhoneNo.Text, "",
            DATA, VOICE, m_type, MNP, "", "", txtProdNo2.Text);


        SAL07_Facade fs = new SAL07_Facade();

        if (dtDiscount != null && dtDiscount.Rows.Count > 0)
        {
            foreach (DataRow drDis in dtDiscount.Rows)
            {
                if (fs.QueryPromotionDiscount( StringUtil.CStr(drDis[0]) ) )//查詢是否有在促銷之中
                {
                    DataRow dr = dt.NewRow();
                    dr["DISCOUNT_CODE"] = drDis["DISCOUNT_CODE"];
                    dr["DISCOUNT_NAME"] = drDis["DISCOUNT_NAME"];
                    dr["DISCOUNT_MONEY"] = drDis["DISCOUNT_MONEY"];
                    dr["GIFT"] = fs.QueryDiscountGIFT(StringUtil.CStr(drDis["DISCOUNT_MASTER_ID"]));

                    dt.Rows.InsertAt(dr, 0);
                    dt.AcceptChanges();
                }
            }
        }

        //dt = new SAL07_Facade().Query_Discount(
        //    lblPromoCode.Text
        //    , m_type, txtARPB.Text, txtStoreNo2.Text, txtPhoneNo.Text, txtProdNo2.Text, "");
        ASPxGridView1.DataSource = dt;

        ASPxGridView1.DataBind();
        
    }

    void SetDetailGridClear()
    {
        DataTable dt = getProdData(); //輸出的資料
        DataTable dtDiscount = new SAL01_Facade().getMixPromotion_ItemDiscount(
                    "", "", "123dddd", "", "",
                    "", "", "", "", "", "", "");
        if (dtDiscount != null && dtDiscount.Rows.Count > 0)
        {
            foreach (DataRow drDis in dtDiscount.Rows)
            {
                DataRow dr = dt.NewRow();
                dr["DISCOUNT_CODE"] = drDis["DISCOUNT_CODE"];
                dr["DISCOUNT_NAME"] = drDis["DISCOUNT_NAME"];
                dr["DISCOUNT_MONEY"] = drDis["DISCOUNT_MONEY"];
                dr["GIFT"] = "";

                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();
            }
        }

        //dt = new SAL07_Facade().Query_Discount(
        //    lblPromoCode.Text
        //    , m_type, txtARPB.Text, txtStoreNo2.Text, txtPhoneNo.Text, txtProdNo2.Text, "");

        ASPxGridView1.DataSource = dt;

        ASPxGridView1.DataBind();
    }

    #region Button 觸發事件

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvMaster.PageIndex = 0;
        ASPxPageControl1.ActiveTabIndex = 0;
        BindMasterData();
        DISItemChargesAndApply1.Clear();
        //ASPxGridView1.DataSource = null;
        SetDetailGridClear();
    }

    protected void btnSearchD_Click(object sender, EventArgs e)
    {
        BindDetailData2();
    }

    protected void ASPxButton5_Click(object sender, EventArgs e)
    {
        txtProdNo2.Text = string.Empty;
        txtARPB.Text = string.Empty;
        txtStoreNo2.Text = string.Empty;
        txtPhoneNo.Text = string.Empty;
        this.DISItemChargesAndApply1.Clear();

        SetDetailGridClear();
    }

    private DataTable getProdData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("DISCOUNT_CODE", typeof(string));
        dtResult.Columns.Add("DISCOUNT_NAME", typeof(string));
        dtResult.Columns.Add("DISCOUNT_MONEY", typeof(string));
        dtResult.Columns.Add("GIFT", typeof(string));

        return dtResult;
    }

    /// <summary>
    /// 匯出
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        //判斷是否已查詢
        ExportExcelData eel = new ExportExcelData();
        this.Page.Controls.Add(eel);
        DataTable dtTmp = new DataTable();
        dtTmp.TableName = "Table";
        if (ViewState["getData"] != null && (bool)ViewState["getData"] == true)
        {
            dtTmp = GetMasterData(true);
        }
        
        string[] sSTNO = { "", "", "", "" };
        for (int i = 0; i < dtTmp.Rows.Count; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (sSTNO[j] == StringUtil.CStr(dtTmp.Rows[i][j]))
                {
                    dtTmp.Rows[i][j] = DBNull.Value;
                }
                else
                {
                    sSTNO[j] = StringUtil.CStr(dtTmp.Rows[i][j]);
                    if (j == 1 && i != 0)
                    {
                        break;
                    }

                }
            }
        }
        eel.ExportExcel(dtTmp);
    }

    #endregion

    #region gvMaster 觸發事件

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }

    protected void gvMaster_FocusedRowChanged(object sender, EventArgs e)
    {
        if (gvMaster.FocusedRowIndex < 0) { return; }

        BindDetailData();
    }

    #endregion

    #region gvDetail 觸發事件

    protected void gvDetail_PageIndexChanged(object sender, EventArgs e)
    {
        BindDetailData();
    }

    protected void gvDetail_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            if (StringUtil.CStr(e.GetValue("ONHANDQTY")).Trim() == string.Empty)
            {
                e.Row.Cells[e.Row.Cells.Count - 1].Text = "0";
            }
        }
    }

    #endregion

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public int getProdInfo(string PRODNO)
    {
        int ProdNoCount = 0;
        if (!string.IsNullOrEmpty(PRODNO))
        {
            ProdNoCount = new Product_Facade().Query_ProductInfo(PRODNO).Rows.Count;
        }

        return ProdNoCount;
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getStoreInfo(string STORENO)
    {
        string ProdNoCount = "";
        if (!string.IsNullOrEmpty(STORENO))
        {
            ProdNoCount = new Store_Facade().GetStoreName(STORENO);
        }

        return ProdNoCount;
    }
}
