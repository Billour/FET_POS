using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OracleClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Configuration;
using System.Web;

namespace Advtek.Utility.Utility
{
    class DebitNote
    {
        #region 屬性宣告
        protected Font s02;
        protected Font s03;
        protected Font s04;
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
        protected PdfPTable tbBorder;
        protected DataTable dtData;    //前端傳來的資料表
        protected string filename;
        protected PdfWriter writer;
        protected string filePath;
        protected string ChapPath;
        //OracleConnection gCon;
        //string sCon;
        #endregion
        public DebitNote()
        {
            #region PDF區塊，字型設定
            FontFactory.Register(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\..\Fonts\simhei.ttf");
            FontFactory.Register(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\..\Fonts\kaiu.ttf");
            s02 = ChineseFontFactory.Create("標楷體", 2f, Font.NORMAL);
            s03 = ChineseFontFactory.Create("標楷體", 3f, Font.NORMAL);
            s04 = ChineseFontFactory.Create("標楷體", 4f, Font.NORMAL);
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
            tbBorder = new PdfPTable(1);
            tbBorder.DefaultCell.Border = 0;
            tbBorder.SetTotalWidth(new float[] { 532f });
            tbBorder.LockedWidth = true;
            tbBorder.SpacingBefore = 0;
            tbBorder.SpacingAfter = 0;

            #region 取得下載路徑的系統參數設定
            //using (OracleConnection con = OracleDBUtil.GetConnection())
            //{
            //    try
            //    {
            //        string sql = "SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY='INVOICE_DOWNLOAD_PATH'";
            //        using (DataTable dt = new DataTable())
            //        {
            //            using (OracleCommand cmd = new OracleCommand(sql, con))
            //            {
            //                using (OracleDataAdapter da = new OracleDataAdapter(cmd))
            //                {
            //                    da.Fill(dt);
            //                    filePath = "~" + dt.Rows[0][0].ToString();
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        filePath = "~/Downloads";

            //        throw new Exception("查詢系統參數資料表錯誤(SYS_PARA.PARA_KEY='INVOICE_DOWNLOAD_PATH')");
            //    }
            //    finally
            //    {
            //        //if (con.State == ConnectionState.Open) con.Close();
            //        //con.Dispose();
            //        //OracleConnection.ClearAllPools();
            //    }
            //}
            #endregion
            #endregion
        }

        public void getnerateDebitNote(string PdfFileName)
        {
            //PDF Start
            Paragraph p1;
            PdfPCell tc;
            pdfDoc = new Document(PageSize.A4, 0f, 0f, 24f, 10f);
            //檔名
            if (!string.IsNullOrEmpty(PdfFileName))
            {
                filename = PdfFileName + ".pdf";
            }
            else
            {
                filename = Guid.NewGuid().ToString() + ".pdf";
            }

            //產生PDF檔案
            FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath("~/Downloads"), filename), FileMode.Create);
            writer = PdfWriter.GetInstance(pdfDoc, stream);

            pdfDoc.Open();

            Table01(pdfDoc);

            //TABLE間的間距
            tc = new PdfPCell(new Paragraph(""));
            tc.FixedHeight = 10f;
            tc.BorderWidth = 0;
            tbBorder.AddCell(tc);

            Table02(pdfDoc);

            //TABLE間的間距
            tc = new PdfPCell(new Paragraph(""));
            tc.FixedHeight = 10f;
            tc.BorderWidth = 0;
            tbBorder.AddCell(tc);

            Table03(pdfDoc);

            //TABLE間的間距
            tc = new PdfPCell(new Paragraph(""));
            tc.FixedHeight = 10f;
            tc.BorderWidth = 0;
            tbBorder.AddCell(tc);

            Table04(pdfDoc);

            pdfDoc.Add(tbBorder);
            pdfDoc.Close();

        }

        private void Table01(Document pDoc)
        {
            //外框(pdfTable)，1欄
            //PdfPTable tbBorder = new PdfPTable(1);
            //tbBorder.DefaultCell.Border = 0;
            //tbBorder.SetTotalWidth(new float[] { 532f });
            //tbBorder.LockedWidth = true;
            //tbBorder.SpacingBefore = 0;
            //tbBorder.SpacingAfter = 0;

            //Temparory variable
            PdfPTable tbTemp01 = null;
            PdfPTable tbTemp02 = null;
            //PdfPTable tbTemp03 = null;
            PdfPCell tc01 = null;
            Paragraph p01 = null;
            //標題(pdfTable),3欄
            PdfPTable tbTitle = new PdfPTable(new float[] { 160f, 218f, 154f });
            tbTitle.DefaultCell.BorderWidth = 0;
            tbTitle.SpacingAfter = 0;
            tbTitle.SpacingBefore = 0;
            tbTitle.WidthPercentage = 100f;
            //PdfPCell tc = new PdfPCell(new Paragraph("123"));
            //tbTitle.AddCell(new Paragraph("1"));
            tbTemp01 = new PdfPTable(new float[] { 20f, 35f, 105f });
            tbTemp01.SpacingAfter = 0;
            tbTemp01.SpacingBefore = 0;
            tbTemp01.LockedWidth = true;
            tbTemp01.TotalWidth = 160f;
            //tbTemp01.WidthPercentage = 100f;

            //標題>1欄>左
            tbTemp02 = new PdfPTable(new float[] { 10f, 10f });
            tbTemp02.DefaultCell.BorderWidth = 0;
            tbTemp02.SpacingAfter = 0;
            tbTemp02.SpacingBefore = 0;
            tbTemp02.TotalWidth = 20f;
            tbTemp02.LockedWidth = true;

            tc01 = new PdfPCell(new Paragraph("原開立銷貨", s07));
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tc01.PaddingLeft = 3f;
            tc01.PaddingRight = 1f;
            tbTemp02.AddCell(tc01);

            p01 = new Paragraph("發", s07);
            //p01.SpacingAfter = 0;
            p01.Add(new Chunk("\n ", s03));
            p01.Add(new Chunk("票", s07));
            p01.Add(new Chunk("\n ", s04));
            p01.Add(new Chunk("單位", s07));

            tc01 = new PdfPCell(p01);
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tc01.PaddingLeft = 1f;
            tc01.PaddingRight = 3f;
            tbTemp02.AddCell(tc01);

            tc01 = new PdfPCell(tbTemp02);

            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 0f;
            tc01.PaddingLeft = 0f;
            tc01.PaddingRight = 0f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            //tc01.FixedHeight = 45.354f;
            //tc01.Rowspan = 3;
            tbTemp01.AddCell(tc01);

            //標題>1欄>中
            tbTemp02 = new PdfPTable(new float[] { 1f });
            tbTemp02.SpacingAfter = 0;
            tbTemp02.SpacingBefore = 0;
            tc01 = new PdfPCell(new Paragraph("名    稱", s06));
            tc01.FixedHeight = 15.118f;
            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 4f;
            tc01.PaddingLeft = 5f;
            tc01.PaddingRight = 5f;
            tc01.BorderWidthTop = 0;
            //tc01.BorderWidthBottom = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthRight = 0.5f;

            tbTemp02.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("營利事業\n統一發票", s06));
            tc01.FixedHeight = 15.118f;
            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 0f;
            tc01.PaddingLeft = 4f;
            tc01.PaddingRight = 4f;
            //tc01.BorderWidthLeft = 0.5f;
            tc01.BorderWidthTop = 0f;
            //tc01.BorderWidthBottom = 0;
            tc01.BorderWidthRight = 0.5f;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("營業所在\n地    址", s06));
            tc01.FixedHeight = 15.118f;
            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 0f;
            tc01.PaddingLeft = 4f;
            tc01.PaddingRight = 4f;
            //tc01.BorderWidthLeft = 0;
            //tc01.BorderWidthLeft = 0.5f;
            tc01.BorderWidthTop = 0;
            tbTemp02.AddCell(tc01);

