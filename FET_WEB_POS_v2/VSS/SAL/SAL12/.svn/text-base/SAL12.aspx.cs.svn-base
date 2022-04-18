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
using FET.POS.Model.DTO;
using Advtek.Utility;

public partial class VSS_SAL_SAL_SAL12 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //商品分類
            cboProductCategory.DataSource = DIS10_PageHelper.GetProductCategory();
            cboProductCategory.TextField = "CATE_NAME";
            cboProductCategory.ValueField = "CATE_NO";
            cboProductCategory.DataBind();
            cboProductCategory.SelectedIndex = 0;
        }
    }

    private void BindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        gvMaster.DataSource = dtResult;

        GridViewDataComboBoxColumn column = (GridViewDataComboBoxColumn)gvMaster.Columns["IMEI_FLAG"];
        column.PropertiesComboBox.DataSource = INV23_PageHelper.GetCheckImeiTypeMethodData();
        column.PropertiesComboBox.TextField = "CHECK_IMEI_TYPE_NAME";
        column.PropertiesComboBox.ValueField = "CHECK_IMEI_TYPE";

        gvMaster.DataBind();
    }

    private void BindDetailData2()
    {
        //費率及申辦類型
        //DataTable dt = new SAL13_Facade().Query_RATE_PLAN_DISCOUNT(sDISCOUNT_MASTER_ID);
        //if (dt.Rows.Count > 0)
        //{
        string v_Data = "";
        string v_Voice = "";
        string v_RatePlan = "";
        string v_ProdNo = lblProdNo.Text;
        string v_PromoteCode = "";
        string v_StoreNo = txtStoreNo.Text.Trim();
        int v_RRPB = Convert.ToInt32(txtARPB.Text.Trim());
        string v_Msisdn = txtMSISDN.Text.Trim();
        foreach (Control _ctr in DISItemChargesAndApply1.Controls)
        {
            string sTmp = StringUtil.CStr(_ctr.GetType());
            if (sTmp == "System.Web.UI.WebControls.CheckBoxList")
            {
                CheckBoxList _cbo = (CheckBoxList)_ctr;
                if (_cbo.Items.Count > 0)
                {
                    for (int i = 0; i < _cbo.Items.Count; i++)
                    {
                        if (!(string.IsNullOrEmpty(_cbo.Items[i].Value)))
                        {
                            if (_cbo.ID == "cbRate")
                            {
                                switch (_cbo.Items[i].Value) 
                                {
                                    case "VOICE":
                                        v_Voice = "VOICE";
                                        break;
                                    case "DATA":
                                        v_Data = "DATA";
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else 
                            {
                                if (_cbo.Items[i].Selected == true) 
                                {
                                    v_RatePlan += _cbo.Items[i].Value + ",";
                                }                               
                            }
                        }
                    }
                }
            }
        }
        if (v_RatePlan != "") v_RatePlan=v_RatePlan.Substring(0, v_RatePlan.Length - 1);
        DataTable dTmp = new SAL12_Facade().getDiscount(v_Data, v_Voice, v_RatePlan, v_ProdNo, v_PromoteCode, v_StoreNo, v_RRPB, v_Msisdn);
        gvDetail.DataSource = dTmp;
        gvDetail.DataBind();
        //DataTable dtResult = new DataTable();
        //dtResult = getMasterData();
        //gvMaster.DataSource = dtResult;
        //gvMaster.DataBind();
    }

    private DataTable getMasterData()
    {
        return new SAL12_Facade().Query_PRODUCT(StringUtil.CStr(chkISCONSIGNMENT_ISEXPIRED.Items[0].Selected)
                                               , StringUtil.CStr(chkISCONSIGNMENT_ISEXPIRED.Items[1].Selected)
                                               , txtProductTypeNo.Text
                                               , StringUtil.CStr(cboProductCategory.SelectedItem.Value)
                                               , txtPRODNO.Text.Trim()
                                               , txtPRODNAME.Text.Trim()
                                               , txtSUPPNAME.Text.Trim()
                                               , txtSDate_S.Text.Trim()
                                               , txtSDate_E.Text.Trim()
                                               , txtEDate_S.Text.Trim()
                                               , txtEDate_E.Text.Trim()
                                               , txtPRICE1.Text.Trim()
                                               , txtPRICE2.Text.Trim());

    }

    private void clearDetail() 
    {
        gvDetail.DataSource = null;
        gvDetail.DataBind();
        txtStoreNo.Text = string.Empty;
        txtMSISDN.Text = string.Empty;
        txtARPB.Text = "0";
        DISItemChargesAndApply1.Clear();
    }

    #region Button 觸發事件

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {

            if (!(String.IsNullOrEmpty(txtSDate_S.Text)) && !(String.IsNullOrEmpty(txtSDate_E.Text)) && Convert.ToDateTime(txtSDate_S.Text) > Convert.ToDateTime(txtSDate_E.Text))
            {
                txtSDate_E.Focus();
                throw new Exception("生效起日查詢訖日不允許小於起日，請重新輸入");
            }
            if (!(String.IsNullOrEmpty(txtEDate_S.Text)) && !(String.IsNullOrEmpty(txtEDate_E.Text)) && Convert.ToDateTime(txtEDate_S.Text) > Convert.ToDateTime(txtEDate_E.Text))
            {
                txtEDate_E.Focus();
                throw new Exception("生效訖日查詢訖日不允許小於起日，請重新輸入");
            }

            BindMasterData();

        }
        catch (Exception ex)
        {
            gvMaster.DataSource = null;
            gvMaster.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Common", @"alert('" + ex.Message + "');", true);
        }
        finally
        {
            gvMaster.FocusedRowIndex = -1;
            gvMaster.PageIndex = 0;
            ASPxPageControl1.ActiveTabIndex = 0;
            clearDetail();
        }
    }

    protected void btnSearchD_Click(object sender, EventArgs e)
    {
        try
        {
            int iTmp;
            if (int.TryParse(this.txtARPB.Text.Trim(), out iTmp) == false)
            {
                throw new Exception("ARPB金額輸入字串非數字格式，請重新輸入");
            }
            if (!string.IsNullOrEmpty(txtStoreNo.Text.Trim()) && (new Store_Facade().GetStoreName(txtStoreNo.Text.Trim()) == ""))
            {
                throw new Exception("門市編號不存在，請重新輸入");
            }

            BindDetailData2();
            gvDetail.FocusedRowIndex = -1;
            gvDetail.PageIndex = 0;
        }
        catch (Exception ex)
        {
            gvDetail.DataSource = null;
            gvDetail.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Common", @"alert('" + ex.Message + "');", true);
        }

    }

    protected void btnReset2_Click(object sender, EventArgs e)
    {
        clearDetail();
    }

    #endregion

    #region gvMaster 觸發事件

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }

    protected void gvMaster_FocusedRowChanged(object sender,  EventArgs e) 
    {
        int fIndex = gvMaster.FocusedRowIndex;
        if ( fIndex > -1)
        {
            object[] arrObj = (object[])gvMaster.GetRowValues(fIndex, new string[] { "PRODNO", "PRODNAME" });
            this.lblProdNo.Text = StringUtil.CStr(arrObj[0]);
            this.lblProdName.Text = StringUtil.CStr(arrObj[1]);
            //this.ASPxPageControl1.ActiveTabIndex = 1;
            //this.ASPxPageControl1.ResetViewStateStoringFlag();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "change", "ASPxPageControl1.SetActiveTab(ASPxPageControl1.GetTab(1)); ", true);
            
            //this.ASPxPageControl1.TabIndex = 1;
        }
    }

    #endregion

    #region gvDetail 觸發事件

    protected void gvDetail_PageIndexChanged(object sender, EventArgs e) 
    {
        BindDetailData2();
    }

    #endregion

    protected void ASPxPageControl1_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
    {
        int iIndex=this.ASPxPageControl1.ActiveTabIndex;
        int fIndex=gvMaster.FocusedRowIndex;
       
        if (iIndex == 1 && fIndex == -1)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('請先選擇產品料號');", true);
            this.ASPxPageControl1.ActiveTabIndex = 0;
        }
        else 
        {
            switch (iIndex)
            {
                case 0:
                    //BindMasterData();
                    break;

                case 1:
                    object[] arrObj=(object[])gvMaster.GetRowValues(fIndex, new string[] { "PRODNO", "PRODNAME" });
                    this.lblProdNo.Text = StringUtil.CStr(arrObj[0]);
                    this.lblProdName.Text = StringUtil.CStr(arrObj[1]);
                    break;

                default:
                    break;
            }
        }
        
    }
}
