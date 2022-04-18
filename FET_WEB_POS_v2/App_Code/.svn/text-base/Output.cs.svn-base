using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DevExpress.Web.ASPxGridView.Export;

/// <summary>
/// Output 的摘要描述
/// </summary>
public class Output
{
    public Output()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

    /// <summary>
    /// 列印格式設定
    /// </summary>
    /// <param name="model">客製列印格式的頁面名稱</param>
    /// <param name="TitleName">標題</param>
    /// <param name="dtHeader">表頭資訊(可以為null)</param>
    /// <param name="dtData">表身明細資料</param>
    /// <param name="dtFooter">表尾資訊(可以為null)</param>
    /// <returns>檔案名稱(PDF)</returns>
    public string Print(string model, string TitleName, DataTable dtHeader, DataTable dtData, DataTable dtFooter)
    {
        Printer[] exps = new Printer[] { new PDFExportor(), new INV11(), new RPL053(), new XLSExportor(), new PRE01() };
        Printer exp = null;
        foreach (Printer tmp in exps)
        {
            if (tmp.accept(model))
            {
                exp = tmp;
            }
        }
        string filename = OrganizeFormat(exp, model, TitleName, dtHeader, dtData, dtFooter);
        return filename;
    }

    /// <summary>
    /// 列印格式設定
    /// </summary>
    /// <param name="model">客製列印格式的頁面名稱</param>
    /// <param name="TitleName">標題</param>
    /// <param name="dtHeader">表頭資訊(可以為null)</param>
    /// <param name="dtData">表身明細資料</param>
    /// <param name="dtFooter">表尾資訊(可以為null)</param>
    /// <returns>檔案名稱(PDF)</returns>
    public string Print_ORD10(string model, string TitleName, DataTable dtHeader, DataTable dtData, DataTable dtFooter)
    {
        Printer[] exps = new Printer[] { new PDFExportor(), new INV11(), new RPL053(), new XLSExportor(), new PRE01() };
        Printer exp = null;
        foreach (Printer tmp in exps)
        {
            if (tmp.accept(model))
            {
                exp = tmp;
            }
        }
        string filename = OrganizeFormatORD10(exp, model, TitleName, dtHeader, dtData, dtFooter);
        return filename;
    }

    /// <summary>
    /// 列印格式設定
    /// </summary>
    /// <param name="model">客製列印格式的頁面名稱</param>
    /// <param name="TitleName">標題</param>
    /// <param name="dtHeader">表頭資訊(可以為null)</param>
    /// <param name="dtData">表身明細資料</param>
    /// <param name="dtFooter">表尾資訊(可以為null)</param>
    /// <returns>檔案名稱(PDF)</returns>
    public string PrintRPL046(string model, string TitleName, DataTable dtHeader, DataTable dtData, DataTable dtFooter)
    {
        Printer[] exps = new Printer[] { new PDFExportor(), new INV11(), new RPL046(), new XLSExportor() };
        Printer exp = null;
        foreach (Printer tmp in exps)
        {
            if (tmp.accept(model))
            {
                exp = tmp;
            }
        }
        string filename = OrganizeFormat(exp, model, TitleName, dtHeader, dtData, dtFooter);
        return filename;
    }

    /// <summary>
    /// 列印格式設定
    /// </summary>
    /// <param name="model">客製列印格式的頁面名稱</param>
    /// <param name="TitleName">標題</param>
    /// <param name="dtHeader">表頭資訊(可以為null)</param>
    /// <param name="dtData">表身明細資料</param>
    /// <param name="dtFooter">表尾資訊(可以為null)</param>
    /// <returns>檔案名稱(PDF)</returns>
    public string PrintRPL046_SUM(string model, string TitleName, DataTable dtHeader, DataTable dtData, DataTable dtFooter)
    {
        Printer[] exps = new Printer[] { new PDFExportor(), new INV11(), new RPL046_SUM(), new XLSExportor() };
        Printer exp = null;
        foreach (Printer tmp in exps)
        {
            if (tmp.accept(model))
            {
                exp = tmp;
            }
        }
        string filename = OrganizeFormat_RPL046_SUM(exp, model, TitleName, dtHeader, dtData, dtFooter);
        return filename;
    }

    private string OrganizeFormat_RPL046_SUM(Printer Exp, string model, string TitleName, DataTable dtHeader, DataTable dtData, DataTable dtFooter)
    {
        Printer exp = Exp;
        exp.prepareData(dtData);
        exp.exportTitle(TitleName);
        exp.exportHeader(dtHeader);
        //exp.exportDataHeader();
        exp.exportData();
        exp.exportFooter(dtFooter);
        string filename = exp.output();

        return filename;
    }

    private string OrganizeFormat(Printer Exp, string model, string TitleName, DataTable dtHeader, DataTable dtData, DataTable dtFooter)
    {
        Printer exp = Exp;
        exp.prepareData(dtData);
        exp.exportTitle(TitleName);
        exp.exportHeader(dtHeader);
        exp.exportDataHeader();
        exp.exportData();
        exp.exportFooter(dtFooter);
        string filename = exp.output();

        return filename;
    }

    private string OrganizeFormatORD10(Printer Exp, string model, string TitleName, DataTable dtHeader, DataTable dtData, DataTable dtFooter)
    {
        Printer exp = Exp;
        exp.prepareData(dtData);
        exp.exportTitle(TitleName);
        //exp.exportHeader(dtHeader);
        exp.exportDataHeader();
        exp.exportData();
        exp.exportFooter(dtFooter);
        string filename = exp.output();

        return filename;
    }

    /// <summary>
    /// 將GridView匯出Excel檔
    /// </summary>
    /// <param name="PageWidth">頁面寬度</param>
    /// <param name="model">匯出格式</param>
    /// <param name="TitleName">標題</param>
    /// <param name="dtHeader">表頭資訊</param>
    /// <param name="ASPxGridViewExporter1">ASPxGridViewExporter control</param>
    /// <param name="dtFooter">表尾資訊</param>
    /// <param name="Bottom">結尾</param>
    public void ExportXLS(int PageWidth, string model, string TitleName, DataTable dtHeader, ASPxGridViewExporter ASPxGridViewExporter1, DataTable dtFooter, string Bottom)
    {
        string strmodel = "gvXLS";
        if (!string.IsNullOrEmpty(model))
        {
            strmodel = model;
        }

        Exportor[] exps = new Exportor[] { new XLSDefaultExportor(), new RPL044() };
        Exportor exp = null;
        foreach (Exportor tmp in exps)
        {
            if (tmp.accept(strmodel))
            {
                exp = tmp;
            }
        }

        //Exportor exp = new XLSDefaultExportor();
        exp.prepareData(PageWidth);
        exp.exportTitle(TitleName);
        exp.exportHeader(dtHeader);
        exp.exportData(ASPxGridViewExporter1);
        exp.exportFooter(dtFooter);
        exp.exportBottom(Bottom);
        exp.output();
    }
}
