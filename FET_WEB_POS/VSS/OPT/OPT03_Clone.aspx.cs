using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Globalization;

using Advtek.Utility;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.DTO;
using FET.POS.Model.Common;


public partial class VSS_OPT_OPT03 : System.Web.UI.Page
{
    #region Class Variable

    IOPT03_Facade _OPT03_Facade;
    OPT03_CreditCardInstallment_DTO _OPT03_CreditCardInstallment_DTO;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDdlCostCenter(ddlCostCenter);
            BindDdlBank(ddlCardBank);
            
            gvMaster.DataSource = GetEmptyDataTable1();
            gvMaster.DataBind();

            gvDetail.DataSource = GetEmptyDataTable2();
            gvDetail.DataBind();           
        }
    }

    private void BindDdlCostCenter(DropDownList ddl)
    {
        ddl.DataSource = OPT03_PageHelper.GetCostCenterNameNo(true);
        ddl.DataValueField = "COST_CENTER_NO";
        ddl.DataTextField = "COST_CENTER_NAME";
        ddl.DataBind();
    }

    private void BindDdlBank(DropDownList ddl)
    {
        ddl.DataSource = OPT03_PageHelper.GetBankNameId(true);
        ddl.DataValueField = "BANK_ID";
        ddl.DataTextField = "BANK_NAME";
        ddl.DataBind();
    }

    protected void bindMasterData( )
    {
        gvMaster.DataSource = ViewState["gvMaster"];
        gvMaster.DataBind();
    }

    protected void bindMasterDataAfterWrite()
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = getMasterData();
        ViewState["gvMaster"] = dtGvMaster;
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
    }

    private DataTable GetEmptyDataTable1()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("序號", typeof(string));
        dt.Columns.Add("狀態", typeof(string));
        dt.Columns.Add("發卡銀行", typeof(string));
        dt.Columns.Add("分期期數", typeof(string));
        dt.Columns.Add("分期利率", typeof(string));
        dt.Columns.Add("成本中心", typeof(string));
        dt.Columns.Add("成本中心折分比率", typeof(string));
        dt.Columns.Add("開始日期", typeof(string));
        dt.Columns.Add("結束日期", typeof(string));
        dt.Columns.Add("更新日期", typeof(string));
        dt.Columns.Add("更新人員", typeof(string));
        return dt;
    }

    //private DataTable getMasterData( )
    //{
    //    DataTable dtMaster = new DataTable();
    //    dtMaster.Columns.Clear();
    //    dtMaster.Columns.Add("序號", typeof(string));
    //    dtMaster.Columns.Add("狀態", typeof(string));
    //    dtMaster.Columns.Add("發卡銀行", typeof(string));
    //    dtMaster.Columns.Add("分期期數", typeof(string));
    //    dtMaster.Columns.Add("分期利率", typeof(string));
    //    dtMaster.Columns.Add("成本中心", typeof(string));
    //    dtMaster.Columns.Add("成本中心折分比率", typeof(string));
    //    dtMaster.Columns.Add("開始日期", typeof(string));
    //    dtMaster.Columns.Add("結束日期", typeof(string));
    //    dtMaster.Columns.Add("更新日期", typeof(string));
    //    dtMaster.Columns.Add("更新人員", typeof(string));

    //   // string[] array1 = { "有效", "過期", "已停止" };
    //    string[] array2 = { "台灣銀行", "遠東銀行", "中國信託", "台新銀行" };
    //    string[] array3 = { "3", "6", "12" };
    //    for (int i = 1; i < 12; i++)
    //    {
    //        DataRow dtMasterRow = dtMaster.NewRow();
    //        dtMasterRow["序號"] = i;
    //        dtMasterRow["狀態"] = "有效";
    //        dtMasterRow["發卡銀行"] = array2[i % 4];
    //        dtMasterRow["分期期數"] = array3[i % 3];
    //        dtMasterRow["分期利率"] = (Convert.ToDouble(i)-0.4);
    //        dtMasterRow["成本中心"] = "成本中心" + i;
    //        dtMasterRow["成本中心折分比率"] = (90/i) ;
    //        dtMasterRow["開始日期"] = "2010/08/01";
    //        if (i == 2) dtMasterRow["結束日期"] = "";
    //        else dtMasterRow["結束日期"] = "2010/12/31";
    //        dtMasterRow["更新日期"] = DateTime.Now.AddDays(-i).AddMinutes(i*32).ToString("yyyy/MM/dd HH:mm:ss");
    //        dtMasterRow["更新人員"] = "王小明";
    //        dtMaster.Rows.Add(dtMasterRow);
    //    }

    //    DataRow dtMasterRow5 = dtMaster.NewRow();
    //    dtMasterRow5["序號"] = 5;
    //    dtMasterRow5["狀態"] = "尚未生效";
    //    dtMasterRow5["發卡銀行"] = "花旗銀行";
    //    dtMasterRow5["分期期數"] = "6";
    //    dtMasterRow5["分期利率"] = "3.6";
    //    dtMasterRow5["成本中心"] = "成本中心5";
    //    dtMasterRow5["成本中心折分比率"] = "40";
    //    dtMasterRow5["開始日期"] = "2010/10/01";
    //    dtMasterRow5["結束日期"] = "2011/12/31";
    //    dtMasterRow5["更新日期"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
    //    dtMasterRow5["更新人員"] = "王小明";
    //    dtMaster.Rows.Add(dtMasterRow5);

    //    return dtMaster;
    //}

    private DataTable getMasterData()
    {        
        try
        {
            //_OPT03_CreditCardInstallment_DTO = OPT03_DataSource_Provider.Get_OPT03_CreditCardInstallment_DTO();

            //return _OPT03_CreditCardInstallment_DTO.Tables["CREDIT_CART_INSTALLMENT"];

            //_OPT03_Facade = new FET.POS.Model.Facade.FacadeMock.OPT03_Facade_Mock();
            _OPT03_Facade = new FET.POS.Model.Facade.FacadeImpl.OPT03_Facade();

            return _OPT03_Facade.Query_CreditCardInstallment(txtIssuingBank.Text, ddlCostCenter.SelectedValue, txtPaySeqment.Text, ddlStatus.SelectedValue);           
        }
        catch (Exception ex)
        {

            throw ex;
        }
        finally
        {
            _OPT03_CreditCardInstallment_DTO = null;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        _OPT03_Facade = new FET.POS.Model.Facade.FacadeMock.OPT03_Facade_Mock();
        DataTable dtMaster = _OPT03_Facade.Query_CreditCardInstallment(txtIssuingBank.Text, ddlCostCenter.SelectedValue,
                                                  txtPaySeqment.Text, ddlStatus.SelectedValue);
        ViewState["gvMaster"] = dtMaster;
        bindMasterData();
        //btnNew.Visible = true;
        //btnDelete.Visible = true;
        //Div1.Visible = true;
    }


    protected void gvMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        //設定編輯欄位
        gridview.EditIndex = e.NewEditIndex;
        //Bind原查詢資料
        bindMasterData();
        
        //取得 發卡銀行 預設值
        DropDownList ddlBankNameId = (DropDownList)gridview.Rows[e.NewEditIndex].Cells[3].FindControl("editDdlBankId");
        BindDdlBank(ddlBankNameId);
        ddlBankNameId.Items.FindByText(((Label)gridview.Rows[e.NewEditIndex].Cells[3].FindControl("editBankId")).Text).Selected = true;

    }
    protected void gvMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView gridview = sender as GridView;

        //取得資料
        _OPT03_CreditCardInstallment_DTO = new OPT03_CreditCardInstallment_DTO();
        OPT03_CreditCardInstallment_DTO.CREDIT_CART_INSTALLMENTDataTable dtCCI;
        OPT03_CreditCardInstallment_DTO.CREDIT_CART_INSTALLMENTRow drCCI;

        dtCCI = _OPT03_CreditCardInstallment_DTO.Tables["CREDIT_CART_INSTALLMENT"] as OPT03_CreditCardInstallment_DTO.CREDIT_CART_INSTALLMENTDataTable;
        drCCI = dtCCI.NewCREDIT_CART_INSTALLMENTRow();        
        drCCI.ItemArray = new object[] 
                           {
                             gvMaster.DataKeys[e.RowIndex].Value, //INSELLMENT_ID
                             Convert.ToDecimal(((TextBox)gvMaster.Rows[e.RowIndex].FindControl("editPaySeqment")).Text), //PAY_SEQMENT
                             Convert.ToDecimal(((TextBox)gvMaster.Rows[e.RowIndex].FindControl("editSeqmentRate")).Text), //SEQMENT_RATE
                             Convert.ToDateTime(((TextBox)gvMaster.Rows[e.RowIndex].FindControl("editSDate")).Text), //S_DATE
                             Convert.ToDateTime(((TextBox)gvMaster.Rows[e.RowIndex].FindControl("editEDate")).Text), //E_DATE
                             "Josh", //CREATE_USER
                             Convert.ToDateTime("2007/7/7"), //CREATE_DTM                             
                             gvMaster.Rows[e.RowIndex].Cells[9].Text, //MODI_USER
                             Convert.ToDateTime(gvMaster.Rows[e.RowIndex].Cells[8].Text), //MODI_DTM
                             ((DropDownList)gvMaster.Rows[e.RowIndex].Cells[3].FindControl("editDdlBankId")).SelectedValue //BANK_ID
                           };

        dtCCI.Rows.Add(drCCI);
        _OPT03_CreditCardInstallment_DTO.AcceptChanges();

        //更新資料庫
        _OPT03_Facade = new OPT03_Facade();
        _OPT03_Facade.UpdateOne_CreditCardInstallment(_OPT03_CreditCardInstallment_DTO);


        //取消編輯狀態
        gridview.EditIndex = -1;

        //Bind新資料(重取資料)
        bindMasterDataAfterWrite();
    }

    protected void bindDetailData()
    {         
        gvDetail.DataSource = ViewState["gvDetail"];
        gvDetail.DataBind();
    }

    protected void bindDetailDataAfterWrite()
    {
        DataTable dtgvDetail = new DataTable();
        dtgvDetail = getDetailData();
        ViewState["gvDetail"] = dtgvDetail;
        gvDetail.DataSource = dtgvDetail;
        gvDetail.DataBind();
    }

    private DataTable GetEmptyDataTable2()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("項次", typeof(string));
        dt.Columns.Add("成本中心", typeof(string));
        dt.Columns.Add("成本中心拆帳比率", typeof(string));
        
        return dt;
    }

    //private DataTable getDetailData()
    //{
    //    DataTable gvDetail = new DataTable();
    //    gvDetail.Columns.Clear();
    //    gvDetail.Columns.Add("項次", typeof(string));
    //    gvDetail.Columns.Add("成本中心", typeof(string));
    //    gvDetail.Columns.Add("成本中心拆帳比率", typeof(string));

    //    DataRow gvDetailRow = gvDetail.NewRow();
    //    gvDetailRow["項次"] = 1;
    //    gvDetailRow["成本中心"] = "行銷部";
    //    gvDetailRow["成本中心拆帳比率"] = "0.3";
    //    gvDetail.Rows.Add(gvDetailRow);

    //    gvDetailRow = gvDetail.NewRow();
    //    gvDetailRow["項次"] = 2;
    //    gvDetailRow["成本中心"] = "通路管理部";
    //    gvDetailRow["成本中心拆帳比率"] = "0.3";
    //    gvDetail.Rows.Add(gvDetailRow);

    //    return gvDetail;
    //}

    private DataTable getDetailData()
    {
        _OPT03_Facade = new OPT03_Facade();
        DataTable dt = _OPT03_Facade.Query_CreditCardSettlement(ViewState["InstellmentId"].ToString());
        ViewState["gvDetail"] = dt;
        return dt;
    }

    /// <summary>
    /// 新增-顯示Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd1_Click(object sender, EventArgs e)
    {
        gvMaster.ShowFooter = true;
        gvMaster.ShowFooterWhenEmpty = true;
        HtmlTableRow tr = gvMaster.FindChildControl<HtmlTableRow>("trEmptyData");
        if (tr != null)
        {
            tr.Visible = false;
        }
        else
        {
            bindMasterData();
            DropDownList ddlBank = (DropDownList)gvMaster.FooterRow.Cells[3].FindControl("footerDdlBankId");
            BindDdlBank(ddlBank);
        }
    }

    protected void btnSave1_Click(object sender, EventArgs e)
    {
        try
        {
            _OPT03_CreditCardInstallment_DTO = new OPT03_CreditCardInstallment_DTO();
            OPT03_CreditCardInstallment_DTO.CREDIT_CART_INSTALLMENTDataTable dtCCI;
            OPT03_CreditCardInstallment_DTO.CREDIT_CART_INSTALLMENTRow drCCI;

            dtCCI = _OPT03_CreditCardInstallment_DTO.Tables["CREDIT_CART_INSTALLMENT"] as OPT03_CreditCardInstallment_DTO.CREDIT_CART_INSTALLMENTDataTable;
            drCCI = dtCCI.NewCREDIT_CART_INSTALLMENTRow();
            string InstellmentId = GuidNo.getUUID();
            drCCI.ItemArray = new object[] 
                            {
                                InstellmentId, //INSELLMENT_ID
                                int.Parse(((TextBox)gvMaster.FooterRow.FindControl("footerPaySeqment")).Text), //PAY_SEQMENT
                                Convert.ToDecimal(((TextBox)gvMaster.FooterRow.FindControl("footerSeqmentRate")).Text), //SEQMENT_RATE
                                Convert.ToDateTime(((TextBox)gvMaster.FooterRow.FindControl("footerSDate")).Text), //S_DATE
                                Convert.ToDateTime(((TextBox)gvMaster.FooterRow.FindControl("footerEDate")).Text), //E_DATE
                                "Josh", //CREATE_USER
                                Convert.ToDateTime("2007/7/7"), //CREATE_DTM                                
                                "Josh", //MODI_USER
                                Convert.ToDateTime(System.DateTime.Today), //MODI_DTM
                                ((DropDownList)gvMaster.FooterRow.Cells[3].FindControl("footerDdlBankId")).SelectedValue //BANK_ID
                            };


            dtCCI.Rows.Add(drCCI);
            _OPT03_CreditCardInstallment_DTO.AcceptChanges();

            //更新資料庫
            _OPT03_Facade = new OPT03_Facade();
            _OPT03_Facade.AddNewOne_CreditCardInstallment(_OPT03_CreditCardInstallment_DTO);

            bindMasterDataAfterWrite();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            _OPT03_CreditCardInstallment_DTO = null;
            _OPT03_Facade = null;
        }
    }

    /// <summary>
    /// 取消新增-隱藏Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        gvMaster.ShowFooterWhenEmpty = false;
        gvMaster.ShowFooter = false;
        if (gvMaster.Rows.Count == 0)
        {
            gvMaster.DataSource = GetEmptyDataTable1();
            gvMaster.DataBind();
        }
        else
        {
            bindMasterData();
        }
    }
    protected void gvMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        gridview.EditIndex = -1;
        //Bind原查詢資料
        DataTable dt = ViewState["gvMaster"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }
    protected void gvMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvMasterSelectedRow = ((WebControl)e.CommandSource).NamingContainer as GridViewRow;                 

        if (e.CommandName == "select")
        {
            ViewState["InstellmentId"] = gvMaster.DataKeys[gvMasterSelectedRow.DataItemIndex].Value;
            ViewState["SeqmentRate"] = ((Label)gvMasterSelectedRow.Cells[5].FindControl("itemSeqmentRate")).Text;
            dvDetail.Style["display"] = "";            
            this.gvDetail.Visible = true;

            gvDetail.DataSource = getDetailData();
            gvDetail.DataBind();
            //Button1.Visible = Button2.Visible = true;
        }
    }

    /// <summary>
    /// 新增-顯示Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd2_Click(object sender, EventArgs e)
    {
        gvDetail.ShowFooter = true;
        gvDetail.ShowFooterWhenEmpty = true;
        HtmlTableRow tr = gvDetail.FindChildControl<HtmlTableRow>("trEmptyData");
        if (tr != null)
        {
            tr.Visible = false;
        }
        else
        {
            bindDetailData();
            DropDownList ddlCostCenter = (DropDownList)gvDetail.FooterRow.FindControl("footerDdlCostCenter");
            BindDdlCostCenter(ddlCostCenter);
        }
    }

    protected void btnSave2_Click(object sender, EventArgs e)
    {
        try
        {
            _OPT03_CreditCardInstallment_DTO = new OPT03_CreditCardInstallment_DTO();
            OPT03_CreditCardInstallment_DTO.CREDIT_CARD_SETTLEMMENTDataTable dtCCS;
            OPT03_CreditCardInstallment_DTO.CREDIT_CARD_SETTLEMMENTRow drCCS;

            dtCCS = _OPT03_CreditCardInstallment_DTO.Tables["CREDIT_CARD_SETTLEMMENT"] as OPT03_CreditCardInstallment_DTO.CREDIT_CARD_SETTLEMMENTDataTable;
            drCCS = dtCCS.NewCREDIT_CARD_SETTLEMMENTRow();
            
            drCCS.ItemArray = new object[] 
                            {
                               GuidNo.getUUID(),//SETTLEMENT_ID
                               ViewState["InstellmentId"],//INSTELLMENT_ID
                               gvDetail.Rows.Count + 1,//LINE_NO
                               ((TextBox)gvDetail.FooterRow.FindControl("footerSettlementRate")).Text,//SETTLEMENT_RATE
                               "Josh", //CREATE_USER
                               Convert.ToDateTime(System.DateTime.Today), //CREATE_DTM
                               "Elly", //MODI_USER
                               Convert.ToDateTime(System.DateTime.Today), //MODI_DTM
                               ((DropDownList)gvDetail.FooterRow.FindControl("footerDdlCostCenter")).SelectedValue, //COST_CENTER_NO
                            };


            dtCCS.Rows.Add(drCCS);
            _OPT03_CreditCardInstallment_DTO.AcceptChanges();

            //更新資料庫
            _OPT03_Facade = new OPT03_Facade();
            _OPT03_Facade.AddNew_CreditCardSettlements(_OPT03_CreditCardInstallment_DTO);

            bindMasterDataAfterWrite();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            _OPT03_CreditCardInstallment_DTO = null;
            _OPT03_Facade = null;
        }
    }

    /// <summary>
    /// 取消新增-隱藏Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel2_Click(object sender, EventArgs e)
    {
        gvDetail.ShowFooterWhenEmpty = false;
        gvDetail.ShowFooter = false;
        if (gvDetail.Rows.Count == 0)
        {
            gvDetail.DataSource = GetEmptyDataTable2();
            gvDetail.DataBind();
        }
        else
        {
            bindDetailData();
        }
    }
    protected void gvDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView grDetail = sender as GridView;
        //設定編輯欄位
        grDetail.EditIndex = e.NewEditIndex;

        //Bind原查詢資料
        bindDetailData();

        //取得 成本中心 預設值
        DropDownList ddlCostCenter = (DropDownList)grDetail.Rows[e.NewEditIndex].Cells[3].FindControl("editDdlCostCenter");
        BindDdlCostCenter(ddlCostCenter);
        ddlCostCenter.Items.FindByText(((Label)grDetail.Rows[e.NewEditIndex].Cells[3].FindControl("editCostCenter")).Text).Selected = true;

    }
    protected void gvDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView grDetail = sender as GridView;
        grDetail.EditIndex = -1;
        //Bind原查詢資料
        bindDetailData();
    }
    protected void gvDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
       GridView gridview = sender as GridView;

        //取得資料
        _OPT03_CreditCardInstallment_DTO = new OPT03_CreditCardInstallment_DTO();
        OPT03_CreditCardInstallment_DTO.CREDIT_CARD_SETTLEMMENTDataTable dtCCS;
        OPT03_CreditCardInstallment_DTO.CREDIT_CARD_SETTLEMMENTRow drCCS;

        dtCCS = _OPT03_CreditCardInstallment_DTO.Tables["CREDIT_CARD_SETTLEMMENT"] as OPT03_CreditCardInstallment_DTO.CREDIT_CARD_SETTLEMMENTDataTable;
        drCCS = dtCCS.NewCREDIT_CARD_SETTLEMMENTRow();
        drCCS.ItemArray = new object[] 
                           {
                               gridview.DataKeys[e.RowIndex].Value,//SETTLEMENT_ID
                               ViewState["InstellmentId"],//INSTELLMENT_ID
                               e.RowIndex,//LINE_NO
                               ((TextBox)gridview.Rows[e.RowIndex].FindControl("editSettlementRate")).Text,//SETTLEMENT_RATE
                               "Josh", //CREATE_USER
                               Convert.ToDateTime(System.DateTime.Today), //CREATE_DTM
                               "Elly", //MODI_USER
                               Convert.ToDateTime(System.DateTime.Today), //MODI_DTM
                               ((DropDownList)gridview.Rows[e.RowIndex].FindControl("editDdlCostCenter")).SelectedValue, //COST_CENTER_NO                               
                           };

        dtCCS.Rows.Add(drCCS);
        _OPT03_CreditCardInstallment_DTO.AcceptChanges();

        //更新資料庫
        _OPT03_Facade = new OPT03_Facade();
        _OPT03_Facade.Update_CreditCardSettlements(_OPT03_CreditCardInstallment_DTO);

        //取消編輯狀態
        gridview.EditIndex = -1;

        //Bind新資料(重取資料)
        bindDetailDataAfterWrite();
    }
    #region 分頁相關 (共用)
    protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       //此函式可共用
       GridView gridview = sender as GridView;
       gridview.PageIndex = e.NewPageIndex;

       DataTable dt = ViewState[gridview.ID] as DataTable ?? null;//GridView的資料要存於ViewState以方便共用此Function
       gridview.DataSource = dt;
       gridview.DataBind();
    }
    protected void btnGoToIndex_Click(object sender, EventArgs e)
    {
       //此函式可共用
       GridView gridview = (sender as Button).Parent.Parent.Parent.Parent as GridView;
       TextBox textbox = (sender as Button).Parent.FindControl("tbGoToIndex") as TextBox;
       string strIndex = textbox.Text.Trim();
       int index = 0;
       if (int.TryParse(strIndex, out index))
       {
          index = index - 1;
          if (index >= 0 && index <= gridview.PageCount - 1)
          {
             gridview.PageIndex = index;
             DataTable dt = ViewState[gridview.ID] as DataTable ?? null;//GridView的資料要存於ViewState以方便共用此Function
             gridview.DataSource = dt;
             gridview.DataBind();
          }
          else
          {
             textbox.Text = string.Empty;
          }
       }
       else
       {
          textbox.Text = string.Empty;
       }
    }
    #endregion
   
}
