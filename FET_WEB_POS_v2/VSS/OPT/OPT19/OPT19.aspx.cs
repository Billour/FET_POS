using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Data.OracleClient;
using Advtek.Utility;
public partial class VSS_OPT_OPT19_OPT19 : System.Web.UI.Page
{
    private string RtCode;
    private string RtMessage;
    protected void btnOk_Click(object sender, EventArgs e)
    {
        string SaleNo = string.Empty;
        string Sdate = string.Empty;
        string Edate = string.Empty;
        string Type = string.Empty;
        string Message = string.Empty;
        var command = ((ASPxButton)sender).CommandName;
        //清空分錄
        if (command == "Clear")
        {
            Type = "1";
            SaleNo = txtSaleNo.Text;
            Sdate = txtSdate.Text;
            Edate = txtEdate.Text;

        }//產生分錄
        else if (command == "Process")
        {
            Type = "2";
            SaleNo = txtSaleNo2.Text;
            Sdate = txtSdate2.Text;
            Edate = txtEdate2.Text;
        }//上傳分錄
        else
        {
            Type = "3";
            SaleNo = txtSaleNo3.Text;
            Sdate = txtSdate3.Text;
            Edate = txtEdate3.Text;
        }
        CallSP_DataAcess(SaleNo, Sdate, Edate, Type);
        if (Type == "1")
        {
            txtClearMessage.Value = RtMessage;
        }
        else if (Type == "2")
        {
            txtClearMessage2.Value = RtMessage;
        }
        else
        {
            txtClearMessage3.Value = RtMessage;
        }
        if (RtCode.CompareTo("000") == 0)
        {

           // ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", "alrt('執行成功');", true);

        }
        else
        {
           // ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", "alrt('執行失敗');", true);
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        var command = ((ASPxButton)sender).CommandName;
        if (command == "Clear")
        {
            txtClearMessage.Value = "";
            txtSaleNo.Text = "";
            txtSdate.Text = null;
            txtEdate.Text = null;

        }
        else if (command == "Process")
        {
            txtClearMessage2.Value = "";
            txtSaleNo2.Text = "";
            txtSdate2.Text = null;
            txtEdate2.Text = null;
        }
        else
        {
            txtClearMessage3.Value = "";
            txtSaleNo3.Text = "";
            txtSdate3.Text = null;
            txtEdate3.Text = null;
        }
    }

    /// <summary>
    /// CALL SP 
    /// </summary>
    /// <param name="SaleNo"></param>
    /// <param name="Sdate"></param>
    /// <param name="Edate"></param>
    /// <param name="Type"></param>
    private void CallSP_DataAcess(string SaleNo, string Sdate, string Edate, string Type)
    {

        OracleConnection objConn = null;
        OracleTransaction objTX = null;
        try
        {
            objConn = OracleDBUtil.GetConnection();
            objTX = objConn.BeginTransaction();

            OracleCommand oraCmd = new OracleCommand("PK_GL.SP_TEST");
            oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
            //input
            oraCmd.Parameters.Add(new OracleParameter("I_SALE_NO", OracleType.VarChar, 2000)).Value = SaleNo;
            oraCmd.Parameters.Add(new OracleParameter("I_SDATE", OracleType.VarChar, 2000)).Value = Sdate;
            oraCmd.Parameters.Add(new OracleParameter("I_EDATE", OracleType.VarChar, 2000)).Value = Edate;
            oraCmd.Parameters.Add(new OracleParameter("I_TYPE", OracleType.VarChar, 2000)).Value = Type;
            //output
            oraCmd.Parameters.Add(new OracleParameter("O_RT_CODE", OracleType.VarChar,2000)).Direction = ParameterDirection.Output;
            oraCmd.Parameters.Add(new OracleParameter("O_RT_MESSAGE", OracleType.VarChar,2000)).Direction = ParameterDirection.Output;

            oraCmd.Connection = objConn;
            oraCmd.Transaction = objTX;
            oraCmd.ExecuteNonQuery();
            RtMessage  = oraCmd.Parameters["O_RT_MESSAGE"].Value.ToString();
            RtCode = StringUtil.CStr(oraCmd.Parameters["O_RT_CODE"].Value);
            if (RtCode.CompareTo("000") == 0)
            {
                objTX.Commit();
            }
            else
            {
                objTX.Rollback();
            }
        

        }
        catch (Exception ex)
        {
            objTX.Rollback();
            throw ex;
        }
        finally
        {
            objTX.Dispose();
            if (objConn.State == ConnectionState.Open) objConn.Close();
            objConn.Dispose();
            OracleConnection.ClearAllPools();
        }
    }
}
