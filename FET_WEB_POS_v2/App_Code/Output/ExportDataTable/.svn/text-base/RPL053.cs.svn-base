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
/// RPL053 的摘要描述
/// </summary>
public class RPL053 : AbstractXLS
{
    public RPL053()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

    public override bool accept(string model)
    {
        return (model == "RPL053");
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
            clSpace.BorderWidth = 1;
            switch (i)
            {
                case 3:
                    clSpace.Text = "銷售倉";
                    clSpace.ColumnSpan = 9;
                    clSpace.VerticalAlign =  VerticalAlign.Middle; //垂直置中
                    clSpace.HorizontalAlign = HorizontalAlign.Center;//水平置中
                    theHeadRow.Cells.Add(clSpace);
                    break;
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
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

        for (int i = 3; i <= 11; i++)
        {
            TableCell clSpace = new TableCell();

            //clSpace.RowSpan = 2;
            clSpace.Text = this.dtData.Columns[i].ToString();
            clSpace.BorderWidth = 1;
            theHeadRow1.Cells.Add(clSpace);

        }

    }
    public override void exportData()
    {
        int i = 0;

        foreach (DataRow dr in dtData.Rows)
        {

            TableRow r = new TableRow();
            foreach (DataColumn dc in dtData.Columns)
            {
                TableCell c = new TableCell();
                c.Text = dr[dc].ToString();
                c.BorderWidth = 1;
                r.Cells.Add(c);
            }
            i++;
            inWebTable.Rows.Add(r);
        }
    }





}
