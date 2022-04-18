using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;

namespace PRODUCT_TYPE_IMPORT
{
    class Program
    {
        private static OracleConnection objConn;
        private static OracleConnection objConn2;
        private static OracleTransaction objTX2;
        static void Main(string[] args)
        {
            
            try
            {

                //初始化LOG
                Console.WriteLine("PRODUCT_TYPE_IMPORT");
                Console.WriteLine("初始化LOG");
                ConvertLog con_log = new ConvertLog("PRODUCT_TYPE_IMPORT");

                try
                {
                    Console.WriteLine("PRODTYPE(ERP)->PRODTYPE_TEMP(WEB);SP_SCH08");
                    string sMsg = ConvertProductType();

                    //成功資訊
                    Console.WriteLine("執行成功,寫入log");
                    con_log.Success(sMsg);
                }
                catch (Exception ex)
                {
                    //失敗資訊
                    Console.WriteLine(ex.Message);
                    con_log.Fail(ex.Message);
                    //Console.ReadKey(true);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Console.ReadKey(true);
            }

        }

        public static string ConvertProductType()
        {
           
            DataTable dt = new DataTable();
            string sRet = "";
            try
            {
                #region 取得所有的PRODTYPE
                
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("SELECT * ");
                sb.AppendLine("FROM PRODTYPE where companycode in ('01','02') ");

                objConn = OracleDBUtil.GetERPPOSConnection();
                objConn2 = OracleDBUtil.GetConnection();
                objTX2 = objConn2.BeginTransaction();
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                #endregion

                if (dt.Rows.Count > 0)
                {
                    Console.WriteLine("清除PRODTYPE_TEMP(WEB)");
                    OracleDBUtil.ExecuteSql(objTX2, "DELETE PRODTYPE_TEMP");

                    foreach (DataRow dr in dt.Rows) {
                        sb.Length = 0;
                        sb.AppendLine(
                                @"INSERT INTO PRODTYPE_TEMP( 
                            PRODTYPENO, COMPANYCODE, PRODTYPENAME, 
                            UPDDATE, UPDUSRNO, ULSNO
                          )VALUES(
                            :PRODTYPENO, :COMPANYCODE, :PRODTYPENAME, 
                            :UPDDATE, :UPDUSRNO, :ULSNO
                          )"
                        );
                        sb.Replace(":PRODTYPENO", OracleDBUtil.SqlStr(dr["PRODTYPENO"].ToString()));
                        sb.Replace(":COMPANYCODE", OracleDBUtil.SqlStr(dr["COMPANYCODE"].ToString()));
                        sb.Replace(":PRODTYPENAME", OracleDBUtil.SqlStr(dr["PRODTYPENAME"].ToString()));
                        sb.Replace(":UPDDATE", OracleDBUtil.SqlStr(dr["UPDDATE"].ToString()));
                        sb.Replace(":UPDUSRNO", OracleDBUtil.SqlStr(dr["UPDUSRNO"].ToString()));
                        sb.Replace(":ULSNO", OracleDBUtil.SqlStr(dr["ULSNO"].ToString()));
                        OracleDBUtil.ExecuteSql(objTX2, sb.ToString());  
                    
                    }
                    

                    #region 由SP_SCH08做新增修改

                    OracleParameter op = new OracleParameter("outMessage", OracleType.VarChar, 2000);
                    op.Direction = ParameterDirection.Output;
                    OracleDBUtil.ExecuteSql_SP(
                        objTX2
                        , "SP_SCH08", op
                        );

                    objTX2.Commit();
                    sRet = op.Value.ToString();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                objTX2.Rollback();
                throw ex;
            }
            finally
            {
                objTX2.Dispose();
                if (objConn2.State == ConnectionState.Open) objConn2.Close();
                objConn2.Dispose();

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();

                OracleConnection.ClearAllPools();
            }
            return sRet;
        }

        #region 備份20110117
        //static void Main(string[] args)
        //{
        //    try {

        //        //初始化LOG
        //        Console.WriteLine("PRODUCT_TYPE_IMPORT");
        //        Console.WriteLine("初始化LOG");
        //        ConvertLog con_log = new ConvertLog("PRODUCT_TYPE_IMPORT");

        //        try
        //        {
        //            Console.WriteLine("PRODTYPE(ERP)->PRODTYPE_TEMP(WEB);SP_SCH08");
        //            string sMsg = new SCH08_Facade().ConvertProductType();

        //            //成功資訊
        //            Console.WriteLine("執行成功,寫入log");
        //            con_log.Success(sMsg);
        //        }
        //        catch (Exception ex)
        //        {
        //            //失敗資訊
        //            Console.WriteLine(ex.Message);
        //            con_log.Fail(ex.Message);
        //            Console.ReadKey(true);
        //        }

        //    }
        //    catch (Exception ex) {
        //        Console.WriteLine(ex.Message);
        //        Console.ReadKey(true);
        //    }

        //}
        #endregion
    }
}
