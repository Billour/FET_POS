using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ExportExcelData : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void ExportExcel(DataTable ivTable)
    {
        
        if (ivTable != null)
        {
            string tbName = ivTable.TableName;
            Table tbExport = new Table();
            BindDataToTable(ivTable, tbExport);
            Response.ClearHeaders();
            Response.Clear();
            Response.Write("<html><head>");
            Response.Write("<meta http-equiv=Content-Type content=text/html;charset=big5></head><body>");
            Response.AddHeader("content-disposition", "attachment;filename=" + tbName + ".xls");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("BIG5");
            Response.ContentType = "application/vnd.ms-excel";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
          //  string ExcelHeader = "<html><head><meta http-equiv=Content-Type content='text/html;charset=big5'></head><body>";
            string ExcelHeader = "";//<html><head><meta http-equiv=Content-Type ></head><body>";
            string ExcelFooter = "</body></html>";
            tbExport.RenderControl(htmlWrite);
            Response.Write(ExcelHeader + stringWrite.ToString() + ExcelFooter);
            Response.End();
        }
    }

    public void BindDataToTable(DataTable inDataTable, System.Web.UI.WebControls.Table inWebTable)
    {
        int i = 0;
        inWebTable.Rows.Clear();
    //    inWebTable.Style["text-size"] = "12px";
        TableRow theHeadRow = new TableRow();
        //theHeadRow.BorderStyle = BorderStyle.Solid;
        //theHeadRow.BorderColor = System.Drawing.Color.Black;
        inWebTable.Rows.Add(theHeadRow);
        theHeadRow.BackColor = System.Drawing.Color.Silver;
     //   theHeadRow.Style["color"] = "blue";
        foreach (DataColumn dc in inDataTable.Columns)
        {
            TableCell c = new TableCell();
     //       c.Style["border-color"] = "while";
            c.Text = dc.ColumnName;
            theHeadRow.Cells.Add(c);
        }
        foreach (DataRow dr in inDataTable.Rows)        {

            TableRow r = new TableRow();            
            foreach (DataColumn dc in inDataTable.Columns)
            {
                TableCell c = new TableCell();
                c.Text = dr[dc].ToString();
                r.Cells.Add(c);
            }
            i++;
            inWebTable.Rows.Add(r);
        }

    }

}
