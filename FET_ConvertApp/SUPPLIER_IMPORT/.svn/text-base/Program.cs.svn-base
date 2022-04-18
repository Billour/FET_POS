using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using Advtek.Utility;

namespace SUPPLIER_IMPORT
{
    class Program
    {
        static void Main(string[] args)
        {
            try {
                //初始化LOG
                Console.WriteLine("SUPPLIER_IMPORT");
                Console.WriteLine("初始化LOG");
                ConvertLog con_log = new ConvertLog("SUPPLIER_IMPORT");

                DataTable dt = new DataTable();
                OracleConnection objConn = null;
                OracleTransaction objTX = null;
                OracleConnection objConn_erp = null;
                try
                {
                    Console.WriteLine("建立新舊DB連線");
                    objConn = OracleDBUtil.GetConnection();
                    objTX = objTX = objConn.BeginTransaction();
                    objConn_erp = OracleDBUtil.GetERPPOSConnection();

                    Console.WriteLine("查詢ERP.SUPPLIER");
                    dt = OracleDBUtil.GetDataSet(
                        objConn_erp,
                        @"SELECT  *
                    FROM SUPPLIER
                    WHERE COMPANYCODE='01'or COMPANYCODE='02'").Tables[0]; //COMPANYCODE='02' 增加，但SP中只處理01資料

                    if (dt.Rows.Count > 0)
                    {
                        Console.WriteLine("清除SUPPLIER_TEMP(WEB)");
                        OracleDBUtil.ExecuteSql(objTX, "DELETE SUPPLIER_TEMP");

                        #region insert temp
                        Console.WriteLine("寫入SUPPLIER_TEMP");
                        foreach (DataRow dr in dt.Rows)
                        {
                            StringBuilder sb = new StringBuilder();

                            sb.AppendLine(
                                @"Insert into SUPPLIER_TEMP
                       (SUPPNO, 
                        SUPPNAME, UNINO, TAXNO, TEL, BOSSNAME, 
                        FAX, CONTNAME, CONTTEL, EMAIL, ADDR, 
                        BANKNO, MEMO, CREATE_USER, CREATE_DTM, MODI_USER, 
                        MODU_DTM ,COMPANYCODE)
                        Values
                       (:SUPPNO, 
                        :SUPPNAME, :UNINO, :TAXNO, :TEL, :BOSSNAME, 
                        :FAX, :CONTNAME, :CONTTEL, :EMAIL, :ADDR, 
                        :BANKNO, :MEMO, 'Convert', SYSDATE, 'Convert', 
                        SYSDATE ,:COMPANYCODE)");

                            sb.Replace(":SUPPNO", OracleDBUtil.SqlStr(dr["SUPPNO"].ToString()));
                            sb.Replace(":SUPPNAME", OracleDBUtil.SqlStr(dr["SUPPNAME"].ToString()));
                            sb.Replace(":UNINO", OracleDBUtil.SqlStr(dr["UNINO"].ToString()));
                            sb.Replace(":TAXNO", OracleDBUtil.SqlStr(dr["TAXNO"].ToString()));
                            sb.Replace(":TEL", OracleDBUtil.SqlStr(dr["TEL"].ToString()));
                            sb.Replace(":BOSSNAME", OracleDBUtil.SqlStr(dr["BOSSNAME"].ToString()));
                            sb.Replace(":FAX", OracleDBUtil.SqlStr(dr["FAX"].ToString()));
                            sb.Replace(":CONTNAME", OracleDBUtil.SqlStr(dr["CONTNAME"].ToString()));
                            sb.Replace(":CONTTEL", OracleDBUtil.SqlStr(dr["CONTTEL"].ToString()));
                            sb.Replace(":EMAIL", OracleDBUtil.SqlStr(dr["EMAIL"].ToString()));
                            sb.Replace(":ADDR", OracleDBUtil.SqlStr(dr["ADDR"].ToString()));
                            sb.Replace(":BANKNO", OracleDBUtil.SqlStr(dr["BANKNO"].ToString()));
                            sb.Replace(":MEMO", OracleDBUtil.SqlStr(dr["MEMO"].ToString()));
                            sb.Replace(":COMPANYCODE", OracleDBUtil.SqlStr(dr["COMPANYCODE"].ToString()));

                            OracleDBUtil.ExecuteSql(objTX, sb.ToString());
                        }

                        #endregion
                        string sMsg = "";
                        Console.WriteLine("SP_SUPPLIER_IMPORT");
                        OracleParameter op = new OracleParameter("outMessage", OracleType.VarChar, 2000);
                        op.Direction = ParameterDirection.Output;
                        OracleDBUtil.ExecuteSql_SP(
                        objTX,
                        "SP_SUPPLIER_IMPORT", op
                        );

                        objTX.Commit();
                        Console.WriteLine("執行成功,寫入log");
                        //成功資訊
                        con_log.Success(op.Value.ToString());
                    }
                    else {
                        con_log.Success("SUPPLIER(ERP)來源無資料");
                        Console.WriteLine("SUPPLIER(ERP)來源無資料");
                        myPause();
                    }
                }
                catch (Exception ex)
                {
                    objTX.Rollback();
                    Console.WriteLine(ex.Message);
                    //失敗資訊
                    con_log.Fail(ex.Message);
                    myPause();
                   
                }
                finally
                {
                    objConn.Dispose();
                    objConn_erp.Dispose();
                }
            
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                myPause();
            }
            
        }

        static void myPause()
        {
            Console.WriteLine("請按任意鍵繼續...");
            //Console.ReadKey(true);
        }
    }
}
