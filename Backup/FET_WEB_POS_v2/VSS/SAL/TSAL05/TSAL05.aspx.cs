using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using DevExpress.Web.ASPxGridView;
using System.Collections.Specialized;
using System.Data.OracleClient;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.DTO;
using DevExpress.Web.ASPxEditors;
using Advtek.Utility;

public partial class VSS_SAL_TSAL05 : BasePage
{
	OrderedDictionary QryArgs = new OrderedDictionary();
	//string POSUUID_DETAIL = "";

    /// <summary>
    /// 是否由交易補登資料而來, 是："1", 否：""
    /// </summary>
    private string qS
    {
        get
        {
            string s = "";

            //**2011/04/21 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "s")
                    {
                        s = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            return s;
        }
    }



	#region Protected Method : void Page_Load(object sender, EventArgs e)
	protected void Page_Load(object sender, EventArgs e)
	{

		if (!IsPostBack)
		{
			getSALE_PERSON();
			cbSALE_PERSON.Value = this.logMsg.OPERATOR;//操作人員
			//由其他系統來
			//POSUUID_DETAIL = Request.QueryString["POSUUID_DETAIL"] as string;
			//if (!string.IsNullOrEmpty(POSUUID_DETAIL))
			//{
			//    getMasterDataByUUID(POSUUID_DETAIL);
			//}
            if (!string.IsNullOrEmpty(qS))
            {   //UAT_AL_B195, 2011-04-07 從交易補登進入時,交易取消按鈕設為Disable
                btnCancelTran.Enabled = false;
            }

			if (!string.IsNullOrEmpty(this.logMsg.OPERATOR))
			{
				bindMasterData();
			}
		}

		ClientScript.RegisterClientScriptBlock(this.GetType(), "GV_OPERATOR", "var GV_OPERATOR = '" + logMsg.OPERATOR + "';", true);

	}
	#endregion

	#region Private Method : void getSALE_PERSON()
    void getSALE_PERSON()
    {
        DataTable dtSalePerson = new SAL05_Facade().getEmployee(logMsg.STORENO, logMsg.OPERATOR);
        cbSALE_PERSON.DataSource = dtSalePerson;
        cbSALE_PERSON.ValueField = "EMPNO";
        cbSALE_PERSON.TextField = "EMPNAME";
        cbSALE_PERSON.DataBind();
        DataRow[] drs = null;
        if (dtSalePerson != null && dtSalePerson.Rows.Count > 0)
            drs = dtSalePerson.Select(" EMPNO = '" + logMsg.OPERATOR + "'");
        if (drs == null || drs.Length == 0)
        {
            Employee_Facade empFacade = new Employee_Facade();
            string empName = empFacade.GetEmpName(logMsg.MODI_USER);
            cbSALE_PERSON.Items.Insert(1, new DevExpress.Web.ASPxEditors.ListEditItem(empName + "-" + logMsg.OPERATOR, logMsg.OPERATOR));
        }
        cbSALE_PERSON.Items.Insert(0, new ListEditItem("ALL", "ALL"));
        cbSALE_PERSON.SelectedIndex = cbSALE_PERSON.Items.IndexOfValue(logMsg.OPERATOR);
    }
	#endregion

	#region Protected Method : void btnSearch_Click(object sender, EventArgs e)
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		gvMaster.FocusedRowIndex = -1;
		gvMaster.PageIndex = 0;
		bindMasterData();
		divContent.Visible = true;
	}
	#endregion

	#region Protected Method : void bindMasterData()
	protected void bindMasterData()
	{
        DataTable dtMaster = getMasterData();
      
        gvMaster.DataSource = dtMaster;
        gvMaster.DataBind();
	}
	#endregion

