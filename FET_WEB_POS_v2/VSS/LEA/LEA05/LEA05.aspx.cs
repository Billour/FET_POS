using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using Advtek.Utility;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class VSS_LEA_LEA05 : BasePage
{
    string _TYPE
    {
        get
        {
            return Request.QueryString["type"] == null ? "" : StringUtil.CStr(Request.QueryString["type"]).Trim();
        }
    }

    string _METH
    {
        get
        {
            return Request.QueryString["meth"] == null ? "" : StringUtil.CStr(Request.QueryString["meth"]).Trim();
        }
    }

    string _IMEI
    {
        get
        {
            return Request.QueryString["imei"] == null ? "" : StringUtil.CStr(Request.QueryString["imei"]).Trim();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //上一頁網址
            Session["Up_Url"] = Request.ServerVariables["http_referer"];

            txtIS_IND_FLAG.ClientEnabled = false;
            txtREAL_RECEV_DATE.ClientEnabled = false;
            txtREAL_RETURN_DTM.ClientEnabled = false;
            gvList.ClientVisible = false;
            btnOK.ClientVisible = false;
            lblBOOKING_DATE.Text = DateTime.Now.ToString("yyyy/MM/dd"); //預約日期
            lblSTUTS.Text = "00-未存檔";

            if (_IMEI != "")
            {
                DataTable Ldt = new LEA05_Facade().Get_LEASE_STOCK(_IMEI);
                DataTable Rdt = new DataTable();
                if (Ldt.Rows.Count > 0)
                {

                    lblDEVICE_TYPE.Text = StringUtil.CStr(Ldt.Rows[0]["DEVICE_TYPE_NAME"]);
                    lblIMEI.Text = StringUtil.CStr(Ldt.Rows[0]["IMEI"]);
                    lblIMEI.ToolTip = StringUtil.CStr(Ldt.Rows[0]["LEASE_ID"]);
                    lblSTORE_NO.Text = StringUtil.CStr(Ldt.Rows[0]["STORE_NAME"]);
                    lblSTORE_NO.ToolTip = StringUtil.CStr(Ldt.Rows[0]["STORE_NO"]);
                    lblEARNEST_AMT1.Text = StringUtil.CStr(Ldt.Rows[0]["EARNEST_MONEY"]);
                    lblRENT_AMT1.Text = StringUtil.CStr(Ldt.Rows[0]["DAILY_RENT_PRICE"]);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('查無些IMEI');", true);
                    btnSave.Visible = false;
                    btnReserCancel.Visible = false;
                    btnCheck.Visible = false;
                    btnReturn.Visible = false;
                    btnCancel.Visible = true;
                    return;
                }
                txtUpdateTime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                this.lblMOUSER.Text = logMsg.OPERATOR + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER);
                switch (_METH)
                {
                    case "預約":

                        lblBOOKING_DATE.Text = DateTime.Now.ToString("yyyy/MM/dd"); //預約日期
                        lblSTUTS.Text = "00-未存檔";
                        if (StringUtil.CStr(Ldt.Rows[0]["SSTATUS"]) != "10")
                        {
                            btnSave.Visible = false;
                            btnReserCancel.Visible = false;
                            btnCheck.Visible = false;
                            btnReturn.Visible = false;
                            btnCancel.Visible = false;
                        }
                        else
                        {
                            btnSave.Visible = true;
                            btnReserCancel.Visible = false;
                            btnCheck.Visible = false;
                            btnReturn.Visible = false;
                            btnCancel.Visible = true;
                        }
                        break;
                    case "新增":

                        if (StringUtil.CStr(Ldt.Rows[0]["SSTATUS"]) != "10" && StringUtil.CStr(Ldt.Rows[0]["SSTATUS"]) != "20")
                        {
                            btnSave.Visible = false;
                            btnReserCancel.Visible = false;
                            btnCheck.Visible = false;
                            btnReturn.Visible = false;
                            btnCancel.Visible = false;

                        }
                        else
                        {
                            btnSave.Visible = false;
                            btnReserCancel.Visible = false;
                            btnCheck.Visible = true;
                            btnReturn.Visible = false;
                            btnCancel.Visible = true;
                            txtREAL_RECEV_DATE.ClientEnabled = true;
                        }

                        Rdt = new LEA05_Facade().GetRENT_M(_IMEI, "10");
                        break;
                    case "已租賃":
                        switch (new LEA05_Facade().GetRENT_STATUS(_IMEI))
                        {
                            case "20":
                                btnSave.Visible = true;
                                btnReserCancel.Visible = true;
                                btnCheck.Visible = true;
                                btnReturn.Visible = false;
                                btnCancel.Visible = true;
                                txtREAL_RECEV_DATE.ClientEnabled = true;
                                Rdt = new LEA05_Facade().GetRENT_M(_IMEI, "10");
                                break;
                            case "30":
                            case "40":
                                btnSave.Visible = false;
                                btnReserCancel.Visible = false;
                                btnCheck.Visible = false;
                                btnReturn.Visible = true;
                                btnCancel.Visible = true;
                                txtREAL_RETURN_DTM.ClientEnabled = true;
                                Rdt = new LEA05_Facade().GetRENT_M(_IMEI, "20");
                                break;
                            default:
                                btnSave.Visible = false;
                                btnReserCancel.Visible = false;
                                btnCheck.Visible = false;
                                btnReturn.Visible = false;
                                btnCancel.Visible = false;
                                break;
                        }
                        break;
                    default:
                        btnSave.Visible = false;
                        btnReserCancel.Visible = false;
                        btnCheck.Visible = false;
                        btnReturn.Visible = false;
                        btnCancel.Visible = false;
                        break;
                }
                if (Rdt != null)
                {
                    ViewState["Rdt"] = Rdt;
                    if (Rdt.Rows.Count > 0)
                    {
                        lblRENT_SHEET_NO.Text = StringUtil.CStr(Rdt.Rows[0]["RENT_SHEET_NO"]);
                        lblBOOKING_DATE.Text = StringUtil.CStr(Rdt.Rows[0]["BOOKING_DATE"]);
                        lblSTUTS.Text = StringUtil.CStr(Rdt.Rows[0]["RSTATUS"]) + "-" + StringUtil.CStr(Rdt.Rows[0]["RSTATUS_NAME"]);

                        lblRENT_SHEET_NO.Text = StringUtil.CStr(Rdt.Rows[0]["RENT_SHEET_NO"]);
                        txtMSISDN.Text = StringUtil.CStr(Rdt.Rows[0]["MSISDN"]);
                        txtCUST_NAME.Text = StringUtil.CStr(Rdt.Rows[0]["CUST_NAME"]);
                        txtCUST_LEVEL.Text = StringUtil.CStr(Rdt.Rows[0]["CUST_LEVEL"]);
                        //txtSEX.Value = StringUtil.CStr(Rdt.Rows[0]["SEX"]);
                        txtPRE_S_DATE.Text = StringUtil.CStr(Rdt.Rows[0]["PRE_S_DATE"]);
                        txtPRE_E_DATE.Text = StringUtil.CStr(Rdt.Rows[0]["PRE_E_DATE"]);

                        txtRECEIVE_TYPE.Value = StringUtil.CStr(Rdt.Rows[0]["RECEIVE_TYPE"]);
                        //txtCUST_ADDR.Text = StringUtil.CStr(Rdt.Rows[0]["CUST_ADDR"]);
                        //txtDEPARTURE_DTM.Text = StringUtil.CStr(Rdt.Rows[0]["DEPARTURE_DTM"]);
                        //txtARRIVAL_DTM.Text = StringUtil.CStr(Rdt.Rows[0]["REAL_RECEV_DATE"]);
                        txtREAL_RECEV_DATE.Text = StringUtil.CStr(Rdt.Rows[0]["REAL_RECEV_DATE"]);
                        lblEARNEST_AMT.Text = StringUtil.CStr(Rdt.Rows[0]["EARNEST_AMT"]);

                        txtREAL_RETURN_DTM.Text = StringUtil.CStr(Rdt.Rows[0]["REAL_RETURN_DTM"]);
                        lblRENT_AMT.Text = StringUtil.CStr(Rdt.Rows[0]["RENT_AMT"]);
                        txtIS_IND_FLAG.Value = StringUtil.CStr(Rdt.Rows[0]["IS_IND_FLAG"]);
                        lblIND_AMT.Text = StringUtil.CStr(Rdt.Rows[0]["IND_AMT"]);
                    }
                }

                bindDetailData();
            }
            else
            {
                btnSave.Visible = false;
                btnReserCancel.Visible = false;
                btnCheck.Visible = false;
                btnReturn.Visible = false;
                btnCancel.Visible = false;
            }
            //showList.Visible = false;
        }
        if (txtREAL_RETURN_DTM.Text != null && txtREAL_RETURN_DTM.Text != "")
        {
            txtIS_IND_FLAG.ClientEnabled = true;
            if (txtIS_IND_FLAG.Value != null)
            {
                if (StringUtil.CStr(txtIS_IND_FLAG.Value) == "1")
                {
                    gvList.ClientVisible = true;
                    btnOK.ClientVisible = true;
                }
            }
        }
    }

    private void bindDetailData()
    {
        DataTable dtResult = new DataTable();
        dtResult = new LEA05_Facade().GetIND_ITEM_NAME(lblIMEI.Text);
        ViewState["gvList"] = dtResult;
        gvList.DataSource = dtResult;
        gvList.DataBind();
    }

    private DataTable REAL_IDEMNIFY_ITEMS()
    {
        DataTable REAL_IDEMNIFY_ITEMS = new DataTable();
        if (lblIND_AMT1.Text != null && lblIND_AMT1.Text != "")
        {
            REAL_IDEMNIFY_ITEMS.TableName = "REAL_IDEMNIFY_ITEMS";
            REAL_IDEMNIFY_ITEMS.Columns.Add("REAL_IDEMNIFY_ITEMS_ID", typeof(string)); //REAL_IDEMNIFY_ITEMS_ID      
            REAL_IDEMNIFY_ITEMS.Columns.Add("RENT_INDEMNIFY_ITEMS", typeof(string)); //RENT_INDEMNIFY_ITEMS        
            REAL_IDEMNIFY_ITEMS.Columns.Add("RENT_SHEET_NO", typeof(string)); //RENT_SHEET_NO               
            REAL_IDEMNIFY_ITEMS.Columns.Add("IND_QTY", typeof(int)); //IND_QTY                     
            REAL_IDEMNIFY_ITEMS.Columns.Add("IND_AMOUNT", typeof(int)); //IND_AMOUNT                  
            REAL_IDEMNIFY_ITEMS.Columns.Add("IND_UNIT_PRICE", typeof(int)); //IND_UNIT_PRICE              
            REAL_IDEMNIFY_ITEMS.Columns.Add("CREATE_DTM", typeof(DateTime)); //CREATE_DTM                  
            REAL_IDEMNIFY_ITEMS.Columns.Add("CREATE_USER", typeof(string)); //CREATE_USER                 
            REAL_IDEMNIFY_ITEMS.Columns.Add("MODI_DTM", typeof(DateTime)); //MODI_DTM                    
            REAL_IDEMNIFY_ITEMS.Columns.Add("MODI_USER", typeof(string)); //MODI_USER       
            string[] valArray = lblIND_AMT1.Text.Split('#');

            for (int i = 1; i < valArray.Length; i++)
            {
                string[] valArray1 = valArray[i - 1].Split('^');
                DataRow REAL_IDEMNIFY_ITEMS_NewRow = REAL_IDEMNIFY_ITEMS.NewRow();
                REAL_IDEMNIFY_ITEMS_NewRow["REAL_IDEMNIFY_ITEMS_ID"] = GuidNo.getUUID();
                REAL_IDEMNIFY_ITEMS_NewRow["RENT_INDEMNIFY_ITEMS"] = valArray1[0];
                REAL_IDEMNIFY_ITEMS_NewRow["RENT_SHEET_NO"] = lblRENT_SHEET_NO.Text;
                REAL_IDEMNIFY_ITEMS_NewRow["IND_QTY"] = 1;
                REAL_IDEMNIFY_ITEMS_NewRow["IND_AMOUNT"] = int.Parse(valArray1[1]);
                REAL_IDEMNIFY_ITEMS_NewRow["IND_UNIT_PRICE"] = int.Parse(valArray1[1]);
                REAL_IDEMNIFY_ITEMS_NewRow["CREATE_DTM"] = StringUtil.CStr(this.logMsg.MODI_DTM).Trim();
                REAL_IDEMNIFY_ITEMS_NewRow["CREATE_USER"] = StringUtil.CStr(this.logMsg.MODI_USER).Trim();
                REAL_IDEMNIFY_ITEMS_NewRow["MODI_DTM"] = StringUtil.CStr(this.logMsg.MODI_DTM).Trim();
                REAL_IDEMNIFY_ITEMS_NewRow["MODI_USER"] = StringUtil.CStr(this.logMsg.MODI_USER).Trim();
                REAL_IDEMNIFY_ITEMS.Rows.Add(REAL_IDEMNIFY_ITEMS_NewRow);
            }
        }
        return REAL_IDEMNIFY_ITEMS;
    }

    private DataTable GetRENT_MData()
    {
        DataTable RENT_M = new DataTable();
        RENT_M.TableName = "RENT_M";
        RENT_M.Columns.Add("RENT_SHEET_NO", typeof(string)); //RENT_SHEET_NO              
        RENT_M.Columns.Add("IMEI", typeof(string)); //IMEI                                
        RENT_M.Columns.Add("BOOKING_DATE", typeof(DateTime)); //BOOKING_DATE              
        RENT_M.Columns.Add("MSISDN", typeof(string)); //MSISDN                                 
        RENT_M.Columns.Add("CUST_NAME", typeof(string)); //CUST_NAME                      
        RENT_M.Columns.Add("CUST_LEVEL", typeof(string)); //CUST_LEVEL                    
        //RENT_M.Columns.Add("SEX", typeof(string)); //SEX                                  
        RENT_M.Columns.Add("PRE_S_DATE", typeof(DateTime)); //PRE_S_DATE                  
        RENT_M.Columns.Add("PRE_E_DATE", typeof(DateTime)); //PRE_E_DATE                  
        RENT_M.Columns.Add("RECEIVE_TYPE", typeof(string)); //RECEIVE_TYPE                
        //RENT_M.Columns.Add("CUST_ADDR", typeof(string)); //CUST_ADDR                      
        //RENT_M.Columns.Add("DEPARTURE_DTM", typeof(DateTime)); //DEPARTURE_DTM            
        //RENT_M.Columns.Add("ARRIVAL_DTM", typeof(DateTime)); //ARRIVAL_DTM                
        RENT_M.Columns.Add("REAL_RECEV_DATE", typeof(DateTime)); //REAL_RECEV_DATE        
        RENT_M.Columns.Add("REAL_RETURN_DTM", typeof(DateTime)); //REAL_RETURN_DTM        
        RENT_M.Columns.Add("IS_IND_FLAG", typeof(string)); //IS_IND_FLAG                  
        RENT_M.Columns.Add("EARNEST_AMT", typeof(string)); //EARNEST_AMT                  
        RENT_M.Columns.Add("RENT_AMT", typeof(string)); //RENT_AMT                        
        RENT_M.Columns.Add("IND_AMT", typeof(string)); //IND_AMT                          
        RENT_M.Columns.Add("MODI_USER", typeof(string)); //MODI_USER                      
        RENT_M.Columns.Add("MODI_DTM", typeof(DateTime)); //MODI_DTM                      
        RENT_M.Columns.Add("LEASE_ID", typeof(string)); //LEASE_ID                        
        RENT_M.Columns.Add("RENT_STATUS", typeof(string)); //RENT_STATUS                  

        DataRow RENT_M_NewRow = RENT_M.NewRow();
        RENT_M_NewRow["RENT_SHEET_NO"] = lblRENT_SHEET_NO.Text;
        RENT_M_NewRow["IMEI"] = lblIMEI.Text;
        RENT_M_NewRow["BOOKING_DATE"] = lblBOOKING_DATE.Text;
        RENT_M_NewRow["MSISDN"] = txtMSISDN.Text;
        RENT_M_NewRow["CUST_NAME"] = txtCUST_NAME.Text;
        RENT_M_NewRow["CUST_LEVEL"] = txtCUST_LEVEL.Text;
        //RENT_M_NewRow["SEX"] = txtSEX.Value ;
        if (!string.IsNullOrEmpty(txtPRE_S_DATE.Text))
        {
            RENT_M_NewRow["PRE_S_DATE"] = Convert.ToDateTime(txtPRE_S_DATE.Text);
        }
        else
        {
            RENT_M_NewRow["PRE_S_DATE"] = DBNull.Value;
        }

        if (!string.IsNullOrEmpty(txtPRE_E_DATE.Text))
        {
            RENT_M_NewRow["PRE_E_DATE"] = Convert.ToDateTime(txtPRE_E_DATE.Text);
        }
        else
        {
            RENT_M_NewRow["PRE_E_DATE"] = DBNull.Value;
        }
        RENT_M_NewRow["RECEIVE_TYPE"] = txtRECEIVE_TYPE.Value;
        //RENT_M_NewRow["CUST_ADDR"] = txtCUST_ADDR.Text;
        //RENT_M_NewRow["DEPARTURE_DTM"] = txtDEPARTURE_DTM.Text;
        //RENT_M_NewRow["ARRIVAL_DTM"] = txtARRIVAL_DTM.Text;
        if (!string.IsNullOrEmpty(txtREAL_RECEV_DATE.Text))
        {
            RENT_M_NewRow["REAL_RECEV_DATE"] = Convert.ToDateTime(txtREAL_RECEV_DATE.Text);
        }
        else
        {
            RENT_M_NewRow["REAL_RECEV_DATE"] = DBNull.Value;
        }
        if (!string.IsNullOrEmpty(txtREAL_RETURN_DTM.Text))
        {
            RENT_M_NewRow["REAL_RETURN_DTM"] = Convert.ToDateTime(txtREAL_RETURN_DTM.Text);
        }
        else
        {
            RENT_M_NewRow["REAL_RETURN_DTM"] = DBNull.Value;
        }
        RENT_M_NewRow["IS_IND_FLAG"] = txtIS_IND_FLAG.Value;
        RENT_M_NewRow["EARNEST_AMT"] = lblEARNEST_AMT.Text;
        RENT_M_NewRow["RENT_AMT"] = lblRENT_AMT.Text;
        RENT_M_NewRow["IND_AMT"] = lblIND_AMT.Text;
        RENT_M_NewRow["MODI_USER"] = StringUtil.CStr(this.logMsg.MODI_USER).Trim();
        RENT_M_NewRow["MODI_DTM"] = StringUtil.CStr(this.logMsg.MODI_DTM).Trim();
        RENT_M_NewRow["LEASE_ID"] = lblIMEI.ToolTip;
        RENT_M_NewRow["RENT_STATUS"] = "10";

        RENT_M.Rows.Add(RENT_M_NewRow);

        return RENT_M;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        this.lblIND_AMT.Visible = true;
        int AMT = 0;
        if (ViewState["gvList"] != null)
        {
            DataTable dt = ViewState["gvList"] as DataTable;
            List<object> keyValues = this.gvList.GetSelectedFieldValues(gvList.KeyFieldName);
            if (keyValues.Count > 0)
            {
                DataTable REAL_IDEMNIFY_ITEMS = new DataTable();
                REAL_IDEMNIFY_ITEMS.TableName = "REAL_IDEMNIFY_ITEMS";
                REAL_IDEMNIFY_ITEMS.Columns.Add("REAL_IDEMNIFY_ITEMS_ID", typeof(string)); //REAL_IDEMNIFY_ITEMS_ID      
                REAL_IDEMNIFY_ITEMS.Columns.Add("RENT_INDEMNIFY_ITEMS", typeof(string)); //RENT_INDEMNIFY_ITEMS        
                REAL_IDEMNIFY_ITEMS.Columns.Add("RENT_SHEET_NO", typeof(string)); //RENT_SHEET_NO               
                REAL_IDEMNIFY_ITEMS.Columns.Add("IND_QTY", typeof(int)); //IND_QTY                     
                REAL_IDEMNIFY_ITEMS.Columns.Add("IND_AMOUNT", typeof(int)); //IND_AMOUNT                  
                REAL_IDEMNIFY_ITEMS.Columns.Add("IND_UNIT_PRICE", typeof(int)); //IND_UNIT_PRICE              
                REAL_IDEMNIFY_ITEMS.Columns.Add("CREATE_DTM", typeof(DateTime)); //CREATE_DTM                  
                REAL_IDEMNIFY_ITEMS.Columns.Add("CREATE_USER", typeof(string)); //CREATE_USER                 
                REAL_IDEMNIFY_ITEMS.Columns.Add("MODI_DTM", typeof(DateTime)); //MODI_DTM                    
                REAL_IDEMNIFY_ITEMS.Columns.Add("MODI_USER", typeof(string)); //MODI_USER       
                foreach (string skey in keyValues)
                {
                    DataRow REAL_IDEMNIFY_ITEMS_NewRow = REAL_IDEMNIFY_ITEMS.NewRow();
                    REAL_IDEMNIFY_ITEMS_NewRow["REAL_IDEMNIFY_ITEMS_ID"] = GuidNo.getUUID();
                    REAL_IDEMNIFY_ITEMS_NewRow["RENT_INDEMNIFY_ITEMS"] = skey;
                    REAL_IDEMNIFY_ITEMS_NewRow["RENT_SHEET_NO"] = lblRENT_SHEET_NO.Text;
                    REAL_IDEMNIFY_ITEMS_NewRow["IND_QTY"] = 1;
                    DataRow[] dr = dt.Select("IND_ITEM_NAME='" + skey + "'");
                    if (dr.Length > 0)
                    {
                        REAL_IDEMNIFY_ITEMS_NewRow["IND_AMOUNT"] = int.Parse(StringUtil.CStr(dr[0]["IND_UNIT_PRICE"]));
                        REAL_IDEMNIFY_ITEMS_NewRow["IND_UNIT_PRICE"] = int.Parse(StringUtil.CStr(dr[0]["IND_UNIT_PRICE"]));
                        AMT = AMT + int.Parse(StringUtil.CStr(dr[0]["IND_UNIT_PRICE"]));
                    }
                    REAL_IDEMNIFY_ITEMS_NewRow["CREATE_DTM"] = StringUtil.CStr(this.logMsg.MODI_DTM).Trim();
                    REAL_IDEMNIFY_ITEMS_NewRow["CREATE_USER"] = StringUtil.CStr(this.logMsg.MODI_USER).Trim();
                    REAL_IDEMNIFY_ITEMS_NewRow["MODI_DTM"] = StringUtil.CStr(this.logMsg.MODI_DTM).Trim();
                    REAL_IDEMNIFY_ITEMS_NewRow["MODI_USER"] = StringUtil.CStr(this.logMsg.MODI_USER).Trim();
                    REAL_IDEMNIFY_ITEMS.Rows.Add(REAL_IDEMNIFY_ITEMS_NewRow);
                }
                Session["REAL_IDEMNIFY_ITEMS"] = REAL_IDEMNIFY_ITEMS;
            }
        }

        this.lblIND_AMT.Text = StringUtil.CStr(AMT);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (lblSTUTS.Text == "00-未存檔")
        {
            lblRENT_SHEET_NO.Text = "L" + SerialNo.GenNo("L{0}").Replace("L{0}", lblSTORE_NO.ToolTip); //"L{0}";   
            int intResult = new LEA05_Facade().SaveData(GetRENT_MData(), "N", REAL_IDEMNIFY_ITEMS());
            btnSave.Visible = true;
            btnReserCancel.Visible = true;
            btnCheck.Visible = false;
            btnReturn.Visible = false;
            btnCancel.Visible = false;
            lblSTUTS.Text = "10-已預約";
        }
        else
        {
            int intResult = new LEA05_Facade().SaveData(GetRENT_MData(), "U", REAL_IDEMNIFY_ITEMS());
        }
        GetModiRecord();
    }

    protected void btnReserCancel_Click(object sender, EventArgs e)
    {
        int intResult = new LEA05_Facade().SaveData(GetRENT_MData(), "C", REAL_IDEMNIFY_ITEMS());
        btnSave.Visible = false;
        btnReserCancel.Visible = false;
        btnCheck.Visible = false;
        btnReturn.Visible = false;
        btnCancel.Visible = false;
        Response.Redirect("LEA05.aspx");
    }

    protected void btnCheck_Click(object sender, EventArgs e)
    {
        if (lblSTUTS.Text == "00-未存檔")
        {
            lblRENT_SHEET_NO.Text = "L" + SerialNo.GenNo("L{0}").Replace("L{0}", lblSTORE_NO.ToolTip); //"L{0}";   
            int intResult = new LEA05_Facade().SaveData(GetRENT_MData(), "GN", REAL_IDEMNIFY_ITEMS());
        }
        else
        {
            int intResult = new LEA05_Facade().SaveData(GetRENT_MData(), "GO", REAL_IDEMNIFY_ITEMS());
        }
        //Response.Redirect("~/VSS/SAL/SAL01/SAL01.aspx");
        GetModiRecord();
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        int intResult = new LEA05_Facade().SaveData(GetRENT_MData(), "R", REAL_IDEMNIFY_ITEMS());
        //Response.Redirect("~/VSS/SAL/SAL01/SAL01.aspx");
        GetModiRecord();

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "取消", "document.location.href ='" + Session["Up_Url"] + "';", true);

    }
    /// <summary>
    /// 顯示異動欄位。
    /// </summary>
    /// <returns></returns>
    private void GetModiRecord()
    {
        string strUpdateRecord = string.Empty;
        if (ViewState["Rdt"] != null)
        {
            DataTable Rdt = ViewState["Rdt"] as DataTable;

            if (Rdt.Rows.Count > 0)
            {
                if (txtMSISDN.Text != StringUtil.CStr(Rdt.Rows[0]["MSISDN"]))
                {
                    strUpdateRecord += "客戶門號<br>";
                }
                if (txtCUST_NAME.Text != StringUtil.CStr(Rdt.Rows[0]["CUST_NAME"]))
                {
                    strUpdateRecord += "客戶姓名<br>";
                }
                if (txtCUST_LEVEL.Text != StringUtil.CStr(Rdt.Rows[0]["CUST_LEVEL"]))
                {
                    strUpdateRecord += "客戶等級<br>";
                }
                //txtSEX.Value = StringUtil.CStr(Rdt.Rows[0]["SEX"]);
                string PreSDate = DateTime.Parse(StringUtil.CStr(Rdt.Rows[0]["PRE_S_DATE"])).ToString("yyyy/MM/dd");
                if (txtPRE_S_DATE.Text != PreSDate)
                {
                    strUpdateRecord += "租賃時間(起)<br>";
                }
                string PreEDate = DateTime.Parse(StringUtil.CStr(Rdt.Rows[0]["PRE_E_DATE"])).ToString("yyyy/MM/dd");

                if (txtPRE_E_DATE.Text != PreEDate)
                {
                    strUpdateRecord += "租賃時間(訖)<br>";
                }

                if (Convert.ToInt16(txtRECEIVE_TYPE.SelectedItem.Value) != Convert.ToInt16(StringUtil.CStr(Rdt.Rows[0]["RECEIVE_TYPE"])))
                {
                    strUpdateRecord += "領取方式<br>";
                }
                DateTime dtRecevDate = new DateTime();
                string RealRecevDate = string.Empty;
                if (DateTime.TryParse(StringUtil.CStr(Rdt.Rows[0]["REAL_RECEV_DATE"]), out dtRecevDate))
                {
                    RealRecevDate = dtRecevDate.ToString("yyyy/MM/dd");
                }


                if (txtREAL_RECEV_DATE.Text != RealRecevDate)
                {
                    strUpdateRecord += "實際領取日<br>";
                }
                DateTime dtReturnDate = new DateTime();
                string RealReturnDate = string.Empty;
                if (DateTime.TryParse(StringUtil.CStr(Rdt.Rows[0]["REAL_RETURN_DTM"]), out dtReturnDate))
                {
                    RealReturnDate = dtReturnDate.ToString("yyyy/MM/dd");
                }
               

                if (txtREAL_RETURN_DTM.Text != RealReturnDate)
                {
                    strUpdateRecord += "實際歸還日<br>";
                }
                
            }
            if (!string.IsNullOrEmpty(strUpdateRecord))
            {
                Panel2.Visible = true;
                ModiEmpNo.Text = logMsg.OPERATOR;
                ModiUser.Text = new Employee_Facade().GetEmpName(logMsg.MODI_USER);
                ModiRecord.Text = strUpdateRecord;
                ModiDtm.Text = txtUpdateTime.Text;
            }
        }
        
    }
}
