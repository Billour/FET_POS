using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

/// <summary>
/// PDF明細報表產出範例
/// </summary>
public partial class DetailReport : System.Web.UI.Page
{    
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    /*
    public static void ExportToPdf(HttpResponse Response, GridView grid)
    {
        string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
       
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        HtmlForm form = new HtmlForm();
        grid.AllowPaging = false;        
        grid.Parent.Controls.Add(form);       
        form.Controls.Add(grid);
        form.RenderControl(hw);

        string html = sw.ToString();
       
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
       
        System.Xml.XmlTextReader xmlReader =
        new System.Xml.XmlTextReader(new StringReader(html));
        HtmlParser.Parse(pdfDoc, xmlReader);

        pdfDoc.Close();

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment; filename=" + filename);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Write(pdfDoc);
        Response.End();
    }
    */

    int priceTotal = 0;

    /// <summary>
    /// 處理彙總資訊
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            priceTotal += Convert.ToInt32((e.Row.DataItem as DataRowView)["Price"]);
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells.RemoveAt(1);
            e.Row.Cells.RemoveAt(1);

            e.Row.Cells[0].ColumnSpan = 3;
            e.Row.Cells[0].Text = "Total:";

            e.Row.Cells[1].Text = priceTotal.ToString("N0");
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
        }
    }

    /// <summary>
    /// 從GridView匯出PDF文件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxExportGridViewToPdf_Click(object sender, EventArgs e)
    {
        string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
        uxProducts.ExportToPdf(Page.Title, filename);                
    }

    /// <summary>
    /// 從DataTable匯出PDF文件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxExportDataTableToPdf_Click(object sender, EventArgs e)
    {
        
        DataView dataView = SalesDataSource.Select(DataSourceSelectArguments.Empty) as DataView;
        DataTable dataTable = dataView.ToTable();
        Document pdfDoc = dataTable.ToPdfDocument(Response.OutputStream, 20);
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Write(pdfDoc);
        Response.End();
    }
}
