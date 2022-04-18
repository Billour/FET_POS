using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Collections;
using System.Reflection;
using System.Text;


namespace Advtek.Utility
{
   public class OracleDBUtil
   {
      private static string key = ConfigurationManager.AppSettings["ConnStrKey"].ToString();
      private static string iv = ConfigurationManager.AppSettings["ConnStrIV"].ToString();
      private static string strConnString = GetDecryptConnectionString("Connection_String"); // ConfigurationManager.AppSettings["Connection_String"]; 
      private static LogUtil Logger = new LogUtil(typeof(OracleDBUtil));

      public OracleDBUtil()
      {

      }

      /// <summary>
      /// 傳遞 Oracle / PL SQL 連線字串
      /// </summary>
      public static string GetConnectionStringByTNSName()
      {
         string _OracleLogin_HOST = ConfigurationManager.AppSettings["OraclePLSQL_HOST"].ToString();
         string _OracleLogin_USERID = ConfigurationManager.AppSettings["OraclePLSQL_USERID"].ToString();
         string _OracleLogin_PWD = ConfigurationManager.AppSettings["OraclePLSQL_PWD"].ToString();
         string _OracleLogin_SERVICENAME = ConfigurationManager.AppSettings["OraclePLSQL_SERVICENAME"].ToString();
         string _OracleLogin_PORT = ConfigurationManager.AppSettings["OraclePLSQL_PORT"].ToString();

         return "Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = " +
                 _OracleLogin_HOST + ")(PORT = " + _OracleLogin_PORT +
                 "))) (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = " +
                 _OracleLogin_SERVICENAME + ")));Persist Security Info=True;User ID=" +
                 _OracleLogin_USERID + ";Password=" +
                _OracleLogin_PWD + ";";
      }

      /// <summary>
      /// 傳遞 ERP POS Connection 連線
      /// </summary>
      /// <returns></returns>
      public static OracleConnection GetERPPOSConnection()
      {
         OracleConnection conn = null;
         string sRet = null;

         try
         {
            OracleConnection objConn = GetConnection();

            string strSql = " select (select para_value from sys_para where para_key='OLD_POS_USER') as OLD_POS_USER,"
                      + "       (select para_value from sys_para where para_key='OLD_POS_PW') as OLD_POS_PW,"
                      + "       (select para_value from sys_para where para_key='OLD_POS_HOST') as OLD_POS_HOST,"
                      + "       (select para_value from sys_para where para_key='OLD_POS_DBSID') as OLD_POS_DBSID,"
                      + "       (select para_value from sys_para where para_key='OLD_POS_DBPORT') as OLD_POS_DBPORT "
                      + " from dual ";

            DataTable dt = GetDataSet(objConn, strSql).Tables[0];
            if (dt.Rows.Count > 0)
            {
               string _OracleLogin_HOST = dt.Rows[0]["OLD_POS_HOST"].ToString();
               string _OracleLogin_USERID = dt.Rows[0]["OLD_POS_USER"].ToString();
               string _OracleLogin_PWD = dt.Rows[0]["OLD_POS_PW"].ToString();
               string _OracleLogin_SERVICENAME = dt.Rows[0]["OLD_POS_DBSID"].ToString();
               string _OracleLogin_PORT = dt.Rows[0]["OLD_POS_DBPORT"].ToString();
               sRet = "Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = " +
                   _OracleLogin_HOST + ")(PORT = " + _OracleLogin_PORT +
                   "))) (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = " +
                   _OracleLogin_SERVICENAME + ")));Persist Security Info=True;User ID=" +
                   _OracleLogin_USERID + ";Password=" +
                   _OracleLogin_PWD + ";";
            }

            //關閉原來的CONN
            objConn.Close();
            objConn.Dispose();

            Logger.Log.Info("Conn String" + sRet);
            conn = new OracleConnection();
            conn.ConnectionString = sRet;
            conn.Open();
         }
         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);

            //ORA-01427: 單列子查詢所傳回的資料列不只一列
            if (ex.Message.IndexOf("ORA-01427") >= 0)
            {
                throw new Exception("sys_para設定錯誤!");
            }

            throw ex;
         }

