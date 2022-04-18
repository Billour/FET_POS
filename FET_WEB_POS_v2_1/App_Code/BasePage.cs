using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using Advtek.Utility;
using DevExpress.Web.ASPxEditors;

/// <summary>
/// BasePage 的摘要描述
/// </summary>
public class BasePage : System.Web.UI.Page
{
    Control _postBackControl;
    //protected LogMessage logMsg = new LogMessage();
    //protected static LogUtil Logger; 
   
    protected override void OnLoad(EventArgs e)
    {
        //Type pageType = this.GetType();
        //Logger = new LogUtil(this.GetType());

        //string Name = pageType.BaseType.Name;  //ex. VSS_DIS_DIS01, PopupControl, _Default
        //string[] PageFullName = Name.Split('_');
        //if (PageFullName.Length >= 2)
        //{
        //    this.logMsg.FUNC_GROUP = PageFullName[1];                           //FUNC GROUP
        //    if (PageFullName.Length >= 3)
        //    {
        //        this.logMsg.FUNCTION_NO = PageFullName[PageFullName.Length - 1]; //Class Nmae
        //    }
        //    else
        //    {
        //        this.logMsg.FUNCTION_NO = Name; //Class Nmae
        //    }
        //}
        //else
        //{
        //    this.logMsg.FUNC_GROUP = "-";
        //    this.logMsg.FUNCTION_NO = Name;     //Class Nmae
        //}

        //if (Request.QueryString != null)
        //{
        //    string SNO = GuidNo.getUUID();
        //    string HostName = Request.UserHostName;                 //主機名稱
        //    string strIP = Request.UserHostAddress;                 //IP位置
        //    string UserID = Request.QueryString["EmployeeId"];      //使用者帳號

        //    this.logMsg.SNO = SNO;                                  //系統使用記錄序號
        //    this.logMsg.SYS_ID = Request.QueryString["SysId"];      //系統別
        //    this.logMsg.MACHINE_ID = HostName;                      //門市機台編號
        //    this.logMsg.HOST_IP = strIP;                            //機台IP
        //    this.logMsg.PARAMETER = Request.QueryString.ToString(); //事件請求傳入參數
        //    this.logMsg.OPERATOR = UserID;                          //操作者
        //    this.logMsg.ENTERY_DTM = System.DateTime.Now;           //進入時間
        //    this.logMsg.ROLE_TYPE = UserID;                         //系統操作者角色
        //    this.logMsg.CREATE_USER = UserID;                       //建立人員
        //    this.logMsg.CREATE_DTM = System.DateTime.Now;           //建立時間
        //    this.logMsg.MODI_USER = UserID;                         //異動人員
        //    this.logMsg.MODI_DTM = System.DateTime.Now;             //異動時間

        //    if (!IsPostBack && !IsCallback)
        //    {
        //        log_Event(0, 0);
        //    }
        //}
        
        base.OnLoad(e);


    }
    /// <summary>
    /// Log至DB
    /// </summary>
    /// <param name="action">請求事件，0:SSO第一次連線請求, 1.insert, 2.update, 3.delete, 4.print, 5.query, 6.export, 7.import, 8.others</param>
    /// <param name="Rec_Count">影響資料筆數</param>
    protected void log_Event(int action, int Rec_Count)
    {
        //this.logMsg.IMPACT_REC_COUNT = 0;                       //影響資料筆數
        //this.logMsg.ACTION_TYPE = action.ToString();            //請求事件
        //Logger.Log.Info(this.logMsg);
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
}
