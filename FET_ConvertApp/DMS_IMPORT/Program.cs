using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;

using Advtek.Utility;
using System.Threading;

namespace DMS_IMPORT
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DMS_IMPORT");
            ConvertLog con_log = new ConvertLog("DMS_IMPORT");

            Console.WriteLine("建立DB連線");
            OracleConnection objConn = OracleDBUtil.GetConnection();
            SqlConnection objConn_erp = GetERPPOSConnection(objConn);                

            OracleTransaction trans = objConn.BeginTransaction();
            SqlTransaction trans_erp = objConn_erp.BeginTransaction();

            try
            {
                OracleDBUtil.ExecuteSql(
                    trans,
                    "delete from V_DMR0010");

                DataTable dt = ExecuteDataset(
                    trans_erp,
                    "select * from V_DMR0010"
                    ).Tables[0];

                Console.WriteLine("    INSERT V_DMR0010 ");

                foreach (DataRow dr in dt.Rows)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine(
                        @"Insert into V_DMR0010
                         Values
                           ("
                    );

                    for (int i = 0; i <= 27; i++)
                    {
                        sb.AppendLine("'" + dr[i].ToString() + "',");
                    }

                    sb.AppendLine("null)");

                    OracleDBUtil.ExecuteSql(trans, sb.ToString());
                }

                Console.WriteLine("    CALL SP SP_DMS_IMPORT ");

                OracleParameter outSTATUS = new OracleParameter();
                outSTATUS.OracleType = OracleType.VarChar;
                outSTATUS.Size = 1;
                outSTATUS.ParameterName = "outSTATUS";
                outSTATUS.Direction = ParameterDirection.Output;

                OracleParameter outMESSAGE = new OracleParameter();
                outMESSAGE.OracleType = OracleType.VarChar;
                outMESSAGE.Size = 200;
                outMESSAGE.ParameterName = "outMESSAGE";
                outMESSAGE.Direction = ParameterDirection.Output;

                OracleDBUtil.ExecuteSql_SP(
                    trans,
                    @"SP_DMS_IMPORT",
                    outSTATUS,
                    outMESSAGE
                    );

                if (outSTATUS.Value.ToString() == "1")
                {
                    trans.Commit();
                    trans_erp.Commit();

                    Console.WriteLine("執行結束，寫入LOG");

                    //成功資訊
                    con_log.Success(outMESSAGE.Value.ToString());
                    Thread.Sleep(3000);                
                }
                else
                {
                    throw new Exception(outMESSAGE.Value.ToString());
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                trans_erp.Rollback();

                //失敗資訊
                con_log.Fail(ex.Message);
                Console.WriteLine("例外產生，寫入LOG");
                Console.WriteLine(ex.Message);
                Thread.Sleep(3000);
            }
            finally
            {
                if (objConn_erp.State == ConnectionState.Open) objConn_erp.Close();
                objConn_erp.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public static SqlConnection GetERPPOSConnection(OracleConnection objConn)
        {
            SqlConnection conn = null;
            string sRet = null;

            try
            {
                string strSql = " select para_value from sys_para where para_key='DMS_CONN_STRING' ";

                DataTable dt = OracleDBUtil.GetDataSet(objConn, strSql).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    sRet = dt.Rows[0]["para_value"].ToString();
                }

                conn = new SqlConnection();
                conn.ConnectionString = sRet;
                conn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return conn;
        }

        public static DataSet ExecuteDataset(SqlTransaction transaction, string commandText)
        {
            //create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = transaction.Connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = commandText;
            cmd.Transaction = transaction;

            //create the DataAdapter & DataSet
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            //fill the DataSet using default values for DataTable names, etc.
            da.Fill(ds);

            //return the dataset
            return ds;
        }
    }
}
