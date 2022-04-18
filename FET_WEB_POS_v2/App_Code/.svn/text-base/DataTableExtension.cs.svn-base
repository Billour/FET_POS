using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// DataTable 的擴充方法類別
/// </summary>
public static class DataTableExtension
{
    /// <summary>
    /// DataTable 分頁與排序
    /// </summary>
    /// <param name="dt">DataTable 資料來源</param>
    /// <param name="PageIndex">第 n 頁(Index 由 1 開始)</param>
    /// <param name="PageSize">每頁資料筆數</param>
    /// <param name="SortField">排序的欄位名稱(大小寫不分)</param>
    /// <param name="IsDescending">設定 true 則為 Descending；否則為 Aescending。</param>
    /// <returns>分頁與排序後的 DataTable</returns>
    public static DataTable PagingAndSorting(this DataTable dt, int PageIndex, int PageSize, string SortField, bool IsDescending)
    {
        if (PageIndex < 0 || PageSize <= 0)
        {
            dt.Rows.Clear();
            return dt;
        }

        var skip = (PageIndex) * PageSize;
        if (skip >= dt.Rows.Count || dt.Columns.Contains(SortField) == false)
        {
            dt.Rows.Clear();
            return dt;
        }

        if (IsDescending)
        {
            return dt.AsEnumerable().OrderByDescending(dr => dr[SortField]).Skip(skip).Take(PageSize).CopyToDataTable();
        }

        return dt.AsEnumerable().OrderBy(dr => dr[SortField]).Skip(skip).Take(PageSize).CopyToDataTable();
    }

    public static DataTable PagingAndSorting(this DataTable dt, int PageIndex, int PageSize)
    {
        if (PageIndex < 0 || PageSize <= 0)
        {
            dt.Rows.Clear();
            return dt;
        }
        var skip = (PageIndex) * PageSize;
        
        if (skip >= dt.Rows.Count)
        {
            dt.Rows.Clear();
            return dt;
        }
            
        return dt.AsEnumerable().Skip(skip).Take(PageSize).CopyToDataTable();               
    }
}
