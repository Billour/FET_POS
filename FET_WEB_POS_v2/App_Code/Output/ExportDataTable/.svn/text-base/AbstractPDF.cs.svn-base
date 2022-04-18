using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;
using System.Configuration;

/// <summary>
/// AbstractPDFExportor 的摘要描述
/// </summary>
public abstract class AbstractPDF : BasePage, Printer
{
    protected Font smallFont;
    protected Font largerFont;
    protected Font RedsmallFont;
    protected Document pdfDoc;
    protected PdfPTable pdfTable;  //組成整體樣式
    protected PdfPTable details;   //組成明細樣式
    protected DataTable dtData;    //前端傳來的資料表
    protected string filename;

    public AbstractPDF()
	{
        smallFont = ChineseFontFactory.Create("細明體", 9f, Font.NORMAL);
        largerFont = ChineseFontFactory.Create("細明體", 12f, Font.BOLD);
        RedsmallFont = ChineseFontFactory.Create("細明體", 9f, Font.NORMAL, BaseColor.RED);
    }

    #region Exportor 成員

    public virtual bool accept(string model)
    {
        return true;
    }

    public virtual void prepareData(DataTable dt)
    {
        //string printerName = @"\\192.168.8.100\HP LaserJet 3055 PCL5"; //"\\192.168.8.1\Brother MFC9600/9870 Series";
        //string printerName = @"HP LaserJet 3050 Series PCL 5e"; //印表機和傳真 
        //string printerName = @"\\192.168.8.100\HP LaserJet 3050 Series PCL 6"; //"\\192.168.8.1\Brother MFC9600/9870 Series";
        string printerName = ConfigurationManager.AppSettings["PDFPrinterName"].ToString();//web.config中設定

        pdfDoc = new Document(PageSize.A4, 10f, 10f, 20f, 20f);

        filename = Guid.NewGuid().ToString() + ".pdf";
        FileStream stream = new FileStream(Path.Combine(Server.MapPath("~/Downloads"), filename), FileMode.Create);
        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
        pdfDoc.Open();

        // 加入自動列印指令碼
        //writer.AddSilentPrintAction(printerName);
        writer.AddJavaScript("this.print(false);", true);

        
        
        

        Paragraph title = new Paragraph();
        title.Alignment = Element.ALIGN_CENTER;
        title.Add(new Chunk("", largerFont));
        pdfDoc.Add(title);

        pdfTable = new PdfPTable(1);
        pdfTable.DefaultCell.Border = 0;
        pdfTable.SplitRows = false;
        pdfTable.KeepTogether = true;

        this.dtData = dt;
        this.details = new PdfPTable(dt.Columns.Count);
        this.details.HeaderRows = 1;
       
        
    }

    public virtual void exportTitle(string TitleName)
    {
        if (!string.IsNullOrEmpty(TitleName))
        {
            pdfTable.AddCell(new PdfPCell(new Paragraph(TitleName, largerFont))
               {
                   HorizontalAlignment = PdfPCell.ALIGN_CENTER,
                   Border = Rectangle.NO_BORDER
               });
        }
    }

    public virtual void exportHeader(DataTable dtHeader)
    {
        // 表頭資訊
        if (dtHeader != null)
        {
            PdfPTable header = new PdfPTable(dtHeader.Columns.Count);  //new float[]{0.15f, 0.35f, 0.15f, 0.35f});
            header.DefaultCell.Border = 0;
            header.DefaultCell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            foreach (DataRow drHeader in dtHeader.Rows)
            {
                for (int i = 0; i <= dtHeader.Columns.Count - 1; i++)
                {
                  header.AddCell(new Paragraph(drHeader[i].ToString(), smallFont));
                }
            }
            pdfTable.AddCell(header);
        }

    }

    public virtual void exportDataHeader()
    {
        //欄位標題
        List<string> headerCols = new List<string>();
        int c = 0;
        foreach (DataColumn dc in this.dtData.Columns)
        {
            headerCols.Add(dc.ColumnName);
            c++;
        }

        headerCols.ForEach(col =>
        {
            details.AddCell(new PdfPCell(new Paragraph(col, smallFont))
            {
                BackgroundColor = BaseColor.LIGHT_GRAY,
                HorizontalAlignment = Rectangle.ALIGN_CENTER
            });
        });

    }

    public virtual void exportData()
    {
        // 表身明細資料
        List<string[]> dataRows = new List<string[]>();
        foreach (DataRow dr in this.dtData.Rows)
        {
            string[] data = new string[this.dtData.Columns.Count];
            for (int i = 0; i <= this.dtData.Columns.Count - 1; i++)
            {
                data[i] = dr[i].ToString();
            }

            dataRows.Add(data);
        }

        dataRows.ForEach(dr =>
        {
            for (int i = 0; i < dr.Length; i++)
            {
                try
                {
                    long Number = Convert.ToInt64(dr[i]);
                    details.AddCell(new PdfPCell(new Paragraph(dr[i], smallFont))
                    {
                        HorizontalAlignment = Rectangle.ALIGN_RIGHT
                    });
                }
                catch
                {
                    details.AddCell(new PdfPCell(new Paragraph(dr[i], smallFont))
                    {
                        HorizontalAlignment = Rectangle.ALIGN_LEFT
                    });
                }

                //details.AddCell(new PdfPCell(new Paragraph(dr[i], smallFont))
                //{
                //    HorizontalAlignment = (dr[i].GetType().Name == "String") ? Rectangle.ALIGN_LEFT: Rectangle.ALIGN_RIGHT
                //});
            }
            details.CompleteRow();
        });

        pdfTable.AddCell(details);

    }

    public virtual void exportFooter(DataTable dtFooter)
    {
        // 表尾資訊
        if (dtFooter != null)
        {
            PdfPTable footer = new PdfPTable(dtFooter.Columns.Count); //(new float[] { 0.15f , 0.95f });
            footer.DefaultCell.Border = Rectangle.NO_BORDER;
            footer.DefaultCell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
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

    public virtual string output()
    {
        pdfDoc.Add(pdfTable);
        pdfDoc.Close();

        // 動態建立內嵌框架，以輸出文件內容
        GenerateIFrameToLoadDocument(
            // 建立防竄改的文件下載網址
            Utils.CreateTamperProofDownloadURL(filename)
            );

        return filename;
    }

    #endregion

}
