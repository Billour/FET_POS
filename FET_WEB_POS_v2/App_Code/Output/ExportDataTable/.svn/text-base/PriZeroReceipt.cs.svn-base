using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using System.IO;
using System.Data;
using System.Configuration;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
using System.Data.OracleClient;
using Advtek.Utility;
/// <summary>
/// PriZeroReceipt 的摘要描述
/// </summary>
public class PriZeroReceipt : BasePage
{
    protected Font smallFont;
    protected Font largerFont;
    protected Font RedsmallFont;
    protected Document pdfDoc;
    protected PdfPTable pdfTable;  //組成整體樣式
    protected PdfPTable details;   //組成明細樣式
    protected DataTable dtData;    //前端傳來的資料表
    protected string filename;
    protected PdfWriter writer;

    public PdfPTable getHeaderTb(string Title, BaseFont bfChinese,DataTable dtOrg)
    {
        PdfPTable HeaderTb = new PdfPTable(new float[] { 1 ,1,1});
        HeaderTb.TotalWidth = 550;//設定Table 寬度
       
        HeaderTb.LockedWidth = true;

        float setFontSize = 12f; //設定字型大小


        //代收款專用繳款證明(XX聯)
        PdfPCell HeaderTitle_C1 = new PdfPCell(new Phrase("遠傳電信股份有限公司 " + Title + " ", new Font(bfChinese, setFontSize, Font.NORMAL)));
        // HeaderTitle_C12.FixedHeight = 30.0f; //設定欄位高度
        HeaderTitle_C1.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTitle_C1.HorizontalAlignment = Element.ALIGN_CENTER; //字置中
        HeaderTitle_C1.VerticalAlignment = Element.ALIGN_MIDDLE; //水平置中   
        HeaderTitle_C1.Colspan = 3;
        HeaderTb.AddCell(HeaderTitle_C1); //將資料加入Table

        //遠傳電信股份有限公司
        PdfPCell HeaderTitle_C2 = new PdfPCell(new Phrase("交易日期:", new Font(bfChinese, setFontSize, Font.NORMAL)));
        // HeaderTitle_C12.FixedHeight = 30.0f; //設定欄位高度
        HeaderTitle_C2.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTitle_C2.HorizontalAlignment = Element.ALIGN_CENTER; //字置中
        HeaderTitle_C2.VerticalAlignment = Element.ALIGN_MIDDLE; //水平置中   
        HeaderTb.AddCell(HeaderTitle_C2); //將資料加入Table

        //TEL
        PdfPCell HeaderTitle_C4 = new PdfPCell(new Phrase("", new Font(bfChinese, setFontSize, Font.NORMAL)));
        // HeaderTitle_C12.FixedHeight = 30.0f; //設定欄位高度
        HeaderTitle_C4.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTitle_C4.HorizontalAlignment = Element.ALIGN_CENTER; //字置中
        HeaderTitle_C4.VerticalAlignment = Element.ALIGN_MIDDLE; //水平置中   
        HeaderTb.AddCell(HeaderTitle_C4); //將資料加入Table
        //TEL
        PdfPCell HeaderTitle_C3 = new PdfPCell(new Phrase("交易序號:888", new Font(bfChinese, setFontSize, Font.NORMAL)));
        // HeaderTitle_C12.FixedHeight = 30.0f; //設定欄位高度
        HeaderTitle_C3.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTitle_C3.HorizontalAlignment = Element.ALIGN_CENTER; //字置中
        HeaderTitle_C3.VerticalAlignment = Element.ALIGN_MIDDLE; //水平置中   
        HeaderTb.AddCell(HeaderTitle_C3); //將資料加入Table
        return HeaderTb;
    }


