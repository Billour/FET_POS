using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

public interface Printer
{
    /// <summary>
    /// 依照 列印格式的頁面名稱 取得class
    /// </summary>
    /// <param name="model">列印格式的頁面名稱</param>
    /// <returns>bool</returns>
    bool accept(string model);

    /// <summary>
    /// 列印格式設定
    /// </summary>
    /// <param name="dt">列印資料表</param>
    void prepareData(DataTable dt);

    /// <summary>
    /// 標題
    /// </summary>
    /// <param name="TitleName">標題</param>
    void exportTitle(string TitleName);

    /// <summary>
    /// 表頭資訊
    /// </summary>
    /// <param name="dtHeader">表頭資料表</param>
    void exportHeader(DataTable dtHeader);

    /// <summary>
    /// 表身欄位名稱
    /// </summary>
    void exportDataHeader();

    /// <summary>
    /// 表身明細資料
    /// </summary>
    void exportData();

    /// <summary>
    /// 表尾資訊
    /// </summary>
    /// <param name="dtFooter">表尾資料表</param>
    void exportFooter(DataTable dtFooter);

    /// <summary>
    /// 輸出
    /// </summary>
    /// <returns>檔案名稱(PDF)</returns>
    string output();
}

