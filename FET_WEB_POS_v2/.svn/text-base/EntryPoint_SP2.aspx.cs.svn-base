using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Advtek.Utility;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Specialized;
using System.Data;
using System.Data.OracleClient;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using log4net.Config;
using log4net;

public partial class EntryPoint_SP2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string myKey = "eP9mZ7Qs";
        EnDecrypt cCrypt = new EnDecrypt();
        if (string.IsNullOrEmpty(Request["Param"]))
        {
            string encryptUrl = Request.Url.Query.Substring(1);
            encryptUrl = cCrypt.Encrypt(encryptUrl, myKey);
            Response.Write(Server.UrlEncode(encryptUrl));
        }
        else
        {
            string code = Request["Param"];
            if (!string.IsNullOrEmpty(code))
            {
                  OracleConnection conn_new_pos = null;
                  bool IsSale = false; //判斷是否為銷售相關
                  bool IsBigMoney = false;//判斷是否檢核繳大鈔
                  bool IsRedirect = false; //是否轉業
                  bool IsSmaill_Change = false; //是否需要找零金
                  string query_url = "", small_change_url = "VSS/CHK/CHK04/CHK04.aspx?";
                try
                {
                        conn_new_pos = OracleDBUtil.GetConnection();

                        //先將傳入參數寫入
                        OracleDBUtil.ExecuteSql(
                            conn_new_pos,
                            @"INSERT INTO SYS_PROCESS_LOG(
                              FUNC_GROUP, ACTION_TYPE, CREATE_DTM, PARAMETER
                              )VALUES(
                              'EntryPoint_SP', 'OnLoad', SYSDATE, " + OracleDBUtil.SqlStr(Request.QueryString.ToString()) + ")");


                   Hashtable queryTable = new Hashtable();
                    string query = cCrypt.Decrypt(code, myKey);
                    NameValueCollection qscoll = HttpUtility.ParseQueryString(query);
                   
                    foreach (string key in qscoll.AllKeys)
                    {
                        if (key == "url")
                        {
                            query_url = string.Join(",", qscoll.GetValues(key)) + "?";
                            IsSale = query_url.Contains("SAL");
                            IsBigMoney = query_url.Contains("SAL01") || query_url.Contains("SAL031") || query_url.Contains("SAL05") || query_url.Contains("SAL041") || query_url.Contains("SAL02") || query_url.Contains("TSAL01") || query_url.Contains("TSAL05"); 
                        }
                        else
                        {
                            queryTable.Add(key,string.Join(",", qscoll.GetValues(key)));
                            query_url += string.Format("&{0}={1}", key, string.Join(",", qscoll.GetValues(key)));
                            small_change_url += string.Format("&{0}={1}", key, string.Join(",", qscoll.GetValues(key)));
                        }
                    }

                    #region 2011/04/21 Tina：傳遞參數時，要先以加密處理。

                    string[] strURL_Params = query_url.Split('?');
                    string strParams = "";
                    if (strURL_Params.Length >= 2)
                    {
                        strParams = strURL_Params[1];
                        string encryptUrl = Utils.Param_Encrypt(strParams);
                        query_url = strURL_Params[0] + "?ParamBasePage=" + encryptUrl;

                    }

                    strURL_Params = small_change_url.Split('?');
                    string strParams_small = "";
                    if (strURL_Params.Length >= 2)
                    {
                        strParams_small = strURL_Params[1];
                        string encryptUrl = Utils.Param_Encrypt(strParams_small);
                        small_change_url = strURL_Params[0] + "?ParamBasePage=" + encryptUrl;
                    }

                    #endregion

                    //先將傳入參數寫入
                    OracleDBUtil.ExecuteSql(
                        conn_new_pos,
                        @"INSERT INTO SYS_PROCESS_LOG(
                          FUNC_GROUP, ACTION_TYPE, CREATE_DTM, PARAMETER
                          )VALUES(
                          'EntryPoint_SP', 'OnLoad', SYSDATE, " + OracleDBUtil.SqlStr(query) + ")");

                    //處理
                    if (IsSale)
                    {
                        //判斷是否有輸入找零金
                        string sqlstr = string.Format("select ID from  SMALL_CHANGE   where trunc(TRADE_DATE)=trunc(WorkingDay('{0}')) and STORE_NO='{0}' and MACHINE_ID = '{1}'", queryTable["storeId"].ToString(), queryTable["machine_id"].ToString());
                        OracleCommand cmd = new OracleCommand(sqlstr, conn_new_pos);
                       
                         OracleDataReader dr = cmd.ExecuteReader();
                         IsSmaill_Change = dr.HasRows;
                         dr.Close();
                         if (!IsSmaill_Change)
                         {
                             Response.Write(string.Format("<script>alert('機台{1}無找零金輸入，請輸入找零金');location.href='{0}';</script>", Server.HtmlDecode(small_change_url), queryTable["machine_id"].ToString()));
                         }
                         else
                         {
                             if (IsBigMoney)
                             {
                                 //讀取系統設定金額
                                 sqlstr = string.Format("select FN_GET_MACHINE_PRICE('{0}','{1}') from dual", queryTable["storeId"].ToString(), queryTable["machine_id"].ToString());
                                 cmd = new OracleCommand(sqlstr, conn_new_pos);
                                 string machine_price = cmd.ExecuteScalar() == null ? "0" : cmd.ExecuteScalar().ToString();

                                 //判斷金額
                                 cmd = new OracleCommand("SP_GET_MACHINE_PRICE", conn_new_pos);

                                 cmd.CommandType = CommandType.StoredProcedure;
                                 cmd.Parameters.Add("inSTORE_NO", OracleType.NVarChar).Value = queryTable["storeId"].ToString();
                                 cmd.Parameters.Add("inMACHINE_ID", OracleType.NVarChar).Value = queryTable["machine_id"].ToString();
                                 cmd.Parameters.Add("RESULT", OracleType.Number).Direction = ParameterDirection.Output;
                                 cmd.ExecuteNonQuery();
                                 if ((decimal)cmd.Parameters["RESULT"].Value == 0)
                                 {
                                     Response.Write(string.Format("<script>alert('收款金額超過{1}，請繳大鈔');location.href='{0}';</script>", Server.HtmlDecode(query_url), machine_price));
                                 }
                                 else
                                 {
                                     IsRedirect = true;

                                 }
                             }
                             else
                             {
                                 IsRedirect = true;

                             }
                         }
                    }
                    else
                    {
                        IsRedirect = true;
                    }
                }
                catch (Exception ex)
                {
                    if (IsSale)
                    {
                        IsRedirect = true;
                    }
                    else
                    {
                        Response.Write(ex.Message);
                    }
                }
                finally
                {
                    if (conn_new_pos.State == ConnectionState.Open) conn_new_pos.Close();
                    conn_new_pos.Dispose();
                    OracleConnection.ClearAllPools();
                }
                if (IsRedirect)
                {
                    Response.Redirect(query_url);
                }
            }
        }
    }
}
