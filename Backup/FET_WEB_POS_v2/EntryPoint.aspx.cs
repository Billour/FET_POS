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
using log4net.Config;
using log4net;

public partial class EntryPoint : System.Web.UI.Page
{
    private OracleConnection conn_new_pos = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        XmlConfigurator.Configure(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\web.config"));
        lblString.Text = Request.QueryString.ToString();

        //將傳入的參數寫入至SYS_PROCESS_LOG
        string sql = @"INSERT INTO SYS_PROCESS_LOG(FUNC_GROUP, ACTION_TYPE, CREATE_DTM, PARAMETER)
                            VALUES('EntryPoint', 'OnLoad', SYSDATE, " + OracleDBUtil.SqlStr(Request.QueryString.ToString()) + ")";
        SaveInfoToLog(sql);

        EnDecrypt cCrypt = new EnDecrypt();
        string myKey = "eP9mZ7Qs";

        string sURI = HttpContext.Current.Request.Url.AbsoluteUri;
        string sTemp = Server.UrlDecode(sURI.Substring(sURI.IndexOf('?') + 1));

        if (sTemp != null || sTemp != "")
        {
            NameValueCollection qscoll = HttpUtility.ParseQueryString(sTemp);

            string sQuery = sTemp;

            //PARAM=
            string arrTemp = sQuery.Substring(6);

            if (arrTemp.Length >= 0)
            {
                try
                {
                    sQuery = cCrypt.Decrypt(arrTemp, myKey);
                }
                catch //(Exception ex)
                {
                    lblmessage.Text = "加密字串錯誤";
                    return;

                }

                string[] arrTemp2 = sQuery.Split('&');
                string sUUID = "", StoreId = "", EmployeeId = "", SysId = "", RoleId = "", TimeStamp = "", MACHINE_ID = "", ActionId = "", HostIP = "", fun_id = "";

                //取得參數值
                GetParamValue(arrTemp2, ref sUUID, ref StoreId, ref EmployeeId, ref SysId, ref RoleId, ref TimeStamp, ref MACHINE_ID, ref ActionId, ref HostIP, ref fun_id);


                //將傳入的參數寫入至SYS_PROCESS_LOG
                sql = @"INSERT INTO SYS_PROCESS_LOG(FUNC_GROUP, ACTION_TYPE, CREATE_DTM, PARAMETER, HOST_IP)
                               VALUES('EntryPoint', 'OnLoad', SYSDATE, ' 加密:" + lblString.Text + ",解密:" + sQuery + " ','" + Request.UserHostAddress + "')";
                SaveInfoToLog(sql);


                MACHINE_ID = get_mach_no();

                RedirectURL(ActionId, sUUID, MACHINE_ID, StoreId, EmployeeId, SysId, RoleId);  //網頁自動轉址

            }
        }

    }

