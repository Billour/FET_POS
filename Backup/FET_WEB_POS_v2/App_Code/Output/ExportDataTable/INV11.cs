using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using iTextSharp.text.pdf;
using iTextSharp.text;

/// <summary>
/// INV11 的摘要描述
/// </summary>
public class INV11: AbstractPDF
{
	public INV11()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}

    public override bool accept(string model)
    {
        return (model == "INV11");
    }

    public override void exportTitle(string TitleName)
    {
        if (!string.IsNullOrEmpty(TitleName))
        {
            pdfTable.AddCell(new PdfPCell(new Paragraph(TitleName, largerFont))
            {
                HorizontalAlignment = PdfPCell.ALIGN_LEFT,
                BackgroundColor = BaseColor.LIGHT_GRAY,
                Border = Rectangle.NO_BORDER
            });
        }

    }

    public override void exportDataHeader()
    {
        //欄位標題
        List<string> headerCols = new List<string>();
        int c = 0;
        foreach (DataColumn dc in this.dtData.Columns)
        {
            headerCols.Add(dc.ColumnName);
            c++;
        }

        // Creates a phrase for a new line.
        PdfPCell clSpaceNull = new PdfPCell();
        clSpaceNull.Border = PdfPCell.NO_BORDER;
        clSpaceNull.Colspan = headerCols.Count;
        details.AddCell(clSpaceNull);

        for (int i = 0; i < 4; i++)
        {
            PdfPCell clSpace = new PdfPCell();
            clSpace.BackgroundColor = BaseColor.PINK;
            clSpace.HorizontalAlignment = Rectangle.ALIGN_CENTER;

            if (i <= 2)
            {
                clSpace.Phrase = new Paragraph(headerCols[i], smallFont);
                clSpace.Rowspan = 2;
                details.AddCell(clSpace);
            }
            else
            {
                Font BoldsmllFont = ChineseFontFactory.Create("細明體", 12f, Font.BOLD);

                clSpace.Phrase = new Paragraph("盤點數量", BoldsmllFont);
                clSpace.Colspan = 3;
                details.AddCell(clSpace);
                for (int j = i; j < headerCols.Count; j++)
                {
                    PdfPCell clSpace2 = new PdfPCell();
                    clSpace2.BackgroundColor = BaseColor.PINK;
                    clSpace2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    if (headerCols[j] == "展示倉")
                    {
                        clSpace2.Phrase = new Paragraph(headerCols[j], RedsmallFont);
                    }
                    else
                    {
                        clSpace2.Phrase = new Paragraph(headerCols[j], smallFont);
                    }
                    details.AddCell(clSpace2);
                }
            }
        }

    }


    public override void exportFooter(DataTable dtFooter)
    { 
        // 表尾資訊
        if (dtFooter != null)
        {
            PdfPTable footer = new PdfPTable(dtFooter.Columns.Count);
            footer.DefaultCell.Border = Rectangle.NO_BORDER;
            footer.DefaultCell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            foreach (DataRow drFooterer in dtFooter.Rows)
            {
                for (int i = 0; i <= dtFooter.Columns.Count - 1; i++)
                {
                    footer.AddCell(new Paragraph(drFooterer[i].ToString(), smallFont));
                }
            }

            pdfTable.AddCell(footer);
        }
    }
}
