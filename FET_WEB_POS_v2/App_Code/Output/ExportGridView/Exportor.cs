using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DevExpress.XtraPrinting;
using DevExpress.Web.ASPxGridView.Export;
using System.IO;

public interface Exportor
{
    /// <summary>
    /// 依照 匯出的頁面名稱 取得class
    /// </summary>
    /// <param name="model">匯出的頁面名稱</param>
    /// <returns>bool</returns>
    bool accept(string model);

    /// <summary>
    /// 匯出格式設定
    /// </summary>
    /// <param name="Width">頁面寬度</param>
    void prepareData(int Width);

    /// <summary>
    /// 標題
    /// </summary>
    void exportTitle(string TitleName);

    /// <summary>
    /// 表頭資訊
    /// </summary>
    void exportHeader(DataTable dtHeader);

    /// <summary>
    /// 表身明細資料
    /// </summary>
    void exportData(ASPxGridViewExporter ASPxGridViewExporter1);

    /// <summary>
    /// 表尾資訊
    /// </summary>
    void exportFooter(DataTable dtFooter);

    /// <summary>
    /// 結尾
    /// </summary>
    void exportBottom(string Bottom);

    /// <summary>
    /// 輸出
    /// </summary>
    void output();
}

