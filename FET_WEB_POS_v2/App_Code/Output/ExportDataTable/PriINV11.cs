using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;
using System.Configuration;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;

/// <summary>
/// PriECard 的摘要描述
/// </summary>
public class PriINV11 : BasePage
{
    protected Font smallFont;
    protected Font largerFont;
    protected Font RedsmallFont;
    protected Document pdfDoc;
    protected PdfPTable pdfTable;  //組成整體樣式
    protected PdfPTable details;   //組成明細樣式

    protected string filename;
    protected PdfWriter writer;

    //組表頭
    public PdfPTable getTitleTb(string Title, DataTable dtHeader, BaseFont bfChinese)
    {
        PdfPTable TitleTb = new PdfPTable(new float[] { 1 });
        TitleTb.TotalWidth = 500f;//設定Table 寬度
        TitleTb.LockedWidth = true;

        float setFontSize = 12f; //設定字型大小
        float setFontSize2 = 9f; //設定字型大小

        //代收款專用繳款證明(XX聯)
        PdfPCell Title_C1 = new PdfPCell(new Phrase(Title, new Font(bfChinese, setFontSize, Font.BOLD)));
        // HeaderTitle_C12.FixedHeight = 30.0f; //設定欄位高度
        Title_C1.Border = Rectangle.NO_BORDER; //去框架格線
        Title_C1.HorizontalAlignment = Element.ALIGN_LEFT; //字置左
        Title_C1.VerticalAlignment = Element.ALIGN_MIDDLE; //水平置中   
        Title_C1.BackgroundColor = BaseColor.GRAY;
        TitleTb.AddCell(Title_C1); //將資料加入Table
        for (int i = 0; i < dtHeader.Rows.Count; i++)
        {
            PdfPCell Title_C2 = new PdfPCell(new Phrase(dtHeader.Rows[i][0].ToString(), new Font(bfChinese, setFontSize2, Font.NORMAL)));
            // HeaderTitle_C12.FixedHeight = 30.0f; //設定欄位高度
            Title_C2.Border = Rectangle.NO_BORDER; //去框架格線
            Title_C2.HorizontalAlignment = Element.ALIGN_LEFT; //字置左
            Title_C2.VerticalAlignment = Element.ALIGN_MIDDLE; //水平置中   
            TitleTb.AddCell(Title_C2); //將資料加入Table
        }

        return TitleTb;
    }