         return conn;
      }

      /// <summary>
      /// 傳遞 ESF Connection 連線
      /// </summary>
      /// <returns></returns>
      public static OracleConnection GetESFConnection()
      {
          OracleConnection conn = null;
          string sRet = null;

          try
          {
              OracleConnection objConn = GetConnection();

              string strSql = " select (select para_value from sys_para where para_key='ESF_POS_USER') as OLD_POS_USER,"
                        + "       (select para_value from sys_para where para_key='ESF_POS_PW') as OLD_POS_PW,"
                        + "       (select para_value from sys_para where para_key='ESF_POS_HOST') as OLD_POS_HOST,"
                        + "       (select para_value from sys_para where para_key='ESF_POS_DBSID') as OLD_POS_DBSID,"
                        + "       (select para_value from sys_para where para_key='ESF_POS_DBPORT') as OLD_POS_DBPORT "
                        + " from dual ";

              DataTable dt = GetDataSet(objConn, strSql).Tables[0];
              if (dt.Rows.Count > 0)
              {
                  string _OracleLogin_HOST = dt.Rows[0]["OLD_POS_HOST"].ToString();
                  string _OracleLogin_USERID = dt.Rows[0]["OLD_POS_USER"].ToString();
                  string _OracleLogin_PWD = dt.Rows[0]["OLD_POS_PW"].ToString();
                  string _OracleLogin_SERVICENAME = dt.Rows[0]["OLD_POS_DBSID"].ToString();
                  string _OracleLogin_PORT = dt.Rows[0]["OLD_POS_DBPORT"].ToString();
                  sRet = "Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = " +
                      _OracleLogin_HOST + ")(PORT = " + _OracleLogin_PORT +
                      "))) (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = " +
                      _OracleLogin_SERVICENAME + ")));Persist Security Info=True;User ID=" +
                      _OracleLogin_USERID + ";Password=" +
                      _OracleLogin_PWD + ";";
              }

              //關閉原來的CONN
              objConn.Close();
              objConn.Dispose();

              Logger.Log.Info("Conn String" + sRet);
              conn = new OracleConnection();
              conn.ConnectionString = sRet;
              conn.Open();
          }
          catch (Exception ex)
          {
              Logger.Log.Error(ex.Message, ex);

              //ORA-01427: 單列子查詢所傳回的資料列不只一列
              if (ex.Message.IndexOf("ORA-01427") >= 0)
              {
                  throw new Exception("sys_para設定錯誤!");
              }

              throw ex;
          }

          return conn;
      }

      /// <summary>
      /// 傳遞 HRS Connection 連線
      /// </summary>
      /// <returns></returns>
      public static OracleConnection GetHRSConnection()
      {
          OracleConnection conn = null;
          string sRet = null;

          try
          {
              OracleConnection objConn = GetConnection();

              string strSql = " select (select para_value from sys_para where para_key='HRS_POS_USER') as OLD_POS_USER,"
                        + "       (select para_value from sys_para where para_key='HRS_POS_PW') as OLD_POS_PW,"
                        + "       (select para_value from sys_para where para_key='HRS_POS_HOST') as OLD_POS_HOST,"
                        + "       (select para_value from sys_para where para_key='HRS_POS_DBSID') as OLD_POS_DBSID,"
                        + "       (select para_value from sys_para where para_key='HRS_POS_DBPORT') as OLD_POS_DBPORT "
                        + " from dual ";

              DataTable dt = GetDataSet(objConn, strSql).Tables[0];
              if (dt.Rows.Count > 0)
              {
                  string _OracleLogin_HOST = dt.Rows[0]["OLD_POS_HOST"].ToString();
                  string _OracleLogin_USERID = dt.Rows[0]["OLD_POS_USER"].ToString();
                  string _OracleLogin_PWD = dt.Rows[0]["OLD_POS_PW"].ToString();
                  string _OracleLogin_SERVICENAME = dt.Rows[0]["OLD_POS_DBSID"].ToString();
                  string _OracleLogin_PORT = dt.Rows[0]["OLD_POS_DBPORT"].ToString();
                  sRet = "Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = " +
                      _OracleLogin_HOST + ")(PORT = " + _OracleLogin_PORT +
                      "))) (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = " +
                      _OracleLogin_SERVICENAME + ")));Persist Security Info=True;User ID=" +
                      _OracleLogin_USERID + ";Password=" +
                      _OracleLogin_PWD + ";";
              }

              //關閉原來的CONN
              objConn.Close();
              objConn.Dispose();

              Logger.Log.Info("Conn String" + sRet);
              conn = new OracleConnection();
              conn.ConnectionString = sRet;
              conn.Open();
          }
          catch (Exception ex)
          {
              Logger.Log.Error(ex.Message, ex);

              //ORA-01427: 單列子查詢所傳回的資料列不只一列
              if (ex.Message.IndexOf("ORA-01427") >= 0)
              {
                  throw new Exception("sys_para設定錯誤!");
              }

              throw ex;
          }

          return conn;
      }

      public static OracleConnection GetSPDBConnection()
      {
          OracleConnection conn = null;
          string sRet = null;

          try
          {
              OracleConnection objConn = GetConnection();

              string strSql = " select (select para_value from sys_para where para_key='SP_DB_USER') as OLD_POS_USER,"
                        + "       (select para_value from sys_para where para_key='SP_DB_PW') as OLD_POS_PW,"
                        + "       (select para_value from sys_para where para_key='SP_DB_HOST') as OLD_POS_HOST,"
                        + "       (select para_value from sys_para where para_key='SP_DB_DBSID') as OLD_POS_DBSID,"
                        + "       (select para_value from sys_para where para_key='SP_DB_DBPORT') as OLD_POS_DBPORT "
                        + " from dual ";

              DataTable dt = GetDataSet(objConn, strSql).Tables[0];
              if (dt.Rows.Count > 0)
              {
                  string _OracleLogin_HOST = dt.Rows[0]["OLD_POS_HOST"].ToString();
                  string _OracleLogin_USERID = dt.Rows[0]["OLD_POS_USER"].ToString();
                  string _OracleLogin_PWD = dt.Rows[0]["OLD_POS_PW"].ToString();
                  string _OracleLogin_SERVICENAME = dt.Rows[0]["OLD_POS_DBSID"].ToString();
                  string _OracleLogin_PORT = dt.Rows[0]["OLD_POS_DBPORT"].ToString();
                  sRet = "Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = " +
                      _OracleLogin_HOST + ")(PORT = " + _OracleLogin_PORT +
                      "))) (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = " +
                      _OracleLogin_SERVICENAME + ")));Persist Security Info=True;User ID=" +
                      _OracleLogin_USERID + ";Password=" +
                      _OracleLogin_PWD + ";";
              }

              //關閉原來的CONN
              objConn.Close();
              objConn.Dispose();

              Logger.Log.Info("Conn String" + sRet);
              conn = new OracleConnection();
              conn.ConnectionString = sRet;
              conn.Open();
          }
          catch (Exception ex)
          {
              Logger.Log.Error(ex.Message, ex);

              //ORA-01427: 單列子查詢所傳回的資料列不只一列
              if (ex.Message.IndexOf("ORA-01427") >= 0)
              {
                  throw new Exception("sys_para設定錯誤!");
              }

              throw ex;
          }

          return conn;
      }

      /// <summary>
      /// 傳遞 OLD POD Oracle / PL SQL 連線字串
      /// </summary>
      /// <returns></returns>
      public static string GetOldPOSConnectionStringByTNSName()
      {
         string sRet = "";
         string scon = GetConnectionStringByTNSName();
         using (OracleConnection con = new OracleConnection(scon))
         {
            try
            {
               string sql = " select (select para_value from sys_para where para_key='OLD_POS_USER') as OLD_POS_USER,"
                         + "       (select para_value from sys_para where para_key='OLD_POS_PW') as OLD_POS_PW,"
                         + "       (select para_value from sys_para where para_key='OLD_POS_HOST') as OLD_POS_HOST,"
                         + "       (select para_value from sys_para where para_key='OLD_POS_DBSID') as OLD_POS_DBSID,"
                         + "       (select para_value from sys_para where para_key='OLD_POS_DBPORT') as OLD_POS_DBPORT "
                         + " from dual ";
               DataTable dt = new DataTable();
               OracleCommand cmd = new OracleCommand(sql, con);
               OracleDataAdapter da = new OracleDataAdapter(cmd);
               da.Fill(dt);
               if (dt.Rows.Count > 0)
               {
                  string _OracleLogin_HOST = dt.Rows[0]["OLD_POS_HOST"].ToString();
                  string _OracleLogin_USERID = dt.Rows[0]["OLD_POS_USER"].ToString();
                  string _OracleLogin_PWD = dt.Rows[0]["OLD_POS_PW"].ToString();
                  string _OracleLogin_SERVICENAME = dt.Rows[0]["OLD_POS_DBSID"].ToString();
                  string _OracleLogin_PORT = dt.Rows[0]["OLD_POS_DBPORT"].ToString();
                  sRet = "Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = " +
                      _OracleLogin_HOST + ")(PORT = " + _OracleLogin_PORT +
                      "))) (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = " +
                      _OracleLogin_SERVICENAME + ")));Persist Security Info=True;User ID=" +
                      _OracleLogin_USERID + ";Password=" +
                      _OracleLogin_PWD + ";";
               }
            }
            catch (Exception ex) 
            {     
                throw ex;
            }
            finally
            {
               con.Dispose();
            }

         }
         return sRet;
      }

      /// <summary>
      /// 傳遞 IA Oracle / PL SQL 連線字串
      /// </summary>
      /// <returns></returns>
      public static string GetIAConnectionStringByTNSName()
      {
          string sRet = "";
          using (OracleConnection con = GetConnection())
          {
              try
              {
                  string sql = " select (select para_value from sys_para where para_key='IA_DB_USER') as IA_DB_USER,"
                            + "       (select para_value from sys_para where para_key='IA_DB_PW') as IA_DB_PW,"
                            + "       (select para_value from sys_para where para_key='IA_DB_HOST') as IA_DB_HOST,"
                            + "       (select para_value from sys_para where para_key='IA_DB_DBSID') as IA_DB_DBSID,"
                            + "       (select para_value from sys_para where para_key='IA_DB_DBPORT') as IA_DB_DBPORT "
                            + " from dual ";
                  DataTable dt = new DataTable();
                  OracleCommand cmd = new OracleCommand(sql, con);
                  OracleDataAdapter da = new OracleDataAdapter(cmd);
                  da.Fill(dt);
                  if (dt.Rows.Count > 0)
                  {
                      string _OracleLogin_HOST = dt.Rows[0]["IA_DB_HOST"].ToString();
                      string _OracleLogin_USERID = dt.Rows[0]["IA_DB_USER"].ToString();
                      string _OracleLogin_PWD = dt.Rows[0]["IA_DB_PW"].ToString();
                      string _OracleLogin_SERVICENAME = dt.Rows[0]["IA_DB_DBSID"].ToString();
                      string _OracleLogin_PORT = dt.Rows[0]["IA_DB_DBPORT"].ToString();
                      sRet = "Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = " +
                          _OracleLogin_HOST + ")(PORT = " + _OracleLogin_PORT +
                          "))) (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = " +
                          _OracleLogin_SERVICENAME + ")));Persist Security Info=True;User ID=" +
                          _OracleLogin_USERID + ";Password=" +
                          _OracleLogin_PWD + ";";
                  }
              }
              catch (Exception ex)
              {
                  throw ex;
              }
              finally
              {
                  if (con.State == ConnectionState.Open) con.Close();
                  con.Dispose();
                  OracleConnection.ClearAllPools();
              }

          }
          return sRet;
      }

      /// <summary>
      /// 傳遞 LOY Oracle / PL SQL 連線字串
      /// </summary>
      /// <returns></returns>
      public static string GetLOYConnectionStringByTNSName()
      {
          string sRet = "";
          using (OracleConnection con = GetConnection())
          {
              try
              {
                  string sql = " select (select para_value from sys_para where para_key='LY_DB_USER') as LY_DB_USER,"
                            + "       (select para_value from sys_para where para_key='LY_DB_PW') as LY_DB_PW,"
                            + "       (select para_value from sys_para where para_key='LY_DB_HOST') as LY_DB_HOST,"
                            + "       (select para_value from sys_para where para_key='LY_DB_DBSID') as LY_DB_DBSID,"
                            + "       (select para_value from sys_para where para_key='LY_DB_DBPORT') as LY_DB_DBPORT "
                            + " from dual ";
                  DataTable dt = new DataTable();
                  OracleCommand cmd = new OracleCommand(sql, con);
                  OracleDataAdapter da = new OracleDataAdapter(cmd);
                  da.Fill(dt);
                  if (dt.Rows.Count > 0)
                  {
                      string _OracleLogin_HOST = dt.Rows[0]["LY_DB_HOST"].ToString();
                      string _OracleLogin_USERID = dt.Rows[0]["LY_DB_USER"].ToString();
                      string _OracleLogin_PWD = dt.Rows[0]["LY_DB_PW"].ToString();
                      string _OracleLogin_SERVICENAME = dt.Rows[0]["LY_DB_DBSID"].ToString();
                      string _OracleLogin_PORT = dt.Rows[0]["LY_DB_DBPORT"].ToString();
                      sRet = "Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = " +
                          _OracleLogin_HOST + ")(PORT = " + _OracleLogin_PORT +
                          "))) (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = " +
                          _OracleLogin_SERVICENAME + ")));Persist Security Info=True;User ID=" +
                          _OracleLogin_USERID + ";Password=" +
                          _OracleLogin_PWD + ";";
                  }
              }
              catch (Exception ex)
              {
                  throw ex;
              }
              finally
              {
                  if (con.State == ConnectionState.Open) con.Close();
                  con.Dispose();
                  OracleConnection.ClearAllPools();
              }

          }
          return sRet;
      }

      /// <summary>
      /// 傳遞 SSI Oracle / PL SQL 連線字串
      /// </summary>
      /// <returns></returns>
      public static string GetSSIConnectionStringByTNSName()
      {
          string sRet = "";
          using (OracleConnection con = GetConnection())
          {
              try
              {
                  string sql = " select (select para_value from sys_para where para_key='SSI_DB_USER') as SSI_DB_USER,"
                            + "       (select para_value from sys_para where para_key='SSI_DB_PW') as SSI_DB_PW,"
                            + "       (select para_value from sys_para where para_key='SSI_DB_HOST') as SSI_DB_HOST,"
                            + "       (select para_value from sys_para where para_key='SSI_DB_DBSID') as SSI_DB_DBSID,"
                            + "       (select para_value from sys_para where para_key='SSI_DB_DBPORT') as SSI_DB_DBPORT "
                            + " from dual ";
                  DataTable dt = new DataTable();
                  OracleCommand cmd = new OracleCommand(sql, con);
                  OracleDataAdapter da = new OracleDataAdapter(cmd);
                  da.Fill(dt);
                  if (dt.Rows.Count > 0)
                  {
                      string _OracleLogin_HOST = dt.Rows[0]["SSI_DB_HOST"].ToString();
                      string _OracleLogin_USERID = dt.Rows[0]["SSI_DB_USER"].ToString();
                      string _OracleLogin_PWD = dt.Rows[0]["SSI_DB_PW"].ToString();
                      string _OracleLogin_SERVICENAME = dt.Rows[0]["SSI_DB_DBSID"].ToString();
                      string _OracleLogin_PORT = dt.Rows[0]["SSI_DB_DBPORT"].ToString();
                      sRet = "Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = " +
                          _OracleLogin_HOST + ")(PORT = " + _OracleLogin_PORT +
                          "))) (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = " +
                          _OracleLogin_SERVICENAME + ")));Persist Security Info=True;User ID=" +
                          _OracleLogin_USERID + ";Password=" +
                          _OracleLogin_PWD + ";";
                  }
              }
              catch (Exception ex)
              {
                  throw ex;
              }
              finally
              {
                  if (con.State == ConnectionState.Open) con.Close();
                  con.Dispose();
                  OracleConnection.ClearAllPools();
              }

          }
          return sRet;
      }

      /// <summary>
      /// 傳遞 PAYMENT Oracle / PL SQL 連線字串
      /// </summary>
      /// <returns></returns>
      public static string GetPAYMENTConnectionStringByTNSName()
      {
          string sRet = "";
          using (OracleConnection con = GetConnection())
          {
              try
              {
                  string sql = " select (select para_value from sys_para where para_key='PY_DB_USER') as PY_DB_USER,"
                            + "       (select para_value from sys_para where para_key='PY_DB_PW') as PY_DB_PW,"
                            + "       (select para_value from sys_para where para_key='PY_DB_HOST') as PY_DB_HOST,"
                            + "       (select para_value from sys_para where para_key='PY_DB_DBSID') as PY_DB_DBSID,"
                            + "       (select para_value from sys_para where para_key='PY_DB_DBPORT') as PY_DB_DBPORT "
                            + " from dual ";
                  DataTable dt = new DataTable();
                  OracleCommand cmd = new OracleCommand(sql, con);
                  OracleDataAdapter da = new OracleDataAdapter(cmd);
                  da.Fill(dt);
                  if (dt.Rows.Count > 0)
                  {
                      string _OracleLogin_HOST = dt.Rows[0]["PY_DB_HOST"].ToString();
                      string _OracleLogin_USERID = dt.Rows[0]["PY_DB_USER"].ToString();
                      string _OracleLogin_PWD = dt.Rows[0]["PY_DB_PW"].ToString();
                      string _OracleLogin_SERVICENAME = dt.Rows[0]["PY_DB_DBSID"].ToString();
                      string _OracleLogin_PORT = dt.Rows[0]["PY_DB_DBPORT"].ToString();
                      sRet = "Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = " +
                          _OracleLogin_HOST + ")(PORT = " + _OracleLogin_PORT +
                          "))) (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = " +
                          _OracleLogin_SERVICENAME + ")));Persist Security Info=True;User ID=" +
                          _OracleLogin_USERID + ";Password=" +
                          _OracleLogin_PWD + ";";
                  }
              }
              catch (Exception ex)
              {
                  throw ex;
              }
              finally
              {
                  if (con.State == ConnectionState.Open) con.Close();
                  con.Dispose();
                  OracleConnection.ClearAllPools();
              }

          }
          return sRet;
      }

      /// <summary>
      /// 傳遞 OLR Oracle / PL SQL 連線字串
      /// </summary>
      /// <returns></returns>
      public static string GetOLRConnectionStringByTNSName()
      {
          string sRet = "";
          using (OracleConnection con = GetConnection())
          {
              try
              {
                  string sql = " select (select para_value from sys_para where para_key='OLR_DB_USER') as OLR_DB_USER,"
                            + "       (select para_value from sys_para where para_key='OLR_DB_PW') as OLR_DB_PW,"
                            + "       (select para_value from sys_para where para_key='OLR_DB_HOST') as OLR_DB_HOST,"
                            + "       (select para_value from sys_para where para_key='OLR_DB_DBSID') as OLR_DB_DBSID,"
                            + "       (select para_value from sys_para where para_key='OLR_DB_DBPORT') as OLR_DB_DBPORT "
                            + " from dual ";
                  DataTable dt = new DataTable();
                  OracleCommand cmd = new OracleCommand(sql, con);
                  OracleDataAdapter da = new OracleDataAdapter(cmd);
                  da.Fill(dt);
                  if (dt.Rows.Count > 0)
                  {
                      string _OracleLogin_HOST = dt.Rows[0]["OLR_DB_HOST"].ToString();
                      string _OracleLogin_USERID = dt.Rows[0]["OLR_DB_USER"].ToString();
                      string _OracleLogin_PWD = dt.Rows[0]["OLR_DB_PW"].ToString();
                      string _OracleLogin_SERVICENAME = dt.Rows[0]["OLR_DB_DBSID"].ToString();
                      string _OracleLogin_PORT = dt.Rows[0]["OLR_DB_DBPORT"].ToString();
                      sRet = "Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = " +
                          _OracleLogin_HOST + ")(PORT = " + _OracleLogin_PORT +
                          "))) (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = " +
                          _OracleLogin_SERVICENAME + ")));Persist Security Info=True;User ID=" +
                          _OracleLogin_USERID + ";Password=" +
                          _OracleLogin_PWD + ";";
                  }
              }
              catch (Exception ex)
              {
                  throw ex;
              }
              finally
              {
                  if (con.State == ConnectionState.Open) con.Close();
                  con.Dispose();
                  OracleConnection.ClearAllPools();
              }

          }
          return sRet;
      }

      /// <summary>
      /// 傳遞 BOU Oracle / PL SQL 連線字串
      /// </summary>
      /// <returns></returns>
      public static string GetBOUConnectionStringByTNSName()
      {
          string sRet = "";
          using (OracleConnection con = GetConnection())
          {
              try
              {
                  string sql = " select (select para_value from sys_para where para_key='BOU_USER') as BOU_USER,"
                            + "       (select para_value from sys_para where para_key='BOU_PW') as BOU_PW,"
                            + "       (select para_value from sys_para where para_key='BOU_HOST') as BOU_HOST,"
                            + "       (select para_value from sys_para where para_key='BOU_DBSID') as BOU_DBSID,"
                            + "       (select para_value from sys_para where para_key='BOU_DBPORT') as BOU_DBPORT "
                            + " from dual ";
                  DataTable dt = new DataTable();
                  OracleCommand cmd = new OracleCommand(sql, con);
                  OracleDataAdapter da = new OracleDataAdapter(cmd);
                  da.Fill(dt);
                  if (dt.Rows.Count > 0)
                  {
                      string _OracleLogin_HOST = dt.Rows[0]["BOU_HOST"].ToString();
                      string _OracleLogin_USERID = dt.Rows[0]["BOU_USER"].ToString();
                      string _OracleLogin_PWD = dt.Rows[0]["BOU_PW"].ToString();
                      string _OracleLogin_SERVICENAME = dt.Rows[0]["BOU_DBSID"].ToString();
                      string _OracleLogin_PORT = dt.Rows[0]["BOU_DBPORT"].ToString();
                      sRet = "Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = " +
                          _OracleLogin_HOST + ")(PORT = " + _OracleLogin_PORT +
                          "))) (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = " +
                          _OracleLogin_SERVICENAME + ")));Persist Security Info=True;User ID=" +
                          _OracleLogin_USERID + ";Password=" +
                          _OracleLogin_PWD + ";";
                  }
              }
              catch (Exception ex)
              {
                  throw ex;
              }
              finally
              {
                  if (con.State == ConnectionState.Open) con.Close();
                  con.Dispose();
                  OracleConnection.ClearAllPools();
              }

          }
          return sRet;
      }

      /// <summary>
      /// 傳遞 ESTore Oracle / PL SQL 連線字串
      /// </summary>
      /// <returns></returns>
      public static string GetEStoreConnectionStringByTNSName()
      {
          string sRet = "";
          using (OracleConnection con = GetConnection())
          {
              try
              {
                  string sql = " select (select para_value from sys_para where para_key='ESF_POS_USER') as ESF_POS_USER,"
                            + "       (select para_value from sys_para where para_key='ESF_POS_PW') as ESF_POS_PW,"
                            + "       (select para_value from sys_para where para_key='ESF_POS_HOST') as ESF_POS_HOST,"
                            + "       (select para_value from sys_para where para_key='ESF_POS_DBSID') as ESF_POS_DBSID,"
                            + "       (select para_value from sys_para where para_key='ESF_POS_DBPORT') as ESF_POS_DBPORT "
                            + " from dual ";
                  DataTable dt = new DataTable();
                  OracleCommand cmd = new OracleCommand(sql, con);
                  OracleDataAdapter da = new OracleDataAdapter(cmd);
                  da.Fill(dt);
                  if (dt.Rows.Count > 0)
                  {
                      string _OracleLogin_HOST = dt.Rows[0]["ESF_POS_HOST"].ToString();
                      string _OracleLogin_USERID = dt.Rows[0]["ESF_POS_USER"].ToString();
                      string _OracleLogin_PWD = dt.Rows[0]["ESF_POS_PW"].ToString();
                      string _OracleLogin_SERVICENAME = dt.Rows[0]["ESF_POS_DBSID"].ToString();
                      string _OracleLogin_PORT = dt.Rows[0]["ESF_POS_DBPORT"].ToString();
                      sRet = "Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = " +
                          _OracleLogin_HOST + ")(PORT = " + _OracleLogin_PORT +
                          "))) (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = " +
                          _OracleLogin_SERVICENAME + ")));Persist Security Info=True;User ID=" +
                          _OracleLogin_USERID + ";Password=" +
                          _OracleLogin_PWD + ";";
                  }
              }
              catch (Exception ex)
              {
                  throw ex;
              }
              finally
              {
                  if (con.State == ConnectionState.Open) con.Close();
                  con.Dispose();
                  OracleConnection.ClearAllPools();
              }

          }
          return sRet;
      }

      /// <summary>
      /// 傳遞 Connection 連線 BY PARA_KEY
      /// </summary>
      /// <returns></returns>
      public static OracleConnection GetConnectionByParaKey(string sUser,string sPW ,string sHost,string sSID ,string sPort)
      {
          OracleConnection conn = null;
          OracleConnection objConn = null;
          string sRet = null;
          try
          {
              objConn = GetConnection();

              string strSql = string.Format(@"select 
                               (select para_value from sys_para where para_key={0} and rownum=1) as OLD_POS_USER,
                               (select para_value from sys_para where para_key={1} and rownum=1) as OLD_POS_PW,
                               (select para_value from sys_para where para_key={2} and rownum=1) as OLD_POS_HOST,
                               (select para_value from sys_para where para_key={3} and rownum=1) as OLD_POS_DBSID,
                               (select para_value from sys_para where para_key={4} and rownum=1) as OLD_POS_DBPORT 
                              from dual "
                             ,OracleDBUtil.SqlStr(sUser),OracleDBUtil.SqlStr(sPW),OracleDBUtil.SqlStr(sHost)
                             ,OracleDBUtil.SqlStr(sSID),OracleDBUtil.SqlStr(sPort)) ;

              DataTable dt = GetDataSet(objConn, strSql).Tables[0];
              if (dt.Rows.Count > 0)
              {
                  string _OracleLogin_HOST = dt.Rows[0]["OLD_POS_HOST"].ToString();
                  string _OracleLogin_USERID = dt.Rows[0]["OLD_POS_USER"].ToString();
                  string _OracleLogin_PWD = dt.Rows[0]["OLD_POS_PW"].ToString();
                  string _OracleLogin_SERVICENAME = dt.Rows[0]["OLD_POS_DBSID"].ToString();
                  string _OracleLogin_PORT = dt.Rows[0]["OLD_POS_DBPORT"].ToString();
                  sRet = "Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = " +
                      _OracleLogin_HOST + ")(PORT = " + _OracleLogin_PORT +
                      "))) (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = " +
                      _OracleLogin_SERVICENAME + ")));Persist Security Info=True;User ID=" +
                      _OracleLogin_USERID + ";Password=" +
                      _OracleLogin_PWD + ";";
              }

              //關閉原來的CONN
              objConn.Close();
              objConn.Dispose();

              Logger.Log.Info("Conn String" + sRet);
              conn = new OracleConnection();
              conn.ConnectionString = sRet;
              conn.Open();
          }
          catch (Exception ex)
          {
              Logger.Log.Error(ex.Message, ex);

              //ORA-01427: 單列子查詢所傳回的資料列不只一列
              if (ex.Message.IndexOf("ORA-01427") >= 0)
              {
                  throw new Exception("sys_para設定錯誤!");
              }

              throw ex;
          }
          finally {
              if (objConn.State == ConnectionState.Open) objConn.Close();
              objConn.Dispose();
              OracleConnection.ClearAllPools();
          }

          return conn;
      }

      public static string GetConnectionString()
      {
         return strConnString;
      }

      public static string GetConnectionString(string Connection_String_KEY)
      {
         string strResult = "";

         try
         {
            strResult = GetDecryptConnectionString(Connection_String_KEY);

         }
         catch (Exception ex)
         {
            throw ex;
         }
         return strResult;

      }

      public static string GetDecryptConnectionString(string Connection_String_KEY)
      {
         string ConnString;
         string strResult = "";

         try
         {
            ConnString = ConfigurationManager.AppSettings[Connection_String_KEY].ToString();
            strResult = DESCryptography.DecryptDES(ConnString, key, iv);
         }
         catch (Exception ex)
         {
            throw ex;
         }
         return strResult;

      }

      #region "GetConnection 取得 connection "

      public static OracleConnection GetConnection()
      {
         OracleConnection conn = null;
         string Connection_String_KEY = "";

         try
         {
            Connection_String_KEY = "Connection_String";
            strConnString = GetDecryptConnectionString(Connection_String_KEY);

            Logger.Log.Info("Conn String" + strConnString);
            conn = new OracleConnection();

            try
            {
                conn.ConnectionString = strConnString;
            }
            catch (ArgumentException ArgumentEX)
            {
                throw new Exception("Config中的ConnString設定錯誤!", ArgumentEX);
            }
            
            conn.Open();
         }
         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
         }

         return conn;
      }

      public static OracleConnection GetConnection(string KeyString)
      {
         OracleConnection conn = null;
         string strConnString;

         try
         {

            strConnString = GetDecryptConnectionString(KeyString);
            Logger.Log.Info("Conn String" + strConnString);
            conn = new OracleConnection();
            conn.ConnectionString = strConnString;
            conn.Open();
         }
         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
         }

         return conn;
      }
      #endregion

      #region "GetConnectionByConnString "

      public static OracleConnection GetConnectionByConnString(string ConnString)
      {
         OracleConnection conn = null;
         string strConnString;

         try
         {
            strConnString = ConnString;
            conn = new OracleConnection();

            conn.ConnectionString = strConnString;
            conn.Open();
         }
         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
         }

         return conn;
      }
      #endregion

      #region "DataReader2Hashtable"
      public static Hashtable DataReader2Hashtable(OracleDataReader sdr)
      {
         Hashtable htlRow = null;
         int intLoop;

         try
         {
            htlRow = new Hashtable();

            string strFieldValue = "";
            string strFieldName = "";
            int intRowIndex = 0;

            while (sdr.Read())
            {
               Hashtable htlCol = new Hashtable();

               for (intLoop = 0; intLoop <= sdr.FieldCount - 1; intLoop++)
               {

                  strFieldName = StringUtil.CStr(sdr.GetName(intLoop).ToUpper());
                  strFieldValue = StringUtil.CStr(sdr.GetValue(intLoop));

                  if (!htlCol.ContainsKey(strFieldName))
                  {
                     htlCol.Add(strFieldName, strFieldValue);
                  }

               }

               htlRow.Add(intRowIndex, htlCol);
               intRowIndex = intRowIndex + 1;

            }
         }

         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
         }
         finally
         {

         }

         return htlRow;
      }
      #endregion

      #region "GetData 2 HashTable 有 OracleTransaction"
      public static Hashtable GetData(OracleTransaction PA_Tx, string PA_Sql)
      {
         Hashtable htlRow = null;
         OracleDataReader sdr = null;

         try
         {
            Logger.Log.Info("SQL=" + PA_Sql);
            sdr = OracleHelper.ExecuteReader(PA_Tx, CommandType.Text, PA_Sql, null);

            htlRow = DataReader2Hashtable(sdr);
         }
         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
         }
         finally
         {
            if ((sdr != null))
            {
               sdr.Close();
               sdr = null;
            }
            
         }

         return htlRow;
      }



      public static Hashtable GetData(OracleConnection PA_Conn, string PA_Sql)
      {
         Hashtable htlRow = null;
         OracleDataReader sdr = null;

         try
         {
            Logger.Log.Info("SQL=" + PA_Sql);
            sdr = OracleHelper.ExecuteReader(PA_Conn, CommandType.Text, PA_Sql, null);

            htlRow = DataReader2Hashtable(sdr);
         }

         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
         }
         finally
         {
            if ((sdr != null))
            {
               sdr.Close();
               sdr = null;
            }

         }

         return htlRow;
      }


      public static Hashtable GetData(string PA_ConnString, string PA_Sql)
      {
         Hashtable htlRow = null;
         OracleDataReader sdr = null;

         try
         {
            Logger.Log.Info("SQL=" + PA_Sql);
            sdr = OracleHelper.ExecuteReader(PA_ConnString, CommandType.Text, PA_Sql, null);

            htlRow = DataReader2Hashtable(sdr);
         }
         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
         }
         finally
         {
            if ((sdr != null))
            {
               sdr.Close();
               sdr = null;
            }
         }

         return htlRow;
      }
      #endregion

      #region "GetDataReader"
      public static OracleDataReader GetDataReader(OracleTransaction PA_Tx, string PA_Sql)
      {
         OracleDataReader sdr = null;

         try
         {
            Logger.Log.Info("SQL=" + PA_Sql);
            sdr = OracleHelper.ExecuteReader(PA_Tx, CommandType.Text, PA_Sql, null);
         }

         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
         }
         finally
         {
         }

         return sdr;
      }


      public static OracleDataReader GetDataReader(OracleConnection PA_Conn, string PA_Sql)
      {
         OracleDataReader sdr = null;

         try
         {
            Logger.Log.Info("SQL=" + PA_Sql);
            sdr = OracleHelper.ExecuteReader(PA_Conn, CommandType.Text, PA_Sql, null);
         }

         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
         }
         finally
         {
         }

         return sdr;
      }


      public static OracleDataReader GetDataReader(string PA_ConnString, string PA_Sql)
      {
         OracleDataReader sdr = null;

         try
         {
            Logger.Log.Info("SQL=" + PA_Sql);
            sdr = OracleHelper.ExecuteReader(PA_ConnString, CommandType.Text, PA_Sql, null);
         }

         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
         }
         finally
         {

         }

         return sdr;
      }
      #endregion

      #region "GetData 2 DataTable 有 OracleTransaction"
      public static DataSet GetDataSet(OracleTransaction PA_Tx, string PA_Sql)
      {
         DataSet ds = null;

         try
         {
            Logger.Log.Info("SQL=" + PA_Sql);
            ds = OracleHelper.ExecuteDataset(PA_Tx, CommandType.Text, PA_Sql, null);
         }
         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
         }
         finally
         {
         }

         return ds;
      }


      public static DataSet GetDataSet(OracleConnection PA_Conn, string PA_Sql)
      {
         DataSet ds = null;

         try
         {
            Logger.Log.Info("SQL=" + PA_Sql);
            ds = OracleHelper.ExecuteDataset(PA_Conn, CommandType.Text, PA_Sql, null);
         }

         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
         }
         finally
         {
         }

         return ds;
      }


      public static DataSet GetDataSet(string PA_ConnString, string PA_Sql)
      {
         DataSet ds = null;

         try
         {
            Logger.Log.Info("SQL=" + PA_Sql);
            ds = OracleHelper.ExecuteDataset(PA_ConnString, CommandType.Text, PA_Sql, null);
         }
         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
         }
         finally
         {

         }

         return ds;
      }
      #endregion

      #region "ExecuteSql By Connection "
      public static int ExecuteSql(OracleConnection PA_Conn, string PA_SQL)
      {
         int intResult;

         try
         {
            Logger.Log.Info("SQL=" + PA_SQL);
            intResult = OracleHelper.ExecuteNonQuery(PA_Conn, CommandType.Text, PA_SQL);
         }

         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
         }
         finally
         {
             
         }

         return intResult;
      }

      public static int ExecuteSql(string PA_ConnString, string PA_SQL)
      {
         int intResult;

         try
         {
            Logger.Log.Info("SQL=" + PA_SQL);
            intResult = OracleHelper.ExecuteNonQuery(PA_ConnString, CommandType.Text, PA_SQL);
         }

         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
         }
         finally
         {

         }

         return intResult;
      }

      public static int ExecuteSql(OracleTransaction PA_Tx, string PA_SQL)
      {
         int intResult;

         try
         {
            Logger.Log.Info("SQL=" + PA_SQL);
            intResult = OracleHelper.ExecuteNonQuery(PA_Tx, CommandType.Text, PA_SQL);
         }

         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
         }
         finally
         {

         }

         return intResult;
      }


      public static int ExecuteSql_SP(OracleTransaction PA_Tx, string spName, params OracleParameter[] para)
      {
         int intResult;

         try
         {
            //Logger.Log.Info("SQL=" + PA_SQL);
            intResult = OracleHelper.ExecuteNonQuery(PA_Tx, CommandType.StoredProcedure, spName, para);
         }

         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
         }
         finally
         {

         }

         return intResult;
      }

      public static int ExecuteSql_SP(OracleTransaction PA_Tx, string spName, out string code, out string message)
      {
          int intResult;

          OracleParameter outSTATUS = new OracleParameter();
          outSTATUS.OracleType = OracleType.VarChar;
          outSTATUS.Size = 10;
          outSTATUS.ParameterName = "O_RT_CODE";
          outSTATUS.Direction = ParameterDirection.Output;

          OracleParameter outMESSAGE = new OracleParameter();
          outMESSAGE.OracleType = OracleType.VarChar;
          outMESSAGE.Size = 2000;
          outMESSAGE.ParameterName = "O_RT_MESSAGE";
          outMESSAGE.Direction = ParameterDirection.Output;

          try
          {
              //Logger.Log.Info("SQL=" + PA_SQL);
              intResult = OracleHelper.ExecuteNonQuery(
                  PA_Tx, 
                  CommandType.StoredProcedure, 
                  spName, 
                  outSTATUS,
                  outMESSAGE
                  );

              code = outSTATUS.Value.ToString();
              message = outMESSAGE.Value.ToString();
          }

          catch (Exception ex)
          {
              Logger.Log.Error(ex.Message, ex);
              throw ex;
          }
          finally
          {

          }

          return intResult;
      }


      #endregion

      #region 處理日期格式
      public static string DateFormate(object objValue)
      {
         string strResult = "";
         DateTime dtmValue;
         string strValue;

         try
         {
            if (StringUtil.CStr(objValue).Length > 0)
            {
               dtmValue = (DateTime)objValue;
               strValue = dtmValue.ToString("yyyy/MM/dd HH:mm:ss");
               if (dtmValue.ToString("yyyy/MM/dd") == Advtek.Utility.DateUtil.NullDateFormatString)
                  strResult = "NULL";
               else
                  if (strValue.IndexOf(" 00:00:00") > 0)
                  {
                     strResult = "to_date(" + OracleDBUtil.SqlStr(dtmValue.ToString("yyyy/MM/dd")) + ",'yyyy/mm/dd')";
                  }
                  else
                  {
                     strResult = "to_date(" + OracleDBUtil.SqlStr(dtmValue.ToString("yyyy/MM/dd HH:mm:ss")) + ",'yyyy/mm/dd hh24:mi:ss')";
                  }
            }
            else
            {//處理更新日期NULL的問題

               strResult = "to_date(" + OracleDBUtil.SqlStr(StringUtil.CStr(objValue)) + ",'yyyy/mm/dd')";
            }


         }
         catch (Exception ex)
         {
            throw ex;
         }

         return strResult;

      }
      #endregion

      #region 轉換產生 Oracle Script 的 INSERT INTO (DataTable _DT, bool bDBNull)
      /// <summary>
      /// 解析資料表格產生 INSERT INTO 語法
      /// </summary>
      /// <param name="_DT">要析解的資料表</param>
      /// <param name="_strIndexTableName">指定索引欄位</param>
      /// <param name="_strOracleSequence">Oracle Sequence Name</param>
      /// <returns>StringBuilder</returns>
      /// 
      public int Insert(DataTable _DT, String _strIndexColumnName, string _strOracleSequence)
      {
         int intResult = 0;
         OracleConnection objConn = null;
         OracleTransaction objTX = null;

         try
         {
            objConn = OracleDBUtil.GetConnection();
            objTX = objConn.BeginTransaction();
            intResult = Insert(objTX, _DT, _strIndexColumnName, _strOracleSequence);
            objTX.Commit();

         }
         catch (Exception ex)
         {
            objTX.Rollback();
            throw ex;
         }
         finally
         {
            objTX = null;
            //objConn = null;
            if (objConn.State == ConnectionState.Open) objConn.Close();
            objConn.Dispose();
            OracleConnection.ClearAllPools();

         }
         return intResult;
      }

      public int Insert(OracleTransaction objTX, DataTable _DT, String _strIndexColumnName, string _strOracleSequence)
      {
         int intResult = 0;
         StringBuilder _sbScript;
         StringBuilder _sbColumnNameScript;
         StringBuilder _sbColumnNameScriptValues;

         try
         {
            foreach (DataRow dr in _DT.Rows)
            {
               _sbScript = new StringBuilder();
               _sbColumnNameScript = new StringBuilder();
               _sbColumnNameScriptValues = new StringBuilder();

               _sbScript.Append(" INSERT INTO " + _DT.TableName.ToString());
               _sbScript.Append("(");


               //判斷是否有指定欄位為自動遞增值
               if (_strIndexColumnName.Length != 0)
               {
                  _sbColumnNameScript.Append(_strIndexColumnName);
                  _strOracleSequence = _strOracleSequence.ToUpper().Replace("NEXTVAL", "");
                  _strOracleSequence = _strOracleSequence.ToUpper().Replace(".", "");
                  _sbColumnNameScriptValues.Append(_strOracleSequence + ".NEXTVAL");
               }


               for (int i = 0; i <= _DT.Columns.Count - 1; i++)
               {

                  if (Convert.IsDBNull(dr[i]) == false)
                  {
                     if (_DT.Columns[i].ColumnName.ToString().ToUpper() != _strIndexColumnName.ToUpper())
                     {
                        //取欄位名稱
                        _sbColumnNameScript.Append(_DT.Columns[i].ColumnName.ToString());


                        //取欄位值組VALUE
                        switch (_DT.Columns[i].DataType.Name.ToUpper())
                        {
                           case "DATETIME":
                           case "DATE":
                              _sbColumnNameScriptValues.Append(DateFormate(dr[i]));
                              break;

                           default:
                              _sbColumnNameScriptValues.Append(OracleDBUtil.SqlStr(StringUtil.CStr(dr[i])));
                              break;
                        }

                        if (i != _DT.Columns.Count - 1)
                        {
                           _sbColumnNameScript.Append(",");
                           _sbColumnNameScriptValues.Append(",");
                        }
                     }
                  }

               }

               _sbScript.Append(_sbColumnNameScript.ToString() + ") values(" + _sbColumnNameScriptValues.ToString() + ")");

               intResult = OracleDBUtil.ExecuteSql(objTX, _sbScript.ToString());

               if (intResult == 0) throw new Exception("Insert into SQL Execute 失敗. ");

            }
         }
         catch (Exception onerror)
         {
            throw onerror;
         }
         finally
         {

         }
         return intResult;

      }

      /// <summary>
      /// 解析資料表格產生 INSERT INTO 語法
      /// </summary>
      /// <param name="_DT">要析解的資料表</param>
      /// <returns>StringBuilder</returns>
      /// 
      public static int Insert(DataTable _DT)
      {
         int intResult = 0;
         OracleConnection objConn = null;
         OracleTransaction objTX = null;

         try
         {
            objConn = OracleDBUtil.GetConnection();
            objTX = objConn.BeginTransaction();
            intResult = Insert(objTX, _DT);
            objTX.Commit();

         }
         catch (Exception ex)
         {
            objTX.Rollback();
            throw ex;
         }
         finally
         {
             if (objConn.State == ConnectionState.Open)
                 objConn.Close();
             objConn.Dispose();
             OracleConnection.ClearAllPools();
         }
         return intResult;
      }

      public static int Insert(OracleTransaction objTX, DataTable _DT)
      {
         int intResult = 0;
         StringBuilder _sbScript;
         StringBuilder _sbColumnNameScript;
         StringBuilder _sbColumnNameScriptValues;

         try
         {
            foreach (DataRow dr in _DT.Rows)
            {
               _sbScript = new StringBuilder();
               _sbColumnNameScript = new StringBuilder();
               _sbColumnNameScriptValues = new StringBuilder();

               _sbScript.Append(" INSERT INTO " + _DT.TableName.ToString());
               _sbScript.Append("(");

               for (int i = 0; i <= _DT.Columns.Count - 1; i++)
               {
                  if (Convert.IsDBNull(dr[i]) == false)
                  {
                     //取欄位名稱
                     _sbColumnNameScript.Append(_DT.Columns[i].ColumnName.ToString());


                     //取欄位值組VALUE
                     switch (_DT.Columns[i].DataType.Name.ToUpper())
                     {
                        case "DATETIME":
                        case "DATE":
                           _sbColumnNameScriptValues.Append(DateFormate(dr[i]));
                           break;

                        default:
                           _sbColumnNameScriptValues.Append(OracleDBUtil.SqlStr(StringUtil.CStr(dr[i])));
                           break;
                     }

                     if (i != _DT.Columns.Count - 1)
                     {
                        _sbColumnNameScript.Append(",");
                        _sbColumnNameScriptValues.Append(",");
                     }
                  }
               }

               //避免表格非最後一個欄位不是null時出現,問題
               _sbColumnNameScript.Replace(",", "", _sbColumnNameScript.Length - 1, 1);
               _sbColumnNameScriptValues.Replace(",", "", _sbColumnNameScriptValues.Length - 1, 1);


               //避免 ',' 造成SQL語法錯誤
               string tempColumn = _sbColumnNameScript.ToString();
               string tempValue = _sbColumnNameScriptValues.ToString();
               int ColLength = tempColumn.Length;
               int ValLangth = tempValue.Length;
               string strCol = tempColumn.Substring(ColLength - 1, 1);
               string strVal = tempValue.Substring(ValLangth - 1, 1);

               if (strCol == ",") tempColumn = tempColumn.Substring(0, ColLength - 1);
               if (strVal == ",") tempValue = tempValue.Substring(0, ColLength - 1);


               _sbScript.Append(tempColumn.ToString() + ") values(" + tempValue.ToString() + ")");

               intResult = OracleDBUtil.ExecuteSql(objTX, _sbScript.ToString());

               if (intResult == 0) throw new Exception("Insert into SQL Execute 失敗. ");

            }
         }
         catch (Exception onerror)
         {
            throw onerror;
         }
         finally
         {

         }
         return intResult;
      }

      /// <summary>
      /// 解析資料表格產生 INSERT INTO 語法
      /// </summary>
      /// <param name="_DT">要析解的資料表</param>
      /// <param name="_strExclusionColumnName">要排除的欄位名稱</param>
      /// <returns>StringBuilder</returns>
      /// 
      public static int Insert(DataTable _DT, string _strExclusionColumnName)
      {
         int intResult = 0;
         OracleConnection objConn = null;
         OracleTransaction objTX = null;

         try
         {
            objConn = OracleDBUtil.GetConnection();
            objTX = objConn.BeginTransaction();
            intResult = Insert(objTX, _DT, _strExclusionColumnName);
            objTX.Commit();

         }
         catch (Exception ex)
         {
            objTX.Rollback();
            throw ex;
         }
         finally
         {
            objTX = null;
            //objConn = null;
            if (objConn.State == ConnectionState.Open) objConn.Close();
            objConn.Dispose();
            OracleConnection.ClearAllPools();

         }
         return intResult;
      }

      public static int Insert(OracleTransaction objTX, DataTable _DT, string _strExclusionColumnName)
      {
         int intResult = 0;
         StringBuilder _sbScript;
         StringBuilder _sbColumnNameScript;
         StringBuilder _sbColumnNameScriptValues;

         try
         {
            foreach (DataRow dr in _DT.Rows)
            {
               _sbScript = new StringBuilder();
               _sbColumnNameScript = new StringBuilder();
               _sbColumnNameScriptValues = new StringBuilder();

               _sbScript.Append(" INSERT INTO " + _DT.TableName.ToString());
               _sbScript.Append("(");

               for (int i = 0; i <= _DT.Columns.Count - 1; i++)
               {
                  if (Convert.IsDBNull(dr[i]) == false)
                  {
                     if (_strExclusionColumnName.ToUpper().IndexOf(_DT.Columns[i].ColumnName.ToUpper().ToString()) == -1)
                     {  //取欄位名稱
                        _sbColumnNameScript.Append(_DT.Columns[i].ColumnName.ToString());


                        //取欄位值組VALUE
                        switch (_DT.Columns[i].DataType.Name.ToUpper())
                        {
                           case "DATETIME":
                           case "DATE":
                              _sbColumnNameScriptValues.Append(DateFormate(dr[i]));
                              break;

                           default:
                              _sbColumnNameScriptValues.Append(OracleDBUtil.SqlStr(StringUtil.CStr(dr[i])));
                              break;
                        }

                        if (i != _DT.Columns.Count - 1)
                        {
                           _sbColumnNameScript.Append(",");
                           _sbColumnNameScriptValues.Append(",");
                        }
                     }

                  }
                  //避免 ',' 造成SQL語法錯誤
                  string tempColumn = _sbColumnNameScript.ToString();
                  string tempValue = _sbColumnNameScriptValues.ToString();
                  int ColLength = tempColumn.Length;
                  int ValLangth = tempValue.Length;
                  string strCol = tempColumn.Substring(ColLength - 1, 1);
                  string strVal = tempValue.Substring(ValLangth - 1, 1);

                  if (strCol == ",") tempColumn = tempColumn.Substring(0, ColLength - 1);
                  if (strVal == ",") tempValue = tempValue.Substring(0, ColLength - 1);

                  _sbScript.Append(tempColumn + ") values(" + tempValue + ")");

                  intResult = OracleDBUtil.ExecuteSql(objTX, _sbScript.ToString());

                  if (intResult == 0) throw new Exception("Insert into SQL Execute 失敗. ");
               }

            }
         }

         catch (Exception onerror)
         {
            throw onerror;
         }
         finally
         {

         }
         return intResult;
      }

      #endregion

      #region 轉換產生 Oracle Script 的UPDDATE(DataTable _DTNoDBIsNull, string strSQLWhereScript)
      /// <summary>
      /// 解析資料表格產生 UPDATE 語法，排除 Data 為 Null 的欄位就不予以產生
      /// </summary>
      /// <param name="_DT">資料結構</param>
      /// <param name="strSQLWhereScript">SQL Where 條件</param>
      /// <returns>StringBuilder</returns>
      /// 

      public static int UPDDATE(DataTable _DT, string strSQLWhereScript)
      {
         int intResult = 0;
         OracleConnection objConn = null;
         OracleTransaction objTX = null;

         try
         {
            objConn = OracleDBUtil.GetConnection();
            objTX = objConn.BeginTransaction();
            intResult = UPDDATE(objTX, _DT, strSQLWhereScript);
            objTX.Commit();

         }
         catch (Exception ex)
         {
            objTX.Rollback();
            throw ex;
         }
         finally
         {
            objTX = null;
            //objConn = null;
            if (objConn.State == ConnectionState.Open) objConn.Close();
            objConn.Dispose();
            OracleConnection.ClearAllPools();

         }
         return intResult;
      }

      public static int UPDDATE(OracleTransaction objTX, DataTable _DT, string strSQLWhereScript)
      {
         int intResult = 0;
         StringBuilder _sbScript;
         StringBuilder _sbWhereCondition;

         try
         {
            foreach (DataRow dr in _DT.Rows)
            {
               _sbScript = new StringBuilder();
               _sbWhereCondition =

               _sbScript.Append(" UPDATE " + _DT.TableName.ToString());
               _sbScript.Append(" SET ");

               for (int i = 0; i <= _DT.Columns.Count - 1; i++)
               {
                  if (Convert.IsDBNull(dr[i]) == false)
                  {
                     //取欄位名稱
                     _sbScript.Append(_DT.Columns[i].ColumnName.ToString() + "=");

                     //取欄位值
                     switch (_DT.Columns[i].DataType.Name.ToUpper())
                     {
                        case "DATETIME":
                        case "DATE":
                           // Fix by Albert Hsu 2010/10/28 
                           #region 原先的程式碼
                           //_sbScript.Append(DateFormate(dr[i]));
                           #endregion
                           #region 修改過後的程式碼
                           if (Convert.ToDateTime(dr[i]) == DateTime.MaxValue)
                           {
                              _sbScript.Append("NULL");
                           }
                           else
                           {
                              _sbScript.Append(DateFormate(dr[i]));
                           }
                           break;
                           #endregion
                        default:
                           _sbScript.Append(OracleDBUtil.SqlStr(StringUtil.CStr(dr[i])));
                           break;
                     }
                     if (i != _DT.Columns.Count - 1) _sbScript.Append(",");
                  }
               }

               //避免 ',' 造成SQL語法錯誤
               string tempSQL = _sbScript.ToString();
               int intLength = tempSQL.Length;
               string strCol = tempSQL.Substring(intLength - 1, 1);
               if (strCol == ",") tempSQL = tempSQL.Substring(0, intLength - 1);

               string strSQL = tempSQL + " Where 1=1  " + strSQLWhereScript;
               intResult = OracleDBUtil.ExecuteSql(objTX, strSQL);
               if (intResult == 0) throw new Exception("UPDATE SQL Execute 失敗. ");
            }
         }
         catch (Exception ex)
         {
            throw ex;
         }
         finally
         {

         }
         return intResult;
      }

      /// <summary>
      /// 解析資料表格產生 UPDATE 語法，以UUID為對象
      /// </summary>
      /// <param name="_DT">資料結構</param>
      /// <param name="_strUUIDField">UUID的欄位名稱</param>
      /// <returns>int</returns>
      /// 
      public static int UPDDATEByUUID(DataTable _DT, string _strUUIDField)
      {
         int intResult = 0;
         OracleConnection objConn = null;
         OracleTransaction objTX = null;

         try
         {
            objConn = OracleDBUtil.GetConnection();
            objTX = objConn.BeginTransaction();
            intResult = UPDDATEByUUID(objTX, _DT, _strUUIDField);
            objTX.Commit();

         }
         catch (Exception ex)
         {
            objTX.Rollback();
            throw ex;
         }
         finally
         {
             if (objConn.State == ConnectionState.Open)
                 objConn.Close();
             objConn.Dispose();
             OracleConnection.ClearAllPools();
         }
         return intResult;
      }

      public static int UPDDATEByUUID(OracleTransaction objTX, DataTable _DT, string _strUUIDField)
      {
         int intResult = 0;
         StringBuilder _sbScript;
         StringBuilder _sbWhereCondition;

         try
         {
            foreach (DataRow dr in _DT.Rows)
            {
               _sbScript = new StringBuilder();
               _sbWhereCondition =

               _sbScript.Append(" UPDATE " + _DT.TableName.ToString());
               _sbScript.Append(" SET ");

               for (int i = 0; i <= _DT.Columns.Count - 1; i++)
               {
                  if (Convert.IsDBNull(dr[i]) == false)
                  {
                     //取欄位名稱
                     _sbScript.Append(_DT.Columns[i].ColumnName.ToString() + "=");

                     //取欄位值
                     switch (_DT.Columns[i].DataType.Name.ToUpper())
                     {
                        case "DATETIME":
                        case "DATE":
                           _sbScript.Append(DateFormate(dr[i]));
                           break;

                        default:
                           _sbScript.Append(OracleDBUtil.SqlStr(StringUtil.CStr(dr[i])));
                           break;
                     }
                     if (i != _DT.Columns.Count - 1) _sbScript.Append(",");
                  }
               }

               string tempSQL = _sbScript.ToString();
               int intLength = tempSQL.Length;
               string strCol = tempSQL.Substring(intLength - 1, 1);
               if (strCol == ",") tempSQL = tempSQL.Substring(0, intLength - 1);

               string strSQL = tempSQL.ToString() + " Where  " + _strUUIDField + "=" + OracleDBUtil.SqlStr(StringUtil.CStr(dr[_strUUIDField]));

               intResult = OracleDBUtil.ExecuteSql(objTX, strSQL);

               if (intResult == 0) throw new Exception("UPDATE SQL Execute 失敗. ");
            }
         }
         catch (Exception ex)
         {
            throw ex;
         }
         finally
         {

         }
         return intResult;
      }

      /// <summary>
      /// 更新資料表，包含Null值
      /// </summary>
      /// <param name="_DT">資料結構</param>
      /// <param name="_strUUIDField">UUID的欄位名稱</param>
      /// <returns>int</returns>
      public static int UPDDATEByUUID_IncludeDBNull(DataTable _DT, string _strUUIDField)
      {
          int intResult = 0;
          OracleConnection objConn = null;
          OracleTransaction objTX = null;

          try
          {
              objConn = OracleDBUtil.GetConnection();
              objTX = objConn.BeginTransaction();
              intResult = UPDDATEByUUID_IncludeDBNull(objTX, _DT, _strUUIDField);
              objTX.Commit();

          }
          catch (Exception ex)
          {
              objTX.Rollback();
              throw ex;
          }
          finally
          {
              if (objConn.State == ConnectionState.Open)
                  objConn.Close();
              objConn.Dispose();
              OracleConnection.ClearAllPools();
          }
          return intResult;
      }

      public static int UPDDATEByUUID_IncludeDBNull(OracleTransaction objTX, DataTable _DT, string _strUUIDField)
      {
          int intResult = 0;
          StringBuilder _sbScript;
          StringBuilder _sbWhereCondition;

          try
          {
              foreach (DataRow dr in _DT.Rows)
              {
                  _sbScript = new StringBuilder();
                  _sbWhereCondition =

                  _sbScript.Append(" UPDATE " + _DT.TableName.ToString());
                  _sbScript.Append(" SET ");

                  for (int i = 0; i <= _DT.Columns.Count - 1; i++)
                  {
                          //取欄位名稱
                          _sbScript.Append(_DT.Columns[i].ColumnName.ToString() + "=");

                          if (Convert.IsDBNull(dr[i]))
                          {
                              _sbScript.Append("null");
                          }
                          else
                          {
                              //取欄位值
                              switch (_DT.Columns[i].DataType.Name.ToUpper())
                              {
                                  case "DATETIME":
                                  case "DATE":
                                      _sbScript.Append(OracleDBUtil.DateFormate(dr[i]));
                                      break;

                                  default:
                                      _sbScript.Append(OracleDBUtil.SqlStr(StringUtil.CStr(dr[i])));
                                      break;
                              }
                          }
                          if (i != _DT.Columns.Count - 1) _sbScript.Append(",");
                      
                  }

                  string tempSQL = _sbScript.ToString();
                  int intLength = tempSQL.Length;
                  string strCol = tempSQL.Substring(intLength - 1, 1);
                  if (strCol == ",") tempSQL = tempSQL.Substring(0, intLength - 1);

                  string strSQL = tempSQL.ToString() + " Where  " + _strUUIDField + "=" + OracleDBUtil.SqlStr(StringUtil.CStr(dr[_strUUIDField]));

                  intResult = OracleDBUtil.ExecuteSql(objTX, strSQL);

                  if (intResult == 0) throw new Exception("UPDATE SQL Execute 失敗. ");
              }
          }
          catch (Exception ex)
          {
              throw ex;
          }
          finally
          {

          }
          return intResult;
      }


      #endregion

      #region 轉換產生 Oracle Script 的DELETE(DataTable _DT, string strSQLWhereScript)

      /// <summary>
      /// 解析資料表格產生 DELETE 語法+SQL Where 條件
      /// </summary>
      /// <param name="_DT">資料結構</param>
      /// <param name="strSQLWhereScript">SQL Where 條件</param>
      /// <returns>StringBuilder</returns>
      /// 
      public static int DELETE(DataTable _DT, string _strWhereScript)
      {
         int intResult = 0;
         OracleConnection objConn = null;
         OracleTransaction objTX = null;

         try
         {
            objConn = OracleDBUtil.GetConnection();
            objTX = objConn.BeginTransaction();
            intResult = DELETE(objTX, _DT, _strWhereScript);
            objTX.Commit();

         }
         catch (Exception ex)
         {
            objTX.Rollback();
            throw ex;
         }
         finally
         {
            objTX = null;
            //objConn = null;
            if (objConn.State == ConnectionState.Open) objConn.Close();
            objConn.Dispose();
            OracleConnection.ClearAllPools();

         }
         return intResult;
      }

      public static int DELETE(OracleTransaction objTX, DataTable _DT, string _strWhereScript)
      {
         int intResult = 0;
         StringBuilder _sbScript;

         try
         {
            foreach (DataRow dr in _DT.Rows)
            {
               _sbScript = new StringBuilder();
               _sbScript.Append(" Delete " + _DT.TableName.ToString());
               _sbScript.Append("  Where 1=1 " + _strWhereScript);

               intResult = OracleDBUtil.ExecuteSql(objTX, _sbScript.ToString());
               if (intResult == 0) throw new Exception("DELETE SQL Execute 失敗. ");
            }
         }
         catch (Exception ex)
         {
            throw ex;
         }
         finally
         {

         }
         return intResult;
      }


      /// <summary>
      /// 解析資料表格產生 DELETE 語法，以UUID為對象
      /// </summary>
      /// <param name="_DT">資料結構</param>
      /// <param name="_strUUIDField">UUID的欄位名稱</param>
      /// <returns>int</returns>
      /// 
      public static int DELETEByUUID(DataTable _DT, string _strUUIDField)
      {
         int intResult = 0;
         OracleConnection objConn = null;
         OracleTransaction objTX = null;

         try
         {
            objConn = OracleDBUtil.GetConnection();
            objTX = objConn.BeginTransaction();
            intResult = DELETEByUUID(objTX, _DT, _strUUIDField);
            objTX.Commit();

         }
         catch (Exception ex)
         {
            objTX.Rollback();
            throw ex;
         }
         finally
         {
             if (objConn.State == ConnectionState.Open)
                 objConn.Close();
             objConn.Dispose();
             OracleConnection.ClearAllPools();
         }
         return intResult;
      }

      public static int DELETEByUUID(OracleTransaction objTX, DataTable _DT, string _strUUIDField)
      {
         int intResult = 0;
         StringBuilder _sbScript;

         try
         {
            foreach (DataRow dr in _DT.Rows)
            {
               _sbScript = new StringBuilder();
               _sbScript.Append(" Delete " + _DT.TableName.ToString());
               _sbScript.Append("  Where  " + _strUUIDField + "=" + OracleDBUtil.SqlStr(StringUtil.CStr(dr[_strUUIDField])));

               intResult = OracleDBUtil.ExecuteSql(objTX, _sbScript.ToString());
               if (intResult == 0) throw new Exception("DELETE SQL Execute 失敗. ");
            }
         }
         catch (Exception ex)
         {
            throw ex;
         }
         finally
         {

         }
         return intResult;
      }

      #endregion

      #region "NullStr"
      public static string NullStr(string strValue)
      {
         string result = "";

         try
         {
            if (strValue.Equals(""))
            {
               result = "null";
            }
            else
            {
               result = strValue;
            }
         }
         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
         }
         finally
         {

         }

         return result;
      }
      #endregion

      #region "MultiSqlStr"
      public static string MultiSqlStr(string strValue, string strSplitter)
      {
         string strResult = "";
         string[] strTemp = strValue.Split(strSplitter.ToCharArray());// Strings.Split(strValue, strSplitter); 
         //string strItem; 

         try
         {
            foreach (string strItem in strTemp)
            {
               if (!strResult.Equals(""))
               {
                  strResult += ",";
               }
               strResult += SqlStr(strItem);
            }
         }
         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
         }
         finally
         {

         }

         return strResult;
      }
      #endregion

      #region "SqlStr 清除字串中有 SQL 隱碼攻擊的字串 「'」「 」「;」「--」 「|」「\t」「\n」"
      public static string SqlStr(string mString)
      {
         string result;

         try
         {

            if (mString == null)
               mString = "";
            else
            {
               mString = ReplaceInjectionStrig(mString);
            }

            result = "'" + mString + "'";
         }
         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
         }
         finally
         {

         }

         return result;
      }

      private static string ReplaceInjectionStrig(string mString)
      {
         mString = mString.Replace("'", "''");
         //mString = mString.Replace(" ", "");
         mString = mString.Replace(";", "");
         mString = mString.Replace("--", "");
         mString = mString.Replace("|", "");
         mString = mString.Replace("\t", "");
         mString = mString.Replace("\n", "");

         return mString;
      }

      #endregion

      #region "DateStr"
      public static string DateStr(string strValue)
      {
         string result;
         try
         {
            if (strValue.Equals(""))
            {
               result = "null";
            }
            else
            {
               result = "to_date(" + OracleDBUtil.SqlStr(strValue) + ", 'YYYY/MM/DD') ";
            }
         }
         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
         }
         finally
         {

         }

         return result;
      }
      #endregion

      #region "TimeStr"
      public static string TimeStr(string strValue)
      {
         string result;

         try
         {
            if (strValue.Equals(""))
            {
               result = "null";
            }
            else
            {
               result = "to_date(" + OracleDBUtil.SqlStr(strValue) + ", 'YYYY/MM/DD HH24:MI:SS') ";
            }
         }

         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
         }
         finally
         {

         }

         return result;
      }
      #endregion

      #region "LikeStr"
      public static string LikeStr(string strValue)
      {
         string result;
         try
         {
            result = "'%" + ReplaceInjectionStrig(strValue) + "%'";
         }
         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
         }
         finally
         {

         }

         return result;
      }
      #endregion

      #region "LastLikeStr"
      public static string LastLikeStr(string strValue)
      {
          string result;
          try
          {
              result = "'" + ReplaceInjectionStrig(strValue) + "%'";
          }
          catch (Exception ex)
          {
              Logger.Log.Error(ex.Message, ex);
              throw ex;
          }
          finally
          {

          }

          return result;
      }
      #endregion

      #region "ToLowerLikeStr"
      public static string ToLowerLikeStr(string strValue)
      {
          string result;
          try
          {
              result = "LOWER('%" + ReplaceInjectionStrig(strValue) + "%')";
          }
          catch (Exception ex)
          {
              Logger.Log.Error(ex.Message, ex);
              throw ex;
          }
          finally
          {

          }

          return result;
      }
      #endregion

      #region "NumberStr"
      public static string NumberStr(string strValue)
      {
         string result;
         try
         {
            if (strValue.Equals(""))
            {
               result = "0";
            }
            else
            {
               result = "to_number(" + OracleDBUtil.SqlStr(strValue) + ") ";
            }
         }
         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
         }
         finally
         {

         }

         return result;
      }
      #endregion

      #region "WorkDay"
      /// <summary>
      /// 取得營業日 ex. 2010/11/09
      /// </summary>
      /// <param name="STORE_NO">門市編號</param>
      /// <returns>營業日</returns>
      public static string WorkDay(string STORE_NO)
      {
         OracleConnection conn = null;
         string strSQL = " select WorkingDay(" + SqlStr(STORE_NO) + ") from dual ";
         DataSet ds = null;
         string strResult = "";

         try
         {
            conn = OracleDBUtil.GetConnection();
            ds = OracleDBUtil.GetDataSet(conn, strSQL);

            if (ds.Tables[0].Rows.Count > 0)
            {
               foreach (DataRow dr in ds.Tables[0].Rows)
               {
                  strResult = Convert.ToDateTime(dr[0]).ToString("yyyy/MM/dd");

                  Logger.Log.Info("營業日 取日:" + strResult.ToString() + "   :   Length=" + strResult.Length);
               }
            }
            else
            {
               throw new Exception("營業日No Data Fund");
            }
         }
         catch (Exception ex)
         {
            Logger.Log.Error(ex.Message);
            throw ex;
         }
         finally
         {
            //conn = null;
             if (conn.State == ConnectionState.Open) conn.Close();
             conn.Dispose();
             OracleConnection.ClearAllPools();
            ds.Dispose();
         }
         return strResult;
      }
      #endregion

      #region "傳入查詢字串，回傳DataTable"
      /// <summary>
      /// 傳入查詢字串，回傳DataTable。
      /// </summary>
      /// <param name="sSQL"></param>
      /// <returns></returns>
      public static DataTable Query_Data(string sSQL)
      {
          OracleConnection objConn = null;
          DataTable dt = new DataTable();
          try
          {
              objConn = GetConnection();
              dt = GetDataSet(objConn, sSQL).Tables[0];
          }
          catch (Exception ex)
          {
              throw ex;
          }
          finally
          {
              if (objConn.State == ConnectionState.Open) objConn.Close();
              objConn.Dispose();
              OracleConnection.ClearAllPools();
          }

          return dt;

      }
      #endregion


   }
}