using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
//using FET.POS.Model.DTO;
//using FET.POS.Model.Facade;
//using FET.POS.Model.Facade.ConvertApp;
//using FET.POS.Model.DTO.ConvertApp;
//using FET.POS.Model.Common;

namespace FETT_DROPSHIPITEM_ONBOARD_TRANSFER
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable dt = new DataTable();
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            OracleConnection objConn_erp = null;
            try
            {

                //初始化LOG
                Console.WriteLine("DROPSHIPITEM_ONBOARD");
                Console.WriteLine("初始化LOG");
                ConvertLog con_log = new ConvertLog("DROPSHIPITEM_ONBOARD");
                try
                {
                    Console.WriteLine("建立新舊DB連線");
                    objConn = OracleDBUtil.GetConnection();
                    objTX = objTX = objConn.BeginTransaction();
                    objConn_erp = OracleDBUtil.GetERPPOSConnection();

                    //EFP SOURCE TABLE > WEB TEMP TABLE
                    Console.WriteLine("EFP SOURCE TABLE > WEB TEMP TABLE");

                    dt = Query_OLD_FETT_DROPSHIPITEM_ONBOARD(objConn_erp);
                    if (dt.Rows.Count > 0)
                    {
                        Console.WriteLine("清除FETT_DROPSHIPITEM_ONBOARD_TEMP(WEB)");
                        OracleDBUtil.ExecuteSql(objTX, "DELETE FETT_DROPSHIPITEM_ONBOARD_TEMP");

                        Console.WriteLine("寫入FETT_DROPSHIPITEM_ONBOARD_TEMP");
                        foreach (DataRow dr in dt.Rows)
                        {
                            StringBuilder sb = new StringBuilder();

                            sb.AppendLine(
                                @"Insert into FETT_DROPSHIPITEM_ONBOARD_TEMP
                                  (SEGMENT1, D_ONBOARD)
                                  Values
                                  (:SEGMENT1, :D_ONBOARD)");

                            sb.Replace(":SEGMENT1", OracleDBUtil.SqlStr(dr["SEGMENT1"].ToString()));
                            sb.Replace(":D_ONBOARD", OracleDBUtil.SqlStr(dr["D_ONBOARD"].ToString()));
                            OracleDBUtil.ExecuteSql(objTX, sb.ToString());
                        }

                        ////WEB TEMP TABLE > WEB TARGET TABLE
                        Console.WriteLine("WEB TEMP TABLE > WEB TARGET TABLE");
                        string sMsg = PK_CONVERT_SP_DROPSHIPITEM_ONBOARD(objConn, objTX);

                        //成功資訊
                        Console.WriteLine("執行成功");
                        Console.WriteLine(sMsg);
                        con_log.Success(sMsg);
                    }
                    else {
                        Console.WriteLine("DROPSHIPITEM_ONBOARD(POS)無資料");
                        con_log.Success("DROPSHIPITEM_ONBOARD(POS)無資料");
                    }
                   
                    Console.WriteLine("執行成功寫入log");
                }
                catch (Exception ex)
                {
                    //失敗資訊
                    Console.WriteLine(ex.Message);
                    con_log.Fail(ex.Message);
                    //Console.ReadKey(true);
                    throw ex;
                }
                finally
                {
                    if (objTX != null) objTX.Dispose();
                    if (objConn != null)
                    {
                        if (objConn.State == ConnectionState.Open) objConn.Close();
                        objConn.Dispose();
                    }

                    if (objConn_erp != null)
                    {
                        if (objConn_erp.State == ConnectionState.Open) objConn_erp.Close();
                        objConn_erp.Dispose();
                    }
                    OracleConnection.ClearAllPools();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Console.ReadKey(true);
            }
            
        }

        public static DataTable Query_OLD_FETT_DROPSHIPITEM_ONBOARD(OracleConnection con)
        {
            try
            {
                //OLD POS

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Length = 0;
                sb.Append("SELECT SEGMENT1, D_ONBOARD  ");
                sb.Append("FROM FETT_DROPSHIPITEM_ONBOARD  ");

                DataTable dt = OracleDBUtil.GetDataSet(con, sb.ToString()).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string PK_CONVERT_SP_DROPSHIPITEM_ONBOARD(OracleConnection con,OracleTransaction tx)
        {
            string sRet = "";
            try
            {
                OracleCommand oraCmd = new OracleCommand("PK_CONVERT.SP_DROPSHIPITEM_ONBOARD");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("outMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Connection = con;
                oraCmd.Transaction = tx;
                oraCmd.ExecuteNonQuery();
                sRet = oraCmd.Parameters["outMESSAGE"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sRet;
        }


        #region 備份20110117
        //static void Main(string[] args)
        //{
        //    try
        //    {

        //        //初始化LOG
        //        Console.WriteLine("DROPSHIPITEM_ONBOARD");
        //        Console.WriteLine("初始化LOG");
        //        ConvertLog con_log = new ConvertLog("DROPSHIPITEM_ONBOARD");

        //        try
        //        {
        //            //EFP SOURCE TABLE > WEB TEMP TABLE
        //            Console.WriteLine("EFP SOURCE TABLE > WEB TEMP TABLE");
        //            FETT_DROPSHIPITEM_ONBOARD_TRANSFER_Facade cFacade = new FETT_DROPSHIPITEM_ONBOARD_TRANSFER_Facade();
        //            DataTable dtAdd = cFacade.Query_OLD_FETT_DROPSHIPITEM_ONBOARD();
        //            dtAdd.TableName = "FETT_DROPSHIPITEM_ONBOARD_TEMP";
        //            cFacade.Insert_DROPSHIPITEM_ONBOARD(dtAdd);

        //            //WEB TEMP TABLE > WEB TARGET TABLE
        //            Console.WriteLine("WEB TEMP TABLE > WEB TARGET TABLE");
        //            string sMsg = cFacade.PK_CONVERT_SP_DROPSHIPITEM_ONBOARD();

        //            //成功資訊
        //            Console.WriteLine("執行成功");
        //            Console.WriteLine(sMsg);
        //            con_log.Success(sMsg);
        //            Console.WriteLine("執行成功寫入log");
        //        }
        //        catch (Exception ex)
        //        {
        //            //失敗資訊
        //            Console.WriteLine(ex.Message);
        //            con_log.Fail(ex.Message);
        //            Console.ReadKey(true);
        //            //throw ex;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        Console.ReadKey(true);
        //    }
        //}
        #endregion
    }
}
