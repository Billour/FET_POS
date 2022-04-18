using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Text;
using Advtek.Utility;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下一行。
// [System.Web.Script.Services.ScriptService]
public class ServiceGetNO : System.Web.Services.WebService
{
    public ServiceGetNO()
    {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    /// <summary>
    /// 取得銷售單號
    /// </summary>
    /// <param name="PA_Sheet_Type">"SALE"</param>
    /// <param name="STORE_NO">門市編號</param>
    /// <param name="MACHINE_ID">機台編號</param>
    /// <returns>銷售單號</returns>
    [WebMethod]
    public string GetSALE_No(string PA_Sheet_Type, string STORE_NO, string MACHINE_ID)
    {
        OracleConnection conn = null;
        string strResult = "";
        OracleCommand oraCmd = null;
        OracleTransaction objTX = null;
        try
        {
            conn = OracleDBUtil.GetConnection();
            objTX = conn.BeginTransaction(IsolationLevel.Serializable);
            oraCmd = new OracleCommand("PK_GET_GEN_NO.SP_GET_STORE_GEN_NO");
            oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
            oraCmd.Parameters.Add(new OracleParameter("PA_Sheet_Type", OracleType.VarChar, 2000)).Value = PA_Sheet_Type;
            oraCmd.Parameters.Add(new OracleParameter("I_STORE_NO", OracleType.VarChar, 2000)).Value = STORE_NO;
            oraCmd.Parameters.Add(new OracleParameter("I_MACHINE_ID", OracleType.VarChar, 2000)).Value = MACHINE_ID;
            oraCmd.Parameters.Add(new OracleParameter("strResult", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
            oraCmd.Connection = objTX.Connection;
            oraCmd.Transaction = objTX;
            oraCmd.ExecuteNonQuery();
            strResult = oraCmd.Parameters["strResult"].Value.ToString();
            objTX.Commit();
            return strResult;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            oraCmd.Dispose();
            if (conn.State == ConnectionState.Open) conn.Close();
            conn.Dispose();
            OracleConnection.ClearAllPools();
        }

    }

    /// <summary>
    /// 取得發票號碼
    /// </summary>
    /// <param name="STORE_NO">門市編號</param>
    /// <returns>發票號碼</returns>
    [WebMethod]
    public string GetINVOICE_NO(string STORE_NO)
    {
        string strResult = "";
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(@" SELECT   invoice_no, ASSIGN_ID            
                             FROM invoice_no_pool
                             WHERE store_no = '" + STORE_NO + @"'
                             AND status = '0'
                             AND s_use_ym <= TO_CHAR(SYSDATE, 'YYYY/MM')
                             AND e_use_ym >= TO_CHAR(SYSDATE, 'YYYY/MM')
                             AND USE_TYPE='1'
                             AND ROWNUM = 1
                             ORDER BY  BOOK_INV_SEQNO ASC, invoice_no ASC
                          ");

        DataTable dt = OracleDBUtil.Query_Data(sb.ToString());


        if (dt.Rows.Count > 0)
        {
            strResult = dt.Rows[0]["invoice_no"].ToString();
        }

        return strResult;

    }

    /// <summary>
    /// 取得收據號碼
    /// </summary>
    /// <param name="PA_Sheet_Type">"RECITT"</param>
    /// <returns>收據編號</returns>
    [WebMethod]
    public string GetRECITT_NO(string PA_Sheet_Type)
    {
        OracleConnection conn = null;
        string strResult = "";
        OracleCommand oraCmd = null;
        OracleTransaction objTX = null;
        try
        {
            conn = OracleDBUtil.GetConnection();
            objTX = conn.BeginTransaction(IsolationLevel.Serializable);
            oraCmd = new OracleCommand("PK_GET_GEN_NO.SP_GET_GEN_NO");
            oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
            oraCmd.Parameters.Add(new OracleParameter("PA_Sheet_Type", OracleType.VarChar, 2000)).Value = PA_Sheet_Type;
            oraCmd.Parameters.Add(new OracleParameter("strResult", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
            oraCmd.Connection = objTX.Connection;
            oraCmd.Transaction = objTX;
            oraCmd.ExecuteNonQuery();
            strResult = oraCmd.Parameters["strResult"].Value.ToString();
            objTX.Commit();
            return strResult;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            oraCmd.Dispose();
            if (conn.State == ConnectionState.Open) conn.Close();
            conn.Dispose();
            OracleConnection.ClearAllPools();
        }

    }

}