    public PdfPTable getBodyTb(List<string> DataList, bool generateImg, BaseFont bfChinese,DataTable dtOrg,DataTable dtItem)
    {
        PdfPTable BodyTb = new PdfPTable(6);
        float[] widths = new float[] { 177f, 70f, 145f, 75f, 35f, 50f };
        BodyTb.SpacingAfter = 0;
        BodyTb.SpacingBefore = 0;
        BodyTb.SetTotalWidth(widths);
      //  BodyTb.TotalWidth = 500f;//設定Table 寬度
        BodyTb.LockedWidth = true;
        float setFontSize = 12f; //設定字型大小

        //空白欄
        PdfPCell S1 = new PdfPCell(new Paragraph("　", new Font(bfChinese, setFontSize, Font.NORMAL)));
        S1.Border = Rectangle.NO_BORDER; //去框架格線

   //     BodyTb.AddCell(S1); //將空白加入Table
   //     BodyTb.AddCell(S1); //將空白加入Table

        PdfPCell Body_C11 = new PdfPCell(new Phrase("促銷名稱：" , new Font(bfChinese, setFontSize, Font.NORMAL)));
        Body_C11.HorizontalAlignment = Element.ALIGN_CENTER; //字靠左
        Body_C11.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
        //Body_C11.Border = Rectangle.NO_BORDER; //去框架格線
        BodyTb.AddCell(Body_C11); //將資料加入Table

        PdfPCell Body_C12 = new PdfPCell(new Phrase("商品料號：", new Font(bfChinese, setFontSize, Font.NORMAL)));
        Body_C12.HorizontalAlignment = Element.ALIGN_CENTER; //字靠左
        Body_C12.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
       // Body_C12.Border = Rectangle.NO_BORDER; //去框架格線
        BodyTb.AddCell(Body_C12); //將資料加入Table

        PdfPCell Body_C21 = new PdfPCell(new Phrase("品名：" , new Font(bfChinese, setFontSize, Font.NORMAL)));
        Body_C21.HorizontalAlignment = Element.ALIGN_CENTER; //字靠左
        Body_C21.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
    //    Body_C21.Border = Rectangle.NO_BORDER; //去框架格線
        BodyTb.AddCell(Body_C21); //將資料加入Table

        PdfPCell Body_C22 = new PdfPCell(new Phrase("門號", new Font(bfChinese, setFontSize, Font.NORMAL)));
        Body_C22.HorizontalAlignment = Element.ALIGN_LEFT; //字靠左
        Body_C22.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
      //  Body_C22.Border = Rectangle.NO_BORDER; //去框架格線
        BodyTb.AddCell(Body_C22); //將資料加入Table

        PdfPCell Body_C31 = new PdfPCell(new Phrase("數量", new Font(bfChinese, setFontSize, Font.NORMAL)));
        Body_C31.HorizontalAlignment = Element.ALIGN_LEFT; //字靠左
        Body_C31.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
       // Body_C31.Border = Rectangle.NO_BORDER; //去框架格線
        BodyTb.AddCell(Body_C31); //將資料加入Table

        PdfPCell Body_C32 = new PdfPCell(new Phrase("金額" , new Font(bfChinese, setFontSize, Font.NORMAL)));
        Body_C32.HorizontalAlignment = Element.ALIGN_LEFT; //字靠左
        Body_C32.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
    //    Body_C32.Border = Rectangle.NO_BORDER; //去框架格線
        BodyTb.AddCell(Body_C32); //將資料加入Table
        //左內容副框:細項框內容列
    //    p1 = new Paragraph("", s09);
        if (dtItem != null && dtItem.Rows.Count > 0)
        {
      //      Paragraph p2 = new Paragraph("", s09);
      //      Paragraph p3 = new Paragraph("", s09);
     //       Paragraph p4 = new Paragraph("", s09);
            for (int j = 0; j < dtItem.Rows.Count; j++)
            {
                DataRow _dr = dtItem.Rows[j];
                string sPROMO_NAME = (_dr["PROMO_NAME"] != DBNull.Value) ? _dr["PROMO_NAME"].ToString() : "";
                string sPROD_NAME = (_dr["PROD_NAME"] != DBNull.Value) ? _dr["PROD_NAME"].ToString() : "";
                string sPRODNO = (_dr["PRODNO"] != DBNull.Value) ? _dr["PRODNO"].ToString() : "";
                string sQUANTITY = (_dr["QUANTITY"] != DBNull.Value) ? _dr["QUANTITY"].ToString() : "";
            //    string sPRICE = (_dr["PRICE"] != DBNull.Value) ? _dr["PRICE"].ToString() : "";
                string sAMOUNT = (_dr["AMOUNT"] != DBNull.Value) ? _dr["AMOUNT"].ToString() : "0";
                string sMSISDN = (_dr["MSISDN"] != DBNull.Value) ? _dr["MSISDN"].ToString() : "";
                //if (j != 0)
                //{
                //    p1.Add("\n");
                //    p2.Add("\n");
                //    p3.Add("\n");
                //    p4.Add("\n");
                //}
                PdfPCell Body_C01 = new PdfPCell(new Phrase(sPROMO_NAME, new Font(bfChinese, setFontSize, Font.NORMAL)));
                Body_C01.HorizontalAlignment = Element.ALIGN_CENTER; //字靠左
                Body_C01.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
                //Body_C11.Border = Rectangle.NO_BORDER; //去框架格線
                BodyTb.AddCell(Body_C01); //將資料加入Table
                PdfPCell Body_C02 = new PdfPCell(new Phrase(sPRODNO, new Font(bfChinese, setFontSize, Font.NORMAL)));
                Body_C02.HorizontalAlignment = Element.ALIGN_CENTER; //字靠左
                Body_C02.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
                //Body_C11.Border = Rectangle.NO_BORDER; //去框架格線
                BodyTb.AddCell(Body_C02); //將資料加入Table
                PdfPCell Body_C03 = new PdfPCell(new Phrase(sPROD_NAME, new Font(bfChinese, setFontSize, Font.NORMAL)));
                Body_C03.HorizontalAlignment = Element.ALIGN_CENTER; //字靠左
                Body_C03.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
                //Body_C11.Border = Rectangle.NO_BORDER; //去框架格線
                BodyTb.AddCell(Body_C03); //將資料加入Table
                PdfPCell Body_C04 = new PdfPCell(new Phrase(sMSISDN, new Font(bfChinese, setFontSize, Font.NORMAL)));
                Body_C04.HorizontalAlignment = Element.ALIGN_CENTER; //字靠左
                Body_C04.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
                //Body_C11.Border = Rectangle.NO_BORDER; //去框架格線
                BodyTb.AddCell(Body_C04); //將資料加入Table
                PdfPCell Body_C05 = new PdfPCell(new Phrase(sQUANTITY, new Font(bfChinese, setFontSize, Font.NORMAL)));
                Body_C05.HorizontalAlignment = Element.ALIGN_CENTER; //字靠左
                Body_C05.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
                //Body_C11.Border = Rectangle.NO_BORDER; //去框架格線
                BodyTb.AddCell(Body_C05); //將資料加入Table
                PdfPCell Body_C06 = new PdfPCell(new Phrase(sAMOUNT, new Font(bfChinese, setFontSize, Font.NORMAL)));
                Body_C06.HorizontalAlignment = Element.ALIGN_CENTER; //字靠左
                Body_C06.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
                //Body_C11.Border = Rectangle.NO_BORDER; //去框架格線
                BodyTb.AddCell(Body_C06); //將資料加入Table

            }
                //p1.Add(sPROD_NAME);
                //p2.Add(sQUANTITY);

        }
      
        PdfPCell Body_C101 = new PdfPCell(new Phrase("備註攔：查詢繳費狀況，請攜帶此單據", new Font(bfChinese, setFontSize, Font.NORMAL)));
        Body_C101.HorizontalAlignment = Element.ALIGN_LEFT; //字靠左
        Body_C101.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
       // Body_C101.Border = Rectangle.NO_BORDER; //去框架格線
        Body_C101.Colspan = 5;
        BodyTb.AddCell(Body_C101); //將資料加入Table
        if (generateImg)
        {
            //圖片     
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images") + "\\PriReceipt.jpg");
            jpg.SetAbsolutePosition(0, 0);
            jpg.ScalePercent(30f);
            //PdfPCell tc = new PdfPCell(jpg,true);
            PdfPCell Body_C102 = new PdfPCell(jpg, true);
            Body_C102.Border = Rectangle.NO_BORDER; //去框架格線
            Body_C102.PaddingTop = 1;
            Body_C102.PaddingLeft = 1;
            Body_C102.Rowspan = 6;
            BodyTb.AddCell(Body_C102);
        }
        else
        {
            PdfPCell Body_C102 = new PdfPCell(new Phrase("", new Font(bfChinese, setFontSize, Font.NORMAL)));
            Body_C102.Rowspan = 3;
            Body_C102.Border = Rectangle.NO_BORDER; //去框架格線
            BodyTb.AddCell(Body_C102); //將空白加入Table
        }

        //if (generateImg)
        //{
        //    //補空白行
        //    for (int i = 0; i < 8; i++)
        //        BodyTb.AddCell(S1); //將空白加入Table
        //}


        return BodyTb;
    }


