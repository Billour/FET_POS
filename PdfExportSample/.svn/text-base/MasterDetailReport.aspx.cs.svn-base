using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

/// <summary>
/// PDF巢狀式報表產出範例
/// </summary>
public partial class MasterDetailReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    int priceTotal = 0;
    int quantityTotal = 0;

    /// <summary>
    /// 處理彙總資訊
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxOrderDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            quantityTotal += Convert.ToInt32((e.Row.DataItem as DataRowView)["Quantity"]);
            priceTotal += Convert.ToInt32((e.Row.DataItem as DataRowView)["Subtotal"]);            
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells.RemoveAt(1);
            e.Row.Cells.RemoveAt(1);

            e.Row.Cells[0].ColumnSpan = 3;
            e.Row.Cells[0].Text = "Total:";

            e.Row.Cells[1].Text = quantityTotal.ToString();
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            quantityTotal = 0;

            e.Row.Cells[2].Text = priceTotal.ToString("N0");
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            priceTotal = 0;
        }
    }

    /// <summary>
    /// 從GridView匯出PDF文件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void uxExport_Click(object sender, EventArgs e)
    {               
        Font smallFont = ChineseFontFactory.Create("細明體", 9f, Font.NORMAL);
        Font largerFont = ChineseFontFactory.Create("細明體", 12f, Font.BOLD);

        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();

        Paragraph title = new Paragraph();
        title.Alignment = Element.ALIGN_CENTER;
        title.Add(new Chunk(Page.Title, largerFont));
        pdfDoc.Add(title);
                        
        foreach (GridViewRow row in uxOrders.Rows)
        {
            PdfPTable pdfTable = new PdfPTable(1);
            pdfTable.DefaultCell.Border = 0;            
            pdfTable.SplitRows = false;
            pdfTable.KeepTogether = true;

            float[] widths = {50f, 150f};
            PdfPTable header = new PdfPTable(widths);
            header.DefaultCell.Border = 0;
            header.TotalWidth = 200f;
            header.LockedWidth = true;
            header.HorizontalAlignment = PdfPCell.ALIGN_LEFT;

            header.AddCell(new Paragraph("訂單編號：", smallFont));
            header.AddCell(new Paragraph((row.FindControl("uxOrderNumber") as Label).Text, smallFont));
            header.AddCell(new Paragraph("訂購人：", smallFont));
            header.AddCell(new Paragraph((row.FindControl("uxCustomerName") as Label).Text, smallFont));
            header.AddCell(new Paragraph("訂購日期：", smallFont));
            header.AddCell(new Paragraph((row.FindControl("uxOrderDate") as Label).Text, smallFont));
            pdfTable.AddCell(header);
            
            GridView uxOrderDetails = row.FindControl("uxOrderDetails") as GridView;
            PdfPTable details = uxOrderDetails.ToPdfTable(null, 20);
            pdfTable.AddCell(details);
            pdfDoc.Add(pdfTable);
        }
        
        pdfDoc.Close();

        string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment; filename=" + filename);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Write(pdfDoc);
        Response.End();          
    }    

}