    //組表頭
    public PdfPTable getHeaderTb(DataTable dtData, BaseFont bfChinese)
    {
        PdfPTable HeaderTb = new PdfPTable(dtData.Columns.Count);
        HeaderTb.TotalWidth = 500f;//設定Table 寬度
        HeaderTb.LockedWidth = true;


        //欄位標題
        List<string> headerCols = new List<string>();
        int c = 0;
        foreach (DataColumn dc in dtData.Columns)
        {
            headerCols.Add(dc.ColumnName);
            c++;
        }

        // Creates a phrase for a new line.
        PdfPCell clSpaceNull = new PdfPCell();
        clSpaceNull.Border = PdfPCell.NO_BORDER;
        clSpaceNull.Colspan = headerCols.Count;
        HeaderTb.AddCell(clSpaceNull);

        for (int i = 0; i < 4; i++)
        {
            PdfPCell clSpace = new PdfPCell();
            clSpace.BackgroundColor = BaseColor.PINK;
            clSpace.HorizontalAlignment = Rectangle.ALIGN_CENTER;

            if (i <= 2)
            {
                clSpace.Phrase = new Paragraph(headerCols[i], smallFont);
                clSpace.Rowspan = 2;
                HeaderTb.AddCell(clSpace);
            }
            else
            {
                Font BoldsmllFont = ChineseFontFactory.Create("細明體", 12f, Font.BOLD);

                clSpace.Phrase = new Paragraph("盤點數量", BoldsmllFont);
                clSpace.Colspan = 4;
                HeaderTb.AddCell(clSpace);
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
                    HeaderTb.AddCell(clSpace2);
                }
            }
        }
        return HeaderTb;
    }

    public PdfPTable GetFooterTb(DataTable dtFooter, BaseFont bfChinese)
    {
        PdfPTable footer = new PdfPTable(dtFooter.Columns.Count);
        // 表尾資訊
        if (dtFooter != null)
        {

            footer.DefaultCell.Border = Rectangle.NO_BORDER;
            footer.DefaultCell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            foreach (DataRow drFooterer in dtFooter.Rows)
            {
                for (int i = 0; i <= dtFooter.Columns.Count - 1; i++)
                {
                    footer.AddCell(new Paragraph(drFooterer[i].ToString(), smallFont));
                }
            }

        }
        return footer;
    }

    public PdfPTable getBodyTb(List<string> DataList, bool generateImg, BaseFont bfChinese)
    {
        PdfPTable BodyTb = new PdfPTable(new float[] { 1, 1 });
        BodyTb.TotalWidth = 500f;//設定Table 寬度
        BodyTb.LockedWidth = true;
        float setFontSize = 12f; //設定字型大小

        //空白欄
        PdfPCell S1 = new PdfPCell(new Paragraph("　", new Font(bfChinese, setFontSize, Font.NORMAL)));
        S1.Border = Rectangle.NO_BORDER; //去框架格線

        BodyTb.AddCell(S1); //將空白加入Table
        BodyTb.AddCell(S1); //將空白加入Table

        PdfPCell Body_C11 = new PdfPCell(new Phrase("交易日期：" + DataList[1], new Font(bfChinese, setFontSize, Font.NORMAL)));
        Body_C11.HorizontalAlignment = Element.ALIGN_LEFT; //字靠左
        Body_C11.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
        Body_C11.Border = Rectangle.NO_BORDER; //去框架格線
        BodyTb.AddCell(Body_C11); //將資料加入Table

        PdfPCell Body_C12 = new PdfPCell(new Phrase("門市代碼：" + DataList[2], new Font(bfChinese, setFontSize, Font.NORMAL)));
        Body_C12.HorizontalAlignment = Element.ALIGN_LEFT; //字靠左
        Body_C12.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
        Body_C12.Border = Rectangle.NO_BORDER; //去框架格線
        BodyTb.AddCell(Body_C12); //將資料加入Table

        PdfPCell Body_C21 = new PdfPCell(new Phrase("機　　號：" + DataList[3], new Font(bfChinese, setFontSize, Font.NORMAL)));
        Body_C21.HorizontalAlignment = Element.ALIGN_LEFT; //字靠左
        Body_C21.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
        Body_C21.Border = Rectangle.NO_BORDER; //去框架格線
        BodyTb.AddCell(Body_C21); //將資料加入Table

        PdfPCell Body_C22 = new PdfPCell(new Phrase("序    號：" + DataList[4], new Font(bfChinese, setFontSize, Font.NORMAL)));
        Body_C22.HorizontalAlignment = Element.ALIGN_LEFT; //字靠左
        Body_C22.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
        Body_C22.Border = Rectangle.NO_BORDER; //去框架格線
        BodyTb.AddCell(Body_C22); //將資料加入Table

        PdfPCell Body_C31 = new PdfPCell(new Phrase("代收項目：" + DataList[5], new Font(bfChinese, setFontSize, Font.NORMAL)));
        Body_C31.HorizontalAlignment = Element.ALIGN_LEFT; //字靠左
        Body_C31.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
        Body_C31.Border = Rectangle.NO_BORDER; //去框架格線
        BodyTb.AddCell(Body_C31); //將資料加入Table

        PdfPCell Body_C32 = new PdfPCell(new Phrase("加值金額：" + DataList[6], new Font(bfChinese, setFontSize, Font.NORMAL)));
        Body_C32.HorizontalAlignment = Element.ALIGN_LEFT; //字靠左
        Body_C32.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
        Body_C32.Border = Rectangle.NO_BORDER; //去框架格線
        BodyTb.AddCell(Body_C32); //將資料加入Table

        BodyTb.AddCell(S1); //將空白加入Table
        BodyTb.AddCell(S1); //將空白加入Table

        PdfPCell Body_C41 = new PdfPCell(new Phrase("加值卡卡號：" + DataList[7], new Font(bfChinese, setFontSize, Font.NORMAL)));
        Body_C41.HorizontalAlignment = Element.ALIGN_LEFT; //字靠左
        Body_C41.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
        Body_C41.Border = Rectangle.NO_BORDER; //去框架格線
        BodyTb.AddCell(Body_C41); //將資料加入Table

        BodyTb.AddCell(S1); //將空白加入Table

        PdfPCell Body_C51 = new PdfPCell(new Phrase("加值前金額：" + DataList[8], new Font(bfChinese, setFontSize, Font.NORMAL)));
        Body_C51.HorizontalAlignment = Element.ALIGN_LEFT; //字靠左
        Body_C51.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
        Body_C51.Border = Rectangle.NO_BORDER; //去框架格線
        BodyTb.AddCell(Body_C51); //將資料加入Table

        BodyTb.AddCell(S1); //將空白加入Table

        PdfPCell Body_C61 = new PdfPCell(new Phrase("加值金額：" + DataList[9], new Font(bfChinese, setFontSize, Font.NORMAL)));
        Body_C61.HorizontalAlignment = Element.ALIGN_LEFT; //字靠左
        Body_C61.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
        Body_C61.Border = Rectangle.NO_BORDER; //去框架格線
        BodyTb.AddCell(Body_C61); //將資料加入Table

        BodyTb.AddCell(S1); //將空白加入Table

        PdfPCell Body_C71 = new PdfPCell(new Phrase("加值後金額：" + DataList[10], new Font(bfChinese, setFontSize, Font.NORMAL)));
        Body_C71.HorizontalAlignment = Element.ALIGN_LEFT; //字靠左
        Body_C71.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
        Body_C71.Border = Rectangle.NO_BORDER; //去框架格線
        BodyTb.AddCell(Body_C71); //將資料加入Table

        BodyTb.AddCell(S1); //將空白加入Table
        BodyTb.AddCell(S1); //將空白加入Table


        PdfPCell Body_C81 = new PdfPCell(new Phrase("銷售專員：" + DataList[11], new Font(bfChinese, setFontSize, Font.NORMAL)));
        Body_C81.HorizontalAlignment = Element.ALIGN_RIGHT; //字靠右
        Body_C81.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
        Body_C81.Border = Rectangle.NO_BORDER; //去框架格線
        BodyTb.AddCell(Body_C81); //將資料加入Table

        PdfPCell Body_C91 = new PdfPCell(new Phrase("備註：查詢繳費狀況，請攜帶此單據", new Font(bfChinese, setFontSize, Font.NORMAL)));
        Body_C91.HorizontalAlignment = Element.ALIGN_LEFT; //字靠左
        Body_C91.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
        Body_C91.Border = Rectangle.NO_BORDER; //去框架格線
        BodyTb.AddCell(Body_C91); //將資料加入Table
        if (generateImg)
        {
            //圖片     
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images") + "\\PriReceipt.jpg");
            jpg.SetAbsolutePosition(0, 0);
            jpg.ScalePercent(30f);
            //PdfPCell tc = new PdfPCell(jpg,true);
            PdfPCell Body_C92 = new PdfPCell(jpg, true);
            Body_C92.Border = Rectangle.NO_BORDER; //去框架格線
            Body_C92.PaddingTop = 1;
            Body_C92.PaddingLeft = 1;
            BodyTb.AddCell(Body_C92);

            //補空白行
            for (int i = 0; i < 12; i++)
                BodyTb.AddCell(S1); //將空白加入Table
        }
        else
            BodyTb.AddCell(S1); //將空白加入Table



        return BodyTb;
    }


    public string generateReceipt(string TitleName, DataTable dtHeader, DataTable dtData, DataTable dtFooter, string strPrinterName)
    {

        smallFont = ChineseFontFactory.Create("細明體", 9f, Font.NORMAL);
        largerFont = ChineseFontFactory.Create("細明體", 12f, Font.BOLD);
        RedsmallFont = ChineseFontFactory.Create("細明體", 9f, Font.NORMAL, BaseColor.RED);
        MemoryStream Memory = new MemoryStream();
        //string printerName = @"\\192.168.8.100\HP LaserJet 3055 PCL5"; //"\\192.168.8.1\Brother MFC9600/9870 Series";
        //string printerName = @"HP LaserJet 3050 Series PCL 5e"; //印表機和傳真 
        //string printerName = @"\\192.168.8.100\HP LaserJet 3050 Series PCL 6"; //"\\192.168.8.1\Brother MFC9600/9870 Series";
        // string printerName = ConfigurationManager.AppSettings["Receipt_PDFPrinterName"].ToString();//web.config中設定
        //string printerName = ConfigurationManager.AppSettings["PDFPrinterName"].ToString();//web.config中設定
        //字型設定
        BaseFont bfChinese = BaseFont.CreateFont(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\..\Fonts\simhei.ttf"
                             , BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        Font ChFont_HeaderTitle_C1 = new Font(Font.FontFamily.HELVETICA, 20f, Font.BOLD);

        //設定PDF PageSize 及 Margin left,right,top,bottom
        pdfDoc = new Document(PageSize.A4, 10f, 10f, 20f, 20f);

        filename = Guid.NewGuid().ToString() + ".pdf";

        FileStream stream = new FileStream(Path.Combine(Server.MapPath("~/Downloads"), filename), FileMode.Create);
        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
        pdfDoc.Open();

        //string printerName1 = ConfigurationManager.AppSettings["Receipt_PDFPrinterName"].ToString();//web.config中設定
        // 加入自動列印指令碼

        AddPrintAction(writer, strPrinterName);
       // writer.AddJavaScript("this.print(false);", true);

        PdfPTable TitleTb = getTitleTb(TitleName, dtHeader, bfChinese);//頁簽頭
        PdfPTable HeaderTb = getHeaderTb(dtData, bfChinese); //表頭
        PdfPTable FooterTb = GetFooterTb(dtFooter, bfChinese); //表尾

        int RowsCount = dtData.Rows.Count;

        //組表身
        for (int i = 0; i < RowsCount; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                try
                {
                    long Number = Convert.ToInt64(dtData.Rows[i][j].ToString());
                    HeaderTb.AddCell(new PdfPCell(new Paragraph(dtData.Rows[i][j].ToString(), smallFont))
                    {
                        HorizontalAlignment = Rectangle.ALIGN_RIGHT
                    });
                }
                catch
                {
                    HeaderTb.AddCell(new PdfPCell(new Paragraph(dtData.Rows[i][j].ToString(), smallFont))
                    {
                        HorizontalAlignment = Rectangle.ALIGN_LEFT
                    });
                }

            }

            if ((i + 1) % 30 == 0 && i != 0) //30筆換頁
            {
                pdfDoc.Add(TitleTb); //將頁簽頭加入主頁簽
                pdfDoc.Add(HeaderTb); //將資料表頭與資料加入主頁簽 
                pdfDoc.Add(FooterTb); //將資料表尾加入主頁簽 
                pdfDoc.NewPage(); //換頁
                HeaderTb.Rows.Clear(); //清除已組過的資料

                HeaderTb = getHeaderTb(dtData, bfChinese);

            }
        }

        //**2011/04/29 Tina：沒有資料，也要列印出頁面架構
        if (RowsCount == 0)
        {
            pdfDoc.Add(TitleTb); //將頁簽頭加入主頁簽
            pdfDoc.Add(HeaderTb); //將資料表頭與資料加入主頁簽 
            pdfDoc.Add(FooterTb); //將資料表尾加入主頁簽 
        }
        else
        {
            //**2011/04/29 Tina： 不滿30筆筆數的頁面列印，如果不加此判斷的話，若筆數剛好整除30，會多一頁出來。
            if (RowsCount % 30 > 0)
            {
                pdfDoc.Add(TitleTb); //將頁簽頭加入主頁簽
                pdfDoc.Add(HeaderTb); //將資料表頭與資料加入主頁簽 
                pdfDoc.Add(FooterTb); //將資料表尾加入主頁簽 
            }
        }

        //文件關閉
        pdfDoc.Close();

        // 動態建立內嵌框架，以輸出文件內容
        GenerateIFrameToLoadDocument(
            // 建立防竄改的文件下載網址
            Utils.CreateTamperProofDownloadURL(filename)
            );

        return filename;

    }

    /// <summary>
    /// 加入自動列印指令碼
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="printerName"></param>
    public static void AddPrintAction(PdfWriter writer, string printerName)
    {
        string strJavaScript = @"var pp = this.getPrintParams();
                                    //pp.interactive = pp.constants.interactionLevel.silent;
                                    pp.interactive = pp.constants.interactionLevel.automatic
                                    pp.printerName = '" + printerName.Replace(@"\", @"\\") + @"';
                                    pp.pageHandling = pp.constants.handling.none;         
                                    var fv = pp.constants.flagValues;
                                    pp.flags |= fv.setPageSize;
                                    pp.flags |= (fv.suppressCenter | fv.suppressRotate);
                                    this.print(pp);";

        PdfAction js = PdfAction.JavaScript(strJavaScript, writer);
        writer.AddJavaScript(js);
    }
}
