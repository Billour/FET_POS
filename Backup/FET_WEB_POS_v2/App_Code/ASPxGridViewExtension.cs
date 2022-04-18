using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

/// <summary>
/// ASPxGridViewExtension 的摘要描述
/// </summary>
public static class ASPxGridViewExtension
{

   public static List<string> GetGridViewSeletedColName(this ASPxGridView grid)
   {
      //取得所有欄位FILEDNAME
      List<string> allColName = new List<string>();

      for (int i = 0; i <= grid.Columns.Count - 1; i++)
      {

         GridViewColumn gvtc = grid.Columns[i];
         if (gvtc is GridViewDataColumn)
         {
            allColName.Add(((GridViewDataColumn)gvtc).FieldName.ToString());
         }
      }

      return allColName;
   }

   public static List<object> GetGridViewSeletedColValue(this ASPxGridView grid)
   {
      //取得所有欄位FILEDNAME
      List<string> allColName = GetGridViewSeletedColName(grid);

      //取得所有欄位值
      List<object> keyValues = grid.GetSelectedFieldValues(allColName.ToArray());//控件的ID

      return keyValues;
   }

   public static List<object> GetGridViewSeletedPKColValue(this ASPxGridView grid, string[] pkColName)
   {
      //取得所有欄位值
      List<object> keyValues = grid.GetSelectedFieldValues(pkColName);//控件的ID

      return keyValues;
   }

}
