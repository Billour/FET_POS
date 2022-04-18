using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

/// <summary>
/// 用來建構PDF表格的類別
/// </summary>
public class PdfTableBuilder 
{
    private PdfPTable _pdfTable;

    public PdfTableBuilder(int numberOfColumns)
    {       
        _pdfTable = new PdfPTable(numberOfColumns);                
    }

    /// <summary>
    /// 建立表格表頭
    /// </summary>
    /// <param name="title"></param>
    public void MakeTitle(string title)
    {        
        _pdfTable.AddCell(new PdfPCell(new Paragraph(title, ChineseFontFactory.Create("細明體", 14, Font.BOLD)))
        {
            Colspan = _pdfTable.NumberOfColumns,
            HorizontalAlignment = Element.ALIGN_CENTER,
            VerticalAlignment = Element.ALIGN_MIDDLE,
            Border = Rectangle.NO_BORDER
        });
    }
        
    /// <summary>
    /// 建立表格欄位標題
    /// </summary>
    public void MakeHeader(params PdfPCell[] cells)
    {
        _pdfTable.HeaderRows = 1;
        MakeRow(cells);
    }

    /// <summary>
    /// 建立表格列
    /// </summary>
    /// <param name="cells"></param>
    public void MakeRow(params PdfPCell[] cells)
    {
        foreach(PdfPCell cell in cells)
        {
            _pdfTable.AddCell(cell);
        }
    }
  
    /// <summary>
    /// 取得已建立的表格物件
    /// </summary>
    /// <returns></returns>
    public PdfPTable GetResult()
    {
        return this._pdfTable;
    }
}