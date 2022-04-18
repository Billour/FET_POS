using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

/// <summary>
/// 公用程式類別
/// </summary>
public static class Utils
{    
    /// <summary>
    /// 擴充Gridview增加轉換成PDF表格功能
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="title">文件標題</param>
    /// <returns></returns>
    public static PdfPTable ToPdfTable(this GridView grid, string title, int pageSize)
    {
        int noOfColumns = 0, noOfRows = 0;
        DataTable dataTable = null;
        
        if (grid.AutoGenerateColumns)
        {
            dataTable = grid.DataSource as DataTable; 
            noOfColumns = dataTable.Columns.Count;
            noOfRows = dataTable.Rows.Count;
        }
        else
        {
            noOfColumns = grid.Columns.Count;
            noOfRows = grid.Rows.Count;
        }
   
        PdfPTable mainTable = new PdfPTable(noOfColumns);        
        mainTable.HeaderRows = 1;  
  
        Phrase phHeader = new Phrase(title, ChineseFontFactory.Create("細明體", 12f, iTextSharp.text.Font.NORMAL));
        PdfPCell clHeader = new PdfPCell(phHeader);
        clHeader.Colspan = noOfColumns;
        clHeader.Border = PdfPCell.NO_BORDER;
        clHeader.HorizontalAlignment = Element.ALIGN_CENTER;
        mainTable.AddCell(clHeader);

        // Creates a phrase for a new line.
        Phrase phSpace = new Phrase("\n");
        PdfPCell clSpace = new PdfPCell(phSpace);
        clSpace.Border = PdfPCell.NO_BORDER;
        clSpace.Colspan = noOfColumns;
        //mainTable.AddCell(clSpace);

        // Sets the gridview column names as table headers.
        for (int i = 0; i < noOfColumns; i++)
        {
            iTextSharp.text.Font headerFont = ChineseFontFactory.Create("標楷體", 10f, iTextSharp.text.Font.BOLD);
            string headerText = grid.AutoGenerateColumns ? dataTable.Columns[i].ColumnName : grid.Columns[i].HeaderText;            
            mainTable.AddCell(new Phrase(headerText, headerFont));
        }

        // Reads the gridview rows and adds them to the mainTable
        iTextSharp.text.Font itemFont = ChineseFontFactory.Create("細明體", 9f, iTextSharp.text.Font.NORMAL);
        for (int rowNo = 0; rowNo < noOfRows; rowNo++)
        {
            for (int columnNo = 0; columnNo < noOfColumns; columnNo++)
            {
                int align = ConvertToPdfPCellHorizontalAlign(grid.Columns[columnNo].ItemStyle.HorizontalAlign);  
                
                if (grid.AutoGenerateColumns)
                {                    
                    Phrase ph = new Phrase(grid.Rows[rowNo].Cells[columnNo].Text.Trim(), itemFont);
                    mainTable.AddCell(new PdfPCell(ph) {
                        HorizontalAlignment = align  
                    });
                }
                else
                {
                    if (grid.Columns[columnNo] is TemplateField)
                    {                        
                        DataBoundLiteralControl lc = grid.Rows[rowNo].Cells[columnNo].Controls[0] as DataBoundLiteralControl;
                        Phrase ph = new Phrase(lc.Text.Trim(), itemFont);                        
                        mainTable.AddCell(new PdfPCell(ph) {
                            HorizontalAlignment = align
                        });
                    }
                    else
                    {                        
                        Phrase ph = new Phrase(grid.Rows[rowNo].Cells[columnNo].Text.Trim(), itemFont);
                        mainTable.AddCell(new PdfPCell(ph) {
                            HorizontalAlignment = align
                        });
                    }
                }
            }            
            mainTable.CompleteRow();            
        }

        if (grid.ShowFooter)
        {
            for (int i = 0; i < grid.FooterRow.Cells.Count; i++)
            {
                iTextSharp.text.Font headerFont = ChineseFontFactory.Create("細明體", 9f, iTextSharp.text.Font.BOLD);                
                Phrase ph = new Phrase(grid.FooterRow.Cells[i].Text.Trim(), itemFont);
                mainTable.AddCell(new PdfPCell(ph)
                {
                    Colspan = grid.FooterRow.Cells[i].ColumnSpan,
                    HorizontalAlignment = ConvertToPdfPCellHorizontalAlign(grid.FooterRow.Cells[i].HorizontalAlign)
                });
            }
        }
       
        return mainTable;        
    }

