<%@ WebHandler Language="C#" Class="Download" %>

using System;
using System.Web;
using System.IO;

public class Download : IHttpHandler
{                   
    public void ProcessRequest(HttpContext context)
    {       
        string filename = context.Request.QueryString["FileName"]; 
        // 檢查網址是否竄改
        TamperProofQueryString.EnsureURLNotTampered(String.Format("FileName={0}",filename));
                          
        string filePath = Path.Combine(context.Server.MapPath("~/Downloads"), filename);
        FileInfo file = new FileInfo(filePath);

        // 控制輸出文件類型
        string contentType = "";        
        switch (file.Extension.ToLower())
        {
            case ".pdf":
                contentType = "application/pdf";
                break;
            case ".htm":
            case ".html":
                contentType = "text/html";
                break;
            case ".txt":
                contentType = "text/plain";
                break;
            case ".xls":
                contentType = "application/vnd.ms-excel";
                break;
            case ".doc":
            case ".rtf":
                contentType = "application/msword";
                break;
            default:
                // 不接受其他檔案類型下載
                throw new Exception("Unknown content type.");                
        }
                    
        context.Response.Clear();        
        //context.Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}", filename));        
        context.Response.AddHeader("Content-Length", file.Length.ToString());//actual size (padd bytes discarded below)
        //context.Response.ContentType = "application/octet-stream";
        context.Response.ContentType = contentType;
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        context.Response.WriteFile(file.FullName);
        context.Response.Flush(); 
        // 下載完成後，自動刪除檔案       
        file.Delete();                
        context.Response.End();                   
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}