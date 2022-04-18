using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
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

/// <summary>
/// 電子發票範例
/// </summary>
public partial class Receipt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    /// <summary>
    /// 將HTML轉換成PDF文件(iTextSharp似乎仍無法轉換複雜的HTML表格)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxExport_Click(object sender, EventArgs e)
    {
        Document pdfDoc = new Document( PageSize.A4, 0, 0, 0, 0 );
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
                       
        string htmlString = RenderControl(uxMainListView);       
        htmlString = "<html><body>" + htmlString + "</body></html>"; 

        StringReader stringReader = new StringReader(htmlString);
        FontFactory.Register(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\..\Fonts\simhei.ttf");
        FontFactory.Register(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\..\Fonts\kaiu.ttf");

        StyleSheet st = new StyleSheet();      
        st.LoadTagStyle("body", "face", "SIMHEI"); // 字型
        st.LoadTagStyle("body", "size", "9px"); // 字體大小
        st.LoadTagStyle("body", "encoding", "Identity-H"); // 顯示中文字

        HTMLWorker worker = new HTMLWorker(pdfDoc);
        ArrayList elements = HTMLWorker.ParseToList(stringReader, st);

        for (int i = 0; i < elements.Count; i++)
        {            
            pdfDoc.Add((IElement)elements[i]);
        }
        pdfDoc.Close();

        string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment; filename=" + filename);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Write(pdfDoc);
        Response.End();   
    }

    /// <summary>
    /// 將伺服器控制項轉換為前端的HTML碼
    /// </summary>
    /// <param name="ctrl"></param>
    /// <returns></returns>
    public string RenderControl(Control ctrl)
    {
        StringBuilder sb = new StringBuilder();
        StringWriter tw = new StringWriter(sb);
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        // Prevent exception: must be placed inside a form tag with runat=server
        HtmlForm form = new HtmlForm();
        Controls.Add(form);
        form.Controls.Add(ctrl);
        form.RenderControl(hw);

        string htmlString = sb.ToString();
        htmlString = Regex.Replace(htmlString, @"<form\s[^>]+>|</form>", "");
        htmlString = Regex.Replace(htmlString, @"<input type=""hidden"" name=""__VIEWSTATE""\s[^>]+/>", "");

        return htmlString;
    }

    /// <summary>
    /// 顯示彙總資訊
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxNestedListView_DataBound(object sender, EventArgs e)
    {
        ListView uxNestedListView = sender as ListView;
        (uxNestedListView.FindControl("uxAmountTotal") as Label).Text = amountTotal.ToString();
        amountTotal = 0;

    }

    int amountTotal = 0;
    /// <summary>
    /// 處理彙總資訊、資料列合併
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxNestedListView_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem dataItem = (ListViewDataItem)e.Item;    
            DataRowView rowView = (DataRowView)dataItem.DataItem;
            HtmlTableRow tr = e.Item.FindControl("uxTr") as HtmlTableRow;

            if (dataItem.DataItemIndex == 0)
            {
                
                tr.Cells[4].RowSpan = rowView.DataView.Count + 5;
            }
            else
            {
                tr.Cells.RemoveAt(4);
            }

            amountTotal += Convert.ToInt32(rowView["Subtotal"]);
        }
    }
}