    private bool CheckOtherDetail(string posuuid_detail, string posuuid_master)
    {
        bool result = false;
        OracleConnection conn = null;
        OracleCommand cmd = null;
        OracleDataReader dr = null;
        string sqlstr = "";

        //先抓出msisdn
        try
        {
            conn = OracleDBUtil.GetConnection();
            //利用posuuid_master判斷是否有其他的detail 
            if (!string.IsNullOrEmpty(posuuid_master) && !string.IsNullOrEmpty(posuuid_detail))
            {
                sqlstr = string.Format("select * from SALE_DETAIL where posuuid_master = '{0}' and posuuid_detail != '{1}' and item_type in ('1','2','7','13','14')", posuuid_master, posuuid_detail);
                cmd = new OracleCommand(sqlstr, conn);
                dr = cmd.ExecuteReader();
                bool hasRow = dr.HasRows;
                dr.Close();
                //如果有資料就是要換貨
                if (hasRow)
                {
                    result = true;
                }
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

    private void get_possuid_master(string sUUID, out string posuuid_master)
    {
        OracleConnection conn = null;
        posuuid_master = "";
        try
        {
            conn = OracleDBUtil.GetConnection();
            string sqlstr = string.Format("select posuuid_master from to_close_head where posuuid_detail = {0}", OracleDBUtil.SqlStr(sUUID));
            OracleCommand cmd = new OracleCommand(sqlstr, conn);
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                posuuid_master = dr[0].ToString();
            }
            dr.Close();
            OracleDBUtil.ExecuteSql(
                       conn,
                       @"INSERT INTO SYS_PROCESS_LOG(
                 		 FUNC_GROUP, ACTION_TYPE, CREATE_DTM, PARAMETER
                			  )VALUES(
                  'EntryPoint', 'OnLoad', SYSDATE, " + OracleDBUtil.SqlStr("SQL:" + sqlstr + ";POSUUID_MASTER=" + posuuid_master) + ")");
            //posuuid_master = cmd.ExecuteScalar() == null ? "" : cmd.ExecuteScalar().ToString();

            //判斷SALE_HEAD的status
            bool hasRow = false;
            string sale_status = "";
            string original_id = "";
            sqlstr = "select sale_status,original_id from sale_head where posuuid_master = " + OracleDBUtil.SqlStr(posuuid_master);
            cmd = new OracleCommand(sqlstr, conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                sale_status = StringUtil.CStr(dr[0]);
                original_id = StringUtil.CStr(dr[1]);
                hasRow = true;
            }

            dr.Close();

            if (hasRow)
            {
                //如果不是已結帳
                if (sale_status != "2")
                {
                    sqlstr = "select POSUUID_MASTER from SALE_HEAD where SALE_STATUS = '2' and  ORIGINAL_ID = " + OracleDBUtil.SqlStr(original_id);
                    cmd = new OracleCommand(sqlstr, conn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        posuuid_master = dr.GetString(0);
                    }
                    else
                    {
                        throw new Exception("POSUUID_MASTER查無資料");
                    }
                    dr.Close();
                }
            }
        }
        catch (Exception ex)
        {
            string sqlstrs = string.Format(
                  @"INSERT INTO SYS_PROCESS_LOG(
                 		 FUNC_GROUP, ACTION_TYPE, CREATE_DTM, PARAMETER
                			  )VALUES(
                  'EntryPoint', 'Error', SYSDATE, 'getPosuuidMaster PosuuidDetail  : {1},Error:{0}')", ex.Message, sUUID);
            OracleDBUtil.ExecuteSql(
                       conn, sqlstrs);
            lblmessage.Text = ex.Message;
            return;
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            OracleConnection.ClearPool(conn);
        }

    }

    private void getPosuuidMaster(ref string posuuid_master)
    {
        string sale_status = "";
        OracleConnection conn = null;
        string sqlstr = string.Format("select posuuid_master,sale_status from sale_head where posuuid_master = (select posuuid_master from sale_head where invalid_id = {0})", OracleDBUtil.SqlStr(posuuid_master));
        try
        {
            conn = OracleDBUtil.GetConnection();
            OracleCommand cmd = new OracleCommand(sqlstr, conn);
            OracleDataReader dr = cmd.ExecuteReader();
            bool hasrow = false;
            if (dr.Read())
            {
                posuuid_master = dr[0].ToString();
                sale_status = dr[1].ToString();
                hasrow = true;
            }
            dr.Close();
            if (hasrow)
            {
                if (sale_status != "2")
                {
                    getPosuuidMaster(ref posuuid_master);
                }
            }
            else
            {

                throw new Exception("POSUUID_MASTER查無資料");
            }
        }
        catch (Exception ex)
        {
            string sqlstrs = string.Format(
                       @"INSERT INTO SYS_PROCESS_LOG(
                 		 FUNC_GROUP, ACTION_TYPE, CREATE_DTM, PARAMETER
                			  )VALUES(
                  'EntryPoint', 'Error', SYSDATE, 'getPosuuidMaster PosuuidMaster : {1},Error:{0}')", ex.Message, posuuid_master);
            OracleDBUtil.ExecuteSql(
                       conn, sqlstrs);
            throw ex;
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            OracleConnection.ClearPool(conn);
        }
    }

    private string get_mach_no()
    {
        string ip = Request.UserHostAddress;
        string result = "99";
        string sqlstr = string.Format("select HOST_NO from STORE_TERMINATING_MACHINE where IP_ADDRESS = '{0}'", ip);
        OracleConnection conn = null;
        try
        {
            conn = OracleDBUtil.GetConnection();
            OracleCommand cmd = new OracleCommand(sqlstr, conn);
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                result = dr.GetString(0);
            }
            dr.Close();
            string sqlstrs = string.Format(
                  @"INSERT INTO SYS_PROCESS_LOG(
                 		 FUNC_GROUP, ACTION_TYPE, CREATE_DTM, PARAMETER
                			  )VALUES(
                  'EntryPoint', 'Onload', SYSDATE, 'get_mach_no : HOST_NO:{0},IP:{1}')", result, ip);
            OracleDBUtil.ExecuteSql(
                       conn, sqlstrs);
        }
        catch (Exception ex)
        {
            string sqlstrs = string.Format(
                     @"INSERT INTO SYS_PROCESS_LOG(
                 		 FUNC_GROUP, ACTION_TYPE, CREATE_DTM, PARAMETER
                			  )VALUES(
                  'EntryPoint', 'Error', SYSDATE, 'get_mach_no : IP:{1},Error:{0}')", ex.Message, ip);
            OracleDBUtil.ExecuteSql(
                       conn, sqlstrs);
            throw ex;
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            OracleConnection.ClearPool(conn);
        }
        return result;
    }

    /// <summary>
    /// 將傳入的參數寫入至SYS_PROCESS_LOG
    /// </summary>
    private void SaveInfoToLog(string sql)
    {
        try
        {
            conn_new_pos = OracleDBUtil.GetConnection();

            //將傳入參數寫入
            OracleDBUtil.ExecuteSql(conn_new_pos, sql);
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message;
            return;
        }
        finally
        {
            if (conn_new_pos.State == ConnectionState.Open) conn_new_pos.Close();
            conn_new_pos.Dispose();
            OracleConnection.ClearAllPools();
        }

    }

    /// <summary>
    /// 取得參數值
    /// </summary>
    /// <param name="arrTemp2">參數集合</param>
    /// <param name="sUUID">Detail_UUID</param>
    /// <param name="StoreId">門市編號</param>
    /// <param name="EmployeeId">員工編號</param>
    /// <param name="SysId"></param>
    /// <param name="RoleId">角色代號</param>
    /// <param name="TimeStamp"></param>
    /// <param name="MACHINE_ID"></param>
    /// <param name="ActionId"></param>
    /// <param name="HostIP"></param>
    /// <param name="fun_id"></param>
    private void GetParamValue(string[] arrTemp2, ref string sUUID, ref string StoreId, ref string EmployeeId,
        ref string SysId, ref string RoleId, ref string TimeStamp, ref string MACHINE_ID, ref string ActionId,
        ref string HostIP, ref string fun_id)
    {
        foreach (string POSD1 in arrTemp2)
        {
            string POSD = POSD1.ToString().ToUpper();

            if (POSD.ToUpper().IndexOf("POSD=") >= 0)
            {
                string[] arrTemp3 = POSD.Split('=');
                sUUID = arrTemp3[1];
            }

            if (POSD.ToUpper().IndexOf("STOREID=") >= 0)
            {
                string[] arrTemp3 = POSD.Split('=');
                StoreId = arrTemp3[1];
            }

            if (POSD.ToUpper().IndexOf("EMPLOYEEID=") >= 0)
            {
                string[] arrTemp3 = POSD.Split('=');
                EmployeeId = arrTemp3[1];
            }

            if (POSD.ToUpper().IndexOf("SYSID=") >= 0)
            {
                string[] arrTemp3 = POSD.Split('=');
                SysId = arrTemp3[1];
            }

            if (POSD.ToUpper().IndexOf("TIMESTAMP=") >= 0)
            {
                string[] arrTemp3 = POSD.Split('=');
                TimeStamp = arrTemp3[1];
            }

            if (POSD.ToUpper().IndexOf("ROLEID=") >= 0)
            {
                string[] arrTemp3 = POSD.Split('=');
                RoleId = arrTemp3[1];
            }

            if (POSD.ToUpper().IndexOf("MACHINEID=") >= 0)
            {
                string[] arrTemp3 = POSD.Split('=');
                MACHINE_ID = arrTemp3[1];
            }

            if (POSD.ToUpper().IndexOf("ACTIONID=") >= 0)
            {
                string[] arrTemp3 = POSD.Split('=');
                ActionId = arrTemp3[1];
            }

            if (POSD.ToUpper().IndexOf("HOSTIP=") >= 0)
            {
                string[] arrTemp3 = POSD.Split('=');
                HostIP = arrTemp3[1];
            }

            if (POSD.ToUpper().IndexOf("FUNID=") >= 0)
            {
                string[] arrTemp3 = POSD.Split('=');
                fun_id = arrTemp3[1];
            }
        }

    }

    /// <summary>
    /// 網頁自動轉址
    /// </summary>
    /// <param name="ActionId"></param>
    /// <param name="sUUID"></param>
    /// <param name="MACHINE_ID"></param>
    private void RedirectURL(string ActionId, string sUUID, string MACHINE_ID, string StoreId, string EmployeeId, string SysId, string RoleId)
    {
        if (!string.IsNullOrEmpty(ActionId))
        {
            LogMessage QueryStingMsg = new LogMessage();
            QueryStingMsg.STORENO = string.IsNullOrEmpty(StoreId) ? System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultStore"] : StoreId;         //門市代號
            QueryStingMsg.OPERATOR = QueryStingMsg.CREATE_USER = QueryStingMsg.MODI_USER = string.IsNullOrEmpty(EmployeeId) ? System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultEmployee"] : EmployeeId;     //操作者 (登入者帳號)
            QueryStingMsg.ROLE_TYPE = string.IsNullOrEmpty(RoleId) ? System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"] : RoleId;
            QueryStingMsg.FUNCTION_NO = ActionId;    //前端欲連結的頁面 .....待確認 by Tina
            QueryStingMsg.MACHINE_ID = string.IsNullOrEmpty(MACHINE_ID) ? System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultMachineID"] : MACHINE_ID;   //門市機台編號_UUID
            QueryStingMsg.SYS_ID = SysId;           //系統別
            QueryStingMsg.HOST_IP = Request.UserHostAddress;//string.IsNullOrEmpty(HostIP) ? System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultIP"] : HostIP;           //系統別

            Session["QueryStingMsg"] = QueryStingMsg;
        }

        if (ActionId.Equals("CR", StringComparison.InvariantCultureIgnoreCase) || ActionId.Equals("CT", StringComparison.InvariantCultureIgnoreCase) || ActionId.Equals("SC", StringComparison.InvariantCultureIgnoreCase))
        {
            RunCheck(sUUID, MACHINE_ID, StoreId);
        }
        else if (ActionId.Equals("FT", StringComparison.InvariantCultureIgnoreCase))
        {
            lblQ.Text = "";
            lblmessage.Text = "處理完成";
        }
        else if (ActionId.Equals("CC", StringComparison.InvariantCultureIgnoreCase))
        {
            lblQ.Text = "";
            lblmessage.Text = "換卡成功，POS已扣庫存";
        }
        else
        {
            Response.Redirect("~/index.htm", true);
        }

    }

    private void RunCheck(string sUUID, string MACHINE_ID, string StoreId)
    {
        OracleConnection conn = null;
        try
        {
            conn = OracleDBUtil.GetConnection();
            string sqlstr = "select service_type,fun_id from to_close_head where posuuid_detail = " + OracleDBUtil.SqlStr(sUUID);
            OracleCommand cmd = new OracleCommand(sqlstr, conn);
            string source_type = "";
            string fun_id = "";
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                source_type = StringUtil.CStr(dr[0]);
                fun_id = StringUtil.CStr(dr[1]);
            }
            dr.Close();

            bool IsSC = (source_type == "4" && (fun_id == "150" || fun_id == "8" || fun_id == "180" || fun_id == "1" || fun_id == "11"));
            if (source_type == "1" || source_type == "2" || IsSC)
            {
                #region SC
                sc(sUUID, MACHINE_ID, StoreId, conn);
                #endregion
            }
            else if (source_type == "4")
            {
                //判斷FUN_ID
                switch (fun_id)
                {
                    case "121":
                    case "123":
                        #region CR
                        cr(sUUID, MACHINE_ID, StoreId, conn);
                        #endregion
                        break;
                    case "122":
                    case "124":
                        #region CT

                         ct(sUUID, MACHINE_ID);
                        #endregion


                        break;
                    default:
                        string encryptUrl = Utils.Param_Encrypt("POSUUID_DETAIL=" + sUUID + "&machine_id=" + MACHINE_ID);
                        Response.Redirect(string.Format("~/VSS/SAL/TSAL05/TSAL05.aspx?Param={0}", encryptUrl));
                        break;
                }
            }
            else if (source_type == "3")
            {
                sc(sUUID, MACHINE_ID, StoreId, conn);
            }
            else if (source_type == "10")
            {
                sc(sUUID, MACHINE_ID, StoreId, conn);
            }
            else if (source_type == "5")
            {
                sc(sUUID, MACHINE_ID, StoreId, conn);
            }
            else
            {
                string encryptUrl = Utils.Param_Encrypt("POSUUID_DETAIL=" + sUUID + "&machine_id=" + MACHINE_ID);
                Response.Redirect(string.Format("~/VSS/SAL/TSAL05/TSAL05.aspx?Param={0}", encryptUrl));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (conn_new_pos.State == ConnectionState.Open) conn_new_pos.Close();
            OracleConnection.ClearPool(conn_new_pos);
        }
    }

    private void cr(string sUUID, string MACHINE_ID, string StoreId,  OracleConnection conn)
    {
        string posuuid_detail = sUUID;
        string posuuid_master = "";
        get_possuid_master(posuuid_detail, out posuuid_master);

        if (!string.IsNullOrEmpty(posuuid_master))
        {
            string encryptUrl = Utils.Param_Encrypt("SRC_TYPE=CR&PKEY=" + posuuid_master + "&machine_id=" + MACHINE_ID);
            Response.Redirect("~/VSS/SAL/SAL03/SAL03.aspx?Param=" + encryptUrl, true);
        }
        else
        {
            #region SC
            sc(sUUID, MACHINE_ID, StoreId, conn);
            #endregion
        }
       
    }

    private void ct(string sUUID, string MACHINE_ID)
    {
        string posuuid_master;
        get_possuid_master(sUUID, out posuuid_master);

        if (!string.IsNullOrEmpty(posuuid_master))
        {
            bool result = CheckOtherDetail(sUUID, posuuid_master);
            if (result)
            {
              
                string encryptUrl = Utils.Param_Encrypt("SRC_TYPE=CT&PKEY=" + posuuid_master + "&machine_id=" + MACHINE_ID);
                Response.Redirect("~/VSS/SAL/SAL03/SAL03.aspx?Param=" + encryptUrl, true);
            }
            else
            {
              
                string encryptUrl = Utils.Param_Encrypt("SRC_TYPE=CT&PKEY=" + posuuid_master + "&machine_id=" + MACHINE_ID);
                Response.Redirect("~/VSS/SAL/SAL041/SAL04.aspx?Param=" + encryptUrl, true);
            }
        }
        else
        {
            
            string encryptUrl = Utils.Param_Encrypt("SRC_TYPE=TSAL05&PKEY=" + sUUID);
            Response.Redirect("~/VSS/SAL/TSAL01/TSAL01.aspx?Param=" + encryptUrl);
        }
    }

    private void sc(string sUUID, string MACHINE_ID, string StoreId, OracleConnection conn)
    {
        bool IsSmaill_Change = false; //是否需要找零金
        string small_change_url = "~/VSS/CHK/CHK04/CHK04.aspx?";
        //判斷是否有輸入找零金
        string sqlstr = string.Format("select ID from  SMALL_CHANGE   where trunc(TRADE_DATE)=trunc(WorkingDay('{0}')) and STORE_NO='{0}' and MACHINE_ID = '{1}'", StoreId, MACHINE_ID);
    
        OracleCommand cmd = new OracleCommand(sqlstr, conn);

       OracleDataReader   dr = cmd.ExecuteReader();
         IsSmaill_Change = dr.HasRows;
          dr.Close();
    
    
       
        if (IsSmaill_Change)
        {
            //Response.Redirect("~/VSS/SAL/TSAL01/TSAL01.aspx?SRC_TYPE=TSAL05&PKEY=" + sUUID + "&machine_id=" + MACHINE_ID, true);

            //**2011/04/21 Tina：傳遞參數時，要先以加密處理。
            string encryptUrl = Utils.Param_Encrypt("SRC_TYPE=TSAL05&PKEY=" + sUUID + "&machine_id=" + MACHINE_ID);
            Response.Redirect("~/VSS/SAL/TSAL01/TSAL01.aspx?Param=" + encryptUrl, true);
        }
        else
        {
            string encryptUrl = Utils.Param_Encrypt("StoreId=" + StoreId + "&machine_id=" + MACHINE_ID);
            Response.Redirect(small_change_url + "Param=" + encryptUrl, true);
        }
    }


}

