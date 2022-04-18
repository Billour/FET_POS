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
/// AbstractPDFExportor 的摘要描述
/// </summary>
public class PriPRE01 : BasePage
{
    protected Font smallFont;
    protected Font largerFont;
    protected Font RedsmallFont;
    protected Document pdfDoc;
    protected PdfPTable pdfTable;  //組成整體樣式
    protected PdfPTable details;   //組成明細樣式
    protected DataTable dtData;    //前端傳來的資料表
    protected string filename;



    #region Exportor 成員

    public PdfPTable getHeaderTb(DataTable TitleDt, string Title, BaseFont bfChinese)
    {
        PdfPTable HeaderTb = new PdfPTable(new float[] { 2, 2, 2, 2, 2, 2, 1 });
        HeaderTb.TotalWidth = 500f;//設定Table 寬度
        HeaderTb.LockedWidth = true;

        float setFontSize = 10f; //設定字型大小

        //預收單
        PdfPCell HeaderTitle_C11 = new PdfPCell(new Phrase("預  收  單", new Font(bfChinese, 15f, Font.BOLD)));
        HeaderTitle_C11.Colspan = 6; //合併欄   
        HeaderTitle_C11.FixedHeight = 32.0f; //設定欄位高度
        HeaderTitle_C11.Border = Rectangle.NO_BORDER; ;//去框架格線
        HeaderTitle_C11.HorizontalAlignment = Element.ALIGN_CENTER; //字置中
        HeaderTitle_C11.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
        HeaderTb.AddCell(HeaderTitle_C11); //將資料加入Table

        //XX聯
        PdfPCell HeaderTitle_C12 = new PdfPCell(new Phrase(Title, new Font(bfChinese, setFontSize, Font.NORMAL)));
        // HeaderTitle_C12.FixedHeight = 30.0f; //設定欄位高度
        // HeaderTitle_C12.Column.Alignment = Element.ALIGN_CENTER;
        HeaderTitle_C12.HorizontalAlignment = Element.ALIGN_CENTER; //字置中
        HeaderTitle_C12.VerticalAlignment = Element.ALIGN_MIDDLE; //水平置中    

        HeaderTb.AddCell(HeaderTitle_C12); //將資料加入Table

        //購買門市
        PdfPCell HeaderTitle_C21 = new PdfPCell(new Phrase("購買門市 :", new Font(bfChinese, setFontSize, Font.BOLD)));
        HeaderTitle_C21.HorizontalAlignment = Element.ALIGN_RIGHT; //字置右
        HeaderTitle_C21.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTb.AddCell(HeaderTitle_C21); //將資料加入Table

        //購買門市-DATA
        PdfPCell HeaderTitle_C22 = new PdfPCell(new Phrase(TitleDt.Rows[0]["STORE_NAME"].ToString(), new Font(bfChinese, setFontSize, Font.NORMAL)));
        //HeaderTitle_C22.HorizontalAlignment = Element.ALIGN_RIGHT; //字置右
        HeaderTitle_C22.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTb.AddCell(HeaderTitle_C22); //將資料加入Table

        //購買日期
        PdfPCell HeaderTitle_C23 = new PdfPCell(new Phrase("購買日期 :", new Font(bfChinese, setFontSize, Font.BOLD)));
        HeaderTitle_C23.HorizontalAlignment = Element.ALIGN_RIGHT; //字置右
        HeaderTitle_C23.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTb.AddCell(HeaderTitle_C23); //將資料加入Table

        //購買日期-DATA
        PdfPCell HeaderTitle_C24 = new PdfPCell(new Phrase(Convert.ToDateTime(TitleDt.Rows[0]["TRADE_DATE"]).ToString("yyyy/MM/dd"), new Font(bfChinese, setFontSize, Font.NORMAL)));
        //HeaderTitle_C22.HorizontalAlignment = Element.ALIGN_RIGHT; //字置右
        HeaderTitle_C24.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTb.AddCell(HeaderTitle_C24); //將資料加入Table

        //預收單號
        PdfPCell HeaderTitle_C25 = new PdfPCell(new Phrase("預收單號 :", new Font(bfChinese, setFontSize, Font.BOLD)));
        HeaderTitle_C25.HorizontalAlignment = Element.ALIGN_RIGHT; //字置右
        HeaderTitle_C25.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTb.AddCell(HeaderTitle_C25); //將資料加入Table

        //預收單號-DATA
        PdfPCell HeaderTitle_C26 = new PdfPCell(new Phrase(TitleDt.Rows[0]["PREPAY_NO"].ToString(), new Font(bfChinese, setFontSize, Font.NORMAL)));
        //HeaderTitle_C22.HorizontalAlignment = Element.ALIGN_RIGHT; //字置右
        HeaderTitle_C26.Colspan = 2; //合併欄   
        HeaderTitle_C26.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTb.AddCell(HeaderTitle_C26); //將資料加入Table

        //啟用類型
        PdfPCell HeaderTitle_C31 = new PdfPCell(new Phrase("啟用類型 :", new Font(bfChinese, setFontSize, Font.BOLD)));
        HeaderTitle_C31.HorizontalAlignment = Element.ALIGN_RIGHT; //字置右
        HeaderTitle_C31.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTb.AddCell(HeaderTitle_C31); //將資料加入Table

        //啟用類型-DATA
        PdfPCell HeaderTitle_C32 = new PdfPCell(new Phrase(TitleDt.Rows[0]["START_TYPE_NAME"].ToString(), new Font(bfChinese, setFontSize, Font.NORMAL)));
        //HeaderTitle_C22.HorizontalAlignment = Element.ALIGN_RIGHT; //字置右
        HeaderTitle_C32.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTb.AddCell(HeaderTitle_C32); //將資料加入Table

        //客戶姓名
        PdfPCell HeaderTitle_C33 = new PdfPCell(new Phrase("客戶姓名 :", new Font(bfChinese, setFontSize, Font.BOLD)));
        HeaderTitle_C33.HorizontalAlignment = Element.ALIGN_RIGHT; //字置右
        HeaderTitle_C33.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTb.AddCell(HeaderTitle_C33); //將資料加入Table

        //客戶姓名-DATA
        PdfPCell HeaderTitle_C34 = new PdfPCell(new Phrase(DataHide(TitleDt.Rows[0]["CUST_NAME"].ToString(), 1, 1), new Font(bfChinese, setFontSize, Font.NORMAL)));
        //HeaderTitle_C22.HorizontalAlignment = Element.ALIGN_RIGHT; //字置右
        HeaderTitle_C34.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTb.AddCell(HeaderTitle_C34); //將資料加入Table

        //客戶身份證號
        PdfPCell HeaderTitle_C35 = new PdfPCell(new Phrase("客戶身份證號:", new Font(bfChinese, setFontSize, Font.BOLD)));
        HeaderTitle_C35.HorizontalAlignment = Element.ALIGN_RIGHT; //字置右
        HeaderTitle_C35.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTb.AddCell(HeaderTitle_C35); //將資料加入Table

        //客戶身份證號-DATA
        PdfPCell HeaderTitle_C36 = new PdfPCell(new Phrase(DataHide(TitleDt.Rows[0]["ID_NO"].ToString(), 6, 1), new Font(bfChinese, setFontSize, Font.NORMAL)));
        //HeaderTitle_C22.HorizontalAlignment = Element.ALIGN_RIGHT; //字置右
        HeaderTitle_C36.Colspan = 2; //合併欄   
        HeaderTitle_C36.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTb.AddCell(HeaderTitle_C36); //將資料加入Table


        //客戶門號
        PdfPCell HeaderTitle_C41 = new PdfPCell(new Phrase("客戶門號 :", new Font(bfChinese, setFontSize, Font.BOLD)));
        HeaderTitle_C41.HorizontalAlignment = Element.ALIGN_RIGHT; //字置右
        HeaderTitle_C41.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTb.AddCell(HeaderTitle_C41); //將資料加入Table

        //客戶門號-DATA
        PdfPCell HeaderTitle_C42 = new PdfPCell(new Phrase(DataHide(TitleDt.Rows[0]["MSISDN"].ToString(), 5, 3), new Font(bfChinese, setFontSize, Font.NORMAL)));
        //HeaderTitle_C22.HorizontalAlignment = Element.ALIGN_RIGHT; //字置右
        HeaderTitle_C42.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTb.AddCell(HeaderTitle_C42); //將資料加入Table

        //聯絡電話
        PdfPCell HeaderTitle_C43 = new PdfPCell(new Phrase("聯絡電話 :", new Font(bfChinese, setFontSize, Font.BOLD)));
        HeaderTitle_C43.HorizontalAlignment = Element.ALIGN_RIGHT; //字置右
        HeaderTitle_C43.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTb.AddCell(HeaderTitle_C43); //將資料加入Table

        //聯絡電話-DATA
        PdfPCell HeaderTitle_C44 = new PdfPCell(new Phrase(DataHide(TitleDt.Rows[0]["CONTACT_PHONE"].ToString(), 6, 1), new Font(bfChinese, setFontSize, Font.NORMAL)));
        //HeaderTitle_C22.HorizontalAlignment = Element.ALIGN_RIGHT; //字置右
        HeaderTitle_C44.Colspan = 4; //合併欄 
        HeaderTitle_C44.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTb.AddCell(HeaderTitle_C44); //將資料加入Table

        //電子信箱
        PdfPCell HeaderTitle_C51 = new PdfPCell(new Phrase("電子信箱 :", new Font(bfChinese, setFontSize, Font.BOLD)));
        HeaderTitle_C51.HorizontalAlignment = Element.ALIGN_RIGHT; //字置右
        HeaderTitle_C51.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTb.AddCell(HeaderTitle_C51); //將資料加入Table

        //電子信箱-DATA
        PdfPCell HeaderTitle_C52 = new PdfPCell(new Phrase(DataHide(TitleDt.Rows[0]["EMAIL"].ToString(), 0, TitleDt.Rows[0]["EMAIL"].ToString().Length - 2), new Font(bfChinese, setFontSize, Font.NORMAL)));
        //HeaderTitle_C22.HorizontalAlignment = Element.ALIGN_RIGHT; //字置右
        HeaderTitle_C52.Border = Rectangle.NO_BORDER; //去框架格線
        HeaderTitle_C52.Colspan = 6; //合併欄 
        HeaderTb.AddCell(HeaderTitle_C52); //將資料加入Table
        return HeaderTb;
    }

