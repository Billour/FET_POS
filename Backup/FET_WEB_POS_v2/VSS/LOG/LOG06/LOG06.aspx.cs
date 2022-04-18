using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using System.Data;

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;

public partial class VSS_LOG_LOG06 : BasePage
{ 
    //private string desKey = "AAAAAAAA";
    //private string desIv = "AAAAAAAA";
    private string desKey = StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["ConnStrKey"]);
    private string desIv = StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["ConnStrIV"]);

    protected void Page_Load(object sender, EventArgs e)
    {
        txtSDate.MinDate = DateTime.Today;
        txtEDate.MinDate = DateTime.Today;

        if (!IsPostBack)
        {
            //if (logMsg.ROLE_TYPE == "1")
            //{
                string storeNo = logMsg.STORENO;// Request.QueryString["storeId"];
                string EmployeeNo = logMsg.OPERATOR;// Request.QueryString["EmployeeId"];
                string MachineID = logMsg.MACHINE_ID;// Request.QueryString["machine_id"];
                bindMasterData();
            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('本功能只允門市店長進入!!');", true);
            //    btnCommit.Enabled = false;
            //    btnCancel.Enabled = false;
            //}
        }
    }

    private void AddData(string EmployeeNo)
    {
        try
        {
            LOG06_StoreHeaderDiscountPWD _LOG06_SysPara_DTO = new LOG06_StoreHeaderDiscountPWD();
            LOG06_StoreHeaderDiscountPWD.STORE_HEADER_DISCOUNT_PWDDataTable dtSYS;
            LOG06_StoreHeaderDiscountPWD.STORE_HEADER_DISCOUNT_PWDRow drSYS;

            dtSYS = _LOG06_SysPara_DTO.Tables["STORE_HEADER_DISCOUNT_PWD"] as LOG06_StoreHeaderDiscountPWD.STORE_HEADER_DISCOUNT_PWDDataTable;

            drSYS = dtSYS.NewSTORE_HEADER_DISCOUNT_PWDRow();

            string SID = GuidNo.getUUID();
            string Enpw = DESCryptography.EncryptDES(pw.Text, desKey, desIv);
            DateTime Renddate = new DateTime();
            if (txtEDate.Text == "")
            {
                Renddate = Advtek.Utility.DateUtil.NullDateFormat(null);
            }
            else
            {
                Renddate = System.DateTime.Parse(txtEDate.Text);
            }

            drSYS.ItemArray = new object[] {
                    SID,
                    System.DateTime.Parse(txtSDate.Text) ,     //Value_Start_Date
                    Renddate,
                    Enpw,    //Password
                    "1",//Status
                    logMsg.CREATE_USER,
                    Convert.ToDateTime(DateTime.Now), //CREATE_DTM                                
                    logMsg.MODI_USER,
                    Convert.ToDateTime(DateTime.Now), //MODI_DTM
                    EmployeeNo,//EMPNO
                    EmployeeNo
                };

            dtSYS.Rows.Add(drSYS);
            _LOG06_SysPara_DTO.AcceptChanges();

            //寫入資料庫
            LOG06_Facade _LOG06_Facade = new LOG06_Facade();
            _LOG06_Facade.AddNewOne_SysPara(_LOG06_SysPara_DTO);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void UpdateData(string SID, string ModifyUser)
    {
        try
        {
            LOG06_StoreHeaderDiscountPWD _LOG06_SysPara_DTO = new LOG06_StoreHeaderDiscountPWD();
            LOG06_StoreHeaderDiscountPWD.STORE_HEADER_DISCOUNT_PWDDataTable dtSYS;
            LOG06_StoreHeaderDiscountPWD.STORE_HEADER_DISCOUNT_PWDRow drSYS;

            dtSYS = _LOG06_SysPara_DTO.Tables["STORE_HEADER_DISCOUNT_PWD"] as LOG06_StoreHeaderDiscountPWD.STORE_HEADER_DISCOUNT_PWDDataTable;

            drSYS = dtSYS.NewSTORE_HEADER_DISCOUNT_PWDRow();

            DataTable dt06 = LOG06_PageHelper.GetSHDP(SID);

            if (dt06.Rows.Count == 0)
            {
                lblMessage.Text = "查無可更新的資料!";
            }
            else
            {
                DataRow row = dt06.Rows[0];

                drSYS.ItemArray = new object[] {
                    SID,
                    row["VALID_START_DATE"],
                    row["VALID_END_DATE"],
                    row["ENCRYPT_PASSWORD"],
                    "0",                 //STATUS
                    row["CREATE_USER"],
                    row["CREATE_DTM"],
                    ModifyUser,          //MODI_USER
                    DateTime.Now,        //MODI_DTM
                    row["EMPNO"],
                    row["USERID"]
                };
            }

            dtSYS.Rows.Add(drSYS);
            _LOG06_SysPara_DTO.AcceptChanges();

            //更新資料庫
            LOG06_Facade _LOG06_Facade = new LOG06_Facade();
            _LOG06_Facade.UpdateOne_SysPara(_LOG06_SysPara_DTO);

            //新增一筆資料
            AddData(logMsg.OPERATOR);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool CheckInputData()
    {
        bool result = false;
        string checkdate = System.DateTime.Now.ToString("yyyy/MM/dd");
        if (txtSDate.Text == "")
        {
            lblMessage.Text = "請輸入有效日期起日";
        }
        else if (System.DateTime.Parse(txtSDate.Text) < System.DateTime.Parse(checkdate))
        {
            lblMessage.Text = "有效日期起日不允許小於系統日，請重新輸入";
        }
        else if (txtEDate.Text != "" && System.DateTime.Parse(txtEDate.Text) < System.DateTime.Parse(txtSDate.Text))
        {
            lblMessage.Text = "有效日期訖日不允許小於起日或系統日，請重新輸入";
        }
        else if (pw.Text == "")
        {
            lblMessage.Text = "密碼不得為空白";
        }
        else if (cpw.Text == "")
        {
            lblMessage.Text = "確認密碼不得為空白";
        }
        else if (pw.Text != cpw.Text)
        {
            lblMessage.Text = "確認密碼錯誤，請重新輸入";
        }
        else
        {
            result = true;
        }

        return result;
    }

    protected void bindMasterData()
    {
        //若資料已存在, 須帶出資料
        LOG06_Facade _LOG06_Facade = new LOG06_Facade();;
        DataTable dt = _LOG06_Facade.Query_SYSPara(logMsg.OPERATOR, logMsg.OPERATOR);
        if (dt.Rows.Count > 0)
        {
            trCurrentPassword.Visible = true;
            lblPassword.Text = "選擇一個新密碼";
            lblConfirmPassword.Text = "確認你的新密碼";

            DataRow row = dt.Rows[0];
            string StartDate = StringUtil.CStr(row["VALID_START_DATE"]);
            string EndDate = StringUtil.CStr(row["VALID_END_DATE"]);
            string Passwd = StringUtil.CStr(row["ENCRYPT_PASSWORD"]);  

            if (!string.IsNullOrEmpty(StartDate))
            {
                txtSDate.Text = System.DateTime.Parse(StartDate).ToShortDateString();
            }

            if (!string.IsNullOrEmpty(EndDate))
            {
                if (System.DateTime.Parse(EndDate).ToShortDateString() ==  System.DateTime.Parse(Advtek.Utility.DateUtil.NullDateFormatString).ToShortDateString())
                {

                }
                else 
                {
                    txtEDate.Text = System.DateTime.Parse(EndDate).ToShortDateString();
                }
            }

            if (!string.IsNullOrEmpty(Passwd))
            {
                //pw.Text = Passwd;
                //cpw.Text = Passwd;
            }
        }
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        if (!CheckInputData())
            return;

        string newEncryptPW = DESCryptography.EncryptDES(pw.Text, desKey, desIv);
        LOG06_Facade _LOG06_Facade = new LOG06_Facade();
        //DataTable dt = _LOG06_Facade.Query_SYSPara(logMsg.OPERATOR, logMsg.OPERATOR);
        //門市特殊折扣密碼設定改設定到store資料表
        DataTable dt = _LOG06_Facade.getStoreDiscountPWD(logMsg.STORENO);
        if (dt.Rows.Count > 0)
        {
            //更新

            DataRow row = dt.Rows[0];
            //string SID = StringUtil.CStr(row["SID"]);
            string encryptPW = StringUtil.CStr(row["STORE_PW"]);

            //檢核目前密碼
            string decryptPW = DESCryptography.DecryptDES(encryptPW, desKey, desIv);
            if (txtCurrentPassword.Text == "")
            {
                lblMessage.Text = "請輸入目前使用的密碼";
                txtCurrentPassword.Focus();
            }
            else if (txtCurrentPassword.Text != decryptPW)
            {
                lblMessage.Text = "目前使用的密碼輸入錯誤，請重新輸入";
            }
            else
            {
                //UpdateData(SID, logMsg.OPERATOR);
                _LOG06_Facade.updStoreDiscountPWD(logMsg.STORENO, newEncryptPW);

                lblUpdateDate.Text = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                lblMessage.Text = "";
                txtSDate.Text = "";
                txtEDate.Text = "";

                bindMasterData();
            }
        }
        else
        {
            //新增 
            //AddData(logMsg.OPERATOR);
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('無此門市，門市代碼[" + logMsg.STORENO + "]!');", true);
            lblUpdateDate.Text = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            lblMessage.Text = "";
            txtSDate.Text = "";
            txtEDate.Text = "";

            bindMasterData();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        LOG06_Facade _LOG06_Facade = new LOG06_Facade();
        string EmployeeNo = logMsg.OPERATOR;// Request.QueryString["EmployeeId"];
        DataTable dt = _LOG06_Facade.Query_SYSPara(EmployeeNo, EmployeeNo);
        if (dt.Rows.Count == 0)
        {
            txtSDate.Text = "";
            txtEDate.Text = "";
            txtCurrentPassword.Text = "";
            pw.Text = "";
            cpw.Text = "";
            lblUpdateDate.Text = ""; 
            lblMessage.Text = "";
        }
        else
            bindMasterData();
    }
}