    public string generateReceipt(List<string> DataList,string POSID,string printerName,string strPath1)
    {
        if (POSID!= "")
        {

            DataTable dtOrg = getPROMOData(POSID);
            DataTable dtItem = new DataTable();
            dtItem.Columns.Add("PROD_NAME");
            dtItem.Columns.Add("QUANTITY");
            dtItem.Columns.Add("PROMO_NAME");
            dtItem.Columns.Add("AMOUNT");
            dtItem.Columns.Add("PRODNO");
            dtItem.Columns.Add("MSISDN");
            getZeroProperItem(dtOrg, dtItem);


            MemoryStream Memory = new MemoryStream();
            //string printerName = @"\\192.168.8.100\HP LaserJet 3055 PCL5"; //"\\192.168.8.1\Brother MFC9600/9870 Series";
            //string printerName = @"HP LaserJet 3050 Series PCL 5e"; //印表機和傳真 
            //string printerName = @"\\192.168.8.100\HP LaserJet 3050 Series PCL 6"; //"\\192.168.8.1\Brother MFC9600/9870 Series";
            //string printerName = ConfigurationManager.AppSettings["Receipt_PDFPrinterName"].ToString();//web.config中設定

            //字型設定
            BaseFont bfChinese = BaseFont.CreateFont(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\..\Fonts\simhei.ttf"
                                 , BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font ChFont_HeaderTitle_C1 = new Font(Font.FontFamily.HELVETICA, 20f, Font.BOLD);

            //設定PDF PageSize 及 Margin left,right,top,bottom
            pdfDoc = new Document(PageSize.A4, 0f, 0f, 24f, 10f);

            filename = Guid.NewGuid().ToString() + ".pdf";

         //   string strPath = "~" + Common_PageHelper.GetPriReceipt_Path();
            FileStream stream = new FileStream(Path.Combine(Server.MapPath(strPath1), filename), FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
            pdfDoc.Open();

            //string printerName = ConfigurationManager.AppSettings["PDFPrinterName"].ToString();//web.config中設定
            // 加入自動列印指令碼
            //writer.AddSilentPrintAction(printerName);
            //AddPrintAction(writer, printerName);
            //writer.AddJavaScript("this.print(false);", true);

            PdfPTable HeaderTb_P1 = getHeaderTb("顧客聯", bfChinese,dtOrg);
            pdfDoc.Add(HeaderTb_P1); //將表頭加入主頁簽

            PdfPTable BodyTb_1 = getBodyTb(DataList, true, bfChinese,dtOrg,dtItem);
            pdfDoc.Add(BodyTb_1); //將資料加入主頁簽

            ////畫虛線
            //PdfContentByte cb = writer.DirectContent;
            //for (int j = 0; j < pdfDoc.Right - 10; j += 4)
            //{
            //    cb.MoveTo(pdfDoc.Left + j, 420.945f);
            //    cb.LineTo(pdfDoc.Left + j + 1, 420.945f);
            //}
            //cb.Stroke();

            //PdfPTable HeaderTb_P2 = getHeaderTb("門市收執聯", bfChinese);
            //pdfDoc.Add(HeaderTb_P2); //將表頭加入主頁簽

            //PdfPTable BodyTb_2 = getBodyTb(DataList, false, bfChinese);
            //pdfDoc.Add(BodyTb_2); //將資料加入主頁簽


            //文件關閉
            pdfDoc.Close();

            // 動態建立內嵌框架，以輸出文件內容
            GenerateIFrameToLoadDocument(
                // 建立防竄改的文件下載網址
                Utils.CreateTamperProofDownloadURL(filename)
                );

        }
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

    private DataTable getReceiptData(string POSUUID_MASTER)
    {
        DataTable dt = new DataTable();
        using (OracleConnection con = OracleDBUtil.GetConnection())
        {
            try
            {
                string sql = "SELECT S.STORENAME AS STORENAME  " +
                           ",SH.TRADE_DATE AS TRADE_DATE  " +
                           ",SH.SALE_NO AS SALE_NO " +
                           ",SD.QUANTITY AS QUANTITY  " +         
                           ",SD.TOTAL_AMOUNT AS AMOUNT  " +
                           ",SH.STORE_NO STORE_NO   " +
                           ",SH.SALE_TOTAL_AMOUNT  " +
                           ",SD.MSISDN  " +
                           ",PP.PRODNAME  " +
                           ",mm.PROMO_NAME  " +
                           "FROM SALE_HEAD SH,SALE_DETAIL SD ,STORE S ,PRODUCT PP ,mm  " +
                           "WHERE SH.POSUUID_MASTER = SD.POSUUID_MASTER  " +
                           "  AND SH.STORE_NO=S.STORE_NO  " +
                           "  AND SD.PRODNO = PP.PRODNO  " +
                           "  AND SD.PROMOTION_CODE = mm.promo_no(+)  " +
                           " AND SH.POSUUID_MASTER=" + OracleDBUtil.SqlStr(POSUUID_MASTER);
                          // "  AND IH.POSUUID_MASTER=" + OracleDBUtil.SqlStr(POSUUID_MASTER);

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
            string sProdName = dr["PRODNAME"].ToString();
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
                        drItem["MSISDN"] = dr["MSISDN"];
                        drItem["PROMO_NAME"] = dr["PROMO_NAME"];
                        drItem["PRODNO"] = dr["PRODNO"];


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
                drItem["MSISDN"] = dr["MSISDN"];
                drItem["PROMO_NAME"] = dr["PROMO_NAME"];
                drItem["PRODNO"] = dr["PRODNO"];



            }
            dtItem.Rows.Add(drItem);
        }
    }

    private int getBytes(string str)
        {
            return System.Text.Encoding.Default.GetBytes(str).Length;
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
                               ",SD.TOTAL_AMOUNT as AMOUNT "+
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
   
}
