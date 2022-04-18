using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.OracleClient;
using System.Web;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Configuration;



namespace Advtek.Utility
{
    public class Receipt
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
        protected Font sdebit;

        protected Font largerFont;
        protected Font RedsmallFont;

        protected Font s021;
        protected Font s031;
        protected Font s041;
        protected Font s051;
        protected Font s061;
        protected Font s071;
        protected Font s081;
        protected Font s091;
        protected Font s101;
        protected Font s1051;
        protected Font s111;
        protected Font s141;

        protected Font largerFont1;
        protected Font RedsmallFont1;
        protected Document pdfDoc;
        protected PdfPTable pdfTable;  //組成整體樣式
        protected PdfPTable details;   //組成明細樣式
        protected DataTable dtData;    //前端傳來的資料表
        protected PdfPTable tbBorder;
        protected PdfPTable cleartb;
        protected string filename;
        protected PdfWriter writer;
        protected PdfWriter writer1;
        protected string filePath;
        protected string ChapPath;

        //OracleConnection gCon;
        //string sCon;
        #endregion

        public Receipt()
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
         //   sdebit = ChineseFontFactory.Create("標楷體", 

            largerFont = ChineseFontFactory.Create("標楷體", 12f, Font.BOLD);
            RedsmallFont = ChineseFontFactory.Create("標楷體", 9f, Font.NORMAL, BaseColor.RED);

            s021 = ChineseFontFactory.Create("新細明體", 2f, Font.NORMAL);
            s031 = ChineseFontFactory.Create("新細明體", 3f, Font.NORMAL);
            s041 = ChineseFontFactory.Create("新細明體", 4f, Font.NORMAL);
            s051 = ChineseFontFactory.Create("新細明體", 5f, Font.NORMAL);
            s061 = ChineseFontFactory.Create("新細明體", 6f, Font.NORMAL);
            s071 = ChineseFontFactory.Create("新細明體", 7f, Font.NORMAL);
            s081 = ChineseFontFactory.Create("新細明體", 8f, Font.NORMAL);
            s091 = ChineseFontFactory.Create("新細明體", 9f, Font.NORMAL);
            s101 = ChineseFontFactory.Create("新細明體", 10f, Font.NORMAL);
            s1051 = ChineseFontFactory.Create("新細明體", 10.5f, Font.NORMAL);
            s111 = ChineseFontFactory.Create("新細明體", 11f, Font.NORMAL);
            s141 = ChineseFontFactory.Create("新細明體", 14f, Font.NORMAL);

            largerFont1 = ChineseFontFactory.Create("新細明體", 12f, Font.BOLD);
            RedsmallFont1 = ChineseFontFactory.Create("新細明體", 9f, Font.NORMAL, BaseColor.RED);
            tbBorder = new PdfPTable(1);
            tbBorder.DefaultCell.Border = 0;
            tbBorder.SetTotalWidth(new float[] { 532f });
            tbBorder.LockedWidth = true;
            tbBorder.SpacingBefore = 0;
            tbBorder.SpacingAfter = 0;
            cleartb = new PdfPTable(1);
            cleartb.DefaultCell.Border = 0;
            cleartb.SetTotalWidth(new float[] { 532f });
            cleartb.LockedWidth = true;
            cleartb.SpacingBefore = 0;
            cleartb.SpacingAfter = 0;

            using (OracleConnection con = OracleDBUtil.GetConnection())
            {
                try
                {
                    string sql = "SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY='INVOICE_DOWNLOAD_PATH'";
                    using (DataTable dt = new DataTable())
                    {
                        using (OracleCommand cmd = new OracleCommand(sql, con))
                        {
                            using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                            {
                                da.Fill(dt);
                                filePath = "~" + dt.Rows[0][0].ToString();
                            }
                        }
                    }
                }
                catch //(Exception ex)
                {
                    filePath = "~/Downloads";

                    throw new Exception("查詢系統參數資料表錯誤(SYS_PARA.PARA_KEY='INVOICE_DOWNLOAD_PATH')");
                }
                finally
                {
                    //if (con.State == ConnectionState.Open) con.Close();
                    //con.Dispose();
                    //OracleConnection.ClearAllPools();
                }

                try
                {
                    string sql = "SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY='INVOICE_CHAPTER_PATH'";
                    using (DataTable dt = new DataTable())
                    {
                        using (OracleCommand cmd = new OracleCommand(sql, con))
                        {
                            using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                            {
                                da.Fill(dt);
                                ChapPath = "~" + dt.Rows[0][0].ToString();
                            }
                        }
                    }
                }
                catch //(Exception ex)
                {
                    filePath = "~/IMAGES/INVOICECHAPTERS";

                    throw new Exception("查詢系統參數資料表錯誤(SYS_PARA.PARA_KEY='INVOICE_CHAPTER_PATH')");
                }
                finally
                {
                    if (con.State == ConnectionState.Open) con.Close();
                    con.Dispose();
                    OracleConnection.ClearAllPools();
                }

            }

            #endregion



        }

        /// <summary>
        /// 發票測試用function
        /// </summary>
        /// <param name="POSUUID_MASTER">SALE_HEAD.POSUUID_MASTER</param>
        /// <returns>檔名(~/Downloads/)</returns>
        public string test(string POSUUID_MASTER)
        {
            string fname = generateReceipt(POSUUID_MASTER);
            return fname;

            #region 測試
            //   Barcode test = new Barcode();
            //       BarcodePDF417 test = new BarcodePDF417();
            //BarcodeEAN testbarcode = new BarcodeEAN();



            ////取得資料
            //DataTable dtOrg = getReceiptData(POSUUID_MASTER);

            ////處理細項長度
            //DataTable dtItem = new DataTable();
            //dtItem.Columns.Add("PROD_NAME");
            //dtItem.Columns.Add("QUANTITY");
            //dtItem.Columns.Add("PRICE");
            //dtItem.Columns.Add("AMOUNT");

            //getProperItem(dtOrg, dtItem);

            //DataSet dsItems = new DataSet();
            //DataTable dtTo = null;
            //if (dtItem.Rows.Count > 0) 
            //{
            //    int TablesCount = dtItem.Rows.Count / 13 + ((dtItem.Rows.Count % 13 == 0) ? 0 : 1);
            //    for (int i = 0; i < dtItem.Rows.Count; i++)
            //    {
            //        if (i % 13 == 0 )
            //        {
            //            dtTo = dtItem.Clone();
            //            dsItems.Tables.Add(dtTo);
            //        }
            //        DataRow drFrom = dtItem.Rows[i];
            //        DataRow drTo = dtTo.NewRow();
            //        drTo.ItemArray = drFrom.ItemArray;
            //        dtTo.Rows.Add(drTo);
            //    }
            //}

            ////處理備註長度
            //DataTable dtRemark = new DataTable();
            //dtRemark.Columns.Add("REMARK");
            //getProperRemark(dtOrg, dtRemark);

            //DataSet dsRemark = new DataSet();
            //dtTo = null;
            //if (dtRemark.Rows.Count > 0) 
            //{
            //    //1頁9列
            //    int TablesCount = dtRemark.Rows.Count / 9 + ((dtRemark.Rows.Count % 9 == 0) ? 0 : 1);
            //    for (int i = 0; i < dtRemark.Rows.Count; i++)
            //    {
            //        if (i % 9 == 0)
            //        {
            //            dtTo = dtRemark.Clone();
            //            dsRemark.Tables.Add(dtTo);
            //        }
            //        DataRow drFrom = dtRemark.Rows[i];
            //        DataRow drTo = dtTo.NewRow();
            //        drTo.ItemArray = drFrom.ItemArray;
            //        dtTo.Rows.Add(drTo);
            //    }

            //}

            ////return dsItems;
            //return dsRemark;
            #endregion
        }

        public static Image GetBarcode128(PdfContentByte pdfContentByte, string code, bool extended, int codeType)
        {
            Barcode128 code128 = new Barcode128 { Code = code, Extended = extended, CodeType = codeType };
            
            return code128.CreateImageWithBarcode(pdfContentByte, null, null);
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

        public string testbarcode(DataTable dt)
        {
            Document document = new Document(PageSize.A4, 0f, 0f, 24f, 10f);
            string Path1 = @"\Downloads\";
            string fileName = Guid.NewGuid() + ".pdf";
            string path = HttpContext.Current.Server.MapPath("~") + Path1 + fileName;
            //  string sSubPath = filePath + "/" + sYYYYMMDD + "/" + sStoreNo;
            //  FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(sSubPath), filename), FileMode.Create);
            //  writer = PdfWriter.GetInstance(pdfDoc, stream);
            //  pdfDoc.Open();
            PdfWriter pdfWriter = PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));
            document.Open();
            pdfWriter.AddJavaScript("this.print(false);", true);


            PdfContentByte pdfContentByte = pdfWriter.DirectContent;
            PdfPTable table = new PdfPTable(2) { WidthPercentage = 100 };
            table.DefaultCell.Border = Rectangle.NO_BORDER;
            table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.DefaultCell.FixedHeight = 70;
            Paragraph p1;
            PdfPCell tc;
            foreach (DataRow dr in dt.Rows)
            {
                PdfPTable tbInner04 = new PdfPTable(1);
                tbInner04.DefaultCell.Border = Rectangle.NO_BORDER;
                tbInner04.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tbInner04.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                tbInner04.DefaultCell.FixedHeight = 50;
                p1 = new Paragraph(dr["PRODNAME"].ToString(), s10);

                // Barcode 128 EAN
                Image imageEan = GetBarcode128(pdfContentByte, dr["PRODNO"].ToString(), false, Barcode.EAN13);
                //       imageEan.
                

                tc = new PdfPCell(imageEan, false);
                tc.Border = Rectangle.NO_BORDER;
                tc.VerticalAlignment = Element.ALIGN_MIDDLE;
                tc.HorizontalAlignment = Element.ALIGN_CENTER;
                //   tc.FixedHeight = 50;
                tbInner04.AddCell(tc);
                tc = new PdfPCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
                tc.Border = Rectangle.NO_BORDER;
                tc.VerticalAlignment = Element.ALIGN_MIDDLE;
                tc.HorizontalAlignment = Element.ALIGN_CENTER;
                // tc.FixedHeight = 50;
                tbInner04.AddCell(tc);



                table.AddCell(tbInner04);
            }
            int countt = Convert.ToInt32(dt.Rows.Count);
            Image test1 = GetBarcode128(pdfContentByte, "", false, Barcode.EAN13);
            if (countt % 2 == 1)
            {

                table.AddCell(table.DefaultCell);
            }
            //if (dt.Rows.Count == 1)
            //{
            //    Image imageEan = null;
            //    table.AddCell(new Phrase(new Chunk(imageEan, 0, 0)));
            //}
            document.Add(table);

            document.Close();
            return fileName;
        }
        /// <summary>
        /// INV08專用barcode
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="printerName"></param>
        public string inv08barcode(DataTable dt)
        {
            Document document = new Document(PageSize.A4, 0f, 0f, 24f, 10f);
            string Path1 = @"\Downloads\";
            string fileName = Guid.NewGuid() + ".pdf";
            string path = HttpContext.Current.Server.MapPath("~") + Path1 + fileName;
            //  string sSubPath = filePath + "/" + sYYYYMMDD + "/" + sStoreNo;
            //  FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(sSubPath), filename), FileMode.Create);
            //  writer = PdfWriter.GetInstance(pdfDoc, stream);
            //  pdfDoc.Open();
            PdfWriter pdfWriter = PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));
            document.Open();
            pdfWriter.AddJavaScript("this.print(false);", true);


            PdfContentByte pdfContentByte = pdfWriter.DirectContent;
            PdfPTable table = new PdfPTable(2) { WidthPercentage = 100 };
            table.DefaultCell.Border = Rectangle.NO_BORDER;
            table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.DefaultCell.FixedHeight = 70;
            Paragraph p1;
            PdfPCell tc;
            int cellcount = 0;
            foreach (DataRow dr in dt.Rows)
            {
                PdfPTable tbInner04 = new PdfPTable(1);
                tbInner04.DefaultCell.Border = Rectangle.NO_BORDER;
                tbInner04.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tbInner04.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                tbInner04.DefaultCell.FixedHeight = 50;
                p1 = new Paragraph(dr["PRODNAME"].ToString(), s10);
                int qcount = Convert.ToInt32(dr["ACCEPT_QTY"].ToString());
                for (int i = 0; i < qcount; i++)
                {
                    // Barcode 128 EAN
                    Image imageEan = GetBarcode128(pdfContentByte, dr["PRODNO"].ToString(), false, Barcode.EAN13);
                    //       imageEan.

                    tc = new PdfPCell(imageEan, false);
                    tc.Border = Rectangle.NO_BORDER;
                    tc.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tc.HorizontalAlignment = Element.ALIGN_CENTER;
                    //   tc.FixedHeight = 50;
                    tbInner04.AddCell(tc);
                    tc = new PdfPCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
                    tc.Border = Rectangle.NO_BORDER;
                    tc.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tc.HorizontalAlignment = Element.ALIGN_CENTER;
                    // tc.FixedHeight = 50;
                    tbInner04.AddCell(tc);
                    table.AddCell(tbInner04);
                    cellcount = cellcount + 1;
                }




            }
            int countt = Convert.ToInt32(dt.Rows.Count);
            Image test1 = GetBarcode128(pdfContentByte, "", false, Barcode.EAN13);
            if (cellcount % 2 == 1)
            {

                table.AddCell(table.DefaultCell);
            }
            //if (dt.Rows.Count == 1)
            //{
            //    Image imageEan = null;
            //    table.AddCell(new Phrase(new Chunk(imageEan, 0, 0)));
            //}
            document.Add(table);

            document.Close();
            return fileName;
        }

        /// <summary>
        /// 條碼列印barcode
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="printerName"></param>
        public string generateBarcode(string POSUUID_MASTER)
        {
            filename = "";
            try
            {
                //取得資料
                DataTable dtOrg = getReceiptData(POSUUID_MASTER);

                //20110127 發票檔名(年度+發票號碼)
                string sInvoiceNo = "";
                string sStoreNo = "";
                string sYYYYMMDD = "";
                if (dtOrg.Rows.Count > 0)
                {
                    DateTime aDate = Convert.ToDateTime(dtOrg.Rows[0]["INVOICE_DATE"]);
                    sInvoiceNo = aDate.Year.ToString();
                    sInvoiceNo += "_" + dtOrg.Rows[0]["INVOICE_NO"].ToString();
                    sStoreNo = dtOrg.Rows[0]["STORE_NO"].ToString();
                    sYYYYMMDD = aDate.ToString("yyyyMMdd");
                }

                //處理細項長度
                DataTable dtItem = new DataTable();
                dtItem.Columns.Add("PROD_NAME");
                dtItem.Columns.Add("QUANTITY");
                dtItem.Columns.Add("PRICE");
                dtItem.Columns.Add("AMOUNT");
                getProperItem(dtOrg, dtItem);

                DataSet dsItems = new DataSet();
                DataTable dtTo = null;
                //1頁13列
                if (dtItem.Rows.Count > 0)
                {
                    int TablesCount = dtItem.Rows.Count / 13 + ((dtItem.Rows.Count % 13 == 0) ? 0 : 1);
                    for (int i = 0; i < dtItem.Rows.Count; i++)
                    {
                        if (i % 13 == 0)
                        {
                            dtTo = dtItem.Clone();
                            dsItems.Tables.Add(dtTo);
                        }
                        DataRow drFrom = dtItem.Rows[i];
                        DataRow drTo = dtTo.NewRow();
                        drTo.ItemArray = drFrom.ItemArray;
                        dtTo.Rows.Add(drTo);
                    }
                }


                //處理備註長度
                DataTable dtRemark = new DataTable();
                dtRemark.Columns.Add("REMARK");
                getProperRemark(dtOrg, dtRemark);

                DataSet dsRemark = new DataSet();
                dtTo = null;
                //1頁9列
                if (dtRemark.Rows.Count > 0)
                {
                    //1頁9列
                    int TablesCount = dtRemark.Rows.Count / 9 + ((dtRemark.Rows.Count % 9 == 0) ? 0 : 1);
                    for (int i = 0; i < dtRemark.Rows.Count; i++)
                    {
                        if (i % 9 == 0)
                        {
                            dtTo = dtRemark.Clone();
                            dsRemark.Tables.Add(dtTo);
                        }
                        DataRow drFrom = dtRemark.Rows[i];
                        DataRow drTo = dtTo.NewRow();
                        drTo.ItemArray = drFrom.ItemArray;
                        dtTo.Rows.Add(drTo);
                    }

                }

                //PDF Start
                //PdfPCell tc;
                pdfDoc = new Document(PageSize.A4, 0f, 0f, 24f, 10f);
                //檔名
                if (!string.IsNullOrEmpty(sInvoiceNo))
                {
                    filename = "test.pdf";
                }
                else
                {
                    filename = "test.pdf";
                }
                //檢查路徑
                if (string.IsNullOrEmpty(sYYYYMMDD)) sYYYYMMDD = DateTime.Now.ToString("yyyyMMdd");
                string sSubPath = filePath + "/" + sYYYYMMDD + "/" + sStoreNo;
                checkDirectory(sSubPath);

                #region 備份CODE
                //if (!Directory.Exists(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD)))
                //{
                //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD));
                //}
                //if (!Directory.Exists(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD + "\\" + sStoreNo)))
                //{
                //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD + "\\" + sStoreNo));
                //}
                #endregion
                //產生PDF檔案
                //FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(filePath), filename), FileMode.Create);
                //FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD + "\\" + sStoreNo), filename), FileMode.Create);
                FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(sSubPath), filename), FileMode.Create);
                writer = PdfWriter.GetInstance(pdfDoc, stream);

                pdfDoc.Open();



                //string printerName = ConfigurationManager.AppSettings["PDFPrinterName"].ToString();//web.config中設定
                // 加入自動列印指令碼
                //writer.AddSilentPrintAction(printerName);
                writer.AddJavaScript("this.print(false);", true);



                int iCopies = (dsItems.Tables.Count > dsRemark.Tables.Count) ? dsItems.Tables.Count : dsRemark.Tables.Count;

                for (int i = 0; i < iCopies; i++)
                {
                    DataTable dtItem1 = null;
                    DataTable dtRemark1 = null;
                    if (i < dsItems.Tables.Count)
                    {
                        dtItem1 = dsItems.Tables[i];
                    }
                    else
                    {
                        dtItem1 = null;
                    }
                    if (i < dsRemark.Tables.Count)
                    {
                        dtRemark1 = dsRemark.Tables[i];
                    }
                    else
                    {
                        dtRemark1 = null;
                    }

                    //第一聯
                    addBarcode(pdfDoc, dtOrg, dtItem1, dtRemark1);

                    //畫虛線
                    PdfContentByte cb = writer.DirectContent;
                    for (int j = 0; j < pdfDoc.Right - 10; j += 4)
                    {
                        cb.MoveTo(pdfDoc.Left + j, 420.945f);
                        cb.LineTo(pdfDoc.Left + j + 1, 420.945f);
                    }
                    cb.Stroke();


                    //換頁
                    pdfDoc.NewPage();

                    //第二聯
                 //   add1(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(i), Convert.ToString(iCopies));

                    //產生第三聯
                    //add2(pdfDoc);
                    add3(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(1), Convert.ToString(iCopies * 2));

                    //產生註記欄
                    addMarkTable1(pdfDoc);
                    addMarkTable2(pdfDoc);

                    if (i != iCopies - 1)
                    {
                        //換頁
                        pdfDoc.NewPage();
                    }
                }

                pdfDoc.Close();
            }
            catch (Exception ex)
            {
                if (pdfDoc.IsOpen()) pdfDoc.Close();
                throw ex;
            }

            return filename;

        }

        public string generateReceipt(string POSUUID_MASTER)
        {
            filename = "";
            try
            {
                //取得資料
                DataTable dtOrg = getReceiptData(POSUUID_MASTER);

                //20110127 發票檔名(年度+發票號碼)
                string sInvoiceNo = "";
                string sStoreNo = "";
                string sYYYYMMDD = "";
                string UNINO = "";
                if (dtOrg.Rows.Count > 0)
                {
                    DateTime aDate = Convert.ToDateTime(dtOrg.Rows[0]["INVOICE_DATE"]);
                    sInvoiceNo = aDate.Year.ToString();
                    sInvoiceNo += "_" + dtOrg.Rows[0]["INVOICE_NO"].ToString();
                    sStoreNo = dtOrg.Rows[0]["STORE_NO"].ToString();
                    sYYYYMMDD = aDate.ToString("yyyyMMdd");
                    UNINO = dtOrg.Rows[0]["UNI_NO"].ToString();
                }

                //處理細項長度
                DataTable dtItem = new DataTable();
                dtItem.Columns.Add("PROD_NAME");
                dtItem.Columns.Add("QUANTITY");
                dtItem.Columns.Add("PRICE");
                dtItem.Columns.Add("AMOUNT");
                getProperItem(dtOrg, dtItem);

                DataSet dsItems = new DataSet();
                DataTable dtTo = null;
                //1頁13列
                if (dtItem.Rows.Count > 0)
                {
                    int TablesCount = dtItem.Rows.Count / 13 + ((dtItem.Rows.Count % 13 == 0) ? 0 : 1);
                    for (int i = 0; i < dtItem.Rows.Count; i++)
                    {
                        if (i % 13 == 0)
                        {
                            dtTo = dtItem.Clone();
                            dsItems.Tables.Add(dtTo);
                        }
                        DataRow drFrom = dtItem.Rows[i];
                        DataRow drTo = dtTo.NewRow();
                        drTo.ItemArray = drFrom.ItemArray;
                        dtTo.Rows.Add(drTo);
                    }
                }


                //處理備註長度
                DataTable dtRemark = new DataTable();
                dtRemark.Columns.Add("REMARK");
                getProperRemark(dtOrg, dtRemark);

                DataSet dsRemark = new DataSet();
                dtTo = null;
                //1頁9列
                if (dtRemark.Rows.Count > 0)
                {
                    //1頁9列
                    int TablesCount = dtRemark.Rows.Count / 9 + ((dtRemark.Rows.Count % 9 == 0) ? 0 : 1);
                    for (int i = 0; i < dtRemark.Rows.Count; i++)
                    {
                        if (i % 9 == 0)
                        {
                            dtTo = dtRemark.Clone();
                            dsRemark.Tables.Add(dtTo);
                        }
                        DataRow drFrom = dtRemark.Rows[i];
                        DataRow drTo = dtTo.NewRow();
                        drTo.ItemArray = drFrom.ItemArray;
                        dtTo.Rows.Add(drTo);
                    }

                }

                //PDF Start
                //Paragraph p1;
                //PdfPCell tc;
                pdfDoc = new Document(PageSize.A4, 0f, 0f, 24f, 10f);
                //檔名
                if (!string.IsNullOrEmpty(sInvoiceNo))
                {
                    filename = sInvoiceNo + ".pdf";
                }
                else
                {
                    filename = Guid.NewGuid().ToString() + ".pdf";
                }
                //檢查路徑
                if (string.IsNullOrEmpty(sYYYYMMDD)) sYYYYMMDD = DateTime.Now.ToString("yyyyMMdd");
                string sSubPath = filePath + "/" + DateTime.Now.ToString("yyyyMMdd") + "/" + sStoreNo;
                checkDirectory(sSubPath);

                #region 備份CODE
                //if (!Directory.Exists(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD)))
                //{
                //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD));
                //}
                //if (!Directory.Exists(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD + "\\" + sStoreNo)))
                //{
                //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD + "\\" + sStoreNo));
                //}
                #endregion
                //產生PDF檔案
                //FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(filePath), filename), FileMode.Create);
                //FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD + "\\" + sStoreNo), filename), FileMode.Create);
                FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(sSubPath), filename), FileMode.Create);
                writer = PdfWriter.GetInstance(pdfDoc, stream);

                pdfDoc.Open();

                string printerName = ConfigurationManager.AppSettings["Invoice_PDFPrinterName"].ToString();//web.config中設定
                // 加入自動列印指令碼
                AddPrintAction(writer, printerName);
                // writer.AddJavaScript("this.print(false);", true);




                int iCopies = (dsItems.Tables.Count > dsRemark.Tables.Count) ? dsItems.Tables.Count : dsRemark.Tables.Count;

                for (int i = 0; i < iCopies; i++)
                {
                    DataTable dtItem1 = null;
                    DataTable dtRemark1 = null;
                    if (i < dsItems.Tables.Count)
                    {
                        dtItem1 = dsItems.Tables[i];
                    }
                    else
                    {
                        dtItem1 = null;
                    }
                    if (i < dsRemark.Tables.Count)
                    {
                        dtRemark1 = dsRemark.Tables[i];
                    }
                    else
                    {
                        dtRemark1 = null;
                    }

                    ////第一聯先mark
                    ////第一聯
                    //add0(pdfDoc, dtOrg, dtItem1, dtRemark1);

                    ////畫虛線
                    //PdfContentByte cb = writer.DirectContent;
                    //for (int j = 0; j < pdfDoc.Right - 10; j += 4)
                    //{
                    //    cb.MoveTo(pdfDoc.Left + j, 420.945f);
                    //    cb.LineTo(pdfDoc.Left + j + 1, 420.945f);
                    //}
                    //cb.Stroke();


                    //換頁
                    pdfDoc.NewPage();

                    //第二聯
                    if (UNINO == "")
                    {
                        add1NoUNI(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(i), Convert.ToString(iCopies));
                    }
                    else
                    {
                        add1(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(i), Convert.ToString(iCopies));
                    }
                    

                    //產生第三聯
                    //add2(pdfDoc);
                    if (UNINO == "")
                    {
                        add3NoUNI(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(i), Convert.ToString(iCopies * 2));
                    }
                    else
                    {
                        add3(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(i), Convert.ToString(iCopies * 2));
                    }
                    

                    //產生註記欄
                    addMarkTable1(pdfDoc);
                    addMarkTable2(pdfDoc);

                    if (i != iCopies - 1)
                    {
                        //換頁
                        pdfDoc.NewPage();
                    }
                }

                pdfDoc.Close();
            }
            catch (Exception ex)
            {
                if (pdfDoc.IsOpen()) pdfDoc.Close();
                throw ex;
            }

            return filename;

        }

        public string generateDebitReceipt(string POSUUID_MASTER, string DPOSUUID_MASTER)
        {
            filename = "";
            try
            {
                //取得資料
                DataTable dtOrg = getReceiptData(POSUUID_MASTER);

                //20110127 發票檔名(年度+發票號碼)
                string sInvoiceNo = "";
                string sStoreNo = "";
                string sYYYYMMDD = "";
                if (dtOrg.Rows.Count > 0)
                {
                    DateTime aDate = Convert.ToDateTime(dtOrg.Rows[0]["INVOICE_DATE"]);
                    sInvoiceNo = aDate.Year.ToString();
                    sInvoiceNo += "_" + dtOrg.Rows[0]["INVOICE_NO"].ToString();
                    sStoreNo = dtOrg.Rows[0]["STORE_NO"].ToString();
                    sYYYYMMDD = aDate.ToString("yyyyMMdd");
                }

                //處理細項長度
                DataTable dtItem = new DataTable();
                dtItem.Columns.Add("PROD_NAME");
                dtItem.Columns.Add("QUANTITY");
                dtItem.Columns.Add("PRICE");
                dtItem.Columns.Add("AMOUNT");
                getProperItem(dtOrg, dtItem);

                DataSet dsItems = new DataSet();
                DataTable dtTo = null;
                //1頁13列
                if (dtItem.Rows.Count > 0)
                {
                    int TablesCount = dtItem.Rows.Count / 5 + ((dtItem.Rows.Count % 13 == 0) ? 0 : 1);
                    for (int i = 0; i < dtItem.Rows.Count; i++)
                    {
                        if (i % 5 == 0)
                        {
                            dtTo = dtItem.Clone();
                            dsItems.Tables.Add(dtTo);
                        }
                        DataRow drFrom = dtItem.Rows[i];
                        DataRow drTo = dtTo.NewRow();
                        drTo.ItemArray = drFrom.ItemArray;
                        dtTo.Rows.Add(drTo);
                    }
                }


                //處理備註長度
                DataTable dtRemark = new DataTable();
                dtRemark.Columns.Add("REMARK");
                getProperRemark(dtOrg, dtRemark);

                DataSet dsRemark = new DataSet();
                dtTo = null;
                //1頁9列
                if (dtRemark.Rows.Count > 0)
                {
                    //1頁9列
                    int TablesCount = dtRemark.Rows.Count / 9 + ((dtRemark.Rows.Count % 9 == 0) ? 0 : 1);
                    for (int i = 0; i < dtRemark.Rows.Count; i++)
                    {
                        if (i % 9 == 0)
                        {
                            dtTo = dtRemark.Clone();
                            dsRemark.Tables.Add(dtTo);
                        }
                        DataRow drFrom = dtRemark.Rows[i];
                        DataRow drTo = dtTo.NewRow();
                        drTo.ItemArray = drFrom.ItemArray;
                        dtTo.Rows.Add(drTo);
                    }

                }

                //PDF Start
                //Paragraph p1;
                PdfPCell tc;
                pdfDoc = new Document(PageSize.A4, 0f, 0f, 24f, 10f);
                //檔名
                if (!string.IsNullOrEmpty(sInvoiceNo))
                {
                    filename = sInvoiceNo + ".pdf";
                }
                else
                {
                    filename = Guid.NewGuid().ToString() + ".pdf";
                }
                //檢查路徑
                if (string.IsNullOrEmpty(sYYYYMMDD)) sYYYYMMDD = DateTime.Now.ToString("yyyyMMdd");
                string sSubPath = filePath + "/" + sYYYYMMDD + "/" + sStoreNo;
                checkDirectory(sSubPath);

                #region 備份CODE
                //if (!Directory.Exists(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD)))
                //{
                //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD));
                //}
                //if (!Directory.Exists(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD + "\\" + sStoreNo)))
                //{
                //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD + "\\" + sStoreNo));
                //}
                #endregion
                //產生PDF檔案
                //FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(filePath), filename), FileMode.Create);
                //FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD + "\\" + sStoreNo), filename), FileMode.Create);
                FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(sSubPath), filename), FileMode.Create);
                writer = PdfWriter.GetInstance(pdfDoc, stream);

                pdfDoc.Open();

                string printerName = ConfigurationManager.AppSettings["PDFPrinterName"].ToString();//web.config中設定
                // 加入自動列印指令碼
                AddPrintAction(writer, printerName);
                //              writer.AddJavaScript("this.print(false);", true);
                //


                int iCopies = (dsItems.Tables.Count > dsRemark.Tables.Count) ? dsItems.Tables.Count : dsRemark.Tables.Count;

                for (int i = 0; i < iCopies; i++)
                {
                    DataTable dtItem1 = null;
                    DataTable dtRemark1 = null;
                    if (i < dsItems.Tables.Count)
                    {
                        dtItem1 = dsItems.Tables[i];
                    }
                    else
                    {
                        dtItem1 = null;
                    }
                    if (i < dsRemark.Tables.Count)
                    {
                        dtRemark1 = dsRemark.Tables[i];
                    }
                    else
                    {
                        dtRemark1 = null;
                    }

                    //第一聯
               //     add0(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(i), Convert.ToString(iCopies));

                    //畫虛線
                    PdfContentByte cb = writer.DirectContent;
                    for (int j = 0; j < pdfDoc.Right - 10; j += 4)
                    {
                        cb.MoveTo(pdfDoc.Left + j, 420.945f);
                        cb.LineTo(pdfDoc.Left + j + 1, 420.945f);
                    }
                    cb.Stroke();


                    //換頁
                    pdfDoc.NewPage();

                    //第二聯
                    add1(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(i), Convert.ToString(iCopies));

                    //產生第三聯
                    //add2(pdfDoc);
                    add3(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(i), Convert.ToString(iCopies));

                    //產生註記欄
                    addMarkTable1(pdfDoc);
                    addMarkTable2(pdfDoc);

                    if (i != iCopies - 1)
                    {
                        //換頁
                        pdfDoc.NewPage();
                    }
                }

                pdfDoc.NewPage();

                DataTable dtOrg1 = getSALEData(POSUUID_MASTER);
                DataTable dtItemd = getDebitReceiptData(POSUUID_MASTER);

                string SALE_NO = "";
                string sStore_No = "";
                string CREDIT_NOTE_NO = "";
                string UNINO = "";
                string UNITITLE = "";
                if (dtOrg1.Rows.Count > 0)
                {
                    //  DateTime aDate = Convert.ToDateTime(dtOrg.Rows[0]["INVOICE_DATE"]);
                    //  sInvoiceNo = aDate.Year.ToString();
                    //  sInvoiceNo += "_" + dtOrg.Rows[0]["INVOICE_NO"].ToString();
                    sStore_No = dtOrg1.Rows[0]["STORE_NO"].ToString();
                    //  sYYYYMMDD = aDate.ToString("yyyyMMdd");
                    SALE_NO = dtOrg1.Rows[0]["SALE_NO"].ToString();
                    UNINO = (dtOrg1.Rows[0]["UNI_NO"] != DBNull.Value) ? dtOrg1.Rows[0]["UNI_NO"].ToString() : "";
                    UNITITLE = (dtOrg1.Rows[0]["UNI_TITLE"] != DBNull.Value) ? dtOrg1.Rows[0]["UNI_TITLE"].ToString() : ""; 
                }
                if (dtItemd.Rows.Count > 0)
                {
                    //  CREDIT_NOTE_NO = dtItem.Rows
                    CREDIT_NOTE_NO = (dtItemd.Rows[0]["CREDIT_NOTE_NO"] != DBNull.Value) ? dtItemd.Rows[0]["CREDIT_NOTE_NO"].ToString() : "";//店名 = (drOrg["STORENAME"]!=DBNull.Value)?drOrg["STORENAME"].ToString():"";//店名
                }
                Table01D(pdfDoc, sStoreNo, SALE_NO, CREDIT_NOTE_NO, dtItemd, UNITITLE, UNINO);

                //TABLE間的間距
                tc = new PdfPCell(new Paragraph(""));
                tc.FixedHeight = 10f;
                tc.BorderWidth = 0;
                tbBorder.AddCell(tc);

                Table02D(pdfDoc, sStore_No, SALE_NO, CREDIT_NOTE_NO, dtItemd, UNITITLE, UNINO);

                //TABLE間的間距
                tc = new PdfPCell(new Paragraph(""));
                tc.FixedHeight = 10f;
                tc.BorderWidth = 0;
                tbBorder.AddCell(tc);

                Table03D(pdfDoc, sStore_No, SALE_NO, CREDIT_NOTE_NO, dtItemd, UNITITLE, UNINO);

                //TABLE間的間距
                tc = new PdfPCell(new Paragraph(""));
                tc.FixedHeight = 10f;
                tc.BorderWidth = 0;
                tbBorder.AddCell(tc);

                Table04D(pdfDoc, sStore_No, SALE_NO, CREDIT_NOTE_NO, dtItemd, UNITITLE, UNINO);
                pdfDoc.Add(tbBorder);
                pdfDoc.Close();
                //  pdfDoc.Close();
            }
            catch (Exception ex)
            {
                if (pdfDoc.IsOpen()) pdfDoc.Close();
                throw ex;
            }

            return filename;

        }
        /// <summary>
        /// 發票列印
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="printerName"></param>
        public string generateReceiptfortest(string POSUUID_MASTER,string printname)
        {
            filename = "";
            try
            {
                //取得資料
                DataTable dtOrg = getReceiptData(POSUUID_MASTER);
                if (dtOrg.Rows.Count == 0)
                {
                    return "nodata";
                }
                string checkTOTAL_AMOUNT = (dtOrg.Rows[0]["TOTAL_AMOUNT"] != DBNull.Value) ? dtOrg.Rows[0]["TOTAL_AMOUNT"].ToString() : "0";//總額
               
                    //20110127 發票檔名(年度+發票號碼)
                    string sInvoiceNo = "";
                    string sStoreNo = "";
                    string sYYYYMMDD = "";
                    string UNINO = "";
                    if (dtOrg.Rows.Count > 0)
                    {
                        DateTime aDate = Convert.ToDateTime(dtOrg.Rows[0]["INVOICE_DATE"]);
                        sInvoiceNo = aDate.Year.ToString();
                        sInvoiceNo += "_" + dtOrg.Rows[0]["INVOICE_NO"].ToString();
                        sStoreNo = dtOrg.Rows[0]["STORE_NO"].ToString();
                        sYYYYMMDD = aDate.ToString("yyyyMMdd");
                        UNINO = dtOrg.Rows[0]["UNI_NO"].ToString();
                    }


                    //處理細項長度
                    DataTable dtItem = new DataTable();
                    dtItem.Columns.Add("PROD_NAME");
                    dtItem.Columns.Add("QUANTITY");
                    dtItem.Columns.Add("PRICE");
                    dtItem.Columns.Add("AMOUNT");
                    if (UNINO == "")
                    {
                        getNIProperItem(dtOrg, dtItem);
                    }
                    else
                    {
                        getIProperItem(dtOrg, dtItem);
                    }
                    DataSet dsItems = new DataSet();
                    DataTable dtTo = null;
                    //1頁13列
                    if (dtItem.Rows.Count > 0)
                    {
                        int defaultcount = 5;
                        int TablesCount = dtItem.Rows.Count / 5 + ((dtItem.Rows.Count % 5 == 0) ? 0 : 1);
                        for (int i = 0; i < dtItem.Rows.Count; i++)
                        {
                            if (i % defaultcount == 0)
                            {
                                dtTo = dtItem.Clone();
                                dsItems.Tables.Add(dtTo);
                            }
                            DataRow drFrom = dtItem.Rows[i];
                            DataRow drTo = dtTo.NewRow();
                        //    
                            drTo.ItemArray = drFrom.ItemArray;
                            if (drTo["PROD_NAME"].ToString() != "" && drTo["QUANTITY"].ToString() == "" && drTo["PRICE"].ToString() == "" && drTo["AMOUNT"].ToString() == "")
                            {
                                defaultcount = defaultcount + 1;
                            }
                            dtTo.Rows.Add(drTo);
                        }
                    }


                    //處理備註長度
                    DataTable dtRemark = new DataTable();
                    dtRemark.Columns.Add("REMARK");
                    getProperRemark(dtOrg, dtRemark);

                    DataSet dsRemark = new DataSet();
                    dtTo = null;
                    //1頁9列
                    if (dtRemark.Rows.Count > 0)
                    {
                        //1頁9列
                        int TablesCount = dtRemark.Rows.Count / 9 + ((dtRemark.Rows.Count % 9 == 0) ? 0 : 1);
                        for (int i = 0; i < dtRemark.Rows.Count; i++)
                        {
                            if (i % 9 == 0)
                            {
                                dtTo = dtRemark.Clone();
                                dsRemark.Tables.Add(dtTo);
                            }
                            DataRow drFrom = dtRemark.Rows[i];
                            DataRow drTo = dtTo.NewRow();
                            drTo.ItemArray = drFrom.ItemArray;
                            dtTo.Rows.Add(drTo);
                        }

                    }

                    //PDF Start
                    //Paragraph p1;
                    //PdfPCell tc;
                    pdfDoc = new Document(PageSize.A4, 0f, 0f, 24f, 10f);
                    //檔名
                    if (!string.IsNullOrEmpty(sInvoiceNo))
                    {
                        filename = sInvoiceNo + ".pdf";
                    }
                    else
                    {
                        filename = Guid.NewGuid().ToString() + ".pdf";
                    }
                    //檢查路徑
                    if (string.IsNullOrEmpty(sYYYYMMDD)) sYYYYMMDD = DateTime.Now.ToString("yyyyMMdd");
                    string sSubPath = filePath + "/" + DateTime.Now.ToString("yyyyMMdd") + "/" + sStoreNo;
                    checkDirectory(sSubPath);

                    #region 備份CODE
                    //if (!Directory.Exists(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD)))
                    //{
                    //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD));
                    //}
                    //if (!Directory.Exists(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD + "\\" + sStoreNo)))
                    //{
                    //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD + "\\" + sStoreNo));
                    //}
                    #endregion
                    //產生PDF檔案
                    //FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(filePath), filename), FileMode.Create);
                    //FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD + "\\" + sStoreNo), filename), FileMode.Create);
                    FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(sSubPath), filename), FileMode.Create);
                    writer = PdfWriter.GetInstance(pdfDoc, stream);

                    pdfDoc.Open();

                    string printerName = ConfigurationManager.AppSettings["Invoice_PDFPrinterName"].ToString();//web.config中設定
                    // 加入自動列印指令碼
                    AddPrintAction(writer, printname);
                    // writer.AddJavaScript("this.print(false);", true);




                    int iCopies = (dsItems.Tables.Count > dsRemark.Tables.Count) ? dsItems.Tables.Count : dsRemark.Tables.Count;
                    int nowpage = 0;
                    for (int i = 0; i < iCopies; i++)
                    {
                        if (i == 0)
                        {
                            nowpage = 1;
                        }
                        else
                        {
                            nowpage = nowpage + 1;
                        }
                        DataTable dtItem1 = null;
                        DataTable dtRemark1 = null;
                        if (i < dsItems.Tables.Count)
                        {
                            dtItem1 = dsItems.Tables[i];
                        }
                        else
                        {
                            dtItem1 = null;
                        }
                        if (i < iCopies)
                        {
                            dtRemark1 = dsRemark.Tables[0];
                        }
                        else
                        {
                            dtRemark1 = null;
                        }

                        //第一聯
                        //if (UNINO == "")
                        //{
                        //  //  add0NoUNI(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(nowpage), Convert.ToString(iCopies*2));
                        //}
                        //else
                        //{
                        //    add0(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(nowpage), Convert.ToString(iCopies*2));
                        //}
                        ////畫虛線
                        //PdfContentByte cb = writer.DirectContent;
                        //for (int j = 0; j < pdfDoc.Right - 10; j += 4)
                        //{
                        //    cb.MoveTo(pdfDoc.Left + j, 420.945f);
                        //    cb.LineTo(pdfDoc.Left + j + 1, 420.945f);
                        //}
                        //cb.Stroke();


                        ////換頁
                        //pdfDoc.NewPage();
                        //nowpage = nowpage + 1;
                        //第二聯
                        if (UNINO == "")
                        {
                            // add1NoUNI(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(nowpage), Convert.ToString(iCopies * 2));
                            PdfPTable pdfTable = new PdfPTable(1);
                            pdfTable.DefaultCell.Border = 0;
                            pdfTable.SetTotalWidth(new float[] { 532f });
                            pdfTable.LockedWidth = true;
                            pdfTable.SpacingBefore = 0;
                            pdfTable.SpacingAfter = 0;
                            PdfPCell tc = new PdfPCell(new Paragraph(""));
                            Paragraph p1;
                            p1 = new Paragraph("");
                            tc = CF(new PdfPCell(p1), "B", 0);
                            tc.Border = 0;
                            tc.FixedHeight = 420.2f;
                            //tc.FixedHeight = 60f;
                            pdfTable.AddCell(tc);
                            //標題框(tbTitle)，3欄(圖片，標題，留白
                            //     PdfPTable tbTitle = new PdfPTable(new float[] { 1f, 3f, 1f });
                            pdfDoc.Add(pdfTable);
                            PdfContentByte cb = writer.DirectContent;
                            for (int q = 0; q < pdfDoc.Right - 10; q += 4)
                            {
                                //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfTable.TotalHeight - 55);
                                //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfTable.TotalHeight - 55);
                                //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfDoc.Top / 2);
                                //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfDoc.Top / 2);
                                //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfTable.TotalHeight + 22.84f);
                                //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfTable.TotalHeight + 22.84f);
                                //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfTable.TotalHeight -19.84f);
                                //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfTable.TotalHeight - 19.84f);
                                cb.MoveTo(pdfDoc.Left + q, 420.945f);
                                cb.LineTo(pdfDoc.Left + q + 1, 420.945f);
                            }
                        }
                        else
                        {
                            add1(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(nowpage), Convert.ToString(iCopies ));
                        }
                        //產生第三聯
                        //add2(pdfDoc);
                        if (UNINO == "")
                        {
                            add3NoUNI(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(nowpage), Convert.ToString(iCopies));
                        }
                        else
                        {
                            add3(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(nowpage), Convert.ToString(iCopies));
                        }
                        //產生註記欄
                        if (UNINO == "")
                        {
                            addMarkTable2(pdfDoc);
                        }
                        else
                        {
                            addMarkTable1(pdfDoc);
                            addMarkTable2(pdfDoc);
                        }
                        if (i != iCopies - 1)
                        {
                            //換頁
                            pdfDoc.NewPage();
                        }
                    }

                    pdfDoc.Close();
               
            }
            catch (Exception ex)
            {
                if (pdfDoc.IsOpen()) pdfDoc.Close();
                throw ex;
            }
           
            return filename;
        
        }

        public string generateReceiptforAccount(string POSUUID_MASTER, string printname)
        {
            filename = "";
            try
            {
                //取得資料
                DataTable dtOrg = getReceiptData(POSUUID_MASTER);
                string checkTOTAL_AMOUNT = (dtOrg.Rows[0]["TOTAL_AMOUNT"] != DBNull.Value) ? dtOrg.Rows[0]["TOTAL_AMOUNT"].ToString() : "0";//總額

                //20110127 發票檔名(年度+發票號碼)
                string sInvoiceNo = "";
                string sStoreNo = "";
                string sYYYYMMDD = "";
                string UNINO = "";
                if (dtOrg.Rows.Count > 0)
                {
                    DateTime aDate = Convert.ToDateTime(dtOrg.Rows[0]["INVOICE_DATE"]);
                    sInvoiceNo = aDate.Year.ToString();
                    sInvoiceNo += "_" + dtOrg.Rows[0]["INVOICE_NO"].ToString();
                    sStoreNo = dtOrg.Rows[0]["STORE_NO"].ToString();
                    sYYYYMMDD = aDate.ToString("yyyyMMdd");
                    UNINO = dtOrg.Rows[0]["UNI_NO"].ToString();
                }


                //處理細項長度
                DataTable dtItem = new DataTable();
                dtItem.Columns.Add("PROD_NAME");
                dtItem.Columns.Add("QUANTITY");
                dtItem.Columns.Add("PRICE");
                dtItem.Columns.Add("AMOUNT");
                if (UNINO == "")
                {
                    getNIProperItem(dtOrg, dtItem);
                }
                else
                {
                    getIProperItem(dtOrg, dtItem);
                }
                DataSet dsItems = new DataSet();
                DataTable dtTo = null;
                //1頁13列
                if (dtItem.Rows.Count > 0)
                {
                    int defaultcount = 5;
                    int TablesCount = dtItem.Rows.Count / 5 + ((dtItem.Rows.Count % 5 == 0) ? 0 : 1);
                    for (int i = 0; i < dtItem.Rows.Count; i++)
                    {
                        if (i % defaultcount == 0)
                        {
                            dtTo = dtItem.Clone();
                            dsItems.Tables.Add(dtTo);
                        }
                        DataRow drFrom = dtItem.Rows[i];
                        DataRow drTo = dtTo.NewRow();
                        drTo.ItemArray = drFrom.ItemArray;
                        if (drTo["PROD_NAME"].ToString() != "" && drTo["QUANTITY"].ToString() == "" && drTo["PRICE"].ToString() == "" && drTo["AMOUNT"].ToString() == "")
                        {
                            defaultcount = defaultcount + 1;
                        }
                        dtTo.Rows.Add(drTo);
                    }
                }


                //處理備註長度
                DataTable dtRemark = new DataTable();
                dtRemark.Columns.Add("REMARK");
                getProperRemark(dtOrg, dtRemark);

                DataSet dsRemark = new DataSet();
                dtTo = null;
                //1頁9列
                if (dtRemark.Rows.Count > 0)
                {
                    //1頁9列
                    int TablesCount = dtRemark.Rows.Count / 9 + ((dtRemark.Rows.Count % 9 == 0) ? 0 : 1);
                    for (int i = 0; i < dtRemark.Rows.Count; i++)
                    {
                        if (i % 9 == 0)
                        {
                            dtTo = dtRemark.Clone();
                            dsRemark.Tables.Add(dtTo);
                        }
                        DataRow drFrom = dtRemark.Rows[i];
                        DataRow drTo = dtTo.NewRow();
                        drTo.ItemArray = drFrom.ItemArray;
                        dtTo.Rows.Add(drTo);
                    }

                }

                //PDF Start
                //Paragraph p1;
                //PdfPCell tc;
                pdfDoc = new Document(PageSize.A4, 0f, 0f, 24f, 10f);
                //檔名
                if (!string.IsNullOrEmpty(sInvoiceNo))
                {
                    filename = sInvoiceNo + "_Account.pdf";
                }
                else
                {
                    filename = Guid.NewGuid().ToString() + "_Account.pdf";
                }
                //檢查路徑
                if (string.IsNullOrEmpty(sYYYYMMDD)) sYYYYMMDD = DateTime.Now.ToString("yyyyMMdd");
                string sSubPath = filePath + "/" + DateTime.Now.ToString("yyyyMMdd") + "/" + sStoreNo;
                checkDirectory(sSubPath);

                #region 備份CODE
                //if (!Directory.Exists(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD)))
                //{
                //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD));
                //}
                //if (!Directory.Exists(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD + "\\" + sStoreNo)))
                //{
                //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD + "\\" + sStoreNo));
                //}
                #endregion
                //產生PDF檔案
                //FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(filePath), filename), FileMode.Create);
                //FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD + "\\" + sStoreNo), filename), FileMode.Create);
                FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(sSubPath), filename), FileMode.Create);
                writer = PdfWriter.GetInstance(pdfDoc, stream);

                pdfDoc.Open();

                string printerName = ConfigurationManager.AppSettings["Invoice_PDFPrinterName"].ToString();//web.config中設定
                // 加入自動列印指令碼
                // AddPrintAction(writer, printname);
                // writer.AddJavaScript("this.print(false);", true);




                int iCopies = (dsItems.Tables.Count > dsRemark.Tables.Count) ? dsItems.Tables.Count : dsRemark.Tables.Count;
                int nowpage = 0; 
                for (int i = 0; i < iCopies; i++)
                {
                    if (i == 0)
                    {
                        nowpage = 1;
                    }
                    else
                    {
                        nowpage = nowpage + 1;
                    }
                    DataTable dtItem1 = null;
                    DataTable dtRemark1 = null;
                    if (i < dsItems.Tables.Count)
                    {
                        dtItem1 = dsItems.Tables[i];
                    }
                    else
                    {
                        dtItem1 = null;
                    }
                    if (i < iCopies)
                    {
                        dtRemark1 = dsRemark.Tables[0];
                    }
                    else
                    {
                        dtRemark1 = null;
                    }

                  //  第一聯
                    if (UNINO == "")
                    {
                        add0NoUNI(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(nowpage), Convert.ToString(iCopies));
                    }
                    else
                    {
                        add0(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(nowpage), Convert.ToString(iCopies));
                    }
                    //畫虛線
                    PdfContentByte cb = writer.DirectContent;
                    for (int j = 0; j < pdfDoc.Right - 10; j += 4)
                    {
                        cb.MoveTo(pdfDoc.Left + j, 420.945f);
                        cb.LineTo(pdfDoc.Left + j + 1, 420.945f);
                    }
                    cb.Stroke();


                    ////換頁
                    //pdfDoc.NewPage();
                    //nowpage = nowpage + 1;
                    //第二聯
                    //if (UNINO == "")
                    //{
                    //    // add1NoUNI(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(nowpage), Convert.ToString(iCopies * 2));
                    //    PdfPTable pdfTable = new PdfPTable(1);
                    //    pdfTable.DefaultCell.Border = 0;
                    //    pdfTable.SetTotalWidth(new float[] { 532f });
                    //    pdfTable.LockedWidth = true;
                    //    pdfTable.SpacingBefore = 0;
                    //    pdfTable.SpacingAfter = 0;
                    //    PdfPCell tc = new PdfPCell(new Paragraph(""));
                    //    Paragraph p1;
                    //    p1 = new Paragraph("");
                    //    tc = CF(new PdfPCell(p1), "B", 0);
                    //    tc.Border = 0;
                    //    tc.FixedHeight = 420.2f;
                    //    //tc.FixedHeight = 60f;
                    //    pdfTable.AddCell(tc);
                    //    //標題框(tbTitle)，3欄(圖片，標題，留白
                    //    //     PdfPTable tbTitle = new PdfPTable(new float[] { 1f, 3f, 1f });
                    //    pdfDoc.Add(pdfTable);
                    //    PdfContentByte cb = writer.DirectContent;
                    //    for (int q = 0; q < pdfDoc.Right - 10; q += 4)
                    //    {
                    //        //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfTable.TotalHeight - 55);
                    //        //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfTable.TotalHeight - 55);
                    //        //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfDoc.Top / 2);
                    //        //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfDoc.Top / 2);
                    //        //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfTable.TotalHeight + 22.84f);
                    //        //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfTable.TotalHeight + 22.84f);
                    //        //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfTable.TotalHeight -19.84f);
                    //        //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfTable.TotalHeight - 19.84f);
                    //        cb.MoveTo(pdfDoc.Left + q, 420.945f);
                    //        cb.LineTo(pdfDoc.Left + q + 1, 420.945f);
                    //    }
                    //}
                    //else
                    //{
                    //    add1(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(nowpage), Convert.ToString(iCopies));
                    //}
                    //產生第三聯
                    //add2(pdfDoc);
                    //if (UNINO == "")
                    //{
                    //    add3NoUNI(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(nowpage), Convert.ToString(iCopies));
                    //}
                    //else
                    //{
                    //    add3(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(nowpage), Convert.ToString(iCopies));
                    //}
                    //產生註記欄
                    if (UNINO == "")
                    {
                        addMarkTable1(pdfDoc);
                    }
                    else
                    {
                        addMarkTable1(pdfDoc);
                        //addMarkTable2(pdfDoc);
                    }
                    if (i != iCopies - 1)
                    {
                        //換頁
                        pdfDoc.NewPage();
                    }
                }

                pdfDoc.Close();

            }
            catch (Exception ex)
            {
                if (pdfDoc.IsOpen()) pdfDoc.Close();
                throw ex;
            }

            return filename;

        }
        public string generateReceipt(string POSUUID_MASTER, string repeatprint)
        {
            filename = "";
            try
            {
                //取得資料
                DataTable dtOrg = getReceiptData(POSUUID_MASTER);

                //20110127 發票檔名(年度+發票號碼)
                string sInvoiceNo = "";
                string sStoreNo = "";
                string sYYYYMMDD = "";
                string UNINO = "";
                if (dtOrg.Rows.Count > 0)
                {
                    DateTime aDate = Convert.ToDateTime(dtOrg.Rows[0]["INVOICE_DATE"]);
                    sInvoiceNo = aDate.Year.ToString();
                    sInvoiceNo += "_" + dtOrg.Rows[0]["INVOICE_NO"].ToString();
                    sStoreNo = dtOrg.Rows[0]["STORE_NO"].ToString();
                    sYYYYMMDD = aDate.ToString("yyyyMMdd");
                    UNINO = dtOrg.Rows[0]["UNI_NO"].ToString();
                }

                //處理細項長度
                DataTable dtItem = new DataTable();
                dtItem.Columns.Add("PROD_NAME");
                dtItem.Columns.Add("QUANTITY");
                dtItem.Columns.Add("PRICE");
                dtItem.Columns.Add("AMOUNT");
                getProperItem(dtOrg, dtItem);

                DataSet dsItems = new DataSet();
                DataTable dtTo = null;
                //1頁13列
                if (dtItem.Rows.Count > 0)
                {
                    int TablesCount = dtItem.Rows.Count / 13 + ((dtItem.Rows.Count % 13 == 0) ? 0 : 1);
                    for (int i = 0; i < dtItem.Rows.Count; i++)
                    {
                        if (i % 13 == 0)
                        {
                            dtTo = dtItem.Clone();
                            dsItems.Tables.Add(dtTo);
                        }
                        DataRow drFrom = dtItem.Rows[i];
                        DataRow drTo = dtTo.NewRow();
                        drTo.ItemArray = drFrom.ItemArray;
                        dtTo.Rows.Add(drTo);
                    }
                }


                //處理備註長度
                DataTable dtRemark = new DataTable();
                dtRemark.Columns.Add("REMARK");
                getProperRemark(dtOrg, dtRemark);

                DataSet dsRemark = new DataSet();
                dtTo = null;
                //1頁9列
                if (dtRemark.Rows.Count > 0)
                {
                    //1頁9列
                    int TablesCount = dtRemark.Rows.Count / 9 + ((dtRemark.Rows.Count % 9 == 0) ? 0 : 1);
                    for (int i = 0; i < dtRemark.Rows.Count; i++)
                    {
                        if (i % 9 == 0)
                        {
                            dtTo = dtRemark.Clone();
                            dsRemark.Tables.Add(dtTo);
                        }
                        DataRow drFrom = dtRemark.Rows[i];
                        DataRow drTo = dtTo.NewRow();
                        drTo.ItemArray = drFrom.ItemArray;
                        dtTo.Rows.Add(drTo);
                    }

                }

                //PDF Start
                //Paragraph p1;
                //PdfPCell tc;
                pdfDoc = new Document(PageSize.A4, 0f, 0f, 24f, 10f);
                //檔名
                if (!string.IsNullOrEmpty(sInvoiceNo))
                {
                    filename = sInvoiceNo + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                }
                else
                {
                    filename = Guid.NewGuid().ToString() + ".pdf";
                }
                //檢查路徑
                if (string.IsNullOrEmpty(sYYYYMMDD)) sYYYYMMDD = DateTime.Now.ToString("yyyyMMdd");
                string sSubPath = filePath + "/" + DateTime.Now.ToString("yyyyMMdd") + "/" + sStoreNo;
                checkDirectory(sSubPath);

                #region 備份CODE
                //if (!Directory.Exists(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD)))
                //{
                //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD));
                //}
                //if (!Directory.Exists(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD + "\\" + sStoreNo)))
                //{
                //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD + "\\" + sStoreNo));
                //}
                #endregion
                //產生PDF檔案
                //FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(filePath), filename), FileMode.Create);
                //FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD + "\\" + sStoreNo), filename), FileMode.Create);
                FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(sSubPath), filename), FileMode.Create);
                writer = PdfWriter.GetInstance(pdfDoc, stream);

                pdfDoc.Open();

                string printerName = ConfigurationManager.AppSettings["Invoice_PDFPrinterName"].ToString();//web.config中設定
                // 加入自動列印指令碼
                AddPrintAction(writer, printerName);
                //writer.AddJavaScript("this.print(false);", true);



                int iCopies = (dsItems.Tables.Count > dsRemark.Tables.Count) ? dsItems.Tables.Count : dsRemark.Tables.Count;

                for (int i = 0; i < iCopies; i++)
                {
                    DataTable dtItem1 = null;
                    DataTable dtRemark1 = null;
                    if (i < dsItems.Tables.Count)
                    {
                        dtItem1 = dsItems.Tables[i];
                    }
                    else
                    {
                        dtItem1 = null;
                    }
                    if (i < dsRemark.Tables.Count)
                    {
                        dtRemark1 = dsRemark.Tables[i];
                    }
                    else
                    {
                        dtRemark1 = null;
                    }

                    //第一聯
                    add0(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(i), Convert.ToString(iCopies));
                    addMarkTable1(pdfDoc, "1");
                    //畫虛線
                    PdfContentByte cb = writer.DirectContent;
                    for (int j = 0; j < pdfDoc.Right - 10; j += 4)
                    {
                        cb.MoveTo(pdfDoc.Left + j, 420.945f);
                        cb.LineTo(pdfDoc.Left + j + 1, 420.945f);
                    }
                    cb.Stroke();


                    //換頁
                    pdfDoc.NewPage();

                    //第二聯
                    if (UNINO == "")
                    {
                        add1NoUNI(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(i), Convert.ToString(iCopies));
                    }
                    else
                    {
                        add1(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(i), Convert.ToString(iCopies));
                    }
                    //addMarkTable3(pdfDoc, "3");
                    //產生第三聯
                    //add2(pdfDoc);
                    if (UNINO == "")
                    {
                        add3NoUNI(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(i), Convert.ToString(iCopies * 2));
                    }
                    else
                    {
                        add3(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(i), Convert.ToString(iCopies * 2));
                    }

                    //產生註記欄
                    addMarkTable1(pdfDoc);
                    addMarkTable2(pdfDoc);

                    addMarkTable2(pdfDoc, "2");

                    if (i != iCopies - 1)
                    {
                        //換頁
                        pdfDoc.NewPage();
                    }
                }

                pdfDoc.Close();
            }
            catch (Exception ex)
            {
                if (pdfDoc.IsOpen()) pdfDoc.Close();
                throw ex;
            }

            return filename;

        }
        /// <summary>
        /// 發票重印
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="printerName"></param>
        public string generateReceiptretest(string POSUUID_MASTER, string repeatprint,string printname)
        {
            filename = "";
            try
            {
                //取得資料
                DataTable dtOrg = getReceiptData(POSUUID_MASTER);

                //20110127 發票檔名(年度+發票號碼)
                string sInvoiceNo = "";
                string sStoreNo = "";
                string sYYYYMMDD = "";
                string UNINO = "";
                if (dtOrg.Rows.Count > 0)
                {
                    DateTime aDate = Convert.ToDateTime(dtOrg.Rows[0]["INVOICE_DATE"]);
                    sInvoiceNo = aDate.Year.ToString();
                    sInvoiceNo += "_" + dtOrg.Rows[0]["INVOICE_NO"].ToString();
                    sStoreNo = dtOrg.Rows[0]["STORE_NO"].ToString();
                    sYYYYMMDD = aDate.ToString("yyyyMMdd");
                    UNINO = dtOrg.Rows[0]["UNI_NO"].ToString();
                }

                //處理細項長度
                DataTable dtItem = new DataTable();
                dtItem.Columns.Add("PROD_NAME");
                dtItem.Columns.Add("QUANTITY");
                dtItem.Columns.Add("PRICE");
                dtItem.Columns.Add("AMOUNT");
                if (UNINO == "")
                { 
                    getNIProperItem(dtOrg, dtItem); 
                }
                else
                {
                    getIProperItem(dtOrg, dtItem);  
                }
              

                DataSet dsItems = new DataSet();
                DataTable dtTo = null;
                //1頁13列
                if (dtItem.Rows.Count > 0)
                {
                    int defaultcount = 5;
                    int TablesCount = dtItem.Rows.Count / 5 + ((dtItem.Rows.Count % 5 == 0) ? 0 : 1);
                    for (int i = 0; i < dtItem.Rows.Count; i++)
                    {
                        if (i % defaultcount == 0)
                        {
                            dtTo = dtItem.Clone();
                            dsItems.Tables.Add(dtTo);
                        }
                        DataRow drFrom = dtItem.Rows[i];
                        DataRow drTo = dtTo.NewRow();
                        drTo.ItemArray = drFrom.ItemArray;
                        if (drTo["PROD_NAME"].ToString() != "" && drTo["QUANTITY"].ToString() == "" && drTo["PRICE"].ToString() == "" && drTo["AMOUNT"].ToString() == "")
                        {
                            defaultcount = defaultcount + 1;
                        }
                        dtTo.Rows.Add(drTo);
                    }
                }


                //處理備註長度
                DataTable dtRemark = new DataTable();
                dtRemark.Columns.Add("REMARK");
                getProperRemark(dtOrg, dtRemark);

                DataSet dsRemark = new DataSet();
                dtTo = null;
                //1頁9列
                if (dtRemark.Rows.Count > 0)
                {
                    //1頁9列
                    int TablesCount = dtRemark.Rows.Count / 9 + ((dtRemark.Rows.Count % 9 == 0) ? 0 : 1);
                    for (int i = 0; i < dtRemark.Rows.Count; i++)
                    {
                        if (i % 9 == 0)
                        {
                            dtTo = dtRemark.Clone();
                            dsRemark.Tables.Add(dtTo);
                        }
                        DataRow drFrom = dtRemark.Rows[i];
                        DataRow drTo = dtTo.NewRow();
                        drTo.ItemArray = drFrom.ItemArray;
                        dtTo.Rows.Add(drTo);
                    }

                }

                //PDF Start
                //Paragraph p1;
                //PdfPCell tc;
                pdfDoc = new Document(PageSize.A4, 0f, 0f, 24f, 10f);
                //檔名
                if (!string.IsNullOrEmpty(sInvoiceNo))
                {
                    filename = sInvoiceNo + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                }
                else
                {
                    filename = Guid.NewGuid().ToString() + ".pdf";
                }
                //檢查路徑
                if (string.IsNullOrEmpty(sYYYYMMDD)) sYYYYMMDD = DateTime.Now.ToString("yyyyMMdd");
                string sSubPath = filePath + "/" + DateTime.Now.ToString("yyyyMMdd") + "/" + sStoreNo;
                checkDirectory(sSubPath);

                #region 備份CODE
                //if (!Directory.Exists(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD)))
                //{
                //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD));
                //}
                //if (!Directory.Exists(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD + "\\" + sStoreNo)))
                //{
                //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD + "\\" + sStoreNo));
                //}
                #endregion
                //產生PDF檔案
                //FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(filePath), filename), FileMode.Create);
                //FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD + "\\" + sStoreNo), filename), FileMode.Create);
                FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(sSubPath), filename), FileMode.Create);
                writer = PdfWriter.GetInstance(pdfDoc, stream);

                pdfDoc.Open();

                string printerName = ConfigurationManager.AppSettings["Invoice_PDFPrinterName"].ToString();//web.config中設定
                // 加入自動列印指令碼
                AddPrintAction(writer, printerName);
                //writer.AddJavaScript("this.print(false);", true);



                int iCopies = (dsItems.Tables.Count > dsRemark.Tables.Count) ? dsItems.Tables.Count : dsRemark.Tables.Count;
                int nowpage = 0;
                for (int i = 0; i < iCopies; i++)
                {
                    DataTable dtItem1 = null;
                    DataTable dtRemark1 = null;
                   
                    if (i == 0)
                    {
                        nowpage = 1;
                    }
                    else
                    {
                        nowpage = nowpage + 1;
                    }
                    if (i < dsItems.Tables.Count)
                    {
                        dtItem1 = dsItems.Tables[i];
                    }
                    else
                    {
                        dtItem1 = null;
                    }
                    if (i < iCopies)
                    {
                        dtRemark1 = dsRemark.Tables[0];
                    }
                    else
                    {
                        dtRemark1 = null;
                    }
                    
                    //第一聯
                    //if (UNINO == "")
                    //{
                    //    add0NoUNI(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(nowpage), Convert.ToString(iCopies *2));
                    //}
                    //else
                    //{
                    //    add0(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(nowpage), Convert.ToString(iCopies*2));
                    //}
                    //if (UNINO == "")
                    //{

                    //}
                    //else
                    //{
                    //    addMarkTable1(pdfDoc);
                    //}
                    //addMarkTable1(pdfDoc, "1");
                    ////畫虛線
                    //PdfContentByte cb = writer.DirectContent;
                    //for (int j = 0; j < pdfDoc.Right - 10; j += 4)
                    //{
                    //    cb.MoveTo(pdfDoc.Left + j, 420.945f);
                    //    cb.LineTo(pdfDoc.Left + j + 1, 420.945f);
                    //}
                    //cb.Stroke();


                    ////換頁
                    //pdfDoc.NewPage();
                    //nowpage = nowpage + 1;
                    //第二聯
                    if (UNINO == "")
                    {
                       // add1NoUNI(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(nowpage), Convert.ToString(iCopies * 2));
                        PdfPTable pdfTable = new PdfPTable(1);
                        pdfTable.DefaultCell.Border = 0;
                        pdfTable.SetTotalWidth(new float[] { 532f });
                        pdfTable.LockedWidth = true;
                        pdfTable.SpacingBefore = 0;
                        pdfTable.SpacingAfter = 0;
                        PdfPCell tc = new PdfPCell(new Paragraph(""));
                        Paragraph p1;
                        p1 = new Paragraph("");
                        tc = CF(new PdfPCell(p1), "B", 0);
                        tc.Border = 0;
                        tc.FixedHeight = 420.2f;
                        //tc.FixedHeight = 60f;
                        pdfTable.AddCell(tc);
                        //標題框(tbTitle)，3欄(圖片，標題，留白
                   //     PdfPTable tbTitle = new PdfPTable(new float[] { 1f, 3f, 1f });
                        pdfDoc.Add(pdfTable);
                        PdfContentByte cb = writer.DirectContent;
                        for (int q = 0; q < pdfDoc.Right - 10; q += 4)
                        {
                            //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfTable.TotalHeight - 55);
                            //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfTable.TotalHeight - 55);
                            //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfDoc.Top / 2);
                            //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfDoc.Top / 2);
                            //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfTable.TotalHeight + 22.84f);
                            //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfTable.TotalHeight + 22.84f);
                            //cb.MoveTo(pdfDoc.Left + i, pdfDoc.Top - pdfTable.TotalHeight -19.84f);
                            //cb.LineTo(pdfDoc.Left + i + 1, pdfDoc.Top - pdfTable.TotalHeight - 19.84f);
                            cb.MoveTo(pdfDoc.Left + q, 420.945f);
                            cb.LineTo(pdfDoc.Left + q + 1, 420.945f);
                        }
                    }
                    else
                    {
                        add1(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(nowpage), Convert.ToString(iCopies));
                    }
                    //addMarkTable3(pdfDoc, "3");
                    //產生第三聯
                    //add2(pdfDoc);
                    if (UNINO == "")
                    {
                        add3NoUNI(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(nowpage), Convert.ToString(iCopies));
                    }
                    else
                    {
                        add3(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(nowpage), Convert.ToString(iCopies));
                    }
                    //產生註記欄
                    if (UNINO == "")
                    {
                        addMarkTable4(pdfDoc, "2");
                        addMarkTable2(pdfDoc);
                    }
                    else
                    {
                        addMarkTable1(pdfDoc);
                        addMarkTable2(pdfDoc);
                        addMarkTable2(pdfDoc, "2");
                    }
                  

                    if (i != iCopies - 1)
                    {
                        //換頁
                        pdfDoc.NewPage();
                    }
                }

                pdfDoc.Close();
            }
            catch (Exception ex)
            {
                if (pdfDoc.IsOpen()) pdfDoc.Close();
                throw ex;
            }

            return filename;

        }


        private DataTable getReceiptData(string POSUUID_MASTER)
        {
            DataTable dt = new DataTable();
            using (OracleConnection con = OracleDBUtil.GetConnection())
            {
                try
                {
                    string sql = "SELECT S.STORENAME AS STORENAME " + //店名
                               ",IH.INVOICE_DATE AS INVOICE_DATE " + //發票日
                               ",IH.INVOICE_NO AS INVOICE_NO " +  //發票號碼
                               ",IH.BUYER AS BUYER " +  //買受人
                               ",IH.UNI_NO AS UNI_NO " +  //統一編號
                               ",IH.REMARK AS H_REMARK " +  //備註_HEAD 
                               ",IH.ADDRESS AS ADDRESS " +  //地址
                               ",IH.TAX_TYPE AS TAX_TYPE " +  //營業稅種類
                               ",IH.TOTAL_AMOUNT AS TOTAL_AMOUNT " + //總額
                               ",IH.TAX AS TAX " +  //營業稅
                               ",IH.SALE_AMOUNT AS SALE_AMOUNT " + //銷售合計
                               ",II.PROD_NAME AS PROD_NAME " + //品名
                               ",II.QUANTITY AS QUANTITY " + //數量
                               ",II.PRICE AS PRICE " + //單價
                               ",II.TOTAL_AMOUNT AS AMOUNT " +  //金額
                               ",II.REMARK I_REMARK " + //備註_ITEM
                               ",IH.STORE_NO STORE_NO " + //店點代碼
                               ",SALE_HEAD.UNI_TITLE " +
                               ",SALE_HEAD.SALE_TAX " +
                               ",SALE_HEAD.SALE_TOTAL_AMOUNT " +
                               ",IH.BEFORE_TAX " +
                               ",II.AMOUNT as IAMOUNT " +
                               ",S.POS_RECEIPT_TITLE AS POS_RECEIPT_TITLE " + //發票店名
                               ",length(II.PRODNO) as PRODNOLEN " +
                               "FROM INVOICE_HEAD IH,INVOICE_ITEM  II,STORE S ,SALE_HEAD  " +
                               "WHERE IH.ID =II.INVOICE_HEAD_ID " +
                               "  AND IH.POSUUID_MASTER = SALE_HEAD.POSUUID_MASTER(+) " +
                               "  AND IH.STORE_NO=S.STORE_NO " +
                               "  AND IH.POSUUID_MASTER=" + OracleDBUtil.SqlStr(POSUUID_MASTER) +
                               " Union SELECT S.STORENAME AS STORENAME " + //店名
                               ",IH.INVOICE_DATE AS INVOICE_DATE " + //發票日
                               ",IH.INVOICE_NO AS INVOICE_NO " +  //發票號碼
                               ",IH.BUYER AS BUYER " +  //買受人
                               ",IH.UNI_NO AS UNI_NO " +  //統一編號
                               ",IH.REMARK AS H_REMARK " +  //備註_HEAD 
                               ",IH.ADDRESS AS ADDRESS " +  //地址
                               ",IH.TAX_TYPE AS TAX_TYPE " +  //營業稅種類
                               ",IH.TOTAL_AMOUNT AS TOTAL_AMOUNT " + //總額
                               ",IH.TAX AS TAX " +  //營業稅
                               ",IH.SALE_AMOUNT AS SALE_AMOUNT " + //銷售合計
                               ",II.PROD_NAME AS PROD_NAME " + //品名
                               ",II.QUANTITY AS QUANTITY " + //數量
                               ",II.PRICE AS PRICE " + //單價
                               ",II.TOTAL_AMOUNT AS AMOUNT " +  //金額
                               ",II.REMARK I_REMARK " + //備註_ITEM
                               ",IH.STORE_NO STORE_NO " + //店點代碼
                               ",SALE_HEAD.UNI_TITLE " +
                               ",SALE_HEAD.SALE_TAX " +
                               ",SALE_HEAD.SALE_TOTAL_AMOUNT " +
                               ",null as BEFORE_TAX " +
                               ",II.AMOUNT as IAMOUNT " +
                               ",S.POS_RECEIPT_TITLE AS POS_RECEIPT_TITLE " + //發票店名
                               ",length(II.PRODNO) as PRODNOLEN " +
                               "FROM MANUAL_INVOICE_HEAD IH, MANUAL_INVOICE_ITEM  II,STORE S ,SALE_HEAD  " +
                               "WHERE IH.ID =II.MANUAL_INVOICE_HEAD_ID " +
                               "  AND IH.POSUUID_MASTER = SALE_HEAD.POSUUID_MASTER(+) " +
                               "  AND IH.STORE_NO=S.STORE_NO " +
                               "  AND IH.POSUUID_MASTER=" + OracleDBUtil.SqlStr(POSUUID_MASTER) +
                               "   order by PRODNOLEN desc ";
                                


                    using (OracleCommand cmd = new OracleCommand(sql, con))
                    {
                        using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (con.State == ConnectionState.Open) con.Close();
                    con.Dispose();
                }
                if (con.State == ConnectionState.Open) con.Close();
            }


            return dt;
        }

        private DataTable getPROMOData(string POSUUID_MASTER)
        { 
            DataTable dt = new DataTable();
            using (OracleConnection con = OracleDBUtil.GetConnection())
            {
                try
                {
                    string sql = "SELECT S.STORENAME AS STORENAME "+
                               ",SH.TRADE_DATE AS TRADE_DATE "+
                               ",SH.STORE_NO AS STORE_NO "+
                               ",SH.SALE_NO AS SALE_NO  "+
                               ",SD.PRODNO AS PRODNO  "+
                               ",PP.PRODNAME "+
                               ",SD.MSISDN AS MSISDN "+
                               ",SD.QUANTITY as QUANTITY "+
                               ",SH.SALE_TOTAL_AMOUNT "+
                               ",MM.PROMO_NAME "+
                               "FROM SALE_HEAD SH,STORE S ,SALE_DETAIL SD,MM,PRODUCT PP "+
                               "WHERE SH.POSUUID_MASTER =SD.POSUUID_MASTER "+
                               "  AND SH.STORE_NO=S.STORE_NO "+
                               "  AND SD.PRODNO = PP.PRODNO "+
                               "  AND SD.PROMOTION_CODE = MM.PROMO_NO(+) "+
                               "  AND SH.POSUUID_MASTER=" + OracleDBUtil.SqlStr(POSUUID_MASTER);

                    using (OracleCommand cmd = new OracleCommand(sql, con))
                    {
                        using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (con.State == ConnectionState.Open) con.Close();
                    con.Dispose();
                }
                if (con.State == ConnectionState.Open) con.Close();
            }


            return dt;
        }

        private DataTable getDebitReceiptData(string POSUUID_MASTER)
        {
            DataTable dt = new DataTable();
            using (OracleConnection con = OracleDBUtil.GetConnection())
            {
                try
                {
                    string sql = "SELECT S.STORENAME AS STORENAME " + //店名
                               ",to_char(IH.INVOICE_DATE,'YYYY/MM/DD') AS INVOICE_DATE " + //發票日
                               ",IH.INVOICE_NO AS INVOICE_NO " +  //發票號碼
                               ",IH.BUYER AS BUYER " +  //買受人
                               ",IH.UNI_NO AS UNI_NO " +  //統一編號
                               ",IH.REMARK AS H_REMARK " +  //備註_HEAD 
                               ",IH.ADDRESS AS ADDRESS " +  //地址
                               ",IH.TAX_TYPE AS TAX_TYPE " +  //營業稅種類
                               ",IH.TOTAL_AMOUNT AS TOTAL_AMOUNT " + //總額
                               ",IH.TAX AS TAX " +  //營業稅
                               ",IH.SALE_AMOUNT AS SALE_AMOUNT " + //銷售合計
                               ",II.PROD_NAME AS PROD_NAME " + //品名
                               ",II.QUANTITY AS QUANTITY " + //數量
                               ",II.PRICE AS PRICE " + //單價
                               ",II.TOTAL_AMOUNT AS AMOUNT " +  //金額
                               ",II.REMARK I_REMARK " + //備註_ITEM
                               ",II.TAX AS ITEMTAX " + //稅
                               ",IH.STORE_NO STORE_NO " + //店點代碼
                               ",IH.CREDIT_NOTE_NO " +
                               ",P.TAXABLE " +
                               ",P.TAXRATE " +
                               ",IH.INVALID_DATE " +
                               ",SALE_HEAD.UNI_TITLE " +
                               ",II.AMOUNT as IAMOUNT " +
                               ",S.UNIADDR " +
                               ",IH.INVALID_DATE " +
                               ",S.UNINO as SUNINO " +
                                ",S.POS_RECEIPT_TITLE AS POS_RECEIPT_TITLE " + //發票店名
                               ",length(II.PRODNO) as PRODNOLEN " +
                               "FROM INVOICE_HEAD IH,INVOICE_ITEM  II,STORE S ,PRODUCT P,SALE_HEAD " +
                               "WHERE IH.ID =II.INVOICE_HEAD_ID " +
                               "  AND IH.POSUUID_MASTER = SALE_HEAD.POSUUID_MASTER " +
                               "  AND IH.STORE_NO=S.STORE_NO " +
                               "  AND II.PRODNO = P.PRODNO " +
                               "  AND IH.POSUUID_MASTER=" + OracleDBUtil.SqlStr(POSUUID_MASTER) +
                               " Union SELECT S.STORENAME AS STORENAME " + //店名
                               ",to_char(IH.INVOICE_DATE,'YYYY/MM/DD') AS INVOICE_DATE " + //發票日
                               ",IH.INVOICE_NO AS INVOICE_NO " +  //發票號碼
                               ",IH.BUYER AS BUYER " +  //買受人
                               ",IH.UNI_NO AS UNI_NO " +  //統一編號
                               ",IH.REMARK AS H_REMARK " +  //備註_HEAD 
                               ",IH.ADDRESS AS ADDRESS " +  //地址
                               ",IH.TAX_TYPE AS TAX_TYPE " +  //營業稅種類
                               ",IH.TOTAL_AMOUNT AS TOTAL_AMOUNT " + //總額
                               ",IH.TAX AS TAX " +  //營業稅
                               ",IH.SALE_AMOUNT AS SALE_AMOUNT " + //銷售合計
                               ",II.PROD_NAME AS PROD_NAME " + //品名
                               ",II.QUANTITY AS QUANTITY " + //數量
                               ",II.PRICE AS PRICE " + //單價
                               ",II.TOTAL_AMOUNT AS AMOUNT " +  //金額
                               ",II.REMARK I_REMARK " + //備註_ITEM
                               ",II.TAX AS ITEMTAX " + //稅
                               ",IH.STORE_NO STORE_NO " + //店點代碼
                               ",IH.CREDIT_NOTE_NO " +
                               ",P.TAXABLE  " +
                               ",P.TAXRATE " +
                               ",IH.INVALID_DATE " +
                               ",SALE_HEAD.UNI_TITLE " +
                               ",II.AMOUNT as IAMOUNT " +
                               ",S.UNIADDR " +
                               ",IH.INVALID_DATE " +
                               ",S.UNINO as SUNINO " +
                                ",S.POS_RECEIPT_TITLE AS POS_RECEIPT_TITLE " + //發票店名
                               ",length(II.PRODNO) as PRODNOLEN " +
                               "FROM MANUAL_INVOICE_HEAD IH, MANUAL_INVOICE_ITEM  II,STORE S ,PRODUCT P,SALE_HEAD " +
                               "WHERE IH.ID =II.MANUAL_INVOICE_HEAD_ID " +
                               "  AND IH.POSUUID_MASTER = SALE_HEAD.POSUUID_MASTER " +
                               "  AND IH.STORE_NO=S.STORE_NO " +
                               "  AND II.PRODNO = P.PRODNO " +
                               "  AND IH.POSUUID_MASTER=" + OracleDBUtil.SqlStr(POSUUID_MASTER) +
                               "  order by PRODNOLEN desc";

                    using (OracleCommand cmd = new OracleCommand(sql, con))
                    {
                        using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (con.State == ConnectionState.Open) con.Close();
                    con.Dispose();
                }
                if (con.State == ConnectionState.Open) con.Close();
            }


            return dt;
        }

        private DataTable getSALEData(string POSUUID_MASTER)
        {
            DataTable dt = new DataTable();
            using (OracleConnection con = OracleDBUtil.GetConnection())
            {
                try
                {
                    string sql = "SELECT * from sale_head " + //店名
                                 "  WHERE POSUUID_MASTER=" + OracleDBUtil.SqlStr(POSUUID_MASTER);

                    using (OracleCommand cmd = new OracleCommand(sql, con))
                    {
                        using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (con.State == ConnectionState.Open) con.Close();
                    con.Dispose();
                }
                if (con.State == ConnectionState.Open) con.Close();
            }


            return dt;
        }

        private DataTable getTaxstr(string store_no)
        {
            DataTable dt = new DataTable();
            using (OracleConnection con = OracleDBUtil.GetConnection())
            {
                try
                {
                    string sql = "SELECT IRS_ALLOWED_DEPARTMENT,IRS_ALLOWED_DATE,IRS_ALLOWED_TYPE,IRS_ALLOWED_NO from store " + //店名
                                 "  WHERE STORE_NO=" + OracleDBUtil.SqlStr(store_no);

                    using (OracleCommand cmd = new OracleCommand(sql, con))
                    {
                        using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (con.State == ConnectionState.Open) con.Close();
                    con.Dispose();
                }
                if (con.State == ConnectionState.Open) con.Close();
            }


            return dt;
        }

        private void getProperItem(DataTable dtOrg, DataTable dtItem)
        {
            #region 參考SQL
            //string sql = "SELECT S.STORENAME AS STORENAME " + //店名
            //                   ",IH.INVOICE_DATE AS INVOICE_DATE " + //發票日
            //                   ",IH.INVOICE_NO AS INVOICE_NO " +  //發票號碼
            //                   ",IH.BUYER AS BUYER " +  //買受人
            //                   ",IH.UNI_NO AS UNI_NO " +  //統一編號
            //                   ",IH.REMARK AS H_REMARK " +  //備註_HEAD 
            //                   ",IH.ADDRESS AS ADDRESS " +  //地址
            //                   ",IH.TAX_TYPE AS TAX_TYPE " +  //營業稅種類
            //                   ",IH.TOTAL_AMOUNT AS TOTAL_AMOUNT " + //總額
            //                   ",IH.TAX AS TAX " +  //營業稅
            //                   ",IH.SALE_AMOUNT AS SALE_AMOUNT " + //銷售合計
            //                   ",II.PROD_NAME AS PROD_NAME " + //品名
            //                   ",II.QUANTITY AS QUANTITY " + //數量
            //                   ",II.AMOUNT AS AMOUNT " +  //金額
            //                   ",II.REMARK I_REMARK " + //備註_ITEM
            //                   "FROM INVOICE_HEAD IH,INVOICE_ITEM  II,STORE S  " +
            //                   "WHERE IH.ID =II.INVOICE_HEAD_ID " +
            //                   "  AND IH.STORE_NO=S.STORE_NO " +
            //                   "  AND INVOICE_HEAD.POSUUID_MASTER=" + OracleDBUtil.SqlStr(POSUUID_MASTER);
            #endregion
            for (int i = 0; i < dtOrg.Rows.Count; i++)
            {
                DataRow dr = dtOrg.Rows[i];
                string sProdName = dr["PROD_NAME"].ToString();
                //int byteCount = getBytes(sProdName);
                //int rowNum = byteCount / 35 + ((byteCount % 35 == 0) ? 0 : 1);
                char[] arrChar = sProdName.ToCharArray();
                DataRow drItem = dtItem.NewRow();
                int rowMax = 35;
                int sumChar = 0;
                int nextCount = 0;
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < arrChar.Length; j++)
                {
                    int bytes = getBytes(arrChar[j].ToString());
                    sumChar += bytes;
                    if (sumChar <= rowMax)
                    {
                        sb.Append(arrChar[j]);
                    }
                    else
                    {
                        drItem["PROD_NAME"] = sb.ToString();
                        if (nextCount == 0)
                        {
                            drItem["QUANTITY"] = dr["QUANTITY"];
                            drItem["AMOUNT"] = dr["AMOUNT"];
                            drItem["PRICE"] = dr["PRICE"];

                        }
                        dtItem.Rows.Add(drItem);
                        nextCount += 1;

                        sumChar = 0;
                        sb.Length = 0;
                        sb.Append(arrChar[j]);
                        drItem = dtItem.NewRow();
                    }
                }

                drItem["PROD_NAME"] = sb.ToString();
                if (nextCount == 0)
                {
                    drItem["QUANTITY"] = dr["QUANTITY"];
                    drItem["AMOUNT"] = dr["AMOUNT"];
                    drItem["PRICE"] = dr["PRICE"];

                }
                dtItem.Rows.Add(drItem);
            }



        }

        private void getIProperItem(DataTable dtOrg, DataTable dtItem)
        {
            #region 參考SQL
            //string sql = "SELECT S.STORENAME AS STORENAME " + //店名
            //                   ",IH.INVOICE_DATE AS INVOICE_DATE " + //發票日
            //                   ",IH.INVOICE_NO AS INVOICE_NO " +  //發票號碼
            //                   ",IH.BUYER AS BUYER " +  //買受人
            //                   ",IH.UNI_NO AS UNI_NO " +  //統一編號
            //                   ",IH.REMARK AS H_REMARK " +  //備註_HEAD 
            //                   ",IH.ADDRESS AS ADDRESS " +  //地址
            //                   ",IH.TAX_TYPE AS TAX_TYPE " +  //營業稅種類
            //                   ",IH.TOTAL_AMOUNT AS TOTAL_AMOUNT " + //總額
            //                   ",IH.TAX AS TAX " +  //營業稅
            //                   ",IH.SALE_AMOUNT AS SALE_AMOUNT " + //銷售合計
            //                   ",II.PROD_NAME AS PROD_NAME " + //品名
            //                   ",II.QUANTITY AS QUANTITY " + //數量
            //                   ",II.AMOUNT AS AMOUNT " +  //金額
            //                   ",II.REMARK I_REMARK " + //備註_ITEM
            //                   "FROM INVOICE_HEAD IH,INVOICE_ITEM  II,STORE S  " +
            //                   "WHERE IH.ID =II.INVOICE_HEAD_ID " +
            //                   "  AND IH.STORE_NO=S.STORE_NO " +
            //                   "  AND INVOICE_HEAD.POSUUID_MASTER=" + OracleDBUtil.SqlStr(POSUUID_MASTER);
            #endregion
            for (int i = 0; i < dtOrg.Rows.Count; i++)
            {
                DataRow dr = dtOrg.Rows[i];
                string sProdName = dr["PROD_NAME"].ToString();
                //int byteCount = getBytes(sProdName);
                //int rowNum = byteCount / 35 + ((byteCount % 35 == 0) ? 0 : 1);
                char[] arrChar = sProdName.ToCharArray();
                DataRow drItem = dtItem.NewRow();
                int rowMax = 35;
                int sumChar = 0;
                int nextCount = 0;
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < arrChar.Length; j++)
                {
                    int bytes = getBytes(arrChar[j].ToString());
                    sumChar += bytes;
                    if (sumChar <= rowMax)
                    {
                        sb.Append(arrChar[j]);
                    }
                    else
                    {
                        drItem["PROD_NAME"] = sb.ToString();
                        if (nextCount == 0)
                        {
                            drItem["QUANTITY"] = dr["QUANTITY"];
                            drItem["AMOUNT"] = dr["IAMOUNT"];
                            drItem["PRICE"] = dr["PRICE"];

                        }
                        dtItem.Rows.Add(drItem);
                        nextCount += 1;

                        sumChar = 0;
                        sb.Length = 0;
                        sb.Append(arrChar[j]);
                        drItem = dtItem.NewRow();
                    }
                }

                drItem["PROD_NAME"] = sb.ToString();
                if (nextCount == 0)
                {
                    drItem["QUANTITY"] = dr["QUANTITY"];
                    drItem["AMOUNT"] = dr["IAMOUNT"];
                    drItem["PRICE"] = dr["PRICE"];

                }
                dtItem.Rows.Add(drItem);
            }



        }
        private void getNIProperItem(DataTable dtOrg, DataTable dtItem)
        {
            #region 參考SQL
            //string sql = "SELECT S.STORENAME AS STORENAME " + //店名
            //                   ",IH.INVOICE_DATE AS INVOICE_DATE " + //發票日
            //                   ",IH.INVOICE_NO AS INVOICE_NO " +  //發票號碼
            //                   ",IH.BUYER AS BUYER " +  //買受人
            //                   ",IH.UNI_NO AS UNI_NO " +  //統一編號
            //                   ",IH.REMARK AS H_REMARK " +  //備註_HEAD 
            //                   ",IH.ADDRESS AS ADDRESS " +  //地址
            //                   ",IH.TAX_TYPE AS TAX_TYPE " +  //營業稅種類
            //                   ",IH.TOTAL_AMOUNT AS TOTAL_AMOUNT " + //總額
            //                   ",IH.TAX AS TAX " +  //營業稅
            //                   ",IH.SALE_AMOUNT AS SALE_AMOUNT " + //銷售合計
            //                   ",II.PROD_NAME AS PROD_NAME " + //品名
            //                   ",II.QUANTITY AS QUANTITY " + //數量
            //                   ",II.AMOUNT AS AMOUNT " +  //金額
            //                   ",II.REMARK I_REMARK " + //備註_ITEM
            //                   "FROM INVOICE_HEAD IH,INVOICE_ITEM  II,STORE S  " +
            //                   "WHERE IH.ID =II.INVOICE_HEAD_ID " +
            //                   "  AND IH.STORE_NO=S.STORE_NO " +
            //                   "  AND INVOICE_HEAD.POSUUID_MASTER=" + OracleDBUtil.SqlStr(POSUUID_MASTER);
            #endregion
            for (int i = 0; i < dtOrg.Rows.Count; i++)
            {
                DataRow dr = dtOrg.Rows[i];
                string sProdName = dr["PROD_NAME"].ToString();
                //int byteCount = getBytes(sProdName);
                //int rowNum = byteCount / 35 + ((byteCount % 35 == 0) ? 0 : 1);
                char[] arrChar = sProdName.ToCharArray();
                DataRow drItem = dtItem.NewRow();
                int rowMax = 35;
                int sumChar = 0;
                int nextCount = 0;
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < arrChar.Length; j++)
                {
                    int bytes = getBytes(arrChar[j].ToString());
                    sumChar += bytes;
                    if (sumChar <= rowMax)
                    {
                        sb.Append(arrChar[j]);
                    }
                    else
                    {
                        drItem["PROD_NAME"] = sb.ToString();
                        if (nextCount == 0)
                        {
                            drItem["QUANTITY"] = dr["QUANTITY"];
                            drItem["AMOUNT"] = dr["AMOUNT"];
                            drItem["PRICE"] = dr["PRICE"];

                        }
                        dtItem.Rows.Add(drItem);
                        nextCount += 1;

                        sumChar = 0;
                        sb.Length = 0;
                        sb.Append(arrChar[j]);
                        drItem = dtItem.NewRow();
                    }
                }

                drItem["PROD_NAME"] = sb.ToString();
                if (nextCount == 0)
                {
                    drItem["QUANTITY"] = dr["QUANTITY"];
                    drItem["AMOUNT"] = dr["AMOUNT"];
                    drItem["PRICE"] = dr["PRICE"];

                }
                dtItem.Rows.Add(drItem);
            }



        }


        private void getZeroProperItem(DataTable dtOrg, DataTable dtItem)
        {
            #region 參考SQL
            //string sql = "SELECT S.STORENAME AS STORENAME " + //店名
            //                   ",IH.INVOICE_DATE AS INVOICE_DATE " + //發票日
            //                   ",IH.INVOICE_NO AS INVOICE_NO " +  //發票號碼
            //                   ",IH.BUYER AS BUYER " +  //買受人
            //                   ",IH.UNI_NO AS UNI_NO " +  //統一編號
            //                   ",IH.REMARK AS H_REMARK " +  //備註_HEAD 
            //                   ",IH.ADDRESS AS ADDRESS " +  //地址
            //                   ",IH.TAX_TYPE AS TAX_TYPE " +  //營業稅種類
            //                   ",IH.TOTAL_AMOUNT AS TOTAL_AMOUNT " + //總額
            //                   ",IH.TAX AS TAX " +  //營業稅
            //                   ",IH.SALE_AMOUNT AS SALE_AMOUNT " + //銷售合計
            //                   ",II.PROD_NAME AS PROD_NAME " + //品名
            //                   ",II.QUANTITY AS QUANTITY " + //數量
            //                   ",II.AMOUNT AS AMOUNT " +  //金額
            //                   ",II.REMARK I_REMARK " + //備註_ITEM
            //                   "FROM INVOICE_HEAD IH,INVOICE_ITEM  II,STORE S  " +
            //                   "WHERE IH.ID =II.INVOICE_HEAD_ID " +
            //                   "  AND IH.STORE_NO=S.STORE_NO " +
            //                   "  AND INVOICE_HEAD.POSUUID_MASTER=" + OracleDBUtil.SqlStr(POSUUID_MASTER);
            #endregion
            for (int i = 0; i < dtOrg.Rows.Count; i++)
            {
                DataRow dr = dtOrg.Rows[i];
                string sProdName = dr["PROD_NAME"].ToString();
                //int byteCount = getBytes(sProdName);
                //int rowNum = byteCount / 35 + ((byteCount % 35 == 0) ? 0 : 1);
                char[] arrChar = sProdName.ToCharArray();
                DataRow drItem = dtItem.NewRow();
                int rowMax = 35;
                int sumChar = 0;
                int nextCount = 0;
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < arrChar.Length; j++)
                {
                    int bytes = getBytes(arrChar[j].ToString());
                    sumChar += bytes;
                    if (sumChar <= rowMax)
                    {
                        sb.Append(arrChar[j]);
                    }
                    else
                    {
                        drItem["PROD_NAME"] = sb.ToString();
                        if (nextCount == 0)
                        {
                            drItem["QUANTITY"] = dr["QUANTITY"];
                            drItem["AMOUNT"] = dr["AMOUNT"];
                            drItem["PRICE"] = dr["PRICE"];

                        }
                        dtItem.Rows.Add(drItem);
                        nextCount += 1;

                        sumChar = 0;
                        sb.Length = 0;
                        sb.Append(arrChar[j]);
                        drItem = dtItem.NewRow();
                    }
                }

                drItem["PROD_NAME"] = sb.ToString();
                if (nextCount == 0)
                {
                    drItem["QUANTITY"] = dr["QUANTITY"];
                    drItem["AMOUNT"] = dr["AMOUNT"];
                    drItem["PRICE"] = dr["PRICE"];

                }
                dtItem.Rows.Add(drItem);
            }



        }

        private void getProperRemark(DataTable dtOrg, DataTable dtRemark)
        {
            #region 參考SQL
            //string sql = "SELECT S.STORENAME AS STORENAME " + //店名
            //                   ",IH.INVOICE_DATE AS INVOICE_DATE " + //發票日
            //                   ",IH.INVOICE_NO AS INVOICE_NO " +  //發票號碼
            //                   ",IH.BUYER AS BUYER " +  //買受人
            //                   ",IH.UNI_NO AS UNI_NO " +  //統一編號
            //                   ",IH.REMARK AS H_REMARK " +  //備註_HEAD 
            //                   ",IH.ADDRESS AS ADDRESS " +  //地址
            //                   ",IH.TAX_TYPE AS TAX_TYPE " +  //營業稅種類
            //                   ",IH.TOTAL_AMOUNT AS TOTAL_AMOUNT " + //總額
            //                   ",IH.TAX AS TAX " +  //營業稅
            //                   ",IH.SALE_AMOUNT AS SALE_AMOUNT " + //銷售合計
            //                   ",II.PROD_NAME AS PROD_NAME " + //品名
            //                   ",II.QUANTITY AS QUANTITY " + //數量
            //                   ",II.AMOUNT AS AMOUNT " +  //金額
            //                   ",II.REMARK I_REMARK " + //備註_ITEM
            //                   "FROM INVOICE_HEAD IH,INVOICE_ITEM  II,STORE S  " +
            //                   "WHERE IH.ID =II.INVOICE_HEAD_ID " +
            //                   "  AND IH.STORE_NO=S.STORE_NO " +
            //                   "  AND INVOICE_HEAD.POSUUID_MASTER=" + OracleDBUtil.SqlStr(POSUUID_MASTER);
            #endregion
            for (int i = 0; i < 1; i++)
            {
                DataRow dr = dtOrg.Rows[i];
                if (dr["H_REMARK"] != DBNull.Value && !string.IsNullOrEmpty(dr["H_REMARK"].ToString()))
                {
                    string sProdName = dr["H_REMARK"].ToString();
                    //int byteCount = getBytes(sProdName);
                    //int rowNum = byteCount / 35 + ((byteCount % 35 == 0) ? 0 : 1);
                    char[] arrChar = sProdName.ToCharArray();
                    DataRow drRemark = dtRemark.NewRow();
                    int rowMax = 30;
                    int sumChar = 0;
                    int nextCount = 0;
                    StringBuilder sb = new StringBuilder();
                    for (int j = 0; j < arrChar.Length; j++)
                    {
                        int bytes = getBytes(arrChar[j].ToString());
                        sumChar += bytes;
                        if (sumChar <= rowMax)
                        {
                            sb.Append(arrChar[j]);
                        }
                        else
                        {
                            drRemark["Remark"] = sb.ToString();

                            dtRemark.Rows.Add(drRemark);
                            nextCount += 1;

                            sumChar = 0;
                            sb.Length = 0;
                            sb.Append(arrChar[j]);
                            drRemark = dtRemark.NewRow();
                        }
                    }
                    drRemark["Remark"] = sb.ToString();
                    dtRemark.Rows.Add(drRemark);
                }
            }

            for (int i = 0; i < dtOrg.Rows.Count; i++)
            {
                DataRow dr = dtOrg.Rows[i];
                if (dr["I_REMARK"] != DBNull.Value && !string.IsNullOrEmpty(dr["I_REMARK"].ToString()))
                {
                    string sProdName = dr["I_REMARK"].ToString();
                    //int byteCount = getBytes(sProdName);
                    //int rowNum = byteCount / 35 + ((byteCount % 35 == 0) ? 0 : 1);
                    char[] arrChar = sProdName.ToCharArray();
                    DataRow drRemark = dtRemark.NewRow();
                    int rowMax = 30;
                    int sumChar = 0;
                    int nextCount = 0;
                    StringBuilder sb = new StringBuilder();
                    for (int j = 0; j < arrChar.Length; j++)
                    {
                        int bytes = getBytes(arrChar[j].ToString());
                        sumChar += bytes;
                        if (sumChar <= rowMax)
                        {
                            sb.Append(arrChar[j]);
                        }
                        else
                        {
                            drRemark["Remark"] = sb.ToString();

                            dtRemark.Rows.Add(drRemark);
                            nextCount += 1;

                            sumChar = 0;
                            sb.Length = 0;
                            sb.Append(arrChar[j]);
                            drRemark = dtRemark.NewRow();
                        }
                    }
                    drRemark["Remark"] = sb.ToString();
                    dtRemark.Rows.Add(drRemark);
                }
            }


        }

        private int getCopies(DataTable dt)
        {
            int iRet = 1;
            return iRet;
        }

        /// <summary>
        /// 產生第一聯:存根聯(第一張)
        /// </summary>
        /// <param name="pdfDoc">ITEXT.Document</param>
        private void add0(Document pdfDoc, DataTable dtOrg, DataTable dtItem, DataTable dtRemark,string nowpage,string totalpage)
        {
            if (dtOrg.Rows.Count > 0)
            {
                DataRow drOrg = dtOrg.Rows[0];
                string STORENAME = (drOrg["STORENAME"] != DBNull.Value) ? drOrg["STORENAME"].ToString() : "";//店名
                string INVOICE_DATE = (drOrg["INVOICE_DATE"] != DBNull.Value) ? drOrg["INVOICE_DATE"].ToString() : "";//發票日
                string INVOICE_NO = (drOrg["INVOICE_NO"] != DBNull.Value) ? drOrg["INVOICE_NO"].ToString() : "";//發票號碼
                string BUYER = (drOrg["BUYER"] != DBNull.Value) ? drOrg["BUYER"].ToString() : "";//買受人
                string UNI_NO = (drOrg["UNI_NO"] != DBNull.Value) ? drOrg["UNI_NO"].ToString() : "";//統一編號
                string ADDRESS = (drOrg["ADDRESS"] != DBNull.Value) ? drOrg["ADDRESS"].ToString() : "";//地址
                string TAX_TYPE = (drOrg["TAX_TYPE"] != DBNull.Value) ? drOrg["TAX_TYPE"].ToString() : "";//營業稅種類
                string TOTAL_AMOUNT = (drOrg["TOTAL_AMOUNT"] != DBNull.Value) ? drOrg["TOTAL_AMOUNT"].ToString() : "0";//總額
                string TAX = (drOrg["TAX"] != DBNull.Value) ? drOrg["TAX"].ToString() : "0";//營業稅
                string SALE_AMOUNT = (drOrg["SALE_AMOUNT"] != DBNull.Value) ? drOrg["SALE_AMOUNT"].ToString() : "0";//銷售合計
                string I_AMOUNT = (drOrg["IAMOUNT"] != DBNull.Value) ? drOrg["IAMOUNT"].ToString() : "0";//單價
                string STORENO = (drOrg[16] != DBNull.Value) ? drOrg[16].ToString() : "0";//門市編號
                string UNI_TITLE = (drOrg[17] != DBNull.Value) ? drOrg[17].ToString() : "";//發票抬頭
                string POS_RECEIPT_TITLE = (drOrg["POS_RECEIPT_TITLE"] != DBNull.Value) ? drOrg["POS_RECEIPT_TITLE"].ToString() : "";//發票抬頭
                string N = INVOICE_NO.Substring(7, 1);
                string M = INVOICE_NO.Substring(9, 1);
                string NM = Convert.ToString((Convert.ToInt32(N) + Convert.ToInt32(M)) * 3);
                string checkcode = NM.Substring(NM.Length - 1, 1);
                string IRS_ALLOWED_DEPARTMENT = "";
                string IRS_ALLOWED_DATE = "";
                string IRS_ALLOWED_TYPE = "";
                string IRS_ALLOWED_NO = "";
                string INVOICE_TOTAL_AMOUNT = (dtOrg.Rows[0]["TOTAL_AMOUNT"] != DBNull.Value) ? dtOrg.Rows[0]["TOTAL_AMOUNT"].ToString() : "0";
                string BEFORE_TAX_AMOUNT = (dtOrg.Rows[0]["BEFORE_TAX"] != DBNull.Value) ? dtOrg.Rows[0]["BEFORE_TAX"].ToString() : "0";
                string SALE_TAX = Convert.ToString(Convert.ToInt32(INVOICE_TOTAL_AMOUNT) - Convert.ToInt32(BEFORE_TAX_AMOUNT));
                if (STORENO != "")
                {
                    DataTable dttax = getTaxstr(STORENO);
                    IRS_ALLOWED_DEPARTMENT = (dttax.Rows[0]["IRS_ALLOWED_DEPARTMENT"] != DBNull.Value) ? dttax.Rows[0]["IRS_ALLOWED_DEPARTMENT"].ToString() : "";
                    IRS_ALLOWED_DATE = (dttax.Rows[0]["IRS_ALLOWED_DATE"] != DBNull.Value) ? dttax.Rows[0]["IRS_ALLOWED_DATE"].ToString() : "";
                    IRS_ALLOWED_TYPE = (dttax.Rows[0]["IRS_ALLOWED_TYPE"] != DBNull.Value) ? dttax.Rows[0]["IRS_ALLOWED_TYPE"].ToString() : "";
                    IRS_ALLOWED_NO = (dttax.Rows[0]["IRS_ALLOWED_NO"] != DBNull.Value) ? dttax.Rows[0]["IRS_ALLOWED_NO"].ToString() : "";

                }
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
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(ChapPath) + "\\" + STORENO + ".JPG");
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
                //p1 = new Paragraph("遠傳電信股份有限公司台大公館門市", s14);
                p1 = new Paragraph(POS_RECEIPT_TITLE, s14);
                tc = new PdfPCell(p1);
                tc.FixedHeight = 27f;
                tbTemp.AddCell(CF(CF(tc, "H", Rectangle.ALIGN_CENTER), "B", 0));
                p1 = new Paragraph("電子計算機統一發票", s11);
                tc = new PdfPCell(p1);
                tbTemp.AddCell(CF(CF(tc, "H", Rectangle.ALIGN_CENTER), "B", 0));
                //p1 = new Paragraph("中華民國" + DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日", s09);
                //p1 = new Paragraph("中 華 民 國    年   月   日", s11);
                p1 = new Paragraph("中 華 民 國", s11);
                DateTime dINVOICE_DATE = Convert.ToDateTime(INVOICE_DATE);
                //p1.Add(dINVOICE_DATE.Year.ToString());
                p1.Add((dINVOICE_DATE.Year - 1911).ToString().PadLeft(4, ' '));
                p1.Add("年");
                p1.Add(dINVOICE_DATE.Month.ToString().PadLeft(4, ' '));
                p1.Add("　月");
                p1.Add(dINVOICE_DATE.Day.ToString().PadLeft(4, ' '));
                p1.Add("　日");
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
                p1 = new Paragraph("發票號碼:" + INVOICE_NO, s105);//左
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("", s105);//右
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("買 受 人:" + UNI_TITLE, s105);//左
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("", s105);//右
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("統一編號:" + UNI_NO, s105);//左
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("檢查號碼:" + checkcode + "      頁次:" + nowpage + "/" + totalpage, s105);//右
          
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("地    址:" + ADDRESS , s105);//左
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
                if (dtItem != null && dtItem.Rows.Count > 0)
                {
                    Paragraph p2 = new Paragraph("", s09);
                    Paragraph p3 = new Paragraph("", s09);
                    Paragraph p4 = new Paragraph("", s09);
                    for (int j = 0; j < dtItem.Rows.Count; j++)
                    {
                        DataRow _dr = dtItem.Rows[j];
                        string sPROD_NAME = (_dr["PROD_NAME"] != DBNull.Value) ? _dr["PROD_NAME"].ToString() : "";
                        string sQUANTITY = (_dr["QUANTITY"] != DBNull.Value) ? _dr["QUANTITY"].ToString() : "";
                        string sPRICE = (_dr["PRICE"] != DBNull.Value) ? _dr["PRICE"].ToString() : "";
                        string sAMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                        if (j == 0 && nowpage != "1")
                        {
                            p1.Add("        ＜承     上     頁＞");
                            p2.Add("");
                            p3.Add("");
                            p4.Add("");
                            p1.Add("\n");
                            p2.Add("\n");
                            p3.Add("\n");
                            p4.Add("\n");
                        }
                        if (j != 0)
                        {
                            p1.Add("\n");
                            p2.Add("\n");
                            p3.Add("\n");
                            p4.Add("\n");
                        }


                        p1.Add(sPROD_NAME);
                        p2.Add(sQUANTITY);

                        if (!string.IsNullOrEmpty(sPRICE))
                        {
                            double dTmp = Convert.ToDouble(sPRICE);
                            int iprice = Convert.ToInt32(Math.Round(dTmp / 1.05, 0, MidpointRounding.AwayFromZero));
                            sPRICE = StringUtil.NumberFormat(iprice, 0, true);
                        }
                        p3.Add(sPRICE);
                        if (!string.IsNullOrEmpty(sAMOUNT))
                        {
                            double dTmp = Convert.ToDouble(sAMOUNT);
                            sAMOUNT = StringUtil.NumberFormat(dTmp, 0, true);
                        }
                        p4.Add(sAMOUNT);
                    }
                    if (nowpage != totalpage)
                    {
                        p1.Add("\n");
                        p2.Add("\n");
                        p3.Add("\n");
                        p4.Add("\n");
                        p1.Add("        ＜續     下     頁＞");
                        p2.Add("");
                        p3.Add("");
                        p4.Add("");
                    }
                    tc = CF(new PdfPCell(p1), "HT", 127.56f);
                    tbInner02.AddCell(tc);
                    tc = new PdfPCell(p2);
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbInner02.AddCell(tc);
                    tc = new PdfPCell(p3);
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbInner02.AddCell(tc);
                    tc = new PdfPCell(p4);
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbInner02.AddCell(tc);
                }
                else
                {
                    tbInner02.AddCell(CF(new PdfPCell(p1), "HT", 127.56f));
                    tbInner02.AddCell(new PdfPCell(p1));
                    tbInner02.AddCell(new PdfPCell(p1));
                    tbInner02.AddCell(new PdfPCell(p1));
                }


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
                double dSALE_AMOUNT = Convert.ToDouble(SALE_AMOUNT);
                SALE_AMOUNT = StringUtil.NumberFormat(dSALE_AMOUNT, 0, true);
                if (nowpage != "1")
                {
                    SALE_AMOUNT = "";
                }
                p1 = new Paragraph(SALE_AMOUNT, s10);
                tc = CF(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_RIGHT), "V", Rectangle.ALIGN_MIDDLE);
                //tc.FixedHeight = 14.17f;
                tbInner04.AddCell(tc);//銷售額合計Value

                p1 = new Paragraph("營業稅", s09);
                tc = CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER);
                tc.FixedHeight = 14.17f;
                tbInner04.AddCell(tc);//營業稅Title
                p1 = new Paragraph("應稅", s09);
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//應稅Title
                if (TAX_TYPE == "1")
                {
                    p1 = new Paragraph("V", s09);
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//應稅value
                p1 = new Paragraph("零稅率", s09);
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//零稅率Title
                if (TAX_TYPE == "2")
                {
                    p1 = new Paragraph("V", s09);
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//零稅率Value
                p1 = new Paragraph("免稅", s09);
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//免稅Title
                if (TAX_TYPE == "3")
                {
                    p1 = new Paragraph("V", s09);
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }
                tbInner04.AddCell(p1);//免稅Value
                double dTAX = Convert.ToDouble(SALE_TAX);
                TAX = StringUtil.NumberFormat(dTAX, 0, true);
                if (nowpage != "1")
                {
                    TAX = "";
                }
                tc = new PdfPCell(new Paragraph(TAX, s09));
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tbInner04.AddCell(tc);//營業稅Value

                tc2 = new PdfPCell(new Paragraph("總                                             額", s10));
                tc2.Colspan = 7;
                tc2.FixedHeight = 14.17f;
                tbInner04.AddCell(CF(tc2, "H", Element.ALIGN_CENTER));//總計Title
                double dTOTAL_AMOUNT = Convert.ToDouble(INVOICE_TOTAL_AMOUNT);
                string sTOTAL_AMOUNT = StringUtil.NumberFormat(dTOTAL_AMOUNT, 0, true);
                if (nowpage != "1")
                {
                    sTOTAL_AMOUNT = "";
                }
                tc = new PdfPCell(new Paragraph(sTOTAL_AMOUNT, s09));
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tbInner04.AddCell(tc);//總計Value
                //        append to Outer Table
                tc = new PdfPCell(tbInner04);
                tc.BorderWidth = 1;
                tc.Colspan = 4;
                tbInner02.AddCell(tc);

                PdfPTable tTemp = new PdfPTable(new float[] { 1.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1.4f });
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

                if (nowpage != "1")
                {
                    INVOICE_TOTAL_AMOUNT = "0";
                }
                int iTOTAL_AMOUNT = Convert.ToInt32(INVOICE_TOTAL_AMOUNT);           
                int amount = iTOTAL_AMOUNT / 10000000;
                string sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 10000000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　仟", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "佰", s09);

                amount = iTOTAL_AMOUNT / 1000000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 1000000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　佰", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "拾", s09);

                amount = iTOTAL_AMOUNT / 100000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 100000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　拾", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "萬", s09);

                amount = iTOTAL_AMOUNT / 10000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 10000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　萬", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "仟", s09);

                amount = iTOTAL_AMOUNT / 1000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 1000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　仟", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "佰", s09);

                amount = iTOTAL_AMOUNT / 100;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 100;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　佰", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "拾", s09);

                amount = iTOTAL_AMOUNT / 10;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 10;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　拾", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "元整", s09);

                sAmount = GF_Converts(iTOTAL_AMOUNT.ToString());
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　元整", s10));
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
                if (dtRemark != null && dtRemark.Rows.Count > 0)
                {
                    for (int j = 0; j < dtRemark.Rows.Count; j++)
                    {
                      //  if (j != 0) { p1.Add("\n"); }
                        p1.Add(dtRemark.Rows[j][0].ToString().Replace(',', ' '));
                        //p1.Add(dtRemark.Rows[j][0].ToString());
                    }
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }


                tbInner03.AddCell(CF(new PdfPCell(p1), "HT", 90.71f));
                p1 = new Paragraph("營業人蓋用統一發票專用章", s10);
                tbInner03.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
                //jpg = iTextSharp.text.Image.GetInstance("F:\\Title02.JPG");
                // iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(ChapPath) + "\\" + STORENO + ".JPG");
                jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(ChapPath) + "\\" + STORENO + ".JPG");
                jpg.SetAbsolutePosition(0, 0);
                float x1 = jpg.Height;
                float y1 = jpg.Width;
                int JpgH = Convert.ToInt32(x1);
                int reallyH = 14100 / JpgH;
                jpg.ScalePercent(7);

                tc = new PdfPCell(jpg, false);
                tc.PaddingTop = 1;
                tc.PaddingLeft = 15;
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
                string[] YYMMDD = IRS_ALLOWED_DATE.Split('/');
                string YYYYYY = "";
                string MMMMMM = "";
                string DDDDDD = "";
                if (IRS_ALLOWED_DATE != "") 
                {
                    YYYYYY = YYMMDD[0].ToString();
                    MMMMMM = YYMMDD[1].ToString();
                    DDDDDD = YYMMDD[2].ToString();
                }
                //                p1 = new Paragraph("本發票依    " + IRS_ALLOWED_DEPARTMENT + "  " + YYMMDD[0].ToString() + " 年" + YYMMDD[1].ToString() + " 月" + YYMMDD[2].ToString() + " 日" + IRS_ALLOWED_TYPE + " 字第" + IRS_ALLOWED_NO + " 號函核准使用。", s07);

                //p1.Add(new Chunk("\n※本公司若要作廢或更改者，應於發票次月五日前寄送本公司，逾期不受理。", s07));
                //p1.Add(new Chunk("\n※依台北市國稅局大安分局 年 月 日         字第        號函核准使用。", s07));
                p1 = new Paragraph("依" + IRS_ALLOWED_DEPARTMENT + " " + YYYYYY + "年 " + MMMMMM + "月 " + DDDDDD + "日 " + IRS_ALLOWED_TYPE + "字第 " + IRS_ALLOWED_NO + "號函核准使用。", s07);
                tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));

                pdfTable.AddCell(CF(new PdfPCell(tbFooter), "B", 0));

                ////調整版面用
                p1 = new Paragraph("");
                tc = CF(new PdfPCell(p1), "B", 0);
                tc.FixedHeight = 43.2f;
                pdfTable.AddCell(tc);


                pdfDoc.Add(pdfTable);
            }
        }

        /// <summary>
        /// 產生第一聯:存根聯(第一張)(個人發票，無統編)
        /// </summary>
        /// <param name="pdfDoc">ITEXT.Document</param>
        private void add0NoUNI(Document pdfDoc, DataTable dtOrg, DataTable dtItem, DataTable dtRemark, string nowpage, string totalpage)
        {
            if (dtOrg.Rows.Count > 0)
            {
                DataRow drOrg = dtOrg.Rows[0];
                string STORENAME = (drOrg["STORENAME"] != DBNull.Value) ? drOrg["STORENAME"].ToString() : "";//店名
                string INVOICE_DATE = (drOrg["INVOICE_DATE"] != DBNull.Value) ? drOrg["INVOICE_DATE"].ToString() : "";//發票日
                string INVOICE_NO = (drOrg["INVOICE_NO"] != DBNull.Value) ? drOrg["INVOICE_NO"].ToString() : "";//發票號碼
                string BUYER = (drOrg["BUYER"] != DBNull.Value) ? drOrg["BUYER"].ToString() : "";//買受人
                string UNI_NO = (drOrg["UNI_NO"] != DBNull.Value) ? drOrg["UNI_NO"].ToString() : "";//統一編號
                string ADDRESS = (drOrg["ADDRESS"] != DBNull.Value) ? drOrg["ADDRESS"].ToString() : "";//地址
                string TAX_TYPE = (drOrg["TAX_TYPE"] != DBNull.Value) ? drOrg["TAX_TYPE"].ToString() : "";//營業稅種類
                string TOTAL_AMOUNT = (drOrg["TOTAL_AMOUNT"] != DBNull.Value) ? drOrg["TOTAL_AMOUNT"].ToString() : "0";//總額
                string TAX = (drOrg["TAX"] != DBNull.Value) ? drOrg["TAX"].ToString() : "0";//營業稅
                string SALE_AMOUNT = (drOrg["SALE_AMOUNT"] != DBNull.Value) ? drOrg["SALE_AMOUNT"].ToString() : "0";//銷售合計
                string STORENO = (drOrg[16] != DBNull.Value) ? drOrg[16].ToString() : "";//門市編號
                string POS_RECEIPT_TITLE = (drOrg["POS_RECEIPT_TITLE"] != DBNull.Value) ? drOrg["POS_RECEIPT_TITLE"].ToString() : "";//發票抬頭
                string N = INVOICE_NO.Substring(7, 1);
                string M = INVOICE_NO.Substring(9, 1);
                string NM = Convert.ToString((Convert.ToInt32(N) + Convert.ToInt32(M)) * 3);
                string checkcode = NM.Substring(NM.Length - 1, 1);
                string IRS_ALLOWED_DEPARTMENT = "";
                string IRS_ALLOWED_DATE = "";
                string IRS_ALLOWED_TYPE = "";
                string IRS_ALLOWED_NO = "";
                if (STORENO != "")
                {
                    DataTable dttax = getTaxstr(STORENO);
                    IRS_ALLOWED_DEPARTMENT = (dttax.Rows[0]["IRS_ALLOWED_DEPARTMENT"] != DBNull.Value) ? dttax.Rows[0]["IRS_ALLOWED_DEPARTMENT"].ToString() : "";
                    IRS_ALLOWED_DATE = (dttax.Rows[0]["IRS_ALLOWED_DATE"] != DBNull.Value) ? dttax.Rows[0]["IRS_ALLOWED_DATE"].ToString() : "";
                    IRS_ALLOWED_TYPE = (dttax.Rows[0]["IRS_ALLOWED_TYPE"] != DBNull.Value) ? dttax.Rows[0]["IRS_ALLOWED_TYPE"].ToString() : "";
                    IRS_ALLOWED_NO = (dttax.Rows[0]["IRS_ALLOWED_NO"] != DBNull.Value) ? dttax.Rows[0]["IRS_ALLOWED_NO"].ToString() : "";

                }
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
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(ChapPath) + "\\" + STORENO + ".JPG");
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
                //p1 = new Paragraph("遠傳電信股份有限公司台大公館門市", s14);
                p1 = new Paragraph(POS_RECEIPT_TITLE, s14);
                tc = new PdfPCell(p1);
                tc.FixedHeight = 27f;
                tbTemp.AddCell(CF(CF(tc, "H", Rectangle.ALIGN_CENTER), "B", 0));
                p1 = new Paragraph("電子計算機統一發票", s11);
                tc = new PdfPCell(p1);
                tbTemp.AddCell(CF(CF(tc, "H", Rectangle.ALIGN_CENTER), "B", 0));
                //p1 = new Paragraph("中華民國" + DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日", s09);
                //p1 = new Paragraph("中 華 民 國    年   月   日", s11);
                p1 = new Paragraph("中 華 民 國", s11);
                DateTime dINVOICE_DATE = Convert.ToDateTime(INVOICE_DATE);
                //p1.Add(dINVOICE_DATE.Year.ToString());
                p1.Add((dINVOICE_DATE.Year - 1911).ToString().PadLeft(4, ' '));
                p1.Add("年");
                p1.Add(dINVOICE_DATE.Month.ToString().PadLeft(4, ' '));
                p1.Add("　月");
                p1.Add(dINVOICE_DATE.Day.ToString().PadLeft(4, ' '));
                p1.Add("　日");
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
                p1 = new Paragraph("發票號碼:" + INVOICE_NO, s105);//左
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("", s105);//右
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("買 受 人:" + BUYER, s105);//左
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("", s105);//右
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("統一編號:"  , s105);//左
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("檢查號碼:" + checkcode + "      頁次:" + nowpage + "/" + totalpage, s105);//右
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("地    址:" + ADDRESS, s105);//左
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
                if (dtItem != null && dtItem.Rows.Count > 0)
                {
                    Paragraph p2 = new Paragraph("", s09);
                    Paragraph p3 = new Paragraph("", s09);
                    Paragraph p4 = new Paragraph("", s09);
                    for (int j = 0; j < dtItem.Rows.Count; j++)
                    {
                        DataRow _dr = dtItem.Rows[j];
                        string sPROD_NAME = (_dr["PROD_NAME"] != DBNull.Value) ? _dr["PROD_NAME"].ToString() : "";
                        string sQUANTITY = (_dr["QUANTITY"] != DBNull.Value) ? _dr["QUANTITY"].ToString() : "";
                        string sPRICE = (_dr["PRICE"] != DBNull.Value) ? _dr["PRICE"].ToString() : "";
                        string sAMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                        if (j == 0 && nowpage != "1")
                        {
                            p1.Add("        ＜承     上     頁＞");
                            p2.Add("");
                            p3.Add("");
                            p4.Add("");
                            p1.Add("\n");
                            p2.Add("\n");
                            p3.Add("\n");
                            p4.Add("\n");
                        }
                        if (j != 0)
                        {
                            p1.Add("\n");
                            p2.Add("\n");
                            p3.Add("\n");
                            p4.Add("\n");
                        }


                        p1.Add(sPROD_NAME);
                        p2.Add(sQUANTITY);

                        if (!string.IsNullOrEmpty(sPRICE))
                        {
                            double dTmp = Convert.ToDouble(sPRICE);
                            sPRICE = StringUtil.NumberFormat(dTmp, 0, true);
                        }
                        p3.Add(sPRICE);
                        if (!string.IsNullOrEmpty(sAMOUNT))
                        {
                            double dTmp = Convert.ToDouble(sAMOUNT);
                            sAMOUNT = StringUtil.NumberFormat(dTmp, 0, true);
                        }
                        p4.Add(sAMOUNT);
                    }
                    if (nowpage != totalpage)
                    {
                        p1.Add("\n");
                        p2.Add("\n");
                        p3.Add("\n");
                        p4.Add("\n");
                        p1.Add("        ＜續     下     頁＞");
                        p2.Add("");
                        p3.Add("");
                        p4.Add("");
                    }
                    tc = CF(new PdfPCell(p1), "HT", 127.56f);
                    tbInner02.AddCell(tc);
                    tc = new PdfPCell(p2);
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbInner02.AddCell(tc);
                    tc = new PdfPCell(p3);
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbInner02.AddCell(tc);
                    tc = new PdfPCell(p4);
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbInner02.AddCell(tc);
                }
                else
                {
                    tbInner02.AddCell(CF(new PdfPCell(p1), "HT", 127.56f));
                    tbInner02.AddCell(new PdfPCell(p1));
                    tbInner02.AddCell(new PdfPCell(p1));
                    tbInner02.AddCell(new PdfPCell(p1));
                }
               


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
                double dSALE_AMOUNT = Convert.ToDouble(TOTAL_AMOUNT);
                SALE_AMOUNT = StringUtil.NumberFormat(dSALE_AMOUNT, 0, true);
                if (nowpage != "1")
                {
                    SALE_AMOUNT = "";
                }
                p1 = new Paragraph(SALE_AMOUNT, s10);
                tc = CF(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_RIGHT), "V", Rectangle.ALIGN_MIDDLE);
                //tc.FixedHeight = 14.17f;
                tbInner04.AddCell(tc);//銷售額合計Value

                p1 = new Paragraph("營業稅", s09);
                tc = CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER);
                tc.FixedHeight = 14.17f;
                tbInner04.AddCell(tc);//營業稅Title
                p1 = new Paragraph("應稅", s09);
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//應稅Title
                if (TAX_TYPE == "1")
                {
                    p1 = new Paragraph("V", s09);
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//應稅value
                p1 = new Paragraph("零稅率", s09);
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//零稅率Title
                if (TAX_TYPE == "2")
                {
                    p1 = new Paragraph("V", s09);
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//零稅率Value
                p1 = new Paragraph("免稅", s09);
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//免稅Title
                if (TAX_TYPE == "3")
                {
                    p1 = new Paragraph("V", s09);
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }
                tbInner04.AddCell(p1);//免稅Value
                double dTAX = Convert.ToDouble(TAX);
                TAX = StringUtil.NumberFormat(dTAX, 0, true);
                tc = new PdfPCell(new Paragraph("", s09));
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tbInner04.AddCell(tc);//營業稅Value

                tc2 = new PdfPCell(new Paragraph("總                                             額", s10));
                tc2.Colspan = 7;
                tc2.FixedHeight = 14.17f;
                tbInner04.AddCell(CF(tc2, "H", Element.ALIGN_CENTER));//總計Title
                double dTOTAL_AMOUNT = Convert.ToDouble(TOTAL_AMOUNT);
                string sTOTAL_AMOUNT = StringUtil.NumberFormat(dTOTAL_AMOUNT, 0, true);
                if (nowpage != "1")
                {
                    sTOTAL_AMOUNT = "";
                }
                tc = new PdfPCell(new Paragraph(sTOTAL_AMOUNT, s09));
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tbInner04.AddCell(tc);//總計Value
                //        append to Outer Table
                tc = new PdfPCell(tbInner04);
                tc.BorderWidth = 1;
                tc.Colspan = 4;
                tbInner02.AddCell(tc);

                PdfPTable tTemp = new PdfPTable(new float[] { 1.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1.4f });
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
                if (nowpage != "1")
                {
                    TOTAL_AMOUNT = "0";
                }
                int iTOTAL_AMOUNT = Convert.ToInt32(TOTAL_AMOUNT);
                int amount = iTOTAL_AMOUNT / 10000000;
                string sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 10000000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　仟", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "佰", s09);

                amount = iTOTAL_AMOUNT / 1000000;
                sAmount = GF_Converts(amount.ToString());
               
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 1000000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　佰", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "拾", s09);

                amount = iTOTAL_AMOUNT / 100000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 100000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　拾", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "萬", s09);

                amount = iTOTAL_AMOUNT / 10000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 10000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　萬", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "仟", s09);

                amount = iTOTAL_AMOUNT / 1000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 1000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　仟", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "佰", s09);

                amount = iTOTAL_AMOUNT / 100;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 100;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　佰", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "拾", s09);

                amount = iTOTAL_AMOUNT / 10;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 10;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　拾", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "元整", s09);

                sAmount = GF_Converts(iTOTAL_AMOUNT.ToString());
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　元整", s10));
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
                if (dtRemark != null && dtRemark.Rows.Count > 0)
                {
                    for (int j = 0; j < dtRemark.Rows.Count; j++)
                    {
                        //if (j != 0) { p1.Add("\n"); }
                        p1.Add(dtRemark.Rows[j][0].ToString().Replace(',', ' '));
                    }
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }


                tbInner03.AddCell(CF(new PdfPCell(p1), "HT", 90.71f));
                p1 = new Paragraph("營業人蓋用統一發票專用章", s10);
                tbInner03.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
                //jpg = iTextSharp.text.Image.GetInstance("F:\\Title02.JPG");
                // iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(ChapPath) + "\\" + STORENO + ".JPG");
                jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(ChapPath) + "\\" + STORENO + ".JPG");
                jpg.SetAbsolutePosition(0, 0);
                float x1 = jpg.Height;
                float y1 = jpg.Width;
                int JpgH = Convert.ToInt32(x1);
                int reallyH = 14100 / JpgH;
                jpg.ScalePercent(7);

                tc = new PdfPCell(jpg, false);
                tc.PaddingTop = 1;
                tc.PaddingLeft = 15;
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
                string[] YYMMDD = IRS_ALLOWED_DATE.Split('/');
                string YYYYYY = "";
                string MMMMMM = "";
                string DDDDDD = "";
                if (IRS_ALLOWED_DATE != "")
                {
                    YYYYYY = YYMMDD[0].ToString();
                    MMMMMM = YYMMDD[1].ToString();
                    DDDDDD = YYMMDD[2].ToString();
                }
                p1 = new Paragraph("本發票依" + IRS_ALLOWED_DEPARTMENT + " " + YYYYYY + "年 " + MMMMMM + "月 " + DDDDDD + "日 " + IRS_ALLOWED_TYPE + "字第 " + IRS_ALLOWED_NO + "號函核准使用。", s07);


              
                tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));

                pdfTable.AddCell(CF(new PdfPCell(tbFooter), "B", 0));

                ////調整版面用
                p1 = new Paragraph("");
                tc = CF(new PdfPCell(p1), "B", 0);
                tc.FixedHeight = 43.2f;
                pdfTable.AddCell(tc);


                pdfDoc.Add(pdfTable);
            }
        }


        /// <summary>
        /// 產生第二聯:扣抵聯(第二張)
        /// </summary>
        /// <param name="pdfDoc">ITEXT.Document</param>
        private void add1(Document pdfDoc, DataTable dtOrg, DataTable dtItem, DataTable dtRemark, string nowpage, string totalpage)
        {
            if (dtOrg.Rows.Count > 0)
            {
                DataRow drOrg = dtOrg.Rows[0];
                string STORENAME = (drOrg["STORENAME"] != DBNull.Value) ? drOrg["STORENAME"].ToString() : "";//店名
                string INVOICE_DATE = (drOrg["INVOICE_DATE"] != DBNull.Value) ? drOrg["INVOICE_DATE"].ToString() : "";//發票日
                string INVOICE_NO = (drOrg["INVOICE_NO"] != DBNull.Value) ? drOrg["INVOICE_NO"].ToString() : "";//發票號碼
                string BUYER = (drOrg["BUYER"] != DBNull.Value) ? drOrg["BUYER"].ToString() : "";//買受人
                string UNI_NO = (drOrg["UNI_NO"] != DBNull.Value) ? drOrg["UNI_NO"].ToString() : "";//統一編號
                string ADDRESS = (drOrg["ADDRESS"] != DBNull.Value) ? drOrg["ADDRESS"].ToString() : "";//地址
                string TAX_TYPE = (drOrg["TAX_TYPE"] != DBNull.Value) ? drOrg["TAX_TYPE"].ToString() : "";//營業稅種類
                string TOTAL_AMOUNT = (drOrg["TOTAL_AMOUNT"] != DBNull.Value) ? drOrg["TOTAL_AMOUNT"].ToString() : "0";//總額
                string TAX = (drOrg["TAX"] != DBNull.Value) ? drOrg["TAX"].ToString() : "0";//營業稅
                string SALE_AMOUNT = (drOrg["SALE_AMOUNT"] != DBNull.Value) ? drOrg["SALE_AMOUNT"].ToString() : "0";//銷售合計
                string I_AMOUNT = (drOrg["IAMOUNT"] != DBNull.Value) ? drOrg["IAMOUNT"].ToString() : "0";//單價
                string STORENO = (drOrg[16] != DBNull.Value) ? drOrg[16].ToString() : "";//門市編號
                string UNI_TITLE = (drOrg[17] != DBNull.Value) ? drOrg[17].ToString() : "";//發票抬頭
                string POS_RECEIPT_TITLE = (drOrg["POS_RECEIPT_TITLE"] != DBNull.Value) ? drOrg["POS_RECEIPT_TITLE"].ToString() : "";//發票抬頭
                
                string N = INVOICE_NO.Substring(7, 1);
                string M = INVOICE_NO.Substring(9, 1);
                string NM = Convert.ToString((Convert.ToInt32(N) + Convert.ToInt32(M)) * 3);
                string checkcode = NM.Substring(NM.Length - 1, 1);
                string IRS_ALLOWED_DEPARTMENT = "";
                string IRS_ALLOWED_DATE = "";
                string IRS_ALLOWED_TYPE = "";
                string IRS_ALLOWED_NO = "";
                string INVOICE_TOTAL_AMOUNT = (dtOrg.Rows[0]["TOTAL_AMOUNT"] != DBNull.Value) ? dtOrg.Rows[0]["TOTAL_AMOUNT"].ToString() : "0";
                string BEFORE_TAX_AMOUNT = (dtOrg.Rows[0]["BEFORE_TAX"] != DBNull.Value) ? dtOrg.Rows[0]["BEFORE_TAX"].ToString() : "0";
                string SALE_TAX = Convert.ToString(Convert.ToInt32(INVOICE_TOTAL_AMOUNT) - Convert.ToInt32(BEFORE_TAX_AMOUNT));
                if (STORENO != "")
                {
                    DataTable dttax = getTaxstr(STORENO);
                    IRS_ALLOWED_DEPARTMENT = (dttax.Rows[0]["IRS_ALLOWED_DEPARTMENT"] != DBNull.Value) ? dttax.Rows[0]["IRS_ALLOWED_DEPARTMENT"].ToString() : "";
                    IRS_ALLOWED_DATE = (dttax.Rows[0]["IRS_ALLOWED_DATE"] != DBNull.Value) ? dttax.Rows[0]["IRS_ALLOWED_DATE"].ToString() : "";
                    IRS_ALLOWED_TYPE = (dttax.Rows[0]["IRS_ALLOWED_TYPE"] != DBNull.Value) ? dttax.Rows[0]["IRS_ALLOWED_TYPE"].ToString() : "";
                    IRS_ALLOWED_NO = (dttax.Rows[0]["IRS_ALLOWED_NO"] != DBNull.Value) ? dttax.Rows[0]["IRS_ALLOWED_NO"].ToString() : ""; 
                }
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
                // iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images") + "\\Title01.JPG");
            //    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(ChapPath) + "\\" + STORENO + ".JPG");
             //   jpg.SetAbsolutePosition(0, 0);
              //  jpg.ScalePercent(30f);
                //PdfPCell tc = new PdfPCell(jpg,true);
                PdfPCell tc = new PdfPCell(new Paragraph(""));//USER要求先拿掉圖片，欲進行套版比對。
                tc.Border = 0;
                tc.PaddingTop = 1;
                tc.PaddingLeft = 1;
                tbTitle.AddCell(CF(tc, "B", 0));

                //第2欄，標題
                PdfPTable tbTemp = new PdfPTable(1);
                Paragraph p1;
                //p1 = new Paragraph("遠傳電信股份有限公司台大公館門市", s14);
                p1 = new Paragraph(POS_RECEIPT_TITLE, s14);
                tc = new PdfPCell(p1);
                tc.FixedHeight = 27f;
                tbTemp.AddCell(CF(CF(tc, "H", Rectangle.ALIGN_CENTER), "B", 0));
                p1 = new Paragraph("電子計算機統一發票", s11);
                tc = new PdfPCell(p1);
                tbTemp.AddCell(CF(CF(tc, "H", Rectangle.ALIGN_CENTER), "B", 0));
                //p1 = new Paragraph("中華民國" + DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日", s09);
                //p1 = new Paragraph("中 華 民 國    年   月   日", s11);
                p1 = new Paragraph("中 華 民 國", s11);
                DateTime dINVOICE_DATE = Convert.ToDateTime(INVOICE_DATE);
                //p1.Add(dINVOICE_DATE.Year.ToString());
                p1.Add((dINVOICE_DATE.Year - 1911).ToString().PadLeft(4, ' '));
                p1.Add("年");
                p1.Add(dINVOICE_DATE.Month.ToString().PadLeft(4, ' '));
                p1.Add("　月");
                p1.Add(dINVOICE_DATE.Day.ToString().PadLeft(4, ' '));
                p1.Add("　日");
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
                p1 = new Paragraph("發票號碼:" + INVOICE_NO, s105);//左
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("", s105);//右
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("買 受 人:" + UNI_TITLE, s105);//左
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("", s105);//右
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("統一編號:" + UNI_NO, s105);//左
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("檢查號碼:" + checkcode + "      頁次:" + nowpage + "/" + totalpage, s105);//右
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("地    址:" + ADDRESS, s105);//左
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
                if (dtItem != null && dtItem.Rows.Count > 0)
                {
                    Paragraph p2 = new Paragraph("", s09);
                    Paragraph p3 = new Paragraph("", s09);
                    Paragraph p4 = new Paragraph("", s09);
                    for (int j = 0; j < dtItem.Rows.Count; j++)
                    {
                        DataRow _dr = dtItem.Rows[j];
                        string sPROD_NAME = (_dr["PROD_NAME"] != DBNull.Value) ? _dr["PROD_NAME"].ToString() : "";
                        string sQUANTITY = (_dr["QUANTITY"] != DBNull.Value) ? _dr["QUANTITY"].ToString() : "";
                        string sPRICE = (_dr["PRICE"] != DBNull.Value) ? _dr["PRICE"].ToString() : "";
                        string sAMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                        if (j == 0 && nowpage != "1")
                        {
                            p1.Add("        ＜承     上     頁＞");
                            p2.Add("");
                            p3.Add("");
                            p4.Add("");
                            p1.Add("\n");
                            p2.Add("\n");
                            p3.Add("\n");
                            p4.Add("\n");
                        }

                        if (j != 0)
                        {
                            p1.Add("\n");
                            p2.Add("\n");
                            p3.Add("\n");
                            p4.Add("\n");
                        }
                        p1.Add(sPROD_NAME);
                        p2.Add(sQUANTITY);
                        if (!string.IsNullOrEmpty(sPRICE))
                        {
                            double dTmp = Convert.ToDouble(sPRICE);
                            int iprice = Convert.ToInt32(Math.Round(dTmp / 1.05, 0, MidpointRounding.AwayFromZero));
                            sPRICE = StringUtil.NumberFormat(iprice, 0, true);
                        }
                        p3.Add(sPRICE);
                        if (!string.IsNullOrEmpty(sAMOUNT))
                        {
                            double dTmp = Convert.ToDouble(sAMOUNT);
                            sAMOUNT = StringUtil.NumberFormat(dTmp, 0, true);
                        }
                        p4.Add(sAMOUNT);
                    }
                    if (nowpage != totalpage)
                    {
                        p1.Add("\n");
                        p2.Add("\n");
                        p3.Add("\n");
                        p4.Add("\n");
                        p1.Add("        ＜續     下     頁＞");
                        p2.Add("");
                        p3.Add("");
                        p4.Add("");
                    }

                    tc = CF(new PdfPCell(p1), "HT", 127.56f);
                    tbInner02.AddCell(tc);
                    tc = new PdfPCell(p2);
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbInner02.AddCell(tc);
                    tc = new PdfPCell(p3);
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbInner02.AddCell(tc);
                    tc = new PdfPCell(p4);
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbInner02.AddCell(tc);
                }
                else
                {
                    tbInner02.AddCell(CF(new PdfPCell(p1), "HT", 127.56f));
                    tbInner02.AddCell(new PdfPCell(p1));
                    tbInner02.AddCell(new PdfPCell(p1));
                    tbInner02.AddCell(new PdfPCell(p1));
                }


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
               
                double dSALE_AMOUNT = Convert.ToDouble(SALE_AMOUNT);
                SALE_AMOUNT = StringUtil.NumberFormat(dSALE_AMOUNT, 0, true);
                if (nowpage != "1")
                {
                    SALE_AMOUNT = "";
                }
                p1 = new Paragraph(SALE_AMOUNT, s10);
                tc = CF(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_RIGHT), "V", Rectangle.ALIGN_CENTER);
                //tc.FixedHeight = 14.17f;
                tbInner04.AddCell(tc);//銷售額合計Value

                p1 = new Paragraph("營業稅", s09);
                tc = CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER);
                tc.FixedHeight = 14.17f;
                tbInner04.AddCell(tc);//營業稅Title
                p1 = new Paragraph("應稅", s09);
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//應稅Title
                if (TAX_TYPE == "1")
                {
                    p1 = new Paragraph("V", s09);
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//應稅value
                p1 = new Paragraph("零稅率", s09);
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//零稅率Title
                if (TAX_TYPE == "2")
                {
                    p1 = new Paragraph("V", s09);
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//零稅率Value
                p1 = new Paragraph("免稅", s09);
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//免稅Title
                if (TAX_TYPE == "3")
                {
                    p1 = new Paragraph("V", s09);
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }
                tbInner04.AddCell(p1);//免稅Value

                double dTAX = Convert.ToDouble(SALE_TAX);
                TAX = StringUtil.NumberFormat(dTAX, 0, true);
                if (nowpage != "1")
                {
                    TAX = "";
                }
                tc = new PdfPCell(new Paragraph(TAX, s09));
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tbInner04.AddCell(tc);//營業稅Value

                tc2 = new PdfPCell(new Paragraph("總                                             額", s10));
                tc2.Colspan = 7;
                tc2.FixedHeight = 14.17f;
                tbInner04.AddCell(CF(tc2, "H", Element.ALIGN_CENTER));//總計Title
                double dTOTAL_AMOUNT = Convert.ToDouble(INVOICE_TOTAL_AMOUNT);
                string sTOTAL_AMOUNT = StringUtil.NumberFormat(dTOTAL_AMOUNT, 0, true);
                if (nowpage != "1")
                {
                    sTOTAL_AMOUNT = "";
                }
                tc = new PdfPCell(new Paragraph(sTOTAL_AMOUNT, s09));
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tbInner04.AddCell(tc);//總計Value
                //        append to Outer Table
                tc = new PdfPCell(tbInner04);
                tc.BorderWidth = 1;
                tc.Colspan = 4;
                tbInner02.AddCell(tc);

                PdfPTable tTemp = new PdfPTable(new float[] { 1.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1.4f });
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
                if (nowpage != "1")
                {
                    INVOICE_TOTAL_AMOUNT = "0";
                }
                int iTOTAL_AMOUNT = Convert.ToInt32(INVOICE_TOTAL_AMOUNT);

                int amount = iTOTAL_AMOUNT / 10000000;
                string sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 10000000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　仟", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "佰", s09);

                amount = iTOTAL_AMOUNT / 1000000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 1000000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　佰", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "拾", s09);

                amount = iTOTAL_AMOUNT / 100000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 100000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　拾", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "萬", s09);

                amount = iTOTAL_AMOUNT / 10000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 10000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　萬", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "仟", s09);

                amount = iTOTAL_AMOUNT / 1000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 1000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　仟", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "佰", s09);

                amount = iTOTAL_AMOUNT / 100;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 100;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　佰", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "拾", s09);

                amount = iTOTAL_AMOUNT / 10;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 10;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　拾", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "元整", s09);

                sAmount = GF_Converts(iTOTAL_AMOUNT.ToString());
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　元整", s10));
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
                
                if (dtRemark != null && dtRemark.Rows.Count > 0)
                {
                    for (int j = 0; j < dtRemark.Rows.Count; j++)
                    {
                        //if (j != 0)
                        //{
                        //    p1.Add("\n");
                        //}
                        p1.Add(dtRemark.Rows[j][0].ToString().Replace(',', ' '));
                    }
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }


                tbInner03.AddCell(CF(new PdfPCell(p1), "HT", 90.71f));
                p1 = new Paragraph("營業人蓋用統一發票專用章", s10);
                tbInner03.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
                //jpg = iTextSharp.text.Image.GetInstance("F:\\Title02.JPG");

                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(ChapPath) + "\\" + STORENO + ".JPG");
                jpg.SetAbsolutePosition(0, 0);

                jpg.ScalePercent(7);

                tc = new PdfPCell(jpg, false);
                tc.PaddingTop = 1;
                tc.PaddingLeft = 20;
                tc.PaddingBottom = 1;
                tbInner03.AddCell(tc);

                //add to Outer Table
                tc = new PdfPCell(tbInner03);
                tc.BorderWidthTop = 0.5f;
                tc.BorderWidthRight = 0.5f;
                tc.BorderWidthBottom = 0.5f;
                tc.BorderWidthLeft = 0.5f;
                tbDetail.AddCell(tc);


                //add to Outer Table
                pdfTable.AddCell(tbDetail);

                //Footer
                PdfPTable tbFooter = new PdfPTable(1);
                //tbFooter.KeepTogether = true;
                p1 = new Paragraph("※應稅、零稅率、免稅之銷售額應分別開立統一發票，並應於各該欄打「v」。"
                                  + "                                                                 ", s07);
                p1.Add(new Chunk("第二聯:扣抵聯", s08));
                tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
                string[] YYMMDD = IRS_ALLOWED_DATE.Split('/');
                string YYYYYY = "";
                string MMMMMM = "";
                string DDDDDD = "";
                if (IRS_ALLOWED_DATE != "") 
                {
                    YYYYYY = YYMMDD[0].ToString();
                    MMMMMM = YYMMDD[1].ToString();
                    DDDDDD = YYMMDD[2].ToString();
                }
//                p1 = new Paragraph("本發票依    " + IRS_ALLOWED_DEPARTMENT + "  " + YYMMDD[0].ToString() + " 年" + YYMMDD[1].ToString() + " 月" + YYMMDD[2].ToString() + " 日" + IRS_ALLOWED_TYPE + " 字第" + IRS_ALLOWED_NO + " 號函核准使用。", s07);

                //p1.Add(new Chunk("\n※本公司若要作廢或更改者，應於發票次月五日前寄送本公司，逾期不受理。", s07));
                //p1.Add(new Chunk("\n※依台北市國稅局大安分局 年 月 日         字第        號函核准使用。", s07));
                p1 = new Paragraph("依" + IRS_ALLOWED_DEPARTMENT + " " + YYYYYY + "年 " + MMMMMM + "月 " + DDDDDD + "日 " + IRS_ALLOWED_TYPE + "字第 " + IRS_ALLOWED_NO + "號函核准使用。", s07);
                tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
                //p1.Add(new Chunk("\n買受人註記欄之註記方法：", s07));
                p1 = new Paragraph("買受人註記欄之註記方法：", s07);
                tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
                //p1.Add(new Chunk("\n營業人購進貨物或勞務應先按其用途區分為「進貨及費用」與「固定資產」，其進項稅額，除營業稅法第十九條第一項", s07));
                p1 = new Paragraph("營業人購進貨物或勞務應先按其用途區分為「進貨及費用」與「固定資產」，其進項稅額，除營業稅法第十九條第一項", s07);
                tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
                //p1.Add(new Chunk("\n屬不可扣抵外，其餘均得扣抵，並在各該適當欄內打「v」符號。", s07));
                p1 = new Paragraph("屬不可扣抵外，其餘均得扣抵，並在各該適當欄內打「v」符號。", s07);
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

        }

        /// <summary>
        /// 產生第二聯:扣抵聯(第二張)個人無統編
        /// </summary>
        /// <param name="pdfDoc">ITEXT.Document</param>
        private void add1NoUNI(Document pdfDoc, DataTable dtOrg, DataTable dtItem, DataTable dtRemark, string nowpage, string totalpage)
        {
            if (dtOrg.Rows.Count > 0)
            {
                DataRow drOrg = dtOrg.Rows[0];
                string STORENAME = (drOrg["STORENAME"] != DBNull.Value) ? drOrg["STORENAME"].ToString() : "";//店名
                string INVOICE_DATE = (drOrg["INVOICE_DATE"] != DBNull.Value) ? drOrg["INVOICE_DATE"].ToString() : "";//發票日
                string INVOICE_NO = (drOrg["INVOICE_NO"] != DBNull.Value) ? drOrg["INVOICE_NO"].ToString() : "";//發票號碼
                string BUYER = (drOrg["BUYER"] != DBNull.Value) ? drOrg["BUYER"].ToString() : "";//買受人
                string UNI_NO = (drOrg["UNI_NO"] != DBNull.Value) ? drOrg["UNI_NO"].ToString() : "";//統一編號
                string ADDRESS = (drOrg["ADDRESS"] != DBNull.Value) ? drOrg["ADDRESS"].ToString() : "";//地址
                string TAX_TYPE = (drOrg["TAX_TYPE"] != DBNull.Value) ? drOrg["TAX_TYPE"].ToString() : "";//營業稅種類
                string TOTAL_AMOUNT = (drOrg["TOTAL_AMOUNT"] != DBNull.Value) ? drOrg["TOTAL_AMOUNT"].ToString() : "0";//總額
                string TAX = (drOrg["TAX"] != DBNull.Value) ? drOrg["TAX"].ToString() : "0";//營業稅
                string SALE_AMOUNT = (drOrg["SALE_AMOUNT"] != DBNull.Value) ? drOrg["SALE_AMOUNT"].ToString() : "0";//銷售合計
                string STORENO = (drOrg[16] != DBNull.Value) ? drOrg[16].ToString() : "";//門市編號
                string N = INVOICE_NO.Substring(7, 1);
                string M = INVOICE_NO.Substring(9, 1);
                string NM = Convert.ToString((Convert.ToInt32(N) + Convert.ToInt32(M)) * 3);
                string checkcode = NM.Substring(NM.Length - 1, 1);
                string IRS_ALLOWED_DEPARTMENT = "";
                string IRS_ALLOWED_DATE = "";
                string IRS_ALLOWED_TYPE = "";
                string IRS_ALLOWED_NO = "";
                if (STORENO != "")
                {
                    DataTable dttax = getTaxstr(STORENO);
                    IRS_ALLOWED_DEPARTMENT = (dttax.Rows[0]["IRS_ALLOWED_DEPARTMENT"] != DBNull.Value) ? dttax.Rows[0]["IRS_ALLOWED_DEPARTMENT"].ToString() : "";
                    IRS_ALLOWED_DATE = (dttax.Rows[0]["IRS_ALLOWED_DATE"] != DBNull.Value) ? dttax.Rows[0]["IRS_ALLOWED_DATE"].ToString() : "";
                    IRS_ALLOWED_TYPE = (dttax.Rows[0]["IRS_ALLOWED_TYPE"] != DBNull.Value) ? dttax.Rows[0]["IRS_ALLOWED_TYPE"].ToString() : "";
                    IRS_ALLOWED_NO = (dttax.Rows[0]["IRS_ALLOWED_NO"] != DBNull.Value) ? dttax.Rows[0]["IRS_ALLOWED_NO"].ToString() : ""; 

                }

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
                // iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images") + "\\Title01.JPG");
              //  iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(ChapPath) + "\\" + STORENO + ".JPG");
              //  jpg.SetAbsolutePosition(0, 0);
              //  jpg.ScalePercent(30f);
                //PdfPCell tc = new PdfPCell(jpg,true);
                PdfPCell tc = new PdfPCell(new Paragraph(""));//USER要求先拿掉圖片，欲進行套版比對。
                tc.Border = 0;
                tc.PaddingTop = 1;
                tc.PaddingLeft = 1;
                tbTitle.AddCell(CF(tc, "B", 0));

                //第2欄，標題
                PdfPTable tbTemp = new PdfPTable(1);
                Paragraph p1;
                //p1 = new Paragraph("遠傳電信股份有限公司台大公館門市", s14);
                p1 = new Paragraph("遠傳電信股份有限公司" + STORENAME, s14);
                tc = new PdfPCell(p1);
                tc.FixedHeight = 27f;
                tbTemp.AddCell(CF(CF(tc, "H", Rectangle.ALIGN_CENTER), "B", 0));
                p1 = new Paragraph("電子計算機統一發票", s11);
                tc = new PdfPCell(p1);
                tbTemp.AddCell(CF(CF(tc, "H", Rectangle.ALIGN_CENTER), "B", 0));
                //p1 = new Paragraph("中華民國" + DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日", s09);
                //p1 = new Paragraph("中 華 民 國    年   月   日", s11);
                p1 = new Paragraph("中 華 民 國", s11);
                DateTime dINVOICE_DATE = Convert.ToDateTime(INVOICE_DATE);
                //p1.Add(dINVOICE_DATE.Year.ToString());
                p1.Add((dINVOICE_DATE.Year - 1911).ToString().PadLeft(4, ' '));
                p1.Add("年");
                p1.Add(dINVOICE_DATE.Month.ToString().PadLeft(4, ' '));
                p1.Add("　月");
                p1.Add(dINVOICE_DATE.Day.ToString().PadLeft(4, ' '));
                p1.Add("　日");
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
                p1 = new Paragraph("發票號碼:" + INVOICE_NO, s105);//左
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("", s105);//右
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("買 受 人:" + BUYER, s105);//左
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("", s105);//右
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("統一編號:" , s105);//左
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("檢查號碼:" + checkcode + "      頁次:" + nowpage + "/" + totalpage, s105);//右
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("地    址:" + ADDRESS, s105);//左
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
                if (dtItem != null && dtItem.Rows.Count > 0)
                {
                    Paragraph p2 = new Paragraph("", s09);
                    Paragraph p3 = new Paragraph("", s09);
                    Paragraph p4 = new Paragraph("", s09);
                    for (int j = 0; j < dtItem.Rows.Count; j++)
                    {
                        DataRow _dr = dtItem.Rows[j];
                        string sPROD_NAME = (_dr["PROD_NAME"] != DBNull.Value) ? _dr["PROD_NAME"].ToString() : "";
                        string sQUANTITY = (_dr["QUANTITY"] != DBNull.Value) ? _dr["QUANTITY"].ToString() : "";
                        string sPRICE = (_dr["PRICE"] != DBNull.Value) ? _dr["PRICE"].ToString() : "";
                        string sAMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                        if (j == 0 && nowpage != "1")
                        {
                            p1.Add("        ＜承     上     頁＞");
                            p2.Add("");
                            p3.Add("");
                            p4.Add("");
                            p1.Add("\n");
                            p2.Add("\n");
                            p3.Add("\n");
                            p4.Add("\n");
                        }

                        if (j != 0)
                        {
                            p1.Add("\n");
                            p2.Add("\n");
                            p3.Add("\n");
                            p4.Add("\n");
                        }
                        p1.Add(sPROD_NAME);
                        p2.Add(sQUANTITY);
                        if (!string.IsNullOrEmpty(sPRICE))
                        {
                            double dTmp = Convert.ToDouble(sPRICE);
                            sPRICE = StringUtil.NumberFormat(dTmp, 0, true);
                        }
                        p3.Add(sPRICE);
                        if (!string.IsNullOrEmpty(sAMOUNT))
                        {
                            double dTmp = Convert.ToDouble(sAMOUNT);
                            sAMOUNT = StringUtil.NumberFormat(dTmp, 0, true);
                        }
                        p4.Add(sAMOUNT);
                    }
                    if (nowpage != totalpage)
                    {
                        p1.Add("\n");
                        p2.Add("\n");
                        p3.Add("\n");
                        p4.Add("\n");
                        p1.Add("        ＜續     下     頁＞");
                        p2.Add("");
                        p3.Add("");
                        p4.Add("");
                    }
                    tc = CF(new PdfPCell(p1), "HT", 127.56f);
                    tbInner02.AddCell(tc);
                    tc = new PdfPCell(p2);
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbInner02.AddCell(tc);
                    tc = new PdfPCell(p3);
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbInner02.AddCell(tc);
                    tc = new PdfPCell(p4);
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbInner02.AddCell(tc);
                }
                else
                {
                    tbInner02.AddCell(CF(new PdfPCell(p1), "HT", 127.56f));
                    tbInner02.AddCell(new PdfPCell(p1));
                    tbInner02.AddCell(new PdfPCell(p1));
                    tbInner02.AddCell(new PdfPCell(p1));
                }


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
                double dSALE_AMOUNT = Convert.ToDouble(TOTAL_AMOUNT);
                SALE_AMOUNT = StringUtil.NumberFormat(dSALE_AMOUNT, 0, true);
                p1 = new Paragraph(SALE_AMOUNT, s10);
                tc = CF(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_RIGHT), "V", Rectangle.ALIGN_CENTER);
                //tc.FixedHeight = 14.17f;
                tbInner04.AddCell(tc);//銷售額合計Value

                p1 = new Paragraph("營業稅", s09);
                tc = CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER);
                tc.FixedHeight = 14.17f;
                tbInner04.AddCell(tc);//營業稅Title
                p1 = new Paragraph("應稅", s09);
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//應稅Title
                if (TAX_TYPE == "1")
                {
                    p1 = new Paragraph("V", s09);
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//應稅value
                p1 = new Paragraph("零稅率", s09);
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//零稅率Title
                if (TAX_TYPE == "2")
                {
                    p1 = new Paragraph("V", s09);
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//零稅率Value
                p1 = new Paragraph("免稅", s09);
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//免稅Title
                if (TAX_TYPE == "3")
                {
                    p1 = new Paragraph("V", s09);
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }
                tbInner04.AddCell(p1);//免稅Value
                double dTAX = Convert.ToDouble(TAX);
                TAX = StringUtil.NumberFormat(dTAX, 0, true);
                tc = new PdfPCell(new Paragraph("", s09));
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tbInner04.AddCell(tc);//營業稅Value

                tc2 = new PdfPCell(new Paragraph("總                                             額", s10));
                tc2.Colspan = 7;
                tc2.FixedHeight = 14.17f;
                tbInner04.AddCell(CF(tc2, "H", Element.ALIGN_CENTER));//總計Title
                double dTOTAL_AMOUNT = Convert.ToDouble(TOTAL_AMOUNT);
                string sTOTAL_AMOUNT = StringUtil.NumberFormat(dTOTAL_AMOUNT, 0, true);
                tc = new PdfPCell(new Paragraph(sTOTAL_AMOUNT, s09));
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tbInner04.AddCell(tc);//總計Value
                //        append to Outer Table
                tc = new PdfPCell(tbInner04);
                tc.BorderWidth = 1;
                tc.Colspan = 4;
                tbInner02.AddCell(tc);

                PdfPTable tTemp = new PdfPTable(new float[] { 1.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1.4f });
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

                int iTOTAL_AMOUNT = Convert.ToInt32(TOTAL_AMOUNT);
                int amount = iTOTAL_AMOUNT / 10000000;
                string sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 10000000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　仟", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "佰", s09);

                amount = iTOTAL_AMOUNT / 1000000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 1000000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　佰", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "拾", s09);

                amount = iTOTAL_AMOUNT / 100000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 100000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　拾", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "萬", s09);

                amount = iTOTAL_AMOUNT / 10000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 10000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　萬", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "仟", s09);

                amount = iTOTAL_AMOUNT / 1000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 1000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　仟", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "佰", s09);

                amount = iTOTAL_AMOUNT / 100;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 100;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　佰", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "拾", s09);

                amount = iTOTAL_AMOUNT / 10;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 10;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　拾", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "元整", s09);

                sAmount = GF_Converts(iTOTAL_AMOUNT.ToString());
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　元整", s10));
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
                if (dtRemark != null && dtRemark.Rows.Count > 0)
                {
                    for (int j = 0; j < dtRemark.Rows.Count; j++)
                    {
                        if (j != 0)
                        {
                            p1.Add("\n");
                        }
                        p1.Add(dtRemark.Rows[j][0].ToString());
                    }
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }


                tbInner03.AddCell(CF(new PdfPCell(p1), "HT", 90.71f));
                p1 = new Paragraph("營業人蓋用統一發票專用章", s10);
                tbInner03.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
                //jpg = iTextSharp.text.Image.GetInstance("F:\\Title02.JPG");

                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(ChapPath) + "\\" + STORENO + ".JPG");
                jpg.SetAbsolutePosition(0, 0);
                jpg.ScaleAbsolute(100f, 100f);

                jpg.ScalePercent(60f);

                tc = new PdfPCell(jpg, false);
                tc.PaddingTop = 1;
                tc.PaddingLeft = 15;
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
                //tbFooter.KeepTogether = true;
                p1 = new Paragraph("※應稅、零稅率、免稅之銷售額應分別開立統一發票，並應於各該欄打「v」。"
                                  + "                                                                 ", s07);
                p1.Add(new Chunk("第二聯:扣抵聯", s08));
                tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
                string[] YYMMDD = IRS_ALLOWED_DATE.Split('/');

                //                p1 = new Paragraph("本發票依    " + IRS_ALLOWED_DEPARTMENT + "  " + YYMMDD[0].ToString() + " 年" + YYMMDD[1].ToString() + " 月" + YYMMDD[2].ToString() + " 日" + IRS_ALLOWED_TYPE + " 字第" + IRS_ALLOWED_NO + " 號函核准使用。", s07);

                //p1.Add(new Chunk("\n※本公司若要作廢或更改者，應於發票次月五日前寄送本公司，逾期不受理。", s07));
                //p1.Add(new Chunk("\n※依台北市國稅局大安分局 年 月 日         字第        號函核准使用。", s07));
                p1 = new Paragraph("依" + IRS_ALLOWED_DEPARTMENT + " " + YYMMDD[0].ToString() + "年 " + YYMMDD[1].ToString() + "月 " + YYMMDD[2].ToString() + "日 " + IRS_ALLOWED_TYPE + "字第 " + IRS_ALLOWED_NO + "號函核准使用。", s07);

                tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
                //p1.Add(new Chunk("\n買受人註記欄之註記方法：", s07));
                p1 = new Paragraph("買受人註記欄之註記方法：", s07);
                tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
                //p1.Add(new Chunk("\n營業人購進貨物或勞務應先按其用途區分為「進貨及費用」與「固定資產」，其進項稅額，除營業稅法第十九條第一項", s07));
                p1 = new Paragraph("營業人購進貨物或勞務應先按其用途區分為「進貨及費用」與「固定資產」，其進項稅額，除營業稅法第十九條第一項", s07);
                tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
                //p1.Add(new Chunk("\n屬不可扣抵外，其餘均得扣抵，並在各該適當欄內打「v」符號。", s07));
                p1 = new Paragraph("屬不可扣抵外，其餘均得扣抵，並在各該適當欄內打「v」符號。", s07);
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

        }

        /// <summary>
        /// 產生第二聯:扣抵聯(第二張)
        /// </summary>
        /// <param name="pdfDoc">ITEXT.Document</param>
        private void add1_old(Document pdfDoc, DataTable dtOrg, DataTable dtItem, DataTable dtRemark)
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
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images") + "\\Title01.JPG");
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
            p1 = new Paragraph("遠傳電信股份有限公司台大公館門市", s14);
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
            PdfPTable tTemp = new PdfPTable(new float[] { 1.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1.4f });
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
            p1.Add(new Chunk("　仟", s10));
            tc = new PdfPCell(p1);
            tc.PaddingTop = fPadding;
            tc.HorizontalAlignment = Element.ALIGN_RIGHT;
            tc.BorderWidthLeft = 0;
            tc.BorderWidthRight = 0;
            tc.BorderWidthBottom = 0;
            tTemp.AddCell(tc);
            //p1 = new Paragraph("一二三" + "佰", s09);
            p1 = new Paragraph(new Chunk("", s10));
            p1.Add(new Chunk("　佰", s10));
            tc = new PdfPCell(p1);
            tc.PaddingTop = fPadding;
            tc.HorizontalAlignment = Element.ALIGN_RIGHT;
            tc.BorderWidthLeft = 0;
            tc.BorderWidthRight = 0;
            tc.BorderWidthBottom = 0;
            tTemp.AddCell(tc);
            //p1 = new Paragraph("一二三" + "拾", s09);
            p1 = new Paragraph(new Chunk("", s10));
            p1.Add(new Chunk("　拾", s10));
            tc = new PdfPCell(p1);
            tc.PaddingTop = fPadding;
            tc.HorizontalAlignment = Element.ALIGN_RIGHT;
            tc.BorderWidthLeft = 0;
            tc.BorderWidthRight = 0;
            tc.BorderWidthBottom = 0;
            tTemp.AddCell(tc);
            //p1 = new Paragraph("一二三" + "萬", s09);
            p1 = new Paragraph(new Chunk("", s10));
            p1.Add(new Chunk("　萬", s10));
            tc = new PdfPCell(p1);
            tc.PaddingTop = fPadding;
            tc.HorizontalAlignment = Element.ALIGN_RIGHT;
            tc.BorderWidthLeft = 0;
            tc.BorderWidthRight = 0;
            tc.BorderWidthBottom = 0;
            tTemp.AddCell(tc);
            //p1 = new Paragraph("一二三" + "仟", s09);
            p1 = new Paragraph(new Chunk("", s07));
            p1.Add(new Chunk("　仟", s10));
            tc = new PdfPCell(p1);
            tc.PaddingTop = fPadding;
            tc.HorizontalAlignment = Element.ALIGN_RIGHT;
            tc.BorderWidthLeft = 0;
            tc.BorderWidthRight = 0;
            tc.BorderWidthBottom = 0;
            tTemp.AddCell(tc);
            //p1 = new Paragraph("一二三" + "佰", s09);
            p1 = new Paragraph(new Chunk("", s10));
            p1.Add(new Chunk("　佰", s10));
            tc = new PdfPCell(p1);
            tc.PaddingTop = fPadding;
            tc.HorizontalAlignment = Element.ALIGN_RIGHT;
            tc.BorderWidthLeft = 0;
            tc.BorderWidthRight = 0;
            tc.BorderWidthBottom = 0;
            tTemp.AddCell(tc);
            //p1 = new Paragraph("一二三" + "拾", s09);
            p1 = new Paragraph(new Chunk("", s07));
            p1.Add(new Chunk("　拾", s10));
            tc = new PdfPCell(p1);
            tc.PaddingTop = fPadding;
            tc.HorizontalAlignment = Element.ALIGN_RIGHT;
            tc.BorderWidthLeft = 0;
            tc.BorderWidthRight = 0;
            tc.BorderWidthBottom = 0;
            tTemp.AddCell(tc);
            //p1 = new Paragraph("一二三" + "元整", s09);
            p1 = new Paragraph(new Chunk("", s10));
            p1.Add(new Chunk("　元整", s10));
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
            jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images") + "\\Title02.JPG");
            jpg.SetAbsolutePosition(0, 0);

            jpg.ScalePercent(60f);

            tc = new PdfPCell(jpg, false);
            tc.PaddingTop = 1;
            tc.PaddingLeft = 15;
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
                              + "                                                                 ", s07);
            p1.Add(new Chunk("第二聯:扣抵聯", s08));
            tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));

            //p1.Add(new Chunk("\n※本公司若要作廢或更改者，應於發票次月五日前寄送本公司，逾期不受理。", s07));
            //p1.Add(new Chunk("\n※依台北市國稅局大安分局 年 月 日         字第        號函核准使用。", s07));
            p1 = new Paragraph("依台北市國稅局大安分局 年 月 日         字第        號函核准使用。", s07);
            tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
            //p1.Add(new Chunk("\n買受人註記欄之註記方法：", s07));
            p1 = new Paragraph("買受人註記欄之註記方法：", s07);
            tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
            //p1.Add(new Chunk("\n營業人購進貨物或勞務應先按其用途區分為「進貨及費用」與「固定資產」，其進項稅額，除營業稅法第十九條第一項", s07));
            p1 = new Paragraph("營業人購進貨物或勞務應先按其用途區分為「進貨及費用」與「固定資產」，其進項稅額，除營業稅法第十九條第一項", s07);
            tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
            //p1.Add(new Chunk("\n屬不可扣抵外，其餘均得扣抵，並在各該適當欄內打「v」符號。", s07));
            p1 = new Paragraph("屬不可扣抵外，其餘均得扣抵，並在各該適當欄內打「v」符號。", s07);
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

        private void add3(Document pdfDoc, DataTable dtOrg, DataTable dtItem, DataTable dtRemark, string nowpage, string totalpage)
        {
            if (dtOrg.Rows.Count > 0)
            {
                DataRow drOrg = dtOrg.Rows[0];
                string STORENAME = (drOrg["STORENAME"] != DBNull.Value) ? drOrg["STORENAME"].ToString() : "";//店名
                string INVOICE_DATE = (drOrg["INVOICE_DATE"] != DBNull.Value) ? drOrg["INVOICE_DATE"].ToString() : "";//發票日
                string INVOICE_NO = (drOrg["INVOICE_NO"] != DBNull.Value) ? drOrg["INVOICE_NO"].ToString() : "";//發票號碼
                string BUYER = (drOrg["BUYER"] != DBNull.Value) ? drOrg["BUYER"].ToString() : "";//買受人
                string UNI_NO = (drOrg["UNI_NO"] != DBNull.Value) ? drOrg["UNI_NO"].ToString() : "";//統一編號
                string ADDRESS = (drOrg["ADDRESS"] != DBNull.Value) ? drOrg["ADDRESS"].ToString() : "";//地址
                string TAX_TYPE = (drOrg["TAX_TYPE"] != DBNull.Value) ? drOrg["TAX_TYPE"].ToString() : "";//營業稅種類
                string TOTAL_AMOUNT = (drOrg["TOTAL_AMOUNT"] != DBNull.Value) ? drOrg["TOTAL_AMOUNT"].ToString() : "0";//總額
                string TAX = (drOrg["TAX"] != DBNull.Value) ? drOrg["TAX"].ToString() : "0";//營業稅
                string SALE_AMOUNT = (drOrg["SALE_AMOUNT"] != DBNull.Value) ? drOrg["SALE_AMOUNT"].ToString() : "0";//銷售合計
                string I_AMOUNT = (drOrg["IAMOUNT"] != DBNull.Value) ? drOrg["IAMOUNT"].ToString() : "0";//單價
                string STORENO = (drOrg[16] != DBNull.Value) ? drOrg[16].ToString() : "0";//門市編號
                string UNI_TITLE = (drOrg[17] != DBNull.Value) ? drOrg[17].ToString() : "";//發票抬頭
                string POS_RECEIPT_TITLE = (drOrg["POS_RECEIPT_TITLE"] != DBNull.Value) ? drOrg["POS_RECEIPT_TITLE"].ToString() : "";//發票抬頭
                string N = INVOICE_NO.Substring(7, 1);
                string M = INVOICE_NO.Substring(9, 1);
                string NM = Convert.ToString((Convert.ToInt32(N) + Convert.ToInt32(M)) * 3);
                string checkcode = NM.Substring(NM.Length - 1, 1);
                string IRS_ALLOWED_DEPARTMENT = "";
                string IRS_ALLOWED_DATE = "";
                string IRS_ALLOWED_TYPE = "";
                string IRS_ALLOWED_NO = "";
                string INVOICE_TOTAL_AMOUNT = (dtOrg.Rows[0]["TOTAL_AMOUNT"] != DBNull.Value) ? dtOrg.Rows[0]["TOTAL_AMOUNT"].ToString() : "0";
                string BEFORE_TAX_AMOUNT = (dtOrg.Rows[0]["BEFORE_TAX"] != DBNull.Value) ? dtOrg.Rows[0]["BEFORE_TAX"].ToString() : "0";
                string SALE_TAX = Convert.ToString(Convert.ToInt32(INVOICE_TOTAL_AMOUNT) - Convert.ToInt32(BEFORE_TAX_AMOUNT));
                if (STORENO != "")
                {
                    DataTable dttax = getTaxstr(STORENO);
                    IRS_ALLOWED_DEPARTMENT = (dttax.Rows[0]["IRS_ALLOWED_DEPARTMENT"] != DBNull.Value) ? dttax.Rows[0]["IRS_ALLOWED_DEPARTMENT"].ToString() : "";
                    IRS_ALLOWED_DATE = (dttax.Rows[0]["IRS_ALLOWED_DATE"] != DBNull.Value) ? dttax.Rows[0]["IRS_ALLOWED_DATE"].ToString() : "";
                    IRS_ALLOWED_TYPE = (dttax.Rows[0]["IRS_ALLOWED_TYPE"] != DBNull.Value) ? dttax.Rows[0]["IRS_ALLOWED_TYPE"].ToString() : "";
                    IRS_ALLOWED_NO = (dttax.Rows[0]["IRS_ALLOWED_NO"] != DBNull.Value) ? dttax.Rows[0]["IRS_ALLOWED_NO"].ToString() : ""; 

                }
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
                //   iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images") + "\\Title01.JPG");
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(ChapPath) + "\\" + STORENO + ".JPG");
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
                //p1 = new Paragraph("遠傳電信股份有限公司台大公館門市", s14);
                p1 = new Paragraph(POS_RECEIPT_TITLE, s14);
                tc = new PdfPCell(p1);
                tc.FixedHeight = 27f;
                tbTemp.AddCell(CF(CF(tc, "H", Rectangle.ALIGN_CENTER), "B", 0));
                p1 = new Paragraph("電子計算機統一發票", s11);
                tc = new PdfPCell(p1);
                tbTemp.AddCell(CF(CF(tc, "H", Rectangle.ALIGN_CENTER), "B", 0));
                //p1 = new Paragraph("中華民國" + DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日", s09);
                //p1 = new Paragraph("中 華 民 國    年   月   日", s11);
                p1 = new Paragraph("中 華 民 國", s11);
                DateTime dINVOICE_DATE = Convert.ToDateTime(INVOICE_DATE);
                //p1.Add(dINVOICE_DATE.Year.ToString());
                p1.Add((dINVOICE_DATE.Year - 1911).ToString().PadLeft(4, ' '));
                p1.Add("年");
                p1.Add(dINVOICE_DATE.Month.ToString().PadLeft(4, ' '));
                p1.Add("　月");
                p1.Add(dINVOICE_DATE.Day.ToString().PadLeft(4, ' '));
                p1.Add("　日");
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
                p1 = new Paragraph("發票號碼:" + INVOICE_NO, s105);//左
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("", s105);//右
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("買 受 人:" + UNI_TITLE, s105);//左
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("", s105);//右
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("統一編號:" + UNI_NO, s105);//左
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("檢查號碼:" + checkcode + "      頁次:" + nowpage + "/" + totalpage, s105);//右
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("地    址:" + ADDRESS, s105);//左
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
                if (dtItem != null && dtItem.Rows.Count > 0)
                {
                    Paragraph p2 = new Paragraph("", s09);
                    Paragraph p3 = new Paragraph("", s09);
                    Paragraph p4 = new Paragraph("", s09);
                    for (int j = 0; j < dtItem.Rows.Count; j++)
                    {
                        DataRow _dr = dtItem.Rows[j];
                        string sPROD_NAME = (_dr["PROD_NAME"] != DBNull.Value) ? _dr["PROD_NAME"].ToString() : "";
                        string sQUANTITY = (_dr["QUANTITY"] != DBNull.Value) ? _dr["QUANTITY"].ToString() : "";
                        string sPRICE = (_dr["PRICE"] != DBNull.Value) ? _dr["PRICE"].ToString() : "";
                        string sAMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                        if (j == 0 && nowpage != "1")
                        {
                            p1.Add("        ＜承     上     頁＞");
                            p2.Add("");
                            p3.Add("");
                            p4.Add("");
                            p1.Add("\n");
                            p2.Add("\n");
                            p3.Add("\n");
                            p4.Add("\n");
                        }
                        if (j != 0)
                        {
                            p1.Add("\n");
                            p2.Add("\n");
                            p3.Add("\n");
                            p4.Add("\n");
                        }
                        p1.Add(sPROD_NAME);
                        p2.Add(sQUANTITY);
                        if (!string.IsNullOrEmpty(sPRICE))
                        {
                            double dTmp = Convert.ToDouble(sPRICE);
                            int iprice = Convert.ToInt32(Math.Round(dTmp / 1.05, 0, MidpointRounding.AwayFromZero));
                            sPRICE = StringUtil.NumberFormat(iprice, 0, true);
                        }
                        p3.Add(sPRICE);
                        if (!string.IsNullOrEmpty(sAMOUNT))
                        {
                            double dTmp = Convert.ToDouble(sAMOUNT);
                            sAMOUNT = StringUtil.NumberFormat(dTmp, 0, true);
                        }
                        p4.Add(sAMOUNT);
                    }
                    if (nowpage != totalpage)
                    {
                        p1.Add("\n");
                        p2.Add("\n");
                        p3.Add("\n");
                        p4.Add("\n");
                        p1.Add("        ＜續     下     頁＞");
                        p2.Add("");
                        p3.Add("");
                        p4.Add("");
                    }
                    tc = CF(new PdfPCell(p1), "HT", 127.56f);
                    tbInner02.AddCell(tc);
                    tc = new PdfPCell(p2);
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbInner02.AddCell(tc);
                    tc = new PdfPCell(p3);
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbInner02.AddCell(tc);
                    tc = new PdfPCell(p4);
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbInner02.AddCell(tc);
                }
                else
                {
                    tbInner02.AddCell(CF(new PdfPCell(p1), "HT", 127.56f));
                    tbInner02.AddCell(new PdfPCell(p1));
                    tbInner02.AddCell(new PdfPCell(p1));
                    tbInner02.AddCell(new PdfPCell(p1));
                }


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
                double dSALE_AMOUNT = Convert.ToDouble(SALE_AMOUNT);
                SALE_AMOUNT = StringUtil.NumberFormat(dSALE_AMOUNT, 0, true);
                if (nowpage != "1")
                {
                    SALE_AMOUNT = "";
                }
                p1 = new Paragraph(SALE_AMOUNT, s10);
                tc = CF(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_RIGHT), "V", Rectangle.ALIGN_CENTER);
                //tc.FixedHeight = 14.17f;
                tbInner04.AddCell(tc);//銷售額合計Value

                p1 = new Paragraph("營業稅", s09);
                tc = CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER);
                tc.FixedHeight = 14.17f;
                tbInner04.AddCell(tc);//營業稅Title
                p1 = new Paragraph("應稅", s09);
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//應稅Title
                if (TAX_TYPE == "1")
                {
                    p1 = new Paragraph("V", s09);
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//應稅value
                p1 = new Paragraph("零稅率", s09);
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//零稅率Title
                if (TAX_TYPE == "2")
                {
                    p1 = new Paragraph("V", s09);
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//零稅率Value
                p1 = new Paragraph("免稅", s09);
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//免稅Title
                if (TAX_TYPE == "3")
                {
                    p1 = new Paragraph("V", s09);
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }
                tbInner04.AddCell(p1);//免稅Value
                double dTAX = Convert.ToDouble(SALE_TAX);
                TAX = StringUtil.NumberFormat(dTAX, 0, true);
                if (nowpage != "1")
                {
                    TAX = "";
                }
                tc = new PdfPCell(new Paragraph(TAX, s09));
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tbInner04.AddCell(tc);//營業稅Value

                tc2 = new PdfPCell(new Paragraph("總                                             額", s10));
                tc2.Colspan = 7;
                tc2.FixedHeight = 14.17f;
                tbInner04.AddCell(CF(tc2, "H", Element.ALIGN_CENTER));//總計Title
                double dTOTAL_AMOUNT = Convert.ToDouble(INVOICE_TOTAL_AMOUNT);
                string sTOTAL_AMOUNT = StringUtil.NumberFormat(dTOTAL_AMOUNT, 0, true);
                if (nowpage != "1")
                {
                    sTOTAL_AMOUNT = "";
                }
                tc = new PdfPCell(new Paragraph(sTOTAL_AMOUNT, s09));
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tbInner04.AddCell(tc);//總計Value
                //        append to Outer Table
                tc = new PdfPCell(tbInner04);
                tc.BorderWidth = 1;
                tc.Colspan = 4;
                tbInner02.AddCell(tc);

                PdfPTable tTemp = new PdfPTable(new float[] { 1.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1.4f });
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
                if (nowpage != "1")
                {
                    INVOICE_TOTAL_AMOUNT = "0";
                }
                int iTOTAL_AMOUNT = Convert.ToInt32(INVOICE_TOTAL_AMOUNT);
               
                int amount = iTOTAL_AMOUNT / 10000000;
                string sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 10000000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　仟", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "佰", s09);

                amount = iTOTAL_AMOUNT / 1000000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 1000000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　佰", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "拾", s09);

                amount = iTOTAL_AMOUNT / 100000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 100000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　拾", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "萬", s09);

                amount = iTOTAL_AMOUNT / 10000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 10000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　萬", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "仟", s09);

                amount = iTOTAL_AMOUNT / 1000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 1000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　仟", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "佰", s09);

                amount = iTOTAL_AMOUNT / 100;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 100;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　佰", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "拾", s09);

                amount = iTOTAL_AMOUNT / 10;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 10;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　拾", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "元整", s09);

                sAmount = GF_Converts(iTOTAL_AMOUNT.ToString());
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　元整", s10));
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
                if (dtRemark != null && dtRemark.Rows.Count > 0)
                {
                    for (int j = 0; j < dtRemark.Rows.Count; j++)
                    {
                        //if (j != 0) { p1.Add("\n"); }
                        p1.Add(dtRemark.Rows[j][0].ToString().Replace(',', ' '));
                    }
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }


                tbInner03.AddCell(CF(new PdfPCell(p1), "HT", 90.71f));
                p1 = new Paragraph("營業人蓋用統一發票專用章", s10);
                tbInner03.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
                //jpg = iTextSharp.text.Image.GetInstance("F:\\Title02.JPG");
                //  jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images") + "\\Title02.JPG");
                jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(ChapPath) + "\\" + STORENO + ".JPG");
                jpg.SetAbsolutePosition(0, 0);

                jpg.ScalePercent(7);

                tc = new PdfPCell(jpg, false);
                tc.PaddingTop = 1;
                tc.PaddingLeft = 20;
                tc.PaddingBottom = 1;
                tbInner03.AddCell(tc);

                //add to Outer Table
                tc = new PdfPCell(tbInner03);
                tc.BorderWidthTop = 0.8f;
                tc.BorderWidthRight = 0.8f;
                tc.BorderWidthBottom = 0.8f;
                tc.BorderWidthLeft = 0.8f;
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
                string[] YYMMDD = IRS_ALLOWED_DATE.Split('/');
                string YYYYYY = "";
                string MMMMMM = "";
                string DDDDDD = "";
                if (IRS_ALLOWED_DATE != "") 
                {
                    YYYYYY = YYMMDD[0].ToString();
                    MMMMMM = YYMMDD[1].ToString();
                    DDDDDD = YYMMDD[2].ToString();
                }
                p1 = new Paragraph("依" + IRS_ALLOWED_DEPARTMENT + " " + YYYYYY + "年 " + MMMMMM + "月 " + DDDDDD + "日 " + IRS_ALLOWED_TYPE + "字第 " + IRS_ALLOWED_NO + "號函核准使用。", s07);

                tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("買受人註記欄之註記方法：", s07);
                tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("營業人購進貨物或勞務應先按其用途區分為「進貨及費用」與「固定資產」，其進項稅額，除營業稅法第十九條第一項", s07);
                tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("屬不可扣抵外，其餘均得扣抵，並在各該適當欄內打「v」符號。", s07);
                tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));

                pdfTable.AddCell(CF(new PdfPCell(tbFooter), "B", 0));
                pdfDoc.Add(pdfTable);
            }
        }

        private void add3NoUNI(Document pdfDoc, DataTable dtOrg, DataTable dtItem, DataTable dtRemark, string nowpage, string totalpage)
        {
            if (dtOrg.Rows.Count > 0)
            {
                DataRow drOrg = dtOrg.Rows[0];
                string STORENAME = (drOrg["STORENAME"] != DBNull.Value) ? drOrg["STORENAME"].ToString() : "";//店名
                string INVOICE_DATE = (drOrg["INVOICE_DATE"] != DBNull.Value) ? drOrg["INVOICE_DATE"].ToString() : "";//發票日
                string INVOICE_NO = (drOrg["INVOICE_NO"] != DBNull.Value) ? drOrg["INVOICE_NO"].ToString() : "";//發票號碼
                string BUYER = (drOrg["BUYER"] != DBNull.Value) ? drOrg["BUYER"].ToString() : "";//買受人
                string UNI_NO = (drOrg["UNI_NO"] != DBNull.Value) ? drOrg["UNI_NO"].ToString() : "";//統一編號
                string ADDRESS = (drOrg["ADDRESS"] != DBNull.Value) ? drOrg["ADDRESS"].ToString() : "";//地址
                string TAX_TYPE = (drOrg["TAX_TYPE"] != DBNull.Value) ? drOrg["TAX_TYPE"].ToString() : "";//營業稅種類
                string TOTAL_AMOUNT = (drOrg["TOTAL_AMOUNT"] != DBNull.Value) ? drOrg["TOTAL_AMOUNT"].ToString() : "0";//總額
                string TAX = (drOrg["TAX"] != DBNull.Value) ? drOrg["TAX"].ToString() : "0";//營業稅
                string SALE_AMOUNT = (drOrg["SALE_AMOUNT"] != DBNull.Value) ? drOrg["SALE_AMOUNT"].ToString() : "0";//銷售合計
                string STORENO = (drOrg[16] != DBNull.Value) ? drOrg[16].ToString() : "";//門市編號
                string POS_RECEIPT_TITLE = (drOrg["POS_RECEIPT_TITLE"] != DBNull.Value) ? drOrg["POS_RECEIPT_TITLE"].ToString() : "";//發票抬頭
                string N = INVOICE_NO.Substring(7, 1);
                string M = INVOICE_NO.Substring(9, 1);
                string NM = Convert.ToString((Convert.ToInt32(N) + Convert.ToInt32(M)) * 3);
                string checkcode = NM.Substring(NM.Length - 1, 1);
                string IRS_ALLOWED_DEPARTMENT = "";
                string IRS_ALLOWED_DATE = "";
                string IRS_ALLOWED_TYPE = "";
                string IRS_ALLOWED_NO = "";
                if (STORENO != "")
                {
                    DataTable dttax = getTaxstr(STORENO);
                    IRS_ALLOWED_DEPARTMENT = (dttax.Rows[0]["IRS_ALLOWED_DEPARTMENT"] != DBNull.Value) ? dttax.Rows[0]["IRS_ALLOWED_DEPARTMENT"].ToString() : "";
                    IRS_ALLOWED_DATE = (dttax.Rows[0]["IRS_ALLOWED_DATE"] != DBNull.Value) ? dttax.Rows[0]["IRS_ALLOWED_DATE"].ToString() : "";
                    IRS_ALLOWED_TYPE = (dttax.Rows[0]["IRS_ALLOWED_TYPE"] != DBNull.Value) ? dttax.Rows[0]["IRS_ALLOWED_TYPE"].ToString() : "";
                    IRS_ALLOWED_NO = (dttax.Rows[0]["IRS_ALLOWED_NO"] != DBNull.Value) ? dttax.Rows[0]["IRS_ALLOWED_NO"].ToString() : "";

                }
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
                //   iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images") + "\\Title01.JPG");
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(ChapPath) + "\\" + STORENO + ".JPG");
                //  jpg.SetAbsolutePosition(0, 0);
                //  jpg.ScalePercent(30f);
                //PdfPCell tc = new PdfPCell(jpg,true);
                PdfPCell tc = new PdfPCell(new Paragraph(""));//USER要求先拿掉圖片，欲進行套版比對。
                tc.Border = 0;
                tc.PaddingTop = 1;
                tc.PaddingLeft = 1;
                tbTitle.AddCell(CF(tc, "B", 0));

                //第2欄，標題
                PdfPTable tbTemp = new PdfPTable(1);
                Paragraph p1;
                //p1 = new Paragraph("遠傳電信股份有限公司台大公館門市", s14);
                p1 = new Paragraph(POS_RECEIPT_TITLE, s14);
                tc = new PdfPCell(p1);
                tc.FixedHeight = 27f;
                tbTemp.AddCell(CF(CF(tc, "H", Rectangle.ALIGN_CENTER), "B", 0));
                p1 = new Paragraph("電子計算機統一發票", s11);
                tc = new PdfPCell(p1);
                tbTemp.AddCell(CF(CF(tc, "H", Rectangle.ALIGN_CENTER), "B", 0));
                //p1 = new Paragraph("中華民國" + DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日", s09);
                //p1 = new Paragraph("中 華 民 國    年   月   日", s11);
                p1 = new Paragraph("中 華 民 國", s11);
                DateTime dINVOICE_DATE = Convert.ToDateTime(INVOICE_DATE);
                //p1.Add(dINVOICE_DATE.Year.ToString());
                p1.Add((dINVOICE_DATE.Year - 1911).ToString().PadLeft(4, ' '));
                p1.Add("年");
                p1.Add(dINVOICE_DATE.Month.ToString().PadLeft(4, ' '));
                p1.Add("　月");
                p1.Add(dINVOICE_DATE.Day.ToString().PadLeft(4, ' '));
                p1.Add("　日");
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
                p1 = new Paragraph("發票號碼:" + INVOICE_NO, s105);//左
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("", s105);//右
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("買 受 人:" + BUYER, s105);//左
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("", s105);//右
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("統一編號:", s105);//左
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("檢查號碼:" + checkcode + "      頁次:" + nowpage + "/" + totalpage, s105);//右
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("地    址:" + ADDRESS, s105);//左
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
                if (dtItem != null && dtItem.Rows.Count > 0)
                {
                    Paragraph p2 = new Paragraph("", s09);
                    Paragraph p3 = new Paragraph("", s09);
                    Paragraph p4 = new Paragraph("", s09);
                    for (int j = 0; j < dtItem.Rows.Count; j++)
                    {
                        DataRow _dr = dtItem.Rows[j];
                        string sPROD_NAME = (_dr["PROD_NAME"] != DBNull.Value) ? _dr["PROD_NAME"].ToString() : "";
                        string sQUANTITY = (_dr["QUANTITY"] != DBNull.Value) ? _dr["QUANTITY"].ToString() : "";
                        string sPRICE = (_dr["PRICE"] != DBNull.Value) ? _dr["PRICE"].ToString() : "";
                        string sAMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                        if (j == 0 && nowpage != "1")
                        {
                            p1.Add("        ＜承     上     頁＞");
                            p2.Add("");
                            p3.Add("");
                            p4.Add("");
                            p1.Add("\n");
                            p2.Add("\n");
                            p3.Add("\n");
                            p4.Add("\n");
                        }
                        if (j != 0)
                        {
                            p1.Add("\n");
                            p2.Add("\n");
                            p3.Add("\n");
                            p4.Add("\n");
                        }
                        p1.Add(sPROD_NAME);
                        p2.Add(sQUANTITY);
                        if (!string.IsNullOrEmpty(sPRICE))
                        {
                            double dTmp = Convert.ToDouble(sPRICE);
                      //      int iprice = Convert.ToInt32(Math.Round(dTmp / 1.05, 0, MidpointRounding.AwayFromZero));
                            sPRICE = StringUtil.NumberFormat(dTmp, 0, true);
                        }
                        p3.Add(sPRICE);
                        if (!string.IsNullOrEmpty(sAMOUNT))
                        {
                            double dTmp = Convert.ToDouble(sAMOUNT);
                            sAMOUNT = StringUtil.NumberFormat(dTmp, 0, true);
                        }
                        p4.Add(sAMOUNT);
                    }
                    if (nowpage != totalpage)
                    {
                        p1.Add("\n");
                        p2.Add("\n");
                        p3.Add("\n");
                        p4.Add("\n");
                        p1.Add("        ＜續     下     頁＞");
                        p2.Add("");
                        p3.Add("");
                        p4.Add("");
                    }
                    tc = CF(new PdfPCell(p1), "HT", 127.56f);
                    tbInner02.AddCell(tc);
                    tc = new PdfPCell(p2);
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbInner02.AddCell(tc);
                    tc = new PdfPCell(p3);
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbInner02.AddCell(tc);
                    tc = new PdfPCell(p4);
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbInner02.AddCell(tc);
                }
                else
                {
                    tbInner02.AddCell(CF(new PdfPCell(p1), "HT", 127.56f));
                    tbInner02.AddCell(new PdfPCell(p1));
                    tbInner02.AddCell(new PdfPCell(p1));
                    tbInner02.AddCell(new PdfPCell(p1));
                }


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
                double dSALE_AMOUNT = Convert.ToDouble(TOTAL_AMOUNT);
                SALE_AMOUNT = StringUtil.NumberFormat(dSALE_AMOUNT, 0, true);
                if (nowpage != "1")
                {
                    SALE_AMOUNT = "";
                }
                p1 = new Paragraph(SALE_AMOUNT, s10);
                tc = CF(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_RIGHT), "V", Rectangle.ALIGN_CENTER);
                //tc.FixedHeight = 14.17f;
                tbInner04.AddCell(tc);//銷售額合計Value

                p1 = new Paragraph("營業稅", s09);
                tc = CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER);
                tc.FixedHeight = 14.17f;
                tbInner04.AddCell(tc);//營業稅Title
                p1 = new Paragraph("應稅", s09);
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//應稅Title
                if (TAX_TYPE == "1")
                {
                    p1 = new Paragraph("V", s09);
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//應稅value
                p1 = new Paragraph("零稅率", s09);
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//零稅率Title
                if (TAX_TYPE == "2")
                {
                    p1 = new Paragraph("V", s09);
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//零稅率Value
                p1 = new Paragraph("免稅", s09);
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//免稅Title
                if (TAX_TYPE == "3")
                {
                    p1 = new Paragraph("V", s09);
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }
                tbInner04.AddCell(p1);//免稅Value
                double dTAX = Convert.ToDouble(TAX);
                TAX = StringUtil.NumberFormat(dTAX, 0, true);
                if (nowpage != "1")
                {
                    TAX = "";
                }
                tc = new PdfPCell(new Paragraph("", s09));
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tbInner04.AddCell(tc);//營業稅Value

                tc2 = new PdfPCell(new Paragraph("總                                             額", s10));
                tc2.Colspan = 7;
                tc2.FixedHeight = 14.17f;
                tbInner04.AddCell(CF(tc2, "H", Element.ALIGN_CENTER));//總計Title
                double dTOTAL_AMOUNT = Convert.ToDouble(TOTAL_AMOUNT);
                string sTOTAL_AMOUNT = StringUtil.NumberFormat(dTOTAL_AMOUNT, 0, true);
                if (nowpage != "1")
                {
                    sTOTAL_AMOUNT = "";
                }
                tc = new PdfPCell(new Paragraph(sTOTAL_AMOUNT, s09));
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tbInner04.AddCell(tc);//總計Value
                //        append to Outer Table
                tc = new PdfPCell(tbInner04);
                tc.BorderWidth = 1;
                tc.Colspan = 4;
                tbInner02.AddCell(tc);

                PdfPTable tTemp = new PdfPTable(new float[] { 1.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1.4f });
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
                if (nowpage != "1")
                {
                    TOTAL_AMOUNT = "0";
                }
                int iTOTAL_AMOUNT = Convert.ToInt32(TOTAL_AMOUNT);
                int amount = iTOTAL_AMOUNT / 10000000;
                string sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 10000000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　仟", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "佰", s09);

                amount = iTOTAL_AMOUNT / 1000000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 1000000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　佰", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "拾", s09);

                amount = iTOTAL_AMOUNT / 100000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 100000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　拾", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "萬", s09);

                amount = iTOTAL_AMOUNT / 10000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 10000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　萬", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "仟", s09);

                amount = iTOTAL_AMOUNT / 1000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 1000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　仟", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "佰", s09);

                amount = iTOTAL_AMOUNT / 100;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 100;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　佰", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "拾", s09);

                amount = iTOTAL_AMOUNT / 10;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 10;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　拾", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "元整", s09);

                sAmount = GF_Converts(iTOTAL_AMOUNT.ToString());
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　元整", s10));
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
                if (dtRemark != null && dtRemark.Rows.Count > 0)
                {
                    for (int j = 0; j < dtRemark.Rows.Count; j++)
                    {
                        //if (j != 0) { p1.Add("\n"); }
                        p1.Add(dtRemark.Rows[j][0].ToString().Replace(',', ' '));
                    }
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }


                tbInner03.AddCell(CF(new PdfPCell(p1), "HT", 90.71f));
                p1 = new Paragraph("營業人蓋用統一發票專用章", s10);
                tbInner03.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
                //jpg = iTextSharp.text.Image.GetInstance("F:\\Title02.JPG");
                //  jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images") + "\\Title02.JPG");
                jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(ChapPath) + "\\" + STORENO + ".jpg");
                jpg.SetAbsolutePosition(0, 0);
                float x1 = jpg.Height;
                float y1 = jpg.Width;
                int JpgH = Convert.ToInt32(x1);
                int reallyH = 14100 / JpgH;
                jpg.ScalePercent(7);
                //           jpg.ScaledHeight = 100;

                tc = new PdfPCell(jpg, false);
                //tc.FixedHeight = 30;
                // tc.Width = 30;
                tc.PaddingTop = 1;
                tc.PaddingLeft = 20;
                tc.PaddingBottom = 1;
                tbInner03.AddCell(tc);

                //add to Outer Table
                tc = new PdfPCell(tbInner03);
                tc.BorderWidthTop = 0.8f;
                tc.BorderWidthRight = 0.8f;
                tc.BorderWidthBottom = 0.8f;
                tc.BorderWidthLeft = 0.8f;
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

                string[] YYMMDD = IRS_ALLOWED_DATE.Split('/');
                string YYYYYY = "";
                string MMMMMM = "";
                string DDDDDD = "";
                if (IRS_ALLOWED_DATE != "") 
                {
                    YYYYYY = YYMMDD[0].ToString();
                    MMMMMM = YYMMDD[1].ToString();
                    DDDDDD = YYMMDD[2].ToString();
                }
                p1 = new Paragraph("依" + IRS_ALLOWED_DEPARTMENT + " " + YYYYYY + "年 " + MMMMMM + "月 " + DDDDDD + "日 " + IRS_ALLOWED_TYPE + "字第 " + IRS_ALLOWED_NO + "號函核准使用。", s07);

                tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("買受人註記欄之註記方法：", s07);
                tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("營業人購進貨物或勞務應先按其用途區分為「進貨及費用」與「固定資產」，其進項稅額，除營業稅法第十九條第一項", s07);
                tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("屬不可扣抵外，其餘均得扣抵，並在各該適當欄內打「v」符號。", s07);
                tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));

                pdfTable.AddCell(CF(new PdfPCell(tbFooter), "B", 0));
                pdfDoc.Add(pdfTable);
            }
        }

        /// <summary>
        /// 產生第三聯:收執聯(第二張)
        /// </summary>
        /// <param name="pdfDoc">ITEXT.Document</param>
        private void add3_old(Document pdfDoc, DataTable dtOrg, DataTable dtItem, DataTable dtRemark)
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
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images") + "\\Title01.JPG");
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
            p1 = new Paragraph("遠傳電信股份有限公司台大公館門市", s14);
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
            PdfPTable tTemp = new PdfPTable(new float[] { 1.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1.4f });
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
            p1.Add(new Chunk("　仟", s10));
            tc = new PdfPCell(p1);
            tc.PaddingTop = fPadding;
            tc.HorizontalAlignment = Element.ALIGN_RIGHT;
            tc.BorderWidthLeft = 0;
            tc.BorderWidthRight = 0;
            tc.BorderWidthBottom = 0;
            tTemp.AddCell(tc);
            //p1 = new Paragraph("一二三" + "佰", s09);
            p1 = new Paragraph(new Chunk("", s10));
            p1.Add(new Chunk("　佰", s10));
            tc = new PdfPCell(p1);
            tc.PaddingTop = fPadding;
            tc.HorizontalAlignment = Element.ALIGN_RIGHT;
            tc.BorderWidthLeft = 0;
            tc.BorderWidthRight = 0;
            tc.BorderWidthBottom = 0;
            tTemp.AddCell(tc);
            //p1 = new Paragraph("一二三" + "拾", s09);
            p1 = new Paragraph(new Chunk("", s10));
            p1.Add(new Chunk("　拾", s10));
            tc = new PdfPCell(p1);
            tc.PaddingTop = fPadding;
            tc.HorizontalAlignment = Element.ALIGN_RIGHT;
            tc.BorderWidthLeft = 0;
            tc.BorderWidthRight = 0;
            tc.BorderWidthBottom = 0;
            tTemp.AddCell(tc);
            //p1 = new Paragraph("一二三" + "萬", s09);
            p1 = new Paragraph(new Chunk("", s10));
            p1.Add(new Chunk("　萬", s10));
            tc = new PdfPCell(p1);
            tc.PaddingTop = fPadding;
            tc.HorizontalAlignment = Element.ALIGN_RIGHT;
            tc.BorderWidthLeft = 0;
            tc.BorderWidthRight = 0;
            tc.BorderWidthBottom = 0;
            tTemp.AddCell(tc);
            //p1 = new Paragraph("一二三" + "仟", s09);
            p1 = new Paragraph(new Chunk("", s07));
            p1.Add(new Chunk("　仟", s10));
            tc = new PdfPCell(p1);
            tc.PaddingTop = fPadding;
            tc.HorizontalAlignment = Element.ALIGN_RIGHT;
            tc.BorderWidthLeft = 0;
            tc.BorderWidthRight = 0;
            tc.BorderWidthBottom = 0;
            tTemp.AddCell(tc);
            //p1 = new Paragraph("一二三" + "佰", s09);
            p1 = new Paragraph(new Chunk("", s10));
            p1.Add(new Chunk("　佰", s10));
            tc = new PdfPCell(p1);
            tc.PaddingTop = fPadding;
            tc.HorizontalAlignment = Element.ALIGN_RIGHT;
            tc.BorderWidthLeft = 0;
            tc.BorderWidthRight = 0;
            tc.BorderWidthBottom = 0;
            tTemp.AddCell(tc);
            //p1 = new Paragraph("一二三" + "拾", s09);
            p1 = new Paragraph(new Chunk("", s07));
            p1.Add(new Chunk("　拾", s10));
            tc = new PdfPCell(p1);
            tc.PaddingTop = fPadding;
            tc.HorizontalAlignment = Element.ALIGN_RIGHT;
            tc.BorderWidthLeft = 0;
            tc.BorderWidthRight = 0;
            tc.BorderWidthBottom = 0;
            tTemp.AddCell(tc);
            //p1 = new Paragraph("一二三" + "元整", s09);
            p1 = new Paragraph(new Chunk("", s10));
            p1.Add(new Chunk("　元整", s10));
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
            jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images") + "\\Title02.JPG");
            jpg.SetAbsolutePosition(0, 0);

            jpg.ScalePercent(60f);

            tc = new PdfPCell(jpg, false);
            tc.PaddingTop = 1;
            tc.PaddingLeft = 15;
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
            p1 = new Paragraph("依台北市國稅局大安分局 年 月 日         字第        號函核准使用。", s07);
            tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
            p1 = new Paragraph("買受人註記欄之註記方法：", s07);
            tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
            p1 = new Paragraph("營業人購進貨物或勞務應先按其用途區分為「進貨及費用」與「固定資產」，其進項稅額，除營業稅法第十九條第一項", s07);
            tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));
            p1 = new Paragraph("屬不可扣抵外，其餘均得扣抵，並在各該適當欄內打「v」符號。", s07);
            tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));

            pdfTable.AddCell(CF(new PdfPCell(tbFooter), "B", 0));
            pdfDoc.Add(pdfTable);

            //打上註記欄圖片
            //jpg = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images") + "\\物件-2.gif");
            //jpg.SetAbsolutePosition(455, 340);
            //jpg.ScaleToFit(70, 40);

            //pdfDoc.Add(jpg);
        }

        /// <summary>
        /// 產生扣抵聯註記欄
        /// </summary>
        /// <param name="pdfDoc">ITEXT.Document</param>
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

        private void addMarkTable1(Document pdfDoc, string repeat)
        {
            PdfPTable pdfTable = new PdfPTable(3);
            //pdfTable.DefaultCell.Border = 0;
            //pdfTable.SetTotalWidth(new float[] { 31.18f, 51.02f, 48.19f });
            pdfTable.SetTotalWidth(new float[] { 15.016f, 15.016f, 15.016f });
            pdfTable.LockedWidth = true;
            pdfTable.SpacingBefore = 0;
            pdfTable.SpacingAfter = 0;
            Paragraph p1;
            PdfPCell tc;
            //第1列
            p1 = new Paragraph("與正本相符", s09);
            tc = new PdfPCell(p1);
            tc.HorizontalAlignment = Element.ALIGN_LEFT;
            tc.PaddingLeft = 0;
            tc.PaddingRight = 0;
            tc.PaddingTop = 0;
            tc.FixedHeight = 12.756f;
            tc.Colspan = 3;
            pdfTable.AddCell(tc);
            ////第2列
            //p1 = new Paragraph("區    分", s06);
            //tc = new PdfPCell(p1);
            //tc.FixedHeight = 12.756f;
            //tc.PaddingLeft = 0;
            //tc.PaddingRight = 0;
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            //p1 = new Paragraph("進貨及費用", s06);
            //tc = new PdfPCell(p1);
            //tc.PaddingLeft = 0;
            //tc.PaddingRight = 0;
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            //p1 = new Paragraph("固定資產", s06);
            //tc = new PdfPCell(p1);
            //tc.PaddingLeft = 0;
            //tc.PaddingRight = 0;
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);

            ////第3列
            //p1 = new Paragraph("得 抵 扣", s06);
            //tc = new PdfPCell(p1);
            //tc.FixedHeight = 14.173f;
            //tc.PaddingLeft = 0;
            //tc.PaddingRight = 0;
            //tc.PaddingTop = 3f;
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            //p1 = new Paragraph("", s06);
            //tc = new PdfPCell(p1);
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            //p1 = new Paragraph("", s06);
            //tc = new PdfPCell(p1);
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);

            ////第4列
            //p1 = new Paragraph("不得抵扣", s06);
            //tc = new PdfPCell(p1);
            //tc.FixedHeight = 11.339f;
            //tc.PaddingLeft = 0;
            //tc.PaddingRight = 0;
            //tc.PaddingTop = 1;
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            //p1 = new Paragraph("", s06);
            //tc = new PdfPCell(p1);
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            //p1 = new Paragraph("", s06);
            //tc = new PdfPCell(p1);
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            pdfTable.CompleteRow();

            pdfTable.WriteSelectedRows(0, 4, 458.5f, 800, writer.DirectContent);
            //pdfDoc.Add(pdfTable);
        }

        private void addMarkTable3(Document pdfDoc, string repeat)
        {
            PdfPTable pdfTable = new PdfPTable(3);
            //pdfTable.DefaultCell.Border = 0;
            //pdfTable.SetTotalWidth(new float[] { 31.18f, 51.02f, 48.19f });
            pdfTable.SetTotalWidth(new float[] { 15.016f, 15.016f, 15.016f });
            pdfTable.LockedWidth = true;
            pdfTable.SpacingBefore = 0;
            pdfTable.SpacingAfter = 0;
            Paragraph p1;
            PdfPCell tc;
            //第1列
            p1 = new Paragraph("與正本相符", s09);
            tc = new PdfPCell(p1);
            tc.HorizontalAlignment = Element.ALIGN_LEFT;
            tc.PaddingLeft = 0;
            tc.PaddingRight = 0;
            tc.PaddingTop = 0;
            tc.FixedHeight = 12.756f;
            tc.Colspan = 3;
            pdfTable.AddCell(tc);
            ////第2列
            //p1 = new Paragraph("區    分", s06);
            //tc = new PdfPCell(p1);
            //tc.FixedHeight = 12.756f;
            //tc.PaddingLeft = 0;
            //tc.PaddingRight = 0;
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            //p1 = new Paragraph("進貨及費用", s06);
            //tc = new PdfPCell(p1);
            //tc.PaddingLeft = 0;
            //tc.PaddingRight = 0;
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            //p1 = new Paragraph("固定資產", s06);
            //tc = new PdfPCell(p1);
            //tc.PaddingLeft = 0;
            //tc.PaddingRight = 0;
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);

            ////第3列
            //p1 = new Paragraph("得 抵 扣", s06);
            //tc = new PdfPCell(p1);
            //tc.FixedHeight = 14.173f;
            //tc.PaddingLeft = 0;
            //tc.PaddingRight = 0;
            //tc.PaddingTop = 3f;
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            //p1 = new Paragraph("", s06);
            //tc = new PdfPCell(p1);
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            //p1 = new Paragraph("", s06);
            //tc = new PdfPCell(p1);
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);

            ////第4列
            //p1 = new Paragraph("不得抵扣", s06);
            //tc = new PdfPCell(p1);
            //tc.FixedHeight = 11.339f;
            //tc.PaddingLeft = 0;
            //tc.PaddingRight = 0;
            //tc.PaddingTop = 1;
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            //p1 = new Paragraph("", s06);
            //tc = new PdfPCell(p1);
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            //p1 = new Paragraph("", s06);
            //tc = new PdfPCell(p1);
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            pdfTable.CompleteRow();

            pdfTable.WriteSelectedRows(0, 4, 458.5f, 800, writer.DirectContent);
            //pdfDoc.Add(pdfTable);
        }

        /// <summary>
        /// 產生收執聯註記欄
        /// </summary>
        /// <param name="pdfDoc">ITEXT.Document</param>
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

        private void addMarkTable2(Document pdfDoc, string repeat)
        {
            PdfPTable pdfTable = new PdfPTable(3);
            //pdfTable.DefaultCell.Border = 0;
            //pdfTable.SetTotalWidth(new float[] { 31.18f,51.02f,48.19f });
            pdfTable.SetTotalWidth(new float[] { 15.016f, 15.016f, 15.016f });
            pdfTable.LockedWidth = true;
            pdfTable.SpacingBefore = 0;
            pdfTable.SpacingAfter = 0;
            Paragraph p1;
            PdfPCell tc;
            //第1列
            p1 = new Paragraph("與正本相符", s09);
            tc = new PdfPCell(p1);
            tc.HorizontalAlignment = Element.ALIGN_LEFT;
            tc.PaddingLeft = 0;
            tc.PaddingRight = 0;
            tc.PaddingTop = 0;
            tc.FixedHeight = 12.756f;
            tc.Colspan = 3;
            pdfTable.AddCell(tc);
            ////第2列
            //p1 = new Paragraph("區    分", s06);
            //tc = new PdfPCell(p1);
            //tc.FixedHeight = 12.756f;
            //tc.PaddingLeft = 0;
            //tc.PaddingRight = 0;
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            //p1 = new Paragraph("進貨及費用", s06);
            //tc = new PdfPCell(p1);
            //tc.PaddingLeft = 0;
            //tc.PaddingRight = 0;
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            //p1 = new Paragraph("固定資產", s06);
            //tc = new PdfPCell(p1);
            //tc.PaddingLeft = 0;
            //tc.PaddingRight = 0;
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);

            ////第3列
            //p1 = new Paragraph("得 抵 扣", s06);
            //tc = new PdfPCell(p1);
            //tc.FixedHeight = 14.173f;
            //tc.PaddingTop = 3f;
            //tc.PaddingLeft = 0;
            //tc.PaddingRight = 0;
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            //p1 = new Paragraph("", s06);
            //tc = new PdfPCell(p1);
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            //p1 = new Paragraph("", s06);
            //tc = new PdfPCell(p1);
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);

            ////第4列
            //p1 = new Paragraph("不得抵扣", s06);
            //tc = new PdfPCell(p1);
            //tc.FixedHeight = 11.339f;
            //tc.PaddingLeft = 0;
            //tc.PaddingRight = 0;
            //tc.PaddingTop = 1;
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            //p1 = new Paragraph("", s06);
            //tc = new PdfPCell(p1);
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            //p1 = new Paragraph("", s06);
            //tc = new PdfPCell(p1);
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            pdfTable.CompleteRow();
            pdfTable.WriteSelectedRows(0, 4, 458.5f, 375, writer.DirectContent);
            pdfTable.WriteSelectedRows(0, 4, 458.5f, 800, writer.DirectContent);
          //  PdfPTable pdfTable1 = new PdfPTable(3);
            //pdfTable.DefaultCell.Border = 0;
            //pdfTable.SetTotalWidth(new float[] { 31.18f,51.02f,48.19f });
         //   pdfTable1.SetTotalWidth(new float[] { 15.016f, 15.016f, 15.016f });
         //   pdfTable1.LockedWidth = true;
         //   pdfTable1.SpacingBefore = 0;
         //   pdfTable1.SpacingAfter = 0;

            //pdfDoc.Add(pdfTable);
        }
        private void addMarkTable4(Document pdfDoc, string repeat)
        {
            PdfPTable pdfTable = new PdfPTable(3);
            //pdfTable.DefaultCell.Border = 0;
            //pdfTable.SetTotalWidth(new float[] { 31.18f,51.02f,48.19f });
            pdfTable.SetTotalWidth(new float[] { 15.016f, 15.016f, 15.016f });
            pdfTable.LockedWidth = true;
            pdfTable.SpacingBefore = 0;
            pdfTable.SpacingAfter = 0;
            Paragraph p1;
            PdfPCell tc;
            //第1列
            p1 = new Paragraph("與正本相符", s09);
            tc = new PdfPCell(p1);
            tc.HorizontalAlignment = Element.ALIGN_LEFT;
            tc.PaddingLeft = 0;
            tc.PaddingRight = 0;
            tc.PaddingTop = 0;
            tc.FixedHeight = 12.756f;
            tc.Colspan = 3;
            pdfTable.AddCell(tc);
            ////第2列
            //p1 = new Paragraph("區    分", s06);
            //tc = new PdfPCell(p1);
            //tc.FixedHeight = 12.756f;
            //tc.PaddingLeft = 0;
            //tc.PaddingRight = 0;
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            //p1 = new Paragraph("進貨及費用", s06);
            //tc = new PdfPCell(p1);
            //tc.PaddingLeft = 0;
            //tc.PaddingRight = 0;
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            //p1 = new Paragraph("固定資產", s06);
            //tc = new PdfPCell(p1);
            //tc.PaddingLeft = 0;
            //tc.PaddingRight = 0;
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);

            ////第3列
            //p1 = new Paragraph("得 抵 扣", s06);
            //tc = new PdfPCell(p1);
            //tc.FixedHeight = 14.173f;
            //tc.PaddingTop = 3f;
            //tc.PaddingLeft = 0;
            //tc.PaddingRight = 0;
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            //p1 = new Paragraph("", s06);
            //tc = new PdfPCell(p1);
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            //p1 = new Paragraph("", s06);
            //tc = new PdfPCell(p1);
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);

            ////第4列
            //p1 = new Paragraph("不得抵扣", s06);
            //tc = new PdfPCell(p1);
            //tc.FixedHeight = 11.339f;
            //tc.PaddingLeft = 0;
            //tc.PaddingRight = 0;
            //tc.PaddingTop = 1;
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            //p1 = new Paragraph("", s06);
            //tc = new PdfPCell(p1);
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            //p1 = new Paragraph("", s06);
            //tc = new PdfPCell(p1);
            //tc.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfTable.AddCell(tc);
            pdfTable.CompleteRow();
            pdfTable.WriteSelectedRows(0, 4, 458.5f, 375, writer.DirectContent);
         //   pdfTable.WriteSelectedRows(0, 4, 458.5f, 800, writer.DirectContent);
            //  PdfPTable pdfTable1 = new PdfPTable(3);
            //pdfTable.DefaultCell.Border = 0;
            //pdfTable.SetTotalWidth(new float[] { 31.18f,51.02f,48.19f });
            //   pdfTable1.SetTotalWidth(new float[] { 15.016f, 15.016f, 15.016f });
            //   pdfTable1.LockedWidth = true;
            //   pdfTable1.SpacingBefore = 0;
            //   pdfTable1.SpacingAfter = 0;

            //pdfDoc.Add(pdfTable);
        }
        /// <summary>
        /// 設定PdfPCell Style
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="sStyle"></param>
        /// <param name="oValue"></param>
        /// <returns></returns>
        private PdfPCell CF(PdfPCell dc, string sStyle, object oValue)
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


        private int getBytes(string str)
        {
            return System.Text.Encoding.Default.GetBytes(str).Length;
        }
        //*** 轉換一位數 
        private string GF_Converts(string NumStr)
        {
            object functionReturnValue = null;

            switch (StringUtil.CInt(NumStr))
            {
                case 0:
                    functionReturnValue = "零";
                    break;
                case 1:
                    functionReturnValue = "壹";
                    break;
                case 2:
                    functionReturnValue = "貳";
                    break;
                case 3:
                    functionReturnValue = "参";
                    break;
                case 4:
                    functionReturnValue = "肆";
                    break;
                case 5:
                    functionReturnValue = "伍";
                    break;
                case 6:
                    functionReturnValue = "陸";
                    break;
                case 7:
                    functionReturnValue = "柒";
                    break;
                case 8:
                    functionReturnValue = "捌";
                    break;
                case 9:
                    functionReturnValue = "玖";
                    break;
            }
            return functionReturnValue.ToString();
        }
        void checkDirectory(string sPath)
        {
            if (!string.IsNullOrEmpty(sPath))
            {
                //sPath = sPath.Replace('/', '\\');
                string[] arrDir = sPath.Split('/');
                string sCheckPath = "";
                string sRealPath = "";
                for (int i = 0; i < arrDir.Length; i++)
                {
                    sCheckPath += arrDir[i] + "/";
                    sRealPath = HttpContext.Current.Server.MapPath(sCheckPath);
                    if (!Directory.Exists(sRealPath)) Directory.CreateDirectory(sRealPath);
                    //if(Directory.Exists(HttpContext.Current.Server.MapPath(
                }
            }
        }


        private void addBarcode(Document pdfDoc, DataTable dtOrg, DataTable dtItem, DataTable dtRemark)
        {
            if (dtOrg.Rows.Count > 0)
            {
                DataRow drOrg = dtOrg.Rows[0];
                string STORENAME = (drOrg["STORENAME"] != DBNull.Value) ? drOrg["STORENAME"].ToString() : "";//店名
                string INVOICE_DATE = (drOrg["INVOICE_DATE"] != DBNull.Value) ? drOrg["INVOICE_DATE"].ToString() : "";//發票日
                string INVOICE_NO = (drOrg["INVOICE_NO"] != DBNull.Value) ? drOrg["INVOICE_NO"].ToString() : "";//發票號碼
                string BUYER = (drOrg["BUYER"] != DBNull.Value) ? drOrg["BUYER"].ToString() : "";//買受人
                string UNI_NO = (drOrg["UNI_NO"] != DBNull.Value) ? drOrg["UNI_NO"].ToString() : "";//統一編號
                string ADDRESS = (drOrg["ADDRESS"] != DBNull.Value) ? drOrg["ADDRESS"].ToString() : "";//地址
                string TAX_TYPE = (drOrg["TAX_TYPE"] != DBNull.Value) ? drOrg["TAX_TYPE"].ToString() : "";//營業稅種類
                string TOTAL_AMOUNT = (drOrg["TOTAL_AMOUNT"] != DBNull.Value) ? drOrg["TOTAL_AMOUNT"].ToString() : "0";//總額
                string TAX = (drOrg["TAX"] != DBNull.Value) ? drOrg["TAX"].ToString() : "0";//營業稅
                string SALE_AMOUNT = (drOrg["SALE_AMOUNT"] != DBNull.Value) ? drOrg["SALE_AMOUNT"].ToString() : "0";//銷售合計

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
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images") + "\\Title01.JPG");
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
                //p1 = new Paragraph("遠傳電信股份有限公司台大公館門市", s14);
                p1 = new Paragraph("遠傳電信股份有限公司" + STORENAME, s14);
                tc = new PdfPCell(p1);
                tc.FixedHeight = 27f;
                tbTemp.AddCell(CF(CF(tc, "H", Rectangle.ALIGN_CENTER), "B", 0));
                p1 = new Paragraph("電子計算機統一發票", s11);
                tc = new PdfPCell(p1);
                tbTemp.AddCell(CF(CF(tc, "H", Rectangle.ALIGN_CENTER), "B", 0));
                //p1 = new Paragraph("中華民國" + DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日", s09);
                //p1 = new Paragraph("中 華 民 國    年   月   日", s11);
                p1 = new Paragraph("中 華 民 國", s11);
                DateTime dINVOICE_DATE = Convert.ToDateTime(INVOICE_DATE);
                //p1.Add(dINVOICE_DATE.Year.ToString());
                p1.Add((dINVOICE_DATE.Year - 1911).ToString().PadLeft(4, ' '));
                p1.Add("年");
                p1.Add(dINVOICE_DATE.Month.ToString().PadLeft(4, ' '));
                p1.Add("　月");
                p1.Add(dINVOICE_DATE.Day.ToString().PadLeft(4, ' '));
                p1.Add("　日");
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
                p1 = new Paragraph("發票號碼:" + INVOICE_NO, s105);//左
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("", s105);//右
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("買 受 人:" + BUYER, s105);//左
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("", s105);//右
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("統一編號:" + UNI_NO, s105);//左
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("檢查號碼:", s105);//右
                tbMaster.AddCell(CF(new PdfPCell(p1), "B", 0));
                p1 = new Paragraph("地    址:" + ADDRESS, s105);//左
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
                if (dtItem != null && dtItem.Rows.Count > 0)
                {
                    Paragraph p2 = new Paragraph("", s09);
                    Paragraph p3 = new Paragraph("", s09);
                    Paragraph p4 = new Paragraph("", s09);
                    for (int j = 0; j < dtItem.Rows.Count; j++)
                    {
                        DataRow _dr = dtItem.Rows[j];
                        string sPROD_NAME = (_dr["PROD_NAME"] != DBNull.Value) ? _dr["PROD_NAME"].ToString() : "";
                        string sQUANTITY = (_dr["QUANTITY"] != DBNull.Value) ? _dr["QUANTITY"].ToString() : "";
                        string sPRICE = (_dr["PRICE"] != DBNull.Value) ? _dr["PRICE"].ToString() : "";
                        string sAMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                        if (j != 0)
                        {
                            p1.Add("\n");
                            p2.Add("\n");
                            p3.Add("\n");
                            p4.Add("\n");
                        }


                        p1.Add(sPROD_NAME);
                        p2.Add(sQUANTITY);

                        if (!string.IsNullOrEmpty(sPRICE))
                        {
                            double dTmp = Convert.ToDouble(sPRICE);
                            sPRICE = StringUtil.NumberFormat(dTmp, 0, true);
                        }
                        p3.Add(sPRICE);
                        if (!string.IsNullOrEmpty(sAMOUNT))
                        {
                            double dTmp = Convert.ToDouble(sAMOUNT);
                            sAMOUNT = StringUtil.NumberFormat(dTmp, 0, true);
                        }
                        p4.Add(sAMOUNT);
                    }
                    tc = CF(new PdfPCell(p1), "HT", 127.56f);
                    tbInner02.AddCell(tc);
                    tc = new PdfPCell(p2);
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbInner02.AddCell(tc);
                    tc = new PdfPCell(p3);
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbInner02.AddCell(tc);
                    tc = new PdfPCell(p4);
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbInner02.AddCell(tc);
                }
                else
                {
                    tbInner02.AddCell(CF(new PdfPCell(p1), "HT", 127.56f));
                    tbInner02.AddCell(new PdfPCell(p1));
                    tbInner02.AddCell(new PdfPCell(p1));
                    tbInner02.AddCell(new PdfPCell(p1));
                }


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
                double dSALE_AMOUNT = Convert.ToDouble(SALE_AMOUNT);
                SALE_AMOUNT = StringUtil.NumberFormat(dSALE_AMOUNT, 0, true);
                p1 = new Paragraph(SALE_AMOUNT, s10);
                tc = CF(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_RIGHT), "V", Rectangle.ALIGN_MIDDLE);
                //tc.FixedHeight = 14.17f;
                tbInner04.AddCell(tc);//銷售額合計Value

                p1 = new Paragraph("營業稅", s09);
                tc = CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER);
                tc.FixedHeight = 14.17f;
                tbInner04.AddCell(tc);//營業稅Title
                p1 = new Paragraph("應稅", s09);
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//應稅Title
                if (TAX_TYPE == "1")
                {
                    p1 = new Paragraph("V", s09);
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//應稅value
                p1 = new Paragraph("零稅率", s09);
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//零稅率Title
                if (TAX_TYPE == "2")
                {
                    p1 = new Paragraph("V", s09);
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//零稅率Value
                p1 = new Paragraph("免稅", s09);
                tbInner04.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));//免稅Title
                if (TAX_TYPE == "3")
                {
                    p1 = new Paragraph("V", s09);
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }
                tbInner04.AddCell(p1);//免稅Value
                double dTAX = Convert.ToDouble(TAX);
                TAX = StringUtil.NumberFormat(dTAX, 0, true);
                tc = new PdfPCell(new Paragraph(TAX, s09));
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tbInner04.AddCell(tc);//營業稅Value

                tc2 = new PdfPCell(new Paragraph("總                                             額", s10));
                tc2.Colspan = 7;
                tc2.FixedHeight = 14.17f;
                tbInner04.AddCell(CF(tc2, "H", Element.ALIGN_CENTER));//總計Title
                double dTOTAL_AMOUNT = Convert.ToDouble(TOTAL_AMOUNT);
                string sTOTAL_AMOUNT = StringUtil.NumberFormat(dTOTAL_AMOUNT, 0, true);
                tc = new PdfPCell(new Paragraph(sTOTAL_AMOUNT, s09));
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tbInner04.AddCell(tc);//總計Value
                //        append to Outer Table
                tc = new PdfPCell(tbInner04);
                tc.BorderWidth = 1;
                tc.Colspan = 4;
                tbInner02.AddCell(tc);

                PdfPTable tTemp = new PdfPTable(new float[] { 1.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1.4f });
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

                int iTOTAL_AMOUNT = Convert.ToInt32(TOTAL_AMOUNT);
                int amount = iTOTAL_AMOUNT / 10000000;
                string sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 10000000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　仟", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "佰", s09);

                amount = iTOTAL_AMOUNT / 1000000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 1000000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　佰", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "拾", s09);

                amount = iTOTAL_AMOUNT / 100000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 100000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　拾", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "萬", s09);

                amount = iTOTAL_AMOUNT / 10000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 10000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　萬", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "仟", s09);

                amount = iTOTAL_AMOUNT / 1000;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 1000;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　仟", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "佰", s09);

                amount = iTOTAL_AMOUNT / 100;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 100;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　佰", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "拾", s09);

                amount = iTOTAL_AMOUNT / 10;
                sAmount = GF_Converts(amount.ToString());
                iTOTAL_AMOUNT = iTOTAL_AMOUNT % 10;
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　拾", s10));
                tc = new PdfPCell(p1);
                tc.PaddingTop = fPadding;
                tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc.BorderWidthLeft = 0;
                tc.BorderWidthRight = 0;
                tc.BorderWidthBottom = 0;
                tTemp.AddCell(tc);
                //p1 = new Paragraph("一二三" + "元整", s09);

                sAmount = GF_Converts(iTOTAL_AMOUNT.ToString());
                p1 = new Paragraph(new Chunk(sAmount, s10));
                p1.Add(new Chunk("　元整", s10));
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
                if (dtRemark != null && dtRemark.Rows.Count > 0)
                {
                    for (int j = 0; j < dtRemark.Rows.Count; j++)
                    {
                        if (j != 0) { p1.Add("\n"); }
                        p1.Add(dtRemark.Rows[j][0].ToString());
                    }
                }
                else
                {
                    p1 = new Paragraph("", s09);
                }


                tbInner03.AddCell(CF(new PdfPCell(p1), "HT", 90.71f));
                p1 = new Paragraph("營業人蓋用統一發票專用章", s10);
                tbInner03.AddCell(CF(new PdfPCell(p1), "H", Rectangle.ALIGN_CENTER));
                //jpg = iTextSharp.text.Image.GetInstance("F:\\Title02.JPG");
                jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images") + "\\Title02.JPG");
                jpg.SetAbsolutePosition(0, 0);

                jpg.ScalePercent(60f);

                tc = new PdfPCell(jpg, false);
                tc.PaddingTop = 1;
                tc.PaddingLeft = 15;
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
                p1 = new Paragraph("本發票依    國稅局 年 月 日    字第        號函核准使用。", s07);
                tbFooter.AddCell(CF(new PdfPCell(p1), "B", 0));

                pdfTable.AddCell(CF(new PdfPCell(tbFooter), "B", 0));

                ////調整版面用
                p1 = new Paragraph("");
                tc = CF(new PdfPCell(p1), "B", 0);
                tc.FixedHeight = 43.2f;
                pdfTable.AddCell(tc);


                pdfDoc.Add(pdfTable);
            }
        }

        /// <summary>
        /// 折讓單列印
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="printerName"></param>
        public string getnerateDebitNote(string POSUUID_MASTER,string PrintName)
        {

            DataTable dtOrg = getSALEData(POSUUID_MASTER);
            DataTable dtItem = getDebitReceiptData(POSUUID_MASTER);

            string SALE_NO = "";
            string sStoreNo = "";
            string CREDIT_NOTE_NO = "";
            string sYYYYMMDD = "";
            string UNINO = "";
            string UNITITLE = "";
            if (dtOrg.Rows.Count > 0)
            {
                DateTime aDate = Convert.ToDateTime(dtOrg.Rows[0]["TRADE_DATE"]);
                //  sInvoiceNo = aDate.Year.ToString();
                //  sInvoiceNo += "_" + dtOrg.Rows[0]["INVOICE_NO"].ToString();
                sStoreNo = dtOrg.Rows[0]["STORE_NO"].ToString();
                //    sYYYYMMDD = aDate.ToString("yyyyMMdd");
                SALE_NO = dtOrg.Rows[0]["SALE_NO"].ToString();
               // UNINO = "";
                UNINO = (dtOrg.Rows[0]["UNI_NO"] != DBNull.Value) ? dtOrg.Rows[0]["UNI_NO"].ToString() : "";
                UNITITLE = (dtOrg.Rows[0]["UNI_TITLE"] != DBNull.Value) ? dtOrg.Rows[0]["UNI_TITLE"].ToString() : ""; 
            }
            if (dtItem.Rows.Count > 0)
            {
                //  CREDIT_NOTE_NO = dtItem.Rows
                CREDIT_NOTE_NO = (dtItem.Rows[0]["CREDIT_NOTE_NO"] != DBNull.Value) ? dtItem.Rows[0]["CREDIT_NOTE_NO"].ToString() : "";//店名 = (drOrg["STORENAME"]!=DBNull.Value)?drOrg["STORENAME"].ToString():"";//店名
            }

            DataSet dsItems = new DataSet();
            DataTable dtTo = null;
            //1頁5列
            if (dtItem.Rows.Count > 0)
            {
                int TablesCount = dtItem.Rows.Count / 5 + ((dtItem.Rows.Count % 5 == 0) ? 0 : 1);
                for (int i = 0; i < dtItem.Rows.Count; i++)
                {
                    if (i % 5 == 0)
                    {
                        dtTo = dtItem.Clone();
                        dsItems.Tables.Add(dtTo);
                    }
                    DataRow drFrom = dtItem.Rows[i];
                    DataRow drTo = dtTo.NewRow();
                    drTo.ItemArray = drFrom.ItemArray;
                    dtTo.Rows.Add(drTo);
                }
            }
            //PDF Start
            //Paragraph p1;
            PdfPCell tc;
            pdfDoc = new Document(PageSize.A4, 0f, 0f, 24f, 10f);
            //檔名
            if (!string.IsNullOrEmpty(SALE_NO))
            {
                filename = "Debit" + DateTime.Now.ToString("yyyyMMddHHMMSS") + ".pdf";
            }
            else
            {
                filename = Guid.NewGuid().ToString() + ".pdf";
            }

            //檢查路徑
            if (string.IsNullOrEmpty(sYYYYMMDD)) sYYYYMMDD = DateTime.Now.ToString("yyyyMMdd");
            string sSubPath = filePath + "/" + sYYYYMMDD + "/" + sStoreNo;
            checkDirectory(sSubPath);
            // if (!Directory.Exists(sSubPath)) Directory.CreateDirectory(sSubPath);

            //產生PDF檔案
            FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(sSubPath), filename), FileMode.Create);

            writer1 = PdfWriter.GetInstance(pdfDoc, stream);

            pdfDoc.Open();

            string printerName = ConfigurationManager.AppSettings["Credit_PDFPrinterName"].ToString();//web.config中設定
            // 加入自動列印指令碼
            AddPrintAction(writer1, PrintName);
            //writer1.AddJavaScript("this.print(false);", true);
            int iCopies = dsItems.Tables.Count;
            int nowpage = 0;
            PdfPTable tb1 = cleartb;
            for (int i = 0; i < iCopies; i++)
            {
                DataTable dtItem1 = null;
              //  DataTable dtRemark1 = null;
                if (i == 0)
                {
                    nowpage = 1;
                }
                else
                {
                    nowpage = nowpage + 1;
                }
                if (i < dsItems.Tables.Count)
                {
                    dtItem1 = dsItems.Tables[i];
                }
                else
                {
                    dtItem1 = null;
                }
                if (UNINO == "")
                {
                    Table01NOUNI(pdfDoc, sStoreNo, SALE_NO, CREDIT_NOTE_NO, dtItem1, Convert.ToString(nowpage), Convert.ToString(iCopies));

                }
                else
                {
                    Table01(pdfDoc, sStoreNo, SALE_NO, CREDIT_NOTE_NO, dtItem1, UNITITLE, UNINO, Convert.ToString(nowpage), Convert.ToString(iCopies));

                }
               
                //TABLE間的間距
                tc = new PdfPCell(new Paragraph(""));
                tc.FixedHeight = 10f;
                tc.BorderWidth = 0;
                tbBorder.AddCell(tc);

                if (UNINO == "")
                {
                    Table02NOUNI(pdfDoc, sStoreNo, SALE_NO, CREDIT_NOTE_NO, dtItem1, Convert.ToString(nowpage), Convert.ToString(iCopies));

                }
                else
                {
                    Table02(pdfDoc, sStoreNo, SALE_NO, CREDIT_NOTE_NO, dtItem1, UNITITLE, UNINO, Convert.ToString(nowpage), Convert.ToString(iCopies));

                }
               
                //TABLE間的間距
                tc = new PdfPCell(new Paragraph(""));
                tc.FixedHeight = 10f;
                tc.BorderWidth = 0;
                tbBorder.AddCell(tc);

                if (UNINO == "")
                {
                    Table03NOUNI(pdfDoc, sStoreNo, SALE_NO, CREDIT_NOTE_NO, dtItem1, Convert.ToString(nowpage), Convert.ToString(iCopies));

                }
                else
                {
                    Table03(pdfDoc, sStoreNo, SALE_NO, CREDIT_NOTE_NO, dtItem1, UNITITLE, UNINO, Convert.ToString(nowpage), Convert.ToString(iCopies));

                }


                //TABLE間的間距
                tc = new PdfPCell(new Paragraph(""));
                tc.FixedHeight = 10f;
                tc.BorderWidth = 0;
                tbBorder.AddCell(tc);

                if (UNINO == "")
                {
                    Table04NOUNI(pdfDoc, sStoreNo, SALE_NO, CREDIT_NOTE_NO, dtItem1, Convert.ToString(nowpage), Convert.ToString(iCopies));

                }
                else
                {
                    Table04(pdfDoc, sStoreNo, SALE_NO, CREDIT_NOTE_NO, dtItem1, UNITITLE, UNINO, Convert.ToString(nowpage), Convert.ToString(iCopies));

                }

                pdfDoc.Add(tbBorder);
                
                tbBorder = tb1;
               // tbBorder = null;
                if (i != iCopies - 1)
                {
                    //換頁
                    pdfDoc.NewPage();
                }
            }
          
            pdfDoc.Close();
            return filename;

        }

        private void Table01(Document pDoc, string Store_NO, string SALE_NO, string CREDIT_NOTE_NO, DataTable dtItem,string UNITITLE,string UNINO,string nowpage,string totalpage)
        {
            //外框(pdfTable)，1欄
            //PdfPTable tbBorder = new PdfPTable(1);
            //tbBorder.DefaultCell.Border = 0;
            //tbBorder.SetTotalWidth(new float[] { 532f });
            //tbBorder.LockedWidth = true;
            //tbBorder.SpacingBefore = 0;
            //tbBorder.SpacingAfter = 0;
            string SUNINO = (dtItem.Rows[0]["SUNINO"] != DBNull.Value) ? dtItem.Rows[0]["SUNINO"].ToString() : "";
            string STORENAME = (dtItem.Rows[0]["STORENAME"] != DBNull.Value) ? dtItem.Rows[0]["STORENAME"].ToString() : "";
            string UNIADDR = (dtItem.Rows[0]["UNIADDR"] != DBNull.Value) ? dtItem.Rows[0]["UNIADDR"].ToString() : "";
            string INVALID_DATE = (dtItem.Rows[0]["INVALID_DATE"] != DBNull.Value) ? dtItem.Rows[0]["INVALID_DATE"].ToString() : "";
            string POS_RECEIPT_TITLE = (dtItem.Rows[0]["POS_RECEIPT_TITLE"] != DBNull.Value) ? dtItem.Rows[0]["POS_RECEIPT_TITLE"].ToString() : "";//發票抬頭
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
            tc01 = new PdfPCell(new Paragraph(POS_RECEIPT_TITLE, s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthTop = 0;
            tc01.BorderWidthLeft = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph(SUNINO, s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthTop = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph(UNIADDR, s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthLeft = 0;
            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 0f;
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
            tc01.PaddingLeft = 20f;

            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            //tc01.VerticalAlignment = Element.ALIGN_TOP;
            tbTemp01.AddCell(tc01);

            p01 = new Paragraph("中 華 民 國  ", s09);
            DateTime DebitNoteDate = Convert.ToDateTime(INVALID_DATE);
            //  p01.Add(DebitNoteDate.Year.ToString());
            p01.Add((DebitNoteDate.Year - 1911).ToString().PadLeft(2, ' '));
            p01.Add("  年");
            p01.Add(DebitNoteDate.Month.ToString().PadLeft(4, ' '));
            p01.Add("　月");
            p01.Add(DebitNoteDate.Day.ToString().PadLeft(4, ' '));
            p01.Add("　日");
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
            
            p01 = new Paragraph("                                                            頁次: " + nowpage + "/" + totalpage, s10);
            tc01 = new PdfPCell(p01);
            tc01.PaddingLeft = 100f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbTitle.AddCell(tc01);

            tc01 = new PdfPCell(tbTitle);
            tc01.PaddingBottom = 0;
            tc01.PaddingTop = 0;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbBorder.AddCell(tc01);

            //單號,3欄
            PdfPTable tbNO = new PdfPTable(new float[] { 3f, 0.75f, 1.55f, 0.75f, 1.75f, 0.75f, 1.25f });
            tbNO.DefaultCell.BorderWidthBottom = 0;
            tbNO.DefaultCell.BorderWidthLeft = 0;
            tbNO.DefaultCell.BorderWidthRight = 0;
            tbNO.DefaultCell.BorderWidthTop = 0;
            tbNO.AddCell(new Paragraph(""));
            tbNO.AddCell(new Paragraph("折讓單號：", s07));
            tbNO.AddCell(new Paragraph(CREDIT_NOTE_NO, s07));//折讓單號
            tc01 = new PdfPCell(new Paragraph("訂單編號：", s07));
           // tc01.PaddingLeft = 17f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbNO.AddCell(tc01);
            tbNO.AddCell(new Paragraph(SALE_NO, s07));//訂單編號
            tbNO.AddCell(new Paragraph("門市代號：", s07));
            tbNO.AddCell(new Paragraph(Store_NO, s07));//門市代號
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
            // tbContent.LockedWidth = true;
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
            string SALEAMOUNT = "0";
            string SALETAX = "0";
            if (dtItem != null && dtItem.Rows.Count > 0)
            {

                for (int j = 0; j < dtItem.Rows.Count; j++)
                {
                    DataRow _dr = dtItem.Rows[j];
                    SALEAMOUNT = (_dr["SALE_AMOUNT"] != DBNull.Value) ? _dr["SALE_AMOUNT"].ToString() : "";
                    SALETAX = (_dr["TAX"] != DBNull.Value) ? _dr["TAX"].ToString() : "";
                    string sPROD_NAME = (_dr["PROD_NAME"] != DBNull.Value) ? _dr["PROD_NAME"].ToString() : "";
                    string sQUANTITY = (_dr["QUANTITY"] != DBNull.Value) ? _dr["QUANTITY"].ToString() : "";
                    string sPRICE = (_dr["PRICE"] != DBNull.Value) ? _dr["PRICE"].ToString() : "";
                    string sAMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                    string INVOICE_DATE = (_dr["INVOICE_DATE"] != DBNull.Value) ? _dr["INVOICE_DATE"].ToString() : "";
                    string INVOICE_NO = (_dr["INVOICE_NO"] != DBNull.Value) ? _dr["INVOICE_NO"].ToString() : "";
                    string AMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                    string TAX = (_dr["ITEMTAX"] != DBNull.Value) ? _dr["ITEMTAX"].ToString() : "";
                    string TAXABLE = (_dr["TAXABLE"] != DBNull.Value) ? _dr["TAXABLE"].ToString() : "";
                    string TAXRATE = (_dr["TAXRATE"] != DBNull.Value) ? _dr["TAXRATE"].ToString() : "";
                    string IAMOUNT = (_dr["IAMOUNT"] != DBNull.Value) ? _dr["IAMOUNT"].ToString() : "0";
                    string YY = "";
                    string NN = "";
                    string NOTAX = "";
                    if (TAXABLE == "Y")
                    {

                        YY = "V";
                        if (TAXRATE == "0")
                        {
                            NOTAX = "V";
                        }
                    }
                    else if (TAXABLE == "N")
                    {
                        NN = "V";
                    }


                    if (j != 0)
                    { 
                    
                    }


                    //line3
                    tc01 = new PdfPCell(new Paragraph("3", s07));//c01
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tc01.FixedHeight = 15f;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(INVOICE_DATE, s07));//c02
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(INVOICE_NO, s07));//c03
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sPROD_NAME, s07));//c04
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.PaddingTop = 0f;
                    tc01.PaddingBottom = 0f;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sQUANTITY, s07));//c05
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    //  tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph("", s07));//c06
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    // tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(IAMOUNT, s07));//c07
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //    tc01.BorderWidthLeft = 1;
                    //  tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(TAX, s07));//c08
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(YY, s07));//c09
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthRight = 1;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(NOTAX, s07));//c10
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthRight = 1;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(NN, s07));//c11
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    //tc01.BorderWidthRight = 3;
                    tbContent.AddCell(tc01);
                }
            }
            if (dtItem.Rows.Count < 5)
            {
                int xcount = 5 - dtItem.Rows.Count;
                //xcount = xcount * 2;

                //line3
                tc01 = new PdfPCell(new Paragraph("", s05));//c01
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tc01.FixedHeight = 75f / 5 * xcount;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c02
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c03
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c04
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c05
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                //  tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c06
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                // tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c07
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //    tc01.BorderWidthLeft = 1;
                //  tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c08
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c09
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthRight = 1;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c10
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthRight = 1;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c11
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                //tc01.BorderWidthRight = 3;
                tbContent.AddCell(tc01);


            }
            //line4
            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 4;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("總      計", s08));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 2;
            tbContent.AddCell(tc01);
            if (totalpage != "1" && nowpage != "1")
            {
                SALEAMOUNT = "XX";
                SALETAX = "XX";
            }
            tc01 = new PdfPCell(new Paragraph(SALEAMOUNT, s07));
            tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph(SALETAX, s07));
            tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
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
            string pageword = checkpage(nowpage, totalpage);
            p01 = new Paragraph("本證明單所列銷貨退回或折讓，確屬事實，特此證明。                                                                       ", s08);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);
            string space = "                      ";
            if (UNITITLE.Length > 11)
            {
                UNITITLE = UNITITLE.Substring(0, 11);
            }
            int unilength = UNITITLE.Length * 2;
            int difflength = space.Length - unilength;
            
            if (difflength <= 0)
            {
                space = "";
            }
            else
            {
                space = space.Substring(0, space.Length - unilength);
            }
            p01 = new Paragraph("原進貨營業人(或原買受人)名稱: " + UNITITLE + space + "(簽章或姓名)               地址:                                   " + pageword, s08);
            //  p01 = new Paragraph("原進貨營業人(或原買受人)名稱:                       (簽章或姓名)               地址:                                     " + pageword, s08);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);

            p01 = new Paragraph("身分證字號/營利事業統一編號: "+UNINO, s08);
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
            PdfContentByte cb = writer1.DirectContent;
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

        private void Table02(Document pDoc, string Store_NO, string SALE_NO, string CREDIT_NOTE_NO, DataTable dtItem, string UNITITLE, string UNINO, string nowpage, string totalpage)
        {
            //外框(pdfTable)，1欄
            //PdfPTable tbBorder = new PdfPTable(1);
            //tbBorder.DefaultCell.Border = 0;
            //tbBorder.SetTotalWidth(new float[] { 532f });
            //tbBorder.LockedWidth = true;
            //tbBorder.SpacingBefore = 0;
            //tbBorder.SpacingAfter = 0;
            string SUNINO = (dtItem.Rows[0]["SUNINO"] != DBNull.Value) ? dtItem.Rows[0]["SUNINO"].ToString() : "";
            string STORENAME = (dtItem.Rows[0]["STORENAME"] != DBNull.Value) ? dtItem.Rows[0]["STORENAME"].ToString() : "";
            string UNIADDR = (dtItem.Rows[0]["UNIADDR"] != DBNull.Value) ? dtItem.Rows[0]["UNIADDR"].ToString() : "";
            string INVALID_DATE = (dtItem.Rows[0]["INVALID_DATE"] != DBNull.Value) ? dtItem.Rows[0]["INVALID_DATE"].ToString() : "";
            string POS_RECEIPT_TITLE = (dtItem.Rows[0]["POS_RECEIPT_TITLE"] != DBNull.Value) ? dtItem.Rows[0]["POS_RECEIPT_TITLE"].ToString() : "";//發票抬頭
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
            tc01 = new PdfPCell(new Paragraph(POS_RECEIPT_TITLE, s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthTop = 0;
            tc01.BorderWidthLeft = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph(SUNINO, s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthTop = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph(UNIADDR, s06));
            tc01.FixedHeight = 15.118f;
            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 0f;
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
            tc01.PaddingLeft = 20f;

            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            //tc01.VerticalAlignment = Element.ALIGN_TOP;
            tbTemp01.AddCell(tc01);
            p01 = new Paragraph("中 華 民 國  ", s09);
            DateTime DebitNoteDate = Convert.ToDateTime(INVALID_DATE);
            //  p01.Add(DebitNoteDate.Year.ToString());
            p01.Add((DebitNoteDate.Year - 1911).ToString().PadLeft(2, ' '));
            p01.Add("  年");
            p01.Add(DebitNoteDate.Month.ToString().PadLeft(4, ' '));
            p01.Add("　月");
            p01.Add(DebitNoteDate.Day.ToString().PadLeft(4, ' '));
            p01.Add("　日");
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
            p01 = new Paragraph("                                                            頁次: " + nowpage + "/" + totalpage, s10);
            tc01 = new PdfPCell(p01);
            tc01.PaddingLeft = 100f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbTitle.AddCell(tc01);

            tc01 = new PdfPCell(tbTitle);
            tc01.PaddingBottom = 0;
            tc01.PaddingTop = 0;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbBorder.AddCell(tc01);

            //單號,3欄
            PdfPTable tbNO = new PdfPTable(new float[] { 3f, 0.75f, 1.55f, 0.75f, 1.75f, 0.75f, 1.25f });
            tbNO.DefaultCell.BorderWidthBottom = 0;
            tbNO.DefaultCell.BorderWidthLeft = 0;
            tbNO.DefaultCell.BorderWidthRight = 0;
            tbNO.DefaultCell.BorderWidthTop = 0;
            tbNO.AddCell(new Paragraph(""));
            tbNO.AddCell(new Paragraph("折讓單號：", s07));
            tbNO.AddCell(new Paragraph(CREDIT_NOTE_NO, s07));//折讓單號
            tc01 = new PdfPCell(new Paragraph("訂單編號：", s07));
         //   tc01.PaddingLeft = 17f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbNO.AddCell(tc01);
            tbNO.AddCell(new Paragraph(SALE_NO, s07));//訂單編號
            tbNO.AddCell(new Paragraph("門市代號：", s07));
            tbNO.AddCell(new Paragraph(Store_NO, s07));//門市代號
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

            string SALEAMOUNT = "0";
            string SALETAX = "0";
            if (dtItem != null && dtItem.Rows.Count > 0)
            {

                for (int j = 0; j < dtItem.Rows.Count; j++)
                {
                    DataRow _dr = dtItem.Rows[j];
                    SALEAMOUNT = (_dr["SALE_AMOUNT"] != DBNull.Value) ? _dr["SALE_AMOUNT"].ToString() : "";
                    SALETAX = (_dr["TAX"] != DBNull.Value) ? _dr["TAX"].ToString() : "";
                    string sPROD_NAME = (_dr["PROD_NAME"] != DBNull.Value) ? _dr["PROD_NAME"].ToString() : "";
                    string sQUANTITY = (_dr["QUANTITY"] != DBNull.Value) ? _dr["QUANTITY"].ToString() : "";
                    string sPRICE = (_dr["PRICE"] != DBNull.Value) ? _dr["PRICE"].ToString() : "";
                    string sAMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                    string INVOICE_DATE = (_dr["INVOICE_DATE"] != DBNull.Value) ? _dr["INVOICE_DATE"].ToString() : "";
                    string INVOICE_NO = (_dr["INVOICE_NO"] != DBNull.Value) ? _dr["INVOICE_NO"].ToString() : "";
                    string AMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                    string TAX = (_dr["ITEMTAX"] != DBNull.Value) ? _dr["ITEMTAX"].ToString() : "";
                    string TAXABLE = (_dr["TAXABLE"] != DBNull.Value) ? _dr["TAXABLE"].ToString() : "";
                    string TAXRATE = (_dr["TAXRATE"] != DBNull.Value) ? _dr["TAXRATE"].ToString() : "";
                    string IAMOUNT = (_dr["IAMOUNT"] != DBNull.Value) ? _dr["IAMOUNT"].ToString() : "0";
                    string YY = "";
                    string NN = "";
                    string NOTAX = "";
                    if (TAXABLE == "Y")
                    {

                        YY = "V";
                        if (TAXRATE == "0")
                        {
                            NOTAX = "V";
                        }
                    }
                    else if (TAXABLE == "N")
                    {
                        NN = "V";
                    }




                    //line3
                    //line3
                    tc01 = new PdfPCell(new Paragraph("3", s07));//c01
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tc01.FixedHeight = 15f;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(INVOICE_DATE, s07));//c02
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(INVOICE_NO, s07));//c03
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sPROD_NAME, s07));//c04
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.PaddingTop = 0f;
                    tc01.PaddingBottom = 0f;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sQUANTITY, s07));//c05
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    //  tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph("", s07));//c06
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    // tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(IAMOUNT, s07));//c07
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //    tc01.BorderWidthLeft = 1;
                    //  tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(TAX, s07));//c08
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(YY, s07));//c09
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthRight = 1;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(NOTAX, s07));//c10
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthRight = 1;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(NN, s07));//c11
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    //tc01.BorderWidthRight = 3;
                    tbContent.AddCell(tc01);
                }
            }
            if (dtItem.Rows.Count < 5)
            {
                int xcount = 5 - dtItem.Rows.Count;
             //   xcount = xcount * 2;

                //line3
                tc01 = new PdfPCell(new Paragraph("", s05));//c01
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tc01.FixedHeight = 75f / 5 * xcount;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c02
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c03
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c04
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c05
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                //  tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c06
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                // tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c07
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //    tc01.BorderWidthLeft = 1;
                //  tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c08
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c09
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthRight = 1;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c10
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthRight = 1;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c11
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                //tc01.BorderWidthRight = 3;
                tbContent.AddCell(tc01);


            }
            //line4
            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 4;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("總      計", s08));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 2;
            tbContent.AddCell(tc01);
            if (totalpage != "1" && nowpage != "1")
            {
                SALEAMOUNT = "XX";
                SALETAX = "XX";
            }
            tc01 = new PdfPCell(new Paragraph(SALEAMOUNT, s07));
            tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph(SALETAX, s07));
            tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
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
            p01 = new Paragraph(@"                                                                                                                                       第二聯:交付原銷貨人作為記帳憑證", s06);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingBottom = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingRight = 0;
            tbFooter.AddCell(tc01);

            string pageword = checkpage(nowpage, totalpage);
            p01 = new Paragraph("本證明單所列銷貨退回或折讓，確屬事實，特此證明。                                                                       ", s08);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);
            string space = "                      ";
            if (UNITITLE.Length > 11)
            {
                UNITITLE = UNITITLE.Substring(0, 11);
            }
            int unilength = UNITITLE.Length * 2;
            int difflength = space.Length - unilength;

            if (difflength <= 0)
            {
                space = "";
            }
            else
            {
                space = space.Substring(0, space.Length - unilength);
            }
            p01 = new Paragraph("原進貨營業人(或原買受人)名稱: " + UNITITLE + space + "(簽章或姓名)               地址:                                   " + pageword, s08);

        //    p01 = new Paragraph("原進貨營業人(或原買受人)名稱: " + UNITITLE + "                      (簽章或姓名)               地址:                                     ", s08);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);

            p01 = new Paragraph("身分證字號/營利事業統一編號: " + UNINO, s08); tc01 = new PdfPCell(p01);
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
            PdfContentByte cb = writer1.DirectContent;
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

        private void Table03(Document pDoc, string Store_NO, string SALE_NO, string CREDIT_NOTE_NO, DataTable dtItem, string UNITITLE, string UNINO, string nowpage, string totalpage)
        {
            //外框(pdfTable)，1欄
            //PdfPTable tbBorder = new PdfPTable(1);
            //tbBorder.DefaultCell.Border = 0;
            //tbBorder.SetTotalWidth(new float[] { 532f });
            //tbBorder.LockedWidth = true;
            //tbBorder.SpacingBefore = 0;
            //tbBorder.SpacingAfter = 0;
            string SUNINO = (dtItem.Rows[0]["SUNINO"] != DBNull.Value) ? dtItem.Rows[0]["SUNINO"].ToString() : "";
            string STORENAME = (dtItem.Rows[0]["STORENAME"] != DBNull.Value) ? dtItem.Rows[0]["STORENAME"].ToString() : "";
            string UNIADDR = (dtItem.Rows[0]["UNIADDR"] != DBNull.Value) ? dtItem.Rows[0]["UNIADDR"].ToString() : "";
            string INVALID_DATE = (dtItem.Rows[0]["INVALID_DATE"] != DBNull.Value) ? dtItem.Rows[0]["INVALID_DATE"].ToString() : "";
            string POS_RECEIPT_TITLE = (dtItem.Rows[0]["POS_RECEIPT_TITLE"] != DBNull.Value) ? dtItem.Rows[0]["POS_RECEIPT_TITLE"].ToString() : "";//發票抬頭
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
            tc01 = new PdfPCell(new Paragraph(POS_RECEIPT_TITLE, s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthTop = 0;
            tc01.BorderWidthLeft = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph(SUNINO, s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthTop = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph(UNIADDR, s06));
            tc01.FixedHeight = 15.118f;
            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 0f;
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
            tc01.PaddingLeft = 20f;

            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            //tc01.VerticalAlignment = Element.ALIGN_TOP;
            tbTemp01.AddCell(tc01);
            string yy = DateTime.Now.ToString("yyyy");
            string mm = Convert.ToString(DateTime.Today.Month);
            string dd = Convert.ToString(DateTime.Today.Day);
            //p1 = new Paragraph("中 華 民 國", s11);
            //DateTime dINVOICE_DATE = Convert.ToDateTime(INVOICE_DATE);
            ////p1.Add(dINVOICE_DATE.Year.ToString());
            //p1.Add((dINVOICE_DATE.Year - 1911).ToString().PadLeft(4, ' '));
            //p1.Add("年");
            //p1.Add(dINVOICE_DATE.Month.ToString().PadLeft(4, ' '));
            //p1.Add("月");
            //p1.Add(dINVOICE_DATE.Day.ToString().PadLeft(4, ' '));
            //p1.Add("日");
            //  string dd = DateTime.Now.ToString("dd");
            p01 = new Paragraph("中 華 民 國  ", s09);
            DateTime DebitNoteDate = Convert.ToDateTime(INVALID_DATE);
            //  p01.Add(DebitNoteDate.Year.ToString());
            p01.Add((DebitNoteDate.Year - 1911).ToString().PadLeft(2, ' '));
            p01.Add("  年");
            p01.Add(DebitNoteDate.Month.ToString().PadLeft(4, ' '));
            p01.Add("　月");
            p01.Add(DebitNoteDate.Day.ToString().PadLeft(4, ' '));
            p01.Add("　日");
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
            p01 = new Paragraph("                                                            頁次: " + nowpage + "/" + totalpage, s10);
            tc01 = new PdfPCell(p01);
            tc01.PaddingLeft = 100f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbTitle.AddCell(tc01);

            tc01 = new PdfPCell(tbTitle);
            tc01.PaddingBottom = 0;
            tc01.PaddingTop = 0;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbBorder.AddCell(tc01);

            //單號,3欄
            PdfPTable tbNO = new PdfPTable(new float[] { 3f, 0.75f, 1.55f, 0.75f, 1.75f, 0.75f, 1.25f });
            tbNO.DefaultCell.BorderWidthBottom = 0;
            tbNO.DefaultCell.BorderWidthLeft = 0;
            tbNO.DefaultCell.BorderWidthRight = 0;
            tbNO.DefaultCell.BorderWidthTop = 0;
            tbNO.AddCell(new Paragraph(""));
            tbNO.AddCell(new Paragraph("折讓單號：", s07));
            tbNO.AddCell(new Paragraph(CREDIT_NOTE_NO, s07));//折讓單號
            tc01 = new PdfPCell(new Paragraph("訂單編號：", s07));
          //  tc01.PaddingLeft = 17f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbNO.AddCell(tc01);
            tbNO.AddCell(new Paragraph(SALE_NO, s07));//訂單編號
            tbNO.AddCell(new Paragraph("門市代號：", s07));
            tbNO.AddCell(new Paragraph(Store_NO, s07));//門市代號
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

            string SALEAMOUNT = "0";
            string SALETAX = "0";
            if (dtItem != null && dtItem.Rows.Count > 0)
            {

                for (int j = 0; j < dtItem.Rows.Count; j++)
                {
                    DataRow _dr = dtItem.Rows[j];
                    SALEAMOUNT = (_dr["SALE_AMOUNT"] != DBNull.Value) ? _dr["SALE_AMOUNT"].ToString() : "";
                    SALETAX = (_dr["TAX"] != DBNull.Value) ? _dr["TAX"].ToString() : "";
                    string sPROD_NAME = (_dr["PROD_NAME"] != DBNull.Value) ? _dr["PROD_NAME"].ToString() : "";
                    string sQUANTITY = (_dr["QUANTITY"] != DBNull.Value) ? _dr["QUANTITY"].ToString() : "";
                    string sPRICE = (_dr["PRICE"] != DBNull.Value) ? _dr["PRICE"].ToString() : "";
                    string sAMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                    string INVOICE_DATE = (_dr["INVOICE_DATE"] != DBNull.Value) ? _dr["INVOICE_DATE"].ToString() : "";
                    string INVOICE_NO = (_dr["INVOICE_NO"] != DBNull.Value) ? _dr["INVOICE_NO"].ToString() : "";
                    string AMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                    string TAX = (_dr["ITEMTAX"] != DBNull.Value) ? _dr["ITEMTAX"].ToString() : "";
                    string TAXABLE = (_dr["TAXABLE"] != DBNull.Value) ? _dr["TAXABLE"].ToString() : "";
                    string TAXRATE = (_dr["TAXRATE"] != DBNull.Value) ? _dr["TAXRATE"].ToString() : "";
                    string IAMOUNT = (_dr["IAMOUNT"] != DBNull.Value) ? _dr["IAMOUNT"].ToString() : "0";
                    string YY = "";
                    string NN = "";
                    string NOTAX = "";
                    if (TAXABLE == "Y")
                    {

                        YY = "V";
                        if (TAXRATE == "0")
                        {
                            NOTAX = "V";
                        }
                    }
                    else if (TAXABLE == "N")
                    {
                        NN = "V";
                    }




                    //line3
                    //line3
                    tc01 = new PdfPCell(new Paragraph("3", s07));//c01
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tc01.FixedHeight = 15f;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(INVOICE_DATE, s07));//c02
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(INVOICE_NO, s07));//c03
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sPROD_NAME, s07));//c04
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.PaddingTop = 0f;
                    tc01.PaddingBottom = 0f;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sQUANTITY, s07));//c05
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    //  tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph("", s07));//c06
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    // tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(IAMOUNT, s07));//c07
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //    tc01.BorderWidthLeft = 1;
                    //  tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(TAX, s07));//c08
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(YY, s07));//c09
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthRight = 1;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(NOTAX, s07));//c10
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthRight = 1;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(NN, s07));//c11
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    //tc01.BorderWidthRight = 3;
                    tbContent.AddCell(tc01);
                }
            }
            if (dtItem.Rows.Count < 5)
            {
                int xcount = 5 - dtItem.Rows.Count;
             //   xcount = xcount * 2;

                //line3
                tc01 = new PdfPCell(new Paragraph("", s07));//c01
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tc01.FixedHeight = 75f / 5 * xcount;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c02
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c03
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c04
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c05
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                //  tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c06
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                // tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c07
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //    tc01.BorderWidthLeft = 1;
                //  tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c08
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c09
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthRight = 1;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c10
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthRight = 1;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c11
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                //tc01.BorderWidthRight = 3;
                tbContent.AddCell(tc01);


            }
            //line4
            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 4;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("總      計", s08));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 2;
            tbContent.AddCell(tc01);
            if (totalpage != "1" && nowpage != "1")
            {
                SALEAMOUNT = "XX";
                SALETAX = "XX";
            }
            tc01 = new PdfPCell(new Paragraph(SALEAMOUNT, s07));
            tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph(SALETAX, s07));
            tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
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
            string pageword = checkpage(nowpage, totalpage);
            p01 = new Paragraph("本證明單所列銷貨退回或折讓，確屬事實，特此證明。                                                                       " , s08);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);

            string space = "                      ";
            if (UNITITLE.Length > 11)
            {
                UNITITLE = UNITITLE.Substring(0, 11);
            }
            int unilength = UNITITLE.Length * 2;
            int difflength = space.Length - unilength;

            if (difflength <= 0)
            {
                space = "";
            }
            else
            {
                space = space.Substring(0, space.Length - unilength);
            }
            p01 = new Paragraph("原進貨營業人(或原買受人)名稱: " + UNITITLE + space + "(簽章或姓名)               地址:                                   " + pageword, s08);


            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);

            p01 = new Paragraph("身分證字號/營利事業統一編號: " + UNINO, s08);
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
            PdfContentByte cb = writer1.DirectContent;
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

        private void Table04(Document pDoc, string Store_NO, string SALE_NO, string CREDIT_NOTE_NO, DataTable dtItem, string UNITITLE, string UNINO, string nowpage, string totalpage)
        {
            //外框(pdfTable)，1欄
            //PdfPTable tbBorder = new PdfPTable(1);
            //tbBorder.DefaultCell.Border = 0;
            //tbBorder.SetTotalWidth(new float[] { 532f });
            //tbBorder.LockedWidth = true;
            //tbBorder.SpacingBefore = 0;
            //tbBorder.SpacingAfter = 0;
            string SUNINO = (dtItem.Rows[0]["SUNINO"] != DBNull.Value) ? dtItem.Rows[0]["SUNINO"].ToString() : "";
            string STORENAME = (dtItem.Rows[0]["STORENAME"] != DBNull.Value) ? dtItem.Rows[0]["STORENAME"].ToString() : "";
            string UNIADDR = (dtItem.Rows[0]["UNIADDR"] != DBNull.Value) ? dtItem.Rows[0]["UNIADDR"].ToString() : "";
            string INVALID_DATE = (dtItem.Rows[0]["INVALID_DATE"] != DBNull.Value) ? dtItem.Rows[0]["INVALID_DATE"].ToString() : "";
            string POS_RECEIPT_TITLE = (dtItem.Rows[0]["POS_RECEIPT_TITLE"] != DBNull.Value) ? dtItem.Rows[0]["POS_RECEIPT_TITLE"].ToString() : "";//發票抬頭
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
            tc01 = new PdfPCell(new Paragraph(POS_RECEIPT_TITLE, s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthTop = 0;
            tc01.BorderWidthLeft = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph(SUNINO, s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthTop = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph(UNIADDR, s06));
            tc01.FixedHeight = 15.118f;
            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 0f;
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
            tc01.PaddingLeft = 20f;

            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            //tc01.VerticalAlignment = Element.ALIGN_TOP;
            tbTemp01.AddCell(tc01);

            p01 = new Paragraph("中 華 民 國  ", s09);
            DateTime DebitNoteDate = Convert.ToDateTime(INVALID_DATE);
            //  p01.Add(DebitNoteDate.Year.ToString());
            p01.Add((DebitNoteDate.Year - 1911).ToString().PadLeft(2, ' '));
            p01.Add("  年");
            p01.Add(DebitNoteDate.Month.ToString().PadLeft(4, ' '));
            p01.Add("　月");
            p01.Add(DebitNoteDate.Day.ToString().PadLeft(4, ' '));
            p01.Add("　日");
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
            p01 = new Paragraph("                                                            頁次: " + nowpage + "/" + totalpage, s10);
            tc01 = new PdfPCell(p01);
            tc01.PaddingLeft = 100f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbTitle.AddCell(tc01);

            tc01 = new PdfPCell(tbTitle);
            tc01.PaddingBottom = 0;
            tc01.PaddingTop = 0;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbBorder.AddCell(tc01);

            //單號,3欄
            PdfPTable tbNO = new PdfPTable(new float[] { 3f, 0.75f, 1.55f, 0.75f, 1.75f, 0.75f, 1.25f });
            tbNO.DefaultCell.BorderWidthBottom = 0;
            tbNO.DefaultCell.BorderWidthLeft = 0;
            tbNO.DefaultCell.BorderWidthRight = 0;
            tbNO.DefaultCell.BorderWidthTop = 0;
            tbNO.AddCell(new Paragraph(""));
            tbNO.AddCell(new Paragraph("折讓單號：", s07));
            tbNO.AddCell(new Paragraph(CREDIT_NOTE_NO, s07));//折讓單號
            tc01 = new PdfPCell(new Paragraph("訂單編號：", s07));
            //tc01.PaddingLeft = 17f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbNO.AddCell(tc01);
            tbNO.AddCell(new Paragraph(SALE_NO, s07));//訂單編號
            tbNO.AddCell(new Paragraph("門市代號：", s07));
            tbNO.AddCell(new Paragraph(Store_NO, s07));//門市代號
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

            string SALEAMOUNT = "0";
            string SALETAX = "0";
            if (dtItem != null && dtItem.Rows.Count > 0)
            {

                for (int j = 0; j < dtItem.Rows.Count; j++)
                {
                    DataRow _dr = dtItem.Rows[j];
                    SALEAMOUNT = (_dr["SALE_AMOUNT"] != DBNull.Value) ? _dr["SALE_AMOUNT"].ToString() : "";
                    SALETAX = (_dr["TAX"] != DBNull.Value) ? _dr["TAX"].ToString() : "";
                    string sPROD_NAME = (_dr["PROD_NAME"] != DBNull.Value) ? _dr["PROD_NAME"].ToString() : "";
                    string sQUANTITY = (_dr["QUANTITY"] != DBNull.Value) ? _dr["QUANTITY"].ToString() : "";
                    string sPRICE = (_dr["PRICE"] != DBNull.Value) ? _dr["PRICE"].ToString() : "";
                    string sAMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                    string INVOICE_DATE = (_dr["INVOICE_DATE"] != DBNull.Value) ? _dr["INVOICE_DATE"].ToString() : "";
                    string INVOICE_NO = (_dr["INVOICE_NO"] != DBNull.Value) ? _dr["INVOICE_NO"].ToString() : "";
                    string AMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                    string TAX = (_dr["ITEMTAX"] != DBNull.Value) ? _dr["ITEMTAX"].ToString() : "";
                    string TAXABLE = (_dr["TAXABLE"] != DBNull.Value) ? _dr["TAXABLE"].ToString() : "";
                    string TAXRATE = (_dr["TAXRATE"] != DBNull.Value) ? _dr["TAXRATE"].ToString() : "";
                    string IAMOUNT = (_dr["IAMOUNT"] != DBNull.Value) ? _dr["IAMOUNT"].ToString() : "0";
                    string YY = "";
                    string NN = "";
                    string NOTAX = "";
                    if (TAXABLE == "Y")
                    {

                        YY = "V";
                        if (TAXRATE == "0")
                        {
                            NOTAX = "V";
                        }
                    }
                    else if (TAXABLE == "N")
                    {
                        NN = "V";
                    }




                    //line3
                    //line3
                    tc01 = new PdfPCell(new Paragraph("3", s07));//c01
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tc01.FixedHeight = 15f;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(INVOICE_DATE, s07));//c02
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(INVOICE_NO, s07));//c03
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sPROD_NAME, s07));//c04
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.PaddingTop = 0f;
                    tc01.PaddingBottom = 0f;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sQUANTITY, s07));//c05
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    //  tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph("", s07));//c06
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    // tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(IAMOUNT, s07));//c07
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //    tc01.BorderWidthLeft = 1;
                    //  tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(TAX, s07));//c08
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(YY, s07));//c09
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthRight = 1;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(NOTAX, s07));//c10
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthRight = 1;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(NN, s07));//c11
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    //tc01.BorderWidthRight = 3;
                    tbContent.AddCell(tc01);
                }
            }
            if (dtItem.Rows.Count < 5)
            {
                int xcount = 5 - dtItem.Rows.Count;
               // xcount = xcount * 2;

                //line3
                tc01 = new PdfPCell(new Paragraph("", s05));//c01
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tc01.FixedHeight = 75f / 5 * xcount;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c02
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c03
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c04
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c05
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                //  tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c06
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                // tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c07
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //    tc01.BorderWidthLeft = 1;
                //  tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c08
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c09
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthRight = 1;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c10
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthRight = 1;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c11
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                //tc01.BorderWidthRight = 3;
                tbContent.AddCell(tc01);


            }
            //line4
            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 4;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("總      計", s08));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 2;
            tbContent.AddCell(tc01);
             if (totalpage != "1" && nowpage != "1")
            {
                SALEAMOUNT = "XX";
                SALETAX = "XX";
            }
            tc01 = new PdfPCell(new Paragraph(SALEAMOUNT, s07));
            tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph(SALETAX, s07));
            tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
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
            string pageword = checkpage(nowpage, totalpage);
            p01 = new Paragraph("本證明單所列銷貨退回或折讓，確屬事實，特此證明。                                                                       "  , s08);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);

            string space = "                      ";
            if (UNITITLE.Length > 11)
            {
                UNITITLE = UNITITLE.Substring(0, 11);
            }
            int unilength = UNITITLE.Length * 2;
            int difflength = space.Length - unilength;

            if (difflength <= 0)
            {
                space = "";
            }
            else
            {
                space = space.Substring(0, space.Length - unilength);
            }
            p01 = new Paragraph("原進貨營業人(或原買受人)名稱: " + UNITITLE + space + "(簽章或姓名)               地址:                                   " + pageword, s08);

            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);

            p01 = new Paragraph("身分證字號/營利事業統一編號: " + UNINO, s08);
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

        private void Table01NOUNI(Document pDoc, string Store_NO, string SALE_NO, string CREDIT_NOTE_NO, DataTable dtItem, string nowpage, string totalpage)
        {
            //外框(pdfTable)，1欄
            //PdfPTable tbBorder = new PdfPTable(1);
            //tbBorder.DefaultCell.Border = 0;
            //tbBorder.SetTotalWidth(new float[] { 532f });
            //tbBorder.LockedWidth = true;
            //tbBorder.SpacingBefore = 0;
            //tbBorder.SpacingAfter = 0;
            string SUNINO = (dtItem.Rows[0]["SUNINO"] != DBNull.Value) ? dtItem.Rows[0]["SUNINO"].ToString() : "";
            string STORENAME = (dtItem.Rows[0]["STORENAME"] != DBNull.Value) ? dtItem.Rows[0]["STORENAME"].ToString() : "";
            string UNIADDR = (dtItem.Rows[0]["UNIADDR"] != DBNull.Value) ? dtItem.Rows[0]["UNIADDR"].ToString() : "";
            string INVALID_DATE = (dtItem.Rows[0]["INVALID_DATE"] != DBNull.Value) ? dtItem.Rows[0]["INVALID_DATE"].ToString() : "";
            string POS_RECEIPT_TITLE = (dtItem.Rows[0]["POS_RECEIPT_TITLE"] != DBNull.Value) ? dtItem.Rows[0]["POS_RECEIPT_TITLE"].ToString() : "";//發票抬頭
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
            tc01 = new PdfPCell(new Paragraph(POS_RECEIPT_TITLE, s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthTop = 0;
            tc01.BorderWidthLeft = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph(SUNINO, s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthTop = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph(UNIADDR, s06));
            tc01.FixedHeight = 15.118f;
            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 0f;
            tc01.PaddingBottom = 0f;
            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 0f;
           // tc01.PaddingTop = 0f;
            tc01.BorderWidthLeft = 0;
          //  tc01.HorizontalAlignment = Element.ALIGN_TOP;
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
            tc01.PaddingLeft = 20f;

            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            //tc01.VerticalAlignment = Element.ALIGN_TOP;
            tbTemp01.AddCell(tc01);

            p01 = new Paragraph("中 華 民 國  ", s09);
            DateTime DebitNoteDate = Convert.ToDateTime(INVALID_DATE);
            //  p01.Add(DebitNoteDate.Year.ToString());
            p01.Add((DebitNoteDate.Year - 1911).ToString().PadLeft(2, ' '));
            p01.Add("  年");
            p01.Add(DebitNoteDate.Month.ToString().PadLeft(4, ' '));
            p01.Add("　月");
            p01.Add(DebitNoteDate.Day.ToString().PadLeft(4, ' '));
            p01.Add("　日");
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
           
            tc01 = new PdfPCell(p01);
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tc01.PaddingTop = 0;
            tc01.AddElement(tbTemp01);
            tbTitle.AddCell(tc01);

            //標題>3欄
            p01 = new Paragraph("                                                            頁次: "+nowpage+"/"+totalpage, s10);
            tc01 = new PdfPCell(p01);
            tc01.PaddingLeft = 100f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbTitle.AddCell(tc01);

            tc01 = new PdfPCell(tbTitle);
            tc01.PaddingBottom = 0;
            tc01.PaddingTop = 0;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbBorder.AddCell(tc01);

            //單號,3欄
            PdfPTable tbNO = new PdfPTable(new float[] {3f, 0.75f, 1.55f, 0.75f, 1.75f, 0.75f, 1.25f});
            tbNO.DefaultCell.BorderWidthBottom = 0;
            tbNO.DefaultCell.BorderWidthLeft = 0;
            tbNO.DefaultCell.BorderWidthRight = 0;
            tbNO.DefaultCell.BorderWidthTop = 0;
            tbNO.AddCell(new Paragraph(""));
            tbNO.AddCell(new Paragraph("折讓單號：", s07));
            tbNO.AddCell(new Paragraph(CREDIT_NOTE_NO, s07));//折讓單號
            tc01 = new PdfPCell(new Paragraph("訂單編號：", s07));
            //tc01.PaddingLeft = 17f;
            tc01.BorderWidthBottom =0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbNO.AddCell(tc01);
            tbNO.AddCell(new Paragraph(SALE_NO, s07));//訂單編號
            tbNO.AddCell(new Paragraph("門市代號：", s07));
            tbNO.AddCell(new Paragraph(Store_NO, s07));//門市代號
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
            // tbContent.LockedWidth = true;
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
            string SALEAMOUNT = "0";
            string SALETAX = "0";
            if (dtItem != null && dtItem.Rows.Count > 0)
            {

                for (int j = 0; j < dtItem.Rows.Count; j++)
                {
                    DataRow _dr = dtItem.Rows[j];
                    SALEAMOUNT = (_dr["SALE_AMOUNT"] != DBNull.Value) ? _dr["SALE_AMOUNT"].ToString() : "";
                    SALETAX = (_dr["TAX"] != DBNull.Value) ? _dr["TAX"].ToString() : "";
                    string sPROD_NAME = (_dr["PROD_NAME"] != DBNull.Value) ? _dr["PROD_NAME"].ToString() : "";
                    string sQUANTITY = (_dr["QUANTITY"] != DBNull.Value) ? _dr["QUANTITY"].ToString() : "";
                    string sPRICE = (_dr["PRICE"] != DBNull.Value) ? _dr["PRICE"].ToString() : "";
                    string sAMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                    string INVOICE_DATE = (_dr["INVOICE_DATE"] != DBNull.Value) ? _dr["INVOICE_DATE"].ToString() : "";
                    string INVOICE_NO = (_dr["INVOICE_NO"] != DBNull.Value) ? _dr["INVOICE_NO"].ToString() : "";
                    string AMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                    string TAX = (_dr["ITEMTAX"] != DBNull.Value) ? _dr["ITEMTAX"].ToString() : "";
                    string TAXABLE = (_dr["TAXABLE"] != DBNull.Value) ? _dr["TAXABLE"].ToString() : "";
                    string TAXRATE = (_dr["TAXRATE"] != DBNull.Value) ? _dr["TAXRATE"].ToString() : "";
                    string IAMOUNT = (_dr["IAMOUNT"] != DBNull.Value) ? _dr["IAMOUNT"].ToString() : "0";
                    double dTmp = Convert.ToDouble(sPRICE);
                    int iprice = Convert.ToInt32(Math.Round(dTmp / 1.05, 0, MidpointRounding.AwayFromZero));
                    sPRICE = StringUtil.NumberFormat(iprice, 0, true);
                    string YY = "";
                    string NN = "";
                    string NOTAX = "";
                    if (TAXABLE == "Y")
                    {

                        YY = "V";
                        if (TAXRATE == "0")
                        {
                            NOTAX = "V";
                        }
                    }
                    else if (TAXABLE == "N")
                    {
                        NN = "V";
                    }




                    //line3
                    tc01 = new PdfPCell(new Paragraph("3", s07));//c01
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tc01.FixedHeight = 15f;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(INVOICE_DATE, s07));//c02
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(INVOICE_NO, s07));//c03
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sPROD_NAME, s07));//c04
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.PaddingTop = 0f;
                    tc01.PaddingBottom = 0f;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sQUANTITY, s07));//c05
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    //  tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph("", s07));//c06
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    // tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(IAMOUNT, s07));//c07
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //    tc01.BorderWidthLeft = 1;
                    //  tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(TAX, s07));//c08
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(YY, s07));//c09
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthRight = 1;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(NOTAX, s07));//c10
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthRight = 1;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(NN, s07));//c11
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    //tc01.BorderWidthRight = 3;
                    tbContent.AddCell(tc01);
                }
            }
            if (dtItem.Rows.Count < 5)
            {
                int xcount = 5 - dtItem.Rows.Count;
               // xcount = xcount * 2;

                //line3
                tc01 = new PdfPCell(new Paragraph("", s07));//c01
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tc01.FixedHeight = 75f / 5 * xcount;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c02
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c03
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c04
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c05
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                //  tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c06
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                // tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c07
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //    tc01.BorderWidthLeft = 1;
                //  tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c08
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c09
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthRight = 1;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c10
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthRight = 1;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c11
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                //tc01.BorderWidthRight = 3;
                tbContent.AddCell(tc01);


            }
            //line4
            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 4;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("總      計", s08));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 2;
            tbContent.AddCell(tc01);
            if (totalpage != "1" && nowpage != "1")
            {
                SALEAMOUNT = "XX";
                SALETAX = "XX";
            }
            tc01 = new PdfPCell(new Paragraph(SALEAMOUNT, s07));
            tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph(SALETAX, s07));
            tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
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
            string pageword = checkpage(nowpage, totalpage);
            p01 = new Paragraph("原進貨營業人(或原買受人)名稱:                       (簽章或姓名)               地址:                                     "+pageword, s08);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);

            p01 = new Paragraph("身分證字號/營利事業統一編號:  ", s08);
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
            PdfContentByte cb = writer1.DirectContent;
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

        private void Table02NOUNI(Document pDoc, string Store_NO, string SALE_NO, string CREDIT_NOTE_NO, DataTable dtItem, string nowpage, string totalpage)
        {
            //外框(pdfTable)，1欄
            //PdfPTable tbBorder = new PdfPTable(1);
            //tbBorder.DefaultCell.Border = 0;
            //tbBorder.SetTotalWidth(new float[] { 532f });
            //tbBorder.LockedWidth = true;
            //tbBorder.SpacingBefore = 0;
            //tbBorder.SpacingAfter = 0;
            string SUNINO = (dtItem.Rows[0]["SUNINO"] != DBNull.Value) ? dtItem.Rows[0]["SUNINO"].ToString() : "";
            string STORENAME = (dtItem.Rows[0]["STORENAME"] != DBNull.Value) ? dtItem.Rows[0]["STORENAME"].ToString() : "";
            string UNIADDR = (dtItem.Rows[0]["UNIADDR"] != DBNull.Value) ? dtItem.Rows[0]["UNIADDR"].ToString() : "";
            string INVALID_DATE = (dtItem.Rows[0]["INVALID_DATE"] != DBNull.Value) ? dtItem.Rows[0]["INVALID_DATE"].ToString() : "";
            string POS_RECEIPT_TITLE = (dtItem.Rows[0]["POS_RECEIPT_TITLE"] != DBNull.Value) ? dtItem.Rows[0]["POS_RECEIPT_TITLE"].ToString() : "";//發票抬頭
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
            tc01 = new PdfPCell(new Paragraph(POS_RECEIPT_TITLE, s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthTop = 0;
            tc01.BorderWidthLeft = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph(SUNINO, s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthTop = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph(UNIADDR, s06));
            tc01.FixedHeight = 15.118f;
            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 0f;
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
            tc01.PaddingLeft = 20f;

            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            //tc01.VerticalAlignment = Element.ALIGN_TOP;
            tbTemp01.AddCell(tc01);
            p01 = new Paragraph("中 華 民 國  ", s09);
            DateTime DebitNoteDate = Convert.ToDateTime(INVALID_DATE);
            //  p01.Add(DebitNoteDate.Year.ToString());
            p01.Add((DebitNoteDate.Year - 1911).ToString().PadLeft(2, ' '));
            p01.Add("  年");
            p01.Add(DebitNoteDate.Month.ToString().PadLeft(4, ' '));
            p01.Add("　月");
            p01.Add(DebitNoteDate.Day.ToString().PadLeft(4, ' '));
            p01.Add("　日");
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
            p01 = new Paragraph("                                                            頁次: " + nowpage + "/" + totalpage, s10);
            tc01 = new PdfPCell(p01);
            tc01.PaddingLeft = 100f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbTitle.AddCell(tc01);

            tc01 = new PdfPCell(tbTitle);
            tc01.PaddingBottom = 0;
            tc01.PaddingTop = 0;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbBorder.AddCell(tc01);

            //單號,3欄
            PdfPTable tbNO = new PdfPTable(new float[] { 3f, 0.75f, 1.55f, 0.75f, 1.75f, 0.75f, 1.25f });
            tbNO.DefaultCell.BorderWidthBottom = 0;
            tbNO.DefaultCell.BorderWidthLeft = 0;
            tbNO.DefaultCell.BorderWidthRight = 0;
            tbNO.DefaultCell.BorderWidthTop = 0;
            tbNO.AddCell(new Paragraph(""));
            tbNO.AddCell(new Paragraph("折讓單號：", s07));
            tbNO.AddCell(new Paragraph(CREDIT_NOTE_NO, s07));//折讓單號
            tc01 = new PdfPCell(new Paragraph("訂單編號：", s07));
            //tc01.PaddingLeft = 17f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbNO.AddCell(tc01);
            tbNO.AddCell(new Paragraph(SALE_NO, s07));//訂單編號
            tbNO.AddCell(new Paragraph("門市代號：", s07));
            tbNO.AddCell(new Paragraph(Store_NO, s07));//門市代號
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

            string SALEAMOUNT = "0";
            string SALETAX = "0";
            if (dtItem != null && dtItem.Rows.Count > 0)
            {

                for (int j = 0; j < dtItem.Rows.Count; j++)
                {
                    DataRow _dr = dtItem.Rows[j];
                    SALEAMOUNT = (_dr["SALE_AMOUNT"] != DBNull.Value) ? _dr["SALE_AMOUNT"].ToString() : "";
                    SALETAX = (_dr["TAX"] != DBNull.Value) ? _dr["TAX"].ToString() : "";
                    string sPROD_NAME = (_dr["PROD_NAME"] != DBNull.Value) ? _dr["PROD_NAME"].ToString() : "";
                    string sQUANTITY = (_dr["QUANTITY"] != DBNull.Value) ? _dr["QUANTITY"].ToString() : "";
                    string sPRICE = (_dr["PRICE"] != DBNull.Value) ? _dr["PRICE"].ToString() : "";
                    string sAMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                    string INVOICE_DATE = (_dr["INVOICE_DATE"] != DBNull.Value) ? _dr["INVOICE_DATE"].ToString() : "";
                    string INVOICE_NO = (_dr["INVOICE_NO"] != DBNull.Value) ? _dr["INVOICE_NO"].ToString() : "";
                    string AMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                    string TAX = (_dr["ITEMTAX"] != DBNull.Value) ? _dr["ITEMTAX"].ToString() : "";
                    string TAXABLE = (_dr["TAXABLE"] != DBNull.Value) ? _dr["TAXABLE"].ToString() : "";
                    string TAXRATE = (_dr["TAXRATE"] != DBNull.Value) ? _dr["TAXRATE"].ToString() : "";
                    string IAMOUNT = (_dr["IAMOUNT"] != DBNull.Value) ? _dr["IAMOUNT"].ToString() : "0";
                    double dTmp = Convert.ToDouble(sPRICE);
                    int iprice = Convert.ToInt32(Math.Round(dTmp / 1.05, 0, MidpointRounding.AwayFromZero));
                    sPRICE = StringUtil.NumberFormat(iprice, 0, true);
                    string YY = "";
                    string NN = "";
                    string NOTAX = "";
                    if (TAXABLE == "Y")
                    {

                        YY = "V";
                        if (TAXRATE == "0")
                        {
                            NOTAX = "V";
                        }
                    }
                    else if (TAXABLE == "N")
                    {
                        NN = "V";
                    }




                    //line3
                    //line3
                    tc01 = new PdfPCell(new Paragraph("3", s07));//c01
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tc01.FixedHeight = 15f;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(INVOICE_DATE, s07));//c02
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(INVOICE_NO, s07));//c03
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sPROD_NAME, s07));//c04
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.PaddingTop = 0f;
                    tc01.PaddingBottom = 0f;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sQUANTITY, s07));//c05
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    //  tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph("", s07));//c06
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    // tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(IAMOUNT, s07));//c07
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //    tc01.BorderWidthLeft = 1;
                    //  tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(TAX, s07));//c08
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(YY, s07));//c09
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthRight = 1;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(NOTAX, s07));//c10
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthRight = 1;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(NN, s07));//c11
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    //tc01.BorderWidthRight = 3;
                    tbContent.AddCell(tc01);
                }
            }
            if (dtItem.Rows.Count < 5)
            {
                int xcount = 5 - dtItem.Rows.Count;
              //  xcount = xcount * 2;
                //line3
                tc01 = new PdfPCell(new Paragraph("", s07));//c01
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tc01.FixedHeight = 75f / 5  * xcount;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c02
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c03
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c04
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c05
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                //  tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c06
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                // tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c07
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //    tc01.BorderWidthLeft = 1;
                //  tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c08
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c09
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthRight = 1;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c10
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthRight = 1;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c11
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                //tc01.BorderWidthRight = 3;
                tbContent.AddCell(tc01);


            }
            //line4
            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 4;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("總      計", s08));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 2;
            tbContent.AddCell(tc01);
            if (totalpage != "1" && nowpage != "1")
            {
                SALEAMOUNT = "XX";
                SALETAX = "XX";
            }
            tc01 = new PdfPCell(new Paragraph(SALEAMOUNT, s07));
            tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph(SALETAX, s07));
            tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
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
            p01 = new Paragraph(@"                                                                                                                                       第二聯:交付原銷貨人作為記帳憑證", s06);
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
            string pageword = checkpage(nowpage, totalpage);
            p01 = new Paragraph("原進貨營業人(或原買受人)名稱:                       (簽章或姓名)               地址:                                     " + pageword, s08);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);

            p01 = new Paragraph("身分證字號/營利事業統一編號:  ", s08);
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
            PdfContentByte cb = writer1.DirectContent;
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

        private void Table03NOUNI(Document pDoc, string Store_NO, string SALE_NO, string CREDIT_NOTE_NO, DataTable dtItem, string nowpage, string totalpage)
        {
            //外框(pdfTable)，1欄
            //PdfPTable tbBorder = new PdfPTable(1);
            //tbBorder.DefaultCell.Border = 0;
            //tbBorder.SetTotalWidth(new float[] { 532f });
            //tbBorder.LockedWidth = true;
            //tbBorder.SpacingBefore = 0;
            //tbBorder.SpacingAfter = 0;
            string SUNINO = (dtItem.Rows[0]["SUNINO"] != DBNull.Value) ? dtItem.Rows[0]["SUNINO"].ToString() : "";
            string STORENAME = (dtItem.Rows[0]["STORENAME"] != DBNull.Value) ? dtItem.Rows[0]["STORENAME"].ToString() : "";
            string UNIADDR = (dtItem.Rows[0]["UNIADDR"] != DBNull.Value) ? dtItem.Rows[0]["UNIADDR"].ToString() : "";
            string INVALID_DATE = (dtItem.Rows[0]["INVALID_DATE"] != DBNull.Value) ? dtItem.Rows[0]["INVALID_DATE"].ToString() : "";
            string POS_RECEIPT_TITLE = (dtItem.Rows[0]["POS_RECEIPT_TITLE"] != DBNull.Value) ? dtItem.Rows[0]["POS_RECEIPT_TITLE"].ToString() : "";//發票抬頭
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
            tc01 = new PdfPCell(new Paragraph(POS_RECEIPT_TITLE, s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthTop = 0;
            tc01.BorderWidthLeft = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph(SUNINO, s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthTop = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph(UNIADDR, s06));
            tc01.FixedHeight = 15.118f;
            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 0f;
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
            tc01.PaddingLeft = 20f;

            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            //tc01.VerticalAlignment = Element.ALIGN_TOP;
            tbTemp01.AddCell(tc01);
            string yy = DateTime.Now.ToString("yyyy");
            string mm = Convert.ToString(DateTime.Today.Month);
            string dd = Convert.ToString(DateTime.Today.Day);
            //p1 = new Paragraph("中 華 民 國", s11);
            //DateTime dINVOICE_DATE = Convert.ToDateTime(INVOICE_DATE);
            ////p1.Add(dINVOICE_DATE.Year.ToString());
            //p1.Add((dINVOICE_DATE.Year - 1911).ToString().PadLeft(4, ' '));
            //p1.Add("年");
            //p1.Add(dINVOICE_DATE.Month.ToString().PadLeft(4, ' '));
            //p1.Add("月");
            //p1.Add(dINVOICE_DATE.Day.ToString().PadLeft(4, ' '));
            //p1.Add("日");
            //  string dd = DateTime.Now.ToString("dd");
            p01 = new Paragraph("中 華 民 國  ", s09);
            DateTime DebitNoteDate = Convert.ToDateTime(INVALID_DATE);
            //  p01.Add(DebitNoteDate.Year.ToString());
            p01.Add((DebitNoteDate.Year - 1911).ToString().PadLeft(2, ' '));
            p01.Add("  年");
            p01.Add(DebitNoteDate.Month.ToString().PadLeft(4, ' '));
            p01.Add("　月");
            p01.Add(DebitNoteDate.Day.ToString().PadLeft(4, ' '));
            p01.Add("　日");
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
            p01 = new Paragraph("                                                            頁次: " + nowpage + "/" + totalpage, s10);
            tc01 = new PdfPCell(p01);
            tc01.PaddingLeft = 100f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbTitle.AddCell(tc01);

            tc01 = new PdfPCell(tbTitle);
            tc01.PaddingBottom = 0;
            tc01.PaddingTop = 0;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbBorder.AddCell(tc01);

            //單號,3欄
            PdfPTable tbNO = new PdfPTable(new float[] { 3f, 0.75f, 1.55f, 0.75f, 1.75f, 0.75f, 1.25f });
            tbNO.DefaultCell.BorderWidthBottom = 0;
            tbNO.DefaultCell.BorderWidthLeft = 0;
            tbNO.DefaultCell.BorderWidthRight = 0;
            tbNO.DefaultCell.BorderWidthTop = 0;
            tbNO.AddCell(new Paragraph(""));
            tbNO.AddCell(new Paragraph("折讓單號：", s07));
            tbNO.AddCell(new Paragraph(CREDIT_NOTE_NO, s07));//折讓單號
            tc01 = new PdfPCell(new Paragraph("訂單編號：", s07));
         //   tc01.PaddingLeft = 17f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbNO.AddCell(tc01);
            tbNO.AddCell(new Paragraph(SALE_NO, s07));//訂單編號
            tbNO.AddCell(new Paragraph("門市代號：", s07));
            tbNO.AddCell(new Paragraph(Store_NO, s07));//門市代號
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

            string SALEAMOUNT = "0";
            string SALETAX = "0";
            if (dtItem != null && dtItem.Rows.Count > 0)
            {

                for (int j = 0; j < dtItem.Rows.Count; j++)
                {
                    DataRow _dr = dtItem.Rows[j];
                    SALEAMOUNT = (_dr["SALE_AMOUNT"] != DBNull.Value) ? _dr["SALE_AMOUNT"].ToString() : "";
                    SALETAX = (_dr["TAX"] != DBNull.Value) ? _dr["TAX"].ToString() : "";
                    string sPROD_NAME = (_dr["PROD_NAME"] != DBNull.Value) ? _dr["PROD_NAME"].ToString() : "";
                    string sQUANTITY = (_dr["QUANTITY"] != DBNull.Value) ? _dr["QUANTITY"].ToString() : "";
                    string sPRICE = (_dr["PRICE"] != DBNull.Value) ? _dr["PRICE"].ToString() : "";
                    string sAMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                    string INVOICE_DATE = (_dr["INVOICE_DATE"] != DBNull.Value) ? _dr["INVOICE_DATE"].ToString() : "";
                    string INVOICE_NO = (_dr["INVOICE_NO"] != DBNull.Value) ? _dr["INVOICE_NO"].ToString() : "";
                    string AMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                    string TAX = (_dr["ITEMTAX"] != DBNull.Value) ? _dr["ITEMTAX"].ToString() : "";
                    string TAXABLE = (_dr["TAXABLE"] != DBNull.Value) ? _dr["TAXABLE"].ToString() : "";
                    string TAXRATE = (_dr["TAXRATE"] != DBNull.Value) ? _dr["TAXRATE"].ToString() : "";
                    string IAMOUNT = (_dr["IAMOUNT"] != DBNull.Value) ? _dr["IAMOUNT"].ToString() : "0";
                    double dTmp = Convert.ToDouble(sPRICE);
                    int iprice = Convert.ToInt32(Math.Round(dTmp / 1.05, 0, MidpointRounding.AwayFromZero));
                    sPRICE = StringUtil.NumberFormat(iprice, 0, true);
                    string YY = "";
                    string NN = "";
                    string NOTAX = "";
                    if (TAXABLE == "Y")
                    {

                        YY = "V";
                        if (TAXRATE == "0")
                        {
                            NOTAX = "V";
                        }
                    }
                    else if (TAXABLE == "N")
                    {
                        NN = "V";
                    }




                    //line3
                    //line3
                    tc01 = new PdfPCell(new Paragraph("3", s07));//c01
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tc01.FixedHeight = 15f;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(INVOICE_DATE, s07));//c02
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(INVOICE_NO, s07));//c03
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sPROD_NAME, s07));//c04
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.PaddingTop = 0f;
                    tc01.PaddingBottom = 0f;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sQUANTITY, s07));//c05
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    //  tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph("", s07));//c06
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    // tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(IAMOUNT, s07));//c07
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //    tc01.BorderWidthLeft = 1;
                    //  tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(TAX, s07));//c08
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(YY, s07));//c09
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthRight = 1;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(NOTAX, s07));//c10
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthRight = 1;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(NN, s07));//c11
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    //tc01.BorderWidthRight = 3;
                    tbContent.AddCell(tc01);
                }
            }
            if (dtItem.Rows.Count < 5)
            {
                int xcount = 5 - dtItem.Rows.Count;
               // xcount = xcount * 2;
                //line3
                tc01 = new PdfPCell(new Paragraph("", s05));//c01
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tc01.FixedHeight = 75f / 5 * xcount;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c02
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c03
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c04
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c05
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                //  tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c06
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                // tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c07
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //    tc01.BorderWidthLeft = 1;
                //  tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c08
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c09
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthRight = 1;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c10
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthRight = 1;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c11
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                //tc01.BorderWidthRight = 3;
                tbContent.AddCell(tc01);


            }
            //line4
            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 4;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("總      計", s08));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 2;
            tbContent.AddCell(tc01);
            if (totalpage != "1" && nowpage != "1")
            {
                SALEAMOUNT = "XX";
                SALETAX = "XX";
            }
            tc01 = new PdfPCell(new Paragraph(SALEAMOUNT, s07));
            tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph(SALETAX, s07));
            tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
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
            string pageword = checkpage(nowpage, totalpage);
            p01 = new Paragraph("原進貨營業人(或原買受人)名稱:                       (簽章或姓名)               地址:                                     " + pageword, s08);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);

            p01 = new Paragraph("身分證字號/營利事業統一編號:  ", s08);
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
            PdfContentByte cb = writer1.DirectContent;
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

        private void Table04NOUNI(Document pDoc, string Store_NO, string SALE_NO, string CREDIT_NOTE_NO, DataTable dtItem, string nowpage, string totalpage)
        {
            //外框(pdfTable)，1欄
            //PdfPTable tbBorder = new PdfPTable(1);
            //tbBorder.DefaultCell.Border = 0;
            //tbBorder.SetTotalWidth(new float[] { 532f });
            //tbBorder.LockedWidth = true;
            //tbBorder.SpacingBefore = 0;
            //tbBorder.SpacingAfter = 0;
            string SUNINO = (dtItem.Rows[0]["SUNINO"] != DBNull.Value) ? dtItem.Rows[0]["SUNINO"].ToString() : "";
            string STORENAME = (dtItem.Rows[0]["STORENAME"] != DBNull.Value) ? dtItem.Rows[0]["STORENAME"].ToString() : "";
            string UNIADDR = (dtItem.Rows[0]["UNIADDR"] != DBNull.Value) ? dtItem.Rows[0]["UNIADDR"].ToString() : "";
            string INVALID_DATE = (dtItem.Rows[0]["INVALID_DATE"] != DBNull.Value) ? dtItem.Rows[0]["INVALID_DATE"].ToString() : "";
            string POS_RECEIPT_TITLE = (dtItem.Rows[0]["POS_RECEIPT_TITLE"] != DBNull.Value) ? dtItem.Rows[0]["POS_RECEIPT_TITLE"].ToString() : "";//發票抬頭
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
            tc01 = new PdfPCell(new Paragraph(POS_RECEIPT_TITLE, s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthTop = 0;
            tc01.BorderWidthLeft = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph(SUNINO, s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthTop = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph(UNIADDR, s06));
            tc01.FixedHeight = 15.118f;
            tc01.PaddingBottom = 0f;
            tc01.PaddingTop = 0f;
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
            tc01.PaddingLeft = 20f;

            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            //tc01.VerticalAlignment = Element.ALIGN_TOP;
            tbTemp01.AddCell(tc01);

            p01 = new Paragraph("中 華 民 國  ", s09);
            DateTime DebitNoteDate = Convert.ToDateTime(INVALID_DATE);
            //  p01.Add(DebitNoteDate.Year.ToString());
            p01.Add((DebitNoteDate.Year - 1911).ToString().PadLeft(2, ' '));
            p01.Add("  年");
            p01.Add(DebitNoteDate.Month.ToString().PadLeft(4, ' '));
            p01.Add("　月");
            p01.Add(DebitNoteDate.Day.ToString().PadLeft(4, ' '));
            p01.Add("　日");
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
            p01 = new Paragraph("                                                            頁次: " + nowpage + "/" + totalpage, s10);
            tc01 = new PdfPCell(p01);
            tc01.PaddingLeft = 100f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbTitle.AddCell(tc01);

            tc01 = new PdfPCell(tbTitle);
            tc01.PaddingBottom = 0;
            tc01.PaddingTop = 0;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbBorder.AddCell(tc01);

            //單號,3欄
            PdfPTable tbNO = new PdfPTable(new float[] { 3f, 0.75f, 1.55f, 0.75f, 1.75f, 0.75f, 1.25f });
            tbNO.DefaultCell.BorderWidthBottom = 0;
            tbNO.DefaultCell.BorderWidthLeft = 0;
            tbNO.DefaultCell.BorderWidthRight = 0;
            tbNO.DefaultCell.BorderWidthTop = 0;
            tbNO.AddCell(new Paragraph(""));
            tbNO.AddCell(new Paragraph("折讓單號：", s07));
            tbNO.AddCell(new Paragraph(CREDIT_NOTE_NO, s07));//折讓單號
            tc01 = new PdfPCell(new Paragraph("訂單編號：", s07));
           // tc01.PaddingLeft = 17f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbNO.AddCell(tc01);
            tbNO.AddCell(new Paragraph(SALE_NO, s07));//訂單編號
            tbNO.AddCell(new Paragraph("門市代號：", s07));
            tbNO.AddCell(new Paragraph(Store_NO, s07));//門市代號
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

            string SALEAMOUNT = "0";
            string SALETAX = "0";
            if (dtItem != null && dtItem.Rows.Count > 0)
            {

                for (int j = 0; j < dtItem.Rows.Count; j++)
                {
                    DataRow _dr = dtItem.Rows[j];
                    SALEAMOUNT = (_dr["SALE_AMOUNT"] != DBNull.Value) ? _dr["SALE_AMOUNT"].ToString() : "";
                    SALETAX = (_dr["TAX"] != DBNull.Value) ? _dr["TAX"].ToString() : "";
                    string sPROD_NAME = (_dr["PROD_NAME"] != DBNull.Value) ? _dr["PROD_NAME"].ToString() : "";
                    string sQUANTITY = (_dr["QUANTITY"] != DBNull.Value) ? _dr["QUANTITY"].ToString() : "";
                    string sPRICE = (_dr["PRICE"] != DBNull.Value) ? _dr["PRICE"].ToString() : "";
                    string sAMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                    string INVOICE_DATE = (_dr["INVOICE_DATE"] != DBNull.Value) ? _dr["INVOICE_DATE"].ToString() : "";
                    string INVOICE_NO = (_dr["INVOICE_NO"] != DBNull.Value) ? _dr["INVOICE_NO"].ToString() : "";
                    string AMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                    string TAX = (_dr["ITEMTAX"] != DBNull.Value) ? _dr["ITEMTAX"].ToString() : "";
                    string TAXABLE = (_dr["TAXABLE"] != DBNull.Value) ? _dr["TAXABLE"].ToString() : "";
                    string TAXRATE = (_dr["TAXRATE"] != DBNull.Value) ? _dr["TAXRATE"].ToString() : "";
                    string IAMOUNT = (_dr["IAMOUNT"] != DBNull.Value) ? _dr["IAMOUNT"].ToString() : "0";
                    double dTmp = Convert.ToDouble(sPRICE);
                    int iprice = Convert.ToInt32(Math.Round(dTmp / 1.05, 0, MidpointRounding.AwayFromZero));
                    sPRICE = StringUtil.NumberFormat(iprice, 0, true);
                    string YY = "";
                    string NN = "";
                    string NOTAX = "";
                    if (TAXABLE == "Y")
                    {

                        YY = "V";
                        if (TAXRATE == "0")
                        {
                            NOTAX = "V";
                        }
                    }
                    else if (TAXABLE == "N")
                    {
                        NN = "V";
                    }




                    //line3
                    //line3
                    tc01 = new PdfPCell(new Paragraph("3", s07));//c01
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tc01.FixedHeight = 15f;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(INVOICE_DATE, s07));//c02
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(INVOICE_NO, s07));//c03
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sPROD_NAME, s07));//c04
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.PaddingTop = 0f;
                    tc01.PaddingBottom = 0f;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sQUANTITY, s07));//c05
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    //  tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph("", s07));//c06
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    // tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(IAMOUNT, s07));//c07
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //    tc01.BorderWidthLeft = 1;
                    //  tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(TAX, s07));//c08
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(YY, s07));//c09
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthRight = 1;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(NOTAX, s07));//c10
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthRight = 1;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(NN, s07));//c11
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    //tc01.BorderWidthRight = 3;
                    tbContent.AddCell(tc01);
                }
            }
            if (dtItem.Rows.Count < 5)
            {
                int xcount = 5 - dtItem.Rows.Count;
              //  xcount = xcount * 2;
                //line3
                tc01 = new PdfPCell(new Paragraph("", s05));//c01
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tc01.FixedHeight = 75f / 5 * xcount;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c02
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c03
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c04
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c05
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                //  tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c06
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                // tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c07
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //    tc01.BorderWidthLeft = 1;
                //  tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c08
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c09
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthRight = 1;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c10
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthRight = 1;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c11
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                //tc01.BorderWidthRight = 3;
                tbContent.AddCell(tc01);


            }
            //line4
            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 4;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("總      計", s08));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 2;
            tbContent.AddCell(tc01);
            if (totalpage != "1" && nowpage != "1")
            {
                SALEAMOUNT = "XX";
                SALETAX = "XX";
            }
            tc01 = new PdfPCell(new Paragraph(SALEAMOUNT, s07));
            tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph(SALETAX, s07));
            tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
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
            string pageword = checkpage(nowpage, totalpage);
            p01 = new Paragraph("原進貨營業人(或原買受人)名稱:                       (簽章或姓名)               地址:                                     " + pageword, s08);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);

            p01 = new Paragraph("身分證字號/營利事業統一編號:  ", s08);
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



        private void Table01D(Document pDoc, string Store_NO, string SALE_NO, string CREDIT_NOTE_NO, DataTable dtItem, string UNITITLE, string UNINO)
        {
            //外框(pdfTable)，1欄
            //PdfPTable tbBorder = new PdfPTable(1);
            //tbBorder.DefaultCell.Border = 0;
            //tbBorder.SetTotalWidth(new float[] { 532f });
            //tbBorder.LockedWidth = true;
            //tbBorder.SpacingBefore = 0;
            //tbBorder.SpacingAfter = 0;
            string SUNINO = (dtItem.Rows[0]["SUNINO"] != DBNull.Value) ? dtItem.Rows[0]["SUNINO"].ToString() : "";
            string STORENAME = (dtItem.Rows[0]["STORENAME"] != DBNull.Value) ? dtItem.Rows[0]["STORENAME"].ToString() : "";
            string UNIADDR = (dtItem.Rows[0]["UNIADDR"] != DBNull.Value) ? dtItem.Rows[0]["UNIADDR"].ToString() : "";
            string INVALID_DATE = (dtItem.Rows[0]["INVALID_DATE"] != DBNull.Value) ? dtItem.Rows[0]["INVALID_DATE"].ToString() : "";
            string POS_RECEIPT_TITLE = (dtItem.Rows[0]["POS_RECEIPT_TITLE"] != DBNull.Value) ? dtItem.Rows[0]["POS_RECEIPT_TITLE"].ToString() : "";//發票抬頭

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
            tc01 = new PdfPCell(new Paragraph(POS_RECEIPT_TITLE, s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthTop = 0;
            tc01.BorderWidthLeft = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph(SUNINO, s06));
            tc01.FixedHeight = 15.118f;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthTop = 0;
            tbTemp02.AddCell(tc01);
            tc01 = new PdfPCell(new Paragraph(UNIADDR, s06));
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

            p01 = new Paragraph("中 華 民 國  ", s09);
            DateTime DebitNoteDate = Convert.ToDateTime(INVALID_DATE);
            //  p01.Add(DebitNoteDate.Year.ToString());
            p01.Add((DebitNoteDate.Year - 1911).ToString().PadLeft(4, ' '));
            p01.Add("年");
            p01.Add(DebitNoteDate.Month.ToString().PadLeft(4, ' '));
            p01.Add("　月");
            p01.Add(DebitNoteDate.Day.ToString().PadLeft(4, ' '));
            p01.Add("　日");
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
            PdfPTable tbNO = new PdfPTable(new float[] {3f, 1f, 1.3f, 1f, 1.5f, 1f, 1f});
            tbNO.DefaultCell.BorderWidthBottom = 0;
            tbNO.DefaultCell.BorderWidthLeft = 0;
            tbNO.DefaultCell.BorderWidthRight = 0;
            tbNO.DefaultCell.BorderWidthTop = 0;
            tbNO.AddCell(new Paragraph(""));
            tbNO.AddCell(new Paragraph("折讓單號：", s07));
            tbNO.AddCell(new Paragraph(CREDIT_NOTE_NO, s07));//折讓單號
            tc01 = new PdfPCell(new Paragraph("訂單編號：", s07));
            tc01.PaddingLeft = 17f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbNO.AddCell(tc01);
            tbNO.AddCell(new Paragraph(SALE_NO, s07));//訂單編號
            tbNO.AddCell(new Paragraph("門市代號：", s07));
            tbNO.AddCell(new Paragraph(Store_NO, s07));//門市代號
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
            // tbContent.LockedWidth = true;
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
            string SALEAMOUNT = "0";
            string SALETAX = "0";
            if (dtItem != null && dtItem.Rows.Count > 0)
            {

                for (int j = 0; j < dtItem.Rows.Count; j++)
                {
                    DataRow _dr = dtItem.Rows[j];
                    SALEAMOUNT = (_dr["SALE_AMOUNT"] != DBNull.Value) ? _dr["SALE_AMOUNT"].ToString() : "";
                    SALETAX = (_dr["TAX"] != DBNull.Value) ? _dr["TAX"].ToString() : "";
                    string sPROD_NAME = (_dr["PROD_NAME"] != DBNull.Value) ? _dr["PROD_NAME"].ToString() : "";
                    string sQUANTITY = (_dr["QUANTITY"] != DBNull.Value) ? _dr["QUANTITY"].ToString() : "";
                    string sPRICE = (_dr["PRICE"] != DBNull.Value) ? _dr["PRICE"].ToString() : "";
                    string sAMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                    string INVOICE_DATE = (_dr["INVOICE_DATE"] != DBNull.Value) ? _dr["INVOICE_DATE"].ToString() : "";
                    string INVOICE_NO = (_dr["INVOICE_NO"] != DBNull.Value) ? _dr["INVOICE_NO"].ToString() : "";
                    string AMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                    string TAX = (_dr["ITEMTAX"] != DBNull.Value) ? _dr["ITEMTAX"].ToString() : "";
                    string TAXABLE = (_dr["TAXABLE"] != DBNull.Value) ? _dr["TAXABLE"].ToString() : "";
                    string TAXRATE = (_dr["TAXRATE"] != DBNull.Value) ? _dr["TAXRATE"].ToString() : "";
                    string IAMOUNT = (_dr["IAMOUNT"] != DBNull.Value) ? _dr["IAMOUNT"].ToString() : "0";
                    string YY = "";
                    string NN = "";
                    string NOTAX = "";
                    if (TAXABLE == "Y")
                    {

                        YY = "V";
                        if (TAXRATE == "0")
                        {
                            NOTAX = "V";
                        }
                    }
                    else if (TAXABLE == "N")
                    {
                        NN = "V";
                    }




                    //line3
                    tc01 = new PdfPCell(new Paragraph("3", s05));//c01
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    //   tc01.FixedHeight = 70f/2;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(INVOICE_DATE, s05));//c02
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(INVOICE_NO, s05));//c03
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sPROD_NAME, s05));//c04
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sQUANTITY, s05));//c05
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    //  tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph("", s05));//c06
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    // tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(IAMOUNT, s05));//c07
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //    tc01.BorderWidthLeft = 1;
                    //  tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(TAX, s05));//c08
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(YY, s05));//c09
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthRight = 1;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(NOTAX, s05));//c10
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthRight = 1;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(NN, s05));//c11
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    //tc01.BorderWidthRight = 3;
                    tbContent.AddCell(tc01);
                }
            }
            if (dtItem.Rows.Count < 5)
            {
                int xcount = 5 - dtItem.Rows.Count;
                xcount = xcount * 2;
                //line3
                tc01 = new PdfPCell(new Paragraph("", s05));//c01
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tc01.FixedHeight = 75f / 10 * xcount;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c02
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c03
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c04
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c05
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                //  tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c06
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                // tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c07
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //    tc01.BorderWidthLeft = 1;
                //  tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c08
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c09
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthRight = 1;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c10
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthRight = 1;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s05));//c11
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                //tc01.BorderWidthRight = 3;
                tbContent.AddCell(tc01);


            }
            //line4
            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 4;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("總      計", s08));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 2;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph(SALEAMOUNT, s07));
            tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph(SALETAX, s07));
            tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
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

            p01 = new Paragraph("原進貨營業人(或原買受人)名稱: " + UNITITLE + "                      (簽章或姓名)               地址:", s08);
            tc01 = new PdfPCell(p01);
            tc01.BorderWidth = 0;
            tc01.PaddingTop = 0;
            tc01.PaddingBottom = 0;
            tbFooter.AddCell(tc01);

            p01 = new Paragraph("身分證字號/營利事業統一編號:  " + UNINO, s08);
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

        private void Table02D(Document pDoc, string Store_NO, string SALE_NO, string CREDIT_NOTE_NO, DataTable dtItem, string UNITITLE, string UNINO)
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
            p01 = new Paragraph("中 華 民 國  ", s09);
            DateTime DebitNoteDate = DateTime.Now;
            //  p01.Add(DebitNoteDate.Year.ToString());
            p01.Add((DebitNoteDate.Year - 1911).ToString().PadLeft(4, ' '));
            p01.Add("年");
            p01.Add(DebitNoteDate.Month.ToString().PadLeft(4, ' '));
            p01.Add("　月");
            p01.Add(DebitNoteDate.Day.ToString().PadLeft(4, ' '));
            p01.Add("　日");
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
            PdfPTable tbNO = new PdfPTable(new float[] {3f, 1f, 1.3f, 1f, 1.5f, 1f, 1f});
            tbNO.DefaultCell.BorderWidthBottom = 0;
            tbNO.DefaultCell.BorderWidthLeft = 0;
            tbNO.DefaultCell.BorderWidthRight = 0;
            tbNO.DefaultCell.BorderWidthTop = 0;
            tbNO.AddCell(new Paragraph(""));
            tbNO.AddCell(new Paragraph("折讓單號：", s07));
            tbNO.AddCell(new Paragraph(CREDIT_NOTE_NO, s07));//折讓單號
            tc01 = new PdfPCell(new Paragraph("訂單編號：", s07));
            tc01.PaddingLeft = 17f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbNO.AddCell(tc01);
            tbNO.AddCell(new Paragraph(SALE_NO, s07));//訂單編號
            tbNO.AddCell(new Paragraph("門市代號：", s07));
            tbNO.AddCell(new Paragraph(Store_NO, s07));//門市代號
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

            string SALEAMOUNT = "0";
            string SALETAX = "0";
            if (dtItem != null && dtItem.Rows.Count > 0)
            {

                for (int j = 0; j < dtItem.Rows.Count; j++)
                {
                    DataRow _dr = dtItem.Rows[j];
                    SALEAMOUNT = (_dr["SALE_AMOUNT"] != DBNull.Value) ? _dr["SALE_AMOUNT"].ToString() : "";
                    SALETAX = (_dr["TAX"] != DBNull.Value) ? _dr["TAX"].ToString() : "";
                    string sPROD_NAME = (_dr["PROD_NAME"] != DBNull.Value) ? _dr["PROD_NAME"].ToString() : "";
                    string sQUANTITY = (_dr["QUANTITY"] != DBNull.Value) ? _dr["QUANTITY"].ToString() : "";
                    string sPRICE = (_dr["PRICE"] != DBNull.Value) ? _dr["PRICE"].ToString() : "";
                    string sAMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                    string INVOICE_DATE = (_dr["INVOICE_DATE"] != DBNull.Value) ? _dr["INVOICE_DATE"].ToString() : "";
                    string INVOICE_NO = (_dr["INVOICE_NO"] != DBNull.Value) ? _dr["INVOICE_NO"].ToString() : "";
                    string AMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                    string TAX = (_dr["ITEMTAX"] != DBNull.Value) ? _dr["ITEMTAX"].ToString() : "";
                    string TAXABLE = (_dr["TAXABLE"] != DBNull.Value) ? _dr["TAXABLE"].ToString() : "";
                    string TAXRATE = (_dr["TAXRATE"] != DBNull.Value) ? _dr["TAXRATE"].ToString() : "";
                    string YY = "";
                    string NN = "";
                    string NOTAX = "";
                    if (TAXABLE == "Y")
                    {

                        YY = "V";
                        if (TAXRATE == "0")
                        {
                            NOTAX = "V";
                        }
                    }
                    else if (TAXABLE == "N")
                    {
                        NN = "V";
                    }




                    //line3
                    //line3
                    tc01 = new PdfPCell(new Paragraph("3", s07));//c01
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    //   tc01.FixedHeight = 70f/2;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(INVOICE_DATE, s07));//c02
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(INVOICE_NO, s07));//c03
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sPROD_NAME, s07));//c04
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sQUANTITY, s07));//c05
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    //  tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sPRICE, s07));//c06
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    // tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(AMOUNT, s07));//c07
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //    tc01.BorderWidthLeft = 1;
                    //  tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(TAX, s07));//c08
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(YY, s07));//c09
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthRight = 1;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(NOTAX, s07));//c10
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthRight = 1;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(NN, s07));//c11
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    //tc01.BorderWidthRight = 3;
                    tbContent.AddCell(tc01);
                }
            }
            if (dtItem.Rows.Count < 5)
            {
                int xcount = 5 - dtItem.Rows.Count;

                //line3
                tc01 = new PdfPCell(new Paragraph("", s07));//c01
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tc01.FixedHeight = 75f / 5 * xcount;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c02
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c03
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c04
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c05
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                //  tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c06
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                // tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c07
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //    tc01.BorderWidthLeft = 1;
                //  tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c08
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c09
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthRight = 1;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c10
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthRight = 1;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c11
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                //tc01.BorderWidthRight = 3;
                tbContent.AddCell(tc01);


            }
            //line4
            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 4;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("總      計", s08));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 2;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph(SALEAMOUNT, s07));
            tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph(SALETAX, s07));
            tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
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

        private void Table03D(Document pDoc, string Store_NO, string SALE_NO, string CREDIT_NOTE_NO, DataTable dtItem, string UNITITLE, string UNINO)
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
            string yy = DateTime.Now.ToString("yyyy");
            string mm = Convert.ToString(DateTime.Today.Month);
            string dd = Convert.ToString(DateTime.Today.Day);
            //p1 = new Paragraph("中 華 民 國", s11);
            //DateTime dINVOICE_DATE = Convert.ToDateTime(INVOICE_DATE);
            ////p1.Add(dINVOICE_DATE.Year.ToString());
            //p1.Add((dINVOICE_DATE.Year - 1911).ToString().PadLeft(4, ' '));
            //p1.Add("年");
            //p1.Add(dINVOICE_DATE.Month.ToString().PadLeft(4, ' '));
            //p1.Add("月");
            //p1.Add(dINVOICE_DATE.Day.ToString().PadLeft(4, ' '));
            //p1.Add("日");
            //  string dd = DateTime.Now.ToString("dd");
            p01 = new Paragraph("中 華 民 國  ", s09);
            DateTime DebitNoteDate = DateTime.Now;
            //  p01.Add(DebitNoteDate.Year.ToString());
            p01.Add((DebitNoteDate.Year - 1911).ToString().PadLeft(4, ' '));
            p01.Add("年");
            p01.Add(DebitNoteDate.Month.ToString().PadLeft(4, ' '));
            p01.Add("　月");
            p01.Add(DebitNoteDate.Day.ToString().PadLeft(4, ' '));
            p01.Add("　日");
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
            PdfPTable tbNO = new PdfPTable(new float[] {3f, 1f, 1.3f, 1f, 1.5f, 1f, 1f});
            tbNO.DefaultCell.BorderWidthBottom = 0;
            tbNO.DefaultCell.BorderWidthLeft = 0;
            tbNO.DefaultCell.BorderWidthRight = 0;
            tbNO.DefaultCell.BorderWidthTop = 0;
            tbNO.AddCell(new Paragraph(""));
            tbNO.AddCell(new Paragraph("折讓單號：", s07));
            tbNO.AddCell(new Paragraph(CREDIT_NOTE_NO, s07));//折讓單號
            tc01 = new PdfPCell(new Paragraph("訂單編號：", s07));
            tc01.PaddingLeft = 17f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbNO.AddCell(tc01);
            tbNO.AddCell(new Paragraph(SALE_NO, s07));//訂單編號
            tbNO.AddCell(new Paragraph("門市代號：", s07));
            tbNO.AddCell(new Paragraph(Store_NO, s07));//門市代號
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

            string SALEAMOUNT = "0";
            string SALETAX = "0";
            if (dtItem != null && dtItem.Rows.Count > 0)
            {

                for (int j = 0; j < dtItem.Rows.Count; j++)
                {
                    DataRow _dr = dtItem.Rows[j];
                    SALEAMOUNT = (_dr["SALE_AMOUNT"] != DBNull.Value) ? _dr["SALE_AMOUNT"].ToString() : "";
                    SALETAX = (_dr["TAX"] != DBNull.Value) ? _dr["TAX"].ToString() : "";
                    string sPROD_NAME = (_dr["PROD_NAME"] != DBNull.Value) ? _dr["PROD_NAME"].ToString() : "";
                    string sQUANTITY = (_dr["QUANTITY"] != DBNull.Value) ? _dr["QUANTITY"].ToString() : "";
                    string sPRICE = (_dr["PRICE"] != DBNull.Value) ? _dr["PRICE"].ToString() : "";
                    string sAMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                    string INVOICE_DATE = (_dr["INVOICE_DATE"] != DBNull.Value) ? _dr["INVOICE_DATE"].ToString() : "";
                    string INVOICE_NO = (_dr["INVOICE_NO"] != DBNull.Value) ? _dr["INVOICE_NO"].ToString() : "";
                    string AMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                    string TAX = (_dr["ITEMTAX"] != DBNull.Value) ? _dr["ITEMTAX"].ToString() : "";
                    string TAXABLE = (_dr["TAXABLE"] != DBNull.Value) ? _dr["TAXABLE"].ToString() : "";
                    string TAXRATE = (_dr["TAXRATE"] != DBNull.Value) ? _dr["TAXRATE"].ToString() : "";
                    string YY = "";
                    string NN = "";
                    string NOTAX = "";
                    if (TAXABLE == "Y")
                    {

                        YY = "V";
                        if (TAXRATE == "0")
                        {
                            NOTAX = "V";
                        }
                    }
                    else if (TAXABLE == "N")
                    {
                        NN = "V";
                    }




                    //line3
                    //line3
                    tc01 = new PdfPCell(new Paragraph("3", s07));//c01
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    //   tc01.FixedHeight = 70f/2;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(INVOICE_DATE, s07));//c02
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(INVOICE_NO, s07));//c03
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sPROD_NAME, s07));//c04
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sQUANTITY, s07));//c05
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    //  tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sPRICE, s07));//c06
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    // tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(AMOUNT, s07));//c07
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //    tc01.BorderWidthLeft = 1;
                    //  tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(TAX, s07));//c08
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(YY, s07));//c09
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthRight = 1;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(NOTAX, s07));//c10
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthRight = 1;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(NN, s07));//c11
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    //tc01.BorderWidthRight = 3;
                    tbContent.AddCell(tc01);
                }
            }
            if (dtItem.Rows.Count < 5)
            {
                int xcount = 5 - dtItem.Rows.Count;

                //line3
                tc01 = new PdfPCell(new Paragraph("", s07));//c01
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tc01.FixedHeight = 75f / 5 * xcount;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c02
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c03
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c04
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c05
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                //  tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c06
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                // tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c07
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //    tc01.BorderWidthLeft = 1;
                //  tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c08
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c09
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthRight = 1;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c10
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthRight = 1;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c11
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                //tc01.BorderWidthRight = 3;
                tbContent.AddCell(tc01);


            }
            //line4
            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 4;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("總      計", s08));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 2;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph(SALEAMOUNT, s07));
            tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph(SALETAX, s07));
            tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
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

        private void Table04D(Document pDoc, string Store_NO, string SALE_NO, string CREDIT_NOTE_NO, DataTable dtItem, string UNITITLE, string UNINO)
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

            p01 = new Paragraph("中 華 民 國  ", s09);
            DateTime DebitNoteDate = DateTime.Now;
            //  p01.Add(DebitNoteDate.Year.ToString());
            p01.Add((DebitNoteDate.Year - 1911).ToString().PadLeft(4, ' '));
            p01.Add("年");
            p01.Add(DebitNoteDate.Month.ToString().PadLeft(4, ' '));
            p01.Add("　月");
            p01.Add(DebitNoteDate.Day.ToString().PadLeft(4, ' '));
            p01.Add("　日");
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
            PdfPTable tbNO = new PdfPTable(new float[] {3f, 1f, 1.3f, 1f, 1.5f, 1f, 1f});
            tbNO.DefaultCell.BorderWidthBottom = 0;
            tbNO.DefaultCell.BorderWidthLeft = 0;
            tbNO.DefaultCell.BorderWidthRight = 0;
            tbNO.DefaultCell.BorderWidthTop = 0;
            tbNO.AddCell(new Paragraph(""));
            tbNO.AddCell(new Paragraph("折讓單號：", s07));
            tbNO.AddCell(new Paragraph(CREDIT_NOTE_NO, s07));//折讓單號
            tc01 = new PdfPCell(new Paragraph("訂單編號：", s07));
            tc01.PaddingLeft = 17f;
            tc01.BorderWidthBottom = 0;
            tc01.BorderWidthLeft = 0;
            tc01.BorderWidthRight = 0;
            tc01.BorderWidthTop = 0;
            tbNO.AddCell(tc01);
            tbNO.AddCell(new Paragraph(SALE_NO, s07));//訂單編號
            tbNO.AddCell(new Paragraph("門市代號：", s07));
            tbNO.AddCell(new Paragraph(Store_NO, s07));//門市代號
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

            string SALEAMOUNT = "0";
            string SALETAX = "0";
            if (dtItem != null && dtItem.Rows.Count > 0)
            {

                for (int j = 0; j < dtItem.Rows.Count; j++)
                {
                    DataRow _dr = dtItem.Rows[j];
                    SALEAMOUNT = (_dr["SALE_AMOUNT"] != DBNull.Value) ? _dr["SALE_AMOUNT"].ToString() : "";
                    SALETAX = (_dr["TAX"] != DBNull.Value) ? _dr["TAX"].ToString() : "";
                    string sPROD_NAME = (_dr["PROD_NAME"] != DBNull.Value) ? _dr["PROD_NAME"].ToString() : "";
                    string sQUANTITY = (_dr["QUANTITY"] != DBNull.Value) ? _dr["QUANTITY"].ToString() : "";
                    string sPRICE = (_dr["PRICE"] != DBNull.Value) ? _dr["PRICE"].ToString() : "";
                    string sAMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                    string INVOICE_DATE = (_dr["INVOICE_DATE"] != DBNull.Value) ? _dr["INVOICE_DATE"].ToString() : "";
                    string INVOICE_NO = (_dr["INVOICE_NO"] != DBNull.Value) ? _dr["INVOICE_NO"].ToString() : "";
                    string AMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "";
                    string TAX = (_dr["ITEMTAX"] != DBNull.Value) ? _dr["ITEMTAX"].ToString() : "";
                    string TAXABLE = (_dr["TAXABLE"] != DBNull.Value) ? _dr["TAXABLE"].ToString() : "";
                    string TAXRATE = (_dr["TAXRATE"] != DBNull.Value) ? _dr["TAXRATE"].ToString() : "";
                    string YY = "";
                    string NN = "";
                    string NOTAX = "";
                    if (TAXABLE == "Y")
                    {

                        YY = "V";
                        if (TAXRATE == "0")
                        {
                            NOTAX = "V";
                        }
                    }
                    else if (TAXABLE == "N")
                    {
                        NN = "V";
                    }




                    //line3
                    //line3
                    tc01 = new PdfPCell(new Paragraph("3", s07));//c01
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    //   tc01.FixedHeight = 70f/2;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(INVOICE_DATE, s07));//c02
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(INVOICE_NO, s07));//c03
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sPROD_NAME, s07));//c04
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sQUANTITY, s07));//c05
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    //  tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(sPRICE, s07));//c06
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    // tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(AMOUNT, s07));//c07
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //    tc01.BorderWidthLeft = 1;
                    //  tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(TAX, s07));//c08
                    tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(YY, s07));//c09
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthRight = 1;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(NOTAX, s07));//c10
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthRight = 1;
                    tc01.BorderWidthRight = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    tbContent.AddCell(tc01);
                    tc01 = new PdfPCell(new Paragraph(NN, s07));//c11
                    tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                    tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                    //tc01.BorderWidthLeft = 0;
                    tc01.BorderWidthTop = 0;
                    tc01.BorderWidthBottom = 0;
                    //tc01.BorderWidthRight = 3;
                    tbContent.AddCell(tc01);
                }
            }
            if (dtItem.Rows.Count < 5)
            {
                int xcount = 5 - dtItem.Rows.Count;

                //line3
                tc01 = new PdfPCell(new Paragraph("", s07));//c01
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tc01.FixedHeight = 75f / 5 * xcount;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c02
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c03
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c04
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c05
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                //  tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c06
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                // tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c07
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //    tc01.BorderWidthLeft = 1;
                //  tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c08
                tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c09
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthRight = 1;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c10
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthRight = 1;
                tc01.BorderWidthRight = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                tbContent.AddCell(tc01);
                tc01 = new PdfPCell(new Paragraph("", s07));//c11
                tc01.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                tc01.BorderColorTop = iTextSharp.text.BaseColor.WHITE;
                //tc01.BorderWidthLeft = 0;
                tc01.BorderWidthTop = 0;
                tc01.BorderWidthBottom = 0;
                //tc01.BorderWidthRight = 3;
                tbContent.AddCell(tc01);


            }
            //line4
            tc01 = new PdfPCell(new Paragraph("", s07));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 4;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph("總      計", s08));
            tc01.HorizontalAlignment = Element.ALIGN_CENTER;
            tc01.Colspan = 2;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph(SALEAMOUNT, s07));
            tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
            tbContent.AddCell(tc01);

            tc01 = new PdfPCell(new Paragraph(SALETAX, s07));
            tc01.HorizontalAlignment = Element.ALIGN_RIGHT;
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

        public string generateZeroReceipt(string POSUUID_MASTER, string printname)
        {
            filename = "";
            try
            {
                //取得資料
                DataTable dtOrg = getPROMOData(POSUUID_MASTER);

                //20110127 發票檔名(年度+發票號碼)
                string sInvoiceNo = "";
                string sStoreNo = "";
                string sYYYYMMDD = "";
                string UNINO = "";
                if (dtOrg.Rows.Count > 0)
                {
                    DateTime aDate = Convert.ToDateTime(dtOrg.Rows[0]["TRADE_DATE"]);
                    sInvoiceNo = aDate.Year.ToString();
                    sInvoiceNo += "_" + dtOrg.Rows[0]["SALE_NO"].ToString();
                    sStoreNo = dtOrg.Rows[0]["STORE_NO"].ToString();
                    sYYYYMMDD = aDate.ToString("yyyyMMdd");
               //     UNINO = dtOrg.Rows[0]["UNI_NO"].ToString();
                }

                //處理細項長度
                DataTable dtItem = new DataTable();
                dtItem.Columns.Add("PROD_NAME");
                dtItem.Columns.Add("QUANTITY");
                dtItem.Columns.Add("PROMO_NAME");
                dtItem.Columns.Add("AMOUNT");
                dtItem.Columns.Add("PRODNO");
                dtItem.Columns.Add("MSISDN");
                getZeroProperItem(dtOrg, dtItem);

                DataSet dsItems = new DataSet();
                DataTable dtTo = null;
                //1頁13列
                if (dtItem.Rows.Count > 0)
                {
                    int TablesCount = dtItem.Rows.Count / 13 + ((dtItem.Rows.Count % 13 == 0) ? 0 : 1);
                    for (int i = 0; i < dtItem.Rows.Count; i++)
                    {
                        if (i % 13 == 0)
                        {
                            dtTo = dtItem.Clone();
                            dsItems.Tables.Add(dtTo);
                        }
                        DataRow drFrom = dtItem.Rows[i];
                        DataRow drTo = dtTo.NewRow();
                        drTo.ItemArray = drFrom.ItemArray;
                        dtTo.Rows.Add(drTo);
                    }
                }


                //處理備註長度
                DataTable dtRemark = new DataTable();
                dtRemark.Columns.Add("REMARK");
                getProperRemark(dtOrg, dtRemark);

                DataSet dsRemark = new DataSet();
                dtTo = null;
                //1頁9列
                if (dtRemark.Rows.Count > 0)
                {
                    //1頁9列
                    int TablesCount = dtRemark.Rows.Count / 9 + ((dtRemark.Rows.Count % 9 == 0) ? 0 : 1);
                    for (int i = 0; i < dtRemark.Rows.Count; i++)
                    {
                        if (i % 9 == 0)
                        {
                            dtTo = dtRemark.Clone();
                            dsRemark.Tables.Add(dtTo);
                        }
                        DataRow drFrom = dtRemark.Rows[i];
                        DataRow drTo = dtTo.NewRow();
                        drTo.ItemArray = drFrom.ItemArray;
                        dtTo.Rows.Add(drTo);
                    }

                }

                //PDF Start
                //Paragraph p1;
                //PdfPCell tc;
                pdfDoc = new Document(PageSize.A4, 0f, 0f, 24f, 10f);
                //檔名
                if (!string.IsNullOrEmpty(sInvoiceNo))
                {
                    filename = sInvoiceNo + ".pdf";
                }
                else
                {
                    filename = Guid.NewGuid().ToString() + ".pdf";
                }
                //檢查路徑
                if (string.IsNullOrEmpty(sYYYYMMDD)) sYYYYMMDD = DateTime.Now.ToString("yyyyMMdd");
                string sSubPath = filePath + "/" + DateTime.Now.ToString("yyyyMMdd") + "/" + sStoreNo;
                checkDirectory(sSubPath);

                #region 備份CODE
                //if (!Directory.Exists(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD)))
                //{
                //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD));
                //}
                //if (!Directory.Exists(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD + "\\" + sStoreNo)))
                //{
                //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD + "\\" + sStoreNo));
                //}
                #endregion
                //產生PDF檔案
                //FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(filePath), filename), FileMode.Create);
                //FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(filePath + "\\" + sYYYYMMDD + "\\" + sStoreNo), filename), FileMode.Create);
                FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(sSubPath), filename), FileMode.Create);
                writer = PdfWriter.GetInstance(pdfDoc, stream);

                pdfDoc.Open();

                string printerName = ConfigurationManager.AppSettings["Invoice_PDFPrinterName"].ToString();//web.config中設定
                // 加入自動列印指令碼
                AddPrintAction(writer, printname);
                // writer.AddJavaScript("this.print(false);", true);




                int iCopies = (dsItems.Tables.Count > dsRemark.Tables.Count) ? dsItems.Tables.Count : dsRemark.Tables.Count;

                for (int i = 0; i < iCopies; i++)
                {
                    DataTable dtItem1 = null;
                    DataTable dtRemark1 = null;
                    if (i < dsItems.Tables.Count)
                    {
                        dtItem1 = dsItems.Tables[i];
                    }
                    else
                    {
                        dtItem1 = null;
                    }
                    if (i < dsRemark.Tables.Count)
                    {
                        dtRemark1 = dsRemark.Tables[i];
                    }
                    else
                    {
                        dtRemark1 = null;
                    }

                    //第一聯
                    if (UNINO == "")
                    {
                        add0NoUNI(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(i), Convert.ToString(iCopies));
                    }
                    else
                    {
                        add0(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(i), Convert.ToString(iCopies));
                    }
                    //畫虛線
                    PdfContentByte cb = writer.DirectContent;
                    for (int j = 0; j < pdfDoc.Right - 10; j += 4)
                    {
                        cb.MoveTo(pdfDoc.Left + j, 420.945f);
                        cb.LineTo(pdfDoc.Left + j + 1, 420.945f);
                    }
                    cb.Stroke();


                    //換頁
                    pdfDoc.NewPage();

                    //第二聯
                    if (UNINO == "")
                    {
                        add1NoUNI(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(i), Convert.ToString(iCopies));
                    }
                    else
                    {
                        add1(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(i), Convert.ToString(iCopies));
                    }
                    //產生第三聯
                    //add2(pdfDoc);
                    if (UNINO == "")
                    {
                        add3NoUNI(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(i), Convert.ToString(iCopies * 2));
                    }
                    else
                    {
                        add3(pdfDoc, dtOrg, dtItem1, dtRemark1, Convert.ToString(i), Convert.ToString(iCopies * 2));
                    }
                    //產生註記欄
                    addMarkTable1(pdfDoc);
                    addMarkTable2(pdfDoc);

                    if (i != iCopies - 1)
                    {
                        //換頁
                        pdfDoc.NewPage();
                    }
                }

                pdfDoc.Close();
            }
            catch (Exception ex)
            {
                if (pdfDoc.IsOpen()) pdfDoc.Close();
                throw ex;
            }

            return filename;

        }

        private string checkpage(string nowpage, string totalpage)
        {
            string pageword = "";
            if (totalpage != "1")
            {
                if (nowpage == "1")
                {
                    pageword = "續下頁";
                }
                else
                {
                    pageword = "承上頁";
                }
            }
            else
            {
                pageword = "";
            }
            return pageword;
        
        }

    }


}
