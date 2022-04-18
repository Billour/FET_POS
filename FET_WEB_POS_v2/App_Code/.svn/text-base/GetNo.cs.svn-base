using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// GetNo 的摘要描述
/// </summary>
public class GetNo
{
    public GetNo()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

    /// <summary>
    /// 取得銷售單號
    /// </summary>
    /// <param name="PA_Sheet_Type">"SALE"</param>
    /// <param name="STORE_NO">門市編號</param>
    /// <param name="MACHINE_ID">機台編號</param>
    /// <returns>銷售單號</returns>
    public static string GetSALE_No(string PA_Sheet_Type, string STORE_NO, string MACHINE_ID)
    {
        WEB_SERVICE_PROXY.QueryNO ws = new WEB_SERVICE_PROXY.QueryNO();
        return ws.GetSALE_No(PA_Sheet_Type, STORE_NO, MACHINE_ID);
    }

    /// <summary>
    /// 取得發票號碼
    /// </summary>
    /// <param name="STORE_NO">門市編號</param>
    /// <returns>發票號碼</returns>
    public static string GetINVOICE_NO(string STORE_NO)
    {
        WEB_SERVICE_PROXY.QueryNO ws = new WEB_SERVICE_PROXY.QueryNO();
        return ws.GetINVOICE_NO(STORE_NO);
    }

    /// <summary>
    /// 取得收據號碼
    /// </summary>
    /// <param name="PA_Sheet_Type">"RECITT"</param>
    /// <returns>收據編號</returns>
    public static string GetRECITT_NO(string PA_Sheet_Type)
    {
        WEB_SERVICE_PROXY.QueryNO ws = new WEB_SERVICE_PROXY.QueryNO();
        return ws.GetRECITT_NO(PA_Sheet_Type);
    }
}
