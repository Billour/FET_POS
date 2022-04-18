using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data.OracleClient;
using System.Data;

namespace FET.POS.Model.Common
{
    public class PRODUCT_PageHelper
    {
        /// <summary>
        /// 商品類別
        /// </summary>
        /// <param name="canEmpty">是否要顯示"ALL"選項</param>
        /// <returns></returns>
        public static DataTable GetProDTypeNo(bool canEmpty)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT DISTINCT                          ");
            strSql.Append("       PRODTYPENO AS PRODTYPE_NO,        ");
            strSql.Append("       PRODTYPENAME AS PRODTYPE_NAME     ");
            strSql.Append("  FROM PRODUCT_TYPE                      ");
            strSql.Append(" ORDER BY PRODTYPE_NO                    ");

            DataTable dt = OracleDBUtil.Query_Data(strSql.ToString());

            if (canEmpty)
            {
                DataRow dr = dt.NewRow();
                dr["PRODTYPE_NAME"] = "ALL";
                dr["PRODTYPE_NO"] = "";
                dt.Rows.InsertAt(dr, 0);
            }

            return dt;
        }

        /// <summary>
        /// 寄銷商品類別
        /// </summary>
        /// <param name="canEmpty">是否要顯示"ALL"選項</param>
        /// <returns></returns>
        public static DataTable GetCsmProDTypeNo(bool canEmpty)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT DISTINCT                          ");
            strSql.Append("       PRODTYPENO AS PRODTYPE_NO,        ");
            strSql.Append("       PRODTYPENAME AS PRODTYPE_NAME     ");
            strSql.Append("  FROM CSM_PRODUCT_TYPE                      ");
            strSql.Append(" ORDER BY PRODTYPE_NO                    ");

            DataTable dt = OracleDBUtil.Query_Data(strSql.ToString());

            if (canEmpty)
            {
                DataRow dr = dt.NewRow();
                dr["PRODTYPE_NAME"] = "ALL";
                dr["PRODTYPE_NO"] = "";
                dt.Rows.InsertAt(dr, 0);
            }

            return dt;
        }
        /// <summary>
        /// 加價購商品類別
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProDTypeNoForExtraSale(bool canEmpty, string ed_status)
        {
            OracleConnection conn = null;
            DataSet objDS = null;
            StringBuilder strSql = null;

            try
            {
                strSql = new StringBuilder();
                strSql.Append("SELECT DISTINCT                          ");
                strSql.Append("       PRODTYPENO AS PRODTYPE_NO,        ");
                strSql.Append("       PRODTYPENAME AS PRODTYPE_NAME     ");
                strSql.Append("  FROM PRODUCT_TYPE                      ");
                strSql.Append(" WHERE PRODTYPENO IN ('10','11','15','22','30','31','35','80') ");
                strSql.Append(" ORDER BY PRODTYPE_NO                    ");
                conn = OracleDBUtil.GetConnection();

                objDS = OracleDBUtil.GetDataSet(conn, strSql.ToString());

                if ((canEmpty))
                {
                    if (ed_status == "Select")
                    {
                        DataRow dr = objDS.Tables[0].NewRow();
                        dr["PRODTYPE_NAME"] = "ALL";
                        dr["PRODTYPE_NO"] = "";
                        objDS.Tables[0].Rows.InsertAt(dr, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                //Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                OracleConnection.ClearAllPools();
            }

            return objDS.Tables[0];
        }

        /// <summary>
        /// SIM卡商品商品類別
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProDTypeForSIMGroup(bool canEmpty, string ed_status)
        {
            OracleConnection conn = null;
            DataSet objDS = null;
            StringBuilder strSql = null;

            try
            {
                strSql = new StringBuilder();
                strSql.Append("SELECT DISTINCT                          ");
                strSql.Append("       PRODTYPENO AS PRODTYPE_NO,        ");
                strSql.Append("       PRODTYPENAME AS PRODTYPE_NAME     ");
                strSql.Append("  FROM PRODUCT_TYPE                      ");
                strSql.Append(" WHERE PRODTYPENO IN ('20','21','25','26') ");
                strSql.Append(" ORDER BY PRODTYPE_NO                    ");
                conn = OracleDBUtil.GetConnection();

                objDS = OracleDBUtil.GetDataSet(conn, strSql.ToString());

                if ((canEmpty))
                {
                    if (ed_status == "Select")
                    {
                        DataRow dr = objDS.Tables[0].NewRow();
                        dr["PRODTYPE_NAME"] = "ALL";
                        dr["PRODTYPE_NO"] = "";
                        objDS.Tables[0].Rows.InsertAt(dr, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                //Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                OracleConnection.ClearAllPools();
            }

            return objDS.Tables[0];
        }

        /// <summary>
        /// 加價購商品類別
        /// </summary>
        /// <returns></returns>
        //public static DataTable GetProDTypeForSIMGroup(bool canEmpty, string ed_status)
        //{
        //    OracleConnection conn = null;
        //    DataSet objDS = null;
        //    StringBuilder strSql = null;

        //    try
        //    {
        //        strSql = new StringBuilder();
        //        strSql.Append("SELECT DISTINCT                          ");
        //        strSql.Append("       PRODTYPENO AS PRODTYPE_NO,        ");
        //        strSql.Append("       PRODTYPENAME AS PRODTYPE_NAME     ");
        //        strSql.Append("  FROM CSM_PRODUCT_TYPE                      ");
        //        strSql.Append(" WHERE PRODTYPENO IN ('20','21','25','26') ");
        //        strSql.Append(" ORDER BY PRODTYPE_NO                    ");
        //        conn = OracleDBUtil.GetConnection();

        //        objDS = OracleDBUtil.GetDataSet(conn, strSql.ToString());

        //        if ((canEmpty))
        //        {
        //            if (ed_status == "Select")
        //            {
        //                DataRow dr = objDS.Tables[0].NewRow();
        //                dr["PRODTYPE_NAME"] = "ALL";
        //                dr["PRODTYPE_NO"] = "";
        //                objDS.Tables[0].Rows.InsertAt(dr, 0);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Logger.Log.Error(ex.Message, ex);
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (conn.State == ConnectionState.Open) conn.Close();
        //        conn.Dispose();
        //        OracleConnection.ClearAllPools();
        //    }

        //    return objDS.Tables[0];
        //}

        /// <summary>
        /// 寄銷商品類別
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProDTypeForConsignmentSale(bool canEmpty, string ed_status)
        {
            OracleConnection conn = null;
            DataSet objDS = null;
            StringBuilder strSql = null;

            try
            {
                strSql = new StringBuilder();
                strSql.Append("SELECT DISTINCT                          ");
                strSql.Append("       PRODTYPENO AS PRODTYPE_NO,        ");
                strSql.Append("       PRODTYPENAME AS PRODTYPE_NAME     ");
                strSql.Append("  FROM CSM_PRODUCT_TYPE                      ");
                //strSql.Append(" WHERE PRODTYPENO IN ('20','21','25','26') ");
                strSql.Append(" ORDER BY PRODTYPE_NO                    ");
                conn = OracleDBUtil.GetConnection();

                objDS = OracleDBUtil.GetDataSet(conn, strSql.ToString());

                if ((canEmpty))
                {
                    if (ed_status == "Select")
                    {
                        DataRow dr = objDS.Tables[0].NewRow();
                        dr["PRODTYPE_NAME"] = "ALL";
                        dr["PRODTYPE_NO"] = "";
                        objDS.Tables[0].Rows.InsertAt(dr, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                //Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                OracleConnection.ClearAllPools();
            }

            return objDS.Tables[0];
        }
    }
}
