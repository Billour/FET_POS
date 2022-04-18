using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;


public partial class Receipt9 : System.Web.UI.Page
{
    protected Font s05;
    protected Font s06;
    protected Font s07;
    protected Font s08;
    protected Font s09;
    protected Font s10;
    protected Font s105;
    protected Font s11;
    protected Font s14;
    
    protected Font largerFont;
    protected Font RedsmallFont;
    protected Document pdfDoc;
    protected PdfPTable pdfTable;  //組成整體樣式
    protected PdfPTable details;   //組成明細樣式
    protected DataTable dtData;    //前端傳來的資料表
    protected string filename;
    protected PdfWriter writer;
    OleDbConnection gCon;
    string sCon;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        FontFactory.Register(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\..\Fonts\simhei.ttf");
        FontFactory.Register(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\..\Fonts\kaiu.ttf");
        s05 = ChineseFontFactory.Create("標楷體", 5f, Font.NORMAL);
        s06 = ChineseFontFactory.Create("標楷體", 6f, Font.NORMAL);
        s07 = ChineseFontFactory.Create("標楷體", 7f, Font.NORMAL);
        s08 = ChineseFontFactory.Create("標楷體", 8f, Font.NORMAL);
        s09 = ChineseFontFactory.Create("標楷體", 9f, Font.NORMAL);
        s10 = ChineseFontFactory.Create("標楷體", 10f, Font.NORMAL);
        s105 = ChineseFontFactory.Create("標楷體", 10.5f, Font.NORMAL);
        s11 = ChineseFontFactory.Create("標楷體", 11f, Font.NORMAL);
        s14 = ChineseFontFactory.Create("標楷體", 14f, Font.NORMAL);

        largerFont = ChineseFontFactory.Create("標楷體", 12f, Font.BOLD);
        RedsmallFont = ChineseFontFactory.Create("標楷體", 9f, Font.NORMAL, BaseColor.RED);
        #region 連DB區塊
        //sCon="Driver={Microsoft Access Driver (*.mdb)};Dbq=G:\\RAY\\PDF_WEB\\App_Data\\Sales.mdb;Uid=Admin;Pwd=;";
        //gCon = new OleDbConnection(sCon);
        //gCon.Close();
        //string printerName = ConfigurationManager.AppSettings["PDFPrinterName"].ToString();//web.config中設定
        #endregion

    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        Paragraph p1;
        PdfPCell tc;
        pdfDoc = new Document(PageSize.A4, 0f, 0f, 24f, 10f);
        filename = Guid.NewGuid().ToString() + ".pdf";
        FileStream stream = new FileStream(Path.Combine(Server.MapPath("~/Downloads"), filename), FileMode.Create);
        writer = PdfWriter.GetInstance(pdfDoc, stream);
        pdfDoc.Open();

        //第一聯
        add0(pdfDoc);

        //畫虛線
        PdfContentByte cb = writer.DirectContent;
        for (int i = 0; i < pdfDoc.Right - 10; i += 4)
        {
            //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfTable.TotalHeight - 55);
            //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfTable.TotalHeight - 55);
            //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfDoc.Top / 2);
            //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfDoc.Top / 2);
            //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfTable.TotalHeight + 22.84f);
            //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfTable.TotalHeight + 22.84f);
            cb.MoveTo(pdfDoc.Left + i, 420.945f);
            cb.LineTo(pdfDoc.Left + i + 1, 420.945f);
        }
        cb.Stroke();


        //換頁
        pdfDoc.NewPage();

        //第二聯
        add1(pdfDoc);

        ////調整版面用
        //p1 = new Paragraph("");
        //tc = CF(new PdfPCell(p1), "B", 0);
        //tc.FixedHeight = 43.2f;
        //pdfTable.AddCell(tc);

        //pdfDoc.Add(pdfTable);

        ////畫虛線

        //for (int i = 0; i < pdfDoc.Right - 10; i += 4)
        //{
        //    //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfTable.TotalHeight - 55);
        //    //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfTable.TotalHeight - 55);
        //    cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfDoc.Top / 2);
        //    cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfDoc.Top / 2);
        //    //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfTable.TotalHeight+22.84f );
        //    //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfTable.TotalHeight+22.84f);
        //}
        ////cb.Stroke();

        //產生第三聯
        //add2(pdfDoc);
        add3(pdfDoc);

        //產生註記欄
        addMarkTable1(pdfDoc);
        addMarkTable2(pdfDoc);

        pdfDoc.Close();
    }

    private void add0(Document pdfDoc)
    {
        //pdfTable = new PdfPTable(new float[] { 527.5f });
        //外框(pdfTable)，1欄
        PdfPTable pdfTable = new PdfPTable(1);
        pdfTable.DefaultCell.Border = 0;
        pdfTable.SetTotalWidth(new float[] { 532f });
        pdfTable.LockedWidth = true;
        pdfTable.SpacingBefore = 0;
        pdfTable.SpacingAfter = 0;

        //標題框(tbTitle)，3欄(圖片，標題，留白
        PdfPTable tbTitle = new PdfPTable(new float[] { 1f, 3f, 1f });
        //第1欄，圖片
        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images") + "\\Title01.JPG");
        jpg.SetAbsolutePosition(0, 0);
        jpg.ScalePercent(30f);
        //PdfPCell tc = new PdfPCell(jpg,true);
        PdfPCell tc = new PdfPCell(new Paragraph(""));//USER要求先拿掉圖片，欲進行套版比對。
        tc.Border = 0;
        tc.PaddingTop = 1;
        tc.PaddingLeft = 1;
        tbTitle.AddCell(CF(tc, "B", 0));

        //第2欄，標題
        PdfPTable tbTemp = new PdfPTable(1);
        Paragraph p1;
        p1 = new Paragraph("吸引力綜合百貨股份有限公司信義分公司", s14);
        tc = new PdfPCell(p1);
        tc.FixedHeight = 27f;
        tbTemp.AddCell(CF(CF(tc, "H", Rectangle.ALIGN_CENTER), "B", 0));
        p1 = new Paragraph("電子計算機統一發票", s11);
        tc = new PdfPCell(p1);
        tbTemp.AddCell(CF(CF(tc, "H", Rectangle.ALIGN_CENTER), "B", 0));
        //p1 = new Paragraph("中華民國" + DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日", s09);
        //p1 = new Paragraph("中 華 民 國    年   月   日", s11);
        p1 = new Paragraph("中 華 民 國", s11);
        p1.Add("    ");
        p1.Add("年");
        p1.Add("    ");
        p1.Add("月");
        p1.Add("    ");
        p1.Add("日");
        tc = CF(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER), "B", 0);
        tc.PaddingTop = 6f;
        tc.FixedHeight = 20f;
        tbTemp.AddCell(tc);
        tbTitle.AddCell(CF(new PdfPCell(tbTemp), "B", 0));

        //第3欄，留白
        p1 = new Paragraph("", s09);
        tbTitle.AddCell(CF(new PdfPCell(p1), "B", 0));

        //外框附加標題框
        pdfTable.AddCell(tbTitle);

        //內容主框2欄，分左右
        PdfPTable tbMaster = new PdfPTable(new float[] { 2.05f, 2f });
        p1 = new Paragraph("發票號碼:", s105);//左
        tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1 = new Paragraph("", s105);//右
        tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1 = new Paragraph("買 受 人:", s105);//左
        tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1 = new Paragraph("", s105);//右
        tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1 = new Paragraph("統一編號:", s105);//左
        tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1 = new Paragraph("檢查號碼:", s105);//右
        tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1 = new Paragraph("地    址:", s105);//左
        tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1 = new Paragraph("", s105);//右
        tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));

        //外框附加內容主框
        pdfTable.AddCell(tbMaster);

        //內容副框2欄，分左(細項、小計、合計)，右(備註、發票章)
        //PdfPTable tbDetail = new PdfPTable(new float[] { 7f, 2f });
        PdfPTable tbDetail = new PdfPTable(2);
        //tbDetail.SetTotalWidth(new float[] { 351.46f,174f });
        tbDetail.SetTotalWidth(new float[] { 385.48f, 141.76f });
        tbDetail.SpacingBefore = 0;
        tbDetail.SpacingAfter = 0;
        tbDetail.LockedWidth = true;

        //左內容副框，細項框4欄
        //PdfPTable tbInner02 = new PdfPTable(new float[] { 2f, 1f, 1f, 1f });
        PdfPTable tbInner02 = new PdfPTable(4);
        //tbInner02.SetTotalWidth(new float[] { 161.57f, 68.03f, 70.87f, 86f });
        tbInner02.SetTotalWidth(new float[] { 161.57f, 68.03f, 69.88f, 86f });
        tbInner02.LockedWidth = true;
        //左內容副框:細項框標題列
        p1 = new Paragraph("品               名", s10);
        tbInner02.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        p1 = new Paragraph("數       量", s10);
        tbInner02.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        p1 = new Paragraph("單       價", s10);
        tbInner02.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        p1 = new Paragraph("金       額", s10);
        tbInner02.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        //左內容副框:細項框內容列
        p1 = new Paragraph("", s09);
        tbInner02.AddCell(CF(new PdfPCell(p1), "HT", 127.56f));
        tbInner02.AddCell(new PdfPCell(p1));
        tbInner02.AddCell(new PdfPCell(p1));
        tbInner02.AddCell(new PdfPCell(p1));

        //左內容副框，小計框8欄
        //PdfPTable tbInner04 = new PdfPTable(new float[] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1.75f });
        PdfPTable tbInner04 = new PdfPTable(8);
        float[] widths = new float[] { 46.97f, 36.4f, 46.85f, 38.27f, 47.7f, 38.77f, 45f, 86.83f };
        tbInner04.SpacingAfter = 0;
        tbInner04.SpacingBefore = 0;
        tbInner04.SetTotalWidth(widths);
        tbInner04.LockedWidth = true;


        p1 = new Paragraph("銷          售           額        合          計", s10);
        p1.Leading = 0;

        //PdfPCell tc2 = CF(CF(CF(CF(new PdfPCell(p1), "CS", 7), "B", 2), "H", Rectangle.ALIGN_CENTER), "V", Element.ALIGN_MIDDLE);
        PdfPCell tc2 = CF(CF(CF(new PdfPCell(p1), "CS", 7), "H", Rectangle.ALIGN_CENTER), "V", Element.ALIGN_MIDDLE);
        tc2.PaddingTop = 0;
        tc2.PaddingBottom = 0;
        tc2.PaddingLeft = 0;
        tc2.PaddingLeft = 0;
        tc2.FixedHeight = 12f;
        tbInner04.AddCell(tc2);//銷售額合計Title
        p1 = new Paragraph("", s10);
        tc = CF(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER), "V", Rectangle.ALIGN_CENTER);
        //tc.FixedHeight = 14.17f;
        tbInner04.AddCell(tc);//銷售額合計Value
        p1 = new Paragraph("營業稅", s09);
        tc = CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER);
        tc.FixedHeight = 14.17f;
        tbInner04.AddCell(tc);//營業稅Title
        p1 = new Paragraph("應稅", s09);
        tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//應稅Title
        p1 = new Paragraph("", s09);
        tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//應稅value
        p1 = new Paragraph("零稅率", s09);
        tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//零稅率Title
        p1 = new Paragraph("", s09);
        tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//零稅率Value
        p1 = new Paragraph("免稅", s09);
        tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//免稅Title
        tbInner04.AddCell(new Paragraph("", s09));//免稅Value
        tbInner04.AddCell(new Paragraph("", s09));//營業稅Value

        tc2 = new PdfPCell(new Paragraph("總                                             額", s10));
        tc2.Colspan = 7;
        tc2.FixedHeight = 14.17f;
        tbInner04.AddCell(CF(tc2, "H", Element.ALIGN_CENTER));//總計Title
        tbInner04.AddCell(new Paragraph("", s09));//總計Value
        //        append to Outer Table
        tc = new PdfPCell(tbInner04);
        tc.BorderWidth = 1;
        tc.Colspan = 4;
        tbInner02.AddCell(tc);

        //tc = new PdfPCell(new Paragraph( "總計新台幣 " + Utils.GF_Converts(0)
        //                               + "仟 " + Utils.GF_Converts(0 )
        //                               + "佰 " + Utils.GF_Converts(0 )
        //                               + "拾 " + Utils.GF_Converts(1) + Utils.GF_Converts(0) + Utils.GF_Converts(0)
        //                               + "萬 " + Utils.GF_Converts(9) 
        //                               + "仟 " + Utils.GF_Converts(0) 
        //                               + "佰 " + Utils.GF_Converts(0 )
        //                               + "拾 " + Utils.GF_Converts(0 )
        //                               + " 元整",s09));
        PdfPTable tTemp = new PdfPTable(new float[] { 1.9f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f });
        float fPadding = 6f;
        p1 = new Paragraph("總計新台幣", s10);
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.FixedHeight = 25.51f;
        tc.HorizontalAlignment = Element.ALIGN_LEFT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);
        p1 = new Paragraph(new Chunk("", s10));
        p1.Add(new Chunk("仟", s10));
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);
        //p1 = new Paragraph("一二三" + "佰", s09);
        p1 = new Paragraph(new Chunk("", s10));
        p1.Add(new Chunk("佰", s10));
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);
        //p1 = new Paragraph("一二三" + "拾", s09);
        p1 = new Paragraph(new Chunk("", s10));
        p1.Add(new Chunk("拾", s10));
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);
        //p1 = new Paragraph("一二三" + "萬", s09);
        p1 = new Paragraph(new Chunk("", s10));
        p1.Add(new Chunk("萬", s10));
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);
        //p1 = new Paragraph("一二三" + "仟", s09);
        p1 = new Paragraph(new Chunk("", s07));
        p1.Add(new Chunk("仟", s10));
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);
        //p1 = new Paragraph("一二三" + "佰", s09);
        p1 = new Paragraph(new Chunk("", s10));
        p1.Add(new Chunk("佰", s10));
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);
        //p1 = new Paragraph("一二三" + "拾", s09);
        p1 = new Paragraph(new Chunk("", s07));
        p1.Add(new Chunk("拾", s10));
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);
        //p1 = new Paragraph("一二三" + "元整", s09);
        p1 = new Paragraph(new Chunk("", s10));
        p1.Add(new Chunk("元整", s10));
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);

        tc = new PdfPCell(tTemp);
        tc.Colspan = 4;
        //tc.BorderWidth = 0.5f;
        tbInner02.AddCell(tc);

        tc = new PdfPCell(tbInner02);
        tc.BorderWidthTop = 0.8f;
        tc.BorderWidthRight = 0.5f;
        tc.BorderWidthBottom = 0.8f;
        tc.BorderWidthLeft = 0.8f;
        tbDetail.AddCell(tc);


        //右內容副框，1欄
        PdfPTable tbInner03 = new PdfPTable(1);
        p1 = new Paragraph("備              註", s10);
        tbInner03.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        p1 = new Paragraph("", s09);
        tbInner03.AddCell(CF(new PdfPCell(p1), "HT", 90.71f));
        p1 = new Paragraph("營業人蓋用統一發票專用章", s10);
        tbInner03.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        //jpg = iTextSharp.text.Image.GetInstance("F:\\Title02.JPG");
        jpg = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images") + "\\Title03.JPG");
        jpg.SetAbsolutePosition(0, 0);

        jpg.ScalePercent(30f);

        tc = new PdfPCell(jpg, false);
        tc.PaddingTop = 3;
        tc.PaddingLeft = 25;
        tc.PaddingBottom = 1;
        tbInner03.AddCell(tc);

        //add to Outer Table
        tc = new PdfPCell(tbInner03);
        tc.BorderWidthTop = 0.8f;
        tc.BorderWidthRight = 0.8f;
        tc.BorderWidthBottom = 0.8f;
        tc.BorderWidthLeft = 0.5f;
        tbDetail.AddCell(tc);


        //add to Outer Table
        pdfTable.AddCell(tbDetail);

        //Footer
        PdfPTable tbFooter = new PdfPTable(1);
        //p1 = new Paragraph("※應稅、零稅率、免稅之銷售額應分別開立統一發票，並應於各該欄打「v」。"
        //                  + "                                                                 ", s07);
        //p1.Add(new Chunk("第一聯:存根聯", s08));
        //p1.Add(new Chunk("\n※依台北市國稅局大安分局 年 月 日         字第        號函核准使用。", s07));
        p1 = new Paragraph("※應稅、零稅率、免稅之銷售額應分別開立統一發票，並應於各該欄打「v」。"
                          + "                                                                 ", s07);
        p1.Add(new Chunk("第一聯:存根聯", s08));
        tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1= new Paragraph("本發票依    國稅局 年 月 日    字第        號函核准使用。", s07);
        tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));

        pdfTable.AddCell(CF(new PdfPCell(tbFooter), "B", 0));

        ////調整版面用
        p1 = new Paragraph("");
        tc = CF(new PdfPCell(p1), "B", 0);
        tc.FixedHeight = 43.2f;
        pdfTable.AddCell(tc);


        pdfDoc.Add(pdfTable);
    }

    private void add1(Document pdfDoc) 
    {
        //pdfTable = new PdfPTable(new float[] { 527.5f });
        //外框(pdfTable)，1欄
       PdfPTable pdfTable = new PdfPTable(1);
        pdfTable.DefaultCell.Border = 0;
        pdfTable.SetTotalWidth(new float[] { 532f });
        pdfTable.LockedWidth = true;
        pdfTable.SpacingBefore = 0;
        pdfTable.SpacingAfter = 0;

        //標題框(tbTitle)，3欄(圖片，標題，留白
        PdfPTable tbTitle = new PdfPTable(new float[] { 1f, 3f, 1f });
        //第1欄，圖片
        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images") + "\\Title01.JPG");
        jpg.SetAbsolutePosition(0, 0);
        jpg.ScalePercent(30f);
        //PdfPCell tc = new PdfPCell(jpg,true);
        PdfPCell tc = new PdfPCell(new Paragraph(""));//USER要求先拿掉圖片，欲進行套版比對。
        tc.Border = 0;
        tc.PaddingTop = 1;
        tc.PaddingLeft = 1;
        tbTitle.AddCell(CF(tc, "B", 0));
        
        //第2欄，標題
        PdfPTable tbTemp = new PdfPTable(1);
        Paragraph p1;
        p1 = new Paragraph("吸引力綜合百貨股份有限公司信義分公司", s14);
        tc = new PdfPCell(p1);
        tc.FixedHeight = 27f;
        tbTemp.AddCell(CF(CF(tc, "H", Rectangle.ALIGN_CENTER), "B", 0));
        p1 = new Paragraph("電子計算機統一發票", s11);
        tc = new PdfPCell(p1);
        tbTemp.AddCell(CF(CF(tc, "H", Rectangle.ALIGN_CENTER), "B", 0));
        //p1 = new Paragraph("中華民國" + DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日", s09);
        //p1 = new Paragraph("中 華 民 國    年   月   日", s11);
        p1 = new Paragraph("中 華 民 國",s11);
        p1.Add("    ");
        p1.Add("年");
        p1.Add("    ");
        p1.Add("月");
        p1.Add("    ");
        p1.Add("日");
        tc = CF(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER), "B", 0);
        tc.PaddingTop = 6f;
        tc.FixedHeight = 20f;
        tbTemp.AddCell(tc);
        tbTitle.AddCell(CF(new PdfPCell(tbTemp), "B", 0));

        //第3欄，留白
        p1 = new Paragraph("", s09);
        tbTitle.AddCell(CF(new PdfPCell(p1), "B", 0));

        //外框附加標題框
        pdfTable.AddCell(tbTitle);

        //內容主框2欄，分左右
        float fHeight=13f;
        PdfPTable tbMaster = new PdfPTable(new float[] { 2.05f, 2f });
        p1 = new Paragraph("發票號碼:", s105);//左
        tc = CF(new PdfPCell(p1), "B", 0);
        tc.FixedHeight = fHeight;
        tbMaster.AddCell(tc);
        p1 = new Paragraph("", s105);//右
        tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1 = new Paragraph("買 受 人:", s105);//左
        tc = CF(new PdfPCell(p1), "B", 0);
        tc.FixedHeight = fHeight;
        tbMaster.AddCell(tc);
        p1 = new Paragraph("", s105);//右
        tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1 = new Paragraph("統一編號:", s105);//左
        tc = CF(new PdfPCell(p1), "B", 0);
        tc.FixedHeight = fHeight;
        tbMaster.AddCell(tc);
        p1 = new Paragraph("檢查號碼:", s105);//右
        tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1 = new Paragraph("地    址:", s105);//左
        tc = CF(new PdfPCell(p1), "B", 0);
        tc.FixedHeight = fHeight;
        tbMaster.AddCell(tc);
        p1 = new Paragraph("", s105);//右
        tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));

        //外框附加內容主框
        pdfTable.AddCell(tbMaster);

        //內容副框2欄，分左(細項、小計、合計)，右(備註、發票章)
        //PdfPTable tbDetail = new PdfPTable(new float[] { 7f, 2f });
        PdfPTable tbDetail = new PdfPTable(2);
        //tbDetail.SetTotalWidth(new float[] { 351.46f,174f });
        tbDetail.SetTotalWidth(new float[] { 385.48f, 141.76f });
        tbDetail.SpacingBefore = 0;
        tbDetail.SpacingAfter = 0;
        tbDetail.LockedWidth = true;

        //左內容副框，細項框4欄
        //PdfPTable tbInner02 = new PdfPTable(new float[] { 2f, 1f, 1f, 1f });
        PdfPTable tbInner02 = new PdfPTable(4);
        //tbInner02.SetTotalWidth(new float[] { 161.57f, 68.03f, 70.87f, 86f });
        tbInner02.SetTotalWidth(new float[] { 161.57f, 68.03f, 69.88f, 86f });
        tbInner02.LockedWidth = true;
        //左內容副框:細項框標題列
        p1 = new Paragraph("品               名", s10);
        tbInner02.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        p1 = new Paragraph("數       量", s10);
        tbInner02.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        p1 = new Paragraph("單       價", s10);
        tbInner02.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        p1 = new Paragraph("金       額", s10);
        tbInner02.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        //左內容副框:細項框內容列
        p1 = new Paragraph("", s09);
        tbInner02.AddCell(CF(new PdfPCell(p1), "HT", 127.56f));
        tbInner02.AddCell(new PdfPCell(p1));
        tbInner02.AddCell(new PdfPCell(p1));
        tbInner02.AddCell(new PdfPCell(p1));

        //左內容副框，小計框8欄
        //PdfPTable tbInner04 = new PdfPTable(new float[] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1.75f });
        PdfPTable tbInner04 = new PdfPTable(8);
        float[] widths=new float[] { 46.97f, 36.4f, 46.85f, 38.27f, 47.7f, 38.77f, 45f, 86.83f };
        tbInner04.SpacingAfter = 0;
        tbInner04.SpacingBefore = 0;
        tbInner04.SetTotalWidth(widths);
        tbInner04.LockedWidth = true;


        p1 = new Paragraph("銷          售           額        合          計", s10);
        p1.Leading = 0;
        
        //PdfPCell tc2 = CF(CF(CF(CF(new PdfPCell(p1), "CS", 7), "B", 2), "H", Rectangle.ALIGN_CENTER), "V", Element.ALIGN_MIDDLE);
        PdfPCell tc2 = CF(CF(CF(new PdfPCell(p1), "CS", 7), "H", Rectangle.ALIGN_CENTER), "V", Element.ALIGN_MIDDLE);
        tc2.PaddingTop = 0;
        tc2.PaddingBottom = 0;
        tc2.PaddingLeft = 0;
        tc2.PaddingLeft = 0;
        tc2.FixedHeight = 12f;
        tbInner04.AddCell(tc2);//銷售額合計Title
        p1 = new Paragraph("", s10);
        tc = CF(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER), "V", Rectangle.ALIGN_CENTER);
        //tc.FixedHeight = 14.17f;
        tbInner04.AddCell(tc);//銷售額合計Value
        p1 = new Paragraph("營業稅", s09);
        tc = CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER);
        tc.FixedHeight = 14.17f;
        tbInner04.AddCell(tc);//營業稅Title
        p1 = new Paragraph("應稅", s09);
        tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//應稅Title
        p1 = new Paragraph("", s09);
        tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//應稅value
        p1 = new Paragraph("零稅率", s09);
        tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//零稅率Title
        p1 = new Paragraph("", s09);
        tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//零稅率Value
        p1 = new Paragraph("免稅", s09);
        tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//免稅Title
        tbInner04.AddCell(new Paragraph("", s09));//免稅Value
        tbInner04.AddCell(new Paragraph("", s09));//營業稅Value

        tc2 = new PdfPCell(new Paragraph("總                                             額", s10));
        tc2.Colspan = 7;
        tc2.FixedHeight = 14.17f;
        tbInner04.AddCell(CF(tc2,"H",Element.ALIGN_CENTER));//總計Title
        tbInner04.AddCell(new Paragraph("", s09));//總計Value
        //        append to Outer Table
        tc = new PdfPCell(tbInner04);
        tc.BorderWidth = 1;
        tc.Colspan = 4;
        tbInner02.AddCell(tc);

        //tc = new PdfPCell(new Paragraph( "總計新台幣 " + Utils.GF_Converts(0)
        //                               + "仟 " + Utils.GF_Converts(0 )
        //                               + "佰 " + Utils.GF_Converts(0 )
        //                               + "拾 " + Utils.GF_Converts(1) + Utils.GF_Converts(0) + Utils.GF_Converts(0)
        //                               + "萬 " + Utils.GF_Converts(9) 
        //                               + "仟 " + Utils.GF_Converts(0) 
        //                               + "佰 " + Utils.GF_Converts(0 )
        //                               + "拾 " + Utils.GF_Converts(0 )
        //                               + " 元整",s09));
        PdfPTable tTemp = new PdfPTable(new float[] { 1.9f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f });
        float fPadding = 6f;
        p1 = new Paragraph("總計新台幣", s10);
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.FixedHeight = 25.51f;
        tc.HorizontalAlignment = Element.ALIGN_LEFT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);
        p1 = new Paragraph(new Chunk("", s10));
        p1.Add(new Chunk("仟", s10));
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);
        //p1 = new Paragraph("一二三" + "佰", s09);
        p1 = new Paragraph(new Chunk("", s10));
        p1.Add(new Chunk("佰", s10));
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);
        //p1 = new Paragraph("一二三" + "拾", s09);
        p1 = new Paragraph(new Chunk("", s10));
        p1.Add(new Chunk("拾", s10));
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);
        //p1 = new Paragraph("一二三" + "萬", s09);
        p1 = new Paragraph(new Chunk("", s10));
        p1.Add(new Chunk("萬", s10));
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);
        //p1 = new Paragraph("一二三" + "仟", s09);
        p1 = new Paragraph(new Chunk("", s07));
        p1.Add(new Chunk("仟", s10));
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);
        //p1 = new Paragraph("一二三" + "佰", s09);
        p1 = new Paragraph(new Chunk("", s10));
        p1.Add(new Chunk("佰", s10));
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);
        //p1 = new Paragraph("一二三" + "拾", s09);
        p1 = new Paragraph(new Chunk("", s07));
        p1.Add(new Chunk("拾", s10));
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);
        //p1 = new Paragraph("一二三" + "元整", s09);
        p1 = new Paragraph(new Chunk("", s10));
        p1.Add(new Chunk("元整", s10));
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);

        tc = new PdfPCell(tTemp);
        tc.Colspan = 4;
        //tc.BorderWidth = 0.5f;
        tbInner02.AddCell(tc);

        //tc = CF(new PdfPCell(tbInner02), "B", 0);
        tc = new PdfPCell(tbInner02);
        tc.BorderWidthTop = 0.8f;
        tc.BorderWidthLeft = 0.8f;
        tc.BorderWidthBottom = 0.8f;
        tc.BorderWidthRight = 0.5f;
        tbDetail.AddCell(tc);


        //右內容副框，1欄
        PdfPTable tbInner03 = new PdfPTable(1);
        p1 = new Paragraph("備              註", s10);
        tbInner03.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        p1 = new Paragraph("", s09);
        tbInner03.AddCell(CF(new PdfPCell(p1), "HT", 90.71f));
        p1 = new Paragraph("營業人蓋用統一發票專用章", s10);
        tbInner03.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        //jpg = iTextSharp.text.Image.GetInstance("F:\\Title02.JPG");
        jpg = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images") + "\\Title03.JPG");
        jpg.SetAbsolutePosition(0, 0);
        
        jpg.ScalePercent(30f);

        tc = new PdfPCell(jpg, false);
        tc.PaddingTop = 3;
        tc.PaddingLeft = 25;
        tc.PaddingBottom = 1;
        tbInner03.AddCell(tc);

        //add to Outer Table
        //tc = CF(new PdfPCell(tbInner03), "B", 0);
        tc = new PdfPCell(tbInner03);
        tc.BorderWidthTop = 0.8f;
        tc.BorderWidthRight = 0.8f;
        tc.BorderWidthBottom = 0.8f;
        tc.BorderWidthLeft = 0.5f;
        tbDetail.AddCell(tc);


        //add to Outer Table
        pdfTable.AddCell(tbDetail);

        //Footer
        PdfPTable tbFooter = new PdfPTable(1);
        //tbFooter.KeepTogether = true;
        p1 = new Paragraph("※應稅、零稅率、免稅之銷售額應分別開立統一發票，並應於各該欄打「v」。"
                          + "                                                                 ",s07 );
        p1.Add(new Chunk("第二聯:扣抵聯", s08));
        tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));

        //p1.Add(new Chunk("\n※本公司若要作廢或更改者，應於發票次月五日前寄送本公司，逾期不受理。", s07));
        //p1.Add(new Chunk("\n※依台北市國稅局大安分局 年 月 日         字第        號函核准使用。", s07));
        p1= new Paragraph("依台北市國稅局大安分局 年 月 日         字第        號函核准使用。", s07);
        tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
        //p1.Add(new Chunk("\n買受人註記欄之註記方法：", s07));
        p1=new Paragraph("買受人註記欄之註記方法：", s07);
        tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
        //p1.Add(new Chunk("\n營業人購進貨物或勞務應先按其用途區分為「進貨及費用」與「固定資產」，其進項稅額，除營業稅法第十九條第一項", s07));
        p1=new Paragraph("營業人購進貨物或勞務應先按其用途區分為「進貨及費用」與「固定資產」，其進項稅額，除營業稅法第十九條第一項", s07);
        tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
        //p1.Add(new Chunk("\n屬不可扣抵外，其餘均得扣抵，並在各該適當欄內打「v」符號。", s07));
        p1=new Paragraph("屬不可扣抵外，其餘均得扣抵，並在各該適當欄內打「v」符號。", s07);
        tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
        //p1.Leading = 5f;
        //p1.MultipliedLeading = 10f;
        
        //tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));

        pdfTable.AddCell(CF(new PdfPCell(tbFooter), "B", 0));

        //畫虛線
        PdfContentByte cb = writer.DirectContent;
        for (int i = 0; i < pdfDoc.Right - 10; i += 4)
        {
            //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfTable.TotalHeight - 55);
            //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfTable.TotalHeight - 55);
            //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfDoc.Top / 2);
            //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfDoc.Top / 2);
            //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfTable.TotalHeight + 22.84f);
            //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfTable.TotalHeight + 22.84f);
            //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfTable.TotalHeight -19.84f);
            //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfTable.TotalHeight - 19.84f);
            cb.MoveTo(pdfDoc.Left + i, 420.945f);
            cb.LineTo(pdfDoc.Left + i + 1, 420.945f);
        }
        //cb.Stroke();

        ////調整版面用
        p1 = new Paragraph("");
        tc = CF(new PdfPCell(p1), "B", 0);
        tc.FixedHeight = 44.2f;
        //tc.FixedHeight = 60f;
        pdfTable.AddCell(tc);

        pdfDoc.Add(pdfTable);
    }

    private void add3(Document pdfDoc)
    {
        //pdfTable = new PdfPTable(new float[] { 527.5f });
        //外框(pdfTable)，1欄
        PdfPTable pdfTable = new PdfPTable(1);
        pdfTable.DefaultCell.Border = 0;
        pdfTable.SetTotalWidth(new float[] { 532f });
        pdfTable.LockedWidth = true;
        pdfTable.SpacingBefore = 0;
        pdfTable.SpacingAfter = 0;

        //標題框(tbTitle)，3欄(圖片，標題，留白
        PdfPTable tbTitle = new PdfPTable(new float[] { 1f, 3f, 1f });
        //第1欄，圖片
        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images") + "\\Title01.JPG");
        jpg.SetAbsolutePosition(0, 0);
        jpg.ScalePercent(30f);
        //PdfPCell tc = new PdfPCell(jpg, true);
        PdfPCell tc = new PdfPCell(new Paragraph(""));//USER要求先拿掉圖片，欲進行套版比對。
        tc.Border = 0;
        tc.PaddingTop = 1;
        tc.PaddingLeft = 1;
        tbTitle.AddCell(CF(tc, "B", 0));

        //第2欄，標題
        PdfPTable tbTemp = new PdfPTable(1);
        Paragraph p1;
        p1 = new Paragraph("吸引力綜合百貨股份有限公司信義分公司", s14);
        tc = new PdfPCell(p1);
        tc.FixedHeight = 27f;
        tbTemp.AddCell(CF(CF(tc, "H", Rectangle.ALIGN_CENTER), "B", 0));
        p1 = new Paragraph("電子計算機統一發票", s11);
        tc = new PdfPCell(p1);
        tbTemp.AddCell(CF(CF(tc, "H", Rectangle.ALIGN_CENTER), "B", 0));
        //p1 = new Paragraph("中華民國" + DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日", s09);
        //p1 = new Paragraph("中 華 民 國    年   月   日", s11);
        p1 = new Paragraph("中 華 民 國", s11);
        p1.Add("    ");
        p1.Add("年");
        p1.Add("    ");
        p1.Add("月");
        p1.Add("    ");
        p1.Add("日");
        tc = CF(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER), "B", 0);
        tc.PaddingTop = 6f;
        tc.FixedHeight = 20f;
        tbTemp.AddCell(tc);
        tbTitle.AddCell(CF(new PdfPCell(tbTemp), "B", 0));

        //第3欄，留白
        p1 = new Paragraph("", s09);
        tbTitle.AddCell(CF(new PdfPCell(p1), "B", 0));

        //外框附加標題框
        pdfTable.AddCell(tbTitle);

        //內容主框2欄，分左右
        float fHeight = 13f;
        PdfPTable tbMaster = new PdfPTable(new float[] { 2.05f, 2f });
        p1 = new Paragraph("發票號碼:", s105);//左
        tc = CF(new PdfPCell(p1), "B", 0);
        tc.FixedHeight = fHeight;
        tbMaster.AddCell(tc);
        p1 = new Paragraph("", s105);//右
        tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1 = new Paragraph("買 受 人:", s105);//左
        tc = CF(new PdfPCell(p1), "B", 0);
        tc.FixedHeight = fHeight;
        tbMaster.AddCell(tc);
        p1 = new Paragraph("", s105);//右
        tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1 = new Paragraph("統一編號:", s105);//左
        tc = CF(new PdfPCell(p1), "B", 0);
        tc.FixedHeight = fHeight;
        tbMaster.AddCell(tc);
        p1 = new Paragraph("檢查號碼:", s105);//右
        tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1 = new Paragraph("地    址:", s105);//左
        tc = CF(new PdfPCell(p1), "B", 0);
        tc.FixedHeight = fHeight;
        tbMaster.AddCell(tc);
        p1 = new Paragraph("", s105);//右
        tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));

        //外框附加內容主框
        pdfTable.AddCell(tbMaster);

        //內容副框2欄，分左(細項、小計、合計)，右(備註、發票章)
        //PdfPTable tbDetail = new PdfPTable(new float[] { 7f, 2f });
        PdfPTable tbDetail = new PdfPTable(2);
        //tbDetail.SetTotalWidth(new float[] { 351.46f,174f });
        tbDetail.SetTotalWidth(new float[] { 385.48f, 141.76f });
        tbDetail.SpacingBefore = 0;
        tbDetail.SpacingAfter = 0;
        tbDetail.LockedWidth = true;
        

        //左內容副框，細項框4欄
        //PdfPTable tbInner02 = new PdfPTable(new float[] { 2f, 1f, 1f, 1f });
        PdfPTable tbInner02 = new PdfPTable(4);
        //tbInner02.SetTotalWidth(new float[] { 161.57f, 68.03f, 70.87f, 86f });
        tbInner02.SetTotalWidth(new float[] { 161.57f, 68.03f, 69.88f, 86f });
        tbInner02.LockedWidth = true;
        //左內容副框:細項框標題列
        p1 = new Paragraph("品               名", s10);
        tbInner02.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        p1 = new Paragraph("數       量", s10);
        tbInner02.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        p1 = new Paragraph("單       價", s10);
        tbInner02.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        p1 = new Paragraph("金       額", s10);
        tbInner02.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        //左內容副框:細項框內容列
        p1 = new Paragraph("", s09);
        tbInner02.AddCell(CF(new PdfPCell(p1), "HT", 127.56f));
        tbInner02.AddCell(new PdfPCell(p1));
        tbInner02.AddCell(new PdfPCell(p1));
        tbInner02.AddCell(new PdfPCell(p1));

        //左內容副框，小計框8欄
        //PdfPTable tbInner04 = new PdfPTable(new float[] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1.75f });
        PdfPTable tbInner04 = new PdfPTable(8);
        float[] widths = new float[] { 46.97f, 36.4f, 46.85f, 38.27f, 47.7f, 38.77f, 45f, 86.83f };
        tbInner04.SpacingAfter = 0;
        tbInner04.SpacingBefore = 0;
        tbInner04.SetTotalWidth(widths);
        tbInner04.LockedWidth = true;


        p1 = new Paragraph("銷          售           額        合          計", s10);
        p1.Leading = 0;

        //PdfPCell tc2 = CF(CF(CF(CF(new PdfPCell(p1), "CS", 7), "B", 2), "H", Rectangle.ALIGN_CENTER), "V", Element.ALIGN_MIDDLE);
        PdfPCell tc2 = CF(CF(CF(new PdfPCell(p1), "CS", 7), "H", Rectangle.ALIGN_CENTER), "V", Element.ALIGN_MIDDLE);
        tc2.PaddingTop = 0;
        tc2.PaddingBottom = 0;
        tc2.PaddingLeft = 0;
        tc2.PaddingLeft = 0;
        tc2.FixedHeight = 12f;
        tbInner04.AddCell(tc2);//銷售額合計Title
        p1 = new Paragraph("", s10);
        tc = CF(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER), "V", Rectangle.ALIGN_CENTER);
        //tc.FixedHeight = 14.17f;
        tbInner04.AddCell(tc);//銷售額合計Value
        p1 = new Paragraph("營業稅", s09);
        tc = CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER);
        tc.FixedHeight = 14.17f;
        tbInner04.AddCell(tc);//營業稅Title
        p1 = new Paragraph("應稅", s09);
        tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//應稅Title
        p1 = new Paragraph("", s09);
        tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//應稅value
        p1 = new Paragraph("零稅率", s09);
        tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//零稅率Title
        p1 = new Paragraph("", s09);
        tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//零稅率Value
        p1 = new Paragraph("免稅", s09);
        tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//免稅Title
        tbInner04.AddCell(new Paragraph("", s09));//免稅Value
        tbInner04.AddCell(new Paragraph("", s09));//營業稅Value

        tc2 = new PdfPCell(new Paragraph("總                                             額", s10));
        tc2.Colspan = 7;
        tc2.FixedHeight = 14.17f;
        tbInner04.AddCell(CF(tc2, "H", Element.ALIGN_CENTER));//總計Title
        tbInner04.AddCell(new Paragraph("", s09));//總計Value
        //        append to Outer Table
        tc = new PdfPCell(tbInner04);
        tc.BorderWidth = 1;
        tc.Colspan = 4;
        tbInner02.AddCell(tc);

        //tc = new PdfPCell(new Paragraph( "總計新台幣 " + Utils.GF_Converts(0)
        //                               + "仟 " + Utils.GF_Converts(0 )
        //                               + "佰 " + Utils.GF_Converts(0 )
        //                               + "拾 " + Utils.GF_Converts(1) + Utils.GF_Converts(0) + Utils.GF_Converts(0)
        //                               + "萬 " + Utils.GF_Converts(9) 
        //                               + "仟 " + Utils.GF_Converts(0) 
        //                               + "佰 " + Utils.GF_Converts(0 )
        //                               + "拾 " + Utils.GF_Converts(0 )
        //                               + " 元整",s09));
        PdfPTable tTemp = new PdfPTable(new float[] { 1.9f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f });
        float fPadding = 6f;
        p1 = new Paragraph("總計新台幣", s10);
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.FixedHeight = 25.51f;
        tc.HorizontalAlignment = Element.ALIGN_LEFT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);
        p1 = new Paragraph(new Chunk("", s10));
        p1.Add(new Chunk("仟", s10));
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);
        //p1 = new Paragraph("一二三" + "佰", s09);
        p1 = new Paragraph(new Chunk("", s10));
        p1.Add(new Chunk("佰", s10));
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);
        //p1 = new Paragraph("一二三" + "拾", s09);
        p1 = new Paragraph(new Chunk("", s10));
        p1.Add(new Chunk("拾", s10));
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);
        //p1 = new Paragraph("一二三" + "萬", s09);
        p1 = new Paragraph(new Chunk("", s10));
        p1.Add(new Chunk("萬", s10));
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);
        //p1 = new Paragraph("一二三" + "仟", s09);
        p1 = new Paragraph(new Chunk("", s07));
        p1.Add(new Chunk("仟", s10));
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);
        //p1 = new Paragraph("一二三" + "佰", s09);
        p1 = new Paragraph(new Chunk("", s10));
        p1.Add(new Chunk("佰", s10));
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);
        //p1 = new Paragraph("一二三" + "拾", s09);
        p1 = new Paragraph(new Chunk("", s07));
        p1.Add(new Chunk("拾", s10));
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);
        //p1 = new Paragraph("一二三" + "元整", s09);
        p1 = new Paragraph(new Chunk("", s10));
        p1.Add(new Chunk("元整", s10));
        tc = new PdfPCell(p1);
        tc.PaddingTop = fPadding;
        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
        tc.BorderWidthLeft = 0;
        tc.BorderWidthRight = 0;
        tc.BorderWidthBottom = 0;
        tTemp.AddCell(tc);

        tc = new PdfPCell(tTemp);
        tc.Colspan = 4;
        //tc.BorderWidth = 0.5f;
        tbInner02.AddCell(tc);

        tc = new PdfPCell(tbInner02);
        tc.BorderWidthTop = 0.8f;
        tc.BorderWidthRight = 0.3f;
        tc.BorderWidthBottom = 0.8f;
        tc.BorderWidthLeft = 0.8f;
        tbDetail.AddCell(tc);


        //右內容副框，1欄
        PdfPTable tbInner03 = new PdfPTable(1);
        p1 = new Paragraph("備              註", s10);
        tbInner03.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        p1 = new Paragraph("", s09);
        tbInner03.AddCell(CF(new PdfPCell(p1), "HT", 90.71f));
        p1 = new Paragraph("營業人蓋用統一發票專用章", s10);
        tbInner03.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        //jpg = iTextSharp.text.Image.GetInstance("F:\\Title02.JPG");
        jpg = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images") + "\\Title03.JPG");
        jpg.SetAbsolutePosition(0, 0);

        jpg.ScalePercent(30f);

        tc = new PdfPCell(jpg, false);
        tc.PaddingTop = 3;
        tc.PaddingLeft = 25;
        tc.PaddingBottom = 1;
        tbInner03.AddCell(tc);

        //add to Outer Table
        tc = new PdfPCell(tbInner03);
        tc.BorderWidthTop = 0.8f;
        tc.BorderWidthRight = 0.8f;
        tc.BorderWidthBottom = 0.8f;
        tc.BorderWidthLeft = 0.3f;
        tbDetail.AddCell(tc);


        //add to Outer Table
        pdfTable.AddCell(tbDetail);

        //Footer
        PdfPTable tbFooter = new PdfPTable(1);
        //p1 = new Paragraph("※應稅、零稅率、免稅之銷售額應分別開立統一發票，並應於各該欄打「v」。"
        //                  + "                                                                 ", s07);
        //p1.Add(new Chunk("第三聯:收執聯", s08));
        //p1.Add(new Chunk("\n※依台北市國稅局大安分局 年 月 日         字第        號函核准使用。", s07));
        //p1.Add(new Chunk("\n買受人註記欄之註記方法：", s07));
        //p1.Add(new Chunk("\n營業人購進貨物或勞務應先按其用途區分為「進貨及費用」與「固定資產」，其進項稅額，除營業稅法第十九條第一項", s07));
        //p1.Add(new Chunk("\n屬不可扣抵外，其餘均得扣抵，並在各該適當欄內打「v」符號。", s07));
        p1 = new Paragraph("※應稅、零稅率、免稅之銷售額應分別開立統一發票，並應於各該欄打「v」。"
                          + "                                                                 ", s07);
        p1.Add(new Chunk("第三聯:收執聯", s08));
        tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1=new Paragraph("依台北市國稅局大安分局 年 月 日         字第        號函核准使用。", s07);
        tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1=new Paragraph("買受人註記欄之註記方法：", s07);
        tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1=new Paragraph("營業人購進貨物或勞務應先按其用途區分為「進貨及費用」與「固定資產」，其進項稅額，除營業稅法第十九條第一項", s07);
        tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1=new Paragraph("屬不可扣抵外，其餘均得扣抵，並在各該適當欄內打「v」符號。", s07);
        tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));

        pdfTable.AddCell(CF(new PdfPCell(tbFooter), "B", 0));
        pdfDoc.Add(pdfTable);

        //打上註記欄圖片
        //jpg = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images") + "\\物件-2.gif");
        //jpg.SetAbsolutePosition(455, 340);
        //jpg.ScaleToFit(70, 40);

        //pdfDoc.Add(jpg);
    }

    private void addMarkTable1(Document pdfDoc)
    {
        PdfPTable pdfTable = new PdfPTable(3);
        //pdfTable.DefaultCell.Border = 0;
        //pdfTable.SetTotalWidth(new float[] { 31.18f, 51.02f, 48.19f });
        pdfTable.SetTotalWidth(new float[] { 34.016f, 34.016f, 34.016f });
        pdfTable.LockedWidth = true;
        pdfTable.SpacingBefore = 0;
        pdfTable.SpacingAfter = 0;
        Paragraph p1;
        PdfPCell tc;
        //第1列
        p1 = new Paragraph("買 受 人 註 記 欄", s09);
        tc = new PdfPCell(p1);
        tc.HorizontalAlignment = Element.ALIGN_CENTER;
        tc.PaddingLeft = 0;
        tc.PaddingRight = 0;
        tc.PaddingTop = 0;
        tc.FixedHeight = 12.756f;
        tc.Colspan = 3;
        pdfTable.AddCell(tc);
        //第2列
        p1 = new Paragraph("區    分", s06);
        tc = new PdfPCell(p1);
        tc.FixedHeight = 12.756f;
        tc.PaddingLeft = 0;
        tc.PaddingRight = 0;
        tc.HorizontalAlignment = Element.ALIGN_CENTER;
        pdfTable.AddCell(tc);
        p1 = new Paragraph("進貨及費用", s06);
        tc = new PdfPCell(p1);
        tc.PaddingLeft = 0;
        tc.PaddingRight = 0;
        tc.HorizontalAlignment = Element.ALIGN_CENTER;
        pdfTable.AddCell(tc);
        p1 = new Paragraph("固定資產", s06);
        tc = new PdfPCell(p1);
        tc.PaddingLeft = 0;
        tc.PaddingRight = 0;
        tc.HorizontalAlignment = Element.ALIGN_CENTER;
        pdfTable.AddCell(tc);

        //第3列
        p1 = new Paragraph("得 抵 扣", s06);
        tc = new PdfPCell(p1);
        tc.FixedHeight = 14.173f;
        tc.PaddingLeft = 0;
        tc.PaddingRight = 0;
        tc.PaddingTop = 3f;
        tc.HorizontalAlignment = Element.ALIGN_CENTER;
        pdfTable.AddCell(tc);
        p1 = new Paragraph("", s06);
        tc = new PdfPCell(p1);
        tc.HorizontalAlignment = Element.ALIGN_CENTER;
        pdfTable.AddCell(tc);
        p1 = new Paragraph("", s06);
        tc = new PdfPCell(p1);
        tc.HorizontalAlignment = Element.ALIGN_CENTER;
        pdfTable.AddCell(tc);

        //第4列
        p1 = new Paragraph("不得抵扣", s06);
        tc = new PdfPCell(p1);
        tc.FixedHeight = 11.339f;
        tc.PaddingLeft = 0;
        tc.PaddingRight = 0;
        tc.PaddingTop = 1;
        tc.HorizontalAlignment = Element.ALIGN_CENTER;
        pdfTable.AddCell(tc);
        p1 = new Paragraph("", s06);
        tc = new PdfPCell(p1);
        tc.HorizontalAlignment = Element.ALIGN_CENTER;
        pdfTable.AddCell(tc);
        p1 = new Paragraph("", s06);
        tc = new PdfPCell(p1);
        tc.HorizontalAlignment = Element.ALIGN_CENTER;
        pdfTable.AddCell(tc);
        pdfTable.CompleteRow();
        
        pdfTable.WriteSelectedRows(0, 4, 458.5f, 775, writer.DirectContent);
        //pdfDoc.Add(pdfTable);
    }

    private void addMarkTable2(Document pdfDoc) 
    {
        PdfPTable pdfTable = new PdfPTable(3);
        //pdfTable.DefaultCell.Border = 0;
        //pdfTable.SetTotalWidth(new float[] { 31.18f,51.02f,48.19f });
        pdfTable.SetTotalWidth(new float[] { 34.016f, 34.016f, 34.016f });
        pdfTable.LockedWidth = true;
        pdfTable.SpacingBefore = 0;
        pdfTable.SpacingAfter = 0;
        Paragraph p1;
        PdfPCell tc;
        //第1列
        p1=new Paragraph("買 受 人 註 記 欄",s09);
        tc=new PdfPCell(p1);
        tc.HorizontalAlignment = Element.ALIGN_CENTER;
        tc.PaddingLeft = 0;
        tc.PaddingRight = 0;
        tc.PaddingTop = 0;
        tc.FixedHeight = 12.756f;
        tc.Colspan=3;
        pdfTable.AddCell(tc);
        //第2列
        p1 = new Paragraph("區    分", s06);
        tc = new PdfPCell(p1);
        tc.FixedHeight = 12.756f;
        tc.PaddingLeft = 0;
        tc.PaddingRight = 0;
        tc.HorizontalAlignment = Element.ALIGN_CENTER;
        pdfTable.AddCell(tc);
        p1 = new Paragraph("進貨及費用", s06);
        tc = new PdfPCell(p1);
        tc.PaddingLeft = 0;
        tc.PaddingRight = 0;
        tc.HorizontalAlignment = Element.ALIGN_CENTER;
        pdfTable.AddCell(tc);
        p1 = new Paragraph("固定資產", s06);
        tc = new PdfPCell(p1);
        tc.PaddingLeft = 0;
        tc.PaddingRight = 0;
        tc.HorizontalAlignment = Element.ALIGN_CENTER;
        pdfTable.AddCell(tc);

        //第3列
        p1 = new Paragraph("得 抵 扣", s06);
        tc = new PdfPCell(p1);
        tc.FixedHeight = 14.173f;
        tc.PaddingTop = 3f;
        tc.PaddingLeft = 0;
        tc.PaddingRight = 0;
        tc.HorizontalAlignment = Element.ALIGN_CENTER;
        pdfTable.AddCell(tc);
        p1 = new Paragraph("", s06);
        tc = new PdfPCell(p1);
        tc.HorizontalAlignment = Element.ALIGN_CENTER;
        pdfTable.AddCell(tc);
        p1 = new Paragraph("", s06);
        tc = new PdfPCell(p1);
        tc.HorizontalAlignment = Element.ALIGN_CENTER;
        pdfTable.AddCell(tc);

        //第4列
        p1 = new Paragraph("不得抵扣", s06);
        tc = new PdfPCell(p1);
        tc.FixedHeight = 11.339f;
        tc.PaddingLeft = 0;
        tc.PaddingRight = 0;
        tc.PaddingTop = 1;
        tc.HorizontalAlignment = Element.ALIGN_CENTER;
        pdfTable.AddCell(tc);
        p1 = new Paragraph("", s06);
        tc = new PdfPCell(p1);
        tc.HorizontalAlignment = Element.ALIGN_CENTER;
        pdfTable.AddCell(tc);
        p1 = new Paragraph("", s06);
        tc = new PdfPCell(p1);
        tc.HorizontalAlignment = Element.ALIGN_CENTER;
        pdfTable.AddCell(tc);
        pdfTable.CompleteRow();
        pdfTable.WriteSelectedRows(0, 4, 458.5f, 354, writer.DirectContent);
        //pdfDoc.Add(pdfTable);
    }

    #region 舊方法
    //private void add2(Document pdfDoc) 
    //{
    //    pdfTable = new PdfPTable(new float[] { 400 });
    //    pdfTable.SpacingAfter = 0;
    //    pdfTable.SpacingBefore = 0;
    //    pdfTable.DefaultCell.Border = 0;
    //    pdfTable.SplitRows = false;
    //    pdfTable.KeepTogether = true;

    //    //Paragraph pNormal;
    //    //pdfTable.AddCell(new PdfPCell(new Paragraph("Hello World", largerFont)));
    //    //Title:Table 2 columns
    //    PdfPTable tbInner01 = new PdfPTable(new float[] { 1f, 3f, 1f });
    //    //Title_col1:image
    //    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images") + "\\Title01.JPG");
    //    jpg.SetAbsolutePosition(0, 0);
    //    jpg.ScalePercent(30f);
    //    //PdfPCell tc = new PdfPCell(jpg, true);
    //    PdfPCell tc = new PdfPCell(new Paragraph(""));
    //    tc.Border = 0;
    //    tc.PaddingTop = 1;
    //    tc.PaddingLeft = 1;
    //    tbInner01.AddCell(CF(tc, "B", 0));

    //    //Title_col2: 3 rows
    //    PdfPTable tbTemp = new PdfPTable(1);
    //    Paragraph p1;
    //    p1 = new Paragraph("遠傳電信股份有險公司 台大公館門市", largerFont);
    //    tbTemp.AddCell(CF(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER), "B", 0));
    //    p1 = new Paragraph("電子計算機統一發票", s09);
    //    tbTemp.AddCell(CF(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER), "B", 0));
    //    //p1 = new Paragraph("中華民國" + DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日", s09);
    //    p1 = new Paragraph("中華民國   年   月   日", s09);
    //    tbTemp.AddCell(CF(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER), "B", 0));
    //    tbInner01.AddCell(CF(new PdfPCell(tbTemp), "B", 0));
    //    p1 = new Paragraph("", s09);
    //    tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));

    //    //add to Outer Table
    //    pdfTable.AddCell(tbInner01);

    //    //Master:Table 2 columns
    //    tbInner01 = new PdfPTable(new float[] { 2f, 2f });
    //    p1 = new Paragraph("發票號碼:", s09);
    //    tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));
    //    p1 = new Paragraph("", s09);
    //    tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));
    //    p1 = new Paragraph("買受人:", s09);
    //    tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));
    //    p1 = new Paragraph("", s09);
    //    tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));
    //    p1 = new Paragraph("統一編號:", s09);
    //    tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));
    //    p1 = new Paragraph("檢查號碼:", s09);
    //    tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));
    //    p1 = new Paragraph("地址:", s09);
    //    tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));
    //    p1 = new Paragraph("", s09);
    //    tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));
    //    //add to Outer Table
    //    pdfTable.AddCell(tbInner01);

    //    //Detail:Table 2 columns
    //    tbInner01 = new PdfPTable(new float[] { 7f, 2f });

    //    //       left table:Table 4 columns
    //    PdfPTable tbInner02 = new PdfPTable(new float[] { 2f, 1f, 1f, 1f });

    //    //Details left table Title
    //    p1 = new Paragraph("品               名", s09);
    //    tbInner02.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
    //    p1 = new Paragraph("數       量", s09);
    //    tbInner02.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
    //    p1 = new Paragraph("單       價", s09);
    //    tbInner02.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
    //    p1 = new Paragraph("金       額", s09);
    //    tbInner02.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
    //    //        left table Data
    //    p1 = new Paragraph("", s09);
    //    tbInner02.AddCell(CF(new PdfPCell(p1), "HT", 160f));
    //    tbInner02.AddCell(new PdfPCell(p1));
    //    tbInner02.AddCell(new PdfPCell(p1));
    //    tbInner02.AddCell(new PdfPCell(p1));

    //    //        Left table 合計:Table 8 columns
    //    PdfPTable tbInner04 = new PdfPTable(new float[] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1.75f });
    //    //tbInner04.DefaultCell.PaddingBottom = 2;
    //    p1 = new Paragraph("銷          售           額        合          計", s09);

    //    PdfPCell tc2 = CF(CF(CF(CF(new PdfPCell(p1), "CS", 7), "B", 2), "H", Rectangle.ALIGN_CENTER), "V", Element.ALIGN_MIDDLE);
    //    //tc2.Colspan = 7;
    //    //tc2.Border = 10;
    //    tbInner04.AddCell(tc2);
    //    p1 = new Paragraph("", s09);
    //    tbInner04.AddCell(CF(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER), "V", Rectangle.ALIGN_CENTER));
    //    p1 = new Paragraph("營業稅", s09);
    //    tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
    //    p1 = new Paragraph("應稅", s09);
    //    tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
    //    p1 = new Paragraph("", s09);
    //    tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
    //    p1 = new Paragraph("零稅率", s09);
    //    tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
    //    p1 = new Paragraph("", s09);
    //    tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
    //    p1 = new Paragraph("免稅", s09);
    //    tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
    //    tbInner04.AddCell(new Paragraph("", s09));
    //    tbInner04.AddCell(new Paragraph("", s09));

    //    tc2 = new PdfPCell(new Paragraph("總              計", s09));
    //    tc2.Colspan = 7;
    //    tbInner04.AddCell(tc2);
    //    tbInner04.AddCell(new Paragraph("", s09));
    //    //        add to Outer Table
    //    tc = new PdfPCell(tbInner04);
    //    tc.BorderWidth = 1;
    //    tc.Colspan = 4;
    //    tbInner02.AddCell(tc);

    //    //tc = new PdfPCell(new Paragraph("總計新台幣 " + Utils.GF_Converts(0)
    //    //                               + "仟 " + Utils.GF_Converts(0)
    //    //                               + "佰 " + Utils.GF_Converts(0)
    //    //                               + "拾 " + Utils.GF_Converts(1) + Utils.GF_Converts(0) + Utils.GF_Converts(0)
    //    //                               + "萬 " + Utils.GF_Converts(9)
    //    //                               + "仟 " + Utils.GF_Converts(0)
    //    //                               + "佰 " + Utils.GF_Converts(0)
    //    //                               + "拾 " + Utils.GF_Converts(0)
    //    //                               + " 元整", s09));
    //    PdfPTable tTemp = new PdfPTable(new float[] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f });
    //    p1 = new Paragraph("總計新台幣", s07);
    //    tc = new PdfPCell(p1);
    //    tc.HorizontalAlignment = Element.ALIGN_LEFT;
    //    tc.BorderWidthLeft = 0;
    //    tc.BorderWidthRight = 0;
    //    tc.BorderWidthBottom = 0.5f;
    //    tTemp.AddCell(tc);
    //    p1 = new Paragraph(new Chunk("", s07));
    //    p1.Add(new Chunk("仟", s07));
    //    tc = new PdfPCell(p1);
    //    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
    //    tc.BorderWidthLeft = 0;
    //    tc.BorderWidthRight = 0;
    //    tc.BorderWidthBottom = 0.5f;
    //    tTemp.AddCell(tc);
    //    //p1 = new Paragraph("一二三" + "佰", s09);
    //    p1 = new Paragraph(new Chunk("", s07));
    //    p1.Add(new Chunk("佰", s07));
    //    tc = new PdfPCell(p1);
    //    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
    //    tc.BorderWidthLeft = 0;
    //    tc.BorderWidthRight = 0;
    //    tc.BorderWidthBottom = 0.5f;
    //    tTemp.AddCell(tc);
    //    //p1 = new Paragraph("一二三" + "拾", s09);
    //    p1 = new Paragraph(new Chunk("", s07));
    //    p1.Add(new Chunk("拾", s07));
    //    tc = new PdfPCell(p1);
    //    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
    //    tc.BorderWidthLeft = 0;
    //    tc.BorderWidthRight = 0;
    //    tc.BorderWidthBottom = 0.5f;
    //    tTemp.AddCell(tc);
    //    //p1 = new Paragraph("一二三" + "萬", s09);
    //    p1 = new Paragraph(new Chunk("", s07));
    //    p1.Add(new Chunk("萬", s07));
    //    tc = new PdfPCell(p1);
    //    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
    //    tc.BorderWidthLeft = 0;
    //    tc.BorderWidthRight = 0;
    //    tc.BorderWidthBottom = 0.5f;
    //    tTemp.AddCell(tc);
    //    //p1 = new Paragraph("一二三" + "仟", s09);
    //    p1 = new Paragraph(new Chunk("", s07));
    //    p1.Add(new Chunk("仟", s07));
    //    tc = new PdfPCell(p1);
    //    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
    //    tc.BorderWidthLeft = 0;
    //    tc.BorderWidthRight = 0;
    //    tc.BorderWidthBottom = 0.5f;
    //    tTemp.AddCell(tc);
    //    //p1 = new Paragraph("一二三" + "佰", s09);
    //    p1 = new Paragraph(new Chunk("", s07));
    //    p1.Add(new Chunk("佰", s07));
    //    tc = new PdfPCell(p1);
    //    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
    //    tc.BorderWidthLeft = 0;
    //    tc.BorderWidthRight = 0;
    //    tc.BorderWidthBottom = 0.5f;
    //    tTemp.AddCell(tc);
    //    //p1 = new Paragraph("一二三" + "拾", s09);
    //    p1 = new Paragraph(new Chunk("", s07));
    //    p1.Add(new Chunk("拾", s07));
    //    tc = new PdfPCell(p1);
    //    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
    //    tc.BorderWidthLeft = 0;
    //    tc.BorderWidthRight = 0;
    //    tc.BorderWidthBottom = 0.5f;
    //    tTemp.AddCell(tc);
    //    //p1 = new Paragraph("一二三" + "元整", s09);
    //    p1 = new Paragraph(new Chunk("", s07));
    //    p1.Add(new Chunk("元整", s07));
    //    tc = new PdfPCell(p1);
    //    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
    //    tc.BorderWidthLeft = 0;
    //    tc.BorderWidthBottom = 0.5f;
    //    tTemp.AddCell(tc);

    //    tc = new PdfPCell(tTemp);
    //    tc.Colspan = 4;
    //    tc.BorderWidth = 0.5f;
    //    tc.BorderWidthBottom = 0.5f;

    //    tbInner02.AddCell(tc);


    //    tbInner01.AddCell(CF(new PdfPCell(tbInner02), "B", 0));


    //    //        Right table:Table 1 columns
    //    PdfPTable tbInner03 = new PdfPTable(1);
    //    p1 = new Paragraph("備              註", s09);
    //    tbInner03.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
    //    p1 = new Paragraph("", s09);
    //    tbInner03.AddCell(CF(new PdfPCell(p1), "HT", 130f));
    //    p1 = new Paragraph("營業人蓋用統一發票專用章", s07);
    //    tbInner03.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
    //    jpg = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images") + "\\Title02.JPG");
    //    jpg.SetAbsolutePosition(0, 0);
    //    jpg.ScalePercent(50f);

    //    tc = new PdfPCell(jpg, true);
    //    tc.PaddingTop = 1;
    //    tc.PaddingLeft = 1;
    //    tc.PaddingBottom = 1;
    //    tbInner03.AddCell(tc);

    //    //add to Outer Table
    //    tbInner01.AddCell(CF(new PdfPCell(tbInner03), "B", 0));

    //    //add to Outer Table
    //    pdfTable.AddCell(tbInner01);

    //    //Footer
    //    tbInner01 = new PdfPTable(1);
    //    tbInner01.KeepTogether = true;
    //    p1 = new Paragraph("※應稅、零稅率、免稅之銷售額應分別開立統一發票，並應於各該欄打「v」。"
    //                      +"                                                                     "
    //                      + "                         ",s05);
    //    p1.Add(new Paragraph("第二聯:扣抵聯", s07));
    //    p1.Add(new Chunk("\n※本公司若要作廢或更改者，應於發票次月五日前寄送本公司，逾期不受理。", s05));
    //    p1.Add(new Chunk("\n※依台北市國稅局大安分局   年  月  日       字第      號函核准使用。", s05));
    //    p1.Add(new Chunk("買受人註記欄之註記方法：", s05));
    //    p1.Add(new Chunk("\n營業人購進貨物或勞務應先按其用途區分為「進貨及費用」與「固定資產」，其進項稅額，除營業稅法第十九條第一項：", s05));
    //    p1.Add(new Chunk("\n屬不可扣抵外，其餘均得扣抵，並在各該適當欄內打「v」符號。", s05));
    //    tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));

    //    pdfTable.AddCell(CF(new PdfPCell(tbInner01), "B", 0));

    //    pdfDoc.Add(pdfTable);

    //    //打上註記欄圖片
    //    jpg = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images")+"\\物件-2.gif");
    //    jpg.SetAbsolutePosition(455, 340);
    //    jpg.ScaleToFit(70, 40);

    //    pdfDoc.Add(jpg);
    //}
    #endregion

    private PdfPCell CF(PdfPCell dc,string sStyle,object oValue) 
    {
        dc.UseAscender = true;
        switch (sStyle) 
        {
            case "H":
                dc.HorizontalAlignment = (int)oValue;
                break;
            case "V":
                dc.VerticalAlignment = (int)oValue;
                break;
            case "B":
                dc.Border = (int)oValue;
                break;
            case "CS":
                dc.Colspan = (int)oValue;
                break;
            case "HT":
                dc.FixedHeight = (float)oValue;
                
                break;
            default:
                break;
        }
        return dc;
    }
}

