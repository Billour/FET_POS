using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using AdvTek.CustomControls;
using System.Data;
using System.Data.OracleClient;
using System.IO;

public partial class PrintTest : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            INVPrintName.Value = System.Configuration.ConfigurationManager.AppSettings["Invoice_PDFPrinterName"].ToString();
            RECPrintName.Value = System.Configuration.ConfigurationManager.AppSettings["Receipt_PDFPrinterName"].ToString();
            CrePrintName.Value = System.Configuration.ConfigurationManager.AppSettings["Credit_PDFPrinterName"].ToString();
        }
    }
    protected void InvoiceTest_Click(object sender, EventArgs e)
    {
     //   PrintName.Value = System.Configuration.ConfigurationManager.AppSettings["Invoice_PDFPrinterName"].ToString();//web.config中設定
      //  ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintInvoice", " Call_DetectPrinterName();", true);
        
        Receipt myReceipt = new Receipt();
        SAL01_Facade SAL01Facade = new SAL01_Facade();
        string POSUUID = "";
        POSUUID = INVOICE_ID.Text;

        if (POSUUID != "")
        {
            string filePath = SAL01Facade.getUploadPath(StringUtil.CStr(POSUUID));
            if (!string.IsNullOrEmpty(filePath))
            {
                
                string fileName = myReceipt.generateReceiptretest(StringUtil.CStr(POSUUID),"",INVPrintName.Value);
                if (fileName == null || string.IsNullOrEmpty(fileName))
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('重印發票失敗，無法取得電子發票檔名!!');", true);
                }
                else
                {
                    if (fileName == "nodata")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('查無任何資料');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PrintInvoice", "alert('發票列印測試');document.getElementById('" +
                                                                                  fDownload.ClientID + "').src='" + Request.ApplicationPath + filePath + "/" + fileName + "';", true);
                    }
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('列印發票失敗，此ID無任何資料!!');", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('請輸入ID');", true);
        }

       
    }
    protected void Receipt_Click(object sender, EventArgs e)
    {
        string url = Collection_Receipt(RECEIPT_ID.Text,RECPrintName.Value);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PrintInvoice", "alert('收據列印測試');document.getElementById('" +
                                                                           fDownload.ClientID + "').src='" + url+"';", true);
    }

    private string Collection_Receipt(string posuuid_master, string receiptname)
    {
        string return_url = "";
        List<List<string>> dir = new List<List<string>>();
        OracleConnection conn = null;
        OracleCommand cmd = null;
        OracleDataAdapter da = null;
        OracleDataReader dr = null;
        string trade_date = "";
        string store_no = "";
        string machice_id = "";
        string RECEIPT_NO = "";
        string total_amount = "";
        string barcode1 = "";
        string msisdn = "";
        string SALE_PERSON = "";
        string hg_card_no = "";
        string pay_mode1 = ""; //現金
        string pay_mode2 = ""; //信用卡
        string pay_mode7 = "";//HappyGo折抵
        string pay_mode8 = "";//找零金
        string sqlstr = "select sh.trade_date,SH.STORE_NO,SH.MACHINE_ID,RH.RECEIPT_NO,SH.SALE_PERSON,sh.hg_card_no from sale_head sh  join RECEIPT_HEAD rh on RH.POSUUID_MASTER = SH.POSUUID_MASTER where  sh.posuuid_master = " + OracleDBUtil.SqlStr(posuuid_master);
        try
        {
            conn = OracleDBUtil.GetConnection();
            cmd = new OracleCommand(sqlstr, conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                trade_date = dr.IsDBNull(0) ? "" : dr.GetDateTime(0).ToString("yyyy-MM-dd hh:mm");//StringUtil.CStr(dr[0]);
                store_no = StringUtil.CStr(dr[1]);
                machice_id = StringUtil.CStr(dr[2]);
                RECEIPT_NO = StringUtil.CStr(dr[3]);
                SALE_PERSON = StringUtil.CStr(dr[4]);
                hg_card_no = StringUtil.CStr(dr[5]);
            }
            dr.Read();

            //抓取detal
            sqlstr = "select barcode1,msisdn,total_amount,id from sale_detail where source_type = 3 and  posuuid_master = " + OracleDBUtil.SqlStr(posuuid_master);
            DataTable detail = new DataTable();
            cmd = new OracleCommand(sqlstr, conn);
            da = new OracleDataAdapter(cmd);
            da.Fill(detail);
            foreach (DataRow row in detail.Rows)
            {
                string sale_detail_id = StringUtil.CStr(row["id"]);
                barcode1 = StringUtil.CStr(row["barcode1"]);
                msisdn = StringUtil.CStr(row["msisdn"]);
                total_amount = StringUtil.CStr(row["total_amount"]);

                //抓出拆分金額
                //抓現金
                sqlstr = "select sum(amount) from bill_dispatch where pay_mode_id = 1 and sale_detail_id = " + OracleDBUtil.SqlStr(sale_detail_id);
                cmd = new OracleCommand(sqlstr, conn);
                pay_mode1 = cmd.ExecuteScalar() == null ? "0" : StringUtil.CStr(cmd.ExecuteScalar());

                //抓信用卡
                sqlstr = "select sum(amount) from bill_dispatch where pay_mode_id in(2,3,4) and sale_detail_id = " + OracleDBUtil.SqlStr(sale_detail_id);
                cmd = new OracleCommand(sqlstr, conn);
                pay_mode2 = cmd.ExecuteScalar() == null ? "0" : StringUtil.CStr(cmd.ExecuteScalar());

                //找零金
                sqlstr = "select sum(amount) from bill_dispatch where pay_mode_id = 8 and sale_detail_id = " + OracleDBUtil.SqlStr(sale_detail_id);
                cmd = new OracleCommand(sqlstr, conn);
                pay_mode8 = cmd.ExecuteScalar() == null ? "0" : StringUtil.CStr(cmd.ExecuteScalar());

                //happyGo
                sqlstr = "select sum(amount) from bill_dispatch where pay_mode_id = 7 and sale_detail_id = " + OracleDBUtil.SqlStr(sale_detail_id);
                cmd = new OracleCommand(sqlstr, conn);
                pay_mode7 = cmd.ExecuteScalar() == null ? "0" : StringUtil.CStr(cmd.ExecuteScalar());

                //判斷類型
                if (barcode1.Length == 10)
                {
                    //FET
                    List<string> list = new List<string>();
                    list.Add("4");
                    list.Add(trade_date);
                    list.Add(store_no);
                    list.Add(machice_id);
                    list.Add(RECEIPT_NO);
                    list.Add("遠傳電信帳單");
                    list.Add(total_amount);
                    list.Add(barcode1);
                    list.Add(msisdn);
                    list.Add(pay_mode1);
                    list.Add(pay_mode2);
                    list.Add(pay_mode7);
                    list.Add("0");
                    list.Add(hg_card_no);
                    list.Add(SALE_PERSON);
                    dir.Add(list);
                }
                else if (barcode1.Length == 14)
                {
                    //KGT
                    List<string> list = new List<string>();
                    list.Add("5");
                    list.Add(trade_date);
                    list.Add(store_no);
                    list.Add(machice_id);
                    list.Add(RECEIPT_NO);
                    list.Add("和信電信帳單");
                    list.Add(total_amount);
                    list.Add(barcode1);
                    list.Add(pay_mode1);
                    list.Add(pay_mode2);
                    list.Add(pay_mode7);
                    list.Add(pay_mode8);
                    list.Add(hg_card_no);
                    list.Add(SALE_PERSON);
                    dir.Add(list);
                }
            }

            string fileName = "";
            if (dir.Count > 0)
            {
                SAL01_Facade facade = new SAL01_Facade();
                //string filePath = facade.getUploadPath(posuuid_master);
                IRClass pri = new PriReceipt();
                fileName = pri.Print("M", null, dir, receiptname);

                if (fileName == null || string.IsNullOrEmpty(fileName))
                {
                    throw new Exception("列印收據失敗，請重印收據!!");
                }
                else
                {
                    return_url = Request.ApplicationPath + "/Downloads/Receipt/" + fileName;
                }
            }
            if (dir.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('列印收據失敗，此ID無任何資料!!');", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            OracleConnection.ClearPool(conn);
        }
        return return_url;
    }

    protected void Credit_Click(object sender, EventArgs e)
    {
        //  ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintInvoice", " Call_DetectPrinterName();", true);

        Receipt myReceipt = new Receipt();
        SAL01_Facade SAL01Facade = new SAL01_Facade();
        string POSUUID = "";
        POSUUID = CREDIT_ID.Text;

        if (POSUUID != "")
        {
            string filePath = SAL01Facade.getUploadPath(StringUtil.CStr(POSUUID));
            if (!string.IsNullOrEmpty(filePath))
            {
                string fileName = myReceipt.getnerateDebitNote(StringUtil.CStr(POSUUID),CrePrintName.Value);
                if (fileName == null || string.IsNullOrEmpty(fileName))
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('列印折讓單失敗，請重印折讓單!!');", true);
                }
                else
                {
                //    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('列印折讓單測試');", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PrintInvoice", "alert('折讓單列印測試');document.getElementById('" +
                                                                              fDownload.ClientID + "').src='" + Request.ApplicationPath + filePath + "/" + fileName + "';", true);
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('列印折讓單失敗，ID查無資料!!');", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('請輸入ID');", true);
        }
      
    }
    protected void General_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PrintInvoice", "alert('一般列印測試');document.getElementById('" +
                                                                           fDownload.ClientID + "').src='" + Request.ApplicationPath + "/Downloads/Testinv.pdf';", true);
    }
    protected void Barcode_Print_Click(object sender, EventArgs e)
    {
        if (ORDER_NO.Text == "" || Rec_NO.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('請輸入OrderNO & 驗收單號');", true);
        }
        else
        {
            string ORDERNO1 = ORDER_NO.Text;
            string recno = Rec_NO.Text;
            INV08_Facade _INV08_Facade = new INV08_Facade();
            DataTable dt = _INV08_Facade.GetINVD_INVANO(recno);

            string barcodestr = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //if (dataList == "")'
                //    dataList = txtProdNo2.value + "^" + txtProdName2.value + "^" + txtCount2.value;
                //else
                //    dataList = dataList + "|" + txtProdNo2.value + "^" + txtProdName2.value + "^" + txtCount2.value;
                if (barcodestr == "")
                {
                    barcodestr = StringUtil.CStr(dt.Rows[i]["PRODNO"]) + "^" + StringUtil.CStr(dt.Rows[i]["PRODNAME"]) + "^" + StringUtil.CStr(dt.Rows[i]["IN_QTY"]);
                }
                else
                {
                    barcodestr = barcodestr + "|" + StringUtil.CStr(dt.Rows[i]["PRODNO"]) + "^" + StringUtil.CStr(dt.Rows[i]["PRODNAME"]) + "^" + StringUtil.CStr(dt.Rows[i]["IN_QTY"]);
                }
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PrintInvoice", " Call_BarcodePrintFile(\"" + ORDERNO1 + "\",\"" + barcodestr + "\");", true);
        }
        
    }
    //protected void testprintr_Click(object sender, EventArgs e)
    //{
    //    List<string> LD = new List<string>();
    //    //string filePath = new PRE01_Facade().getUploadPath("", "");
    //    string strPath = Common_PageHelper.GetPriReceipt_Path();
    //    //foreach (KeyValuePair<string, List<List<string>>> item in LR)
    //    //{
    //    //    foreach (List<string> item1 in item.Value)
    //    //    {
    //    //        string fileName = Print(item.Key, item1, null);
    //    //        LD.Add(HttpContext.Current.Request.ApplicationPath + filePath + "/" + fileName);

    //    //    }
    //    //}
    //    //foreach (List<string> item in LR)
    //    //{
    //    //    string fileName = Print(item[0], item, null);
    //    //    LD.Add(HttpContext.Current.Request.ApplicationPath + strPath + "/" + fileName);
    //    //}
    //    //for (int i=0; i < LR.Count; i++)
    //    //{ 
    //    //Print (LD)
    //    //}
    //    PdfReader reader;
    //    Document document = new Document();
    //    string filename = "";
    //    strPath = "~" + strPath;

    //    //判斷資料夾是否存在 不存在則建一個
    //    if (!Directory.Exists(HttpContext.Current.Server.MapPath(strPath)))
    //        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(strPath));
    //    PriZeroReceipt x = new PriZeroReceipt();

    //    filename = x.generateReceipt(LD, "BFD0BEAFF3D143EC89762E7906EB31F5", INVPrintName.Value,strPath);
    //    if (filename == null || string.IsNullOrEmpty(filename))
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('列印折讓單失敗，請重印折讓單!!');", true);
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PrintInvoice", "alert('折讓單列印測試');document.getElementById('" +
    //                                                                  fDownload.ClientID + "').src='" + strPath + "/" + filename + "';", true);
    //    }

     //   FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(strPath), filename), FileMode.Create);
     //   PdfWriter writer = PdfWriter.GetInstance(document, stream);

     //   document.Open();
     //   // 加入自動列印指令碼
     //   writer.AddJavaScript("this.print(false);", true);
     //   PdfContentByte cb = writer.DirectContent;
     //   PdfImportedPage newPage;
     //   for (int i = 0; i < LD.Count(); i++)
     //   {
     //       reader = new PdfReader(HttpContext.Current.Server.MapPath(LD[i]));
     //       int iPageNum = reader.NumberOfPages;
     //       for (int j = 1; j <= iPageNum; j++)
     //       {
     //           document.NewPage();
     //           newPage = writer.GetImportedPage(reader, j);
     //           cb.AddTemplate(newPage, 0, 0);
     //       }
     //   }
     //   document.Close();
     ////   return filename;
    

}
