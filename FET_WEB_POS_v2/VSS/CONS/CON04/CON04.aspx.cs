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
public partial class VSS_CONS_CON04 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //商品類別下拉
        bindDdlValTxt(ddlProductCategory, PRODUCT_PageHelper.GetCsmProDTypeNo(true), "PRODTYPE_NO", "PRODTYPE_NAME");

        if (!IsPostBack)
        {
            //上一頁網址
            Session["Up_Url"] = Request.ServerVariables["http_referer"];



            if (logMsg.ROLE_TYPE != System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"].ToString())
            {
                //彈跳視窗
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);
                PanelPage.Enabled = false;
                return;
            }
            Session["gvMaster"] = null;
            Session["GridView1"] = null;
            SupportDateRangeFrom.MinDate = DateTime.Now.AddDays(-1);
            SupportDateRangeTo.MinDate = DateTime.Now.AddDays(-1);
            txtCeaseDate.MinDate = DateTime.Now.AddDays(1);
            if (!string.IsNullOrEmpty(Request.QueryString["ProdNo"]))
            {
                Session["ReturnUrl"] = Request.UrlReferrer.ToString();
                string strProdNo = Request.QueryString["ProdNo"].ToString();
                lblStatus.Text = "已存檔";
                CON04_Facade CON04_Facade = new CON04_Facade();
                DataSet ds = CON04_Facade.QueryCsmProduct(strProdNo);
                DataTable dtProduct = ds.Tables["PRODUCT"];
                DataTable dtCommission = ds.Tables["CSM_PROD_COMMISSION"];
                DataTable dtCommissionDelete = dtCommission.Copy();
                importButton.Enabled = false;
                gvMaster.DataSource = dtCommission;
                gvMaster.DataBind();
                dtCommissionDelete.Rows.Clear();
                Session["gvMaster"] = dtCommission;
                Session["gvMaster_Delete"] = dtCommissionDelete;
                if (dtProduct.Rows.Count > 0)
                {
                    DataRow dr = dtProduct.Rows[0];
                    txtSupplierNo.Text = dr["SUPP_NO"].ToString();
                    txtProductName.Text = dr["PRODNAME"].ToString();
                    txtProductCode.Text = dr["PRODNO"].ToString();
                    txtProductCode.ReadOnly = true;
                    ddlProductCategory.Value = dr["PRODTYPENO"].ToString();
                    string strSDate = dr["S_DATE"].ToString();
                    DateTime SDate = new DateTime();
                    if (DateTime.TryParse(strSDate, out SDate))
                    {
                        SupportDateRangeFrom.Text = SDate.ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        SupportDateRangeFrom.Text = strSDate;
                    }
                    if (DateTime.Now > SDate)
                    {
                        btnDelete.Enabled = false;
                    }
                    string strCeaseDate = dr["CEASEDATE"].ToString();
                    DateTime CeaseDate = new DateTime();
                    if (DateTime.TryParse(strCeaseDate, out CeaseDate))
                    {
                        txtCeaseDate.Text = CeaseDate.ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        txtCeaseDate.Text = strCeaseDate;
                    }
                    txtAcct1.Text = dr["ACCT1"].ToString();
                    txtAcct2.Text = dr["ACCT2"].ToString();
                    txtAcct3.Text = dr["ACCT3"].ToString();
                    txtAcct4.Text = dr["ACCT4"].ToString();
                    txtAcct5.Text = dr["ACCT5"].ToString();
                    txtAcct6.Text = dr["ACCT6"].ToString();
                    txtUnit.Text = dr["UNIT"].ToString();
                }
                lblDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                lblUser.Text = logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER);

                lblStatus.Text = "已存檔";
            }
            else
            {

                //更新日期,更新人員
                lblDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                lblUser.Text = logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER);
                lblStatus.Text = "未存檔";
                bindMasterData();
                //bindGridView1Data();
            }


        }
        else
        {

            DataTable dtGvMaster = Session["gvMaster"] as DataTable;
            if (dtGvMaster != null)
            {
                gvMaster.DataSource = dtGvMaster;
                gvMaster.DataBind();
            }
            DataTable dtGridView1 = Session["GridView1"] as DataTable;
            if (dtGridView1 != null)
            {
                GridView1.DataSource = dtGridView1;
                GridView1.DataBind();
            }

        }
    }
    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        Session["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    protected void bindGridView1Data()
    {
        DataTable dtResult = new DataTable();
        dtResult = getGridView1Data();
        Session["GridView1"] = dtResult;
        GridView1.DataSource = dtResult;
        GridView1.DataBind();
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("CPC_ID", typeof(string));
        dtResult.Columns.Add("COMMISSION", typeof(string));
        dtResult.Columns.Add("S_DATE", typeof(string));
        dtResult.Columns.Add("E_DATE", typeof(string));
        return dtResult;
    }
    private DataTable getGridView1Data()
    {
        CON04_Facade CON04 = new CON04_Facade();
        DataTable dtResult = new DataTable();
        dtResult = CON04.QueryCsmProdPrice(txtSupplierNo.Text, txtProductCode.Text);
        return dtResult;
    }
    #region Button 觸發事件
    protected void btnAddRow_Click(object sender, EventArgs e)
    {

        if (!gvMaster.IsEditing)
        {
            gvMaster.Selection.UnselectAll();
            gvMaster.AddNewRow();
        }
    }
    protected void btnDeleteRow_Click(object sender, EventArgs e)
    {
        //List<object> gvPKValues = gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);

        //string pkFName = gvMaster.KeyFieldName;

        DataTable dtSYS;
        DataTable dtRemove = new DataTable();
        if (Session["gvMaster"] == null || StringUtil.CStr(Session["gvMaster"]) == "")
        {
            dtSYS = new DataTable();
        }
        else
        {
            dtSYS = Session["gvMaster"] as DataTable;
        }

        if (Session["gvMaster_Delete"] == null || StringUtil.CStr(Session["gvMaster_Delete"]) == "")
        {
            dtRemove = new DataTable();
        }
        else
        {
            dtRemove = Session["gvMaster_Delete"] as DataTable;
        }

        if (dtSYS.Rows.Count > 0)
        {

            List<object> keyValues = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
            foreach (object key in keyValues)
            {
                DataRow drSYS = dtSYS.Select("CPC_ID='" + key.ToString() + "'")[0];
                dtRemove.ImportRow(drSYS);

                dtSYS.Rows.Remove(drSYS);


            }
            Session["gvMaster_Delete"] = dtRemove;
            gvMaster.Selection.UnselectAll();
            Session["gvMaster"] = dtSYS;
            gvMaster.DataSource = Session["gvMaster"];
            gvMaster.DataBind();

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        string log = new Product_Facade().Check_Id(txtProductCode.Text.ToString());
        
        
        DataTable dtSYS = (DataTable)Session["gvMaster"];
        if (dtSYS.Rows.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "商品", "alert('未輸入佣金比率不可存檔!');", true);
            return;
        }
        //若為未存檔表示為新增反之為修改
        if (lblStatus.Text == "未存檔")
        {
            if (log.ToString() != "")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('商品料號重複!!');", true);
                txtProductCode.Text = "";
                return;
            }
            CON04_Facade CON04_Facade = new CON04_Facade();
            CON04_CSM_PROD CON04_CSM_PROD = new CON04_CSM_PROD();

            CON04_CSM_PROD.PRODUCTDataTable dtProduct = doInsertProduct();
            CON04_CSM_PROD.CSM_PROD_COMMISSIONDataTable dtCommission = doInsertCsmProdCommission();
            int intResult = CON04_Facade.InsertCsmProdData(dtProduct, dtCommission);
            lblStatus.Text = "已存檔";
            string msgStr = "存檔完成!";
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "儲存", "alert('" + msgStr + "!'); ", true);

        }
        else
        {
            CON04_Facade CON04_Facade = new CON04_Facade();
            CON04_CSM_PROD CON04_CSM_PROD = new CON04_CSM_PROD();

            CON04_CSM_PROD.PRODUCTDataTable dtProduct = doUpdateProduct();
            CON04_CSM_PROD.CSM_PROD_COMMISSIONDataTable dtCommission = doUpdateCsmProdCommission();
            CON04_CSM_PROD.CSM_PROD_COMMISSIONDataTable dtDeleteCommission = doDeleteCsmProdCommission();
            int intResult = CON04_Facade.UpdateCsmProdData(dtProduct, dtCommission, dtDeleteCommission);
            lblStatus.Text = "已存檔";
            string msgStr = "存檔完成!";
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "儲存", "alert('" + msgStr + "!');", true);

        }

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CON04_Facade CON04_Facade = new CON04_Facade();

        int intResult = CON04_Facade.DeleteCsmProdData(txtProductCode.Text);
        lblStatus.Text = "已存檔";
        string msgStr = "刪除完成!";
        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "刪除", "alert('" + msgStr + "!'); document.location.href ='" + Session["Up_Url"] + "';", true);

    }
    protected void btnSave1_Click(object sender, EventArgs e)
    {
        DataTable dt;
        if (Session["gvMaster"] == null)
        {
            dt = new DataTable();
            dt.Columns.Add("CPC_ID");
            dt.Columns.Add("COMMISSION");
            dt.Columns.Add("S_DATE");
            dt.Columns.Add("E_DATE");
        }
        else
        {
            dt = (DataTable)Session["gvMaster"];

        }
        try
        {
            ASPxTextBox tb1 = gvMaster.FindChildControl<ASPxTextBox>("txtCommissionRate");
            ASPxDateEdit tb2 = gvMaster.FindChildControl<ASPxDateEdit>("txtSDate");
            ASPxDateEdit tb3 = gvMaster.FindChildControl<ASPxDateEdit>("txtEDate");

            if (dt.Rows.Count > 0)
            {
                DataRow[] drSYS = dt.Select("CPC_ID='" + Session["CPC_ID"] + "'");
                if (drSYS.Length > 0)
                {
                    for (int i = 0; i < drSYS.Length; i++)
                    {
                        string strS_DATE;
                        string strNow_DATE;
                        strS_DATE = drSYS[i]["S_DATE"].ToString().Replace("/", "");
                        strNow_DATE = DateTime.Now.ToString("yyyyMM");
                        if (int.Parse(strS_DATE) <= int.Parse(strNow_DATE) && drSYS[i]["E_DATE"].ToString() == "")
                        {
                            drSYS[i]["E_DATE"] = DateTime.Now.ToString("yyyy/MM");

                        }
                    }
                }
            }
            DataRow dr = dt.NewRow();
            dr["CPC_ID"] = GuidNo.getUUID();
            dr["COMMISSION"] = tb1.Text.Trim();
            dr["S_DATE"] = tb2.Text.Trim();
            dr["E_DATE"] = tb3.Text.Trim();
            dt.Rows.Add(dr);
            Session["gvMaster"] = dt;
            gvMaster.CancelEdit();
            gvMaster.DataSource = dt;
            gvMaster.DataBind();
        }
        catch //(Exception ex)
        {

            throw;
        }

    }
    #endregion


    #region gvMaster 觸發事件
      

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dt;
        if (Session["gvMaster"] == null)
        {
            dt = new DataTable();
            dt.Columns.Add("CPC_ID");
            dt.Columns.Add("COMMISSION");
            dt.Columns.Add("S_DATE");
            dt.Columns.Add("E_DATE");
        }
        else
        {
            dt = (DataTable)Session["gvMaster"];
        }

        string e_date = e.NewValues["E_DATE"].ToString();
        string CommissionRate = e.NewValues["COMMISSION"].ToString();
        ASPxTextBox tb1 = gvMaster.FindChildControl<ASPxTextBox>("txtCommissionRate");
        ASPxDateEdit tb2 = gvMaster.FindChildControl<ASPxDateEdit>("txtSDate");
        ASPxDateEdit tb3 = gvMaster.FindChildControl<ASPxDateEdit>("txtEDate");
        bool chData = false;

        if (dt.Rows.Count > 0)
        {
            DataRow[] drSYS = dt.Select("COMMISSION='" + CommissionRate + "'");
            if (drSYS.Length > 0)
            {
                for (int i = 0; i < drSYS.Length; i++)
                {
                    string strS_DATE;
                    string strE_DATE;
                    string strNow_DATE;
                    strS_DATE = drSYS[i]["S_DATE"].ToString().Replace("/", "");
                    strE_DATE = drSYS[i]["E_DATE"].ToString().Replace("/", "");
                    strNow_DATE = DateTime.Now.ToString("yyyyMM");
                    if (int.Parse(strS_DATE) <= int.Parse(strNow_DATE) && (string.IsNullOrEmpty(strE_DATE) || int.Parse(strE_DATE) >= int.Parse(strNow_DATE)))
                    {
                        drSYS[i]["E_DATE"] = e_date;
                        chData = true;
                        //Session["CPC_ID"] = drSYS[i]["CPC_ID"].ToString();
                        break;
                    }
                }
            }
        }

        if (chData)
        {
            //string Rtn = "您確定要儲存資料嗎？";

            //string javascript = "";
            //javascript += "if (confirm('" + Rtn + "')) {";
            //javascript += " btnSave1.SendPostBack('Click');";
            //javascript += "} ";
            //ScriptManager.RegisterClientScriptBlock(this, typeof(string), "msg", javascript, true);
            Session["gvMaster"] = dt;
            gvMaster.CancelEdit();
            e.Cancel = true;
            gvMaster.DataSource = dt;
            gvMaster.DataBind();
            return;
        }
        else
        {
            DataRow dr = dt.NewRow();
            dr["CPC_ID"] = "";
            dr["COMMISSION"] = tb1.Text.Trim();
            dr["S_DATE"] = tb2.Text.Trim();
            dr["E_DATE"] = tb3.Text.Trim();
            dt.Rows.Add(dr);
            Session["gvMaster"] = dt;
            gvMaster.CancelEdit();
            e.Cancel = true;
            gvMaster.DataSource = dt;
            gvMaster.DataBind();
        }


    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataTable dtSYS;
        if (Session["gvMaster"] == null || StringUtil.CStr(Session["gvMaster"]) == "")
        {
            dtSYS = new DataTable();
        }
        else
        {
            dtSYS = Session["gvMaster"] as DataTable;
        }

        string sCpcId = e.NewValues["CPC_ID"].ToString();
        string sNewCommission = e.NewValues["COMMISSION"].ToString();
        string sNewSDate = e.NewValues["S_DATE"].ToString();
        string sNewEDate = e.NewValues["E_DATE"].ToString();
        string sOldCpcId = e.OldValues["CPC_ID"].ToString();
        string sOldCommission = e.OldValues["COMMISSION"].ToString();
        string sOldSDate = e.OldValues["S_DATE"].ToString();
        string sOldEDate = e.OldValues["E_DATE"].ToString();
        for (int i = 0; i < dtSYS.Rows.Count; i++)
        {
            DataRow dr = dtSYS.Rows[i];
            if (dr["CPC_ID"].ToString().CompareTo(sCpcId) == 0)
            {
                //dr["CPC_ID"] = sCpcId.Trim();
                dr["COMMISSION"] = sNewCommission.Trim();
                dr["S_DATE"] = sNewSDate.Trim();
                dr["E_DATE"] = sNewEDate.Trim();
                dtSYS.AcceptChanges();
                break;
            }
        }

        gvMaster.CancelEdit();
        e.Cancel = true;
        Session["gvMaster"] = dtSYS;
        gvMaster.DataSource = dtSYS;
        gvMaster.DataBind();
    }

    protected void gvMaster_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        DataTable dtSYS;
        if (Session["gvMaster"] == null || StringUtil.CStr(Session["gvMaster"]) == "")
        {
            dtSYS = new DataTable();
        }
        else
        {
            dtSYS = Session["gvMaster"] as DataTable;
        }
        string strCommission = e.NewValues["COMMISSION"].ToString();
        string strSDate = e.NewValues["S_DATE"].ToString();
        string strEDate = e.NewValues["E_DATE"].ToString();
        string strCpcId = e.NewValues["CPC_ID"].ToString();
        int intSDate =0;
         int.TryParse( strSDate.Replace("/", "").ToString(),out intSDate);
        int intEDate = 0;
        if (string.IsNullOrEmpty(strEDate)) { strEDate = "9999/12"; }
         int.TryParse( strEDate.Replace("/","").ToString(),out intEDate);
        foreach (DataRow dr in dtSYS.Rows )
        {
            string strCPC_ID_Table = e.Keys["CPC_ID"] == null ? "新增" : e.Keys["CPC_ID"].ToString();
            string strdrCommission = dr["COMMISSION"].ToString();
            string strCPC_ID = dr["CPC_ID"].ToString();
            string strdrEdate = dr["E_DATE"].ToString(); ;
           int drSDate = 0;
             int.TryParse( dr["S_DATE"].ToString().Replace("/", "").ToString(),out drSDate);
            int drEDate =0;
            if (string.IsNullOrEmpty(dr["E_DATE"].ToString())) { strdrEdate = "9999/12"; }
            int.TryParse(strdrEdate.Replace("/", "").ToString(), out drEDate);
            if (!string.IsNullOrEmpty(strCPC_ID_Table) && strCPC_ID_Table != strCPC_ID && strCommission != strdrCommission)
            {
                if ((intSDate >= drSDate && intSDate <= drEDate) || (intEDate >= drSDate && intEDate <= drEDate))
                {
                 e.RowError += "佣金比率生效起訖區間不可重複,請重新輸入!!";
                return;
                }
               
            }

        } 
        if (!string.IsNullOrEmpty(strCommission) && !string.IsNullOrEmpty(txtSupplierNo.Text) && !string.IsNullOrEmpty(txtProductCode.Text))
        {
            DataTable dtProd = new CON04_Facade().QueryCsmProdCommission(strCommission,strCpcId, txtSupplierNo.Text, txtProductCode.Text, strSDate, strEDate);
            if (dtProd.Rows.Count > 0)
            {
                e.RowError += "佣金比率生效起訖區間不可重複,請重新輸入!!";
                return;

            }

        }


    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = Session["gvMaster"];
        grid.DataBind();
    }
    protected void gvMaster_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxTextBox tb1 = gvMaster.FindChildControl<ASPxTextBox>("txtCommissionRate");
        tb1.Focus();
    }


    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            string STATUS = lblStatus.Text;

            if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
                //string S_DATE = gvMaster.GetRowValues(e.VisibleIndex, "S_DATE").ToString();
                string E_DATE = gvMaster.GetRowValues(e.VisibleIndex, "E_DATE").ToString();
                if (E_DATE != "")
                {
                    if (int.Parse(E_DATE.Replace("/", "")) < int.Parse(DateTime.Now.ToString("yyyyMM")))
                    {
                        e.Enabled = false;
                    }
                }

            }
        }
        if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Update || e.ButtonType == ColumnCommandButtonType.Cancel)
        {
            //e.Visible = false;
        }
        
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        try
        {
                e.Row.Attributes["canSelect"] = "true";
                        if (e.RowType == GridViewRowType.Data)
                        {
                            string E_DATE = e.GetValue("E_DATE").ToString();
                            if (E_DATE != "")
                            {
                                if (int.Parse(E_DATE.Replace("/", "")) < int.Parse(DateTime.Now.ToString("yyyyMM")))
                                {
                                    e.Row.Attributes["canSelect"] = "false";
                                }
                            }
                        }
        }
        catch //(Exception ex)
        {
            
            throw;
        }
        
    }

    #endregion
    /// <summary>
    /// change  tab事件
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void ASPxPageControl1_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
    {
        int iIndex = this.ASPxPageControl1.ActiveTabIndex;
        int fIndex = gvMaster.FocusedRowIndex;
        bool blCheck = true;
        if (iIndex == 1 && txtSupplierNo.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('請先選擇廠商編號');", true);
            this.ASPxPageControl1.ActiveTabIndex = 0;
            blCheck = false;
        }
        if (iIndex == 1 && txtProductCode.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('請先選擇商品料號');", true);
            this.ASPxPageControl1.ActiveTabIndex = 0;
            blCheck = false;
        }
        if (iIndex == 1 && blCheck)
        {
            bindGridView1Data();
        }

    }

    protected void bindDdlValTxt(ASPxComboBox AspCB, object dataSrc, string valCol, string txtCol)
    {
        AspCB.DataSource = dataSrc;
        AspCB.ValueField = valCol;
        AspCB.TextField = txtCol;
        AspCB.DataBind();
    }
    #region 組Insert Update DataTable
    /// <summary>
    /// 組INSERT PRODUCT DATATABLE
    /// </summary>
    /// <returns></returns>
    private CON04_CSM_PROD.PRODUCTDataTable doInsertProduct()
    {
        CON04_CSM_PROD _CON04_CSM_PROD = new CON04_CSM_PROD();
        CON04_CSM_PROD.PRODUCTDataTable dtProduct = null;

        string strProdNo = Request.QueryString["ProdNo"] == null ? "" : Request.QueryString["ProdNo"].ToString().Trim();
        CON04_CSM_PROD.PRODUCTRow drProduct;
        dtProduct = _CON04_CSM_PROD.Tables["PRODUCT"] as CON04_CSM_PROD.PRODUCTDataTable;
        drProduct = dtProduct.NewPRODUCTRow();

        //異動的欄位

        //商品料號
        drProduct["PRODNO"] = txtProductCode.Text.ToString();
        //商品類別
        drProduct["PRODTYPENO"] = ddlProductCategory.SelectedItem.Value.ToString();
        //商口名稱
        drProduct["PRODNAME"] = txtProductName.Text.ToString();
        //單位
        drProduct["UNIT"] = txtUnit.Text.ToString();
        //是否為寄銷商品
        drProduct["ISCONSIGNMENT"] = "1";
        //商品狀態
        drProduct["STATUS"] = "1";
        //會計科目
        drProduct["ACCOUNTCODE"] = txtAcct1.Text.ToString().ToString() + txtAcct2.Text.ToString() + txtAcct3.Text.ToString() + txtAcct4.Text.ToString() + txtAcct5.Text.ToString() + txtAcct6.Text.ToString();
        //是否扣庫存
        drProduct["ISSTOCK"] = "1";
        //是否POS自訂價格
        drProduct["IS_POS_DEF_PRICE"] = "Y";
        //建立人員
        drProduct["CREATE_USER"] = logMsg.MODI_USER;
        //建立時間
        drProduct["CREATE_DTM"] = Advtek.Utility.DateUtil.NullDateFormat(lblDate.Text.ToString());
        //廠商ID
        Supplier_Facade Supplier_Facade = new Supplier_Facade();
        string SuppId = Supplier_Facade.GetSuppId(txtSupplierNo.Text.ToString());

        drProduct["SUPP_ID"] = SuppId;
        //上架日期
        drProduct["S_DATE"] = Advtek.Utility.DateUtil.NullDateFormat(SupportDateRangeFrom.Text.ToString());
        //下架日期
        string strEDate = SupportDateRangeTo.Text.ToString();
        string strCeaseDate = txtCeaseDate.Text.Replace("/", "");
        if (!string.IsNullOrEmpty(strEDate) || strEDate != " ")
        {
            drProduct["E_DATE"] = Advtek.Utility.DateUtil.NullDateFormat(SupportDateRangeTo.Text.ToString()); ;
        }

        if (!string.IsNullOrEmpty(strCeaseDate) || strCeaseDate != " ")
        {
            //失效日期
            drProduct["CEASEDATE"] = strCeaseDate;
        }

        //刪除註記
        drProduct["DEL_FLAG"] = "N";

        dtProduct.Rows.Add(drProduct);

        return dtProduct;
    }
    /// <summary>
    /// 組INSERT CSM_PROD_COMMISSION DATATABLE
    /// </summary>
    /// <returns></returns>
    private CON04_CSM_PROD.CSM_PROD_COMMISSIONDataTable doInsertCsmProdCommission()
    {
        CON04_CSM_PROD.CSM_PROD_COMMISSIONDataTable dtCsmProdCommission = null;
        DataTable dtProd = (DataTable)Session["gvMaster"];
        dtCsmProdCommission = new CON04_CSM_PROD.CSM_PROD_COMMISSIONDataTable();
        CON04_CSM_PROD.CSM_PROD_COMMISSIONRow drCsmProdCommission;
        Supplier_Facade Supplier_Facade = new Supplier_Facade();
        string SuppId = Supplier_Facade.GetSuppId(txtSupplierNo.Text.ToString());
        if (dtProd.Rows.Count > 0)
        {
            int i = 1;
            foreach (DataRow dr in dtProd.Rows)
            {

                drCsmProdCommission = dtCsmProdCommission.NewCSM_PROD_COMMISSIONRow();
                drCsmProdCommission["CPC_ID"] = GuidNo.getUUID();
                drCsmProdCommission["SEQNO"] = i;
                drCsmProdCommission["PRODNO"] = txtProductCode.Text.ToString();
                drCsmProdCommission["SUPP_ID"] = SuppId;
                drCsmProdCommission["COMMISSION"] = dr["COMMISSION"];
                drCsmProdCommission["S_DATE"] = dr["S_DATE"];
                drCsmProdCommission["E_DATE"] = dr["E_DATE"];
                //drCsmProdCommission["MODI_USER"] = logMsg.MODI_USER;
                //drCsmProdCommission["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
                drCsmProdCommission["CREATE_USER"] = logMsg.MODI_USER;
                drCsmProdCommission["CREATE_DTM"] = Convert.ToDateTime(lblDate.Text.ToString());
                dtCsmProdCommission.Rows.Add(drCsmProdCommission);
                i++;
            }
        }
        Session["gvMaster"] = dtCsmProdCommission;
        return dtCsmProdCommission;
    }

    /// <summary>
    /// 組UPDATE PRODUCT DATATABLE
    /// </summary>
    /// <returns></returns>
    private CON04_CSM_PROD.PRODUCTDataTable doUpdateProduct()
    {
        CON04_CSM_PROD _CON04_CSM_PROD = new CON04_CSM_PROD();
        CON04_CSM_PROD.PRODUCTDataTable dtProduct = null;

        string strProdNo = Request.QueryString["ProdNo"] == null ? "" : Request.QueryString["ProdNo"].ToString().Trim();
        CON04_CSM_PROD.PRODUCTRow drProduct;
        dtProduct = _CON04_CSM_PROD.Tables["PRODUCT"] as CON04_CSM_PROD.PRODUCTDataTable;
        drProduct = dtProduct.NewPRODUCTRow();

        //異動的欄位

        ////商品料號
        drProduct["PRODNO"] = txtProductCode.Text.ToString();
        //商品類別
        drProduct["PRODTYPENO"] = ddlProductCategory.SelectedItem.Value.ToString();
        //商口名稱
        drProduct["PRODNAME"] = txtProductName.Text.ToString();
        //單位
        drProduct["UNIT"] = txtUnit.Text.ToString();
        //會計科目
        drProduct["ACCOUNTCODE"] = txtAcct1.Text.ToString().ToString() + txtAcct2.Text.ToString() + txtAcct3.Text.ToString() + txtAcct4.Text.ToString() + txtAcct5.Text.ToString() + txtAcct6.Text.ToString();
        //更新人員
        drProduct["MODI_USER"] = logMsg.MODI_USER;
        //更新時間
        drProduct["MODI_DTM"] = Advtek.Utility.DateUtil.NullDateFormat(lblDate.Text.ToString());
        //廠商ID
        Supplier_Facade Supplier_Facade = new Supplier_Facade();
        string SuppId = Supplier_Facade.GetSuppId(txtSupplierNo.Text.ToString());

        drProduct["SUPP_ID"] = SuppId;
        //上架日期
        drProduct["S_DATE"] = Advtek.Utility.DateUtil.NullDateFormat(SupportDateRangeFrom.Text.ToString());
        //下架日期
        string strEDate = SupportDateRangeTo.Text.ToString();
        string strCeaseDate = txtCeaseDate.Text.Replace("/", "");
        if (!string.IsNullOrEmpty(strEDate) || strEDate != " ")
        {
            drProduct["E_DATE"] = Advtek.Utility.DateUtil.NullDateFormat(SupportDateRangeTo.Text.ToString()); ;
        }

        if (!string.IsNullOrEmpty(strCeaseDate) || strCeaseDate != " ")
        {
            //失效日期
            drProduct["CEASEDATE"] = strCeaseDate;
        }


        dtProduct.Rows.Add(drProduct);

        return dtProduct;
    }

    /// <summary>
    /// 組UPDATE CSM_PROD_COMMISSION DATATABLE
    /// </summary>
    /// <returns></returns>
    private CON04_CSM_PROD.CSM_PROD_COMMISSIONDataTable doUpdateCsmProdCommission()
    {
        CON04_CSM_PROD.CSM_PROD_COMMISSIONDataTable dtCsmProdCommission = null;
        DataTable dtProd = (DataTable)Session["gvMaster"];

        dtCsmProdCommission = new CON04_CSM_PROD.CSM_PROD_COMMISSIONDataTable();
        CON04_CSM_PROD.CSM_PROD_COMMISSIONRow drCsmProdCommission;
        Supplier_Facade Supplier_Facade = new Supplier_Facade();
        string SuppId = Supplier_Facade.GetSuppId(txtSupplierNo.Text.ToString());
        if (dtProd.Rows.Count > 0)
        {
            int i = 1;
            foreach (DataRow dr in dtProd.Rows)
            {

                drCsmProdCommission = dtCsmProdCommission.NewCSM_PROD_COMMISSIONRow();
                if (dr["CPC_ID"].ToString() != "")
                {
                    drCsmProdCommission["CPC_ID"] = dr["CPC_ID"];
                }
                else
                {
                    drCsmProdCommission["CPC_ID"] = "新增";
                    drCsmProdCommission["CREATE_USER"] = logMsg.MODI_USER;
                    drCsmProdCommission["CREATE_DTM"] = Convert.ToDateTime(lblDate.Text.ToString());
                }

                drCsmProdCommission["SEQNO"] = i;
                drCsmProdCommission["PRODNO"] = txtProductCode.Text.ToString();
                drCsmProdCommission["SUPP_ID"] = SuppId;
                drCsmProdCommission["COMMISSION"] = dr["COMMISSION"];
                drCsmProdCommission["S_DATE"] = dr["S_DATE"];
                drCsmProdCommission["E_DATE"] = dr["E_DATE"];
                drCsmProdCommission["MODI_USER"] = logMsg.MODI_USER;
                drCsmProdCommission["MODI_DTM"] = Convert.ToDateTime(lblDate.Text.ToString());
                dtCsmProdCommission.Rows.Add(drCsmProdCommission);
                i++;
            }
        }

        Session["gvMaster"] = dtCsmProdCommission;
        return dtCsmProdCommission;
    }

    /// <summary>
    /// 組DELETE CSM_PROD_COMMISSION DATATABLE
    /// </summary>
    /// <returns></returns>
    private CON04_CSM_PROD.CSM_PROD_COMMISSIONDataTable doDeleteCsmProdCommission()
    {
        CON04_CSM_PROD.CSM_PROD_COMMISSIONDataTable dtCsmProdCommission = null;
        DataTable dtProd = (DataTable)Session["gvMaster_Delete"];

        dtCsmProdCommission = new CON04_CSM_PROD.CSM_PROD_COMMISSIONDataTable();
        CON04_CSM_PROD.CSM_PROD_COMMISSIONRow drCsmProdCommission;
        Supplier_Facade Supplier_Facade = new Supplier_Facade();
        string SuppId = Supplier_Facade.GetSuppId(txtSupplierNo.Text.ToString());
        if (dtProd.Rows.Count > 0)
        {
            int i = 1;
            foreach (DataRow dr in dtProd.Rows)
            {

                drCsmProdCommission = dtCsmProdCommission.NewCSM_PROD_COMMISSIONRow();
                drCsmProdCommission["CPC_ID"] = dr["CPC_ID"];
                drCsmProdCommission["SEQNO"] = i;
                drCsmProdCommission["PRODNO"] = txtProductCode.Text.ToString();
                drCsmProdCommission["SUPP_ID"] = SuppId;
                drCsmProdCommission["COMMISSION"] = dr["COMMISSION"];
                drCsmProdCommission["S_DATE"] = dr["S_DATE"];
                drCsmProdCommission["E_DATE"] = dr["E_DATE"];
                drCsmProdCommission["MODI_USER"] = logMsg.MODI_USER;
                drCsmProdCommission["MODI_DTM"] = Convert.ToDateTime(lblDate.Text.ToString());
                dtCsmProdCommission.Rows.Add(drCsmProdCommission);
                i++;
            }
        }

        Session["gvMaster_Delete"] = dtCsmProdCommission;
        return dtCsmProdCommission;
    }

    #endregion

    #region ajax 呼叫client js網頁的方式
    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getSupplierId(string strSupplierNo)
    {
        //廠商ID
        string SuppId = new Supplier_Facade().GetSuppId(strSupplierNo);


        return SuppId;
    }
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getSupplierAccountCode(string strSupplierId)
    {

        return new CON04_Facade().QueryCsmAccountCode(strSupplierId);
    }
    #endregion


    //前端回傳處理後端資料
    protected void ac1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {

        // 繫結空的資料表，以顯示表頭欄位
        DataTable dtGvMaster;
        dtGvMaster = new CON04_Facade().QueryCsmProdCommission(this.txtSupplierNo.Text, this.txtProductCode.Text);
        Session["gvMaster"] = dtGvMaster;
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
        gvMaster.Selection.UnselectAll();
        gvMaster.CancelEdit();
    }

    protected void importButton_Click(object sender, EventArgs e)
    {
        //lp.Controls.Clear();
    }
    protected void gvMaster_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        e.Properties["cpallRowsCount"] = gvMaster.VisibleRowCount;
    }
}



