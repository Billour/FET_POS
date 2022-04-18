using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using System.Threading;

namespace MM_UPLOAD
{
    class Program
    {
        static string sMsg;
        static void Main(string[] args)
        {
            sMsg = "";
            //初始化LOG
            Console.WriteLine("MM_UPLOAD");
            Console.WriteLine("1.初始化LOG");
            ConvertLog con_log = new ConvertLog("MM_UPLOAD");

            try
            {
                //GET NEW POS DATA
                Console.WriteLine("2.查詢MM(WEB)");
                DataTable dt_MM = Query_MM();
                sMsg += "MM(WEB):查詢筆數:" + dt_MM.Rows.Count.ToString() + "\n";

                Console.WriteLine("3.查詢MM_PLU(WEB)");
                DataTable dt_MM_PLU = Query_MM_PLU();
                sMsg += "MM_PLU(WEB):查詢筆數:" + dt_MM_PLU.Rows.Count.ToString() + "\n";

                Insert(dt_MM, dt_MM_PLU);

                //成功資訊
                Console.WriteLine("4.執行結束，寫入LOG");
                con_log.Success(sMsg);
                Thread.Sleep(10000);
            }
            catch (Exception ex)
            {
                //失敗資訊
                Console.WriteLine("例外產生");
                con_log.Fail( ex.Message);
                Console.WriteLine(ex.Message);
                Thread.Sleep(10000);
            }
        }

