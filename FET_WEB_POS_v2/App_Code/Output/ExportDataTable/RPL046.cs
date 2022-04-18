using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Runtime.Serialization.Formatters.Binary;
using iTextSharp.text;
using iTextSharp.text.xml;
/// <summary>
/// RPL046 的摘要描述
/// </summary>
public class RPL046 : AbstractXLS
{
    public RPL046()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

    public override bool accept(string model)
    {
        return (model == "RPL046");
    }

    public override void exportHeader(DataTable dtHeader)
    {
        // 表頭資訊
        if (dtHeader != null)
        {


            for (int i = 0; i < dtHeader.Rows.Count; i++)
            {
                TableRow HeadRow = new TableRow();
                inWebTable.Rows.Add(HeadRow);

                for (int j = 0; j < dtHeader.Columns.Count; j++)
                {
                    TableCell c = new TableCell();
                    c.Text = dtHeader.Rows[i][j].ToString();
                    c.ColumnSpan = 8;
                    HeadRow.Cells.Add(c);

                }

            }

        }
        else
        {
            TableRow NullRow = new TableRow();
            inWebTable.Rows.Add(NullRow);
            TableCell c = new TableCell();
            c.Text = "";
            NullRow.Cells.Add(c);
        }
    }
    public override void exportDataHeader()
    {

        TableRow theHeadRow = new TableRow();
        inWebTable.Rows.Add(theHeadRow);

        for (int i = 0; i < this.dtData.Columns.Count; i++)
        {
            TableCell clSpace = new TableCell();
            clSpace.BorderWidth = (Unit)1;
            clSpace.VerticalAlign = VerticalAlign.Middle; //垂直置中
            clSpace.HorizontalAlign = HorizontalAlign.Center;//水平置中

            switch (this.dtData.Columns[i].ToString())
            {
                case "發票號碼":
                    clSpace.Text = "發票號碼";
                    clSpace.ColumnSpan = 2;
                    theHeadRow.Cells.Add(clSpace);
                    break;
                case "帳單號碼_條碼一":
                case "門號_條碼二":
                case "條碼三":
                case "POSUUID_MASTER":
                    break;

                case "商品名稱":
                    clSpace.RowSpan = 1;
                    clSpace.Text = this.dtData.Columns[i].ToString();
                    theHeadRow.Cells.Add(clSpace);
                    break;
                default:
                    clSpace.RowSpan = 2;
                    clSpace.Text = this.dtData.Columns[i].ToString();
                    theHeadRow.Cells.Add(clSpace);
                    break;
            }

        }

        TableRow theHeadRow1 = new TableRow();
        inWebTable.Rows.Add(theHeadRow1);

        TableCell clSpace1 = new TableCell();

        clSpace1 = new TableCell();
        clSpace1.Text = this.dtData.Columns["帳單號碼_條碼一"].ToString().Replace("_", " / ").ToString();
        clSpace1.VerticalAlign = VerticalAlign.Middle; //垂直置中
        clSpace1.HorizontalAlign = HorizontalAlign.Center;//水平置中
        clSpace1.BorderWidth = 1;
        theHeadRow1.Cells.Add(clSpace1);

        clSpace1 = new TableCell();
        clSpace1.Text = this.dtData.Columns["門號_條碼二"].ToString().Replace("_", " / ").ToString();
        clSpace1.VerticalAlign = VerticalAlign.Middle; //垂直置中
        clSpace1.HorizontalAlign = HorizontalAlign.Center;//水平置中
        clSpace1.BorderWidth = 1;
        theHeadRow1.Cells.Add(clSpace1);

        clSpace1 = new TableCell();
        clSpace1.Text = this.dtData.Columns["條碼三"].ToString().ToString();
        clSpace1.VerticalAlign = VerticalAlign.Middle; //垂直置中
        clSpace1.HorizontalAlign = HorizontalAlign.Center;//水平置中
        clSpace1.BorderWidth = 1;
        theHeadRow1.Cells.Add(clSpace1);

    }
    public override void exportData()
    {
        int i = 0;

        foreach (DataRow dr in dtData.Rows)
        {
            TableRow r = new TableRow();
            TableCell c = new TableCell();

            if (!String.IsNullOrEmpty(dr["交易日期"].ToString()))
            {
                for (int j = 0; j < dtData.Columns.Count; j++)
                {
                    c = new TableCell();
                    switch (this.dtData.Columns[j].ToString())
                    {
                        case "發票號碼":
                            c.Text = dr[j].ToString();
                            c.ColumnSpan = 2;
                            c.BorderWidth = 1;
                            r.Cells.Add(c);
                            break;
                        case "帳單號碼_條碼一":
                        case "門號_條碼二":
                        case "條碼三":
                        case "POSUUID_MASTER":
                            break;

                        case "商品名稱":
                            c.RowSpan = 1;
                            c.Text = dr[j].ToString();
                            c.BorderWidth = 1;
                            r.Cells.Add(c);
                            break;

                        default:
                            //if (i == 31)
                            //    j = j;

                            c.RowSpan = 2;
                            c.Text = dr[j].ToString();
                            c.BorderWidth = 1;
                            r.Cells.Add(c);
                            break;
                    }
                }
                i++;
                inWebTable.Rows.Add(r);

                r = new TableRow();
                c = new TableCell();

                c = new TableCell();
                c.Text = "'" + Advtek.Utility.StringUtil.CStr(dr["帳單號碼_條碼一"].ToString().Replace("_", " / "));
                c.VerticalAlign = VerticalAlign.Middle; //垂直置中
                c.HorizontalAlign = HorizontalAlign.Center;//水平置中
                c.BorderWidth = 1;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "'" + dr["門號_條碼二"].ToString().Replace("_", " / ");
                c.VerticalAlign = VerticalAlign.Middle; //垂直置中
                c.HorizontalAlign = HorizontalAlign.Center;//水平置中
                c.BorderWidth = 1;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "'" + dr["條碼三"].ToString().Replace("_", " / ");
                c.VerticalAlign = VerticalAlign.Middle; //垂直置中
                c.HorizontalAlign = HorizontalAlign.Center;//水平置中
                c.BorderWidth = 1;
                r.Cells.Add(c);

                inWebTable.Rows.Add(r);

            }
            else
            {
                for (int j = 0; j < dtData.Columns.Count; j++)
                {
                    c = new TableCell();
                    switch (this.dtData.Columns[j].ToString())
                    {
                        case "發票號碼":
                        case "未稅金額":
                        case "銷售金額":
                            c.Text = dr[j].ToString();
                            c.ColumnSpan = 2;
                            c.BorderWidth = 0;
                            r.Cells.Add(c);
                            break;
                        case "帳單號碼_條碼一":
                        case "門號_條碼二":
                        case "條碼三":
                        case "稅額":
                        case "促銷代碼":
                        case "POSUUID_MASTER":
                            break;

                        case "商品名稱":
                            c.RowSpan = 1;
                            c.Text = dr[j].ToString();
                            c.BorderWidth = 0;
                            r.Cells.Add(c);
                            break;

                        default:
                            //if (i == 31)
                            //    j = j;

                            if (!String.IsNullOrEmpty(dr["交易日期"].ToString())) c.RowSpan = 2;
                            c.Text = dr[j].ToString();
                            c.BorderWidth = 0;
                            r.Cells.Add(c);
                            break;
                    }
                }
                i++;
                inWebTable.Rows.Add(r);
            }
        }
    }





}