    /// <summary>
    /// 將HorizontalAlign列舉型別轉換成PdfPCell相對應的水平對齊常數
    /// </summary>
    /// <param name="align"></param>
    /// <returns></returns>
    private static int ConvertToPdfPCellHorizontalAlign(HorizontalAlign align)
    {
        switch (align)
        {
            case HorizontalAlign.Right:
                return PdfPCell.ALIGN_RIGHT;   
            case HorizontalAlign.Center:
                return PdfPCell.ALIGN_CENTER;
        }
        return PdfPCell.ALIGN_LEFT;
    }

    /// <summary>
    /// 擴充DataTable增加轉換成PDF文件功能
    /// </summary>
    /// <param name="dataTable"></param>
    /// <param name="stream">文件的輸出串流</param>
    /// <param name="pageSize">資料頁的大小</param>
    /// <returns></returns>
    public static Document ToPdfDocument(this DataTable dataTable, Stream stream, int pageSize)
    {     
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
        PdfWriter.GetInstance(pdfDoc, stream);

        // Creates a footer for the PDF document.
        HeaderFooter pdfFooter = new HeaderFooter(new Phrase(), true);
        pdfFooter.Alignment = Element.ALIGN_CENTER;
        pdfFooter.Border = iTextSharp.text.Rectangle.NO_BORDER;
        pdfDoc.Footer = pdfFooter;
        pdfDoc.Open();
        
        int dataIndex = 0;
        int pageNumber = 0;
        while (dataIndex < dataTable.Rows.Count)
        {
            PdfPTable pdfTable = new PdfPTable(dataTable.Columns.Count);

            PdfTableBuilder pdfBuilder = new PdfTableBuilder(dataTable.Columns.Count);
            pdfBuilder.MakeHeader(dataTable.Columns.ToPdfPCells());

            for (int i = dataIndex; i < dataTable.Rows.Count; i++)
            {
                pdfBuilder.MakeRow(dataTable.Rows[i].ToPdfPCells());

                dataIndex++;

                if (dataIndex % pageSize == 0 || dataIndex == dataTable.Rows.Count)
                {
                    if (pageNumber > 0)
                        pdfDoc.NewPage();

                    pdfDoc.Add(pdfBuilder.GetResult());
                    pageNumber++;
                    break;
                }
            }                                              
        }

        pdfDoc.Close();
        return pdfDoc;
    }

    /// <summary>
    /// 擴充DataColumnCollection增加轉換成PdfPCell陣列功能
    /// </summary>
    /// <param name="columns"></param>
    /// <returns></returns>
    private static PdfPCell[] ToPdfPCells(this DataColumnCollection columns)
    {
        iTextSharp.text.Font font = ChineseFontFactory.Create("標楷體", 10f, iTextSharp.text.Font.BOLD);
        List<PdfPCell> cells = new List<PdfPCell>();
        foreach (DataColumn column in columns)
        {
            cells.Add(new PdfPCell(new Phrase(column.Caption, font)));
        }

        return cells.ToArray<PdfPCell>();
    }

    /// <summary>
    /// 擴充DataRow增加轉換成PdfPCell陣列功能
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    private static PdfPCell[] ToPdfPCells(this DataRow row)
    {
        iTextSharp.text.Font font = ChineseFontFactory.Create("細明體", 9f, iTextSharp.text.Font.NORMAL);
        List<PdfPCell> cells = new List<PdfPCell>();
        for (int i = 0; i < row.ItemArray.Length; i++)
        {
            cells.Add(new PdfPCell(new Phrase(row[i].ToString(), font)));
        }

        return cells.ToArray<PdfPCell>();
    }
    
    /// <summary>
    /// 擴充Gridview增加匯出PDF功能，提供使用者下載PDF檔案
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="title">文件標題</param>
    /// <param name="filename">匯出的檔案名稱</param>
    public static void ExportToPdf(this GridView grid, string title, string filename)
    {                     
        PdfPTable pdfTable = grid.ToPdfTable(title, 20);                 
        HttpContext context = HttpContext.Current;
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
        PdfWriter.GetInstance(pdfDoc, context.Response.OutputStream);
        pdfDoc.Open();
        pdfDoc.Add(pdfTable);
        pdfDoc.Close();
                
        context.Response.ContentType = "application/pdf";
        context.Response.AddHeader("content-disposition", "attachment; filename=" + filename);
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        context.Response.Write(pdfDoc);
        context.Response.End();
    }
   
}