	#region Private Method : DataTable getMasterData()
	private DataTable getMasterData()
	{
        SAL05_Facade facade = new SAL05_Facade();
		setQueryParams();
        DataTable dtResult = facade.getTO_CLOSE_HEAD(QryArgs);
        if (dtResult != null && dtResult.Rows.Count > 0)
        {
            DataRow[] drs = dtResult.Select(" SERVICE_TYPE = 4 ");
            if (drs != null && drs.Length > 0)
            {
                foreach (DataRow dr in drs)
                {
                    if (StringUtil.CStr(dr["FUN_ID"]) == "12")
                        dr["SERVICE_TYPENAME"] = "換卡";
                    else if (StringUtil.CStr(dr["FUN_ID"]) == "13")
                        dr["SERVICE_TYPENAME"] = "換號";
                    else if (StringUtil.CStr(dr["FUN_ID"]) == "150")
                        dr["SERVICE_TYPENAME"] = "一退一租";
                    else if (StringUtil.CStr(dr["FUN_ID"]) == "8")
                        dr["SERVICE_TYPENAME"] = "加值服務-資費高改低";
                    else if (StringUtil.CStr(dr["FUN_ID"]) == "11")
                        dr["SERVICE_TYPENAME"] = "暫時停機";
                    else if (StringUtil.CStr(dr["FUN_ID"]) == "180")
                        dr["SERVICE_TYPENAME"] = "保証金退現及查詢";
                    else if (StringUtil.CStr(dr["FUN_ID"]) == "1")
                        dr["SERVICE_TYPENAME"] = "合約資料-提前解約";
                    else if (StringUtil.CStr(dr["FUN_ID"]) == "121" || StringUtil.CStr(dr["FUN_ID"]) == "123")
                        dr["SERVICE_TYPENAME"] = "合約資料-變更促代";
                    else if (StringUtil.CStr(dr["FUN_ID"]) == "122" || StringUtil.CStr(dr["FUN_ID"]) == "124")
                        dr["SERVICE_TYPENAME"] = "合約資料-取消促代";
                    else if (StringUtil.CStr(dr["FUN_ID"]) == "25")
                        dr["SERVICE_TYPENAME"] = "全球卡換卡";
                    else if (StringUtil.CStr(dr["FUN_ID"]) == "2")
                        dr["SERVICE_TYPENAME"] = "Nonstop-換卡";
                    else if (StringUtil.CStr(dr["FUN_ID"]) == "505")
                        dr["SERVICE_TYPENAME"] = "Prepaid-換卡";
                }
                dtResult.AcceptChanges();
            }
        }
		return dtResult;
	}
	#endregion

	#region Private Method : void getMasterDataByUUID(string Detail_UUID)
	private void getMasterDataByUUID(string Detail_UUID)
	{
		DataTable dtResult = TSAL05_Facade.getTO_CLOSE_HEADByUUID(Detail_UUID, logMsg.STORENO, Convert.ToInt32(cbSTATUS.SelectedItem.Value));
		gvMaster.DataSource = dtResult;
		gvMaster.DataBind();
	}
	#endregion

	#region Protected Method : void gvMaster_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
	protected void gvMaster_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
	{
		gvMaster.SettingsPager.PageSize = int.Parse(e.Parameters);
		gvMaster.DataBind();
	}
	#endregion

	#region Protected Method : void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
	protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
	{
		e.Row.Attributes["canSelect"] = "true";
		if (e.RowType == GridViewRowType.Data)
		{
			string STATUS = gvMaster.GetRowValues(e.VisibleIndex, "STATUS").ToString();
			if (STATUS == "3" || STATUS == "2")
			{
				e.Row.Attributes["canSelect"] = "false";
			}
		}


		DevExpress.Web.ASPxEditors.ASPxButton btnDiscount = e.Row.FindChildControl<DevExpress.Web.ASPxEditors.ASPxButton>("btnDiscount");
		if (btnDiscount != null)
		{
			DataTable dt = TSAL05_Facade.getTO_CLOSE_DISCOUNT(e.KeyValue.ToString(), Convert.ToInt32(cbSTATUS.SelectedItem.Value));
			if (dt.Rows.Count > 0)
				btnDiscount.Enabled = true;
			else
			{
				btnDiscount.Enabled = false;
				DevExpress.Web.ASPxPopupControl.ASPxPopupControl ASPxPopupControl1 = e.Row.FindChildControl<DevExpress.Web.ASPxPopupControl.ASPxPopupControl>("ASPxPopupControl1");
				// ASPxPopupControl1.ContentUrl = "";
				ASPxPopupControl1.Enabled = false;
			}
		}
		/*
		if (e.RowType == GridViewRowType.Detail)
		{
			// 繫結明細資料表           
			ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("detailGrid");
			detailGrid.DataSource = GetDetailData();
			detailGrid.DataBind();            
		}
		*/
	}
	#endregion

