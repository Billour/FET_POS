using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using Advtek.Utility;
using DevExpress.Web.ASPxEditors;
using FET.POS.Model.Facade.FacadeImpl;
using System.Collections.Specialized;

/// <summary>
/// BasePage 的摘要描述
/// </summary>
public class BasePage : System.Web.UI.Page
{
    Control _postBackControl = null;
    protected LogMessage logMsg = new LogMessage();
    protected static LogUtil Logger;
    
    protected override void OnLoad(EventArgs e)
    {
        object obj = HttpContext.Current.Session["QueryStingMsg"];
        if (obj != null)
        {
            logMsg = obj as LogMessage;
        }
        else
        {
            if (!string.IsNullOrEmpty(Request["ParamBasePage"]))
            {
                GetURLParam(Request["ParamBasePage"], ref logMsg); //透過EntryPoint頁傳過來的參數進行解密並儲存
            }
            else
            {
                // for Develop start
                if (Request.QueryString["storeId"] != null && Request.QueryString["EmployeeId"] != null && Request.QueryString["machine_id"] != null)
                {
                    GetMenuParam(ref logMsg); //透過menu.htm頁傳過來的參數進行儲存
                }
                else if (Session["logMsg"] != null)
                {
                    logMsg = (LogMessage)Session["logMsg"];
                }
                // for Develop end
            }
        }

        if (string.IsNullOrEmpty(logMsg.STORENO) || string.IsNullOrEmpty(logMsg.MACHINE_ID)
            || string.IsNullOrEmpty(logMsg.OPERATOR) || string.IsNullOrEmpty(logMsg.CREATE_USER)
            || string.IsNullOrEmpty(logMsg.MODI_USER) || string.IsNullOrEmpty(logMsg.ROLE_TYPE)
            || string.IsNullOrEmpty(logMsg.HOST_IP))
        {
            string StorePortalPage = System.Web.Configuration.WebConfigurationManager.AppSettings["StorePortalPage"];
            Response.Redirect(StorePortalPage);
        }

        logData();  //log資訊

        Session["logMsg"] = this.logMsg;

        base.OnLoad(e);


    }

    /// <summary>
    /// Log至DB
    /// </summary>
    /// <param name="action">請求事件，0:SSO第一次連線請求, 1.insert, 2.update, 3.delete, 4.print, 5.query, 6.export, 7.import, 8.others</param>
    /// <param name="Rec_Count">影響資料筆數</param>
    protected void log_Event(string action, int Rec_Count)
    {
        this.logMsg.IMPACT_REC_COUNT = 0;                       //影響資料筆數
        this.logMsg.ACTION_TYPE = action.ToString();            //請求事件
        Logger.Log.Info(this.logMsg);
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        ScriptManager scriptManager = ScriptManager.GetCurrent(this);

        if (!IsPostBack)
        {
            this.ViewState["PostBackCount"] = -1;
        }
        else if (scriptManager == null || !scriptManager.IsInAsyncPostBack)
        {
            this.ViewState["PostBackCount"] = Convert.ToInt16(this.ViewState["PostBackCount"]) - 1;
        }

        string script = @"function goBack() {
                            history.go(" + Convert.ToString(this.ViewState["PostBackCount"]) + @");
                        }
                        function isPostBack() {
                            return " + IsPostBack.ToString().ToLower() + @";
                        }";

