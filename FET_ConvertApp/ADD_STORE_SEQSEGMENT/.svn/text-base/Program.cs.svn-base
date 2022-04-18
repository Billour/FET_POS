using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using System.Threading;

namespace ADD_STORE_SEQSEGMENT
{
    class Program
    {
        private static string sMSG = "";
        static void Main(string[] args)
        {
            OracleConnection conn = OracleDBUtil.GetConnection();
            OracleTransaction trans = conn.BeginTransaction();
            ConvertLog log = new ConvertLog("ADD_STORE_SEQSEGMENT");
            try
            {
                OutputMsg("1.讀取未產生SALE的資料");
                log.Success("1.讀取未產生SALE的資料");
                DataTable dt = getSTORE(trans);
                OutputMsg("2.未產生資料共" + dt.Rows.Count + "筆");
                log.Success("2.未產生資料共" + dt.Rows.Count + "筆");
                foreach (DataRow dr in dt.Rows)
                {
                    OutputMsg("3.Insert" + dr["STORE_NO"].ToString() + "門市");
                    log.Success("3.Insert" + dr["STORE_NO"].ToString() + "門市");
                
                    string sqlStr = @"Insert into WEBPOS.STORE_SEQSEGMENT (SHEET_TYPE, STORE_NO, SEQ_NO, SEG_TYPE)
                                        Values ('SALE', :STORE_NO, 1, 'YY')";
                    sqlStr = sqlStr.Replace(":STORE_NO", OracleDBUtil.SqlStr(dr["STORE_NO"].ToString()));
                    OracleDBUtil.ExecuteSql(trans, sqlStr.ToString());
                    sqlStr = @"Insert into WEBPOS.STORE_SEQSEGMENT (SHEET_TYPE, STORE_NO, SEQ_NO, SEG_TYPE)
                                        Values ('SALE', :STORE_NO, 2, 'MM')";
                    sqlStr = sqlStr.Replace(":STORE_NO", OracleDBUtil.SqlStr(dr["STORE_NO"].ToString()));
                    OracleDBUtil.ExecuteSql(trans, sqlStr.ToString());
                    sqlStr = @"Insert into WEBPOS.STORE_SEQSEGMENT (SHEET_TYPE, STORE_NO, SEQ_NO, SEG_TYPE, SEG_LENGTH, RESET_TYPE, PAD_CHAR)
                                        Values ('SALE', :STORE_NO, 3, 'NUMBER',  4, 'BYMONTH', '0')";
                    sqlStr = sqlStr.Replace(":STORE_NO", OracleDBUtil.SqlStr(dr["STORE_NO"].ToString()));
                    OracleDBUtil.ExecuteSql(trans, sqlStr.ToString());
                }
                trans.Commit();
                log.Success(sMSG);

            }
            catch (Exception ex)
            {
                OutputMsg("<<例外產生>>");
                OutputMsg(ex.Message);
                log.Fail(sMSG);

            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                OracleConnection.ClearAllPools();
                conn = null;
                Thread.Sleep(3000);
            }
        }
        static void OutputMsg(string s)
        {
            Console.WriteLine(s);
            sMSG += s + "\r\n";
        }
        /// <summary>
        /// 取得未結清單資料 for 取消交易用
        /// </summary>
        /// <returns></returns>
        static DataTable getSTORE(OracleTransaction trans)
        {

            string sqlStr = @"SELECT STORE_NO FROM STORE 
                            WHERE   TO_CHAR(SYSDATE,'yyyyMMdd') <= NVL(CLOSEDATE,'99991231')                       
                            AND (TO_CHAR(SYSDATE,'yyyyMMdd') < NVL(STOP_BDATE, TO_CHAR(SYSDATE+1,'yyyyMMdd')) 
                            OR  TO_CHAR(SYSDATE,'yyyyMMdd') > NVL(STOP_EDATE,TO_CHAR(SYSDATE-1,'yyyyMMdd')))   
                            AND STORE_NO NOT IN (SELECT STORE_NO FROM STORE_SEQSEGMENT ) ORDER BY STORE_NO";
            DataTable dt = dt = OracleDBUtil.GetDataSet(trans, sqlStr).Tables[0];

            return dt;
        }
    }
}
