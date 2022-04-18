using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;


/// <summary>
/// ACell 的摘要描述
/// </summary>
public class ACell
{
    private PdfPCell _cell;

	public ACell():base()
	{
		  _cell = new PdfPCell();            
	}
    public ACell(Paragraph p)
        : base()
    {
        _cell = new PdfPCell(p);
    }
    public PdfPCell get() 
    {
        return _cell;
    }
    public ACell set(string sStyle, object oValue) 
    {
        switch (sStyle)
        {
            case "H":
                _cell.HorizontalAlignment = (int)oValue;
                break;
            case "V":
                _cell.VerticalAlignment = (int)oValue;
                break;
            case "B":
                _cell.Border = (int)oValue;
                break;
            case "CS":
                _cell.Colspan = (int)oValue;
                break;
            case "HT":
                _cell.FixedHeight = (int)oValue;
                break;
            default:
                break;
        }
        return this;
    }
}
