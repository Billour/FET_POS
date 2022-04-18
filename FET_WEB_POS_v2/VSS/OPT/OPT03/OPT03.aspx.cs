using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using DevExpress.Web.Data;


public partial class VSS_OPT_OPT03 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !Page.IsCallback)
        {
            if (logMsg.ROLE_TYPE != StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"]))
            {
                //彈跳視窗
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);
                gvMaster.Enabled = false;
                gvDetail.Enabled = false;
                ddlCardBank.Enabled = false;
                ddlCostCenter.Enabled = false;
                ddlStatus.Enabled = false;
                txtPaySeqment.Enabled = false;
                txtSDate_S.Enabled = false;
                txtSDate_E.Enabled = false;
                txtEDate_S.Enabled = false;
                txtEDate_E.Enabled = false;
                btnSearch.Enabled = false;
                btnReset.Enabled = false;
                txtSDate_S.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                txtSDate_E.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                txtEDate_S.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                txtEDate_E.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                return;
            }

            //取得空的資料表
            DataTable dt = new OPT03_Facade().Query_CreditCardInstallment("-1","-1","","","","","","");
            gvMaster.DataSource = dt;
                //new OPT03_CreditCardInstallment_DTO().CREDIT_CART_INSTELLMENT;
            gvMaster.DataBind();
            Session["INSTELLMENT_ID"] = null;
            BindddlCardBank(this.ddlCardBank, true);
            BindddlCostCenter(this.ddlCostCenter, true);
        }

    }

    private void BindddlCardBank(ASPxComboBox ddl, bool IsQry)
    {
        ddl.DataSource = OPT03_PageHelper.GetBankNameId(IsQry);
        ddl.TextField = "BANK_NAME";
        ddl.ValueField = "BANK_ID";
        ddl.DataBind();
        ddl.SelectedIndex = 0;
    }

    private void BindddlCostCenter(ASPxComboBox ddl, bool IsQry)
    {
        if (ddl != null)
        {
            ddl.DataSource = OPT03_PageHelper.GetCostCenterNameNo(IsQry);
            ddl.TextField = "COST_CENTER_NO";
            ddl.ValueField = "COST_CENTER_NO";
            ddl.DataBind();
            ddl.SelectedIndex = 0;
        }
    }

    private void BindMasterData()
    {
        DataTable dt = getMasterData();
        gvMaster.DataSource = dt;
        gvMaster.DataBind();
        gvMaster.FocusedRowIndex = -1;
        gvMaster.Selection.UnselectAll();
        if (gvMaster.IsEditing)
        {
            gvMaster.CancelEdit();
        }
        if (dt.Rows.Count == 0)
        {
            gvDetail.Visible = false;
        }
    }

    private void BindDetailData(bool IsEdit)
    {
        DataTable dt = new DataTable();

        if (gvMaster.FocusedRowIndex != -1)
        {

            string INSTELLMENT_ID = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName));
            string status = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "STATUS"));
            if (status == "有效" || status == "已過期")
            {
                IsEdit = false;
            }

            if (ViewState["dtCreditDetail"] != null)
            {
                dt = ViewState["dtCreditDetail"] as DataTable;

                DataRow[] dr = dt.Select("INSTELLMENT_ID='" + INSTELLMENT_ID + "'");
                if (dr.Length <= 0)
                {
                    dt = new OPT03_Facade().Query_CreditCardSettlement(INSTELLMENT_ID);
                }
                else
                {
                    dt = ViewState["dtCreditDetail"] as DataTable;
                }
            }
            else
            {
                dt = new OPT03_Facade().Query_CreditCardSettlement(INSTELLMENT_ID);
            }

        }
        else  //新增主檔資料的同時，也要新增明細資料
        {
            dt = new OPT03_Facade().Query_CreditCardSettlement("");
        }


        ViewState["dtCreditDetail"] = dt;
        gvDetail.DataSource = dt;
        gvDetail.DataBind();
        gvDetail.FocusedRowIndex = -1;
        gvDetail.Selection.UnselectAll();

        gvDetail.Settings.ShowTitlePanel = false;
        gvDetail.Columns[1].Visible = false;
        gvDetail.Columns[0].Visible = false;
        if (IsEdit)
        {
            gvDetail.Settings.ShowTitlePanel = true;
            gvDetail.Columns[0].Visible = true;
            gvDetail.Columns[1].Visible = true;
        }

    }

    private DataTable getMasterData()
    {
        DataTable dtMaster = new DataTable();
        dtMaster = new OPT03_Facade().Query_CreditCardInstallment(StringUtil.CStr(ddlCardBank.Value),
                                                             StringUtil.CStr(ddlCostCenter.Value),
                                                             txtPaySeqment.Text,
                                                             StringUtil.CStr(ddlStatus.Value),
                                                             txtSDate_S.Text,
                                                             txtSDate_E.Text,
                                                             txtEDate_S.Text,
                                                             txtEDate_E.Text);
        return dtMaster;
    }

    #region Button 觸發事件

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
        gvDetail.Visible = false;
    }

    protected void btnAddM_Click(object sender, EventArgs e)
    {
        if (!gvMaster.IsEditing)
        {
            gvMaster.AddNewRow();
            gvMaster.Selection.UnselectAll();
        }
    }

    protected void btnAddD_Click(object sender, EventArgs e)
    {
        if (!gvDetail.IsEditing)
        {
            SaveEditMasterDataq(); //明細檔在儲存資料時，先把主檔資料存起來

            gvDetail.AddNewRow();
            gvDetail.Selection.UnselectAll();
        }
    }

    protected void btnDeleteM_Click(object sender, EventArgs e)
    {
        if (!gvMaster.IsEditing)
        {
            OPT03_CreditCardInstallment_DTO OPT03_DTO = new OPT03_CreditCardInstallment_DTO();
            OPT03_CreditCardInstallment_DTO.CREDIT_CART_INSTELLMENTDataTable dtMaster = OPT03_DTO.CREDIT_CART_INSTELLMENT; //分期期數設定檔

            OPT03_Facade facade = new OPT03_Facade();

            List<object> keyValues = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);

            foreach (string key in keyValues)
            {
                OPT03_CreditCardInstallment_DTO.CREDIT_CART_INSTELLMENTRow drMaster = dtMaster.NewCREDIT_CART_INSTELLMENTRow();
                DataRow dirtyDr = OPT03_PageHelper.GetCreditCardInstallmentByInstellmentId(key);
                drMaster["BANK_ID"] = dirtyDr["BANK_ID"];
                drMaster["CREATE_DTM"] = dirtyDr["CREATE_DTM"];
                drMaster["CREATE_USER"] = dirtyDr["CREATE_USER"];
                drMaster["CREDIT_CARD_TYPE_ID"] = dirtyDr["CREDIT_CARD_TYPE_ID"];
                drMaster["E_DATE"] = dirtyDr["E_DATE"];
                drMaster["INSTELLMENT_ID"] = dirtyDr["INSTELLMENT_ID"];
                drMaster["MODI_DTM"] = dirtyDr["MODI_DTM"];
                drMaster["MODI_USER"] = dirtyDr["MODI_USER"];
                drMaster["PAY_SEQMENT"] = dirtyDr["PAY_SEQMENT"];
                drMaster["S_DATE"] = dirtyDr["S_DATE"];
                drMaster["SEQMENT_RATE"] = dirtyDr["SEQMENT_RATE"];
                dtMaster.Rows.Add(drMaster);
            }

            dtMaster.AcceptChanges();

            facade.Delete_CreditCartInstellment(dtMaster);

            BindMasterData();
            gvMaster.FocusedRowIndex = -1;
            ViewState["dtCreditDetail"] = null;
            gvDetail.Visible = false;
        }
    }

    protected void btnDeleteD_Click(object sender, EventArgs e)
    {
        if (ViewState["dtCreditDetail"] != null && !gvDetail.IsEditing)
        {
            DataTable dt = ViewState["dtCreditDetail"] as DataTable;
            string where = "";
            List<object> keyValues = this.gvDetail.GetSelectedFieldValues(gvDetail.KeyFieldName);
            foreach (string skey in keyValues)
            {
                where += "'" + skey + "',";
            }
            if (where.Length > 0)
                where = where.Substring(0, where.Length - 1);
            else where = "''";

            DataRow[] dra = dt.Select("SETTLEMENT_ID in(" + where + ")");

            foreach (DataRow dr in dra)
            {
                dt.Rows.Remove(dr);
                dt.AcceptChanges();
            }
            ViewState["dtCreditDetail"] = dt;
            //if (Session["INSTELLMENT_ID"] == null)
            //{
            //    BindDetailData(true);
            //}
            //else
            //{
                gvDetail.DataSource = dt;
                gvDetail.DataBind();
            //}
            SaveEditMasterDataq(); //明細檔在刪除資料後，把主檔資料存起來
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.ddlCardBank.SelectedIndex = 0;
        this.ddlCostCenter.SelectedIndex = 0;
        this.ddlStatus.SelectedIndex = 0;
        this.txtSDate_S.Text = "";
        this.txtSDate_E.Text = "";
        this.txtEDate_S.Text = "";
        this.txtEDate_E.Text = "";
        this.txtPaySeqment.Text = "";
    }

    #endregion

    #region gvMaster 觸發事件

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        e.Row.Attributes["canSelect"] = "false";
        if (e.RowType == GridViewRowType.Data)
        {
            string status = StringUtil.CStr(e.GetValue("STATUS"));

            if (status == "尚未生效")
            {
                e.Row.Attributes["canSelect"] = "true";
            }

        }
        else
        {
            if (e.RowType == GridViewRowType.InlineEdit)
            {
                ASPxComboBox ddlBankID = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["BANK_ID"], "ddlBankID") as ASPxComboBox;
                BindddlCardBank(ddlBankID, false); //繫結 發卡銀行
                ASPxTextBox txtPaySeqment = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["PAY_SEQMENT"], "txtPAY_SEQMENT") as ASPxTextBox;
                ASPxTextBox txtSeqmentRate = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["SEQMENT_RATE"], "txtSEQMENT_RATE") as ASPxTextBox;
                ASPxDateEdit txtSDate = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["S_DATE"], "txtSDate") as ASPxDateEdit;
                ASPxDateEdit txtEDate = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["E_DATE"], "txtEDate") as ASPxDateEdit;
                ASPxTextBox txtSTATUS = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["STATUS"], "txtSTATUS") as ASPxTextBox;

                if (ViewState["dtCreditMaster"] != null)  //明細檔儲存資料時，要把主檔資料擺回去
                {
                    DataTable dt = ViewState["dtCreditMaster"] as DataTable;
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        ddlBankID.Text = StringUtil.CStr(dr["BANK_ID"]);
                        txtSDate.Text = StringUtil.CStr(dr["S_DATE"]);
                        txtEDate.Text = StringUtil.CStr(dr["E_DATE"]);
                        txtPaySeqment.Text = StringUtil.CStr(dr["PAY_SEQMENT"]) == "0" ? "" : StringUtil.CStr(dr["PAY_SEQMENT"]);
                        txtSeqmentRate.Text = StringUtil.CStr(dr["SEQMENT_RATE"]) == "0" ? "" : StringUtil.CStr(dr["SEQMENT_RATE"]);
                    }
                }

                if (ViewState["TempData"] != null)
                {
                    DataTable dt = ViewState["TempData"] as DataTable;
                    DataRow dr = dt.Rows[0];

                    ddlBankID.Value = StringUtil.CStr(dr["DropDownValue"]);
                    ViewState["TempData"] = null;
                }
                else
                {
                    if (!gvMaster.IsNewRowEditing)
                    {
                        ddlBankID.Value = StringUtil.CStr(e.GetValue("BANK_ID"));
                    }
                }

                if (!gvMaster.IsNewRowEditing)
                {
                    txtPaySeqment.Text = StringUtil.CStr(e.GetValue("PAY_SEQMENT"));
                    txtSeqmentRate.Text = StringUtil.CStr(e.GetValue("SEQMENT_RATE"));
                    txtSDate.Text = StringUtil.CStr(e.GetValue("S_DATE"));
                    txtEDate.Text = StringUtil.CStr(e.GetValue("E_DATE"));
                    txtSTATUS.Text = StringUtil.CStr(e.GetValue("STATUS"));

                    string status = StringUtil.CStr(e.GetValue("STATUS"));

                    switch (status)
                    {
                        case "有效":
                            ddlBankID.Enabled = false;
                            txtPaySeqment.Enabled = false;
                            txtSeqmentRate.Enabled = false;
                            txtSDate.Enabled = false;
                            txtEDate.Enabled = true;
                            break;
                        case "已過期":
                            ddlBankID.Enabled = false;
                            txtPaySeqment.Enabled = false;
                            txtSeqmentRate.Enabled = false;
                            txtSDate.Enabled = false;
                            txtEDate.Enabled = false;
                            break;
                        //case "尚未生效":
                        //    //txtSDate.Value = DateTime.Today.AddDays(1).ToString("yyyy/MM/dd");
                        //    txtPaySeqment.ReadOnly = false;
                        //    txtSeqmentRate.ReadOnly = false;
                        //    txtSDate.ReadOnly = false;
                        //    txtEDate.ReadOnly = false;
                        //    break;
                    }
                }//end-if (!gvMaster.IsNewRowEditing)
            }//end-if (e.RowType == GridViewRowType.InlineEdit)
        }//end-if (e.RowType == GridViewRowType.Data)
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
        this.gvDetail.Visible = false;
        ViewState["dtCreditDetail"] = null;
    }

    protected void gvMaster_RowInserting(object sender, ASPxDataInsertingEventArgs e)
    {
        if (ViewState["dtCreditDetail"] != null)
        {
            OPT03_Facade Facade = new OPT03_Facade();
            OPT03_CreditCardInstallment_DTO OPT03_DTO = new OPT03_CreditCardInstallment_DTO();

            OPT03_CreditCardInstallment_DTO.CREDIT_CART_INSTELLMENTDataTable dtCCI = OPT03_DTO.CREDIT_CART_INSTELLMENT;
            OPT03_CreditCardInstallment_DTO.CREDIT_CARD_SETTLEMMENTDataTable dtCCS = OPT03_DTO.CREDIT_CARD_SETTLEMMENT;

            OPT03_CreditCardInstallment_DTO.CREDIT_CART_INSTELLMENTRow drCCI = dtCCI.NewCREDIT_CART_INSTELLMENTRow();

            ASPxComboBox ddlBankID = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["BANK_ID"], "ddlBankID") as ASPxComboBox;
            ASPxTextBox txtPaySeqment = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["PAY_SEQMENT"], "txtPAY_SEQMENT") as ASPxTextBox;
            ASPxTextBox txtSeqmentRate = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["SEQMENT_RATE"], "txtSEQMENT_RATE") as ASPxTextBox;
            ASPxDateEdit txtSDate = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["S_DATE"], "txtSDate") as ASPxDateEdit;
            ASPxDateEdit txtEDate = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["E_DATE"], "txtEDate") as ASPxDateEdit;

            drCCI.INSTELLMENT_ID = StringUtil.CStr(Session["INSTELLMENT_ID"]); //GuidNo.getUUID();
            drCCI.BANK_ID = StringUtil.CStr(ddlBankID.Value);
            drCCI.PAY_SEQMENT = int.Parse(txtPaySeqment.Text);
            drCCI.SEQMENT_RATE = Convert.ToDecimal(txtSeqmentRate.Text);
            drCCI.S_DATE = Convert.ToDateTime(txtSDate.Text);

            if (!string.IsNullOrEmpty(txtEDate.Text))
                drCCI.E_DATE = Convert.ToDateTime(txtEDate.Text);

            drCCI.MODI_DTM = System.DateTime.Now;
            drCCI.MODI_USER = logMsg.OPERATOR;
            drCCI.CREATE_DTM = drCCI.MODI_DTM;
            drCCI.CREATE_USER = drCCI.MODI_USER;
            dtCCI.AddCREDIT_CART_INSTELLMENTRow(drCCI);

            DataTable dt = ViewState["dtCreditDetail"] as DataTable;
            foreach (DataRow dr in dt.Rows)
            {
                OPT03_CreditCardInstallment_DTO.CREDIT_CARD_SETTLEMMENTRow drCCS = dtCCS.NewCREDIT_CARD_SETTLEMMENTRow();
                drCCS.SETTLEMENT_ID = StringUtil.CStr(dr["SETTLEMENT_ID"]);
                drCCS.INSTELLMENT_ID = StringUtil.CStr(dr["INSTELLMENT_ID"]);
                drCCS.LINE_NO = Convert.ToDecimal(StringUtil.CStr(dr["LINE_NO"]));
                drCCS.SETTLEMENT_RATE = Convert.ToDecimal(StringUtil.CStr(dr["SETTLEMENT_RATE"]));
                drCCS.CREATE_USER = StringUtil.CStr(dr["CREATE_USER"]);
                drCCS.CREATE_DTM = Convert.ToDateTime(StringUtil.CStr(dr["CREATE_DTM"]));
                drCCS.MODI_USER = StringUtil.CStr(dr["MODI_USER"]);
                drCCS.MODI_DTM = Convert.ToDateTime(StringUtil.CStr(dr["MODI_DTM"]));
                drCCS.COST_CENTER_NO = StringUtil.CStr(dr["COST_CENTER_NO"]);
                dtCCS.AddCREDIT_CARD_SETTLEMMENTRow(drCCS);
            }

            OPT03_DTO.AcceptChanges();

            //更新資料庫
            Facade.AddNewOne_CreditCardInstallment(OPT03_DTO);

            gvMaster.CancelEdit();
            e.Cancel = true;

            ViewState["dtCreditMaster"] = null;
            Session["INSTELLMENT_ID"] = null;
            BindMasterData();
        }
    }

    protected void gvMaster_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
    {
        if (ViewState["dtCreditDetail"] != null)
        {
            OPT03_Facade Facade = new OPT03_Facade();

            OPT03_CreditCardInstallment_DTO OPT03_DTO = new OPT03_CreditCardInstallment_DTO();

            OPT03_CreditCardInstallment_DTO.CREDIT_CART_INSTELLMENTDataTable dtCCI = OPT03_DTO.CREDIT_CART_INSTELLMENT;
            OPT03_CreditCardInstallment_DTO.CREDIT_CARD_SETTLEMMENTDataTable dtCCS = OPT03_DTO.CREDIT_CARD_SETTLEMMENT;
            OPT03_CreditCardInstallment_DTO.CREDIT_CART_INSTELLMENTRow drCCI = dtCCI.NewCREDIT_CART_INSTELLMENTRow();

            string INSTELLMENT_ID = StringUtil.CStr(e.Keys["INSTELLMENT_ID"]);

            DataRow dirtyDr = OPT03_PageHelper.GetCreditCardInstallmentByInstellmentId(INSTELLMENT_ID);
            ASPxComboBox ddlBankID = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["BANK_ID"], "ddlBankID") as ASPxComboBox;
            ASPxTextBox txtPaySeqment = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["PAY_SEQMENT"], "txtPAY_SEQMENT") as ASPxTextBox;
            ASPxTextBox txtSeqmentRate = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["SEQMENT_RATE"], "txtSEQMENT_RATE") as ASPxTextBox;
            ASPxDateEdit txtSDate = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["S_DATE"], "txtSDate") as ASPxDateEdit;
            ASPxDateEdit txtEDate = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["E_DATE"], "txtEDate") as ASPxDateEdit;

            drCCI.INSTELLMENT_ID = INSTELLMENT_ID;
            drCCI.PAY_SEQMENT = Convert.ToDecimal(txtPaySeqment.Text);
            drCCI.SEQMENT_RATE = Convert.ToDecimal(txtSeqmentRate.Text);
            drCCI.S_DATE = Convert.ToDateTime(txtSDate.Text);

            if (!string.IsNullOrEmpty(txtEDate.Text))
                drCCI["E_DATE"] = Convert.ToDateTime(txtEDate.Text);
            else
                drCCI["E_DATE"] = DBNull.Value;

            drCCI.BANK_ID = StringUtil.CStr(ddlBankID.Value);
            drCCI.CREATE_DTM = Convert.ToDateTime(StringUtil.CStr(dirtyDr["CREATE_DTM"]));
            drCCI.CREATE_USER = StringUtil.CStr(dirtyDr["CREATE_USER"]);
            drCCI.MODI_DTM = System.DateTime.Now;
            drCCI.MODI_USER = logMsg.OPERATOR;
            drCCI.CREDIT_CARD_TYPE_ID = StringUtil.CStr(dirtyDr["CREDIT_CARD_TYPE_ID"]);
            dtCCI.Rows.Add(drCCI);

            DataTable dt = ViewState["dtCreditDetail"] as DataTable;
            foreach (DataRow dr in dt.Rows)
            {
                OPT03_CreditCardInstallment_DTO.CREDIT_CARD_SETTLEMMENTRow drCCS = dtCCS.NewCREDIT_CARD_SETTLEMMENTRow();
                drCCS.SETTLEMENT_ID = StringUtil.CStr(dr["SETTLEMENT_ID"]);
                drCCS.INSTELLMENT_ID = StringUtil.CStr(dr["INSTELLMENT_ID"]);
                drCCS.LINE_NO = Convert.ToDecimal(StringUtil.CStr(dr["LINE_NO"]));
                drCCS.SETTLEMENT_RATE = Convert.ToDecimal(StringUtil.CStr(dr["SETTLEMENT_RATE"]));
                drCCS.CREATE_USER = StringUtil.CStr(dr["CREATE_USER"]);
                drCCS.CREATE_DTM = Convert.ToDateTime(StringUtil.CStr(dr["CREATE_DTM"]));
                drCCS.MODI_USER = StringUtil.CStr(dr["MODI_USER"]);
                drCCS.MODI_DTM = Convert.ToDateTime(StringUtil.CStr(dr["MODI_DTM"]));
                drCCS.COST_CENTER_NO = StringUtil.CStr(dr["COST_CENTER_NO"]);
                dtCCS.AddCREDIT_CARD_SETTLEMMENTRow(drCCS);
            }

            OPT03_DTO.AcceptChanges();

            //更新資料庫
            Facade.UpdateOne_CreditCardInstallment(OPT03_DTO);

            gvMaster.CancelEdit();
            e.Cancel = true;

            ViewState["dtCreditMaster"] = null;
            BindMasterData();
        }

    }

    protected void gvMaster_FocusedRowChanged(object sender, EventArgs e)
    {

        if (gvMaster.FocusedRowIndex >= 0)
        {
            BindDetailData(false);
            gvDetail.Visible = true;

            if (gvMaster.IsEditing)
            {
                gvMaster.CancelEdit();
            }
        }
    }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                string status1 = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "STATUS"));

                if (status1 == "已過期")
                {
                    e.Enabled = false;
                }
            }

            if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
                string status1 = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "STATUS"));

                if (status1 == "尚未生效")
                    e.Enabled = true;
                else
                    e.Enabled = false;
            }
        }
    }

    protected void gvMaster_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
    {
        Session["INSTELLMENT_ID"] = GuidNo.getUUID();
        ASPxDateEdit sDate = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["S_DATE"], "txtSDATE") as ASPxDateEdit;
        sDate.Text = DateTime.Today.AddDays(1).ToString("yyyy/MM/dd");
        ViewState["dtCreditDetail"] = null;
        gvMaster.FocusedRowIndex = -1;
        BindDetailData(true);
        gvDetail.Visible = true;
        ViewState["dtCreditMaster"] = new OPT03_Facade().Query_CreditCardInstallment("-1", "-1", "", "", "", "", "", "");
    }

    protected void gvMaster_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
    {
        Session["INSTELLMENT_ID"] = null;
        BindDetailData(true);
        gvDetail.Visible = true;
        gvMaster.Selection.UnselectAll();
    }

    protected void gvMaster_RowValidating(object sender, ASPxDataValidationEventArgs e)
    {
        SaveEditMasterDataq();
        ASPxTextBox txtSTATUS = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["STATUS"], "txtSTATUS") as ASPxTextBox;
        ASPxTextBox txtPAY_SEQMENT = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["PAY_SEQMENT"], "txtPAY_SEQMENT") as ASPxTextBox;
        ASPxTextBox txtSEQMENT_RATE = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["SEQMENT_RATE"], "txtSEQMENT_RATE") as ASPxTextBox;
        ASPxComboBox ddlBankID = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["BANK_ID"], "ddlBankID") as ASPxComboBox;
        ASPxDateEdit sDate = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["S_DATE"], "txtSDATE") as ASPxDateEdit;
        ASPxDateEdit eDate = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["E_DATE"], "txtEDATE") as ASPxDateEdit;

        DateTime SDATE = DateTime.Parse(StringUtil.CStr(sDate.Value));
        DateTime EDATE = DateTime.Parse(eDate.Value == null ? "9999/12/31" : StringUtil.CStr(eDate.Value));

        string status = txtSTATUS.Text;

        if (SDATE < DateTime.Today && status != "有效" && status != "已過期")
        {
            e.RowError = "【開始日期】不可小於系統日!";
            SaveTempDropDownValue(StringUtil.CStr(ddlBankID.Value));
            return;
        }

        if (EDATE < DateTime.Today)
        {
            e.RowError = "【結束日期】不可小於系統日!";
            SaveTempDropDownValue(StringUtil.CStr(ddlBankID.Value));
            return;
        }

        if (EDATE < SDATE)
        {
            e.RowError = "【結束日期】必須大於等於【開始日期】!";
            SaveTempDropDownValue(StringUtil.CStr(ddlBankID.Value));
            return;
        }

        string ID = e.Keys["INSTELLMENT_ID"] == null ? StringUtil.CStr(Session["INSTELLMENT_ID"]) : StringUtil.CStr(e.Keys["INSTELLMENT_ID"]);
        int Count = OPT03_PageHelper.CheckCreditCardInstallmentModifyData(sDate.Text, eDate.Text, txtSEQMENT_RATE.Text, StringUtil.CStr(ddlBankID.Value), ID, txtPAY_SEQMENT.Text);
        if (Count > 0)
        {
            e.RowError = "同一家銀行同一分期期數同一手續費利率在同一時間區間不可設定二筆以上!";  //PartI. 同一家銀行同一手續費利率在同一時間區間不可設定二筆以上；PartII. 同一家銀行同一分期期數不可設定二筆以上
            SaveTempDropDownValue(StringUtil.CStr(ddlBankID.Value));
            return;
        }

        Decimal SRate = Convert.ToDecimal(txtSEQMENT_RATE.Text);
        if (SRate > 100)
        {
            e.RowError = "分期利率不可大於100%!";
            SaveTempDropDownValue(StringUtil.CStr(ddlBankID.Value));
            return;
        }

        DataTable dtDetail = ViewState["dtCreditDetail"] as DataTable;
        string Sum = StringUtil.CStr(dtDetail.Compute("Sum(SETTLEMENT_RATE)", "INSTELLMENT_ID = '" + ID + "'"));
        Decimal TotalRate = Convert.ToDecimal(string.IsNullOrEmpty(Sum) ? "0" : Sum);    //總成本中心拆帳比率
        if (SRate != TotalRate)
        {
            e.RowError = "成本中心拆帳比率總合須等於分期利率!";
            SaveTempDropDownValue(StringUtil.CStr(ddlBankID.Value));
            return;
        }

    }

    protected void gvMaster_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        ViewState["dtCreditDetail"] = null;
        ViewState["dtCreditMaster"] = null;
        BindDetailData(false);

        if (gvMaster.FocusedRowIndex < 0)
        {
            gvDetail.Visible = false;
        }
    }

    #endregion

    #region gvDetail 觸發事件

    protected void gvDetail_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.InlineEdit && e.RowType != GridViewRowType.EditingErrorRow)
        {
            ASPxComboBox ddlCostCenterNo = gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["COST_CENTER_NO"], "ddlCostCenterNo") as ASPxComboBox;
            BindddlCostCenter(ddlCostCenterNo, false); //繫結 成本中心

            if (ViewState["TempData"] != null)
            {
                DataTable dt = ViewState["TempData"] as DataTable;
                DataRow dr = dt.Rows[0];

                ddlCostCenterNo.Value = StringUtil.CStr(dr["DropDownValue"]);
                ViewState["TempData"] = null;
            }
            else
            {
                if (!gvDetail.IsNewRowEditing)
                {
                    ddlCostCenterNo.Value = StringUtil.CStr(e.GetValue("COST_CENTER_NO"));
                }//end-if (!gvMaster.IsNewRowEditing)
                else
                {
                    if (gvDetail.VisibleRowCount == 0)
                    {
                        ddlCostCenterNo.Value = "5602";  //通路管理部
                    }
                }
            }
        }//end-if (e.RowType == GridViewRowType.InlineEdit)
    }

    protected void gvDetail_RowInserting(object sender, ASPxDataInsertingEventArgs e)
    {
        if (ViewState["dtCreditDetail"] != null)
        {
            DataTable dt = ViewState["dtCreditDetail"] as DataTable;
            DataRow dr = dt.NewRow();

            ASPxComboBox ddlCostCenterNo = gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["COST_CENTER_NO"], "ddlCostCenterNo") as ASPxComboBox;
            var ID = gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName);
            //string INSTELLMENT_ID = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName));

            dr["INSTELLMENT_ID"] = ID == null ? StringUtil.CStr(Session["INSTELLMENT_ID"]) : StringUtil.CStr(ID);
            dr["SETTLEMENT_ID"] = GuidNo.getUUID();
            dr["LINE_NO"] = dt.Rows.Count + 1;  //項次
            dr["COST_CENTER_NO"] = StringUtil.CStr(ddlCostCenterNo.Value); //e.NewValues["COST_CENTER_NO"]; //成本中心代碼
            dr["COST_CENTER_NAME"] = ddlCostCenterNo.Text;
            dr["SETTLEMENT_RATE"] = e.NewValues["SETTLEMENT_RATE"]; //成本中心拆帳比率
            dr["CREATE_USER"] = logMsg.OPERATOR;
            dr["CREATE_DTM"] = System.DateTime.Now;
            dr["MODI_USER"] = logMsg.OPERATOR;
            dr["MODI_DTM"] = System.DateTime.Now;

            dt.Rows.Add(dr);
            dt.AcceptChanges();

            gvDetail.CancelEdit();
            e.Cancel = true;

            ViewState["dtCreditDetail"] = dt;
            gvDetail.DataSource = dt;
            gvDetail.DataBind();
            //BindDetailData(true);
        }
    }

    protected void gvDetail_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
    {
        if (ViewState["dtCreditDetail"] != null)
        {
            DataTable dt = ViewState["dtCreditDetail"] as DataTable;
            string expression = "SETTLEMENT_ID = '" + StringUtil.CStr(e.Keys["SETTLEMENT_ID"]) + "'";
            DataRow dr = dt.Select(expression)[0];

            ASPxComboBox ddlCostCenterNo = gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["COST_CENTER_NO"], "ddlCostCenterNo") as ASPxComboBox;

            dr["COST_CENTER_NO"] = StringUtil.CStr(ddlCostCenterNo.Value); // e.NewValues["COST_CENTER_NAME"]; //成本中心代碼
            dr["COST_CENTER_NAME"] = ddlCostCenterNo.Text;
            dr["SETTLEMENT_RATE"] = e.NewValues["SETTLEMENT_RATE"]; //成本中心拆帳比率
            dr["MODI_USER"] = logMsg.OPERATOR;
            dr["MODI_DTM"] = System.DateTime.Now;

            dt.AcceptChanges();

            gvDetail.CancelEdit();
            e.Cancel = true;

            ViewState["dtCreditDetail"] = dt;
            gvDetail.DataSource = dt;
            gvDetail.DataBind();
            //BindDetailData(true);
        }
    }

    protected void gvDetail_PageIndexChanged(object sender, EventArgs e)
    {
        BindDetailData(gvMaster.IsEditing);
    }

    protected void gvDetail_RowValidating(object sender, ASPxDataValidationEventArgs e)
    {
        string strCOST_CENTER_NO = StringUtil.CStr(((ASPxComboBox)gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["COST_CENTER_NO"], "ddlCostCenterNo")).Value);

        DataTable dt = ViewState["dtCreditDetail"] as DataTable;

        string expression;
        expression = "COST_CENTER_NO = '" + strCOST_CENTER_NO + "'";
        DataRow[] data = dt.Select(expression);
        for (int i = 0; i < data.Length; i++)
        {
            string strID_Table = e.Keys["SETTLEMENT_ID"] == null ? "" : StringUtil.CStr(e.Keys["SETTLEMENT_ID"]);
            string strID = StringUtil.CStr(data[i]["SETTLEMENT_ID"]);
            if (strID_Table != strID)
            {
                e.RowError = "成本中心重複!!";
                SaveTempDropDownValue(strCOST_CENTER_NO);
                return;
            }

        }
    }

    protected void gvDetail_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
    {
        SaveEditMasterDataq(); //明細檔在編輯資料時，先把主檔資料存起來
        gvDetail.Selection.UnselectAll();
        
    }

    #endregion

    private void SaveTempDropDownValue(string Value)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("DropDownValue", typeof(string));

        DataRow dr = dt.NewRow();
        dr["DropDownValue"] = Value;
        dt.Rows.Add(dr);

        ViewState["TempData"] = dt;
    }

    private void SaveEditMasterDataq()
    {
        if (ViewState["dtCreditMaster"] != null && Session["INSTELLMENT_ID"] != null)
        {
            DataTable dt = ViewState["dtCreditMaster"] as DataTable;

            string expression = "INSTELLMENT_ID = '" + StringUtil.CStr(Session["INSTELLMENT_ID"]) + "'";
            DataRow[] data = dt.Select(expression);
            DataRow dr = null;
            if (data.Length == 0){
                dr = dt.NewRow();
            }
            else{
                dr = data[0];
            }

            ASPxComboBox ddlBankID = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["BANK_ID"], "ddlBankID") as ASPxComboBox;
            ASPxDateEdit txtSDate = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["S_DATE"], "txtSDate") as ASPxDateEdit;
            ASPxDateEdit txtEDate = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["E_DATE"], "txtEDate") as ASPxDateEdit;
            ASPxTextBox txtPaySeqment = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["PAY_SEQMENT"], "txtPAY_SEQMENT") as ASPxTextBox;
            ASPxTextBox txtSeqmentRate = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["SEQMENT_RATE"], "txtSEQMENT_RATE") as ASPxTextBox;

            dr["BANK_ID"] = StringUtil.CStr(ddlBankID.Value);
            dr["E_DATE"] = txtEDate.Text;
            dr["INSTELLMENT_ID"] = StringUtil.CStr(Session["INSTELLMENT_ID"]);
            dr["PAY_SEQMENT"] = string.IsNullOrEmpty(txtPaySeqment.Text) ? "0" : txtPaySeqment.Text;
            dr["S_DATE"] = txtSDate.Text;
            dr["SEQMENT_RATE"] = string.IsNullOrEmpty(txtSeqmentRate.Text) ? "0" : txtSeqmentRate.Text;
            if (data.Length == 0)
            {
                dt.Rows.Add(dr);
            }
            dt.AcceptChanges();
            ViewState["dtCreditMaster"] = dt;
        }
    }


    protected void gvDetail_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
    {

    }
}