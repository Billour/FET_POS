using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.XtraPrinting;
using System.Drawing;
using DevExpress.XtraPrintingLinks;
using DevExpress.Web.ASPxGridView.Export.Helper;
using DevExpress.Web.ASPxGridView.Export;
using System.IO;
using System.Data;

/// <summary>
/// AbstractXLSExportor 的摘要描述
/// </summary>
public class AbstractXLSExportor : BasePage, Exportor
{
    protected PrintingSystem ps;
    protected Link PageHeader;
    protected Link ReportHeader;
    protected Link ReportFooter;
    protected Link PageFooter;
    protected ASPxGridViewExporter ASPxGridViewExporter1;

    private string TitleName;
    protected DataTable Header;
    private DataTable Footer;
    private string Bottom;

    protected object[] Objects;

    public int PageWidth = 500;

    public AbstractXLSExportor()
    {

    }

    #region Exportor 成員

    public virtual bool accept(string model)
    {
        return true;
    }

    public virtual void prepareData(int Width)
    {
        ps = new PrintingSystem();
        PageWidth = Width;
    }

    public virtual void exportTitle(string Title)
    {
        this.TitleName = Title;
        PageHeader = new Link();
        PageHeader.CreateDetailArea += new CreateAreaEventHandler(PageHeader_CreateDetailArea);
    }

    public virtual void exportHeader(DataTable dtHeader)
    {
       this.Header = dtHeader;
       ReportHeader = new Link();
       ReportHeader.CreateDetailArea += new CreateAreaEventHandler(ReportHeader_CreateDetailArea);
    }

    public virtual void exportData(ASPxGridViewExporter gridExport)
    {
        this.ASPxGridViewExporter1 = gridExport;
    }

    public virtual void exportFooter(DataTable dtFooter)
    {
        this.Footer = dtFooter;
        ReportFooter = new Link();
        ReportFooter.CreateDetailArea += new CreateAreaEventHandler(ReportFooter_CreateDetailArea);

    }

    public virtual void exportBottom(string BottomDesc)
    {
        this.Bottom = BottomDesc;
        PageFooter = new Link();
        PageFooter.CreateDetailArea += new CreateAreaEventHandler(PageFooter_CreateDetailArea);
    }

    public virtual void output()
    {

        CompositeLink compositeLink = new CompositeLink(ps);
        compositeLink.Links.AddRange(new object[] { PageHeader, ReportHeader, new GridViewLink(ASPxGridViewExporter1), ReportFooter, PageFooter });
        compositeLink.CreateDocument();

        using (MemoryStream stream = new MemoryStream())
        {
            compositeLink.PrintingSystem.ExportToXls(stream);
            Session["XLSstream"] = stream;
            ps.Dispose();
        }

    }

    #endregion

    protected virtual void PageHeader_CreateDetailArea(object sender, CreateAreaEventArgs e)
    {
        if (!string.IsNullOrEmpty(TitleName))
        {
            TextBrick Brick = new TextBrick();

            TextBrick tb = new TextBrick();
            tb.Text = this.TitleName;
            tb.Font = new System.Drawing.Font("標楷體", 15, FontStyle.Bold);
            tb.Rect = new RectangleF(0, 0, PageWidth, 30);
            tb.BorderWidth = 0;
            tb.BackColor = Color.Silver;
            tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            Brick = tb;

            e.Graph.DrawBrick(Brick);
        }
    }
    protected virtual void ReportHeader_CreateDetailArea(object sender, CreateAreaEventArgs e)
    {
        if (Header != null)
        {
            int i = 0;
            foreach (DataRow dr in Header.Rows)
            {
                string[] strText = dr["Text"].ToString().Split('|');
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                TextBrick tb = new TextBrick();

                foreach (string text in strText)
                {
                    sb.Append(text + System.Environment.NewLine);
                    tb.Text = sb.ToString().Substring(0, sb.ToString().Length - 2);
                    tb.Font = new System.Drawing.Font("標楷體", Convert.ToInt16(dr["FontSize"].ToString()), FontStyle.Regular);
                    tb.Rect = new RectangleF(i * (PageWidth / Header.Rows.Count), 0, PageWidth / Header.Rows.Count, 21 * strText.Length);  //21為像素，等於Excel預設高度15.75
                    tb.BorderWidth = 0;
                    tb.BackColor = System.Drawing.ColorTranslator.FromHtml(dr["BackColor"].ToString());
                    tb.HorzAlignment = dr["Align"].ToString() == "LEFT" ? DevExpress.Utils.HorzAlignment.Near :
                    dr["Align"].ToString() == "RIGHT" ? DevExpress.Utils.HorzAlignment.Far : DevExpress.Utils.HorzAlignment.Center;
                }
                e.Graph.DrawBrick(tb);

                i++;
            }
        }

    }
    protected virtual void ReportFooter_CreateDetailArea(object sender, CreateAreaEventArgs e)
    {
        //TextBrick[] Bricks = null;

        if (Footer != null)
        {
            //Bricks = new TextBrick[dtFooter.Rows.Count];

            int i = 0;
            foreach (DataRow dr in Footer.Rows)
            {
                TextBrick tb = new TextBrick();
                string[] strText = dr["Text"].ToString().Split('|');

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (string text in strText)
                {
                    sb.Append(text + System.Environment.NewLine);
                }
                tb.Text = sb.ToString().Substring(0, sb.ToString().Length - 2);
                tb.Font = new System.Drawing.Font("標楷體", Convert.ToInt16(dr["FontSize"].ToString()), FontStyle.Regular);
                tb.Rect = new RectangleF(i * (PageWidth / Footer.Rows.Count), 0, PageWidth / Footer.Rows.Count, 21 * strText.Length);  //21為像素，等於Excel預設高度15.75
                tb.BorderWidth = 0;
                tb.BackColor = System.Drawing.ColorTranslator.FromHtml(dr["BackColor"].ToString());
                tb.HorzAlignment = dr["Align"].ToString() == "LEFT" ? DevExpress.Utils.HorzAlignment.Near :
                    dr["Align"].ToString() == "RIGHT" ? DevExpress.Utils.HorzAlignment.Far : DevExpress.Utils.HorzAlignment.Center;

                //Bricks[i] = tb;
                e.Graph.DrawBrick(tb);
                i++;
            }
        }

    }
    protected virtual void PageFooter_CreateDetailArea(object sender, CreateAreaEventArgs e)
    {
        if (!string.IsNullOrEmpty(Bottom))
        {
            TextBrick textBrick = new TextBrick(BorderSide.None, 0, Color.Red, Color.Silver, Color.Black);
            textBrick.Rect = new RectangleF(0, 0, PageWidth, 30);
            textBrick.Text = Bottom;
            textBrick.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;

            e.Graph.DrawBrick(textBrick);
        }

    }

}
