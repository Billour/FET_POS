using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Advtek.Utility;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
/// <summary>
/// RClass 的摘要描述
/// </summary>
public interface IRClass
{
    string Print(string Type, List<string> LF, List<List<string>> LR, string PrintName);
}

public class PrintClass
{
    public static IRClass print()
    {
        return new PriReceipt();
    }
}

public class PriReceipt : IRClass
{

    #region IRClass 成員

    public string Print(string Type, List<string> LF, List<List<string>> LR, string PrintName)
    {

        switch (Type)
        {
            case "1": //遠通電收e通卡加值
                PriECard PriECard1 = new PriECard();
                return PriECard1.generateReceipt(LF, PrintName);
            case "2"://遠通電信保證金
                PriGuarantee PriGuarantee = new PriGuarantee();
                return PriGuarantee.generateReceipt(LF, PrintName);
            case "3"://ETC代收
                PriETC PriETC = new PriETC();
                return PriETC.generateReceipt(LF, PrintName);
            case "4"://遠傳電信帳單

            case "5"://何信電信帳單

            case "6"://速博電信條碼
                PrFET PrFET1 = new PrFET();
                return PrFET1.generateReceipt(LF, Type, PrintName);
            case "7"://遠通電信保證金 car
                PriGuaranteeCar PriGuaranteeCar = new PriGuaranteeCar();
                return PriGuaranteeCar.generateReceipt(LF, PrintName);
            case "8"://Seednet帳單
                PriSEEDNET PriSEEDNET = new PriSEEDNET();
                return PriSEEDNET.generateReceipt(LF, PrintName);
            case "M":
                List<string> LD = new List<string>();
                //string filePath = new PRE01_Facade().getUploadPath("", "");
                string strPath =  Common_PageHelper.GetPriReceipt_Path();
                //foreach (KeyValuePair<string, List<List<string>>> item in LR)
                //{
                //    foreach (List<string> item1 in item.Value)
                //    {
                //        string fileName = Print(item.Key, item1, null);
                //        LD.Add(HttpContext.Current.Request.ApplicationPath + filePath + "/" + fileName);

                //    }
                //}
                foreach (List<string> item in LR)
                {
                    string fileName = Print(item[0], item, null, PrintName);
                    LD.Add(HttpContext.Current.Request.ApplicationPath + strPath + "/" + fileName);
                }
                //for (int i=0; i < LR.Count; i++)
                //{ 
                //Print (LD)
                //}
                PdfReader reader;
                Document document = new Document();
                string filename = Guid.NewGuid().ToString() + ".pdf";

                strPath = "~" + strPath;

                //判斷資料夾是否存在 不存在則建一個
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(strPath)))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(strPath));

                FileStream stream = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(strPath), filename), FileMode.Create);
                PdfWriter writer = PdfWriter.GetInstance(document, stream);

                document.Open();
                // 加入自動列印指令碼
               // writer.AddJavaScript("this.print(false);", true);
                AddPrintAction(writer, PrintName);
                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage newPage;
                for (int i = 0; i < LD.Count(); i++)
                {
                    reader = new PdfReader(HttpContext.Current.Server.MapPath(LD[i]));
                    int iPageNum = reader.NumberOfPages;
                    for (int j = 1; j <= iPageNum; j++)
                    {
                        document.NewPage();
                        newPage = writer.GetImportedPage(reader, j);
                        cb.AddTemplate(newPage, 0, 0);
                    }
                }
                document.Close();
                return filename;
            default:
                return "";
        }

    }

    #endregion

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
