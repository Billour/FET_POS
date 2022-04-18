using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.OracleClient; 
using Advtek.Utility;

namespace PRODUCT_IMPORT
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
                Console.WriteLine("PRODUCT_IMPORT");
                Console.WriteLine("初始化LOG");
                ConvertLog con_log = new ConvertLog("PRODUCT_IMPORT");
                try
                {
                    Console.WriteLine("建立新舊DB連線");
                    objConn = OracleDBUtil.GetConnection();
                    objTX = objTX = objConn.BeginTransaction();
                    objConn_erp = OracleDBUtil.GetERPPOSConnection();

                    //EFP SOURCE TABLE > WEB TEMP TABLE
                    Console.WriteLine("EFP SOURCE TABLE > WEB TEMP TABLE");

                    dt = Query_OLD_Product(objConn_erp);
                    if (dt.Rows.Count > 0)
                    {
                        Console.WriteLine("清除PRODUCT_TEMP(WEB)");
                        OracleDBUtil.ExecuteSql(objTX, "DELETE PRODUCT_TEMP");
                        StringBuilder sb = new StringBuilder();
                        Console.WriteLine("寫入PRODUCT_TEMP");
                        foreach (DataRow dr in dt.Rows)
                        {

                            sb.Length = 0;
                            sb.AppendLine(
                                @"Insert into PRODUCT_TEMP
                                  (PRODNO, PRODNAME, UNIT, 
                                   CEASEDATE, ISKEY, TOSO, 
                                   ISCONSIGNMENT, PRICE, PRICEDATE, 
                                   BARCODE1, COST, BARCODE2, 
                                   COSTDATE, BARCODE3, BARCODE4, 
                                   ULSNO, STATUS, ACCOUNTCODE, 
                                   ISSTOCK,IMEI_FLAG,  PRODTYPENO, 
                                   CREATE_USER, MODI_USER, MODI_DTM, 
                                   CREATE_DTM, ERP_ATTRIBUTE_1, ERP_ATTRIBUTE_2, 
                                   IS_DISCOUNT,
                                   S_DATE, E_DATE, COMPANYCODE )
                                  Values
                                  (:PRODNO, :PRODNAME, :UNIT, 
                                   :CEASEDATE, :ISKEY, :TOSO, 
                                   :ISCONSIGNMENT, :PRICE1, :PRICEDATE, 
                                   :BARCODE1, :COST1, :BARCODE2, 
                                   :COSTDATE, :BARCODE3, :BARCODE4, 
                                   :ULSNO, :STATUS, :ACCOUNTCODE, 
                                   :ISSTOCK, :IMEI_FLAG,:PRODTYPENO, 
                                   :CREATE_USER, :MODI_USER, :MODI_DTM, 
                                   :CREATE_DTM, :ERP_ATTRIBUTE_1, :ERP_ATTRIBUTE_2, 
                                   :IS_DISCOUNT,
                                   :S_DATE, :E_DATE, :COMPANYCODE )");

                            sb.Replace(":ACCOUNTCODE", OracleDBUtil.SqlStr(dr["ACCOUNTCODE"].ToString()));
                            sb.Replace(":ERP_ATTRIBUTE_1", OracleDBUtil.SqlStr(dr["ATTRIBUTE1"].ToString()));
                            sb.Replace(":ERP_ATTRIBUTE_2", OracleDBUtil.SqlStr(dr["ATTRIBUTE2"].ToString()));
                            sb.Replace(":BARCODE1", OracleDBUtil.SqlStr(dr["BARCODE1"].ToString()));
                            sb.Replace(":BARCODE2", OracleDBUtil.SqlStr(dr["BARCODE2"].ToString()));
                            sb.Replace(":BARCODE3", OracleDBUtil.SqlStr(dr["BARCODE3"].ToString()));
                            sb.Replace(":BARCODE4", OracleDBUtil.SqlStr(dr["BARCODE4"].ToString()));
                            sb.Replace(":CEASEDATE", OracleDBUtil.SqlStr(dr["CEASEDATE"].ToString()));
                            sb.Replace(":COMPANYCODE", OracleDBUtil.SqlStr(dr["COMPANYCODE"].ToString()));
                            sb.Replace(":COST1", OracleDBUtil.SqlStr(dr["COST"].ToString()));
                            sb.Replace(":ISCONSIGNMENT", OracleDBUtil.SqlStr(dr["ISCONSIGNMENT"].ToString()));
                            sb.Replace(":ISKEY", OracleDBUtil.SqlStr(dr["ISKEY"].ToString()));
                            sb.Replace(":ISSTOCK", OracleDBUtil.SqlStr(dr["ISSTOCK"].ToString()));
                            sb.Replace(":PRICE1", OracleDBUtil.SqlStr(dr["PRICE"].ToString()));
                            sb.Replace(":PRICEDATE", OracleDBUtil.SqlStr(dr["PRICEDATE"].ToString()));
                            sb.Replace(":PRODNAME", OracleDBUtil.SqlStr(dr["PRODNAME"].ToString()));
                            //sb.Replace(":PRODNO", OracleDBUtil.SqlStr(dr["PRODNO"].ToString()));
                            sb.Replace(":PRODNO", "'" + dr["PRODNO"].ToString() + "'"); //20110221 PRODNO不去隱碼，避免重複資料產生
                            sb.Replace(":PRODTYPENO", OracleDBUtil.SqlStr(dr["PRODTYPENO"].ToString()));
                            sb.Replace(":STATUS", OracleDBUtil.SqlStr(dr["STATUS"].ToString()));
                            sb.Replace(":TOSO", OracleDBUtil.SqlStr(dr["TOSO"].ToString()));
                            sb.Replace(":ULSNO", OracleDBUtil.SqlStr(dr["ULSNO"].ToString()));
                            sb.Replace(":UNIT", OracleDBUtil.SqlStr(dr["UNIT"].ToString()));
                            sb.Replace(":COSTDATE", OracleDBUtil.SqlStr(dr["COSTDATE"].ToString()));
                            sb.Replace(":IMEI_FLAG", OracleDBUtil.SqlStr(dr["IMEI_FLAG"].ToString()));
                            
                            string sDate = dr["CREATEDATE"].ToString();
                            sb.Replace(":S_DATE", "to_date(" + OracleDBUtil.SqlStr(sDate) + ",'YYYYMMDD')");
                            sb.Replace(":CREATE_DTM", "to_date(" + OracleDBUtil.SqlStr(sDate) + ",'YYYYMMDD')");

                            if (dr["CEASEDATE"] != DBNull.Value) {
                                sDate = dr["CEASEDATE"].ToString();
                                sb.Replace(":E_DATE", "to_date(" + OracleDBUtil.SqlStr(sDate) + ",'YYYYMMDD')");
                            } else {
                                sb.Replace(":E_DATE", "null");
                            }
                            sb.Replace(":CREATE_USER", "'SYSTEM'");
                            sb.Replace(":MODI_USER", "'SYSTEM'");
                            sb.Replace(":IS_DISCOUNT", "'N'");
                            sDate = dr["UPDDATE"].ToString();
                            sb.Replace(":MODI_DTM", "to_date(" + OracleDBUtil.SqlStr(sDate) + ",'YYYYMMDD')");
                            OracleDBUtil.ExecuteSql(objTX, sb.ToString());
                        }

                        //////WEB TEMP TABLE > WEB TARGET TABLE
                        OracleParameter O_SUC_COUNT = new OracleParameter();
                        O_SUC_COUNT.OracleType = OracleType.VarChar;
                        O_SUC_COUNT.ParameterName = "outMessage";
                        O_SUC_COUNT.Size = 2000;
                        O_SUC_COUNT.Direction = ParameterDirection.Output;
                        Console.WriteLine("執行SP_PRODUCT_IMPORT");
                        OracleDBUtil.ExecuteSql_SP(
                            objTX,
                            "SP_PRODUCT_IMPORT",
                            O_SUC_COUNT
                            );

                        objTX.Commit();

                        con_log.Success(O_SUC_COUNT.Value.ToString());
                        ////成功資訊
                        Console.WriteLine("執行成功");
                       
                    }
                    else
                    {
                        Console.WriteLine("PRODUCT_IMPORT(POS)無資料");
                        con_log.Success("PRODUCT_IMPORT(POS)無資料");
                    }

                    Console.WriteLine("執行成功寫入log");
                }
                catch (Exception ex)
                {
                    //失敗資訊
                    objTX.Rollback();
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

        public static DataTable Query_OLD_Product(OracleConnection con)
        {
            try
            {
                //回寫狀態STATUS='T'
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Length = 0;
                sb.Append("SELECT * ");
                sb.Append("FROM PRODUCT ");
                sb.Append("WHERE (LENGTH(PRODNO)=10 OR LENGTH(PRODNO)=9)  AND COMPANYCODE in ('01','02') ");

                DataTable dt = OracleDBUtil.GetDataSet(con, sb.ToString()).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region 備份20110117
        //static void Main(string[] args)
        //{
        //    try {
        //        Console.WriteLine("初始化LOG");
        //        //初始化LOG
        //        ConvertLog con_log = new ConvertLog("PRODUCT_IMPORT");

        //        OracleConnection conn = OracleDBUtil.GetConnection();
        //        OracleTransaction objTX = objTX = conn.BeginTransaction();

        //        try
        //        {
        //            //**001CLEAR WEBPOS.PRODUCT_TEMP
        //            //**002FETDB01T.PRODUCT -> WEBPOS.PRODUCT_TEMP
        //            //***002-1WEBPOS.DISCOUNT_MASTER -> WEBPOS.PRODUCT_TEMP
        //            Console.WriteLine("WEBPOS.DISCOUNT_MASTER -> WEBPOS.PRODUCT_TEMPLOG");
        //            int row_count = FETDB01T_PRODUCT_WEBPOS_PRODUCT_TEMP();

        //            //**003比對 WEBPOS.PRODUCT_TEMP WEBPOS.PRODUCT
        //            //ProductTemp2Product();

        //            OracleParameter O_SUC_COUNT = new OracleParameter();
        //            O_SUC_COUNT.OracleType = OracleType.Number;
        //            O_SUC_COUNT.ParameterName = "O_SUC_COUNT";
        //            O_SUC_COUNT.Direction = ParameterDirection.Output;
        //            Console.WriteLine("執行SP_PRODUCT_IMPORT");
        //            OracleDBUtil.ExecuteSql_SP(
        //                objTX,
        //                "SP_PRODUCT_IMPORT",
        //                O_SUC_COUNT
        //                );

        //            objTX.Commit();

        //            con_log.Success("來源筆數:" + row_count.ToString() + ",更新筆數:" + O_SUC_COUNT.Value.ToString());
        //            Console.WriteLine("執行成功" + "來源筆數:" + row_count.ToString() + ",更新筆數:" + O_SUC_COUNT.Value.ToString());
        //        }
        //        catch (Exception ex)
        //        {
        //            //objTX.Rollback();
        //            con_log.Fail(ex.Message);
        //            throw ex;
        //        }
        //        finally
        //        {
        //            if (conn.State == ConnectionState.Open) conn.Close();
        //            conn.Dispose();
        //            OracleConnection.ClearAllPools();
        //        }
        //    }
        //    catch (Exception ex) {
        //        Console.WriteLine(ex.Message);
        //        Console.ReadKey(true);
            
        //    }
            
        //}

        ////**002FETDB01T.PRODUCT -> WEBPOS.PRODUCT_TEMP
        //static int FETDB01T_PRODUCT_WEBPOS_PRODUCT_TEMP() 
        //{
        //    int row_count = 0;

        //    APP01_Facade cApp01 = new APP01_Facade();
        //    APP01_ProductImport dsTEMP = new APP01_ProductImport();
        //    try 
        //    {
        //        //Clear PRODUCT_TEMP
        //        DataTable dtTmp = cApp01.Query_One_Product_TEMP();
        //        dtTmp.TableName = "PRODUCT_TEMP";
        //        OracleDBUtil.DELETE(dtTmp, "");
        //        dtTmp.Dispose();

        //        //**002FETDB01T.PRODUCT -> WEBPOS.PRODUCT_TEMP
        //        Dictionary<string, string> dcProduct = new Dictionary<string, string>();
        //        dcProduct.Add("ACCOUNTCODE", "ACCOUNTCODE");
        //        dcProduct.Add("ATTRIBUTE1", "ERP_ATTRIBUTE_1");
        //        dcProduct.Add("ATTRIBUTE2", "ERP_ATTRIBUTE_2");
        //        dcProduct.Add("BARCODE1", "BARCODE1");
        //        dcProduct.Add("BARCODE2", "BARCODE2");
        //        dcProduct.Add("BARCODE3", "BARCODE3");
        //        dcProduct.Add("BARCODE4", "BARCODE4");
        //        dcProduct.Add("CEASEDATE", "CEASEDATE");
        //        dcProduct.Add("COMPANYCODE", "COMPANYCODE");
        //        dcProduct.Add("COST", "COST");
        //        //dcProduct.Add("IMEI_FLAG", "IMEI_FLAG");
        //        dcProduct.Add("ISCONSIGNMENT", "ISCONSIGNMENT");
        //        dcProduct.Add("ISKEY", "ISKEY");
        //        dcProduct.Add("ISSTOCK", "ISSTOCK");
        //        dcProduct.Add("PRICE", "PRICE");
        //        dcProduct.Add("PRICEDATE", "PRICEDATE");
        //        dcProduct.Add("PRODNAME", "PRODNAME");
        //        dcProduct.Add("PRODNO", "PRODNO");
        //        dcProduct.Add("PRODTYPENO", "PRODTYPENO");
        //        dcProduct.Add("STATUS", "STATUS");
        //        dcProduct.Add("TOSO", "TOSO");
        //        dcProduct.Add("ULSNO", "ULSNO");
        //        dcProduct.Add("UNIT", "UNIT");
        //        dcProduct.Add("COSTDATE", "COSTDATE");

        //        DataTable dtOldProduct = cApp01.Query_OLD_Product();


        //        APP01_ProductImport.PRODUCT_TEMPDataTable dtTEMP = dsTEMP.PRODUCT_TEMP;
        //        foreach (DataColumn dc in dtTEMP.Columns)
        //            dc.AllowDBNull = true;

        //        for (int i = 0; i < dtOldProduct.Rows.Count; i++)
        //        {
        //            row_count++;

        //            APP01_ProductImport.PRODUCT_TEMPRow drTEMP = dtTEMP.NewPRODUCT_TEMPRow();
        //            DataRow drProduct = dtOldProduct.Rows[i];
        //            //common
        //            foreach (KeyValuePair<string, string> pair in dcProduct)
        //            {
        //                if (pair.Key == "PRODNO")
        //                {
        //                    drTEMP[pair.Value] = OracleDBUtil.SqlStr(drProduct[pair.Key].ToString()).Replace("'", "");
        //                }
        //                else if (pair.Key == "STATUS") 
        //                {
        //                    drTEMP[pair.Value] = "0";
        //                }
        //                else
        //                {
        //                    drTEMP[pair.Value] = drProduct[pair.Key];
        //                }

        //            }

        //            //special
        //            string sDate = drProduct["CREATEDATE"].ToString();
        //            drTEMP["S_Date"] = Convert.ToDateTime(sDate.Substring(0, 4) + "/" + sDate.Substring(4, 2) + "/" + sDate.Substring(6, 2));
        //            sDate = drProduct["CEASEDATE"].ToString();
        //            drTEMP["E_DATE"] = Convert.ToDateTime(sDate.Substring(0, 4) + "/" + sDate.Substring(4, 2) + "/" + sDate.Substring(6, 2));
        //            drTEMP["CREATE_USER"] = "SYSTEM";
        //            drTEMP["MODI_USER"] = "SYSTEM";
        //            drTEMP["IS_DISCOUNT"] = "N";// default N
        //            sDate = drProduct["UPDDATE"].ToString();
        //            drTEMP["MODI_DTM"] = Convert.ToDateTime(sDate.Substring(0, 4) + "/" + sDate.Substring(4, 2) + "/" + sDate.Substring(6, 2)); 
        //            dtTEMP.Rows.Add(drTEMP);
        //        }
        //        dtTEMP.AcceptChanges();

                
        //        ////WEBPOS.DISCOUNT_MASTER -> WEBPOS.PRODUCT_TEMP
        //        //DataTable dtDiscount = cApp01.Query_Discount_Master_Today_Yesterday();
        //        //for (int i = 0; i < dtDiscount.Rows.Count; i++)
        //        //{
        //        //    row_count++;

        //        //    APP01_ProductImport.PRODUCT_TEMPRow drTEMP = dtTEMP.NewPRODUCT_TEMPRow();
        //        //    drTEMP["PRODNO"] = dtDiscount.Rows[i]["DISCOUNT_CODE"];
        //        //    drTEMP["PRODNAME"] = dtDiscount.Rows[i]["DISCOUNT_NAME"];
        //        //    drTEMP["CREATE_USER"] = dtDiscount.Rows[i]["CREATE_USER"];
        //        //    drTEMP["CREATE_DTM"] = dtDiscount.Rows[i]["CREATE_DTM"];
        //        //    drTEMP["MODI_USER"] = dtDiscount.Rows[i]["MODI_USER"];
        //        //    drTEMP["MODI_DTM"] = dtDiscount.Rows[i]["MODI_DTM"];
        //        //    drTEMP["S_DATE"] = dtDiscount.Rows[i]["S_DATE"];
        //        //    drTEMP["E_DATE"] = dtDiscount.Rows[i]["E_DATE"];

        //        //    if (dtDiscount.Rows[i]["E_DATE"] == DBNull.Value)
        //        //    {
        //        //        drTEMP["CEASEDATE"] = "99991231";
        //        //    }
        //        //    else
        //        //    {
        //        //        drTEMP["CEASEDATE"] = ((DateTime)dtDiscount.Rows[i]["E_DATE"]).ToString("yyyyMMdd");
        //        //    }

        //        //    drTEMP["ACCOUNTCODE"] = dtDiscount.Rows[i]["ACCOUNT_CODE"];
        //        //    drTEMP["DEL_FLAG"] = dtDiscount.Rows[i]["DEL_FLAG"];
        //        //    drTEMP["STATUS"] = "0";
        //        //    drTEMP["IS_DISCOUNT"] = "Y";
        //        //    dtTEMP.Rows.Add(drTEMP);
        //        //}
        //        //dtTEMP.AcceptChanges();

        //        //Insert WEBPOS.PRODUCT_TEMP
        //        cApp01.Add_PRODUCT_TEMP(dsTEMP);

        //    }
        //    catch (Exception ex) 
        //    {
        //        throw ex;
        //    }

        //    return row_count;
        //}
        #endregion
    }
}

