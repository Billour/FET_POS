using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OracleClient;
using System.Globalization;
using FET.POS.Model.Helper;
using FET.POS.Model.DTO;
using Advtek.Utility;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class CON08_Facade
    {
        private DateTime SysDate = DateTime.Now;

        /// <summary>
        /// </summary>
        /// <param name="sdt"></param>
        /// <param name="MODI_USER"></param>
        public static DataTable SP_CHECK_CONSIGNMENT_GOODS_PS(string I_BATCH_NO, string I_USER_ID, string I_FINC_ID)
        {
            DataTable O_DATA = new DataTable();
            using (OracleConnection oConn = OracleDBUtil.GetConnection())
            {
                OracleCommand oraCmd = new OracleCommand("SP_CHECK_CONSIGNMENT_GOODS_PS");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_BATCH_NO", OracleType.VarChar, 2000)).Value = I_BATCH_NO;
                oraCmd.Parameters.Add(new OracleParameter("I_USER_ID", OracleType.VarChar, 2000)).Value = I_USER_ID;
                oraCmd.Parameters.Add(new OracleParameter("I_FINC_ID", OracleType.VarChar, 2000)).Value = I_FINC_ID;
                oraCmd.Parameters.Add(new OracleParameter("O_DATA", OracleType.Cursor)).Direction = ParameterDirection.Output;
                oraCmd.Connection = oConn;
                oraCmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(oraCmd);
                da.Fill(O_DATA);
                return O_DATA;
            }

        }

        public void UpdateOne_UPLOAD(ORD09_DropShipment ds)
        {
            OracleDBUtil.UPDDATEByUUID(ds.Tables["UPLOAD_TEMP"], "F8");
        }

        public void Insert_CSM_DISTRIBUTES_ORDER_M(DataTable sdt, string MODI_USER, string strSTNO)
        {
            if (sdt.Rows.Count > 0)//如果有資料才新增
            {
                OracleConnection objConn = null;
                OracleTransaction objTX = null;
                try
                {
                    objConn = OracleDBUtil.GetConnection();
                    objTX = objConn.BeginTransaction();
                    foreach (DataRow dr in sdt.Rows)
                    {
                        StringBuilder sbD = new StringBuilder();
                        sbD.Append("Insert INTO CSM_DIREC_ORDER_M VALUES(");
                        sbD.Append(OracleDBUtil.SqlStr(Advtek.Utility.GuidNo.getUUID().ToString()) + ",");
                        sbD.Append(OracleDBUtil.SqlStr(dr["PRODNO"].ToString()) + ",");
                        sbD.Append(OracleDBUtil.SqlStr(strSTNO) + ",");
                        sbD.Append(dr["DIS_QTY"] + ",");
                        sbD.Append(OracleDBUtil.SqlStr(dr["ERR_DESC"].ToString()) + ",");
                        sbD.Append("SYSDATE,");
                        sbD.Append("SYSDATE,");
                        sbD.Append(OracleDBUtil.SqlStr(MODI_USER) + ",");
                        sbD.Append("'S',");
                        sbD.Append(OracleDBUtil.SqlStr(MODI_USER) + ",");
                        sbD.Append("SYSDATE,");
                        sbD.Append(OracleDBUtil.SqlStr(MODI_USER) + ",");
                        sbD.Append("SYSDATE,");
                        sbD.Append(OracleDBUtil.SqlStr(dr["STORENO"].ToString()) + ",");
                        sbD.Append(OracleDBUtil.SqlStr(dr["SUPP_ID"].ToString()) + ",");
                        sbD.Append("'',");
                        sbD.Append("'')");
                        OracleDBUtil.ExecuteSql(objTX, sbD.ToString());
                    }
                    objTX.Commit();
                }
                catch (Exception ex)
                {
                    objTX.Rollback();
                    throw ex;
                }
                finally
                {
                    objTX = null;

                    if (objConn.State == ConnectionState.Open) objConn.Close();
                    objConn.Dispose();
                    OracleConnection.ClearAllPools();
                }
            }
        }
    }
}
