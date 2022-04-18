using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;

/// <summary>
/// AdvTekUtility 的摘要描述
/// </summary>
public static class Utils
{
    /// <summary>     
    /// Similar to Control.FindControl, but recurses through child controls.
    /// Assumes that startingControl is NOT the control you are searching for.
    /// </summary>
    public static T FindChildControl<T>(this Control startingControl, string id) where T : Control
    {
        T found = null;

        foreach (Control activeControl in startingControl.Controls)
        {
            found = activeControl as T;

            if (found == null || (string.Compare(id, found.ID, true) != 0))
            {
                found = FindChildControl<T>(activeControl, id);
            }

            if (found != null)
            {
                break;
            }
        }

        return found;
    }

    /// <summary>
    /// Returns a site relative HTTP path from a partial path starting out with a ~.
    /// Same syntax that ASP.Net internally supports but this method can be used
    /// outside of the Page framework.
    /// 
    /// Works like Control.ResolveUrl including support for ~ syntax
    /// but returns an absolute URL.
    /// </summary>
    /// <param name="originalUrl">Any Url including those starting with ~</param>
    /// <returns>relative url</returns>
    public static string ResolveUrl(string originalUrl)
    {
        if (originalUrl == null)
            return null;

        // *** Absolute path - just return
        if (originalUrl.IndexOf("://") != -1)
            return originalUrl;

        // *** Fix up image path for ~ root app dir directory
        if (originalUrl.StartsWith("~"))
        {
            string newUrl = "";
            if (HttpContext.Current != null)
                newUrl = HttpContext.Current.Request.ApplicationPath +
                      originalUrl.Substring(1).Replace("//", "/");
            else
                // *** Not context: assume current directory is the base directory
                throw new ArgumentException("Invalid URL: Relative URL not allowed.");

            // *** Just to be sure fix up any double slashes
            if (newUrl.StartsWith("//"))
            {
                newUrl = newUrl.Substring(1);
            }
            return newUrl;
        }

        return originalUrl;
    }

    /// <summary>
    /// 加入自動列印指令碼
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="printerName"></param>
    public static void AddSilentPrintAction(this PdfWriter writer, string printerName)
    {
        PdfAction js = PdfAction.JavaScript(@"        
        var pp = this.getPrintParams();
        //pp.interactive = pp.constants.interactionLevel.silent;
        pp.interactive = pp.constants.interactionLevel.automatic
        pp.printerName = '" + printerName.Replace(@"\", @"\\") + @"';
        pp.pageHandling = pp.constants.handling.none;         
        var fv = pp.constants.flagValues;
        pp.flags |= fv.setPageSize;
        pp.flags |= (fv.suppressCenter | fv.suppressRotate);
        this.print(pp);
        ", writer);
        writer.AddJavaScript(js);
    }

    /// <summary>
    /// 建立防竄改的文件下載網址
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string CreateTamperProofDownloadURL(string fileName) 
    {
        return TamperProofQueryString.CreateTamperProofURL(
            "~/Download.ashx", string.Empty, "FileName=" + fileName);
    }

    public static string CreateTamperProofDownloadXlsURL(string fileName)
    {
        return TamperProofQueryString.CreateTamperProofURL(
        "~/Export.ashx", string.Empty, "FileName=" + fileName);
    }
    //*** 轉換一位數 
    public static object GF_Converts(object NumStr)
    {
        object functionReturnValue = null;

        switch (Convert.ToInt32(NumStr))
        {
            case 0:
                functionReturnValue = "零";
                break;
            case 1:
                functionReturnValue = "一";
                break;
            case 2:
                functionReturnValue = "二";
                break;
            case 3:
                functionReturnValue = "三";
                break;
            case 4:
                functionReturnValue = "四";
                break;
            case 5:
                functionReturnValue = "五";
                break;
            case 6:
                functionReturnValue = "六";
                break;
            case 7:
                functionReturnValue = "七";
                break;
            case 8:
                functionReturnValue = "八";
                break;
            case 9:
                functionReturnValue = "九";
                break;
        }
        return functionReturnValue;
    } 
}