            tc01 = new PdfPCell(tbTemp02);
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbTemp01.AddCell(tc01);

            //標題>1欄>右
            tbTemp02 = new PdfPTable(new float[] { 1f });
            tbTemp02.SpacingAfter = 0;
            tbTemp02.SpacingBefore = 0;
            tc01 = new PdfPCell(new Paragraph("", s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthTop = 0;
            tc01.BorderWidthLeft = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthTop = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthTop = 0;
            tbTemp02.AddCell(tc01);

            tc01 = new PdfPCell(tbTemp02);
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;

            tbTemp01.AddCell(tc01);
            #region backup01
            //tc01 = new PdfPCell(new Paragraph("...",s05));
            //tbTemp01.AddCell(tc01);

            //tc01 = new PdfPCell(new Paragraph("營利事業統一發票",s05));
            //tbTemp01.AddCell(tc01);

            //tc01 = new PdfPCell(new Paragraph("...",s05));
            //tbTemp01.AddCell(tc01);

            //tc01 = new PdfPCell(new Paragraph("營業所在地址",s05));
            //tbTemp01.AddCell(tc01);

            //tc01 = new PdfPCell(new Paragraph("...",s05));
            //tbTemp01.AddCell(tc01);
            #endregion
            tc01 = new PdfPCell(tbTemp01);
            tc01.BorderWidthBottom = 1;
            tc01.BorderWidthLeft = 1;
            tc01.BorderWidthRight = 1;
            tc01.BorderWidthTop = 1;

            //tbTitle.AddCell(tbTemp01);
            tbTitle.AddCell(tc01);

            //標題>2欄
            //tbTitle.AddCell(new Paragraph("2"));
            tbTemp01 = new PdfPTable(1);
            tbTemp01.SpacingAfter = 0;
            tbTemp01.SpacingBefore = 0;
            tbTemp01.WidthPercentage = 100f;

            p01 = new Paragraph("營業人銷貨退回進貨退出或折讓單證明書", s10);
            tc01 = new PdfPCell(p01);
            tc01.PaddingBottom = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingLeft = 10f;

            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            //tc01.VerticalAlignment = Element.ALIGN_TOP;
            tbTemp01.AddCell(tc01);

            p01 = new Paragraph("中 華 民 國     年    月    日", s09);
            tc01 = new PdfPCell(p01);
            tc01.PaddingLeft = 30f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbTemp01.AddCell(tc01);

            p01 = new Paragraph("", s10);
            tc01 = new PdfPCell(p01);

            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbTemp01.AddCell(tc01);

            tc01 = new PdfPCell();
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tc01.PaddingTop = 0;
            tc01.AddElement(tbTemp01);
            tbTitle.AddCell(tc01);

            //標題>3欄
            tbTitle.AddCell(new Paragraph(""));

            tc01 = new PdfPCell(tbTitle);
            tc01.PaddingBottom = 0;
            tc01.PaddingTop = 0;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbBorder.AddCell(tc01);

            //單號,3欄
            PdfPTable tbNO = new PdfPTable(new float[] { 3.3f, 1f, 1f, 1f, 1f, 1f, 1f });
            tbNO.DefaultCell.BorderWidthBottom = 0;
            tbNO.DefaultCell.BorderWidthLeft = 0;
            tbNO.DefaultCell.BorderWidthRight = 0;
            tbNO.DefaultCell.BorderWidthTop = 0;
            tbNO.AddCell(new Paragraph(""));
            tbNO.AddCell(new Paragraph("折讓單號：", s07));
            tbNO.AddCell(new Paragraph("", s07));//折讓單號
            tc01 = new PdfPCell(new Paragraph("訂單編號：", s07));
            tc01.PaddingLeft = 17f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbNO.AddCell(tc01);
            tbNO.AddCell(new Paragraph("", s07));//訂單編號
            tbNO.AddCell(new Paragraph("門市代號：", s07));
            tbNO.AddCell(new Paragraph("", s07));//門市代號
            tc01 = new PdfPCell(tbNO);
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbBorder.AddCell(tc01);

            //內容,11欄
            PdfPTable tbContent = new PdfPTable(new float[] { 1f, 3f, 3f, 7f, 3f, 4f, 4.5f, 4f, 1f, 1f, 1f });
            //tbContent.AddCell(new Paragraph("c01"));
            tc01 = new PdfPCell(new Paragraph("開    立    發    票", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 3;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c02"));
            tc01 = new PdfPCell(new Paragraph("品         名", s09));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Rowspan = 2;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c03"));
            tc01 = new PdfPCell(new Paragraph("數 量", s09));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Rowspan = 2;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c04"));
            tc01 = new PdfPCell(new Paragraph("單  價", s09));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Rowspan = 2;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c05"));
            tc01 = new PdfPCell(new Paragraph("退  出  或  折  讓  內  容", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 2;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c06"));
            tc01 = new PdfPCell(new Paragraph("備    註", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 3;
            tbContent.AddCell(tc01);


            //line2
            tc01 = new PdfPCell(new Paragraph("聯式", s06));
            tc01.PaddingLeft = 0;
            tc01.PaddingRight = 0;
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c02"));
            tc01 = new PdfPCell(new Paragraph("年 /月 /日", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c03"));
            tc01 = new PdfPCell(new Paragraph("發 票 號 碼", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);


            //tbContent.AddCell(new Paragraph("c05"));
            tc01 = new PdfPCell(new Paragraph("金額(不含稅之進貨額)", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c06"));
            tc01 = new PdfPCell(new Paragraph("營業稅額", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c06"));
            tc01 = new PdfPCell(new Paragraph("應稅", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.PaddingRight = 0;
            tc01.PaddingLeft = 0;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c06"));
            tc01 = new PdfPCell(new Paragraph("零稅率", s05));
            tc01.PaddingRight = 0;
            tc01.PaddingLeft = 0;
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c06"));
            tc01 = new PdfPCell(new Paragraph("免稅", s06));
            tc01.PaddingRight = 0;
            tc01.PaddingLeft = 0;
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            //line3
            tc01 = new PdfPCell(new Paragraph("", s07));//c01
            tc01.FixedHeight = 50f;
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c02
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c03
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c04
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c05
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c06
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c07
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c08
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c09
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c10
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c11
            tbContent.AddCell(tc01);

            //line4
            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 4;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("總      計", s08));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 2;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(tbContent);
            tc01.BorderWidthBottom = 1;
            tc01.BorderWidthLeft = 1;
            tc01.BorderWidthRight = 1;
            tc01.BorderWidthTop = 1;
            tc01.PaddingBottom = 0;
            tbBorder.AddCell(tc01);


            //Footer
            PdfPTable tbFooter = new PdfPTable(1);
            p01 = new Paragraph(@"                                                                                                                                       第一聯:交付原銷貨人作為銷項稅額之扣繳憑證", s06);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingBottom = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingRight = 0;
            tbFooter.AddCell(tc01);

            p01 = new Paragraph("本證明單所列銷貨退回或折讓，確屬事實，特此證明。", s08);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);

            p01 = new Paragraph("原進貨營業人(或原買受人)名稱:                       (簽章或姓名)               地址:", s08);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);

            p01 = new Paragraph("身分證字號/營利事業統一編號:  XXXXXXXX", s08);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);

            //pdfTable.AddCell(CF(new PdfPCell(tbFooter), "B", 0));
            tc01 = new PdfPCell(tbFooter);
            tc01.BorderWidth = 0;
            tbBorder.AddCell(tc01);

            //外框加入Document
            //pDoc.Add(tbBorder);
            //tbBorder.TotalHeight
            //畫虛線
            PdfContentByte cb = writer.DirectContent;
            for (int i = 0; i < pdfDoc.Right; i += 4)
            {
                cb.MoveTo((pdfDoc.Left) + i, pdfDoc.Top - tbBorder.TotalHeight - 5);
                cb.LineTo((pdfDoc.Left) + i + 1, pdfDoc.Top - tbBorder.TotalHeight - 5);
                //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfDoc.Top / 2);
                //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfDoc.Top / 2);
                //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfTable.TotalHeight + 22.84f);
                //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfTable.TotalHeight + 22.84f);
                //cb.MoveTo(pdfDoc.Left + i, 420.945f);
                //cb.LineTo(pdfDoc.Left + i + 1, 420.945f);
            }
            cb.Stroke();
        }

        private void Table02(Document pDoc)
        {
            //外框(pdfTable)，1欄
            //PdfPTable tbBorder = new PdfPTable(1);
            //tbBorder.DefaultCell.Border = 0;
            //tbBorder.SetTotalWidth(new float[] { 532f });
            //tbBorder.LockedWidth = true;
            //tbBorder.SpacingBefore = 0;
            //tbBorder.SpacingAfter = 0;

            //Temparory variable
            PdfPTable tbTemp01 = null;
            PdfPTable tbTemp02 = null;
            //PdfPTable tbTemp03 = null;
            PdfPCell tc01 = null;
            Paragraph p01 = null;
            //標題(pdfTable),3欄
            PdfPTable tbTitle = new PdfPTable(new float[] { 160f, 218f, 154f });
            tbTitle.DefaultCell.BorderWidth = 0;
            tbTitle.SpacingAfter = 0;
            tbTitle.SpacingBefore = 0;
            tbTitle.WidthPercentage = 100f;
            //PdfPCell tc = new PdfPCell(new Paragraph("123"));
            //tbTitle.AddCell(new Paragraph("1"));
            tbTemp01 = new PdfPTable(new float[] { 20f, 35f, 105f });
            tbTemp01.SpacingAfter = 0;
            tbTemp01.SpacingBefore = 0;
            tbTemp01.LockedWidth = true;
            tbTemp01.TotalWidth = 160f;
            //tbTemp01.WidthPercentage = 100f;

            //標題>1欄>左
            tbTemp02 = new PdfPTable(new float[] { 10f, 10f });
            tbTemp02.DefaultCell.BorderWidth = 0;
            tbTemp02.SpacingAfter = 0;
            tbTemp02.SpacingBefore = 0;
            tbTemp02.TotalWidth = 20f;
            tbTemp02.LockedWidth = true;

            tc01 = new PdfPCell(new Paragraph("原開立銷貨", s07));
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tc01.PaddingLeft = 3f;
            tc01.PaddingRight = 1f;
            tbTemp02.AddCell(tc01);

            p01 = new Paragraph("發", s07);
            //p01.SpacingAfter = 0;
            p01.Add(new Chunk("\n ", s03));
            p01.Add(new Chunk("票", s07));
            p01.Add(new Chunk("\n ", s04));
            p01.Add(new Chunk("單位", s07));

            tc01 = new PdfPCell(p01);
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tc01.PaddingLeft = 1f;
            tc01.PaddingRight = 3f;
            tbTemp02.AddCell(tc01);

            tc01 = new PdfPCell(tbTemp02);

            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 0f;
            tc01.PaddingLeft = 0f;
            tc01.PaddingRight = 0f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            //tc01.FixedHeight = 45.354f;
            //tc01.Rowspan = 3;
            tbTemp01.AddCell(tc01);

            //標題>1欄>中
            tbTemp02 = new PdfPTable(new float[] { 1f });
            tbTemp02.SpacingAfter = 0;
            tbTemp02.SpacingBefore = 0;
            tc01 = new PdfPCell(new Paragraph("名    稱", s06));
            tc01.FixedHeight = 15.118f;
            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 4f;
            tc01.PaddingLeft = 5f;
            tc01.PaddingRight = 5f;
            tc01.BorderWidthTop = 0;
            //tc01.BorderWidthBottom = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthRight = 0.5f;

            tbTemp02.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("營利事業\n統一發票", s06));
            tc01.FixedHeight = 15.118f;
            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 0f;
            tc01.PaddingLeft = 4f;
            tc01.PaddingRight = 4f;
            //tc01.BorderWidthLeft = 0.5f;
            tc01.BorderWidthTop = 0f;
            //tc01.BorderWidthBottom = 0;
            tc01.BorderWidthRight = 0.5f;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("營業所在\n地    址", s06));
            tc01.FixedHeight = 15.118f;
            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 0f;
            tc01.PaddingLeft = 4f;
            tc01.PaddingRight = 4f;
            //tc01.BorderWidthLeft = 0;
            //tc01.BorderWidthLeft = 0.5f;
            tc01.BorderWidthTop = 0;
            tbTemp02.AddCell(tc01);

            tc01 = new PdfPCell(tbTemp02);
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbTemp01.AddCell(tc01);

            //標題>1欄>右
            tbTemp02 = new PdfPTable(new float[] { 1f });
            tbTemp02.SpacingAfter = 0;
            tbTemp02.SpacingBefore = 0;
            tc01 = new PdfPCell(new Paragraph("", s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthTop = 0;
            tc01.BorderWidthLeft = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthTop = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthTop = 0;
            tbTemp02.AddCell(tc01);

            tc01 = new PdfPCell(tbTemp02);
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;

            tbTemp01.AddCell(tc01);
            #region backup01
            //tc01 = new PdfPCell(new Paragraph("...",s05));
            //tbTemp01.AddCell(tc01);

            //tc01 = new PdfPCell(new Paragraph("營利事業統一發票",s05));
            //tbTemp01.AddCell(tc01);

            //tc01 = new PdfPCell(new Paragraph("...",s05));
            //tbTemp01.AddCell(tc01);

            //tc01 = new PdfPCell(new Paragraph("營業所在地址",s05));
            //tbTemp01.AddCell(tc01);

            //tc01 = new PdfPCell(new Paragraph("...",s05));
            //tbTemp01.AddCell(tc01);
            #endregion
            tc01 = new PdfPCell(tbTemp01);
            tc01.BorderWidthBottom = 1;
            tc01.BorderWidthLeft = 1;
            tc01.BorderWidthRight = 1;
            tc01.BorderWidthTop = 1;

            //tbTitle.AddCell(tbTemp01);
            tbTitle.AddCell(tc01);

            //標題>2欄
            //tbTitle.AddCell(new Paragraph("2"));
            tbTemp01 = new PdfPTable(1);
            tbTemp01.SpacingAfter = 0;
            tbTemp01.SpacingBefore = 0;
            tbTemp01.WidthPercentage = 100f;

            p01 = new Paragraph("營業人銷貨退回進貨退出或折讓單證明書", s10);
            tc01 = new PdfPCell(p01);
            tc01.PaddingBottom = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingLeft = 10f;

            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            //tc01.VerticalAlignment = Element.ALIGN_TOP;
            tbTemp01.AddCell(tc01);

            p01 = new Paragraph("中 華 民 國     年    月    日", s09);
            tc01 = new PdfPCell(p01);
            tc01.PaddingLeft = 30f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbTemp01.AddCell(tc01);

            p01 = new Paragraph("", s10);
            tc01 = new PdfPCell(p01);

            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbTemp01.AddCell(tc01);

            tc01 = new PdfPCell();
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tc01.PaddingTop = 0;
            tc01.AddElement(tbTemp01);
            tbTitle.AddCell(tc01);

            //標題>3欄
            tbTitle.AddCell(new Paragraph(""));

            tc01 = new PdfPCell(tbTitle);
            tc01.PaddingBottom = 0;
            tc01.PaddingTop = 0;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbBorder.AddCell(tc01);

            //單號,3欄
            PdfPTable tbNO = new PdfPTable(new float[] { 3.3f, 1f, 1f, 1f, 1f, 1f, 1f });
            tbNO.DefaultCell.BorderWidthBottom = 0;
            tbNO.DefaultCell.BorderWidthLeft = 0;
            tbNO.DefaultCell.BorderWidthRight = 0;
            tbNO.DefaultCell.BorderWidthTop = 0;
            tbNO.AddCell(new Paragraph(""));
            tbNO.AddCell(new Paragraph("折讓單號：", s07));
            tbNO.AddCell(new Paragraph("", s07));//折讓單號
            tc01 = new PdfPCell(new Paragraph("訂單編號：", s07));
            tc01.PaddingLeft = 17f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbNO.AddCell(tc01);
            tbNO.AddCell(new Paragraph("", s07));//訂單編號
            tbNO.AddCell(new Paragraph("門市代號：", s07));
            tbNO.AddCell(new Paragraph("", s07));//門市代號
            tc01 = new PdfPCell(tbNO);
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbBorder.AddCell(tc01);

            //內容,11欄
            PdfPTable tbContent = new PdfPTable(new float[] { 1f, 3f, 3f, 7f, 3f, 4f, 4.5f, 4f, 1f, 1f, 1f });
            //tbContent.AddCell(new Paragraph("c01"));
            tc01 = new PdfPCell(new Paragraph("開    立    發    票", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 3;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c02"));
            tc01 = new PdfPCell(new Paragraph("品         名", s09));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Rowspan = 2;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c03"));
            tc01 = new PdfPCell(new Paragraph("數 量", s09));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Rowspan = 2;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c04"));
            tc01 = new PdfPCell(new Paragraph("單  價", s09));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Rowspan = 2;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c05"));
            tc01 = new PdfPCell(new Paragraph("退  出  或  折  讓  內  容", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 2;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c06"));
            tc01 = new PdfPCell(new Paragraph("備    註", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 3;
            tbContent.AddCell(tc01);


            //line2
            tc01 = new PdfPCell(new Paragraph("聯式", s06));
            tc01.PaddingLeft = 0;
            tc01.PaddingRight = 0;
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c02"));
            tc01 = new PdfPCell(new Paragraph("年 /月 /日", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c03"));
            tc01 = new PdfPCell(new Paragraph("發 票 號 碼", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);


            //tbContent.AddCell(new Paragraph("c05"));
            tc01 = new PdfPCell(new Paragraph("金額(不含稅之進貨額)", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c06"));
            tc01 = new PdfPCell(new Paragraph("營業稅額", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c06"));
            tc01 = new PdfPCell(new Paragraph("應稅", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.PaddingRight = 0;
            tc01.PaddingLeft = 0;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c06"));
            tc01 = new PdfPCell(new Paragraph("零稅率", s05));
            tc01.PaddingRight = 0;
            tc01.PaddingLeft = 0;
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c06"));
            tc01 = new PdfPCell(new Paragraph("免稅", s06));
            tc01.PaddingRight = 0;
            tc01.PaddingLeft = 0;
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            //line3
            tc01 = new PdfPCell(new Paragraph("", s07));//c01
            tc01.FixedHeight = 50f;
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c02
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c03
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c04
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c05
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c06
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c07
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c08
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c09
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c10
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c11
            tbContent.AddCell(tc01);

            //line4
            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 4;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("總      計", s08));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 2;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(tbContent);
            tc01.BorderWidthBottom = 1;
            tc01.BorderWidthLeft = 1;
            tc01.BorderWidthRight = 1;
            tc01.BorderWidthTop = 1;
            tc01.PaddingBottom = 0;
            tbBorder.AddCell(tc01);


            //Footer
            PdfPTable tbFooter = new PdfPTable(1);
            p01 = new Paragraph(@"                                                                                                                                       第二聯:交付原銷貨人作為記賬憑證", s06);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingBottom = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingRight = 0;
            tbFooter.AddCell(tc01);

            p01 = new Paragraph("本證明單所列銷貨退回或折讓，確屬事實，特此證明。", s08);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);

            p01 = new Paragraph("原進貨營業人(或原買受人)名稱:                       (簽章或姓名)               地址:", s08);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);

            p01 = new Paragraph("身分證字號/營利事業統一編號:  XXXXXXXX", s08);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);

            //pdfTable.AddCell(CF(new PdfPCell(tbFooter), "B", 0));
            tc01 = new PdfPCell(tbFooter);
            tc01.BorderWidth = 0;
            tbBorder.AddCell(tc01);

            //外框加入Document
            //pDoc.Add(tbBorder);
            //tbBorder.TotalHeight
            //畫虛線
            PdfContentByte cb = writer.DirectContent;
            for (int i = 0; i < pdfDoc.Right; i += 4)
            {
                cb.MoveTo((pdfDoc.Left) + i, pdfDoc.Top - tbBorder.TotalHeight - 5);
                cb.LineTo((pdfDoc.Left) + i + 1, pdfDoc.Top - tbBorder.TotalHeight - 5);
                //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfDoc.Top / 2);
                //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfDoc.Top / 2);
                //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfTable.TotalHeight + 22.84f);
                //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfTable.TotalHeight + 22.84f);
                //cb.MoveTo(pdfDoc.Left + i, 420.945f);
                //cb.LineTo(pdfDoc.Left + i + 1, 420.945f);
            }
            cb.Stroke();
        }

        private void Table03(Document pDoc)
        {
            //外框(pdfTable)，1欄
            //PdfPTable tbBorder = new PdfPTable(1);
            //tbBorder.DefaultCell.Border = 0;
            //tbBorder.SetTotalWidth(new float[] { 532f });
            //tbBorder.LockedWidth = true;
            //tbBorder.SpacingBefore = 0;
            //tbBorder.SpacingAfter = 0;

            //Temparory variable
            PdfPTable tbTemp01 = null;
            PdfPTable tbTemp02 = null;
            //PdfPTable tbTemp03 = null;
            PdfPCell tc01 = null;
            Paragraph p01 = null;
            //標題(pdfTable),3欄
            PdfPTable tbTitle = new PdfPTable(new float[] { 160f, 218f, 154f });
            tbTitle.DefaultCell.BorderWidth = 0;
            tbTitle.SpacingAfter = 0;
            tbTitle.SpacingBefore = 0;
            tbTitle.WidthPercentage = 100f;
            //PdfPCell tc = new PdfPCell(new Paragraph("123"));
            //tbTitle.AddCell(new Paragraph("1"));
            tbTemp01 = new PdfPTable(new float[] { 20f, 35f, 105f });
            tbTemp01.SpacingAfter = 0;
            tbTemp01.SpacingBefore = 0;
            tbTemp01.LockedWidth = true;
            tbTemp01.TotalWidth = 160f;
            //tbTemp01.WidthPercentage = 100f;

            //標題>1欄>左
            tbTemp02 = new PdfPTable(new float[] { 10f, 10f });
            tbTemp02.DefaultCell.BorderWidth = 0;
            tbTemp02.SpacingAfter = 0;
            tbTemp02.SpacingBefore = 0;
            tbTemp02.TotalWidth = 20f;
            tbTemp02.LockedWidth = true;

            tc01 = new PdfPCell(new Paragraph("原開立銷貨", s07));
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tc01.PaddingLeft = 3f;
            tc01.PaddingRight = 1f;
            tbTemp02.AddCell(tc01);

            p01 = new Paragraph("發", s07);
            //p01.SpacingAfter = 0;
            p01.Add(new Chunk("\n ", s03));
            p01.Add(new Chunk("票", s07));
            p01.Add(new Chunk("\n ", s04));
            p01.Add(new Chunk("單位", s07));

            tc01 = new PdfPCell(p01);
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tc01.PaddingLeft = 1f;
            tc01.PaddingRight = 3f;
            tbTemp02.AddCell(tc01);

            tc01 = new PdfPCell(tbTemp02);

            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 0f;
            tc01.PaddingLeft = 0f;
            tc01.PaddingRight = 0f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            //tc01.FixedHeight = 45.354f;
            //tc01.Rowspan = 3;
            tbTemp01.AddCell(tc01);

            //標題>1欄>中
            tbTemp02 = new PdfPTable(new float[] { 1f });
            tbTemp02.SpacingAfter = 0;
            tbTemp02.SpacingBefore = 0;
            tc01 = new PdfPCell(new Paragraph("名    稱", s06));
            tc01.FixedHeight = 15.118f;
            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 4f;
            tc01.PaddingLeft = 5f;
            tc01.PaddingRight = 5f;
            tc01.BorderWidthTop = 0;
            //tc01.BorderWidthBottom = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthRight = 0.5f;

            tbTemp02.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("營利事業\n統一發票", s06));
            tc01.FixedHeight = 15.118f;
            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 0f;
            tc01.PaddingLeft = 4f;
            tc01.PaddingRight = 4f;
            //tc01.BorderWidthLeft = 0.5f;
            tc01.BorderWidthTop = 0f;
            //tc01.BorderWidthBottom = 0;
            tc01.BorderWidthRight = 0.5f;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("營業所在\n地    址", s06));
            tc01.FixedHeight = 15.118f;
            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 0f;
            tc01.PaddingLeft = 4f;
            tc01.PaddingRight = 4f;
            //tc01.BorderWidthLeft = 0;
            //tc01.BorderWidthLeft = 0.5f;
            tc01.BorderWidthTop = 0;
            tbTemp02.AddCell(tc01);

            tc01 = new PdfPCell(tbTemp02);
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbTemp01.AddCell(tc01);

            //標題>1欄>右
            tbTemp02 = new PdfPTable(new float[] { 1f });
            tbTemp02.SpacingAfter = 0;
            tbTemp02.SpacingBefore = 0;
            tc01 = new PdfPCell(new Paragraph("", s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthTop = 0;
            tc01.BorderWidthLeft = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthTop = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthTop = 0;
            tbTemp02.AddCell(tc01);

            tc01 = new PdfPCell(tbTemp02);
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;

            tbTemp01.AddCell(tc01);
            #region backup01
            //tc01 = new PdfPCell(new Paragraph("...",s05));
            //tbTemp01.AddCell(tc01);

            //tc01 = new PdfPCell(new Paragraph("營利事業統一發票",s05));
            //tbTemp01.AddCell(tc01);

            //tc01 = new PdfPCell(new Paragraph("...",s05));
            //tbTemp01.AddCell(tc01);

            //tc01 = new PdfPCell(new Paragraph("營業所在地址",s05));
            //tbTemp01.AddCell(tc01);

            //tc01 = new PdfPCell(new Paragraph("...",s05));
            //tbTemp01.AddCell(tc01);
            #endregion
            tc01 = new PdfPCell(tbTemp01);
            tc01.BorderWidthBottom = 1;
            tc01.BorderWidthLeft = 1;
            tc01.BorderWidthRight = 1;
            tc01.BorderWidthTop = 1;

            //tbTitle.AddCell(tbTemp01);
            tbTitle.AddCell(tc01);

            //標題>2欄
            //tbTitle.AddCell(new Paragraph("2"));
            tbTemp01 = new PdfPTable(1);
            tbTemp01.SpacingAfter = 0;
            tbTemp01.SpacingBefore = 0;
            tbTemp01.WidthPercentage = 100f;

            p01 = new Paragraph("營業人銷貨退回進貨退出或折讓單證明書", s10);
            tc01 = new PdfPCell(p01);
            tc01.PaddingBottom = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingLeft = 10f;

            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            //tc01.VerticalAlignment = Element.ALIGN_TOP;
            tbTemp01.AddCell(tc01);

            p01 = new Paragraph("中 華 民 國     年    月    日", s09);
            tc01 = new PdfPCell(p01);
            tc01.PaddingLeft = 30f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbTemp01.AddCell(tc01);

            p01 = new Paragraph("", s10);
            tc01 = new PdfPCell(p01);

            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbTemp01.AddCell(tc01);

            tc01 = new PdfPCell();
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tc01.PaddingTop = 0;
            tc01.AddElement(tbTemp01);
            tbTitle.AddCell(tc01);

            //標題>3欄
            tbTitle.AddCell(new Paragraph(""));

            tc01 = new PdfPCell(tbTitle);
            tc01.PaddingBottom = 0;
            tc01.PaddingTop = 0;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbBorder.AddCell(tc01);

            //單號,3欄
            PdfPTable tbNO = new PdfPTable(new float[] { 3.3f, 1f, 1f, 1f, 1f, 1f, 1f });
            tbNO.DefaultCell.BorderWidthBottom = 0;
            tbNO.DefaultCell.BorderWidthLeft = 0;
            tbNO.DefaultCell.BorderWidthRight = 0;
            tbNO.DefaultCell.BorderWidthTop = 0;
            tbNO.AddCell(new Paragraph(""));
            tbNO.AddCell(new Paragraph("折讓單號：", s07));
            tbNO.AddCell(new Paragraph("", s07));//折讓單號
            tc01 = new PdfPCell(new Paragraph("訂單編號：", s07));
            tc01.PaddingLeft = 17f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbNO.AddCell(tc01);
            tbNO.AddCell(new Paragraph("", s07));//訂單編號
            tbNO.AddCell(new Paragraph("門市代號：", s07));
            tbNO.AddCell(new Paragraph("", s07));//門市代號
            tc01 = new PdfPCell(tbNO);
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbBorder.AddCell(tc01);

            //內容,11欄
            PdfPTable tbContent = new PdfPTable(new float[] { 1f, 3f, 3f, 7f, 3f, 4f, 4.5f, 4f, 1f, 1f, 1f });
            //tbContent.AddCell(new Paragraph("c01"));
            tc01 = new PdfPCell(new Paragraph("開    立    發    票", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 3;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c02"));
            tc01 = new PdfPCell(new Paragraph("品         名", s09));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Rowspan = 2;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c03"));
            tc01 = new PdfPCell(new Paragraph("數 量", s09));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Rowspan = 2;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c04"));
            tc01 = new PdfPCell(new Paragraph("單  價", s09));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Rowspan = 2;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c05"));
            tc01 = new PdfPCell(new Paragraph("退  出  或  折  讓  內  容", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 2;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c06"));
            tc01 = new PdfPCell(new Paragraph("備    註", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 3;
            tbContent.AddCell(tc01);


            //line2
            tc01 = new PdfPCell(new Paragraph("聯式", s06));
            tc01.PaddingLeft = 0;
            tc01.PaddingRight = 0;
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c02"));
            tc01 = new PdfPCell(new Paragraph("年 /月 /日", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c03"));
            tc01 = new PdfPCell(new Paragraph("發 票 號 碼", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);


            //tbContent.AddCell(new Paragraph("c05"));
            tc01 = new PdfPCell(new Paragraph("金額(不含稅之進貨額)", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c06"));
            tc01 = new PdfPCell(new Paragraph("營業稅額", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c06"));
            tc01 = new PdfPCell(new Paragraph("應稅", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.PaddingRight = 0;
            tc01.PaddingLeft = 0;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c06"));
            tc01 = new PdfPCell(new Paragraph("零稅率", s05));
            tc01.PaddingRight = 0;
            tc01.PaddingLeft = 0;
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c06"));
            tc01 = new PdfPCell(new Paragraph("免稅", s06));
            tc01.PaddingRight = 0;
            tc01.PaddingLeft = 0;
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            //line3
            tc01 = new PdfPCell(new Paragraph("", s07));//c01
            tc01.FixedHeight = 50f;
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c02
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c03
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c04
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c05
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c06
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c07
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c08
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c09
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c10
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c11
            tbContent.AddCell(tc01);

            //line4
            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 4;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("總      計", s08));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 2;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(tbContent);
            tc01.BorderWidthBottom = 1;
            tc01.BorderWidthLeft = 1;
            tc01.BorderWidthRight = 1;
            tc01.BorderWidthTop = 1;
            tc01.PaddingBottom = 0;
            tbBorder.AddCell(tc01);


            //Footer
            PdfPTable tbFooter = new PdfPTable(1);
            p01 = new Paragraph(@"                                                                                                                                       第三聯:由進貨人作為進項稅額之扣減憑證", s06);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingBottom = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingRight = 0;
            tbFooter.AddCell(tc01);

            p01 = new Paragraph("本證明單所列銷貨退回或折讓，確屬事實，特此證明。", s08);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);

            p01 = new Paragraph("原進貨營業人(或原買受人)名稱:                       (簽章或姓名)               地址:", s08);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);

            p01 = new Paragraph("身分證字號/營利事業統一編號:  XXXXXXXX", s08);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);

            //pdfTable.AddCell(CF(new PdfPCell(tbFooter), "B", 0));
            tc01 = new PdfPCell(tbFooter);
            tc01.BorderWidth = 0;
            tbBorder.AddCell(tc01);

            //外框加入Document
            //pDoc.Add(tbBorder);
            //tbBorder.TotalHeight
            //畫虛線
            PdfContentByte cb = writer.DirectContent;
            for (int i = 0; i < pdfDoc.Right; i += 4)
            {
                cb.MoveTo((pdfDoc.Left) + i, pdfDoc.Top - tbBorder.TotalHeight - 5);
                cb.LineTo((pdfDoc.Left) + i + 1, pdfDoc.Top - tbBorder.TotalHeight - 5);
                //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfDoc.Top / 2);
                //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfDoc.Top / 2);
                //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfTable.TotalHeight + 22.84f);
                //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfTable.TotalHeight + 22.84f);
                //cb.MoveTo(pdfDoc.Left + i, 420.945f);
                //cb.LineTo(pdfDoc.Left + i + 1, 420.945f);
            }
            cb.Stroke();
        }

        private void Table04(Document pDoc)
        {
            //外框(pdfTable)，1欄
            //PdfPTable tbBorder = new PdfPTable(1);
            //tbBorder.DefaultCell.Border = 0;
            //tbBorder.SetTotalWidth(new float[] { 532f });
            //tbBorder.LockedWidth = true;
            //tbBorder.SpacingBefore = 0;
            //tbBorder.SpacingAfter = 0;

            //Temparory variable
            PdfPTable tbTemp01 = null;
            PdfPTable tbTemp02 = null;
            //PdfPTable tbTemp03 = null;
            PdfPCell tc01 = null;
            Paragraph p01 = null;
            //標題(pdfTable),3欄
            PdfPTable tbTitle = new PdfPTable(new float[] { 160f, 218f, 154f });
            tbTitle.DefaultCell.BorderWidth = 0;
            tbTitle.SpacingAfter = 0;
            tbTitle.SpacingBefore = 0;
            tbTitle.WidthPercentage = 100f;
            //PdfPCell tc = new PdfPCell(new Paragraph("123"));
            //tbTitle.AddCell(new Paragraph("1"));
            tbTemp01 = new PdfPTable(new float[] { 20f, 35f, 105f });
            tbTemp01.SpacingAfter = 0;
            tbTemp01.SpacingBefore = 0;
            tbTemp01.LockedWidth = true;
            tbTemp01.TotalWidth = 160f;
            //tbTemp01.WidthPercentage = 100f;

            //標題>1欄>左
            tbTemp02 = new PdfPTable(new float[] { 10f, 10f });
            tbTemp02.DefaultCell.BorderWidth = 0;
            tbTemp02.SpacingAfter = 0;
            tbTemp02.SpacingBefore = 0;
            tbTemp02.TotalWidth = 20f;
            tbTemp02.LockedWidth = true;

            tc01 = new PdfPCell(new Paragraph("原開立銷貨", s07));
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tc01.PaddingLeft = 3f;
            tc01.PaddingRight = 1f;
            tbTemp02.AddCell(tc01);

            p01 = new Paragraph("發", s07);
            //p01.SpacingAfter = 0;
            p01.Add(new Chunk("\n ", s03));
            p01.Add(new Chunk("票", s07));
            p01.Add(new Chunk("\n ", s04));
            p01.Add(new Chunk("單位", s07));

            tc01 = new PdfPCell(p01);
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tc01.PaddingLeft = 1f;
            tc01.PaddingRight = 3f;
            tbTemp02.AddCell(tc01);

            tc01 = new PdfPCell(tbTemp02);

            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 0f;
            tc01.PaddingLeft = 0f;
            tc01.PaddingRight = 0f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            //tc01.FixedHeight = 45.354f;
            //tc01.Rowspan = 3;
            tbTemp01.AddCell(tc01);

            //標題>1欄>中
            tbTemp02 = new PdfPTable(new float[] { 1f });
            tbTemp02.SpacingAfter = 0;
            tbTemp02.SpacingBefore = 0;
            tc01 = new PdfPCell(new Paragraph("名    稱", s06));
            tc01.FixedHeight = 15.118f;
            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 4f;
            tc01.PaddingLeft = 5f;
            tc01.PaddingRight = 5f;
            tc01.BorderWidthTop = 0;
            //tc01.BorderWidthBottom = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthRight = 0.5f;

            tbTemp02.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("營利事業\n統一發票", s06));
            tc01.FixedHeight = 15.118f;
            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 0f;
            tc01.PaddingLeft = 4f;
            tc01.PaddingRight = 4f;
            //tc01.BorderWidthLeft = 0.5f;
            tc01.BorderWidthTop = 0f;
            //tc01.BorderWidthBottom = 0;
            tc01.BorderWidthRight = 0.5f;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("營業所在\n地    址", s06));
            tc01.FixedHeight = 15.118f;
            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 0f;
            tc01.PaddingLeft = 4f;
            tc01.PaddingRight = 4f;
            //tc01.BorderWidthLeft = 0;
            //tc01.BorderWidthLeft = 0.5f;
            tc01.BorderWidthTop = 0;
            tbTemp02.AddCell(tc01);

            tc01 = new PdfPCell(tbTemp02);
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbTemp01.AddCell(tc01);

            //標題>1欄>右
            tbTemp02 = new PdfPTable(new float[] { 1f });
            tbTemp02.SpacingAfter = 0;
            tbTemp02.SpacingBefore = 0;
            tc01 = new PdfPCell(new Paragraph("", s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthTop = 0;
            tc01.BorderWidthLeft = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthTop = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthTop = 0;
            tbTemp02.AddCell(tc01);

            tc01 = new PdfPCell(tbTemp02);
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;

            tbTemp01.AddCell(tc01);
            #region backup01
            //tc01 = new PdfPCell(new Paragraph("...",s05));
            //tbTemp01.AddCell(tc01);

            //tc01 = new PdfPCell(new Paragraph("營利事業統一發票",s05));
            //tbTemp01.AddCell(tc01);

            //tc01 = new PdfPCell(new Paragraph("...",s05));
            //tbTemp01.AddCell(tc01);

            //tc01 = new PdfPCell(new Paragraph("營業所在地址",s05));
            //tbTemp01.AddCell(tc01);

            //tc01 = new PdfPCell(new Paragraph("...",s05));
            //tbTemp01.AddCell(tc01);
            #endregion
            tc01 = new PdfPCell(tbTemp01);
            tc01.BorderWidthBottom = 1;
            tc01.BorderWidthLeft = 1;
            tc01.BorderWidthRight = 1;
            tc01.BorderWidthTop = 1;

            //tbTitle.AddCell(tbTemp01);
            tbTitle.AddCell(tc01);

            //標題>2欄
            //tbTitle.AddCell(new Paragraph("2"));
            tbTemp01 = new PdfPTable(1);
            tbTemp01.SpacingAfter = 0;
            tbTemp01.SpacingBefore = 0;
            tbTemp01.WidthPercentage = 100f;

            p01 = new Paragraph("營業人銷貨退回進貨退出或折讓單證明書", s10);
            tc01 = new PdfPCell(p01);
            tc01.PaddingBottom = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingLeft = 10f;

            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            //tc01.VerticalAlignment = Element.ALIGN_TOP;
            tbTemp01.AddCell(tc01);

            p01 = new Paragraph("中 華 民 國     年    月    日", s09);
            tc01 = new PdfPCell(p01);
            tc01.PaddingLeft = 30f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbTemp01.AddCell(tc01);

            p01 = new Paragraph("", s10);
            tc01 = new PdfPCell(p01);

            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbTemp01.AddCell(tc01);

            tc01 = new PdfPCell();
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tc01.PaddingTop = 0;
            tc01.AddElement(tbTemp01);
            tbTitle.AddCell(tc01);

            //標題>3欄
            tbTitle.AddCell(new Paragraph(""));

            tc01 = new PdfPCell(tbTitle);
            tc01.PaddingBottom = 0;
            tc01.PaddingTop = 0;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbBorder.AddCell(tc01);

            //單號,3欄
            PdfPTable tbNO = new PdfPTable(new float[] { 3.3f, 1f, 1f, 1f, 1f, 1f, 1f });
            tbNO.DefaultCell.BorderWidthBottom = 0;
            tbNO.DefaultCell.BorderWidthLeft = 0;
            tbNO.DefaultCell.BorderWidthRight = 0;
            tbNO.DefaultCell.BorderWidthTop = 0;
            tbNO.AddCell(new Paragraph(""));
            tbNO.AddCell(new Paragraph("折讓單號：", s07));
            tbNO.AddCell(new Paragraph("", s07));//折讓單號
            tc01 = new PdfPCell(new Paragraph("訂單編號：", s07));
            tc01.PaddingLeft = 17f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbNO.AddCell(tc01);
            tbNO.AddCell(new Paragraph("", s07));//訂單編號
            tbNO.AddCell(new Paragraph("門市代號：", s07));
            tbNO.AddCell(new Paragraph("", s07));//門市代號
            tc01 = new PdfPCell(tbNO);
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbBorder.AddCell(tc01);

            //內容,11欄
            PdfPTable tbContent = new PdfPTable(new float[] { 1f, 3f, 3f, 7f, 3f, 4f, 4.5f, 4f, 1f, 1f, 1f });
            //tbContent.AddCell(new Paragraph("c01"));
            tc01 = new PdfPCell(new Paragraph("開    立    發    票", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 3;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c02"));
            tc01 = new PdfPCell(new Paragraph("品         名", s09));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Rowspan = 2;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c03"));
            tc01 = new PdfPCell(new Paragraph("數 量", s09));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Rowspan = 2;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c04"));
            tc01 = new PdfPCell(new Paragraph("單  價", s09));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Rowspan = 2;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c05"));
            tc01 = new PdfPCell(new Paragraph("退  出  或  折  讓  內  容", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 2;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c06"));
            tc01 = new PdfPCell(new Paragraph("備    註", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 3;
            tbContent.AddCell(tc01);


            //line2
            tc01 = new PdfPCell(new Paragraph("聯式", s06));
            tc01.PaddingLeft = 0;
            tc01.PaddingRight = 0;
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c02"));
            tc01 = new PdfPCell(new Paragraph("年 /月 /日", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c03"));
            tc01 = new PdfPCell(new Paragraph("發 票 號 碼", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);


            //tbContent.AddCell(new Paragraph("c05"));
            tc01 = new PdfPCell(new Paragraph("金額(不含稅之進貨額)", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c06"));
            tc01 = new PdfPCell(new Paragraph("營業稅額", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c06"));
            tc01 = new PdfPCell(new Paragraph("應稅", s06));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.PaddingRight = 0;
            tc01.PaddingLeft = 0;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c06"));
            tc01 = new PdfPCell(new Paragraph("零稅率", s05));
            tc01.PaddingRight = 0;
            tc01.PaddingLeft = 0;
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            //tbContent.AddCell(new Paragraph("c06"));
            tc01 = new PdfPCell(new Paragraph("免稅", s06));
            tc01.PaddingRight = 0;
            tc01.PaddingLeft = 0;
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            //line3
            tc01 = new PdfPCell(new Paragraph("", s07));//c01
            tc01.FixedHeight = 50f;
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c02
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c03
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c04
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c05
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c06
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c07
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c08
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c09
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c10
            tbContent.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph("", s07));//c11
            tbContent.AddCell(tc01);

            //line4
            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 4;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("總      計", s08));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 2;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(tbContent);
            tc01.BorderWidthBottom = 1;
            tc01.BorderWidthLeft = 1;
            tc01.BorderWidthRight = 1;
            tc01.BorderWidthTop = 1;
            tc01.PaddingBottom = 0;
            tbBorder.AddCell(tc01);


            //Footer
            PdfPTable tbFooter = new PdfPTable(1);
            p01 = new Paragraph(@"                                                                                                                                       第四聯:由進貨人作為記賬憑證", s06);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingBottom = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingRight = 0;
            tbFooter.AddCell(tc01);

            p01 = new Paragraph("本證明單所列銷貨退回或折讓，確屬事實，特此證明。", s08);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);

            p01 = new Paragraph("原進貨營業人(或原買受人)名稱:                       (簽章或姓名)               地址:", s08);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);

            p01 = new Paragraph("身分證字號/營利事業統一編號:  XXXXXXXX", s08);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);

            //pdfTable.AddCell(CF(new PdfPCell(tbFooter), "B", 0));
            tc01 = new PdfPCell(tbFooter);
            tc01.BorderWidth = 0;
            tbBorder.AddCell(tc01);

            //外框加入Document
            //pDoc.Add(tbBorder);
            //tbBorder.TotalHeight
            ////畫虛線
            //PdfContentByte cb = writer.DirectContent;
            //for (int i = 0; i < pdfDoc.Right; i += 4)
            //{
            //    cb.MoveTo((pdfDoc.Left) + i, pdfDoc.Top - tbBorder.TotalHeight - 5);
            //    cb.LineTo((pdfDoc.Left) + i + 1, pdfDoc.Top - tbBorder.TotalHeight - 5);
            //    //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfDoc.Top / 2);
            //    //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfDoc.Top / 2);
            //    //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfTable.TotalHeight + 22.84f);
            //    //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfTable.TotalHeight + 22.84f);
            //    //cb.MoveTo(pdfDoc.Left + i, 420.945f);
            //    //cb.LineTo(pdfDoc.Left + i + 1, 420.945f);
            //}
            //cb.Stroke();
        }
    }
}
