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
using iTextSharp.text.pdf.draw;



public partial class Receipt7 : System.Web.UI.Page
{
    protected Font s05;
    protected Font s07;
    protected Font s09;
    protected Font largerFont;
    protected Font RedsmallFont;
    protected Document pdfDoc;
    protected PdfPTable pdfTable;  //組成整體樣式
    protected PdfPTable details;   //組成明細樣式
    protected DataTable dtData;    //前端傳來的資料表
    protected string filename;
    OleDbConnection gCon;
    string sCon;
    PdfWriter writer;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        FontFactory.Register(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\..\Fonts\simhei.ttf");
        FontFactory.Register(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\..\Fonts\kaiu.ttf");
        s05 = ChineseFontFactory.Create("標楷體", 5f, Font.NORMAL);
        s07 = ChineseFontFactory.Create("標楷體", 7f, Font.NORMAL);
        s09 = ChineseFontFactory.Create("標楷體", 9f, Font.NORMAL);
        largerFont = ChineseFontFactory.Create("標楷體", 12f, Font.BOLD);
        RedsmallFont = ChineseFontFactory.Create("標楷體", 9f, Font.NORMAL, BaseColor.RED);
        
        //sCon="Driver={Microsoft Access Driver (*.mdb)};Dbq=G:\\RAY\\PDF_WEB\\App_Data\\Sales.mdb;Uid=Admin;Pwd=;";
        //gCon = new OleDbConnection(sCon);
        //gCon.Close();
        //string printerName = ConfigurationManager.AppSettings["PDFPrinterName"].ToString();//web.config中設定

    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        pdfDoc = new Document(PageSize.A4, 10f, 10f, 1f, 20f);
        filename = Guid.NewGuid().ToString() + ".pdf";
        FileStream stream = new FileStream(Path.Combine(Server.MapPath("~/Downloads"), filename), FileMode.Create);
        writer = PdfWriter.GetInstance(pdfDoc, stream);
        pdfDoc.Open();
        add3(pdfDoc);
        

        pdfDoc.Close();
    }
    private void add3(Document pdfDoc) 
    {
            //pdfDoc.Add(new Paragraph("Hello World",s09));
        ACell cell;
        PdfPTable mainTable = new PdfPTable(2);
        mainTable.HorizontalAlignment=Element.ALIGN_LEFT;
        //mainTable.WidthPercentage = 50;
        //mainTable.SetWidths(new float[]{1.97f,1.97f});

        //固定寬度
        //mainTable.TotalWidth = 100f;
        //mainTable.LockedWidth = true;

        float[] widths = new float[] {20f,20f };
        //Rectangle r = new Rectangle(5f, 5f);
        //mainTable.SetWidthPercentage(widths,r);
        mainTable.SetTotalWidth(widths);
        mainTable.TotalWidth = 283.46f;
        mainTable.LockedWidth = true;
        
        Paragraph p1;
        p1 = new Paragraph("Hello World", s09);
        //p1.SetLeading(0, 0);
        //This accepts a string, with "Left", "Center", "Justify", and "Right"
        p1.SetAlignment("Right");
        p1.KeepTogether=true;
        //p1.SetIndentationLeft(18);
        //p1.SetFirstLineIndent(-18);
        //cell = new ACell(p1).set("H", Element.ALIGN_CENTER);
        cell = new ACell(p1);
        
        mainTable.AddCell(cell.get());
        p1 = new Paragraph("Hello World", s05);
        
        p1.Add(Chunk.NEWLINE);
        p1.Add(new Chunk("Itext"));
        p1 = new Paragraph("Hello World", s09);
        cell = new ACell(p1);
        mainTable.AddCell(cell.get());
        p1 = new Paragraph("Hello World", s09);
        cell = new ACell(p1);
        mainTable.AddCell(cell.get());
        p1 = new Paragraph("Hello World", s09);
        cell = new ACell(p1);
        mainTable.AddCell(cell.get());
        p1 = new Paragraph("Hello World", s09);
        cell = new ACell(p1);
        mainTable.AddCell(cell.get());
        p1 = new Paragraph("Hello World", s09);
        cell = new ACell(p1);
        mainTable.AddCell(cell.get());
        p1 = new Paragraph("Hello World", s09);
        cell = new ACell(p1);
        mainTable.AddCell(cell.get());
        
        //cell = new ACell(p1).set("B", 0);
        //mainTable.AddCell(cell.get());
        pdfDoc.Add(mainTable);

        //PdfContentByte cb = writer.DirectContent;
        //for (int i = 0; i < pdfDoc.Right; i += 4) 
        //{
        //    cb.MoveTo(pdfDoc.Left+i, pdfDoc.Top - mainTable.TotalHeight - 5);
        //    cb.LineTo(pdfDoc.Left+i+1, pdfDoc.Top - mainTable.TotalHeight - 5);
        //}

        //cb.Stroke();
        //pdfDoc.Add(new Paragraph(new Chunk(new DottedLineSeparator())));
        //PdfPTable aTable = new PdfPTable(3);
        ////aTable.SetTotalWidth(new float[] {20});
        //aTable.WidthPercentage = 10;
        //aTable.TotalWidth = 100;
        ////aTable.SetWidths(new Single[] { 5, 5, 5 });
        //aTable.SpacingBefore = 0;
        //aTable.HorizontalAlignment = Element.ALIGN_LEFT;
        //p1 = new Paragraph("A-1", s09);
        //cell =new ACell(p1);
        //aTable.AddCell(cell.get());
        //p1 = new Paragraph("A-2", s09);
        //cell = new ACell(p1);
        //aTable.AddCell(cell.get());
        //p1 = new Paragraph("A-3", s09);
        //cell = new ACell(p1);
        //aTable.AddCell(cell.get());
        //p1 = new Paragraph("B-1", s09);
        //cell = new ACell(p1);
        //aTable.AddCell(cell.get());
        //p1 = new Paragraph("B-2", s09);
        //cell = new ACell(p1);
        //aTable.AddCell(cell.get());
        //p1 = new Paragraph("B-3", s09);
        //cell = new ACell(p1);
        //aTable.AddCell(cell.get());
        //aTable.CompleteRow();
        //aTable.WriteSelectedRows(0, 2, pdfDoc.Right -200 , pdfDoc.Top  , writer.DirectContent);
        //pdfDoc.Add(aTable);

        
       
    }
    private void add2(Document pdfDoc) 
    {
        pdfTable = new PdfPTable(new float[] { 400 });
        pdfTable.DefaultCell.Border = 0;
        pdfTable.SplitRows = false;
        pdfTable.KeepTogether = true;

        //Paragraph pNormal;
        //pdfTable.AddCell(new PdfPCell(new Paragraph("Hello World", largerFont)));
        //Title:Table 2 columns
        PdfPTable tbInner01 = new PdfPTable(new float[] { 1f, 3f, 1f });
        //Title_col1:image
        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("F:\\Title01.JPG");
        jpg.SetAbsolutePosition(0, 0);
        jpg.ScalePercent(30f);
        //PdfPCell tc = new PdfPCell(jpg, true);
        PdfPCell tc = new PdfPCell(new Paragraph(""));
        tc.Border = 0;
        tc.PaddingTop = 1;
        tc.PaddingLeft = 1;
        tbInner01.AddCell(CF(tc, "B", 0));

        //Title_col2: 3 rows
        PdfPTable tbTemp = new PdfPTable(1);
        Paragraph p1;
        p1 = new Paragraph("遠傳電信股份有險公司 台大公館門市", largerFont);
        tbTemp.AddCell(CF(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER), "B", 0));
        p1 = new Paragraph("電子計算機統一發票", s09);
        tbTemp.AddCell(CF(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER), "B", 0));
        //p1 = new Paragraph("中華民國" + DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日", s09);
        p1 = new Paragraph("中華民國   年   月   日", s09);
        tbTemp.AddCell(CF(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER), "B", 0));
        tbInner01.AddCell(CF(new PdfPCell(tbTemp), "B", 0));
        p1 = new Paragraph("", s09);
        tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));

        //add to Outer Table
        pdfTable.AddCell(tbInner01);

        //Master:Table 2 columns
        tbInner01 = new PdfPTable(new float[] { 2f, 2f });
        p1 = new Paragraph("發票號碼:", s09);
        tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1 = new Paragraph("", s09);
        tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1 = new Paragraph("買受人:", s09);
        tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1 = new Paragraph("", s09);
        tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1 = new Paragraph("統一編號:", s09);
        tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1 = new Paragraph("檢查號碼:", s09);
        tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1 = new Paragraph("地址:", s09);
        tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));
        p1 = new Paragraph("", s09);
        tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));
        //add to Outer Table
        pdfTable.AddCell(tbInner01);

        //Detail:Table 2 columns
        tbInner01 = new PdfPTable(new float[] { 7f, 2f });

        //       left table:Table 4 columns
        PdfPTable tbInner02 = new PdfPTable(new float[] { 2f, 1f, 1f, 1f });

        //Details left table Title
        p1 = new Paragraph("品               名", s09);
        tbInner02.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        p1 = new Paragraph("數       量", s09);
        tbInner02.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        p1 = new Paragraph("單       價", s09);
        tbInner02.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        p1 = new Paragraph("金       額", s09);
        tbInner02.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        //        left table Data
        p1 = new Paragraph("", s09);
        tbInner02.AddCell(CF(new PdfPCell(p1), "HT", 80));
        tbInner02.AddCell(new PdfPCell(p1));
        tbInner02.AddCell(new PdfPCell(p1));
        tbInner02.AddCell(new PdfPCell(p1));

        //        Left table 合計:Table 8 columns
        PdfPTable tbInner04 = new PdfPTable(new float[] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f });
        //tbInner04.DefaultCell.PaddingBottom = 2;
        p1 = new Paragraph("銷          售           額        合          計", s09);

        PdfPCell tc2 = CF(CF(CF(CF(new PdfPCell(p1), "CS", 7), "B", 2), "H", Rectangle.ALIGN_CENTER), "V", Element.ALIGN_MIDDLE);
        //tc2.Colspan = 7;
        //tc2.Border = 10;
        tbInner04.AddCell(tc2);
        p1 = new Paragraph("0", s09);
        tbInner04.AddCell(CF(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER), "V", Rectangle.ALIGN_CENTER));
        p1 = new Paragraph("營業稅", s09);
        tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        p1 = new Paragraph("應稅", s09);
        tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        p1 = new Paragraph("", s09);
        tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        p1 = new Paragraph("零稅率", s09);
        tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        p1 = new Paragraph("", s09);
        tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        p1 = new Paragraph("免稅", s09);
        tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        tbInner04.AddCell(new Paragraph("", s09));
        tbInner04.AddCell(new Paragraph("", s09));

        tc2 = new PdfPCell(new Paragraph("總                計", s09));
        tc2.Colspan = 7;
        tbInner04.AddCell(tc2);
        tbInner04.AddCell(new Paragraph("0", s09));
        //        add to Outer Table
        tc = new PdfPCell(tbInner04);
        tc.BorderWidth = 1;
        tc.Colspan = 4;
        tbInner02.AddCell(tc);

        tc = new PdfPCell(new Paragraph("總計新台幣   " + 0
                                       + "   仟  " + 0
                                       + "   佰  " + 0
                                       + "   拾  " + 100
                                       + "   萬  " + 9
                                       + "   仟  " + 0
                                       + "   佰  " + 0
                                       + "   拾  " + 0
                                       + " 元整", s09));
        tc.Colspan = 4;
        tbInner02.AddCell(tc);


        tbInner01.AddCell(CF(new PdfPCell(tbInner02), "B", 0));


        //        Right table:Table 1 columns
        PdfPTable tbInner03 = new PdfPTable(1);
        p1 = new Paragraph("備              註", s09);
        tbInner03.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        p1 = new Paragraph("", s09);
        tbInner03.AddCell(CF(new PdfPCell(p1), "HT", 40));
        p1 = new Paragraph("營業人蓋用統一發票專用章", s07);
        tbInner03.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
        jpg = iTextSharp.text.Image.GetInstance("F:\\Title02.JPG");
        jpg.SetAbsolutePosition(0, 0);
        jpg.ScalePercent(50f);

        tc = new PdfPCell(jpg, true);
        tc.PaddingTop = 1;
        tc.PaddingLeft = 1;
        tc.PaddingBottom = 1;
        tbInner03.AddCell(tc);

        //add to Outer Table
        tbInner01.AddCell(CF(new PdfPCell(tbInner03), "B", 0));


        //add to Outer Table
        pdfTable.AddCell(tbInner01);

        //Footer
        tbInner01 = new PdfPTable(1);
        tbInner01.KeepTogether = true;
        p1 = new Paragraph("※應稅、零稅率、免稅之銷售額應分別開立統一發票，並應於各該欄打「v」。"
                          +"                                                                     "
                          + "                         ",s05);
        p1.Add(new Paragraph("第二聯:扣抵聯", s07));
        p1.Add(new Chunk("\n※本公司若要作廢或更改者，應於發票次月五日前寄送本公司，逾期不受理。", s05));
        p1.Add(new Chunk("\n※依台北市國稅局大安分局   年  月  日       字第      號函核准使用。", s05));
        p1.Add(new Chunk("買受人註記欄之註記方法：", s05));
        p1.Add(new Chunk("\n營業人購進貨物或勞務應先按其用途區分為「進貨及費用」與「固定資產」，其進項稅額，除營業稅法第十九條第一項：", s05));
        p1.Add(new Chunk("\n屬不可扣抵外，其餘均得扣抵，並在各該適當欄內打「v」符號。", s05));
        tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));

        pdfTable.AddCell(CF(new PdfPCell(tbInner01), "B", 0));

        pdfDoc.Add(pdfTable);

        //jpg = iTextSharp.text.Image.GetInstance("F:\\table01.JPG");
        jpg = iTextSharp.text.Image.GetInstance("F:\\物件-2.gif");
        jpg.SetAbsolutePosition(455, 450);
        jpg.ScaleToFit(70, 40);

        pdfDoc.Add(jpg);
    }

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
                dc.FixedHeight = (int)oValue;
                
                break;
            default:
                break;
        }
        return dc;
    }

    
    //pdfTable = new PdfPTable(new float[] { 400 });
    //    pdfTable.DefaultCell.Border = 1;
    //    pdfTable.SplitRows = true;
    //    pdfTable.KeepTogether = true;
    //    tbInner01 = new PdfPTable(new float[] { 1f, 3f, 1f });
    //    //Title_col1:image
    //     jpg = iTextSharp.text.Image.GetInstance("F:\\Title01.JPG");
    //    jpg.SetAbsolutePosition(0, 0);
    //    jpg.ScalePercent(30f);
    //    tc = new PdfPCell(jpg, true);
    //    tc.Border = 0;
    //    tc.PaddingTop = 1;
    //    tc.PaddingLeft = 1;
    //    tbInner01.AddCell(CF(tc, "B", 0));

    //    //Title_col2: 3 rows
    //    tbTemp = new PdfPTable(1);
    //    p1 = new Paragraph("遠傳電信股份有險公司 台大公館門市", largerFont);
    //    tbTemp.AddCell(CF(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER), "B", 0));
    //    p1 = new Paragraph("電子計算機統一發票", s09);
    //    tbTemp.AddCell(CF(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER), "B", 0));
    //    p1 = new Paragraph("中華民國" + DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日", s09);
    //    tbTemp.AddCell(CF(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER), "B", 0));
    //    tbInner01.AddCell(CF(new PdfPCell(tbTemp), "B", 0));
    //    p1 = new Paragraph("", s09);
    //    tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));

    //    //add to Outer Table
    //    pdfTable.AddCell(tbInner01);

    //    //Master:Table 2 columns
    //    tbInner01 = new PdfPTable(new float[] { 3f, 1f });

    //    tbInner02 = new PdfPTable(2);
    //    //tbInner02.DefaultCell.SetLeading(10, 10);
    //    p1 = new Paragraph("發票號碼:", s09);
    //    tbInner02.AddCell(CF(CF(new PdfPCell(p1), "B", 0),"HT",15));
    //    p1 = new Paragraph("", s09);
    //    tbInner02.AddCell(CF(new PdfPCell(p1), "B", 0));
    //    p1 = new Paragraph("買受人:", s09);
    //    tbInner02.AddCell(CF(new PdfPCell(p1), "B", 0));
    //    p1 = new Paragraph("", s09);
    //    tbInner02.AddCell(CF(new PdfPCell(p1), "B", 0));
    //    tbInner01.AddCell(CF(new PdfPCell(tbInner02),"B",0));

    //    //tbInner02.AddCell(CF(new PdfPCell(p1), "B", 0));
    //    //p1 = new Paragraph("統一編號:", s09);
    //    //tbInner02.AddCell(CF(new PdfPCell(p1), "B", 0));
    //    //p1 = new Paragraph("檢查號碼:", s09);
    //    //tbInner02.AddCell(CF(CF(new PdfPCell(p1), "B", 0),"H",Element.ALIGN_CENTER));
    //    //p1 = new Paragraph("地址:", s09);
    //    //tbInner02.AddCell(CF(CF(new PdfPCell(p1), "B", 0),"H",5));
    //    //p1 = new Paragraph("", s09);
    //    //tbInner02.AddCell(CF(new PdfPCell(p1), "B", 0));

    //    //tbInner01.AddCell(new PdfPCell(tbInner02));
        
    //    PdfPTable tbInner05 = new PdfPTable(3);
    //    tbInner05.DefaultCell.Border = 1;
    //    Paragraph p2 = new Paragraph("買受人註記欄", s05);
    //    //p1 = new Paragraph("買受人註記欄", s05);
    //    tc2 = new PdfPCell(p2);
    //    tc2.Colspan = 3;
    //    tc2.Border=1;
    //    tbInner05.AddCell(tc2);
    //    p2 = new Paragraph("區分", s05);
    //    tbInner05.AddCell(CF(new PdfPCell(p2), "B", 1));
    //    p2 = new Paragraph("進貨及費用", s05);
    //    tbInner05.AddCell(CF(new PdfPCell(p2), "B", 1));
    //    p2 = new Paragraph("固定資產", s05);
    //    tbInner05.AddCell(CF(new PdfPCell(p2), "B", 1));

    //    p2 = new Paragraph("得抵扣", s05);
    //    tbInner05.AddCell(CF(new PdfPCell(p2), "B", 1));
    //    p2 = new Paragraph("", s05);
    //    tbInner05.AddCell(CF(new PdfPCell(p2), "B", 1));
    //    p2 = new Paragraph("", s05);
    //    tbInner05.AddCell(CF(new PdfPCell(p2), "B", 1));

    //    p2 = new Paragraph("不得抵扣", s05);
    //    tbInner05.AddCell(CF(new PdfPCell(p2), "B", 1));
    //    p2 = new Paragraph("", s05);
    //    tbInner05.AddCell(CF(new PdfPCell(p2), "B", 1));
    //    p2 = new Paragraph("", s05);
    //    tbInner05.AddCell(CF(new PdfPCell(p2), "B", 1));

    //    tc = new PdfPCell(tbInner05);
    //    //tc = new PdfPCell(new Paragraph("123"));
    //    //tc.Rowspan = 2;
    //    //tc.FixedHeight = 50;
    //    tbInner01.AddCell(tc);
    //    PdfContentByte pb=
    //    //p1 = new Paragraph("買受人:", s09);
    //    //tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));
    //    ////p1 = new Paragraph("", s09);
    //    ////tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));
    //    //p1 = new Paragraph("統一編號:", s09);
    //    //tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));
    //    //p1 = new Paragraph("檢查號碼:", s09);
    //    //tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));
    //    //p1 = new Paragraph("地址:", s09);
    //    //tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));
    //    //p1 = new Paragraph("", s09);
    //    //tbInner01.AddCell(CF(new PdfPCell(p1), "B", 0));
    //    //add to Outer Table
    //    pdfTable.AddCell(tbInner01);

    //    pdfDoc.Add(pdfTable);
}