    public PdfPTable getBodyTb(DataTable tmpBodyDt, int SetPageRow, BaseFont bfChinese)
    {
        PdfPTable BodyTb = new PdfPTable(new float[] { 2, 2, 1, 2, 3 });
        BodyTb.TotalWidth = 500f;//設定Table 寬度
        BodyTb.LockedWidth = true;
        float setFontSize = 10f; //設定字型大小

        PdfPCell Header_C1 = new PdfPCell(new Phrase("商品項目", new Font(bfChinese, setFontSize, Font.BOLD)));
        Header_C1.HorizontalAlignment = Element.ALIGN_CENTER; //字置中
        Header_C1.VerticalAlignment = Element.ALIGN_MIDDLE; //水平置中    
        BodyTb.AddCell(Header_C1); //將資料加入Table

        PdfPCell Header_C2 = new PdfPCell(new Phrase("預收款說明", new Font(bfChinese, setFontSize, Font.BOLD)));
        Header_C2.HorizontalAlignment = Element.ALIGN_CENTER; //字置中
        Header_C2.VerticalAlignment = Element.ALIGN_MIDDLE; //水平置中    
        BodyTb.AddCell(Header_C2); //將資料加入Table

        PdfPCell Header_C3 = new PdfPCell(new Phrase("數量", new Font(bfChinese, setFontSize, Font.BOLD)));
        Header_C3.HorizontalAlignment = Element.ALIGN_CENTER; //字置中
        Header_C3.VerticalAlignment = Element.ALIGN_MIDDLE; //水平置中    
        BodyTb.AddCell(Header_C3); //將資料加入Table

        PdfPCell Header_C4 = new PdfPCell(new Phrase("價格", new Font(bfChinese, setFontSize, Font.BOLD)));
        Header_C4.HorizontalAlignment = Element.ALIGN_CENTER; //字置中
        Header_C4.VerticalAlignment = Element.ALIGN_MIDDLE; //水平置中 
        BodyTb.AddCell(Header_C4); //將資料加入Table

        PdfPCell Header_C5 = new PdfPCell(new Phrase("備註", new Font(bfChinese, setFontSize, Font.BOLD)));
        Header_C5.HorizontalAlignment = Element.ALIGN_CENTER; //字置中
        Header_C5.VerticalAlignment = Element.ALIGN_MIDDLE; //水平置中
        BodyTb.AddCell(Header_C5); //將資料加入Table

        Int32 TotalTmp = 0;

        float SetBodyCellHeight = 15.0f;

        //將Body資料加入Table
        for (int i = 0; i < tmpBodyDt.Rows.Count; i++)
        {
            PdfPCell Body_C1 = new PdfPCell(new Phrase(tmpBodyDt.Rows[i]["PRODNAME"].ToString(), new Font(bfChinese, setFontSize, Font.NORMAL)));
            Body_C1.FixedHeight = SetBodyCellHeight; //設定欄位高度   
            Body_C1.HorizontalAlignment = Element.ALIGN_LEFT; //字靠左
            Body_C1.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
            BodyTb.AddCell(Body_C1); //將資料加入Table

            PdfPCell Body_C2 = new PdfPCell(new Phrase(tmpBodyDt.Rows[i]["DESCRIPTION"].ToString(), new Font(bfChinese, setFontSize, Font.NORMAL)));
            Body_C2.FixedHeight = SetBodyCellHeight; //設定欄位高度 
            Body_C2.HorizontalAlignment = Element.ALIGN_LEFT; //字靠左
            Body_C2.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
            BodyTb.AddCell(Body_C2); //將資料加入Table

            PdfPCell Body_C3 = new PdfPCell(new Phrase(tmpBodyDt.Rows[i]["QUANTITY"].ToString(), new Font(bfChinese, setFontSize, Font.NORMAL)));
            Body_C3.FixedHeight = SetBodyCellHeight; //設定欄位高度 
            Body_C3.HorizontalAlignment = Element.ALIGN_RIGHT; //字靠右
            Body_C3.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
            BodyTb.AddCell(Body_C3); //將資料加入Table

            TotalTmp += Convert.ToInt32(tmpBodyDt.Rows[i]["AMOUNT"].ToString());
            PdfPCell Body_C4 = new PdfPCell(new Phrase(string.Format("{0:N0}", tmpBodyDt.Rows[i]["AMOUNT"]), new Font(bfChinese, setFontSize, Font.NORMAL)));
            Body_C4.FixedHeight = SetBodyCellHeight; //設定欄位高度 
            Body_C4.HorizontalAlignment = Element.ALIGN_RIGHT; //字靠右
            Body_C4.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
            BodyTb.AddCell(Body_C4); //將資料加入Table

            PdfPCell Body_C5 = new PdfPCell(new Phrase(tmpBodyDt.Rows[i]["REMARK"].ToString(), new Font(bfChinese, setFontSize, Font.NORMAL)));
            Body_C5.FixedHeight = SetBodyCellHeight; //設定欄位高度 
            Body_C5.HorizontalAlignment = Element.ALIGN_LEFT; //字靠左
            Body_C5.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
            BodyTb.AddCell(Body_C5); //將資料加入Table


        }

        //塞空白行
        if ((SetPageRow - tmpBodyDt.Rows.Count) != 0)
        {
            PdfPCell SBody = new PdfPCell(new Phrase(" ", new Font(bfChinese, setFontSize, Font.NORMAL)));
            SBody.FixedHeight = SetBodyCellHeight; //設定欄位高度    
            for (int i = 0; i < (SetPageRow - tmpBodyDt.Rows.Count); i++)
            {

                BodyTb.AddCell(SBody); //將資料加入Table
                BodyTb.AddCell(SBody); //將資料加入Table
                BodyTb.AddCell(SBody); //將資料加入Table
                BodyTb.AddCell(SBody); //將資料加入Table
                BodyTb.AddCell(SBody); //將資料加入Table
            }

        }


        BodyTb.AddCell(""); //將資料加入Table
        BodyTb.AddCell(""); //將資料加入Table
        BodyTb.AddCell(""); //將資料加入Table

        PdfPCell Tot = new PdfPCell(new Phrase("Tot:" + string.Format("{0:N0}", TotalTmp), new Font(bfChinese, setFontSize, Font.NORMAL)));
        Tot.HorizontalAlignment = Element.ALIGN_RIGHT; //字靠右
        Tot.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
        BodyTb.AddCell(Tot); //將資料加入Table

        PdfPCell User = new PdfPCell(new Phrase("服務人員:" + tmpBodyDt.Rows[0]["S_USER"].ToString() + " " + new Employee_Facade().GetEmpName(tmpBodyDt.Rows[0]["S_USER"].ToString()), new Font(bfChinese, setFontSize, Font.NORMAL)));
        User.HorizontalAlignment = Element.ALIGN_LEFT; //字靠左
        User.VerticalAlignment = Element.ALIGN_CENTER; //水平置中
        BodyTb.AddCell(User); //將資料加入Table

        PdfPCell S = new PdfPCell(new Phrase(" "));
        S.Border = Rectangle.NO_BORDER; //去框架格線
        BodyTb.AddCell(S); //將資料加入Table
        BodyTb.AddCell(S); //將資料加入Table
        BodyTb.AddCell(S); //將資料加入Table
        BodyTb.AddCell(S); //將資料加入Table

        PdfPCell Sign = new PdfPCell(new Phrase("客戶簽收:_______________", new Font(bfChinese, setFontSize, Font.NORMAL)));
        Sign.Border = Rectangle.NO_BORDER; //去框架格線
        User.VerticalAlignment = Element.ALIGN_BOTTOM; //水平置底
        BodyTb.AddCell(Sign); //將資料加入Table

        PdfPCell S1 = new PdfPCell(new Paragraph("　", new Font(bfChinese, setFontSize, Font.NORMAL)));
        S1.Border = Rectangle.NO_BORDER; //去框架格線
        BodyTb.AddCell(S1); //將資料加入Table
        BodyTb.AddCell(S1); //將資料加入Table
        BodyTb.AddCell(S1); //將資料加入Table
        BodyTb.AddCell(S1); //將資料加入Table
        BodyTb.AddCell(S1); //將資料加入Table

        return BodyTb;
    }