	#region Protected Method : void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
	protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
	{
		if (e.RowType == GridViewRowType.Detail)
		{
			// 繫結明細資料表           
			ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("detailGrid");

			detailGrid.DataSource = new SAL05_Facade().getTO_CLOSE_ITEM(e.KeyValue.ToString());
			detailGrid.DataBind();
		}
	}
	#endregion

	#region Portected Method : void gvMaster_PageIndexChanged(object sender, EventArgs e)
	protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
	{
		ASPxGridView grid = sender as ASPxGridView;
		grid.DataSource = getMasterData();
		grid.DataBind();
        //**2011/04/05 Tina：合併結帳要能跨頁選取。
		//grid.Selection.UnselectAll();
	}
	#endregion

	#region Protected Method : void gvMaster_SelectionChanged(object sender, EventArgs e)
	protected void gvMaster_SelectionChanged(object sender, EventArgs e)
	{
		if (gvMaster.Selection.Count > 0)
		{
			List<object> li = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);

			//  DevExpress.Web.Data.WebDescriptorRowBase wdsrb = gvMaster.Selection.

		}
	}
	#endregion

	#region Protected Method : void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
	protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
	{
		string STATUS = gvMaster.GetRowValues(e.VisibleIndex, "STATUS").ToString();
		if (STATUS == "3" || STATUS == "2") //交昜取消資料 和已結帳
		{
			if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox) { e.Enabled = false; }
		}
	}
	#endregion

	#region Protected Method : void detailGrid_PageIndexChanged(object sender, EventArgs e)
	protected void detailGrid_PageIndexChanged(object sender, EventArgs e)
	{
		//  ASPxGridView grid = sender as ASPxGridView;
		//  grid.DataSource = GetDetailData();
		//  grid.DataBind();
	}
	#endregion

	#region Protected Method : void combinedPaymentButton_Click(object sender, EventArgs e)
	protected void combinedPaymentButton_Click(object sender, EventArgs e)
	{
		//鎖住合併結帳按鈕
		((ASPxButton)sender).ClientEnabled = false;
		List<object> li = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
		SAL05_Facade SAL05 = new SAL05_Facade();
		INVENTORY_Facade Inventory = new INVENTORY_Facade();
		string Stock = FET.POS.Model.Common.Common_PageHelper.GetGoodLOCUUID();
		string havePaymentBill = "";
		string haveEStore = "";
        string haveService = "";
		string curServiceType = "";
		string POSUUID_DETAIL = "";
        DataTable dt = new DataTable();
        DataTable dtHead = new DataTable();
        List<string> funList = new List<string>();
        string fun_id = "";
		if (li.Count > 0)
		{
			foreach (string key in li)
			{
				if (POSUUID_DETAIL.IndexOf(key) < 0)
				{
					POSUUID_DETAIL += key + ";";
					dt = SAL05.getTO_CLOSE_ITEM(key);
					dtHead = SAL05.getTO_CLOSE_HEADByUUID(key, logMsg.STORENO);
					if (dtHead != null && dtHead.Rows.Count > 0)
					{
						try
						{
							curServiceType = dtHead.Rows[0]["SERVICE_TYPE"].ToString();
                            //判斷fun_id
                            if (curServiceType == "3")
                            {
                                fun_id = StringUtil.CStr(dtHead.Rows[0]["FUN_ID"]);
                                bool IsETC = (fun_id == "4" || fun_id == "5");

                                bool IsTogether = (IsETC && !funList.Contains("4") && !funList.Contains("5") && funList.Count > 0) || ((funList.Contains("4") || funList.Contains("5")) && !IsETC);
                             
                                if  (IsTogether)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "CheckETC", "alert('ETC不允許合併結帳!');", true);
                                    //恢復合併結帳按鈕
                                    ((ASPxButton)sender).ClientEnabled = true;
                                    return;
                                }
                                else
                                {
                                    funList.Add(fun_id);
                                }
                            }else if(curServiceType == "4"){
                                 fun_id = StringUtil.CStr(dtHead.Rows[0]["FUN_ID"]);
                            }
						}
						catch
						{

						}

						if (havePaymentBill == "")
							if (curServiceType == "3")
								havePaymentBill = "YES";
							else
								havePaymentBill = "NO";
						else
							if ((curServiceType == "3" && havePaymentBill == "NO")
								|| (curServiceType != "3" && havePaymentBill == "YES"))
							{
								ScriptManager.RegisterClientScriptBlock(this, typeof(string), "havePaymentBillData", "alert('帳單代收交易與銷售交易不允許合併結帳!');", true);
								//恢復合併結帳按鈕
								((ASPxButton)sender).ClientEnabled = true;
								return;
							}
						if (haveEStore == "")
							if (curServiceType == "10")
								haveEStore = "YES";
							else
								haveEStore = "NO";
						else
							if ((curServiceType == "10" && haveEStore == "NO")
								|| (curServiceType != "10" && haveEStore == "YES"))
							{
								ScriptManager.RegisterClientScriptBlock(this, typeof(string), "haveEStore", "alert('網購交易與銷售交易不允許合併結帳!');", true);
								//恢復合併結帳按鈕
								((ASPxButton)sender).ClientEnabled = true;
								return;
							}

                        if (haveService == "")
                            if (curServiceType == "4" && (fun_id == "121" || fun_id == "122" || fun_id == "123" || fun_id == "124"))
                                haveService = "YES";
                            else
                                haveService = "NO";
                        else
                            if (haveService == "YES")
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "haveEStore", "alert('變更促代與取消促代不允許合併結帳!');", true);
                                //恢復合併結帳按鈕
                                ((ASPxButton)sender).ClientEnabled = true;
                                return;
                            }
					}

					if (dt != null && dt.Rows.Count > 0)
					{
						foreach (DataRow dr in dt.Rows)
						{
                           



							if (!string.IsNullOrEmpty(dr["AMOUNT"].ToString()))
							{
								try
								{
									int.Parse(dr["AMOUNT"].ToString());
								}
								catch
								{
									ScriptManager.RegisterClientScriptBlock(this, typeof(string), "noPriceProductData", "alert('商品單價不存在,不允許結帳!');", true);
									//恢復合併結帳按鈕
									((ASPxButton)sender).ClientEnabled = true;
									return;
								}
							}
							else
							{
								ScriptManager.RegisterClientScriptBlock(this, typeof(string), "getProductPriceError", "alert('讀取商品單價資料失敗,不允許結帳!');", true);
								//恢復合併結帳按鈕
								((ASPxButton)sender).ClientEnabled = true;
								return;
							}

							try
							{
								if (dr["ISSTOCK"].ToString() == "1")
								{
									int qty = new SAL01_Facade().getINV_ON_HAND_CURRENT(dr["PRODNO"].ToString(), logMsg.STORENO);
									if (qty <= 0)
									{
										ScriptManager.RegisterClientScriptBlock(this, typeof(string), "noINVProductData", "alert('商品無庫存量，該筆交易不允許結帳!');", true);
										//恢復合併結帳按鈕
										((ASPxButton)sender).ClientEnabled = true;
										return;
									}
								}
							}
							catch
							{
								ScriptManager.RegisterClientScriptBlock(this, typeof(string), "getINVDataError", "alert('讀取庫存商品的資料失敗,不允許結帳!');", true);
								//恢復合併結帳按鈕
								((ASPxButton)sender).ClientEnabled = true;
								return;
							}
						}
					}
				}
			}

            //判斷是否為ETC

            //**2011/04/21 Tina：傳遞參數時，要先以加密處理。
			if (qS == "1") //由交易補登資料而來傳回交易補登
			{
				//Response.Redirect("~/VSS/SAL/SAL11/SAL11.aspx?s=1&SRC_TYPE=SAL05&PKEY=" + POSUUID_DETAIL);

                string encryptUrl = Utils.Param_Encrypt("s=1&SRC_TYPE=SAL05&PKEY=" + POSUUID_DETAIL);
                Response.Redirect("~/VSS/SAL/SAL11/SAL11.aspx?Param=" + encryptUrl);
			}
            else if (qS == "2")
            {
                //Response.Redirect("~/VSS/SAL/TSAL11/TSAL11.aspx?s=2&SRC_TYPE=TSAL05&PKEY=" + POSUUID_DETAIL);

                string encryptUrl = Utils.Param_Encrypt("s=2&SRC_TYPE=TSAL05&PKEY=" + POSUUID_DETAIL);
                Response.Redirect("~/VSS/SAL/TSAL11/TSAL11.aspx?Param=" + encryptUrl);
            }
            else
            {
                //判斷
                if (curServiceType == "4")
                {
                    fun_id = StringUtil.CStr(dtHead.Rows[0]["FUN_ID"]);
                    if (fun_id == "121" || fun_id == "123" )
                    {
                        #region CR
                        string posuuid_detail = StringUtil.CStr(dtHead.Rows[0]["POSUUID_DETAIL"]);
                        string posuuid_master = "";
                        TSAL05_Facade.get_possuid_master(posuuid_detail, out posuuid_master);

                        if (!string.IsNullOrEmpty(posuuid_master))
                        {
                            string encryptUrl = Utils.Param_Encrypt("SRC_TYPE=CR&PKEY=" + posuuid_master + "&machine_id=" + this.logMsg.MACHINE_ID + "&UUID=" + posuuid_detail);
                            Response.Redirect("~/VSS/SAL/SAL03/SAL03.aspx?Param=" + encryptUrl, true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "selectTranData", "alert('查無欲變更之交易!');", true);
                        }
                        #endregion
                    }
                    else if (fun_id == "122" || fun_id == "124")
                    {
                        #region CT
                        string posuuid_detail = StringUtil.CStr(dtHead.Rows[0]["POSUUID_DETAIL"]);
                        string posuuid_master = "";
                        TSAL05_Facade.get_possuid_master(posuuid_detail, out posuuid_master);

                        if (!string.IsNullOrEmpty(posuuid_master))
                        {
                            bool result = TSAL05_Facade.CheckOtherDetail(posuuid_detail, posuuid_master);
                            if (result)
                            {
                                //Response.Redirect("~/VSS/SAL/SAL03/SAL03.aspx?SRC_TYPE=CT&PKEY=" + posuuid_master + "&machine_id=" + MACHINE_ID, true);

                                //**2011/04/21 Tina：傳遞參數時，要先以加密處理。
                                string encryptUrl = Utils.Param_Encrypt("SRC_TYPE=CT&PKEY=" + posuuid_master + "&machine_id=" + logMsg.MACHINE_ID + "&UUID=" + posuuid_detail);
                                Response.Redirect("~/VSS/SAL/SAL03/SAL03.aspx?Param=" + encryptUrl, true);
                            }
                            else
                            {
                                //Response.Redirect("~/VSS/SAL/SAL041/SAL04.aspx?SRC_TYPE=CT&PKEY=" + posuuid_master + "&machine_id=" + MACHINE_ID, true);

                                //**2011/04/21 Tina：傳遞參數時，要先以加密處理。
                                string encryptUrl = Utils.Param_Encrypt("SRC_TYPE=CT&PKEY=" + posuuid_master + "&machine_id=" + logMsg.MACHINE_ID );
                                Response.Redirect("~/VSS/SAL/SAL041/SAL04.aspx?Param=" + encryptUrl, true);
                            }
                        }
                        else
                        {
                            //lblmessage.Text = "查無舊交易,posuuid_detail:" + sUUID;
                            string encryptUrl = Utils.Param_Encrypt("SRC_TYPE=TSAL05&PKEY=" + POSUUID_DETAIL);
                            Response.Redirect("~/VSS/SAL/TSAL01/TSAL01.aspx?Param=" + encryptUrl);
                        }
                        #endregion
                    }
                    else
                    {
                        string encryptUrl = Utils.Param_Encrypt("SRC_TYPE=TSAL05&PKEY=" + POSUUID_DETAIL);
                        Response.Redirect("~/VSS/SAL/TSAL01/TSAL01.aspx?Param=" + encryptUrl);
                    }

                }
                else
                {
                    string encryptUrl = Utils.Param_Encrypt("SRC_TYPE=TSAL05&PKEY=" + POSUUID_DETAIL);
                    Response.Redirect("~/VSS/SAL/TSAL01/TSAL01.aspx?Param=" + encryptUrl);
                }
            }
		}
		else ScriptManager.RegisterClientScriptBlock(this, typeof(string), "selectTranData", "alert('請選取未結交易資料!');", true);
		//恢復合併結帳按鈕
		((ASPxButton)sender).ClientEnabled = true;
	}
	#endregion

	#region Private Method : void clearQueryParams()
	private void clearQueryParams()
	{
		txtS_Date.Text = "";
		txtE_Date.Text = "";
		txtMSISDN.Text = "";
		if (cbSALE_PERSON.Items.Count > 0)
			cbSALE_PERSON.SelectedIndex = 0;
		if (cbSERVICE_TYPE.Items.Count > 0)
			cbSERVICE_TYPE.SelectedIndex = 0;
		if (cbSTATUS.Items.Count > 0)
			cbSTATUS.SelectedIndex = 0;
		if (cbSALE_PERSON.SelectedItem != null)
		{
			QryArgs["SALE_PERSON"] = cbSALE_PERSON.SelectedItem.Value;
			cbSALE_PERSON.Value = logMsg.OPERATOR;
		}
	}
	#endregion

	#region Private Method : void setQueryParams()
	private void setQueryParams()
	{
		//取得查詢條件值
		try
		{
			QryArgs["STORE_NO"] = logMsg.STORENO;
			QryArgs["S_DATE"] = txtS_Date.Text;
			QryArgs["E_DATE"] = txtE_Date.Text;
			QryArgs["STATUS"] = cbSTATUS.SelectedItem.Value;
			QryArgs["SERVICE_TYPE"] = cbSERVICE_TYPE.SelectedItem.Value;
			QryArgs["MSISDN"] = txtMSISDN.Text;//客戶門號
			if (cbSALE_PERSON.SelectedItem != null)
				QryArgs["SALE_PERSON"] = cbSALE_PERSON.SelectedItem.Value;
		}
		catch //(Exception e) 
        { }
	}
	#endregion

	#region Protected Method : void btnClear_Click(object sender, EventArgs e)
	protected void btnClear_Click(object sender, EventArgs e)
	{
		clearQueryParams();
		setQueryParams();
	}
	#endregion

	#region Protected Method : void btnCancelTran_Click(object sender, EventArgs e)
	protected void btnCancelTran_Click(object sender, EventArgs e)
	{
        string MSISDN = "";
        List<object> li = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
        //string POSUUID_DETAIL = "";
        SAL05_Facade Facade = new SAL05_Facade();
        SAL01_Facade Facade01 = new SAL01_Facade();
        if (li.Count > 0)
        {
            Facade.updateCancelTrancation(li, logMsg.MODI_USER);
            DataTable dt = Facade.getCancleTO_CLOSE_DATA(li);
            DataRow[] dr2 = dt.Select("SERVICE_TYPE = 5");
            if (dr2.Length > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "CancelTrancation","alert('線上儲值不得取消!');", true);
                return;
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    MSISDN = StringUtil.CStr(dr["MSISDN"]);
                    
                    int ret = Facade01.CancelOuterSystem(StringUtil.CStr(dr["POSUUID_DETAIL"]), StringUtil.CStr(dr["SERVICE_TYPE"]),
                                                     StringUtil.CStr(dr["SERVICE_SYS_ID"]), StringUtil.CStr(dr["BUNDLE_ID"]),
                                                     StringUtil.CStr(dr["STORE_NO"]), StringUtil.CStr(dr["SALE_PERSON"]),
                                                     StringUtil.CStr(dr["BARCODE1"]), StringUtil.CStr(dr["BARCODE2"]),
                                                     StringUtil.CStr(dr["BARCODE3"]), StringUtil.CStr(dr["AMOUNT"]));
                    if (ret == 0)
                    {
                        //取消交易,commit外部系統成功才刪除未結清單中資料
                        StringBuilder posuuid_detailList = new StringBuilder("");
                        posuuid_detailList.Append(OracleDBUtil.SqlStr(StringUtil.CStr(dr["POSUUID_DETAIL"]))).Append(",");
                        Facade01.delTO_CLOSE(posuuid_detailList);
                    }
                    else
                    {
                        Facade01.InsertDataUploadLog(StringUtil.CStr(dr["POSUUID_DETAIL"]));
                    }
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "CancelTrancation", string.Format("alert('您已取消({0})，系統將會自行同步取消或更正該門號於業務園地的狀態!');",MSISDN), true);
            //update回原來的系統 目前未寫
            bindMasterData();
        }
        else ScriptManager.RegisterClientScriptBlock(this, typeof(string), "selectTranData", "alert('請選取未結交易資料!');", true);
	}
	#endregion

    public string TransferURL(string strValue)
    {
        //**2011/04/27 Tina：傳遞參數時，要先以加密處理。
        string encryptUrl = Utils.Param_Encrypt(strValue);
        return string.Format("Param={0}", encryptUrl);
    }


}
