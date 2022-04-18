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
public class PriGuarantee : BasePage
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
    //組表頭
    public PdfPTable getHeaderTb(string Title, BaseFont bfChinese)
    {
        PdfPTable HeaderTb = new PdfPTable(new float[] { 1 });
        HeaderTb.TotalWidth = 500f;//設定Table 寬度
        HeaderTb.LockedWidth = true;

        float setFontSize = 12f; //設定字型大小


        //代收款專用繳款證明(XX聯)
        PdfPCell HeaderTitle_C1 = new PdfPCell(new Phrase("代收款專用繳款證明(" + Title + ")", new Font(bfChinese, setFontSize, Font.NORMAL)));
        // HeaderTitle_C12.FixedHeight = 30.0f; //設定欄位高度
        HeaderTitle_C1.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTitle_C1.HorizontalAlignment = Element.ALIGN_CENTER; //字置中
        HeaderTitle_C1.VerticalAlignment = Element.ALIGN_MIDDLE; //水平置中   
        HeaderTb.AddCell(HeaderTitle_C1); //將資料加入Table

        //遠傳電信股份有限公司
        PdfPCell HeaderTitle_C2 = new PdfPCell(new Phrase("遠傳電信股份有限公司", new Font(bfChinese, setFontSize, Font.NORMAL)));
        // HeaderTitle_C12.FixedHeight = 30.0f; //設定欄位高度
        HeaderTitle_C2.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTitle_C2.HorizontalAlignment = Element.ALIGN_CENTER; //字置中
        HeaderTitle_C2.VerticalAlignment = Element.ALIGN_MIDDLE; //水平置中   
        HeaderTb.AddCell(HeaderTitle_C2); //將資料加入Table

        //TEL
        PdfPCell HeaderTitle_C3 = new PdfPCell(new Phrase("TEL:手機直撥888", new Font(bfChinese, setFontSize, Font.NORMAL)));
        // HeaderTitle_C12.FixedHeight = 30.0f; //設定欄位高度
        HeaderTitle_C3.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTitle_C3.HorizontalAlignment = Element.ALIGN_CENTER; //字置中
        HeaderTitle_C3.VerticalAlignment = Element.ALIGN_MIDDLE; //水平置中   
        HeaderTb.AddCell(HeaderTitle_C3); //將資料加入Table
        return HeaderTb;
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

        PdfPCell Body_C32 = new PdfPCell(new Phrase("實收金額：" + DataList[6], new Font(bfChinese, setFontSize, Font.NORMAL)));
        Body_C32.HorizontalAlignment = Element.ALIGN_LEFT; //字靠左
        Body_C32.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
        Body_C32.Border = Rectangle.NO_BORDER; //去框架格線
        BodyTb.AddCell(Body_C32); //將資料加入Table

        BodyTb.AddCell(S1); //將空白加入Table
        BodyTb.AddCell(S1); //將空白加入Table

        PdfPCell Body_C41 = new PdfPCell(new Phrase("用戶號碼：" + DataList[7], new Font(bfChinese, setFontSize, Font.NORMAL)));
        Body_C41.HorizontalAlignment = Element.ALIGN_LEFT; //字靠左
        Body_C41.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
        Body_C41.Border = Rectangle.NO_BORDER; //去框架格線
        BodyTb.AddCell(Body_C41); //將資料加入Table

        BodyTb.AddCell(S1); //將空白加入Table

      
        BodyTb.AddCell(S1); //將空白加入Table


        PdfPCell Body_C51 = new PdfPCell(new Phrase("銷售專員：" + DataList[8], new Font(bfChinese, setFontSize, Font.NORMAL)));
        Body_C51.HorizontalAlignment = Element.ALIGN_RIGHT; //字靠右
        Body_C51.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
        Body_C51.Border = Rectangle.NO_BORDER; //去框架格線
        BodyTb.AddCell(Body_C51); //將資料加入Table

        PdfPCell Body_C61 = new PdfPCell(new Phrase("備註：查詢繳費狀況，請攜帶此單據", new Font(bfChinese, setFontSize, Font.NORMAL)));
        Body_C61.HorizontalAlignment = Element.ALIGN_LEFT; //字靠左
        Body_C61.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
        Body_C61.Border = Rectangle.NO_BORDER; //去框架格線
        BodyTb.AddCell(Body_C61); //將資料加入Table
        if (generateImg)
        {
            //圖片     
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images") + "\\PriReceipt.jpg");
            jpg.SetAbsolutePosition(0, 0);
            jpg.ScalePercent(30f);
            //PdfPCell tc = new PdfPCell(jpg,true);
            PdfPCell Body_C62 = new PdfPCell(jpg, true);
            Body_C62.Border = Rectangle.NO_BORDER; //去框架格線
            Body_C62.PaddingTop = 1;
            Body_C62.PaddingLeft = 1;
            BodyTb.AddCell(Body_C62);

            //補空白行
            for (int i = 0; i < 18; i++)
                BodyTb.AddCell(S1); //將空白加入Table
        }
        else
            BodyTb.AddCell(S1); //將空白加入Table

       
      


        return BodyTb;
    }


    public string generateReceipt(List<string> DataList, string PrintName)
    {
        if (DataList.Count > 0)
        {
            MemoryStream Memory = new MemoryStream();
            //string printerName = @"\\192.168.8.100\HP LaserJet 3055 PCL5"; //"\\192.168.8.1\Brother MFC9600/9870 Series";
            //string printerName = @"HP LaserJet 3050 Series PCL 5e"; //印表機和傳真 
            //string printerName = @"\\192.168.8.100\HP LaserJet 3050 Series PCL 6"; //"\\192.168.8.1\Brother MFC9600/9870 Series";
            //string printerName = ConfigurationManager.AppSettings["Receipt_PDFPrinterName"].ToString();//web.config中設定
            string printerName = PrintName;

            //字型設定
            BaseFont bfChinese = BaseFont.CreateFont(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\..\Fonts\simhei.ttf"
                                 , BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font ChFont_HeaderTitle_C1 = new Font(Font.FontFamily.HELVETICA, 20f, Font.BOLD);

            //設定PDF PageSize 及 Margin left,right,top,bottom
            pdfDoc = new Document(PageSize.A4, 10f, 10f, 20f, 20f);

            filename = Guid.NewGuid().ToString() + ".pdf";

            string strPath = "~" + Common_PageHelper.GetPriReceipt_Path();

            //判斷資料夾是否存在 不存在則建一個
            if (!Directory.Exists(Server.MapPath(strPath)))
                Directory.CreateDirectory(Server.MapPath(strPath));

            FileStream stream = new FileStream(Path.Combine(Server.MapPath(strPath), filename), FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
            pdfDoc.Open();

            //string printerName = ConfigurationManager.AppSettings["PDFPrinterName"].ToString();//web.config中設定
            // 加入自動列印指令碼
            //writer.AddSilentPrintAction(printerName);
            AddPrintAction(writer, printerName);
            //writer.AddJavaScript("this.print(false);", true);

            PdfPTable HeaderTb_P1 = getHeaderTb("顧客聯", bfChinese);
            pdfDoc.Add(HeaderTb_P1); //將表頭加入主頁簽

            PdfPTable BodyTb_1 = getBodyTb(DataList, true, bfChinese);
            pdfDoc.Add(BodyTb_1); //將資料加入主頁簽

            //畫虛線
            PdfContentByte cb = writer.DirectContent;
            for (int j = 0; j < pdfDoc.Right - 10; j += 4)
            {
                cb.MoveTo(pdfDoc.Left + j, 420.945f);
                cb.LineTo(pdfDoc.Left + j + 1, 420.945f);
            }
            cb.Stroke();

            PdfPTable HeaderTb_P2 = getHeaderTb("門市收執聯", bfChinese);
            pdfDoc.Add(HeaderTb_P2); //將表頭加入主頁簽

            PdfPTable BodyTb_2 = getBodyTb(DataList, false, bfChinese);
            pdfDoc.Add(BodyTb_2); //將資料加入主頁簽


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
}
