using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;

namespace Advtek.Utility
{
    public class ConvertLog
    {
        public string Task_Name;

        /// <summary>
        /// 開始時間
        /// </summary>
        public DateTime S_Time;

        /// <summary>
        /// 結束時間
        /// </summary>
        public DateTime E_Time;

        /// <summary>
        /// 訊息
        /// </summary>
        public string Message = "";

        public string SID;

        /// <summary>
        /// 狀態(0:開始 1:成功 2:失敗)
        /// </summary>
        public string STATUS = "0";

        public ConvertLog(string name)
        {
            Task_Name = name;

            OracleConnection conn = OracleDBUtil.GetConnection();

            StringBuilder sb = new StringBuilder();

            try
            {
                SID = GuidNo.getUUID();

                sb.AppendLine(@"INSERT INTO SCHEDULE_JOB_LOG(   
                            SID, 
                            TASK_NAME, START_DTM, CREATE_DTM, 
                            CREATE_USER, STATUS
                            )
                            VALUES(
                            :SID,
                            :TASK_NAME,
                            SYSDATE,
                            SYSDATE,
                            'Convert',
                            :STATUS
                          )");

                sb.Replace(":SID", OracleDBUtil.SqlStr(SID));
                sb.Replace(":TASK_NAME", OracleDBUtil.SqlStr(Task_Name));
                sb.Replace(":STATUS", OracleDBUtil.SqlStr(STATUS));

                OracleDBUtil.ExecuteSql(
                    conn,
                    sb.ToString()
                    );
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                OracleConnection.ClearAllPools();
                conn = null;
            }
        }

        public void Success(string message)
        {
            Write(message, "1");
        }

        public void Fail(string message)
        {
            Write(message, "2");
        }

        public void Write(string message, string status)
        {
            Message = message;
            OracleConnection conn = OracleDBUtil.GetConnection();
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.AppendLine(@"UPDATE SCHEDULE_JOB_LOG SET
                            END_DTM=SYSDATE,
                            PROCESS_DESC=PROCESS_DESC||:PROCESS_DESC,
                            STATUS=:STATUS
                          WHERE SID=:SID");

                sb.Replace(":SID", OracleDBUtil.SqlStr(SID));
                sb.Replace(":PROCESS_DESC", OracleDBUtil.SqlStr(Message));
                sb.Replace(":STATUS", OracleDBUtil.SqlStr(status));

                OracleDBUtil.ExecuteSql(
                       conn,
                       sb.ToString()
                       );
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                OracleConnection.ClearAllPools();
                conn = null;
            }
        }

        public void Write_Detail(string message)
        {
            OracleConnection conn = OracleDBUtil.GetConnection();

            StringBuilder sb = new StringBuilder();
            try
            {
                sb.AppendLine(@"Insert into SYS_PROCESS_LOG
                   (SNO, FUNC_GROUP, MACHINE_ID, PARAMETER, 
                    CREATE_USER, CREATE_DTM, MODI_USER, MODI_DTM)
                 Values
                   (POS_UUID(), 'CANCLE_TO_CLOSE', :SID, :PROCESS_DESC, 
                    'Convert', SYSDATE, 'Convert', SYSDATE)");

                sb.Replace(":SID", OracleDBUtil.SqlStr(SID));
                sb.Replace(":PROCESS_DESC", OracleDBUtil.SqlStr(message));

                OracleDBUtil.ExecuteSql(
                      conn,
                      sb.ToString()
                      );
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                OracleConnection.ClearAllPools();
                conn = null;
            }
        }
    }
}
