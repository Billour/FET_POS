using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.XtraPrintingLinks;
using System.IO;
using DevExpress.Web.ASPxGridView.Export.Helper;
using DevExpress.XtraPrinting;
using System.Drawing;
using System.Data;

/// <summary>
/// RPL044 的摘要描述
/// </summary>
public class RPL044 : AbstractXLSExportor
{
    public RPL044()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

    public override bool accept(string model)
    {
        return (model.ToUpper() == "RPL044".ToUpper());
    }

    public override void output()
    {
        Link GVTitle = new Link();
        GVTitle.CreateDetailArea += new CreateAreaEventHandler(GVTitle_CreateDetailArea);

        CompositeLink compositeLink = new CompositeLink(ps);
        compositeLink.Links.AddRange(new object[] { PageHeader, ReportHeader, GVTitle, new GridViewLink(ASPxGridViewExporter1), ReportFooter, PageFooter });
        compositeLink.CreateDocument();

        using (MemoryStream stream = new MemoryStream())
        {
            compositeLink.PrintingSystem.ExportToXls(stream);
            Session["XLSstream"] = stream;
            ps.Dispose();
        }

    }

    protected void GVTitle_CreateDetailArea(object sender, CreateAreaEventArgs e)
    {
            TextBrick Brick = new TextBrick();

            DataRow dr = Header.Rows[0];
            string[] strText = dr["Text"].ToString().Split('|');

            TextBrick tb = new TextBrick();
            tb.Text = strText[strText.Length-1];
            tb.Font = new System.Drawing.Font("標楷體", Convert.ToInt16(dr["FontSize"].ToString()), FontStyle.Regular);
            tb.Rect = new RectangleF(0, 0, PageWidth, 21);
            tb.BorderWidth = 0;
            tb.BackColor = Color.SkyBlue;
            tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            Brick = tb;

            e.Graph.DrawBrick(Brick);
    }

    protected override void ReportHeader_CreateDetailArea(object sender, CreateAreaEventArgs e)
    {
        if (Header != null)
        {
            int i = 0;
            foreach (DataRow dr in Header.Rows)
            {
                string[] strText = dr["Text"].ToString().Split('|');
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                TextBrick tb = new TextBrick();

                for (int j = 0; j< strText.Length-1; j++)
                {
                    string text = strText[j];
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


}
