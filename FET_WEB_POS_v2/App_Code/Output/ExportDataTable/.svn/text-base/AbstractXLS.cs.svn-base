using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// AbstractXLSExportor 的摘要描述
/// </summary>
public class AbstractXLS : BasePage, Printer
{
    protected Table inWebTable;    //組成完成的樣式
    protected DataTable dtData;    //前端傳來的資料表

    public AbstractXLS()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

    #region Printer 成員

    public virtual bool accept(string model)
    {
        return true;
    }

    public virtual void prepareData(DataTable dt)
    {
        this.dtData = dt;
        inWebTable = new Table();
        inWebTable.Rows.Clear();
    }

    public virtual void exportTitle(string TitleName)
    {
        if (!string.IsNullOrEmpty(TitleName))
        {
            TableRow TitleRow = new TableRow();
            inWebTable.Rows.Add(TitleRow);
            TitleRow.BackColor = System.Drawing.Color.Silver;
            TableCell c = new TableCell();
            c.Text = TitleName;
            TitleRow.Cells.Add(c);
        }
    }

    public virtual void exportHeader(DataTable dtHeader)
    {
        // 表頭資訊
        if (dtHeader != null)
        {
            TableRow HeadRow = new TableRow();
            inWebTable.Rows.Add(HeadRow);

            foreach (DataRow drHeader in dtHeader.Rows)
            {
                for (int i = 0; i <= dtHeader.Columns.Count - 1; i++)
                {
                    TableCell c = new TableCell();
                    c.Text = drHeader[i].ToString();
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

    public virtual void exportDataHeader()
    {
        TableRow theHeadRow = new TableRow();
        inWebTable.Rows.Add(theHeadRow);
        theHeadRow.BackColor = System.Drawing.Color.Silver;
        foreach (DataColumn dc in dtData.Columns)
        {
            TableCell c = new TableCell();
            c.Text = dc.ColumnName;
            theHeadRow.Cells.Add(c);
        }
    }

    public virtual void exportData()
    {
        int i = 0;

        foreach (DataRow dr in dtData.Rows)
        {

            TableRow r = new TableRow();
            foreach (DataColumn dc in dtData.Columns)
            {
                TableCell c = new TableCell();
                c.Text = dr[dc].ToString();
                r.Cells.Add(c);
            }
            i++;
            inWebTable.Rows.Add(r);
        }
    }

    public virtual void exportFooter(DataTable dtFooter)
    {
        // 表尾資訊
        if (dtFooter != null)
        {
            TableRow FootRow = new TableRow();
            inWebTable.Rows.Add(FootRow);

            foreach (DataRow drFooter in dtFooter.Rows)
            {
                for (int i = 0; i <= dtFooter.Columns.Count - 1; i++)
                {
                    TableCell c = new TableCell();
                    c.Text = drFooter[i].ToString();
                    FootRow.Cells.Add(c);
                }
            }
        }
    }

    public virtual string output()
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        inWebTable.RenderControl(htmlWrite);

        Session["XLstringWrite"] = stringWrite;

       return "";
    }

    #endregion
}
