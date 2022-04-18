using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// PDFDefaultExportor 的摘要描述
/// </summary>
public class PDFExportor: AbstractPDF
{
    public PDFExportor()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}

    public override bool accept(string model)
    {
        return (model.ToUpper() == "PDF".ToUpper());
    }
}
