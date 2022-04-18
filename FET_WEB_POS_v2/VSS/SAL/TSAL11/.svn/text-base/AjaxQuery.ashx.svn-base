<%@ WebHandler Language="C#" Class="AjaxQuery" %>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState;
using Advtek.Utility;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;

public class AjaxQuery : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        if (string.IsNullOrEmpty(context.Request["mode"]))
        {
            OutputData(context, string.Empty);
        }
        else
        {
            string mode = context.Request["mode"];
            switch (mode.ToUpper())
            {
                case "INVOICE_NO":
                    if (string.IsNullOrEmpty(context.Request["invno"]))
                        OutputData(context, string.Empty);
                    else
                        OutputData(context, CheckInvoiceNo(context.Request["invno"], context.Request["invtype"]));
                    break;
                default:
                    OutputData(context, string.Empty);
                    break;
            }
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    public LogMessage logMsg { get { return HttpContext.Current.Session["logMsg"] as LogMessage; } }

    #region Private Method : void OutputData(HttpContext context, string data)
    private void OutputData(HttpContext context, string data)
    {
        context.Response.Cache.SetCacheability(HttpCacheability.Public);
        context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        context.Response.Expires = 0;
        context.Response.Cache.SetValidUntilExpires(true);
        context.Response.BufferOutput = false;
        context.Response.ContentType = "text/plain";
        context.Response.Write(data);
    }
    #endregion


    private string CheckInvoiceNo(string InvoiceNo, string InvoiceType)
    {
        // RESULT:1表示發票號碼存在Invoice Pool，可以使用。RESULT:1表示發票號碼不存在或不允許使用
        SAL01_Facade SAL01Facade = new SAL01_Facade();
        if (SAL01Facade.IsValidINVNo(this.logMsg.STORENO, InvoiceNo, InvoiceType))
            return string.Format("{{ RESULT:'{0}' }}", 1);
        else
            return string.Format("{{ RESULT:'{0}' }}", 0);
    }
}