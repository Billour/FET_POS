using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;
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

public partial class EntryPoint_SP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
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
            string sURI = HttpContext.Current.Request.Url.AbsoluteUri;
            string sTemp = sURI.Substring(sURI.IndexOf('?') + 1);

            string code = sTemp.Substring(6); //Request.QueryString["Param"];
            //code = "oBcVKn+ssvzRXxVRksow467bAt9XEhruTo8h4Fpse9O+a78YaAQuNz8wutpizGI3UE1h9odABVUlHamvYU0Sc4BTeAzxwT1UgMuZKiI4OVoefXN1ZdkjM5HRvSepveF75sPHfHRksQc781t+zb2vlw==";
            //code = "oBcVKn ssvzRXxVRksow467bAt9XEhruTo8h4Fpse9O a78YaAQuNz8wutpizGI3UE1h9odABVUlHamvYU0Sc4BTeAzxwT1UgMuZKiI4OVoefXN1ZdkjM5HRvSepveF75sPHfHRksQc781t zb2vlw==";
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
                          'EntryPoint_SP', 'OnLoad', SYSDATE, " + OracleDBUtil.SqlStr(code) + ")");


                    Hashtable queryTable = new Hashtable();
                    string query = cCrypt.Decrypt(code, myKey);
                    NameValueCollection qscoll = HttpUtility.ParseQueryString(query);

                    foreach (string key in qscoll.AllKeys)
                    {
                        string value = string.Join(",", qscoll.GetValues(key));
                        //判斷是否有machine_id
                        if (key.ToLower() == "machine_id" && string.IsNullOrEmpty(value))
                        {
                            try
                            {
                                value = System.Configuration.ConfigurationManager.AppSettings["DefaultMachineID"].ToString();
                            }
                            catch
                            {
                                value = "01";
                            }
                        }
                        else if (key.ToLower() == "storeid" && string.IsNullOrEmpty(value))
                        {
                            try
                            {
                                value = System.Configuration.ConfigurationManager.AppSettings["DefaultStore"].ToString();
                            }
                            catch
                            {
                                value = "HQ";
                            }
                        }
                        else if (key.ToLower() == "role" && string.IsNullOrEmpty(value))
                        {
                            try
                            {
                                value = System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"].ToString();
                            }
                            catch
                            {
                                value = "HQ";
                            }
                        }

                        queryTable.Add(key.ToLower(), value);
                        //query_url += string.Format("&{0}={1}", key, value);
                        //small_change_url += string.Format("&{0}={1}", key, value);
                    }

                    if (queryTable.ContainsKey("role"))
                    {
                        queryTable["role"] = get_role(queryTable["employeeid"].ToString());
                    }
                    else
                    {
                        string role = get_role(queryTable["employeeid"].ToString());
                        queryTable.Add("role", role);
                    }

                    foreach (DictionaryEntry de in queryTable)
                    {
                        query_url += string.Format("&{0}={1}", de.Key.ToString(), de.Value.ToString());
                        small_change_url += string.Format("&{0}={1}", de.Key.ToString(), de.Value.ToString());
                    }

                    //先將傳入參數寫入
                    OracleDBUtil.ExecuteSql(
                        conn_new_pos,
                        @"INSERT INTO SYS_PROCESS_LOG(
                          FUNC_GROUP, ACTION_TYPE, CREATE_DTM, PARAMETER
                          )VALUES(
                          'EntryPoint_SP', 'OnLoad', SYSDATE, " + OracleDBUtil.SqlStr("加密:" + code + ",解密:" + query + ",實際連結:" + query_url) + ")");

                    //判斷功能
                    string url = "VSS/{0}/{1}/{2}.aspx";

                    string action_id = queryTable["action_id"].ToString();
                    string fun_id = queryTable["fun_id"].ToString();

                    #region query_url
                    if (action_id.Equals("DF", StringComparison.InvariantCultureIgnoreCase))
                    {
                        #region 判斷頁面
                        IsSale = fun_id.Contains("SAL") && queryTable["storeid"].ToString() != "HQ";
                        IsBigMoney = fun_id.Contains("SAL01") || fun_id.Contains("SAL031") || fun_id.Contains("SAL05") || fun_id.Contains("SAL041") || fun_id.Contains("SAL02") || fun_id.Contains("TSAL01") || fun_id.Contains("TSAL05");
                        if (fun_id.Substring(0, 1) == "T")//測試功能
                        {
                            url = string.Format(url, fun_id.Substring(1, 3), fun_id.Substring(0, 6), fun_id);
                        }
                        else if (fun_id.Length == 6)
                        {
                            if (fun_id.Substring(0, 3) == "RPL")//報表
                            {
                                url = string.Format("VSS/RPT/{0}.aspx", fun_id);
                            }
                            else //特殊處理 SAL041,SAL031 ,OSAL02,OSAL04
                            {
                                if (fun_id == "SAL041")
                                {
                                    url = string.Format(url, fun_id.Substring(0, 3), fun_id.Substring(0, 6), fun_id);
                                }
                                else if (fun_id == "SAL031" || fun_id == "OPT05a")
                                {
                                    url = string.Format(url, fun_id.Substring(0, 3), fun_id.Substring(0, 5), fun_id);
                                }
                                else if (fun_id == "OSAL02" || fun_id == "OSAL04")
                                {
                                    url = string.Format(url, fun_id.Substring(1, 3), fun_id, fun_id);
                                }
                                else
                                {
                                    url = string.Format(url, fun_id.Substring(0, 3), fun_id.Substring(0, 6), fun_id);
                                }
                            }
                        }
                        else if (fun_id.Length == 7)
                        {
                            if (fun_id == "INV18_1")
                            {
                                url = "VSS/INV/INV18_1/INV18_1.aspx";
                            }
                            else
                            {
                                url = string.Format(url, fun_id.Substring(0, 3), fun_id.Substring(0, 5), fun_id);
                            }
                         
                        }
                        else
                        {
                            if (fun_id == "SAL06")//特殊處理 SAL06 ORD03
                            {
                                url = "VSS/SAL/SAL06.aspx";
                            }
                            else if (fun_id == "ORD03")
                            {
                                url = "VSS/ORD/ORD03.aspx";
                            }
                            else if (fun_id == "SAL01")
                            {
                                url = "VSS/SAL/TSAL01/TSAL01.aspx";
                            }
                            else if (fun_id == "SAL05")
                            {
                                url = "VSS/SAL/TSAL05/TSAL05.aspx";
                            }
                            else if (fun_id == "SAL11") //交易補登
                            {
                                url = "VSS/SAL/TSAL11/TSAL11.aspx";
                            }
                            else if (fun_id == "SAL14") //紙本授權
                            {
                                url = "VSS/SAL/TSAL14/TSAL14.aspx";
                            }
                            else
                            {
                                url = string.Format(url, fun_id.Substring(0, 3), fun_id.Substring(0, 5), fun_id);
                            }
                        }

                        //query_url = url + "?" + query_url;

                        //**2011/04/21 Tina：傳遞參數時，要先以加密處理。
                        string encryptUrl = Utils.Param_Encrypt(query_url);
                        query_url = url + "?ParamBasePage=" + query_url;

                        #endregion
                    }
                    else if (action_id.Equals("UC", StringComparison.InvariantCultureIgnoreCase))
                    {
                        IsRedirect = true;
                        //query_url = "~/VSS/SAL/SAL05/SAL05.aspx?POSUUID_DETAIL=" + queryTable["POSD"].ToString();

                        //**2011/04/21 Tina：傳遞參數時，要先以加密處理。
                        string encryptUrl = Utils.Param_Encrypt("POSUUID_DETAIL=" + queryTable["POSD"].ToString());
                        query_url = "~/VSS/SAL/SAL05/SAL05.aspx?Param=" + encryptUrl;
                    }
                    else if (action_id.Equals("SC", StringComparison.InvariantCultureIgnoreCase))
                    {
                        IsRedirect = true;
                        //query_url = "~/VSS/SAL/SAL01/TSAL01.aspx?SRC_TYPE=TSAL05&PKEY=" + queryTable["POSD"].ToString();

                        //**2011/04/21 Tina：傳遞參數時，要先以加密處理。
                        string encryptUrl = Utils.Param_Encrypt("SRC_TYPE=TSAL05&PKEY=" + queryTable["POSD"].ToString());
                        query_url = "~/VSS/SAL/SAL01/TSAL01.aspx?Param=" + encryptUrl;
                    }
                    else if (action_id.Equals("CR", StringComparison.InvariantCultureIgnoreCase))
                    {
                        IsRedirect = true;
                        //query_url = "~/VSS/SAL/SAL03/SAL031.aspx?SRC_TYPE=CR&PKEY=" + queryTable["POSD"].ToString();

                        //**2011/04/21 Tina：傳遞參數時，要先以加密處理。
                        string encryptUrl = Utils.Param_Encrypt("SRC_TYPE=CR&PKEY=" + queryTable["POSD"].ToString());
                        query_url = "~/VSS/SAL/SAL03/SAL031.aspx?Param=" + encryptUrl;
                    }
                    else if (action_id.Equals("CT", StringComparison.InvariantCultureIgnoreCase))
                    {

                        IsRedirect = true;
                        //query_url = "~/VSS/SAL/SAL041/SAL041.aspx?SRC_TYPE=CR&PKEY=" + queryTable["POSD"].ToString();

                        //**2011/04/21 Tina：傳遞參數時，要先以加密處理。
                        string encryptUrl = Utils.Param_Encrypt("SRC_TYPE=CR&PKEY=" + queryTable["POSD"].ToString());
                        query_url = "~/VSS/SAL/SAL041/SAL041.aspx?Param=" + encryptUrl;
                    }
                    else if (action_id.Equals("FT", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Response.Write("處理完成");
                    }
                    #endregion

                    #region 處理
                    if (IsSale)
                    {
                        //判斷是否有輸入找零金
                        string sqlstr = string.Format("select ID from  SMALL_CHANGE   where trunc(TRADE_DATE)=trunc(WorkingDay('{0}')) and STORE_NO='{0}' and MACHINE_ID = '{1}'", queryTable["storeid"].ToString(), queryTable["machine_id"].ToString());
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
                                sqlstr = string.Format("select FN_GET_MACHINE_PRICE('{0}','{1}') from dual", queryTable["storeid"].ToString(), queryTable["machine_id"].ToString());
                                cmd = new OracleCommand(sqlstr, conn_new_pos);
                                string machine_price = cmd.ExecuteScalar() == null ? "0" : cmd.ExecuteScalar().ToString();

                                //判斷金額
                                cmd = new OracleCommand("SP_GET_MACHINE_PRICE", conn_new_pos);

                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("inSTORE_NO", OracleType.NVarChar).Value = queryTable["storeid"].ToString();
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
                    #endregion

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



    private string get_role(string userid)
    {
        
        string a = "1";//店長
       
        string b = "2";//店員
        string HQ = "HQ";//總部人員
        string result = HQ;
        OracleConnection conn = null;
        try
        {
            conn = OracleDBUtil.GetConnection();
            string sqlstr = "select POSITIONID from SALESORG_INTERNAL_ID where userid = " + OracleDBUtil.SqlStr(userid);
            OracleCommand cmd = new OracleCommand(sqlstr, conn);
            string positionid = cmd.ExecuteScalar() == null ? "" : cmd.ExecuteScalar().ToString();
            switch (positionid)
            {
                case "8":
                case "10":
                case "11":
                    result = a;
                    break;
                case "9":
                case "10061":
                    result = b;
                    break;
                default:
                    result = HQ;
                    break;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            OracleConnection.ClearPool(conn);
        }
        return result;
    }


}
