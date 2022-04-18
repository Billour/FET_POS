using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using Advtek.Utility;

namespace PRODUCT_PRICE_IMPORT
{
    class Program
    {
        static void Main(string[] args)
        {
            try {
                //初始化LOG
                ConvertLog con_log = new ConvertLog("PRODUCT_PRICE_IMPORT");

                DataTable dt = new DataTable();

                try
                {
                    #region 取得所有的 PROD_PRICE
                    //string runningDate = "20101210";
                    //string runningDate2 = "20101208";
                    Console.WriteLine("取得所有的 PROD_PRICE");
                    string runningDate = DateTime.Now.ToString("yyyyMMdd");
                    string runningDate2 = DateTime.Now.AddDays(-2).ToString("yyyyMMdd");
                    //using (OracleConnection objConn = OracleDBUtil.GetConnection())
                    using (OracleConnection objConn = OracleDBUtil.GetERPPOSConnection())
                    {
                        #region 舊查詢
                        //                    dt = OracleDBUtil.GetDataSet(
                        //                        objConn,
                        //                        @"SELECT *
                        //                    FROM DM
                        //                    WHERE upddate = TO_CHAR (SYSDATE, 'yyyymmdd')
                        //                    Order by prodno, companycode"
                        //                        ).Tables[0];
                        //***
                        //dt = OracleDBUtil.GetDataSet(
                        //    objConn,
                        //    @" SELECT * " +
                        //    @" FROM DM T1 " +
                        //    @" WHERE T1.UPDDATE = TO_CHAR (TO_DATE('" + runningDate + "','YYYYMMDD'), 'YYYYMMDD') AND T1.COMPANYCODE='01' " +
                        //    @" AND  T1.MYDATETIME_PRICE=( " +
                        //    @"         SELECT MAX(T2.MYDATETIME_PRICE) " +
                        //    @"         FROM DM T2 " +
                        //    @"         WHERE T2.PRODNO = T1.PRODNO " +
                        //    @"           AND TO_CHAR (TO_DATE('" + runningDate + "','YYYYMMDD'), 'YYYYMMDD')  BETWEEN T2.B_DATE AND T2.E_DATE and T2.COMPANYCODE='01' " +
                        //    @"           AND ROWNUM =1 )" +
                        //    @" ORDER BY PRODNO, UPDDATE DESC "
                        //    ).Tables[0];
                        #endregion
                        try
                        {
                            dt = OracleDBUtil.GetDataSet(
                               objConn,
                               @" SELECT * " +
                               @" FROM DM T1 " +
                               @" WHERE T1.UPDDATE between TO_CHAR (TO_DATE('" + runningDate2 + "','YYYYMMDD'), 'YYYYMMDD') " +
                               @"   AND  TO_CHAR (TO_DATE('" + runningDate + "','YYYYMMDD'), 'YYYYMMDD') " +
                               @"   AND T1.COMPANYCODE in ('01','02') " +
                               @" ORDER BY PRODNO, UPDDATE DESC "
                               ).Tables[0];
                        }
                        catch (Exception ex) {
                            con_log.Fail(ex.Message);
                            Console.WriteLine(ex.Message);
                            //Console.ReadKey(true);
                        
                        }
                        finally
                        {
                            if (objConn.State == ConnectionState.Open) objConn.Close();
                            objConn.Dispose();
                            OracleConnection.ClearAllPools();
                        }
                    }
                    #endregion
                    string sMsg = "";
                    //if (dt.Rows.Count > 0)
                    //{
                        using (OracleConnection objConn = OracleDBUtil.GetConnection())
                        {
                            OracleTransaction objTX = null;
                            try
                            {
                                #region 將所有的 PROD_PRICE 寫入TEMP
                                Console.WriteLine("將所有的 PROD_PRICE 寫入TEMP");
                                string BATCH_NO = GuidNo.getUUID();
                                //***
                                StringBuilder sb = new StringBuilder();
                                //sb.Append(@"delete from PRODUCT_DM_TEMP where UPDDATE <> to_date('" + runningDate + "','YYYYMMDD') "); //**
                                sb.Append(@"delete from PRODUCT_DM_TEMP ");

                                OracleDBUtil.ExecuteSql(objConn, sb.ToString());
                                
                                foreach (DataRow dr in dt.Rows)
                                {
                                    sb.Length = 0;

                                    sb.AppendLine(
                                        @"INSERT INTO PRODUCT_DM_TEMP( 
                                        PRODUCT_DM_TEMP_ID, COMPANYCODE, B_DATE, 
                                        E_DATE, 
                                        PRODNO, PRICE, COST, UPDDATE, 
                                        UPDUSRNO, ULSNO, BATCH_NO ,
                                        MYDATETIME,MYDATETIME_PRICE
                                      )VALUES(
                                        SYS_GUID(), :COMPANYCODE, TO_DATE(:B_DATE, 'YYYYMMDD'), 
                                        TO_DATE(:E_DATE, 'YYYYMMDD'), 
                                        :PRODNO, :PRICE, :COST, TO_DATE(:UPDDATE, 'YYYYMMDD'), 
                                        :UPDUSRNO, :ULSNO , :BATCH_NO ,
                                        :MYDATETIME, :PK
                                      )"
                                    );

                                    sb.Replace(":COMPANYCODE", OracleDBUtil.SqlStr(dr["COMPANYCODE"].ToString()));
                                    sb.Replace(":B_DATE", OracleDBUtil.SqlStr(dr["B_DATE"].ToString()));
                                    sb.Replace(":E_DATE", OracleDBUtil.SqlStr(dr["E_DATE"].ToString()));
                                    //sb.Replace(":PRODNO", OracleDBUtil.SqlStr(dr["PRODNO"].ToString()));
                                    sb.Replace(":PRODNO", "'" + dr["PRODNO"].ToString() + "'");//20110221 PRODNO不去隱碼，避免重複資料產生
                                    sb.Replace(":PRICE", OracleDBUtil.SqlStr(dr["PRICE"].ToString()));
                                    sb.Replace(":COST", OracleDBUtil.SqlStr(dr["COST"].ToString()));
                                    sb.Replace(":UPDDATE", OracleDBUtil.SqlStr(dr["UPDDATE"].ToString()));
                                    sb.Replace(":UPDUSRNO", OracleDBUtil.SqlStr(dr["UPDUSRNO"].ToString()));
                                    sb.Replace(":ULSNO", OracleDBUtil.SqlStr(dr["ULSNO"].ToString()));
                                    sb.Replace(":BATCH_NO", OracleDBUtil.SqlStr(BATCH_NO));
                                    sb.Replace(":MYDATETIME", OracleDBUtil.SqlStr((dr["MYDATETIME"] == DBNull.Value) ? "" : dr["MYDATETIME"].ToString()));
                                    sb.Replace(":PK", OracleDBUtil.SqlStr(dr["MYDATETIME_PRICE"].ToString()));

                                    OracleDBUtil.ExecuteSql(objConn, sb.ToString());
                                }

                                #endregion

                                #region 由SP_PROD_PRICE_IMPORT做新增修改
                                Console.WriteLine("由SP_PROD_PRICE_IMPORT做新增修改");
                                objTX = objConn.BeginTransaction();
                                OracleParameter op = new OracleParameter("outMessage", OracleType.VarChar);
                                op.Size = 2000;
                                op.Direction = ParameterDirection.Output;
                                OracleDBUtil.ExecuteSql_SP(
                                    objTX
                                    , "SP_PROD_PRICE_IMPORT"
                                    , new OracleParameter("I_BATCH_NO", BATCH_NO)
                                    , op
                                    );

                                objTX.Commit();
                                sMsg = op.Value.ToString();
                                #endregion
                            }
                            catch (Exception ex)
                            {
                                objTX.Rollback();
                                Console.WriteLine(ex.Message);
                                con_log.Fail(ex.Message);
                                //Console.ReadKey(true);
                            }
                            finally
                            {
                                objTX.Dispose();
                                if (objConn.State == ConnectionState.Open) objConn.Close();
                                objConn.Dispose();
                                OracleConnection.ClearAllPools();
                            }
                        }
                    //}
                    //else {
                        if (dt.Rows.Count == 0) sMsg += "DM(POS)來源無資料";
                    //    Console.WriteLine("DM(POS)來源無資料");
                    //    sMsg += "DM(POS)來源無資料";
                    //}

                    //成功資訊
                    con_log.Success(sMsg);
                }
                catch (Exception ex)
                {
                    //失敗資訊
                    con_log.Fail(ex.Message);
                    Console.WriteLine(ex.Message);
                    //Console.ReadKey(true);
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                //Console.ReadKey(true);
            }
            
        }
    }
}