        public static void Insert(DataTable dtAdd, DataTable dtAdd2)
        {
            OracleConnection objConnOld = null;
            OracleTransaction objTXOld = null;

            try
            {
                objConnOld = OracleDBUtil.GetERPPOSConnection();//OLD

                objTXOld = objConnOld.BeginTransaction();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                if (dtAdd2.Rows.Count > 0)
                {
                    OracleDBUtil.ExecuteSql(
                        objTXOld,
                        "DELETE FROM MM_PLU"
                    );
                }

                if (dtAdd.Rows.Count > 0)
                {
                    OracleDBUtil.ExecuteSql(
                        objTXOld,
                        "DELETE FROM MM"
                    );
                }

                if (dtAdd.Rows.Count > 0)
                {
                    //OLD POS
                    foreach (DataRow dr in dtAdd.Rows)
                    {
                        sb.Length = 0;
                        sb.AppendLine(
                            @"Insert into MM
                               (PROMOTENO, 
                                COMPANYCODE, 
                                PROMOTENAME, 
                                B_DATE, 
                                E_DATE, 
                                MM_TYPE, 
                                MAX_CNT, 
                                GROUP_1_QTY, 
                                GROUP_2_QTY, 
                                GROUP_3_QTY, 
                                GROUP_4_QTY, 
                                GROUP_5_QTY, 
                                GROUP_6_QTY, 
                                PACK_1_QTY, 
                                PACK_1_DISCAMT, 
                                PACK_2_QTY, 
                                PACK_2_DISCAMT, 
                                PACK_3_QTY, 
                                PACK_3_DISCAMT, 
                                PACK_4_QTY, 
                                PACK_4_DISCAMT, 
                                PACK_5_QTY, 
                                PACK_5_DISCAMT, 
                                PACK_6_QTY, 
                                PACK_6_DISCAMT, 
                                PACK_7_QTY, 
                                PACK_7_DISCAMT, 
                                PACK_8_QTY, 
                                PACK_8_DISCAMT, 
                                TERMPAY_FLAG, 
                                TERMPAY_RATE, 
                                TERMPAY_BANK, 
                                TERMPAY_TERM, 
                                UPDDATE, 
                                UPDUSRNO, 
                                MM_AMOUNT, 
                                MM_NO, 
                                AMT_TYPE, 
                                LOYALTY, 
                                ULSNO, 
                                PREDOWNLOADDATE, 
                                S_GROUP_NO, 
                                ISSETTLE
                               )
                             Values
                               ({PROMOTENO} /*PROMOTENO*/, 
                                '01' /*COMPANYCODE*/, 
                                {PROMOTENAME} /*PROMOTENAME*/, 
                                {B_DATE} /*B_DATE*/, 
                                {E_DATE} /*E_DATE*/, 
                                'B' /*MM_TYPE*/, 
                                0 /*MAX_CNT*/, 
                                {GROUP_1_QTY} /*GROUP_1_QTY*/, 
                                {GROUP_2_QTY} /*GROUP_2_QTY*/, 
                                {GROUP_3_QTY} /*GROUP_3_QTY*/, 
                                {GROUP_4_QTY} /*GROUP_4_QTY*/, 
                                {GROUP_5_QTY} /*GROUP_5_QTY*/, 
                                {GROUP_6_QTY} /*GROUP_6_QTY*/, 
                                0 /*PACK_1_QTY*/, 
                                0 /*PACK_1_DISCAMT*/, 
                                0 /*PACK_2_QTY*/, 
                                0 /*PACK_2_DISCAMT*/, 
                                0 /*PACK_3_QTY*/, 
                                0 /*PACK_3_DISCAMT*/, 
                                0 /*PACK_4_QTY*/, 
                                0 /*PACK_4_DISCAMT*/, 
                                0 /*PACK_5_QTY*/, 
                                0 /*PACK_5_DISCAMT*/, 
                                0 /*PACK_6_QTY*/, 
                                0 /*PACK_6_DISCAMT*/, 
                                0 /*PACK_7_QTY*/, 
                                0 /*PACK_7_DISCAMT*/, 
                                0 /*PACK_8_QTY*/, 
                                0 /*PACK_8_DISCAMT*/, 
                                NULL /*TERMPAY_FLAG*/, 
                                0 /*TERMPAY_RATE*/, 
                                NULL /*TERMPAY_BANK*/, 
                                NULL /*TERMPAY_TERM*/, 
                                {UPDDATE} /*UPDDATE*/, 
                                {UPDUSRNO} /*UPDUSRNO*/, 
                                0 /*MM_AMOUNT*/, 
                                NULL /*MM_NO*/, 
                                NULL /*AMT_TYPE*/, 
                                {LOYALTY} /*LOYALTY*/, 
                                NULL /*ULSNO*/, 
                                NULL /*PREDOWNLOADDATE*/, 
                                NULL /*S_GROUP_NO*/, 
                                '0' /*ISSETTLE*/)"
                            );

                        sb.Replace("{PROMOTENO}", OracleDBUtil.SqlStr(dr["PROMO_NO"].ToString()));
                        sb.Replace("{PROMOTENAME}", OracleDBUtil.SqlStr(dr["PROMO_NAME"].ToString()));
                        sb.Replace("{B_DATE}", OracleDBUtil.SqlStr(Convert.ToDateTime(dr["B_DATE"]).ToString("yyyyMMdd")));
                        sb.Replace("{E_DATE}", OracleDBUtil.SqlStr(Convert.ToDateTime(dr["E_DATE"]).ToString("yyyyMMdd")));
                        sb.Replace("{GROUP_1_QTY}", OracleDBUtil.SqlStr(dr["GROUP_1_QTY"].ToString()));
                        sb.Replace("{GROUP_2_QTY}", OracleDBUtil.SqlStr(dr["GROUP_2_QTY"].ToString()));
                        sb.Replace("{GROUP_3_QTY}", OracleDBUtil.SqlStr(dr["GROUP_3_QTY"].ToString()));
                        sb.Replace("{GROUP_4_QTY}", OracleDBUtil.SqlStr(dr["GROUP_4_QTY"].ToString()));
                        sb.Replace("{GROUP_5_QTY}", OracleDBUtil.SqlStr(dr["GROUP_5_QTY"].ToString()));
                        sb.Replace("{GROUP_6_QTY}", OracleDBUtil.SqlStr(dr["GROUP_6_QTY"].ToString()));
                        sb.Replace("{UPDDATE}", dr["MODI_DTM"] == DBNull.Value ? "NULL" : OracleDBUtil.SqlStr(Convert.ToDateTime(dr["MODI_DTM"]).ToString("yyyyMMdd")));
                        sb.Replace("{UPDUSRNO}", OracleDBUtil.SqlStr(dr["MODI_USER"].ToString()));
                        sb.Replace("{LOYALTY}", dr["IA_LOYALTY_FLAG"].ToString() == "L1" ? "'1'" : "'0'");

                        OracleDBUtil.ExecuteSql(objTXOld, sb.ToString());
                    }
                }

                if (dtAdd2.Rows.Count > 0)
                {
                    //OLD POS
                    foreach (DataRow dr in dtAdd2.Rows)
                    {
                        sb.Length = 0;
                        sb.AppendLine(
                            @"Insert into MM_PLU
                               (PROMOTENO, 
                                PRODNO, 
                                COMPANYCODE, 
                                GROUP_NO, 
                                AMOUNT, 
                                UPDDATE, 
                                UPDUSRNO, 
                                MM_NO
                               )
                             Values
                               ({PROMOTENO} /*PROMOTENO*/, 
                                {PRODNO} /*PRODNO*/, 
                                '01' /*COMPANYCODE*/, 
                                {GROUP_NO} /*GROUP_NO*/, 
                                {AMOUNT} /*AMOUNT*/, 
                                {UPDDATE} /*UPDDATE*/, 
                                {UPDUSRNO} /*UPDUSRNO*/, 
                                NULL /*MM_NO*/)"
                        );
                        sb.Replace("{PROMOTENO}", OracleDBUtil.SqlStr(dr["PROMO_NO"].ToString()));
                        sb.Replace("{PRODNO}", OracleDBUtil.SqlStr(dr["PROD_NO"].ToString()));
                        sb.Replace("{GROUP_NO}", OracleDBUtil.SqlStr(dr["PROMO_PROD_GROUP"].ToString()));
                        sb.Replace("{AMOUNT}", OracleDBUtil.SqlStr(dr["AMOUNT"].ToString()));
                        sb.Replace("{UPDDATE}", dr["MODI_DTM"] == DBNull.Value ? "NULL" : OracleDBUtil.SqlStr(Convert.ToDateTime(dr["MODI_DTM"]).ToString("yyyyMMdd")));
                        sb.Replace("{UPDUSRNO}", OracleDBUtil.SqlStr(dr["MODI_USER"].ToString()));

                        OracleDBUtil.ExecuteSql(objTXOld, sb.ToString());
                    }
                }

                objTXOld.Commit();

                sMsg += "MM_PLU(ERP):新增筆數:" + dtAdd2.Rows.Count.ToString() + "\n";                
            }
            catch (Exception ex)
            {
                if (objTXOld != null) objTXOld.Rollback();
                sMsg += "UpdateAndInsert_Store(DataTable dtAdd)執行發生錯誤\n";
                throw ex;
            }
            finally
            {
                if (objConnOld.State == ConnectionState.Open) objConnOld.Close();
                objConnOld.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public static DataTable Query_MM()
        {
            OracleConnection oCon = null;
            try
            {
                oCon = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Length = 0;
                sb.Append(@"SELECT * FROM MM WHERE B_DATE IS NOT NULL AND E_DATE IS NOT NULL AND PROMO_STATUS='1' ");

                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];

                return dt;
            }
            catch (Exception ex)
            {
                sMsg += "Query_MM()執行發生錯誤\n";
                throw ex;
            }
            finally
            {
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public static DataTable Query_MM_PLU()
        {
            OracleConnection oCon = null;
            try
            {
                oCon = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Length = 0;
                sb.Append(@"SELECT * FROM MM_PLU ");

                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];

                return dt;
            }
            catch (Exception ex)
            {
                sMsg += "Query_MM_PLU()執行發生錯誤\n";
                throw ex;
            }
            finally
            {
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }
    }
}
