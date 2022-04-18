using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;
using System.Threading;

namespace SyncHRS_FIXI
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化LOG
            ConvertLog con_log = new ConvertLog("SyncHRS_FIXI");

            OracleConnection conn = null;
            OracleTransaction objTX = null;

            OracleConnection conn_hrs = null;

            try
            {
                conn = OracleDBUtil.GetConnection();                

                //step:1 get SREIALNO
                string temp_no = OracleDBUtil.GetDataSet(
                    conn,
                    "Select Max(Nvl(SREIALNO,0)) from HRS_POSFIXI "
                    ).Tables[0].Rows[0][0].ToString();
                Console.WriteLine("1.找出最近一筆維修單號:" + temp_no);

                //step2:
                OracleDBUtil.ExecuteSql(
                    conn,
                    "Delete HRS_POSFIXI_TEMP "
                    );
                Console.WriteLine("2.Delete HRS_POS_FIXI_TEMP");

                conn_hrs = OracleDBUtil.GetHRSConnection();

                //step3:
                Console.WriteLine("3.連線到HRS DB ，找出大於" + temp_no + " 的維修單");
                string sSQL = @"SELECT sreialno, storeno, fixno, custname, custtel, pro_amt,
                                       fixdate, imeino, status_code, uppdate,
                                       (SELECT brand
                                          FROM ro_main
                                         WHERE romainid = SUBSTR (fixno, 1, LENGTH (fixno) - 1)) AS BRAND,
                                       (SELECT model
                                          FROM ro_main
                                         WHERE romainid = SUBSTR (fixno, 1, LENGTH (fixno) - 1)) AS MODEL,
                                       DECODE
                                       ((SELECT outsource
                                           FROM ro_main
                                          WHERE romainid = SUBSTR (fixno, 1, LENGTH (fixno) - 1)),
                                         2, (SELECT DISTINCT vendorname
                                               FROM def_vendor
                                              WHERE vendorid IN (
                                                  SELECT outcomp
                                                    FROM ro_main
                                                   WHERE romainid =
                                                          SUBSTR (fixno,
                                                                      1,
                                                        LENGTH (fixno) - 1
                                                    ))),'遠傳內修' ) AS FIX_BRAND,
                                           '' AS PROD_TYPE
                                   FROM pos_fixi where SREIALNO >  NVL('" + temp_no + "','0') ";
                DataTable sourceDt = OracleDBUtil.GetDataSet(conn_hrs,sSQL).Tables[0];                
                //DataTable sourceDt = OracleDBUtil.GetDataSet(
                //    conn_hrs,
                //    "Select * FROM POS_FIXI where SREIALNO >  NVL('" + temp_no + "','0') "
                //    ).Tables[0];                

                conn_hrs.Close();

                objTX = conn.BeginTransaction();

                Console.WriteLine("4.Insert 到WEB POS DB 的HRS_POSFIXI_TEMP 資料表中。");
                if (sourceDt.Rows.Count > 0)
                {
                    string ulsno = OracleDBUtil.GetDataSet(
                    objTX,
                    "Select pos_uuid() from dual "
                    ).Tables[0].Rows[0][0].ToString();

                    foreach (DataRow dr in sourceDt.Rows)
                    {
                        StringBuilder sb = new StringBuilder();
                        //***20110225 InsertDate > YYYY/MM/DD
                        sb.AppendLine(
                            @"Insert into HRS_POSFIXI_TEMP(
                              SREIALNO, STORENO, FIXNO, 
                              PRO_AMT, IMEINO, STATUS_CODE, 
                              UPPDATE, INSERT_DATE, ULSNO, 
                              CUSTNAME, CUSTTEL, FIXDATE, 
                              FIX_BRAND, PROD_TYPE, BRAND, 
                              MODEL
                            )
                            Values
                           (:SREIALNO, 
                            Substr(:STORENO,2,4) ,
                            :FIXNO, 
                            :PRO_AMT, 
                            :IMEINO, 
                            :STATUS_CODE, 
                            :UPPDATE, 
                            to_char(Sysdate,'YYYY/MM/DD'), 
                            :ULSNO, 
                            :CUSTNAME, 
                            :CUSTTEL, 
                            :FIXDATE,
                            [FIX_BRAND],
                            [PROD_TYPE], 
                            [BRAND], 
                            [MODEL]
                            )"
                        );

                        sb.Replace(":STORENO", OracleDBUtil.SqlStr(dr["STORENO"].ToString()));
                        sb.Replace(":SREIALNO", OracleDBUtil.SqlStr(dr["SREIALNO"].ToString()));
                        sb.Replace(":FIXNO", OracleDBUtil.SqlStr(dr["FIXNO"].ToString()));
                        sb.Replace(":IMEINO", OracleDBUtil.SqlStr(dr["IMEINO"].ToString()));
                        sb.Replace(":UPPDATE", OracleDBUtil.DateStr(Convert.ToDateTime(dr["UPPDATE"]).ToString("yyyy/MM/dd")));
                        sb.Replace(":PRO_AMT", OracleDBUtil.SqlStr(dr["PRO_AMT"].ToString()));
                        sb.Replace(":STATUS_CODE", OracleDBUtil.SqlStr(dr["STATUS_CODE"].ToString()));
                        sb.Replace(":CUSTNAME", OracleDBUtil.SqlStr(dr["CUSTNAME"].ToString()));
                        sb.Replace(":CUSTTEL", OracleDBUtil.SqlStr(dr["CUSTTEL"].ToString()));
                        sb.Replace(":FIXDATE", OracleDBUtil.SqlStr(dr["FIXDATE"].ToString()));
                        sb.Replace(":ULSNO", OracleDBUtil.SqlStr(ulsno));
                        sb.Replace("[FIX_BRAND]", OracleDBUtil.SqlStr(dr["FIX_BRAND"].ToString())); //***20110317
                        sb.Replace("[PROD_TYPE]", OracleDBUtil.SqlStr(dr["PROD_TYPE"].ToString()));
                        sb.Replace("[BRAND]", OracleDBUtil.SqlStr(dr["BRAND"].ToString()));
                        sb.Replace("[MODEL]", OracleDBUtil.SqlStr(dr["MODEL"].ToString()));

                        OracleDBUtil.ExecuteSql(objTX, sb.ToString());
                    }
                }

                //step 4:覆查重覆的維修單
                OracleDBUtil.ExecuteSql(
                    objTX,
                    @"Update HRS_POSFIXI_TEMP 
                    set STATUS_CODE ='X'
                       where SREIALNO in (select SREIALNO from HRS_POSFIXI ) "
                    );

                objTX.Commit();
                con_log.Success("由HRS轉入HRS_POS_FIXI_TEMP的維修單資料筆數" + sourceDt.Rows.Count + "。");
                Console.WriteLine("5.由HRS轉入HRS_POS_FIXI_TEMP的維修單資料筆數" + sourceDt.Rows.Count);

                //step 5:開始處理HRS_POS_FIXI
                Console.WriteLine("6.開始處理HRS_POS_FIXI");

                objTX = conn.BeginTransaction();

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
                    objTX,
                    "SP_HRS_POSFIXI_DELETE",
                    outSTATUS,
                    outMESSAGE
                    );

                if (outSTATUS.Value.ToString() == "1")
                {
                    objTX.Commit();

                    con_log.Success(outMESSAGE.Value.ToString());
                    Console.WriteLine("6." + outMESSAGE.Value.ToString());
                    Thread.Sleep(3000);
                }
                else
                {
                    throw new Exception(outMESSAGE.Value.ToString());
                }
            }
            catch (Exception ex)
            {
                if(objTX != null)
                {
                    objTX.Rollback();
                }

                con_log.Fail(ex.Message);
                Console.WriteLine(ex.Message);
                Thread.Sleep(3000);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                if (conn_hrs.State == ConnectionState.Open) conn_hrs.Close();
                conn_hrs.Dispose();
                OracleConnection.ClearAllPools();
            }
        }        
    }
}
