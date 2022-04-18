using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using iTextSharp.text.pdf;
using Advtek.Utility;
using System.Collections.Specialized;
using System.Data.OracleClient;
using System.Data;

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

    /// <summary>
    /// 是否為Debug
    /// </summary>
    /// <returns></returns>
    public static bool IsDebug()
    {
        bool result = false;

        if (ConfigurationManager.AppSettings["Debug"] == null)
        {
            return false;
        }
        else
        {
            result = Convert.ToBoolean(ConfigurationManager.AppSettings["Debug"]);
             return result;
        }
    }

    /// <summary>
    /// 轉換Excel欄位值(將日期格式轉換成文字格式)
    /// </summary>
    /// <param name="cell">Excel欄位</param>
    /// <returns></returns>
    public static string ExcelColumnValueToString(NPOI.SS.UserModel.Cell cell)
    {
        string strValue = "";
        strValue = StringUtil.CStr(cell);

        if (cell.CellType == NPOI.SS.UserModel.CellType.NUMERIC)
        {
            DateTime Datetime;
            if (DateTime.TryParse(strValue, System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, out Datetime))
            {
                strValue = Datetime.ToString("yyyy/MM/dd");
            }
        }

        return strValue;
    }

    /// <summary>
    /// HTML傳遞參數時，要以加密處理
    /// </summary>
    /// <param name="sValue">欲加密的字串</param>
    /// <returns>加密後的字串</returns>
    public static string Param_Encrypt(string sValue)
    {
        //**2011/04/21 Tina：傳遞參數時，要先以加密處理。
        //HttpUtility.UrlEncode是為了避免傳遞特殊字元時發生異常。
        string myKey = "eP9mZ7Qs";
        EnDecrypt cCrypt = new EnDecrypt();
        string encryptUrl = HttpUtility.UrlEncode(cCrypt.Encrypt(sValue, myKey));

//        //將傳入的參數寫入至SYS_PROCESS_LOG
//        string sql = @"INSERT INTO SYS_PROCESS_LOG(FUNC_GROUP, ACTION_TYPE, CREATE_DTM, PARAMETER)
//                            VALUES('URL_PARAM', '-', SYSDATE, ' 加密:" + encryptUrl + "')";
//        SaveInfoToLog(sql);  


        return encryptUrl;

    }

    /// <summary>
    /// 解密HTML傳遞的參數
    /// </summary>
    /// <param name="sValue">欲解密的字串</param>
    /// <returns>解密後的字串</returns>
    public static NameValueCollection Param_UrlDecode(string sValue)
    {
        string myKey = "eP9mZ7Qs";
        EnDecrypt cCrypt = new EnDecrypt();
        string decodeUrl = sValue; //Tina：註解 HttpUtility.UrlDecode(sValue); 因為 HttpUtility.UrlDecode 會把加號("+")清空
        decodeUrl = cCrypt.Decrypt(decodeUrl, myKey);
        NameValueCollection qscoll = HttpUtility.ParseQueryString(decodeUrl);

        //將傳入的參數寫入至SYS_PROCESS_LOG
        string sql = @"INSERT INTO SYS_PROCESS_LOG(FUNC_GROUP, ACTION_TYPE, CREATE_DTM, PARAMETER)
                            VALUES('URL_PARAM', '-', SYSDATE, ' 加密:" + sValue + ",解密:" + decodeUrl + " ')";
        SaveInfoToLog(sql);  

        return qscoll;
    }

    /// <summary>
    /// 將傳入的參數寫入至SYS_PROCESS_LOG
    /// </summary>
    private static void SaveInfoToLog(string sql)
    {
        OracleConnection conn_new_pos = null;

        try
        {
            conn_new_pos = OracleDBUtil.GetConnection();

            //將傳入參數寫入
            OracleDBUtil.ExecuteSql(conn_new_pos, sql);
        }
        catch (Exception ex)
        {
            return;
        }
        finally
        {
            if (conn_new_pos.State == ConnectionState.Open) conn_new_pos.Close();
            conn_new_pos.Dispose();
            OracleConnection.ClearAllPools();
        }

    }

}
