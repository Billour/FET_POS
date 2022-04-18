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
public class RPL046_SUM : AbstractXLS
{
    public RPL046_SUM()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

    public override bool accept(string model)
    {
        return (model == "RPL046_SUM");
    }

    public override void exportTitle(string TitleName)
    {
        if (!string.IsNullOrEmpty(TitleName))
        {
            TableRow TitleRow = new TableRow();
            inWebTable.Rows.Add(TitleRow);
            TableCell c = new TableCell();
            c.Text = TitleName;
            c.ColumnSpan = 8;
            c.BackColor = System.Drawing.Color.Silver;
            c.Font.Size = 20;
            TitleRow.Cells.Add(c);
        }
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
                    c.ColumnSpan = 4;
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
    private void exportDataHeader(string HeaderName)
    {
        TableRow theHeadRow;
        TableCell clSpace;

        theHeadRow = new TableRow();
        inWebTable.Rows.Add(theHeadRow);
        clSpace = new TableCell();
        clSpace.BorderWidth = (Unit)1;
        clSpace.VerticalAlign = VerticalAlign.Middle; //垂直置中
        clSpace.BackColor = System.Drawing.Color.Silver;
        clSpace.Text = HeaderName;
        clSpace.ColumnSpan = 8;
        clSpace.Font.Size = 12;
        clSpace.Height = 30;
        theHeadRow.Cells.Add(clSpace);

        theHeadRow = new TableRow();
        inWebTable.Rows.Add(theHeadRow);

        for (int i = 0; i < this.dtData.Columns.Count; i++)
        {
            clSpace = new TableCell();
            clSpace.BorderWidth = (Unit)1;
            clSpace.VerticalAlign = VerticalAlign.Middle; //垂直置中
            clSpace.HorizontalAlign = HorizontalAlign.Center;//水平置中
            clSpace.BackColor = System.Drawing.Color.Silver;

            switch (this.dtData.Columns[i].ToString())
            {
                case "TITLE_INDEX":
                case "MACHINE_ID":
                    break;
                case "TITLE":
                    clSpace.Text = "項目";
                    theHeadRow.Cells.Add(clSpace);
                    break;

                case "現金":
                    clSpace.RowSpan = 1;
                    clSpace.ColumnSpan = 7;
                    clSpace.Text = "支付方式";
                    theHeadRow.Cells.Add(clSpace);
                    break;
                default:
                    break;
            }

        }

        theHeadRow = new TableRow();
        inWebTable.Rows.Add(theHeadRow);

        for (int i = 0; i < this.dtData.Columns.Count; i++)
        {
            clSpace = new TableCell();
            clSpace.BorderWidth = (Unit)1;
            clSpace.VerticalAlign = VerticalAlign.Middle; //垂直置中
            clSpace.HorizontalAlign = HorizontalAlign.Center;//水平置中
            clSpace.BackColor = System.Drawing.Color.Silver;

            if (this.dtData.Columns[i].ToString() == "TITLE_INDEX") continue;
            if (this.dtData.Columns[i].ToString() == "MACHINE_ID") continue;

            clSpace.RowSpan = 1;
            if (this.dtData.Columns[i].ToString() == "TITLE")
            {
                clSpace.Text = "開立發票";
                clSpace.Width = 100;
            }
            else
            {
                clSpace.Text = this.dtData.Columns[i].ToString();
                clSpace.Width = 100;
            }
            theHeadRow.Cells.Add(clSpace);

        }
    }
    public override void exportData()
    {
        TableRow r;
        TableCell c;

        foreach (DataRow dr in dtData.Rows)
        {

            if (dr["TITLE_INDEX"].ToString() == "0101")
            {
                if (dr["MACHINE_ID"].ToString() == "-1") exportDataHeader("門市總計");
                else
                {
                    r = new TableRow();
                    inWebTable.Rows.Add(r);
                    exportDataHeader(dr["MACHINE_ID"].ToString() + " 機台小計");
                }

            }

            r = new TableRow();
            c = new TableCell();
            for (int j = 0; j < dtData.Columns.Count; j++)
            {
                c = new TableCell();
                switch (this.dtData.Columns[j].ToString())
                {
                    case "TITLE_INDEX":
                    case "MACHINE_ID":
                        break;
                    default:
                        c.Text = dr[j].ToString();
                        c.BorderWidth = 1;
                        r.Cells.Add(c);
                        break;
                }
            }
            inWebTable.Rows.Add(r);
        }
    }





}
