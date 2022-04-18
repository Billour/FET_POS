<%@ WebHandler Language="C#" Class="Export" %>

using System;
using System.Web;
using System.IO;
using System.Web.UI;

public class Export : IHttpHandler, System.Web.SessionState.IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {

        string filename = context.Request.QueryString["FileName"];

        if (HttpContext.Current.Session["XLSstream"] != null)
        {
            MemoryStream stream = HttpContext.Current.Session["XLSstream"] as MemoryStream;
            context.Response.Clear();
            context.Response.Buffer = false;
            context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            context.Response.ContentType = "application/vnd.ms-excel";
            context.Response.AppendHeader("Content-Disposition", String.Format("attachment; filename={0}", filename));
            context.Response.BinaryWrite(stream.GetBuffer());
            context.Response.End();
            stream.Close();
            stream.Dispose();
            HttpContext.Current.Session["XLSstream"] = null;
        }
        else
        {
            StringWriter stringWrite = HttpContext.Current.Session["XLstringWrite"] as StringWriter;
            context.Response.ClearHeaders();
            context.Response.Clear();
            context.Response.Write("<meta http-equiv=Content-Type content=text/html;charset=BIG5>");
            context.Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("BIG5");
            context.Response.ContentType = "application/vnd.ms-excel";
            context.Response.Write(stringWrite.ToString());
            context.Response.End();
            HttpContext.Current.Session["XLstringWrite"] = null;

        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}