    public string generateReceipt(string PREPAY_NO)
    {
        DataTable TitleDt = new PRE01_Facade().Query_PriPREPAY_Title(PREPAY_NO);

        if (TitleDt.Rows.Count > 0)
        {
            DataTable BodyDt = new PRE01_Facade().Query_PriPREPAY_Body(TitleDt.Rows[0]["ID"].ToString());

            MemoryStream Memory = new MemoryStream();
            //string printerName = @"\\192.168.8.100\HP LaserJet 3055 PCL5"; //"\\192.168.8.1\Brother MFC9600/9870 Series";
            //string printerName = @"HP LaserJet 3050 Series PCL 5e"; //印表機和傳真 
            //string printerName = @"\\192.168.8.100\HP LaserJet 3050 Series PCL 6"; //"\\192.168.8.1\Brother MFC9600/9870 Series";
            string printerName = ConfigurationManager.AppSettings["PDFPrinterName"].ToString();//web.config中設定

            //字型設定
            BaseFont bfChinese = BaseFont.CreateFont(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\..\Fonts\kaiu.ttf"
                                 , BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font ChFont_HeaderTitle_C1 = new Font(Font.FontFamily.HELVETICA, 20f, Font.BOLD);

            //設定PDF PageSize 及 Margin left,right,top,bottom
            pdfDoc = new Document(PageSize.A4, 10f, 10f, 20f, 20f);

            filename = Guid.NewGuid().ToString() + ".pdf";


            FileStream stream = new FileStream(Path.Combine(Server.MapPath("~/Downloads"), filename), FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
            pdfDoc.Open();

            // 加入自動列印指令碼
            //writer.AddSilentPrintAction(printerName);
            writer.AddJavaScript("this.print(false);", true);

            //文件開啟
            pdfDoc.Open();
            PdfPTable HeaderTb_Customer = getHeaderTb(TitleDt, "客戶聯", bfChinese);
            PdfPTable HeaderTb_Company = getHeaderTb(TitleDt, "公司聯", bfChinese);
            int SetPageRow = 6; //設定一頁最多幾行
            if (BodyDt.Rows.Count > 0)
            {
                DataTable tmpBodyDt = new DataTable();

                tmpBodyDt = BodyDt.Clone();

                for (int i = 0; i < BodyDt.Rows.Count; i++)
                {

                    tmpBodyDt.Rows.Add(BodyDt.Rows[i].ItemArray);
                    if (tmpBodyDt.Rows.Count == SetPageRow)
                    {
                        PdfPTable BodyTb = getBodyTb(tmpBodyDt, SetPageRow, bfChinese);

                        pdfDoc.Add(HeaderTb_Customer); //將客戶聯表頭加入主頁簽
                        pdfDoc.Add(BodyTb);
                        pdfDoc.Add(HeaderTb_Company); //將公司聯表頭加入主頁簽
                        pdfDoc.Add(BodyTb);
                        //換頁
                        pdfDoc.NewPage();
                        tmpBodyDt.Rows.Clear();
                    }
                }

                //如果不滿設定的最多行數，要再多跑一次將剩餘的資料產生
                if (tmpBodyDt.Rows.Count > 0)
                {
                    PdfPTable BodyTb = getBodyTb(tmpBodyDt, SetPageRow, bfChinese);

                    pdfDoc.Add(HeaderTb_Customer); //將客戶聯表頭加入主頁簽
                    pdfDoc.Add(BodyTb);
                    pdfDoc.Add(HeaderTb_Company); //將公司聯表頭加入主頁簽
                    pdfDoc.Add(BodyTb);
                    tmpBodyDt.Rows.Clear();
                }
            }
           
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

    public string DataHide(string strData, int intHide_S, int intHide_E)
    {
        string retData = "";
        string ReplaceTmpData = "";
        string tmpData = "";
        try
        {


            if (strData.Length > intHide_S && (strData.Length - intHide_S) > intHide_E)
            {
                ReplaceTmpData = strData.Substring(intHide_S, strData.Length - (intHide_S - 1) - intHide_E - 1);
                for (int i = 0; i <= ReplaceTmpData.Length; i++)
                    tmpData += "*";

                retData = strData.Remove(intHide_S, strData.Length - (intHide_S - 1) - intHide_E - 1);
                retData = retData.Insert(intHide_S, tmpData);
            }
            else
            {
                intHide_S = 1;
                ReplaceTmpData = strData.Substring(intHide_S, strData.Length - 1);
                for (int i = 0; i <= ReplaceTmpData.Length; i++)
                    tmpData += "*";

                retData = strData.Remove(intHide_S, strData.Length - 1);
                retData = retData.Insert(intHide_S, tmpData);
            }

        }
        catch (Exception)
        {


        }
        return retData;
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