        if (scriptManager != null)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Common", script, true);
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Common", script, true);
        }
    }

    public Control GetPostBackControl()
    {
        return _postBackControl ?? Global.GetPostBackControl(this);
    }

    protected bool IsFilterRelatedCallback
    {
        get
        {
            if (IsGridFilterBuilderCallback)
                return true;

            if (IsGridCallback)
            {
                return Request.Params["__CALLBACKPARAM"].Contains("FILTER");
            }
            return false;
        }
    }

    protected bool IsGridCallback
    {
        get
        {
            if (IsCallback && GetPostBackControl() is ASPxGridView)
            {
                return object.Equals(Request.Params["__CALLBACKID"], GetPostBackControl().UniqueID);
            }

            return false;
        }
    }

    protected bool IsGridFilterBuilderCallback
    {
        get
        {
            if (IsCallback && GetPostBackControl() is ASPxGridView)
            {
                object.Equals(Request.Params["__CALLBACKID"], GetPostBackControl().UniqueID + "$DXPFCForm$DXPFC");
            }

            return false;
        }
    }

    /// <summary>
    /// 動態建立內嵌框架，以輸出文件內容
    /// </summary>
    /// <param name="url"></param>
    protected void GenerateIFrameToLoadDocument(string url)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "SilentPrint", @"
        (function() {
        var el = document.createElement('iframe');
        el.setAttribute('id', 'printFrame');
        el.style.display = 'none';
        document.body.appendChild(el);
        el.setAttribute('src', '" + url + @"');
        })();
        ", true);
    }

    /// <summary>
    /// 透過EntryPoint頁傳過來的參數進行解密並儲存
    /// </summary>
    /// <param name="Param"></param>
    /// <param name="logMsg"></param>
    private void GetURLParam(string Param, ref LogMessage logMsg)
    {
        NameValueCollection qscoll = Utils.Param_UrlDecode(Param);

        foreach (string key in qscoll.AllKeys)
        {
            if (key == null) { continue; }

            string value = string.Join(",", qscoll.GetValues(key));
            string keyName = key.ToLower();

            //判斷是否有machine_id
            if (keyName == "machine_id")
            {
                logMsg.MACHINE_ID = value;
            }
            else if (keyName == "storeid")
            {
                logMsg.STORENO = value;
            }
            else if (keyName == "role")
            {
                logMsg.ROLE_TYPE = value;
            }
            else if (keyName == "employeeid")
            {
                logMsg.OPERATOR = logMsg.CREATE_USER = logMsg.MODI_USER = value;
            }
            else if (keyName == "host_ip")
            {
                logMsg.HOST_IP = string.IsNullOrEmpty(value) ? Request.UserHostAddress : value;
            }
        }

        if (!qscoll.AllKeys.Contains("HOST_IP"))
        {
            logMsg.HOST_IP = Request.UserHostAddress;
        }
    }

    /// <summary>
    /// 透過menu.htm頁傳過來的參數進行儲存
    /// </summary>
    /// <param name="logMsg"></param>
    private void GetMenuParam(ref LogMessage logMsg)
    {
        if (Request.QueryString["storeId"].Trim().Length != 0 && Request.QueryString["EmployeeId"].Trim().Length != 0 &&
        Request.QueryString["machine_id"].Trim().Length != 0)
        {
            logMsg.STORENO = Request.QueryString["storeId"];
            logMsg.OPERATOR = logMsg.CREATE_USER = logMsg.MODI_USER = Request.QueryString["EmployeeId"];
            logMsg.MACHINE_ID = Request.QueryString["machine_id"];
            logMsg.ROLE_TYPE = string.IsNullOrEmpty(Request.QueryString["role"]) ? System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"] : Request.QueryString["role"];
            Employee_Facade empFacade = new Employee_Facade();
            string roleType = empFacade.Query_RoleType(logMsg.STORENO, logMsg.OPERATOR);
            if (roleType != "")
                logMsg.ROLE_TYPE = roleType;
            if (logMsg.ROLE_TYPE == "")
                logMsg.ROLE_TYPE = "2"; //店員
            logMsg.HOST_IP = string.IsNullOrEmpty(Request.QueryString["HOST_IP"]) ? Request.UserHostAddress : Request.QueryString["HOST_IP"];
            Session["logMsg"] = logMsg;
        }

    }

    /// <summary>
    /// log資訊
    /// </summary>
    private void logData()
    {
        Type pageType = this.GetType();
        Logger = new LogUtil(this.GetType());

        string Name = pageType.BaseType.Name;  //ex. VSS_DIS_DIS01, PopupControl, _Default
        string[] PageFullName = Name.Split('_');
        if (PageFullName.Length >= 2)
        {
            this.logMsg.FUNC_GROUP = PageFullName[1];                           //FUNC GROUP
            if (PageFullName.Length >= 3)
            {
                this.logMsg.FUNCTION_NO = PageFullName[PageFullName.Length - 1]; //Class Nmae
            }
            else
            {
                this.logMsg.FUNCTION_NO = Name; //Class Nmae
            }
        }
        else
        {
            this.logMsg.FUNC_GROUP = "-";
            this.logMsg.FUNCTION_NO = Name;     //Class Nmae
        }

        this.logMsg.PARAMETER = Request.QueryString.ToString(); //事件請求傳入參數
        this.logMsg.ENTERY_DTM = System.DateTime.Now;           //進入時間
        this.logMsg.CREATE_DTM = System.DateTime.Now;           //建立時間
        this.logMsg.MODI_DTM = System.DateTime.Now;             //異動時間

        if (!IsPostBack && !IsCallback)
        {
            log_Event("OnLoad", 0);
        }

        Control ctl = GetPostBackControl();
        if (ctl != null && (ctl.GetType().Name != "ASPxGridView"))
        {
            IButtonControl Button = ctl as IButtonControl;
            if (Button != null)
            {
                log_Event(Button.Text, 0);
            }
        }

    }
}
