using System;
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

public partial class EntryPoint_SP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 目前未使用
        string actionID = Request.QueryString["ActionId"];

        if (actionID != null)
        {
            if (actionID.Equals("UC", StringComparison.InvariantCultureIgnoreCase))
            {
                Response.Redirect("~/VSS/SAL/SAL05.aspx");
            }
            else if (actionID.Equals("SC", StringComparison.InvariantCultureIgnoreCase))
            {
                Response.Redirect("~/VSS/SAL/SAL01.aspx");
            }
        }

        LogMessage QueryStingMsg = new LogMessage();
        QueryStingMsg.STORENO = Request.QueryString["StoreId"];         //門市代號
        QueryStingMsg.OPERATOR = QueryStingMsg.ROLE_TYPE = QueryStingMsg.CREATE_USER = QueryStingMsg.MODI_USER = Request.QueryString["EmployeeId"];     //操作者 (登入者帳號)
        QueryStingMsg.FUNCTION_NO = Request.QueryString["action_id"];    //前端欲連結的頁面 .....待確認 by Tina
        QueryStingMsg.MACHINE_ID = Request.QueryString["machine_id"];   //門市機台編號_UUID
        QueryStingMsg.SYS_ID = Request.QueryString["SysId"];            //系統別

        Session["QueryStingMsg"] = QueryStingMsg;
        #endregion

        //***20101130
        EnDecrypt cCrypt = new EnDecrypt();
        string myKey = "eP9mZ7Qs";

        string sURI = HttpContext.Current.Request.Url.AbsoluteUri; ;
        string sTemp = sURI.Substring(sURI.IndexOf('?') + 1);

        if (sTemp != null || sTemp != "")
        {
            NameValueCollection qscoll = HttpUtility.ParseQueryString(sTemp);

            string sQuery = sTemp;

            //PARAM=
            string arrTemp = sQuery.Substring(6);

            if (arrTemp.Length >= 0)
            {
                lbl03.Text = "加密字串:" + arrTemp;

                try
                {
                    sQuery = cCrypt.Decrypt(arrTemp, myKey);
                }
                catch
                {
                    lbl01.Text = "參數傳入錯誤!!無法解密字串!!";
                    return;
                }

                lbl04.Text = "解密字串:" + sQuery;

                string[] arrTemp2 = sQuery.Split('&');

                string sUUID = "";

                foreach (string POSD in arrTemp2)
                {
                    if (POSD.IndexOf("POSD=") >= 0)
                    {
                        string[] arrTemp3 = POSD.Split('=');
                        sUUID = arrTemp3[1];
                        break;
                    }
                }

                lbl05.Text = "DETAIL_UUID:" + sUUID;
                using (OracleConnection oCon = OracleDBUtil.GetConnection())
                {
                    try
                    {
                        string sSQL = @"SELECT t1.store_name AS store_name, t1.total_amount AS total_amount,
                                       t2.prodno AS prodno, t2.amount AS amount,
                                       t3.discount_id AS discount_id, t3.discount_price AS discount_price
                                  FROM to_close_head t1 , to_close_item t2, to_close_discount t3 
                                 WHERE t1.posuuid_detail = t2.posuuid_detail
                                    and t1.posuuid_detail = t3.posuuid_detail(+)
                                 and   t1.posuuid_detail = '" + sUUID + "'";

                        using (OracleCommand cmd = new OracleCommand(sSQL, oCon))
                        {
                            using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    gv01.DataSource = dt;
                                    gv01.DataBind();
                                }
                            }
                        }
                    }
                    catch { }
                    finally { oCon.Dispose(); }
                }
            }
            else
            {
                lbl01.Text = "參數傳入錯誤!!";
            }
        }
        else
        {
            lbl01.Text = "參數傳入錯誤!!";
        }
    }
}
