using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// XLSDefaultExportor 的摘要描述
/// </summary>
public class XLSExportor: AbstractXLS
{
    public XLSExportor()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

    public override bool accept(string model)
    {
        return (model.ToUpper() == "XLS".ToUpper());
    }
}
